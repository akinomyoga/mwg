using Interop=System.Runtime.InteropServices;
namespace mwg.Windows{
	public enum WM{
		NULL = 0x00,
		CREATE = 0x01,
		DESTROY = 0x02,
		MOVE = 0x03,
		SIZE = 0x05,
		ACTIVATE = 0x06,
		SETFOCUS = 0x07,
		KILLFOCUS = 0x08,
		ENABLE = 0x0A,
		SETREDRAW = 0x0B,
		SETTEXT = 0x0C,
		GETTEXT = 0x0D,
		GETTEXTLENGTH = 0x0E,
		PAINT = 0x0F,
		CLOSE = 0x10,
		QUERYENDSESSION = 0x11,
		QUIT = 0x12,
		QUERYOPEN = 0x13,
		ERASEBKGND = 0x14,
		SYSCOLORCHANGE = 0x15,
		ENDSESSION = 0x16,
		SYSTEMERROR = 0x17,
		SHOWWINDOW = 0x18,
		CTLCOLOR = 0x19,
		WININICHANGE = 0x1A,
		SETTINGCHANGE = 0x1A,
		DEVMODECHANGE = 0x1B,
		ACTIVATEAPP = 0x1C,
		FONTCHANGE = 0x1D,
		TIMECHANGE = 0x1E,
		CANCELMODE = 0x1F,
		SETCURSOR = 0x20,
		MOUSEACTIVATE = 0x21,
		CHILDACTIVATE = 0x22,
		QUEUESYNC = 0x23,
		GETMINMAXINFO = 0x24,
		PAINTICON = 0x26,
		ICONERASEBKGND = 0x27,
		NEXTDLGCTL = 0x28,
		SPOOLERSTATUS = 0x2A,
		DRAWITEM = 0x2B,
		MEASUREITEM = 0x2C,
		DELETEITEM = 0x2D,
		VKEYTOITEM = 0x2E,
		CHARTOITEM = 0x2F,
		SETFONT = 0x30,
		GETFONT = 0x31,
		SETHOTKEY = 0x32,
		GETHOTKEY = 0x33,
		QUERYDRAGICON = 0x37,
		COMPAREITEM = 0x39,
		COMPACTING = 0x41,
		WINDOWPOSCHANGING = 0x46,
		WINDOWPOSCHANGED = 0x47,
		POWER = 0x48,
		COPYDATA = 0x4A,
		CANCELJOURNAL = 0x4B,
		NOTIFY = 0x4E,
		INPUTLANGCHANGEREQUEST = 0x50,
		INPUTLANGCHANGE = 0x51,
		TCARD = 0x52,
		HELP = 0x53,
		USERCHANGED = 0x54,
		NOTIFYFORMAT = 0x55,
		CONTEXTMENU = 0x7B,
		STYLECHANGING = 0x7C,
		STYLECHANGED = 0x7D,
		DISPLAYCHANGE = 0x7E,
		GETICON = 0x7F,
		SETICON = 0x80,
		NCCREATE = 0x81,
		NCDESTROY = 0x82,
		NCCALCSIZE = 0x83,
		NCHITTEST = 0x84,
		NCPAINT = 0x85,
		NCACTIVATE = 0x86,
		GETDLGCODE = 0x87,
		NCMOUSEMOVE = 0xA0,
		NCLBUTTONDOWN = 0xA1,
		NCLBUTTONUP = 0xA2,
		NCLBUTTONDBLCLK = 0xA3,
		NCRBUTTONDOWN = 0xA4,
		NCRBUTTONUP = 0xA5,
		NCRBUTTONDBLCLK = 0xA6,
		NCMBUTTONDOWN = 0xA7,
		NCMBUTTONUP = 0xA8,
		NCMBUTTONDBLCLK = 0xA9,
		KEYFIRST = 0x100,
		KEYDOWN = 0x100,
		KEYUP = 0x101,
		CHAR = 0x102,
		DEADCHAR = 0x103,
		SYSKEYDOWN = 0x104,
		SYSKEYUP = 0x105,
		SYSCHAR = 0x106,
		SYSDEADCHAR = 0x107,
		KEYLAST = 0x108,
		IME_STARTCOMPOSITION = 0x10D,
		IME_ENDCOMPOSITION = 0x10E,
		IME_COMPOSITION = 0x10F,
		IME_KEYLAST = 0x10F,
		INITDIALOG = 0x110,
		COMMAND = 0x111,
		SYSCOMMAND = 0x112,
		TIMER = 0x113,
		HSCROLL = 0x114,
		VSCROLL = 0x115,
		INITMENU = 0x116,
		INITMENUPOPUP = 0x117,
		MENUSELECT = 0x11F,
		MENUCHAR = 0x120,
		ENTERIDLE = 0x121,
		CTLCOLORMSGBOX = 0x132,
		CTLCOLOREDIT = 0x133,
		CTLCOLORLISTBOX = 0x134,
		CTLCOLORBTN = 0x135,
		CTLCOLORDLG = 0x136,
		CTLCOLORSCROLLBAR = 0x137,
		CTLCOLORSTATIC = 0x138,
		MOUSEFIRST = 0x200,
		MOUSEMOVE = 0x200,
		LBUTTONDOWN = 0x201,
		LBUTTONUP = 0x202,
		LBUTTONDBLCLK = 0x203,
		RBUTTONDOWN = 0x204,
		RBUTTONUP = 0x205,
		RBUTTONDBLCLK = 0x206,
		MBUTTONDOWN = 0x207,
		MBUTTONUP = 0x208,
		MBUTTONDBLCLK = 0x209,
		MOUSELAST = 0x20A,
		MOUSEWHEEL = 0x20A,
		PARENTNOTIFY = 0x210,
		ENTERMENULOOP = 0x211,
		EXITMENULOOP = 0x212,
		NEXTMENU = 0x213,
		SIZING = 0x214,
		CAPTURECHANGED = 0x215,
		MOVING = 0x216,
		POWERBROADCAST = 0x218,
		DEVICECHANGE = 0x219,
		MDICREATE = 0x220,
		MDIDESTROY = 0x221,
		MDIACTIVATE = 0x222,
		MDIRESTORE = 0x223,
		MDINEXT = 0x224,
		MDIMAXIMIZE = 0x225,
		MDITILE = 0x226,
		MDICASCADE = 0x227,
		MDIICONARRANGE = 0x228,
		MDIGETACTIVE = 0x229,
		MDISETMENU = 0x230,
		ENTERSIZEMOVE = 0x231,
		EXITSIZEMOVE = 0x232,
		DROPFILES = 0x233,
		MDIREFRESHMENU = 0x234,
		IME_SETCONTEXT = 0x281,
		IME_NOTIFY = 0x282,
		IME_CONTROL = 0x283,
		IME_COMPOSITIONFULL = 0x284,
		IME_SELECT = 0x285,
		IME_CHAR = 0x286,
		IME_KEYDOWN = 0x290,
		IME_KEYUP = 0x291,
		MOUSEHOVER = 0x2A1,
		NCMOUSELEAVE = 0x2A2,
		MOUSELEAVE = 0x2A3,
		CUT = 0x300,
		COPY = 0x301,
		PASTE = 0x302,
		CLEAR = 0x303,
		UNDO = 0x304,
		RENDERFORMAT = 0x305,
		RENDERALLFORMATS = 0x306,
		DESTROYCLIPBOARD = 0x307,
		DRAWCLIPBOARD = 0x308,
		PAINTCLIPBOARD = 0x309,
		VSCROLLCLIPBOARD = 0x30A,
		SIZECLIPBOARD = 0x30B,
		ASKCBFORMATNAME = 0x30C,
		CHANGECBCHAIN = 0x30D,
		HSCROLLCLIPBOARD = 0x30E,
		QUERYNEWPALETTE = 0x30F,
		PALETTEISCHANGING = 0x310,
		PALETTECHANGED = 0x311,
		HOTKEY = 0x312,
		PRINT = 0x317,
		PRINTCLIENT = 0x318,
		HANDHELDFIRST = 0x358,
		HANDHELDLAST = 0x35F,
		PENWINFIRST = 0x380,
		PENWINLAST = 0x38F,
		COALESCE_FIRST = 0x390,
		COALESCE_LAST = 0x39F,
		DDE_FIRST = 0x3E0,
		DDE_INITIATE = 0x3E0,
		DDE_TERMINATE = 0x3E1,
		DDE_ADVISE = 0x3E2,
		DDE_UNADVISE = 0x3E3,
		DDE_ACK = 0x3E4,
		DDE_DATA = 0x3E5,
		DDE_REQUEST = 0x3E6,
		DDE_POKE = 0x3E7,
		DDE_EXECUTE = 0x3E8,
		DDE_LAST = 0x3E8,
		USER = 0x400,
		APP = 0x8000
	}
	public enum VK{
		//winuser.h より
		LBUTTON=0x01,
		RBUTTON=0x02,
		CANCEL=0x03,
		MBUTTON=0x04,    /* NOT contiguous with L & RBUTTON */

		//if(_WIN32_WINNT >= 0x0500)
		XBUTTON1=0x05,    /* NOT contiguous with L & RBUTTON */
		XBUTTON2=0x06,    /* NOT contiguous with L & RBUTTON */
		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
		 * 0x07 : unassigned
		 */

		BACK=0x08,
		TAB=0x09,

		/*
		 * 0x0A - 0x0B : reserved
		 */

		CLEAR=0x0C,
		RETURN=0x0D,

		SHIFT=0x10,
		CONTROL=0x11,
		MENU=0x12,
		PAUSE=0x13,
		CAPITAL=0x14,
		
		//IME-MODE
		KANA=0x15,
		HANGEUL=0x15,  /* old name - should be here for compatibility */
		HANGUL=0x15,
		JUNJA=0x17,
		FINAL=0x18,
		HANJA=0x19,
		KANJI=0x19,

		ESCAPE=0x1B,
		
		//IME
		CONVERT=0x1C,
		NONCONVERT=0x1D,
		ACCEPT=0x1E,
		MODECHANGE=0x1F,

		/**Space*/	SPACE=0x20,
		/**PageUp*/		PRIOR=0x21,
		/**PageDown*/	NEXT=0x22,
		/**End*/		END=0x23,
		/**Home*/		HOME=0x24,
		/**←*/			LEFT=0x25,
		/**↑*/			UP=0x26,
		/**→*/			RIGHT=0x27,
		/**↓*/			DOWN=0x28,
		/**Select*/		SELECT=0x29,
		/**Print*/		PRINT=0x2A,
		/**Execute*/	EXECUTE=0x2B,
		/**PrintScreen*/SNAPSHOT=0x2C,//PrintScreen
		/**Insert*/		INSERT=0x2D,
		/**Delete*/		DELETE=0x2E,
		/**Help*/		HELP=0x2F,

		/*
		 * VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
		 * 0x40 : unassigned
		 * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
		 * 
		 * '0' - '9' は以下では D を冠して記す事にする
		 */
		D0=0x30,		D1=0x31,		D2=0x32,		D3=0x33,		D4=0x34,
		D5=0x35,		D6=0x36,		D7=0x37,		D8=0x38,		D9=0x39,

		A=0x41,		B=0x42,		C=0x43,		D=0x44,		E=0x45,
		F=0x46,		G=0x47,		H=0x48,		I=0x49,		J=0x4a,
		K=0x4b,		L=0x4c,		M=0x4d,		N=0x4e,		O=0x4f,
		P=0x50,		Q=0x51,		R=0x52,		S=0x53,		T=0x54,
		U=0x55,		V=0x56,		W=0x57,		X=0x58,		Y=0x59,
		Z=0x5a,
		
		LWIN=0x5B,
		RWIN=0x5C,
		APPS=0x5D,

		/*
		 * 0x5E : reserved
		 */

		SLEEP=0x5F,//不明

		NUMPAD0=0x60,		NUMPAD1=0x61,		NUMPAD2=0x62,		NUMPAD3=0x63,		NUMPAD4=0x64,
		NUMPAD5=0x65,		NUMPAD6=0x66,		NUMPAD7=0x67,		NUMPAD8=0x68,		NUMPAD9=0x69,
		MULTIPLY=0x6A,
		ADD=0x6B,
		SEPARATOR=0x6C,
		SUBTRACT=0x6D,
		DECIMAL=0x6E,
		DIVIDE=0x6F,
		F1=0x70,		F2=0x71,		F3=0x72,		F4=0x73,		F5=0x74,
		F6=0x75,		F7=0x76,		F8=0x77,		F9=0x78,		F10=0x79,
		F11=0x7A,		F12=0x7B,		F13=0x7C,		F14=0x7D,		F15=0x7E,
		F16=0x7F,		F17=0x80,		F18=0x81,		F19=0x82,		F20=0x83,
		F21=0x84,		F22=0x85,		F23=0x86,		F24=0x87,

		/*
		 * 0x88 - 0x8F : unassigned
		 */

		NUMLOCK=0x90,
		SCROLL=0x91,

		/*
		 * NEC PC-9800 kbd definitions
		 */
		OEM_NEC_EQUAL=0x92,   // '=' key on numpad

		/*
		 * Fujitsu/OASYS kbd definitions
		 */
		OEM_FJ_JISHO=0x92,   // 'Dictionary' key
		OEM_FJ_MASSHOU=0x93,   // 'Unregister word' key
		OEM_FJ_TOUROKU=0x94,   // 'Register word' key
		OEM_FJ_LOYA=0x95,   // 'Left OYAYUBI' key
		OEM_FJ_ROYA=0x96,   // 'Right OYAYUBI' key

		/*
		 * 0x97 - 0x9F : unassigned
		 */

		/*
		 * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
		 * Used only as parameters to GetAsyncKeyState() and GetKeyState().
		 * No other API or message will distinguish left and right keys in this way.
		 */
		LSHIFT=0xA0,
		RSHIFT=0xA1,
		LCONTROL=0xA2,
		RCONTROL=0xA3,
		LMENU=0xA4,
		RMENU=0xA5,

		//if(_WIN32_WINNT >= 0x0500)
		BROWSER_BACK=0xA6,
		BROWSER_FORWARD=0xA7,
		BROWSER_REFRESH=0xA8,
		BROWSER_STOP=0xA9,
		BROWSER_SEARCH=0xAA,
		BROWSER_FAVORITES=0xAB,
		BROWSER_HOME=0xAC,

		VOLUME_MUTE=0xAD,
		VOLUME_DOWN=0xAE,
		VOLUME_UP=0xAF,
		MEDIA_NEXT_TRACK=0xB0,
		MEDIA_PREV_TRACK=0xB1,
		MEDIA_STOP=0xB2,
		MEDIA_PLAY_PAUSE=0xB3,
		LAUNCH_MAIL=0xB4,
		LAUNCH_MEDIA_SELECT=0xB5,
		LAUNCH_APP1=0xB6,
		LAUNCH_APP2=0xB7,

		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
		 * 0xB8 - 0xB9 : reserved
		 */

		OEM_1=0xBA,   // ';:' for US
		OEM_PLUS=0xBB,   // '+' any country
		OEM_COMMA=0xBC,   // ',' any country
		OEM_MINUS=0xBD,   // '-' any country
		OEM_PERIOD=0xBE,   // '.' any country
		OEM_2=0xBF,   // '/?' for US
		OEM_3=0xC0,   // '`~' for US

		/*
		 * 0xC1 - 0xD7 : reserved
		 */

		/*
		 * 0xD8 - 0xDA : unassigned
		 */

		OEM_4=0xDB,  //  '[{' for US
		OEM_5=0xDC,  //  '\|' for US
		OEM_6=0xDD,  //  ']}' for US
		OEM_7=0xDE,  //  ''"' for US
		OEM_8=0xDF,

		/*
		 * 0xE0 : reserved
		 */

		/*
		 * Various extended or enhanced keyboards
		 */
		//以下不明
		OEM_AX=0xE1,  //  'AX' key on Japanese AX kbd
		OEM_102=0xE2,  //  "<>" or "\|" on RT 102-key kbd.
		ICO_HELP=0xE3,  //  Help key on ICO
		ICO_00=0xE4,  //  00 key on ICO

		//if(WINVER >= 0x0400)
		PROCESSKEY=0xE5,
		//endif /* WINVER >= 0x0400 */

		ICO_CLEAR=0xE6,


		//if(_WIN32_WINNT >= 0x0500)
		PACKET=0xE7,
		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
		 * 0xE8 : unassigned
		 */

		/*
		 * Nokia/Ericsson definitions
		 */
		OEM_RESET=0xE9,
		OEM_JUMP=0xEA,
		OEM_PA1=0xEB,
		OEM_PA2=0xEC,
		OEM_PA3=0xED,
		OEM_WSCTRL=0xEE,
		OEM_CUSEL=0xEF,
		OEM_ATTN=0xF0,
		OEM_FINISH=0xF1,
		OEM_COPY=0xF2,
		OEM_AUTO=0xF3,
		OEM_ENLW=0xF4,
		OEM_BACKTAB=0xF5,

		ATTN=0xF6,
		CRSEL=0xF7,
		EXSEL=0xF8,
		EREOF=0xF9,
		PLAY=0xFA,
		ZOOM=0xFB,
		NONAME=0xFC,
		PA1=0xFD,
		OEM_CLEAR=0xFE

		/*
		 * 0xFF : reserved
		 */
	}
	[Interop.StructLayout(Interop.LayoutKind.Sequential,CharSet=Interop.CharSet.Auto)]   
	public struct LOGFONT{   
		public int lfHeight;
		public int lfWidth;
		public int lfEscapement;
		public int lfOrientation;
		public int lfWeight;
		public byte lfItalic;
		public byte lfUnderline;
		public byte lfStrikeOut;
		public byte lfCharSet;
		public byte lfOutPrecision;
		public byte lfClipPrecision;
		public byte lfQuality;
		public byte lfPitchAndFamily;
		// stringでいけると書いてあったがうまく動かないのでこれで   
		[System.Runtime.InteropServices.MarshalAs(Interop.UnmanagedType.ByValArray, SizeConst=32/*LF_FACESIZE*/*2)]
		public byte[] lfFaceName;
	}
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]   
	public struct POINTAPI{
		int x;
		int y;
		public int X{get{return this.x;}set{this.x=value;}}
		public int Y{get{return this.y;}set{this.y=value;}}
		public POINTAPI(int x,int y){
			this.x=x;
			this.y=y;
		}
		public static implicit operator System.Drawing.Point(POINTAPI pt){
			return new System.Drawing.Point(pt.x,pt.y);
		}
		public static implicit operator POINTAPI(System.Drawing.Point pt){
			return new POINTAPI(pt.X,pt.Y);
		}
	}
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct RECT{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
		public RECT(int left,int top,int right,int bottom){
			this.Left=left;
			this.Top=top;
			this.Right=right;
			this.Bottom=bottom;
		}
		public static implicit operator RECT(System.Drawing.Rectangle rect){
			return new RECT(rect.Left,rect.Top,rect.Right,rect.Bottom);
		}
		public static implicit operator System.Drawing.Rectangle(RECT rect){
			return new System.Drawing.Rectangle(rect.Left,rect.Top,rect.Right-rect.Left,rect.Bottom-rect.Top);
		}
	}

	#region ControlA
	public class ControlA:System.Windows.Forms.UserControl{
		public ControlA(){
			this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint|
				System.Windows.Forms.ControlStyles.DoubleBuffer|
				System.Windows.Forms.ControlStyles.UserPaint,true);
			this.UpdateStyles();
		}
		//WindowProc イベント
		/// <summary>
		/// m.Result を書き換えても、このイベントにフックした別の関数の動作を抑制する事は出来ません
		/// (複数の関数をフックした場合、その中に m.Result の値によって動作を変える関数があると、
		/// 予期せぬ動作を起こすかも知れません。注意して下さい)
		/// </summary>
		public event WndProcEvent WindowProc;
		public delegate void WndProcEvent(object sender,ref System.Windows.Forms.Message m);
		protected virtual void OnWindowProc(ref System.Windows.Forms.Message m){
			if(this.WindowProc==null)return;
			this.WindowProc(this,ref m);
		}
		public event PreProcessEventHandler PreProcess;
		public delegate bool PreProcessEventHandler(object sender,ref System.Windows.Forms.Message m);
		/// <summary>
		/// 他の処理に先立って Message 処理を行います。
		/// </summary>
		/// <param name="msg">Message (WM_(SYS)KEYDOWN, WM_(SYS)CHAR)</param>
		/// <returns>メッセージが処理された場合は true 処理されていない場合は false</returns>
		protected bool OnPreProcess(ref System.Windows.Forms.Message msg){
			if(this.PreProcess==null)return false;
			System.Delegate[] dels=this.PreProcess.GetInvocationList();
			for(int i=0;i<dels.Length;i++){
				if(((PreProcessEventHandler)dels[i])(this,ref msg))return true;
			}
			return false;
		}
		/// <summary>
		/// オーバーライドする場合は必ず、this.OnWindowProc(ref m); か base.WndProc(ref m) を実行して下さい。
		/// </summary>
		protected override void WndProc(ref System.Windows.Forms.Message m){
			this.OnWindowProc(ref m);
			base.WndProc(ref m);
		}
		/// <summary>
		/// オーバーライドする場合は必ず、this.OnPreProcess(ref m); か base.PreProcessMessage(ref m) を実行して下さい。
		/// </summary>
		public override bool PreProcessMessage(ref System.Windows.Forms.Message msg){
			return this.OnPreProcess(ref msg)||base.PreProcessMessage (ref msg);
		}

		#region Image Buffer
		//	Image Buffer
		//		一度描いた物を再描画する処理を書くのが面倒な時には、
		//		描いた物を覚えておいて、再描画が必要な時に自動的に再描画してくれる仕組みがあった方が良い。
		//		(多少メモリを使用するが)
		//		この仕組みを Image Buffer と呼ぶ事にする。(意味的には間違っているかも知れないが)
		//***********************************************************
		//	<field>		flgimgbuf	ImageBuffer を利用するかどうか
		//
		private bool flgimgbuf=false;
		public bool EnableImgBuf{
			get{return this.flgimgbuf;}
			set{
				this.flgimgbuf=value;
				if(value/*&&this.imgbuf!=null*/)this.newBufferImage();
			}
		}
		//***********************************************************
		//	<field>		graphbuf	描画をする為の物
		private System.Drawing.Graphics graphbuf;
		public System.Drawing.Graphics GraphicsB{
			get{return this.graphbuf;}
		}
		/// <summary>
		/// Buffer に対する System.Drawing.Graphics を返します。
		/// Graphics に対して変更や設定を行う場合には、Graphics プロパティではなく、こちらを利用して下さい。
		/// </summary>
		public System.Drawing.Graphics CreateGraphicsBuf(){
			return System.Drawing.Graphics.FromImage(this.imgbuf);
		}
		//***********************************************************
		//	<field>		imgbuf		一度描画した物を覚えておく所
		//
		private System.Drawing.Bitmap imgbuf;
		private void newBufferImage(){
			System.Drawing.Bitmap newimg=new System.Drawing.Bitmap(this.Size.Width,this.Size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			System.Drawing.Graphics g=System.Drawing.Graphics.FromImage(newimg);
			g.Clear(this.BackColor);
			if(this.imgbuf!=null){
				g.DrawImageUnscaled(this.imgbuf,0,0);
				this.imgbuf.Dispose();
			}
			if(this.graphbuf!=null)this.graphbuf.Dispose();
			this.imgbuf=newimg;
			this.graphbuf=g;
			this.OnBufferImageRenew();
		}
		public event System.EventHandler BufferImageRenew;
		protected virtual void OnBufferImageRenew(){
			if(this.BufferImageRenew==null)return;
			this.BufferImageRenew(this,new System.EventArgs());
		}
		//***********************************************************
		/// <summary>
		/// 表示を更新する
		/// </summary>
		public void RefreshBuf(){
			System.Drawing.Graphics g=this.CreateGraphics();
			g.DrawImage(this.imgbuf,0,0);
		}
		//***********************************************************
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e){
			base.OnPaint(e);
			//e.Graphics は始めから描画領域を Clip してある。
			if(this.flgimgbuf){
				e.Graphics.Clear(this.BackColor);
				e.Graphics.DrawImage(this.imgbuf,0,0);
			}
		}
		protected override void OnResize(System.EventArgs e){
			if(this.flgimgbuf)this.newBufferImage();
			base.OnResize(e);
		}
		#endregion

	}
	#endregion
}