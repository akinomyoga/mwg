using Interop=System.Runtime.InteropServices;
namespace mwg.Windows{
	/// <summary>
	/// キーボードに関するイベントを管理
	/// MyKeyManager.AddHandler("Ctrl+A",new System.EventHandler())... 等の事も出来る様にする
	/// </summary>
	public class KeyManager{
		private mwg.Windows.ControlA ctrl;
		public KeyManager(mwg.Windows.ControlA ctrl){
			this.ctrl=ctrl;
			this.ctrl.PreProcess+=new mwg.Windows.ControlA.PreProcessEventHandler(ctrl_PreProcess);
			this.ctrl.WindowProc+=new mwg.Windows.ControlA.WndProcEvent(ctrl_WindowProc);
		}
		~KeyManager(){
			this.ctrl.PreProcess-=new mwg.Windows.ControlA.PreProcessEventHandler(ctrl_PreProcess);
			this.ctrl.WindowProc-=new mwg.Windows.ControlA.WndProcEvent(ctrl_WindowProc);
		}
		private void ctrl_WindowProc(object sender,ref System.Windows.Forms.Message m){
			switch((WM)m.Msg){
				case Windows.WM.KEYDOWN:this.OnKeyEvent("!KeyDown",m.WParam,m.LParam);break;
				case Windows.WM.KEYUP:this.OnKeyEvent("!KeyUp",m.WParam,m.LParam);break;
				case Windows.WM.CHAR:
				case Windows.WM.SYSCHAR:
					this.OnCharEvent("!Char",m.WParam,m.LParam);break;
			}
		}
		private bool ctrl_PreProcess(object sender,ref System.Windows.Forms.Message msg){
			switch((WM)msg.Msg){
				case Windows.WM.KEYDOWN:return this.OnKeyEvent("!PreKeyDown",msg.WParam,msg.LParam);
				case Windows.WM.KEYUP:return this.OnKeyEvent("!PreKeyUp",msg.WParam,msg.LParam);
			}
			return false;
		}
		//***********************************************************
		//		event
		//-----------------------------------------------------------
		protected System.ComponentModel.EventHandlerList _events=new System.ComponentModel.EventHandlerList();
		public event System.Windows.Forms.KeyEventHandler KeyDown{
			add{this._events.AddHandler("!KeyDown",value);}
			remove{this._events.RemoveHandler("!KeyDown",value);}
		}
		public event System.Windows.Forms.KeyEventHandler KeyUp{
			add{this._events.AddHandler("!KeyUp",value);}
			remove{this._events.RemoveHandler("!KeyUp",value);}
		}
		public event KeyManager.CharEH Char{
			add{this._events.AddHandler("!Char",value);}
			remove{this._events.RemoveHandler("!Char",value);}
		}
		public event System.Windows.Forms.KeyEventHandler PreKeyDown{
			add{this._events.AddHandler("!PreKeyDown",value);}
			remove{this._events.RemoveHandler("!PreKeyDown",value);}
		}
		public event System.Windows.Forms.KeyEventHandler PreKeyUp{
			add{this._events.AddHandler("!PreKeyUp",value);}
			remove{this._events.RemoveHandler("!PreKeyUp",value);}
		}
		public delegate void CharEH(object sender,KeyManager.CharEA e);
		public class CharEA{
			private char x;
			public char Char{get{return this.x;}}
			private KeyManager.MOD flags;
			public KeyManager.MOD Modifiers{get{return this.flags;}}
			public bool Alt{get{return (this.flags&KeyManager.MOD.Alt)!=0;}}
			public bool Shift{get{return (this.flags&KeyManager.MOD.Shift)!=0;}}
			public bool Ctrl{get{return (this.flags&KeyManager.MOD.Ctrl)!=0;}}
			public bool Win{get{return (this.flags&KeyManager.MOD.Win)!=0;}}
			private bool handled;
			public bool Handled{get{return this.handled;}set{this.handled=value;}}
			public CharEA(char x,KeyManager.MOD flags){
				this.x=x;
				this.flags=flags;
				this.handled=false;
			}
		}
		protected bool OnCharEvent(string eventName,System.IntPtr wParam,System.IntPtr lParam){
			mwg.Windows.KeyManager.CharEH ceh=this._events[eventName] as KeyManager.CharEH;
			if(ceh==null)return false;
			char x=(char)wParam;
			//修飾
			KeyManager.MOD mod=KeyManager.MOD.None;
			if((0x20000000&(int)lParam)!=0)mod|=KeyManager.MOD.Alt;
			if(this.Ctrl)mod|=KeyManager.MOD.Ctrl;
			if(this.Shift)mod|=KeyManager.MOD.Shift;
			if(this.Win)mod|=KeyManager.MOD.Win;
			//実行
			KeyManager.CharEA e=new CharEA(x,mod);
			System.Delegate[] dels=ceh.GetInvocationList();
			for(int i=0;i<dels.Length;i++){
				((KeyManager.CharEH)dels[i])(this,e);
				if(e.Handled)return true;
			}
			return false;
		}
		protected bool OnKeyEvent(string eventName,System.IntPtr wParam,System.IntPtr lParam){
			//DEBUG:
			//System.Console.WriteLine((int)lParam&0xffff);//←何故か連続押しは適用されていない
			System.Windows.Forms.KeyEventHandler keh=this._events[eventName] as System.Windows.Forms.KeyEventHandler;
			if(keh==null)return false;
			//keyに修飾
			System.Windows.Forms.Keys key=(System.Windows.Forms.Keys)(int)wParam;
			if((0x20000000&(int)lParam)!=0)key|=System.Windows.Forms.Keys.Menu;
			if(this.Ctrl)key|=System.Windows.Forms.Keys.Control;
			if(this.Shift)key|=System.Windows.Forms.Keys.Shift;
			//順番に実行
			System.Windows.Forms.KeyEventArgs e=new System.Windows.Forms.KeyEventArgs(key);
			System.Delegate[] dels=keh.GetInvocationList();
			for(int i=0;i<dels.Length;i++){
				((System.Windows.Forms.KeyEventHandler)dels[i])(this,e);
				if(e.Handled)return true;
			}
			return false;
		}
		//***********************************************************
		//		Modifier KeyState
		//-----------------------------------------------------------
		public bool Alt{
			get{return KeyManager.GetKeyState(System.Windows.Forms.Keys.LMenu)!=0||
					KeyManager.GetKeyState(System.Windows.Forms.Keys.RMenu)!=0;}
		}
		public bool Ctrl{
			get{return KeyManager.GetKeyState(System.Windows.Forms.Keys.LControlKey)!=0||
					KeyManager.GetKeyState(System.Windows.Forms.Keys.RControlKey)!=0;}
		}
		public bool Shift{
			get{return KeyManager.GetKeyState(System.Windows.Forms.Keys.LShiftKey)!=0||
					KeyManager.GetKeyState(System.Windows.Forms.Keys.RShiftKey)!=0;}
		}
		public bool Win{
			get{return KeyManager.GetKeyState(System.Windows.Forms.Keys.LWin)!=0||
					KeyManager.GetKeyState(System.Windows.Forms.Keys.RWin)!=0;}
		}
		//***********************************************************
		//		API
		//-----------------------------------------------------------
		//http://www.itmedia.co.jp/enterprise/articles/0412/07/news034_4.html //←未だ見ていない関数がある
		[Interop.DllImport("user32.dll")]
		public static extern short GetKeyState(System.Windows.Forms.Keys nVertKey);
		[Interop.DllImport("user32.dll")]
		public static extern int GetAsyncKeyState(System.Windows.Forms.Keys vKey);
		[Interop.DllImport("user32.dll")]
		public static extern int RegisterHotKey(System.IntPtr hwnd,int id,MOD fsModifiers,System.Windows.Forms.Keys vk); 
		[Interop.DllImport("user32.dll")]
		public static extern int UnregisterHotKey(System.IntPtr hwnd,int id);
		[System.Flags()]
		public enum MOD{
			None=0,
			Alt=1,
			Ctrl=2,
			Shift=4,
			Win=8
		}
		//GetKeyboardState
	}

	#region KeyStatus
	/// <summary>
	/// 現在のキーボードの様子を把握
	/// →API があるし、Control の標準の KeyDown イベントなどもある。必要なのか?
	/// </summary>
	public class KeyStatus{
		private int[] data=new int[16];
		public KeyStatus(){}
		public KeyStatus(mwg.Windows.ControlA ctrl){
			//ctrl.WindowProc+=new mwg.Windows.ControlA.WndProcEvent(ctrl_WindowProc);
		}
		public bool this[int index]{
			get{
				if(index<0||index>511){
					throw new System.ArgumentOutOfRangeException("index",index,"0 以上 512 未満 で指定下さい");
				}
				return 0!=(data[index>>5]&(1<<(index&0x1f)));
			}
			set{
				if(index<0||index>511){
					throw new System.ArgumentOutOfRangeException("index",index,"0 以上 512 未満 で指定下さい");
				}
				if(value)data[index>>5]|=(1<<(index&0x1f));
				else data[index>>5]&=~(1<<(index&0x1f));
			}
		}
		public void Clear(){
			for(int i=0;i<16;i++)this.data[i]=0;
		}
		public static bool operator ==(KeyStatus a,KeyStatus b){
			for(int i=0;i<16;i++)if(a.data[i]!=b.data[i])return false;
			return true;
		}
		public static bool operator !=(KeyStatus a,KeyStatus b){
			for(int i=0;i<16;i++)if(a.data[i]==b.data[i])return false;
			return true;
		}
		public override int GetHashCode(){
			int r=0x5c99286c;//出鱈目
			for(int i=0;i<16;i++)r^=this.data[i]^(this.data[i]<<11)^(this.data[i]>>20);
			return r;
		}
		public override bool Equals(object obj){
			if(obj.GetType()!=typeof(mwg.Windows.KeyStatus))return false;
			KeyStatus b=(KeyStatus)obj;
			for(int i=0;i<16;i++)if(this.data[i]!=b.data[i])return false;
			return true;
		}
		/// <summary>
		/// Debug: 現在のキーの状況をビットマップで表現します。
		/// </summary>
		public System.Drawing.Bitmap Bitmap(){
			System.Drawing.Bitmap r=new System.Drawing.Bitmap(32,16,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			int j=-1;
			for(int i=0;i<512;i++){
				if(i%32==0)j++;
				r.SetPixel(i%32,j,this[i]?System.Drawing.Color.YellowGreen:System.Drawing.Color.Black);
			}
			return r;
		}
	}
	#endregion
}