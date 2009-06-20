/*
	このソースコードは [afh.Design.dll] afh.Design.TemplateProcessor によって自動的に生成された物です。
	このソースコードを変更しても、このソースコードの元になったファイルを変更しないと変更は適用されません。

	This source code was generated automatically by a file-generator, '[afh.Design.dll] afh.Design.TemplateProcessor'.
	Changes to this source code may not be applied to the binary file, which will cause inconsistency of the whole project.
	If you want to modify any logics in this file, you should change THE SOURCE OF THIS FILE. 
*/
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

	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
	/// <summary>
	/// sbyte 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteSBytePtr:RemotePtr{
		internal RemoteSBytePtr(ProcessMemory mem):base(mem){}
		internal RemoteSBytePtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public sbyte this[int index]{
			get{
				sbyte value=default(sbyte);
				mem.ReadMemory(_base+sizeof(sbyte)*index,&value,sizeof(sbyte));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(sbyte)*index,&value,sizeof(sbyte));
			}
		}

		public static RemoteSBytePtr operator+(RemoteSBytePtr acc,int offset){
			return new RemoteSBytePtr(acc.mem,acc._base+sizeof(sbyte)*offset);
		}
		public static RemoteSBytePtr operator+(RemoteSBytePtr acc,System.IntPtr offset){
			return new RemoteSBytePtr(acc.mem,acc._base+sizeof(sbyte)*(long)offset);
		}
		public static RemoteSBytePtr operator-(RemoteSBytePtr acc,int offset){
			return new RemoteSBytePtr(acc.mem,acc._base-sizeof(sbyte)*offset);
		}
		public static RemoteSBytePtr operator-(RemoteSBytePtr acc,System.IntPtr offset){
			return new RemoteSBytePtr(acc.mem,acc._base-sizeof(sbyte)*(long)offset);
		}
		public static explicit operator RemoteSBytePtr(ProcessMemory mem){
			return new RemoteSBytePtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// short 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteShortPtr:RemotePtr{
		internal RemoteShortPtr(ProcessMemory mem):base(mem){}
		internal RemoteShortPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを short 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public short this[int index]{
			get{
				short value=default(short);
				mem.ReadMemory(_base+sizeof(short)*index,&value,sizeof(short));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(short)*index,&value,sizeof(short));
			}
		}

		public static RemoteShortPtr operator+(RemoteShortPtr acc,int offset){
			return new RemoteShortPtr(acc.mem,acc._base+sizeof(short)*offset);
		}
		public static RemoteShortPtr operator+(RemoteShortPtr acc,System.IntPtr offset){
			return new RemoteShortPtr(acc.mem,acc._base+sizeof(short)*(long)offset);
		}
		public static RemoteShortPtr operator-(RemoteShortPtr acc,int offset){
			return new RemoteShortPtr(acc.mem,acc._base-sizeof(short)*offset);
		}
		public static RemoteShortPtr operator-(RemoteShortPtr acc,System.IntPtr offset){
			return new RemoteShortPtr(acc.mem,acc._base-sizeof(short)*(long)offset);
		}
		public static explicit operator RemoteShortPtr(ProcessMemory mem){
			return new RemoteShortPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// int 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteIntPtr:RemotePtr{
		internal RemoteIntPtr(ProcessMemory mem):base(mem){}
		internal RemoteIntPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを int 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public int this[int index]{
			get{
				int value=default(int);
				mem.ReadMemory(_base+sizeof(int)*index,&value,sizeof(int));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(int)*index,&value,sizeof(int));
			}
		}

		public static RemoteIntPtr operator+(RemoteIntPtr acc,int offset){
			return new RemoteIntPtr(acc.mem,acc._base+sizeof(int)*offset);
		}
		public static RemoteIntPtr operator+(RemoteIntPtr acc,System.IntPtr offset){
			return new RemoteIntPtr(acc.mem,acc._base+sizeof(int)*(long)offset);
		}
		public static RemoteIntPtr operator-(RemoteIntPtr acc,int offset){
			return new RemoteIntPtr(acc.mem,acc._base-sizeof(int)*offset);
		}
		public static RemoteIntPtr operator-(RemoteIntPtr acc,System.IntPtr offset){
			return new RemoteIntPtr(acc.mem,acc._base-sizeof(int)*(long)offset);
		}
		public static explicit operator RemoteIntPtr(ProcessMemory mem){
			return new RemoteIntPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// long 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteLongPtr:RemotePtr{
		internal RemoteLongPtr(ProcessMemory mem):base(mem){}
		internal RemoteLongPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを long 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public long this[int index]{
			get{
				long value=default(long);
				mem.ReadMemory(_base+sizeof(long)*index,&value,sizeof(long));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(long)*index,&value,sizeof(long));
			}
		}

		public static RemoteLongPtr operator+(RemoteLongPtr acc,int offset){
			return new RemoteLongPtr(acc.mem,acc._base+sizeof(long)*offset);
		}
		public static RemoteLongPtr operator+(RemoteLongPtr acc,System.IntPtr offset){
			return new RemoteLongPtr(acc.mem,acc._base+sizeof(long)*(long)offset);
		}
		public static RemoteLongPtr operator-(RemoteLongPtr acc,int offset){
			return new RemoteLongPtr(acc.mem,acc._base-sizeof(long)*offset);
		}
		public static RemoteLongPtr operator-(RemoteLongPtr acc,System.IntPtr offset){
			return new RemoteLongPtr(acc.mem,acc._base-sizeof(long)*(long)offset);
		}
		public static explicit operator RemoteLongPtr(ProcessMemory mem){
			return new RemoteLongPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// byte 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteBytePtr:RemotePtr{
		internal RemoteBytePtr(ProcessMemory mem):base(mem){}
		internal RemoteBytePtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを byte 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public byte this[int index]{
			get{
				byte value=default(byte);
				mem.ReadMemory(_base+sizeof(byte)*index,&value,sizeof(byte));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(byte)*index,&value,sizeof(byte));
			}
		}

		public static RemoteBytePtr operator+(RemoteBytePtr acc,int offset){
			return new RemoteBytePtr(acc.mem,acc._base+sizeof(byte)*offset);
		}
		public static RemoteBytePtr operator+(RemoteBytePtr acc,System.IntPtr offset){
			return new RemoteBytePtr(acc.mem,acc._base+sizeof(byte)*(long)offset);
		}
		public static RemoteBytePtr operator-(RemoteBytePtr acc,int offset){
			return new RemoteBytePtr(acc.mem,acc._base-sizeof(byte)*offset);
		}
		public static RemoteBytePtr operator-(RemoteBytePtr acc,System.IntPtr offset){
			return new RemoteBytePtr(acc.mem,acc._base-sizeof(byte)*(long)offset);
		}
		public static explicit operator RemoteBytePtr(ProcessMemory mem){
			return new RemoteBytePtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// ushort 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteWordPtr:RemotePtr{
		internal RemoteWordPtr(ProcessMemory mem):base(mem){}
		internal RemoteWordPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを ushort 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public ushort this[int index]{
			get{
				ushort value=default(ushort);
				mem.ReadMemory(_base+sizeof(ushort)*index,&value,sizeof(ushort));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(ushort)*index,&value,sizeof(ushort));
			}
		}

		public static RemoteWordPtr operator+(RemoteWordPtr acc,int offset){
			return new RemoteWordPtr(acc.mem,acc._base+sizeof(ushort)*offset);
		}
		public static RemoteWordPtr operator+(RemoteWordPtr acc,System.IntPtr offset){
			return new RemoteWordPtr(acc.mem,acc._base+sizeof(ushort)*(long)offset);
		}
		public static RemoteWordPtr operator-(RemoteWordPtr acc,int offset){
			return new RemoteWordPtr(acc.mem,acc._base-sizeof(ushort)*offset);
		}
		public static RemoteWordPtr operator-(RemoteWordPtr acc,System.IntPtr offset){
			return new RemoteWordPtr(acc.mem,acc._base-sizeof(ushort)*(long)offset);
		}
		public static explicit operator RemoteWordPtr(ProcessMemory mem){
			return new RemoteWordPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// uint 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteDWordPtr:RemotePtr{
		internal RemoteDWordPtr(ProcessMemory mem):base(mem){}
		internal RemoteDWordPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを uint 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public uint this[int index]{
			get{
				uint value=default(uint);
				mem.ReadMemory(_base+sizeof(uint)*index,&value,sizeof(uint));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(uint)*index,&value,sizeof(uint));
			}
		}

		public static RemoteDWordPtr operator+(RemoteDWordPtr acc,int offset){
			return new RemoteDWordPtr(acc.mem,acc._base+sizeof(uint)*offset);
		}
		public static RemoteDWordPtr operator+(RemoteDWordPtr acc,System.IntPtr offset){
			return new RemoteDWordPtr(acc.mem,acc._base+sizeof(uint)*(long)offset);
		}
		public static RemoteDWordPtr operator-(RemoteDWordPtr acc,int offset){
			return new RemoteDWordPtr(acc.mem,acc._base-sizeof(uint)*offset);
		}
		public static RemoteDWordPtr operator-(RemoteDWordPtr acc,System.IntPtr offset){
			return new RemoteDWordPtr(acc.mem,acc._base-sizeof(uint)*(long)offset);
		}
		public static explicit operator RemoteDWordPtr(ProcessMemory mem){
			return new RemoteDWordPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// ulong 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteQWordPtr:RemotePtr{
		internal RemoteQWordPtr(ProcessMemory mem):base(mem){}
		internal RemoteQWordPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを ulong 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public ulong this[int index]{
			get{
				ulong value=default(ulong);
				mem.ReadMemory(_base+sizeof(ulong)*index,&value,sizeof(ulong));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(ulong)*index,&value,sizeof(ulong));
			}
		}

		public static RemoteQWordPtr operator+(RemoteQWordPtr acc,int offset){
			return new RemoteQWordPtr(acc.mem,acc._base+sizeof(ulong)*offset);
		}
		public static RemoteQWordPtr operator+(RemoteQWordPtr acc,System.IntPtr offset){
			return new RemoteQWordPtr(acc.mem,acc._base+sizeof(ulong)*(long)offset);
		}
		public static RemoteQWordPtr operator-(RemoteQWordPtr acc,int offset){
			return new RemoteQWordPtr(acc.mem,acc._base-sizeof(ulong)*offset);
		}
		public static RemoteQWordPtr operator-(RemoteQWordPtr acc,System.IntPtr offset){
			return new RemoteQWordPtr(acc.mem,acc._base-sizeof(ulong)*(long)offset);
		}
		public static explicit operator RemoteQWordPtr(ProcessMemory mem){
			return new RemoteQWordPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// float 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteFloatPtr:RemotePtr{
		internal RemoteFloatPtr(ProcessMemory mem):base(mem){}
		internal RemoteFloatPtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを float 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public float this[int index]{
			get{
				float value=default(float);
				mem.ReadMemory(_base+sizeof(float)*index,&value,sizeof(float));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(float)*index,&value,sizeof(float));
			}
		}

		public static RemoteFloatPtr operator+(RemoteFloatPtr acc,int offset){
			return new RemoteFloatPtr(acc.mem,acc._base+sizeof(float)*offset);
		}
		public static RemoteFloatPtr operator+(RemoteFloatPtr acc,System.IntPtr offset){
			return new RemoteFloatPtr(acc.mem,acc._base+sizeof(float)*(long)offset);
		}
		public static RemoteFloatPtr operator-(RemoteFloatPtr acc,int offset){
			return new RemoteFloatPtr(acc.mem,acc._base-sizeof(float)*offset);
		}
		public static RemoteFloatPtr operator-(RemoteFloatPtr acc,System.IntPtr offset){
			return new RemoteFloatPtr(acc.mem,acc._base-sizeof(float)*(long)offset);
		}
		public static explicit operator RemoteFloatPtr(ProcessMemory mem){
			return new RemoteFloatPtr(mem,(byte*)0);
		}
	}
	/// <summary>
	/// double 型によるメモリアクセスを行います。
	/// </summary>
	public unsafe class RemoteDoublePtr:RemotePtr{
		internal RemoteDoublePtr(ProcessMemory mem):base(mem){}
		internal RemoteDoublePtr(ProcessMemory mem,byte* _base):base(mem,_base){ }
		/// <summary>
		/// 指定したアドレスのメモリを double 型の値として取得又は設定します。
		/// </summary>
		/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
		public double this[int index]{
			get{
				double value=default(double);
				mem.ReadMemory(_base+sizeof(double)*index,&value,sizeof(double));
				return value;
			}
			set{
				mem.WriteMemory(_base+sizeof(double)*index,&value,sizeof(double));
			}
		}

		public static RemoteDoublePtr operator+(RemoteDoublePtr acc,int offset){
			return new RemoteDoublePtr(acc.mem,acc._base+sizeof(double)*offset);
		}
		public static RemoteDoublePtr operator+(RemoteDoublePtr acc,System.IntPtr offset){
			return new RemoteDoublePtr(acc.mem,acc._base+sizeof(double)*(long)offset);
		}
		public static RemoteDoublePtr operator-(RemoteDoublePtr acc,int offset){
			return new RemoteDoublePtr(acc.mem,acc._base-sizeof(double)*offset);
		}
		public static RemoteDoublePtr operator-(RemoteDoublePtr acc,System.IntPtr offset){
			return new RemoteDoublePtr(acc.mem,acc._base-sizeof(double)*(long)offset);
		}
		public static explicit operator RemoteDoublePtr(ProcessMemory mem){
			return new RemoteDoublePtr(mem,(byte*)0);
		}
	}
}