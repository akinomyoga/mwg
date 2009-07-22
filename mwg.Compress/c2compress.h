#pragma once
#include "mwgbase.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	class MdkPref{
	public:
		static const uint SUP_RAWLEN	=(1<<6)+1;
		static const uint MIN_RAWLEN	=1;

		//============================================================
		//	参照ブロック
		//------------------------------------------------------------
		//	tt aaaaaa aaaaaa llllll
		//============================================================
		static const uint B_REFADR_L	=8;
		static const uint B_REFADR_H	=8;
		static const uint SUP_REFADR_L	=1<<B_REFADR_L;
		static const uint SUP_REFADR_H	=1<<B_REFADR_H;
		static const uint MSK_REFADR_L	=SUP_REFADR_L-1;
		static const uint MSK_REFADR_H	=SUP_REFADR_H-1;

		static const uint B_WINDOW		=B_REFADR_L+B_REFADR_H;
		static const uint SZ_WINDOW		=1<<B_WINDOW;
		static const uint MSK_WINDOW	=SZ_WINDOW-1;
		static const uint SUP_REFADR	=SZ_WINDOW;

		static const uint SUP_REFLEN	=(1<<6)+4;
		static const uint MIN_REFLEN	=4;

		static const int PP_NRAN_RAWLEN		=16;
		static const int PP_NRAN_REFADR_L	=16;
		static const int PP_NRAN_REFADR_H	=16;
		static const int PP_NRAN_REFLEN		=16;

#define BIN_PREDICTOR	1
#define BYTE_PREDICTOR	2
#define RANGE_PREDICTOR	3
#define PPTYPE_REFLEN	RANGE_PREDICTOR
#define PPTYPE_REFADR	RANGE_PREDICTOR

		class BlockType{
		public:
			enum enum_t{
				Reference	=0,
				RawData,
				Control,
				N,
			};
		};
	public:
		static bool GetRangeData(mwg::uint *range_s,int range_count,int max_value,const char* name="default");
		static void InitializeCache(byte* pdat,std::size_t size){
			// キャッシュを適当な内容に初期化

			byte value=0;
			byte dv=1;
			for(uint i=0;i<size;i++){
				pdat[i]=value;
				value+=dv;
				if((i&0xFF)==0xFF)dv+=2;
			}
		}
	};

	void MdkCompressFile(const char* sourceFile,const char* destinationFile);
	void MdkDecompressFile(const char* sourceFile,const char* destinationFile);
	void MdkCompressFile(const char* sourceFile,const char* destinationFile,void (*pfnProgress)(int percent));
	void MdkDecompressFile(const char* sourceFile,const char* destinationFile,void (*pfnProgress)(int percent));
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
