using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace mwg.Windows{
	/// <summary>
	/// mwgWebView の概要の説明です。
	/// </summary>
	[System.Drawing.ToolboxBitmapAttribute(typeof(mwg.Windows.mwgWebView),"mwgWebView.bmp")]
	public class mwgWebView:System.Windows.Forms.UserControl{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel status;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label lTitle;
		private AxSHDocVw.AxWebBrowser WebBrowser;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.ComponentModel.IContainer components;

		public mwgWebView(){
			InitializeComponent();
			this.WebBrowser.Navigate("about:blank");
		}
		public mwgWebView(string url){
			InitializeComponent();
			this.plocation=url;
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			Console.WriteLine("Window Handle of WebBrowser: "+this.WebBrowser.Handle.ToString());
			//AxWebBrowserに強制的に WindowHandle を取得させるため。
			//補足: 何故だか、'ウィンドウハンドルのない ActiveX コントロールはだめ'と出るので止むなく。
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
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.status = new System.Windows.Forms.StatusBarPanel();
			this.lTitle = new System.Windows.Forms.Label();
			this.WebBrowser = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WebBrowser)).BeginInit();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButton4,
																						this.toolBarButton5,
																						this.toolBarButton1,
																						this.toolBarButton2,
																						this.toolBarButton3,
																						this.toolBarButton6});
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
			// toolBarButton4
			// 
			this.toolBarButton4.ImageIndex = 2;
			this.toolBarButton4.ToolTipText = "戻る";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.ImageIndex = 5;
			this.toolBarButton5.ToolTipText = "進む";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.ToolTipText = "更新";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.ImageIndex = 1;
			this.toolBarButton2.ToolTipText = "中止";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.ImageIndex = 3;
			this.toolBarButton3.ToolTipText = "ホーム";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.ImageIndex = 6;
			this.toolBarButton6.ToolTipText = "検索";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Empty;
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
			this.lTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// WebBrowser
			// 
			this.WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebBrowser.Enabled = true;
			this.WebBrowser.Location = new System.Drawing.Point(0, 44);
			this.WebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebBrowser.OcxState")));
			this.WebBrowser.Size = new System.Drawing.Size(496, 222);
			this.WebBrowser.TabIndex = 13;
			this.WebBrowser.StatusTextChange += new AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEventHandler(this.WebBrowser_StatusTextChange);
			this.WebBrowser.TitleChange += new AxSHDocVw.DWebBrowserEvents2_TitleChangeEventHandler(this.WebBrowser_TitleChange);
			this.WebBrowser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.WebBrowser_DocumentComplete);
			// 
			// mwgWebView
			// 
			this.Controls.Add(this.WebBrowser);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.lTitle);
			this.Name = "mwgWebView";
			this.Size = new System.Drawing.Size(496, 288);
			((System.ComponentModel.ISupportInitialize)(this.status)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WebBrowser)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		
		
		private string plocation;
		//=====================================
		//          EventHandler
		//-------------------------------------
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e){
			switch(e.Button.ToolTipText){
				case "更新":this.CmdRefresh();break;
				case "中止":this.CmdStop();break;
				case "ホーム":this.CmdHome();break;
				case "戻る":this.CmdBack();break;
				case "進む":this.CmdForward();break;
				case "検索":this.CmdSearch();break;
					//case "プロパティ":this.WebBrowser.;break;
				case "保存":this.CmdSaveAs();break;
				default:
					//未設定のボタンです
					break;
			}
		}
		private void WebBrowser_StatusTextChange(object sender, AxSHDocVw.DWebBrowserEvents2_StatusTextChangeEvent e){this.cStatusRenew(e.text);}
		private void WebBrowser_TitleChange(object sender, AxSHDocVw.DWebBrowserEvents2_TitleChangeEvent e){this.cTitleRenew(e.text);}
		//=====================================
		//          機能
		//-------------------------------------
		private void cRefresh(){//cは command の略のつもり
			this.OnBeforeRefresh();
			this.WebBrowser.Refresh();
		}
		private void cStatusRenew(string text){
			this.statusBar1.Panels[0].Text=this.statusBar1.Panels[0].ToolTipText=text;
		}
		private void cTitleRenew(string text){
			this.lTitle.Text=text;
		}
		private void cDeleteCommercial(){
			mshtml.HTMLDocument doc=(mshtml.HTMLDocument)this.WebBrowser.Document;
			//doc.body.innerHTML;
		}
		private void ExecCommand(SHDocVw.OLECMDID id){
			this.WebBrowser.ExecWB(id,SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT);
		}
		public void CmdStop(){this.WebBrowser.Stop();}
		public void CmdHome(){this.WebBrowser.GoHome();}
		public void CmdForward(){
			try{this.WebBrowser.GoForward();}catch(System.Exception e){this.StatusText=e.Message+" : これ以上進めません!";}
		}
		public void CmdBack(){
			try{this.WebBrowser.GoBack();}catch(System.Exception e){this.StatusText=e.Message+" : これ以上戻れません!";}
		}
		public void CmdSearch(){this.WebBrowser.GoSearch();}
		public void CmdRefresh(){this.OnBeforeRefresh();this.WebBrowser.Refresh();}
		public void CmdPrint(){this.ExecCommand(SHDocVw.OLECMDID.OLECMDID_PRINT);}
		public void CmdSaveAs(){this.ExecCommand(SHDocVw.OLECMDID.OLECMDID_SAVEAS);}
		public void CmdSave(){this.ExecCommand(SHDocVw.OLECMDID.OLECMDID_SAVE);}
		public void CmdPrintPreview(){this.ExecCommand(SHDocVw.OLECMDID.OLECMDID_PRINTPREVIEW);}
		public void CmdFind(){this.ExecCommand(SHDocVw.OLECMDID.OLECMDID_FIND);}
		public object ExecScript(string script){
			return this.ExecScript(script,mwgWebView.ScriptLanguage.JScript);
		}
		public object ExecScript(string script,mwg.Windows.mwgWebView.ScriptLanguage lang){
			mshtml.HTMLDocument d=(mshtml.HTMLDocument)this.WebBrowser.Document;
			if(d==null)return null;
			return d.parentWindow.execScript(script,lang.ToString());
		}
		public enum ScriptLanguage{JScript,javascript,vbscript}
		private object ShowUI(mwgWebView.UIName ui){
			return this.ExecScript("window.external.showBrowserUI('"+ui.ToString()+"',null);");
		}
		public enum UIName{LanguageDialog,OrganizeFavorites,PrivacySettings,ProgramAccessAndDefaults}
		public void ShowLanguages(){this.ShowUI(mwgWebView.UIName.LanguageDialog);}
		public void ShowFavorites(){this.ShowUI(mwgWebView.UIName.OrganizeFavorites);}
		public void ShowPrivacies(){this.ShowUI(mwgWebView.UIName.PrivacySettings);}
		public void ShowPrograms(){this.ShowUI(mwgWebView.UIName.ProgramAccessAndDefaults);}
		//=====================================
		//          property
		//-------------------------------------
		public bool ShowTitleBar{
			get{return this.lTitle.Visible;}
			set{this.lTitle.Visible=value;}
		}
		public bool ShowToolBar{
			get{return this.toolBar1.Visible;}
			set{this.toolBar1.Visible=value;}
		}
		public bool ShowStatusBar{
			get{return this.statusBar1.Visible;}
			set{this.statusBar1.Visible=value;}
		}
		public string TitleText{
			get{return (this.document==null)?this.lTitle.Text:this.document.title;}
			set{
				this.lTitle.Text=value;
#warning title 非設定
#if !DEBUG
				if(this.document!=null)this.document.title=value;
#endif
			}
		}
		public string StatusText{
			get{return (this.document==null)?this.status.Text:this.document.parentWindow.status;}
			set{
				this.status.Text=value;
				if(this.document!=null)this.document.parentWindow.status=value;
			}
		}
		public string location{
			get{return this.plocation;}
			set{
				if(value=="")value="about:blank";
				this.plocation=value;
				this.WebBrowser.Navigate(value);
			}
		}
		[System.ComponentModel.Browsable(false)]
		public string bodyInnerHTML{
			get{return ((mshtml.HTMLDocument)this.WebBrowser.Document).body.innerHTML;}
			set{
				mshtml.HTMLDocument doc=(mshtml.HTMLDocument)this.WebBrowser.Document;
				if(doc!=null&&doc.body!=null)doc.body.innerHTML=value;
			}
		}
		[System.ComponentModel.Browsable(false)]
		public mshtml.HTMLDocument document{
			get{return (mshtml.HTMLDocument)this.WebBrowser.Document;}
		}
		[System.ComponentModel.Browsable(false)]
		public mshtml.IHTMLWindow2 window{
			get{
				try{return this.document.parentWindow;}catch{return null;}
			}
		}
		//=====================================
		//          Event
		//-------------------------------------
		//DownloadComplete;
		/// <summary>
		/// ダウンロードが完了した際に、点火せらるイベント。
		/// </summary>
		public event DownloadCompleteEventHandler DownloadComplete;
		public class DownloadCompleteEventArgs:System.EventArgs{
			//downloadに纏わる情報を追加できます。
			public DownloadCompleteEventArgs(){
				
			}
		}
		public delegate void DownloadCompleteEventHandler(object sender,DownloadCompleteEventArgs e);
		protected void OnDownloadComplete(){
			if(this.DownloadComplete!=null){
				this.DownloadComplete(this,new DownloadCompleteEventArgs());
				// TODO: eを作成しなければならない
			}
		}
		//event BeforeRefresh;
		/// <summary>
		/// 表示しているコンテンツの更新を行う前に、点火せらるイベント。
		/// </summary>
		public event BeforeRefreshEventHandler BeforeRefresh;
		// TODO: 新しい Thread で行う為に全然 Before となりえない。実装の方法を検討すべし。
		public class BeforeRefreshEventArgs:System.EventArgs{
			private string loc;
			public BeforeRefreshEventArgs(AxSHDocVw.AxWebBrowser wb){
				//LocationURL など 自動的に取得しなければならない
				this.loc=wb.LocationURL;
			}
			public BeforeRefreshEventArgs(string a){
				this.loc=a;
			}
			public string Location{
				get{return this.loc;}
			}
		}
		public delegate void BeforeRefreshEventHandler(object sender,BeforeRefreshEventArgs e);
		protected void OnBeforeRefresh(){
			if(this.BeforeRefresh!=null){
				BeforeRefresh(this,new BeforeRefreshEventArgs(this.WebBrowser.LocationURL));
			}
		}
		// Event を関連付けるテスト
		private void WebBrowser_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e){
			mshtml.HTMLDocumentEvents2_Event iEvent=(mshtml.HTMLDocumentEvents2_Event)this.document;
			iEvent.onclick+=new mshtml.HTMLDocumentEvents2_onclickEventHandler(ClickEventHandler);
			//iEvent.onmouseover+=new mshtml.HTMLDocumentEvents2_onmouseoverEventHandler(MouseOverEventHandler);
			Console.WriteLine("hooked");
		}
		private bool ClickEventHandler(mshtml.IHTMLEventObj e){
			Console.WriteLine(e.x.ToString()+":"+e.y.ToString());
			return true;
		}
	}
}
