#pragma once
#include "mwgbase.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Stream{
	//****************************************************************
	//	Queue - 循環配列による実装
	//================================================================
	class RingQueue{
		byte* pbuff;
		std::size_t size;
		uint mask;
		uint start;
		uint len;
	public:
		RingQueue(){
			this->size=4;
			this->mask=size-1;
			this->pbuff=new byte[this->size];
			this->start=0;
			this->len=0;
		}
		RingQueue(const RingQueue& q):pbuff(nullptr){
			this->operator=(q);
		}
		RingQueue& operator=(const RingQueue& q){
			this->free();
			this->size=q.size;
			this->mask=size-1;
			this->pbuff=new byte[size];

			this->start=0;
			this->len=q.len;
			for(uint id=0,is=q.start;id<len;){
				this->pbuff[id++]=q.pbuff[is++];
				is&=q.mask;
			}

			return *this;
		}
		~RingQueue(){
			this->free();
		}
	public:
		void push(byte value){
			if(len+1>size){
				this->realloc(size<<1);
			}

			pbuff[start+len&mask]=value;
			this->len++;
		}
		byte peek() const{
			mwg::break_assert(len>0);
			return pbuff[start];
		}
		byte pop(){
			byte ret=peek();
			this->start++;
			this->start&=mask;
			this->len--;
			return ret;
		}
		/// <summary>
		/// 指定した値を末端に追加して先頭の要素を削除します。
		/// </summary>
		byte slide(byte_n value){
			if(value<0)return pop();

			byte ret=pbuff[start];
			pbuff[start+len&mask]=value;
			start=start+1&mask;
			return ret;
		}
	public:
		byte operator[](int index) const{
			return pbuff[this->start+index&mask];
		}
		int length() const{
			return this->len;
		}
		bool empty() const{
			return this->len==0;
		}
	private:
		void realloc(std::size_t newsize){
			byte* newbuff=new byte[newsize];

			for(uint id=0,is=start;id<len;){
				newbuff[id++]=pbuff[is++];
				is&=mask; // size は 1<<n
			}
			delete[] this->pbuff;
			this->pbuff=newbuff;
			this->size=newsize;
			this->mask=size-1;
			this->start=0;
		}
		void free(){
			if(this->pbuff!=nullptr){
				delete[] this->pbuff;
				this->pbuff=nullptr;
			}
		}
	};
}
}