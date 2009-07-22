#pragma once
#include "stream.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	typedef unsigned __int16 ushort;


//====================================================================
//		圧縮形式について
//--------------------------------------------------------------------
//	<圧縮データ> : /* 廃: (B_BLK_CNT:ブロック数) */ <ブロック>+
//	const int BYTES_BLK_CNT=2;
//	<ブロック> : <ブロックRAW> | <ブロックREF>
		const int B_BLK_TYP=2;
//--------------------------------------------------------------------
//	<ブロックRAW> : (B_BLK_TYP:0) (B_RAW_LEN:データ数) (byte:データ)+
//			※ 後続する算術圧縮などを考えるとバイト単位の大きさになる方が良い。→之に関しては対策をしたのでバイト単位の大きさにならなくても良い。
		const int BLK_TYP_RAW=0;
		const int B_RAW_LEN=6;
		const int RAW_LEN_BASE=1;
		const int RAW_LEN_SUP=1+(1<<B_RAW_LEN);
//--------------------------------------------------------------------
//	<ブロックREF> : (B_BLK_TYP:1) (B_REF_ADR:<参照位置>) (B_REF_LEN:<参照長さ>)
//		<参照位置> : 0 が直前のバイトで 1 がその前のバイト
//		<参照長さ> :
//			参照長さは "ブロックRAW ヘッダの約 1B" と "ブロックREF約 2-3B" より長くなくては意味がない
//			従って、参照長さは 3 か 4 をベースとするのが良い。
		const int BLK_TYP_REF=1;
		const int B_REF_ADR=16;//12;//14;//18;//
		const int B_REF_LEN= 6;// 5;// 6;// 6;//
		const int REF_LEN_BASE=4;
		const int REF_LEN_SUP=REF_LEN_BASE+(1<<B_REF_LEN);
//--------------------------------------------------------------------
//	<ブロックDIC> : (B_BLK_TYP) (B_DIC_COD:参照コード)
		const int BLK_TYP_DIC=2;
		const int B_DIC_COD=6;
//	・	ブロック長を 8bit に保つ場合には算術圧縮に期待して、
//		辞書コードの並び替えは行わない方が良い
//	・	辞書コード長は可変にした方が良いか? 2+4n bit
//--------------------------------------------------------------------
	//****************************************************************
	//	履歴の為の補助定数
	//================================================================
	//	過去の出力履歴を配列に保持
	const int SZ_CACHE=1<<B_REF_ADR;
	const int MASK_CACHE=SZ_CACHE-1;
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
