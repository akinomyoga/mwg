using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace mwgDictionary
{
	/// <summary>
	/// mwgWebView の概要の説明です。
	/// </summary>
	public class mwgWebView : System.Windows.Forms.UserControl
	{
		private AxSHDocVw.AxWebBrowser WebBrowser;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel status;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label lTitle;
		private System.ComponentModel.IContainer components;

		public mwgWebView(string url)
		{
			// この呼び出しは、Windows.Forms フォーム デザイナで必要です。
			InitializeComponent();
			this.location=url;
			// TODO: InitializeComponent 呼び出しの後に初期化処理を追加します。

		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(mwgWebView));
			this.WebBrowser = new AxSHDocVw.AxWebBrowser();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.status = new System.Windows.Forms.StatusBarPanel();
			this.lTitle = new System.Windows.Forms.Label();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.WebBrowser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.status)).BeginInit();
			this.SuspendLayout();
			// 
			// WebBrowser
			// 
			this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebBrowser.Enabled = true;
			this.WebBrowser.Location = new System.Drawing.Point(0, 44);
			this.WebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebBrowser.OcxState")));
			this.WebBrowser.Size = new System.Drawing.Size(496, 222);
			this.WebBrowser.TabIndex = 9;
			this.WebBrowser.StatusTextChange += new AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEventHandler(this.WebBrowser_StatusTextChange);
			this.WebBrowser.TitleChange += new AxSHDocVw.DWebBrowserEvents2_TitleChangeEventHandler(this.WebBrowser_TitleChange);
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton1});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new System.Drawing.Point(0, 16);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(496, 28);
			this.toolBar1.TabIndex = 11;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.ToolTipText = "更新";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 266);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.status});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(496, 22);
			this.statusBar1.SizingGrip = false;
			this.statusBar1.TabIndex = 10;
			// 
			// status
			// 
			this.status.Width = 300;
			// 
			// lTitle
			// 
			this.lTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.lTitle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.lTitle.Location = new System.Drawing.Point(0, 0);
			this.lTitle.Name = "lTitle";
			this.lTitle.Size = new System.Drawing.Size(496, 16);
			this.lTitle.TabIndex = 12;
			this.lTitle.Text = "---";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Empty;
			// 
			// mwgWebView
			// 
			this.Controls.Add(this.WebBrowser);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.lTitle);
			this.Name = "mwgWebView";
			this.Size = new System.Drawing.Size(496, 288);
			((System.ComponentModel.ISupportInitialize)(this.WebBrowser)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.status)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		
		
		public string location;
		//=====================================
		//          EventHandler
		//-------------------------------------
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(e.Button.ToolTipText)
			{
				case "更新":this.cRefresh();break;
				default:
					//未設定のボタンです
					break;
			}
		}
		private void WebBrowser_StatusTextChange(object sender, AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEvent e){this.cStatusRenew(e.text);}
		private void WebBrowser_TitleChange(object sender, AxSHDocVw.DWebBrowserEvents2_TitleChangeEvent e){this.cTitleRenew(e.text);}
		//=====================================
		//          functions 機能
		//-------------------------------------
		private void cRefresh()
		{//cは command の略のつもり
			//■ここで、更新のボタンが押された事を通知する:event call
			this.WebBrowser.Refresh();
		}
		private void cStatusRenew(string text)
		{
			//Console.WriteLine("StatusRenew() Called ; "+System.DateTime.Now.ToString()+" ; "+text);
			this.statusBar1.Panels[0].Text=this.statusBar1.Panels[0].ToolTipText=text;
		}
		private void cTitleRenew(string text)
		{
			//Console.WriteLine("TitleRenew()  Called ; "+System.DateTime.Now.ToString()+" ; "+text);
			this.lTitle.Text=text;
		}
		
		
	}
}
