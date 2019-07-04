
namespace mwg.Text{
#if �����������Ă��܂���
	/// <summary>
	/// class �錾��ǂݎ��N���X�ł��B
	/// </summary>
	/**
	���̌`���̃t�@�C���̓ǂݎ����s���܂�
	class object1//���̃N���X���̐���
	{
		private void MyFunction(int a,int b);//��������ׂ̊֐����̐���
		public int MyInt(string str1);//����
		public static explicit operator object1(int a1);//����
		public const int MyContant=1234;//����
		private int MyProperty{get;set;};//����
	}
	(����)
	���s�͋����܂���B
	��̒�`�͈�s�ɂ��ĉ�����
	*/
	public class objectDefinition{
		//static functions
		public static void ToHTML(string defFilename,string htmlFilename){
			System.IO.StreamReader sr=new System.IO.StreamReader(defFilename);
			string lineDat,lineDat2;
			string[] lineDat3;
			int i,j;
			string comment,type;
			
			while(null!=(lineDat=sr.ReadLine())){
				lineDat.Trim(new char[]{' ','�@','\t','\n'});
				if(lineDat.Length==0)continue;
				//�錾�{�̂ƃR�����g�̎擾
				comment="";
				i=lineDat.IndexOf("//");
				comment=lineDat.Substring(i+2);
				lineDat=lineDat.Substring(0,i);
				j=lineDat.IndexOf(";");
				if(j!=-1)lineDat=lineDat.Substring(0,j);
				
				// '=' ������ ==> �ϐ�
				if(lineDat.IndexOf("=")!=-1){
					//HandleVariant(/*�n��������䢂ɋL�q*/);
					//
					//ToDo:������HTML�ɏ����o������Ȃ艽�Ȃ��ǉ�
					//HandleVariant��comment���g�p
					continue;//���̐錾�̏��������ɉ�
				}
				
				// '(' ������ ==> �֐�
				// * '='�ŕϐ��ɉ񂵂Ă���ׁA'('���ϐ��̃R���X�g���N�^�̉\���͂Ȃ�
				if(lineDat.IndexOf("(")!=-1){
					HandleFunction(/*�n��������䢂ɋL�q*/);
					//
					//ToDo:
					//
					continue;//���̐錾�̏����͂���ŏI���B
				}
			}
			
			
		}
		private static string HandleFunction(string line){
			int a=line.IndexOf("(");
			int b=line.LastIndexOf(")");
			string declaration=line.Substring(0,a);
			string hikisuu=line.Substring(a+1,b);
			//getModifiers(/*�n��������䢂ɋL�q*/);
		}
		private static string HandleVariant(string line){
			return "";
			// TODO: �ϐ�����͂��Č��ʂ�Ԃ��悤�ɂ���B
		}
		//=====================================
		//          �C���q�̎擾�ׂ̈̊֐�
		//-------------------------------------
		private enum accessMod{Private,Public,Protected,Internal}
		private enum inheritMod{None,Sealed,Abstract,Virtual,Override}
		private struct modifier{
			public accessMod AccessMod;
			public inheritMod InheritMod;
			public bool Readonly;
			public bool Extern;
			//constructor
			public modifier(string str){
				// TODO: str �̓��e����͂���modifier �\���̂�����������悤�ɂ���B
				AccessMod=accessMod.Private;
				InheritMod=inheritMod.None;
				Readonly=false;
				Extern=false;
			}
			
		}
		private static modifier getModifiers(string line){
			//line �� identifier �܂Ŋ܂߂��`�Ŏ󂯎��B'private void int myFunction'�Ȃ�
			string[] line2=line.Split(new char[]{' ','�@','\t'});
			int i=line2.Length;
			modifier r=new modifier();
			if(i>0)for(int j=0;j<i;j++){
			    switch(line2[i].Trim()){
					case "public":r.AccessMod=accessMod.Public;break;
					case "private":r.AccessMod=accessMod.Private;break;
					case "protected":r.AccessMod=accessMod.Protected;break;
					case "internal":r.AccessMod=accessMod.Internal;break;
					case "sealed":r.InheritMod=inheritMod.Sealed;break;
					case "abstract":r.InheritMod=inheritMod.Abstract;break;
					case "virtual":r.InheritMod=inheritMod.Virtual;break;
					case "override":r.InheritMod=inheritMod.Override;break;
					case "extern":r.Extern=true;break;
					case "readonly":r.Readonly=true;break;
				}
			}
			return r;
		}
		//=======================================================
		//          �N���X�A�f���Q�[�g�ȂǍ\���̎�ނ��擾����
		//-------------------------------------------------------
		private enum structType{Default,Class,Struct,Interface,Enum,Delegate,Const,Event}
		private static structType getStructType(string line){
			string[] line2=line.Split(new char[]{' ','�@','\t'});
			int i=line2.Length;
			structType r=structType.Default;
			if(i>0)for(int j=0;j<i;j++)switch(line.Trim()){
				case "class":r=structType.Class;break;
				case "const":r=structType.Const;break;
				case "enum":r=structType.Enum;break;
				case "struct":r=structType.Struct;break;
				case "delegate":r=structType.Delegate;break;
				case "event":r=structType.Event;break;
				case "interface":r=structType.Interface;break;
			}
			return r;
		}
	}//endof class objectDefinition
	#endif
}//endof namespace