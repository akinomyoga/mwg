using System.Diagnostics;
using Interop=System.Runtime.InteropServices;

namespace mwg.InterProcess{
	public unsafe class RemotePtr {
		protected readonly ProcessMemory mem;
		protected readonly byte* _base;
		internal RemotePtr(ProcessMemory mem,byte* _base) {
			this.mem=mem;
			this._base=_base;
		}
		internal RemotePtr(ProcessMemory mem) : this(mem,(byte*)0) { }
	}

	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	//#→template remote_ptr<type,ClassName,propName>
	/// <summary>
	/// type 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class ClassName:RemotePtr{
		internal ClassName(ProcessMemory mem):base(mem){}
		internal ClassName(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを type 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public type this[int index]{
			get{
				type value=default(type);
				mem.ReadMemory(_base+sizeof(type)*index,&value,sizeof(type));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(type)*index,&value,sizeof(type));
			}
		}

		public static ClassName operator+(ClassName acc,int offset){
			return new ClassName(acc.mem,acc._base+sizeof(type)*offset);
		}
		public static ClassName operator+(ClassName acc,System.IntPtr offset){
			return new ClassName(acc.mem,acc._base+sizeof(type)*(long)offset);
		}
		public static ClassName operator-(ClassName acc,int offset){
			return new ClassName(acc.mem,acc._base-sizeof(type)*offset);
		}
		public static ClassName operator-(ClassName acc,System.IntPtr offset){
			return new ClassName(acc.mem,acc._base-sizeof(type)*(long)offset);
		}
		public static explicit operator ClassName(ProcessMemory mem){
			return new ClassName(mem,(byte*)0);
		}
	}
	//#←template
	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	//#remote_ptr<sbyte,RemoteSBytePtr,sbyte_ptr>
	//#remote_ptr<short,RemoteShortPtr,short_ptr>
	//#remote_ptr<int,RemoteIntPtr,int_ptr>
	//#remote_ptr<long,RemoteLongPtr,long_ptr>
	//#remote_ptr<byte,RemoteBytePtr,byte_ptr>
	//#remote_ptr<ushort,RemoteWordPtr,word_ptr>
	//#remote_ptr<uint,RemoteDWordPtr,dword_ptr>
	//#remote_ptr<ulong,RemoteQWordPtr,qword_ptr>
	//#remote_ptr<float,RemoteFloatPtr,float_ptr>
	//#remote_ptr<double,RemoteDoublePtr,double_ptr>
}