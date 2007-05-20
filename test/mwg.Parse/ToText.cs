/* 構文解析の template
 * 
 *	1.他のファイルにコピー
 *	2.★★★のある所を編集
 *	3.mwg.Parse, Context, Document, LetterHandler2Txt, LetterHandlers
 *		を適当な物に置換[単語単位,大文字小文字区別]する。
 *	4.コンパイル
 */
namespace mwg.Parse{
	public class Document2Txt:Document{
		//***********************************************************
		//		文脈
		//			field:	CurrentContext
		//			method:	GetContext
		//-----------------------------------------------------------
		/// <summary>
		/// 現在使用している、読み取り文脈
		/// </summary>
		public mwg.Parse.Context2Txt CurrentContext;
		/// <summary>
		/// 登録済の被参照文脈を取得します
		/// </summary>
		/// <param name="key">登録名を指定して下さい</param>
		/// <returns>被参照文脈を返します。</returns>
		public mwg.Parse.Context2Txt GetContext(string key){
			return this.contexts[key] as mwg.Parse.Context2Txt;
		}
		//***********************************************************
		//		<constructor>
		//-----------------------------------------------------------
		public Document2Txt(string text):base(text){}
		//***********************************************************
		//		解析
		//-----------------------------------------------------------
		public virtual void Parse(Context2Txt context){
			this.Index=0;
			this.Read(new Context2Txt(context));
		}
		/// <summary>
		/// 指定した文脈で、文書を読みます。
		/// </summary>
		/// <param name="c">使用する読み取り文脈を指定して下さい(被参照文脈は設定しないで下さい)</param>
		public virtual Context2Txt Read(Context2Txt c){
			Context2Txt origContext=this.CurrentContext;
			this.CurrentContext=c;
			do{this.CurrentContext.HandleLetter(this);}while(!this.CurrentContext.EOC);
			this.CurrentContext=origContext;
			return c;
		}
		/// <summary>
		/// 指定した文脈で、文書を読みます。
		/// </summary>
		/// <param name="context">
		/// 使用する読み取り文脈の元となる、
		/// 被参照文脈を Document への登録名で指定して下さい
		/// </param>
		public virtual Context2Txt Read(string context){
			return this.Read(new Context2Txt(this.GetContext(context)));
		}
	}
	public class Context2Txt:Context{
		//-----------------------------------------------------------
		//	★★★ 出力結果に合った field を追加
		//-----------------------------------------------------------
		/// <summary>
		/// 文字列出力
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
		/// 被参照文脈から読み取り文脈を生成します。
		/// </summary>
		/// <param name="c"></param>
		public Context2Txt(Context2Txt c):this(){
			this.isInstance=true;
			this.AddImplement(c);
		}
		//***********************************************************
		//		文字処理関数
		//-----------------------------------------------------------
		protected override System.Type GetLHType(){
			return typeof(Parse.LetterHandler2Txt);
		}
		//***********************************************************
		//		文字処理の実行
		//-----------------------------------------------------------
		/// <summary>
		/// mwg.Parse.Document 文書の現在の文字を処理します。
		/// </summary>
		/// <param name="doc">処理の対象の mwg.Parse.Document instance</param>
		public void HandleLetter(mwg.Parse.Document2Txt doc){
			LetterHandler2Txt lh=this.GetLetterHandler(doc.letter) as LetterHandler2Txt;
			lh(doc);
		}
	}
	//***********************************************************
	//		文字処理関数
	//-----------------------------------------------------------
	/// <summary>
	/// 文字を処理する関数の delegate。
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
				//この context で文章が終わる可能性の無い時
				doc.OutOfRange();
			}
		}
	}

	/*public class INI{
		Document2Txt r;
		public INI(string text){
			r=new Document2Txt(text);
			//context base(コメントの処理など)
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
			string x=doc.ReadUntil("]");//これに関して処理
		}
		private void lh_MainLetter(mwg.Parse.Document2Txt doc){
			doc.Read("name");
		}
		private void lh_NameLetter(mwg.Parse.Document2Txt doc){
			string x=doc.ReadVsName();
			//TODO:モードに応じて、値を格納。既に格納されている時はパス(Error)
			doc.CurrentContext.EOC=true;
		}
		private void lh_NameQuoted(mwg.Parse.Document2Txt doc){
			string x=doc.ReadQuoted();
			//TODO:モードに応じて、値を格納。既に格納されている時はパス(Error)
			doc.CurrentContext.EOC=true;
		}
		private void lh_NameEqual(mwg.Parse.Document doc){
			//TODO:ここにモードを変える記述
			doc.Next();
		}
	}//*/

}