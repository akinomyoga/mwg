#include "stdafx.h"
#include "stream.interop.h"
#ifdef _MANAGED
#pragma managed

namespace mwg{
namespace Stream{
	delegate int ManagedStreamReadByte();
	InStreamExporter* ExportToInStream(System::IO::Stream^ str){
		System::IntPtr ptr=System::Runtime::InteropServices::Marshal::GetFunctionPointerForDelegate(
			gcnew ManagedStreamReadByte(str,&System::IO::Stream::ReadByte)
			);
		return new InStreamExporter((int(__stdcall*)())(void*)ptr);
	}
}
}
#endif
