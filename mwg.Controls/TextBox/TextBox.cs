using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using mwg.Windows;
namespace mwg.Windows{
	/// <summary>
	/// mwg.Windows.TextBox �́A�F�X�ȋ@�\�������� TextBox �ɂ���\��B�̂���B
	/// </summary>
	public class TextBox:mwg.Windows.ControlA{
		/// <summary>
		/// �f�U�C�i�ϐ�
		/// </summary>
		private System.ComponentModel.Container components=null;
		private System.Drawing.Graphics gr;
		private mwg.Drawing.StringDrawer sd;
		private mwg.Windows.IME ime;
		private mwg.Windows.Caret caret;
		public TextBox(){
			InitializeComponent();
			Initialize2();
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing ){
			if(disposing){
				if(components!=null){
					components.Dispose();
				}
				if(ime!=null)ime.Dispose();
			}
			base.Dispose( disposing );
		}

		#region �R���|�[�l���g �f�U�C�i�Ő������ꂽ�R�[�h 
		private void InitializeComponent(){
			// 
			// TextBox
			// 
			this.BackColor = System.Drawing.Color.White;
			this.EnableImgBuf = true;
			this.Name = "TextBox";
			this.Size = new System.Drawing.Size(304, 208);
		}
		#endregion
		
		private void Initialize2(){
			this.format=new StringFormat();
			this.gr=this.CreateGraphicsBuf();
			//this.gr=this.CreateGraphics();
            this.caret=mwg.Windows.Caret.CreateNew(this);
			this.sd=new mwg.Drawing.StringDrawer(this);
			this.ime=new IME(this);
			//
			// caret
			//
			//this.caret.Size=new Size(1,10);
			//
			// sd
			//
			this.sd.LetterSpacing=0;
			this.sd.Font=new System.Drawing.Font("�l�r�@�S�V�b�N",9);
			this.sd.Rectangle=new Rectangle(10,10,200,200);
			this.sd.Return=true;
			this.sd.Position=new PointF(10,10);
			this.sd.PositionChanged+=new mwg.Drawing.StringDrawer.PositionEvent(sd_PositionChanged);
			this.sd.FontChanged+=new mwg.Drawing.StringDrawer.FontEventHandler(sd_FontChanged);
			this.sd.DirectionChanged+=new mwg.Drawing.StringDrawer.FontEventHandler(sd_DirectionChanged);
			//this.sd.Vertical=true;
			//this.sd.RTL=true;
			//
			// ime
			//
			this.ime.Char+=new mwg.Windows.IME.CompositionEventHandler(this.ime_Char);
			this.ime.StartComposition+=new EventHandler(this.ime_StartComposition);
		}

		private void ime_StartComposition(object sender,System.EventArgs e){
			this.ime.LogFont=this.sd.ToLogFont();
		}
		private void ime_Char(object sender,IME.CompositionEventArgs e){
			if(e.String=='\x8'.ToString())this.BS();
			else this.Append(e.String);
		}
		private void sd_PositionChanged(object sender,mwg.Drawing.StringDrawer.PositionEventArgs e){
			this.caret.Position=new System.Drawing.Point((int)e.X,(int)e.Y);
			this.ime.CompositionPos=e.Position;
		}
		private void sd_FontChanged(object sender, mwg.Drawing.StringDrawer.FontEventArgs e){
			if(e.Vertical){
				this.caret.Width=e.LineHeight;
			}else{
				this.caret.Height=e.LineHeight;
			}
		}
		private void sd_DirectionChanged(object sender,mwg.Drawing.StringDrawer.FontEventArgs e){
			if(e.Vertical){
				this.caret.Height=1;
				this.caret.Width=e.LineHeight;
			}else{
				this.caret.Width=1;
				this.caret.Height=e.LineHeight;
			}
		}

		private System.Drawing.StringFormat format;
		//�s�P�ʂŊǗ���������������G�f�B�^�p�� TextBox ���쐬����ۂɂ͂�������
		private string text="";
		public override string Text{
			get{return this.text;}
			set{
				this.text=value;
				this.RefreshText();
			}
		}
		public void Append(string text){
			this.sd.DrawString(text);
			this.RefreshBuf();
			this.text+=text;
		}
		public void RefreshText(){
			this.GraphicsB.Clear(this.BackColor);
			this.sd.Position=new PointF(10f,10f);
			this.sd.DrawString(this.text);
			this.RefreshBuf();
		}
		public void BS(){
			if(this.text.Length==0)return;
			this.text=this.text.Substring(0,this.text.Length-1);
			this.RefreshText();
		}
		//���J�v���p�e�B
		[System.ComponentModel.Category("�\��"),System.ComponentModel.Description("�`��ɗp����t�H���g���擾���͐ݒ肵�܂��B")]
		public System.Drawing.Font TextFont{
			get{return this.sd.Font;}
			set{
				this.sd.Font=value;
			}
		}
		[System.ComponentModel.Category("�\��"),System.ComponentModel.Description("�����̃����_�����O�̕��@���擾���͐ݒ肵�܂��B")]
		public System.Drawing.Text.TextRenderingHint RenderingHint{
			get{return this.sd.RenderingHint;}
			set{this.sd.RenderingHint=value;}
		}
	}
}
