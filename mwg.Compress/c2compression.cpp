#include "stdafx.h"
#include <sys/types.h>
#include <sys/stat.h>
#include "c2compression.h"
#ifdef _MANAGED
#	pragma unmanaged
#endif
namespace mwg{
namespace Compression{
	//================================================================
	//	ƒtƒ@ƒCƒ‹ˆ³k
	//================================================================
	class ProgressReadFileStream;
	class ProgressReadFileStream:public mwg::Stream::ReadFileStream{
		uint total;
		void (*pfn)(int);
		uint count;
		uint next_count;
		uint percent;
	public:
		ProgressReadFileStream(const char* filename,void (*pfnProgress)(int percent))
			:ReadFileStream(filename),total(0),pfn(pfnProgress),count(0),percent(0)
		{
			struct _stat finfo;
			_stat(filename,&finfo);
			this->total=finfo.st_size;
			this->update_percent(0);
		}
		~ProgressReadFileStream(){
			if(percent<100)pfn(100);
		}
		byte_n get_byte(){
			if(++count==next_count)
				this->update_percent(percent+1);

			return ReadFileStream::get_byte();
		}
		bool eos(){
			return ReadFileStream::eos();
		}
	private:
		void update_percent(int percent){
			this->percent=percent;
			this->pfn(percent);
			this->next_count=(percent+1)*total/100;
		}
	};

	void MdkCompressFile(const char* sourceFile,const char* destinationFile){
		mwg::Stream::ReadFileStream istr(sourceFile);
		mwg::Stream::WriteFileStream ostr(destinationFile);

		mwg::Stream::InStreamConnector connector(istr);
		connector
			>>mwg::Stream::InStreamCreator<MdkEncoder>()
//			>>mwg::Stream::InStreamCreator<mwg::Stream::CounterPipe>()
			>>(mwg::Stream::OutStream&)ostr;
	}

	void MdkCompressFile(const char* sourceFile,const char* destinationFile,void (*pfnProgress)(int percent)){
		ProgressReadFileStream istr(sourceFile,pfnProgress);
		mwg::Stream::WriteFileStream ostr(destinationFile);
		mwg::Stream::InStreamConnector connector(istr);
		connector
			>>mwg::Stream::InStreamCreator<MdkEncoder>()
			>>(mwg::Stream::OutStream&)ostr;
	}

	void MdkDecompressFile(const char* sourceFile,const char* destinationFile){
		mwg::Stream::ReadFileStream istr(sourceFile);
		mwg::Stream::WriteFileStream ostr(destinationFile);

		mwg::Stream::InStreamConnector connector(istr);
		connector
			>>mwg::Stream::InStreamCreator<MdkDecoder>()
			>>(mwg::Stream::OutStream&)ostr;
	}

	void MdkDecompressFile(const char* sourceFile,const char* destinationFile,void (*pfnProgress)(int percent)){
		ProgressReadFileStream istr(sourceFile,pfnProgress);
		mwg::Stream::WriteFileStream ostr(destinationFile);

		mwg::Stream::InStreamConnector connector(istr);
		connector
			>>mwg::Stream::InStreamCreator<MdkDecoder>()
			>>(mwg::Stream::OutStream&)ostr;
	}

#ifdef _MANAGED
#	pragma managed
#endif
	ref class MdkSettings abstract sealed{
	public:
		static System::Xml::XmlDocument^ c2range;
		static MdkSettings(){
			System::Resources::ResourceManager^ resman=gcnew System::Resources::ResourceManager(
				"mwgCompress.c2resource",
				System::Reflection::Assembly::GetExecutingAssembly()
				);

			c2range=gcnew System::Xml::XmlDocument();
			c2range->LoadXml((System::String^)resman->GetObject("c2range"));
		}
		static bool GetRangeData(mwg::uint *range_s,int range_count,int max_value,System::String^ name){
			System::String^ xpath=System::String::Format(
				"/RangeData/ranges[@name='{0}'][@range_count='{1}'][@max_value='{2}']",
				name,range_count,max_value
				);
			System::Xml::XmlElement^ elem=dynamic_cast<System::Xml::XmlElement^>(c2range->SelectSingleNode(xpath));
			if(!elem){
				if(name!="default")
					return GetRangeData(range_s,range_count,max_value,"default");
				goto failed;
			}

			cli::array<System::String^>^ nums=elem->InnerText->Split(
				gcnew cli::array<wchar_t>{' ',',','\r','\n','\t'},
				System::StringSplitOptions::RemoveEmptyEntries
				);
			if(nums->Length!=range_count+1)goto failed;

			for(int i=0;i<=range_count;i++){
				if(!uint::TryParse(nums[i],range_s[i]))goto failed;
			}
			if(range_s[range_count]!=max_value)goto failed;

			return true;
		failed:
			return false;
		}
	};

	bool MdkSettings_GetRangeData(mwg::uint *range_s,int range_count,int max_value,const char* cstrName){
		return MdkSettings::GetRangeData(range_s,range_count,max_value,gcnew System::String(cstrName));
	}

#ifdef _MANAGED
#	pragma unmanaged
#endif
	bool MdkPref::GetRangeData(mwg::uint *range_s,int range_count,int max_value,const char* name){
		return MdkSettings_GetRangeData(range_s,range_count,max_value,name);
	}
}
}