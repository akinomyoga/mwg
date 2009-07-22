#pragma once
#include <cstdio>
#include "mwgbase.h"
#include "ringbuf.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Stream{
	class InStream{
	public:
		// -1 means EOF
		virtual byte_n get_byte()=0;
		// End of Stream
		virtual bool eos()=0;
		virtual ~InStream(){}
	};

	class OutStream{
	public:
		virtual void put_byte(byte value)=0;
		virtual ~OutStream(){}
	};

	class BufferStream:public OutStream{
		RingQueue dat;
	public:
		void put_byte(byte value){
			dat.push(value);
		}
	public:
		bool empty() const{
			return this->dat.empty();
		}
		int length() const{
			return this->dat.length();
		}
		byte read_byte(){
			return dat.pop();
		}
	};

	class ReadFileStream:public InStream{
		FILE* fp;
		byte_n next;
		const char* file;

	public:
		ReadFileStream(const char* filename)
			:file(filename),fp(nullptr)
		{
			if(::fopen_s(&this->fp,filename,"rb")){
				mwg::break_throw<std::runtime_error>("ファイルを開くのに失敗しました。");
			}
			this->next=std::fgetc(fp);
		}
		~ReadFileStream(){
			std::fclose(fp);
		}
	public:
		byte_n get_byte(){
			byte_n ret=next;
			next=std::fgetc(fp);
			if(next==EOF)next=-1;
			return ret;
		}
		bool eos(){
			return this->next<0;
		}
	public:
		const char* get_filename(){
			return this->file;
		}
	};

	class WriteFileStream:public OutStream{
		FILE* fp;
		const char* file;
	public:
		WriteFileStream(const char* filename):file(filename){
			if(::fopen_s(&this->fp,filename,"wb"))
				mwg::break_throw<std::runtime_error>("ファイルを開くのに失敗しました。");
		}
		~WriteFileStream(){
			std::fclose(fp);
		}
		void put_byte(byte value){
			std::fputc(value,fp);
		}
	public:
		const char* get_filename(){
			return this->file;
		}
	};

	/// <summary>
	/// デバグ用ストリーム
	/// </summary>
	class CounterPipe:public InStream{
		InStream& istr;
		int c;
	public:
		CounterPipe(InStream& istr):istr(istr),c(0){}
		~CounterPipe(){
			::printf("%d bytes' passed through CounterStream.\n",c);
		}
	public:
		byte_n get_byte(){
			byte_n b=istr.get_byte();
			if(b>=0)c++;
			return (byte)b;
		}
		bool eos(){
			return istr.eos();
		}
	};

	/// <summary>
	/// デバグ用ストリーム
	/// </summary>
	class StdoutPipe:public InStream{
		InStream& istr;
		std::string data;
		int c;
	public:
		StdoutPipe(InStream& istr):istr(istr),c(0){}
		~StdoutPipe(){
			::printf("%d bytes' passed through StdoutPipe.\n",c);
			::printf("%s\n",data.c_str());
		}
	public:
		byte_n get_byte(){
			byte_n b=istr.get_byte();
			if(b>=0){
				c++;
				char buff[4];
				::sprintf_s(buff,"%02X ",b);
				data+=buff;
			}
			return (byte)b;
		}
		bool eos(){
			return istr.eos();
		}
	};

	class InStreamConnector:public InStream{
		std::vector<InStream*> vec;
	public:
		InStreamConnector(InStream& istr){
			vec.push_back(&istr);
		}
		~InStreamConnector(){
			for(std::vector<InStream*>::iterator i=vec.end()-1;i!=vec.begin();i--)
				delete *i;
		}
		void operator>>(OutStream& ostr){
			while(!this->eos())
				ostr.put_byte(this->get_byte());
		}
		template<typename T>
		InStreamConnector& operator>>(const T& creator){
			vec.push_back(creator.CreateInStream(vec.back()));
			return *this;
		}
	public:
		byte_n get_byte(){
			return vec.back()->get_byte();
		}
		bool eos(){
			return vec.back()->eos();
		}
	};
	template<typename T>
	class InStreamCreator{
	public:
		InStream* CreateInStream(InStream* str) const{
			return new T(*str);
		}
	};
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
