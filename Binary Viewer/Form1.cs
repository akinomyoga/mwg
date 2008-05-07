using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace mwgBinary{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1(){
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
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
		private void InitializeComponent()
		{
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.commandBox = new System.Windows.Forms.ListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(136, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "文字 → &Poke";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(312, 320);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(16, 40);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(136, 24);
			this.button2.TabIndex = 2;
			this.button2.Text = "P&ocket Monsters 言葉";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// commandBox
			// 
			this.commandBox.ItemHeight = 12;
			this.commandBox.Items.AddRange(new object[] {
															"pokeBin",
															"pokeTxt",
															"pokeSav",
															"pokeImg",
															"pokeSav編集",
															"pokeSav保存",
															"RIFF構造",
															"実験mwqDiff",
															"実験mwgPoke.Monster"});
			this.commandBox.Location = new System.Drawing.Point(16, 72);
			this.commandBox.Name = "commandBox";
			this.commandBox.Size = new System.Drawing.Size(136, 232);
			this.commandBox.TabIndex = 3;
			this.commandBox.DoubleClick += new System.EventHandler(this.commandBox_DoubleClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(160, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(488, 368);
			this.tabControl1.TabIndex = 5;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(328, 343);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Page1";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.propertyGrid1);
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(480, 343);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "file";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(8, 16);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(216, 320);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(656, 381);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.commandBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e){
			/*dat=new string[]{"アﾞ","イﾞ","ウﾞ","エﾞ","オﾞ","ガ","ギ","グ","ゲ","ゴ","ザ","ジ","ズ","ゼ","ゾ"
								,"ダ","ヂ","ヅ","デ","ド","ナﾞ","ニﾞ","ヌﾞ","ネﾞ","ノﾞ","バ","ビ","ブ","ボ","マﾞ","ミﾞ","ムﾞ","ィﾞ"
								,"あﾞ","いﾞ","うﾞ","えﾞ","おﾞ","が","ぎ","ぐ","げ","ご","ざ","じ","ず","ぜ","ぞ","だ","ぢ","づ","で","ど"
								,"なﾞ","にﾞ","ぬﾞ","ねﾞ","のﾞ","ば","び","ぶ","べ","ぼ","まﾞ","パ","ピ","プ","ポ","ぱ","ぴ","ぷ","ぺ","ぽ"
								,"まﾟ","みﾟ","むﾟ","めﾟ","もﾟ","(↓1)","(↓2)","(End)","(NewWnd1)","(主人公)","(無)","ポケモン","(ライバル)"
								,"……","(Clear)","(NewWnd2)","(Copy)","てきの　(LastPoke)","パソコン","わざマシン"
								,"トレーナー","ロケットだん","[ﾞﾟ]","Ａ","Ｂ","Ｃ","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","『"
								,"T","U","V","W","X","Y","Z","α","β","γ","δ","ε","　","ア","イ","ウ","エ","オ","カ","キ","ク","ケ","コ"
								,"サ","シ","ス","セ","ソ","タ","チ","ツ","テ","ト","ナ","ニ","ヌ","ネ","ノ","ハ","ヒ","フ","ホ"
								,"マ","ミ","ム","メ","モ","ヤ","ユ","ヨ","ラ","ル","レ","ロ","ワ","ヲ","ン","ッ","ャ","ュ","ョ","ィ"
								,"あ","い","う","え","お","か","き","く","け","こ","さ","し","す","せ","そ","た","ち","つ","て","と"
								,"な","に","ぬ","ね","の","は","ひ","ふ","へ","ほ","ま","み","む","め","も","や","ゆ","よ","ら","り","る","れ","ろ","わ","を","ん"
								,"っ","ゃ","ゅ","ょ","－","[無]","ﾞ","？","！","。","ァ","ゥ","ェ","＞","≫","▼","♂","円"
								,"×","・","／","ォ","♀","０","１","２","３","４","５","６","７","８","９"};
			*/
			for(int i=0;i<256;i++){this.textBox1.AppendText("\r　"+i.ToString()+file.dat[i]);}
			
			/*/b/*/
			
			/*///*|

			/*///e*/
			
			/*/s/*
			byte a=128;
			byte b=245;
			Console.WriteLine((a&b).ToString());
			/*///e*/
		}
		
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox commandBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button button1;

		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e){

		}
		
		public file File=new file();
		public string[] dat;

		private void button1_Click(object sender, System.EventArgs e){
			string text=this.textBox1.Text;
			this.textBox1.Clear();
			for(int i=0;i<text.Length;i++){
				for(int j=0;j<256;j++){
					if(file.dat[j]==text.Substring(i,1))this.textBox1.Text+=j.ToString("x")+" ";
				}
			}
		}
		
		private void button2_Click(object sender, System.EventArgs e){//ポケモンの文字列を抽出
			if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
				string filename=this.openFileDialog1.FileName;//読込ファイルを開く
				File.pokeTxt(filename);
			}
		}

		private void commandBox_DoubleClick(object sender, EventArgs e){
			switch(this.commandBox.Text){
				case "pokeBin":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						File.pokeBin(this.openFileDialog1.FileName);
					}
					break;
				case "pokeTxt":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						File.pokeTxt(this.openFileDialog1.FileName);
					}
					break;
				case "pokeSav":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						File.pokeSav(this.openFileDialog1.FileName);
					}
					break;
				case "pokeImg":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						File.pokeImg(this.openFileDialog1.FileName);
					}
					break;
				case "pokeSav編集":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						File.pokeSavW(this.openFileDialog1.FileName);
					}
					break;
				case "pokeSav保存":
					File.pokeSavW2();
					break;
				case "RIFF構造":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						mwg.File.mwqRiff riff=new mwg.File.mwqRiff(this.openFileDialog1.FileName);
						string str1=riff.ToXml();
						this.textBox1.Lines=str1.Split(new char[]{'\n'});
					}
					break;
				case "実験mwgPoke.Monster":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						mwg.Poke.saveData d1=new mwg.Poke.saveData(this.openFileDialog1.FileName);
						mwg.Poke.Monster m1=new mwg.Poke.Monster(d1,0);
						this.propertyGrid1.SelectedObject=m1;
					}
					break;
				case "実験mwqDiff":
					//ファイルの読込
					string[] filenames1=new string[2];
					string[] filenames2=new string[2];
					System.Windows.Forms.MessageBox.Show("一グループ目を二つ指定して下さい");
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames1[0]=this.openFileDialog1.FileName;
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames1[1]=this.openFileDialog1.FileName;
					System.Windows.Forms.MessageBox.Show("二グループ目を二つ指定して下さい");
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames2[0]=this.openFileDialog1.FileName;
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames2[1]=this.openFileDialog1.FileName;
					System.Windows.Forms.MessageBox.Show("ビットマップを保存する先を指定して下さい");
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					string bitmapname=this.openFileDialog1.FileName;
					
					mwg.File.mwqDiff diff1=new mwg.File.mwqDiff(filenames1);
					mwg.File.mwqDiff diff2=new mwg.File.mwqDiff(filenames2);
					mwg.File.mwqDiff diff3=diff1*diff2;
					diff3.ToBitmap().Save(bitmapname);
					break;
				
			}
		}
	}
	
	public class file{
		public static string[] dat=new string[]{
												   "アﾞ","イﾞ","ウﾞ","エﾞ","オﾞ","ガ","ギ","グ","ゲ","ゴ","ザ","ジ","ズ","ゼ","ゾ","ダ"
												   ,"ヂ","ヅ","デ","ド","ナﾞ","ニﾞ","ヌﾞ","ネﾞ","ノﾞ","バ","ビ","ブ","ボ","マﾞ","ミﾞ","ムﾞ"
												   ,"ィﾞ","あﾞ","いﾞ","うﾞ","えﾞ","おﾞ","が","ぎ","ぐ","げ","ご","ざ","じ","ず","ぜ","ぞ"
												   ,"だ","ぢ","づ","で","ど","なﾞ","にﾞ","ぬﾞ","ねﾞ","のﾞ","ば","び","ぶ","べ","ぼ","まﾞ"
												   ,"パ","ピ","プ","ポ","ぱ","ぴ","ぷ","ぺ","ぽ","まﾟ","が　","(▼↓)","(次↓)ﾟ","もﾟ","(↓)","(←)"
												   ,"(♪)","(▼NWnd1)","(主人公ゲーフリ)","クリチャ","ポケモン","(▼↓)","……","(Clear)","(NewWnd2)","てきの　","てきの　(LastPoke)","パソコン","わざマシン","トレーナー","ロケットだん","[ﾞﾟ]"
												   ,"A","B","C","D","E","F","G","H","I","V","S","L","M",":","ぃ","ぅ"
												   ,"「","」","『","』","・","…","ぁ","ぇ","ぉ","┌","─","┐","│","└","┘","　"
												   ,"ア","イ","ウ","エ","オ","カ","キ","ク","ケ","コ","サ","シ","ス","セ","ソ","タ"
												   ,"チ","ツ","テ","ト","ナ","ニ","ヌ","ネ","ノ","ハ","ヒ","フ","ホ","マ","ミ","ム"
												   ,"メ","モ","ヤ","ユ","ヨ","ラ","ル","レ","ロ","ワ","ヲ","ン","ッ","ャ","ュ","ョ"
												   ,"ィ","あ","い","う","え","お","か","き","く","け","こ","さ","し","す","せ","そ"
												   ,"た","ち","つ","て","と","な","に","ぬ","ね","の","は","ひ","ふ","へ","ほ","ま"
												   ,"み","む","め","も","や","ゆ","よ","ら","り","る","れ","ろ","わ","を","ん","っ"
												   ,"ゃ","ゅ","ょ","－","[無]","ﾞ","？","！","。","ァ","ゥ","ェ","＞","≫","▼","♂"
												   ,"円","×","・","／","ォ","♀","０","１","２","３","４","５","６","７","８","９"};
		public static string[] dat2=new string[]{
												   "アﾞ","イﾞ","ウﾞ","エﾞ","オﾞ","ガ","ギ","グ","ゲ","ゴ","ザ","ジ","ズ","ゼ","ゾ","ダ"
												   ,"ヂ","ヅ","デ","ド","ナﾞ","ニﾞ","ヌﾞ","ネﾞ","ノﾞ","バ","ビ","ブ","ボ","マﾞ","ミﾞ","ムﾞ"
												   ,"ィﾞ","あﾞ","いﾞ","うﾞ","えﾞ","おﾞ","が","ぎ","ぐ","げ","ご","ざ","じ","ず","ぜ","ぞ"
												   ,"だ","ぢ","づ","で","ど","なﾞ","にﾞ","ぬﾞ","ねﾞ","のﾞ","ば","び","ぶ","べ","ぼ","まﾞ"
												   ,"パ","ピ","プ","ポ","ぱ","ぴ","ぷ","ぺ","ぽ","まﾟ","が　","むﾟ","めﾟ","もﾟ","(↓1)","(↓2)"
												   ,"(End)","(NewWnd1)","(主人公)","(無)","ポケモン","(ライバル)","……","(Clear)","(NewWnd2)","(Copy)","てきの　(LastPoke)","パソコン","わざマシン","トレーナー","ロケットだん","[ﾞﾟ]"
												   ,"A","B","C","[HP0]","[HP1]","[HP2]","[HP3]","[HP4]","I","V","S","L","M",":","ぃ","ぅ"
												   ,"「","」","『","』","・","…","ぁ","ぇ","ぉ","┌","─","┐","│","└","┘","　"
												   ,"ア","イ","ウ","エ","オ","カ","キ","ク","ケ","コ","サ","シ","ス","セ","ソ","タ"
												   ,"チ","ツ","テ","ト","ナ","ニ","ヌ","ネ","ノ","ハ","ヒ","フ","ホ","マ","ミ","ム"
												   ,"メ","モ","ヤ","ユ","ヨ","ラ","ル","レ","ロ","ワ","ヲ","ン","ッ","ャ","ュ","ョ"
												   ,"ィ","あ","い","う","え","お","か","き","く","け","こ","さ","し","す","せ","そ"
												   ,"た","ち","つ","て","と","な","に","ぬ","ね","の","は","ひ","ふ","へ","ほ","ま"
												   ,"み","む","め","も","や","ゆ","よ","ら","り","る","れ","ろ","わ","を","ん","っ"
												   ,"ゃ","ゅ","ょ","－","[無]","ﾞ","？","！","。","ァ","ゥ","ェ","＞","≫","▼","♂"
												   ,"円","×","・","／","ォ","♀","０","１","２","３","４","５","６","７","８","９"};
		public string[] datWaza=new string[256];
		public string[] datMon=new string[256];
		public string[] datType=new string[256];
		public string[] datStat=new string[256];
		/// <summary>コンストラクタ</summary>
		public file(){
			System.IO.StreamReader sr;
			sr=new System.IO.StreamReader(@"dat\Data技.txt");
			for(int i=0;i<256;i++)this.datWaza[i]=sr.ReadLine();
			sr=new System.IO.StreamReader(@"dat\DataMonster.txt");
			for(int i=0;i<256;i++)this.datMon[i]=sr.ReadLine();
			sr=new System.IO.StreamReader(@"dat\DataType.txt");
			for(int i=0;i<256;i++)this.datType[i]=sr.ReadLine();
			sr=new System.IO.StreamReader(@"dat\DataStat.txt");
			for(int i=0;i<256;i++)this.datStat[i]=sr.ReadLine();
			sr.Close();
		}
	
		public System.IO.FileStream rbf;
		public System.IO.BinaryReader rbr;
		public string rbfn;
		
		public System.IO.StreamWriter sw;


#region public void pokeSavW(string filename)
		/// <summary>ポケモンの情報を書き換えます。(実験中)</summary>
		/// <param name="filename">savファイルを指定します。</param>
		public void pokeSavW(string filename){
			if(!System.IO.File.Exists(filename)){
				Console.Write("指定したファイルは存在しません\n");
				return;
			}
			//Access 確保
			this.pokeSavWfn=filename;
			rbf=new System.IO.FileStream(filename,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
			//初期化
			int ibase=11997;
			int jbase=12261;
			int kbase=12297;
			//--ファイルサイズ取得
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			Console.Write("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			if(imax!=32768)
			{
				//ファイルサイズによって誤ったファイルを排除
				Console.Write("正規のファイルではないようです。ご確認下さい。処理を終了します。\n");
				Console.Write("処理完了\n");
				rbr.Close();
				rbf.Close();
			}
			//--ファイル読込
			this.pokeSavWfile=rbr.ReadBytes(imax);
			for(int i=0;i<6;i++)
			{//読込
				this.pokeSavWdata[i]=new byte[44];
				this.pokeSavWname[i]=new byte[6];
				this.pokeSavWpare[i]=new byte[6];
				
				for(int j=0;j<44;j++)
				{
					this.pokeSavWdata[i][j]=this.pokeSavWfile[ibase+i*44+j];
				}
				for(int j=0;j<6;j++)
				{
					this.pokeSavWname[i][j]=this.pokeSavWfile[kbase+i*6+j];
					this.pokeSavWpare[i][j]=this.pokeSavWfile[jbase+i*6+j];
				}
				pkFrm1 frm0=new pkFrm1(ref this.pokeSavWdata[i],ref this.pokeSavWname[i],ref this.pokeSavWpare[i]);
				frm0.Show(); 
			}
			Console.WriteLine("処理終了");
			rbr.Close();
			rbf.Close();
		}
		public string pokeSavWfn;
		public byte[] pokeSavWfile;
		public byte[][] pokeSavWdata=new byte[6][];
		public byte[][] pokeSavWname=new byte[6][];
		public byte[][] pokeSavWpare=new byte[6][];
		public void pokeSavW2(){
			if(this.pokeSavWfn==null)return;
			System.IO.FileStream fs=new System.IO.FileStream(this.pokeSavWfn,System.IO.FileMode.Create);
			System.IO.BinaryWriter bw=new System.IO.BinaryWriter(fs);
			int ibase=11997;
			int jbase=12261;
			int kbase=12297;
			for(int i=0;i<6;i++)
			{//変更
				for(int j=0;j<44;j++)
				{
					this.pokeSavWfile[ibase+i*44+j]=this.pokeSavWdata[i][j];
				}
				for(int j=0;j<6;j++)
				{
					this.pokeSavWfile[kbase+i*6+j]=this.pokeSavWname[i][j];
					this.pokeSavWfile[jbase+i*6+j]=this.pokeSavWpare[i][j];
				}
			}
			bw.Write(this.pokeSavWfile);
			Console.WriteLine("書き込み終了 - \""+this.pokeSavWfn+"\" に書き込みました");
			bw.Close();
			fs.Close();
		}
#endregion

#region public void pokeSav(string filename)
		/// <summary>ポケモンのセーブデータからポケモンの情報を抽出します。出力ファイル名は<c>filename+".txt"</c>です。</summary>
		/// <param name="filename">開く対称のファイル名を指定します</param>
		public void pokeSav(string filename)
		{
			if(!System.IO.File.Exists(filename))
			{
				Console.Write("指定したファイルは存在しません\n");
				return;
			}
			//Access 確保
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
			sw=new System.IO.StreamWriter(filename+".pokeMon.txt");
			//初期化
			int ibase=11997;
			int jbase=12261;
			int kbase=12297;
			//--ファイルサイズ取得
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			Console.Write("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			if(imax!=32768){
				//ファイルサイズによって誤ったファイルを排除
				Console.Write("正規のファイルではないようです。ご確認下さい。処理を終了します。\n");
				goto labelExt;
			}
			//--ファイル読込
			byte[] ab=rbr.ReadBytes(imax);
			int[] ai=new int[44];
			string aj;
			bool aj0;
			string ak;
			bool ak0;
			string x;
			int nib1;
			string x0;
			for(int i=0;i<6;i++)
			{//読込
				ak=aj="";
				ak0=aj0=true;
				for(int j=0;j<44;j++){
					ai[j]=(int)ab[ibase+j];
				}
				for(int j=0;j<5;j++){
					if((x=file.dat2[(int)ab[jbase+j]])=="(End)")aj0=false;
					if(aj0)aj+=x;
					if((x=file.dat2[(int)ab[kbase+j]])=="(End)")ak0=false;
					if(ak0)ak+=x;
				}
				sw.WriteLine("");
				sw.WriteLine("==== ◎ P O C K E T   M O N S T E R====");
				sw.WriteLine(ak+" : "+this.datMon[ai[0]]+" : Lv"+ai[33].ToString());
				sw.WriteLine("おや      : "+aj+" (ID: "+(ai[12]*256+ai[13]).ToString()+" )");
				sw.WriteLine("のこりHP  : "+(ai[1]*256+ai[2]).ToString());
				sw.WriteLine("けいけんち: "+trp(ai[14],ai[15],ai[16]));
				for(int j=0;j<4;j++)
				{
					int a1=ai[j+29]/64;
					int a2=ai[j+29]-64*a1;
					sw.WriteLine("わざ "+(j+1).ToString()+": "+this.datWaza[ai[j+8]]+" (ポイントアップ "+a1.ToString()+" 回使用 / 残りPP: "+a2.ToString()+" )");
				}
				sw.WriteLine("たいりょく: "+wrd(ai[34],ai[35])+" (努力値: "+wrd(ai[17],ai[18])+" )");
				sw.WriteLine("こうげき  : "+wrd(ai[36],ai[37])+" (努力値: "+wrd(ai[19],ai[20])+" ;固体値: "+((ai[27]-(nib1=ai[27]%16))/16).ToString()+" )");
				sw.WriteLine("ぼうぎょ  : "+wrd(ai[38],ai[39])+" (努力値: "+wrd(ai[21],ai[22])+" ;固体値: "+nib1.ToString()+" )");
				sw.WriteLine("すばやさ  : "+wrd(ai[40],ai[41])+" (努力値: "+wrd(ai[23],ai[24])+" ;固体値: "+((ai[28]-(nib1=ai[28]%16))/16).ToString()+" )");
				sw.WriteLine("とくしゅ  : "+wrd(ai[42],ai[43])+" (努力値: "+wrd(ai[25],ai[26])+" ;固体値: "+nib1.ToString()+" )");
				sw.WriteLine("?:"+ai[3].ToString());
				x="";
				for(int j=0;j<44;j++){x0=ai[j].ToString("X");if(x0.Length==1)x0="0"+x0;x+=x0;}
				sw.WriteLine(x);
				jbase+=6;
				kbase+=6;
				ibase+=44;
				}
			//後始末
			labelExt:
			Console.Write("処理完了\n");
			rbr.Close();
			rbf.Close();
			sw.Close();
			
		}
		/// <summary>二つの 0-255 の数から 0-65536 の数字を文字列として返す。2Byte データを 1Word のデータとする為</summary>
		/// <param name="a">大きい桁の方の数字(0-255)</param>
		/// <param name="b">小さい桁の方の数字(0-255)</param>
		/// <returns>0-65536 の数字の入った文字列</returns>
		private string wrd(int a,int b){return (a*256+b).ToString();}
		/// <summary>三つの 0-255 の数から 0-16777215 の数字を文字列として返す。</summary>
		/// <param name="a">大きい桁の方の数字(0-255)</param>
		/// <param name="b">中位の桁の方の数字(0-255)</param>
		/// <param name="c">小さい桁の方の数字(0-255)</param>
		/// <returns>0-16777215 の数字の入った文字列</returns>
		private string trp(int a,int b,int c){return ((a*256+b)*256+c).ToString();}
	
		#endregion
	
#region public void pokeBin(string filename)
		/// <summary>ポケモンの内容を文字に変換して書き出します。書き出しファイル名は<c>filename+".txt"</c>です。</summary>
		/// <param name="filename">開く対称のファイル名を指定します</param>
		public void pokeBin(string filename)
		{
			if(!System.IO.File.Exists(filename)){
				Console.Write("指定したファイルは存在しません\n");
				return;
			}
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
				
			sw=new System.IO.StreamWriter(filename+".txt");
				
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			Console.Write("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			for(int i=0;i<imax;i++)
			{//読込
				byte[] a=rbr.ReadBytes(1);
				int b=(int)a[0];
				switch(b){
					case 0:
						sw.Write(".");
						break;
					default:
						sw.Write(file.dat[b]);
						break;
					}
			}
			Console.Write("読込完了\n");
			rbr.Close();
			rbf.Close();
			sw.Close();

		}
	#endregion

#region public void pokeTxt(string filename)
		/// <summary>ポケモンの中から、文字列らしい物を抽出して書き出します。書き出しファイル名は<c>filename+".pokeWords.txt"</c>です。</summary>
		/// <param name="filename">開く対称のファイル名を指定します</param>
		public void pokeTxt(string filename)
		{
			if(!System.IO.File.Exists(filename)){
				Console.Write("指定したファイルは存在しません\n");
				return;
			}
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
				
			sw=new System.IO.StreamWriter(filename+".pokeWords.txt");
				
			string x="";
			bool isText=true;
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			Console.Write("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			
			for(int i=0;i<imax;i++)
			{//読込
				byte[] a=rbr.ReadBytes(1);
				int b=(int)(a[0]);
				if(b==0)
				{
					x="";
					isText=true;
				}
				else if(b==87)
				{
					if(isText) sw.WriteLine(x); else isText=true;
					x="";
				}
				else if(0<b&b<5|20<=b&b<=24|29<=b&b<=32|53<=b&b<=57|b==63|73<=b&b<=77)
				{
					isText=false;
				}
				else if(b==83)x+="(Rival)";
				else if(b==79)x+="(∨1)";
				else if(b==85)x+="(∨2)";
				else if(b==81)x+="(□)";
				else
				{
					x+=file.dat[b];
				}
			}
			Console.Write("読込完了\n");
			rbr.Close();
			rbf.Close();
			sw.Close();

		
		}
	
#endregion

#region public void pokeImg(string filename)
		/// <summary></summary>
		/// <param name="filename"></param>
		public void pokeImg(string filename){
			if(!System.IO.File.Exists(filename))
			{
				Console.Write("指定したファイルは存在しません\n");
				return;
			}
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
				
			
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			Console.Write("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			byte[] a=rbr.ReadBytes(imax);
			
			byte a1;
			
			/*/*
			imax/=2;
			int ix=imax*8/500;
			int iy=500;
			System.Drawing.Bitmap image=new System.Drawing.Bitmap(ix+8,iy);
			ix=0;
			iy=0;
			for(int i=0;i<imax;i++){
				byte jm=1;
				for(int j=7;j>=0;j--){
					a1=(byte)(((a[i*2]&jm)+(a[i*2+1]&jm)*2)/jm);
					jm<<=1;
					image.SetPixel(ix+j,iy,file.pokeImgColor[(int)a1]);
				}
				iy++;
				if(iy>=500){
					iy=0;
					ix+=8;
				}
			}
			image.Save(filename+".bmp");/*///*/
			
			int ibase=66585;
			//int ibase=81920;
			int ix0,ix1,iy0,iy1;
			ix0=iy0=ix1=0;
			iy1=0;
			System.Drawing.Bitmap image2=new Bitmap(650,96);
			for(int i=0;i<7200;i++){
				byte jm=1;
				for(int j=7;j>=0;j--){
					a1=(byte)(((a[ibase+i*2]&jm)+(a[ibase+i*2+1]&jm)*2)/jm);
					jm<<=1;
					image2.SetPixel(ix0+ix1+j,iy0+iy1,file.pokeImgColor[(int)a1]);
				}
				iy1++;
				if(iy1>7){
					iy1=0;ix0+=8;
					if(ix0>8){ix0=0;iy0+=8;
						if(iy0>=96){iy0=0;ix1+=16;}
					}
				}
				
			}
			image2.Save(filename+".2.bmp");
			
			Console.WriteLine("処理終了");

		}
		
		private static System.Drawing.Color[] pokeImgColor={System.Drawing.Color.White
														,System.Drawing.Color.Silver
														,System.Drawing.Color.Gray
														,System.Drawing.Color.Black};
		#endregion


/*
		public static System.IO.FileStream rbf;
		public static System.IO.BinaryReader rbr;
		public static string rbfn;
		public static void FileOpenRB(string file){
			if(!System.IO.File.Exists(file)){return;}
			rbfn=file;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
		}
		public static byte ReadRB(){return rbr.ReadByte();}
		public static byte[] ReadRB(int count){return rbr.ReadBytes(count);}

		public static void CloseRB(){rbr.Close();rbf.Close();}
	*/	
		
	}

}
