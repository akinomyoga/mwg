/*
�Q�l:	http://tsuge.astgate.biz/witchgarden/?C%23%20Tips%2fWitchPaper%a4%c7%bc%c2%c1%f5%a4%b7%a4%bfIME%c0%a9%b8%e6
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
	/// IME �������ׂ̃N���X
	/// </summary>
	public class IME:System.IDisposable{
		bool available;
		mwg.Windows.ControlA ctrl;
		System.IntPtr hWnd;
		System.IntPtr hImc;
		public System.IntPtr hIMC{
			get{
				if(!this.available)throw new System.ObjectDisposedException("mwg.Windows.IME","���̃I�u�W�F�N�g�͊��ɔj������Ă��܂��B");
				return this.hImc;
			}
		}
		System.Drawing.Point position;

		//***********************************************************
		//			�C���X�^���X�̍쐬
		//-----------------------------------------------------------
		public IME(mwg.Windows.ControlA ctrl){
			this.ctrl=ctrl;
			this.hWnd=ctrl.Handle;
			this.hImc=IME.ImmGetContext(hWnd);
			if(this.hImc==System.IntPtr.Zero){
				//throw new System.Exception("mwg.WIndows.IME:�C���X�^���X�������Ɏ��s���܂���");
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
		//		�ҏW������
		//-----------------------------------------------------------
		#region Composition
		//***********************************************************
		//		<method> GetCompositionString
		//-----------------------------------------------------------
		/// <summary>
		/// IME �̕�������擾���܂��B(Shift_JIS �����̓ǂݎ�肵���ł��܂���)
		/// </summary>
		/// <param name="dwIndex">�ǂ̕�������擾���邩�� mwg.Windows.IME+GCS �Ŏw�肵�܂��B</param>
		/// <returns>�w��̕������Ԃ��܂�</returns>
		public string GetCompositionString(IME.GCS dwIndex){
			string str="";
			try{
				System.IntPtr hImc0=this.hIMC;
				int len=IME.ImmGetCompositionStringB(hImc0,(uint)dwIndex,null,0);
				byte[] lpBuf=new byte[len+1];
				IME.ImmGetCompositionStringB(hImc0,(uint)dwIndex,lpBuf,(uint)len+1);
				// Default: shift-jis: ������ǂݎ��s�\
				// Unicode: gb2312: �����ƕs�\
				str=System.Text.Encoding.Default.GetString(lpBuf,0,len);
				//���̂����݂�����̂ŏ���
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
				//lf.lfFaceName=new byte[lf.lfFaceName.Length];// 0 Clear �Ӗ�����̂��s��
				System.Array.Copy(bytes,lf.lfFaceName,System.Math.Min(bytes.Length,lf.lfFaceName.Length));
				this.LogFont=lf;
			}
		}
		//TODO: Font LOGFONT �v���p�e�B
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetCompositionFont(System.IntPtr hIMC, ref LOGFONT lplf);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCompositionFont(System.IntPtr hIMC, ref LOGFONT lplf);
		//***********************************************************
		//		CompositionWindow COMPOSITIONFORM
		//			:IME �̈ʒu,�͈͂Ȃǂ̏��
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
		/// WM_IME_COMPOSITION �� lParam �Ƃ��ēn���ꂽ�ꍇ�A�ǂ̏�񂪍X�V���ꂽ���������܂��B
		/// ImmGetCompositionString �� �����Ƃ��ēn���ꍇ�A�ǂ̏����擾���邩�w�肵�܂��B
		/// </summary>
		[System.Flags()]public enum GCS{
			/// <summary>
			/// ���݂̕ҏW������̓ǂ�
			/// </summary>
			COMPREADSTR=0x0001,
			/// <summary>
			/// ���݂̕ҏW������̓ǂ݂̑���
			/// </summary>
			COMPREADATTR=0x0002,
			/// <summary>
			/// ���݂̕ҏW������̕��ߏ��
			/// </summary>
			COMPREADCLAUSE=0x0004,
			/// <summary>
			/// ���݂̕ҏW������
			/// </summary>
			COMPSTR=0x0008,
			/// <summary>
			/// �ҏW������̑���
			/// </summary>
			COMPATTR=0x0010,
			/// <summary>
			/// �ҏW������̕��ߏ��
			/// </summary>
			COMPCLAUSE=0x0020,
			/// <summary>
			/// �ҏW�����񒆂̂̃J�[�\���̈ʒu
			/// </summary>
			CURSORPOS=0x0080,
			/// <summary>
			/// �ҏW�����񂪕ω������ۂ̍����J�n�ʒu?
			/// </summary>
			DELTASTART=0x0100,
			/// <summary>
			/// �m�蕶����̓ǂ�
			/// </summary>
			RESULTREADSTR=0x0200,
			/// <summary>
			/// �m�蕶����̓ǂ݂̕��ߏ��
			/// </summary>
			RESULTREADCLAUSE=0x0400,
			/// <summary>
			/// �m�蕶����
			/// </summary>
			RESULTSTR=0x0800,
			/// <summary>
			/// �m�蕶����̕��ߏ��
			/// </summary>
			RESULTCLAUSE=0x1000
		}
		#endregion

		//***********************************************************
		//		�C�x���g
		//-----------------------------------------------------------
		#region Events
		//***********************************************************
		//		message �ߑ�
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
					if(open&&(int)m.WParam>=32)break;//����ȊO�́~
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
		//���͂� Message �̕\
		//				��OpenStatus	��WM_CHAR		��WM_IME_CHAR	��WM_COMPOSITION(LParam&RESULTSTR)
		//IME�L�[�{�[�h	��On			����(�̂́�?)	����			����
		//IME�I��		��On			����(�̂́�?)	����			����	
		//IME����		��On			����			���~			���~
		//���ړ���		��Off			����			���~			���~
		//
		//��IME�I���̂Ƃ��́A�\����Off�ł����ۂ�On
		//��this.isComposition �� WM_COMPOSITION �̎����� true

		private bool isComposition=false;
		public bool IsComposition{get{return this.isComposition;}}
		//***********************************************************
		//		event delegate �錾
		//-----------------------------------------------------------
		/// <summary>
		/// �ҏW������Ɋւ��� event �����֐�
		/// </summary>
		public delegate void CompositionEventHandler(object sender,CompositionEventArgs e);
		public class CompositionEventArgs:System.EventArgs{
			private string text;
			private bool imeOn;
			private bool composition;
			private bool alt;
			public string String{get{return this.text;}}
			/// <summary>
			/// ���͂��ꂽ���� IME �̏��
			/// </summary>
			public bool ImeOn{get{return this.imeOn;}}
			/// <summary>
			/// ���͂��ꂽ���� Alt ��������Ă������ۂ�
			/// </summary>
			public bool Alt{get{return this.alt;}}
			/// <summary>
			/// ���͂��ꂽ�͕̂ҏW�����񂩂�ł�������
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
		/// �ϊ����ꗗ�Ɋւ��� event �̏����֐�
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
		/// �ϊ����ꗗ����鎞�� event �̏����֐�
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
		/// �ϊ����@�Ɋւ��� event �̏����֐�
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
		//		event �錾
		//-----------------------------------------------------------
		protected System.ComponentModel.EventHandlerList _events=new System.ComponentModel.EventHandlerList();
		/// <summary>
		/// ���͕�����̕ҏW���J�n�������ɔ������܂��B
		/// </summary>
		public event System.EventHandler StartComposition{
			add{this._events.AddHandler("StartComposition",value);}
			remove{this._events.RemoveHandler("StartComposition",value);}
		}
		/// <summary>
		/// ���͕�����̕ҏW�����Ă���ۂɔ������܂��B
		/// </summary>
		public event CompositionEventHandler Composition;
		/// <summary>
		/// ���͕�����̕ҏW���I���������ɔ������܂��B
		/// </summary>
		public event System.EventHandler EndComposition{
			add{this._events.AddHandler("EndComposition",value);}
			remove{this._events.RemoveHandler("EndComposition",value);}
		}
		/// <summary>
		/// ���������͂��ꂽ�ۂɔ������܂��B
		/// </summary>
		public event CompositionEventHandler Char;
		/// <summary>
		/// �ϊ����ꗗ window ���J���ꂽ���ɔ������܂�
		/// </summary>
		public event CandidateEventHandler OpenCandidate{
			add{this._events.AddHandler("OpenCandidate",value);}
			remove{this._events.RemoveHandler("OpenCandidate",value);}
		}
		/// <summary>
		/// �ϊ����ꗗ���ύX���ꂽ���ɔ������܂�
		/// </summary>
		public event CandidateEventHandler ChangeCandidate{
			add{this._events.AddHandler("ChangeCandidate",value);}
			remove{this._events.RemoveHandler("ChangeCandidate",value);}
		}
		/// <summary>
		/// �ϊ����ꗗ window ������ꂽ���ɔ������܂�
		/// </summary>
		public event CloseCandidateEventHandler CloseCandidate{
			add{this._events.AddHandler("CloseCandidate",value);}
			remove{this._events.RemoveHandler("CloseCandidate",value);}
		}
		/// <summary>
		/// ��� window ���J���ꂽ���ɔ������܂�
		/// </summary>
		public event ConversionStatusEH OpenStatusWindow{
			add{this._events.AddHandler("OpenStatusWindow",value);}
			remove{this._events.RemoveHandler("OpenStatusWindow",value);}
		}
		/// <summary>
		/// ���̓��[�h:conversion mode ���ݒ肳�ꂽ���ɔ������܂�
		/// </summary>
		public event ConversionStatusEH ConversionModeChanged{
			add{this._events.AddHandler("SetConversionMode",value);}
			remove{this._events.RemoveHandler("SetConversionMode",value);}
		}
		/// <summary>
		/// �ϊ����[�h:sentence mode ���ݒ肳�ꂽ���ɔ������܂�
		/// </summary>
		public event ConversionStatusEH SentenceModeChanged{
			add{this._events.AddHandler("SetSentenceMode",value);}
			remove{this._events.RemoveHandler("SetSentenceMode",value);}
		}
		/// <summary>
		/// ��� window ������ꂽ���ɔ������܂�
		/// </summary>
		public event System.EventHandler CloseStatusWindow{
			add{this._events.AddHandler("CloseStatusWindow",value);}
			remove{this._events.RemoveHandler("CloseStatusWindow",value);}
		}
		/// <summary>
		/// IME �� on/off �ɂȂ������ɔ������܂��B
		/// e.Value �� IME �̏�Ԃ�\���܂�(on �̎� true)�B
		/// </summary>
		public event mwg.BoolEventHandler OpenStatusChanged{
			add{this._events.AddHandler("SetOpenStatus",value);}
			remove{this._events.RemoveHandler("SetOpenStatus",value);}
		}
		//***********************************************************
		//		event �U�N
		//-----------------------------------------------------------
		//--�ȉ� ���Event
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
		//--�ȉ� ���ٓI event
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
		/// ���ɁAthis.OpenStatus ���擾���Ă���ꍇ�͂�����
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
		//		<property>	hKL		�L�[�{�[�h���C�A�E�g
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
		//		<property>	Description		IME�̖��O
		//-----------------------------------------------------------
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetDescription(System.IntPtr hKL,System.Text.StringBuilder lpszDescription,uint uBufLen);
		/// <summary>
		/// ���݂� IME �̖��O(���͐���)���擾���܂��B
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
				case "��??��?���@ 3.0":
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
		/// ���̓��[�h conversion mode ��\���܂�
		/// </summary>
		[System.Flags()]public enum CMODE:uint{
			ALPHANUMERIC=0x0000,
			NATIVE=0x0001,		CHINESE=0x0001,		HANGUL=0x0001,		JAPANESE=0x0001,
			[System.Obsolete("HANGUL ���g�p���ĉ�����")]
			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
			HANGEUL=0x0001,
			/// <summary>
			/// �Љ����Bonly effect under IME_CMODE_NATIVE
			/// </summary>
			KATAKANA=0x0002,
			LANGUAGE=0x0003,
			/// <summary>
			/// �S�p
			/// </summary>
			FULLSHAPE=0x0008,
			/// <summary>
			/// ���[�}���ϊ�
			/// </summary>
			ROMAN=0x0010,
			CHARCODE=0x0020,
			HANJACONVERT=0x0040,
			/// <summary>
			/// �\�t�g�L�[�{�[�h
			/// </summary>
			SOFTKBD=0x0080,
			/// <summary>
			/// ���ϊ�
			/// </summary>
			NOCONVERSION=0x0100,
			EUDC=0x0200,
			SYMBOL=0x0400,
			FIXED=0x0800,
			RESERVED=0xF0000000
		}
		/// <summary>
		/// �ϊ����[�h sentence mode ��\���܂�
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
			//byte[] ���e;
		}//*/
		public struct CandidateList{
			/// <summary>
			/// ���̃X�^�C���B���𒊏o�������@
			/// </summary>
			public IME.CAND Style;
			/// <summary>
			/// ���ݑI������Ă�����
			/// </summary>
			public int Selection;
			/// <summary>
			/// ���E�B���h�E�ɕ\������Ă��镨�ň�ԏ��߂̌��� index
			/// </summary>
			public int PageStart;
			/// <summary>
			/// ���E�B���h�E�ɕ\���������̐�
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
						throw new System.ArgumentOutOfRangeException("i",i,"�w�肵���C���f�b�N�X�̗v�f�͂���܂���");
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
			/// ��⃊�X�g���ʎq�B0 ���� 31 �܂ł̐��l�Ŏw�肷�鎖�B 
			/// </summary>
			int dwIndex;
			/// <summary>
			/// CFS.CANDIDATEPOS, CFS.EXCLUDE ���w��
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

		#region �e��񋓌^ WM CFS CAND IMN
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
		/// �ϊ����̃X�^�C���������܂��B
		/// </summary>
		public enum CAND{
			/// <summary>
			/// �s���̌��X�^�C��
			/// </summary>
			UNKNOWN=0x0000,
			/// <summary>
			/// ���͓���̓ǂ݂ł�
			/// </summary>
			READ=0x0001,
			/// <summary>
			/// ���͓���̕����R�[�h�͈͂ɂ���܂�
			/// </summary>
			CODE=0x0002,
			/// <summary>
			/// ���͓����Ӗ��ł�
			/// </summary>
			MEANING=0x0003,
			/// <summary>
			/// ���͓���̕�����g���Ă��܂�
			/// </summary>
			RADICAL=0x0004,
			/// <summary>
			/// ���͓���̉搔�ł�
			/// </summary>
			STROKE=0x0005
		}
		/// <summary>
		/// WM_IME_NOTIFY �� wParam
		/// </summary>
		public enum IMN{
			/// <summary>
			/// �X�e�[�^�X�E�B���h�E����悤�Ƃ��Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			CLOSESTATUSWINDOW=0x0001,
			/// <summary>
			/// �X�e�[�^�X�E�B���h�E����낤�Ƃ��Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			OPENSTATUSWINDOW=0x0002,
			/// <summary>
			/// IME�����E�B���h�E�̓��e��ς��悤�Ƃ����Ƃ��ɑ����Ă��܂��B
			/// lParam�͌�⃊�X�g�t���O�ł��B���ꂼ��̃r�b�g�����X�g�ɑΉ����Ă��܂��B�r�b�g�O���ŏ��̃��X�g�ł��B
			/// </summary>
			CHANGECANDIDATE=0x0003,
			/// <summary>
			/// ���E�B���h�E����悤�Ƃ��Ă��܂��BlParam�͌�⃊�X�g�t���O�ł��B
			/// </summary>
			CLOSECANDIDATE=0x0004,
			/// <summary>
			/// ���E�B���h�E���J�����Ƃ��Ă��܂��BlParam�͌�⃊�X�g�t���O�ł��B
			/// </summary>
			OPENCANDIDATE=0x0005,
			/// <summary>
			/// ���͕����񃂁[�h���ω������Ƃ��ɑ����Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			SETCONVERSIONMODE=0x0006,
			/// <summary>
			/// �ϊ����[�h���ω������Ƃ��ɑ����Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			SETSENTENCEMODE=0x0007,
			/// <summary>
			/// IME��ON,OFF��Ԃ��ω������Ƃ��ɑ����܂��BlParam�͎g���܂���B
			/// </summary>
			SETOPENSTATUS=0x0008,
			/// <summary>
			/// ��⏈�����I�����Č��E�B���h�E���ړ����悤�Ƃ��Ă��܂��B
			/// lParam�͌�⃊�X�g�t���O�ł��B
			/// </summary>
			SETCANDIDATEPOS=0x0009,
			/// <summary>
			/// ���̓R���e�L�X�g�̃t�H���g���ŐV�����ꂽ�Ƃ��ɑ����Ă��܂��B
			/// lParam�͎g���܂���B
			/// </summary>
			SETCOMPOSITIONFONT=0x000A,
			/// <summary>
			/// �ҏW�E�B���h�E�̃X�^�C����ʒu���ω������Ƃ��ɑ����Ă��܂��B
			/// lParam�͎g���܂���B
			/// </summary>
			SETCOMPOSITIONWINDOW=0x000B,
			/// <summary>
			/// �X�e�[�^�X�E�B���h�E�̈ʒu���ω������Ƃ��ɑ����܂��BlParam�͎g���܂���B
			/// </summary>
			SETSTATUSWINDOWPOS=0x000C,
			/// <summary>
			/// �G���[���b�Z�[�W��\�����悤�Ƃ��Ă��܂��BlParam�͎g���܂���B
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
		/// ImmNotifyIME �� dwAction�B
		/// [���p:http://msdn.microsoft.com/library/ja/default.asp?url=/library/ja/jpintl/html/_win32_ImmGetConversionStatus.asp]
		/// </summary>
		public enum NI:int{
			/// <summary>
			/// IME �ɁA���ꗗ���J���悤�Ɏw�����܂��B
			/// dwIndex �p�����[�^�ɂ́A�J���ꗗ�̃C���f�b�N�X���w�肵�܂��BDwValue �p�����[�^�ɂ́A�����w�肵�܂���B
			/// IME �́A�ꗗ���J������A�v���P�[�V������ IMN_OPENCANDIDATE ���b�Z�[�W�𑗐M���܂��B
			/// </summary>
			OPENCANDIDATE=0x0010,
			/// <summary>
			/// IME �ɁA���ꗗ�����悤�Ɏw�����܂��B
			/// dwIndex �p�����[�^�ɂ́A����ꗗ�̃C���f�b�N�X���w�肵�AdwValue �p�����[�^�ɂ͉����w�肵�܂���B
			/// IME �́A�ꗗ�������A�v���P�[�V������ IMN_CLOSECANDIDATE ���b�Z�[�W�𑗐M���܂��B
			/// </summary>
			CLOSECANDIDATE=0x0011,
			/// <summary>
			/// �ϊ����� 1 ��I�����܂��BdwIndex �p�����[�^�ɂ́A�ΏۂƂ�����ꗗ�̃C���f�b�N�X���w�肵�܂��B
			/// dwValue �p�����[�^�ɂ́A���̌��ꗗ�ł̌�╶����̃C���f�b�N�X���w�肵�܂��B
			/// </summary>
			SELECTCANDIDATESTR=0x0012,
			/// <summary>
			/// �I������Ă�����ꗗ��ύX���܂��B
			/// dwIndex �p�����[�^�ɂ́A�I��������ꗗ�̃C���f�b�N�X���w�肵�AdwValue �p�����[�^�ɂ͉����w�肵�܂���B
			/// </summary>
			CHANGECANDIDATELIST=0x0013,
			FINALIZECONVERSIONRESULT=0x0014,
			/// <summary>
			/// IME �ɁA�ϊ�������ɑ΂��鏈�������s����悤�Ɏw�����܂��BdwValue �p�����[�^�ɂ͉����w�肵�܂���B
			/// dwIndex �p�����[�^�ɂ́Aenum IME.CPS �̂����ꂩ���w�肵�܂��B
			/// </summary>
			COMPOSITIONSTR=0x0015,
			/// <summary>
			/// dwIndex �p�����[�^�ɂ́A�ύX������ꗗ���w�肵�܂��B0�`3 �͈͓̔��̒l���w�肵�Ȃ���΂Ȃ�܂���B
			/// </summary>
			SETCANDIDATE_PAGESTART=0x0016,
			/// <summary>
			/// dwIndex �p�����[�^�ɂ́A�ύX������ꗗ���w�肵�܂��B0�`3 �͈͓̔��̒l���w�肵�Ȃ���΂Ȃ�܂���B
			/// </summary>
			SETCANDIDATE_PAGESIZE=0x0017,
			/// <summary>
			/// IME �ɁA�w�肵�����j���[���������邱�Ƃ��A�v���P�[�V�����ɋ�����悤�w�����܂��B
			/// dwIndex �p�����[�^�ɂ̓��j���[�� ID ���w�肵�A
			/// dwValue �p�����[�^�ɂ͂��̃��j���[���ڗp�̃A�v���P�[�V������`�̒l���w�肵�܂��B
			/// </summary>
			IMEMENUSELECTED=0x0018		
		}
		/// <summary>
		/// dwIndex for ImmNotifyIME/NI_COMPOSITIONSTR
		/// </summary>
		public enum CPS:int{
			/// <summary>
			/// ���݂̕ϊ��������ϊ����ʂƂ��Ċm�肵�܂��B
			/// </summary>
			COMPLETE=0x0001,
			/// <summary>
			/// �ϊ��������ϊ����܂��B
			/// </summary>
			CONVERT=0x0002,
			/// <summary>
			/// ���݂̕ϊ���������������A���ϊ�������ɖ߂��܂��B
			/// </summary>
			REVERT=0x0003,
			/// <summary>
			/// �ϊ���������������A��Ԃ�ϊ�������Ȃ��ɐݒ肵�܂��B
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
		/// IME �̓o�^�P��̏���ێ����܂��B
		/// </summary>
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct REGISTERWORD{
			/// <summary>
			/// �P��̓ǂ�
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpReading;
			/// <summary>
			/// �P��̕\�L
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpWord;
			/// <summary>
			/// REGISTERWORD �̐V�����C���X�^���X���쐬���܂�
			/// </summary>
			/// <param name="reading">�P��̓ǂ݂��w�肵�܂�</param>
			/// <param name="word">�P��̕\�L���w�肵�܂�</param>
			public REGISTERWORD(string reading,string word){
				this.lpReading=reading;
				this.lpWord=word;
			}
		}
		/// <summary>
		/// ImmConfigureIME() �� Dialog mode ��\���܂�
		/// </summary>
		public enum CONFIG{
			/// <summary>
			/// �v���p�e�B�_�C�A���O�{�b�N�X��\�����܂��B 
			/// </summary>
			GENERAL=1,
			/// <summary>
			/// �P��o�^�p�̃_�C�A���O�{�b�N�X��\�����܂��B
			/// </summary>
			REGISTERWORD=2,
			/// <summary>
			/// �����I��p�̃_�C�A���O�{�b�N�X��\�����܂��B
			/// </summary>
			SELECTDICTIONARY=3
		}
		//TODO:�Ή����Ă��Ȃ���
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
		//TODO:�Ή����Ă��Ȃ����b�Z�[�W
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