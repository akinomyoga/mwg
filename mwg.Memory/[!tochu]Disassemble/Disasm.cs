using Gen=System.Collections.Generic;
using Xml=System.Xml;

namespace mwg.Disassemble{
	public struct StreamPtr{
		private System.IO.Stream str;
		private long index;
		public StreamPtr(System.IO.Stream str,long index){
			this.str=str;
			this.index=index;
		}

		public long Index{
			get{return this.index;}
			set{this.index=value;}
		}
		public long Length{
			get{return str.Length;}
		}
		public bool IsEnd{
			get{return this.index>=str.Length;}
		}
		public long RestLength{
			get{return str.Length-this.index;}
		}

		public bool IsAccessible(int offset){
			return this.index+offset<str.Length;
		}
		public byte this[long offset]{
			get{
				str.Position=this.index+offset;
				return (byte)str.ReadByte();
			}
			set{
				str.Position=this.index+offset;
				str.WriteByte(value);
			}
		}

		public static StreamPtr operator+(StreamPtr ptr,long offset){
			return new StreamPtr(ptr.str,ptr.index+offset);
		}
		public static StreamPtr operator-(StreamPtr ptr,long offset){
			return new StreamPtr(ptr.str,ptr.index-offset);
		}
		public static StreamPtr operator+(StreamPtr ptr,int offset){
			return new StreamPtr(ptr.str,ptr.index+offset);
		}
		public static StreamPtr operator-(StreamPtr ptr,int offset){
			return new StreamPtr(ptr.str,ptr.index-offset);
		}
		public static StreamPtr operator++(StreamPtr ptr){
			return new StreamPtr(ptr.str,ptr.index+1);
		}
		public static StreamPtr operator--(StreamPtr ptr){
			return new StreamPtr(ptr.str,ptr.index-1);
		}
	}

	public class Disasm{
		public class Result{
			public string assembly="";
			public string warning="";
			public string error="";

			public bool is_rare=false;
		}

		Result ret;
		StreamPtr srcB;
		StreamPtr src;
		long ip;
		public Result Read(StreamPtr src,ref long ip){
			if(src.IsEnd)return null;

			this.srcB=src;
			this.src=src;
			this.ip=ip;

			if(!this.ReadPrefix())goto END;

		END:
			ip=this.ip;
			return ret;
		}

		//============================================================
		//		Settings
		//============================================================
		private bool decodevxd=false;
		private bool shortstringcmds=false;
		//============================================================
		//		Read Prefix
		//============================================================
		private Prefix pre_seg=Prefix.None;
		public bool is_data2B=false;
		public bool is_addr2B=false;
		public bool is_lock=false;
		private Prefix pre_rep=Prefix.None;
		private bool ReadPrefix(){
			for(;!src.IsEnd;src++)switch((Prefix)src[0]){
				case Prefix.ES:
				case Prefix.CS:
				case Prefix.SS:
				case Prefix.DS:
				case Prefix.FS:
				case Prefix.GS:
					if(pre_seg!=Prefix.None)goto REPEATED;
					pre_seg=(Prefix)src[0];
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
					ret.assembly+="lock ";
					ret.warning+="lock が使用されています\r\n";
					continue;
				case Prefix.REPNE:
				case Prefix.REPE:
					if(pre_rep!=Prefix.None)goto REPEATED;
					pre_rep=(Prefix)src[0];
					continue;
				default:
					goto BREAK_FOR;
				REPEATED: // 同じ prefix の重複
					ret.assembly=afh.Enum.GetDescription((Prefix)srcB[0]);
					ret.warning="重複または相反する prefix が続いています。";
					ip++;
					return false;
			}
		BREAK_FOR:
			if(src.IsEnd){
				ret.assembly=afh.Enum.GetDescription((Prefix)src[ip]);
				ret.warning="prefix に続く命令が存在しません。";
				ip++;
				return false;
			}
			return true;
		}
		internal enum Prefix:byte{
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
		//============================================================
		//		Read Command
		//============================================================
		Command cmd=null;
		public bool ReadCommand(){
			uint code=src[0];
			if(src.IsAccessible(1))code|=(uint)src[1]<<8;
			if(src.IsAccessible(2))code|=(uint)src[2]<<8;
			if(pre_rep!=Prefix.None)
				code=code<<8|(byte)pre_rep;
			if(decodevxd&&Command.cmd_vxd.Match(code)) {
				cmd=Command.cmd_vxd;
			}else foreach(Command cd0 in Command.cmds) {
				if(!cd0.Match(code))continue;
				if(shortstringcmds&&cd0.ArgIsSourceOrDest)continue;
				cmd=cd0;
				break;
			}

			if(cmd==null){
				ret.assembly="<未知命令>";
				ret.error="未知の命令です。";
				ip=i+1;
				return false;
			}
			
			if(cmd.Is3DNow){
				int suf=Get3DNowSuffix(i,is_addr2B);
				if(suf<0){
					ret.error+="Cross\r\n";
					return false;
				}else foreach(Command cd0 in Command.cmds_3dnow){
					if(cd0.code>>16==suf){
						cmd=cd0;break;
					}
				}
			}

			if(cmd.size>src.RestLength){
				ret.assembly="<未完命令>";
				ret.error="命令が途中で切れています。";
				ip=src.Length;
				return false;
			}

			return true;
		}
	}
}