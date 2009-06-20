using Gen=System.Collections.Generic;
using Xml=System.Xml;

namespace mwg.Disassemble{
	public class Command{
		string name;
		int size;
		int code;
		int mask;
		Bits bits;
		CmdType type;
		ArgType[] args;

		[System.Flags]
		private enum Bits:byte {
			None=0,
			WW=0x01,           // Bit W (size of operand)
			SS=0x02,           // Bit S (sign extention of immediate)
			WS=0x03,           // Bits W and S
			W3=0x08,           // Bit W at position 3
			CC=0x10,           // Conditional jump
			FF=0x20,           // Forced 16-bit size
			LL=0x40,           // Conditional loop
			PR=0x80,           // Protected command
			WP=0x81,           // I/O command with bit W
		}
		private enum CmdType{
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

		public bool Match(uint code){
			return ((code^this.code)&this.mask)!=0;
		}
		public bool ArgIsSourceOrDest{
			get{
				if(0<this.args.Length)
					return this.args[0]==ArgType.MSO||this.args[0]==ArgType.MDE;
				if(1<this.args.Length)
					return this.args[1]==ArgType.MSO||this.args[1]==ArgType.MDE;
				return false;
			}
		}
		//============================================================
		//		コマンド表
		//============================================================
		internal static Command cmd_vxd;
		internal static Command[] cmds;
		static Command(){
			Gen::List<Command> list=new System.Collections.Generic.List<Command>();
			Xml::XmlDocument doc=new System.Xml.XmlDocument();

			const string RES_NAME=__dll__.DEFAULT_NAMESPACE+".Disassemble.Command.xml";
			doc.Load(typeof(Command).Assembly.GetManifestResourceStream(RES_NAME));
			foreach(Xml::XmlElement elem in doc.SelectNodes("//list[@name='Standard']/cmd")){
				list.Add(new Command(elem));
			}
			cmds=list.ToArray();
		}

		private Command(Xml::XmlElement elem){
			this.name=elem.GetAttribute("name");
			this.size=int.Parse(elem.GetAttribute("size"));
			this.mask=int.Parse(elem.GetAttribute("mask"));
			this.code=int.Parse(elem.GetAttribute("code"));

			string sbits=elem.GetAttribute("bits");
			if(sbits!="00"){
				bits=(Bits)System.Enum.Parse(typeof(Bits),sbits);
			}else bits=Bits.None;

			this.type=default(CmdType);
			string[] types=elem.GetAttribute("type").Split('|');
			foreach(string stype in types){
				if('0'<=stype[0]&&stype[0]<='9'){
					this.type|=(CmdType)int.Parse(stype);
				}else{
					this.type|=(CmdType)System.Enum.Parse(typeof(CmdType),stype);
				}
			}

			Gen::List<ArgType> list=new System.Collections.Generic.List<ArgType>();
			foreach(Xml::XmlElement earg in elem.SelectNodes("/operand")){
				string stype=earg.GetAttribute("type");
				ArgType type=(ArgType)System.Enum.Parse(typeof(ArgType),stype);
				list.Add(type);
			}
			this.args=list.ToArray();
		}
	}
}