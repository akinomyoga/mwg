#pragma once
#include "stream.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	//****************************************************************
	//	Nibble 単位の読み出し
	//----------------------------------------------------------------
	class NibbleReader{
		mwg::Stream::InStream* istr;
		byte_n b;//=0;
		int ib;//=0;
	public:
		NibbleReader():istr(nullptr),b(0),ib(0){}
		NibbleReader(mwg::Stream::InStream& istr):istr(&istr),b(0),ib(0){}
		byte read_nibble(){
			if(ib==0){
				b=istr->get_byte();
				if(b<0)return 0;
				ib=4;
				return b&0xF;
			}else{
				ib=0;
				return (b>>4)&0xF;
			}
		}
	public:
		/// <summary>
		///	読み掛けの bit は考えないで、ストリームの末端に達しているか否かを取得します。
		/// </summary>
		bool eos(){
			return istr->eos();
		}
	};
	//****************************************************************
	//	Nibble 単位の書き出し
	//----------------------------------------------------------------
	class NibbleWriter{
		mwg::Stream::OutStream& ostr;
		byte b;
		byte ib;
	public:
		NibbleWriter(mwg::Stream::OutStream& ostr)
			:b(0),ib(0),ostr(ostr){}
		~NibbleWriter(){
			this->terminate();
		}
	public:
		void write_nibble(byte value){
			if(ib==0){
				b=value&0xF;
				ib=4;
			}else{
				ostr.put_byte(b|value<<4);
				ib=0;
			}
		}
		void terminate(){
			if(ib)ostr.put_byte(b);
		}
		/// <summary>
		/// 未出力の nibble に関係なく 1B 出力を行います。
		/// </summary>
		void write_byte(byte value){
			ostr.put_byte(value);
		}
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
