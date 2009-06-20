using afh.Application;
using Diag=System.Diagnostics;
using mwg.Win32;
using mwg.InterProcess;
using Forms=System.Windows.Forms;
using Interop=System.Runtime.InteropServices;

using IMAGE=mwg.Win32.IMAGE;

//#define RemoteBytePtr

#if DEBUG
namespace UnitTest{
internal static class UnitTest{
	public static void obsMulticastDelegate(Log log){
		afh.VoidCB v=null;
		ASSERT(log,"null",v,null);
		v+=sandbox;
		ASSERT(log,"<1>",v.GetInvocationList().Length,1);
		v-=sandbox;
		ASSERT(log,"<0>null",v,null);
		v+=sandbox;
		ASSERT(log,"<1>",v.GetInvocationList().Length,1);
		v+=sandbox;
		ASSERT(log,"<2>",v.GetInvocationList().Length,2);
		v-=sandbox;
		ASSERT(log,"<1>",v.GetInvocationList().Length,1);
		v-=sandbox;
		ASSERT(log,"<0>null",v,null);
		v-=sandbox;
		ASSERT(log,"<-1>null",v,null);
	}
	private static void sandbox(){
	}

	public static unsafe void check_structures(Log log){
		ASSERT(log,"sizeof(IMAGE_DOS_HEADER)",sizeof(IMAGE.DOS_HEADER),64);
		ASSERT(log,"sizeof(IMAGE_FILE_HEADER)",sizeof(IMAGE.FILE_HEADER),20);
		ASSERT(log,"sizeof(IMAGE_DATA_DIRECTORY_Array)",sizeof(IMAGE.DATA_DIRECTORY_Array),8*16);
		ASSERT(log,"sizeof(IMAGE_OPTIONAL_HEADER)",sizeof(IMAGE.NT32_OPTIONAL_HEADER),224);
		ASSERT(log,"sizeof(IMAGE_OPTIONAL_HEADER64)",sizeof(IMAGE.NT64_OPTIONAL_HEADER),240);
		ASSERT(log,"sizeof(IMAGE_ROM_OPTIONAL_HEADER)",sizeof(IMAGE.ROM_OPTIONAL_HEADER),56);
		ASSERT(log,"sizeof(mwg.Win32.FPtr)",sizeof(mwg.Win32.FPtr),System.IntPtr.Size);
	}

	internal static void ASSERT(Log log,string name,object real,object ideal) {
		if(real==null?ideal==null:real.Equals(ideal)) {
			log.WriteLine("ASSERT {0}: {1}",name,"OK");
		} else {
			log.WriteLine("ASSERT {0}: {1}",name,"ERROR");
			log.WriteLine("\tReal : {0}",real);
			log.WriteLine("\tIdeal: {0}",ideal);
		}
	}

	// ポインタ
	// ・Box 化不能
	// ・型パラメータに指定出来ない (ポインタ配列は出来るのに…)
	// ・匿名メソッドの引数に出来ない

	/* そもそもポインタは Box 化出来ない… (System.IntPtr に変換して自動的にやってくれても良いのに)
	[afh.Tester.TestMethod]
	public unsafe static void testPointerBoxing(Log log){
		byte* alpha=(byte*)0x400000;
		object o=alpha;
		log.WriteLine("{0}",o is void*);
	}
	//*/

	public unsafe static void chkEnumModules(Log log){
		// Notepad.exe を取得
		Diag::Process[] procs=Diag::Process.GetProcessesByName("notepad");
		if(procs.Length==0){
			log.WriteLine("現在 notepad.exe は動いていません。");
			return;
		}
		Diag::Process proc=procs[0];
		for(int i=1;i<procs.Length;i++)procs[i].Close();
		log.WriteLine("プロセス notepad.exe を捕まえました");
		log.WriteVar("Id",proc.Id.ToString("X8"));
		log.WriteVar("Handle",proc.Handle.ToString("X8"));

		// Modules
		log.WriteLine("Diag::Process.Modules より");
		log.Lock();
		log.AddIndent();
		try{
			foreach(Diag::ProcessModule mod in proc.Modules){
				log.WriteVar("Module-Name",mod.ModuleName);
				log.WriteVar("Base-Address",mod.BaseAddress.ToString("X8"));
				log.WriteVar("Module-Size",mod.ModuleMemorySize.ToString("X8"));
			}
		}finally{
			log.RemoveIndent();
			log.Unlock();
		}

		// Modules2
		log.WriteLine("<DbgHelp>::EnumerateLoadedModules より");
		log.Lock();
		log.AddIndent();
		try{
#pragma warning disable 618
			DbgHelp.EnumerateLoadedModules(proc.Handle,delegate(string name,System.IntPtr modbase,uint modsize,System.IntPtr userData){
				log.WriteVar("Module-Name",name);
				log.WriteVar("Base-Address",modbase.ToString("X8"));
				log.WriteVar("Module-Size",modsize.ToString("X8"));
				return true;
			},System.IntPtr.Zero);
#pragma warning restore 618
		}finally{
			log.RemoveIndent();
			log.Unlock();
		}
	}

	private static Diag::ProcessModule GetNotepadModule(Log log,out ProcessMemory m){
		const string TARGET="notepad.exe";

		m=new mwg.InterProcess.ProcessMemory(TARGET);
		if(!m.Available){
			log.WriteLine("!現在 notepad.exe が利用出来ません。起動しているかどうか確認して下さい。");
			return null;
		}

		Diag::ProcessModule mod=null;
		foreach(Diag::ProcessModule mod2 in m.Process.Modules){
			//log.Lock();
			//try{dumpModuleHeader(log,m,mod2);}
			//finally{log.Unlock();}

			if(mod2.ModuleName.ToString()!=TARGET)continue;
			mod=mod2;
			break;
		}

		if(mod==null){
			log.WriteLine("!プロセス notepad.exe 内にモジュール 'notepad.exe' が見つかりませんでした。");
			return null;
		}

		return mod;
	}

	public static void chkProcessMemory(Log log){
		ProcessMemory m;
		Diag::ProcessModule mod=GetNotepadModule(log,out m);
		if(mod==null)return;

		log.Lock();
		try{
			dumpModuleHeader2(log,m,mod);
		}finally{
			log.Unlock();
		}
	}
	public static void chkProcessMemory2(Log log){
		ProcessMemory mem;
		Diag::ProcessModule mod=GetNotepadModule(log,out mem);
		if(mod==null)return;

		Module module=new Module(mem,mod);

		Forms::Form f=new System.Windows.Forms.Form();
		Forms::PropertyGrid grid=new System.Windows.Forms.PropertyGrid();
		grid.Dock=Forms::DockStyle.Fill;
		grid.SelectedObject=module;
		f.Controls.Add(grid);
		f.ShowDialog();
		f.Dispose();
	}
	public static void chkProcessMemory3(Log log){
		const string TARGET="notepad.exe";
		ProcessMemory mem=new mwg.InterProcess.ProcessMemory(TARGET);
		if(!mem.Available){
			log.WriteLine("notepad.exe なるプロセスは起動していません。");
			return;
		}

		Forms::Form f=new System.Windows.Forms.Form();
		f.Size=new System.Drawing.Size(700,500);
		ProcessView view=new ProcessView();
		view.Dock=Forms::DockStyle.Fill;
		view.SetProcess(mem);
		f.Controls.Add(view);
		f.ShowDialog();
		f.Dispose();
	}

	private static void dumpModuleHeader2(Log log,ProcessMemory m,Diag::ProcessModule mod){
		Module module=new Module(m,mod);

		ImageImportDirectory import=null;
		foreach(ImageDataDirectory dir in module.Directories){
			if(dir.pData.IsNull)continue;

			log.WriteLine("DirectoryEntry: "+afh.Enum.GetDescription(dir.DirectoryType));
			log.AddIndent();
			log.WriteVar("RVA of Data","0x"+dir.pData.Address.ToString("X8"));
			log.WriteVar("Size of Data","0x"+dir.DataSize.ToString("X8"));
			log.RemoveIndent();
			if(dir is ImageImportDirectory)
				import=(ImageImportDirectory)dir;
		}
		if(import==null)return;
		log.WriteLine("============================================================");
		log.WriteLine("                     IMPORT TABLE                           ");
		log.WriteLine("============================================================");
		foreach(ImageImportDirectory.ImportModule imod in import){
			log.WriteVar("Importing from",imod.Name);
			log.WriteVar("ForwarderChain",imod.ForwarderChain);
			log.WriteVar("TimeDateStamp",imod.TimeDateStamp);
			foreach(ImageImportDirectory.ImportFunction ifunc in imod)
				log.WriteLine(
					"dllimport {0} \t@ 0x{1:X8}",
					ifunc.Name,
					(uint)(System.IntPtr)ifunc.pFptr[0]
					);
			log.WriteLine("------------------------------------------------------------");
		}
	}

	#region dumpModuleHeader1
	private unsafe static void dumpModuleHeader1(Log log,ProcessMemory m,Diag::ProcessModule mod){
		log.WriteLine("モジュール "+mod.ModuleName+":");
		log.AddIndent();
		log.WriteVar("Base-Address","0x"+mod.BaseAddress.ToString("X8"));
		log.WriteVar("Module-Size","0x"+mod.ModuleMemorySize.ToString("X8"));
		try{
			RemotePtr<byte> mbase=(RemotePtr<byte>)m+mod.BaseAddress;
			IMAGE.DOS_HEADER dosHeader
				=mbase.Read<IMAGE.DOS_HEADER>();
			if(dosHeader.magic!=IMAGE.SIGNATURE.DOS){
				log.WriteLine("!モジュールの先頭が DOS Header ではありません。");
				return;
			}

			RemotePtr<byte> pe=mbase+dosHeader.lfanew;
			if(pe.Read<uint>()!=(uint)IMAGE.SIGNATURE.NT){
				log.WriteLine("!IMAGE Header が見つかりません。");
				return;
			}
			IMAGE.FILE_HEADER coffHeader=(pe+=4).Read<IMAGE.FILE_HEADER>();
			log.WriteLine("IMAGE 形式: COFF Header");
			log.AddIndent();
			log.WriteVar("対象機種",coffHeader.MachineDescription);
			log.WriteVar("セクション数",coffHeader.NumberOfSections);
			log.WriteVar("タイムスタンプ",coffHeader.TimeDateStamp);
			log.WriteVar("シンボル表の位置","0x"+coffHeader.PointerToSymbolTable.ToString("X8"));
			log.WriteVar("シンボルの数",coffHeader.NumberOfSymbols);
			log.WriteVar("拡張ヘッダの大きさ","0x"+coffHeader.SizeOfOptionalHeader.ToString("X4"));
			log.WriteVar("属性",coffHeader.Characteristics);
			log.RemoveIndent();

			switch((pe+=sizeof(IMAGE.FILE_HEADER)).Read<IMAGE.OPTIONAL_MAGIC>()) {
				case IMAGE.OPTIONAL_MAGIC.NT_HDR32:
					log.WriteLine("種別: PE32");
					dumpPE32Header(log,mbase,pe);
					break;
				case IMAGE.OPTIONAL_MAGIC.ROM_HDR:
					log.WriteLine("種別: Rom Image");
					break;
				case IMAGE.OPTIONAL_MAGIC.NT_HDR64:
					log.WriteLine("種別: PE32+");
					break;
				default:
					log.WriteLine("未知の拡張ヘッダです。");
					break;
			}
		}finally{
			log.RemoveIndent();
		}
	}

	private unsafe static void dumpPE32Header(Log log,RemotePtr<byte> mbase,RemotePtr<byte> ohead){
		IMAGE.NT32_OPTIONAL_HEADER oHeader=ohead.Read<IMAGE.NT32_OPTIONAL_HEADER>();
		log.AddIndent();
		log.WriteVar("LinkerVersion",oHeader.STD.LinkerVersion);
		log.WriteVar("Size of Code",oHeader.STD.SizeOfCode);
		log.WriteVar("Size of Initialized Data",oHeader.STD.SizeOfInitializedData);
		log.WriteVar("Size of Uninitialized Data",oHeader.STD.SizeOfUninitializedData);
		log.WriteVar("Address of EntryPoint","0x"+oHeader.STD.AddressOfEntryPoint.ToString("X8"));
		log.WriteVar("Base of Code","0x"+oHeader.STD.BaseOfCode.ToString("X8"));

		log.WriteVar("Base of Data","0x"+oHeader.BaseOfData.ToString("X8"));
		log.WriteVar("Preferred Base","0x"+oHeader.ImageBase.ToString("X8"));
		log.WriteVar("Section Alignment","0x"+oHeader.SectionAlignment.ToString("X8"));
		log.WriteVar("File Alignment","0x"+oHeader.FileAlignment.ToString("X8"));
		log.WriteVar("OS Version",oHeader.OSVersion);
		log.WriteVar("Image Version",oHeader.ImageVersion);
		log.WriteVar("Subsystem Version",oHeader.SubsystemVersion);
		log.WriteVar("Win32 Version",oHeader.Win32VersionValue.ToString());
		log.WriteVar("Size of Image","0x"+oHeader.SizeOfImage.ToString("X8"));
		log.WriteVar("Size of Headers","0x"+oHeader.SizeOfHeaders.ToString("X8"));
		log.WriteVar("CheckSum","0x"+oHeader.CheckSum.ToString("X8"));
		log.WriteVar("Subsystem",oHeader.Subsystem);
		log.WriteVar("Dll 属性",oHeader.DllCharacteristics);
		log.WriteVar("Size of Stack Reserve","0x"+oHeader.SizeOfStackReserve.ToString("X8"));
		log.WriteVar("Size of Stack Commit","0x"+oHeader.SizeOfStackCommit.ToString("X8"));
		log.WriteVar("Size of Heap Reserve","0x"+oHeader.SizeOfHeapReserve.ToString("X8"));
		log.WriteVar("Size of Heap Commit","0x"+oHeader.SizeOfHeapCommit.ToString("X8"));

		log.WriteVar("Number of RVA and Sizes",oHeader.NumberOfRvaAndSizes);
		log.RemoveIndent();

		for(int i=0;i<oHeader.NumberOfRvaAndSizes;i++){
			IMAGE.DIRECTORY_ENTRY dindex=(IMAGE.DIRECTORY_ENTRY)i;
			IMAGE.DATA_DIRECTORY dir=oHeader.DataDirectory[dindex];
			if(dir.Size==0&&dir.VirtualAddress==0)continue;

			log.WriteLine("DirectoryEntry: "+afh.Enum.GetDescription(dindex));
			log.AddIndent();
			log.WriteVar("RVA of Data","0x"+dir.VirtualAddress.ToString("X8"));
			log.WriteVar("Size of Data","0x"+dir.Size.ToString("X8"));
			log.RemoveIndent();
		}

		if((int)IMAGE.DIRECTORY_ENTRY.IMPORT<oHeader.NumberOfRvaAndSizes){
			dumpImportTable(log,mbase,oHeader.DataDirectory.importTable);
		}
	}

	private unsafe static void dumpImportTable(Log log,RemotePtr<byte> mbase,IMAGE.DATA_DIRECTORY dir){
		if(dir.VirtualAddress==0)return;
		log.WriteLine("============================================================");
		log.WriteLine("                     IMPORT TABLE                           ");
		log.WriteLine("============================================================");
		RemotePtr<IMAGE.IMPORT_DESCRIPTOR> pDesc
			=(mbase+dir.VirtualAddress).Reinterpret<IMAGE.IMPORT_DESCRIPTOR>();
		RemotePtr<IMAGE.IMPORT_DESCRIPTOR> pDescM
			=pDesc.Advance((System.IntPtr)dir.Size);
		while(pDesc<pDescM){
			IMAGE.IMPORT_DESCRIPTOR desc=(pDesc++)[0];
			log.WriteVar("Importing from",(mbase+(int)desc.pstrName).ReadAnsiString());
			log.WriteVar("ForwarderChain",desc.ForwarderChain);
			log.WriteVar("TimeDateStamp",desc.TimeDateStamp);
			log.WriteVar("FirstThunk","0x"+desc.FirstThunk.ToString("X8"));
			log.WriteVar("OriginalFirstThunk","0x"+desc.OriginalFirstThunk.ToString("X8"));
			if(desc.FirstThunk==0)continue;

			RemotePtr<IMAGE.THUNK_DATA32> pIAT=(mbase+(int)desc.FirstThunk).Reinterpret<IMAGE.THUNK_DATA32>();
			RemotePtr<IMAGE.THUNK_DATA32> pINT=(mbase+(int)desc.OriginalFirstThunk).Reinterpret<IMAGE.THUNK_DATA32>();
			while(true){
				IMAGE.THUNK_DATA32 iat_item=pIAT++.Value;
				IMAGE.THUNK_DATA32 int_item=pINT++.Value;
				if(iat_item.Function==0)break;

				string name;
				if(int_item.IsSnapByOrdinal){
					name="#"+int_item.OrdinalValue.ToString();
				}else{
					const int OffsetName=2; // IMAGE_IMPORT_BY_NAME.Name メンバのオフセット
					name=(mbase+int_item.AddressOfData+OffsetName).ReadAnsiString();
					if(name[0]=='?')
						name=DbgHelp.UnDecorateSymbolName(name,DbgHelp.UNDNAME.COMPLETE);
				}
				log.WriteLine("dllimport {0} \t@ 0x{1:X8}",name,iat_item.Function);
			}
			log.WriteLine("------------------------------------------------------------");
		}
	}
	#endregion
}

[afh.Tester.TestTarget]
internal static class GenericRemotePtr{
#if RemoteBytePtr
	static RemoteBytePtr mNotepad1=null;
#endif
	static RemotePtr<byte> mNotepad2=default(RemotePtr<byte>);
	static GenericRemotePtr() {
		ProcessMemory m=new mwg.InterProcess.ProcessMemory("notepad");
		if(!m.Available)return;

		Diag::ProcessModule mod=null;
		foreach(Diag::ProcessModule mod2 in m.Process.Modules) {
			if(mod2.ModuleName.ToString()=="nodepad.exe") continue;
			mod=mod2;
			break;
		}
		if(mod==null)return;

#if RemoteBytePtr
		mNotepad1=(RemoteBytePtr)m+mod.BaseAddress;
#endif
		mNotepad2=(RemotePtr<byte>)m+(long)mod.BaseAddress;
	}
#if RemoteBytePtr
	public static void viewNotepadImage1(Log log){
		RemoteBytePtr mNotepad=mNotepad1;
		if(mNotepad==null){
			log.WriteLine("!メモ帳が起動していません。");
			return;
		}

		log.Lock();
		for(int i=0;i<0x100;i++){
			log.AppendText(mNotepad[i].ToString("X2"));
			if((i&0xf)==0xf)log.WriteLine();else log.AppendText(" ");
		}
		log.WriteLine();
		log.Unlock();
	}
#endif

	public static void viewNotepadImage2(Log log){
		RemotePtr<byte> mNotepad=mNotepad2;
		if(mNotepad.ProcessMemory==null){
			log.WriteLine("!メモ帳が起動していません。");
			return;
		}

		log.Lock();
		for(int i=0;i<0x100;i++){
			log.AppendText(mNotepad[i].ToString("X2"));
			if((i&0xf)==0xf)log.WriteLine();else log.AppendText(" ");
		}
		log.WriteLine();
		log.Unlock();
	}

	public unsafe static void obsOpIncrement(Log log){
		RemotePtr<byte> b=new RemotePtr<byte>();
		UnitTest.ASSERT(log,"b",(int)b.Address,0);
		RemotePtr<byte> c=b++;
		UnitTest.ASSERT(log,"b++",(int)c.Address,0);
		UnitTest.ASSERT(log,"b",(int)b.Address,1);
		RemotePtr<byte> d=++b;
		UnitTest.ASSERT(log,"++b",(int)d.Address,2);
		UnitTest.ASSERT(log,"b",(int)b.Address,2);
	}
	//*/

#if RemoteBytePtr
	[afh.Tester.BenchMethod]
	public static void benchRemotePtr1(){
		int ret=0;
		for(int i=0;i<0x100;i++)ret+=mNotepad1[i];
	}
#endif

	[afh.Tester.BenchMethod]
	public static void benchRemotePtr2() {
		int ret=0;
		for(int i=0;i<0x100;i++)ret+=mNotepad2[i];
	}

	// 結果: 何故か 2-3% 程しか差が出ない。使い易さから RemotePtr<> の方がよい。

}
}
#endif
