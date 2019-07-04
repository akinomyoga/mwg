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
	//		�Z�p���k�̌`��
	//================================================================
	//	�f�[�^: <bit ��> <padding-bit> <byte:�I���}�[�J�[> EOF
	//	�I���}�[�J�[ : (EOF �̒��O�ɔz����)
	//		���k�O�̃o�C�g���̉� 8bit
	//		������ɂ���āA�����ɉ��o�C�g�ǂݎ��Ηǂ���
	//		�@(== padding-bit �Ƃ��Ăǂꂾ����������ׂ���) ��������
	//
	//----------------------------------------------------------------
	//	��: ���߂̓��� nibble �P�� / ���̓��� byte �P��
	//	��: �p�x�\�ɑO��̒l�Ƃ̑��ւ��������
	//		�X�ɁA�O�X��̒l�Ƃ̑��ւ�O�̑��ւȂǂ��l����B
	//----------------------------------------------------------------
	//****************************************************************
	//		4bit �P�ʎZ�p���k�萔
	//================================================================
	class NibbleRC{
	private:
		NibbleRC();
		NibbleRC(const NibbleRC&);
	public:
		static const int B_SEND		=4; // �o�͒P��
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
		//   ssss    : ������f�[�^�̏ꏊ
		//   aaaa    : freq �|���Z�p�̗]�T
		//   llll    : ���ɂȂ�l�ɕۂ�
		//
		// UNI_SEND  :------------ -----------1 00000000000000000000
		// MSK_SEND  :------------ 111111111111 --------------------
		// MSK_RANGE :------------ 111111111111 11111111111111111111
		// MSK_CUTOFF:------------ ------------ 11111111111111111111
	};
	//****************************************************************
	//		4bit �P�ʕp�x�\
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
					// �ߋ��̏��͎キ
					data2[i]-=data2[i]>>2; // >>7: 99.2% >>4: 93.8% >>2: 75%
					data2[i]+=data[i];
					data[i]=0;
				}
			}
			//*/
		}

		void update_freq(){
			// �ݐϒl�̌v�Z
			uint t=0;
			for(int i=0;i<16;i++){
				cuml[i]=t;
				t+=data[i]+data2[i];
			}

			// ���Œ�
			// �E���������� MAX_CUML_W ���z���Ă��܂����A
			// �E����ɂ��� double �ŉ��Z����̂ŏo���邾���傫�������ǂ�
			// cuml_w=MAX_CUML_W;
			uint t2=1<<cuml_w;
			/*
			// ������
			cuml_w=8;
			uint t2=1<<cuml_w;
			while(t2<t){
				t2<<=1;
				cuml_w++;
			}
			//*/

			// �r���E�\�̊���
			double f=t2/(double)t;
			for(int i=0;i<16;i++)cuml[i]=uint(cuml[i]*f);
			cuml[16]=t2;
		}
	//================================================================
	//	�T��
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
			// �񕪒T�� in [nib_l, nib_u)
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
	//		4bit �P�ʎZ�p���k��
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
	//	�G���R�[�h
	//================================================================
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

			/* r_l �� MSK_SEND �Ɋ|����Ȃ��ʏ������� */
			while((r_l&MSK_SEND)==0){
				write_cached(r_s>>(B_RANGE-B_SEND));
				r_s=r_s<<B_SEND&MSK_RANGE;
				r_l=r_l<<B_SEND;//&MSK_RANGE;
			}

			mwg::break_assert(0<r_l&&r_s<=MSK_RANGE);
		}
		void add(byte value){
			count++; // �I���}�[�J�[

			low.select_range(value,r_s,r_l);
			low.report(value); // �I����ɍX�V (�������L�^����̂Ɏ����̏��͎g���Ȃ��̂�)
			flush();

			high.select_range(value>>4,r_s,r_l);
			high.report(value>>4);
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
			// �� r_s <= �� < r_s + r_l
			//---------------------------------------------
			r_s=r_s+MSK_CUTOFF;
			r_l=MSK_CUTOFF; // ��񂾂������o��
			flush();
			this->flush_cache();
			bw.terminate();
			bw.write_byte(count);
		}
	//================================================================
	//	�X�g���[��
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
	//		4bit �P�ʎZ�p�L����
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

		// �Ō�̃o�C�g���������ׂ̈Ɏ���Ēu���X�g���[��
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
			,te_str(istr),br(/* �󏉊��� */)
			,r(0),count(0)
		{
			this->br=BitReader(te_str);
			for(int i=0;i<B_RANGE;i+=B_SEND){
				r=r<<B_SEND|br.read_bits(B_SEND,false);
			}
		}
	//================================================================
	//	�f�R�[�h
	//================================================================
	private:
		void load(){
			/* r_l �� MSK_SEND �Ɋ|����Ȃ��ʏ������� */
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
			count++; // �I���}�[�J�[�ƈ�v�������
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
	//	�X�g���[��
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
