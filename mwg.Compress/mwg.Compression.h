// mwg.Compression.h
#pragma once
#include "stream.h"
#include "stream.interop.h"

// à≥èkäÌ I
#include "compress1.h"
#include "arith_coder1.h"
#include "encode1.h"
#include "decode1.h"

namespace mwg{
namespace Compression{
	void MZipCompressFile(const char* sourceFile,const char* destinationFile);
	void MZipDecompressFile(const char* sourceFile,const char* destinationFile);
}
}
