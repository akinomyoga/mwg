using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace mwgBinary
{
	/// <summary>
	/// Form2 の概要の説明です。
	/// </summary>
	public class palEdit : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public palEdit()
		{
			InitializeComponent();
			
		}
		
		public palEdit(string path){
			InitializeComponent();
			this.pathname=path;
			this.readFile(path);
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// palEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(304, 269);
			this.Name = "palEdit";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);

		}
		#endregion


		private string pathname;
		private void Form2_Load(object sender, System.EventArgs e)
		{
		
		}
		private void readFile(string filename){
			if(!System.IO.File.Exists(filename))
			{
				//Console.Write("指定したファイルは存在しません\n");
				return;
			}
			string rbfn=filename;
			System.IO.FileStream rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			System.IO.BinaryReader rbr=new System.IO.BinaryReader(rbf);
				
			
			int imax=(int)(new System.IO.FileInfo(filename).Length);//ファイルサイズ取得
			//Console.Write("ファイルサイズを取得しました - "+imax.ToString()+"\n");
			for(int i=0;i<imax;i++)
			{//読込部分
				byte[] a=rbr.ReadBytes(4);
				int b=(int)a[0];
			}
			//Console.Write("読込完了\n");
			rbr.Close();
			rbf.Close();
		}
		
	}
}
