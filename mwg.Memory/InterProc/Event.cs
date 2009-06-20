using mwg.Win32;
using _=mwg.Win32.__global;
using HANDLE=System.IntPtr;

namespace mwg.InterProcess{
	/// <summary>
	/// プロセスを跨る Windows イベントを管理します。
	/// </summary>
	public class WinEvent:System.IDisposable{
		private string name;
		private HANDLE handle=HANDLE.Zero;

		/// <summary>
		/// イベントを識別する為に付けられた名前を取得します。
		/// </summary>
		public string Name{
			get{return this.name;}
		}
		/// <summary>
		/// イベントを管理する為のハンドル値を取得します。
		/// </summary>
		public HANDLE Handle{
			get{return this.handle;}
		}
		/// <summary>
		/// このオブジェクトを表現する文字列を取得します。
		/// </summary>
		/// <returns>WinEvent 型である事とイベント名を表記した文字列を返します。</returns>
		public override string ToString(){
			return "mwg::InterProcess::WinEvent - '"+this.name+"'";
		}

		/// <summary>
		/// イベントを (一回) 発生させます。
		/// </summary>
		public void Pulse(){
			Kernel32.PulseEvent(this.handle);
		}
		/// <summary>
		/// イベントをシグナル状態に設定します。
		/// </summary>
		public void Set(){
			Kernel32.SetEvent(this.handle);
		}
		/// <summary>
		/// イベントのシグナル状態を解除します。
		/// </summary>
		public void Reset(){
			Kernel32.ResetEvent(this.handle);
		}

		private System.Threading.Thread th=null;
		private System.Action<WinEvent> raised=null;
		/// <summary>
		/// Windows イベントが発生した時の処理を設定します。
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
		//		インスタンス作成
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
		/// 既に存在している Windows イベントを取得します。
		/// </summary>
		/// <param name="name">既存の Windows イベントの名前を指定します。</param>
		/// <returns>既存の Windows イベントを管理するオブジェクトを返します。
		/// 指定した名前ののイベントが存在していない場合には null を返します。</returns>
		public static WinEvent Open(string name){
			HANDLE h=Kernel32.OpenEvent(Kernel32.EVENT_ACCESS.ALL_ACCESS,false,name);
			if(h==HANDLE.Zero)return null;
			return new WinEvent(name,h);
		}
		/// <summary>
		/// Windows イベントを作成します。既に存在している場合には、それを返します。
		/// </summary>
		/// <param name="name">作成する Windows イベントを識別する為の名前を指定します。</param>
		/// <param name="manualReset">手動でシグナル状態を解除する場合に true を指定します。それ以外の場合に false を指定します。</param>
		/// <returns>作成した Windows イベントを管理するオブジェクトを返します。
		/// 同名のイベントが既存の場合には、既存の物を返します。</returns>
		public static WinEvent Create(string name,bool manualReset){
			return new WinEvent(name,manualReset);
		}
		/// <summary>
		/// 新しく Windows イベントを作成します。既に存在している場合には、null を返します。
		/// </summary>
		/// <param name="name">作成する Windows イベントを識別する為の名前を指定します。</param>
		/// <param name="manualReset">手動でシグナル状態を解除する場合に true を指定します。それ以外の場合に false を指定します。</param>
		/// <returns>新しく作成した Windows イベントを管理するオブジェクトを返します。
		/// 既に同名のイベントが存在していた場合には、null を返します。</returns>
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
		//		後始末
		//============================================================
		/// <summary>
		/// このイベントを現在のプロセスから解放します。
		/// </summary>
		public void Close(){
			if(this.handle!=System.IntPtr.Zero)
				Kernel32.CloseHandle(this.handle);
			System.GC.SuppressFinalize(this);
			this.handle=System.IntPtr.Zero;
		}
		/// <summary>
		/// このイベントを現在のプロセスから解放します。
		/// </summary>
		public void Dispose(){this.Close();}
		/// <summary>
		/// 後始末の処理を実行します。イベントの解放を行います。
		/// </summary>
		~WinEvent(){this.Close();}
	}
}