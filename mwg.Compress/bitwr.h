#pragma once
#include "stream.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
#define LOWER_MASK(i)	((1<<i)-1)
	class BitReader{
		typedef mwg::Stream::InStream InStream;
		byte_n b;//=0;
		int ib;//=0;
		InStream* istr;
	private:
		/// <summary>
		///	�g�����O�ɃX�g���[������ bits ��ǂݎ��܂��B
		/// </summary>
		bool ensure_bits(){
			if(ib&7)return true;
			b=istr->get_byte();
			return b>=0; 
		}
	public:
		BitReader():istr(nullptr),b(0),ib(0){}
		BitReader(InStream& istr)
			:istr(&istr),b(0),ib(0)
		{}
		uint read_bits(int bits,bool throwOnEos=true){
			assert(1<=bits&&bits<=32);
			uint ret=0;
			int ir=0;
			
			// (1) **|----|----|--
			int ib_res=8-ib;
			if(ib_res<=bits){
				if(!this->ensure_bits())goto at_eos;
				ret|=b>>ib;

				ib=0;
				ir+=ib_res;
				bits-=ib_res;
			}

			// (2) --|****|****|--
			while(8<=bits){
				// �� (1) �����s�������݂̂����ɓ����

				if(!this->ensure_bits())goto at_eos;
				ret|=b<<ir;

				ib=0;
				ir+=8;
				bits-=8;
			}
		
			// (3) --|----|----|**
			if(0<bits){
				if(!this->ensure_bits())goto at_eos;
				ret|=(b>>ib&LOWER_MASK(bits))<<ir;
				ib+=bits;
			}
			return ret;
		at_eos:
			if(throwOnEos)
				mwg::break_throw<std::logic_error>("�\���������̖͂��[�ɒB���܂����B");
			return ret;
		}
	private:
		uint read_bits0(int bits,bool throwOnEos=true){
			assert(1<=bits&&bits<=32);

			/* OPT */
			// ����͉����� (��ł����ƍ�����
			
			uint ret=0;
			int ir=0;
			do{
				if((ib&=7)==0){
					b=istr->get_byte();
					if(b<0)goto end_of_stream;

					// 8bit �ȏ゠�鎞�͂܂Ƃ߂ĉ��Z
					while(bits>=8){
						ret|=b<<ir;
						ir+=8;
						bits-=8;
						if(bits==0)goto ret;

						b=istr->get_byte();
						if(b<0)goto end_of_stream;
					}
				}

				ret|=(b>>ib++&1)<<ir++;
			}while(--bits);
		ret:
			return ret;
		end_of_stream:
			if(throwOnEos)
				throw std::logic_error("�\���������̖͂��[�ɒB���܂����B");
			return ret;
		}
	public:
		byte_n read_byte(){
			return istr->get_byte();
		}
		bool is_end(){
			// �u���b�N�� 1B �ȏ�݂�Ƃ�������
			// ��ib>0 �̎��A���̃u���b�N�͑��݂��Ȃ�
			return istr->eos()&&b<0;
		}
		/// <summary>
		///	�c�� 8 bit �ȏ゠�邩�ۂ����擾���܂��B
		/// </summary>
		bool remains8bits(){
			return !istr->eos();//||b>=0&&ib==0;
		}
		/// <summary>
		///	�ǂ݊|���� bit �͍l���Ȃ��ŁA�X�g���[���̖��[�ɒB���Ă��邩�ۂ����擾���܂��B
		/// </summary>
		bool stream_eos(){
			return istr->eos();
		}
	};
	//****************************************************************
	//	���r���[�� bits �̏����o��
	//----------------------------------------------------------------
	class BitWriter{
		typedef mwg::Stream::OutStream OutStream;
		OutStream* ostr;
		byte b;
		byte ib;
		std::vector<byte> waiter;
	public:
		BitWriter(OutStream& ostr)
			:b(0),ib(0),ostr(&ostr){}
		~BitWriter(){
			this->terminate();
		}
	public:
		void write_bits(uint value,int bits){
			assert(1<=bits&&bits<=32);

			/*
			// OPT
			int i=0;
			while(i<bits){
				b|=(value>>i++&1)<<ib++;
				if((ib&7)==0){
					this->flush_bits();

					while(bits-i>=8){
						ostr->put_byte(byte(value>>i));
						i+=8;
					}
				}
			}
			/*/

			// === OPTIMIZED VERSION ===
			// (1) **|----|----|--
			int w=8-ib;
			if(w<=bits){
				// �S�Ă𖄂߂��鎞
				b|=(value&LOWER_MASK(w))<<ib;
				this->flush_bits();

				bits-=w;
				value>>=w;
			}

			// (2) --|****|****|--
			while(8<=bits){
				ostr->put_byte(value);
				bits-=8;
				value>>=8;
			}

			// (3) --|----|----|**
			if(bits>0){
				b|=(value&LOWER_MASK(bits))<<ib;
				ib=ib+bits&7;
			}
			//*/
		}
		void write_byte(byte value){
			// ���x bit ���������閘�҂�
			if((ib&7)==0){
				ostr->put_byte(value);
			}else{
				waiter.push_back(value);
			}
		}
		void terminate(){
			// �����o�͂��Ă��Ȃ������o��
			if(ib&7)flush_bits();
		}
	private:
		void flush_bits(){
			ostr->put_byte(b);
			if(!waiter.empty()){
				for(std::vector<byte>::iterator i=waiter.begin();i!=waiter.end();i++)
					ostr->put_byte(*i);
				waiter.clear();
			}
			b=0;
			ib=0;
		}
	};
#undef LOWER_MASK
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
