using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Wb=mwg.Controls.WebBrowser;
using Forms=System.Windows.Forms;
using Interop=System.Runtime.InteropServices;

namespace Debug{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class Form1:System.Windows.Forms.Form{
		private System.Windows.Forms.Button bWriteTest;
		private mwg.Windows.TextBox textBox1;
		private System.Windows.Forms.Button bTest0;
		private WebBrowser webBrowser1;
		private Button button1;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1(){
			InitializeComponent();
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent(){
			this.bWriteTest=new System.Windows.Forms.Button();
			this.textBox1=new mwg.Windows.TextBox();
			this.bTest0=new System.Windows.Forms.Button();
			this.webBrowser1=new System.Windows.Forms.WebBrowser();
			this.button1=new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// bWriteTest
			// 
			this.bWriteTest.Location=new System.Drawing.Point(352,8);
			this.bWriteTest.Name="bWriteTest";
			this.bWriteTest.Size=new System.Drawing.Size(88,24);
			this.bWriteTest.TabIndex=1;
			this.bWriteTest.Text="Test 書き込み";
			this.bWriteTest.Click+=new System.EventHandler(this.bWriteTest_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor=System.Drawing.Color.White;
			this.textBox1.EnableImgBuf=true;
			this.textBox1.Location=new System.Drawing.Point(8,8);
			this.textBox1.Name="textBox1";
			this.textBox1.RenderingHint=System.Drawing.Text.TextRenderingHint.SystemDefault;
			this.textBox1.Size=new System.Drawing.Size(248,184);
			this.textBox1.TabIndex=0;
			this.textBox1.TextFont=new System.Drawing.Font("ＭＳ ゴシック",9.75F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(128)));
			// 
			// bTest0
			// 
			this.bTest0.Location=new System.Drawing.Point(352,56);
			this.bTest0.Name="bTest0";
			this.bTest0.Size=new System.Drawing.Size(88,24);
			this.bTest0.TabIndex=2;
			this.bTest0.Text="DelegateTest";
			this.bTest0.Click+=new System.EventHandler(this.bTest0_Click);
			// 
			// webBrowser1
			// 
			this.webBrowser1.Location=new System.Drawing.Point(8,198);
			this.webBrowser1.MinimumSize=new System.Drawing.Size(20,20);
			this.webBrowser1.Name="webBrowser1";
			this.webBrowser1.Size=new System.Drawing.Size(250,188);
			this.webBrowser1.TabIndex=3;
			// 
			// button1
			// 
			this.button1.Location=new System.Drawing.Point(264,198);
			this.button1.Name="button1";
			this.button1.Size=new System.Drawing.Size(84,25);
			this.button1.TabIndex=4;
			this.button1.Text="Test";
			this.button1.UseVisualStyleBackColor=true;
			this.button1.Click+=new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,12);
			this.ClientSize=new System.Drawing.Size(448,433);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.bTest0);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.bWriteTest);
			this.Name="Form1";
			this.Text="Form1";
			this.Load+=new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// main EntryPoint
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}

		private void bWriteTest_Click(object sender, System.EventArgs e){
			this.textBox1.Append('\x2ff1'.ToString()+'\x2ff0'.ToString()+"龍龍"+'\x2ff0'.ToString()+"龍龍");
			this.textBox1.Append('\x2ff0'.ToString()+"屯邑"+'\x2ff2'.ToString()+"水束頁");
			this.textBox1.Append('\x2ff3'.ToString()+"雲"+'\x2ff2'.ToString()+"雲龍雲"+'\x2ff0'.ToString()+"龍龍");
			this.textBox1.Append("アプリケーションのメイン エントリ ポイントです。");
		}
		private mwg.Windows.KeyManager kManager;
		private void bTest0_Click(object sender, System.EventArgs e){
			Console.WriteLine("This is method to invoke test function.");
			this.kManager=new mwg.Windows.KeyManager(this.textBox1);
			this.kManager.KeyDown+=new KeyEventHandler(kManager_KeyDown);
		}
		private void kManager_KeyDown(object sender, KeyEventArgs e){
			Console.WriteLine(e.KeyCode);
		}

		private void Form1_Load(object sender,EventArgs e) {
			this.webBrowser1.Navigate("about:blank");
		}

		private delegate object DRV();
		private void button1_Click(object sender,EventArgs e) {
			/*
			Wb::Document doc=new mwg.Controls.WebBrowser.Document(this.webBrowser1.Document);
			this.webBrowser1.Document.Write(doc.body.scrollTop.ToString());
			//Wb::Window win=new Wb::Window(this.webBrowser1.Document.Window);
			doc.body.attachEvent("onclick",delegate(){
				Forms::MessageBox.Show("This is C#!");
				return false;
			});
			//*/
			
			Wb::Window window=new Wb::Window(this.webBrowser1.Document.Window);
			window.document.body.onclick+=delegate(){
				Forms::MessageBox.Show("Clicked!");
				return false;
			};
			window.document.body.attachEvent("onclick",(System.Action<Wb::Event>)delegate(Wb::Event ev){
				Forms::MessageBox.Show(string.Format("Click ({0},{1})",ev.x,ev.y));
			});

#if DELEGTEST
			//Test - Delegate Export
			Wb::Window window=new Wb::Window(this.webBrowser1.Document.Window);
			Wb::Document document=new mwg.Controls.WebBrowser.Document(this.webBrowser1.Document);
			Wb::ScriptObject f=window.ToScriptObject((DRV)delegate(){
				Forms::MessageBox.Show("Hello!");
				return 100;
			});
			Wb::ScriptObject obj_win=window.ToScriptObject(window);
			obj_win["f"]=f;
			window.eval("alert(f());");
#endif

#if EXECTEST
			ExecTest test=new ExecTest();
			document.body.setAttribute("<mwg::init>",test,0);
			window.execScript("document.body['<mwg::init>'].X(document.body['<mwg::init>']);");
			window.execScript("document.body['<mwg::init>'].getProp2=function(a,b,c){return this.Prop1*2;};","javascript");
			// × window.execScript("var ret=document.body['<mwg::init>'].getProp2(1,2,3);alert(ret);","javascript");
			//window.execScript("var ret=document.body['<mwg::init>'].getProp2.apply(document.body['<mwg::init>'],[]);alert(ret);","javascript");
			//window.execScript("var proc=document.body['<mwg::init>'].getProp2;alert(proc.apply(document.body['<mwg::init>'],[]));","javascript");
#endif
		}
	}
#if EXECTEST
	[Interop::ComVisible(true)]
	public class ExecTest{
		public void F(){
			Forms::MessageBox.Show("This is F.");
		}
		public void G(int x){
			Forms::MessageBox.Show("This is G("+x.ToString()+")");
		}
		public void X(ExecTest test){
			Forms::MessageBox.Show("This is X("+(test==null?"null":"test")+")");
		}
		private int prop1=10;
		public int Prop1{
			get{return prop1;}
			set{this.prop1=value;}
		}

		// javascript function を設定
		private object _getProp2;
		public object getProp2{
			get{return this._getProp2;}
			set{this._getProp2=value;}
		}

		// [] operator ?
		public object item(ref object key){
			Forms::MessageBox.Show("called item:"+(key==null?"null":key.ToString()));
			return 1;
		}
		public object this[object key]{
			get{
				Forms::MessageBox.Show("called [].get:"+(key==null?"null":key.ToString()));
				return 1;
			}
		}
	}
#endif
}
