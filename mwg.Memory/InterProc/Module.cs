using Gen=System.Collections.Generic;
using Diag=System.Diagnostics;
using remote_ptr=mwg.InterProcess.RemotePtr<byte>;
using mwg.Win32;
using Interop=System.Runtime.InteropServices;
using CM=System.ComponentModel;

// require ProcessMemory.cs
// require RemotePtr.cs
// require mwg.WinNT_IMAGE.cs

namespace mwg.InterProcess{

	/// <summary>
	/// 任意のプロセス内に在るモジュールに関する情報を提供します。
	/// </summary>
	public class Module{
		internal ProcessMemory mem;
		private readonly Diag::ProcessModule mod;

		internal readonly remote_ptr mbase;
		private readonly RemotePtr<IMAGE.DOS_HEADER> dhead
			=default(RemotePtr<IMAGE.DOS_HEADER>);
		private readonly RemotePtr<IMAGE.FILE_HEADER> chead
			=default(RemotePtr<IMAGE.FILE_HEADER>);

		internal readonly IMAGE.OPTIONAL_MAGIC omagic
			=default(IMAGE.OPTIONAL_MAGIC);
		private readonly RemotePtr<IMAGE.STD_OPTIONAL_HEADER> ohead
			=default(RemotePtr<IMAGE.STD_OPTIONAL_HEADER>);
		private RemotePtr<IMAGE.NT32_OPTIONAL_HEADER> ohead32{
			get{
				if(omagic!=IMAGE.OPTIONAL_MAGIC.NT_HDR32)
					throw new System.InvalidOperationException("このモジュールは PE32 形式ではありません。");
				return ohead.Reinterpret<IMAGE.NT32_OPTIONAL_HEADER>();
			}
		}
		private RemotePtr<IMAGE.NT64_OPTIONAL_HEADER> ohead64{
			get{
				if(omagic!=IMAGE.OPTIONAL_MAGIC.NT_HDR64)
					throw new System.InvalidOperationException("このモジュールは PE32+ 形式ではありません。");
				return ohead.Reinterpret<IMAGE.NT64_OPTIONAL_HEADER>();
			}
		}
		private RemotePtr<IMAGE.ROM_OPTIONAL_HEADER> ohead_rom{
			get{
				if(omagic!=IMAGE.OPTIONAL_MAGIC.ROM_HDR)
					throw new System.InvalidOperationException("このモジュールは ROM Image ではありません。");
				return ohead.Reinterpret<IMAGE.ROM_OPTIONAL_HEADER>();
			}
		}

		public Module(ProcessMemory mem,Diag::ProcessModule mod){
			this.mem=mem;
			this.mod=mod;

			this.mbase=mem.GetPtr(mod.BaseAddress);
			
			// DOS Header
			this.dhead=mbase.Reinterpret<IMAGE.DOS_HEADER>();
			if(dhead[0].magic!=IMAGE.SIGNATURE.DOS){
				dhead=default(RemotePtr<IMAGE.DOS_HEADER>);
				return;
			}

			// COFF Header
			remote_ptr ptr=mbase+dhead[0].lfanew;
			if(ptr.Read<uint>()!=(uint)IMAGE.SIGNATURE.NT){
				return;
			}
			this.chead=(ptr+4).Reinterpret<IMAGE.FILE_HEADER>();

			// Optional Header
			ohead=(chead+1).Reinterpret<IMAGE.STD_OPTIONAL_HEADER>();
			omagic=ohead[0].Magic;
		}

		[CM::Browsable(false)]
		public Diag::ProcessModule ClrModule{
			get{return this.mod;}
		}
		/// <summary>
		/// モジュール名を取得します。
		/// </summary>
		public string Name{
			get{return this.mod.ModuleName;}
		}
		/// <summary>
		/// ファイル名を取得します。
		/// </summary>
		public string FileName{
			get{return this.mod.FileName;}
		}
		/// <summary>
		/// バージョン情報を取得します。
		/// </summary>
		[CM::ReadOnly(true)]
		[CM::TypeConverter(typeof(CM::ExpandableObjectConverter))]
		public Diag::FileVersionInfo FileVersion{
			get{return this.mod.FileVersionInfo;}
		}
		/// <summary>
		/// モジュールのロードされている基底アドレスを取得します。
		/// </summary>
		public System.IntPtr BaseAddress{
			get{return this.mod.BaseAddress;}
		}
		/// <summary>
		/// モジュールの占有しているメモリサイズを取得します。
		/// </summary>
		public int MemorySize{
			get{return this.mod.ModuleMemorySize;}
		}

		#region -- Properties --
		//------------------------------------------------------------
		//		COFF Header
		//------------------------------------------------------------
		// chead[0].SizeOfOptionalHeader
		// chead[0].PointerToSymbolTable
		public bool HasCoffHeader{
			get{return !chead.IsNull;}
		}
		[CM::Category("COFF Header")]
		public IMAGE.FILE_HEADER_MACHINE Machine{
			get{
				if(chead.IsNull)return IMAGE.FILE_HEADER_MACHINE.UNKNOWN;
				return chead[0].machine;
			}
		}
		[CM::Category("COFF Header")]
		public int SectionCount{
			get{
				if(chead.IsNull)return 0;
				return chead[0].NumberOfSections;
			}
		}
		[CM::Category("COFF Header")]
		public int SymbolCount{
			get{
				if(chead.IsNull)return 0;
				return (int)chead[0].NumberOfSymbols; // unsafe
			}
		}
		[CM::Category("COFF Header")]
		public System.DateTime TimeStamp{
			get{
				if(chead.IsNull)return new System.DateTime(0L);
				return chead[0].TimeDateStamp;
			}
		}
		[CM::Category("COFF Header")]
		public IMAGE.FILE_CHARACTER Characteristics{
			get{
				if(chead.IsNull)return default(IMAGE.FILE_CHARACTER);
				return chead[0].Characteristics;
			}
		}
		[CM::Category("COFF Header")]
		[CM::DisplayName("Pointer to SymbolTable")]
		public uint pSymbolTable{
			get{
				if(chead.IsNull)return 0;
				return chead[0].PointerToSymbolTable;
			}
		}
		[CM::Category("COFF Header")]
		public ushort OptionalHeaderSize{
			get{
				if(chead.IsNull)return 0;
				return chead[0].SizeOfOptionalHeader;
			}
		}
		//------------------------------------------------------------
		//		Optinal Header Standard Fields
		//------------------------------------------------------------
		[CM::Category("Optional Header")]
		public System.Version LinkerVersion{
			get{
				if(ohead.IsNull)return new System.Version(0,0);
				return new System.Version(ohead[0].majorLinkerVersion,ohead[0].minorLinkerVersion);
			}
		}
		[CM::Category("Optional Header")]
		public uint SizeOfCode{
			get{
				if(ohead.IsNull)return 0;
				return ohead[0].SizeOfCode;
			}
		}
		[CM::Category("Optional Header")]
		public uint SizeOfInitialized{
			get{
				if(ohead.IsNull)return 0;
				return ohead[0].SizeOfInitializedData;
			}
		}
		[CM::Category("Optional Header")]
		public uint SizeOfUninitialized{
			get{
				if(ohead.IsNull)return 0;
				return ohead[0].SizeOfUninitializedData;
			}
		}
		[CM::Category("Optional Header")]
		[CM::DisplayName("Pointer to EntryPoint")]
		public unsafe remote_ptr pEntryPoint{
			get{
				if(ohead.IsNull)return default(remote_ptr);
				uint rva=ohead[0].AddressOfEntryPoint;
				return this.mem.GetPtr(rva==0?(byte*)0:(byte*)mod.BaseAddress+rva);
			}
		}
		[CM::Category("Optional Header")]
		public unsafe remote_ptr BaseOfCode{
			get{
				if(ohead.IsNull)return default(remote_ptr);
				uint rva=ohead[0].BaseOfCode;
				return this.mem.GetPtr(rva==0?(byte*)0:(byte*)mod.BaseAddress+rva);
			}
		}

		//------------------------------------------------------------
		//		Optinal Header Special Fields
		//------------------------------------------------------------
		[CM::Category("Optional Header Special")]
		[CM::DisplayName("Pointer to CodeSection")]
		public unsafe remote_ptr pCodeSection{
			get{
				uint rva32;
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						if((rva32=ohead32[0].BaseOfData)==0)goto default;
						return this.mem.GetPtr((byte*)mod.BaseAddress+rva32);
					case IMAGE.OPTIONAL_MAGIC.ROM_HDR:
						if((rva32=ohead32[0].BaseOfData)==0)goto default;
						rva32=ohead_rom[0].BaseOfData;
						return this.mem.GetPtr((byte*)mod.BaseAddress+rva32);
					default:
						return default(remote_ptr);
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.IntPtr PreferredBase{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return (System.IntPtr)ohead32[0].ImageBase;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return (System.IntPtr)ohead64[0].ImageBase;
					default:
						return System.IntPtr.Zero;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public uint SectionAlignment{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].SectionAlignment;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].SectionAlignment;
					default:return 0;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public uint FileAlignment{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].FileAlignment;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].FileAlignment;
					default:return 0;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.Version OsVersion{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						IMAGE.NT32_OPTIONAL_HEADER oheader32=ohead32[0];
						return new System.Version(
							oheader32.majorOperatingSystemVersion,
							oheader32.minorOperatingSystemVersion);
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						IMAGE.NT64_OPTIONAL_HEADER oheader64=ohead64[0];
						return new System.Version(
							oheader64.MajorOperatingSystemVersion,
							oheader64.MinorOperatingSystemVersion);
					default:return new System.Version(0,0);
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.Version ImageVersion{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						IMAGE.NT32_OPTIONAL_HEADER oheader32=ohead32[0];
						return new System.Version(
							oheader32.majorImageVersion,
							oheader32.minorImageVersion);
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						IMAGE.NT64_OPTIONAL_HEADER oheader64=ohead64[0];
						return new System.Version(
							oheader64.MajorImageVersion,
							oheader64.MinorImageVersion);
					default:return new System.Version(0,0);
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.Version SubsystemVersion{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						IMAGE.NT32_OPTIONAL_HEADER oheader32=ohead32[0];
						return new System.Version(
							oheader32.majorSubsystemVersion,
							oheader32.minorSubsystemVersion);
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						IMAGE.NT64_OPTIONAL_HEADER oheader64=ohead64[0];
						return new System.Version(
							oheader64.MajorSubsystemVersion,
							oheader64.MinorSubsystemVersion);
					default:return new System.Version(0,0);
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public uint Win32Version{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].Win32VersionValue;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].Win32VersionValue;
					default:return 0;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public uint ImageSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].SizeOfImage;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].SizeOfImage;
					default:return 0;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public uint HeadersSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].SizeOfHeaders;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].SizeOfHeaders;
					default:return 0;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public uint CheckSum{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].CheckSum;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].CheckSum;
					default:return 0;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public IMAGE.SUBSYSTEM Subsystem{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].Subsystem;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].Subsystem;
					default:return IMAGE.SUBSYSTEM.UNKNOWN;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public IMAGE.DLLCHARACTERISTICS DllCharacteristics{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].DllCharacteristics;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].DllCharacteristics;
					default:
						return default(IMAGE.DLLCHARACTERISTICS);
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.IntPtr StackReservedSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return (System.IntPtr)ohead32[0].SizeOfStackReserve;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return (System.IntPtr)ohead64[0].SizeOfStackReserve;
					default:
						return System.IntPtr.Zero;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.IntPtr StackCommitSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return (System.IntPtr)ohead32[0].SizeOfStackCommit;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return (System.IntPtr)ohead64[0].SizeOfStackCommit;
					default:
						return System.IntPtr.Zero;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.IntPtr HeapReservedSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return (System.IntPtr)ohead32[0].SizeOfHeapReserve;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return (System.IntPtr)ohead64[0].SizeOfHeapReserve;
					default:
						return System.IntPtr.Zero;
				}
			}
		}
		[CM::Category("Optional Header Special")]
		public System.IntPtr HeapCommitSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return (System.IntPtr)ohead32[0].SizeOfHeapCommit;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return (System.IntPtr)ohead64[0].SizeOfHeapCommit;
					default:
						return System.IntPtr.Zero;
				}
			}
		}
		#endregion

		//------------------------------------------------------------
		//		DataDirectory
		//------------------------------------------------------------
		private ImageDataDirectoryCollection directories=null;
		[CM::Browsable(false)]
		public ImageDataDirectoryCollection Directories{
			get{return directories??(directories=new ImageDataDirectoryCollection(this));}
		}
		/// <summary>
		/// ImageDataDirectoryCollection から使う為のプロパティです。
		/// </summary>
		internal IMAGE.DATA_DIRECTORY_Array DataDirectory{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return ohead32[0].DataDirectory;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return ohead64[0].DataDirectory;
					default:
						return default(IMAGE.DATA_DIRECTORY_Array);
				}
			}
		}
		/// <summary>
		/// ImageDataDirectoryCollection から使う為のプロパティです。
		/// </summary>
		internal int CountOfRvaAndSize{
			get{
				switch(omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						return (int)ohead32[0].NumberOfRvaAndSizes;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						return (int)ohead64[0].NumberOfRvaAndSizes;
					default:return 0;
				}
			}
		}
	}

	/// <summary>
	/// 或るモジュール内のデータディレクトリを列挙します。
	/// </summary>
	public class ImageDataDirectoryCollection:Gen::IEnumerable<ImageDataDirectory>{
		private readonly Module module;
		private readonly IMAGE.DATA_DIRECTORY_Array arr;

		internal ImageDataDirectoryCollection(Module module){
			this.module=module;
			this.arr=module.DataDirectory;
		}
		/// <summary>
		/// イメージ内にあるディレクトリの数を取得します。
		/// </summary>
		public int Count{
			get{return module.CountOfRvaAndSize;}
		}
		/// <summary>
		/// 指定した種類のデータディレクトリを取得します。
		/// </summary>
		/// <param name="index">取得するデータディレクトリの種類を指定します。</param>
		/// <returns></returns>
		public ImageDataDirectory this[IMAGE.DIRECTORY_ENTRY index]{
			get{return ImageDataDirectory.Create(this.module,arr[index],index);}
		}
		/// <summary>
		/// データディレクトリを列挙します。
		/// </summary>
		/// <returns>データディレクトリの列挙子を返します。</returns>
		public Gen::IEnumerator<ImageDataDirectory> GetEnumerator(){
			for(int i=0,iM=this.Count;i<iM;i++){
				if(arr[(IMAGE.DIRECTORY_ENTRY)i].Size==0)continue;
				yield return this[(IMAGE.DIRECTORY_ENTRY)i];
			}
		}
		/// <summary>
		/// 指定した種類のデータディレクトリが存在しているか否かを取得します。
		/// </summary>
		/// <param name="index">存在を確認したいデータディレクトリの種類を指定します。</param>
		/// <returns>指定した種類のデータディレクトリが存在していた場合に true を返します。
		/// それ以外の場合に false を返します。</returns>
		public bool HasDirectory(IMAGE.DIRECTORY_ENTRY index){
			return (int)index<this.Count&&arr[index].Size>0;
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return this.GetEnumerator();
		}
	}

	/// <summary>
	/// データディレクトリの基本クラスです。
	/// </summary>
	public class ImageDataDirectory{
		protected readonly Module module;
		protected readonly IMAGE.DATA_DIRECTORY data;
		protected readonly IMAGE.DIRECTORY_ENTRY type;
		protected ImageDataDirectory(Module module,IMAGE.DATA_DIRECTORY data,IMAGE.DIRECTORY_ENTRY type){
			this.module=module;
			this.data=data;
			this.type=type;
		}

		public static ImageDataDirectory Create(Module module,IMAGE.DATA_DIRECTORY data,IMAGE.DIRECTORY_ENTRY type){
			switch(type){
				case IMAGE.DIRECTORY_ENTRY.IMPORT:
					return new ImageImportDirectory(module,data,type);
				default:
					return new ImageDataDirectory(module,data,type);
			}
		}
		/// <summary>
		/// ディレクトリの実体へのポインタを取得します。
		/// </summary>
		public unsafe remote_ptr pData{
			get{
				if(data.VirtualAddress==0)return module.mem.GetPtr((void*)0);
				return module.mbase+data.VirtualAddress;
			}
		}
		/// <summary>
		/// 実体のデータの大きさを取得します。
		/// </summary>
		public uint DataSize{
			get{return data.Size;}
		}
		/// <summary>
		/// ディレクトリの種類を取得します。
		/// </summary>
		public IMAGE.DIRECTORY_ENTRY DirectoryType{
			get{return this.type;}
		}
	}

	#region Import Directory
	/// <summary>
	/// Import 表を表現する Directory です。
	/// </summary>
	public class ImageImportDirectory:ImageDataDirectory,Gen::IEnumerable<ImageImportDirectory.ImportModule>{
		private int modcount;
		RemotePtr<IMAGE.IMPORT_DESCRIPTOR> descs;

		internal ImageImportDirectory(
			Module module,
			IMAGE.DATA_DIRECTORY data,
			IMAGE.DIRECTORY_ENTRY type
		):base(module,data,type){
			unsafe{
				modcount=(int)(data.Size/sizeof(IMAGE.IMPORT_DESCRIPTOR))-1;
				//modcount--; // 最後の空項目を除く
			}
			descs=base.pData.Reinterpret<IMAGE.IMPORT_DESCRIPTOR>();
		}
		/// <summary>
		/// 指定した番号の ImportModule を取得します。
		/// </summary>
		/// <param name="index">取得する ImportModule の番号を指定します。</param>
		/// <returns>指定した番号に対応する ImportModule を返します。</returns>
		public ImportModule this[int index]{
			get{
				if(index<0||modcount<=index)
					throw new System.ArgumentOutOfRangeException("index");
				return new ImportModule(module,this.descs[index]);
			}
		}
		/// <summary>
		/// 含まれている ImportModule の個数を取得します。
		/// </summary>
		[CM::Description("インポート元モジュールの個数を取得します。")]
		public int Count{
			get{return modcount;}
		}

		public Gen::IEnumerator<ImportModule> GetEnumerator(){
			for(int i=0,iM=this.Count;i<iM;i++)yield return this[i];
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return this.GetEnumerator();
		}

		/// <summary>
		/// Import Directory 内のインポート元モジュールを表現します。
		/// </summary>
		public class ImportModule:Gen::IEnumerable<ImportFunction>{
			Module module;
			IMAGE.IMPORT_DESCRIPTOR desc;
			RemotePtr<IMAGE.THUNK_DATA32> pIAT32=default(RemotePtr<IMAGE.THUNK_DATA32>);
			RemotePtr<IMAGE.THUNK_DATA32> pINT32=default(RemotePtr<IMAGE.THUNK_DATA32>);
			RemotePtr<IMAGE.THUNK_DATA64> pIAT64=default(RemotePtr<IMAGE.THUNK_DATA64>);
			RemotePtr<IMAGE.THUNK_DATA64> pINT64=default(RemotePtr<IMAGE.THUNK_DATA64>);
			int funccount;

			internal ImportModule(Module module,IMAGE.IMPORT_DESCRIPTOR desc){
				this.module=module;
				this.desc=desc;

				int i=0;
				if(desc.FirstThunk!=0)switch(module.omagic){
					case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
						this.pIAT32=(module.mbase+(int)desc.FirstThunk).Reinterpret<IMAGE.THUNK_DATA32>();
						this.pINT32=(module.mbase+(int)desc.OriginalFirstThunk).Reinterpret<IMAGE.THUNK_DATA32>();
						while(pIAT32[i].Function!=0)i++;
						break;
					case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
						this.pIAT64=(module.mbase+(int)desc.FirstThunk).Reinterpret<IMAGE.THUNK_DATA64>();
						this.pINT64=(module.mbase+(int)desc.OriginalFirstThunk).Reinterpret<IMAGE.THUNK_DATA64>();
						while(pIAT64[i].Function!=0)i++;
						break;
					default:
						throw new System.InvalidProgramException(
							"ROM Image には ImportTable は存在しない筈です。\r\n"
							+"このオブジェクトが存在することはプログラムとして誤っています。");
				
				}
				this.funccount=i;
			}
			/// <summary>
			/// Import 下の Dll の名前を取得します。
			/// </summary>
			public string Name{
				get{return (module.mbase+(int)desc.pstrName).ReadAnsiString();}
			}
			public uint ForwarderChain{
				get{return desc.ForwarderChain;}
			}
			/// <summary>
			/// Import した dll の時刻を取得します。
			/// 実際にリンクが完了していない場合には System.DateTime.MaxValue を返します。
			/// </summary>
			public unsafe System.DateTime TimeDateStamp{
				get{
					int time=(int)desc.TimeDateStamp;
					if(time==0)return System.DateTime.MinValue;

					// TODO: 本当の時刻は BoundImportTable に書いてある。
					if(time==-1) return System.DateTime.MaxValue;

					long ltime=time;
					return mwg.Crt.Time.localtime(&ltime)->ToLocalTime();
				}
			}


			public ImportFunction this[int index]{
				get{
					if(index<0||funccount<=index)
						throw new System.ArgumentOutOfRangeException("index");

					const int OffsetName=2; // IMAGE_IMPORT_BY_NAME.Name メンバのオフセット
					RemotePtr<FPtr> ppfn;
					string name;
					if(!pIAT32.IsNull){
						// 対象が PE32 の場合
						ppfn=(pIAT32+index).Reinterpret<FPtr>();
						// ■↑■ 64bit から 32bit の中の FPtr を触ると×

						IMAGE.THUNK_DATA32 int32=pINT32[index];
						if(int32.IsSnapByOrdinal){
							return new ImportFunction(int32.OrdinalValue,ppfn);
						}else{
							name=(module.mbase+int32.AddressOfData+OffsetName).ReadAnsiString();
						}
					}else{
						// 対象が PE32+ の場合
						ppfn=(pIAT64+index).Reinterpret<FPtr>();
						IMAGE.THUNK_DATA64 int64=pINT64[index];
						if(int64.IsSnapByOrdinal){
							return new ImportFunction(int64.OrdinalValue,ppfn);
						}else{
							name=(module.mbase+int64.AddressOfData+OffsetName).ReadAnsiString();
						}
					}

					// 名前によるインポートの場合
					if(name==null)
						name="<FAILED TO GET NAME>";
					else if(name.Length>1&&name[0]=='?')
						name=DbgHelp.UnDecorateSymbolName(name,DbgHelp.UNDNAME.COMPLETE);
					return new ImportFunction(name,ppfn);
				}
			}
			/// <summary>
			/// 登録されている import 関数の数を取得します。
			/// </summary>
			public int Count{
				get{return funccount;}
			}

			public Gen::IEnumerator<ImportFunction> GetEnumerator(){
				for(int i=0,iM=this.Count;i<iM;i++)yield return this[i];
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
				return this.GetEnumerator();
			}
		}

		/// <summary>
		/// IAT 内の、個々のインポート関数を表現します。
		/// </summary>
		public class ImportFunction{
			readonly RemotePtr<FPtr> ppfn;
			readonly string name;
			readonly ushort ordinal;

			public ImportFunction(string name,RemotePtr<FPtr> ppfn){
				ordinal=0xffff;
				this.name=name;
				this.ppfn=ppfn;
			}
			public ImportFunction(ushort ordinal,RemotePtr<FPtr> ppfn){
				this.ordinal=ordinal;
				this.name=null;
				this.ppfn=ppfn;
			}
			/// <summary>
			/// import 関数の序数を取得します。
			/// </summary>
			public ushort Ordinal{
				get{return ordinal;}
			}
			/// <summary>
			/// 序数によって import するかどうかを取得します。
			/// </summary>
			public bool IsSnapByOrdinal{
				get{return name==null;}
			}
			/// <summary>
			/// Import 関数の名前を取得します。
			/// </summary>
			public string Name{
				get{return name??("#"+ordinal.ToString());}
			}
			/// <summary>
			/// 関数へのポインタを取得します。
			/// </summary>
			public RemotePtr<FPtr> pFptr{
				get{return ppfn;}
			}
		}
	}
	#endregion
}
