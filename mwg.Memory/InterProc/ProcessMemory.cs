using System.Diagnostics;
using Interop=System.Runtime.InteropServices;
using Gen=System.Collections.Generic;
using Kernel32=mwg.Win32.Kernel32;
namespace mwg.Win32{
	public static partial class Kernel32{
		[Interop::DllImport("kernel32")]
		public static extern int SuspendThread(System.IntPtr threadHandle);
		[Interop::DllImport("kernel32")]
		public static extern int ResumeThread(System.IntPtr threadHandle);
		//============================================================
		//		OpenProcess
		//============================================================
		[Interop::DllImport("kernel32")]
		public static extern System.IntPtr OpenProcess(
			PROCESS access,
			[Interop::MarshalAs(Interop::UnmanagedType.Bool)]bool inherit,
			int procId
		);
		[System.Flags]
		public enum PROCESS:int{
			// from winnt.h
			_STANDARD_RIGHTS_REQUIRED =0x000F0000,
			_SYNCHRONIZE              =0x00100000,
			TERMINATE         =0x0001,
			CREATE_THREAD     =0x0002,
			SET_SESSIONID     =0x0004,
			VM_OPERATION      =0x0008,
			VM_READ           =0x0010,
			VM_WRITE          =0x0020,
			DUP_HANDLE        =0x0040,
			CREATE_PROCESS    =0x0080,
			SET_QUOTA         =0x0100,
			SET_INFORMATION   =0x0200,
			QUERY_INFORMATION =0x0400,
			SUSPEND_RESUME    =0x0800,
			ALL_ACCESS        =_STANDARD_RIGHTS_REQUIRED|_SYNCHRONIZE|0xFFF
		}
		//============================================================
		//		Read/WriteProcessMemory
		//============================================================
		[Interop::DllImport("kernel32",CharSet=Interop::CharSet.Ansi,SetLastError=true)]
		public static unsafe extern int WriteProcessMemory(
			System.IntPtr hProcess,void* lpBaseAddress,void* lpBuffer,int nSize,int* lpNumberOfBytesWritten);

		[Interop::DllImport("kernel32",CharSet=Interop::CharSet.Ansi,SetLastError=true)]
		public static unsafe extern int ReadProcessMemory(
			System.IntPtr hProcess,void* lpBaseAddress,void* lpBuffer,int nSize,int* lpNumberOfBytesWritten);
		//============================================================
		//		OpenThread
		//============================================================
		[Interop::DllImport("kernel32")]
		public static extern System.IntPtr OpenThread(
			THREAD desiredAccess,
			[Interop::MarshalAs(Interop::UnmanagedType.Bool)]bool inheritHandle,
			int threadId
			);
		[System.Flags]
		public enum THREAD:int{
			// from winnt.h
			STANDARD_RIGHTS_REQUIRED		=0x000F0000,
			SYNCHRONIZE						=0x00100000,

			TERMINATE               =0x0001,
			SUSPEND_RESUME          =0x0002,
			GET_CONTEXT             =0x0008,
			SET_CONTEXT             =0x0010,
			SET_INFORMATION         =0x0020,
			QUERY_INFORMATION       =0x0040,
			SET_THREAD_TOKEN        =0x0080,
			IMPERSONATE             =0x0100,
			DIRECT_IMPERSONATION    =0x0200,
			// begin_ntddk begin_wdm begin_ntifs

			ALL_ACCESS         =STANDARD_RIGHTS_REQUIRED|SYNCHRONIZE|0x3FF,
		}
		//============================================================
		//		CloaseHandle
		//============================================================
		[Interop::DllImport("kernel32")]
		[return: Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public static extern bool CloseHandle(
			System.IntPtr handle
		);
	}
}

namespace mwg.InterProcess{

	public partial class ProcessMemory:System.IDisposable{
		private readonly System.IDisposable resource=null;
		private Process process=null;
		private System.IntPtr handle=System.IntPtr.Zero;

		/// <summary>
		/// �w�肵���v���Z�X�ɑ΂��� ProcessMemory ���擾���܂��B
		/// </summary>
		/// <param name="procName">�v���Z�X�̖��O���w�肵�܂��B
		/// �w�肵���v���Z�X���������������ꍇ�ɂ́A
		/// �ŏ��Ɍ��������v���Z�X�ɑ΂��ăC���X�^���X�𐶐����܂��B
		/// �C���X�^���X��������Ȃ������ꍇ�ɂ� Available �v���p�e�B�� false �ɂȂ�܂��B
		/// </param>
		public ProcessMemory(string procName){
			this.resource=new InitializerFromName(this,procName);
		}
		public ProcessMemory(Process process){
			this.resource=new InitializerFromProcess(this,process);
		}

		public void Dispose(){
			if(this.locked_threads!=null)
				this.RestartProcess();
			if(this.resource!=null)
				this.resource.Dispose();
			System.GC.SuppressFinalize(this);
		}
		~ProcessMemory(){
			this.Dispose();
		}

		/// <summary>
		/// �v���Z�X�����w�肵�� ProcessMemory ���쐬�����ꍇ�A
		/// �V�����v���Z�X���擾�������܂��B
		/// </summary>
		public void ReInitialize(){
			InitializerFromName fromName=this.resource as InitializerFromName;
			if(fromName!=null)fromName.ReInitialize();
		}

		#region ������/�㏈��
		private class InitializerFromName:System.IDisposable{
			private readonly ProcessMemory mem;
			private readonly string procName;
			private System.IDisposable res=null;
			private Process process=null;

			public InitializerFromName(ProcessMemory mem,string procName){
				this.mem=mem;
				this.procName=procName;
				this.Initialize();

				// .exe ��]���ɕt���Ďw�肵����
				if(this.process==null&&procName.ToLower().EndsWith(".exe")){
					this.procName=procName.Substring(0,procName.Length-4);
					this.Initialize();
				}
			}

			public void ReInitialize(){
				this.Dispose();
				this.Initialize();
			}

			private void Initialize(){
				Process[] procs=Process.GetProcessesByName(procName);
				if(procs.Length==0)return;

				this.res=new InitializerFromProcess(mem,this.process=procs[0]);
				for(int i=1;i<procs.Length;i++)procs[i].Close();
			}

			public void Dispose(){
				if(this.process!=null)
					this.process.Close();
				if(this.res!=null)
					this.res.Dispose();
			}
		}

		private class InitializerFromProcess:System.IDisposable{
			private System.IntPtr handle=System.IntPtr.Zero;

			public InitializerFromProcess(ProcessMemory mem,Process proc){
				mem.process=proc;
				handle=Kernel32.OpenProcess(
					Kernel32.PROCESS.VM_OPERATION|Kernel32.PROCESS.VM_READ|Kernel32.PROCESS.VM_WRITE,
					false,proc.Id);
				mem.handle=handle;
			}

			public void Dispose(){
				if(this.handle!=System.IntPtr.Zero){
					Kernel32.CloseHandle(this.handle);
					this.handle=System.IntPtr.Zero;
				}
			}

		}
		#endregion

		public unsafe RemotePtr<byte> GetPtr(void* address){
			return new RemotePtr<byte>(this,(byte*)address);
		}
		public unsafe RemotePtr<byte> GetPtr(System.IntPtr address){
			return this.GetPtr((void*)address);
		}
		public unsafe RemotePtr<T> GetPtr<T>(void* address) where T:struct{
			return new RemotePtr<T>(this,(byte*)address);
		}
		public unsafe RemotePtr<T> GetPtr<T>(System.IntPtr address) where T:struct{
			return this.GetPtr<T>((void*)address);
		}

		/// <summary>
		/// ���݂��̃v���Z�X�����݂��āA�A�N�Z�X�\���ǂ������擾���܂��B
		/// </summary>
		public bool Available{
			get{return this.handle!=System.IntPtr.Zero&&!this.process.HasExited;}
		}
		public Process Process{
			get{return this.process;}
		}
		//============================================================
		//		�������ǂݏ���
		//============================================================
		public unsafe int WriteMemory(uint address,params byte[] data){
			return WriteMemory((void*)address,data);
		}
		public unsafe int WriteMemory(uint address,void* buffer,int size) {
			return WriteMemory((void*)address,buffer,size);
		}
		public unsafe int WriteMemoryHex(uint address,string hexstr){
			return WriteMemoryHex((void*)address,hexstr);
		}
		public unsafe int WriteMemory(void* address,params byte[] data){
			fixed(byte* pData=data)
				return WriteMemory(address,pData,data.Length);
		}
		public unsafe int WriteMemory(void* address,void* buffer,int size){
			if(!this.Available)return 0;

			int ret;
			Kernel32.WriteProcessMemory(this.handle,address,buffer,size,&ret);
			return ret;
		}
		public unsafe int WriteMemoryHex(void* address,string hexstr){
			if((hexstr.Length&1)==1)
				throw new System.ArgumentException("str �����̒����� 2 �̔{���łȂ���Έׂ�܂���B","hexstr");

			int len=hexstr.Length/2;
			byte* buffB=(byte*)Interop::Marshal.AllocHGlobal(len);
			try{
				byte* buff=buffB;
				byte* buffM=buffB+len;
				fixed(char* chB=hexstr){
					char* ch=chB;
					while(buff<buffM)
						*buff++=(byte)(char2nibble(*ch++)<<4|char2nibble(*ch++));
				}

				return WriteMemory(address,buffB,len);
			}finally{
				Interop::Marshal.FreeHGlobal((System.IntPtr)buffB);
			}
		}
		private int char2nibble(char ch){
			if('0'<=ch&&ch<='9')return ch-'0';
			if('A'<=ch&&ch<='F')return ch-'A'+10;
			if('a'<=ch&&ch<='f')return ch-'a'+10;
			throw new System.ArgumentException("�w�肳�ꂽ�����͏\�Z�i�̐����ł͂���܂���B","ch");
		}

		public unsafe int ReadMemory(void* address,int size,out byte[] buffer){
			buffer=new byte[size];
			if(this.handle==System.IntPtr.Zero)return 0;

			fixed(byte* pBuff=buffer)
				return ReadMemory(address,pBuff,size);
		}
		public unsafe int ReadMemory(void* address,void* buffer,int size){
			if(!this.Available)return 0;

			int ret;
			Kernel32.ReadProcessMemory(this.handle,address,buffer,size,&ret);
			return ret;
		}
		public unsafe int ReadMemory(uint address,int size,out byte[] buffer){
			return ReadMemory((void*)address,size,out buffer);
		}
		public unsafe int ReadMemory(uint address,void* buffer,int size){
			return ReadMemory((void*)address,buffer,size);
		}
		//============================================================
		//		�X���b�h�̒�~
		//============================================================
		private Gen::List<Thread> locked_threads=null;
		public void StopProcess(){
			if(locked_threads!=null)return;

			locked_threads=new System.Collections.Generic.List<Thread>();
			foreach(ProcessThread th in this.process.Threads){
				Thread thread=new Thread(th.Id);
				thread.Suspend();
				locked_threads.Add(thread);
			}
		}
		public void RestartProcess(){
			if(locked_threads==null)return;

			foreach(Thread thread in this.locked_threads){
				thread.Resume();
				thread.Dispose();
			}
			locked_threads=null;
		}
	}

	public class Thread{
		private System.IntPtr handle;
		public Thread(int id){
			this.handle=Kernel32.OpenThread(Kernel32.THREAD.SUSPEND_RESUME,false,id);
		}

		private int suspend=0;
		public int Suspend(){
			this.suspend++;
			return Kernel32.SuspendThread(this.handle);
		}
		public int Resume(){
			if(this.suspend<=0)return 0;
			this.suspend--;
			return Kernel32.ResumeThread(this.handle);
		}
		public void ClearSuspend(){
			while(this.suspend>0)this.Resume();
		}

		public int ForceSuspend(){
			return Kernel32.SuspendThread(this.handle);
		}
		public int ForceResume(){
			return Kernel32.ResumeThread(this.handle);
		}

		public void Dispose(){
			this.ClearSuspend();
			if(this.handle!=System.IntPtr.Zero)
				Kernel32.CloseHandle(this.handle);
			this.handle=System.IntPtr.Zero;
			System.GC.SuppressFinalize(this);
		}
		~Thread(){
			this.Dispose();
		}
	}
}