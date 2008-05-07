using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace mwgBinary
{
	/// <summary>
	/// pkFrm1 の概要の説明です。
	/// </summary>
	public class pkFrm1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NumericUpDown act4PU;
		private System.Windows.Forms.NumericUpDown act4PP;
		private System.Windows.Forms.ComboBox act4V;
		private System.Windows.Forms.NumericUpDown act3PU;
		private System.Windows.Forms.NumericUpDown act3PP;
		private System.Windows.Forms.ComboBox act3V;
		private System.Windows.Forms.NumericUpDown act2PU;
		private System.Windows.Forms.NumericUpDown act2PP;
		private System.Windows.Forms.ComboBox act2V;
		private System.Windows.Forms.NumericUpDown act1PU;
		private System.Windows.Forms.NumericUpDown act1PP;
		private System.Windows.Forms.ComboBox act1V;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown updnH2;
		private System.Windows.Forms.NumericUpDown updnH1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown updnT3;
		private System.Windows.Forms.NumericUpDown updnT2;
		private System.Windows.Forms.NumericUpDown updnT1;
		private System.Windows.Forms.NumericUpDown updnS3;
		private System.Windows.Forms.NumericUpDown updnS2;
		private System.Windows.Forms.NumericUpDown updnS1;
		private System.Windows.Forms.NumericUpDown updnB3;
		private System.Windows.Forms.NumericUpDown updnB2;
		private System.Windows.Forms.NumericUpDown updnB1;
		private System.Windows.Forms.NumericUpDown updnK3;
		private System.Windows.Forms.NumericUpDown updnK2;
		private System.Windows.Forms.NumericUpDown updnK1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown updnID;
		private System.Windows.Forms.NumericUpDown updnEx;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown updnLv;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown updnHP;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ComboBox poke;
		private System.Windows.Forms.ComboBox type1;
		private System.Windows.Forms.ComboBox type2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtPare;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label lName;
		private System.Windows.Forms.Label lPare;
		private System.Windows.Forms.Button button1;
		public byte[] pDat;
		public byte[] pNm;
		public byte[] pPrt;
		public int total=0;
		public file File;
		private System.Windows.Forms.ComboBox state;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		
#region public pkFrm1(ref byte[] data,ref byte[] name,ref byte[] parent)//コンストラクタ 
		/// <summary>pkFrm1のコンストラクタです。(By め)</summary>
		/// <param name="data">ポケモンの情報</param>
		/// <param name="name">ポケモンの名前</param>
		/// <param name="parent">ポケモンの親</param>
		public pkFrm1(ref byte[] data,ref byte[] name,ref byte[] parent)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			
			//引数処理
			this.pDat=data;
			this.pNm=name;
			this.pPrt=parent;
			this.File=new file();
			for(int i=0;i<44;i++)this.total+=data[i];
			for(int i=0;i<6;i++)this.total+=name[i];
			for(int i=0;i<6;i++)this.total+=parent[i];
			this.total%=256;
			//値設定の為の情報転置
			byte[] data1=new byte[44];
			for(int i=0;i<44;i++){
				data1[i]=data[i];
			}
			//技名、モンスター名の読込
			for(int i=0;i<this.File.datMon.Length;i++)
			{
				this.poke.Items.Add(this.File.datMon[i]);
			}
			for(int i=0;i<256;i++){
				if(this.File.datWaza[i]!=null){
					this.act1V.Items.Add(this.File.datWaza[i]);
					this.act2V.Items.Add(this.File.datWaza[i]);
					this.act3V.Items.Add(this.File.datWaza[i]);
					this.act4V.Items.Add(this.File.datWaza[i]);
				}
				if(this.File.datType[i]!=null){
					this.type1.Items.Add(this.File.datType[i]);
					this.type2.Items.Add(this.File.datType[i]);
				}
				if(this.File.datStat[i]!=null)
				{
					this.state.Items.Add(this.File.datStat[i]);
				}
			}
			//値設定
			this.poke.SelectedIndex=data1[0];
			this.updnHP.Value=(data1[1]<<8)+data1[2];
			this.state.SelectedIndex=data1[4];
			this.type1.SelectedIndex=data1[5];
			this.type2.SelectedIndex=data1[6];
			this.act1V.SelectedIndex=data1[8];
			this.act2V.SelectedIndex=data1[9];
			this.act3V.SelectedIndex=data1[10];
			this.act4V.SelectedIndex=data1[11];
			this.updnID.Value=(data1[12]<<8)+data1[13];
			this.updnEx.Value=(data1[14]<<16)+(data1[15]<<8)+data1[16];
			this.updnH2.Value=(data1[17]<<8)+data1[18];
			this.updnK2.Value=(data1[19]<<8)+data1[20];
			this.updnB2.Value=(data1[21]<<8)+data1[22];
			this.updnS2.Value=(data1[23]<<8)+data1[24];
			this.updnT2.Value=(data1[25]<<8)+data1[26];
			this.updnK3.Value=data1[27]>>4;
			this.updnB3.Value=data1[27]&0x0f;
			this.updnS3.Value=data1[28]>>4;
			this.updnT3.Value=data1[28]&0x0f;
			this.act1PU.Value=data1[29]>>6;
			this.act1PP.Value=data1[29]&0x3f;
			this.act2PU.Value=data1[30]>>6;
			this.act2PP.Value=data1[30]&0x3f;
			this.act3PU.Value=data1[31]>>6;
			this.act3PP.Value=data1[31]&0x3f;
			this.act4PU.Value=data1[32]>>6;
			this.act4PP.Value=data1[32]&0x3f;
			this.updnLv.Value=data1[33];
			this.updnH1.Value=(data1[34]<<8)+data1[35];
			this.updnK1.Value=(data1[36]<<8)+data1[37];
			this.updnB1.Value=(data1[38]<<8)+data1[39];
			this.updnS1.Value=(data1[40]<<8)+data1[41];
			this.updnT1.Value=(data1[42]<<8)+data1[43];
			this.txtName.Text=this.read_name(this.pNm);
			this.txtPare.Text=this.read_name(this.pPrt);
		}
#endregion
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.act4PU = new System.Windows.Forms.NumericUpDown();
			this.act4PP = new System.Windows.Forms.NumericUpDown();
			this.act4V = new System.Windows.Forms.ComboBox();
			this.act3PU = new System.Windows.Forms.NumericUpDown();
			this.act3PP = new System.Windows.Forms.NumericUpDown();
			this.act3V = new System.Windows.Forms.ComboBox();
			this.act2PU = new System.Windows.Forms.NumericUpDown();
			this.act2PP = new System.Windows.Forms.NumericUpDown();
			this.act2V = new System.Windows.Forms.ComboBox();
			this.act1PU = new System.Windows.Forms.NumericUpDown();
			this.act1PP = new System.Windows.Forms.NumericUpDown();
			this.act1V = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.updnH2 = new System.Windows.Forms.NumericUpDown();
			this.updnH1 = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.updnT3 = new System.Windows.Forms.NumericUpDown();
			this.updnT2 = new System.Windows.Forms.NumericUpDown();
			this.updnT1 = new System.Windows.Forms.NumericUpDown();
			this.updnS3 = new System.Windows.Forms.NumericUpDown();
			this.updnS2 = new System.Windows.Forms.NumericUpDown();
			this.updnS1 = new System.Windows.Forms.NumericUpDown();
			this.updnB3 = new System.Windows.Forms.NumericUpDown();
			this.updnB2 = new System.Windows.Forms.NumericUpDown();
			this.updnB1 = new System.Windows.Forms.NumericUpDown();
			this.updnK3 = new System.Windows.Forms.NumericUpDown();
			this.updnK2 = new System.Windows.Forms.NumericUpDown();
			this.updnK1 = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.updnID = new System.Windows.Forms.NumericUpDown();
			this.updnEx = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.updnLv = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.updnHP = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.poke = new System.Windows.Forms.ComboBox();
			this.type1 = new System.Windows.Forms.ComboBox();
			this.type2 = new System.Windows.Forms.ComboBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtPare = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.state = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.lName = new System.Windows.Forms.Label();
			this.lPare = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.act4PU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act4PP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act3PU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act3PP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act2PU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act2PP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act1PU)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.act1PP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnH2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnH1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnT3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnT2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnT1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnS3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnS2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnS1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnB3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnB2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnB1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnK3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnK2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnK1)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.updnID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnEx)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnLv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnHP)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// act4PU
			// 
			this.act4PU.Location = new System.Drawing.Point(184, 104);
			this.act4PU.Maximum = new System.Decimal(new int[] {
																   3,
																   0,
																   0,
																   0});
			this.act4PU.Name = "act4PU";
			this.act4PU.Size = new System.Drawing.Size(56, 19);
			this.act4PU.TabIndex = 71;
			this.act4PU.ValueChanged += new System.EventHandler(this.act4PU_ValueChanged);
			this.act4PU.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act4PP
			// 
			this.act4PP.Location = new System.Drawing.Point(120, 104);
			this.act4PP.Maximum = new System.Decimal(new int[] {
																   63,
																   0,
																   0,
																   0});
			this.act4PP.Name = "act4PP";
			this.act4PP.Size = new System.Drawing.Size(56, 19);
			this.act4PP.TabIndex = 70;
			this.act4PP.ValueChanged += new System.EventHandler(this.act4PP_ValueChanged);
			this.act4PP.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act4V
			// 
			this.act4V.Location = new System.Drawing.Point(8, 104);
			this.act4V.Name = "act4V";
			this.act4V.Size = new System.Drawing.Size(104, 20);
			this.act4V.TabIndex = 69;
			this.act4V.SelectedIndexChanged += new System.EventHandler(this.act4V_SelectedIndexChanged);
			// 
			// act3PU
			// 
			this.act3PU.Location = new System.Drawing.Point(184, 80);
			this.act3PU.Maximum = new System.Decimal(new int[] {
																   3,
																   0,
																   0,
																   0});
			this.act3PU.Name = "act3PU";
			this.act3PU.Size = new System.Drawing.Size(56, 19);
			this.act3PU.TabIndex = 68;
			this.act3PU.ValueChanged += new System.EventHandler(this.act3PU_ValueChanged);
			this.act3PU.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act3PP
			// 
			this.act3PP.Location = new System.Drawing.Point(120, 80);
			this.act3PP.Maximum = new System.Decimal(new int[] {
																   63,
																   0,
																   0,
																   0});
			this.act3PP.Name = "act3PP";
			this.act3PP.Size = new System.Drawing.Size(56, 19);
			this.act3PP.TabIndex = 67;
			this.act3PP.ValueChanged += new System.EventHandler(this.act3PP_ValueChanged);
			this.act3PP.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act3V
			// 
			this.act3V.Location = new System.Drawing.Point(8, 80);
			this.act3V.Name = "act3V";
			this.act3V.Size = new System.Drawing.Size(104, 20);
			this.act3V.TabIndex = 66;
			this.act3V.SelectedIndexChanged += new System.EventHandler(this.act3V_SelectedIndexChanged);
			// 
			// act2PU
			// 
			this.act2PU.Location = new System.Drawing.Point(184, 56);
			this.act2PU.Maximum = new System.Decimal(new int[] {
																   3,
																   0,
																   0,
																   0});
			this.act2PU.Name = "act2PU";
			this.act2PU.Size = new System.Drawing.Size(56, 19);
			this.act2PU.TabIndex = 65;
			this.act2PU.ValueChanged += new System.EventHandler(this.act2PU_ValueChanged);
			this.act2PU.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act2PP
			// 
			this.act2PP.Location = new System.Drawing.Point(120, 56);
			this.act2PP.Maximum = new System.Decimal(new int[] {
																   63,
																   0,
																   0,
																   0});
			this.act2PP.Name = "act2PP";
			this.act2PP.Size = new System.Drawing.Size(56, 19);
			this.act2PP.TabIndex = 64;
			this.act2PP.ValueChanged += new System.EventHandler(this.act2PP_ValueChanged);
			this.act2PP.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act2V
			// 
			this.act2V.Location = new System.Drawing.Point(8, 56);
			this.act2V.Name = "act2V";
			this.act2V.Size = new System.Drawing.Size(104, 20);
			this.act2V.TabIndex = 63;
			this.act2V.SelectedIndexChanged += new System.EventHandler(this.act2V_SelectedIndexChanged);
			// 
			// act1PU
			// 
			this.act1PU.Location = new System.Drawing.Point(184, 32);
			this.act1PU.Maximum = new System.Decimal(new int[] {
																   3,
																   0,
																   0,
																   0});
			this.act1PU.Name = "act1PU";
			this.act1PU.Size = new System.Drawing.Size(56, 19);
			this.act1PU.TabIndex = 62;
			this.act1PU.ValueChanged += new System.EventHandler(this.act1PU_ValueChanged);
			this.act1PU.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act1PP
			// 
			this.act1PP.Location = new System.Drawing.Point(120, 32);
			this.act1PP.Maximum = new System.Decimal(new int[] {
																   63,
																   0,
																   0,
																   0});
			this.act1PP.Name = "act1PP";
			this.act1PP.Size = new System.Drawing.Size(56, 19);
			this.act1PP.TabIndex = 61;
			this.act1PP.ValueChanged += new System.EventHandler(this.act1PP_ValueChanged);
			this.act1PP.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// act1V
			// 
			this.act1V.Location = new System.Drawing.Point(8, 32);
			this.act1V.Name = "act1V";
			this.act1V.Size = new System.Drawing.Size(104, 20);
			this.act1V.TabIndex = 60;
			this.act1V.SelectedIndexChanged += new System.EventHandler(this.act1V_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(184, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 59;
			this.label8.Text = "固体値";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(120, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 58;
			this.label7.Text = "努力値";
			// 
			// updnH2
			// 
			this.updnH2.Location = new System.Drawing.Point(120, 32);
			this.updnH2.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnH2.Name = "updnH2";
			this.updnH2.Size = new System.Drawing.Size(56, 19);
			this.updnH2.TabIndex = 57;
			this.updnH2.ValueChanged += new System.EventHandler(this.updnH2_ValueChanged);
			this.updnH2.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnH1
			// 
			this.updnH1.Location = new System.Drawing.Point(56, 32);
			this.updnH1.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnH1.Name = "updnH1";
			this.updnH1.Size = new System.Drawing.Size(56, 19);
			this.updnH1.TabIndex = 56;
			this.updnH1.ValueChanged += new System.EventHandler(this.updnH1_ValueChanged);
			this.updnH1.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(56, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 55;
			this.label6.Text = "値";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 54;
			this.label5.Text = "HP";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 53;
			this.label4.Text = "とくしゅ";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 52;
			this.label3.Text = "すばやさ";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 14);
			this.label2.TabIndex = 51;
			this.label2.Text = "ぼうぎょ";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 50;
			this.label1.Text = "こうげき";
			// 
			// updnT3
			// 
			this.updnT3.Location = new System.Drawing.Point(184, 128);
			this.updnT3.Maximum = new System.Decimal(new int[] {
																   15,
																   0,
																   0,
																   0});
			this.updnT3.Name = "updnT3";
			this.updnT3.Size = new System.Drawing.Size(56, 19);
			this.updnT3.TabIndex = 49;
			this.updnT3.ValueChanged += new System.EventHandler(this.updnT3_ValueChanged);
			this.updnT3.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnT2
			// 
			this.updnT2.Location = new System.Drawing.Point(120, 128);
			this.updnT2.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnT2.Name = "updnT2";
			this.updnT2.Size = new System.Drawing.Size(56, 19);
			this.updnT2.TabIndex = 48;
			this.updnT2.ValueChanged += new System.EventHandler(this.updnT2_ValueChanged);
			this.updnT2.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnT1
			// 
			this.updnT1.Location = new System.Drawing.Point(56, 128);
			this.updnT1.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnT1.Name = "updnT1";
			this.updnT1.Size = new System.Drawing.Size(56, 19);
			this.updnT1.TabIndex = 47;
			this.updnT1.ValueChanged += new System.EventHandler(this.updnT1_ValueChanged);
			this.updnT1.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnS3
			// 
			this.updnS3.Location = new System.Drawing.Point(184, 104);
			this.updnS3.Maximum = new System.Decimal(new int[] {
																   15,
																   0,
																   0,
																   0});
			this.updnS3.Name = "updnS3";
			this.updnS3.Size = new System.Drawing.Size(56, 19);
			this.updnS3.TabIndex = 46;
			this.updnS3.ValueChanged += new System.EventHandler(this.updnS3_ValueChanged);
			this.updnS3.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnS2
			// 
			this.updnS2.Location = new System.Drawing.Point(120, 104);
			this.updnS2.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnS2.Name = "updnS2";
			this.updnS2.Size = new System.Drawing.Size(56, 19);
			this.updnS2.TabIndex = 45;
			this.updnS2.ValueChanged += new System.EventHandler(this.updnS2_ValueChanged);
			this.updnS2.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnS1
			// 
			this.updnS1.Location = new System.Drawing.Point(56, 104);
			this.updnS1.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnS1.Name = "updnS1";
			this.updnS1.Size = new System.Drawing.Size(56, 19);
			this.updnS1.TabIndex = 44;
			this.updnS1.ValueChanged += new System.EventHandler(this.updnS1_ValueChanged);
			this.updnS1.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnB3
			// 
			this.updnB3.Location = new System.Drawing.Point(184, 80);
			this.updnB3.Maximum = new System.Decimal(new int[] {
																   15,
																   0,
																   0,
																   0});
			this.updnB3.Name = "updnB3";
			this.updnB3.Size = new System.Drawing.Size(56, 19);
			this.updnB3.TabIndex = 43;
			this.updnB3.ValueChanged += new System.EventHandler(this.updnB3_ValueChanged);
			this.updnB3.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnB2
			// 
			this.updnB2.Location = new System.Drawing.Point(120, 80);
			this.updnB2.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnB2.Name = "updnB2";
			this.updnB2.Size = new System.Drawing.Size(56, 19);
			this.updnB2.TabIndex = 42;
			this.updnB2.ValueChanged += new System.EventHandler(this.updnB2_ValueChanged);
			this.updnB2.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnB1
			// 
			this.updnB1.Location = new System.Drawing.Point(56, 80);
			this.updnB1.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnB1.Name = "updnB1";
			this.updnB1.Size = new System.Drawing.Size(56, 19);
			this.updnB1.TabIndex = 41;
			this.updnB1.ValueChanged += new System.EventHandler(this.updnB1_ValueChanged);
			this.updnB1.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnK3
			// 
			this.updnK3.Location = new System.Drawing.Point(184, 56);
			this.updnK3.Maximum = new System.Decimal(new int[] {
																   15,
																   0,
																   0,
																   0});
			this.updnK3.Name = "updnK3";
			this.updnK3.Size = new System.Drawing.Size(56, 19);
			this.updnK3.TabIndex = 40;
			this.updnK3.ValueChanged += new System.EventHandler(this.updnK3_ValueChanged);
			this.updnK3.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnK2
			// 
			this.updnK2.Location = new System.Drawing.Point(120, 56);
			this.updnK2.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnK2.Name = "updnK2";
			this.updnK2.Size = new System.Drawing.Size(56, 19);
			this.updnK2.TabIndex = 39;
			this.updnK2.ValueChanged += new System.EventHandler(this.updnK2_ValueChanged);
			this.updnK2.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnK1
			// 
			this.updnK1.Location = new System.Drawing.Point(56, 56);
			this.updnK1.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnK1.Name = "updnK1";
			this.updnK1.Size = new System.Drawing.Size(56, 19);
			this.updnK1.TabIndex = 38;
			this.updnK1.ValueChanged += new System.EventHandler(this.updnK1_ValueChanged);
			this.updnK1.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(120, 8);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 16);
			this.label9.TabIndex = 72;
			this.label9.Text = "残りPP";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(184, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(56, 16);
			this.label10.TabIndex = 75;
			this.label10.Text = "Point Up";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.act1PP);
			this.panel1.Controls.Add(this.act1V);
			this.panel1.Controls.Add(this.act4PU);
			this.panel1.Controls.Add(this.act4PP);
			this.panel1.Controls.Add(this.act4V);
			this.panel1.Controls.Add(this.act3PU);
			this.panel1.Controls.Add(this.act3PP);
			this.panel1.Controls.Add(this.act3V);
			this.panel1.Controls.Add(this.act2PU);
			this.panel1.Controls.Add(this.act2PP);
			this.panel1.Controls.Add(this.act2V);
			this.panel1.Controls.Add(this.act1PU);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Location = new System.Drawing.Point(248, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(248, 128);
			this.panel1.TabIndex = 76;
			// 
			// updnID
			// 
			this.updnID.Location = new System.Drawing.Point(32, 88);
			this.updnID.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnID.Name = "updnID";
			this.updnID.Size = new System.Drawing.Size(56, 19);
			this.updnID.TabIndex = 77;
			this.updnID.ValueChanged += new System.EventHandler(this.updnID_ValueChanged);
			this.updnID.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// updnEx
			// 
			this.updnEx.Location = new System.Drawing.Point(56, 168);
			this.updnEx.Maximum = new System.Decimal(new int[] {
																   16777215,
																   0,
																   0,
																   0});
			this.updnEx.Name = "updnEx";
			this.updnEx.Size = new System.Drawing.Size(72, 19);
			this.updnEx.TabIndex = 78;
			this.updnEx.Value = new System.Decimal(new int[] {
																 16777215,
																 0,
																 0,
																 0});
			this.updnEx.ValueChanged += new System.EventHandler(this.updnEx_ValueChanged);
			this.updnEx.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 88);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(16, 16);
			this.label11.TabIndex = 79;
			this.label11.Text = "ID";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 168);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(48, 16);
			this.label12.TabIndex = 80;
			this.label12.Text = "経験値";
			// 
			// updnLv
			// 
			this.updnLv.Location = new System.Drawing.Point(64, 144);
			this.updnLv.Maximum = new System.Decimal(new int[] {
																   255,
																   0,
																   0,
																   0});
			this.updnLv.Name = "updnLv";
			this.updnLv.Size = new System.Drawing.Size(64, 19);
			this.updnLv.TabIndex = 81;
			this.updnLv.ValueChanged += new System.EventHandler(this.updnLv_ValueChanged);
			this.updnLv.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 144);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(40, 16);
			this.label13.TabIndex = 82;
			this.label13.Text = "Lv:";
			// 
			// updnHP
			// 
			this.updnHP.Location = new System.Drawing.Point(64, 192);
			this.updnHP.Maximum = new System.Decimal(new int[] {
																   65535,
																   0,
																   0,
																   0});
			this.updnHP.Name = "updnHP";
			this.updnHP.Size = new System.Drawing.Size(64, 19);
			this.updnHP.TabIndex = 83;
			this.updnHP.ValueChanged += new System.EventHandler(this.updnHP_ValueChanged);
			this.updnHP.Leave += new System.EventHandler(this.updn_Leave);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 192);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(40, 16);
			this.label14.TabIndex = 84;
			this.label14.Text = "残りHP";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.label8);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.updnH2);
			this.panel2.Controls.Add(this.updnH1);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.updnT1);
			this.panel2.Controls.Add(this.updnS3);
			this.panel2.Controls.Add(this.updnS2);
			this.panel2.Controls.Add(this.updnS1);
			this.panel2.Controls.Add(this.updnB3);
			this.panel2.Controls.Add(this.updnB2);
			this.panel2.Controls.Add(this.updnB1);
			this.panel2.Controls.Add(this.updnK3);
			this.panel2.Controls.Add(this.updnK2);
			this.panel2.Controls.Add(this.updnK1);
			this.panel2.Controls.Add(this.updnT3);
			this.panel2.Controls.Add(this.updnT2);
			this.panel2.Location = new System.Drawing.Point(248, 144);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(248, 152);
			this.panel2.TabIndex = 85;
			// 
			// poke
			// 
			this.poke.Location = new System.Drawing.Point(136, 8);
			this.poke.Name = "poke";
			this.poke.Size = new System.Drawing.Size(104, 20);
			this.poke.TabIndex = 86;
			// 
			// type1
			// 
			this.type1.Location = new System.Drawing.Point(176, 32);
			this.type1.Name = "type1";
			this.type1.Size = new System.Drawing.Size(64, 20);
			this.type1.TabIndex = 87;
			this.type1.SelectedIndexChanged += new System.EventHandler(this.type1_SelectedIndexChanged);
			// 
			// type2
			// 
			this.type2.Location = new System.Drawing.Point(176, 56);
			this.type2.Name = "type2";
			this.type2.Size = new System.Drawing.Size(64, 20);
			this.type2.TabIndex = 88;
			this.type2.SelectedIndexChanged += new System.EventHandler(this.type2_SelectedIndexChanged);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(32, 24);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(96, 19);
			this.txtName.TabIndex = 89;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// txtPare
			// 
			this.txtPare.Location = new System.Drawing.Point(32, 64);
			this.txtPare.Name = "txtPare";
			this.txtPare.Size = new System.Drawing.Size(96, 19);
			this.txtPare.TabIndex = 90;
			this.txtPare.Text = "";
			this.txtPare.TextChanged += new System.EventHandler(this.txtPare_TextChanged);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(136, 32);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(40, 16);
			this.label15.TabIndex = 91;
			this.label15.Text = "タイプ1";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(136, 56);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(40, 16);
			this.label16.TabIndex = 92;
			this.label16.Text = "タイプ2";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 48);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(16, 16);
			this.label17.TabIndex = 93;
			this.label17.Text = "親";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(8, 8);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(16, 16);
			this.label18.TabIndex = 94;
			this.label18.Text = "名";
			// 
			// state
			// 
			this.state.Location = new System.Drawing.Point(64, 216);
			this.state.Name = "state";
			this.state.Size = new System.Drawing.Size(64, 20);
			this.state.TabIndex = 95;
			this.state.SelectedIndexChanged += new System.EventHandler(this.state_SelectedIndexChanged);
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(8, 216);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(40, 16);
			this.label19.TabIndex = 96;
			this.label19.Text = "状態";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 272);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 24);
			this.button1.TabIndex = 97;
			this.button1.Text = "&OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lName
			// 
			this.lName.BackColor = System.Drawing.SystemColors.Window;
			this.lName.Location = new System.Drawing.Point(32, 8);
			this.lName.Name = "lName";
			this.lName.Size = new System.Drawing.Size(96, 16);
			this.lName.TabIndex = 98;
			// 
			// lPare
			// 
			this.lPare.BackColor = System.Drawing.SystemColors.Window;
			this.lPare.Location = new System.Drawing.Point(32, 48);
			this.lPare.Name = "lPare";
			this.lPare.Size = new System.Drawing.Size(96, 16);
			this.lPare.TabIndex = 99;
			// 
			// pkFrm1
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(496, 301);
			this.Controls.Add(this.lPare);
			this.Controls.Add(this.lName);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.state);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.txtPare);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.type2);
			this.Controls.Add(this.type1);
			this.Controls.Add(this.poke);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.updnHP);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.updnLv);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.updnEx);
			this.Controls.Add(this.updnID);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "pkFrm1";
			this.Text = "monster data";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.pkFrm1_Closing);
			((System.ComponentModel.ISupportInitialize)(this.act4PU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act4PP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act3PU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act3PP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act2PU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act2PP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act1PU)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.act1PP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnH2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnH1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnT3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnT2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnT1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnS3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnS2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnS1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnB3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnB2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnB1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnK3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnK2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnK1)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.updnID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnEx)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnLv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnHP)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

#region functions
		public void updnLeft(System.Windows.Forms.NumericUpDown updn){
			int a=System.Int32.Parse(updn.Text);
			if(a<updn.Minimum)a=(int)updn.Minimum;
			if(a>updn.Maximum)a=(int)updn.Maximum;
			updn.Value=a;
		}
		
		public string read_name(byte[] a){
			string x="";
			string l;
			for(int i=0;i<5;i++){
				l=file.dat2[a[i]];
				if(l=="(End)")return x;
				x+=l;
			}
			return x;
		}
		
		public byte[] write_name(string a){
			byte[] x=new byte[6];
			string al;
			int i,j;
			for(i=0;i<a.Length&i<5;i++){
				j=0;
				al=a.Substring(i,1);
				while(file.dat2[j]!=al){j++;if(j>255){j=230;goto break0;}}
				x[i]=(byte)j;
			}
			break0:/*break0:*/
			x[i]=0x50;i++;
			while(i<6){x[i]=0x1C;i++;}
			return x;
		}
		
		public void setByte(int number,int place){
			this.pDat[place]=(byte)number;
		}
		public void setWord(int number,int place){
			this.pDat[place]=(byte)(number/256);
			this.pDat[place+1]=(byte)(number%256);
		}
		public void setExpe(int number){
			this.pDat[14]=(byte)(number/65536);
			this.pDat[15]=(byte)((number-this.pDat[14])/256);
			this.pDat[16]=(byte)(number%256);
		}

		public void setKB(){this.setByte(((int)this.updnK3.Value<<4)+(int)this.updnB3.Value,27);}
		public void setST(){this.setByte(((int)this.updnS3.Value<<4)+(int)this.updnT3.Value,28);}
		public void setPUPP(int index){
			switch(index){
				case 1:this.setByte(((int)this.act1PU.Value<<6)+(int)this.act1PP.Value,29);break;
				case 2:this.setByte(((int)this.act2PU.Value<<6)+(int)this.act2PP.Value,30);break;
				case 3:this.setByte(((int)this.act3PU.Value<<6)+(int)this.act3PP.Value,31);break;
				case 4:this.setByte(((int)this.act4PU.Value<<6)+(int)this.act4PP.Value,32);break;
			}
		}
#endregion
		
		
		private void txtName_TextChanged(object sender, System.EventArgs e)
		{	
			byte[] x=this.write_name(this.txtName.Text);
			for(int i=0;i<6;i++)this.pNm[i]=x[i];
			this.lName.Text=this.read_name(this.pNm);
		}
		private void txtPare_TextChanged(object sender, System.EventArgs e)
		{
			byte[] x=this.write_name(this.txtPare.Text);
			for(int i=0;i<6;i++)this.pPrt[i]=x[i];
			this.lPare.Text=this.read_name(this.pPrt);
		}

		private void updnHP_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnHP.Value,1);}
		private void updnID_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnID.Value,12);}

		private void updnEx_ValueChanged(object sender, System.EventArgs e){this.setExpe((int)this.updnEx.Value);}
		private void updnH2_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnH2.Value,17);}
		private void updnK2_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnK2.Value,19);}
		private void updnB2_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnB2.Value,21);}
		private void updnS2_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnS2.Value,23);}
		private void updnT2_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnT2.Value,25);}
		private void updnK3_ValueChanged(object sender, System.EventArgs e){this.setKB();}
		private void updnB3_ValueChanged(object sender, System.EventArgs e){this.setKB();}
		private void updnS3_ValueChanged(object sender, System.EventArgs e){this.setST();}
		private void updnT3_ValueChanged(object sender, System.EventArgs e){this.setST();}
		private void act1PP_ValueChanged(object sender, System.EventArgs e){this.setPUPP(1);}
		private void act1PU_ValueChanged(object sender, System.EventArgs e){this.setPUPP(1);}
		private void act2PP_ValueChanged(object sender, System.EventArgs e){this.setPUPP(2);}
		private void act2PU_ValueChanged(object sender, System.EventArgs e){this.setPUPP(2);}
		private void act3PP_ValueChanged(object sender, System.EventArgs e){this.setPUPP(3);}
		private void act3PU_ValueChanged(object sender, System.EventArgs e){this.setPUPP(3);}
		private void act4PP_ValueChanged(object sender, System.EventArgs e){this.setPUPP(4);}
		private void act4PU_ValueChanged(object sender, System.EventArgs e){this.setPUPP(4);}
		private void updnLv_ValueChanged(object sender, System.EventArgs e){this.setByte((int)this.updnLv.Value,33);}
		private void updnH1_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnH1.Value,34);}
		private void updnK1_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnK1.Value,36);}
		private void updnB1_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnB1.Value,38);}
		private void updnS1_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnS1.Value,40);}
		private void updnT1_ValueChanged(object sender, System.EventArgs e){this.setWord((int)this.updnT1.Value,42);}

		private void act1V_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[8]=(byte)this.act1V.SelectedIndex;}
		private void act2V_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[9]=(byte)this.act2V.SelectedIndex;}
		private void act3V_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[10]=(byte)this.act3V.SelectedIndex;}
		private void act4V_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[11]=(byte)this.act4V.SelectedIndex;}
		private void type1_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[5]=(byte)this.type1.SelectedIndex;}
		private void type2_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[6]=(byte)this.type2.SelectedIndex;}
		private void state_SelectedIndexChanged(object sender, System.EventArgs e){this.pDat[4]=(byte)this.state.SelectedIndex;}

		private void pkFrm1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//合計のByte確認check sumに引っかからないようにする
			int total0=0;
			for(int i=0;i<44;i++)total0+=this.pDat[i];
			for(int i=0;i<6;i++)total0+=this.pNm[i];
			for(int i=0;i<6;i++)total0+=this.pPrt[i];
			total0=total0%256;
			this.pDat[2]=(byte)((this.pDat[2]+this.total+256-total0)%0x100);
			//*/*/Console.WriteLine(this.total.ToString()+" >> "+total0.ToString()+" + "+((this.total+256-total0)%0x100).ToString());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void updn_Leave(object sender, System.EventArgs e){
			System.Windows.Forms.NumericUpDown updn=(System.Windows.Forms.NumericUpDown)sender;
			int a=System.Int32.Parse(updn.Text);
			if(a<updn.Minimum)a=(int)updn.Minimum;
			if(a>updn.Maximum)a=(int)updn.Maximum;
			updn.Value=a;
		}



	}
}
