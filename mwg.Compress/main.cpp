// これは メイン DLL ファイルです。

#include "stdafx.h"
#include "mwg.Compression.h"
#include "c2compress.h"
#include <direct.h>
#include <sys/types.h>
#include <sys/stat.h>
#pragma unmanaged

namespace mwg{
namespace Stream{
	class FixedMemoryStream:public InStream{
	private:
		byte* buff;
		std::size_t size;
		uint index;
	public:
		FixedMemoryStream(void* buff,std::size_t size)
			:buff((byte*)buff),size(size),index(0){}
		byte_n get_byte(){
			if(eos())return -1;
			return buff[index++];
		}
		bool eos(){
			return index>=size;
		}

		void print() const{
			for(uint i=0;i<size;i++){
				printf("%c",buff[i]);
			}
			printf(" /size=%d\n",size);
		}

		void clear(){
			this->index=0;
		}
	};

	class HexStdoutStream:public OutStream{
	public:
		HexStdoutStream(){}
		void put_byte(byte value){
			::printf_s("%02X ",value);
		}
	};

	class StdoutStream:public OutStream{
	public:
		StdoutStream(){}
		void put_byte(byte value){
			::printf_s("%c",(char)value);
		}
	};

	class VectorOStream:public OutStream{
		std::vector<byte> buff;
	public:
		VectorOStream(){}
		VectorOStream(InStream& istr){
			*this<<istr;
		}
		void put_byte(byte value){
			buff.push_back(value);
		}

		std::vector<byte>& get_buffer(){
			return buff;
		}
	public:
		void print_hex(){
			for(std::vector<byte>::iterator i=buff.begin();i!=buff.end();i++)
				printf("%02X ",*i);
			printf("/size=%d\n",buff.size());
		}
		void print(){
			for(std::vector<byte>::iterator i=buff.begin();i!=buff.end();i++)
				printf("%c",*i);
			printf(" /size=%d\n",buff.size());
		}
		void clear(){
			this->buff.clear();
		}
		VectorOStream& operator<<(InStream& istr){
			/*
			int i=0;
			while(!istr.eos()){
				byte b=istr.get_byte();
				printf("%0X2",b);
				this->put_byte(b);
				i++;
			}
			/*/

			while(!istr.eos())this->put_byte(istr.get_byte());
			//*/
			return *this;
		}
	};

	class VectorIStream:public InStream{
	public:
		std::vector<byte> buff;
		uint index;
		VectorIStream(const std::vector<byte>& buff):buff(buff),index(0){}
		byte_n get_byte(){
			if(eos())return -1;
			return buff[index++];
		}
		bool eos(){
			return index>=buff.size();
		}
		void clear(){
			this->index=0;
		}
	};
}
}

void compress2();
void compress3();

static WORD progressSBIAttr;
void progress_init(){
	HANDLE hStdout=::GetStdHandle(STD_OUTPUT_HANDLE);

	CONSOLE_SCREEN_BUFFER_INFO cinfo; 
	::GetConsoleScreenBufferInfo(hStdout,&cinfo);
	progressSBIAttr=cinfo.wAttributes;

	::SetConsoleTextAttribute(hStdout,FOREGROUND_GREEN|FOREGROUND_INTENSITY); 
	std::cout<<"[--------------------------------------------------]"<<std::endl;

	cinfo.dwCursorPosition.X++;
	::SetConsoleCursorPosition(hStdout,cinfo.dwCursorPosition);
}
void progress_proc(int percent){
	if(percent==0||percent&1)return; // 正の偶数の時だけ

	std::cout<<"*";
	if(percent==100){
		std::cout<<"]"<<std::endl;
		HANDLE hStdout=::GetStdHandle(STD_OUTPUT_HANDLE);
		::SetConsoleTextAttribute(hStdout,progressSBIAttr); 
	}
}

int _tmain(){
	
	//compress2();
	compress3();

	::printf("... Press any key to exit>");
	::_getch();
	return 0;
}

void compress3(){
	char buff[0x400];
	::_chdir("..\\release");
	::printf("%s>\n",::_getcwd(buff,0x400));

	static const char* FILES[][3]={
		{"target\\test.htm","mdk\\test.mdk","mdk\\test.htm"},
		{"target\\gzcomp.cs","mdk\\gzcomp.mdk","mdk\\gzcomp.cs"},
		{"target\\w300c16.bmp","mdk\\w300c16.mdk","mdk\\w300c16_.bmp"},

		{"target\\Exe1","mdk\\Exe1.mdk","mdk\\Exe1.org"},
		{"target\\Exe2","mdk\\Exe2.mdk","mdk\\Exe2.org"},
		{"target\\Exe3","mdk\\Exe3.mdk",nullptr},

		{"target\\sfx.latex.raw.js","mdk\\sfx.latex.raw.mdk","mdk\\sfx.latex.raw.org"},
	};
	const char* (&files)[3]=FILES[6];

	/*
	::printf("Compressing ...\n");
	mwg::Compression::MZipCompressFile(files[0],files[1]);


	if(files[2]!=nullptr){
		::printf("Decompressing ...\n");
		mwg::Compression::MZipDecompressFile(files[1],files[2]);
	}

	//::printf("Onmemory Compress/Decompress ...\n");
	//mwg::Compression::MZipTestFile(files[0],files[2]);
	/*/

	::printf("-- Compressing --\n");
	::printf("src: %s\n",files[0]);
	::printf("dst: %s\n",files[1]);
	progress_init();
	mwg::Compression::MdkCompressFile(files[0],files[1],progress_proc);

	int fsz_o,fsz_c;{
		struct _stat st;
		_stat(files[0],&st);
		fsz_o=st.st_size;
		_stat(files[1],&st);
		fsz_c=st.st_size;
	}
	::printf("src: %d bytes\ndst: %d bytes\n",fsz_o,fsz_c);

	if(files[2]!=nullptr){
		::printf("-- Decompressing --\n");
		::printf("src: %s\n",files[1]);
		::printf("dst: %s\n",files[2]);
		progress_init();
		mwg::Compression::MdkDecompressFile(files[1],files[2],progress_proc);
	}

	::printf("Completed\n");
	//*/


}

void compress2(){
#pragma region テストコード1
	//char buff[1000];
	//int len=::scanf_s("%999s",buff);
	//char buff[]="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
	//char buff[]="abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabc";
	//char buff[]="printf(\"compressing ...\\n\");	mwg::Compression::FixedMemoryStream istr(buff,len);	mwg::Compression::VectorOStream ostr;";
	//char buff[]="// gzcomp.cs\r\nusing System;\r\nusing System.IO;\r\nusing System.IO.Compression;\r\n\r\npublic class CompressSample {\r\n	static void Main(string[] args) {\r\n\r\n		string inFile = args[0];\r\n\r\n		// 出力ファイルの拡張子は「.gz」";
	char buff[]="参考までに、テキスト・ファイル（上記サンプル・プログラムのgzcomp.cs）"
		"とバイナリ・ファイル（gzcomp.csをコンパイルして作成した gzcomp.exe）を、"
		"上記サンプル・プログラムおよび、ほかのいくつかの圧縮方式で圧縮した"
		"ときのファイル・サイズを示す。ZIP形式およびRAR 形式での圧縮についてはWinRARを用いている。"
		"// gzcomp.cs"
		"using System;"
		"using System.IO;"
		"using System.IO.Compression;"
		"public class CompressSample {"
		"	static void Main(string[] args) {"
		"		string inFile = args[0];"
		"		// 出力ファイルの拡張子は「.gz」"
		"		string outFile = Path.GetFileName(inFile) + \".gz\";"
		"		int num;"
		"		byte[] buf = new byte[1024];  // 1Kbytesずつ処理する"
		"		FileStream inStream // 入力ストリーム"
		"			= new FileStream(inFile, FileMode.Open, FileAccess.Read);"
		"		FileStream outStream // 出力ストリーム"
		"			= new FileStream(outFile, FileMode.Create);"
		"		GZipStream compStream // 圧縮ストリーム"
		"			= new GZipStream("
		"				outStream,  // 出力先となるストリームを指定"
		"				CompressionMode.Compression); // 圧縮を指定"
		"		using (inStream)"
		"		using (outStream)"
		"		using (compStream) {"
		"			while ((num = inStream.Read(buf, 0, buf.Length)) > 0) {"
		"				compStream.Write(buf, 0, num);"
		"			}"
		"		}"
		"	}"
		"}"
		"// コンパイル方法：csc gzcomp.cs"
		"// 使用方法：gzcomp ＜圧縮したいファイル＞"
		;
	int size=sizeof(buff);

	using namespace mwg::Stream;
	using namespace mwg::Compression;

	{
		printf("===== COMPRESSION TEST 1 MZIP =====\n");
		printf("original data ...\n");
		FixedMemoryStream istr(buff,size);
		istr.print();
		printf("\n");

		printf("compressing ...\n");
		VectorOStream ostr;
		mwg::Compression::MZipEncoder* enc=new mwg::Compression::MZipEncoder(istr);
		ostr<<*enc;
		delete enc;
		ostr.print_hex();
		printf("\n");

		printf("expading ...\n");
		VectorIStream str1(ostr.get_buffer());
		MZipDecoder* dec=new MZipDecoder(str1);
		ostr.clear();
		ostr<<*dec;
		delete dec;
		ostr.print();
	}

	{
		printf("===== COMPRESSION TEST 2 ARITH =====\n");
		printf("original data ...\n");
		FixedMemoryStream istr(buff,size);
		istr.print();
		printf("\n");

		printf("compressing ...\n");
		VectorOStream ostr;
		mwg::Compression::NibbleRangeEncoder enc(istr);
		ostr<<enc;
		ostr.print_hex();
		printf("\n");

		printf("expading ...\n");
		VectorIStream str1(ostr.get_buffer());
		NibbleRangeDecoder str2(str1);
		ostr.clear();
		ostr<<str2;
		ostr.print();
	}

	{
		printf("===== COMPRESSION TEST 3 =====\n");
		printf("original data ...\n");
		FixedMemoryStream istr(buff,size);
		istr.print();
		printf("\n");

		printf("compressing ...\n");
		VectorOStream ostr;
		mwg::Compression::MZipEncoder* enc=new mwg::Compression::MZipEncoder(istr);
		mwg::Compression::NibbleRangeEncoder enc2(*enc);
		ostr<<enc2;
		delete enc;
		ostr.print_hex();
		printf("\n");

		printf("expading ...\n");
		VectorIStream str1(ostr.get_buffer());
		NibbleRangeDecoder str2(str1);

		/*
		ostr.clear();
		ostr<<str2;
		ostr.print_hex();
		VectorIStream istr2(ostr.get_buffer());
		//*/

		MZipDecoder* dec=new MZipDecoder(str2);
		ostr.clear();
		ostr<<*dec;
		delete dec;
		ostr.print();
	}
#pragma endregion
}

