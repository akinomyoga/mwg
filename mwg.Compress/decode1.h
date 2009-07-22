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
	//	デコード用クラス
	//================================================================
	class MZipDecoder:public mwg::Stream::InStream{
		mwg::Stream::BufferStream buff;
		BitReader br;
	public:
		MZipDecoder(InStream& istr)
			:icache(0),buff(),br(istr)
		{
			initial_cache(cache);
		}
	//----------------------------------------------------------------
	//		出力
	//----------------------------------------------------------------
	private:
		byte cache[SZ_CACHE];
		uint icache;//;=MASK_CACHE;	// 次に書き込まれる位置
		void write(byte value){
			buff.put_byte(value);

			cache[icache++]=value;
			icache&=MASK_CACHE;
		}
	//----------------------------------------------------------------
	//		デコーダ
	//----------------------------------------------------------------
	public:
		void next(){
			switch(br.read_bits(B_BLK_TYP)){
				case BLK_TYP_RAW: /* <ブロック RAW> */{
					uint jM=RAW_LEN_BASE+br.read_bits(B_RAW_LEN);
					for(uint j=0;j<jM;j++)
						write(br.read_byte()); /* OPT */
					break;
				}
				case BLK_TYP_REF: /* <ブロック REF> */{
					int jcache=icache-1-br.read_bits(B_REF_ADR);
					jcache=MASK_CACHE&(jcache+SZ_CACHE);
					uint len=REF_LEN_BASE+br.read_bits(B_REF_LEN);

					while(len--){
						write(cache[jcache++]);
						jcache&=MASK_CACHE;
					}
					break;
				}
				default:
					__assume(false);
			}		
		}
	//----------------------------------------------------------------
	//	ストリーム
	//----------------------------------------------------------------
	public:
		bool eos(){
			while(buff.empty()&&br.remains8bits()){
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
