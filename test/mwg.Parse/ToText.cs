/* �\����͂� template
 * 
 *	1.���̃t�@�C���ɃR�s�[
 *	2.�������̂��鏊��ҏW
 *	3.mwg.Parse, Context, Document, LetterHandler2Txt, LetterHandlers
 *		��K���ȕ��ɒu��[�P��P��,�啶�����������]����B
 *	4.�R���p�C��
 */
namespace mwg.Parse{
	public class Document2Txt:Document{
		//***********************************************************
		//		����
		//			field:	CurrentContext
		//			method:	GetContext
		//-----------------------------------------------------------
		/// <summary>
		/// ���ݎg�p���Ă���A�ǂݎ�蕶��
		/// </summary>
		public mwg.Parse.Context2Txt CurrentContext;
		/// <summary>
		/// �o�^�ς̔�Q�ƕ������擾���܂�
		/// </summary>
		/// <param name="key">�o�^�����w�肵�ĉ�����</param>
		/// <returns>��Q�ƕ�����Ԃ��܂��B</returns>
		public mwg.Parse.Context2Txt GetContext(string key){
			return this.contexts[key] as mwg.Parse.Context2Txt;
		}
		//***********************************************************
		//		<constructor>
		//-----------------------------------------------------------
		public Document2Txt(string text):base(text){}
		//***********************************************************
		//		���
		//-----------------------------------------------------------
		public virtual void Parse(Context2Txt context){
			this.Index=0;
			this.Read(new Context2Txt(context));
		}
		/// <summary>
		/// �w�肵�������ŁA������ǂ݂܂��B
		/// </summary>
		/// <param name="c">�g�p����ǂݎ�蕶�����w�肵�ĉ�����(��Q�ƕ����͐ݒ肵�Ȃ��ŉ�����)</param>
		public virtual Context2Txt Read(Context2Txt c){
			Context2Txt origContext=this.CurrentContext;
			this.CurrentContext=c;
			do{this.CurrentContext.HandleLetter(this);}while(!this.CurrentContext.EOC);
			this.CurrentContext=origContext;
			return c;
		}
		/// <summary>
		/// �w�肵�������ŁA������ǂ݂܂��B
		/// </summary>
		/// <param name="context">
		/// �g�p����ǂݎ�蕶���̌��ƂȂ�A
		/// ��Q�ƕ����� Document �ւ̓o�^���Ŏw�肵�ĉ�����
		/// </param>
		public virtual Context2Txt Read(string context){
			return this.Read(new Context2Txt(this.GetContext(context)));
		}
	}
	public class Context2Txt:Context{
		//-----------------------------------------------------------
		//	������ �o�͌��ʂɍ����� field ��ǉ�
		//-----------------------------------------------------------
		/// <summary>
		/// ������o��
		/// </summary>
		public string output="";
		//***********************************************************
		//		<constructor>
		//-----------------------------------------------------------
		internal protected Context2Txt(){
			this.implements=new System.Collections.ArrayList();
			this.hLttr=new System.Collections.Hashtable();
		}
		/// <summary>
		/// ��Q�ƕ�������ǂݎ�蕶���𐶐����܂��B
		/// </summary>
		/// <param name="c"></param>
		public Context2Txt(Context2Txt c):this(){
			this.isInstance=true;
			this.AddImplement(c);
		}
		//***********************************************************
		//		���������֐�
		//-----------------------------------------------------------
		protected override System.Type GetLHType(){
			return typeof(Parse.LetterHandler2Txt);
		}
		//***********************************************************
		//		���������̎��s
		//-----------------------------------------------------------
		/// <summary>
		/// mwg.Parse.Document �����̌��݂̕������������܂��B
		/// </summary>
		/// <param name="doc">�����̑Ώۂ� mwg.Parse.Document instance</param>
		public void HandleLetter(mwg.Parse.Document2Txt doc){
			LetterHandler2Txt lh=this.GetLetterHandler(doc.letter) as LetterHandler2Txt;
			lh(doc);
		}
	}
	//***********************************************************
	//		���������֐�
	//-----------------------------------------------------------
	/// <summary>
	/// ��������������֐��� delegate�B
	/// </summary>
	public delegate void LetterHandler2Txt(mwg.Parse.Document2Txt doc);
	public class LHs2Txt{
		public static LetterHandler2Txt HasOverContext{
			get{return new LetterHandler2Txt(LHs2Txt.lh_HasOverContext);}
		}
		public static LetterHandler2Txt EndOfContext{
			get{return new LetterHandler2Txt(LHs2Txt.lh_EndOfContext);}
		}
		public static LetterHandler2Txt Ignore{
			get{return new LetterHandler2Txt(LHs2Txt.lh_Ignore);}
		}
		private static void lh_HasOverContext(mwg.Parse.Document2Txt doc){
			doc.OutOfRange();
			doc.CurrentContext.EOC=true;
		}		
		private static void lh_EndOfContext(mwg.Parse.Document2Txt doc){
			doc.Next();
			doc.CurrentContext.EOC=true;
		}
		private static void lh_Ignore(mwg.Parse.Document2Txt doc){
			if(!doc.Next()){
				doc.CurrentContext.EOC=true;
				//���� context �ŕ��͂��I���\���̖�����
				doc.OutOfRange();
			}
		}
	}

	/*public class INI{
		Document2Txt r;
		public INI(string text){
			r=new Document2Txt(text);
			//context base(�R�����g�̏����Ȃ�)
			Context2Txt b=new Context2Txt();
			c.AddHandler("#",this.AddHandler(this.lh_BaseComment));
			Context2Txt c;
			//context main
			c=new Context2Txt();
			c.AddImplement(b);
			c.AddLetterHandler("*default",LHs2Txt.Ignore);
			c.AddLetterHandler("[",new LetterHandler2Txt(this.lh_MainLeftBra));
			c.AddHandler("*Letter",new LetterHandler2Txt(this.lh_MainLetter));
			r.AddContext("main",c);
			//context name
			c=new Context2Txt();
			c.AddImplement(b);
			c.AddLetterHandler("*default",LHs2Txt.Ignore);
			c.AddHandler("*Letter",this.lh_NameLetter);
			c.AddHandler("\"",this.lh_NameQuoted);
			c.AddHandler("'",this.lh_NameQuoted);
			c.AddHandler("=",this.lh_NameEqual);
			r.AddContext("name",c);
		}
		private void lh_BaseComment(mwg.Parse.Document2Txt doc){
			doc.ReadUntil("\n");
			doc.CurrentContext.EOC=true;
		}
		private void lh_MainLeftBra(mwg.Parse.Document2Txt doc){
			string x=doc.ReadUntil("]");//����Ɋւ��ď���
		}
		private void lh_MainLetter(mwg.Parse.Document2Txt doc){
			doc.Read("name");
		}
		private void lh_NameLetter(mwg.Parse.Document2Txt doc){
			string x=doc.ReadVsName();
			//TODO:���[�h�ɉ����āA�l���i�[�B���Ɋi�[����Ă��鎞�̓p�X(Error)
			doc.CurrentContext.EOC=true;
		}
		private void lh_NameQuoted(mwg.Parse.Document2Txt doc){
			string x=doc.ReadQuoted();
			//TODO:���[�h�ɉ����āA�l���i�[�B���Ɋi�[����Ă��鎞�̓p�X(Error)
			doc.CurrentContext.EOC=true;
		}
		private void lh_NameEqual(mwg.Parse.Document doc){
			//TODO:�����Ƀ��[�h��ς���L�q
			doc.Next();
		}
	}//*/

}