using Forms=System.Windows.Forms;

namespace mwg.Controls.WebBrowser{
	/// <summary>
	/// mwgWebView の概要の説明です。
	/// </summary>
	[System.Drawing.ToolboxBitmapAttribute(typeof(WebView),"WebView.bmp")]
	public class WebView:Forms::UserControl{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel status;
		private System.Windows.Forms.Label lTitle;
		private System.Windows.Forms.WebBrowser WebBrowser;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public WebView(){
			InitializeComponent();
			this.WebBrowser.Navigate("about:blank");
			this.WebBrowser.DocumentCompleted+=delegate(object sender,Forms::WebBrowserDocumentCompletedEventArgs e){
				this._document=new Document(this.WebBrowser.Document);
				this.lTitle.Text=this.document.title;

				// event test
				/*
				this.document.onclick+=delegate(Event e){
					System.Console.WriteLine(e.x.ToString()+":"+e.y.ToString());
					return true;
				};
				System.Console.WriteLine("hooked");
				//*/
			};
			this.WebBrowser.DocumentTitleChanged+=delegate(object sender,System.EventArgs e){
				this.lTitle.Text=this.WebBrowser.Document.Title;
			};
			this.WebBrowser.StatusTextChanged+=delegate(object sender,System.EventArgs e){
				this.statusBar1.Panels[0].Text
					=this.statusBar1.Panels[0].ToolTipText
					=this.WebBrowser.StatusText;
			};
		}

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent(){
			this.components=new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(WebView));
			this.toolBar1=new System.Windows.Forms.ToolBar();
			this.toolBarButton4=new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5=new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1=new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2=new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3=new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6=new System.Windows.Forms.ToolBarButton();
			this.imageList1=new System.Windows.Forms.ImageList(this.components);
			this.statusBar1=new System.Windows.Forms.StatusBar();
			this.status=new System.Windows.Forms.StatusBarPanel();
			this.lTitle=new System.Windows.Forms.Label();
			this.WebBrowser=new System.Windows.Forms.WebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.status)).BeginInit();
			this.SuspendLayout();
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance=System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton4,
            this.toolBarButton5,
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarButton3,
            this.toolBarButton6});
			this.toolBar1.DropDownArrows=true;
			this.toolBar1.ImageList=this.imageList1;
			this.toolBar1.Location=new System.Drawing.Point(0,16);
			this.toolBar1.Name="toolBar1";
			this.toolBar1.ShowToolTips=true;
			this.toolBar1.Size=new System.Drawing.Size(496,28);
			this.toolBar1.TabIndex=11;
			this.toolBar1.TextAlign=System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick+=new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.ImageIndex=2;
			this.toolBarButton4.Name="toolBarButton4";
			this.toolBarButton4.ToolTipText="戻る";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.ImageIndex=5;
			this.toolBarButton5.Name="toolBarButton5";
			this.toolBarButton5.ToolTipText="進む";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex=0;
			this.toolBarButton1.Name="toolBarButton1";
			this.toolBarButton1.ToolTipText="更新";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.ImageIndex=1;
			this.toolBarButton2.Name="toolBarButton2";
			this.toolBarButton2.ToolTipText="中止";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.ImageIndex=3;
			this.toolBarButton3.Name="toolBarButton3";
			this.toolBarButton3.ToolTipText="ホーム";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.ImageIndex=6;
			this.toolBarButton6.Name="toolBarButton6";
			this.toolBarButton6.ToolTipText="検索";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream=((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor=System.Drawing.Color.Empty;
			this.imageList1.Images.SetKeyName(0,"");
			this.imageList1.Images.SetKeyName(1,"");
			this.imageList1.Images.SetKeyName(2,"");
			this.imageList1.Images.SetKeyName(3,"");
			this.imageList1.Images.SetKeyName(4,"");
			this.imageList1.Images.SetKeyName(5,"");
			this.imageList1.Images.SetKeyName(6,"");
			// 
			// statusBar1
			// 
			this.statusBar1.Location=new System.Drawing.Point(0,266);
			this.statusBar1.Name="statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.status});
			this.statusBar1.ShowPanels=true;
			this.statusBar1.Size=new System.Drawing.Size(496,22);
			this.statusBar1.SizingGrip=false;
			this.statusBar1.TabIndex=10;
			// 
			// status
			// 
			this.status.Name="status";
			this.status.Width=300;
			// 
			// lTitle
			// 
			this.lTitle.BackColor=System.Drawing.SystemColors.ActiveCaption;
			this.lTitle.Dock=System.Windows.Forms.DockStyle.Top;
			this.lTitle.ForeColor=System.Drawing.SystemColors.ActiveCaptionText;
			this.lTitle.Location=new System.Drawing.Point(0,0);
			this.lTitle.Name="lTitle";
			this.lTitle.Size=new System.Drawing.Size(496,16);
			this.lTitle.TabIndex=12;
			this.lTitle.Text="---";
			this.lTitle.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// WebBrowser
			// 
			this.WebBrowser.Dock=System.Windows.Forms.DockStyle.Fill;
			this.WebBrowser.Location=new System.Drawing.Point(0,44);
			this.WebBrowser.Name="WebBrowser";
			this.WebBrowser.Size=new System.Drawing.Size(496,222);
			this.WebBrowser.TabIndex=13;
			// 
			// WebView
			// 
			this.Controls.Add(this.WebBrowser);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.lTitle);
			this.Name="WebView";
			this.Size=new System.Drawing.Size(496,288);
			((System.ComponentModel.ISupportInitialize)(this.status)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private Document _document=null;
		[System.ComponentModel.Browsable(false)]
		public Document document{
			get{return this._document;}
		}
		[System.ComponentModel.Browsable(false)]
		public Window window{
			get{return document!=null?document.parentWindow:null;}
		}
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
		//============================================================
		//		コマンド
		//============================================================
		public void CmdStop(){
			this.WebBrowser.Stop();
		}
		public void CmdHome(){
			this.WebBrowser.GoHome();
		}
		public void CmdForward(){
			if(this.WebBrowser.CanGoForward){
				this.WebBrowser.GoForward();
			}else{
				this.StatusText="これ以上進むことは出来ません。";
			}
		}
		public void CmdBack(){
			if(this.WebBrowser.CanGoBack){
				this.WebBrowser.GoBack();
			}else{
				this.StatusText="これ以上戻ることは出来ません。";
			}
		}
		public void CmdSearch(){
			this.WebBrowser.GoSearch();
		}
		public void CmdRefresh(){
			this.OnBeforeRefresh();
			this.WebBrowser.Refresh();
		}
		public void CmdPrint(){
			this.WebBrowser.Print();
		}
		public void CmdSaveAs(){
			this.WebBrowser.ShowSaveAsDialog();
		}
		/*
		public void CmdSave(){
			if(this.document!=null)
				this.document.execCommand("Save",true,null);
		}//*/
		public void ShowPrintPreview(){
			this.WebBrowser.ShowPrintPreviewDialog();
		}
		/*
		public void CmdFind(){
			this.ExecCommand(SHDocVw.OLECMDID.OLECMDID_FIND);
		}
		//*/
		public void ShowLanguages(){
			window.execScript("window.external.showBrowserUI('LanguageDialog',null);");
		}
		public void ShowFavorites(){
			window.execScript("window.external.showBrowserUI('OrganizeFavorites',null);");
		}
		public void ShowPrivacies(){
			window.execScript("window.external.showBrowserUI('PrivacySettings',null);");
		}
		public void ShowPrograms(){
			window.execScript("window.external.showBrowserUI('ProgramAccessAndDefaults',null);");
		}
		//============================================================
		//		Properties
		//============================================================
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
		public string DocumentTitle{
			get{return document!=null?document.title:lTitle.Text;}
			set{
				if(document!=null){
					document.title=value;
				}else{
					this.lTitle.Text=value;
				}
			}
		}
		public string StatusText{
			get{return window!=null?window.status:this.status.Text;}
			set{
				this.status.Text=value;
				if(this.document!=null)
					this.document.parentWindow.status=value;
			}
		}
		public string LocationUrl{
			get{return this.WebBrowser.Url.AbsolutePath;}
			set{
				if(value==null||value=="")value="about:blank";
				if(value==this.WebBrowser.Url.AbsolutePath)return;
				this.WebBrowser.Navigate(value);
			}
		}
		//============================================================
		//		Events
		//============================================================
		public event Forms::WebBrowserDocumentCompletedEventHandler DownloadCompleted{
			add{this.WebBrowser.DocumentCompleted+=value;}
			remove{this.WebBrowser.DocumentCompleted-=value;}
		}
		/// <summary>
		/// 表示しているコンテンツの更新を行う前に発生するイベントです。
		/// </summary>
		public event BeforeRefreshEventHandler BeforeRefresh;
		public class BeforeRefreshEventArgs:System.EventArgs{
			private string loc;
			public BeforeRefreshEventArgs(string a){
				this.loc=a;
			}
			public string Location{
				get{return this.loc;}
			}
		}
		public delegate void BeforeRefreshEventHandler(object sender,BeforeRefreshEventArgs e);
		// CHECK: 新しい Thread で行う為に全然 Before となりえない?
		protected void OnBeforeRefresh(){
			if(this.BeforeRefresh==null)return;
			BeforeRefresh(this,new BeforeRefreshEventArgs(this.WebBrowser.Url.AbsolutePath));
		}
	}

}