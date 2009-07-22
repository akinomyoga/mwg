#pragma once
#include "compress1.h"
#include "bitwr.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	typedef byte nibble;
	//****************************************************************
	//		算術圧縮の形式
	//================================================================
	//	データ: <bit 列> <padding-bit> <byte:終了マーカー> EOF
	//	終了マーカー : (EOF の直前に配する)
	//		圧縮前のバイト数の下 8bit
	//		→これによって、厳密に何バイト読み取れば良いか
	//		　(== padding-bit としてどれだけ無視するべきか) が分かる
	//
	//----------------------------------------------------------------
	//	案: 初めの内は nibble 単位 / その内に byte 単位
	//	案: 頻度表に前回の値との相関を取り入れる
	//		更に、前々回の値との相関や三体相関などを考える。
	//----------------------------------------------------------------
	//****************************************************************
	//		4bit 単位算術圧縮定数
	//================================================================
	class NibbleRC{
	private:
		NibbleRC();
		NibbleRC(const NibbleRC&);
	public:
		static const int B_SEND		=4; // 出力単位
		static const int B_RANGE	=B_SEND*5;
		static const int B_FREQ		=32-B_RANGE;
		static const int SZ_RANGE	=1<<B_RANGE;
		//static const int MSB_RANGE	=SZ_RANGE>>1;
		static const int MSK_RANGE	=SZ_RANGE-1;
		static const int UNI_SEND	=SZ_RANGE>>B_SEND;
		static const int MSK_SEND	=SZ_RANGE-UNI_SEND;
		static const int MSK_CUTOFF	=UNI_SEND-1;
		//static_assert(B_FREQ+B_SEND+B_FREQ<32);
		//static_assert(B_RANGE%B_SEND==0);

		//            <- B_FREQ ->|<-------- B_RANGE -------------->|
		//                        |<- B_SEND ->|
		// r_s       :cccccccccccc ssssssssssss xxxxxxxxxxxxxxxxxxxx
		// r_l       :aaaaaaaaaaaa llllllllllll zzzzzzzzzzzzzzzzzzzz
		//   cccc    : carry trap
		//   ssss    : 送られるデータの場所
		//   aaaa    : freq 掛け算用の余裕
		//   llll    : 非零になる様に保つ所
		//
		// UNI_SEND  :------------ -----------1 00000000000000000000
		// MSK_SEND  :------------ 111111111111 --------------------
		// MSK_RANGE :------------ 111111111111 11111111111111111111
		// MSK_CUTOFF:------------ ------------ 11111111111111111111
	};
	//****************************************************************
	//		4bit 単位頻度表
	//================================================================
	class NibbleFrequency{
	public:
		static const int MSK_RANGE	=NibbleRC::MSK_RANGE;
		static const int MAX_CUML_W	=NibbleRC::B_FREQ;
	private:
		static const int B_UPD0		=4; // 16B
		static const int SZ_UPD0	=1<<B_UPD0;
		static const int MSK_UPD0	=SZ_UPD0-1;

		static const int B_UPD1		=9; // 512B
		static const int SZ_UPD1	=1<<B_UPD1;
		static const int MSK_UPD1	=SZ_UPD1-1;

		int count;
		uint data[16];
		uint data2[16];
	public:
		//uint cuml_w;
		static const uint cuml_w=MAX_CUML_W;
		uint cuml[17];
	public:
		NibbleFrequency():count(0){
			for(int i=0;i<16;i++){
				data2[i]=1;
				data[i]=0;
			}
			this->update_freq();
		}
		void report(byte nib){
			data[nib&0xF]++;
			count++;

			//this->update_freq();
			if(count<256||(count&0xF)==0){
				this->update_freq();
			}
			//if(count<SZ_UPD1&&(count++&MSK_UPD0)==0){
			//	this->update_freq();
			//}

			//*
			if((count&0x3F)==0){
				for(int i=0;i<16;i++){
					// 過去の情報は弱く
					data2[i]-=data2[i]>>2; // >>7: 99.2% >>4: 93.8% >>2: 75%
					data2[i]+=data[i];
					data[i]=0;
				}
			}
			//*/
		}

		void update_freq(){
			// 累積値の計算
			uint t=0;
			for(int i=0;i<16;i++){
				cuml[i]=t;
				t+=data[i]+data2[i];
			}

			// 幅固定
			// ・下手をすると MAX_CUML_W を越えてしまうし、
			// ・何れにせよ double で演算するので出来るだけ大きい方が良い
			// cuml_w=MAX_CUML_W;
			uint t2=1<<cuml_w;
			/*
			// 幅決定
			cuml_w=8;
			uint t2=1<<cuml_w;
			while(t2<t){
				t2<<=1;
				cuml_w++;
			}
			//*/

			// 較正・表の完成
			double f=t2/(double)t;
			for(int i=0;i<16;i++)cuml[i]=uint(cuml[i]*f);
			cuml[16]=t2;
		}
	//================================================================
	//	探索
	//================================================================
	public:
		void select_range(byte nib,uint& r_s,uint& r_l){
			nib&=0xF;
			int freq=cuml[nib+1]-cuml[nib];
			r_s+=r_l*cuml[nib]>>cuml_w;
			r_l=r_l*freq>>cuml_w;

			mwg::break_assert(freq>0&&0<r_l);
		}
		nibble search_range(uint& r,uint& r_s,uint& r_l){
			// *** OPTIMIZED ***
			// 二分探索 in [nib_l, nib_u)
			nibble nib_l=0;
			nibble nib_u=16;
			while(nib_l+1!=nib_u){
				nibble nib_c=(nib_l+nib_u)>>1;
				if(r_l*cuml[nib_c]>>cuml_w<=r)
					nib_l=nib_c;
				else
					nib_u=nib_c;
			}

			int freq=cuml[nib_u]-cuml[nib_l];
			uint dr_s=r_l*cuml[nib_l]>>cuml_w;
			r_s+=dr_s;
			r_l=r_l*freq>>cuml_w;
			r-=dr_s;

			mwg::break_assert(freq>0&&0<r_l);
			return nib_l;
		}
	};
	//****************************************************************
	//		4bit 単位算術圧縮器
	//================================================================
	class NibbleRangeEncoder:public mwg::Stream::InStream{
		static const int B_SEND		=NibbleRC::B_SEND;
		static const int B_RANGE	=NibbleRC::B_RANGE;
		static const int MSK_SEND	=NibbleRC::MSK_SEND;
		static const int UNI_SEND	=NibbleRC::UNI_SEND;
		static const int MSK_RANGE	=NibbleRC::MSK_RANGE;
		static const int MSK_CUTOFF	=NibbleRC::MSK_CUTOFF;

		InStream* istr;
		mwg::Stream::BufferStream buff;
		BitWriter bw;
		uint r_s; // start of range
		uint r_l; // length of range
		NibbleFrequency low;
		NibbleFrequency high;

		byte count;
		bool terminated;
	public:
		NibbleRangeEncoder(InStream& istr)
			:istr(&istr),buff(),bw(buff),r_s(0),r_l(MSK_RANGE)
			,terminated(false),count(0)
			,cache(0),carry(-1)
		{}
	//================================================================
	//	エンコード
	//================================================================
	private:
		/// B_SEND だけデータを送ります。
		uint cache;
		int carry;
		void write_cached(uint data){
			// 初回
			if(carry<0){
				cache=data;
				carry=0;
				return;
			}

			if(data==(1<<B_SEND)-1){
				carry++;
			}else{
				this->flush_cache();
				cache=data;
			}
		}
		void write_carry(){
			cache++;
			while(carry>0){
				bw.write_bits(cache,B_SEND);
				carry--;
				cache=0;
			}
		}
		void flush_cache(){
			bw.write_bits(cache,B_SEND);
			while(carry>0){
				bw.write_bits(-1,B_SEND);
				carry--;
			}
		}
	private:
		void flush(){
			mwg::break_assert(0<r_l);
			if(r_s&~MSK_RANGE){
				write_carry();
				r_s&=MSK_RANGE;
			}

			/* r_l が MSK_SEND に掛からない位小さい時 */
			while((r_l&MSK_SEND)==0){
				write_cached(r_s>>(B_RANGE-B_SEND));
				r_s=r_s<<B_SEND&MSK_RANGE;
				r_l=r_l<<B_SEND;//&MSK_RANGE;
			}

			mwg::break_assert(0<r_l&&r_s<=MSK_RANGE);
		}
		void add(byte value){
			count++; // 終了マーカー

			low.select_range(value,r_s,r_l);
			low.report(value); // 選択後に更新 (自分を記録するのに自分の情報は使えないので)
			flush();

			high.select_range(value>>4,r_s,r_l);
			high.report(value>>4);
			flush();
		}
		void terminate(){
			if(terminated)return;
			terminated=true;
			//---------------------------------------------
			// [r_s, r_s + r_l ) の数で有効数字が最小の物は
			//---------------------------------------------
			assert((r_l&MSK_SEND)!=0);
			// ⇒ MSK_CUTOFF <= r_l
			// ⇒ r_s < r_s + MSK_CUTOFF < r_s + r_l
			//---------------------------------------------
			// 実際に書き込まれる値は
			// α := r_s + MSK_CUTOFF として
			// β := α - (α&MSK_CUTOFF) である。
			// α - (α&MSK_CUTOFF) >= α - MSK_CUTOFF == r_s
			// ⇒ r_s <= β < r_s + r_l
			//---------------------------------------------
			r_s=r_s+MSK_CUTOFF;
			r_l=MSK_CUTOFF; // 一回だけ強制出力
			flush();
			this->flush_cache();
			bw.terminate();
			bw.write_byte(count);
		}
	//================================================================
	//	ストリーム
	//================================================================
	public:
		bool eos(){
			while(buff.empty()){
				if(istr->eos()){
					this->terminate();
					break;
				}
				add(istr->get_byte());
			}

			return buff.empty();
		}
		byte_n get_byte(){
			return eos()?-1: buff.read_byte();
		}
	};
	//****************************************************************
	//		4bit 単位算術伸張器
	//================================================================
	class NibbleRangeDecoder:public mwg::Stream::InStream{
		static const int B_SEND		=NibbleRC::B_SEND;
		static const int B_RANGE	=NibbleRC::B_RANGE;
		static const int MSK_SEND	=NibbleRC::MSK_SEND;
		static const int UNI_SEND	=NibbleRC::UNI_SEND;
		static const int MSK_RANGE	=NibbleRC::MSK_RANGE;
		static const int MSK_CUTOFF	=NibbleRC::MSK_CUTOFF;

		BitReader br;
		uint r_s; // start of range
		uint r_l; // length of range
		uint r;   // exact value from r_s
		byte count;
		NibbleFrequency low;
		NibbleFrequency high;

		// 最後のバイトだけ自分の為に取って置くストリーム
		class TrimEndStream:public InStream{
			InStream& istr;
			byte buff;
		public:
			TrimEndStream(InStream& istr)
				:istr(istr),buff(0)
			{
				if(istr.eos())return;
				buff=istr.get_byte();
			}
			byte_n get_byte(){
				if(istr.eos())return -1;

				byte ret=buff;
				buff=istr.get_byte();
				return ret;
			}
			bool eos(){
				return istr.eos();
			}
		public:
			byte last_byte(){
				return buff;
			}
		} te_str;
	public:
		NibbleRangeDecoder(InStream& istr)
			:r_s(0),r_l(MSK_RANGE)
			,te_str(istr),br(/* 空初期化 */)
			,r(0),count(0)
		{
			this->br=BitReader(te_str);
			for(int i=0;i<B_RANGE;i+=B_SEND){
				r=r<<B_SEND|br.read_bits(B_SEND,false);
			}
		}
	//================================================================
	//	デコード
	//================================================================
	private:
		void load(){
			/* r_l が MSK_SEND に掛からない位小さい時 */
			while((r_l&MSK_SEND)==0){
				r=r<<B_SEND|br.read_bits(B_SEND,false);
				//r&=MSK_RANGE;
				r_s=r_s<<B_SEND;//&MSK_RANGE;
				r_l=r_l<<B_SEND;//&MSK_RANGE;
			}
			mwg::break_assert(0<=r&&r<r_l);
		}
		byte read(){
//			static int i=0;i++;
			count++; // 終了マーカーと一致させる為
//			int f=i;

			nibble l=low.search_range(r,r_s,r_l);
			mwg::break_assert(0<=r&&r<r_l);
			low.report(l);
			load();

			nibble h=high.search_range(r,r_s,r_l);
			mwg::break_assert(0<=r&&r<r_l);
			high.report(h);
			load();

			return l|h<<4;
		}
	//================================================================
	//	ストリーム
	//================================================================
	public:
		bool eos(){
			return te_str.eos()&&count==te_str.last_byte();
		}
		byte_n get_byte(){
			if(this->eos())return -1;
			return read();
		}
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
