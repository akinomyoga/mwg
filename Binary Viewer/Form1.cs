using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace mwgBinary{
	/// <summary>
	/// Form1 �̊T�v�̐����ł��B
	/// </summary>
	public class Form1 : System.Windows.Forms.Form{
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1(){
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
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
			this.button1.Text = "���� �� &Poke";
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
			this.button2.Text = "P&ocket Monsters ���t";
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
															"pokeSav�ҏW",
															"pokeSav�ۑ�",
															"RIFF�\��",
															"����mwqDiff",
															"����mwgPoke.Monster"});
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
		/// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e){
			/*dat=new string[]{"�A�","�C�","�E�","�G�","�I�","�K","�M","�O","�Q","�S","�U","�W","�Y","�[","�]"
								,"�_","�a","�d","�f","�h","�i�","�j�","�k�","�l�","�m�","�o","�r","�u","�{","�}�","�~�","���","�B�"
								,"���","���","���","���","���","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
								,"���","���","���","���","���","��","��","��","��","��","���","�p","�s","�v","�|","��","��","��","��","��"
								,"���","���","���","���","���","(��1)","(��2)","(End)","(NewWnd1)","(��l��)","(��)","�|�P����","(���C�o��)"
								,"�c�c","(Clear)","(NewWnd2)","(Copy)","�Ă��́@(LastPoke)","�p�\�R��","�킴�}�V��"
								,"�g���[�i�[","���P�b�g����","[��]","�`","�a","�b","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","�w"
								,"T","U","V","W","X","Y","Z","��","��","��","��","��","�@","�A","�C","�E","�G","�I","�J","�L","�N","�P","�R"
								,"�T","�V","�X","�Z","�\","�^","�`","�c","�e","�g","�i","�j","�k","�l","�m","�n","�q","�t","�z"
								,"�}","�~","��","��","��","��","��","��","��","��","��","��","��","��","��","�b","��","��","��","�B"
								,"��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
								,"��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
								,"��","��","��","��","�|","[��]","�","�H","�I","�B","�@","�D","�F","��","��","��","��","�~"
								,"�~","�E","�^","�H","��","�O","�P","�Q","�R","�S","�T","�U","�V","�W","�X"};
			*/
			for(int i=0;i<256;i++){this.textBox1.AppendText("\r�@"+i.ToString()+file.dat[i]);}
			
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
		
		private void button2_Click(object sender, System.EventArgs e){//�|�P�����̕�����𒊏o
			if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
				string filename=this.openFileDialog1.FileName;//�Ǎ��t�@�C�����J��
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
				case "pokeSav�ҏW":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						File.pokeSavW(this.openFileDialog1.FileName);
					}
					break;
				case "pokeSav�ۑ�":
					File.pokeSavW2();
					break;
				case "RIFF�\��":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						mwg.File.mwqRiff riff=new mwg.File.mwqRiff(this.openFileDialog1.FileName);
						string str1=riff.ToXml();
						this.textBox1.Lines=str1.Split(new char[]{'\n'});
					}
					break;
				case "����mwgPoke.Monster":
					if(this.openFileDialog1.ShowDialog(this)==System.Windows.Forms.DialogResult.OK){
						mwg.Poke.saveData d1=new mwg.Poke.saveData(this.openFileDialog1.FileName);
						mwg.Poke.Monster m1=new mwg.Poke.Monster(d1,0);
						this.propertyGrid1.SelectedObject=m1;
					}
					break;
				case "����mwqDiff":
					//�t�@�C���̓Ǎ�
					string[] filenames1=new string[2];
					string[] filenames2=new string[2];
					System.Windows.Forms.MessageBox.Show("��O���[�v�ڂ��w�肵�ĉ�����");
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames1[0]=this.openFileDialog1.FileName;
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames1[1]=this.openFileDialog1.FileName;
					System.Windows.Forms.MessageBox.Show("��O���[�v�ڂ��w�肵�ĉ�����");
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames2[0]=this.openFileDialog1.FileName;
					if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)break;
					filenames2[1]=this.openFileDialog1.FileName;
					System.Windows.Forms.MessageBox.Show("�r�b�g�}�b�v��ۑ��������w�肵�ĉ�����");
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
												   "�A�","�C�","�E�","�G�","�I�","�K","�M","�O","�Q","�S","�U","�W","�Y","�[","�]","�_"
												   ,"�a","�d","�f","�h","�i�","�j�","�k�","�l�","�m�","�o","�r","�u","�{","�}�","�~�","���"
												   ,"�B�","���","���","���","���","���","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","��","��","���","���","���","���","���","��","��","��","��","��","���"
												   ,"�p","�s","�v","�|","��","��","��","��","��","���","���@","(����)","(����)�","���","(��)","(��)"
												   ,"(��)","(��NWnd1)","(��l���Q�[�t��)","�N���`��","�|�P����","(����)","�c�c","(Clear)","(NewWnd2)","�Ă��́@","�Ă��́@(LastPoke)","�p�\�R��","�킴�}�V��","�g���[�i�[","���P�b�g����","[��]"
												   ,"A","B","C","D","E","F","G","H","I","V","S","L","M",":","��","��"
												   ,"�u","�v","�w","�x","�E","�c","��","��","��","��","��","��","��","��","��","�@"
												   ,"�A","�C","�E","�G","�I","�J","�L","�N","�P","�R","�T","�V","�X","�Z","�\","�^"
												   ,"�`","�c","�e","�g","�i","�j","�k","�l","�m","�n","�q","�t","�z","�}","�~","��"
												   ,"��","��","��","��","��","��","��","��","��","��","��","��","�b","��","��","��"
												   ,"�B","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","�|","[��]","�","�H","�I","�B","�@","�D","�F","��","��","��","��"
												   ,"�~","�~","�E","�^","�H","��","�O","�P","�Q","�R","�S","�T","�U","�V","�W","�X"};
		public static string[] dat2=new string[]{
												   "�A�","�C�","�E�","�G�","�I�","�K","�M","�O","�Q","�S","�U","�W","�Y","�[","�]","�_"
												   ,"�a","�d","�f","�h","�i�","�j�","�k�","�l�","�m�","�o","�r","�u","�{","�}�","�~�","���"
												   ,"�B�","���","���","���","���","���","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","��","��","���","���","���","���","���","��","��","��","��","��","���"
												   ,"�p","�s","�v","�|","��","��","��","��","��","���","���@","���","���","���","(��1)","(��2)"
												   ,"(End)","(NewWnd1)","(��l��)","(��)","�|�P����","(���C�o��)","�c�c","(Clear)","(NewWnd2)","(Copy)","�Ă��́@(LastPoke)","�p�\�R��","�킴�}�V��","�g���[�i�[","���P�b�g����","[��]"
												   ,"A","B","C","[HP0]","[HP1]","[HP2]","[HP3]","[HP4]","I","V","S","L","M",":","��","��"
												   ,"�u","�v","�w","�x","�E","�c","��","��","��","��","��","��","��","��","��","�@"
												   ,"�A","�C","�E","�G","�I","�J","�L","�N","�P","�R","�T","�V","�X","�Z","�\","�^"
												   ,"�`","�c","�e","�g","�i","�j","�k","�l","�m","�n","�q","�t","�z","�}","�~","��"
												   ,"��","��","��","��","��","��","��","��","��","��","��","��","�b","��","��","��"
												   ,"�B","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","��","��","��","��","��","��","��","��","��","��","��","��","��"
												   ,"��","��","��","�|","[��]","�","�H","�I","�B","�@","�D","�F","��","��","��","��"
												   ,"�~","�~","�E","�^","�H","��","�O","�P","�Q","�R","�S","�T","�U","�V","�W","�X"};
		public string[] datWaza=new string[256];
		public string[] datMon=new string[256];
		public string[] datType=new string[256];
		public string[] datStat=new string[256];
		/// <summary>�R���X�g���N�^</summary>
		public file(){
			System.IO.StreamReader sr;
			sr=new System.IO.StreamReader(@"dat\Data�Z.txt");
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
		/// <summary>�|�P�����̏������������܂��B(������)</summary>
		/// <param name="filename">sav�t�@�C�����w�肵�܂��B</param>
		public void pokeSavW(string filename){
			if(!System.IO.File.Exists(filename)){
				Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			//Access �m��
			this.pokeSavWfn=filename;
			rbf=new System.IO.FileStream(filename,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
			//������
			int ibase=11997;
			int jbase=12261;
			int kbase=12297;
			//--�t�@�C���T�C�Y�擾
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			Console.Write("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
			if(imax!=32768)
			{
				//�t�@�C���T�C�Y�ɂ���Č�����t�@�C����r��
				Console.Write("���K�̃t�@�C���ł͂Ȃ��悤�ł��B���m�F�������B�������I�����܂��B\n");
				Console.Write("��������\n");
				rbr.Close();
				rbf.Close();
			}
			//--�t�@�C���Ǎ�
			this.pokeSavWfile=rbr.ReadBytes(imax);
			for(int i=0;i<6;i++)
			{//�Ǎ�
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
			Console.WriteLine("�����I��");
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
			{//�ύX
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
			Console.WriteLine("�������ݏI�� - \""+this.pokeSavWfn+"\" �ɏ������݂܂���");
			bw.Close();
			fs.Close();
		}
#endregion

#region public void pokeSav(string filename)
		/// <summary>�|�P�����̃Z�[�u�f�[�^����|�P�����̏��𒊏o���܂��B�o�̓t�@�C������<c>filename+".txt"</c>�ł��B</summary>
		/// <param name="filename">�J���Ώ̂̃t�@�C�������w�肵�܂�</param>
		public void pokeSav(string filename)
		{
			if(!System.IO.File.Exists(filename))
			{
				Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			//Access �m��
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
			sw=new System.IO.StreamWriter(filename+".pokeMon.txt");
			//������
			int ibase=11997;
			int jbase=12261;
			int kbase=12297;
			//--�t�@�C���T�C�Y�擾
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			Console.Write("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
			if(imax!=32768){
				//�t�@�C���T�C�Y�ɂ���Č�����t�@�C����r��
				Console.Write("���K�̃t�@�C���ł͂Ȃ��悤�ł��B���m�F�������B�������I�����܂��B\n");
				goto labelExt;
			}
			//--�t�@�C���Ǎ�
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
			{//�Ǎ�
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
				sw.WriteLine("==== �� P O C K E T   M O N S T E R====");
				sw.WriteLine(ak+" : "+this.datMon[ai[0]]+" : Lv"+ai[33].ToString());
				sw.WriteLine("����      : "+aj+" (ID: "+(ai[12]*256+ai[13]).ToString()+" )");
				sw.WriteLine("�̂���HP  : "+(ai[1]*256+ai[2]).ToString());
				sw.WriteLine("��������: "+trp(ai[14],ai[15],ai[16]));
				for(int j=0;j<4;j++)
				{
					int a1=ai[j+29]/64;
					int a2=ai[j+29]-64*a1;
					sw.WriteLine("�킴 "+(j+1).ToString()+": "+this.datWaza[ai[j+8]]+" (�|�C���g�A�b�v "+a1.ToString()+" ��g�p / �c��PP: "+a2.ToString()+" )");
				}
				sw.WriteLine("������傭: "+wrd(ai[34],ai[35])+" (�w�͒l: "+wrd(ai[17],ai[18])+" )");
				sw.WriteLine("��������  : "+wrd(ai[36],ai[37])+" (�w�͒l: "+wrd(ai[19],ai[20])+" ;�ő̒l: "+((ai[27]-(nib1=ai[27]%16))/16).ToString()+" )");
				sw.WriteLine("�ڂ�����  : "+wrd(ai[38],ai[39])+" (�w�͒l: "+wrd(ai[21],ai[22])+" ;�ő̒l: "+nib1.ToString()+" )");
				sw.WriteLine("���΂₳  : "+wrd(ai[40],ai[41])+" (�w�͒l: "+wrd(ai[23],ai[24])+" ;�ő̒l: "+((ai[28]-(nib1=ai[28]%16))/16).ToString()+" )");
				sw.WriteLine("�Ƃ�����  : "+wrd(ai[42],ai[43])+" (�w�͒l: "+wrd(ai[25],ai[26])+" ;�ő̒l: "+nib1.ToString()+" )");
				sw.WriteLine("?:"+ai[3].ToString());
				x="";
				for(int j=0;j<44;j++){x0=ai[j].ToString("X");if(x0.Length==1)x0="0"+x0;x+=x0;}
				sw.WriteLine(x);
				jbase+=6;
				kbase+=6;
				ibase+=44;
				}
			//��n��
			labelExt:
			Console.Write("��������\n");
			rbr.Close();
			rbf.Close();
			sw.Close();
			
		}
		/// <summary>��� 0-255 �̐����� 0-65536 �̐����𕶎���Ƃ��ĕԂ��B2Byte �f�[�^�� 1Word �̃f�[�^�Ƃ����</summary>
		/// <param name="a">�傫�����̕��̐���(0-255)</param>
		/// <param name="b">���������̕��̐���(0-255)</param>
		/// <returns>0-65536 �̐����̓�����������</returns>
		private string wrd(int a,int b){return (a*256+b).ToString();}
		/// <summary>�O�� 0-255 �̐����� 0-16777215 �̐����𕶎���Ƃ��ĕԂ��B</summary>
		/// <param name="a">�傫�����̕��̐���(0-255)</param>
		/// <param name="b">���ʂ̌��̕��̐���(0-255)</param>
		/// <param name="c">���������̕��̐���(0-255)</param>
		/// <returns>0-16777215 �̐����̓�����������</returns>
		private string trp(int a,int b,int c){return ((a*256+b)*256+c).ToString();}
	
		#endregion
	
#region public void pokeBin(string filename)
		/// <summary>�|�P�����̓��e�𕶎��ɕϊ����ď����o���܂��B�����o���t�@�C������<c>filename+".txt"</c>�ł��B</summary>
		/// <param name="filename">�J���Ώ̂̃t�@�C�������w�肵�܂�</param>
		public void pokeBin(string filename)
		{
			if(!System.IO.File.Exists(filename)){
				Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
				
			sw=new System.IO.StreamWriter(filename+".txt");
				
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			Console.Write("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
			for(int i=0;i<imax;i++)
			{//�Ǎ�
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
			Console.Write("�Ǎ�����\n");
			rbr.Close();
			rbf.Close();
			sw.Close();

		}
	#endregion

#region public void pokeTxt(string filename)
		/// <summary>�|�P�����̒�����A������炵�����𒊏o���ď����o���܂��B�����o���t�@�C������<c>filename+".pokeWords.txt"</c>�ł��B</summary>
		/// <param name="filename">�J���Ώ̂̃t�@�C�������w�肵�܂�</param>
		public void pokeTxt(string filename)
		{
			if(!System.IO.File.Exists(filename)){
				Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
				
			sw=new System.IO.StreamWriter(filename+".pokeWords.txt");
				
			string x="";
			bool isText=true;
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			Console.Write("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
			
			for(int i=0;i<imax;i++)
			{//�Ǎ�
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
				else if(b==79)x+="(��1)";
				else if(b==85)x+="(��2)";
				else if(b==81)x+="(��)";
				else
				{
					x+=file.dat[b];
				}
			}
			Console.Write("�Ǎ�����\n");
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
				Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			rbfn=filename;
			rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			rbr=new System.IO.BinaryReader(rbf);
				
			
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			Console.Write("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
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
			
			Console.WriteLine("�����I��");

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
