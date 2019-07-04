// mwgDrawing.cs
// ���f�̏��p���p�͂�����������.
// �ύX���������ꍇ�́A�ύX�҂̖��O�ƔN���� Copyright �̉��ɑ����ď��������ĉ�����.
// Copyright, ��������, 2006. 

namespace mwg.Drawing{
	/// <summary>
	/// �F��ێ�����ׂ̃N���X(class)�ł��B�F�̏��� ARGB �� 32bit �ň����܂��BAlpha�l�͓����x��\�����Ƃ��Ĉ����܂��B���Z�q(operator)��ʂ��āA���F�E��F�Ȃǂ̋@�\��񋟂��܂��B
	/// This is the class to represent a color, which offers, through some operator, some functions (mixing colors, getting ?(��F), and so on).
	/// </summary>
	public class Color{
		//=====================================
		//          fields
		//-------------------------------------
		/// <summary>�Ԃ̋��x��ێ����܂��B</summary>
		private byte r;
		/// <summary>�΂̋��x��ێ����܂��B</summary>
		private byte g;
		/// <summary>�̋��x��ێ����܂��B</summary>
		private byte b;
		/// <summary>�����x��ێ����܂��B</summary>
		private byte a;
		//=====================================
		//          constructor
		//-------------------------------------
		/// <summary>
		/// mwg.Drawing.Color �R���X�g���N�^�B�^����ꂽ��񂩂����̐F�𐶐����A���������F��ێ����� mwg.Drawing.Color �N���X�C���X�^���X���쐬���܂��B�F���̎O���FRGB�\��(red)�A��(green)�A��(blue)�\���ꂼ��̋������w�肵�ĐF���w�肵�܂��B
		/// </summary>
		/// <param name="red">�Ԃ̋��x��ݒ肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		/// <param name="green">�΂̋��x��ݒ肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		/// <param name="blue">�̋��x��ݒ肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		public Color(int red,int green,int blue){
			if(red>255)red=255;if(red<0)red=0;
			r=(byte)red;
			if(green>255)green=255;if(green<0)green=0;
			g=(byte)green;
			if(blue>255)blue=255;if(blue<0)blue=0;
			b=(byte)blue;
			a=(byte)0;
		}
		/// <summary>
		/// mwg.Drawing.Color �R���X�g���N�^�B�^����ꂽ��񂩂����̐F�𐶐����A���������F��ێ����� mwg.Drawing.Color �C���X�^���X���쐬���܂��B
		/// �F���̎O���FRGB�\��(red)�A��(green)�A��(blue)�\���ꂼ��̋������w�肵�ĐF���w�肵�܂��B
		/// </summary>
		/// <param name="red">�Ԃ̋��x��ݒ肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B</param>
		/// <param name="green">�΂̋��x��ݒ肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B</param>
		/// <param name="blue">�̋��x��ݒ肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B</param>
		public Color(byte red,byte green,byte blue){
			r=red;
			g=green;
			b=blue;
			a=(byte)0;
		}
		/// <summary>
		/// mwg.Drawing.Color �R���X�g���N�^�B�^����ꂽ��񂩂����̐F�𐶐����A���������F��ێ����� mwg.Drawing.Color �C���X�^���X���쐬���܂��B
		/// �F���̎O���FRGB�ƃ��lA�\��(red)�A��(green)�A��(blue)�A�����x(transparence)�\���ꂼ��̋������w�肵�ĐF���w�肵�܂��B
		/// </summary>
		/// <param name="red">�Ԃ̋��x��ݒ肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		/// <param name="green">�΂̋��x��ݒ肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		/// <param name="blue">�̋��x��ݒ肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		/// <param name="alpha">�����x���w�肵�܂��B0 ���� 255 ���̒l��ݒ肵�܂��B0 ���w�肵�����ɂ́A�S�������łȂ��A�����W���I�ȕs�����̐F�ƂȂ�܂��B
		/// 255 ���w�肵�����ɂ͊��S�ɓ����ȐF��\���܂��B���S�ɓ����Ȏ��ɂ́Ared,green,blue �͐��������F�Ɏ����I�ɉe���������܂���B0 ��菬�����l��ݒ肵������ 0 �Ƃ��āA255 ���傫���l��ݒ肵������ 255 �Ƃ��ď������܂��B</param>
		public Color(int red,int green,int blue,int alpha){
			if(red>255)red=255;if(red<0)red=0;
			r=(byte)red;
			if(green>255)green=255;if(green<0)green=0;
			g=(byte)green;
			if(blue>255)blue=255;if(blue<0)blue=0;
			b=(byte)blue;
			if(alpha>255)alpha=255;if(alpha<0)alpha=0;
			a=(byte)alpha;
		}
		/// <summary>
		/// mwg.Drawing.Color �R���X�g���N�^�B�^����ꂽ��񂩂����̐F�𐶐����A���������F��ێ����� mwg.Drawing.Color �C���X�^���X���쐬���܂��B�F���̎O���FRGB�ƃ��lA�\��(red)�A��(green)�A��(blue)�A�����x(transparence)�\���ꂼ��̋������w�肵�ĐF���w�肵�܂��B
		/// </summary>
		/// <param name="red">�Ԃ̋��x��ݒ肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B</param>
		/// <param name="green">�΂̋��x��ݒ肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B</param>
		/// <param name="blue">�̋��x��ݒ肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B</param>
		/// <param name="alpha">�����x(transparence)���w�肵�܂��BBinary �l�� 0 ���� 255 ���̒l�ɒu�������ēǂݎ��܂��B0 ���w�肵�����ɂ́A�S�������łȂ��A�����W���I�ȕs�����̐F�ƂȂ�܂��B
		/// 255 ���w�肵�����ɂ͊��S�ɓ����ȐF��\���܂��B���S�ɓ����Ȏ��ɂ́Ared,green,blue �͐��������F�Ɏ����I�ɉe���������܂���B</param>
		public Color(byte red,byte green,byte blue,byte alpha){
			r=red;
			g=green;
			b=blue;
			a=alpha;
		}
		public Color(System.Drawing.Color color){
			this.r=color.R;
			this.g=color.G;
			this.b=color.B;
			this.a=color.A;
		}
		//���F�����w�肵�āA�F���`�����t�@�C�����������āA�F��ݒ肷��R���X�g���N�^�B

		//=====================================
		//          properties
		//-------------------------------------
		/// <summary>
		/// �ێ����Ă���F�̐Ԃ̋��x���擾�܂��͐ݒ肵�܂��B0 ���ł��キ�A255 ���ł��������Ƃ�\���܂��B
		/// </summary>
		public byte Red{
			get{return r;}
			set{r=value;}
		}
		/// <summary>
		/// �ێ����Ă���F�̗΂̋��x���擾�܂��͐ݒ肵�܂��B0 ���ł��キ�A255 ���ł��������Ƃ�\���܂��B
		/// </summary>
		public byte Green{
			get{return g;}
			set{g=value;}
		}
		/// <summary>
		/// �ێ����Ă���F�̐̋��x���擾�܂��͐ݒ肵�܂��B0 ���ł��キ�A255 ���ł��������Ƃ�\���܂��B
		/// </summary>
		public byte Blue{
			get{return b;}
			set{b=value;}
		}
		/// <summary>
		/// �ێ����Ă���F�̓����x���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public byte Alpha{
			get{return a;}
			set{a=value;}
		}
		//=====================================
		//          methods
		//-------------------------------------
		public override string ToString(){
			return "mwg.dll mwg.Drawing.Color "+this.r.ToString()+","+this.g.ToString()+","+this.b.ToString()+","+this.a.ToString();
		}
		//=====================================
		//          operators
		//-------------------------------------
		/// <summary>
		/// ���@���F���s���܂��B�����@�����������܂�(<code>color1 + color2 == color2 + color1</code>)�B
		/// �댳�ɂ́u���v�܂��́u���S�ɓ����ȐF�v���������܂��B�c�O�Ȃ��猋���@���͕s�����ł��B
		/// </summary>
		public static mwg.Drawing.Color operator +(mwg.Drawing.Color col1,mwg.Drawing.Color col2){
			float a1=1-col1.Alpha/255;
			float a2=1-col2.Alpha/255;
			int red=(int)(a1*(int)col1.Red+a2*(int)col2.Red);
			int green=(int)(a1*(int)col1.Green+a2*(int)col2.Green);
			int blue=(int)(a1*(int)col1.Blue+a2*(int)col2.Blue);
			return new mwg.Drawing.Color(red,green,blue,(int)((1-a1*a2)*255));
		}
		/// <summary>
		/// ���@���F�̋t���Z���s���܂��B�܂�A<code>(color1 + color2) - color2 == color1</code>��(�ۂߌ덷�𖳎������)�������܂��B
		/// ���R�����@���͕s�����ƂȂ�܂��B�����@���͐������܂���B
		/// </summary>
		public static mwg.Drawing.Color operator -(mwg.Drawing.Color col1,mwg.Drawing.Color col2){
			float a0=1-col1.Alpha/255;
			float a1=1-col2.Alpha/255;
			if(a0==0||a1==0)return mwg.Drawing.Pallete.Black;
			float a2=a0/a1;
			int red=(int)((col1.Red-a1*col2.Red)/a2);
			int green=(int)((col1.Green-a1*col2.Red)/a2);
			int blue=(int)((col1.Red-a1*col2.Red)/a2);
			return new mwg.Drawing.Color(red,green,blue,(int)(255*(1-a2)));
		}
		//�����@���F
		//������
		/// <summary>
		/// �Ǝ��̐F�𖾎��I�� .NET Framework �� System.Drawing.Color �ɕϊ����܂��B���̕ώ��y�ё����͍݂�܂���B
		/// </summary>
		public static explicit operator System.Drawing.Color(mwg.Drawing.Color col){
			return System.Drawing.Color.FromArgb(col.Alpha,col.Red,col.Green,col.Blue);
		}
		public static explicit operator mwg.Drawing.Color(System.Drawing.Color color){
			return new mwg.Drawing.Color(color);
		}
		
	}
	
	//Pallete
	public class Pallete{
		public mwg.Drawing.Color[] colors;
		//�����Œ�`�������X�g�ɒu��������̂����z�B
		//=====================================
		//          properties
		//-------------------------------------
		public int Length{
			get{return this.colors.Length;}
		}
		//=====================================
		//          
		//-------------------------------------
		public void Add(System.Drawing.Color color){
			//TODO: �V�����F�f�[�^��o�^����葱���������B
		}
		//=====================================
		//          static properties
		//-------------------------------------
		public static mwg.Drawing.Color Black{
			get{return new mwg.Drawing.Color(0,0,0);}
		}
		public static mwg.Drawing.Color White{
			get{return new mwg.Drawing.Color(255,255,255);}
		}
		
	}
	//mwqArray
	// ���ꂼ��̗v�f�̌^�ϊ������āA�R�s�[(�ϊ���̌^�̕ʂɊ֐������Ȃǂ���)
	// ���镔�����爽�镔���܂ł͈̔͂̏����R�s�[

	//mwqXml
	//��{�I�Ƀc���[�\�����������镨
	//XML�ł̏����o����Ǎ����s����悤�ɂ���

	//mwgHtml
	//mwqXml�Ɩw�Ǔ��������A�����ɃN�I�e�[�V�������g���ĂȂ��A�n�܂�̃^�O�ƏI���̃^�O�̑啶������������v���Ȃ��A�n�܂�̃^�O�ɑ΂��āA�I���̃^�O���Ȃ��A���̂��Ƃɑ΂��Ċ��e�ȃN���X�B

	public class StringDrawer{
		#region �ÓI�萔
		static System.Drawing.PointF point0;
		static System.Drawing.StringFormat formatV;
		static StringDrawer(){
			StringDrawer.point0=new System.Drawing.PointF(0,0);
			StringDrawer.formatV=new System.Drawing.StringFormat(System.Drawing.StringFormatFlags.DirectionVertical);
		}
		#endregion

		//***********************************************************
		//		<Field>	Graphics	g
		//-----------------------------------------------------------
		System.Drawing.Graphics g;
		/*private System.Drawing.Graphics Graphics{
			get{return this.g;}
			set{
				if(this.g==value)return;
				if(this.g!=null)this.g.Dispose();
				this.g=value;
			}
		}//*/
		/// <summary>
		/// �����_�����O�̕��@���擾���͐ݒ肵�܂�
		/// </summary>
		public System.Drawing.Text.TextRenderingHint RenderingHint{
			get{return this.g.TextRenderingHint;}
			set{this.g.TextRenderingHint=value;}
		}

		//***********************************************************
		//		<Field>	Brush	brush
		//-----------------------------------------------------------
		//TODO:���̎�ނ̂ɂ��Ή�
		System.Drawing.Brush brush;
		/*public System.Drawing.Color ForeColor{
			get{return this.brush.Color;}
			set{this.brush.Color=value;}
		}//*/

		//TODO:�w�i�F(�摜)�≺���A�g�����ɂ��Ή�����
		//���w�i �����̒i�K�ŐF��t����//�~�摜�ɂ������̂��ꂪ
		//������ Drawing.StringDrawer.Char->DrawTo �� Rectangle ��Ԃ����ɕύX���āA���������
		//***********************************************************
		//		<Field>	PointF	strPosition
		//-----------------------------------------------------------
		/// <summary>
		/// �������݈ʒu
		/// </summary>
		[System.Runtime.CompilerServices.AccessedThroughProperty("Position")]
		System.Drawing.PointF strPosition;
		public System.Drawing.PointF Position{
			get{return this.strPosition;}
			set{
				this.strPosition=value;
				this.OnPositionChanged();
			}
		}
		protected virtual void OnPositionChanged(){
			if(this.PositionChanged==null)return;
			this.PositionChanged(this,new PositionEventArgs(this.strPosition));
		}
		protected virtual void OnPositionReturned(){
			if(this.PositionReturned==null)return;
			this.PositionReturned(this,new PositionEventArgs(this.strPosition));
		}
		public event PositionEvent PositionChanged;
		public event PositionEvent PositionReturned;
		public delegate void PositionEvent(object sender,PositionEventArgs e);
		public class PositionEventArgs:System.EventArgs{
			private System.Drawing.PointF pt;
			public System.Drawing.PointF Position{get{return this.pt;}}
			public float X{get{return this.pt.X;}}
			public float Y{get{return this.pt.Y;}}
			public PositionEventArgs(System.Drawing.PointF pt){this.pt=pt;}
		}

		/// <summary>
		/// �܂�Ԃ����ǂ�����ݒ肵�܂�
		/// </summary>
		[System.Runtime.CompilerServices.AccessedThroughProperty("Return")]
		private bool fReturn=false;
		/// <summary>
		/// �܂�Ԃ����ǂ������擾���͐ݒ肵�܂�
		/// Rectangle ���ݒ肳��Ă��Ȃ��ꍇ�́A�����I�ɂ͐܂�Ԃ����L���ƂȂ��Ă��Ă��A�o�͂͐܂�Ԃ��Ȃ��̎��Ɠ����ɂȂ�܂�
		/// </summary>
		public bool Return{
			get{return this.setRectangle&&this.fReturn;}
			set{this.fReturn=value;}
		}
		/// <summary>
		/// �`��̈��ݒ肵�܂�
		/// </summary>
		[System.Runtime.CompilerServices.AccessedThroughProperty("Rectangle")]
		private System.Drawing.Rectangle rectangle;
		private bool setRectangle=false;
		public System.Drawing.Rectangle Rectangle{
			get{return this.rectangle;}
			set{this.rectangle=value;this.setRectangle=true;}
		}

		public int LetterSpacing=0;
		/// <summary>
		/// �s�ƍs�̊Ԃ�ݒ肵�܂�
		/// </summary>
		public int LineSpacing=0;
		//TODO:
		//�^�u�̒���=����̊Ԋu ���ݒ肷��
		//������������ŔC�ӂɐݒ肷�鎖���o����l�ɂ���B(���Ԋu���[�h�A�J�X�^�����[�h)
		//�� [n�ڂ�Tab]=[n�{�ڂ̊���Ɉړ�] ���A[Tab]=[���̊���Ɉړ�] �̓��ލl���鎖���o����

		#region ���s���� Rt Cr Lf
		/// <summary>
		/// ���s���܂�
		/// </summary>
		private void Rt(){
			this.Cr();
			this.Lf();
			this.OnPositionReturned();
		}
		/// <summary>
		/// CR �����s���܂��B�������݈ʒu���s���Ɉړ����܂��B
		/// </summary>
		/// <returns>
		/// CR �����s�������ۂ���Ԃ��܂��B
		/// ���߂���s���ɂ������ꍇ�ɂ͎��s���܂���B
		/// �܂��A���s�������ɂȂ��Ă���ꍇ�����s���܂���B
		/// </returns>
		private bool Cr(){
			if(!this.Return)return false;
			bool r=false;
			if(this.Vertical){
				if(r=this.strPosition.Y!=this.Rectangle.Top)
					this.strPosition.Y=this.Rectangle.Top;
			}else if(this.RTL){
				if(r=this.strPosition.X!=this.Rectangle.Right)
					this.strPosition.X=this.Rectangle.Right;
			}else{
				if(r=this.strPosition.X!=this.Rectangle.Left)
					this.strPosition.X=this.Rectangle.Left;
			}
			return r;
		}
		/// <summary>
		/// �s��������܂��B
		/// </summary>
		/// <param name="px">���݂̍s�̍�����ݒ肵�܂�</param>
		private void Lf(int px){
			if(this.Vertical){
				this.strPosition.X+=(this.RTL)?-px-this.LineSpacing:px+this.LineSpacing;
			}else{
				this.strPosition.Y+=px+this.LineSpacing;
			}
		}
		/// <summary>
		/// �s��������܂��B
		/// </summary>
		private void Lf(){
			int px;
			if(this.Vertical){
				px=(int)(this.fontSizeKV.Width);
				this.strPosition.X+=(this.RTL)?-px-this.LineSpacing:px+this.LineSpacing;
			}else{
				px=(int)(this.fontSizeK.Height);
				this.strPosition.Y+=px+this.LineSpacing;
			}
		}
		#endregion

		//***********************************************************
		//		<Field>	StringFormat	format
		//-----------------------------------------------------------
		System.Drawing.StringFormat format;
		/// <summary>
		/// �����������E���獶�ł��邩���擾���͐ݒ肵�܂�
		/// </summary>
		public bool RTL{
			get{return 0!=(this.format.FormatFlags&System.Drawing.StringFormatFlags.DirectionRightToLeft);}
			set{
				if(value){
					this.format.FormatFlags|=System.Drawing.StringFormatFlags.DirectionRightToLeft;
				}else{
					this.format.FormatFlags&=System.Drawing.StringFormatFlags.DirectionRightToLeft;
				}
			}
		}
		/// <summary>
		/// �����������ォ�牺�ł��鎖���擾���͐ݒ肵�܂�
		/// </summary>
		public bool Vertical{
			get{return 0!=(this.format.FormatFlags&System.Drawing.StringFormatFlags.DirectionVertical);}
			set{
				if(value){
					this.format.FormatFlags|=System.Drawing.StringFormatFlags.DirectionVertical;
				}else{
					this.format.FormatFlags&=System.Drawing.StringFormatFlags.DirectionVertical;
				}
				if(this.DirectionChanged!=null)this.DirectionChanged(this,new FontEventArgs(this));
			}
		}
		public event StringDrawer.FontEventHandler DirectionChanged;
		//***********************************************************
		//		<Field>	Font	font
		//-----------------------------------------------------------
		[System.Runtime.CompilerServices.AccessedThroughProperty("Font")]
		System.Drawing.Font font;
		System.Drawing.SizeF fontSizeX;
		System.Drawing.SizeF fontSizeK;
		System.Drawing.SizeF fontSizeXV;
		System.Drawing.SizeF fontSizeKV;
		public System.Drawing.Font Font{
			get{return this.font;}
			set{
				this.font=value;
				// �����̑傫���𑪂����擾
				fontSizeX=this.g.MeasureString("x",this.font);
				fontSizeK=this.g.MeasureString("��",this.font);
				fontSizeXV=this.g.MeasureString("x",this.font,point0,formatV);
				fontSizeKV=this.g.MeasureString("��",this.font,point0,formatV);
				this.OnFontChanged();
			}
		}
		/// <summary>
		/// �����̑傫���ɂ���Č��܂�A�s�̍���
		/// </summary>
		//TODO:�s�̍����������ł����߂���l�ɂ���B(�vflag:�����̃J�X�^��)
		public float LineHeight{
			get{return (this.Vertical)?this.fontSizeKV.Width:this.fontSizeK.Height;}
		}
		public event FontEventHandler FontChanged;
		public delegate void FontEventHandler(object sender,FontEventArgs e);
		public class FontEventArgs:System.EventArgs{
			private System.Drawing.Font font;
			private int lineh;
			private bool vertical;
			public System.Drawing.Font Font{
				get{return this.font.Clone() as System.Drawing.Font;}
			}
			public int LineHeight{
				get{return this.lineh;}
			}
			public bool Vertical{get{return this.vertical;}}
			public FontEventArgs(System.Drawing.Font font,int lineh,bool vert){
				this.font=font;this.lineh=lineh;this.vertical=vert;
			}
			public FontEventArgs(mwg.Drawing.StringDrawer sender):this(sender.Font,(int)sender.LineHeight,sender.Vertical){}
		}
		protected virtual void OnFontChanged(){
			if(this.FontChanged==null)return;
			this.FontChanged(this,new FontEventArgs(this));
		}

		//***********************************************************
		//		<constructor>	StringDrawer
		//-----------------------------------------------------------
		public StringDrawer(System.Drawing.Graphics graphics){
			this.g=graphics;
			this.Initialize();
		}
		public StringDrawer(mwg.Windows.ControlA ctrl){
			this.g=ctrl.CreateGraphicsBuf();
			ctrl.BufferImageRenew+=new System.EventHandler(ctrl_BufferImageRenew);
			this.Initialize();
		}
		private void ctrl_BufferImageRenew(object sender,System.EventArgs e){
			this.g=((mwg.Windows.ControlA)sender).CreateGraphicsBuf();
		}
		private void Initialize(){
			this.Font=new System.Drawing.Font("�l�r �S�V�b�N",9);
			this.brush=System.Drawing.Brushes.Black;
			this.strPosition=new System.Drawing.PointF(0,0);
			this.format=new System.Drawing.StringFormat();
		}

		//***********************************************************
		//		<method>	DrawString
		//-----------------------------------------------------------
		//TODO:�ϓ����t�ɑΉ�����
		//TODO:�����v�邾���� DrawString,DrawLetter (Measure)���p�ӂ���
		public void DrawString(string text){
			if(text.Length==0)return;
			char[] chars=text.ToCharArray();
			int i=0;
			while(i<text.Length){
				if(chars[i]>='\x20'){
					this.DrawLetter(new Char(this,ref i,ref chars));
					continue;
				}
				switch((int)chars[i]){
					case 0:this.DrawLetter('\x2400');break;
					case 1:this.DrawLetter('\x2401');break;
					case 2:this.DrawLetter('\x2402');break;
					case 3:this.DrawLetter('\x2403');break;
					case 4:this.DrawLetter('\x2404');break;
					case 5:this.DrawLetter('\x2405');break;
					case 6:this.DrawLetter('\x2406');break;
					case 7:this.DrawLetter('\x2407');break;
					case 8:this.DrawLetter('\x2408');break;//BackSpace
					case 9:this.DrawLetter('\x2409');break;
					case 10:this.Lf();break;//LF
					case 11:this.DrawLetter('\x240b');break;
					case 12:this.DrawLetter('\x240c');break;
					case 13:this.Cr();this.Lf();break;//cr
					case 14:this.DrawLetter('\x240e');break;
					case 15:this.DrawLetter('\x240f');break;
					case 16:this.DrawLetter('\x2410');break;
					case 17:this.DrawLetter('\x2411');break;
					case 18:this.DrawLetter('\x2412');break;
					case 19:this.DrawLetter('\x2413');break;
					case 20:this.DrawLetter('\x2414');break;
					case 21:this.DrawLetter('\x2415');break;
					case 22:this.DrawLetter('\x2416');break;
					case 23:this.DrawLetter('\x2417');break;
					case 24:this.DrawLetter('\x2418');break;
					case 25:this.DrawLetter('\x2419');break;
					case 26:this.DrawLetter('\x241a');break;
					case 27:this.DrawLetter('\x241b');break;
					case 28:this.DrawLetter('\x241c');break;
					case 29:this.DrawLetter('\x241d');break;
					case 30:this.DrawLetter('\x241e');break;
					case 31:this.DrawLetter('\x241f');break;
				}
				i++;
			}
			this.OnPositionChanged();
		}
		public void DrawChar(char c){
			if(c>='\x20'){
				this.DrawLetter(new Char(this,c.ToString()));
				return;
			}
			//��:����͏�� switch �Ɠ������e
			switch((int)c){
				case 0:this.DrawLetter('\x2400');break;
				case 1:this.DrawLetter('\x2401');break;
				case 2:this.DrawLetter('\x2402');break;
				case 3:this.DrawLetter('\x2403');break;
				case 4:this.DrawLetter('\x2404');break;
				case 5:this.DrawLetter('\x2405');break;
				case 6:this.DrawLetter('\x2406');break;
				case 7:this.DrawLetter('\x2407');break;
				case 8:this.DrawLetter('\x2408');break;//BackSpace
				case 9:this.DrawLetter('\x2409');break;//�����^�u
				case 10:this.Lf();break;//LF
				case 11:this.DrawLetter('\x240b');break;//�����^�u
				case 12:this.DrawLetter('\x240c');break;
				case 13:this.Cr();this.Lf();break;//CR/���s
				case 14:this.DrawLetter('\x240e');break;
				case 15:this.DrawLetter('\x240f');break;
				case 16:this.DrawLetter('\x2410');break;
				case 17:this.DrawLetter('\x2411');break;
				case 18:this.DrawLetter('\x2412');break;
				case 19:this.DrawLetter('\x2413');break;
				case 20:this.DrawLetter('\x2414');break;
				case 21:this.DrawLetter('\x2415');break;
				case 22:this.DrawLetter('\x2416');break;
				case 23:this.DrawLetter('\x2417');break;
				case 24:this.DrawLetter('\x2418');break;
				case 25:this.DrawLetter('\x2419');break;
				case 26:this.DrawLetter('\x241a');break;
				case 27:this.DrawLetter('\x241b');break;
				case 28:this.DrawLetter('\x241c');break;
				case 29:this.DrawLetter('\x241d');break;
				case 30:this.DrawLetter('\x241e');break;
				case 31:this.DrawLetter('\x241f');break;
			}
		}

		#region �����̔z�u TODO
		private void DrawLetter(string text){
			this.DrawLetter(new Char(this,text));
		}
		private void DrawLetter(char c){
			this.DrawLetter(new Char(this,c.ToString()));
		}
		private void DrawLetter(StringDrawer.Char c){
			//TODO:���s�E�܂�Ԃ��Ɋւ��Ď������ׂ���
			//�@�֑�����
			//	���[�h���b�v
			//	���X�N���[���\�̎��ɂ͎g���Ȃ��l�ɂ��Ȃ��ƕςȎ��ɂȂ�
			//	�r���ŕ����̑傫�����ς�������̍s����ɑΉ�����
			if(this.Vertical&&this.RTL){
				//�܂�Ԃ�����
				if(this.Return&&this.Rectangle.Bottom<this.strPosition.Y+c.Height){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X-c.Width,this.strPosition.Y);
				this.strPosition.Y+=c.Height+this.LetterSpacing;
			}else if(this.RTL){
				//TEST:�܂�Ԃ�����
				if(this.Return&&this.Rectangle.Left>this.strPosition.X){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X,this.strPosition.Y);
				this.strPosition.X-=c.Width+this.LetterSpacing;
			}else if(this.Vertical){
				//�܂�Ԃ�����
				if(this.Return&&this.Rectangle.Bottom<this.strPosition.Y+c.Height){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X+this.LineHeight-c.Width,this.strPosition.Y);
				this.strPosition.Y+=c.Height+this.LetterSpacing;
			}else{
				//�܂�Ԃ�����
				if(this.Return&&this.Rectangle.Right<this.strPosition.X+c.Width){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X,this.strPosition.Y);
				this.strPosition.X+=c.Width+this.LetterSpacing;
			}		
		}
		#endregion

		//***********************************************************
		//		<operator>
		//-----------------------------------------------------------
		public static implicit operator mwg.Drawing.StringDrawer(System.Drawing.Graphics g){
			return new mwg.Drawing.StringDrawer(g);
		}
		public static implicit operator System.Drawing.Graphics(mwg.Drawing.StringDrawer graphics){
			return graphics.g;
		}

		//***********************************************************
		//		<method:�ϊ�>	
		//-----------------------------------------------------------
		/// <summary>
		/// IME ���ɓn���ׂ� LOGFONT ���擾����̂Ɏg�p���܂�
		/// </summary>
		/// <returns>LOGFONT ��Ԃ��܂�</returns>
		public mwg.Windows.LOGFONT ToLogFont(){
			object lf2=new mwg.Windows.LOGFONT();
			font.ToLogFont(lf2);
			mwg.Windows.LOGFONT lf=(mwg.Windows.LOGFONT)lf2;
			string fontName=this.font.Name;
			if(this.Vertical){
				lf.lfOrientation=2700;
				lf.lfEscapement=2700;
				fontName="@"+fontName;
			}else if(this.RTL){
				lf.lfOrientation=1800;
				lf.lfEscapement=1800;
			}
			byte[] bytes=System.Text.Encoding.Default.GetBytes(fontName);
			for(int i=0;i<lf.lfFaceName.Length;i++){
				lf.lfFaceName[i]=(i>=bytes.Length)?(byte)0:bytes[i];
			}
			return lf;
		}

		//***********************************************************
		//		<class>		Char
		//-----------------------------------------------------------
		#region <class> Char
		/// <summary>
		/// �����̉摜���쐬���A����񋟂���N���X�B�摜�̏����o�����s���B
		/// ���䕶���Ȃǂ������ۂɂ́A����̑ΏۂƂȂ镶���ƕ����āA��̕��Ƃ��Ĉ���
		/// </summary>
		private class Char{
			//����\��*12
			const char CMB_LR='\x2ff0';//��,�E
			const char CMB_TB='\x2ff1';//��,��
			const char CMB_LCR='\x2ff2';//��,��,�E
			const char CMB_TMB='\x2ff3';//��,��,��
			const char CMB_OI='\x2ff4';//�O,��
			const char CMB_OB='\x2ff5';//�O,��
			const char CMB_OT='\x2ff6';//�O,��
			const char CMB_OR='\x2ff7';//�O,�E
			const char CMB_ORB='\x2ff8';//�O,�E��
			const char CMB_OLB='\x2ff9';//�O,����
			const char CMB_ORT='\x2ffa';//�O,�E��
			const char CMB_LTRB='\x2ffb';//����,�E��
			/// <summary>
			/// ���ꂼ��̕���\���ɑ΂��āA�\���v�f�̕����������Ɉʒu���邩��ݒ�B
			/// �l��float�ň�̍\���v�f�̈ʒu�Ƒ傫�������߂�B
			/// (��ڂ����̈ʒu�A��ڂ��E�̈ʒu�A�O�ڂ����A�l�ڂ�����)
			/// ���ꂼ��̐����́A������̊����̕��E�����ɑ΂����ŕ\���B
			/// </summary>
			static float[][] CMB_RATES={
				new float[]{0f,0f,0.54f,1f,0.46f,0f,0.54f,1f},//��,�E
				new float[]{0f,0f,1f,0.56f,0f,0.44f,1f,0.56f},
				new float[]{0f,0f,0.36f,1f,0.32f,0f,0.36f,1f,0.64f,0f,0.36f,1f},
				new float[]{0f,0f,1f,0.38f,0f,0.31f,1f,0.38f,0f,0.62f,1f,0.38f},
				new float[]{0f,0f,1f,1f,0.15f,0.15f,0.7f,0.7f},//�O,��
				new float[]{0f,0f,1f,1f,0.25f,0.5f,0.5f,0.5f},
				new float[]{0f,0f,1f,1f,0.15f,0f,0.7f,0.7f},
				new float[]{0f,0f,1f,1f,0.3f,0.15f,0.7f,0.7f},
				new float[]{0f,0f,1f,1f,0.3f,0.3f,0.7f,0.7f},//�O,�E��
				new float[]{0f,0f,1f,1f,0f,0.3f,0.7f,0.7f},
				new float[]{0f,0f,1f,1f,0.3f,0f,0.7f,0.7f},
				new float[]{0f,0f,0.7f,0.7f,0.3f,0.3f,0.7f,0.7f}//����,�E��
			};
			//--field
			public int Height;
			public int Width;
			public System.Drawing.Bitmap bmp;
			public Char(mwg.Drawing.StringDrawer parent,string text){
				this.initialize(parent,text);
			}
			public Char(mwg.Drawing.StringDrawer parent,ref int i,ref char[] text){
				if(i>=text.Length){
					this.initialize(parent," ");
					return;
				}
				//TODO:�����̍\���̋L����bool �̃v���p�e�B������
				//TODO:�����ȂǁA���ʂ̕��͔䗦��ς���
				if(CMB_LR<=text[i]&&text[i]<=CMB_LTRB){
					this.initialize(parent,"�@");
					System.Drawing.Graphics g=this.CreateGraphics();
					int j=(int)text[i++]-(int)CMB_LR;
					int kM=CMB_RATES[j].Length/4;
					for(int k=0;k<kM;k++){
						this.DrawChar(
							g,new Char(parent,ref i,ref text),
							CMB_RATES[j][k*4],CMB_RATES[j][k*4+1],CMB_RATES[j][k*4+2],CMB_RATES[j][k*4+3]
						);
					}
					return;
				}
				//���S�����̎擾
				string x=text[i++].ToString();
				//�C���q�̕t��
				while(i<text.Length){
					if('\x300'<=text[i]&&text[i]<'\x370'){
						x+=text[i].ToString();
					}else{
						this.initialize(parent,x);
						return;
					}
					i++;
				}
				this.initialize(parent,x);
			}
			private void initialize(StringDrawer parent,string text){
				//--�����̑傫�����擾
				string text2=
					(text==" "||text=="")?"x":
					(text=="�@")?"��":
					text;
				System.Drawing.SizeF inner,outer;
				if(parent.Vertical){
					inner=parent.g.MeasureString("x"+text2,parent.font,StringDrawer.point0,StringDrawer.formatV);
					inner.Height=inner.Height-parent.fontSizeXV.Height;
					outer=parent.g.MeasureString(text2,parent.font,StringDrawer.point0,StringDrawer.formatV);
				}else{
					inner=parent.g.MeasureString("x"+text2,parent.font);
					inner.Width=inner.Width-parent.fontSizeX.Width;
					outer=parent.g.MeasureString(text2,parent.font);
				}
				//--�e��ϐ��̐ݒ�
				this.Height=(int)inner.Height;
				this.Width=(int)inner.Width;
				this.bmp=new System.Drawing.Bitmap(
					(int)outer.Width,(int)outer.Height,
					System.Drawing.Imaging.PixelFormat.Format32bppArgb
				);
				//--�����̕`��
				System.Drawing.Graphics g=this.CreateGraphics();
				g.TextRenderingHint=parent.g.TextRenderingHint;
				g.Clear(System.Drawing.Color.Transparent);
				if(parent.Vertical){
					g.DrawString(text,parent.font,parent.brush,-2,-2,StringDrawer.formatV);
				}else{
					g.DrawString(text,parent.font,parent.brush,-2,0);
				}			
			}
			public void DrawTo(System.Drawing.Graphics graphics,float x,float y){
				graphics.DrawImageUnscaled(this.bmp,(int)x,(int)y);
			}
			public System.Drawing.Graphics CreateGraphics(){
				return System.Drawing.Graphics.FromImage(this.bmp);
			}
			public void DrawChar(System.Drawing.Graphics g,StringDrawer.Char c,float x,float y,float w,float h){
				g.DrawImage(
					c.bmp,
					new System.Drawing.RectangleF(this.Width*x,this.Height*y,this.bmp.Width*w,this.bmp.Height*h)
				);
			}
		}
		#endregion
	}
}