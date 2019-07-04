namespace mwg.Parse{
	/// <summary>
	/// �\����ׂ͂̈̊�{�N���X�B
	/// ���̃N���X�́A���ۂ̕��͂ɑ΂��ĕ�����K�p���A�\����͂����s����N���X�ł��B
	/// �����̏��̏W���Ȃǂ��`���܂��B
	/// �h����̃N���X�ł́A�o�͂ɂ������t�B�[���h�Ȃǂ��쐬���鎖�����߂��܂��B
	/// </summary>
	public class Document{
		/// <summary>
		/// ��͑Ώۂ̕��͂ł��B
		/// </summary>
		public string content;
		//***********************************************************
		//		����
		//			�o�^���Ă����Q�ƕ����͕�����Ŏw�肵�܂��B
		//			ERR: ���݂��Ȃ�������ݒ肵����
		//			ERR: �������O�̕�����ݒ肵�悤�Ƃ�����
		//
		//			field:	contexts
		//			field:	Context�h���^ CurrentContext //�p����Ŏ���
		//			method:	AddContext
		//			method:	RemoveContext
		//			method:	Context�h���^ GetContext //�p����Ŏ���
		//-----------------------------------------------------------
		protected System.Collections.Hashtable contexts=new System.Collections.Hashtable();
		/// <summary>
		/// ��Q�ƕ�����o�^���܂�
		/// </summary>
		/// <param name="key">�o�^�����w�肵�ĉ�����</param>
		/// <param name="c">�o�^���镶�����w�肵�ĉ�����</param>
		public void AddContext(string key,mwg.Parse.Context c){
			this.contexts.Add(key,c);
		}
		/// <summary>
		/// �o�^������Q�ƕ������폜���܂�
		/// </summary>
		/// <param name="key">�o�^�����w�肵�ĉ�����</param>
		public void RemoveContext(string key){
			this.contexts.Remove(key);
		}
		//***********************************************************
		//		<field>	��͈ʒu
		//-----------------------------------------------------------
		/// <summary>
		/// ���݂̉�͈ʒu
		/// </summary>
		protected int index;
		public string letter;
		public int Index{
			get{return this.index;}
			set{
				this.index=value;
				try{this.letter=this.content.Substring(value,1);}catch(System.ArgumentOutOfRangeException e){
					//TODO:�����Athis.index>=this.content.Length �Ȃ�A��͏I���̍��}���o���Ȃǂ̏��u���s���B	
					throw e;
				}
			}
		}
		public bool Next(){
			if(++this.index<this.content.Length){
				this.letter=this.content.Substring(this.index,1);
				return true;
			}else{
				this.letter="";
				return false;
			}
		}
		//***********************************************************
		//		<field> ���O
		//-----------------------------------------------------------
		public string log="";
		public void OutOfRange(){
			this.log+="Err@"+this.index.ToString()+": �\�����ʈʒu�ŕ������r�؂�Ă��܂��B\n";
		}
		public void Error(string txt){
			this.log+="Err@"+this.index.ToString()+": "+txt+"\n";
		}
		//***********************************************************
		//		<constructor>
		//-----------------------------------------------------------
		protected Document(string text){this.content=text;}
		//***********************************************************
		//		��{�̓ǂݏo���֐�
		//			SkipSpace
		//			ReadQuoted
		//			ReadUntil, ReadBefore
		//			ReadName
		//			ReadVsName
		//-----------------------------------------------------------
		#region
		public void SkipSpace(){
			while(this.Next()){
				if(" \t\n\r�@".IndexOf(this.letter)<0)return;
			}
		}
		/// <summary>
		/// " �� ' �ň͂܂ꂽ�������ǂݏo���܂��B
		/// </summary>
		/// <returns>�ǂݏo�������e��Ԃ��܂�</returns>
		public string ReadQuoted(){
			string end="";
			if(this.letter=="\"")end="\"";else if(this.letter=="'")end="'";else return "";
			return this.ReadUntil(end);
		}
		/// <summary>
		/// �w�肵�������������܂œǂݏo�����s���܂��B
		/// escape ����(\)���L���ł��B
		/// </summary>
		/// <param name="end">�I������</param>
		/// <returns>�ǂݏo�������e��Ԃ��܂��Bescape �����͊܂݂܂���B</returns>
		public string ReadUntil(string end){
			string r="";
			while(this.Next()){
				if(this.letter==end){
					this.Next();
					return r;
				}else if(this.letter=="\\"){
					if(this.Next())r+=this.letter;else this.OutOfRange();
				}else{
					r+=this.letter;
				}
			}
			this.OutOfRange();
			return r;
		}
		/// <summary>
		/// �w�肵�������̎�O�܂œǂݏo�����s���܂��B
		/// escape ����(\)���L���ł��B
		/// </summary>
		/// <param name="end">�I������</param>
		/// <returns>�ǂݏo�������e��Ԃ��܂��Bescape �����͊܂݂܂���B</returns>
		public string ReadBefore(string end){
			string r="";
			while(this.Next()){
				if(this.letter==end){
					return r;
				}else if(this.letter=="\\"){
					if(this.Next())r+=this.letter;else this.OutOfRange();
				}else{
					r+=this.letter;
				}
			}
			this.OutOfRange();
			return r;
		}
		/// <summary>
		/// VC# �� VB �� VC++ �ɉ����鎯�ʎq��ǂݎ��܂��B
		/// </summary>
		/// <returns>�ǂݎ�������ʎq��Ԃ��܂��B</returns>
		public string ReadVsName(bool underScore,bool hyphen,bool atMark){
			string r=this.letter;
			while(this.Next()){
				char x=this.letter.ToCharArray(0,1)[0];
				//-Numbers
				if(r!=""&&('\xff10'<=x&&x<='\xff19'||'\x30'<=x&&x<='\x39')){
					r+=this.letter;
					continue;
				}
				//-UnderScore
				if(x=='\x5f'){
					if(!underScore)return r;
					r+=this.letter;
					continue;
				}
				if(hyphen&&x=='\x2d'||atMark&&x=='\x40'){
					r+=this.letter;
					continue;
				}
				//--���̑��̕���
				if(Document.IsLetterForVsName(x))r+=this.letter;else return r;
			}
			//OutOfRange �ł͂Ȃ�//this.OutOfRange();
			return r;
		}
		public static bool IsLetterForVsName(char x){
			//--�A���t�@�x�b�g�Ȃ�
			if(x<='\x5a'&&'\x41'<=x||x<='\x7a'&&'\x61'<=x||x=='\x5f'||
				x<='\x233'&&'\xc0'<=x&&x!='\xd7'&&x!='\xf7'||x<='\x2ad'&&'\x250'<=x){
				return true;
			}
			int x0=(int)x>>8;
			//--�L��/�}�[�N
			if(0x22<=x0&&x0<=0x2f||0xd8<=x0&&x0<=0xf8)return false;
			//--CJK����/�n���O��
			if(0x34<=x0&&x0<=0xa3||0xac<=x0&&x0<=0xd6){
				return true;
			}
			//--���̑�
			switch(x0){
				case 0x03:
					return '\x386'<=x&&x<='\x3f3'&&
						x!='\x3cf'&&x!='\x3d7'&&x!='\x3d8'&&x!='\x3d9'&&
						x!='\x387'&&x!='\x38b'&&x!='\x38d';
				//TODO:case 0x04-0x1d:
				case 0x1e:
					return x<='\x1e9b'||'\x1ea0'<=x&&x<='\x1ef9';
				case 0x20:return false;
				case 0x21:
					return 0x0a<=x&&x<=0x13||0x19<=x&&x<=0x1d||0x2a<=x&&x<=0x2d||
						0x2f<=x&&x<=0x31||0x33<=x&&x<=0x39||0x60<=x&&x<=0x83||
						x==0x02||x==0x07||x==0x15||x==0x24||x==0x26||x==0x28;
				case 0x30:
					return 0x21<=x&&x<=0x29||0x31<=x&&x<=0x35||0x41<=x&&x<=0x94||
						0xa1<=x&&x<=0xfa||0xfc<=x&&x<=0xfe;
				case 0x31:
					return '\x05'<=x&&x<='\x2c'||'\x31'<=x&&x<='\x8e'||'\xa0'<=x&&x<='\xb7';
				case 0x32:case 0x33:return false;
				case 0xa4:return x<='\xa48c';
				case 0xd7:return x<='\xd7a3';
				case 0xf9:return true;
				case 0xfa:return x<='\xfa2d';
					//TODO:case 0xfb-0xfe:
					//fb 0-6
				case 0xff:
					return 0x21<=x&&x<=0x3a||x==0x3f||0x41<=x&&x<=0x5a||0x66<=x&&x<=0xbe||
						0xc2<=x&&x<=0xdc&&x!=0xc8&&x!=0xc9&&x!=0xd0&&x!=0xd1&&x!=0xd8&&x!=0xd9;
				default:return false;
			}
		}
		public string ReadVsName(){
			return this.ReadVsName(true,false,false);
		}
		/// <summary>
		/// ���ʎq�̓ǂݏo�����s���܂�
		/// </summary>
		/// <param name="underScore">���ʎq�� "_" ���܂߂Ă��ǂ����w�肵�܂�</param>
		/// <param name="hyphen">���ʎq�� "-" ���܂߂Ă��ǂ����w�肵�܂�</param>
		/// <param name="atMark">���ʎq�� "@" ���܂߂Ă��ǂ����w�肵�܂�</param>
		/// <returns>�ǂݏo�������ʎq��Ԃ��܂��B</returns>
		public string ReadName(bool underScore,bool hyphen,bool atMark){
			if(" !\"#$%&'()=~|1234567890^\\[:]`{*},./<>?�@\t\r\n".IndexOf(this.letter)>=0)return "";
			if(!underScore&&this.letter=="_"||!hyphen&&this.letter=="-"||!atMark&&this.letter=="@")return "";
			string r=this.letter;
			while(this.Next()){
				if(" !\"#$%&'()=~|^\\[:]`{*},./<>?�@\t\r\n".IndexOf(this.letter)>=0)return r;
				if(!underScore&&this.letter=="_"||!hyphen&&this.letter=="-"||!atMark&&this.letter=="@")return r;
				r+=this.letter;
			}
			return r;
		}
		#endregion
	}
	/// <summary>
	/// �\����ׂ͂̈̊�{�����N���X�B
	/// </summary>
	public abstract class Context:System.IDisposable{
		//***********************************************************
		//		<Field>		isInstance
		//-----------------------------------------------------------
		/// <summary>
		/// true: �ǂݎ�蕶��(���ۂɓǂݎ����s�������I�u�W�F�N�g)�ł��邱�Ƃ������܂��B
		/// false: ��Q�ƕ���(�ǂݎ�蕶������Q�Ƃ���镶��)�ł��邱�Ƃ������܂��B
		/// </summary>
		protected bool isInstance=false;
		/// <summary>
		/// �ǂݎ�蕶���ŗL���ł��B
		/// End of context, �����I����\���܂��B
		/// ����� true �ɐݒ肷�鎖�ɂ��A�����𔲂����Ƃ������� Document �ɒʒm���܂��B
		/// </summary>
		public bool EOC=false;
		//***********************************************************
		//		�p�����镶��
		//-----------------------------------------------------------
		/// <summary>
		/// �p�����̕�����ێ����܂��B
		/// </summary>
		protected System.Collections.ArrayList implements;
		/// <summary>
		/// �p�����镶����o�^���܂��B
		/// ��ɓo�^���������D��x�������Ȃ�܂��B
		/// (�������O�� Handler ���������ꍇ�A�㏑������܂��B)
		/// </summary>
		/// <param name="c">�o�^���镶��</param>
		public void AddImplement(Context c){
			if(this.GetType()!=c.GetType())throw new System.Exception("�����^�� Context ��o�^���ĉ�����");
			this.implements.Add(c);
		}
		//***********************************************************
		//		<Constructor>
		//-----------------------------------------------------------
		bool disposed=false;
		/// <summary>
		/// �g��Ȃ��Ȃ��� instance �̎n�������܂��B
		/// </summary>
		public void Dispose(){
			if(!this.disposed){
				this.implements.Clear();
				this.hLttr.Clear();
				this.disposed=true;
			}
		}
		//***********************************************************
		//		���������֐��̊Ǘ�
		//-----------------------------------------------------------
		protected System.Collections.Hashtable hLttr;
		/// <summary>
		/// ���������֐��� delegate �� System.Type ���擾���܂��B
		/// </summary>
		protected abstract System.Type GetLHType();
		/// <summary>
		/// ��������������֐���o�^���܂��B
		/// </summary>
		/// <param name="letter">
		/// ��������Ώۂ̕���
		/// "*default" ���w�肷��ƁA�o�^����Ă��Ȃ������ɑ΂��鏈����ݒ�o���܂��B
		/// ���ɓo�^����Ă��镨�ɑ΂��ẮA�㏑�����s���܂��B
		/// </param>
		/// <param name="lh">
		/// �o�^���悤�Ƃ��Ă���֐��BGetLHType() �Ŏw�肳��Ă���^�� delegate ���w�肵�ĉ������B
		/// �����ŕ��������ɐi�߂邩�����I���v�����o����K�v������܂��B
		/// <code>
		/// void HandleA(mwg.Parse.Document doc){
		///		doc.Next();//���������ɐi�߂�
		/// }
		///	void HandleRightBracket(mwg.Parse.Document doc){
		///		doc.CurrentContext.EOC=true;//�����I���v�����o���B
		///	}
		///	</code>
		/// </param>
		public void AddLetterHandler(string letter,System.Delegate lh){
			if(lh.GetType()!=this.GetLHType())throw new System.Exception("delegate �̎�ނ��قȂ�דo�^�o���܂���");
			if(this.hLttr.Contains(letter)){
				this.hLttr[letter]=lh;
			}else{
				this.hLttr.Add(letter,lh);
			}
		}
		/// <summary>
		/// �o�^�������������֐����폜���܂��B
		/// </summary>
		/// <param name="letter">
		/// �ǂ̕����Ɋ��蓖�Ă��Ă���֐����폜���邩�w�肵�܂��B
		/// "*default" �͎w�肵�Ă��폜����܂���B
		/// "*default" �̓����ύX�������ꍇ�� AddLetterHandler �ŏ㏑�����ĉ������B
		/// </param>
		public void RemoveLetterHandler(string letter){
			if(letter!="*default"&&this.hLttr.Contains(letter))this.hLttr.Remove(letter);
		}
		/// <summary>
		/// �v�����ꂽ�����ɑΉ�����K�؂ȕ��������֐���Ԃ��܂��B
		/// �����ɒ��ڑΉ�����֐����Ȃ��ꍇ�A�ȉ��̏��ԂŊ֐���T���A�����莟�悻���Ԃ��܂��B
		/// <list type="number">
		/// <item><description>�������A���t�@�x�b�g�Ȃ� *alpha</description></item>
		/// <item><description>�啶���A���t�@�x�b�g�Ȃ� *Alpha</description></item>
		/// <item><description>�A���t�@�x�b�g�Ȃ� *ALPHA</description></item>
		/// <item><description>���ʎq�p�����Ȃ� *letter</description></item>
		/// <item><description>�����Ȃ� *number</description></item>
		/// <item><description>*default</description></item>
		/// </list>
		/// </summary>
		protected System.Delegate GetLetterHandler(string letter){
			System.Delegate dl=this.GetLetterHandlerEx(letter);
			if(dl!=null)return dl;
			char x=letter.ToCharArray(0,1)[0];
			if(char.IsLetter(x)){
				dl=this.GetLetterHandler(char.IsLower(letter,0)?"*alpha":"*ALPHA");
				if(dl!=null)return dl;
				dl=this.GetLetterHandlerEx("*Alpha");
				if(dl!=null)return dl;
			}
			if(Document.IsLetterForVsName(x)){
				dl=this.GetLetterHandlerEx("*letter");
			}else if(char.IsDigit(x)){
				dl=this.GetLetterHandlerEx("*number");
			}//���A�X�y�[�X" ", "�@" ��^�u�E���s"\t\n\r" ��
			dl=this.GetLetterHandlerEx("*default");
			if(dl!=null)return dl;
			throw new System.Exception("*default ���o�^����Ă��܂���");
		}
		/// <summary>
		/// �v�����ꂽ���O�������������֐���Ԃ��܂��B
		/// ������Ȃ��ꍇ�� null ��Ԃ��܂��B
		/// </summary>
		protected System.Delegate GetLetterHandlerEx(string key){
			//--�����̏�����Ăяo��
			System.Delegate dlg=this.hLttr[key] as System.Delegate;
			if(dlg!=null){
				LetterHandler2Txt[] ary=(LetterHandler2Txt[])dlg.GetInvocationList();
				return ary[ary.Length-1];
			}
			//--�p��������̌Ăяo��
			foreach(Context2Txt c in this.implements){
				dlg=c.GetLetterHandlerEx(key);
				if(dlg!=null)return dlg;
			}
			return null;
		}
	}
}
