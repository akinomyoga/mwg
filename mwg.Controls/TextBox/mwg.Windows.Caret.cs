using Interop=System.Runtime.InteropServices;
namespace mwg.Windows{
	//http://www.ne.jp/asahi/nami/mei/cstips/caretsample.html ��
	public class Caret:System.IDisposable{
		/// <summary>
		/// Caret ��\������Ώۂ̃R���g���[���ւ̎Q��
		/// </summary>
		System.Windows.Forms.Control ctrl;
		private Caret(System.Windows.Forms.Control ctrl){
			this.ctrl=ctrl;
			this.Position=System.Drawing.Point.Empty;
			this.Size=new System.Drawing.Size(1,this.ctrl.Font.Height);
			this.Control.GotFocus+=new System.EventHandler(this.OnGotFocus);
			this.Control.LostFocus+=new System.EventHandler(this.OnLostFocus);

			if(ctrl.Focused)this.OnGotFocus(ctrl,new System.EventArgs());
		}
		/// <summary>
		/// Caret ��\������Ώۂ̃R���g���[�����擾���܂�
		/// </summary>
		public System.Windows.Forms.Control Control{
			get{return this.ctrl;}
		}
		/// <summary>
		/// Caret ���폜���܂��B
		/// </summary>
		public void Dispose(){
			if(ctrl.Focused)this.OnLostFocus(ctrl,new System.EventArgs());
			this.Control.GotFocus-=new System.EventHandler(this.OnGotFocus);
			this.Control.LostFocus-=new System.EventHandler(this.OnLostFocus);
		}

		#region Caret ����/���� +bitmap,size
		private System.Drawing.Bitmap bitmap;
		/// <summary>
		/// Caret �Ƃ��ĕ\������摜���擾���͐ݒ肵�܂��B
		/// </summary>
		public System.Drawing.Bitmap Bitmap{
			get{return this.bitmap;}
			set{this.bitmap=value;}
		}
		System.Drawing.Size  size;
		/// <summary>
		/// Caret �̑傫�����擾���͐ݒ肵�܂��B
		/// </summary>
		public System.Drawing.Size Size{
			get{return this.size;}
			set{
				this.size=value;
				if(this.Visible)this.Refresh();
			}
		}
		/// <summary>
		/// Caret �̕����擾���͐ݒ肵�܂��B
		/// </summary>
		public int Width{
			get{return this.size.Width;}
			set{
				this.size.Width=value;
				if(this.Visible)this.Refresh();
			}
		}
		/// <summary>
		/// Caret �̍������擾���͐ݒ肵�܂��B
		/// </summary>
		public int Height{
			get{return this.size.Height;}
			set{
				this.size.Height=value;
				if(this.Visible)this.Refresh();
			}
		}
		private void create(){
			if(this.bitmap!=null){
				Caret.CreateCaret(this.Control.Handle,this.bitmap.GetHbitmap(),this.Size.Width,this.Size.Height);
			}else{
				Caret.CreateCaret(this.Control.Handle,System.IntPtr.Zero,this.Size.Width,this.Size.Height);
			}
		}
		private void destroy(){
			Caret.DestroyCaret();
		}
		private void Refresh(){
			if(this.Visible){
				this.destroy();
				this.create();
			}
		}
		private void OnGotFocus(object sender,System.EventArgs e){
			this.create();
			Caret.SetCaretPos(this.Position.X,this.Position.Y);
			this.Visible=true;
		}
		private void OnLostFocus(object sender,System.EventArgs e){
			this.Visible=false;
			this.destroy();
		}
		//import
		[Interop.DllImport("user32.dll")]
		public static extern int CreateCaret(System.IntPtr hwnd,System.IntPtr hbm,int cx,int cy);
		[Interop.DllImport("user32.dll")]
		public static extern int DestroyCaret();
		#endregion

		#region Caret �ʒu
		System.Drawing.Point  pos;
		bool flgSavePos=true;
		/// <summary>
		/// Caret �̈ʒu�����̃N���X�C���X�^���X�ŊǗ����邩�ǂ����擾���͐ݒ肵�܂��B
		/// true �ɐݒ肵���ꍇ�A�O������ Caret �̈ʒu�ɕύX�������Ă��A
		/// ���̃N���X�C���X�^���X�̕ێ����� Caret �̈ʒu�͕ύX����܂���B
		/// </summary>
		public bool flgSavePosition{
			get{return this.flgSavePos;}
			set{this.flgSavePos=value;}
		}
		/// <summary>
		/// Caret �̈ʒu���擾���͐ݒ肵�܂��B
		/// </summary>
		public System.Drawing.Point Position{
			get{
				if(!this.flgSavePos){
					mwg.Windows.POINTAPI p=new mwg.Windows.POINTAPI();
					Caret.GetCaretPos(ref p);
					this.pos=p;//�^�ϊ�
				}
				return pos;
			}
			set{
				this.pos=value;
				Caret.SetCaretPos(this.pos.X,this.pos.Y);
			}
		}
		public int Left{
			get{return this.Position.X;}
			set{this.Position=new System.Drawing.Point(value,this.Position.Y);}
		}
		public int Top{
			get{return this.Position.Y;}
			set{this.Position=new System.Drawing.Point(this.Position.X,value);}
		}
		//import
		[Interop.DllImport("user32.dll")]
		public static extern int SetCaretPos(int x, int y);
		[Interop.DllImport("user32.dll")]
		public static extern int GetCaretPos(ref POINTAPI lpPoint);
		#endregion

		bool bVisible;
		/// <summary>
		/// Caret �̕\���E��\����ݒ薒�͎擾���܂��B
		/// </summary>
		public bool Visible{
			get{
				return this.bVisible;
			}
			set{
				this.bVisible=value;
				if(value)Caret.ShowCaret(this.Control.Handle);
				else Caret.HideCaret(this.Control.Handle);
			}
		}
		[Interop.DllImport("user32.dll")]
		public static extern int ShowCaret(System.IntPtr hwnd);
		[Interop.DllImport("user32.dll")]
		public static extern int HideCaret(System.IntPtr hwnd);

		#region BlinkTime
		/// <summary>
		/// Caret �̓_�ł��鑬����ݒ薒�͎擾���܂��B
		/// ����ȏ�̃v���O�����������Ă��鎞�͕ύX�����̂܂܎c��\��������
		/// </summary>
		public int BlinkTime{
			get{
				return Caret.GetCaretBlinkTime();
			}
			set{
				if(value!=0)Caret.SetCaretBlinkTime(value);
			}
		}
		~Caret(){Caret.FinalizeOld();}
		//static member
		static int blinkTime=0;
		static int instanceCount=0;
		/// <summary>
		/// Caret �̐V�����C���X�^���X���쐬���܂��B
		/// </summary>
		/// <param name="ctrl">Caret ��\������Ώۂ� System.Windows.Forms.Control</param>
		/// <returns>�V�����쐬�����C���X�^���X</returns>
		public static Caret CreateNew(System.Windows.Forms.Control ctrl){
			if(instanceCount==0){
				blinkTime=Caret.GetCaretBlinkTime();
			}else{
			
			}
			instanceCount++;
			return new Caret(ctrl);
		}
		private static void FinalizeOld(){
			instanceCount--;
			if(instanceCount==0&&blinkTime!=0&&Caret.GetCaretBlinkTime()!=blinkTime)Caret.SetCaretBlinkTime(blinkTime);
		}
		//import
		[Interop.DllImport("user32.dll")]
		public static extern int GetCaretBlinkTime();
		[Interop.DllImport("user32.dll")]
		public static extern int SetCaretBlinkTime(int uMSeconds);
		#endregion
	}
}
