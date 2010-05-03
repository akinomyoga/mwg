using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace mwgDictionary{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class DicMain : System.Windows.Forms.Form{
		private System.Windows.Forms.TextBox text1;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabEdit;
		private System.Windows.Forms.TabPage tabView;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ImageList imageList1;
		private mwg.Controls.WebBrowser.WebView mwgWebView1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.ComponentModel.IContainer components;

		public DicMain(){
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DicMain));
			this.text1 = new System.Windows.Forms.TextBox();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabView = new System.Windows.Forms.TabPage();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.tabEdit = new System.Windows.Forms.TabPage();
			this.mwgWebView1 = new mwg.Controls.WebBrowser.WebView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.tabMain.SuspendLayout();
			this.tabView.SuspendLayout();
			this.tabEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// text1
			// 
			this.text1.Dock = System.Windows.Forms.DockStyle.Left;
			this.text1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.text1.Location = new System.Drawing.Point(0, 0);
			this.text1.Multiline = true;
			this.text1.Name = "text1";
			this.text1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.text1.Size = new System.Drawing.Size(360, 322);
			this.text1.TabIndex = 2;
			this.text1.Text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<HTML>\r\n<HEAD>\r\n<TITLE></TITLE>\r\n</HEAD>\r" +
				"\n<BODY>\r\n</BODY>\r\n</HTML>";
			this.text1.WordWrap = false;
			this.text1.Leave += new System.EventHandler(this.text1_Leave);
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabView);
			this.tabMain.Controls.Add(this.tabEdit);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.ImageList = this.imageList1;
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(880, 349);
			this.tabMain.TabIndex = 5;
			// 
			// tabView
			// 
			this.tabView.Controls.Add(this.treeView1);
			this.tabView.ImageIndex = 0;
			this.tabView.Location = new System.Drawing.Point(4, 23);
			this.tabView.Name = "tabView";
			this.tabView.Size = new System.Drawing.Size(872, 322);
			this.tabView.TabIndex = 1;
			this.tabView.Text = "検索";
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(184, 322);
			this.treeView1.TabIndex = 0;
			// 
			// tabEdit
			// 
			this.tabEdit.Controls.Add(this.mwgWebView1);
			this.tabEdit.Controls.Add(this.splitter1);
			this.tabEdit.Controls.Add(this.text1);
			this.tabEdit.ImageIndex = 1;
			this.tabEdit.Location = new System.Drawing.Point(4, 23);
			this.tabEdit.Name = "tabEdit";
			this.tabEdit.Size = new System.Drawing.Size(872, 322);
			this.tabEdit.TabIndex = 0;
			this.tabEdit.Text = "編集";
			// 
			// mwgWebView1
			// 
			//this.mwgWebView1.bodyInnerHTML = null;
			this.mwgWebView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mwgWebView1.LocationUrl = "about:blank";
			this.mwgWebView1.Location = new System.Drawing.Point(368, 0);
			this.mwgWebView1.Name = "mwgWebView1";
			this.mwgWebView1.ShowStatusBar = false;
			this.mwgWebView1.ShowTitleBar = false;
			this.mwgWebView1.ShowToolBar = false;
			this.mwgWebView1.Size = new System.Drawing.Size(504, 322);
			this.mwgWebView1.StatusText = null;
			this.mwgWebView1.TabIndex = 6;
			this.mwgWebView1.DocumentTitle = "";
			//this.mwgWebView1.BeforeRefresh += new mwg.Controls.WebBrowser.BeforeRefreshEventHandler(this.mwgWebView1_BeforeRefresh);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(360, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 322);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Empty;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "ファイル(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "印刷(&P)";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "言語(&V)";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// DicMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(880, 349);
			this.Controls.Add(this.tabMain);
			this.Menu = this.mainMenu1;
			this.Name = "DicMain";
			this.Text = "茗荷事典:mwgDictionary";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabMain.ResumeLayout(false);
			this.tabView.ResumeLayout(false);
			this.tabEdit.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new DicMain());
		}
		//=====================================
		//          field
		//-------------------------------------
		string path;
		private void Form1_Load(object sender, System.EventArgs e){
			path=System.Reflection.Assembly.GetExecutingAssembly().Location;
			path=path.Substring(0,path.LastIndexOf("\\")+1);
			this.mwgWebView1.LocationUrl=path+"temp\\temp.htm";
			
			using(System.IO.StreamReader sr=new System.IO.StreamReader(this.mwgWebView1.LocationUrl)){
				string str;this.text1.Clear();
				while((str=sr.ReadLine())!=null)this.text1.AppendText(str+"\n");
			}
		}

		//=====================================
		//          EventHandler
		//-------------------------------------
		/*
		private void mwgWebView1_BeforeRefresh(object sender, mwg.Controls.WebBrowser.BeforeRefreshEventArgs e){
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path+"temp\\temp.htm")){
				for(int i=0;i<this.text1.Lines.Length;i++)sw.WriteLine(this.text1.Lines[i]);
			}
		}
		//*/

		private void text1_Leave(object sender, System.EventArgs e){
			this.mwgWebView1.document.body.innerHTML=this.text1.Text;
		}

		private void menuItem2_Click(object sender, System.EventArgs e){
			this.mwgWebView1.CmdPrint();
		}

		private void menuItem3_Click(object sender, System.EventArgs e){
			this.mwgWebView1.window.execScript("new String()");
			//Console.WriteLine((r==null)?"null":r.GetType().ToString());
		}
		//=====================================
		//          functions 機能
		//-------------------------------------

	}
}
