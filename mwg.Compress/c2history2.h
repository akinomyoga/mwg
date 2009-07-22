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

		byte cache[SZ_WINDOW];
		int icache;
	public:
		HistoryWindow(){}
		template<typename TFunc> //void (*cacheInitializer)(byte*,std::size_t)
		void Initialize(TFunc cacheInitializer){
			for(int i=0;i<SZ_WINDOW;i++){
				idx_window iself=table[i].iself(this);
				table[i].inext=iself;
				table[i].iprev=iself;
			}

			cacheInitializer(cache,SZ_WINDOW);
			icache=0;

			for(int i=0;i<SZ_WINDOW;i++){
				indices[i].iprev=-1;
				indices[i].inext=-1;

				if(i<=SZ_WINDOW-MIN_REFLEN)
					register_entry(indices[i]);
			}
		}
	//================================================================
	// Index 管理 : 双方向リスト RingListQueue
	//================================================================
		struct IndexElem{
		public:
			idx_window iprev;
			idx_window inext;
			IndexElem& next(HistoryWindow* win) const{
				return win->indices[inext];
			}
			IndexElem& prev(HistoryWindow* win) const{
				return win->indices[iprev];
			}
			idx_window iself(const HistoryWindow* win) const{
				return this-&win->indices[0];
			}
		public:
			int hash(const HistoryWindow* win) const{
				assert(MIN_REFLEN==4);

				idx_window i=this->iself(win);
				uint header
					=win->cache[i]
					|win->cache[i+1&MSK_WINDOW]<<8
					|win->cache[i+2&MSK_WINDOW]<<16
					|win->cache[i+3&MSK_WINDOW]<<24;
				return HistoryWindow::hash(header);
			}
		public:
			void insert_after(HistoryWindow* win,IndexElem& elem){
				IndexElem& next=this->next(win);
				idx_window ielem=elem.iself(win);

				next.iprev=ielem;
				elem.inext=this->inext;

				this->inext=ielem;
				elem.iprev=this->iself(win);
			}
			void remove(HistoryWindow* win){
				this->next(win).iprev=this->iprev;
				this->prev(win).inext=this->inext;
			}
			void remove_prev(HistoryWindow* win){
				IndexElem& mid=this->prev(win);
				IndexElem& prv=mid.prev(win);
				this->iprev	=mid.iprev;
				prv.inext	=mid.inext;
			}
		};
		IndexElem table[SZ_TABLE];
		IndexElem indices[SZ_WINDOW];
		void register_entry(IndexElem& elem){
			int h=elem.hash(this);
			table[h].insert_after(this,elem);
		}
		void unregister_entry(IndexElem& elem){
			int h=elem.hash(this);
			mwg::break_assert(table[h].iprev==elem.iself(this));
			table[h].remove_prev(this);
		}
	//----------------------------------------------------------------
	//	他
	//----------------------------------------------------------------
		static int hash(uint data){
			return MSK_TABLE&(data^data>>B_TABLE^data>>B_TABLE*2);
		}
		void write_byte(byte value){
			unregister_entry(indices[icache]);
			cache[icache]=value;
			icache=icache+1&MSK_WINDOW;
			register_entry(indices[index_of(MIN_REFLEN-1)]);
		}
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

			// 近い方から順番に確認
			idx_window i=table[h].inext;
			while(i>=0){
				try_match(ret,i,data);
				
				// 最長一致になったら即座に終了
				if(data.length()==ret.length)break;

				i=indices[i].inext;
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
			}
		}
		/// <summary>
		/// 指定した位置からの、一致文字列長を取得します。
		/// </summary>
		int match_length(idx_window i,const target_array& data){
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
