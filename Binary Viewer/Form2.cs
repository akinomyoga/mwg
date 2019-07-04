using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace mwgBinary
{
	/// <summary>
	/// Form2 �̊T�v�̐����ł��B
	/// </summary>
	public class palEdit : System.Windows.Forms.Form
	{
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
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
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
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
				//Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			string rbfn=filename;
			System.IO.FileStream rbf=new System.IO.FileStream(rbfn,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			System.IO.BinaryReader rbr=new System.IO.BinaryReader(rbf);
				
			
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			//Console.Write("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
			for(int i=0;i<imax;i++)
			{//�Ǎ�����
				byte[] a=rbr.ReadBytes(4);
				int b=(int)a[0];
			}
			//Console.Write("�Ǎ�����\n");
			rbr.Close();
			rbf.Close();
		}
		
	}
}
