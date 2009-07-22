#include "stdafx.h"
#include "mwg.Compression.h"
namespace mwg{
namespace Compression{
#ifdef _MANAGED
#	pragma unmanaged
#endif
	void MZipCompressFile(const char* sourceFile,const char* destinationFile){
		mwg::Stream::ReadFileStream istr(sourceFile);
		mwg::Stream::WriteFileStream ostr(destinationFile);

		mwg::Stream::InStreamConnector connector(istr);
		connector
			>>mwg::Stream::InStreamCreator<MZipEncoder>()
			>>mwg::Stream::InStreamCreator<NibbleRangeEncoder>()
//			>>mwg::Stream::InStreamCreator<mwg::Stream::CounterPipe>()
			>>(mwg::Stream::OutStream&)ostr;
	}

	void MZipDecompressFile(const char* sourceFile,const char* destinationFile){
		mwg::Stream::ReadFileStream istr(sourceFile);
		mwg::Stream::WriteFileStream ostr(destinationFile);

		mwg::Stream::InStreamConnector connector(istr);
		connector
//			>>mwg::Stream::InStreamCreator<mwg::Stream::StdoutPipe>()
			>>mwg::Stream::InStreamCreator<NibbleRangeDecoder>()
			>>mwg::Stream::InStreamCreator<MZipDecoder>()
			>>(mwg::Stream::OutStream&)ostr;
	}
#ifdef _MANAGED
#	pragma managed
	public ref class CompressionUtil sealed abstract{
	public:
		static System::IO::Stream^ MZipCompress(System::IO::Stream^ stream){
			mwg::Stream::InStream* str1
				=mwg::Stream::ExportToInStream(stream);
			mwg::Stream::InStream* str2
				=new mwg::Compression::MZipEncoder(*str1);
			mwg::Stream::InStream* str3
				=new mwg::Compression::NibbleRangeEncoder(*str2);
			mwg::Stream::InStreamImporter^ str4
				=gcnew mwg::Stream::InStreamImporter(*str3);
			str4->RegisterToDelete(str1);
			str4->RegisterToDelete(str2);
			str4->RegisterToDelete(str3);
			str4->AddReference(stream);
			return str4;
		}
		static System::IO::Stream^ MZipDecompress(System::IO::Stream^ stream){
			mwg::Stream::InStream* str1
				=mwg::Stream::ExportToInStream(stream);
			mwg::Stream::InStream* str2
				=new mwg::Compression::NibbleRangeDecoder(*str1);
			mwg::Stream::InStream* str3
				=new mwg::Compression::MZipDecoder(*str2);
			mwg::Stream::InStreamImporter^ str4
				=gcnew mwg::Stream::InStreamImporter(*str3);
			str4->RegisterToDelete(str1);
			str4->RegisterToDelete(str2);
			str4->RegisterToDelete(str3);
			str4->AddReference(stream);
			return str4;
		}

	};
#pragma unmanaged
#endif
}
}