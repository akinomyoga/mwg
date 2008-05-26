// mwgDrawing.cs
// 無断の商用利用はご遠慮下さい.
// 変更を加えた場合は、変更者の名前と年月を Copyright の下に続けて書き加えて下さい.
// Copyright, 村瀬功一, 2006. 

namespace mwg.Drawing{
	/// <summary>
	/// 色を保持する為のクラス(class)です。色の情報は ARGB の 32bit で扱います。Alpha値は透明度を表す物として扱います。演算子(operator)を通して、混色・補色などの機能を提供します。
	/// This is the class to represent a color, which offers, through some operator, some functions (mixing colors, getting ?(補色), and so on).
	/// </summary>
	public class Color{
		//=====================================
		//          fields
		//-------------------------------------
		/// <summary>赤の強度を保持します。</summary>
		private byte r;
		/// <summary>緑の強度を保持します。</summary>
		private byte g;
		/// <summary>青の強度を保持します。</summary>
		private byte b;
		/// <summary>透明度を保持します。</summary>
		private byte a;
		//=====================================
		//          constructor
		//-------------------------------------
		/// <summary>
		/// mwg.Drawing.Color コンストラクタ。与えられた情報から特定の色を生成し、生成した色を保持する mwg.Drawing.Color クラスインスタンスを作成します。色光の三原色RGB―赤(red)、緑(green)、青(blue)―それぞれの強さを指定して色を指定します。
		/// </summary>
		/// <param name="red">赤の強度を設定します。0 から 255 迄の値を設定します。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
		/// <param name="green">緑の強度を設定します。0 から 255 迄の値を設定します。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
		/// <param name="blue">青の強度を設定します。0 から 255 迄の値を設定します。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
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
		/// mwg.Drawing.Color コンストラクタ。与えられた情報から特定の色を生成し、生成した色を保持する mwg.Drawing.Color インスタンスを作成します。
		/// 色光の三原色RGB―赤(red)、緑(green)、青(blue)―それぞれの強さを指定して色を指定します。
		/// </summary>
		/// <param name="red">赤の強度を設定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。</param>
		/// <param name="green">緑の強度を設定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。</param>
		/// <param name="blue">青の強度を設定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。</param>
		public Color(byte red,byte green,byte blue){
			r=red;
			g=green;
			b=blue;
			a=(byte)0;
		}
		/// <summary>
		/// mwg.Drawing.Color コンストラクタ。与えられた情報から特定の色を生成し、生成した色を保持する mwg.Drawing.Color インスタンスを作成します。
		/// 色光の三原色RGBとα値A―赤(red)、緑(green)、青(blue)、透明度(transparence)―それぞれの強さを指定して色を指定します。
		/// </summary>
		/// <param name="red">赤の強度を設定します。0 から 255 迄の値を設定します。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
		/// <param name="green">緑の強度を設定します。0 から 255 迄の値を設定します。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
		/// <param name="blue">青の強度を設定します。0 から 255 迄の値を設定します。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
		/// <param name="alpha">透明度を指定します。0 から 255 迄の値を設定します。0 を指定した時には、全く透明でない、則ち標準的な不透明の色となります。
		/// 255 を指定した時には完全に透明な色を表します。完全に透明な時には、red,green,blue は生成される色に実質的に影響を持ちません。0 より小さい値を設定した時は 0 として、255 より大きい値を設定した時は 255 として処理します。</param>
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
		/// mwg.Drawing.Color コンストラクタ。与えられた情報から特定の色を生成し、生成した色を保持する mwg.Drawing.Color インスタンスを作成します。色光の三原色RGBとα値A―赤(red)、緑(green)、青(blue)、透明度(transparence)―それぞれの強さを指定して色を指定します。
		/// </summary>
		/// <param name="red">赤の強度を設定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。</param>
		/// <param name="green">緑の強度を設定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。</param>
		/// <param name="blue">青の強度を設定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。</param>
		/// <param name="alpha">透明度(transparence)を指定します。Binary 値を 0 から 255 迄の値に置き換えて読み取ります。0 を指定した時には、全く透明でない、則ち標準的な不透明の色となります。
		/// 255 を指定した時には完全に透明な色を表します。完全に透明な時には、red,green,blue は生成される色に実質的に影響を持ちません。</param>
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
		//■色名を指定して、色を定義したファイルを検索して、色を設定するコンストラクタ。

		//=====================================
		//          properties
		//-------------------------------------
		/// <summary>
		/// 保持している色の赤の強度を取得または設定します。0 が最も弱く、255 が最も強いことを表します。
		/// </summary>
		public byte Red{
			get{return r;}
			set{r=value;}
		}
		/// <summary>
		/// 保持している色の緑の強度を取得または設定します。0 が最も弱く、255 が最も強いことを表します。
		/// </summary>
		public byte Green{
			get{return g;}
			set{g=value;}
		}
		/// <summary>
		/// 保持している色の青の強度を取得または設定します。0 が最も弱く、255 が最も強いことを表します。
		/// </summary>
		public byte Blue{
			get{return b;}
			set{b=value;}
		}
		/// <summary>
		/// 保持している色の透明度を取得または設定します。
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
		/// 加法混色を行います。交換法則が成立します(<code>color1 + color2 == color2 + color1</code>)。
		/// 零元には「黒」または「完全に透明な色」が相当します。残念ながら結合法則は不成立です。
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
		/// 加法混色の逆演算を行います。つまり、<code>(color1 + color2) - color2 == color1</code>が(丸め誤差を無視すれば)成立します。
		/// 当然交換法則は不成立となります。結合法則は成立しません。
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
		//■減法混色
		//■平均
		/// <summary>
		/// 独自の色を明示的に .NET Framework の System.Drawing.Color に変換します。情報の変質及び増減は在りません。
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
		//自分で定義したリストに置き換えるのが理想。
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
			//TODO: 新しく色データを登録する手続きを書く。
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
	// それぞれの要素の型変換をして、コピー(変換先の型の別に関数を作るなどする)
	// 或る部分から或る部分までの範囲の情報をコピー

	//mwqXml
	//基本的にツリー構造を処理する物
	//XMLでの書き出しや読込を行えるようにする

	//mwgHtml
	//mwqXmlと殆ど同じだが、属性にクオテーションを使ってない、始まりのタグと終わりのタグの大文字小文字が一致しない、始まりのタグに対して、終わりのタグがない、等のことに対して寛容なクラス。

	public class StringDrawer{
		#region 静的定数
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
		/// レンダリングの方法を取得亦は設定します
		/// </summary>
		public System.Drawing.Text.TextRenderingHint RenderingHint{
			get{return this.g.TextRenderingHint;}
			set{this.g.TextRenderingHint=value;}
		}

		//***********************************************************
		//		<Field>	Brush	brush
		//-----------------------------------------------------------
		//TODO:他の種類のにも対応
		System.Drawing.Brush brush;
		/*public System.Drawing.Color ForeColor{
			get{return this.brush.Color;}
			set{this.brush.Color=value;}
		}//*/

		//TODO:背景色(画像)や下線、枠線等にも対応する
		//→背景 文字の段階で色を付ける//×画像にした時のずれが
		//→装飾 Drawing.StringDrawer.Char->DrawTo を Rectangle を返す物に変更して、それを処理
		//***********************************************************
		//		<Field>	PointF	strPosition
		//-----------------------------------------------------------
		/// <summary>
		/// 書き込み位置
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
		/// 折り返すかどうかを設定します
		/// </summary>
		[System.Runtime.CompilerServices.AccessedThroughProperty("Return")]
		private bool fReturn=false;
		/// <summary>
		/// 折り返すかどうかを取得亦は設定します
		/// Rectangle が設定されていない場合は、内部的には折り返しが有効となっていても、出力は折り返しなしの時と同じになります
		/// </summary>
		public bool Return{
			get{return this.setRectangle&&this.fReturn;}
			set{this.fReturn=value;}
		}
		/// <summary>
		/// 描画領域を設定します
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
		/// 行と行の間を設定します
		/// </summary>
		public int LineSpacing=0;
		//TODO:
		//タブの長さ=基準線の間隔 も設定する
		//→基準線を自分で任意に設定する事も出来る様にする。(等間隔モード、カスタムモード)
		//→ [nつ目のTab]=[n本目の基準線に移動] か、[Tab]=[次の基準線に移動] の二種類考える事が出来る

		#region 改行処理 Rt Cr Lf
		/// <summary>
		/// 改行します
		/// </summary>
		private void Rt(){
			this.Cr();
			this.Lf();
			this.OnPositionReturned();
		}
		/// <summary>
		/// CR を実行します。書き込み位置を行頭に移動します。
		/// </summary>
		/// <returns>
		/// CR を実行したか否かを返します。
		/// 初めから行頭にあった場合には実行しません。
		/// また、改行が無効になっている場合も実行しません。
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
		/// 行送りをします。
		/// </summary>
		/// <param name="px">現在の行の高さを設定します</param>
		private void Lf(int px){
			if(this.Vertical){
				this.strPosition.X+=(this.RTL)?-px-this.LineSpacing:px+this.LineSpacing;
			}else{
				this.strPosition.Y+=px+this.LineSpacing;
			}
		}
		/// <summary>
		/// 行送りをします。
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
		/// 書く方向が右から左であるかを取得亦は設定します
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
		/// 書く方向が上から下である事を取得亦は設定します
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
				// 文字の大きさを測る基準を取得
				fontSizeX=this.g.MeasureString("x",this.font);
				fontSizeK=this.g.MeasureString("漢",this.font);
				fontSizeXV=this.g.MeasureString("x",this.font,point0,formatV);
				fontSizeKV=this.g.MeasureString("漢",this.font,point0,formatV);
				this.OnFontChanged();
			}
		}
		/// <summary>
		/// 文字の大きさによって決まる、行の高さ
		/// </summary>
		//TODO:行の高さを自分でも決められる様にする。(要flag:自動⇔カスタム)
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
			this.Font=new System.Drawing.Font("ＭＳ ゴシック",9);
			this.brush=System.Drawing.Brushes.Black;
			this.strPosition=new System.Drawing.PointF(0,0);
			this.format=new System.Drawing.StringFormat();
		}

		//***********************************************************
		//		<method>	DrawString
		//-----------------------------------------------------------
		//TODO:均等割付に対応する
		//TODO:幅を計るだけの DrawString,DrawLetter (Measure)も用意する
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
			//※:これは上の switch と同じ内容
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
				case 9:this.DrawLetter('\x2409');break;//水平タブ
				case 10:this.Lf();break;//LF
				case 11:this.DrawLetter('\x240b');break;//垂直タブ
				case 12:this.DrawLetter('\x240c');break;
				case 13:this.Cr();this.Lf();break;//CR/改行
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

		#region 文字の配置 TODO
		private void DrawLetter(string text){
			this.DrawLetter(new Char(this,text));
		}
		private void DrawLetter(char c){
			this.DrawLetter(new Char(this,c.ToString()));
		}
		private void DrawLetter(StringDrawer.Char c){
			//TODO:改行・折り返しに関して実装すべき物
			//　禁則処理
			//	ワードラップ
			//	横スクロール可能の時には使えない様にしないと変な事になる
			//	途中で文字の大きさが変わった時の行送りに対応する
			if(this.Vertical&&this.RTL){
				//折り返し処理
				if(this.Return&&this.Rectangle.Bottom<this.strPosition.Y+c.Height){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X-c.Width,this.strPosition.Y);
				this.strPosition.Y+=c.Height+this.LetterSpacing;
			}else if(this.RTL){
				//TEST:折り返し処理
				if(this.Return&&this.Rectangle.Left>this.strPosition.X){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X,this.strPosition.Y);
				this.strPosition.X-=c.Width+this.LetterSpacing;
			}else if(this.Vertical){
				//折り返し処理
				if(this.Return&&this.Rectangle.Bottom<this.strPosition.Y+c.Height){
					if(this.Cr())this.Lf();
				}
				c.DrawTo(this.g,this.strPosition.X+this.LineHeight-c.Width,this.strPosition.Y);
				this.strPosition.Y+=c.Height+this.LetterSpacing;
			}else{
				//折り返し処理
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
		//		<method:変換>	
		//-----------------------------------------------------------
		/// <summary>
		/// IME 等に渡す為の LOGFONT を取得するのに使用します
		/// </summary>
		/// <returns>LOGFONT を返します</returns>
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
		/// 文字の画像を作成し、情報を提供するクラス。画像の書き出しも行う。
		/// 制御文字などを扱う際には、制御の対象となる文字と併せて、一つの物として扱う
		/// </summary>
		private class Char{
			//部首構成*12
			const char CMB_LR='\x2ff0';//左,右
			const char CMB_TB='\x2ff1';//上,下
			const char CMB_LCR='\x2ff2';//左,中,右
			const char CMB_TMB='\x2ff3';//上,中,下
			const char CMB_OI='\x2ff4';//外,中
			const char CMB_OB='\x2ff5';//外,下
			const char CMB_OT='\x2ff6';//外,上
			const char CMB_OR='\x2ff7';//外,右
			const char CMB_ORB='\x2ff8';//外,右下
			const char CMB_OLB='\x2ff9';//外,左下
			const char CMB_ORT='\x2ffa';//外,右上
			const char CMB_LTRB='\x2ffb';//左上,右下
			/// <summary>
			/// それぞれの部首構成に対して、構成要素の文字が何処に位置するかを設定。
			/// 四つのfloatで一つの構成要素の位置と大きさを決める。
			/// (一つ目が左の位置、二つ目が右の位置、三つ目が幅、四つ目が高さ)
			/// それぞれの数字は、完成後の漢字の幅・高さに対する比で表す。
			/// </summary>
			static float[][] CMB_RATES={
				new float[]{0f,0f,0.54f,1f,0.46f,0f,0.54f,1f},//左,右
				new float[]{0f,0f,1f,0.56f,0f,0.44f,1f,0.56f},
				new float[]{0f,0f,0.36f,1f,0.32f,0f,0.36f,1f,0.64f,0f,0.36f,1f},
				new float[]{0f,0f,1f,0.38f,0f,0.31f,1f,0.38f,0f,0.62f,1f,0.38f},
				new float[]{0f,0f,1f,1f,0.15f,0.15f,0.7f,0.7f},//外,中
				new float[]{0f,0f,1f,1f,0.25f,0.5f,0.5f,0.5f},
				new float[]{0f,0f,1f,1f,0.15f,0f,0.7f,0.7f},
				new float[]{0f,0f,1f,1f,0.3f,0.15f,0.7f,0.7f},
				new float[]{0f,0f,1f,1f,0.3f,0.3f,0.7f,0.7f},//外,右下
				new float[]{0f,0f,1f,1f,0f,0.3f,0.7f,0.7f},
				new float[]{0f,0f,1f,1f,0.3f,0f,0.7f,0.7f},
				new float[]{0f,0f,0.7f,0.7f,0.3f,0.3f,0.7f,0.7f}//左上,右下
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
				//TODO:漢字の構成の記号→bool のプロパティを実装
				//TODO:草冠など、特別の物は比率を変える
				if(CMB_LR<=text[i]&&text[i]<=CMB_LTRB){
					this.initialize(parent,"　");
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
				//中心文字の取得
				string x=text[i++].ToString();
				//修飾子の付加
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
				//--文字の大きさを取得
				string text2=
					(text==" "||text=="")?"x":
					(text=="　")?"空":
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
				//--各種変数の設定
				this.Height=(int)inner.Height;
				this.Width=(int)inner.Width;
				this.bmp=new System.Drawing.Bitmap(
					(int)outer.Width,(int)outer.Height,
					System.Drawing.Imaging.PixelFormat.Format32bppArgb
				);
				//--文字の描画
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