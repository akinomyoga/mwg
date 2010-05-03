/*
参考:	http://tsuge.astgate.biz/witchgarden/?C%23%20Tips%2fWitchPaper%a4%c7%bc%c2%c1%f5%a4%b7%a4%bfIME%c0%a9%b8%e6
		http://www.kumei.ne.jp/c_lang/sdk3/sdk_278.htm
		http://www.kumei.ne.jp/c_lang/sdk3/sdk_279.htm
		http://www.kumei.ne.jp/c_lang/sdk3/sdk_280.htm
		http://www.kumei.ne.jp/c_lang/sdk3/sdk_281.htm
		http://www.kumei.ne.jp/c_lang/sdk3/sdk_282.htm
		http://www.kumei.ne.jp/c_lang/sdk3/sdk_283.htm
		http://msdn.microsoft.com/library/ja/jpintl/html/Toppage_IME.asp?frame=true
*/
using Interop=System.Runtime.InteropServices;
namespace mwg.Windows{
	/// <summary>
	/// IME を扱う為のクラス
	/// </summary>
	public class IME:System.IDisposable{
		bool available;
		mwg.Windows.ControlA ctrl;
		System.IntPtr hWnd;
		System.IntPtr hImc;
		public System.IntPtr hIMC{
			get{
				if(!this.available)throw new System.ObjectDisposedException("mwg.Windows.IME","このオブジェクトは既に破棄されています。");
				return this.hImc;
			}
		}
		System.Drawing.Point position;

		//***********************************************************
		//			インスタンスの作成
		//-----------------------------------------------------------
		public IME(mwg.Windows.ControlA ctrl){
			this.ctrl=ctrl;
			this.hWnd=ctrl.Handle;
			this.hImc=IME.ImmGetContext(hWnd);
			if(this.hImc==System.IntPtr.Zero){
				//throw new System.Exception("mwg.WIndows.IME:インスタンス初期化に失敗しました");
				this.available=false;
				return;
			}
			this.position=new System.Drawing.Point(0,0);
			this.available=true;
			this.cmpForm=new COMPOSITIONFORM();
			this.cmpForm.dwStyle=CFS.POINT;
			this.cmpForm.ptCurrentPos.X=0;
			this.cmpForm.ptCurrentPos.Y=0;
			ctrl.WindowProc+=new mwg.Windows.ControlA.WndProcEvent(ctrl_WindowProc);
		}
		public void Dispose(){
			if(!this.available)return;
			this.ctrl.WindowProc-=new mwg.Windows.ControlA.WndProcEvent(ctrl_WindowProc);
			this.available=false;
			this.hImc=System.IntPtr.Zero;
			this.hWnd=System.IntPtr.Zero;
			IME.ImmReleaseContext(this.hWnd,this.hImc);
		}
		~IME(){if(this.available)this.Dispose();}
 		[Interop.DllImport("imm32.dll")]
		public static extern System.IntPtr ImmGetContext(System.IntPtr hWnd);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmReleaseContext(System.IntPtr hWnd,System.IntPtr hIMC);

		//***********************************************************
		//		編集文字列
		//-----------------------------------------------------------
		#region Composition
		//***********************************************************
		//		<method> GetCompositionString
		//-----------------------------------------------------------
		/// <summary>
		/// IME の文字列を取得します。(Shift_JIS 文字の読み取りしかできません)
		/// </summary>
		/// <param name="dwIndex">どの文字列を取得するかを mwg.Windows.IME+GCS で指定します。</param>
		/// <returns>指定の文字列を返します</returns>
		public string GetCompositionString(IME.GCS dwIndex){
			string str="";
			try{
				System.IntPtr hImc0=this.hIMC;
				int len=IME.ImmGetCompositionStringB(hImc0,(uint)dwIndex,null,0);
				byte[] lpBuf=new byte[len+1];
				IME.ImmGetCompositionStringB(hImc0,(uint)dwIndex,lpBuf,(uint)len+1);
				// Default: shift-jis: 中国語読み取り不能
				// Unicode: gb2312: もっと不可能
				str=System.Text.Encoding.Default.GetString(lpBuf,0,len);
				//何故かごみが入るので除去
				if(str.Length==2&&(int)str.ToCharArray(1,1)[0]==0xf8f3){
					str=str.Substring(0,1);
				}
			}catch(System.Exception e){throw e;}
			return str;
		}
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCompositionString(System.IntPtr hIMC, uint dwIndex, System.Text.StringBuilder lpBuf, uint dwBufLen);
		[Interop.DllImport("imm32.dll",EntryPoint="ImmGetCompositionString")]
		private static extern int ImmGetCompositionStringB(System.IntPtr hIMC, uint dwIndex,[Interop.Out()]byte[] lpBuf, uint dwBufLen);
		//***********************************************************
		//		<property>	CompositionFont
		//-----------------------------------------------------------
		public mwg.Windows.LOGFONT LogFont{
			get{
				try{
					mwg.Windows.LOGFONT lplf=new LOGFONT();
					IME.ImmGetCompositionFont(this.hIMC,ref lplf);
					return lplf;
				}catch(System.Exception e){throw e;}
			}
			set{
				try{
					ImmSetCompositionFont(this.hIMC,ref value);
				}catch(System.Exception e){throw e;}
			}		
		}
		public System.Drawing.Font Font{
			get{return System.Drawing.Font.FromLogFont(this.LogFont);}
			set{
				System.IntPtr hImc=this.hIMC;
				object lf2=new LOGFONT();
				value.ToLogFont(lf2);
				LOGFONT lf=(LOGFONT)lf2;
				byte[] bytes=System.Text.Encoding.Default.GetBytes(value.Name);
				//lf.lfFaceName=new byte[lf.lfFaceName.Length];// 0 Clear 意味あるのか不明
				System.Array.Copy(bytes,lf.lfFaceName,System.Math.Min(bytes.Length,lf.lfFaceName.Length));
				this.LogFont=lf;
			}
		}
		//TODO: Font LOGFONT プロパティ
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetCompositionFont(System.IntPtr hIMC, ref LOGFONT lplf);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCompositionFont(System.IntPtr hIMC, ref LOGFONT lplf);
		//***********************************************************
		//		CompositionWindow COMPOSITIONFORM
		//			:IME の位置,範囲などの情報
		//-----------------------------------------------------------
		private COMPOSITIONFORM cmpForm;
		public void SetPosition(int x,int y){
			try{
				this.cmpForm.ptCurrentPos.X=x;
				this.cmpForm.ptCurrentPos.Y=y;
				ImmSetCompositionWindow(this.hIMC,ref this.cmpForm);
			}catch(System.Exception e){throw e;}
		}
		public int CompositionX{
			get{return this.cmpForm.ptCurrentPos.X;}
			set{
				this.cmpForm.ptCurrentPos.X=value;
				try{ImmSetCompositionWindow(this.hIMC,ref this.cmpForm);}catch(System.Exception e){throw e;}
			}
		}
		public int CompositionY{
			get{return this.cmpForm.ptCurrentPos.Y;}
			set{
				this.cmpForm.ptCurrentPos.Y=value;
				try{ImmSetCompositionWindow(this.hIMC,ref this.cmpForm);}catch(System.Exception e){throw e;}
			}
		}
		//[System.ComponentModel.NotifyParentProperty(true)]
		public System.Drawing.PointF CompositionPos{
			get{
				return new System.Drawing.PointF(
					(float)this.cmpForm.ptCurrentPos.X,
					(float)this.cmpForm.ptCurrentPos.Y
				);
			}
			set{
				this.cmpForm.ptCurrentPos.X=(int)value.X;
				this.cmpForm.ptCurrentPos.Y=(int)value.Y;
				try{ImmSetCompositionWindow(this.hIMC,ref this.cmpForm);}catch(System.Exception e){throw e;}
			}
		}

		public IME.COMPOSITIONFORM CompositionWindow{
			get{
				COMPOSITIONFORM compForm=new COMPOSITIONFORM();
				IME.ImmGetCompositionWindow(this.hIMC,ref compForm);
				return compForm;
			}
			set{
				IME.ImmSetCompositionWindow(this.hIMC,ref value);
			}
		}
		[Interop.DllImport("imm32.dll")]
		public static extern bool ImmSetCompositionWindow(System.IntPtr hIMC,ref COMPOSITIONFORM lpCompForm);
		[Interop.DllImport("imm32.dll")]
		public static extern bool ImmGetCompositionWindow(System.IntPtr hIMC,ref COMPOSITIONFORM lpCompForm);
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct COMPOSITIONFORM{
			public CFS dwStyle;
			public POINTAPI ptCurrentPos;
			public RECT rcArea;
		}
		//***********************************************************
		//		<enum>	GCS
		//-----------------------------------------------------------
		/// <summary>
		/// WM_IME_COMPOSITION の lParam として渡された場合、どの情報が更新されたかを示します。
		/// ImmGetCompositionString の 引数として渡す場合、どの情報を取得するか指定します。
		/// </summary>
		[System.Flags()]public enum GCS{
			/// <summary>
			/// 現在の編集文字列の読み
			/// </summary>
			COMPREADSTR=0x0001,
			/// <summary>
			/// 現在の編集文字列の読みの属性
			/// </summary>
			COMPREADATTR=0x0002,
			/// <summary>
			/// 現在の編集文字列の文節情報
			/// </summary>
			COMPREADCLAUSE=0x0004,
			/// <summary>
			/// 現在の編集文字列
			/// </summary>
			COMPSTR=0x0008,
			/// <summary>
			/// 編集文字列の属性
			/// </summary>
			COMPATTR=0x0010,
			/// <summary>
			/// 編集文字列の文節情報
			/// </summary>
			COMPCLAUSE=0x0020,
			/// <summary>
			/// 編集文字列中ののカーソルの位置
			/// </summary>
			CURSORPOS=0x0080,
			/// <summary>
			/// 編集文字列が変化した際の差分開始位置?
			/// </summary>
			DELTASTART=0x0100,
			/// <summary>
			/// 確定文字列の読み
			/// </summary>
			RESULTREADSTR=0x0200,
			/// <summary>
			/// 確定文字列の読みの文節情報
			/// </summary>
			RESULTREADCLAUSE=0x0400,
			/// <summary>
			/// 確定文字列
			/// </summary>
			RESULTSTR=0x0800,
			/// <summary>
			/// 確定文字列の文節情報
			/// </summary>
			RESULTCLAUSE=0x1000
		}
		#endregion

		//***********************************************************
		//		イベント
		//-----------------------------------------------------------
		#region Events
		//***********************************************************
		//		message 捕捉
		//-----------------------------------------------------------
		private void ctrl_WindowProc(object sender,ref System.Windows.Forms.Message m){
			if(m.HWnd!=this.hWnd)return;
			int wp=(int)m.WParam;
			switch((Windows.WM)m.Msg){
				case Windows.WM.IME_STARTCOMPOSITION:
					this.isComposition=true;
					this.OnEvent("StartComposition");
					break;
				case Windows.WM.IME_COMPOSITION:
					this.OnComposition((int)m.LParam);
					break;
				case Windows.WM.IME_ENDCOMPOSITION:
					this.OnEvent("EndComposition");
					this.isComposition=false;
					break;
				case Windows.WM.IME_CHAR:
					this.OnChar(((char)m.WParam).ToString(),true,true);
					break;
				case Windows.WM.CHAR:
					bool open=this.OpenStatus;
					if(open&&(int)m.WParam>=32)break;//制御以外は×
					this.OnChar(((char)m.WParam).ToString(),open,false);
					break;
				case Windows.WM.SYSCHAR:
					this.OnCharAlt(((char)m.WParam).ToString(),this.OpenStatus,false);
					break;
				case Windows.WM.IME_NOTIFY:
					IME.IMN imn=(IME.IMN)(int)m.WParam;
					switch(imn){
						case IMN.OPENCANDIDATE:this.OnCandidateEvent("OpenCandidate",(int)m.LParam);break;
						case IMN.CHANGECANDIDATE:this.OnCandidateEvent("ChangeCandidate",(int)m.LParam);break;
						case IMN.CLOSECANDIDATE:this.OnCloseCandidate((int)m.LParam);break;
						case IMN.OPENSTATUSWINDOW:this.OnConversionStatusEvent("OpenStatusWindow");break;
						case IMN.SETCONVERSIONMODE:this.OnConversionStatusEvent("SetConversionMode");break;
						case IMN.SETSENTENCEMODE:this.OnConversionStatusEvent("SetSentenceMode");break;
						case IMN.CLOSESTATUSWINDOW:this.OnEvent("CloseStatusWindow");break;
						case IMN.SETOPENSTATUS:this.OnBoolEvent("SetOpenStatus",this.OpenStatus);break;
					}
					break;
			}
		}
		//入力と Message の表
		//				│OpenStatus	│WM_CHAR		│WM_IME_CHAR	│WM_COMPOSITION(LParam&RESULTSTR)
		//IMEキーボード	│On			│○(昔は△?)	│○			│○
		//IME選択		│On			│○(昔は△?)	│○			│○	
		//IME制御		│On			│○			│×			│×
		//直接入力		│Off			│○			│×			│×
		//
		//※IME選択のときは、表示がOffでも実際はOn
		//※this.isComposition は WM_COMPOSITION の時だけ true

		private bool isComposition=false;
		public bool IsComposition{get{return this.isComposition;}}
		//***********************************************************
		//		event delegate 宣言
		//-----------------------------------------------------------
		/// <summary>
		/// 編集文字列に関する event 処理関数
		/// </summary>
		public delegate void CompositionEventHandler(object sender,CompositionEventArgs e);
		public class CompositionEventArgs:System.EventArgs{
			private string text;
			private bool imeOn;
			private bool composition;
			private bool alt;
			public string String{get{return this.text;}}
			/// <summary>
			/// 入力された時の IME の状態
			/// </summary>
			public bool ImeOn{get{return this.imeOn;}}
			/// <summary>
			/// 入力された時に Alt が押されていたか否か
			/// </summary>
			public bool Alt{get{return this.alt;}}
			/// <summary>
			/// 入力されたのは編集文字列からであったか
			/// </summary>
			public bool IsComposition{get{return this.composition;}}
			internal CompositionEventArgs(string t,bool imeOn,bool comp){
				this.text=t;
				this.imeOn=imeOn;
				this.composition=comp;
				this.alt=false;
			}
			internal CompositionEventArgs(string t,bool imeOn,bool comp,bool alt){
				this.text=t;
				this.imeOn=imeOn;
				this.composition=comp;
				this.alt=alt;
			}
		}
		/// <summary>
		/// 変換候補一覧に関する event の処理関数
		/// </summary>
		public delegate void CandidateEventHandler(object sender,IME.CandidateEventArgs e);
		public class CandidateEventArgs:System.EventArgs{
			public CandidateList List;
			private int listflag;
			public bool listFlag(int index){
				return (this.listflag>>index&0x1)!=0;
			}
			public CandidateEventArgs(CandidateList cdl,int listflag){
				this.List=cdl;
				this.listflag=listflag;
			}
		}
		/// <summary>
		/// 変換候補一覧を閉じる時の event の処理関数
		/// </summary>
		public delegate void CloseCandidateEventHandler(object sender,IME.CloseCandidateEventArgs e);
		public class CloseCandidateEventArgs:System.EventArgs{
			private int listflag;
			public bool listFlag(int index){
				return (this.listflag>>index&0x1)!=0;
			}
			public CloseCandidateEventArgs(int listflag){
				this.listflag=listflag;
			}
		}
		/// <summary>
		/// 変換方法に関する event の処理関数
		/// </summary>
		public delegate void ConversionStatusEH(object sender,IME.ConversionStatusEA e);
		public class ConversionStatusEA:System.EventArgs{
			public IME.CMODE cmode;
			public IME.SMODE smode;
			public ConversionStatusEA(IME.CMODE cmode,IME.SMODE smode){
				this.cmode=cmode;
				this.smode=smode;
			}
		}
		//***********************************************************
		//		event 宣言
		//-----------------------------------------------------------
		protected System.ComponentModel.EventHandlerList _events=new System.ComponentModel.EventHandlerList();
		/// <summary>
		/// 入力文字列の編集を開始した時に発生します。
		/// </summary>
		public event System.EventHandler StartComposition{
			add{this._events.AddHandler("StartComposition",value);}
			remove{this._events.RemoveHandler("StartComposition",value);}
		}
		/// <summary>
		/// 入力文字列の編集をしている際に発生します。
		/// </summary>
		public event CompositionEventHandler Composition;
		/// <summary>
		/// 入力文字列の編集を終了した時に発生します。
		/// </summary>
		public event System.EventHandler EndComposition{
			add{this._events.AddHandler("EndComposition",value);}
			remove{this._events.RemoveHandler("EndComposition",value);}
		}
		/// <summary>
		/// 文字が入力された際に発生します。
		/// </summary>
		public event CompositionEventHandler Char;
		/// <summary>
		/// 変換候補一覧 window が開かれた時に発生します
		/// </summary>
		public event CandidateEventHandler OpenCandidate{
			add{this._events.AddHandler("OpenCandidate",value);}
			remove{this._events.RemoveHandler("OpenCandidate",value);}
		}
		/// <summary>
		/// 変換候補一覧が変更された時に発生します
		/// </summary>
		public event CandidateEventHandler ChangeCandidate{
			add{this._events.AddHandler("ChangeCandidate",value);}
			remove{this._events.RemoveHandler("ChangeCandidate",value);}
		}
		/// <summary>
		/// 変換候補一覧 window が閉じられた時に発生します
		/// </summary>
		public event CloseCandidateEventHandler CloseCandidate{
			add{this._events.AddHandler("CloseCandidate",value);}
			remove{this._events.RemoveHandler("CloseCandidate",value);}
		}
		/// <summary>
		/// 状態 window が開かれた時に発生します
		/// </summary>
		public event ConversionStatusEH OpenStatusWindow{
			add{this._events.AddHandler("OpenStatusWindow",value);}
			remove{this._events.RemoveHandler("OpenStatusWindow",value);}
		}
		/// <summary>
		/// 入力モード:conversion mode が設定された時に発生します
		/// </summary>
		public event ConversionStatusEH ConversionModeChanged{
			add{this._events.AddHandler("SetConversionMode",value);}
			remove{this._events.RemoveHandler("SetConversionMode",value);}
		}
		/// <summary>
		/// 変換モード:sentence mode が設定された時に発生します
		/// </summary>
		public event ConversionStatusEH SentenceModeChanged{
			add{this._events.AddHandler("SetSentenceMode",value);}
			remove{this._events.RemoveHandler("SetSentenceMode",value);}
		}
		/// <summary>
		/// 状態 window が閉じられた時に発生します
		/// </summary>
		public event System.EventHandler CloseStatusWindow{
			add{this._events.AddHandler("CloseStatusWindow",value);}
			remove{this._events.RemoveHandler("CloseStatusWindow",value);}
		}
		/// <summary>
		/// IME が on/off になった時に発生します。
		/// e.Value は IME の状態を表します(on の時 true)。
		/// </summary>
		public event mwg.BoolEventHandler OpenStatusChanged{
			add{this._events.AddHandler("SetOpenStatus",value);}
			remove{this._events.RemoveHandler("SetOpenStatus",value);}
		}
		//***********************************************************
		//		event 誘起
		//-----------------------------------------------------------
		//--以下 一般Event
		protected void OnCandidateEvent(string eventName,int listflag){
			CandidateEventHandler ceh=this._events[eventName] as CandidateEventHandler;
			if(ceh!=null)ceh(this,new CandidateEventArgs(this.GetCandidateList(),listflag));
		}
		protected void OnEvent(string eventName){
			System.EventHandler eh=this._events[eventName] as System.EventHandler;
			if(eh!=null)eh(this,new System.EventArgs());
		}
		protected void OnBoolEvent(string eventName,bool val){
			mwg.BoolEventHandler beh=this._events[eventName] as mwg.BoolEventHandler;
			if(beh!=null)beh(this,new mwg.BoolEventArgs(val));
		}
		protected void OnConversionStatusEvent(string eventName){
			ConversionStatusEH cseh=this._events[eventName] as IME.ConversionStatusEH;
			if(cseh!=null){
				IME.CMODE cmode=(IME.CMODE)0;
				IME.SMODE smode=(IME.SMODE)0;
				IME.ImmGetConversionStatus(this.hIMC,cmode,smode);
				cseh(this,new ConversionStatusEA(cmode,smode));
			}
		}
		//--以下 特異的 event
		protected virtual void OnComposition(int lParam){
			if(this.Composition==null)return;
			if((lParam&(int)IME.GCS.RESULTSTR)==0)return;
			CompositionEventArgs e=new CompositionEventArgs(this.GetCompositionString(IME.GCS.RESULTSTR),true,true);
			this.Composition(this,e);
		}
		protected virtual void OnChar(string c){
			if(this.Char==null)return;
			CompositionEventArgs e=new CompositionEventArgs(c,this.OpenStatus,this.isComposition);
			this.Char(this,e);
		}
		/// <summary>
		/// 既に、this.OpenStatus を取得している場合はこちら
		/// </summary>
		protected virtual void OnChar(string c,bool open,bool composition){
			if(this.Char==null)return;
			CompositionEventArgs e=new CompositionEventArgs(c,open,composition);
			this.Char(this,e);
		}
		protected virtual void OnCharAlt(string c,bool open,bool composition){
			if(this.Char==null)return;
			CompositionEventArgs e=new CompositionEventArgs(c,open,composition,true);
			this.Char(this,e);
		}
		protected void OnCloseCandidate(int listflag){
			CloseCandidateEventHandler cceh=this._events["CloseCandidate"] as CloseCandidateEventHandler;
			if(cceh!=null)cceh(this,new CloseCandidateEventArgs(listflag));
		}
		#endregion

		//***********************************************************
		//		<property>	hKL		キーボードレイアウト
		//-----------------------------------------------------------
		public System.IntPtr hKL{get{return IME.GetKeyboardLayout(0);}}
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr GetKeyboardLayout(int idThread);
		//***********************************************************
		//		<>	
		//-----------------------------------------------------------
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmIsIME(System.IntPtr hKL);
		//***********************************************************
		//		<property>	Description		IMEの名前
		//-----------------------------------------------------------
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetDescription(System.IntPtr hKL,System.Text.StringBuilder lpszDescription,uint uBufLen);
		/// <summary>
		/// 現在の IME の名前(亦は説明)を取得します。
		/// </summary>
		public string Description{
			get{
				System.IntPtr hKL=this.hKL;
				int l=IME.ImmGetDescription(hKL,null,0);
				System.Text.StringBuilder buf=new System.Text.StringBuilder(l);
				IME.ImmGetDescription(hKL,buf,(uint)l);
				return buf.ToString();
			}
		}
		/*private System.Text.Encoding IMEEncoding(string imeDesc){
			switch(imeDesc){
				case "微??音?入法 3.0":
					return System.Text.Encoding.GetEncoding("gb2312");
				case "ATOK16":
				default:
					return System.Text.Encoding.GetEncoding("shift-jis");
			}
			if(imeDesc.GetHashCode)
		}//*/

#if debug
		private byte[] GetDescriptionB(){
			System.IntPtr hKL=this.hKL;
			int l=IME.ImmGetDescriptionB(hKL,null,0);
			byte[] buf=new byte[l];
			IME.ImmGetDescriptionB(hKL,buf,(uint)l);
			while(buf[l-1]==0)l--;
			if(buf.Length==l)return buf;
			byte[] buf2=new byte[l];
			System.Array.Copy(buf,buf2,l);
			return buf2;
		}
		[Interop.DllImport("imm32.dll",EntryPoint="ImmGetDescription")]
		private static extern int ImmGetDescriptionB(System.IntPtr hKL,[Interop.Out()]byte[] lpszDescription,uint uBufLen);
#endif

		//***********************************************************
		//		<property> OpenStatus
		//-----------------------------------------------------------
		public bool OpenStatus{
			get{return 0!=IME.ImmGetOpenStatus(this.hImc);}
			set{ImmSetOpenStatus(this.hIMC,value);}
		}
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetOpenStatus(System.IntPtr hIMC,
			[Interop.MarshalAs(Interop.UnmanagedType.Bool)]
			bool fOpen
			);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetOpenStatus(System.IntPtr hIMC);
		//***********************************************************
		//		TODO:<property>	ConversionStatus
		//-----------------------------------------------------------
		/// <summary>
		/// 入力モード conversion mode を表します
		/// </summary>
		[System.Flags()]public enum CMODE:uint{
			ALPHANUMERIC=0x0000,
			NATIVE=0x0001,		CHINESE=0x0001,		HANGUL=0x0001,		JAPANESE=0x0001,
			[System.Obsolete("HANGUL を使用して下さい")]
			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
			HANGEUL=0x0001,
			/// <summary>
			/// 片仮名。only effect under IME_CMODE_NATIVE
			/// </summary>
			KATAKANA=0x0002,
			LANGUAGE=0x0003,
			/// <summary>
			/// 全角
			/// </summary>
			FULLSHAPE=0x0008,
			/// <summary>
			/// ローマ字変換
			/// </summary>
			ROMAN=0x0010,
			CHARCODE=0x0020,
			HANJACONVERT=0x0040,
			/// <summary>
			/// ソフトキーボード
			/// </summary>
			SOFTKBD=0x0080,
			/// <summary>
			/// 無変換
			/// </summary>
			NOCONVERSION=0x0100,
			EUDC=0x0200,
			SYMBOL=0x0400,
			FIXED=0x0800,
			RESERVED=0xF0000000
		}
		/// <summary>
		/// 変換モード sentence mode を表します
		/// </summary>
		[System.Flags()]public enum SMODE:uint{
			NONE=0x0000,
			PLAURALCLAUSE=0x0001,
			SINGLECONVERT=0x0002,
			AUTOMATIC=0x0004,
			PHRASEPREDICT=0x0008,
			CONVERSATION=0x0010,
			RESERVED=0xF000
		}
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetConversionStatus(System.IntPtr hIMC,IME.CMODE lpfdwConversion,IME.SMODE lpfdwSentence);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetConversionStatus(System.IntPtr hIMC,IME.CMODE lpfdwConversion,IME.SMODE lpfdwSentence);

		//***********************************************************
		//		<method> GetCandidateList
		//-----------------------------------------------------------
		private IME.CandidateList GetCandidateList(){
			int dwSize=IME.ImmGetCandidateList(this.hIMC,0,(byte[])null,0);
			if(dwSize==0)return new CandidateList();
			byte[] lpcdl=new byte[dwSize];
			IME.ImmGetCandidateList(this.hIMC,0,lpcdl,dwSize);
			IME.CandidateList r=new CandidateList(lpcdl);
			return r;
		}

		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct CANDIDATELIST{
			int dwSize;
			int dwStyle;
			int dwCount;
			int dwSelection;
			int dwPageStart;
			int dwPageSize;
			[Interop.MarshalAs(Interop.UnmanagedType.ByValArray,SizeConst=1)]
			int[] dwOffset;
			//byte[] 内容;
		}//*/
		public struct CandidateList{
			/// <summary>
			/// 候補のスタイル。候補を抽出した方法
			/// </summary>
			public IME.CAND Style;
			/// <summary>
			/// 現在選択されている候補
			/// </summary>
			public int Selection;
			/// <summary>
			/// 候補ウィンドウに表示されている物で一番初めの候補の index
			/// </summary>
			public int PageStart;
			/// <summary>
			/// 候補ウィンドウに表示される候補の数
			/// </summary>
			public int PageSize;
			private string[] item;
			public CandidateList(byte[] lpcdl){
				int dwSize=0,dwCount=0;
				int[] dwOffset;
				using(System.IO.MemoryStream str=new System.IO.MemoryStream(lpcdl))
				using(System.IO.BinaryReader reader=new System.IO.BinaryReader(str)){
					dwSize=reader.ReadInt32();
					this.Style=(IME.CAND)reader.ReadInt32();
					dwCount=reader.ReadInt32();
					this.Selection=reader.ReadInt32();
					this.PageStart=reader.ReadInt32();
					this.PageSize=reader.ReadInt32();
					dwOffset=new int[dwCount+1];
					for(int i=0;i<dwCount;i++)dwOffset[i]=reader.ReadInt32();
					dwOffset[dwCount]=dwSize;
				}	
				this.item=new string[dwCount];
				//string str0="";
				for(int i=0;i<dwCount;i++){
					//ATOK16 Encoding.GetEncoding("shift-jis")
					this.item[i]=System.Text.Encoding.GetEncoding("shift-jis").GetString(lpcdl,dwOffset[i],dwOffset[i+1]-dwOffset[i]-1);
				}
			}
			public string this[int i]{
				get{
					if(i<0||i>=this.item.Length){
						throw new System.ArgumentOutOfRangeException("i",i,"指定したインデックスの要素はありません");
					}
					return this.item[i];
				}
			}
			public int Count{get{return this.item.Length;}}
		}
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCandidateList(System.IntPtr hIMC,int dwIndex,IME.CANDIDATELIST lpCandList,int dwBufLen);
		[Interop.DllImport("imm32.dll",EntryPoint="ImmGetCandidateList")]
		private static extern int ImmGetCandidateList(System.IntPtr hIMC,int dwIndex,[Interop.Out()]byte[] lpCandList,int dwBufLen);

		//***********************************************************
		//		TODO:<property> CandidateWindow
		//-----------------------------------------------------------
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct CANDIDATEFORM{
			/// <summary>
			/// 候補リスト識別子。0 から 31 までの数値で指定する事。 
			/// </summary>
			int dwIndex;
			/// <summary>
			/// CFS.CANDIDATEPOS, CFS.EXCLUDE を指定
			/// </summary>
			IME.CFS dwStyle;
			POINTAPI ptCurrentPos;
			RECT  rcArea;
			public CANDIDATEFORM(int index,IME.CFS style,System.Drawing.Point currentPos,System.Drawing.Rectangle area){
				this.dwIndex=(0<=index&&index<32)?index:0;
				this.dwStyle=style&(IME.CFS.EXCLUDE|IME.CFS.CANDIDATEPOS);
				this.ptCurrentPos=currentPos;
				this.rcArea=area;
			}
		}

		#region 各種列挙型 WM CFS CAND IMN
		public enum WM{
			STARTCOMPOSITION=0x10D,
			ENDCOMPOSITION=0x10E,
			COMPOSITION=0x10F,
			KEYLAST=0x10F,
			SETCONTEXT=0x281,
			NOTIFY=0x282,
			CONTROL=0x283,
			COMPOSITIONFULL=0x284,
			SELECT=0x285,
			CHAR=0x286,
			KEYDOWN=0x290,
			KEYUP=0x291
		}
		[System.Flags()]
		public enum CFS:uint{
			DEFAULT=0x0,
			RECT=0x1,
			POINT=0x2,
			FORCE_POSITION=0x20,
			CANDIDATEPOS=0x40,
			EXCLUDE=0x80
		}
		/// <summary>
		/// 変換候補のスタイルを示します。
		/// </summary>
		public enum CAND{
			/// <summary>
			/// 不明の候補スタイル
			/// </summary>
			UNKNOWN=0x0000,
			/// <summary>
			/// 候補は同一の読みです
			/// </summary>
			READ=0x0001,
			/// <summary>
			/// 候補は同一の文字コード範囲にあります
			/// </summary>
			CODE=0x0002,
			/// <summary>
			/// 候補は同じ意味です
			/// </summary>
			MEANING=0x0003,
			/// <summary>
			/// 候補は同一の部首を使っています
			/// </summary>
			RADICAL=0x0004,
			/// <summary>
			/// 候補は同一の画数です
			/// </summary>
			STROKE=0x0005
		}
		/// <summary>
		/// WM_IME_NOTIFY の wParam
		/// </summary>
		public enum IMN{
			/// <summary>
			/// ステータスウィンドウを閉じようとしています。lParamは使われません。
			/// </summary>
			CLOSESTATUSWINDOW=0x0001,
			/// <summary>
			/// ステータスウィンドウを作ろうとしています。lParamは使われません。
			/// </summary>
			OPENSTATUSWINDOW=0x0002,
			/// <summary>
			/// IMEが候補ウィンドウの内容を変えようとしたときに送られてきます。
			/// lParamは候補リストフラグです。それぞれのビットがリストに対応しています。ビット０が最初のリストです。
			/// </summary>
			CHANGECANDIDATE=0x0003,
			/// <summary>
			/// 候補ウィンドウを閉じようとしています。lParamは候補リストフラグです。
			/// </summary>
			CLOSECANDIDATE=0x0004,
			/// <summary>
			/// 候補ウィンドウを開こうとしています。lParamは候補リストフラグです。
			/// </summary>
			OPENCANDIDATE=0x0005,
			/// <summary>
			/// 入力文字列モードが変化したときに送られてきます。lParamは使いません。
			/// </summary>
			SETCONVERSIONMODE=0x0006,
			/// <summary>
			/// 変換モードが変化したときに送られてきます。lParamは使いません。
			/// </summary>
			SETSENTENCEMODE=0x0007,
			/// <summary>
			/// IMEのON,OFF状態が変化したときに送られます。lParamは使いません。
			/// </summary>
			SETOPENSTATUS=0x0008,
			/// <summary>
			/// 候補処理が終了して候補ウィンドウを移動しようとしています。
			/// lParamは候補リストフラグです。
			/// </summary>
			SETCANDIDATEPOS=0x0009,
			/// <summary>
			/// 入力コンテキストのフォントが最新化されたときに送られてきます。
			/// lParamは使われません。
			/// </summary>
			SETCOMPOSITIONFONT=0x000A,
			/// <summary>
			/// 編集ウィンドウのスタイルや位置が変化したときに送られてきます。
			/// lParamは使われません。
			/// </summary>
			SETCOMPOSITIONWINDOW=0x000B,
			/// <summary>
			/// ステータスウィンドウの位置が変化したときに送られます。lParamは使いません。
			/// </summary>
			SETSTATUSWINDOWPOS=0x000C,
			/// <summary>
			/// エラーメッセージを表示しようとしています。lParamは使われません。
			/// </summary>
			GUIDELINE=0x000D,
			PRIVATE=0x000E
		}
		#endregion



		//***********************************************************
		//		<method>	NotifyIME
		//-----------------------------------------------------------
		#region NotifyIME
		/// <summary>
		/// ImmNotifyIME の dwAction。
		/// [引用:http://msdn.microsoft.com/library/ja/default.asp?url=/library/ja/jpintl/html/_win32_ImmGetConversionStatus.asp]
		/// </summary>
		public enum NI:int{
			/// <summary>
			/// IME に、候補一覧を開くように指示します。
			/// dwIndex パラメータには、開く一覧のインデックスを指定します。DwValue パラメータには、何も指定しません。
			/// IME は、一覧を開いたらアプリケーションに IMN_OPENCANDIDATE メッセージを送信します。
			/// </summary>
			OPENCANDIDATE=0x0010,
			/// <summary>
			/// IME に、候補一覧を閉じるように指示します。
			/// dwIndex パラメータには、閉じる一覧のインデックスを指定し、dwValue パラメータには何も指定しません。
			/// IME は、一覧を閉じたらアプリケーションに IMN_CLOSECANDIDATE メッセージを送信します。
			/// </summary>
			CLOSECANDIDATE=0x0011,
			/// <summary>
			/// 変換候補の 1 つを選択します。dwIndex パラメータには、対象とする候補一覧のインデックスを指定します。
			/// dwValue パラメータには、その候補一覧での候補文字列のインデックスを指定します。
			/// </summary>
			SELECTCANDIDATESTR=0x0012,
			/// <summary>
			/// 選択されている候補一覧を変更します。
			/// dwIndex パラメータには、選択する候補一覧のインデックスを指定し、dwValue パラメータには何も指定しません。
			/// </summary>
			CHANGECANDIDATELIST=0x0013,
			FINALIZECONVERSIONRESULT=0x0014,
			/// <summary>
			/// IME に、変換文字列に対する処理を実行するように指示します。dwValue パラメータには何も指定しません。
			/// dwIndex パラメータには、enum IME.CPS のいずれかを指定します。
			/// </summary>
			COMPOSITIONSTR=0x0015,
			/// <summary>
			/// dwIndex パラメータには、変更する候補一覧を指定します。0～3 の範囲内の値を指定しなければなりません。
			/// </summary>
			SETCANDIDATE_PAGESTART=0x0016,
			/// <summary>
			/// dwIndex パラメータには、変更する候補一覧を指定します。0～3 の範囲内の値を指定しなければなりません。
			/// </summary>
			SETCANDIDATE_PAGESIZE=0x0017,
			/// <summary>
			/// IME に、指定したメニューを処理することをアプリケーションに許可するよう指示します。
			/// dwIndex パラメータにはメニューの ID を指定し、
			/// dwValue パラメータにはそのメニュー項目用のアプリケーション定義の値を指定します。
			/// </summary>
			IMEMENUSELECTED=0x0018		
		}
		/// <summary>
		/// dwIndex for ImmNotifyIME/NI_COMPOSITIONSTR
		/// </summary>
		public enum CPS:int{
			/// <summary>
			/// 現在の変換文字列を変換結果として確定します。
			/// </summary>
			COMPLETE=0x0001,
			/// <summary>
			/// 変換文字列を変換します。
			/// </summary>
			CONVERT=0x0002,
			/// <summary>
			/// 現在の変換文字列を取り消し、未変換文字列に戻します。
			/// </summary>
			REVERT=0x0003,
			/// <summary>
			/// 変換文字列を消去し、状態を変換文字列なしに設定します。
			/// </summary>
			CANCEL=0x0004
		}
		public void Notify(IME.NI action,int index,int val){
			IME.ImmNotifyIME(this.hImc,action,index,val);
		}
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmNotifyIME(System.IntPtr hIMC,IME.NI dwAction,int dwIndex,int dwValue);
		#endregion

		//***********************************************************
		//		<>	configureime
		//-----------------------------------------------------------
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmConfigureIME(System.IntPtr hKL,System.IntPtr hWnd,IME.CONFIG dwMode,ref IME.REGISTERWORD lpData);
		/// <summary>
		/// IME の登録単語の情報を保持します。
		/// </summary>
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct REGISTERWORD{
			/// <summary>
			/// 単語の読み
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpReading;
			/// <summary>
			/// 単語の表記
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpWord;
			/// <summary>
			/// REGISTERWORD の新しいインスタンスを作成します
			/// </summary>
			/// <param name="reading">単語の読みを指定します</param>
			/// <param name="word">単語の表記を指定します</param>
			public REGISTERWORD(string reading,string word){
				this.lpReading=reading;
				this.lpWord=word;
			}
		}
		/// <summary>
		/// ImmConfigureIME() の Dialog mode を表します
		/// </summary>
		public enum CONFIG{
			/// <summary>
			/// プロパティダイアログボックスを表示します。 
			/// </summary>
			GENERAL=1,
			/// <summary>
			/// 単語登録用のダイアログボックスを表示します。
			/// </summary>
			REGISTERWORD=2,
			/// <summary>
			/// 辞書選択用のダイアログボックスを表示します。
			/// </summary>
			SELECTDICTIONARY=3
		}
		//TODO:対応していない物
		//ImmAssociateContext
		//ImmAssociateContextEx
		//ImmCreateContxet
		//ImmDestroyContext
		//ImmDisableIME
		//ImmEnumInputContext
		//ImmEnumRegisterWord
		//ImmEscape c.f. http://msdn.microsoft.com/library/default.asp?url=/library/en-us/intl/ime_396b.asp
		//ImmGetCandidateWindow
		//ImmGetCandidateListCount
		//ImmGetConversionList
		//ImmGetDefaultIMEWnd
		//ImmGetGuideLine
		//ImmGetIMEFileName
		//ImmGetImeMenuItems
		//ImmGetProperty
		//ImmGetRegisterWordStyle
		//ImmGetStatusWindowPos
		//ImmGetVirtualKey
		//ImmInstallIME
		//ImmIsUIMessage
		//ImmRegisterWord
		//ImmSetCandidateWindow
		//ImmSetCompositionString
		//ImmSetStatusWindowPos
		//ImmSimulateHotkey
		//ImmUnregisterWord
		//TODO:対応していないメッセージ
		//WM_IME_KEYLAST
		//WM_IME_SETCONTEXT
		//WM_IME_NOTIFY::IMN_SETCANDIDATEPOS
		//WM_IME_NOTIFY::IMN_SETCOMPOSITIONFONT
		//WM_IME_NOTIFY::IMN_SETCOMPOSITIONWINDOW
		//WM_IME_NOTIFY::IMN_SETSTATUSWINDOWPOS
		//WM_IME_NOTIFY::IMN_GUIDELINE
		//WM_IME_CONTROL
		//WM_IME_COMPOSITIONFULL
		//WM_IME_SELECT
		//WM_IME_KEYDOWN
		//WM_IME_KEYUP
	}
}