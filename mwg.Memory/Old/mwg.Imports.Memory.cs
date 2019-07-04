using Interop=System.Runtime.InteropServices;
using Diag=System.Diagnostics;

namespace mwg.Imports{
	public static unsafe class Memory{
		static Memory(){
			if(System.IntPtr.Size!=4)
				throw new System.Exception("64 bit �����ᑽ�����삵�Ȃ��Bx86 �ŃR���p�C������");
		}

		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ���� 32bit �l��ǂݎ��܂��B
		/// </summary>
		/// <param name="procName">�v���Z�X�����w�肵�܂��B</param>
		/// <param name="addr">�w�肵���v���Z�X�̃�������Ԃɉ�����A�ǂݍ��݌��̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�ǂݎ�����l��Ԃ��܂��B</param>
		/// <returns>�ǂݎ�肪���������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Read(string procName,System.IntPtr addr,out int value){
			return ReadMemory(procName,addr,out value,4);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ���� 64bit �l��ǂݎ��܂��B
		/// </summary>
		/// <param name="procName">�v���Z�X�����w�肵�܂��B</param>
		/// <param name="addr">�w�肵���v���Z�X�̃�������Ԃɉ�����A�ǂݍ��݌��̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�ǂݎ�����l��Ԃ��܂��B</param>
		/// <returns>�ǂݎ�肪���������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Read(string procName,System.IntPtr addr,out long value){
			return ReadMemory64(procName,addr,out value,8);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ���� 32bit �l��ǂݎ��܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�w�肵���v���Z�X�̃�������Ԃɉ�����A�ǂݍ��݌��̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�ǂݎ�����l��Ԃ��܂��B</param>
		/// <returns>�ǂݎ�肪���������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Read(Diag::Process proc,System.IntPtr addr,out int value){
			return DReadMemory(proc.Handle,addr,out value,4);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ���� 64bit �l��ǂݎ��܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�w�肵���v���Z�X�̃�������Ԃɉ�����A�ǂݍ��݌��̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�ǂݎ�����l��Ԃ��܂��B</param>
		/// <returns>�ǂݎ�肪���������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Read(Diag::Process proc,System.IntPtr addr,out long value){
			return DReadMemory64(proc.Handle,addr,out value,8);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ����A�w�肵�������̃o�C�i���f�[�^��ǂݎ��܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�w�肵���v���Z�X�̃�������Ԃɉ�����A�ǂݍ��݌��̃A�h���X���w�肵�܂��B</param>
		/// <param name="data">�ǂݎ�����l��Ԃ��܂��B</param>
		/// <param name="size">�ǂݎ��f�[�^�̑傫�����o�C�g�P�ʂŎw�肵�܂��B</param>
		/// <returns>�ǂݎ�肪���������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
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
			System.IntPtr addr, /* C �p�̃w�b�_������� DWORD (32 bit) */
			out int value,
			int size /* 1-4 �̒l���w�肷�邻�������A1-3 ���w�肵���ꍇ�̓��삪�s�� */
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
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�� 32bit �l���������݂܂��B
		/// </summary>
		/// <param name="procName">�v���Z�X�����w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�������ޒl���w�肵�܂��B</param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Write(string procName,System.IntPtr addr,int value){
			return WriteMemory(procName,addr,value,4);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�� 64bit �l���������݂܂��B
		/// </summary>
		/// <param name="procName">�v���Z�X�����w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�������ޒl���w�肵�܂��B</param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Write(string procName,System.IntPtr addr,long value){
			return WriteMemory64(procName,addr,value,8);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�� 32bit �l���������݂܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�������ޒl���w�肵�܂��B</param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Write(Diag::Process proc,System.IntPtr addr,int value){
			return DWriteMemory(proc.Handle,addr,value,4);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�� 64bit �l���������݂܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="value">�������ޒl���w�肵�܂��B</param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Write(Diag::Process proc,System.IntPtr addr,long value){
			return DWriteMemory64(proc.Handle,addr,value,8);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�� �o�C�i���f�[�^���������݂܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="data">�������ރf�[�^���w�肵�܂��B</param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool Write(Diag::Process proc,System.IntPtr addr,params byte[] data){
			fixed(byte* pData=data)
				return DWriteMemoryArray(proc.Handle,addr,(void*)pData,data.Length);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�� �\�Z�i������Ŏw�肵���o�C�i���f�[�^���������݂܂��B
		/// </summary>
		/// <param name="procName">�v���Z�X�����w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="hexValue">
		/// �������ރf�[�^���\�Z�i���ŋL�q������������w�肵�܂��B
		/// �ύX���s��Ȃ��o�C�g�� "**" �Ǝw�肷�邱�Ƃ��\�ł��B
		/// ��: "12**5678"�� 1byte �ڂ� 0x12 �Ə�����A3byte �ڂ� 0x56�A4byte �ڂ� 0x78 �Ə�����܂��B
		/// </param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		public static bool WriteHexStr(string procName,System.IntPtr addr,string hexValue){
			return WriteMemoryS(procName,addr,hexValue);
		}
		/// <summary>
		/// �w�肵���v���Z�X�̎w�肵���ꏊ�ɁA�\�Z�i������Ŏw�肵���o�C�i���f�[�^���������݂܂��B
		/// </summary>
		/// <param name="proc">�v���Z�X���w�肵�܂��B</param>
		/// <param name="addr">�l�̏������ݐ�̃A�h���X���w�肵�܂��B</param>
		/// <param name="hexValue">
		/// �������ރf�[�^���\�Z�i���ŋL�q������������w�肵�܂��B
		/// �ύX���s��Ȃ��o�C�g�� "**" �Ǝw�肷�邱�Ƃ��\�ł��B
		/// ��: "12**5678"�� 1byte �ڂ� 0x12 �Ə�����A3byte �ڂ� 0x56�A4byte �ڂ� 0x78 �Ə�����܂��B
		/// </param>
		/// <returns>�������݂����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�ɂ� false ��Ԃ��܂��B</returns>
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
		/// �w�肵���v���Z�X���̑S�X���b�h���~���܂��B
		/// </summary>
		/// <param name="proc">��~�̑ΏۂƂȂ�v���Z�X���w�肵�܂��B</param>
		/// <returns>�X���b�h�̒�~�����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�� false ��Ԃ��܂��B</returns>
		public static bool StopProcessThreads(Diag::Process proc){
			return StopProcessThreads(proc.ProcessName+".exe");
		}
		/// <summary>
		/// �w�肵���v���Z�X���̑S�X���b�h���ĊJ���܂��B
		/// </summary>
		/// <param name="proc">�ĊJ�̑ΏۂƂȂ�v���Z�X���w�肵�܂��B</param>
		/// <returns>�X���b�h�̍ĊJ�����������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�� false ��Ԃ��܂��B</returns>
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