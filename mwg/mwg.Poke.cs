namespace mwg.Poke{
	public class saveData{
		public byte[] data;
		public saveData(string filename){
			if(!System.IO.File.Exists(filename)){
				System.Windows.Forms.MessageBox.Show("�w�肵���t�@�C���͑��݂��܂���\n");
				return;
			}
			//Access �m��
			string pokeSavWfn=filename;
			System.IO.FileStream rbf=new System.IO.FileStream(filename,System.IO.FileMode.Open,System.IO.FileAccess.Read);
			System.IO.BinaryReader rbr=new System.IO.BinaryReader(rbf);
			//--�t�@�C���T�C�Y�擾
			int imax=(int)(new System.IO.FileInfo(filename).Length);//�t�@�C���T�C�Y�擾
			//System.Windows.Forms.MessageBox.Show("�t�@�C���T�C�Y���擾���܂��� - "+imax.ToString()+"\n");
			if(imax!=32768){
				//�t�@�C���T�C�Y�ɂ���Č�����t�@�C����r��
				System.Windows.Forms.MessageBox.Show("���K�̃t�@�C���ł͂Ȃ��悤�ł��B���m�F�������B�������I�����܂��B\n");
				System.Windows.Forms.MessageBox.Show("��������\n");
				rbr.Close();
				rbf.Close();
			}
			//--�t�@�C���Ǎ�
			this.data=rbr.ReadBytes(imax);
		}
	}

	public class Monster{
		private byte[] data;//����44�̃f�[�^
		private byte[] name;
		private byte[] pare;
		public Monster(byte[] data,byte[] name,byte[] pare){
			this.data=data;
			this.name=name;
			this.pare=pare;
		}
		public Monster(byte[] data,int DataIndex,int NameIndex,int PareIndex){
			initialize(data,DataIndex,NameIndex,PareIndex);

		}
		public Monster(mwg.Poke.saveData data,int index){
			int iData=DataBase+index*44;
			int iPare=PareBase+index*6;
			int iName=NameBase+index*6;
			initialize(data.data,iData,iName,iPare);
		}
		private void initialize(byte[] data,int DataIndex,int NameIndex,int PareIndex){
			this.data=new byte[44];
			this.name=new byte[6];
			this.pare=new byte[6];
			if(data.Length<DataIndex+44)return;
			for(int i=0;i<44;i++){
				this.data[i]=data[i+DataIndex];
			}
			if(data.Length<NameIndex+6)return;
			if(data.Length<PareIndex+6)return;
			for(int i=0;i<6;i++){
				this.name[i]=data[i+NameIndex];
				this.pare[i]=data[i+PareIndex];
			}
		}
		private readonly int DataBase=11997;
		private readonly int PareBase=12261;
		private readonly int NameBase=12297;
		//=====================================
		//          properties
		//-------------------------------------
		[System.ComponentModel.Category("�|�P����"),System.ComponentModel.Description("�|�P�����̊Ǘ��ԍ�")]
		public int Number{
			get{return this.data[0];}
			set{if(0<=value&&value<256)this.data[0]=(byte)value; else this.data[0]=(byte)0;}
		}
		[System.ComponentModel.Category("�|�P����"),System.ComponentModel.Description("�|�P�����̎��")]
		public pokeKind Kind{
			get{return (pokeKind)this.data[0];}
			set{this.data[0]=(byte)value;}
		}
		[System.ComponentModel.Category("���݂̏��"),System.ComponentModel.Description("�c��̗̑�")]
		public int HP{
			get{return (data[1]<<8)+data[2];}
			set{
				if(typeof(int)!=value.GetType())return;
				this.data[1]=(byte)(value/256);this.data[2]=(byte)(value%256);
			}
		}
		[System.ComponentModel.Category("�|�P����"),System.ComponentModel.Description("�^�C�v�P")]
		public pokeType Type1{
			get{return (pokeType)this.data[5];}
			set{this.data[5]=(byte)value;}
		}
		[System.ComponentModel.Category("�|�P����"),System.ComponentModel.Description("�^�C�v�Q")]
		public pokeType Type2{
			get{return (pokeType)this.data[6];}
			set{this.data[5]=(byte)value;}
		}
		[System.ComponentModel.Category("�Z"),System.ComponentModel.Description("�킴�P")]
		public pokeWaza Waza1{
			get{return (pokeWaza)this.data[8];}
			set{this.data[8]=(byte)value;}
		}
		[System.ComponentModel.Category("�Z"),System.ComponentModel.Description("�킴�Q")]
		public pokeWaza Waza2{
			get{return (pokeWaza)this.data[9];}
			set{this.data[9]=(byte)value;}
		}
		[System.ComponentModel.Category("�Z"),System.ComponentModel.Description("�킴�R")]
		public pokeWaza Waza3{
			get{return (pokeWaza)this.data[10];}
			set{this.data[10]=(byte)value;}
		}
		[System.ComponentModel.Category("�Z"),System.ComponentModel.Description("�킴�S")]
		public pokeWaza Waza4{
			get{return (pokeWaza)this.data[11];}
			set{this.data[11]=(byte)value;}
		}
		[System.ComponentModel.Category("ID"),System.ComponentModel.Description("ID")]
		public int ID{
			get{return (data[12]<<8)+data[13];}
			set{
				if(typeof(int)!=value.GetType())return;
				this.data[12]=(byte)(value/256);this.data[3]=(byte)(value%256);
			}
		}
		[System.ComponentModel.Category("�o���l"),System.ComponentModel.Description("�|�P�����̌o���l�̑傫���������Ă��܂�")]
		public int Experience{
			get{return data[14]<<16+data[15]<<8+data[16];}
			set{
				if(typeof(int)!=value.GetType())return;
				this.data[14]=(byte)(value/65536);
				this.data[15]=(byte)((value/256)%256);
				this.data[16]=(byte)(value%256);
			}
		}
		//=====================================
		//          enum
		//-------------------------------------
		public enum pokeType{
			�m�[�}��=0,
			�����Ƃ�=1,
			�Ђ���=2,
			�ǂ�=3,
			���߂�=4,
			����=5,
			�ނ�=7,
			�S�[�X�g=8,
			�͂���=9,
			�ق̂�=20,
			�݂�=21,
			����=22,
			�ł�=23,
			�G�X�p�[=24,
			������=25,
			�h���S��=26,
			����=27
		}
		public enum pokeKind{
			�s��=0, �T�C�h��=1, �K���[��=2, �j�h�����Y=3, �s�b�s=4, �I�j�X�Y��=5, �r�����_�}=6, �j�h�L���O=7,
			���h����=8, �t�V�M�\�E=9, �i�b�V�[=10, �x�������K=11, �^�}�^�}=12, �x�g�x�^�[=13, �Q���K�[=14, �j�h������=15,
			�j�h�N�C��=16, �J���J��=17, �T�C�z�[��=18, ���v���X=19, �E�C���f�B�[=20, �~���E=21, �M�����h�X=22, �V�F���_�[=23,
			���m�N���Q=24, �S�[�X=25, �X�g���C�N=26, �q�g�f�}��=27, �J���b�N�X=28, �J�C���X=29, �����W����=30, �n�b�T��=31,
			�c�{�c�{=32, �K�[�f�B=33, �C���[�N=34, �I�j�h����=35, �|�b�|=36, ���h��=37, �����Q���[=38, �S���[��=39,
			���b�L�[=40, �S�[���L�[=41, �o�����[�h=42, �T�������[=43, �G�r�����[=44, �A�[�{�b�N=45, �p���Z�N�g=46, �R�_�b�N=47,
			�X���[�v=48, �S���[�j��=49, �w���N���X=50, �u�[�o�[=51, �z�E�I�E=52, �G���u�[=53, ���A�R�C��=54, �h�K�[�X=55,
			�j���[��=56, �}���L�[=57, �p�E���E=58, �f�B�O�_=59, �P���^���X=60, �q���O�}=61, �����O�}=62, �}�O�}�b�O=63,
			�J���l�M=64, �R���p��=65, �J�C�����[=66, �}�O�J���S=67, �E�����[=68, �C�m���[=69, �h�[�h�[=70, �j������=71,
			���[�W����=72, �t�@�C���[=73, �t���[�U=74, �T���_�[=75, ���^����=76, �j���[�X=77, �N���u=78, �T�j�[�S=79,
			�e�b�|�E�I=80, �I�N�^��=81, ���R��=82, �L���E�R��=83, �s�J�`���E=84, ���C�`���E=85, �f���o�[�h=86, �}���^�C��=87,
			�~�j�����E=88, �n�N�����E=89, �J�u�g=90, �J�u�g�v�X=91, �^�b�c�[=92, �V�[�h��=93, �G�A�[���h=94, �f���r��=95,
			�T���h=96, �T���h�p��=97, �I���i�C�g=98, �I���X�^�[=99, �v����=100, �v�N����=101, �C�[�u�C=102, �u�[�X�^�[=103,
			�T���_�[�X=104, �V�����[�Y=105, �������L�[=106, �Y�o�b�g=107, �A�[�{=108, �p���X=109, �j�����]=110, �j�����{��=111,
			�r�[�h��=112, �R�N�[��=113, �X�s�A�[=114, �w���K�[=115, �h�[�h���I=116, �I�R���U��=117, �_�O�h���I=118, �����t�H��=119,
			�W���S��=120, �L���O�h��=121, �S�}�]�E=122, �L���^�s�[=123, �g�����Z��=124, �o�^�t���[=125, �J�C���L�[=126, �h���t�@��=127,
			�S���_�b�N=128, �X���[�p�[=129, �S���o�b�g=130, �~���E�c�[=131, �J�r�S��=132, �R�C�L���O=133, �|���S��=134, �I�h�V�V=135,
			�x�g�x�g��=136, �h�[�u��=137, �L���O���[=138, �p���V�F��=139, �o���L�[=140, �}���}�C��=141, �s�N�V�[=142, �}�^�h�K�X=143,
			�y���V�A��=144, �K���K��=145, �J�|�G���[=146, �S�[�X�g=147, �P�[�V�B=148, �t�[�f�B��=149, �s�W����=150, �s�W���b�g=151,
			�X�^�[�~�[=152, �t�V�M�_�l=153, �t�V�M�o�i=154, ���m�N���Q�d��=155, ���`���[��=156, �g�T�L���g=157, �A�Y�}�I�E=158, �G���L�b�h=159,
			�u�r�B=160, �~���^���N=161, �n�s�i�X=162, �|�j�[�^=163, �M�����b�v=164, �R���b�^=165, ���b�^=166, �j�h���[�m=167,
			�j�h���[�i=168, �C�V�c�u�e=169, �|���S���d��=170, �v�e��=171, ���C�R�E=172, �R�C��=173, �G���e�C=174, �X�C�N��=175,
			�q�g�J�Q=176, �[�j�K��=177, ���U�[�h=178, �J���[��=179, ���U�[�h��=180, ���[�M���X=181, �T�i�M���X=182, �o���M���X=183,
			���M�A=184, �i�]�m�N�T=185, �N�T�C�n�i=186, ���t���V�A=187, �}�_�c�{�~=188, �E�c�h��=189, �E�c�{�b�g=190, �`�R���[�^=191,
			�x�C���[�t=192, ���K�j�E��=193, �q�m�A���V=194, �}�O�}���V=195, �o�N�t�[��=196, ���j�m�R=197, �A���Q�C�c=198, �I�[�_�C��=199,
			�I�^�`=200, �I�I�^�`=201, �z�[�z�[=202, �����m�Y�N=203, ���f�B�o=204, ���f�B�A��=205, �C�g�}��=206, �A���A�h�X=207, �N���o�b�g=208,
			�`�����`�[=209, �����^�[��=210, �s�`���[=211, �s�B=212, �v�v����=213, �g�Q�s�[=214, �g�Q�`�b�N=215,
			�l�C�e�B=216, �l�C�e�B�I=217, �����[�v=218, ���R�R=219, �f�������E=220, �L���C�n�i=221, �}����=222, �}������=223,
			�E�\�b�L�[=224, �j�����g�m=225, �n�l�b�R=226, �|�|�b�R=227, ���^�b�R=228, �G�C�p��=229, �q�}�i�b�c=230, �L�}����=231,
			���������}=232, �E�p�[=233, �k�I�[=234, �G�[�t�B=235, �u���b�L�[=236, ���~�J���X=237, ���h�L���O=238, ���E�}=239,
			�A���m�[��=240, �\�[�i���X=241, �L�������L=242, �N�k�M�_�}=243, �t�H�g���X=244, �m�R�b�`=245, �O���C�K�[=246, �n�K�l�[��=247,
			�u���[=248, �O�����u��=249, �n���[�Z��=250, �s��1=251, �s��2=252, �s���z�E�I�E=253, �s��3=254, �s��4 =255
		}
		public enum pokeWaza{
			w����=0, w�͂���=1, w����ă`���b�v=2, w�����ӂ��r���^=3, w��񂼂��p���`=4, w���K�g���p���`=5, w�l�R�ɂ��΂�=6, w�ق̂��̃p���`=7, 
			w�ꂢ�Ƃ��p���`=8, w���݂Ȃ�p���`=9, w�Ђ�����=10, w�͂���=11, w�n�T�~�M���`��=12, w���܂�����=13, w�邬�̂܂�=14, w����������=15, 
			w����������=16, w�΂��ł���=17, w�ӂ��Ƃ΂�=18, w������Ƃ�=19, w���߂���=20, w����������=21, w��̃��`=22, w�ӂ݂�=23, 
			w�ɂǂ���=24, w���K�g���L�b�N=25, w�Ƃт���=26, w�܂킵����=27, w���Ȃ���=28, w����=29, w�̂ł�=30, w�݂���Â�=31, 
			w�̃h�胋=32, w����������=33, w�̂�������=34, w�܂���=35, w�Ƃ�����=36, w���΂��=37, w���Ă݃^�b�N��=38, w�����ۂ��ӂ�=39, 
			w�ǂ��΂�=40, w�_�u���j�[�h��=41, w�~�T�C���΂�=42, w�ɂ�݂���=43, w���݂�=44, w�Ȃ�����=45, w�ق���=46, w������=47, 
			w���傤�����=48, w�\�j�b�N�u�[��=49, w���Ȃ��΂�=50, w�悤��������=51, w�Ђ̂�=52, w������ق�����=53, w���낢����=54, w�݂��ł��ۂ�=55, 
			w�n�C�h���|���v=56, w�Ȃ݂̂�=57, w�ꂢ�Ƃ��r�[��=58, w�ӂԂ�=59, w�T�C�P��������=60, w�o�u����������=61, w�I�[�����r�[��=62, w�͂�����������=63, 
			w��=64, w�h�胋�����΂�=65, w����������� =66,w��������=67, w�J�E���^�[=68, w�����イ�Ȃ�=69, w�����肫=70, w�����Ƃ�=71, 
			w���K�h���C��=72, w��ǂ肬�̃^�l=73, w�������傤=74, w�͂��σJ�b�^�[=75, w�\�[���[�r�[��=76, w�ǂ��̂���=77, w���тꂲ��=78, w�˂ނ育��=79, 
			w�͂Ȃт�̂܂�=80, w���Ƃ��͂�=81, w��イ�̂�����=82, w�ق̂��̂���=83, w�ł񂫃V���b�N=84, w�P�O�܂�{���g=85, w�ł񂶂�=86, w���݂Ȃ�=87, 
			w���킨�Ƃ�=88, w������=89, w�����=90, w���Ȃ��ق�=91, w�ǂ��ǂ�=92, w�˂�肫=93, w�T�C�R�L�l�V�X=94, w�����݂񂶂��=95, 
			w���K�̃|�[�Y=96, w�����������ǂ�=97, w�ł񂱂�������=98, w������=99, w�e���|�[�g=100, w�i�C�g�փb�h=101, w���̂܂�=102, w����Ȃ���=103, 
			w�����Ԃ񂵂�=104, w������������=105, w�������Ȃ�=106, w���������Ȃ�=107, w����܂�=108, w���₵���Ђ���=109, w����ɂ�����=110, w�܂邭�Ȃ�=111, 
			w�o��A�[=112, w�Ђ���̂���=113, w���낢����=114, w��t���N�^�[=115, w����������=116, w���܂�=117, w��т��ӂ�=118, w�I�E��������=119, 
			w���΂�=120, w�^�}�S�΂�����=121, w�����łȂ߂�=122, w�X���b�O=123, w�փh����������=124, w�z�l����ڂ�=125, w��������=126, w�����̂ڂ�=127, 
			w����ł͂���=128, w�X�s�[�h�X�^�[=129, w���P�b�g����=130, w�Ƃ��L���m��=131, w����݂�=132, w�h�킷��=133, w�X�v�[���܂�=134, w�^�}�S����=135, 
			w�ƂтЂ�����=136, w�ւтɂ��=137, w��߂���=138, w�ǂ��K�X=139, w���܂Ȃ�=140, w���イ����=141, w�����܂̃L�b�X=142, w�S�b�h�o�[�h=143, 
			w�ւ񂵂�=144, w����=145, w�s���s���p���`=146, w�L�m�R�̂ق���=147, w�t���b�V��=148, w�T�C�R�E�F�[�u=149, w�͂˂�=150, w�Ƃ���=151, 
			w�N���u�n���}�[=152, w�����΂��͂�=153, w�݂���Ђ�����=154, w�z�l�u�[������=155, w�˂ނ�=156, w����Ȃ���=157, w�Ђ����܂���=158, w�����΂�=159, 
			w�e�N�X�`���[=160, w�g���C�A�^�b�N=161, w������̂܂���=162, w���肳��=163, w�݂����=164, w��邠����=165			
		}
	}

}

