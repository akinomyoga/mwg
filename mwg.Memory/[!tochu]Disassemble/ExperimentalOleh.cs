using Gen=System.Collections.Generic;
using StringBuilder=System.Text.StringBuilder;

namespace mwg.Experimental{
	public unsafe class t_cmddata {
		public uint          mask;                 // Mask for first 4 bytes of the command
		public uint          code;                 // Compare masked bytes with this
		public byte           len;                  // Length of the main command code
		public Bits           bits;                 // Special bits within the command
		public ArgType        arg1,arg2,arg3;       // Types of possible arguments
		public CmdType        type;                 // C_xxx + additional information
		public string         name;                 // Symbolic name for this command

		public t_cmddata(
			uint mask,uint code,byte len,Bits bits,
			ArgType arg1,ArgType arg2,ArgType arg3,
			CmdType type,string name
		){
			this.mask=mask;
			this.code=code;
			this.len=len;
			this.bits=bits;
			this.arg1=arg1;
			this.arg2=arg2;
			this.arg3=arg3;
			this.type=type;
			this.name=name;
		}

		public bool Match(uint code){
			return ((code^this.code)&mask)==0;
		}
		public bool ArgIsSourceOrDest{
			get{
				return this.arg1==ArgType.MSO
					||this.arg1==ArgType.MDE
					||this.arg2==ArgType.MSO
					||this.arg2==ArgType.MDE;
			}
		}
		public bool IsUnknown{
			get{return this.mask==0;}
		}
		public bool Is3DNow{
			get{return (this.type&CmdType.NOW)!=0;}
		}
		public bool IsRare{
			get{return (this.type&CmdType.RARE)!=0;}
		}

		#region Data
		static Bits _0=(Bits)0;
		static Bits WW=Bits.WW;
		static Bits SS=Bits.SS;
		static Bits WS=Bits.WS;
		static Bits W3=Bits.W3;
		static Bits CC=Bits.CC;
		static Bits FF=Bits.FF;
		static Bits LL=Bits.LL;
		static Bits PR=Bits.PR;
		static Bits WP=Bits.WP;
		public static t_cmddata[] data=new t_cmddata[]{
			new t_cmddata( 0x0000FF, 0x000090, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"NOP" ),
			new t_cmddata( 0x0000FE, 0x00008A, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOV" ),
			new t_cmddata( 0x0000F8, 0x000050, 1,_0,  ArgType.RCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|(CmdType)0,				"PUSH" ),
			new t_cmddata( 0x0000FE, 0x000088, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOV" ),
			new t_cmddata( 0x0000FF, 0x0000E8, 1,_0,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.CAL|(CmdType)0,				"CALL" ),
			new t_cmddata( 0x0000FD, 0x000068, 1,SS,  ArgType.IMM,ArgType.NNN,ArgType.NNN, CmdType.PSH|(CmdType)0,				"PUSH" ),
			new t_cmddata( 0x0000FF, 0x00008D, 1,_0,  ArgType.REG,ArgType.MMA,ArgType.NNN, CmdType.CMD|(CmdType)0,				"LEA" ),
			new t_cmddata( 0x0000FF, 0x000074, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JE,JZ" ),
			new t_cmddata( 0x0000F8, 0x000058, 1,_0,  ArgType.RCM,ArgType.NNN,ArgType.NNN, CmdType.POP|(CmdType)0,				"POP" ),
			new t_cmddata( 0x0038FC, 0x000080, 1,WS,  ArgType.MRG,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ADD" ),
			new t_cmddata( 0x0000FF, 0x000075, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNZ,JNE" ),
			new t_cmddata( 0x0000FF, 0x0000EB, 1,_0,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMP|(CmdType)0,				"JMP" ),
			new t_cmddata( 0x0000FF, 0x0000E9, 1,_0,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMP|(CmdType)0,				"JMP" ),
			new t_cmddata( 0x0000FE, 0x000084, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"TEST" ),
			new t_cmddata( 0x0038FE, 0x0000C6, 1,WW,  ArgType.MRG,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MOV" ),
			new t_cmddata( 0x0000FE, 0x000032, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"XOR" ),
			new t_cmddata( 0x0000FE, 0x00003A, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMP" ),
			new t_cmddata( 0x0038FC, 0x003880, 1,WS,  ArgType.MRG,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)1,				"CMP" ),
			new t_cmddata( 0x0038FF, 0x0010FF, 1,_0,  ArgType.MRJ,ArgType.NNN,ArgType.NNN, CmdType.CAL|(CmdType)0,				"CALL" ),
			new t_cmddata( 0x0000FF, 0x0000C3, 1,_0,  ArgType.PRN,ArgType.NNN,ArgType.NNN, CmdType.RET|(CmdType)0,				"RETN,RET" ),
			new t_cmddata( 0x0000F0, 0x0000B0, 1,W3,  ArgType.RCM,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOV" ),
			new t_cmddata( 0x0000FE, 0x0000A0, 1,WW,  ArgType.RAC,ArgType.IMA,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOV" ),
			new t_cmddata( 0x00FFFF, 0x00840F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JE,JZ" ),
			new t_cmddata( 0x0000F8, 0x000040, 1,_0,  ArgType.RCM,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"INC" ),
			new t_cmddata( 0x0038FE, 0x0000F6, 1,WW,  ArgType.MRG,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)1,				"TEST" ),
			new t_cmddata( 0x0000FE, 0x0000A2, 1,WW,  ArgType.IMA,ArgType.RAC,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOV" ),
			new t_cmddata( 0x0000FE, 0x00002A, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SUB" ),
			new t_cmddata( 0x0000FF, 0x00007E, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JLE,JNG" ),
			new t_cmddata( 0x00FFFF, 0x00850F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNZ,JNE" ),
			new t_cmddata( 0x0000FF, 0x0000C2, 1,_0,  ArgType.IM2,ArgType.PRN,ArgType.NNN, CmdType.RET|(CmdType)0,				"RETN" ),
			new t_cmddata( 0x0038FF, 0x0030FF, 1,_0,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.PSH|(CmdType)1,				"PUSH" ),
			new t_cmddata( 0x0038FC, 0x000880, 1,WS,  ArgType.MRG,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)1,				"OR" ),
			new t_cmddata( 0x0038FC, 0x002880, 1,WS,  ArgType.MRG,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SUB" ),
			new t_cmddata( 0x0000F8, 0x000048, 1,_0,  ArgType.RCM,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"DEC" ),
			new t_cmddata( 0x00FFFF, 0x00BF0F, 2,_0,  ArgType.REG,ArgType.MR2,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MOVSX" ),
			new t_cmddata( 0x0000FF, 0x00007C, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JL,JNGE" ),
			new t_cmddata( 0x0000FE, 0x000002, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"ADD" ),
			new t_cmddata( 0x0038FC, 0x002080, 1,WS,  ArgType.MRG,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)1,				"AND" ),
			new t_cmddata( 0x0000FE, 0x00003C, 1,WW,  ArgType.RAC,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMP" ),
			new t_cmddata( 0x0038FF, 0x0020FF, 1,_0,  ArgType.MRJ,ArgType.NNN,ArgType.NNN, CmdType.JMP|(CmdType)0,				"JMP" ),
			new t_cmddata( 0x0038FE, 0x0010F6, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"NOT" ),
			new t_cmddata( 0x0038FE, 0x0028C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SHR" ),
			new t_cmddata( 0x0000FE, 0x000038, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMP" ),
			new t_cmddata( 0x0000FF, 0x00007D, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JGE,JNL" ),
			new t_cmddata( 0x0000FF, 0x00007F, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JG,JNLE" ),
			new t_cmddata( 0x0038FE, 0x0020C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SHL" ),
			new t_cmddata( 0x0000FE, 0x00001A, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SBB" ),
			new t_cmddata( 0x0038FE, 0x0018F6, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"NEG" ),
			new t_cmddata( 0x0000FF, 0x0000C9, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"LEAVE" ),
			new t_cmddata( 0x0000FF, 0x000060, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"&PUSHA*" ),
			new t_cmddata( 0x0038FF, 0x00008F, 1,_0,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.POP|(CmdType)1,				"POP" ),
			new t_cmddata( 0x0000FF, 0x000061, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"&POPA*" ),
			new t_cmddata( 0x0000F8, 0x000090, 1,_0,  ArgType.RAC,ArgType.RCM,ArgType.NNN, CmdType.CMD|(CmdType)0,				"XCHG" ),
			new t_cmddata( 0x0000FE, 0x000086, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"XCHG" ),
			new t_cmddata( 0x0000FE, 0x000000, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"ADD" ),
			new t_cmddata( 0x0000FE, 0x000010, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"ADC" ),
			new t_cmddata( 0x0000FE, 0x000012, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"ADC" ),
			new t_cmddata( 0x0000FE, 0x000020, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"AND" ),
			new t_cmddata( 0x0000FE, 0x000022, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"AND" ),
			new t_cmddata( 0x0000FE, 0x000008, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"OR" ),
			new t_cmddata( 0x0000FE, 0x00000A, 1,WW,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"OR" ),
			new t_cmddata( 0x0000FE, 0x000028, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SUB" ),
			new t_cmddata( 0x0000FE, 0x000018, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SBB" ),
			new t_cmddata( 0x0000FE, 0x000030, 1,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"XOR" ),
			new t_cmddata( 0x0038FC, 0x001080, 1,WS,  ArgType.MRG,ArgType.IMM,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"ADC" ),
			new t_cmddata( 0x0038FC, 0x001880, 1,WS,  ArgType.MRG,ArgType.IMM,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"SBB" ),
			new t_cmddata( 0x0038FC, 0x003080, 1,WS,  ArgType.MRG,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)1,				"XOR" ),
			new t_cmddata( 0x0000FE, 0x000004, 1,WW,  ArgType.RAC,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)0,				"ADD" ),
			new t_cmddata( 0x0000FE, 0x000014, 1,WW,  ArgType.RAC,ArgType.IMM,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"ADC" ),
			new t_cmddata( 0x0000FE, 0x000024, 1,WW,  ArgType.RAC,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)0,				"AND" ),
			new t_cmddata( 0x0000FE, 0x00000C, 1,WW,  ArgType.RAC,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)0,				"OR" ),
			new t_cmddata( 0x0000FE, 0x00002C, 1,WW,  ArgType.RAC,ArgType.IMM,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SUB" ),
			new t_cmddata( 0x0000FE, 0x00001C, 1,WW,  ArgType.RAC,ArgType.IMM,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SBB" ),
			new t_cmddata( 0x0000FE, 0x000034, 1,WW,  ArgType.RAC,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)0,				"XOR" ),
			new t_cmddata( 0x0038FE, 0x0000FE, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"INC" ),
			new t_cmddata( 0x0038FE, 0x0008FE, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"DEC" ),
			new t_cmddata( 0x0000FE, 0x0000A8, 1,WW,  ArgType.RAC,ArgType.IMU,ArgType.NNN, CmdType.CMD|(CmdType)0,				"TEST" ),
			new t_cmddata( 0x0038FE, 0x0020F6, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MUL" ),
			new t_cmddata( 0x0038FE, 0x0028F6, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"IMUL" ),
			new t_cmddata( 0x00FFFF, 0x00AF0F, 2,_0,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"IMUL" ),
			new t_cmddata( 0x0000FF, 0x00006B, 1,_0,  ArgType.REG,ArgType.MRG,ArgType.IMX, CmdType.CMD|CmdType.RARE|(CmdType)0,	"IMUL" ),
			new t_cmddata( 0x0000FF, 0x000069, 1,_0,  ArgType.REG,ArgType.MRG,ArgType.IMM, CmdType.CMD|CmdType.RARE|(CmdType)0,	"IMUL" ),
			new t_cmddata( 0x0038FE, 0x0030F6, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"DIV" ),
			new t_cmddata( 0x0038FE, 0x0038F6, 1,WW,  ArgType.MRG,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"IDIV" ),
			new t_cmddata( 0x0000FF, 0x000098, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"&CBW:CWDE" ),
			new t_cmddata( 0x0000FF, 0x000099, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"&CWD:CDQ" ),
			new t_cmddata( 0x0038FE, 0x0000D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ROL" ),
			new t_cmddata( 0x0038FE, 0x0008D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ROR" ),
			new t_cmddata( 0x0038FE, 0x0010D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"RCL" ),
			new t_cmddata( 0x0038FE, 0x0018D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"RCR" ),
			new t_cmddata( 0x0038FE, 0x0020D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SHL" ),
			new t_cmddata( 0x0038FE, 0x0028D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SHR" ),
			new t_cmddata( 0x0038FE, 0x0038D0, 1,WW,  ArgType.MRG,ArgType.C01,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SAR" ),
			new t_cmddata( 0x0038FE, 0x0000D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ROL" ),
			new t_cmddata( 0x0038FE, 0x0008D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ROR" ),
			new t_cmddata( 0x0038FE, 0x0010D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"RCL" ),
			new t_cmddata( 0x0038FE, 0x0018D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"RCR" ),
			new t_cmddata( 0x0038FE, 0x0020D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SHL" ),
			new t_cmddata( 0x0038FE, 0x0028D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SHR" ),
			new t_cmddata( 0x0038FE, 0x0038D2, 1,WW,  ArgType.MRG,ArgType.RCL,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SAR" ),
			new t_cmddata( 0x0038FE, 0x0000C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ROL" ),
			new t_cmddata( 0x0038FE, 0x0008C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"ROR" ),
			new t_cmddata( 0x0038FE, 0x0010C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"RCL" ),
			new t_cmddata( 0x0038FE, 0x0018C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"RCR" ),
			new t_cmddata( 0x0038FE, 0x0038C0, 1,WW,  ArgType.MRG,ArgType.IMS,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SAR" ),
			new t_cmddata( 0x0000FF, 0x000070, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JO" ),
			new t_cmddata( 0x0000FF, 0x000071, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNO" ),
			new t_cmddata( 0x0000FF, 0x000072, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JB,JC" ),
			new t_cmddata( 0x0000FF, 0x000073, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNB,JNC" ),
			new t_cmddata( 0x0000FF, 0x000076, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JBE,JNA" ),
			new t_cmddata( 0x0000FF, 0x000077, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JA,JNBE" ),
			new t_cmddata( 0x0000FF, 0x000078, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JS" ),
			new t_cmddata( 0x0000FF, 0x000079, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNS" ),
			new t_cmddata( 0x0000FF, 0x00007A, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|CmdType.RARE|(CmdType)0,	"JPE,JP" ),
			new t_cmddata( 0x0000FF, 0x00007B, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|CmdType.RARE|(CmdType)0,	"JPO,JNP" ),
			new t_cmddata( 0x0000FF, 0x0000E3, 1,_0,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|CmdType.RARE|(CmdType)0,	"$JCXZ:JECXZ" ),
			new t_cmddata( 0x00FFFF, 0x00800F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JO" ),
			new t_cmddata( 0x00FFFF, 0x00810F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNO" ),
			new t_cmddata( 0x00FFFF, 0x00820F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JB,JC" ),
			new t_cmddata( 0x00FFFF, 0x00830F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNB,JNC" ),
			new t_cmddata( 0x00FFFF, 0x00860F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JBE,JNA" ),
			new t_cmddata( 0x00FFFF, 0x00870F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JA,JNBE" ),
			new t_cmddata( 0x00FFFF, 0x00880F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JS" ),
			new t_cmddata( 0x00FFFF, 0x00890F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JNS" ),
			new t_cmddata( 0x00FFFF, 0x008A0F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|CmdType.RARE|(CmdType)0,	"JPE,JP" ),
			new t_cmddata( 0x00FFFF, 0x008B0F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|CmdType.RARE|(CmdType)0,	"JPO,JNP" ),
			new t_cmddata( 0x00FFFF, 0x008C0F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JL,JNGE" ),
			new t_cmddata( 0x00FFFF, 0x008D0F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JGE,JNL" ),
			new t_cmddata( 0x00FFFF, 0x008E0F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JLE,JNG" ),
			new t_cmddata( 0x00FFFF, 0x008F0F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JG,JNLE" ),
			new t_cmddata( 0x0000FF, 0x0000F8, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CLC" ),
			new t_cmddata( 0x0000FF, 0x0000F9, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"STC" ),
			new t_cmddata( 0x0000FF, 0x0000F5, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"CMC" ),
			new t_cmddata( 0x0000FF, 0x0000FC, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CLD" ),
			new t_cmddata( 0x0000FF, 0x0000FD, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"STD" ),
			new t_cmddata( 0x0000FF, 0x0000FA, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"CLI" ),
			new t_cmddata( 0x0000FF, 0x0000FB, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"STI" ),
			new t_cmddata( 0x0000FF, 0x00008C, 1,FF,  ArgType.MRG,ArgType.SGM,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"MOV" ),
			new t_cmddata( 0x0000FF, 0x00008E, 1,FF,  ArgType.SGM,ArgType.MRG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"MOV" ),
			new t_cmddata( 0x0000FE, 0x0000A6, 1,WW,  ArgType.MSO,ArgType.MDE,ArgType.NNN, CmdType.CMD|(CmdType)1,				"CMPS" ),
			new t_cmddata( 0x0000FE, 0x0000AC, 1,WW,  ArgType.MSO,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"LODS" ),
			new t_cmddata( 0x0000FE, 0x0000A4, 1,WW,  ArgType.MDE,ArgType.MSO,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MOVS" ),
			new t_cmddata( 0x0000FE, 0x0000AE, 1,WW,  ArgType.MDE,ArgType.PAC,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SCAS" ),
			new t_cmddata( 0x0000FE, 0x0000AA, 1,WW,  ArgType.MDE,ArgType.PAC,ArgType.NNN, CmdType.CMD|(CmdType)1,				"STOS" ),
			new t_cmddata( 0x00FEFF, 0x00A4F3, 1,WW,  ArgType.MDE,ArgType.MSO,ArgType.PCX, CmdType.REP|(CmdType)1,				"REP MOVS" ),
			new t_cmddata( 0x00FEFF, 0x00ACF3, 1,WW,  ArgType.MSO,ArgType.PAC,ArgType.PCX, CmdType.REP|CmdType.RARE|(CmdType)1,	"REP LODS" ),
			new t_cmddata( 0x00FEFF, 0x00AAF3, 1,WW,  ArgType.MDE,ArgType.PAC,ArgType.PCX, CmdType.REP|(CmdType)1,				"REP STOS" ),
			new t_cmddata( 0x00FEFF, 0x00A6F3, 1,WW,  ArgType.MDE,ArgType.MSO,ArgType.PCX, CmdType.REP|(CmdType)1,				"REPE CMPS" ),
			new t_cmddata( 0x00FEFF, 0x00AEF3, 1,WW,  ArgType.MDE,ArgType.PAC,ArgType.PCX, CmdType.REP|(CmdType)1,				"REPE SCAS" ),
			new t_cmddata( 0x00FEFF, 0x00A6F2, 1,WW,  ArgType.MDE,ArgType.MSO,ArgType.PCX, CmdType.REP|(CmdType)1,				"REPNE CMPS" ),
			new t_cmddata( 0x00FEFF, 0x00AEF2, 1,WW,  ArgType.MDE,ArgType.PAC,ArgType.PCX, CmdType.REP|(CmdType)1,				"REPNE SCAS" ),
			new t_cmddata( 0x0000FF, 0x0000EA, 1,_0,  ArgType.JMF,ArgType.NNN,ArgType.NNN, CmdType.JMP|CmdType.RARE|(CmdType)0,	"JMP" ),
			new t_cmddata( 0x0038FF, 0x0028FF, 1,_0,  ArgType.MMS,ArgType.NNN,ArgType.NNN, CmdType.JMP|CmdType.RARE|(CmdType)1,	"JMP" ),
			new t_cmddata( 0x0000FF, 0x00009A, 1,_0,  ArgType.JMF,ArgType.NNN,ArgType.NNN, CmdType.CAL|CmdType.RARE|(CmdType)0,	"CALL" ),
			new t_cmddata( 0x0038FF, 0x0018FF, 1,_0,  ArgType.MMS,ArgType.NNN,ArgType.NNN, CmdType.CAL|CmdType.RARE|(CmdType)1,	"CALL" ),
			new t_cmddata( 0x0000FF, 0x0000CB, 1,_0,  ArgType.PRF,ArgType.NNN,ArgType.NNN, CmdType.RET|CmdType.RARE|(CmdType)0,	"RETF" ),
			new t_cmddata( 0x0000FF, 0x0000CA, 1,_0,  ArgType.IM2,ArgType.PRF,ArgType.NNN, CmdType.RET|CmdType.RARE|(CmdType)0,	"RETF" ),
			new t_cmddata( 0x00FFFF, 0x00A40F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.IMS, CmdType.CMD|(CmdType)0,				"SHLD" ),
			new t_cmddata( 0x00FFFF, 0x00AC0F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.IMS, CmdType.CMD|(CmdType)0,				"SHRD" ),
			new t_cmddata( 0x00FFFF, 0x00A50F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.RCL, CmdType.CMD|(CmdType)0,				"SHLD" ),
			new t_cmddata( 0x00FFFF, 0x00AD0F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.RCL, CmdType.CMD|(CmdType)0,				"SHRD" ),
			new t_cmddata( 0x00F8FF, 0x00C80F, 2,_0,  ArgType.RCM,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"BSWAP" ),
			new t_cmddata( 0x00FEFF, 0x00C00F, 2,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"XADD" ),
			new t_cmddata( 0x0000FF, 0x0000E2, 1,LL,  ArgType.JOB,ArgType.PCX,ArgType.NNN, CmdType.JMC|(CmdType)0,				"$LOOP*" ),
			new t_cmddata( 0x0000FF, 0x0000E1, 1,LL,  ArgType.JOB,ArgType.PCX,ArgType.NNN, CmdType.JMC|(CmdType)0,				"$LOOP*E" ),
			new t_cmddata( 0x0000FF, 0x0000E0, 1,LL,  ArgType.JOB,ArgType.PCX,ArgType.NNN, CmdType.JMC|(CmdType)0,				"$LOOP*NE" ),
			new t_cmddata( 0x0000FF, 0x0000C8, 1,_0,  ArgType.IM2,ArgType.IM1,ArgType.NNN, CmdType.CMD|(CmdType)0,				"ENTER" ),
			new t_cmddata( 0x0000FE, 0x0000E4, 1,WP,  ArgType.RAC,ArgType.IM1,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"IN" ),
			new t_cmddata( 0x0000FE, 0x0000EC, 1,WP,  ArgType.RAC,ArgType.RDX,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"IN" ),
			new t_cmddata( 0x0000FE, 0x0000E6, 1,WP,  ArgType.IM1,ArgType.RAC,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"OUT" ),
			new t_cmddata( 0x0000FE, 0x0000EE, 1,WP,  ArgType.RDX,ArgType.RAC,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"OUT" ),
			new t_cmddata( 0x0000FE, 0x00006C, 1,WP,  ArgType.MDE,ArgType.RDX,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"INS" ),
			new t_cmddata( 0x0000FE, 0x00006E, 1,WP,  ArgType.RDX,ArgType.MDE,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"OUTS" ),
			new t_cmddata( 0x00FEFF, 0x006CF3, 1,WP,  ArgType.MDE,ArgType.RDX,ArgType.PCX, CmdType.REP|CmdType.RARE|(CmdType)1,	"REP INS" ),
			new t_cmddata( 0x00FEFF, 0x006EF3, 1,WP,  ArgType.RDX,ArgType.MDE,ArgType.PCX, CmdType.REP|CmdType.RARE|(CmdType)1,	"REP OUTS" ),
			new t_cmddata( 0x0000FF, 0x000037, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"AAA" ),
			new t_cmddata( 0x0000FF, 0x00003F, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"AAS" ),
			new t_cmddata( 0x00FFFF, 0x000AD4, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"AAM" ),
			new t_cmddata( 0x0000FF, 0x0000D4, 1,_0,  ArgType.IM1,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"AAM" ),
			new t_cmddata( 0x00FFFF, 0x000AD5, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"AAD" ),
			new t_cmddata( 0x0000FF, 0x0000D5, 1,_0,  ArgType.IM1,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"AAD" ),
			new t_cmddata( 0x0000FF, 0x000027, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"DAA" ),
			new t_cmddata( 0x0000FF, 0x00002F, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"DAS" ),
			new t_cmddata( 0x0000FF, 0x0000F4, 1,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.PRI|CmdType.RARE|(CmdType)0,	"HLT" ),
			new t_cmddata( 0x0000FF, 0x00000E, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|CmdType.RARE|(CmdType)0,	"PUSH" ),
			new t_cmddata( 0x0000FF, 0x000016, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|CmdType.RARE|(CmdType)0,	"PUSH" ),
			new t_cmddata( 0x0000FF, 0x00001E, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|CmdType.RARE|(CmdType)0,	"PUSH" ),
			new t_cmddata( 0x0000FF, 0x000006, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|CmdType.RARE|(CmdType)0,	"PUSH" ),
			new t_cmddata( 0x00FFFF, 0x00A00F, 2,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|CmdType.RARE|(CmdType)0,	"PUSH" ),
			new t_cmddata( 0x00FFFF, 0x00A80F, 2,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.PSH|CmdType.RARE|(CmdType)0,	"PUSH" ),
			new t_cmddata( 0x0000FF, 0x00001F, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.POP|CmdType.RARE|(CmdType)0,	"POP" ),
			new t_cmddata( 0x0000FF, 0x000007, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.POP|CmdType.RARE|(CmdType)0,	"POP" ),
			new t_cmddata( 0x0000FF, 0x000017, 1,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.POP|CmdType.RARE|(CmdType)0,	"POP" ),
			new t_cmddata( 0x00FFFF, 0x00A10F, 2,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.POP|CmdType.RARE|(CmdType)0,	"POP" ),
			new t_cmddata( 0x00FFFF, 0x00A90F, 2,_0,  ArgType.SCM,ArgType.NNN,ArgType.NNN, CmdType.POP|CmdType.RARE|(CmdType)0,	"POP" ),
			new t_cmddata( 0x0000FF, 0x0000D7, 1,_0,  ArgType.MXL,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"XLAT" ),
			new t_cmddata( 0x00FFFF, 0x00BE0F, 2,_0,  ArgType.REG,ArgType.MR1,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MOVSX" ),
			new t_cmddata( 0x00FFFF, 0x00B60F, 2,_0,  ArgType.REG,ArgType.MR1,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MOVZX" ),
			new t_cmddata( 0x00FFFF, 0x00B70F, 2,_0,  ArgType.REG,ArgType.MR2,ArgType.NNN, CmdType.CMD|(CmdType)1,				"MOVZX" ),
			new t_cmddata( 0x0000FF, 0x00009B, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"WAIT" ),
			new t_cmddata( 0x0000FF, 0x00009F, 1,_0,  ArgType.PAH,ArgType.PFL,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LAHF" ),
			new t_cmddata( 0x0000FF, 0x00009E, 1,_0,  ArgType.PFL,ArgType.PAH,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SAHF" ),
			new t_cmddata( 0x0000FF, 0x00009C, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.PSH|(CmdType)0,				"&PUSHF*" ),
			new t_cmddata( 0x0000FF, 0x00009D, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLG|(CmdType)0,				"&POPF*" ),
			new t_cmddata( 0x0000FF, 0x0000CD, 1,_0,  ArgType.IM1,ArgType.NNN,ArgType.NNN, CmdType.CAL|CmdType.RARE|(CmdType)0,	"INT" ),
			new t_cmddata( 0x0000FF, 0x0000CC, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CAL|CmdType.RARE|(CmdType)0,	"INT3" ),
			new t_cmddata( 0x0000FF, 0x0000CE, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CAL|CmdType.RARE|(CmdType)0,	"INTO" ),
			new t_cmddata( 0x0000FF, 0x0000CF, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.RTF|CmdType.RARE|(CmdType)0,	"&IRET*" ),
			new t_cmddata( 0x00FFFF, 0x00900F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETO" ),
			new t_cmddata( 0x00FFFF, 0x00910F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETNO" ),
			new t_cmddata( 0x00FFFF, 0x00920F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETB,SETC" ),
			new t_cmddata( 0x00FFFF, 0x00930F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETNB,SETNC" ),
			new t_cmddata( 0x00FFFF, 0x00940F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETE,SETZ" ),
			new t_cmddata( 0x00FFFF, 0x00950F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETNE,SETNZ" ),
			new t_cmddata( 0x00FFFF, 0x00960F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETBE,SETNA" ),
			new t_cmddata( 0x00FFFF, 0x00970F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETA,SETNBE" ),
			new t_cmddata( 0x00FFFF, 0x00980F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETS" ),
			new t_cmddata( 0x00FFFF, 0x00990F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETNS" ),
			new t_cmddata( 0x00FFFF, 0x009A0F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SETPE,SETP" ),
			new t_cmddata( 0x00FFFF, 0x009B0F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SETPO,SETNP" ),
			new t_cmddata( 0x00FFFF, 0x009C0F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETL,SETNGE" ),
			new t_cmddata( 0x00FFFF, 0x009D0F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETGE,SETNL" ),
			new t_cmddata( 0x00FFFF, 0x009E0F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETLE,SETNG" ),
			new t_cmddata( 0x00FFFF, 0x009F0F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SETG,SETNLE" ),
			new t_cmddata( 0x38FFFF, 0x20BA0F, 2,_0,  ArgType.MRG,ArgType.IM1,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BT" ),
			new t_cmddata( 0x38FFFF, 0x28BA0F, 2,_0,  ArgType.MRG,ArgType.IM1,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BTS" ),
			new t_cmddata( 0x38FFFF, 0x30BA0F, 2,_0,  ArgType.MRG,ArgType.IM1,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BTR" ),
			new t_cmddata( 0x38FFFF, 0x38BA0F, 2,_0,  ArgType.MRG,ArgType.IM1,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BTC" ),
			new t_cmddata( 0x00FFFF, 0x00A30F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BT" ),
			new t_cmddata( 0x00FFFF, 0x00AB0F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BTS" ),
			new t_cmddata( 0x00FFFF, 0x00B30F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BTR" ),
			new t_cmddata( 0x00FFFF, 0x00BB0F, 2,_0,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"BTC" ),
			new t_cmddata( 0x0000FF, 0x0000C5, 1,_0,  ArgType.REG,ArgType.MML,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LDS" ),
			new t_cmddata( 0x0000FF, 0x0000C4, 1,_0,  ArgType.REG,ArgType.MML,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LES" ),
			new t_cmddata( 0x00FFFF, 0x00B40F, 2,_0,  ArgType.REG,ArgType.MML,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LFS" ),
			new t_cmddata( 0x00FFFF, 0x00B50F, 2,_0,  ArgType.REG,ArgType.MML,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LGS" ),
			new t_cmddata( 0x00FFFF, 0x00B20F, 2,_0,  ArgType.REG,ArgType.MML,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LSS" ),
			new t_cmddata( 0x0000FF, 0x000063, 1,_0,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"ARPL" ),
			new t_cmddata( 0x0000FF, 0x000062, 1,_0,  ArgType.REG,ArgType.MMB,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"BOUND" ),
			new t_cmddata( 0x00FFFF, 0x00BC0F, 2,_0,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"BSF" ),
			new t_cmddata( 0x00FFFF, 0x00BD0F, 2,_0,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"BSR" ),
			new t_cmddata( 0x00FFFF, 0x00060F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"CLTS" ),
			new t_cmddata( 0x00FFFF, 0x00400F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVO" ),
			new t_cmddata( 0x00FFFF, 0x00410F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVNO" ),
			new t_cmddata( 0x00FFFF, 0x00420F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVB,CMOVC" ),
			new t_cmddata( 0x00FFFF, 0x00430F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVNB,CMOVNC" ),
			new t_cmddata( 0x00FFFF, 0x00440F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVE,CMOVZ" ),
			new t_cmddata( 0x00FFFF, 0x00450F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVNE,CMOVNZ" ),
			new t_cmddata( 0x00FFFF, 0x00460F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVBE,CMOVNA" ),
			new t_cmddata( 0x00FFFF, 0x00470F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVA,CMOVNBE" ),
			new t_cmddata( 0x00FFFF, 0x00480F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVS" ),
			new t_cmddata( 0x00FFFF, 0x00490F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVNS" ),
			new t_cmddata( 0x00FFFF, 0x004A0F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVPE,CMOVP" ),
			new t_cmddata( 0x00FFFF, 0x004B0F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVPO,CMOVNP" ),
			new t_cmddata( 0x00FFFF, 0x004C0F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVL,CMOVNGE" ),
			new t_cmddata( 0x00FFFF, 0x004D0F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVGE,CMOVNL" ),
			new t_cmddata( 0x00FFFF, 0x004E0F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVLE,CMOVNG" ),
			new t_cmddata( 0x00FFFF, 0x004F0F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVG,CMOVNLE" ),
			new t_cmddata( 0x00FEFF, 0x00B00F, 2,WW,  ArgType.MRG,ArgType.REG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"CMPXCHG" ),
			new t_cmddata( 0x38FFFF, 0x08C70F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)1,	"CMPXCHG8B" ),
			new t_cmddata( 0x00FFFF, 0x00A20F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CPUID" ),
			new t_cmddata( 0x00FFFF, 0x00080F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"INVD" ),
			new t_cmddata( 0x00FFFF, 0x00020F, 2,_0,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LAR" ),
			new t_cmddata( 0x00FFFF, 0x00030F, 2,_0,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LSL" ),
			new t_cmddata( 0x38FFFF, 0x38010F, 2,PR,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"INVLPG" ),
			new t_cmddata( 0x00FFFF, 0x00090F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"WBINVD" ),
			new t_cmddata( 0x38FFFF, 0x10010F, 2,PR,  ArgType.MM6,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LGDT" ),
			new t_cmddata( 0x38FFFF, 0x00010F, 2,_0,  ArgType.MM6,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SGDT" ),
			new t_cmddata( 0x38FFFF, 0x18010F, 2,PR,  ArgType.MM6,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LIDT" ),
			new t_cmddata( 0x38FFFF, 0x08010F, 2,_0,  ArgType.MM6,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SIDT" ),
			new t_cmddata( 0x38FFFF, 0x10000F, 2,PR,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LLDT" ),
			new t_cmddata( 0x38FFFF, 0x00000F, 2,_0,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SLDT" ),
			new t_cmddata( 0x38FFFF, 0x18000F, 2,PR,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LTR" ),
			new t_cmddata( 0x38FFFF, 0x08000F, 2,_0,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"STR" ),
			new t_cmddata( 0x38FFFF, 0x30010F, 2,PR,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"LMSW" ),
			new t_cmddata( 0x38FFFF, 0x20010F, 2,_0,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SMSW" ),
			new t_cmddata( 0x38FFFF, 0x20000F, 2,_0,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"VERR" ),
			new t_cmddata( 0x38FFFF, 0x28000F, 2,_0,  ArgType.MR2,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"VERW" ),
			new t_cmddata( 0xC0FFFF, 0xC0220F, 2,PR,  ArgType.CRX,ArgType.RR4,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"MOV" ),
			new t_cmddata( 0xC0FFFF, 0xC0200F, 2,_0,  ArgType.RR4,ArgType.CRX,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"MOV" ),
			new t_cmddata( 0xC0FFFF, 0xC0230F, 2,PR,  ArgType.DRX,ArgType.RR4,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"MOV" ),
			new t_cmddata( 0xC0FFFF, 0xC0210F, 2,PR,  ArgType.RR4,ArgType.DRX,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"MOV" ),
			new t_cmddata( 0x00FFFF, 0x00310F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"RDTSC" ),
			new t_cmddata( 0x00FFFF, 0x00320F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"RDMSR" ),
			new t_cmddata( 0x00FFFF, 0x00300F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"WRMSR" ),
			new t_cmddata( 0x00FFFF, 0x00330F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"RDPMC" ),
			new t_cmddata( 0x00FFFF, 0x00AA0F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.RTF|CmdType.RARE|(CmdType)0,	"RSM" ),
			new t_cmddata( 0x00FFFF, 0x000B0F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"UD2" ),
			new t_cmddata( 0x00FFFF, 0x00340F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SYSENTER" ),
			new t_cmddata( 0x00FFFF, 0x00350F, 2,PR,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SYSEXIT" ),
			new t_cmddata( 0x0000FF, 0x0000D6, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"SALC" ),
			// FPU instructions. Never change the order of instructions!
			new t_cmddata( 0x00FFFF, 0x00F0D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"F2XM1" ),
			new t_cmddata( 0x00FFFF, 0x00E0D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCHS" ),
			new t_cmddata( 0x00FFFF, 0x00E1D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FABS" ),
			new t_cmddata( 0x00FFFF, 0x00E2DB, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCLEX" ),
			new t_cmddata( 0x00FFFF, 0x00E3DB, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FINIT" ),
			new t_cmddata( 0x00FFFF, 0x00F6D9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDECSTP" ),
			new t_cmddata( 0x00FFFF, 0x00F7D9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FINCSTP" ),
			new t_cmddata( 0x00FFFF, 0x00E4D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FTST" ),
			new t_cmddata( 0x00FFFF, 0x00FAD9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSQRT" ),
			new t_cmddata( 0x00FFFF, 0x00FED9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSIN" ),
			new t_cmddata( 0x00FFFF, 0x00FFD9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCOS" ),
			new t_cmddata( 0x00FFFF, 0x00FBD9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSINCOS" ),
			new t_cmddata( 0x00FFFF, 0x00F2D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FPTAN" ),
			new t_cmddata( 0x00FFFF, 0x00F3D9, 2,_0,  ArgType.PS0,ArgType.PS1,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FPATAN" ),
			new t_cmddata( 0x00FFFF, 0x00F8D9, 2,_0,  ArgType.PS1,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FPREM" ),
			new t_cmddata( 0x00FFFF, 0x00F5D9, 2,_0,  ArgType.PS1,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FPREM1" ),
			new t_cmddata( 0x00FFFF, 0x00F1D9, 2,_0,  ArgType.PS0,ArgType.PS1,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FYL2X" ),
			new t_cmddata( 0x00FFFF, 0x00F9D9, 2,_0,  ArgType.PS0,ArgType.PS1,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FYL2XP1" ),
			new t_cmddata( 0x00FFFF, 0x00FCD9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FRNDINT" ),
			new t_cmddata( 0x00FFFF, 0x00E8D9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLD1" ),
			new t_cmddata( 0x00FFFF, 0x00E9D9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDL2T" ),
			new t_cmddata( 0x00FFFF, 0x00EAD9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDL2E" ),
			new t_cmddata( 0x00FFFF, 0x00EBD9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDPI" ),
			new t_cmddata( 0x00FFFF, 0x00ECD9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDLG2" ),
			new t_cmddata( 0x00FFFF, 0x00EDD9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDLN2" ),
			new t_cmddata( 0x00FFFF, 0x00EED9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDZ" ),
			new t_cmddata( 0x00FFFF, 0x00FDD9, 2,_0,  ArgType.PS0,ArgType.PS1,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSCALE" ),
			new t_cmddata( 0x00FFFF, 0x00D0D9, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FNOP" ),
			new t_cmddata( 0x00FFFF, 0x00E0DF, 2,FF,  ArgType.RAX,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSTSW" ),
			new t_cmddata( 0x00FFFF, 0x00E5D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FXAM" ),
			new t_cmddata( 0x00FFFF, 0x00F4D9, 2,_0,  ArgType.PS0,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FXTRACT" ),
			new t_cmddata( 0x00FFFF, 0x00D9DE, 2,_0,  ArgType.PS0,ArgType.PS1,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCOMPP" ),
			new t_cmddata( 0x00FFFF, 0x00E9DA, 2,_0,  ArgType.PS0,ArgType.PS1,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FUCOMPP" ),
			new t_cmddata( 0x00F8FF, 0x00C0DD, 2,_0,  ArgType.RST,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FFREE" ),
			new t_cmddata( 0x00F8FF, 0x00C0DA, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVB" ),
			new t_cmddata( 0x00F8FF, 0x00C8DA, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVE" ),
			new t_cmddata( 0x00F8FF, 0x00D0DA, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVBE" ),
			new t_cmddata( 0x00F8FF, 0x00D8DA, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVU" ),
			new t_cmddata( 0x00F8FF, 0x00C0DB, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVNB" ),
			new t_cmddata( 0x00F8FF, 0x00C8DB, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVNE" ),
			new t_cmddata( 0x00F8FF, 0x00D0DB, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVNBE" ),
			new t_cmddata( 0x00F8FF, 0x00D8DB, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCMOVNU" ),
			new t_cmddata( 0x00F8FF, 0x00F0DB, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCOMI" ),
			new t_cmddata( 0x00F8FF, 0x00F0DF, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCOMIP" ),
			new t_cmddata( 0x00F8FF, 0x00E8DB, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FUCOMI" ),
			new t_cmddata( 0x00F8FF, 0x00E8DF, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FUCOMIP" ),
			new t_cmddata( 0x00F8FF, 0x00C0D8, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FADD" ),
			new t_cmddata( 0x00F8FF, 0x00C0DC, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FADD" ),
			new t_cmddata( 0x00F8FF, 0x00C0DE, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FADDP" ),
			new t_cmddata( 0x00F8FF, 0x00E0D8, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSUB" ),
			new t_cmddata( 0x00F8FF, 0x00E8DC, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSUB" ),
			new t_cmddata( 0x00F8FF, 0x00E8DE, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSUBP" ),
			new t_cmddata( 0x00F8FF, 0x00E8D8, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSUBR" ),
			new t_cmddata( 0x00F8FF, 0x00E0DC, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSUBR" ),
			new t_cmddata( 0x00F8FF, 0x00E0DE, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSUBRP" ),
			new t_cmddata( 0x00F8FF, 0x00C8D8, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FMUL" ),
			new t_cmddata( 0x00F8FF, 0x00C8DC, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FMUL" ),
			new t_cmddata( 0x00F8FF, 0x00C8DE, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FMULP" ),
			new t_cmddata( 0x00F8FF, 0x00D0D8, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCOM" ),
			new t_cmddata( 0x00F8FF, 0x00D8D8, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FCOMP" ),
			new t_cmddata( 0x00F8FF, 0x00E0DD, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FUCOM" ),
			new t_cmddata( 0x00F8FF, 0x00E8DD, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FUCOMP" ),
			new t_cmddata( 0x00F8FF, 0x00F0D8, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDIV" ),
			new t_cmddata( 0x00F8FF, 0x00F8DC, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDIV" ),
			new t_cmddata( 0x00F8FF, 0x00F8DE, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDIVP" ),
			new t_cmddata( 0x00F8FF, 0x00F8D8, 2,_0,  ArgType.RS0,ArgType.RST,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDIVR" ),
			new t_cmddata( 0x00F8FF, 0x00F0DC, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDIVR" ),
			new t_cmddata( 0x00F8FF, 0x00F0DE, 2,_0,  ArgType.RST,ArgType.RS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDIVRP" ),
			new t_cmddata( 0x00F8FF, 0x00C0D9, 2,_0,  ArgType.RST,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLD" ),
			new t_cmddata( 0x00F8FF, 0x00D0DD, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FST" ),
			new t_cmddata( 0x00F8FF, 0x00D8DD, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSTP" ),
			new t_cmddata( 0x00F8FF, 0x00C8D9, 2,_0,  ArgType.RST,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FXCH" ),
			new t_cmddata( 0x0038FF, 0x0000D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FADD" ),
			new t_cmddata( 0x0038FF, 0x0000DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FADD" ),
			new t_cmddata( 0x0038FF, 0x0000DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIADD" ),
			new t_cmddata( 0x0038FF, 0x0000DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIADD" ),
			new t_cmddata( 0x0038FF, 0x0020D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSUB" ),
			new t_cmddata( 0x0038FF, 0x0020DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSUB" ),
			new t_cmddata( 0x0038FF, 0x0020DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISUB" ),
			new t_cmddata( 0x0038FF, 0x0020DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISUB" ),
			new t_cmddata( 0x0038FF, 0x0028D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSUBR" ),
			new t_cmddata( 0x0038FF, 0x0028DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSUBR" ),
			new t_cmddata( 0x0038FF, 0x0028DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISUBR" ),
			new t_cmddata( 0x0038FF, 0x0028DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISUBR" ),
			new t_cmddata( 0x0038FF, 0x0008D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FMUL" ),
			new t_cmddata( 0x0038FF, 0x0008DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FMUL" ),
			new t_cmddata( 0x0038FF, 0x0008DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIMUL" ),
			new t_cmddata( 0x0038FF, 0x0008DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIMUL" ),
			new t_cmddata( 0x0038FF, 0x0010D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FCOM" ),
			new t_cmddata( 0x0038FF, 0x0010DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FCOM" ),
			new t_cmddata( 0x0038FF, 0x0018D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FCOMP" ),
			new t_cmddata( 0x0038FF, 0x0018DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FCOMP" ),
			new t_cmddata( 0x0038FF, 0x0030D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FDIV" ),
			new t_cmddata( 0x0038FF, 0x0030DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FDIV" ),
			new t_cmddata( 0x0038FF, 0x0030DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIDIV" ),
			new t_cmddata( 0x0038FF, 0x0030DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIDIV" ),
			new t_cmddata( 0x0038FF, 0x0038D8, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FDIVR" ),
			new t_cmddata( 0x0038FF, 0x0038DC, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FDIVR" ),
			new t_cmddata( 0x0038FF, 0x0038DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIDIVR" ),
			new t_cmddata( 0x0038FF, 0x0038DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIDIVR" ),
			new t_cmddata( 0x0038FF, 0x0020DF, 1,_0,  ArgType.MDA,ArgType.NNN,ArgType.NNN, CmdType.FLT|CmdType.RARE|(CmdType)1,	"FBLD" ),
			new t_cmddata( 0x0038FF, 0x0030DF, 1,_0,  ArgType.MDA,ArgType.PS0,ArgType.NNN, CmdType.FLT|CmdType.RARE|(CmdType)1,	"FBSTP" ),
			new t_cmddata( 0x0038FF, 0x0010DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FICOM" ),
			new t_cmddata( 0x0038FF, 0x0010DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FICOM" ),
			new t_cmddata( 0x0038FF, 0x0018DE, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FICOMP" ),
			new t_cmddata( 0x0038FF, 0x0018DA, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FICOMP" ),
			new t_cmddata( 0x0038FF, 0x0000DF, 1,_0,  ArgType.MD2,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FILD" ),
			new t_cmddata( 0x0038FF, 0x0000DB, 1,_0,  ArgType.MD4,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FILD" ),
			new t_cmddata( 0x0038FF, 0x0028DF, 1,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FILD" ),
			new t_cmddata( 0x0038FF, 0x0010DF, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIST" ),
			new t_cmddata( 0x0038FF, 0x0010DB, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FIST" ),
			new t_cmddata( 0x0038FF, 0x0018DF, 1,_0,  ArgType.MD2,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISTP" ),
			new t_cmddata( 0x0038FF, 0x0018DB, 1,_0,  ArgType.MD4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISTP" ),
			new t_cmddata( 0x0038FF, 0x0038DF, 1,_0,  ArgType.MD8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FISTP" ),
			new t_cmddata( 0x0038FF, 0x0000D9, 1,_0,  ArgType.MF4,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FLD" ),
			new t_cmddata( 0x0038FF, 0x0000DD, 1,_0,  ArgType.MF8,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FLD" ),
			new t_cmddata( 0x0038FF, 0x0028DB, 1,_0,  ArgType.MFA,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FLD" ),
			new t_cmddata( 0x0038FF, 0x0010D9, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FST" ),
			new t_cmddata( 0x0038FF, 0x0010DD, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FST" ),
			new t_cmddata( 0x0038FF, 0x0018D9, 1,_0,  ArgType.MF4,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSTP" ),
			new t_cmddata( 0x0038FF, 0x0018DD, 1,_0,  ArgType.MF8,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSTP" ),
			new t_cmddata( 0x0038FF, 0x0038DB, 1,_0,  ArgType.MFA,ArgType.PS0,ArgType.NNN, CmdType.FLT|(CmdType)1,				"FSTP" ),
			new t_cmddata( 0x0038FF, 0x0028D9, 1,_0,  ArgType.MB2,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDCW" ),
			new t_cmddata( 0x0038FF, 0x0038D9, 1,_0,  ArgType.MB2,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSTCW" ),
			new t_cmddata( 0x0038FF, 0x0020D9, 1,_0,  ArgType.MFE,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FLDENV" ),
			new t_cmddata( 0x0038FF, 0x0030D9, 1,_0,  ArgType.MFE,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSTENV" ),
			new t_cmddata( 0x0038FF, 0x0020DD, 1,_0,  ArgType.MFS,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FRSTOR" ),
			new t_cmddata( 0x0038FF, 0x0030DD, 1,_0,  ArgType.MFS,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSAVE" ),
			new t_cmddata( 0x0038FF, 0x0038DD, 1,_0,  ArgType.MB2,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FSTSW" ),
			new t_cmddata( 0x38FFFF, 0x08AE0F, 2,_0,  ArgType.MFX,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FXRSTOR" ),
			new t_cmddata( 0x38FFFF, 0x00AE0F, 2,_0,  ArgType.MFX,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FXSAVE" ),
			new t_cmddata( 0x00FFFF, 0x00E0DB, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FENI" ),
			new t_cmddata( 0x00FFFF, 0x00E1DB, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.FLT|(CmdType)0,				"FDISI" ),
			// MMX instructions. Length of MMX operand fields (in bytes) is added to the
			// type, length of 0 means 8-byte MMX operand.
			new t_cmddata( 0x00FFFF, 0x00770F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"EMMS" ),
			new t_cmddata( 0x00FFFF, 0x006E0F, 2,_0,  ArgType.RMX,ArgType.MR4,ArgType.NNN, CmdType.MMX|(CmdType)0,				"MOVD" ),
			new t_cmddata( 0x00FFFF, 0x007E0F, 2,_0,  ArgType.MR4,ArgType.RMX,ArgType.NNN, CmdType.MMX|(CmdType)0,				"MOVD" ),
			new t_cmddata( 0x00FFFF, 0x006F0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"MOVQ" ),
			new t_cmddata( 0x00FFFF, 0x007F0F, 2,_0,  ArgType.MR8,ArgType.RMX,ArgType.NNN, CmdType.MMX|(CmdType)0,				"MOVQ" ),
			new t_cmddata( 0x00FFFF, 0x00630F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PACKSSWB" ),
			new t_cmddata( 0x00FFFF, 0x006B0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PACKSSDW" ),
			new t_cmddata( 0x00FFFF, 0x00670F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PACKUSWB" ),
			new t_cmddata( 0x00FFFF, 0x00FC0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PADDB" ),
			new t_cmddata( 0x00FFFF, 0x00FD0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PADDW" ),
			new t_cmddata( 0x00FFFF, 0x00FE0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PADDD" ),
			new t_cmddata( 0x00FFFF, 0x00F80F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PSUBB" ),
			new t_cmddata( 0x00FFFF, 0x00F90F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSUBW" ),
			new t_cmddata( 0x00FFFF, 0x00FA0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSUBD" ),
			new t_cmddata( 0x00FFFF, 0x00EC0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PADDSB" ),
			new t_cmddata( 0x00FFFF, 0x00ED0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PADDSW" ),
			new t_cmddata( 0x00FFFF, 0x00E80F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PSUBSB" ),
			new t_cmddata( 0x00FFFF, 0x00E90F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSUBSW" ),
			new t_cmddata( 0x00FFFF, 0x00DC0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PADDUSB" ),
			new t_cmddata( 0x00FFFF, 0x00DD0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PADDUSW" ),
			new t_cmddata( 0x00FFFF, 0x00D80F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PSUBUSB" ),
			new t_cmddata( 0x00FFFF, 0x00D90F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSUBUSW" ),
			new t_cmddata( 0x00FFFF, 0x00DB0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PAND" ),
			new t_cmddata( 0x00FFFF, 0x00DF0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PANDN" ),
			new t_cmddata( 0x00FFFF, 0x00740F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PCMPEQB" ),
			new t_cmddata( 0x00FFFF, 0x00750F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PCMPEQW" ),
			new t_cmddata( 0x00FFFF, 0x00760F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PCMPEQD" ),
			new t_cmddata( 0x00FFFF, 0x00640F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PCMPGTB" ),
			new t_cmddata( 0x00FFFF, 0x00650F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PCMPGTW" ),
			new t_cmddata( 0x00FFFF, 0x00660F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PCMPGTD" ),
			new t_cmddata( 0x00FFFF, 0x00F50F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PMADDWD" ),
			new t_cmddata( 0x00FFFF, 0x00E50F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PMULHW" ),
			new t_cmddata( 0x00FFFF, 0x00D50F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PMULLW" ),
			new t_cmddata( 0x00FFFF, 0x00EB0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"POR" ),
			new t_cmddata( 0x00FFFF, 0x00F10F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSLLW" ),
			new t_cmddata( 0x38FFFF, 0x30710F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSLLW" ),
			new t_cmddata( 0x00FFFF, 0x00F20F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSLLD" ),
			new t_cmddata( 0x38FFFF, 0x30720F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSLLD" ),
			new t_cmddata( 0x00FFFF, 0x00F30F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PSLLQ" ),
			new t_cmddata( 0x38FFFF, 0x30730F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PSLLQ" ),
			new t_cmddata( 0x00FFFF, 0x00E10F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSRAW" ),
			new t_cmddata( 0x38FFFF, 0x20710F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSRAW" ),
			new t_cmddata( 0x00FFFF, 0x00E20F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSRAD" ),
			new t_cmddata( 0x38FFFF, 0x20720F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSRAD" ),
			new t_cmddata( 0x00FFFF, 0x00D10F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSRLW" ),
			new t_cmddata( 0x38FFFF, 0x10710F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PSRLW" ),
			new t_cmddata( 0x00FFFF, 0x00D20F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSRLD" ),
			new t_cmddata( 0x38FFFF, 0x10720F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PSRLD" ),
			new t_cmddata( 0x00FFFF, 0x00D30F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PSRLQ" ),
			new t_cmddata( 0x38FFFF, 0x10730F, 2,_0,  ArgType.MR8,ArgType.IM1,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PSRLQ" ),
			new t_cmddata( 0x00FFFF, 0x00680F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PUNPCKHBW" ),
			new t_cmddata( 0x00FFFF, 0x00690F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PUNPCKHWD" ),
			new t_cmddata( 0x00FFFF, 0x006A0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PUNPCKHDQ" ),
			new t_cmddata( 0x00FFFF, 0x00600F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PUNPCKLBW" ),
			new t_cmddata( 0x00FFFF, 0x00610F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PUNPCKLWD" ),
			new t_cmddata( 0x00FFFF, 0x00620F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)4,				"PUNPCKLDQ" ),
			new t_cmddata( 0x00FFFF, 0x00EF0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PXOR" ),
			// AMD extentions to MMX command set (including Athlon/PIII extentions).
			new t_cmddata( 0x00FFFF, 0x000E0F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"FEMMS" ),
			new t_cmddata( 0x38FFFF, 0x000D0F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PREFETCH" ),
			new t_cmddata( 0x38FFFF, 0x080D0F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PREFETCHW" ),
			new t_cmddata( 0x00FFFF, 0x00F70F, 2,_0,  ArgType.RMX,ArgType.RR8,ArgType.PDI, CmdType.MMX|(CmdType)1,				"MASKMOVQ" ),
			new t_cmddata( 0x00FFFF, 0x00E70F, 2,_0,  ArgType.MD8,ArgType.RMX,ArgType.NNN, CmdType.MMX|(CmdType)0,				"MOVNTQ" ),
			new t_cmddata( 0x00FFFF, 0x00E00F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PAVGB" ),
			new t_cmddata( 0x00FFFF, 0x00E30F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PAVGW" ),
			new t_cmddata( 0x00FFFF, 0x00C50F, 2,_0,  ArgType.RR4,ArgType.RMX,ArgType.IM1, CmdType.MMX|(CmdType)2,				"PEXTRW" ),
			new t_cmddata( 0x00FFFF, 0x00C40F, 2,_0,  ArgType.RMX,ArgType.MR2,ArgType.IM1, CmdType.MMX|(CmdType)2,				"PINSRW" ),
			new t_cmddata( 0x00FFFF, 0x00EE0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PMAXSW" ),
			new t_cmddata( 0x00FFFF, 0x00DE0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PMAXUB" ),
			new t_cmddata( 0x00FFFF, 0x00EA0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PMINSW" ),
			new t_cmddata( 0x00FFFF, 0x00DA0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PMINUB" ),
			new t_cmddata( 0x00FFFF, 0x00D70F, 2,_0,  ArgType.RG4,ArgType.RR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PMOVMSKB" ),
			new t_cmddata( 0x00FFFF, 0x00E40F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)2,				"PMULHUW" ),
			new t_cmddata( 0x38FFFF, 0x00180F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PREFETCHNTA" ),
			new t_cmddata( 0x38FFFF, 0x08180F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PREFETCHT0" ),
			new t_cmddata( 0x38FFFF, 0x10180F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PREFETCHT1" ),
			new t_cmddata( 0x38FFFF, 0x18180F, 2,_0,  ArgType.MD8,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"PREFETCHT2" ),
			new t_cmddata( 0x00FFFF, 0x00F60F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.MMX|(CmdType)1,				"PSADBW" ),
			new t_cmddata( 0x00FFFF, 0x00700F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.IM1, CmdType.MMX|(CmdType)2,				"PSHUFW" ),
			new t_cmddata( 0xFFFFFF, 0xF8AE0F, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.MMX|(CmdType)0,				"SFENCE" ),
			// AMD 3DNow! instructions (including Athlon extentions).
			// (dummy) → これで Match した後更に分別
			new t_cmddata( 0x00FFFF, 0xBF0F0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.NOW|(CmdType)1,				"PAVGUSB" ),
			// Some alternative mnemonics for Assembler, not used by Disassembler (so
			// implicit pseudooperands are not marked).
			new t_cmddata( 0x0000FF, 0x0000A6, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMPSB" ),
			new t_cmddata( 0x00FFFF, 0x00A766, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMPSW" ),
			new t_cmddata( 0x0000FF, 0x0000A7, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMPSD" ),
			new t_cmddata( 0x0000FF, 0x0000AC, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"LODSB" ),
			new t_cmddata( 0x00FFFF, 0x00AD66, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"LODSW" ),
			new t_cmddata( 0x0000FF, 0x0000AD, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"LODSD" ),
			new t_cmddata( 0x0000FF, 0x0000A4, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOVSB" ),
			new t_cmddata( 0x00FFFF, 0x00A566, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOVSW" ),
			new t_cmddata( 0x0000FF, 0x0000A5, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"MOVSD" ),
			new t_cmddata( 0x0000FF, 0x0000AE, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SCASB" ),
			new t_cmddata( 0x00FFFF, 0x00AF66, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SCASW" ),
			new t_cmddata( 0x0000FF, 0x0000AF, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"SCASD" ),
			new t_cmddata( 0x0000FF, 0x0000AA, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"STOSB" ),
			new t_cmddata( 0x00FFFF, 0x00AB66, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"STOSW" ),
			new t_cmddata( 0x0000FF, 0x0000AB, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"STOSD" ),
			new t_cmddata( 0x00FFFF, 0x00A4F3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP MOVSB" ),
			new t_cmddata( 0xFFFFFF, 0xA5F366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP MOVSW" ),
			new t_cmddata( 0x00FFFF, 0x00A5F3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP MOVSD" ),
			new t_cmddata( 0x00FFFF, 0x00ACF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP LODSB" ),
			new t_cmddata( 0xFFFFFF, 0xADF366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP LODSW" ),
			new t_cmddata( 0x00FFFF, 0x00ADF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP LODSD" ),
			new t_cmddata( 0x00FFFF, 0x00AAF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP STOSB" ),
			new t_cmddata( 0xFFFFFF, 0xABF366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP STOSW" ),
			new t_cmddata( 0x00FFFF, 0x00ABF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP STOSD" ),
			new t_cmddata( 0x00FFFF, 0x00A6F3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPE CMPSB" ),
			new t_cmddata( 0xFFFFFF, 0xA7F366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPE CMPSW" ),
			new t_cmddata( 0x00FFFF, 0x00A7F3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPE CMPSD" ),
			new t_cmddata( 0x00FFFF, 0x00AEF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPE SCASB" ),
			new t_cmddata( 0xFFFFFF, 0xAFF366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPE SCASW" ),
			new t_cmddata( 0x00FFFF, 0x00AFF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPE SCASD" ),
			new t_cmddata( 0x00FFFF, 0x00A6F2, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPNE CMPSB" ),
			new t_cmddata( 0xFFFFFF, 0xA7F266, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPNE CMPSW" ),
			new t_cmddata( 0x00FFFF, 0x00A7F2, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPNE CMPSD" ),
			new t_cmddata( 0x00FFFF, 0x00AEF2, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPNE SCASB" ),
			new t_cmddata( 0xFFFFFF, 0xAFF266, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPNE SCASW" ),
			new t_cmddata( 0x00FFFF, 0x00AFF2, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REPNE SCASD" ),
			new t_cmddata( 0x0000FF, 0x00006C, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"INSB" ),
			new t_cmddata( 0x00FFFF, 0x006D66, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"INSW" ),
			new t_cmddata( 0x0000FF, 0x00006D, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"INSD" ),
			new t_cmddata( 0x0000FF, 0x00006E, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"OUTSB" ),
			new t_cmddata( 0x00FFFF, 0x006F66, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"OUTSW" ),
			new t_cmddata( 0x0000FF, 0x00006F, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|CmdType.RARE|(CmdType)0,	"OUTSD" ),
			new t_cmddata( 0x00FFFF, 0x006CF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP INSB" ),
			new t_cmddata( 0xFFFFFF, 0x6DF366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP INSW" ),
			new t_cmddata( 0x00FFFF, 0x006DF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP INSD" ),
			new t_cmddata( 0x00FFFF, 0x006EF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP OUTSB" ),
			new t_cmddata( 0xFFFFFF, 0x6FF366, 2,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP OUTSW" ),
			new t_cmddata( 0x00FFFF, 0x006FF3, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.REP|(CmdType)0,				"REP OUTSD" ),
			new t_cmddata( 0x0000FF, 0x0000E1, 1,_0,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"$LOOP*Z" ),
			new t_cmddata( 0x0000FF, 0x0000E0, 1,_0,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"$LOOP*NZ" ),
			new t_cmddata( 0x0000FF, 0x00009B, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"FWAIT" ),
			new t_cmddata( 0x0000FF, 0x0000D7, 1,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"XLATB" ),
			new t_cmddata( 0x00FFFF, 0x00C40F, 2,_0,  ArgType.RMX,ArgType.RR4,ArgType.IM1, CmdType.MMX|(CmdType)2,				"PINSRW" ),
			new t_cmddata( 0x00FFFF, 0x0020CD, 2,_0,  ArgType.VXD,ArgType.NNN,ArgType.NNN, CmdType.CAL|CmdType.RARE|(CmdType)0,	"VxDCall" ),
			// Pseudocommands used by Assembler for masked search only.
			new t_cmddata( 0x0000F0, 0x000070, 1,CC,  ArgType.JOB,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JCC" ),
			new t_cmddata( 0x00F0FF, 0x00800F, 2,CC,  ArgType.JOW,ArgType.NNN,ArgType.NNN, CmdType.JMC|(CmdType)0,				"JCC" ),
			new t_cmddata( 0x00F0FF, 0x00900F, 2,CC,  ArgType.MR1,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)1,				"SETCC" ),
			new t_cmddata( 0x00F0FF, 0x00400F, 2,CC,  ArgType.REG,ArgType.MRG,ArgType.NNN, CmdType.CMD|(CmdType)0,				"CMOVCC" ),
			// End of command table.
			new t_cmddata( 0x000000, 0x000000, 0,_0,  ArgType.NNN,ArgType.NNN,ArgType.NNN, CmdType.CMD|(CmdType)0,				"" ),
		};
		public static t_cmddata[] data_3dnow=new t_cmddata[]{
			// AMD 3DNow! instructions (including Athlon extentions).
			new t_cmddata( 0x00FFFF, 0xBF0F0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.NOW|(CmdType)1,				"PAVGUSB" ),
			new t_cmddata( 0x00FFFF, 0x9E0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFADD" ),
			new t_cmddata( 0x00FFFF, 0x9A0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFSUB" ),
			new t_cmddata( 0x00FFFF, 0xAA0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFSUBR" ),
			new t_cmddata( 0x00FFFF, 0xAE0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFACC" ),
			new t_cmddata( 0x00FFFF, 0x900F0F, 2,_0,  ArgType.RMX,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFCMPGE" ),
			new t_cmddata( 0x00FFFF, 0xA00F0F, 2,_0,  ArgType.RMX,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFCMPGT" ),
			new t_cmddata( 0x00FFFF, 0xB00F0F, 2,_0,  ArgType.RMX,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFCMPEQ" ),
			new t_cmddata( 0x00FFFF, 0x940F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFMIN" ),
			new t_cmddata( 0x00FFFF, 0xA40F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFMAX" ),
			new t_cmddata( 0x00FFFF, 0x0D0F0F, 2,_0,  ArgType.R3D,ArgType.MR8,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PI2FD" ),
			new t_cmddata( 0x00FFFF, 0x1D0F0F, 2,_0,  ArgType.RMX,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PF2ID" ),
			new t_cmddata( 0x00FFFF, 0x960F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFRCP" ),
			new t_cmddata( 0x00FFFF, 0x970F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFRSQRT" ),
			new t_cmddata( 0x00FFFF, 0xB40F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFMUL" ),
			new t_cmddata( 0x00FFFF, 0xA60F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFRCPIT1" ),
			new t_cmddata( 0x00FFFF, 0xA70F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFRSQIT1" ),
			new t_cmddata( 0x00FFFF, 0xB60F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFRCPIT2" ),
			new t_cmddata( 0x00FFFF, 0xB70F0F, 2,_0,  ArgType.RMX,ArgType.MR8,ArgType.NNN, CmdType.NOW|(CmdType)2,				"PMULHRW" ),
			new t_cmddata( 0x00FFFF, 0x1C0F0F, 2,_0,  ArgType.RMX,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PF2IW" ),
			new t_cmddata( 0x00FFFF, 0x8A0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFNACC" ),
			new t_cmddata( 0x00FFFF, 0x8E0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PFPNACC" ),
			new t_cmddata( 0x00FFFF, 0x0C0F0F, 2,_0,  ArgType.R3D,ArgType.MR8,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PI2FW" ),
			new t_cmddata( 0x00FFFF, 0xBB0F0F, 2,_0,  ArgType.R3D,ArgType.MRD,ArgType.NNN, CmdType.NOW|(CmdType)4,				"PSWAPD" ),
		};
		public static t_cmddata vxdcmd=new t_cmddata(             // Decoding of VxD calls (Win95/98)
			0x00FFFF,0x0020CD,2,_0,
			ArgType.VXD,ArgType.NNN,ArgType.NNN,
			CmdType.CAL|CmdType.RARE|(CmdType)0,"VxDCall"
		);
		#endregion

		public bool IsBitsWW{
			get{return (this.bits&Bits.WW)!=0;}
		}
		public bool IsBitsFF{
			get{return (this.bits&Bits.FF)!=0;}
		}
		public bool IsBitsW3{
			get{return (this.bits&Bits.W3)!=0;}
		}
		public enum Bits:byte{
			WW            =0x01,           // Bit W (size of operand)
			SS            =0x02,           // Bit S (sign extention of immediate)
			WS            =0x03,           // Bits W and S
			W3            =0x08,           // Bit W at position 3
			CC            =0x10,           // Conditional jump
			FF            =0x20,           // Forced 16-bit size
			LL            =0x40,           // Conditional loop
			PR            =0x80,           // Protected command
			WP            =0x81,           // I/O command with bit W
		}
		public enum ArgType:byte{
			// All possible types of operands in 80x86. A bit more than you expected, he?
			NNN           =0,              // No operand
			REG           =1,              // Integer register in Reg field
			RCM           =2,              // Integer register in command byte
			RG4           =3,              // Integer 4-byte register in Reg field
			RAC           =4,              // Accumulator (AL/AX/EAX, implicit)
			RAX           =5,              // AX (2-byte, implicit)
			RDX           =6,              // DX (16-bit implicit port address)
			RCL           =7,              // Implicit CL register (for shifts)
			RS0           =8,              // Top of FPU stack (ST(0), implicit)
			RST           =9,              // FPU register (ST(i)) in command byte
			RMX           =10,             // MMX register MMx
			R3D           =11,             // 3DNow! register MMx
			MRG           =12,             // Memory/register in ModRM byte
			MR1           =13,             // 1-byte memory/register in ModRM byte
			MR2           =14,             // 2-byte memory/register in ModRM byte
			MR4           =15,             // 4-byte memory/register in ModRM byte
			RR4           =16,             // 4-byte memory/register (register only)
			MR8           =17,             // 8-byte memory/MMX register in ModRM
			RR8           =18,             // 8-byte MMX register only in ModRM
			MRD           =19,             // 8-byte memory/3DNow! register in ModRM
			RRD           =20,             // 8-byte memory/3DNow! (register only)
			MRJ           =21,             // Memory/reg in ModRM as JUMP target
			MMA           =22,             // Memory address in ModRM byte for LEA
			MML           =23,             // Memory in ModRM byte (for LES)
			MMS           =24,             // Memory in ModRM byte (as SEG:OFFS)
			MM6           =25,             // Memory in ModRm (6-byte descriptor)
			MMB           =26,             // Two adjacent memory locations (BOUND)
			MD2           =27,             // Memory in ModRM (16-bit integer)
			MB2           =28,             // Memory in ModRM (16-bit binary)
			MD4           =29,             // Memory in ModRM byte (32-bit integer)
			MD8           =30,             // Memory in ModRM byte (64-bit integer)
			MDA           =31,             // Memory in ModRM byte (80-bit BCD)
			MF4           =32,             // Memory in ModRM byte (32-bit float)
			MF8           =33,             // Memory in ModRM byte (64-bit float)
			MFA           =34,             // Memory in ModRM byte (80-bit float)
			MFE           =35,             // Memory in ModRM byte (FPU environment)
			MFS           =36,             // Memory in ModRM byte (FPU state)
			MFX           =37,             // Memory in ModRM byte (ext. FPU state)
			MSO           =38,             // Source in string op's ([ESI])
			MDE           =39,             // Destination in string op's ([EDI])
			MXL           =40,             // XLAT operand ([EBX+AL])
			IMM           =41,             // Immediate data (8 or 16/32)
			IMU           =42,             // Immediate unsigned data (8 or 16/32)
			VXD           =43,             // VxD service
			IMX           =44,             // Immediate sign-extendable byte
			C01           =45,             // Implicit constant 1 (for shifts)
			IMS           =46,             // Immediate byte (for shifts)
			IM1           =47,             // Immediate byte
			IM2           =48,             // Immediate word (ENTER/RET)
			IMA           =49,             // Immediate absolute near data address
			JOB           =50,             // Immediate byte offset (for jumps)
			JOW           =51,             // Immediate full offset (for jumps)
			JMF           =52,             // Immediate absolute far jump/call addr
			SGM           =53,             // Segment register in ModRM byte
			SCM           =54,             // Segment register in command byte
			CRX           =55,             // Control register CRx
			DRX           =56,             // Debug register DRx
			// Pseudooperands (implicit operands, never appear in assembler commands). Must
			// have index equal to or exceeding PSEUDOOP.
			PSEUDOOP      =128,            // Base for pseudooperands
			PRN           =PSEUDOOP+0,     // Near return address
			PRF           =PSEUDOOP+1,     // Far return address
			PAC           =PSEUDOOP+2,     // Accumulator (AL/AX/EAX)
			PAH           =PSEUDOOP+3,     // AH (in LAHF/SAHF commands)
			PFL           =PSEUDOOP+4,     // Lower byte of flags (in LAHF/SAHF)
			PS0           =PSEUDOOP+5,     // Top of FPU stack (ST(0))
			PS1           =PSEUDOOP+6,     // ST(1)
			PCX           =PSEUDOOP+7,     // CX/ECX
			PDI           =PSEUDOOP+8,     // EDI (in MMX extentions)
		}
		public enum CmdType{
			TypeMASK=0xF0,
			CMD		=0x00,
			PSH		=0x10,
			POP		=0x20,
			MMX		=0x30,
			FLT		=0x40,
			JMP		=0x50,
			JMC		=0x60,
			CAL		=0x70,
			RET		=0x80,
			FLG		=0x90,
			RTF		=0xA0,
			REP		=0xB0,
			PRI		=0xC0,
			DAT		=0xD0,
			NOW		=0xE0,
			BAD		=0xF0,
			RARE		=0x08,
			SizeMASK	=0x07,
			EXPL		=0x01,
		}
	}
	public class t_disasm{              // Results of disassembling
		public ulong          ip;                   // Instrucion pointer
		public byte[]         dump;//[TEXTLEN];        // Hexadecimal dump of the command
		public byte[]         result;//[TEXTLEN];      // Disassembled command
		public byte[]         comment;//[TEXTLEN];     // Brief comment
		public CommandType    cmdtype;              // One of C_xxx
		public MemoryType     memtype;              // Type of addressed variable in memory
		public int            nprefix;              // Number of prefixes
		public int            indexed;              // Address contains register(s)
		public ulong          jmpconst;             // Constant jump address
		public ulong          jmptable;             // Possible address of switch table
		public ulong          adrconst;             // Constant part of address
		public ulong          immconst;             // Immediate constant
		public int            zeroconst;            // Whether contains zero constant
		public int            fixupoffset;          // Possible offset of 32-bit fixups
		public int            fixupsize;            // Possible total size of fixups or 0
		public Error          error;                // Error while disassembling command
		public int            warnings;             // Combination of DAW_xxx

		public t_disasm(){
			this.dump=new byte[DisasmCls.TEXTLEN];
			this.result=new byte[DisasmCls.TEXTLEN];
			this.comment=new byte[DisasmCls.TEXTLEN];

			ip=0;
			cmdtype=CommandType.Bad;
			memtype=MemoryType.Unknown;
			nprefix=0;
			indexed=0;
			jmpconst=0;
			jmptable=0;
			adrconst=0;
			immconst=0;
			zeroconst=0;
			fixupoffset=0;
			fixupsize=0;
			error=Error.NoErr;
			warnings=0;
		}
	}

	public enum SegmentPrefix{
		Undefined=-1,
		ES=0,
		CS=1,
		SS=2,
		DS=3,
		FS=4,
		GS=5,
	}
	public enum CommandType{
		TypeMASK=0xF0,
		Cmd		=0x00,
		Push	=0x10,
		Pop		=0x20,
		Mmx		=0x30,
		Float	=0x40,
		Jump	=0x50,
		JumpC	=0x60,
		Call	=0x70,
		Ret		=0x80,
		Flag	=0x90,
		Rtf		=0xA0,
		Rep		=0xB0,
		Priv	=0xC0,
		Dat		=0xD0,
		Now		=0xE0,
		Bad		=0xF0,
		Rare		=0x08,
		SizeMASK	=0x07,
		SzExplicit	=0x01
	}
	public enum MemoryType{
		TypeMASK	=0x1F,
		Unknown		=0x00,
		Byte		=0x01,
		Word		=0x02,
		NextData	=0x03,
		Dword		=0x04,
		Float4		=0x05,
		FWord		=0x06,
		Float8		=0x07,
		QWord		=0x08,
		Float10		=0x0A,
		String		=0x0B,
		Unicode		=0x0C,
		_3DNow		=0x0D,
		ByteSW		=0x11,
		NextCode	=0x13,
		Command		=0x1D,
		JmpDest		=0x1E,
		CallDest	=0x1F,

		ProcMASK	=0x60,
		Proc		=0x20,
		ProcBody	=0x40,
		ProcEnd		=0x60,

		CHECKED		=0x80
	}
	public enum Error{
		NoErr		=0,
		BadCommand	=1,
		Cross		=2,
		BadSegment	=3,
		Memory		=4,
		Register	=5,
		Internal	=6
	}

	public class MyDisasm{
		public class Result{
			public string assembly="";
			public string warning="";
			public string error="";

			public bool is_rare=false;
		}

		public enum Prefix:byte{
			None		=0x00,
			[afh.EnumDescription("prefix es:")]
			ES			=0x26,
			[afh.EnumDescription("prefix cs:")]
			CS			=0x2E,
			[afh.EnumDescription("prefix ss:")]
			SS			=0x36,
			[afh.EnumDescription("prefix ds:")]
			DS			=0x3E,
			[afh.EnumDescription("prefix fs:")]
			FS			=0x64,
			[afh.EnumDescription("prefix gs:")]
			GS			=0x65,
			[afh.EnumDescription("prefix datasize:")]
			DATASIZE	=0x66,
			[afh.EnumDescription("prefix addrsize:")]
			ADDRSIZE	=0x67,
			[afh.EnumDescription("prefix lock:")]
			LOCK		=0xF0,
			[afh.EnumDescription("prefix repne:")]
			REPNE		=0xF2,
			[afh.EnumDescription("prefix repe:")]
			REPE		=0xF3,
		}

		public byte[] src;
		public int ip;

		bool decodevxd=true;
		bool shortstringcmds=true;
		int sizesens=0;
		bool lowercase=true;

		public Result Next(int mode){
			const int DISASM_FILE=1;

			Result ret=new Result();

			int i=ip;				// cmd
			int rest=src.Length;	// size : src.Length-i ?
									// もしそうなら、態こんな物を用意する必要はない。
									// レジスタ割り当てにも為らないだろうから速くも為らない。無駄

			if(i>=src.Length)return null;
			//========================================================
			//		Prefix 読み取り
			//========================================================
			Prefix pre_seg=Prefix.None;
			bool is_data2B=false;
			bool is_addr2B=false;
			bool is_lock=false;
			Prefix pre_rep=Prefix.None;
			//--------------------------------------------------------
			for(;i<src.Length;i++){
				switch((Prefix)src[i]){
					case Prefix.ES:
					case Prefix.CS:
					case Prefix.SS:
					case Prefix.DS:
					case Prefix.FS:
					case Prefix.GS:
						if(pre_seg!=Prefix.None)goto REPEATED;
						pre_seg=(Prefix)src[i];
						continue;
					case Prefix.DATASIZE:
						if(is_data2B)goto REPEATED;
						is_data2B=true;
						continue;
					case Prefix.ADDRSIZE:
						if(is_addr2B)goto REPEATED;
						is_addr2B=true;
						continue;
					case Prefix.LOCK:
						if(is_lock)goto REPEATED;
						is_lock=true;
						if(mode>=DISASM_FILE)ret.assembly+="lock ";
						ret.warning+="lock が使用されています\r\n";
						continue;
					case Prefix.REPNE:
					case Prefix.REPE:
						if(pre_rep!=Prefix.None)goto REPEATED;
						pre_rep=(Prefix)src[i];
						continue;
					default:
						break;
					REPEATED: // 同じ prefix の重複
						ret.assembly=afh.Enum.GetDescription((Prefix)src[ip]);
						ret.warning="重複または相反する prefix が続いています。";
						ip++;
						return ret;
				}
			}
			if(i>=src.Length){
				ret.assembly=afh.Enum.GetDescription((Prefix)src[ip]);
				ret.warning="prefix に続く命令が存在しません。";
				ip++;
				return ret;
			}

			//========================================================
			//		Code 読み取り
			//========================================================
			t_cmddata cd=default(t_cmddata);
			//--------------------------------------------------------
			uint code=src[i];
			if(i+1<src.Length)code|=(uint)src[i+1]<<8;
			if(i+2<src.Length)code|=(uint)src[i+2]<<16;
			if(pre_rep!=Prefix.None)
				code=code<<8|(byte)pre_rep;
			if(decodevxd&&t_cmddata.vxdcmd.Match(code)){
				cd=t_cmddata.vxdcmd;
			}else foreach(t_cmddata cd0 in t_cmddata.data){
				if(!cd0.Match(code))continue;
				if(mode>=DISASM_FILE&&shortstringcmds&&cd0.ArgIsSourceOrDest)continue;
				cd=cd0;
				break;
			}
			
			if(cd.Is3DNow){
				int suf=Get3DNowSuffix(i,is_addr2B);
				if(suf<0){
					ret.error+=Error.Cross.ToString();
				}else foreach(t_cmddata cd0 in t_cmddata.data_3dnow){
					if(cd0.code>>16==suf){
						cd=cd0;break;
					}
				}
			}

			if(i+cd.len>src.Length){
				ret.assembly="<未完命令>";
				ret.error="命令が途中で切れています。";
				ip=src.Length;
				return ret;
			}else if(cd.IsUnknown){
				ret.assembly="<未知命令>";
				ret.error="未知の命令です。";
				ip=i+1;
				return ret;
			}

			//========================================================
			//		エラー/警告
			//========================================================
			ret.is_rare=cd.IsRare||is_lock||pre_seg==Prefix.FS||pre_seg==Prefix.GS;
			if(cd.bits==t_cmddata.Bits.PR)
				ret.warning+="Privileged Command; Ring 0\r\n";
			else if(cd.bits==t_cmddata.Bits.WP)
				ret.warning+="I/O Command";

			// Win32 programs usually try to keep stack dword-aligned, so INC ESP
			// (44) and DEC ESP (4C) usually don't appear in real code. Also check for
			// ADD ESP,imm and SUB ESP,imm (81,C4,imm32; 83,C4,imm8; 81,EC,imm32;
			// 83,EC,imm8).
			if(src[i]==0x44||src[i]==0x4C||
			  (i+2<src.Length&&(src[i]==0x81||src[i]==0x83)&&(src[i+1]==0xC4||src[i+1]==0xEC)&&(src[i+2]&0x03)!=0)
			){
				ret.warning+="Collapse Stack Alignment";
				ret.is_rare=true;
			}

			// Warn also on MOV SEG,... (8E...). Win32 works in flat mode.
			if(src[i]==0x8E)
				ret.warning="Segment レジスタの設定は通常行いません。";

			// If opcode is 2-byte, adjust command.
			// 略
			i++; // 更に後で i++ するのか?

			//========================================================
			//		周辺情報
			//========================================================
			int addrsize=is_addr2B?2:4;
			int datasize=is_data2B?2:4;
			//  ret.assembly (mnemonic 書き出し)
			//--------------------------------------------------------

			// Some commands either feature non-standard data size or have bit which
			// allowes to select data size.
			if(cd.IsBitsWW&&(src[i]&(byte)t_cmddata.Bits.WW)==0)
				datasize=1;                      // Bit W in command set to 0
			else if(cd.IsBitsW3&&(src[i]&(byte)t_cmddata.Bits.W3)==0)
				datasize=1;                      // Another position of bit W
			else if(cd.IsBitsFF)
				datasize=2;                      // Forced word (2-byte) size

			// Some commands either have mnemonics which depend on data size (8/16 bits
			// or 32 bits, like CWD/CDQ), or have several different mnemonics (like
			// JNZ/JNE). First case is marked by either '&' (mnemonic depends on
			// operand size) or '$' (depends on address size). In the second case,
			// there is no special marker and disassembler selects main mnemonic.
			string mne=cd.name;
			if(mode>=DISASM_FILE){
				int mnemosize;
				if(mne[0]=='&'){
					mne=mne.Substring(1);
					mnemosize=datasize;
				}else if(mne[0]=='$'){
					mne=mne.Substring(1);
					mnemosize=addrsize;
				}else mnemosize=0;

				if(mnemosize!=0){
					if(mnemosize==4&&sizesens!=2){
						mne=mne.Replace('*','D');
					}else if(mnemosize==2&&sizesens!=0){
						mne=mne.Replace('*','W');
					}

					string[] cands=mne.Split(new char[]{':'},2);
					if(cands.Length>1)mne=cands[mnemosize==4?1:0];
				}else{
					mne=mne.Split(new char[]{','},2)[0];
				}

				// TODO: tabarguments

				ret.assembly+=mne+" ";

				// TODO: lowercase
			}

			//========================================================
			//		引数読み取り
			//========================================================

			return ret;

		}

		private int Get3DNowSuffix(int i,bool is_addr2B) {
			if(i+2>=src.Length)return -1;               // Suffix outside the memory block
			uint offset=3;
			int c=src[i+2]&0xC7;                     // Leave only Mod and M fields

			// Register in ModM - general-purpose, MMX or 3DNow!
			if((c&0xC0)==0xC0){

			// 16-bit addressing mode, SIB byte is never used here.
			}else if(is_addr2B){
				if(c==0x06)                       // Special case of immediate address
					offset+=2;
				else if((c&0xC0)==0x40)         // 8-bit signed displacement
					offset++;
				else if((c&0xC0)==0x80)         // 16-bit unsigned displacement
					offset+=2;
			
			// Immediate 32-bit address.
			}else if(c==0x05)                    // Special case of immediate address
				offset+=4;

			// 32-bit address with SIB byte.
			else if((c&0x07)==0x04){         // SIB addresation
				if(i+3>=src.Length)return -1;             // Suffix outside the memory block
				int sib=src[i+3];
				offset++;
				if(c==0x04&&(sib&0x07)==0x05)
					offset+=4;                       // Immediate address without base
				else if((c&0xC0)==0x40)         // 8-bit displacement
					offset+=1;
				else if((c&0xC0)==0x80)         // 32-bit dislacement
					offset+=4;
				
			// 32-bit address without SIB byte
			}else if((c&0xC0)==0x40)
				offset+=1;
			else if((c&0xC0)==0x80)
				offset+=4;

			if(i+offset>=src.Length)return -1;         // Suffix outside the memory block
			return src[i+offset];
		}
	}
	public unsafe class DisasmCls {
		public const int TEXTLEN=256;

		ulong datasize;
		ulong addrsize;
		SegmentPrefix segprefix;
		int hasrm;
		int hassib;
		int dispsize;
		int immsize;
		int softerror;
		int ndump;
		int nresult;
		int addcomment;

		byte* cmd;
		byte* pfixup;
		ulong size;
		t_disasm da;
		int mode;

		unsafe ulong Disasm(byte *src,ulong srcsize,ulong srcip,t_disasm disasm,int disasmmode) {

			#region Declarations
#if FALSE
			int i,j,isprefix,is3dnow,repeated,operand,mnemosize,arg;
			ulong u,code;
			int lockprefix;                      // Non-zero if lock prefix present
			int repprefix;                       // REPxxx prefix or 0
			int cxsize;
			byte* name=stackalloc byte[TEXTLEN];
			byte *pname;
			t_cmddata pd;
			t_cmddata pdan;

			// Prepare disassembler variables and initialize structure disasm.
			datasize=addrsize=4;                 // 32-bit code and data segments only!
			segprefix=SegmentPrefix.Undefined;
			hasrm=hassib=0;
			dispsize=immsize=0;
			lockprefix=0;
			repprefix=0;
			ndump=0; nresult=0;
			cmd=src;
			size=srcsize;
			pfixup=(byte*)0;
			softerror=0; is3dnow=0;
			da=disasm;
			da.ip=srcip;
			da.comment[0]=0;
			da.cmdtype=CommandType.Bad;
			da.nprefix=0;
			da.memtype=MemoryType.Unknown;
			da.indexed=0;
			da.jmpconst=0;
			da.jmptable=0;
			da.adrconst=0;
			da.immconst=0;
			da.zeroconst=0;
			da.fixupoffset=0;
			da.fixupsize=0;
			da.warnings=0;
			da.error=Error.NoErr;
			mode=disasmmode;                     // No need to use register contents
#endif
			#endregion

			#region Reading Prefix
			// Correct 80x86 command may theoretically contain up to 4 prefixes belonging
			// to different prefix groups. This limits maximal possible size of the
			// command to MAXCMDSIZE=16 bytes. In order to maintain this limit, if
			// Disasm() detects second prefix from the same group, it flushes first
			// prefix in the sequence as a pseudocommand.
#if FALSE
			u=0; repeated=0;
			while (size>0) {
				isprefix=1;                        // Assume that there is some prefix
				switch (*cmd){
					case 0x26: if (segprefix==SEG_UNDEF) segprefix=SEG_ES;
						else repeated=1; break;
					case 0x2E: if (segprefix==SEG_UNDEF) segprefix=SEG_CS;
						else repeated=1; break;
					case 0x36: if (segprefix==SEG_UNDEF) segprefix=SEG_SS;
						else repeated=1; break;
					case 0x3E: if (segprefix==SEG_UNDEF) segprefix=SEG_DS;
						else repeated=1; break;
					case 0x64: if (segprefix==SEG_UNDEF) segprefix=SEG_FS;
						else repeated=1; break;
					case 0x65: if (segprefix==SEG_UNDEF) segprefix=SEG_GS;
						else repeated=1; break;
					case 0x66: if (datasize==4) datasize=2;
						else repeated=1; break;
					case 0x67: if (addrsize==4) addrsize=2;
						else repeated=1; break;
					case 0xF0: if (lockprefix==0) lockprefix=0xF0;
						else repeated=1; break;
					case 0xF2: if (repprefix==0) repprefix=0xF2;
						else repeated=1; break;
					case 0xF3: if (repprefix==0) repprefix=0xF3;
						else repeated=1; break;
					default: isprefix=0; break;
				}
				if (isprefix==0 || repeated!=0)
					break;                           // No more prefixes or duplicated prefix
				if (mode>=DISASM_FILE)
					ndump+=sprintf(da->dump+ndump,"%02X:",*cmd);
				da->nprefix++;
				cmd++; srcip++; size--; u++;
			}
			// We do have repeated prefix. Flush first prefix from the sequence.
			if (repeated) {
				if (mode>=DISASM_FILE) {
				da->dump[3]='\0';                // Leave only first dumped prefix
				da->nprefix=1;
				switch (cmd[-(long)u]) {
					case 0x26: pname=(char *)(segname[SEG_ES]); break;
					case 0x2E: pname=(char *)(segname[SEG_CS]); break;
					case 0x36: pname=(char *)(segname[SEG_SS]); break;
					case 0x3E: pname=(char *)(segname[SEG_DS]); break;
					case 0x64: pname=(char *)(segname[SEG_FS]); break;
					case 0x65: pname=(char *)(segname[SEG_GS]); break;
					case 0x66: pname="DATASIZE"; break;
					case 0x67: pname="ADDRSIZE"; break;
					case 0xF0: pname="LOCK"; break;
					case 0xF2: pname="REPNE"; break;
					case 0xF3: pname="REPE"; break;
					default: pname="?"; break;
				};
				nresult+=sprintf(da->result+nresult,"PREFIX %s:",pname);
				if (lowercase) strlwr(da->result);
				if (extraprefix==0) strcpy(da->comment,"Superfluous prefix"); };
				da->warnings|=DAW_PREFIX;
				if (lockprefix) da->warnings|=DAW_LOCK;
				da->cmdtype=C_RARE;
				return 1;                          // Any prefix is 1 byte long
			};
			// If lock prefix available, display it and forget, because it has no
			// influence on decoding of rest of the command.
			if (lockprefix!=0) {
				if (mode>=DISASM_FILE) nresult+=sprintf(da->result+nresult,"LOCK ");
				da->warnings|=DAW_LOCK;
			};
#endif
			#endregion

			#region Reading OpCode
#if FALSE
			// Fetch (if available) first 3 bytes of the command, add repeat prefix and
			// find command in the command table.
			code=0;
			if (size>0) *(((char *)&code)+0)=cmd[0];
			if (size>1) *(((char *)&code)+1)=cmd[1];
			if (size>2) *(((char *)&code)+2)=cmd[2];
			if (repprefix!=0)                    // RER/REPE/REPNE is considered to be
				code=(code<<8) | repprefix;        // part of command.
			if (decodevxd && (code & 0xFFFF)==0x20CD)
				pd=&vxdcmd;                        // Decode VxD call (Win95/98)
			else {
				for (pd=cmddata; pd->mask!=0; pd++) {
					if (((code^pd->code) & pd->mask)!=0) continue;
					if (mode>=DISASM_FILE && shortstringcmds &&
					(pd->arg1==MSO || pd->arg1==MDE || pd->arg2==MSO || pd->arg2==MDE))
						continue;                      // Search short form of string command
					break;
				};
			};
			if ((pd->type & C_TYPEMASK)==C_NOW) {
				// 3DNow! commands require additional search.
				is3dnow=1;
				j=Get3dnowsuffix();
				if (j<0)
					da->error=DAE_CROSS;
				else {
					for ( ; pd->mask!=0; pd++) {
						if (((code^pd->code) & pd->mask)!=0) continue;
						if (((uchar *)&(pd->code))[2]==j) break;
					};
				};
			};
			if (pd->mask==0) {                   // Command not found
				da->cmdtype=C_BAD;
				if (size<2) da->error=DAE_CROSS;
				else da->error=DAE_BADCMD;
			}else
#endif
			#endregion

			#region 周辺情報
#if FALSE
			da->cmdtype=pd->type;
			cxsize=datasize;                   // Default size of ECX used as counter
			if (segprefix==SEG_FS || segprefix==SEG_GS || lockprefix!=0)
			  da->cmdtype|=C_RARE;             // These prefixes are rare
			if (pd->bits==PR)
			  da->warnings|=DAW_PRIV;          // Privileged command (ring 0)
			else if (pd->bits==WP)
			  da->warnings|=DAW_IO;            // I/O command

			// Win32 programs usually try to keep stack dword-aligned, so INC ESP
			// (44) and DEC ESP (4C) usually don't appear in real code. Also check for
			// ADD ESP,imm and SUB ESP,imm (81,C4,imm32; 83,C4,imm8; 81,EC,imm32;
			// 83,EC,imm8).
			if (cmd[0]==0x44 || cmd[0]==0x4C ||
			  (size>=3 && (cmd[0]==0x81 || cmd[0]==0x83) &&
			  (cmd[1]==0xC4 || cmd[1]==0xEC) && (cmd[2] & 0x03)!=0)
			) {
			  da->warnings|=DAW_STACK;
			  da->cmdtype|=C_RARE; };

			// Warn also on MOV SEG,... (8E...). Win32 works in flat mode.
			if (cmd[0]==0x8E)
			  da->warnings|=DAW_SEGMENT;
			// If opcode is 2-byte, adjust command.
			if (pd->len==2) {
			  if (size==0) da->error=DAE_CROSS;
			  else {
				if (mode>=DISASM_FILE)
				  ndump+=sprintf(da->dump+ndump,"%02X",*cmd);
				cmd++; srcip++; size--;
			  }; };
			if (size==0) da->error=DAE_CROSS;
			// Some commands either feature non-standard data size or have bit which
			// allowes to select data size.
			if ((pd->bits & WW)!=0 && (*cmd & WW)==0)
			  datasize=1;                      // Bit W in command set to 0
			else if ((pd->bits & W3)!=0 && (*cmd & W3)==0)
			  datasize=1;                      // Another position of bit W
			else if ((pd->bits & FF)!=0)
			  datasize=2;                      // Forced word (2-byte) size
			// Some commands either have mnemonics which depend on data size (8/16 bits
			// or 32 bits, like CWD/CDQ), or have several different mnemonics (like
			// JNZ/JNE). First case is marked by either '&' (mnemonic depends on
			// operand size) or '$' (depends on address size). In the second case,
			// there is no special marker and disassembler selects main mnemonic.
			if (mode>=DISASM_FILE) {
			  if (pd->name[0]=='&') mnemosize=datasize;
			  else if (pd->name[0]=='$') mnemosize=addrsize;
			  else mnemosize=0;
			  if (mnemosize!=0) {
				for (i=0,j=1; pd->name[j]!='\0'; j++) {
				  if (pd->name[j]==':') {      // Separator between 16/32 mnemonics
					if (mnemosize==4) i=0;
					else break; }
				  else if (pd->name[j]=='*') { // Substitute by 'W', 'D' or none
					if (mnemosize==4 && sizesens!=2) name[i++]='D';
					else if (mnemosize!=4 && sizesens!=0) name[i++]='W'; }
				  else name[i++]=pd->name[j];
				};
				name[i]='\0'; }
			  else {
				strcpy(name,pd->name);
				for (i=0; name[i]!='\0'; i++) {
				  if (name[i]==',') {          // Use main mnemonic
					name[i]='\0'; break;
				  };
				};
			  };
			  if (repprefix!=0 && tabarguments) {
				for (i=0; name[i]!='\0' && name[i]!=' '; i++)
				  da->result[nresult++]=name[i];
				if (name[i]==' ') {
				  da->result[nresult++]=' '; i++; };
				while (nresult<8) da->result[nresult++]=' ';
				for ( ; name[i]!='\0'; i++)
				  da->result[nresult++]=name[i];
				; }
			  else
				nresult+=sprintf(da->result+nresult,"%s",name);
			  if (lowercase) strlwr(da->result);
			};
#endif
			#endregion

#if FALSE
   {                               // Command recognized, decode it

    // Decode operands (explicit - encoded in command, implicit - present in
    // mmemonic or assumed - used or modified by command). Assumed operands
    // must stay after all explicit and implicit operands. Up to 3 operands
    // are allowed.
    for (operand=0; operand<3; operand++) {
      if (da->error) break;            // Error - no sense to continue
      // If command contains both source and destination, one usually must not
      // decode destination to comment because it will be overwritten on the
      // next step. Global addcomment takes care of this. Decoding routines,
      // however, may ignore this flag.
      if (operand==0 && pd->arg2!=NNN && pd->arg2<PSEUDOOP)
        addcomment=0;
      else
        addcomment=1;
      // Get type of next argument.
      if (operand==0) arg=pd->arg1;
      else if (operand==1) arg=pd->arg2;
      else arg=pd->arg3;
      if (arg==NNN) break;             // No more operands
      // Arguments with arg>=PSEUDOOP are assumed operands and are not
      // displayed in disassembled result, so they require no delimiter.
      if ((mode>=DISASM_FILE) && arg<PSEUDOOP) {
        if (operand==0) {
          da->result[nresult++]=' ';
          if (tabarguments) {
            while (nresult<8) da->result[nresult++]=' ';
          }; }
        else {
          da->result[nresult++]=',';
          if (extraspace) da->result[nresult++]=' ';
        };
      };
      // Decode, analyse and comment next operand of the command.
      switch (arg) {
        case REG:                      // Integer register in Reg field
          if (size<2) da->error=DAE_CROSS;
          else DecodeRG(cmd[1]>>3,datasize,REG);
          hasrm=1; break;
        case RCM:                      // Integer register in command byte
          DecodeRG(cmd[0],datasize,RCM); break;
        case RG4:                      // Integer 4-byte register in Reg field
          if (size<2) da->error=DAE_CROSS;
          else DecodeRG(cmd[1]>>3,4,RG4);
          hasrm=1; break;
        case RAC:                      // Accumulator (AL/AX/EAX, implicit)
          DecodeRG(REG_EAX,datasize,RAC); break;
        case RAX:                      // AX (2-byte, implicit)
          DecodeRG(REG_EAX,2,RAX); break;
        case RDX:                      // DX (16-bit implicit port address)
          DecodeRG(REG_EDX,2,RDX); break;
        case RCL:                      // Implicit CL register (for shifts)
          DecodeRG(REG_ECX,1,RCL); break;
        case RS0:                      // Top of FPU stack (ST(0))
          DecodeST(0,0); break;
        case RST:                      // FPU register (ST(i)) in command byte
          DecodeST(cmd[0],0); break;
        case RMX:                      // MMX register MMx
          if (size<2) da->error=DAE_CROSS;
          else DecodeMX(cmd[1]>>3);
          hasrm=1; break;
        case R3D:                      // 3DNow! register MMx
          if (size<2) da->error=DAE_CROSS;
          else DecodeNR(cmd[1]>>3);
          hasrm=1; break;
        case MRG:                      // Memory/register in ModRM byte
        case MRJ:                      // Memory/reg in ModRM as JUMP target
        case MR1:                      // 1-byte memory/register in ModRM byte
        case MR2:                      // 2-byte memory/register in ModRM byte
        case MR4:                      // 4-byte memory/register in ModRM byte
        case MR8:                      // 8-byte memory/MMX register in ModRM
        case MRD:                      // 8-byte memory/3DNow! register in ModRM
        case MMA:                      // Memory address in ModRM byte for LEA
        case MML:                      // Memory in ModRM byte (for LES)
        case MM6:                      // Memory in ModRm (6-byte descriptor)
        case MMB:                      // Two adjacent memory locations (BOUND)
        case MD2:                      // Memory in ModRM byte (16-bit integer)
        case MB2:                      // Memory in ModRM byte (16-bit binary)
        case MD4:                      // Memory in ModRM byte (32-bit integer)
        case MD8:                      // Memory in ModRM byte (64-bit integer)
        case MDA:                      // Memory in ModRM byte (80-bit BCD)
        case MF4:                      // Memory in ModRM byte (32-bit float)
        case MF8:                      // Memory in ModRM byte (64-bit float)
        case MFA:                      // Memory in ModRM byte (80-bit float)
        case MFE:                      // Memory in ModRM byte (FPU environment)
        case MFS:                      // Memory in ModRM byte (FPU state)
        case MFX:                      // Memory in ModRM byte (ext. FPU state)
          DecodeMR(arg); break;
        case MMS:                      // Memory in ModRM byte (as SEG:OFFS)
          DecodeMR(arg);
          da->warnings|=DAW_FARADDR; break;
        case RR4:                      // 4-byte memory/register (register only)
        case RR8:                      // 8-byte MMX register only in ModRM
        case RRD:                      // 8-byte memory/3DNow! (register only)
          if ((cmd[1] & 0xC0)!=0xC0) softerror=DAE_REGISTER;
          DecodeMR(arg); break;
        case MSO:                      // Source in string op's ([ESI])
          DecodeSO(); break;
        case MDE:                      // Destination in string op's ([EDI])
          DecodeDE(); break;
        case MXL:                      // XLAT operand ([EBX+AL])
          DecodeXL(); break;
        case IMM:                      // Immediate data (8 or 16/32)
        case IMU:                      // Immediate unsigned data (8 or 16/32)
          if ((pd->bits & SS)!=0 && (*cmd & 0x02)!=0)
            DecodeIM(1,datasize,arg);
          else
            DecodeIM(datasize,0,arg);
          break;
        case VXD:                      // VxD service (32-bit only)
          DecodeVX(); break;
        case IMX:                      // Immediate sign-extendable byte
          DecodeIM(1,datasize,arg); break;
        case C01:                      // Implicit constant 1 (for shifts)
          DecodeC1(); break;
        case IMS:                      // Immediate byte (for shifts)
        case IM1:                      // Immediate byte
          DecodeIM(1,0,arg); break;
        case IM2:                      // Immediate word (ENTER/RET)
          DecodeIM(2,0,arg);
          if ((da->immconst & 0x03)!=0) da->warnings|=DAW_STACK;
          break;
        case IMA:                      // Immediate absolute near data address
          DecodeIA(); break;
        case JOB:                      // Immediate byte offset (for jumps)
          DecodeRJ(1,srcip+2); break;
        case JOW:                      // Immediate full offset (for jumps)
          DecodeRJ(datasize,srcip+datasize+1); break;
        case JMF:                      // Immediate absolute far jump/call addr
          DecodeJF();
          da->warnings|=DAW_FARADDR; break;
        case SGM:                      // Segment register in ModRM byte
          if (size<2) da->error=DAE_CROSS;
          DecodeSG(cmd[1]>>3); hasrm=1; break;
        case SCM:                      // Segment register in command byte
          DecodeSG(cmd[0]>>3);
          if ((da->cmdtype & C_TYPEMASK)==C_POP) da->warnings|=DAW_SEGMENT;
          break;
        case CRX:                      // Control register CRx
          if ((cmd[1] & 0xC0)!=0xC0) da->error=DAE_REGISTER;
          DecodeCR(cmd[1]); break;
        case DRX:                      // Debug register DRx
          if ((cmd[1] & 0xC0)!=0xC0) da->error=DAE_REGISTER;
          DecodeDR(cmd[1]); break;
        case PRN:                      // Near return address (pseudooperand)
          break;
        case PRF:                      // Far return address (pseudooperand)
          da->warnings|=DAW_FARADDR; break;
        case PAC:                      // Accumulator (AL/AX/EAX, pseudooperand)
          DecodeRG(REG_EAX,datasize,PAC); break;
        case PAH:                      // AH (in LAHF/SAHF, pseudooperand)
        case PFL:                      // Lower byte of flags (pseudooperand)
          break;
        case PS0:                      // Top of FPU stack (pseudooperand)
          DecodeST(0,1); break;
        case PS1:                      // ST(1) (pseudooperand)
          DecodeST(1,1); break;
        case PCX:                      // CX/ECX (pseudooperand)
          DecodeRG(REG_ECX,cxsize,PCX); break;
        case PDI:                      // EDI (pseudooperand in MMX extentions)
          DecodeRG(REG_EDI,4,PDI); break;
        default:
          da->error=DAE_INTERN;        // Unknown argument type
        break;
      };
    };
    // Check whether command may possibly contain fixups.
    if (pfixup!=NULL && da->fixupsize>0)
      da->fixupoffset=pfixup-src;
    // Segment prefix and address size prefix are superfluous for command which
    // does not access memory. If this the case, mark command as rare to help
    // in analysis.
    if (da->memtype==DEC_UNKNOWN &&
      (segprefix!=SEG_UNDEF || (addrsize!=4 && pd->name[0]!='$'))
    ) {
      da->warnings|=DAW_PREFIX;
      da->cmdtype|=C_RARE; };
    // 16-bit addressing is rare in 32-bit programs. If this is the case,
    // mark command as rare to help in analysis.
    if (addrsize!=4) da->cmdtype|=C_RARE;
  };


  // Suffix of 3DNow! command is accounted best by assuming it immediate byte
  // constant.
  if (is3dnow) {
    if (immsize!=0) da->error=DAE_BADCMD;
    else immsize=1; };
  // Right or wrong, command decoded. Now dump it.
  if (da->error!=0) {                  // Hard error in command detected
    if (mode>=DISASM_FILE)
      nresult=sprintf(da->result,"???");
    if (da->error==DAE_BADCMD &&
      (*cmd==0x0F || *cmd==0xFF) && size>0
    ) {
      if (mode>=DISASM_FILE) ndump+=sprintf(da->dump+ndump,"%02X",*cmd);
      cmd++; size--; };
    if (size>0) {
      if (mode>=DISASM_FILE) ndump+=sprintf(da->dump+ndump,"%02X",*cmd);
      cmd++; size--;
    }; }
  else {                               // No hard error, dump command
    if (mode>=DISASM_FILE) {
      ndump+=sprintf(da->dump+ndump,"%02X",*cmd++);
      if (hasrm) ndump+=sprintf(da->dump+ndump,"%02X",*cmd++);
      if (hassib) ndump+=sprintf(da->dump+ndump,"%02X",*cmd++);
      if (dispsize!=0) {
        da->dump[ndump++]=' ';
        for (i=0; i<dispsize; i++) {
          ndump+=sprintf(da->dump+ndump,"%02X",*cmd++);
        };
      };
      if (immsize!=0) {
        da->dump[ndump++]=' ';
        for (i=0; i<immsize; i++) {
          ndump+=sprintf(da->dump+ndump,"%02X",*cmd++);
        };
      };
    }
    else
      cmd+=1+hasrm+hassib+dispsize+immsize;
    size-=1+hasrm+hassib+dispsize+immsize;
  };
  // Check that command is not a dangerous one.
  if (mode>=DISASM_DATA) {
    for (pdan=dangerous; pdan->mask!=0; pdan++) {
      if (((code^pdan->code) & pdan->mask)!=0)
        continue;
      if (pdan->type==C_DANGERLOCK && lockprefix==0)
        break;                         // Command harmless without LOCK prefix
      if (iswindowsnt && pdan->type==C_DANGER95)
        break;                         // Command harmless under Windows NT
      // Dangerous command!
      if (pdan->type==C_DANGER95) da->warnings|=DAW_DANGER95;
      else da->warnings|=DAW_DANGEROUS;
      break;
    };
  };
  if (da->error==0 && softerror!=0)
    da->error=softerror;               // Error, but still display command
  if (mode>=DISASM_FILE) {
    if (da->error!=DAE_NOERR) switch (da->error) {
      case DAE_CROSS:
        strcpy(da->comment,"Command crosses end of memory block"); break;
      case DAE_BADCMD:
        strcpy(da->comment,"Unknown command"); break;
      case DAE_BADSEG:
        strcpy(da->comment,"Undefined segment register"); break;
      case DAE_MEMORY:
        strcpy(da->comment,"Illegal use of register"); break;
      case DAE_REGISTER:
        strcpy(da->comment,"Memory address not allowed"); break;
      case DAE_INTERN:
        strcpy(da->comment,"Internal OLLYDBG error"); break;
      default:
        strcpy(da->comment,"Unknown error");
      break; }
    else if ((da->warnings & DAW_PRIV)!=0 && privileged==0)
      strcpy(da->comment,"Privileged command");
    else if ((da->warnings & DAW_IO)!=0 && iocommand==0)
      strcpy(da->comment,"I/O command");
    else if ((da->warnings & DAW_FARADDR)!=0 && farcalls==0) {
      if ((da->cmdtype & C_TYPEMASK)==C_JMP)
        strcpy(da->comment,"Far jump");
      else if ((da->cmdtype & C_TYPEMASK)==C_CAL)
        strcpy(da->comment,"Far call");
      else if ((da->cmdtype & C_TYPEMASK)==C_RET)
        strcpy(da->comment,"Far return");
      ; }
    else if ((da->warnings & DAW_SEGMENT)!=0 && farcalls==0)
      strcpy(da->comment,"Modification of segment register");
    else if ((da->warnings & DAW_SHIFT)!=0 && badshift==0)
      strcpy(da->comment,"Shift constant out of range 1..31");
    else if ((da->warnings & DAW_PREFIX)!=0 && extraprefix==0)
      strcpy(da->comment,"Superfluous prefix");
    else if ((da->warnings & DAW_LOCK)!=0 && lockedbus==0)
      strcpy(da->comment,"LOCK prefix");
    else if ((da->warnings & DAW_STACK)!=0 && stackalign==0)
      strcpy(da->comment,"Unaligned stack operation");
    ;
  };
			//*/
#endif
			return (srcsize-size);               // Returns number of recognized bytes
		}
	}
}
