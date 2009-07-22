#pragma once
#include "mwgbase.h"
#include "stream.h"
#ifdef _MANAGED
#	pragma managed(push,on)
namespace mwg{
namespace Stream{
	public ref class InStreamImporter:System::IO::Stream{
		typedef System::Collections::Generic::KeyValuePair<System::Object^,bool> pair_t;
		InStream& istr;
		slong pos;
		initonly System::Collections::Generic::List<pair_t>^ ref_list;
		initonly mwg::Collections::NativeList<InStream*>^ list;
		bool closed;
	internal:
		InStreamImporter(InStream& istr)
			:pos(0),istr(istr),closed(false)
			,list(gcnew mwg::Collections::NativeList<InStream*>())
			,ref_list(gcnew System::Collections::Generic::List<pair_t>())
		{}
	public:
		~InStreamImporter(){
			this->Close();
		}
		virtual void Close() override{
			if(closed)return;
			closed=true;

			for(int i=0;i<list->Count;i++){
				delete list[i];
			}
			for(int i=0;i<ref_list->Count;i++){
				if(!ref_list[i].Value)continue;
				System::IDisposable^ disposable
					=dynamic_cast<System::IDisposable^>(ref_list[i].Key);
				if(disposable!=nullptr)
					delete disposable;
			}
		}
	internal:
		void RegisterToDelete(InStream* str){
			list->Add(str);
		}
		void AddReference(System::Object^ obj){
			AddReference(obj,false);
		}
		void AddReference(System::Object^ obj,bool autoDispose){
			ref_list->Add(pair_t(obj,autoDispose));
		}
	public:
		property bool CanWrite{
			virtual bool get() override{
				return false;
			}
		}
		property bool CanSeek{
			virtual bool get() override{
				return false;
			}
		}
		property bool CanRead{
			virtual bool get() override{
				return true;
			}
		}

		virtual int Read(cli::array<byte>^ buffer,int offset,int count) override{
			if(buffer==nullptr)throw gcnew System::ArgumentNullException("buffer");
			if(offset<0||buffer->Length<=offset)throw gcnew System::ArgumentOutOfRangeException("offset");
			if(count<=0||offset+count>buffer->Length)throw gcnew System::ArgumentOutOfRangeException("count");

			int i=0;
			while(i<count&&!istr.eos()){
				buffer[offset++]=istr.get_byte();
				i++;
			}
			pos+=i;
			return i;
		}
		virtual int ReadByte() override{
			if(istr.eos())return -1;
			pos++;
			return istr.get_byte();
		}

	//================================================================
	//	非対応関数達
	//================================================================
	public:
		property slong Position{
			virtual slong get() override{
				return this->pos;
			}
			virtual void set(slong value) override{
				throw gcnew System::NotSupportedException("この Stream は Seek に対応していません。");
			}
		}
		property slong Length{
			virtual slong get() override{
				throw gcnew System::NotSupportedException("この Stream は Seek に対応していません。");
			}
		}
		virtual void Write(cli::array<byte>^ buffer,int offset,int count) override{
			throw gcnew System::NotSupportedException("この Stream は書き込みに対応していません。");
		}
		virtual void Flush() override{}
		virtual slong Seek(slong offset,System::IO::SeekOrigin origin) override{
			throw gcnew System::NotSupportedException("この Stream は Seek に対応していません。");
		}
		virtual void SetLength(slong value) override{
			throw gcnew System::NotSupportedException("この Stream は Seek に対応していません。");
		}
	};
#pragma unmanaged
	public class InStreamExporter:public InStream{
		int (__stdcall* readByte)();
		byte_n buff;
	public:
		InStreamExporter(int (__stdcall* pfnReadByte)()):readByte(pfnReadByte){
			buff=readByte();
		}
	public:
		byte_n get_byte(){
			byte_n ret=buff;
			buff=readByte();
			return ret;
		}
		bool eos(){
			return buff<0;
		}
	};
#pragma managed
	InStreamExporter* ExportToInStream(System::IO::Stream^ str);
}
}
#	pragma managed(pop)
#endif
