using mwg.Win32;
using _=mwg.Win32.__global;
using HANDLE=System.IntPtr;

namespace mwg.InterProcess{
	/// <summary>
	/// �v���Z�X���ׂ� Windows �C�x���g���Ǘ����܂��B
	/// </summary>
	public class WinEvent:System.IDisposable{
		private string name;
		private HANDLE handle=HANDLE.Zero;

		/// <summary>
		/// �C�x���g�����ʂ���ׂɕt����ꂽ���O���擾���܂��B
		/// </summary>
		public string Name{
			get{return this.name;}
		}
		/// <summary>
		/// �C�x���g���Ǘ�����ׂ̃n���h���l���擾���܂��B
		/// </summary>
		public HANDLE Handle{
			get{return this.handle;}
		}
		/// <summary>
		/// ���̃I�u�W�F�N�g��\�����镶������擾���܂��B
		/// </summary>
		/// <returns>WinEvent �^�ł��鎖�ƃC�x���g����\�L�����������Ԃ��܂��B</returns>
		public override string ToString(){
			return "mwg::InterProcess::WinEvent - '"+this.name+"'";
		}

		/// <summary>
		/// �C�x���g�� (���) ���������܂��B
		/// </summary>
		public void Pulse(){
			Kernel32.PulseEvent(this.handle);
		}
		/// <summary>
		/// �C�x���g���V�O�i����Ԃɐݒ肵�܂��B
		/// </summary>
		public void Set(){
			Kernel32.SetEvent(this.handle);
		}
		/// <summary>
		/// �C�x���g�̃V�O�i����Ԃ��������܂��B
		/// </summary>
		public void Reset(){
			Kernel32.ResetEvent(this.handle);
		}

		private System.Threading.Thread th=null;
		private System.Action<WinEvent> raised=null;
		/// <summary>
		/// Windows �C�x���g�������������̏�����ݒ肵�܂��B
		/// </summary>
		public event System.Action<WinEvent> Raised{
			add{
				if(raised==null&&value!=null&&th==null){
					th=new System.Threading.Thread(waiter);
					th.IsBackground=true;
					th.Name=this.ToString();
					th.Start();
				}
				raised+=value;
			}
			remove{
				raised-=value;
			}
		}
		private void waiter(){
			while(true){
				Kernel32.WAIT wait=Kernel32.WaitForSingleObject(this.handle,_.INFINITE);
				if(wait==Kernel32.WAIT.OBJECT_0){
					if(raised!=null)this.raised(this);
				}
			}
		}
		//============================================================
		//		�C���X�^���X�쐬
		//============================================================
		private unsafe WinEvent(string name,bool manualReset){
			this.name=name;
			handle=Kernel32.CreateEvent(
				(Kernel32.SECURITY_ATTRIBUTES*)_.NULL,
				manualReset,false,name);
		}
		private unsafe WinEvent(string name,HANDLE handle){
			this.name=name;
			this.handle=handle;
		}

		/// <summary>
		/// ���ɑ��݂��Ă��� Windows �C�x���g���擾���܂��B
		/// </summary>
		/// <param name="name">������ Windows �C�x���g�̖��O���w�肵�܂��B</param>
		/// <returns>������ Windows �C�x���g���Ǘ�����I�u�W�F�N�g��Ԃ��܂��B
		/// �w�肵�����O�̂̃C�x���g�����݂��Ă��Ȃ��ꍇ�ɂ� null ��Ԃ��܂��B</returns>
		public static WinEvent Open(string name){
			HANDLE h=Kernel32.OpenEvent(Kernel32.EVENT_ACCESS.ALL_ACCESS,false,name);
			if(h==HANDLE.Zero)return null;
			return new WinEvent(name,h);
		}
		/// <summary>
		/// Windows �C�x���g���쐬���܂��B���ɑ��݂��Ă���ꍇ�ɂ́A�����Ԃ��܂��B
		/// </summary>
		/// <param name="name">�쐬���� Windows �C�x���g�����ʂ���ׂ̖��O���w�肵�܂��B</param>
		/// <param name="manualReset">�蓮�ŃV�O�i����Ԃ���������ꍇ�� true ���w�肵�܂��B����ȊO�̏ꍇ�� false ���w�肵�܂��B</param>
		/// <returns>�쐬���� Windows �C�x���g���Ǘ�����I�u�W�F�N�g��Ԃ��܂��B
		/// �����̃C�x���g�������̏ꍇ�ɂ́A�����̕���Ԃ��܂��B</returns>
		public static WinEvent Create(string name,bool manualReset){
			return new WinEvent(name,manualReset);
		}
		/// <summary>
		/// �V���� Windows �C�x���g���쐬���܂��B���ɑ��݂��Ă���ꍇ�ɂ́Anull ��Ԃ��܂��B
		/// </summary>
		/// <param name="name">�쐬���� Windows �C�x���g�����ʂ���ׂ̖��O���w�肵�܂��B</param>
		/// <param name="manualReset">�蓮�ŃV�O�i����Ԃ���������ꍇ�� true ���w�肵�܂��B����ȊO�̏ꍇ�� false ���w�肵�܂��B</param>
		/// <returns>�V�����쐬���� Windows �C�x���g���Ǘ�����I�u�W�F�N�g��Ԃ��܂��B
		/// ���ɓ����̃C�x���g�����݂��Ă����ꍇ�ɂ́Anull ��Ԃ��܂��B</returns>
		public static WinEvent CreateNew(string name,bool manualReset){
			const int ERROR_ALREADY_EXISTS=183;

			WinEvent w=new WinEvent(name,manualReset);
			if(Kernel32.GetLastError()==ERROR_ALREADY_EXISTS){
				w.Close();
				return null;
			}
			return w;
		}

		//============================================================
		//		��n��
		//============================================================
		/// <summary>
		/// ���̃C�x���g�����݂̃v���Z�X���������܂��B
		/// </summary>
		public void Close(){
			if(this.handle!=System.IntPtr.Zero)
				Kernel32.CloseHandle(this.handle);
			System.GC.SuppressFinalize(this);
			this.handle=System.IntPtr.Zero;
		}
		/// <summary>
		/// ���̃C�x���g�����݂̃v���Z�X���������܂��B
		/// </summary>
		public void Dispose(){this.Close();}
		/// <summary>
		/// ��n���̏��������s���܂��B�C�x���g�̉�����s���܂��B
		/// </summary>
		~WinEvent(){this.Close();}
	}
}