#pragma once
#include "bitwr.h"
#include "compress1.h"
#include "history1.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	//****************************************************************
	//	エンコード用クラス
	//================================================================
	class MZipEncoder:public mwg::Stream::InStream{
		HistoryTable table;
		InStream* istr;
		BitWriter bw;
		mwg::Stream::BufferStream buff;
	public:
		MZipEncoder(InStream& istr)
			:istr(&istr),buff(),bw(buff)
		{
			this->init();
#ifndef NDEBUG
			dbg_count_blkraw=0;
			dbg_count_blkref=0;
#endif
		}
#ifndef NDEBUG
		~MZipEncoder(){
			std::cout
				<<"**************************************************\n"
				<<"    MZipEncoder\n"
				<<"==================================================\n";
			std::cout
				<<"Settings:\n"
				<<"    RAW_LEN ∈ ["<<RAW_LEN_BASE<<", "<<RAW_LEN_SUP<<")\n"
				<<"    REF_LEN ∈ ["<<REF_LEN_BASE<<", "<<REF_LEN_SUP<<")\n"
				<<"    REF_ADR ∈ [0, 2^"<<B_REF_ADR<<")\n"
				;
			std::cout
				<<"Number Of Blocks:\n"
				<<"    BLK_REF = "<<dbg_count_blkref<<"\n"
				<<"    BLK_RAW = "<<dbg_count_blkraw<<std::endl;
		}
		uint dbg_count_blkraw;
		uint dbg_count_blkref;
#endif
	//----------------------------------------------------------------
	//	ブロック書きだし
	//----------------------------------------------------------------
	private:
		std::vector<byte> raw_data;
		void write_raw(byte value){
			if(raw_data.size()+1==RAW_LEN_SUP)flush_raw();
			raw_data.push_back(value);
			table.write_byte(value);
		}
		void flush_raw(){
			if(raw_data.empty())return;
#ifndef NDEBUG
			dbg_count_blkraw++;
#endif
			// データの書き出し
			bw.write_bits(BLK_TYP_RAW,B_BLK_TYP);
			bw.write_bits(raw_data.size()-RAW_LEN_BASE,B_RAW_LEN);
			for(std::vector<byte>::iterator i=raw_data.begin();i!=raw_data.end();i++)
				bw.write_byte(*i);

			raw_data.clear();
		}

		void write_ref(const HistoryTable::Reference& refer,match_cand_array& dat){
			flush_raw();
#ifndef NDEBUG
			dbg_count_blkref++;
#endif
			bw.write_bits(BLK_TYP_REF,B_BLK_TYP);
			bw.write_bits(refer.address(),B_REF_ADR);
			bw.write_bits(refer.length()-REF_LEN_BASE,B_REF_LEN);
			for(int i=0;i<refer.length();i++){
				table.write_byte((byte)dat.deq_push(istr->get_byte()));
			}
		}
	private:
		match_cand_array dat;
		void init(){
			for(int i=0;i<REF_LEN_SUP-1;i++)
				dat.push(istr->get_byte());
		}
		void next(){
			HistoryTable::Reference refer=table.match(dat);
			if(refer){
				write_ref(refer,dat);
				return;
			}

			write_raw((byte)dat.deq_push(istr->get_byte()));
		}
	//----------------------------------------------------------------
	//	ストリーム
	//----------------------------------------------------------------
	public:
		bool eos(){
			while(buff.empty()){
				if(dat.length()==0){
					flush_raw();
					bw.terminate();
					break;
				}

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
