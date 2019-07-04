#pragma once
#include "c2NibWR.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	//****************************************************************
	//		�Z�p���k�� - �萔
	//================================================================
	//�@�o�͌`��
	//�@�@�f�[�^   : <bit ��> <padding-bit> # <byte:�I������> EOF
	//�@�@#        : �o�C�g���E���Ӗ�����
	//�@�@�I������ : EOF �̒��O�ɔz����Apush_value �񐔂Ɋւ�����
	//----------------------------------------------------------------
	//�@��push_value �ďo�񐔂ɑ�������f�[�^���Ō�Ɏ����Ă���̂́A
	//�@�@���͂����S�ɏI���������_�ł��̉񐔂�������ꍇ�ɑΉ�����ׂł���B
	//�@�@(�Ⴆ�΁A�V�[�N�s�\�ȃX�g���[���ɏ�������ł���ꍇ�A
	//�@�@�S�Ă��I�������Ɍďo�񐔂�擪�ɏ������ނƌ��������͏o���Ȃ��B)
	//----------------------------------------------------------------
	class RangeCoderPref{
	private:
		RangeCoderPref();
		RangeCoderPref(const RangeCoderPref&);
	public:
		static const int B_SEND		=4; // �o�͒P��
		static const int B_RANGE	=B_SEND*5;
		static const int B_FREQ		=32-B_RANGE;
		static const int SUP_FREQ	=1<<B_FREQ;

		static const int SZ_RANGE	=1<<B_RANGE;
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
		//   ssss    : ������f�[�^�̏ꏊ
		//   aaaa    : freq �|���Z�p�̗]�T
		//   llll    : ���ɂȂ�l�ɕۂ�
		//
		// UNI_SEND  :------------ -----------1 00000000000000000000
		// MSK_SEND  :------------ 111111111111 --------------------
		// MSK_RANGE :------------ 111111111111 11111111111111111111
		// MSK_CUTOFF:------------ ------------ 11111111111111111111
	};
#if CPP0X
	concept ProbabilityPredictor<typename T>{
�@		uint max_value();
�@		void report(uint val);
�@		uint operator()(uint val);
	};
#endif
	//****************************************************************
	//		�Z�p���k��
	//================================================================
	class RangeEncoder{
		static const int B_SEND		=RangeCoderPref::B_SEND;
		static const int B_RANGE	=RangeCoderPref::B_RANGE;
		static const int B_FREQ		=RangeCoderPref::B_FREQ;
		static const int MSK_SEND	=RangeCoderPref::MSK_SEND;
		static const int UNI_SEND	=RangeCoderPref::UNI_SEND;
		static const int MSK_RANGE	=RangeCoderPref::MSK_RANGE;
		static const int MSK_CUTOFF	=RangeCoderPref::MSK_CUTOFF;

		uint r_s; // start of range
		uint r_l; // length of range

		uint term_code;	// �I���}�[�J�[ : push/pop �̌ďo�� mod. 256
		byte term_code_len;
		bool terminated;
	public:
		RangeEncoder(int termCodeLength=0)
			:buff()
//			,bw(buff)
			,nw(buff)
			,cache(0),carry(-1)
			,r_s(0),r_l(MSK_RANGE)
			,terminated(false),term_code(0),term_code_len(termCodeLength)
		{
			assert(0<=termCodeLength&&termCodeLength<=4);
			assert(B_SEND==4);
		}
	//================================================================
	//	�f�[�^�̗���
	//	push_value �� C �� B �� A �� read_byte
	//================================================================
	//	read_byte ��
	//----------------------------------------------------------------
	public:
		bool empty(){
			return buff.empty();
		}
		byte_n read_byte(){
			return buff.empty()?-1: buff.read_byte();
		}
	//----------------------------------------------------------------
	//	A. bit �o�͋y�уo�b�t�@�����O
	//----------------------------------------------------------------
	private:
		mwg::Stream::BufferStream buff;
		NibbleWriter nw;
//		BitWriter bw;
	//----------------------------------------------------------------
	//	B. ���|���o���o�b�t�@�����O
	//----------------------------------------------------------------
	private:
		/// B_SEND �����f�[�^�𑗂�܂��B
		uint cache;
		int carry;
		void write_cached(uint data){
			// ����
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
				nw.write_nibble(cache);
//				bw.write_bits(cache,B_SEND);
				carry--;
				cache=0;
			}
		}
		void flush_cache(){
			nw.write_nibble(cache);
//			bw.write_bits(cache,B_SEND);
			while(carry>0){
				nw.write_nibble(-1);
//				bw.write_bits(-1,B_SEND);
				carry--;
			}
		}
	//----------------------------------------------------------------
	//	C. Range �I�� / bit �|���o��
	//----------------------------------------------------------------
	private:
		void flush(){
			mwg::break_assert(0<r_l);
			if(r_s&~MSK_RANGE){
				write_carry();
				r_s&=MSK_RANGE;
			}

			/* r_l �� MSK_SEND �Ɋ|����Ȃ��ʏ������� */
			while((r_l&MSK_SEND)==0){
				write_cached(r_s>>(B_RANGE-B_SEND));
				r_s=r_s<<B_SEND&MSK_RANGE;
				r_l=r_l<<B_SEND;//&MSK_RANGE;
			}

			mwg::break_assert(0<r_l&&r_s<=MSK_RANGE);
		}
	public:
		template<typename TPredictor>
		void push_value(uint value,TPredictor& pp){
			term_code++;

			// �͈͂̏�������
			int freq=pp(value+1)-pp(value);
			r_s += r_l* pp(value) >>B_FREQ;
			r_l  = r_l* freq      >>B_FREQ;
			mwg::break_assert(freq>0&&0<r_l);

			// �p�x�\�X�V
			pp.report(value);

			flush();
		}
		void terminate(){
			if(terminated)return;
			terminated=true;
			//---------------------------------------------
			// [r_s, r_s + r_l ) �̐��ŗL���������ŏ��̕���
			//---------------------------------------------
			assert((r_l&MSK_SEND)!=0);
			// �� MSK_CUTOFF <= r_l
			// �� r_s < r_s + MSK_CUTOFF < r_s + r_l
			//---------------------------------------------
			// ���ۂɏ������܂��l��
			// �� := r_s + MSK_CUTOFF �Ƃ���
			// �� := �� - (��&MSK_CUTOFF) �ł���B
			// �� - (��&MSK_CUTOFF) >= �� - MSK_CUTOFF == r_s
			// �� r_s <= �� < r_s + r_l ��
			//---------------------------------------------
			r_s=r_s+MSK_CUTOFF;
			r_l=MSK_CUTOFF; // ��񂾂������o��
			flush();
			this->flush_cache();
			nw.terminate();
//			bw.terminate();

			// �I�[�����o��
			while(term_code_len--){
				nw.write_byte(term_code);
//				bw.write_byte(term_code);
				term_code>>=8;
			}
		}
#if 0
	//================================================================
	//	�g�p��
	//================================================================
		static void sample(mwg::Stream::InStream& istr,mwg::Stream::OutStream& ostr){
			class BytePredictor{
			public:
				// �{���͂����ƌ���������� (���k����䢂̎����Ɍ������Ă���
				uint max_value(){return 256;}
				void report(uint value){}
				uint operator()(uint value){return value<<(B_FREQ-8);}
			} pp;

			RangeEncoder enc;
			while(!istr.eos()){
				enc.push_value(istr.get_byte(),pp);
				while(!enc.empty())ostr.put_byte(enc.read_byte());
			}

			enc.terminate();
			while(!enc.empty())ostr.put_byte(enc.read_byte());
		}
#endif
	};
	//****************************************************************
	//		�Z�p�L����
	//================================================================
	class RangeDecoder{
		static const int B_SEND		=RangeCoderPref::B_SEND;
		static const int B_RANGE	=RangeCoderPref::B_RANGE;
		static const int B_FREQ		=RangeCoderPref::B_FREQ;
		static const int MSK_SEND	=RangeCoderPref::MSK_SEND;
		static const int UNI_SEND	=RangeCoderPref::UNI_SEND;
		static const int MSK_RANGE	=RangeCoderPref::MSK_RANGE;
		static const int MSK_CUTOFF	=RangeCoderPref::MSK_CUTOFF;

		NibbleReader nr;
//		BitReader br;
//		uint r_s; // start of range
		uint r_l; // length of range
		uint r;   // exact value from r_s
		uint term_code;

	public:
		RangeDecoder(mwg::Stream::InStream& istr,int termCodeLength=0)
			:r_l(MSK_RANGE),r(0)
//			,r_s(0)
			,tc_reader(nullptr)
			,nr(/* �󏉊��� */)
//			,br(/* �󏉊��� */)
			,term_code(0)
		{
			// �I�[�����؂���
			assert(0<=termCodeLength&&termCodeLength<=4);
			if(termCodeLength>0){
				this->tc_reader=new TermCodeTrimmer(istr,termCodeLength);
				this->nr=NibbleReader(*this->tc_reader);
//				this->br=BitReader(*this->tc_reader);
			}else{
				this->nr=NibbleReader(istr);
//				this->br=BitReader(istr);
			}

			// �����Ǎ�
			for(int i=0;i<B_RANGE;i+=B_SEND){
				r=r<<B_SEND|nr.read_nibble();
//				r=r<<B_SEND|br.read_bits(B_SEND,false);
			}
		}
	//================================================================
	//	�f�[�^�̗���
	//�@�@istr �� A �� B �� pop_data
	//================================================================
	//	A. �I�[�����؂�o��
	//----------------------------------------------------------------
		/// <summary>�I�[�������o����</summary>
		class TermCodeTrimmer:public mwg::Stream::InStream{
			InStream& str;
			byte buff[4];
			const int len;
			int index;
		public:
			TermCodeTrimmer(InStream& istr,int termCodeLength)
				:str(istr),len(termCodeLength),index(0)
			{
				assert(1<=termCodeLength&&termCodeLength<=4);

				if(istr.eos())return;
				for(int i=0;i<len;i++)
					buff[i]=str.get_byte();
			}
			byte_n get_byte(){
				if(str.eos())return -1;
				byte ret=buff[index];

				buff[index]=str.get_byte();
				index++;
				if(index==len)index=0;

				return ret;
			}
			bool eos(){
				return str.eos();
			}
		public:
			uint term_code(){
				uint ret=0;
				int ir=0;
				int idx=index;
				for(int i=0;i<len;i++){
					ret|=buff[idx++]<<ir;
					if(idx==len)idx=0;
					ir+=8;
				}
				return ret;
			}
		} *tc_reader;
	//----------------------------------------------------------------
	//	B. ��U �y�� �͈͑I�� / �o��
	//----------------------------------------------------------------
	private:
		void load(){
			/* r_l �� MSK_SEND �Ɋ|����Ȃ��ʏ������� */
			while((r_l&MSK_SEND)==0){
				r=r<<B_SEND|nr.read_nibble();
//				r=r<<B_SEND|br.read_bits(B_SEND,false);
//				r_s=r_s<<B_SEND;//&MSK_RANGE;
				r_l=r_l<<B_SEND;//&MSK_RANGE;
			}
			mwg::break_assert(0<=r&&r<r_l);
		}
	public:
		template<typename TPredictor>
		uint peak_data(TPredictor& pp){
			// �񕪒T�� in [0, N)
			// *** OPTIMIZED ***
			uint val_l=0;
			uint val_u=pp.max_value();
			while(val_l+1!=val_u){
				uint val_c=(val_l+val_u)>>1;
				if(r_l*pp(val_c)>>B_FREQ<=r)
					val_l=val_c;
				else
					val_u=val_c;
			}

			return val_l;
		}
		template<typename TPredictor>
		uint pop_data(TPredictor& pp){
			term_code++;

			uint value=peak_data<TPredictor>(pp);

			// r, r_s, r_l �X�V
			uint new_s=pp(value);
			uint new_l=pp(value+1)-new_s;
			new_s =r_l*new_s>>B_FREQ;
			new_l =r_l*new_l>>B_FREQ;
			r_l   =new_l;
//			r_s  +=new_s;
			r    -=new_s;

			mwg::break_assert(new_l>0&&0<r_l);

			mwg::break_assert(0<=r&&r<r_l);
			pp.report(value);
			load();

			return value;
		}
	public:
		/// <summary>
		/// �c��f�[�^���݂邩�ۂ����擾���܂��B
		/// termCodeLength ���w�肵�Ȃ������ꍇ���� 0 ���w�肵���ꍇ�̌ďo�͑z�肵�Ă��܂���B
		/// </summary>
		bool empty(){
			assert(this->tc_reader!=nullptr);
			return nr.eos()&&this->tc_reader->term_code()==term_code;
//			return br.stream_eos()&&this->tc_reader->term_code()==term_code;
		}
#if 0
	//================================================================
	//	�g�p��
	//================================================================
	public:
		static void sample(mwg::Stream::InStream& stream){
			class BytePredictor{
			public:
				// �{���͂����ƌ���������� (���k����䢂̎����Ɍ������Ă���
				uint max_value(){return 256;}
				void report(uint value){}
				uint operator()(uint value){return value<<(B_FREQ-8);}
			} pp;

			RangeDecoder dec(stream,1);
			while(dec.empty()){
				byte b=dec.pop_data(pp);
			}
		}
#endif
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
