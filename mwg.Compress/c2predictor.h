#pragma once
#include "c2arith_coder.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	//****************************************************************
	//	等確率出現予測器 / デバグ用
	//================================================================
	template<int N>
	class UniformPredictor{
		static const uint SUP_FREQ	=RangeCoderPref::SUP_FREQ;
	public:
		UniformPredictor(){}
		uint max_value(){return N;}
		void report(uint value){}
		uint operator()(uint value){
			return value*SUP_FREQ/N;
		}
		uint tune_get_freq(uint value){
			return 1;
		}
	};
	//****************************************************************
	//	範囲出現予測器
	//================================================================
	template<int NRan>
	class RangePredictor{
		static const uint B_FREQ	=RangeCoderPref::B_FREQ;
		static const uint SUP_FREQ	=RangeCoderPref::SUP_FREQ;

		uint count;
		uint cfreq[NRan];

		uint range_w[NRan];
		uint range_s[NRan+1];

		uint freq[NRan];
		uint cuml[NRan+1];

#ifndef NDEBUG
//		uint tfreq[NRan];
#endif
	public:
		RangePredictor(){}
		void init(uint* range_s){
			this->count=0;
			for(int i=0;i<NRan;i++){
				this->range_s[i]=range_s[i];
				this->range_w[i]=range_s[i+1]-range_s[i];
				this->cfreq[i]=range_w[i];
			}
			this->range_s[NRan]=range_s[NRan];
			this->update_cuml();
			this->cuml[NRan]=SUP_FREQ;
		}
		uint max_value(){
			return range_s[NRan];
		}
		void report(uint value){
			uint i_l=which_range(value);
			cfreq[i_l]++;
			count++;
			mwg::break_assert(0<=i_l&&i_l<NRan);

			if(count<NRan*4||(count&0x3F)==0)
				this->update_cuml();
		}
		uint operator()(uint value){
			if(value==max_value())return SUP_FREQ;
			uint i=which_range(value);
			return cuml[i]+freq[i]*(value-range_s[i])/range_w[i];
		}
	private:
		uint which_range(uint value){
			// 二分探索
			uint i_l=0;
			uint i_u=NRan;
			while(i_l+1!=i_u){
				uint i_c=(i_l+i_u)>>1;
				(range_s[i_c]<=value?i_l:i_u)=i_c;
			}

			return i_l;
		}
	private:
		void update_cuml(){
			uint t=0;
			for(int i=0;i<NRan;i++){
				cuml[i]=t;
				t+=cfreq[i];
			}

			// 累積総数が多くなりすぎた時
			if(t>SUP_FREQ){
				mwg::break_assert(max_value()<SUP_FREQ);
				for(int i=0;i<NRan;i++){
					cfreq[i]>>=1;
					if(cfreq[i]<range_w[i])cfreq[i]=range_w[i];
				}
				this->update_cuml();
				return;
			}

			for(int i=0;i<NRan;i++){
				cuml[i]=(cuml[i]<<B_FREQ)/t;
				freq[i]=(cfreq[i]<<B_FREQ)/t;
			}
		}
	//================================================================
	//	デバグ・調整用
	//================================================================
	public:
		uint tune_get_freq(uint value){
			return cfreq[value];
		}
		void dbg_print(){
			std::cout<<"range: [0, "<<max_value()<<")"<<std::endl;
			std::cout<<"ranges: "<<NRan<<" [";
			for(int i=0;i<NRan;i++){
				std::cout<<range_s[i]<<", ";
			}
			std::cout<<"]"<<std::endl;

			std::cout<<"freq-table: ";
			double h=0;
			for(int i=0;i<NRan;i++){
				double p=(cuml[i+1]-cuml[i])/(range_w[i]*(double)SUP_FREQ);
				h-=range_w[i]*p*std::log(p)/std::log(2.0);

				//std::cout<<cfreq[i]<<" ";
				std::cout<<cuml[i+1]-cuml[i]<<" ";
			}
			double uni_h=std::log((double)max_value())/std::log(2.0);
			std::cout<<std::endl;
			std::cout<<"Entropy: "<<h<<" bit ("<<(100*h/uni_h)<<"%)"<<std::endl;

			std::cout<<"Size: "<<count<<" times; "<<int(h*count/8)<<" bytes"<<std::endl;
		}
	};
	//****************************************************************
	//	ビン出現予測器
	//================================================================
	//	予備クラス群
	//----------------------------------------------------------------
	template<int N> struct NBinDefault{
		static const int value=N>16?16:N;
	};
	//----------------------------------------------------------------
	//	0 以上 NSup 未満の整数値の出現予測を行います。
	//----------------------------------------------------------------
	template<int NSup,int NBin=NBinDefault<NSup>::value>
	class BinPredictor{
		static const uint SUP_FREQ	=RangeCoderPref::SUP_FREQ;
		static const uint B_FREQ	=RangeCoderPref::B_FREQ;
		static const uint BIN_WIDTH	=NSup/NBin+(NSup%NBin?1:0); // 切り上げ

		uint cfreq[NBin];
		uint count;

		uint freq[NBin];
		uint cuml[NBin+1];
	public:
		uint max_value(){return NSup;}
		BinPredictor(){
			this->count=0;
			for(int i=0;i<NBin;i++)
				cfreq[i]=1;

			this->update_cuml();
			cuml[NBin]=SUP_FREQ;
		}
		void report(uint value){
			int ibin=value/BIN_WIDTH;
			cfreq[ibin]++;
			count++;
			mwg::break_assert(0<=ibin&&ibin<NBin);

			if(count<NBin*4||(count&0x3F)==0)
				this->update_cuml();
		}
		uint operator()(uint value){
			if(value==NSup)return SUP_FREQ;
			int ibin=value/BIN_WIDTH;
			return cuml[ibin]+freq[ibin]*(value%BIN_WIDTH);
		}
	private:
		void update_cuml(){
			uint t=0;
			for(int i=0;i<NBin;i++){
				cuml[i]=t;
				t+=cfreq[i]*BIN_WIDTH;
			}
			// 最後の bin だけ幅が小さいので総数を修正
			t-=(BIN_WIDTH*NBin-NSup)*cfreq[NBin-1];

			// 累積総数が多くなりすぎた時
			if(t>SUP_FREQ){
				mwg::break_assert(NBin*BIN_WIDTH<SUP_FREQ);
				for(int i=0;i<NBin;i++){
					cfreq[i]>>=1;
					if(cfreq[i]==0)cfreq[i]=1;
				}
				this->update_cuml();
				return;
			}

			for(int i=0;i<NBin;i++){
				cuml[i]=(cuml[i]<<B_FREQ)/t;
				freq[i]=(cfreq[i]<<B_FREQ)/t; // update 時の頻度を記録
			}
		}
	//================================================================
	//	デバグ・調整用
	//================================================================
	public:
		uint tune_get_freq(uint value){
			return cfreq[value];
		}
		void dbg_print(){
			std::cout<<"range: [0, "<<NSup<<")"<<std::endl;
			std::cout<<"bins: "<<NBin<<" * [width "<<BIN_WIDTH<<"]"<<std::endl;

			std::cout<<"freq-table: ";
			double h=0;
			for(int i=0;i<NBin;i++){
				double p=(cuml[i+1]-cuml[i])/(BIN_WIDTH*(double)SUP_FREQ);
				h-=BIN_WIDTH*p*std::log(p)/std::log(2.0);

				std::cout<<cfreq[i]<<" ";
			}
			double uni_h=std::log((double)NSup)/std::log(2.0);
			std::cout<<std::endl;
			std::cout<<"Entropy: "<<h<<" bit ("<<(100*h/uni_h)<<"%)"<<std::endl;
			std::cout<<"Size: "<<count<<" times; "<<int(h*count/8)<<" bytes"<<std::endl;
		}
	};
	//****************************************************************
	//	Nibble 出現予測器
	//================================================================
	class NibblePredictor{
		static const uint SUP_FREQ	=RangeCoderPref::SUP_FREQ;
		uint count;
		uint freq[16];
		uint ofreq[16];
		uint cuml[17];

		int upd_mask;
	public:
		NibblePredictor():upd_mask(0){
			this->count=0;
			for(int i=0;i<16;i++){
				freq[i]=0;
				ofreq[i]=1;
			}
			this->update_cuml();
			cuml[16]=SUP_FREQ;
		}
	public:
		uint max_value(){return 16;}
		uint operator()(uint value){
			return cuml[value];
		}
		void report(byte nib){
			freq[nib&0xF]++;
			count++;

			/*
			if((count&upd_mask)==0){
				this->update_cuml();
				if((count>>8)>upd_mask)
					upd_mask=(upd_mask<<2|0x3)&0x3FF;
			}
			/*/
			if(count<256||(count&0x3F)==0){
				this->update_cuml();
			}
			//*/

			if((count&0x3F)==0){
				this->attenuate();
			}
		}
	private:
		void update_cuml(){
			uint t=0;
			for(int i=0;i<16;i++){
				cuml[i]=t;
				t+=freq[i]+ofreq[i];
			}

			const uint t2=SUP_FREQ;
			for(int i=0;i<16;i++)
				cuml[i]=(cuml[i]*SUP_FREQ)/t;
		}
		/// <summary>
		/// 過去の情報を弱くします。
		/// </summary>
		void attenuate(){
			for(int i=0;i<16;i++){
				ofreq[i]-=ofreq[i]>>2; // >>7: 99.2% >>4: 93.8% >>2: 75%
				ofreq[i]+=freq[i];
				freq[i]=0;
			}
		}
	//================================================================
	//	デバグ・調整用
	//================================================================
	public:
		uint tune_get_freq(uint value){
			return freq[value]+ofreq[value];
		}
		void dbg_print(){
			const int NRan=16;
			std::cout<<"range: [0, "<<max_value()<<")"<<std::endl;
			std::cout<<"ranges: NibbleRangeCoder"<<std::endl;

			std::cout<<"freq-table: ";
			double h=0;
			for(int i=0;i<NRan;i++){
				double p=(cuml[i+1]-cuml[i])/((double)SUP_FREQ);
				h-=p*std::log(p)/std::log(2.0);

				//std::cout<<cfreq[i]<<" ";
				std::cout<<cuml[i+1]-cuml[i]<<" ";
			}
			double uni_h=std::log((double)max_value())/std::log(2.0);
			std::cout<<std::endl;
			std::cout<<"Entropy: "<<h<<" bit ("<<(100*h/uni_h)<<"%)"<<std::endl;

			std::cout<<"Size: "<<count<<" times; "<<int(h*count/8)<<" bytes"<<std::endl;
		}
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
