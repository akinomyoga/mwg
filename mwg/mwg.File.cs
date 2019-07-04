using System;

namespace mwg.File{
	
	/// <summary>
	/// byte[] �̃f�[�^���Ǘ�����N���X�ł��B
	/// �t�@�C������̓ǂݎ��A�ׂ����w��ɂ��r�b�g�}�b�v�C���[�W�ւ̕ϊ��A�X�g���[���ǂݎ��ɑΉ����Ă��܂��B
	/// �t�@�C������ǂݎ��ꍇ�́A�ÓI�����o��ʂ��� byte[] �𓾂邩�A�R���X�g���N�^��ʂ��� mwgBinary �̃C���X�^���X�𓾂ĉ������B
	/// �ׂ����w��ɂ��r�b�g�}�b�v�C���[�W�ւ̕ϊ��́A<see cref="ToBitmap"/> �֐���ʂ��čs���܂��B
	/// </summary>
	public class mwgBinary{
		//=====================================
		//          fields
		//-------------------------------------
		/// <summary>
		/// ���݂̓ǂݎ��ʒu�������܂��B�f�[�^�̐擪�� 0 �ł��B���́A�ϐ��Ŏw�肳��Ă���f�[�^���̈ʒu���A���̓ǂݎ��J�n�ʒu�ł��B
		/// </summary>
		public int current;
		/// <summary>
		/// �ێ����� Binary �f�[�^�̖{�̂ł��B
		/// </summary>
		public byte[] binaryData;
		/// <summary>
		/// ���ɂȂ�A�܂��́A�ۑ���ł���t�@�C������ێ����܂��B�ۑ��悪����̏ꍇ�́Afilename=="%blank%" �ƂȂ�܂��B
		/// </summary>
		public string filename;
		//=====================================
		//          constructors
		//-------------------------------------
		/// <summary>
		/// mwgBinary �R���X�g���N�^�B�w�肳�ꂽ�t�@�C���̓��e��ǂݏo���āAmwgBinary �̃C���X�^���X���쐬���܂��B
		/// </summary>
		/// <param name="filename">�t�@�C�������w�肵�܂��B</param>
		public mwgBinary(string filename){
			this.binaryData=mwgBinary.WholeFileInBinary(filename);
			this.filename=filename;
			this.current=0;
		}
		/// <summary>
		/// mwgBinary �R���X�g���N�^�B�w�肵�� byte[] �����ɂ��āAmwgBinary �̃C���X�^���X���쐬���܂��B
		/// </summary>
		/// <param name="BinaryData">���ɂȂ� byte[] ���w�肵�܂��B</param>
		public mwgBinary(byte[] BinaryData){
			this.binaryData=BinaryData;
			this.filename="%blank%";
			this.current=0;
		}

		#region read and write
		//=====================================
		//          Methods Write
		//-------------------------------------
		//�������݂̊֐�();
		public void writeBytes(byte[] data){
			int iM=this.binaryData.Length;
			if(this.current==iM){
				System.Console.WriteLine("mwg.File.mwgBinary\n\t�w�肳�ꂽ�f�[�^���������ގ����o���܂���B�������݈ʒu�����ɖ��[�ɒB���Ă��܂��B");
				return;
			}
			if(this.current+data.Length>iM){
				for(int i=this.current;i<iM;i++){
					this.binaryData[i]=data[i-this.current];
				}
				this.current=iM;
				//�������݂���Ȃ������f�[�^�̒ʒm�B
				System.Console.WriteLine("mwgBinary �Ƀf�[�^����������܂���B�r���܂ł�����������ł��܂���B");
			}else{
				data.CopyTo(this.binaryData,this.current);
				this.current+=data.Length;
			}
		}
		//�ۑ��̊֐�();
		public void SaveToFile(){
			if(this.filename==null||this.filename=="%blank%")return;
			System.IO.FileStream fs=new System.IO.FileStream(this.filename,System.IO.FileMode.OpenOrCreate,System.IO.FileAccess.Write);
			System.IO.BinaryWriter br=new System.IO.BinaryWriter(fs);
			br.Write(this.binaryData);
			System.Console.WriteLine("mwg.dll: "+this.filename+" �� byte[] ��ۑ����܂���");
			br.Close();
			fs.Close();
		}
		public void Set(int index,byte[] a){
			if(index>=this.binaryData.Length)return;
			int im=(index+a.Length>this.binaryData.Length)?this.binaryData.Length-index:a.Length;
			for(int i=0;i<im;i++){
				this.binaryData[index+i]=a[i];
			}
		}
		//=====================================
		//          Methods Read
		//-------------------------------------
		public byte readByte(){
			if(this.current==this.binaryData.Length)throw new mwg.File.mwgBinary.EndOfDataException();
			return this.binaryData[this.current++];
		}
		public byte[] readBytes(uint length){
			if(length>System.Int32.MaxValue)throw new System.Exception("�ǂݎ��̒��������߂��܂��̂ŏ����o���܂���");
			return this.readBytes((int)length);
		}
		public byte[] readBytes(int length){
			if(length<0)throw new System.Exception("���� length �ɖ����Ȓl���w�肳��܂����B0 ���� ���̐��͎w��o���܂���B");
			if(length==0){
				return new byte[]{};
			}
			try{
				byte[] r=this.preReadBytes(length);
				this.current+=length;
				return r;
			}catch(mwg.File.mwgBinary.EndOfDataException e){
				this.current=this.binaryData.Length;
				throw e;
			}
		}
		public byte[] preReadBytes(uint length){
			if(length>System.Int32.MaxValue)throw new System.Exception("�ǂݎ��̒��������߂��܂��̂ŏ����o���܂���");
			return this.preReadBytes((int)length);
		}
		public byte[] preReadBytes(int length){
			if(length>0){
				int i=this.binaryData.Length;
				if(current+length>i)throw new System.Exception("�f�[�^�̏I���܂ŗ��܂����B����ȏ�̃f�[�^�͂���܂���B");
				if(length==0)return new byte[]{};
				byte[] r=new byte[length];
				for(i=0;i<length;i++){
					r[i]=this.binaryData[i+current];
				}
				return r;
			}else throw new System.Exception("���� length �ɖ����Ȓl���w�肳��܂����B0 ���� ���̐��͎w��o���܂���B");
		}
		public class EndOfDataException:System.Exception{}
		/// <summary>
		/// ���݂̓ǂݎ��ʒu��O��Ɉړ����܂��B
		/// �擪�ʒu�����O���w�肷��ƁA�擪�ʒu�Ɉړ����܂��B�����ʒu��������w�肷��ƁA�ǂݎ��I���ʒu�Ɉړ����܂��B
		/// </summary>
		/// <param name="length">�ǂꂾ���A�ǂݎ��ʒu��i�߂邩���w�肵�܂��B���̒l���w�肵�܂��ƁA�ǂݎ��ʒu��O�Ɉړ����܂��B</param>
		public void Move(int length){
			this.current=length;
			if(this.current<0)this.current=0;
			else if(this.current>this.binaryData.Length)this.current=this.binaryData.Length;
		}
		public int RestLength{
			get{return this.binaryData.Length-this.current;}
		}
		#endregion

		//=====================================
		//          Method Transformation
		//-------------------------------------
		#region ToBitmap()
		/// <summary>
		/// string rule �Ɏw�肵��������ɂ��A�ێ����Ă���f�[�^�́A�摜�ւ̐F�X�ȕϊ����s���܂��B
		/// </summary>
		/// <param name="rule">�ϊ��̕��@���w�肷������ł��B������Ŏw�肵�܂��B�����́u�@���v�����Ԃɕ��ׂĕ\���������ł��B�ڍׂɂ��ẮAToBitmap.htm ���Q�Ɖ������B</param>
		/// <param name="offset">�f�[�^�̓ǂݎ��J�n�ʒu���w�肵�܂��B�擪�́A 0 �ŕ\����܂��B</param>
		/// <returns>�o�C�i���f�[�^���A�K���Ȗ@���ɂ���ĉ摜�ɕϊ����āASystem.Drawing.Bitmap �Ƃ��ĕԂ��܂��B</returns>
		public System.Drawing.Bitmap ToBitmap(string rule,int offset){
			string[] rule2=rule.Split(';');
			int im=rule2.Length;//rule2 �v�f��
			//��1���悸�A���ߗ�̓ǂݎ��ƁA�S�̂łǂ�ʂ̃f�[�^�ʂ��g�p���邩�v�Z�B
			//TODO : leftABC ���̕��������͂���ƁAInt32.Parse �ŃG���[

			int length=1; //�g�p����Ǝv����f�[�^�̗�
			int[,] commands=new int[im,2];
			int ic=0;//���݂� commands ���̈ʒu // �I���� commands �v�f��

			int i; //���݂� rule2 ���̈ʒu
			bool blColored=false;//�F���w�肳�ꂽ���������t���O�B��d�ɁA�z�F���w�肷�鎖��h���ׁB
			string ruleNo;//rule2 �̐�������
			for(i=0;i<im;i++){
				rule2[i]=rule2[i].Trim();
				//�����ו��Ɋւ���u�@���v
				if(rule2[i].StartsWith("left")){
					commands[ic,0]=0;
					ruleNo=rule2[i].Substring(4);
					commands[ic,1]=("*"==ruleNo)?ToBitmap_kiriage(this.binaryData.Length,length):System.Int32.Parse(ruleNo);
					if(commands[ic,0]>0)length*=commands[ic++,1];
				}
				else if(rule2[i].StartsWith("right")){
					commands[ic,0]=1;
					ruleNo=rule2[i].Substring(5);
					commands[ic,1]=("*"==ruleNo)?ToBitmap_kiriage(this.binaryData.Length,length):System.Int32.Parse(ruleNo);
					if(commands[ic,0]>0)length*=commands[ic++,1];
				}
				else if(rule2[i].StartsWith("top")){
					commands[ic,0]=2;
					ruleNo=rule2[i].Substring(3);
					commands[ic,1]=("*"==ruleNo)?ToBitmap_kiriage(this.binaryData.Length,length):System.Int32.Parse(ruleNo);
					if(commands[ic,0]>0)length*=commands[ic++,1];
				}
				else if(rule2[i].StartsWith("bottom")){
					commands[ic,0]=3;
					ruleNo=rule2[i].Substring(6);
					commands[ic,1]=("*"==ruleNo)?ToBitmap_kiriage(this.binaryData.Length,length):System.Int32.Parse(ruleNo);
					if(commands[ic,0]>0)length*=commands[ic++,1];
				}
				//���[�x depth �Ɋւ���u�@���v
				else if(rule2[i].StartsWith("big")){
					commands[ic,0]=4;
					commands[ic,1]=System.Int32.Parse(rule2[i].Substring(3));
					if(commands[ic,1]>0)length*=commands[ic++,1];
				}
				else if(rule2[i].StartsWith("little")){
					commands[ic,0]=5;
					commands[ic,1]=System.Int32.Parse(rule2[i].Substring(6));
					if(commands[ic,1]>0)length*=commands[ic++,1];
				}
				//���z�F�Ɋւ���u�@���v
				else{
					if(!blColored){
						blColored=true;
						switch(rule2[i].ToLower()){
							case "rgb":
								commands[ic++,0]=10;
								length*=3;
								break;
							case "argb":
								commands[ic++,0]=11;
								length*=4;
								break;
							case "rgba":
								commands[ic++,0]=12;
								length*=4;
								break;
							case "bgr":
								commands[ic++,0]=20;
								length*=3;
								break;
							case "abgr":
								commands[ic++,0]=21;
								length*=4;
								break;
							case "bgra":
								commands[ic++,0]=22;
								length*=4;
								break;
								//�����Ƒ��̉\���ɂ��Ă��l����Bcmy �ȂǁB
							default:
								blColored=false;
								break;
						}
					}
				}

			}
			//��2���f�[�^��Ǎ��A��U
			byte[] data=this.ToBitData();
			int [,,,] data0;// [iI�r�b�g�}�b�v�ԍ�,iX��,iY�c,iC argb]
			data0=new int[length,1,1,1];
			if(length>=data.Length){
				for(i=0;i<data.Length;i++)data0[i,0,0,0]=data[i];
				for(i=data.Length;i<length;i++)data0[i,0,0,0]=0;//0��₤�B
			}else{
				for(i=0;i<length;i++)data0[i,0,0,0]=data[i];
			}

			//��3�����ۂɁA�r�b�g�}�b�v���\��
			int iIm=length;int iXm=1;int iYm=1;int iCm=1;//�ϊ��O�̔z��̑傫���B�����l�́u���̃f�[�^�v�̔z��̑傫���B

			int [,,,] data1;//�ϊ���̔z����i�[�B
			int iIm2;//�ϊ����Bitmap�z��̑傫��

			int iI,iX,iY,iC;
			//iI �� ��ڂ́u�ϊ���r�b�g�}�b�v�v���B
			//iX �́u�ϊ��O�r�b�g�}�b�v�v�ɉ�����x���W�B
			//iY �� y���W(����)
			//iC �� �F�̎��(����)
			// i �͂����ł͈�́u�ϊ���r�b�g�}�b�v�v���̊�ڂ́u�ϊ��O�r�b�g�}�b�v�v����\���l�Ƃ��ė��p����B
			
			for(i=0;i<ic;i++){
				/*dummy to compile 'data0 = data1;'*/data1=new int[1,1,1,1];//TODO: ���ׂāAdata1 ���쐬���郍�W�b�N��������������B
				switch(commands[i,0]){
					case 0://left
						iIm2=ToBitmap_kiriage(iIm,commands[i,1]);
						data1=new int[iIm2,iXm*commands[i,1],iYm,iCm];
						for(iI=0;iI<iIm2;i++)
							for(i=0;i<commands[i,1];i++)
								for(iX=0;iX<iXm;iX++)for(iY=0;iY<iYm;iY++)for(iC=0;iC<iCm;iC++){
									data1[iI,i*iXm+iX,iY,iC]=data0[iI*commands[i,1]+i,iX,iY,iC];
								}
						iXm*=commands[i,1];
						break;
					case 1://right
						iIm2=ToBitmap_kiriage(iIm,commands[i,1]);
						data1=new int[iIm2,iXm*commands[i,1],iYm,iCm];
						for(iI=0;iI<iIm2;i++)
							for(i=0;i<commands[i,1];i++)
								for(iX=0;iX<iXm;iX++)for(iY=0;iY<iYm;iY++)for(iC=0;iC<iCm;iC++){
									data1[iI,i*iXm+iXm-iX-1,iY,iC]=data0[iI*commands[i,1]+i,iX,iY,iC];
								}
						iXm*=commands[i,1];
						break;
					case 2://top
						iIm2=ToBitmap_kiriage(iIm,commands[i,1]);
						data1=new int[iIm2,iXm*commands[i,1],iYm,iCm];
						for(iI=0;iI<iIm2;i++)
							for(i=0;i<commands[i,1];i++)
								for(iX=0;iX<iXm;iX++)for(iY=0;iY<iYm;iY++)for(iC=0;iC<iCm;iC++){
									data1[iI,iX,i*iYm+iY,iC]=data0[iI*commands[i,1]+i,iX,iY,iC];
								}
						iYm*=commands[i,1];
						break;
					case 3://bottom
						iIm2=ToBitmap_kiriage(iIm,commands[i,1]);
						data1=new int[iIm2,iXm*commands[i,1],iYm,iCm];
						for(iI=0;iI<iIm2;i++)
							for(i=0;i<commands[i,1];i++)
								for(iX=0;iX<iXm;iX++)for(iY=0;iY<iYm;iY++)for(iC=0;iC<iCm;iC++){
									data1[iI,iX,i*iYm+iYm-iY-1,iC]=data0[iI*commands[i,1]+i,iX,iY,iC];
								}
						iYm*=commands[i,1];
						break;
					case 4:
						//TODO: �܂��r��
						break;
					case 5:break;

					case 10:
						break;
					case 11:
						break;
					case 12:
						break;
					case 20:
						break;
					case 21:
						break;
					case 22:
						break;
				}
				data0=data1;

			}

			//���ۂɕԂ��l�́Adata0[0] �� System.Drawing.Bitmap �ɕϊ��������B
			if(blColored){
				//TODO: RGBA���g�p���āABitmap ���쐬����B
			}else{
				//TODO: RGB �̋������S�ē����� Bitmap ���쐬����B
			}
			/*dummy to compile*/return new System.Drawing.Bitmap(10,10,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
		}
		/// <summary>
		/// string rule �Ɏw�肵��������ɂ��A�ێ����Ă���f�[�^�́A�摜�ւ̐F�X�ȕϊ����s���܂��B
		/// </summary>
		/// <param name="rule">�ϊ��̕��@���w�肷������ł��B������Ŏw�肵�܂��B�����́u�@���v�����Ԃɕ��ׂĕ\���������ł��B�ڍׂɂ��ẮAToBitmap.htm ���Q�Ɖ������B</param>
		/// <returns>�o�C�i���f�[�^���A�K���Ȗ@���ɂ���ĉ摜�ɕϊ����āASystem.Drawing.Bitmap �Ƃ��ĕԂ��܂��B</returns>
		public System.Drawing.Bitmap ToBitmap(string rule)
		{
			return this.ToBitmap(rule,0);
		}//*/
		/// <summary>
		/// a/b �̐؂�グ��Ԃ��֐�
		/// </summary>
		/// <param name="a">�폜��(�����鐔)</param>
		/// <param name="b">�@(���鐔)</param>
		/// <returns>ceiling(a/b)</returns>
		private int ToBitmap_kiriage(int a,int b){
			int r=(int)(a/b);
			if(r*b<a)r++;
			return r;
		}
		#endregion
		
		/// <summary>
		/// �ێ����Ă���o�C�g��f�[�^���A�r�b�g��ɕϊ����ĕԂ��܂��BLE(���g���G���f�B�A��)�B
		/// </summary>
		/// <returns>�ϊ����ʂ��i�[���� byte[] �ł��B[1]_2 �� [1]_256�A[0]_2 �� [0]_256 �ŕ\������܂��B</returns>
		public byte[] ToBitData(){
			int im=this.binaryData.Length;
			byte[] r=new byte[im*8];
			byte bin;
			for(int i=0;i<im;i++){
				bin=this.binaryData[i];
				for(int j=0;j<7;j++){
					r[i*8+j]=(byte)(bin%2);
					bin/=2;
				}
				r[i+8+7]=(byte)(bin%2);
			}
			return r;
		}

		//=====================================
		//          operators
		//-------------------------------------
		public static explicit operator byte[](mwgBinary a){return a.binaryData;}
		public static explicit operator mwgBinary(byte[] a){return new mwgBinary(a);}
		public static mwgBinary operator +(mwg.File.mwgBinary a,mwg.File.mwgBinary b){
			byte[] r=new byte[a.binaryData.Length+b.binaryData.Length];
			a.binaryData.CopyTo(r,0);
			b.binaryData.CopyTo(r,a.binaryData.Length);
			return (mwg.File.mwgBinary)r;
		}
		//=====================================
		//          static member
		//-------------------------------------
		/// <summary>
		/// �w�肳�ꂽ�t�@�C���̓��e���A�o�C�i���őS�Ĕ����o���܂��B
		/// </summary>
		/// <param name="filename">�t�@�C�������w�肵�܂��B</param>
		/// <returns>byte[] �Ƀt�@�C���̓��e���i�[���ĕԂ��܂��B</returns>
		public static byte[] WholeFileInBinary(string filename){
			//�o�C�i���f�[�^�̓Ǎ�
			if(!System.IO.File.Exists(filename)){
				System.Console.Write("�w�肵���t�@�C���͑��݂��܂���\n");
				return new byte[]{};
			}
			byte[] rtn;
			try{
				System.IO.FileStream fs=new System.IO.FileStream(filename,System.IO.FileMode.Open,System.IO.FileAccess.Read);
				System.IO.BinaryReader br=new System.IO.BinaryReader(fs);
				int filelen=(int)(new System.IO.FileInfo(filename).Length);
				rtn=br.ReadBytes(filelen);
				br.Close();
				fs.Close();
			}catch{throw;}
			return rtn;
		}
	}
	
	public abstract class mb{
		//--constructor
		public mb(mwg.File.mwgBinary mbin){}
		public mb(byte[] mbin){}
		public mb(){}
		//--methods
		public void WriteTo(mwg.File.mwgBinary mbin){
			if(this.Length>mbin.RestLength)throw new mwg.File.mwgBinary.EndOfDataException();
			mbin.writeBytes((byte[])this);
		}
		public abstract byte[] ToBinary();
		public virtual mwg.File.mwgBinary ToMwgBinary(){
			return (mwg.File.mwgBinary)this.ToBinary();
		}
		//--properties
		public abstract int Length{get;}
		//--operators
		public static explicit operator byte[](mb a){return a.ToBinary();}
		public static explicit operator mwg.File.mwgBinary(mb a){return a.ToMwgBinary();}
	}
	public class mbReadingException:System.Exception{
		public string x;
		public mbReadingException(){x="";}
		public mbReadingException(string desc,string location){
			x="";
			x+="\n����: "+desc;
			x+="\n\n�G���[�̋N�������ꏊ: "+location;
		}
		public override string Message{
			get{
				return "mb �I�u�W�F�N�g�� �f�[�^����ǂݍ���ł���r���ŃG���[�������܂����B"+x;
			}
		}

	}

	#region mbInt32
	public class mbInt32:mb{
		int dat;
		public mbInt32(mwg.File.mwgBinary mbin){
			this.dat=0;
			if(mbin.RestLength<4)return;
			dat=(int)mbin.readByte();
			if(dat>127){//���̐�
				dat-=128;
				for(int i=0;i<3;i++)dat=dat*256+(int)mbin.readByte();
				dat+=System.Int32.MinValue;
			}else{//���̐�
				for(int i=0;i<3;i++)dat=dat*256+(int)mbin.readByte();
			}
		}
		public mbInt32(byte[] data){
			if(data.Length!=4){
				this.dat=0;
				throw new System.Exception("�����Ƃ��ēn���ꂽ byte[] �̒������s�����Ă��܂��B�C���X�^���X�𐳂����쐬���鎖���o���܂���B");//return;
			}
			dat=(int)data[0];
			if(dat>127){
				this.dat-=128;
				for(int i=1;i<4;i++)dat=dat*256+(int)data[i];
				this.dat+=System.Int32.MinValue;
			}else{
				for(int i=1;i<4;i++)dat=dat*256+(int)data[i];
			}
		}
		public mbInt32(int data){
			this.dat=data;
		}
		//meth
		public override byte[] ToBinary(){
			byte[] r=new byte[4];
			int x=this.dat;
			if(x>=0){//x ���̎�
				for(int i=0;i<4;i++){
					r[3-i]=(byte)(x%256);
					x/=256;
				}
			}else{//x ���̎�
				x-=System.Int32.MinValue;
				for(int i=0;i<4;i++){
					r[3-i]=(byte)(x%256);
					x/=256;
				}
				r[0]+=128;
			}
			return r;
		}
		//prp
		public override int Length{get{return 4;}}
		//op
		public static explicit operator int(mwg.File.mbInt32 a){return a.dat;}
		public static explicit operator mwg.File.mbInt32(int a){return new mwg.File.mbInt32(a);}
		public static explicit operator mbInt32(byte[] a){return new mbInt32(a);}
		public static explicit operator mbInt32(mwg.File.mwgBinary a){return new mbInt32(a);}
	}
	#endregion

	#region mbString
	public class mbString:mb{
		public string dat;
		//constructor
		public mbString(mwg.File.mwgBinary mbin){
			System.Text.UnicodeEncoding uni=new System.Text.UnicodeEncoding();
			this.dat=uni.GetString(mbin.readBytes((uint)(new mwg.File.mbUInt32(mbin))));
		}
		public mbString(byte[] data){
			System.Text.UnicodeEncoding uni=new System.Text.UnicodeEncoding();
			uint x=(uint)(new mwg.File.mbUInt32(data));
			if(x>System.Int32.MaxValue){
				throw new System.Exception("������̑傫�����A����̌��E�𒴂��Ă��܂��B");
			}
			if(x==0){this.dat="";return;}
			this.dat=uni.GetString(data,4,(int)x);
		}
		public mbString(string str){this.dat=str;}
		//property
		public override int Length{
			get{return 4+(new System.Text.UnicodeEncoding()).GetByteCount(this.dat);}
		}
		//method
		public override byte[] ToBinary(){
			int l=this.Length;
			byte[] r0=new byte[l];
			((byte[])(mbInt32)(l-4)).CopyTo(r0,0);//�����̏�����������
			(new System.Text.UnicodeEncoding()).GetBytes(this.dat,0,this.dat.Length,r0,4);//���ۂ̓��e����������
			return r0;
		}
		public override string ToString(){
			return (string)this;
		}
		//operator
		public static explicit operator mbString(string a){return new mwg.File.mbString(a);}
		public static explicit operator string(mbString a){return a.dat;}
		public static explicit operator mbString(byte[] a){return new mbString(a);}
		public static explicit operator mbString(mwg.File.mwgBinary a){return new mbString(a);}
	}
	#endregion

	#region mbBool
	public class mbBool:mb{
		bool dat;
		public const byte True=(byte)128;
		public const byte False=(byte)0;
		public mbBool(mwg.File.mwgBinary mbin){
			this.dat=(127<(int)mbin.readByte());
		}
		public mbBool(byte[] a){
			this.dat=(a.Length!=0)&&(((int)(a[0])&128)!=0);
		}
		public mbBool(byte a){
			this.dat=((int)a&128)!=0;
		}
		public mbBool(bool data){
			this.dat=data;
		}
		public override byte[] ToBinary()
		{
			return new byte[]{(byte)(this.dat?128:0)};
		}
		public override int Length{get{return 1;}}
		public static explicit operator mbBool(bool a){return new mwg.File.mbBool(a);}
		public static explicit operator bool(mbBool a){return a.dat;}
		public static explicit operator mbBool(byte[] a){return new mbBool(a);}
		public static explicit operator mbBool(mwg.File.mwgBinary a){return new mbBool(a);}
	}
	#endregion

	#region mbUInt32
	public class mbUInt32:mb{
		//field
		uint dat;
		//constructor
		public mbUInt32(mwg.File.mwgBinary mbin){
			this.dat=0;
			if(mbin.RestLength<4)return;
			for(int i=0;i<4;i++){
				this.dat=this.dat*256+(uint)mbin.readByte();
			}
		}
		public mbUInt32(byte[] data){
			if(data.Length==0){this.dat=0;return;}
			this.dat=(uint)data[0];
			if(data.Length==1)return;
			int im=(data.Length>4)?4:data.Length;//mwgString �� size ��ǂގ��Ɏg�p����̂ŁA4 bytes ���傫���ꍇ������B
			for(int i=1;i<im;i++){
				this.dat=this.dat*256+(uint)data[i];
			}
		}
		public mbUInt32(uint a){
			this.dat=a;
		}
		//methods
		public override byte[] ToBinary(){
			byte[] r=new byte[4];
			uint x=this.dat;
			for(int i=0;i<3;i++){
				r[i]=(byte)(x%256);
				x/=256;
			}
			r[3]=(byte)x;
			return r;
		}
		//property
		public override int Length{get{return 4;}}
		//operator
		public static explicit operator uint(mwg.File.mbUInt32 a){return a.dat;}
		public static explicit operator mwg.File.mbUInt32(uint a){return new mwg.File.mbUInt32(a);}
		public static explicit operator mbUInt32(byte[] a){return new mbUInt32(a);}
		public static explicit operator mbUInt32(mwg.File.mwgBinary a){return new mbUInt32(a);}
	}

	#endregion

	#region mbFourcc
	public class mbFourcc:mb{
		byte[] dat;
		public static byte[] Null=new byte[]{(byte)32,(byte)32,(byte)32,(byte)32};
		//constructor
		public mbFourcc(mwg.File.mwgBinary mbin){
			if(mbin.RestLength<4)this.dat=mwg.File.mbFourcc.Null;
			else this.dat=mbin.readBytes(4);
		}
		public mbFourcc(byte[] x){
			if(x.Length<4)
				this.dat=mwg.File.mbFourcc.Null;
			else{
				this.dat=new byte[4];
				for(int i=0;i<4;i++)this.dat[i]=x[i];
			}
		}
		public mbFourcc(string str){
			while(str.Length<4)str+=" ";
			System.Text.ASCIIEncoding asc=new System.Text.ASCIIEncoding();
			this.dat=new byte[4];
			asc.GetBytes(str,0,4,this.dat,0);
		}
		//methods & properties
		public override byte[] ToBinary(){return this.dat;}
		public override int Length{get{return 4;}}
		public override string ToString(){
			System.Text.ASCIIEncoding asc=new System.Text.ASCIIEncoding();
			return asc.GetString(this.dat);
		}
		//operator
		public static explicit operator string(mwg.File.mbFourcc a){return a.ToString();}
		public static explicit operator mwg.File.mbFourcc(string a){return new mwg.File.mbFourcc(a);}
		public static explicit operator mwg.File.mbFourcc(byte[] a){return new mwg.File.mbFourcc(a);}
		public static explicit operator mwg.File.mbFourcc(mwg.File.mwgBinary a){return new mwg.File.mbFourcc(a);}
	}
	#endregion

	#region mbBytes
	public class mbBytes:mb{
		public byte[] dat;
		//constructor
		public mbBytes(mwg.File.mwgBinary mbin){
			this.dat=mbin.readBytes((uint)(new mwg.File.mbUInt32(mbin)));
		}
		public mbBytes(byte[] data,bool incLen){
			if(incLen)this.dat=data;
			else if(data.Length<=4)this.dat=new byte[]{};
			else {
				uint len0=(uint)(mwg.File.mbUInt32)data;
				if(len0+4>System.Int32.MaxValue)throw new System.Exception("�o�C�i����̒���������̌��E�𒴂��Ă��܂��B");
				int len=(int)len0;
				this.dat=new byte[len];//�]�ʐ�̗p��
				if(data.Length<4+len)len=data.Length-4;//�]�ʔ͈�(�ǂݎ�茳���)
				if(len!=0)for(int i=0;i<len;i++)this.dat[i]=data[4+i];//�]��
			}
		}
		//methods and properties
		public override int Length{get{return 4+this.dat.Length;}}
		public override byte[] ToBinary(){
			byte[] r=new byte[4+this.dat.Length];
			(new mwg.File.mbUInt32((uint)this.dat.Length)).ToBinary().CopyTo(r,0);
			this.dat.CopyTo(r,4);
			return r;
		}
		//operator : �d�����镨������̂ŁA���Ȃ��Ȃ��Ă���B
		//(���߂ɁA�����̏�񂪓��邩����Ȃ��������ƂȂ邪�A�����ł͓���鎖�ɂ���)
		public static explicit operator mbBytes(byte[] a){return new mbBytes(a,true);}
		public static explicit operator mbBytes(mwg.File.mwgBinary a){return new mbBytes(a);}
	}
	#endregion

	#region mbDateTime
	public class mbDateTime:mb{
		long dat;
		//=====================================
		//          Constructors
		//-------------------------------------
		public mbDateTime(mwg.File.mwgBinary mbin){
			dat=0;
			if(mbin.RestLength<5)return;
			for(int i=0;i<5;i++)dat=dat*256+(int)mbin.readBytes(1)[0];
		}
		public mbDateTime(byte[] data){
			dat=0;
			if(data.Length<5){dat=0;return;}
			dat=data[0];
			dat=dat*256+(long)data[1];
			dat=dat*256+(long)data[2];
			dat=dat*256+(long)data[3];
			dat=dat*256+(long)data[4];
		}
		public mbDateTime(System.DateTime time){
			dat=(time.Year>3420)?3420:time.Year;
			dat=dat*12+time.Month-1;//Month �� 1-12 ������ 0-11 �ɕϊ�
			dat=dat*31+time.Day-1;//0-30�ɕϊ�
			dat=dat*24+time.Hour;
			dat=dat*60+time.Minute;
			dat=dat*60+time.Second;
			dat=dat*10+(int)(time.Millisecond/100);
		}
		/// <summary>
		/// mfTime�R���X�g���N�^
		/// </summary>
		/// <param name="time">�N���������b�A�f�V�b���܂ޔz�񂩂�mfTime�𐶐����܂��B</param>
		public mbDateTime(int[] time){
			dat=(time[0]>3420)?3420:time[0];
			dat=dat*12+time[1]-1;//Month �� 1-12 ������ 0-11 �ɕϊ�
			dat=dat*31+time[2]-1;//0-30�ɕϊ�
			dat=dat*24+time[3];
			dat=dat*60+time[4];
			dat=dat*60+time[5];
			dat=dat*10+time[6];
		}
		//=====================================
		//          Methods
		//-------------------------------------
		public override byte[] ToBinary(){
			byte[] r=new byte[5];
			long b,c;
			c=this.dat;
			for(int i=4;i>0;i--){
				b=c;c=(long)(c/256);
				r[i]=(byte)(b-c*256);	
			}
			r[0]=(byte)c;
			return r;
		}
		//=====================================
		//          Properties
		//-------------------------------------
		public override int Length{get{return 5;}}
		public int Year{
			get{return ((int[])this)[0];}
			set{
				if(value>3420||value<0)return;
				int[] a=(int[])this;
				a[0]=value;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		public int Month{
			get{return ((int[])this)[1]+1;}
			set{
				if(value>12||value<1)return;
				int[] a=(int[])this;
				a[1]=value-1;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		public int Day{
			get{return ((int[])this)[2]+1;}
			set{
				if(value>31||value<1)return;
				int[] a=(int[])this;
				a[2]=value-1;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		public int Hour{
			get{return ((int[])this)[3];}
			set{
				if(value>23||value<0)return;
				int[] a=(int[])this;
				a[3]=value;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		public int Minute{
			get{return ((int[])this)[4];}
			set{
				if(value>59||value<0)return;
				int[] a=(int[])this;
				a[4]=value;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		public int Second{
			get{return ((int[])this)[5];}
			set{
				if(value>59||value<0)return;
				int[] a=(int[])this;
				a[5]=value;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		public int Desisecond{
			get{return ((int[])this)[6];}
			set{
				if(value>9||value<0)return;
				int[] a=(int[])this;
				a[6]=value;
				this.dat=(new mbDateTime(a)).dat;
			}
		}
		//=====================================
		//          Operators
		//-------------------------------------
		public static explicit operator int[](mbDateTime a){
			int[] r=new int[7];
			int[] m=new int[]{12,31,24,60,60,10};
			long b,c;
			c=a.dat;
			for(int i=6;i>0;i--){
				b=c;c=(long)(c/m[i]);
				r[i]=(byte)(b-c*m[i]);	
			}
			r[0]=(int)c;r[1]++;r[2]++;
			return r;
		}
		public static explicit operator System.DateTime(mbDateTime a){
			int[] r1=(int[])a;
			r1[6]*=100;//�f�V�b���~���b�ɕϊ�
			return new System.DateTime(r1[0],r1[1],r1[2],r1[3],r1[4],r1[5],r1[6]);
		}
		public static explicit operator mbDateTime(System.DateTime a){return new mbDateTime(a);}
		public static explicit operator mbDateTime(byte[] a){return new mbDateTime(a);}
		public static explicit operator mbDateTime(mwg.File.mwgBinary a){return new mbDateTime(a);}

		public static explicit operator long(mbDateTime a){return a.dat;}
	}
	#endregion

	/// <summary>
	/// �ȒP�ȍ������������邽�߂̃N���X(�������l���鎞�A�ʒu���Œ肳��Ă��镨�Ƃ��čl����B)
	/// </summary>
	public class mwqDiff{
		/// <summary>
		/// n�Ԗڂ̗v�f�́An�o�C�g�ڂ̒l�������f�[�^�̊Ԃŋ��ʂ������łȂ����������l���Ƃ�B
		/// �l�� 0-255 �̎� = ���� ;
		/// �l�� 256 �̎� = �قȂ� ;
		/// </summary>
		public short[] data;//�قȂ镔��
		public mwqDiff(int length){
			this.data=new short[length];
		}
		public mwqDiff(string[] filenames){
			mwgBinary[] rtn=new mwgBinary[filenames.Length];
			for(int i=0;i<filenames.Length;i++){
				rtn[i]=new mwgBinary(filenames[i]);
			}
			this.data=(new mwqDiff(rtn)).data;
		}
		/// <summary>
		/// �R���X�g���N�^
		/// �����̃o�C�g�ɉ����āA��ł��قȂ�o�C�i���f�[�^�����݂���΁A������قȂ镔���Ƃ��ēo�^����B
		/// </summary>
		/// <param name="x"></param>
		public mwqDiff(mwgBinary[] x){
			//�f�[�^�̒����̍ŏ��l len
			int i;
			int len=0;
			if(x.Length==0){
				len=x[0].binaryData.Length;
				this.data=new short[len];
				for(i=0;i<len;i++){
					this.data[i]=(short)x[0].binaryData[i];
				}
			}else if(x.Length>1){
				len=x[0].binaryData.Length;
				for(i=0;i<x.Length;i++){
					if(x[i].binaryData.Length<len)len=x[i].binaryData.Length;
				}
			}
			//���ۂɈقȂ镔���Ƀ}�[�N(1)��t���Ă���
			this.data=new short[len];
			for(i=0;i<len;i++){
				this.data[i]=(short)x[0].binaryData[i];
			}
			for(i=1;i<x.Length;i++){
				for(int j=0;j<len;j++){
					if(this.data[j]!=(int)x[i].binaryData[j])this.data[j]=256;
				}
			}
			//
		}
		public System.Drawing.Bitmap ToBitmap(){
			//�K���ȑ傫����Bitmap��p�ӂ���B
			int height=this.data.Length/256;
			if(this.data.Length%256!=0)height++;
			System.Drawing.Bitmap rtn=new System.Drawing.Bitmap(256,height,System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			//
			int x=0;int y=0;
			for(int i=0;i<this.data.Length;i++){
				rtn.SetPixel(x,y,(this.data[i]==256)?System.Drawing.Color.Black:System.Drawing.Color.White);
				x++;if(x>=256){x=0;y++;}
			}
			return rtn;
		}
		/// <summary>
		/// ���Ɂu���ʁv�������Ă���ꏊ�̓��A���ۂɃf�[�^���قȂ镔����Ԃ��܂��B����byte���͎����܂��B
		/// </summary>
		public static mwqDiff operator *(mwqDiff a,mwqDiff b){
			int len=System.Math.Min(a.data.Length,b.data.Length);
			mwqDiff rtn=new mwqDiff(len);
			for(int i=0;i<len;i++){
				rtn.data[i]=((int)a.data[i]!=256&&(int)b.data[i]!=256&&a.data[i]!=b.data[i])?(short)256:(short)0;
			}
			return rtn;
		}
		
	}

	public class mwqRiff{
		private byte[] data;
		private string file;
		
		
		private int i;
		private int imax;
		//-- constructor
		public mwqRiff(string filename){
			this.data=mwg.File.mwgBinary.WholeFileInBinary(filename);
			this.imax=this.data.Length;			
			this.file=filename;
		}

		#region "mwqRiff.ToXml() �֐��ƁA���̎���/�֘A�̊֐�"
		public string ToXml(){
			this.i=0;
			string fourcc="";
			string returnStr="<?xml version='1.0' encoding='utf-8'?>\n";
			// RIFF �`�����N�̊m�F 4B
			fourcc=new string(new char[]{(char)this.data[i],(char)this.data[i+1],(char)this.data[i+2],(char)this.data[i+3]});
			if(fourcc!="RIFF")return("<!--error-->");//error;
			this.i+=4;
			//�`�����N�̒������擾 4B
			uint len=256*(uint)this.data[i+3];
			len=256*(len+(uint)this.data[i+2]);
			len=256*(len+(uint)this.data[i+1]);
			len=len+(uint)this.data[i];
			this.i+=4;
			int iend=this.i+(int)(len);if(this.imax<iend)iend=this.imax;//���̃`�����N�̏I���̈ʒu
			//�`�����N�����擾 4B
			fourcc=new string(new char[]{(char)this.data[i],(char)this.data[i+1],(char)this.data[i+2],(char)this.data[i+3]});
			returnStr+="<riff name='"+fourcc+"' length='"+len.ToString()+"' path='"+this.file+"'>\n";//error;
			this.i+=4;
			//���X�g�܂��̓`�����N�̃f�[�^��ǂ�
			string str;
			while(this.i<iend){
				str=this.ToXmlList();
				if(str=="<!--error-->")return str;//�G���[���N�������ꍇ�̓G���[�̒ʒm�����̂܂ܕԂ�
				returnStr+=str;
			}
			//�`�����N���I�������....����return
			this.i=iend;
			return returnStr+"</riff>\n";
		}
		private string ToXmlList()/*���X�g�̓Ǎ����s��*/{
			string returnStr="";
			string fourcc="";
			// LIST �`�����N�̊m�F 4B
			fourcc=new string(new char[]{(char)this.data[i],(char)this.data[i+1],(char)this.data[i+2],(char)this.data[i+3]});
			this.i+=4;
			if(fourcc!="LIST")return this.ToXmlChunk(fourcc);//�`�����N�Ǎ��֐��Ɉ����n��
			//�`�����N�̒������擾 4B
			uint len=256*(uint)this.data[i+3];
			len=256*(len+(uint)this.data[i+2]);
			len=256*(len+(uint)this.data[i+1]);
			len=len+(uint)this.data[i];
			this.i+=4;
			int iend=this.i+(int)(len);if(this.imax<iend)iend=this.imax;//���̃`�����N�̏I����(����)�ʒu
			//�`�����N�����擾 4B
			fourcc=new string(new char[]{(char)this.data[i],(char)this.data[i+1],(char)this.data[i+2],(char)this.data[i+3]});
			returnStr="<list name='"+fourcc+"' length='"+len.ToString()+"'>\n";
			this.i+=4;
			//���X�g�܂��̓`�����N�̃f�[�^��ǂ�
			string str;
			while(this.i<iend){
				str=this.ToXmlList();
				if(str=="<!--error-->")return str;//�G���[���N�������ꍇ�̓G���[�̒ʒm�����̂܂܏�ɓ`����
				returnStr+=str;
			}
			//���X�g���I�������....����return
			this.i=iend;
			return returnStr+"</list>\n";
		}
		private string ToXmlChunk(string streamName)/*�`�����N�̓Ǎ�*/{
			//�`�����N�̒������擾
			int len=256*(int)this.data[i+3];
			len=256*(len+(int)this.data[i+2]);
			len=256*(len+(int)this.data[i+1]);
			len=len+(int)this.data[i];
			//�`�����N�̏I���̈ʒu(�̎�)�܂ňړ�
			this.i+=4+len;
			this.i+=this.i%2;//�ŋ߂� WORD���E�܂ňړ�
			//�����o��
			return "<chunk name='"+streamName+"' length='"+len.ToString()+"'/>\n";
		}
		#endregion


	}
	/*
	public class BinaryStream{
		//�� �v�����ꂽ�ʂ���BinaryData��Ԃ��A�ʒu��i�߂Ă����N���X��mwgBinary�ɓ���
	}
	*/
}