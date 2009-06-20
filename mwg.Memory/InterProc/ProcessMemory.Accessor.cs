using System.Diagnostics;
using Interop=System.Runtime.InteropServices;

namespace mwg.InterProcess{
	public partial class ProcessMemory{

		//#→template Accessor<type,ClassName,propName>
		//#define fieldName _##propName
		/// <summary>
		/// ClassName のインスタンスを取得します。
		/// これを使って type 型によるメモリアクセスを行う事が出来ます。
		/// </summary>
		public ClassName propName {
			get { return fieldName??(fieldName=new ClassName(this)); }
		}
		private ClassName fieldName=null;
		/// <summary>
		/// type 型によるメモリアクセスを行います。
		/// </summary>
		public unsafe class ClassName {
			private readonly ProcessMemory mem;
			internal ClassName(ProcessMemory mem){
				this.mem=mem;
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public type this[uint address] {
				get {return this[(void*)address];}
				set {this[(void*)address]=value;}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public type this[int address] {
				get {
					type value=default(type);
					mem.ReadMemory((void*)address,&value,sizeof(type));
					return value;
				}
				set {
					mem.WriteMemory((void*)address,&value,sizeof(type));
				}
			}
			/// <summary>
			/// 指定したアドレスのメモリを sbyte 型の値として取得又は設定します。
			/// </summary>
			/// <param name="address">読み書き対象の仮想アドレスを指定します。</param>
			public type this[void* address] {
				get {
					type value=default(type);
					mem.ReadMemory(address,&value,sizeof(type));
					return value;
				}
				set {
					mem.WriteMemory(address,&value,sizeof(type));
				}
			}
		}
		//#←template
		//#Accessor<sbyte,Int8Accessor,sbyte_ptr>
		//#Accessor<short,Int16Accessor,short_ptr>
		//#Accessor<int,Int32Accessor,int_ptr>
		//#Accessor<long,Int64Accessor,long_ptr>
		//#Accessor<byte,UInt8Accessor,byte_ptr>
		//#Accessor<ushort,UInt16Accessor,word_ptr>
		//#Accessor<uint,UInt32Accessor,dword_ptr>
		//#Accessor<ulong,UInt64Accessor,qword_ptr>
		//#Accessor<float,FloatAccessor,float_ptr>
		//#Accessor<double,DoubleAccessor,double_ptr>
	}
}