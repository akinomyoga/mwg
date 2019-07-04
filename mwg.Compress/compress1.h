#pragma once
#include "stream.h"
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
namespace Compression{
	typedef unsigned __int16 ushort;


//====================================================================
//		���k�`���ɂ���
//--------------------------------------------------------------------
//	<���k�f�[�^> : /* �p: (B_BLK_CNT:�u���b�N��) */ <�u���b�N>+
//	const int BYTES_BLK_CNT=2;
//	<�u���b�N> : <�u���b�NRAW> | <�u���b�NREF>
		const int B_BLK_TYP=2;
//--------------------------------------------------------------------
//	<�u���b�NRAW> : (B_BLK_TYP:0) (B_RAW_LEN:�f�[�^��) (byte:�f�[�^)+
//			�� �㑱����Z�p���k�Ȃǂ��l����ƃo�C�g�P�ʂ̑傫���ɂȂ�����ǂ��B���V�Ɋւ��Ă͑΍�������̂Ńo�C�g�P�ʂ̑傫���ɂȂ�Ȃ��Ă��ǂ��B
		const int BLK_TYP_RAW=0;
		const int B_RAW_LEN=6;
		const int RAW_LEN_BASE=1;
		const int RAW_LEN_SUP=1+(1<<B_RAW_LEN);
//--------------------------------------------------------------------
//	<�u���b�NREF> : (B_BLK_TYP:1) (B_REF_ADR:<�Q�ƈʒu>) (B_REF_LEN:<�Q�ƒ���>)
//		<�Q�ƈʒu> : 0 �����O�̃o�C�g�� 1 �����̑O�̃o�C�g
//		<�Q�ƒ���> :
//			�Q�ƒ����� "�u���b�NRAW �w�b�_�̖� 1B" �� "�u���b�NREF�� 2-3B" ��蒷���Ȃ��Ă͈Ӗ����Ȃ�
//			�]���āA�Q�ƒ����� 3 �� 4 ���x�[�X�Ƃ���̂��ǂ��B
		const int BLK_TYP_REF=1;
		const int B_REF_ADR=16;//12;//14;//18;//
		const int B_REF_LEN= 6;// 5;// 6;// 6;//
		const int REF_LEN_BASE=4;
		const int REF_LEN_SUP=REF_LEN_BASE+(1<<B_REF_LEN);
//--------------------------------------------------------------------
//	<�u���b�NDIC> : (B_BLK_TYP) (B_DIC_COD:�Q�ƃR�[�h)
		const int BLK_TYP_DIC=2;
		const int B_DIC_COD=6;
//	�E	�u���b�N���� 8bit �ɕۂꍇ�ɂ͎Z�p���k�Ɋ��҂��āA
//		�����R�[�h�̕��ёւ��͍s��Ȃ������ǂ�
//	�E	�����R�[�h���͉ςɂ��������ǂ���? 2+4n bit
//--------------------------------------------------------------------
	//****************************************************************
	//	�����ׂ̈̕⏕�萔
	//================================================================
	//	�ߋ��̏o�͗�����z��ɕێ�
	const int SZ_CACHE=1<<B_REF_ADR;
	const int MASK_CACHE=SZ_CACHE-1;
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
