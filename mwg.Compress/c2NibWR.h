#pragma once
#include "stream.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	//****************************************************************
	//	Nibble �P�ʂ̓ǂݏo��
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
		///	�ǂ݊|���� bit �͍l���Ȃ��ŁA�X�g���[���̖��[�ɒB���Ă��邩�ۂ����擾���܂��B
		/// </summary>
		bool eos(){
			return istr->eos();
		}
	};
	//****************************************************************
	//	Nibble �P�ʂ̏����o��
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
		/// ���o�͂� nibble �Ɋ֌W�Ȃ� 1B �o�͂��s���܂��B
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
