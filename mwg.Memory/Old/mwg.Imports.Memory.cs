using Interop=System.Runtime.InteropServices;
using Diag=System.Diagnostics;

namespace mwg.Imports{
	public static unsafe class Memory{
		static Memory(){
			if(System.IntPtr.Size!=4)
				throw new System.Exception("64 bit 環境じゃ多分動作しない。x86 でコンパイルせよ");
		}

		/// <summary>
		/// 指定したプロセスの指定した場所から 32bit 値を読み取ります。
		/// </summary>
		/// <param name="procName">プロセス名を指定します。</param>
		/// <param name="addr">指定したプロセスのメモリ空間に於ける、読み込み元のアドレスを指定します。</param>
		/// <param name="value">読み取った値を返します。</param>
		/// <returns>読み取りが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Read(string procName,System.IntPtr addr,out int value){
			return ReadMemory(procName,addr,out value,4);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所から 64bit 値を読み取ります。
		/// </summary>
		/// <param name="procName">プロセス名を指定します。</param>
		/// <param name="addr">指定したプロセスのメモリ空間に於ける、読み込み元のアドレスを指定します。</param>
		/// <param name="value">読み取った値を返します。</param>
		/// <returns>読み取りが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Read(string procName,System.IntPtr addr,out long value){
			return ReadMemory64(procName,addr,out value,8);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所から 32bit 値を読み取ります。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">指定したプロセスのメモリ空間に於ける、読み込み元のアドレスを指定します。</param>
		/// <param name="value">読み取った値を返します。</param>
		/// <returns>読み取りが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Read(Diag::Process proc,System.IntPtr addr,out int value){
			return DReadMemory(proc.Handle,addr,out value,4);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所から 64bit 値を読み取ります。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">指定したプロセスのメモリ空間に於ける、読み込み元のアドレスを指定します。</param>
		/// <param name="value">読み取った値を返します。</param>
		/// <returns>読み取りが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Read(Diag::Process proc,System.IntPtr addr,out long value){
			return DReadMemory64(proc.Handle,addr,out value,8);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所から、指定した長さのバイナリデータを読み取ります。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">指定したプロセスのメモリ空間に於ける、読み込み元のアドレスを指定します。</param>
		/// <param name="data">読み取った値を返します。</param>
		/// <param name="size">読み取るデータの大きさをバイト単位で指定します。</param>
		/// <returns>読み取りが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Read(Diag::Process proc,System.IntPtr addr,out byte[] data,int size){
			data=new byte[size];
			fixed(byte* pData=data)
				return DReadMemoryArray(proc.Handle,addr,(void*)pData,size);
		}

		#region __declspec(dllimport) ReadMemory
		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool ReadMemory(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName,
			System.IntPtr addr, /* C 用のヘッダを見ると DWORD (32 bit) */
			out int value,
			int size /* 1-4 の値を指定するそうだが、1-3 を指定した場合の動作が不明 */
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool ReadMemory64(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName,
			System.IntPtr addr,
			out long value,
			int size /* 1-8 */
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DReadMemory(
			System.IntPtr hProc,
			System.IntPtr addr,
			out int value,
			int size /* 1-4 */
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DReadMemory64(
			System.IntPtr hProc,
			System.IntPtr addr,
			out long value,
			int size /* 1-8 */
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DReadMemoryArray(
			System.IntPtr hProc,
			System.IntPtr addr,
			void* pData,
			int size
		);
		#endregion

		/// <summary>
		/// 指定したプロセスの指定した場所に 32bit 値を書き込みます。
		/// </summary>
		/// <param name="procName">プロセス名を指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="value">書き込む値を指定します。</param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Write(string procName,System.IntPtr addr,int value){
			return WriteMemory(procName,addr,value,4);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所に 64bit 値を書き込みます。
		/// </summary>
		/// <param name="procName">プロセス名を指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="value">書き込む値を指定します。</param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Write(string procName,System.IntPtr addr,long value){
			return WriteMemory64(procName,addr,value,8);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所に 32bit 値を書き込みます。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="value">書き込む値を指定します。</param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Write(Diag::Process proc,System.IntPtr addr,int value){
			return DWriteMemory(proc.Handle,addr,value,4);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所に 64bit 値を書き込みます。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="value">書き込む値を指定します。</param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Write(Diag::Process proc,System.IntPtr addr,long value){
			return DWriteMemory64(proc.Handle,addr,value,8);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所に バイナリデータを書き込みます。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="data">書き込むデータを指定します。</param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool Write(Diag::Process proc,System.IntPtr addr,params byte[] data){
			fixed(byte* pData=data)
				return DWriteMemoryArray(proc.Handle,addr,(void*)pData,data.Length);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所に 十六進文字列で指定したバイナリデータを書き込みます。
		/// </summary>
		/// <param name="procName">プロセス名を指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="hexValue">
		/// 書き込むデータを十六進数で記述した文字列を指定します。
		/// 変更を行わないバイトは "**" と指定することが可能です。
		/// 例: "12**5678"→ 1byte 目に 0x12 と書かれ、3byte 目に 0x56、4byte 目に 0x78 と書かれます。
		/// </param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool WriteHexStr(string procName,System.IntPtr addr,string hexValue){
			return WriteMemoryS(procName,addr,hexValue);
		}
		/// <summary>
		/// 指定したプロセスの指定した場所に、十六進文字列で指定したバイナリデータを書き込みます。
		/// </summary>
		/// <param name="proc">プロセスを指定します。</param>
		/// <param name="addr">値の書き込み先のアドレスを指定します。</param>
		/// <param name="hexValue">
		/// 書き込むデータを十六進数で記述した文字列を指定します。
		/// 変更を行わないバイトは "**" と指定することが可能です。
		/// 例: "12**5678"→ 1byte 目に 0x12 と書かれ、3byte 目に 0x56、4byte 目に 0x78 と書かれます。
		/// </param>
		/// <returns>書き込みが成功した場合に true を返します。それ以外の場合には false を返します。</returns>
		public static bool WriteHexStr(Diag::Process proc,System.IntPtr addr,string hexValue){
			return DWriteMemoryS(proc.Handle,addr,hexValue);
		}

		#region __declspec(dllimport) WriteMemory
		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool WriteMemory(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName,
			System.IntPtr addr,
			int value,
			int size
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool WriteMemory64(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName,
			System.IntPtr addr,
			long value,
			int size
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DWriteMemory(
			System.IntPtr hProc,
			System.IntPtr addr,
			int value,
			int size
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DWriteMemory64(
			System.IntPtr hProc,
			System.IntPtr addr,
			long value,
			int size
		);

		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DWriteMemoryArray(
			System.IntPtr hProc,
			System.IntPtr addr,
			void* value,
			int size
		);
		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool WriteMemoryS(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName,
			System.IntPtr addr,
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string hexValue
			);
		[Interop::DllImport("Memory.dll")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool DWriteMemoryS(
			System.IntPtr hProc,
			System.IntPtr addr,
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string hexValue
			);
		#endregion

		// WriteMemoryS
		// DWriteMemoryS
		// GetHandle
		// ExePro

		/// <summary>
		/// 指定したプロセス内の全スレッドを停止します。
		/// </summary>
		/// <param name="proc">停止の対象となるプロセスを指定します。</param>
		/// <returns>スレッドの停止が成功した場合に true を返します。それ以外の場合に false を返します。</returns>
		public static bool StopProcessThreads(Diag::Process proc){
			return StopProcessThreads(proc.ProcessName+".exe");
		}
		/// <summary>
		/// 指定したプロセス内の全スレッドを再開します。
		/// </summary>
		/// <param name="proc">再開の対象となるプロセスを指定します。</param>
		/// <returns>スレッドの再開が成功した場合に true を返します。それ以外の場合に false を返します。</returns>
		public static bool RestartProcessThreads(Diag::Process proc){
			return RestartProcessThreads(proc.ProcessName+".exe");
		}

		[Interop::DllImport("Memory.dll",EntryPoint="StopProc")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.I4)]
		public static extern bool StopProcessThreads(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName
			);

		[Interop::DllImport("Memory.dll",EntryPoint="RestartProc")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.I4)]
		public static extern bool RestartProcessThreads(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string procName
			);
	}
}