#pragma once
#include "ringbuf.h"
#include "c2compress.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	class HistoryWindow{
		static const int B_TABLE		=14;
		static const int SZ_TABLE		=1<<B_TABLE;
		static const int MSK_TABLE		=SZ_TABLE-1;
		static const uint SZ_WINDOW		=MdkPref::SZ_WINDOW;
		static const uint MSK_WINDOW	=MdkPref::MSK_WINDOW;
		static const uint MIN_REFLEN	=MdkPref::MIN_REFLEN;

		// window cache 内に於ける位置を表します。
		typedef signed __int32 idx_window;
		typedef mwg::Stream::RingQueue target_array;

		idx_window table[SZ_TABLE];
		struct Entry{
		public:
			idx_window self;
			idx_window next;
		} entries[SZ_WINDOW];

		byte cache[SZ_WINDOW];
		int icache;
	public:
		HistoryWindow(){}
		template<typename TFunc> //void (*cacheInitializer)(byte*,std::size_t)
		void Initialize(TFunc cacheInitializer){
			for(int i=0;i<SZ_WINDOW;i++)
				table[i]=-1;

			cacheInitializer(cache,SZ_WINDOW);
			icache=0;

			for(int i=0;i<SZ_WINDOW;i++){
				entries[i].self=i;
				entries[i].next=-1;

				if(i<=SZ_WINDOW-MIN_REFLEN)
					register_entry(entries[i]);
			}
		}
		static int hash(uint data){
			return MSK_TABLE&(data^data>>B_TABLE^data>>B_TABLE*2);
		}
		void write_byte(byte value){
			unregister_entry(entries[icache]);
			cache[icache]=value;
			icache=icache+1&MSK_WINDOW;
			register_entry(entries[index_of(MIN_REFLEN-1)]);
		}
	//----------------------------------------------------------------
	//	Entry 管理用関数
	//----------------------------------------------------------------
	private:
		int hash_entry(Entry& entry){
			assert(MIN_REFLEN==4);

			idx_window i=entry.self;
			uint a
				=cache[i]
				|cache[i+1&MSK_WINDOW]<<8
				|cache[i+2&MSK_WINDOW]<<16
				|cache[i+3&MSK_WINDOW]<<24;
			return hash(a);
		}
		void register_entry(Entry& entry){
			mwg::break_assert(&entry-entries==entry.self);

			int h=hash_entry(entry);
			entry.next=table[h];
			table[h]=entry.self;
		}
		void unregister_entry(Entry& entry){
			mwg::break_assert(&entry-entries==entry.self);

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
	//	前方参照データ
	//----------------------------------------------------------------
	public:
		struct Reference{
		public:
			int length;
			int address;
			Reference()
				:length(0),address(0){}

			operator bool(){
				return length>=MIN_REFLEN;
			}
		};
	private:
		int address_of(idx_window position){
			const idx_window ibase=icache-1;
			return MSK_WINDOW&ibase+SZ_WINDOW-position;
		}
		idx_window index_of(int address){
			const idx_window ibase=icache-1;
			return MSK_WINDOW&ibase+SZ_WINDOW-address;
		}
	//----------------------------------------------------------------
	//	前方参照の一致を検索
	//----------------------------------------------------------------
	public:
		Reference match(const target_array& data){
			Reference ret;
			if(data.length()<MIN_REFLEN)return ret;

			for(int i=1;i<MIN_REFLEN;i++)
				try_match(ret,(icache-i+SZ_WINDOW)&MSK_WINDOW,data);

			assert(MIN_REFLEN==4);
			int h=hash(data[0]|data[1]<<8|data[2]<<16|data[3]<<24);

			idx_window i=table[h];
			while(i>=0){
				try_match(ret,i,data);
				i=entries[i].next;
			}

			return ret;
		}
	private:
		/// <summary>
		/// 指定した位置からの一致を試して、最長文字列長を更新します。
		/// </summary>
		void try_match(Reference& refer,idx_window try_idx,const target_array& data){
			int mlen=match_length(try_idx,data);
			int adr=address_of(try_idx);
			if(refer.length<mlen){
				refer.length=mlen;
				refer.address=adr;
			}else if(refer.length==mlen){
				// より近い方に統一
				if(adr<refer.address)
					refer.address=adr;
			}
		}
		/// <summary>
		/// 指定した位置からの、一致文字列長を取得します。
		/// </summary>
		int match_length(idx_window i,const target_array& data){
			Entry& entry=this->entries[i];
			int iData=0;

			// (1) Dirty-Section に入る迄
			do{
				if(data[iData]!=cache[i++])goto judge;
				i&=MSK_WINDOW;
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
