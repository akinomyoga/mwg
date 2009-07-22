#pragma once
#include "c2arith_coder.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	//****************************************************************
	//	バイト出現予測器
	//================================================================
	class BytePredictor{
		static const uint SUP_FREQ	=RangeCoderPref::SUP_FREQ;
		static const uint B_FREQ	=RangeCoderPref::B_FREQ;
		static const int N			=256;

		uint cfreq[N];
		uint count;

		uint cuml[N+1];
	public:
		uint max_value(){return N;}
		BytePredictor(){
			this->count=0;
			for(int i=0;i<N;i++)
				cfreq[i]=1;

			this->update_cuml();
			cuml[N]=SUP_FREQ;
		}
		void report(uint value){
			cfreq[value]++;
			count++;
			mwg::break_assert(0<=value&&value<N);

			if(count<1024&&(count&0xF)==0||(count&0x3FF)==0)
				this->update_cuml();
		}
		uint operator()(uint value){
			if(value==N)return SUP_FREQ;
			return cuml[value];
		}
	private:
		void update_cuml(){
			uint t=0;
			for(int i=0;i<N;i++){
				cuml[i]=t;
				t+=cfreq[i];
			}

			// 累積総数が多くなりすぎた時
			if(t>SUP_FREQ){
				for(int i=0;i<N;i++){
					cfreq[i]>>=1;
					if(cfreq[i]==0)cfreq[i]=1;
				}
				this->update_cuml();
				return;
			}

			for(int i=0;i<N;i++){
				cuml[i]=(cuml[i]<<B_FREQ)/t;
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
			std::cout<<"range: [0, 256) - BytePredictor"<<std::endl;
			std::cout<<"freq-table:\n";
			double h=0;
			for(int i=0;i<N;i++){
				double p=(cuml[i+1]-cuml[i])/(double)SUP_FREQ;
				h-=p*std::log(p)/std::log(2.0);

				std::cout<<cfreq[i]<<" ";
				if((i&0xF)==0xF)std::cout<<"\n";
			}
			double uni_h=8.0;
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
