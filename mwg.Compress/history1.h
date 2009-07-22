#pragma once
#include "compress1.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	template<int BITS> class ring_array;
	typedef ring_array<B_REF_LEN+1> match_cand_array;

	class HistoryTable;
	static const int B_TABLE=B_REF_ADR;
	static const int SZ_TABLE=1<<B_TABLE;
	static const int MASK_TABLE=SZ_TABLE-1;
	/// <summary>
	/// 適当に履歴を初期化。(復号時と圧縮時に同じ物を使用する必要在り)
	/// </summary>
	void initial_cache(byte* cache);
	int hash(uint value);
	int hash(int value);
	int hash(byte (&val)[REF_LEN_BASE]);
	template<typename T>
	int hash(const T& value){
		assert(REF_LEN_BASE==4);
		int a=value[0]|value[1]<<8|value[2]<<16|value[3]<<24;
		return hash(a);
	}

	//****************************************************************
	//	リングバッファ
	//================================================================
	template<int BITS>
	class ring_array{
		static const int L=1<<BITS;
		int len;
		int iBase;
		byte d[L];
	public:
		ring_array():len(0),iBase(0){}
		bool isfull() const{
			return len==L;
		}
		int length() const{
			return this->len;
		}
		byte operator[](int index) const{
			return d[iBase+index&L-1];
		}
	public:
		void push(byte value){
			if(isfull()){
				d[iBase++]=value;
				iBase&=L-1;
			}else{
				d[iBase+len&L-1]=value;
				len++;
			}
		}
		byte_n deq_push(byte value){
			byte_n ret=len==0?-1:d[iBase];
			shift(1);
			push(value);
			return ret;
		}
		void shift(int count){
			iBase+=count;
			iBase&=L-1;
			len-=count;
			if(len<0)len=0;
		}

	public:
		/// <summary>
		/// 指定した値を配列に格納します。
		/// </sumary>
		/// <param name="value">格納するバイトを指定します。
		/// 負の値は EOF を意味し、この時は値を格納しません。</param>
		void push(byte_n value){
			if(value<0)return;
			push((byte)value);
		}
		byte_n deq_push(byte_n value){
			byte_n ret=len==0?-1:d[iBase];
			shift(1);
			push(value);
			return ret;
		}
	};
	//****************************************************************
	//	履歴の保持・検索
	//================================================================
	class HistoryTable{
	private:
		typedef signed __int32 idx_entry; // entries の index
	private:
		/*
		static int hash(byte (&val)[REF_LEN_BASE]){
			assert(REF_LEN_BASE==4&&B_TABLE==12);
			int a=*(int*)val;
			return MASK_TABLE&(a^a>>12^a>>24);
		}
		//*/
	private:
		/* OPT サイズ可変 */
		idx_entry table[SZ_TABLE];

		struct Entry{
		public:
			idx_entry self;
			idx_entry next;
#ifndef NDEBUG
			int reg_count;
#endif
		} entries[SZ_CACHE];

		byte cache[SZ_CACHE];
		int icache;
	public:
		HistoryTable(){
			for(int i=0;i<SZ_TABLE;i++)
				table[i]=-1;

			initial_cache(cache);
			icache=0;

			for(int i=0;i<SZ_CACHE;i++){
				entries[i].self=i;
				entries[i].next=-1;
#ifndef NDEBUG
				entries[i].reg_count=0;
#endif

				if(i<=SZ_CACHE-REF_LEN_BASE)
					register_entry(entries[i]);
			}
		}

		void write_byte(byte value){
			unregister_entry(entries[icache]);
			cache[icache++]=value;
			icache&=MASK_CACHE;
			register_entry(entries[icache-REF_LEN_BASE+SZ_CACHE&MASK_CACHE]);
		}
	//----------------------------------------------------------------
	//	Entry 管理用関数
	//----------------------------------------------------------------
		int hash_entry(Entry& entry){
			assert(REF_LEN_BASE==4);

			uint a
				=cache[entry.self]
				|cache[entry.self+1&MASK_CACHE]<<8
				|cache[entry.self+2&MASK_CACHE]<<16
				|cache[entry.self+3&MASK_CACHE]<<24;
			return hash(reinterpret_cast<byte(&)[REF_LEN_BASE]>(a));
		}
		void register_entry(Entry& entry){
			mwg::break_assert(&entry-entries==entry.self&&entry.reg_count++==0);

			int h=hash_entry(entry);
			entry.next=table[h];
			table[h]=entry.self;
		}
		void unregister_entry(Entry& entry){
			mwg::break_assert(&entry-entries==entry.self&&entry.reg_count--==1);

			int i=entry.self;
			int h=hash_entry(entry);

			if(table[h]==i){
				table[h]=entry.next;
				return;
			}

			int j=table[h];
			while(true){
				if(entries[j].next==i){
					entries[j].next=entry.next;
					return;
				}
				mwg::break_assert(entries[j].next!=-1);
				j=entries[j].next;
			}
		}
	//----------------------------------------------------------------
	//	前方参照の一致を検索
	//----------------------------------------------------------------
	public:
		struct Reference{
			idx_entry ibase;
			idx_entry idx;
			int len;
		public:
			Reference(int icache)
				:ibase(icache-1),idx(-1),len(REF_LEN_BASE-1){}

			void trial(idx_entry idx,int len){
				if(len<=this->len)return;
				this->len=len;
				this->idx=idx;
			}

			operator bool(){
				return this->idx>=0;
			}

			int length() const{
				return this->len;
			}
			int address() const{
				return this->ibase+SZ_CACHE-this->idx&MASK_CACHE;
			}
		};
		Reference match(const match_cand_array& data){
			Reference cand(icache);
			if(data.length()<REF_LEN_BASE)return cand;

			for(int i=1;i<REF_LEN_BASE;i++){
				idx_entry try_idx=icache-i+SZ_CACHE&MASK_CACHE;
				cand.trial(try_idx,match_entry(entries[try_idx],data));
			}

			assert(REF_LEN_BASE==4);
			int h=hash(data);
			idx_entry i=table[h];
			while(i>=0){
				cand.trial(i,match_entry(entries[i],data));
				i=entries[i].next;
			}

			return cand;
		}
	private:
		int match_entry(Entry& entry,const match_cand_array& data){
			int iData=0;

			// (1) Dirty-Section に入る迄
			int i(entry.self);
			do{
				if(data[iData]!=cache[i++])goto judge;
				i&=MASK_CACHE;
				iData++;
				if(iData==data.length())goto judge;
			}while(i!=icache);

			// (2) Dirty-Section に入った後
			int iDirty=0;
			while(true){
				if(data[iData]!=data[iDirty++])goto judge;
				iData++;
				if(iData==data.length())goto judge;
			}

		judge:
			return iData; // 一致バイト数を返します。
		}
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
