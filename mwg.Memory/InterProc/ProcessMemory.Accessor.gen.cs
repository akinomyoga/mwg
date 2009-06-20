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
	public partial class ProcessMemory{


		//#define fieldName _sbyte_ptr
		/// <summary>
		/// Int8Accessor のインスタンスを取得します。
		/// これを使って sbyte 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public Int8Accessor sbyte_ptr {
			get { return _sbyte_ptr??(_sbyte_ptr=new Int8Accessor(this)); }
		}
		private Int8Accessor _sbyte_ptr=null;
		/// <summary>
		/// sbyte 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class Int8Accessor {
			private readonly ProcessMemory mem;
			internal Int8Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public sbyte this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public sbyte this[int address] {
				get {
					sbyte value=default(sbyte);
					mem.ReadMemory((void*)address,&value,sizeof(sbyte));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(sbyte));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public sbyte this[void* address] {
				get {
					sbyte value=default(sbyte);
					mem.ReadMemory(address,&value,sizeof(sbyte));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(sbyte));
				}
			}
		}
		//#define _sbyte_ptr _short_ptr
		/// <summary>
		/// Int16Accessor のインスタンスを取得します。
		/// これを使って short 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public Int16Accessor short_ptr {
			get { return _short_ptr??(_short_ptr=new Int16Accessor(this)); }
		}
		private Int16Accessor _short_ptr=null;
		/// <summary>
		/// short 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class Int16Accessor {
			private readonly ProcessMemory mem;
			internal Int16Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public short this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public short this[int address] {
				get {
					short value=default(short);
					mem.ReadMemory((void*)address,&value,sizeof(short));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(short));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public short this[void* address] {
				get {
					short value=default(short);
					mem.ReadMemory(address,&value,sizeof(short));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(short));
				}
			}
		}
		//#define _short_ptr _int_ptr
		/// <summary>
		/// Int32Accessor のインスタンスを取得します。
		/// これを使って int 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public Int32Accessor int_ptr {
			get { return _int_ptr??(_int_ptr=new Int32Accessor(this)); }
		}
		private Int32Accessor _int_ptr=null;
		/// <summary>
		/// int 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class Int32Accessor {
			private readonly ProcessMemory mem;
			internal Int32Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public int this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public int this[int address] {
				get {
					int value=default(int);
					mem.ReadMemory((void*)address,&value,sizeof(int));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(int));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public int this[void* address] {
				get {
					int value=default(int);
					mem.ReadMemory(address,&value,sizeof(int));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(int));
				}
			}
		}
		//#define _int_ptr _long_ptr
		/// <summary>
		/// Int64Accessor のインスタンスを取得します。
		/// これを使って long 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public Int64Accessor long_ptr {
			get { return _long_ptr??(_long_ptr=new Int64Accessor(this)); }
		}
		private Int64Accessor _long_ptr=null;
		/// <summary>
		/// long 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class Int64Accessor {
			private readonly ProcessMemory mem;
			internal Int64Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public long this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public long this[int address] {
				get {
					long value=default(long);
					mem.ReadMemory((void*)address,&value,sizeof(long));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(long));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public long this[void* address] {
				get {
					long value=default(long);
					mem.ReadMemory(address,&value,sizeof(long));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(long));
				}
			}
		}
		//#define _long_ptr _byte_ptr
		/// <summary>
		/// UInt8Accessor のインスタンスを取得します。
		/// これを使って byte 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public UInt8Accessor byte_ptr {
			get { return _byte_ptr??(_byte_ptr=new UInt8Accessor(this)); }
		}
		private UInt8Accessor _byte_ptr=null;
		/// <summary>
		/// byte 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class UInt8Accessor {
			private readonly ProcessMemory mem;
			internal UInt8Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public byte this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public byte this[int address] {
				get {
					byte value=default(byte);
					mem.ReadMemory((void*)address,&value,sizeof(byte));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(byte));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public byte this[void* address] {
				get {
					byte value=default(byte);
					mem.ReadMemory(address,&value,sizeof(byte));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(byte));
				}
			}
		}
		//#define _byte_ptr _word_ptr
		/// <summary>
		/// UInt16Accessor のインスタンスを取得します。
		/// これを使って ushort 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public UInt16Accessor word_ptr {
			get { return _word_ptr??(_word_ptr=new UInt16Accessor(this)); }
		}
		private UInt16Accessor _word_ptr=null;
		/// <summary>
		/// ushort 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class UInt16Accessor {
			private readonly ProcessMemory mem;
			internal UInt16Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public ushort this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public ushort this[int address] {
				get {
					ushort value=default(ushort);
					mem.ReadMemory((void*)address,&value,sizeof(ushort));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(ushort));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public ushort this[void* address] {
				get {
					ushort value=default(ushort);
					mem.ReadMemory(address,&value,sizeof(ushort));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(ushort));
				}
			}
		}
		//#define _word_ptr _dword_ptr
		/// <summary>
		/// UInt32Accessor のインスタンスを取得します。
		/// これを使って uint 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public UInt32Accessor dword_ptr {
			get { return _dword_ptr??(_dword_ptr=new UInt32Accessor(this)); }
		}
		private UInt32Accessor _dword_ptr=null;
		/// <summary>
		/// uint 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class UInt32Accessor {
			private readonly ProcessMemory mem;
			internal UInt32Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public uint this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public uint this[int address] {
				get {
					uint value=default(uint);
					mem.ReadMemory((void*)address,&value,sizeof(uint));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(uint));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public uint this[void* address] {
				get {
					uint value=default(uint);
					mem.ReadMemory(address,&value,sizeof(uint));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(uint));
				}
			}
		}
		//#define _dword_ptr _qword_ptr
		/// <summary>
		/// UInt64Accessor のインスタンスを取得します。
		/// これを使って ulong 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public UInt64Accessor qword_ptr {
			get { return _qword_ptr??(_qword_ptr=new UInt64Accessor(this)); }
		}
		private UInt64Accessor _qword_ptr=null;
		/// <summary>
		/// ulong 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class UInt64Accessor {
			private readonly ProcessMemory mem;
			internal UInt64Accessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public ulong this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public ulong this[int address] {
				get {
					ulong value=default(ulong);
					mem.ReadMemory((void*)address,&value,sizeof(ulong));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(ulong));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public ulong this[void* address] {
				get {
					ulong value=default(ulong);
					mem.ReadMemory(address,&value,sizeof(ulong));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(ulong));
				}
			}
		}
		//#define _qword_ptr _float_ptr
		/// <summary>
		/// FloatAccessor のインスタンスを取得します。
		/// これを使って float 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public FloatAccessor float_ptr {
			get { return _float_ptr??(_float_ptr=new FloatAccessor(this)); }
		}
		private FloatAccessor _float_ptr=null;
		/// <summary>
		/// float 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class FloatAccessor {
			private readonly ProcessMemory mem;
			internal FloatAccessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public float this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public float this[int address] {
				get {
					float value=default(float);
					mem.ReadMemory((void*)address,&value,sizeof(float));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(float));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public float this[void* address] {
				get {
					float value=default(float);
					mem.ReadMemory(address,&value,sizeof(float));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(float));
				}
			}
		}
		//#define _float_ptr _double_ptr
		/// <summary>
		/// DoubleAccessor のインスタンスを取得します。
		/// これを使って double 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public DoubleAccessor double_ptr {
			get { return _double_ptr??(_double_ptr=new DoubleAccessor(this)); }
		}
		private DoubleAccessor _double_ptr=null;
		/// <summary>
		/// double 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class DoubleAccessor {
			private readonly ProcessMemory mem;
			internal DoubleAccessor(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public double this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public double this[int address] {
				get {
					double value=default(double);
					mem.ReadMemory((void*)address,&value,sizeof(double));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(double));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public double this[void* address] {
				get {
					double value=default(double);
					mem.ReadMemory(address,&value,sizeof(double));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(double));
				}
			}
		}
	}
}