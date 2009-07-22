#pragma once
#include "bitwr.h"
#include "stream.h"
#include "ringbuf.h"
#include "c2compression.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	class MdkCoderBase:public mwg::Stream::InStream{
	protected:
		static const uint SUP_REFLEN	=MdkPref::SUP_REFLEN;
		static const uint MIN_REFLEN	=MdkPref::MIN_REFLEN;
		static const uint SUP_RAWLEN	=MdkPref::SUP_RAWLEN;
		static const uint MIN_RAWLEN	=MdkPref::MIN_RAWLEN;

		static const uint SUP_REFADR	=MdkPref::SUP_REFADR;
		static const uint SUP_REFADR_L	=MdkPref::SUP_REFADR_L;
		static const uint SUP_REFADR_H	=MdkPref::SUP_REFADR_H;
		static const uint MSK_REFADR_L	=MdkPref::MSK_REFADR_L;
		static const uint B_REFADR_L	=MdkPref::B_REFADR_L;

		static const uint PP_NRAN_RAWLEN	=MdkPref::PP_NRAN_RAWLEN;
		static const uint PP_NRAN_REFADR_L	=MdkPref::PP_NRAN_REFADR_L;
		static const uint PP_NRAN_REFADR_H	=MdkPref::PP_NRAN_REFADR_H;
		static const uint PP_NRAN_REFLEN	=MdkPref::PP_NRAN_REFLEN;
		typedef MdkPref::BlockType BlockType;
	protected:
		//UniformPredictor<BlockType::N>			pp_blktyp;
		BinPredictor<BlockType::N>			pp_blktyp;

		//UniformPredictor<SUP_RAWLEN-MIN_RAWLEN>	pp_rawlen;
		//BinPredictor<SUP_RAWLEN-MIN_RAWLEN>	pp_rawlen;
		RangePredictor<PP_NRAN_RAWLEN>		pp_rawlen;

		BytePredictor pp_raw;
		NibblePredictor pp_low;
		NibblePredictor pp_high;

#if		PPTYPE_REFLEN==BIN_PREDICTOR
		BinPredictor<SUP_REFLEN-MIN_REFLEN>	pp_reflen;
#elif	PPTYPE_REFLEN==RANGE_PREDICTOR
		RangePredictor<PP_NRAN_REFLEN>	pp_reflen;
#endif

#if		PPTYPE_REFADR==BIN_PREDICTOR
		BinPredictor<SUP_REFADR_L>			pp_refadr_l;
		BinPredictor<SUP_REFADR_H>			pp_refadr_h;
#elif	PPTYPE_REFADR==BYTE_PREDICTOR
		BytePredictor		pp_refadr_l;
		BytePredictor		pp_refadr_h;
#elif	PPTYPE_REFADR==RANGE_PREDICTOR
		BinPredictor<SUP_REFADR_L>			pp_refadr_l;
		RangePredictor<PP_NRAN_REFADR_H>	pp_refadr_h;
#endif
	protected:
		MdkCoderBase(){
			uint range_s[257]; // 充分に
			MdkPref::GetRangeData(range_s,PP_NRAN_RAWLEN,SUP_RAWLEN-MIN_RAWLEN);
			this->pp_rawlen.init(range_s);
#if		PPTYPE_REFLEN==RANGE_PREDICTOR
			MdkPref::GetRangeData(range_s,PP_NRAN_REFLEN,SUP_REFLEN-MIN_REFLEN);
			this->pp_reflen.init(range_s);
#endif
#if PPTYPE_REFADR==RANGE_PREDICTOR
			//MdkPref::GetRangeData(range_s,PP_NRAN_REFADR_L,SUP_REFADR_L);
			//this->pp_refadr_l.init(range_s);
			MdkPref::GetRangeData(range_s,PP_NRAN_REFADR_H,SUP_REFADR_H);
			this->pp_refadr_h.init(range_s);
#endif
		}
	};
	//****************************************************************
	//	"水銀" 圧縮器
	//================================================================
	class MdkEncoder:public MdkCoderBase{
		HistoryWindow window;
		mwg::Stream::InStream& istr;
		mwg::Stream::RingQueue target;
		RangeEncoder rc_enc;

	public:
		MdkEncoder(InStream& istr)
			:istr(istr),rc_enc(0)
		{
			this->window.Initialize(MdkPref::InitializeCache);
			for(int i=0;i<SUP_REFLEN-1;i++)
				target.push(istr.get_byte());
		}
#ifndef NDEBUG
		~MdkEncoder(){
			std::cout
				<<"**************************************************\n"
				<<"    MdkEncoder\n"
				<<"==================================================\n"
				<<"Settings:\n"
				<<"    RAWLEN ∈ ["<<MIN_RAWLEN<<", "<<SUP_RAWLEN<<")\n"
				<<"    REFLEN ∈ ["<<MIN_REFLEN<<", "<<SUP_REFLEN<<")\n"
				<<"    REFADR ∈ [0, "<<SUP_REFADR<<")\n"
				<<"==================================================\n";

			std::cout<<"* pp_blktyp"<<std::endl;
			pp_blktyp.dbg_print();
			std::cout
				<<"--------------------------------------------------\n";

			std::cout<<"* pp_rawlen"<<std::endl;
			pp_rawlen.dbg_print();
			std::cout<<std::endl;

			std::cout<<"* pp_raw"<<std::endl;
			pp_raw.dbg_print();
			std::cout<<std::endl;

			std::cout<<"* pp_low"<<std::endl;
			pp_low.dbg_print();
			std::cout<<std::endl;

			std::cout<<"* pp_high"<<std::endl;
			pp_high.dbg_print();
			std::cout
				<<"--------------------------------------------------\n";

			std::cout<<"* pp_reflen"<<std::endl;
			pp_reflen.dbg_print();
			std::cout<<std::endl;

			std::cout<<"* pp_refadr_l"<<std::endl;
			pp_refadr_l.dbg_print();
			std::cout<<std::endl;

			std::cout<<"* pp_refadr_h"<<std::endl;
			pp_refadr_h.dbg_print();
			std::cout
				<<"=================================================="
				<<std::endl;
		}
#endif
	//----------------------------------------------------------------
	//	ブロック書きだし
	//----------------------------------------------------------------
	private:
		std::vector<byte> raw_data;
		void write_raw(byte value){
			if(raw_data.size()+1==SUP_RAWLEN)flush_raw();
			raw_data.push_back(value);
			window.write_byte(value);
		}
		void flush_raw(){
			if(raw_data.empty())return;

			// データの書き出し
			rc_enc.push_value(BlockType::RawData,pp_blktyp);
			rc_enc.push_value(raw_data.size()-MIN_RAWLEN,pp_rawlen);
			for(std::vector<byte>::iterator i=raw_data.begin();i!=raw_data.end();i++){
				//rc_enc.push_value(*i&0xF,pp_low);
				//rc_enc.push_value(*i>>4,pp_high);
				rc_enc.push_value(*i,pp_raw);
			}

			raw_data.clear();
		}

		void write_ref(const HistoryWindow::Reference& refer){
			flush_raw();
			rc_enc.push_value(BlockType::Reference,pp_blktyp);
			this->write_refadr(refer.address);
			rc_enc.push_value(refer.length-MIN_REFLEN,pp_reflen);
			for(int i=0;i<refer.length;i++){
				window.write_byte((byte)target.slide(istr.get_byte()));
			}
		}

		void write_refadr(int address){
			rc_enc.push_value(address&MSK_REFADR_L	,pp_refadr_l);
			rc_enc.push_value(address>>B_REFADR_L	,pp_refadr_h);
			/*
			if(address<SUP_REFADR_THR1-1){
				rc_enc.push_value(address					,pp_refadr);
			}else{
				// 相似の仮定
				rc_enc.push_value(SUP_REFADR_THR1-1			,pp_refadr);
				rc_enc.push_value(address&MSK_REFADR_THR1	,pp_refadr);
				rc_enc.push_value(address>>B_REFADR_THR1	,pp_refadr);
			}
			//*/
		}

		void next(){
			HistoryWindow::Reference refer=window.match(target);
			if(refer){
				write_ref(refer);
				return;
			}

			write_raw((byte)target.slide(istr.get_byte()));
		}
	public:
		bool eos(){
			while(rc_enc.empty()){
				if(target.length()==0){
					flush_raw();
					rc_enc.push_value(BlockType::Control,pp_blktyp);
					rc_enc.terminate();
					break;
				}

				next();
			}

			return rc_enc.empty();
		}
		byte_n get_byte(){
			return eos()?-1: rc_enc.read_byte();
		}
	};
	//****************************************************************
	//	"水銀" 伸張器
	//================================================================
	class MdkDecoder:public MdkCoderBase{
		static const uint SZ_WINDOW		=MdkPref::SZ_WINDOW;
		static const uint MSK_WINDOW	=MdkPref::MSK_WINDOW;

		mwg::Stream::BufferStream buff;
		RangeDecoder rc_dec;
	public:
		MdkDecoder(InStream& istr)
			:rc_dec(istr),icache(0),buff()
		{
			MdkPref::InitializeCache(this->cache,SZ_WINDOW);
			this->terminated=false;
		}
	//----------------------------------------------------------------
	//	出力
	//----------------------------------------------------------------
	private:
		byte cache[SZ_WINDOW];
		uint icache; // 次に書き込まれる位置
		void write(byte value){
			buff.put_byte(value);

			cache[icache++]=value;
			icache&=MSK_WINDOW;
		}
	//----------------------------------------------------------------
	//	デコーダ
	//----------------------------------------------------------------
	public:
		bool terminated;
		void next(){
			switch(rc_dec.pop_data(pp_blktyp)){
				case BlockType::RawData: /* <ブロック RAW> */{
					uint jM=MIN_RAWLEN+rc_dec.pop_data(pp_rawlen);
					for(uint j=0;j<jM;j++)
						write(rc_dec.pop_data(pp_raw)); /* OPT */
					break;
				}
				case BlockType::Reference:{
					// 二つに分けるのは評価の順序を保証する為 ref bug#1
					uint adr_l=rc_dec.pop_data(pp_refadr_l);
					uint adr_h=rc_dec.pop_data(pp_refadr_h);
					int address=adr_l|adr_h<<B_REFADR_L;

					uint jcache=MSK_WINDOW&(icache-1-address+SZ_WINDOW);
					uint len=MIN_REFLEN+rc_dec.pop_data(pp_reflen);

					while(len--){
						write(cache[jcache++]);
						jcache&=MSK_WINDOW;
					}
					break;
				}
				case BlockType::Control:
					terminated=true;
					break;
				default:
					__assume(false);
			}
		}
	//----------------------------------------------------------------
	//	ストリーム
	//----------------------------------------------------------------
	public:
		bool eos(){
			while(buff.empty()&&!terminated){
				next();
			}

			return buff.empty();
		}
		byte_n get_byte(){
			return eos()?-1: buff.read_byte();
		}
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
