namespace mwg.Parse{
	/// <summary>
	/// 構文解析の為の基本クラス。
	/// このクラスは、実際の文章に対して文脈を適用し、構文解析を実行するクラスです。
	/// 文脈の情報の集合などを定義します。
	/// 派生先のクラスでは、出力にあったフィールドなどを作成する事が求められます。
	/// </summary>
	public class Document{
		/// <summary>
		/// 解析対象の文章です。
		/// </summary>
		public string content;
		//***********************************************************
		//		文脈
		//			登録している被参照文脈は文字列で指定します。
		//			ERR: 存在しない文脈を設定した時
		//			ERR: 同じ名前の文脈を設定しようとした時
		//
		//			field:	contexts
		//			field:	Context派生型 CurrentContext //継承先で実装
		//			method:	AddContext
		//			method:	RemoveContext
		//			method:	Context派生型 GetContext //継承先で実装
		//-----------------------------------------------------------
		protected System.Collections.Hashtable contexts=new System.Collections.Hashtable();
		/// <summary>
		/// 被参照文脈を登録します
		/// </summary>
		/// <param name="key">登録名を指定して下さい</param>
		/// <param name="c">登録する文脈を指定して下さい</param>
		public void AddContext(string key,mwg.Parse.Context c){
			this.contexts.Add(key,c);
		}
		/// <summary>
		/// 登録した被参照文脈を削除します
		/// </summary>
		/// <param name="key">登録名を指定して下さい</param>
		public void RemoveContext(string key){
			this.contexts.Remove(key);
		}
		//***********************************************************
		//		<field>	解析位置
		//-----------------------------------------------------------
		/// <summary>
		/// 現在の解析位置
		/// </summary>
		protected int index;
		public string letter;
		public int Index{
			get{return this.index;}
			set{
				this.index=value;
				try{this.letter=this.content.Substring(value,1);}catch(System.ArgumentOutOfRangeException e){
					//TODO:もし、this.index>=this.content.Length なら、解析終了の合図を出すなどの処置を行う。	
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
		//		<field> ログ
		//-----------------------------------------------------------
		public string log="";
		public void OutOfRange(){
			this.log+="Err@"+this.index.ToString()+": 予期せぬ位置で文書が途切れています。\n";
		}
		public void Error(string txt){
			this.log+="Err@"+this.index.ToString()+": "+txt+"\n";
		}
		//***********************************************************
		//		<constructor>
		//-----------------------------------------------------------
		protected Document(string text){this.content=text;}
		//***********************************************************
		//		基本の読み出し関数
		//			SkipSpace
		//			ReadQuoted
		//			ReadUntil, ReadBefore
		//			ReadName
		//			ReadVsName
		//-----------------------------------------------------------
		#region
		public void SkipSpace(){
			while(this.Next()){
				if(" \t\n\r　".IndexOf(this.letter)<0)return;
			}
		}
		/// <summary>
		/// " や ' で囲まれた文字列を読み出します。
		/// </summary>
		/// <returns>読み出した内容を返します</returns>
		public string ReadQuoted(){
			string end="";
			if(this.letter=="\"")end="\"";else if(this.letter=="'")end="'";else return "";
			return this.ReadUntil(end);
		}
		/// <summary>
		/// 指定した文字が現れるまで読み出しを行います。
		/// escape 文字(\)が有効です。
		/// </summary>
		/// <param name="end">終了文字</param>
		/// <returns>読み出した内容を返します。escape 文字は含みません。</returns>
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
		/// 指定した文字の手前まで読み出しを行います。
		/// escape 文字(\)が有効です。
		/// </summary>
		/// <param name="end">終了文字</param>
		/// <returns>読み出した内容を返します。escape 文字は含みません。</returns>
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
		/// VC# や VB や VC++ に於ける識別子を読み取ります。
		/// </summary>
		/// <returns>読み取った識別子を返します。</returns>
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
				//--その他の文字
				if(Document.IsLetterForVsName(x))r+=this.letter;else return r;
			}
			//OutOfRange ではない//this.OutOfRange();
			return r;
		}
		public static bool IsLetterForVsName(char x){
			//--アルファベットなど
			if(x<='\x5a'&&'\x41'<=x||x<='\x7a'&&'\x61'<=x||x=='\x5f'||
				x<='\x233'&&'\xc0'<=x&&x!='\xd7'&&x!='\xf7'||x<='\x2ad'&&'\x250'<=x){
				return true;
			}
			int x0=(int)x>>8;
			//--記号/マーク
			if(0x22<=x0&&x0<=0x2f||0xd8<=x0&&x0<=0xf8)return false;
			//--CJK漢字/ハングル
			if(0x34<=x0&&x0<=0xa3||0xac<=x0&&x0<=0xd6){
				return true;
			}
			//--その他
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
		/// 識別子の読み出しを行います
		/// </summary>
		/// <param name="underScore">識別子に "_" を含めても良いか指定します</param>
		/// <param name="hyphen">識別子に "-" を含めても良いか指定します</param>
		/// <param name="atMark">識別子に "@" を含めても良いか指定します</param>
		/// <returns>読み出した識別子を返します。</returns>
		public string ReadName(bool underScore,bool hyphen,bool atMark){
			if(" !\"#$%&'()=~|1234567890^\\[:]`{*},./<>?　\t\r\n".IndexOf(this.letter)>=0)return "";
			if(!underScore&&this.letter=="_"||!hyphen&&this.letter=="-"||!atMark&&this.letter=="@")return "";
			string r=this.letter;
			while(this.Next()){
				if(" !\"#$%&'()=~|^\\[:]`{*},./<>?　\t\r\n".IndexOf(this.letter)>=0)return r;
				if(!underScore&&this.letter=="_"||!hyphen&&this.letter=="-"||!atMark&&this.letter=="@")return r;
				r+=this.letter;
			}
			return r;
		}
		#endregion
	}
	/// <summary>
	/// 構文解析の為の基本文脈クラス。
	/// </summary>
	public abstract class Context:System.IDisposable{
		//***********************************************************
		//		<Field>		isInstance
		//-----------------------------------------------------------
		/// <summary>
		/// true: 読み取り文脈(実際に読み取りを行う文脈オブジェクト)であることを示します。
		/// false: 被参照文脈(読み取り文脈から参照される文脈)であることを示します。
		/// </summary>
		protected bool isInstance=false;
		/// <summary>
		/// 読み取り文脈で有効です。
		/// End of context, 文脈終了を表します。
		/// これを true に設定する事により、文脈を抜けたという事を Document に通知します。
		/// </summary>
		public bool EOC=false;
		//***********************************************************
		//		継承する文脈
		//-----------------------------------------------------------
		/// <summary>
		/// 継承元の文脈を保持します。
		/// </summary>
		protected System.Collections.ArrayList implements;
		/// <summary>
		/// 継承する文脈を登録します。
		/// 後に登録した物程優先度が高くなります。
		/// (同じ名前の Handler があった場合、上書きされます。)
		/// </summary>
		/// <param name="c">登録する文脈</param>
		public void AddImplement(Context c){
			if(this.GetType()!=c.GetType())throw new System.Exception("同じ型の Context を登録して下さい");
			this.implements.Add(c);
		}
		//***********************************************************
		//		<Constructor>
		//-----------------------------------------------------------
		bool disposed=false;
		/// <summary>
		/// 使わなくなった instance の始末をします。
		/// </summary>
		public void Dispose(){
			if(!this.disposed){
				this.implements.Clear();
				this.hLttr.Clear();
				this.disposed=true;
			}
		}
		//***********************************************************
		//		文字処理関数の管理
		//-----------------------------------------------------------
		protected System.Collections.Hashtable hLttr;
		/// <summary>
		/// 文字処理関数の delegate の System.Type を取得します。
		/// </summary>
		protected abstract System.Type GetLHType();
		/// <summary>
		/// 文字を処理する関数を登録します。
		/// </summary>
		/// <param name="letter">
		/// 処理する対象の文字
		/// "*default" を指定すると、登録されていない文字に対する処理を設定出来ます。
		/// 既に登録されている物に対しては、上書きを行います。
		/// </param>
		/// <param name="lh">
		/// 登録しようとしている関数。GetLHType() で指定されている型の delegate を指定して下さい。
		/// 内部で文字を次に進めるか文脈終了要求を提出する必要があります。
		/// <code>
		/// void HandleA(mwg.Parse.Document doc){
		///		doc.Next();//文字を次に進める
		/// }
		///	void HandleRightBracket(mwg.Parse.Document doc){
		///		doc.CurrentContext.EOC=true;//文脈終了要求を出す。
		///	}
		///	</code>
		/// </param>
		public void AddLetterHandler(string letter,System.Delegate lh){
			if(lh.GetType()!=this.GetLHType())throw new System.Exception("delegate の種類が異なる為登録出来ません");
			if(this.hLttr.Contains(letter)){
				this.hLttr[letter]=lh;
			}else{
				this.hLttr.Add(letter,lh);
			}
		}
		/// <summary>
		/// 登録した文字処理関数を削除します。
		/// </summary>
		/// <param name="letter">
		/// どの文字に割り当てられている関数を削除するか指定します。
		/// "*default" は指定しても削除されません。
		/// "*default" の動作を変更したい場合は AddLetterHandler で上書きして下さい。
		/// </param>
		public void RemoveLetterHandler(string letter){
			if(letter!="*default"&&this.hLttr.Contains(letter))this.hLttr.Remove(letter);
		}
		/// <summary>
		/// 要求された文字に対応する適切な文字処理関数を返します。
		/// 文字に直接対応する関数がない場合、以下の順番で関数を探し、見つかり次第それを返します。
		/// <list type="number">
		/// <item><description>小文字アルファベットなら *alpha</description></item>
		/// <item><description>大文字アルファベットなら *Alpha</description></item>
		/// <item><description>アルファベットなら *ALPHA</description></item>
		/// <item><description>識別子用文字なら *letter</description></item>
		/// <item><description>数字なら *number</description></item>
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
			}//他、スペース" ", "　" やタブ・改行"\t\n\r" も
			dl=this.GetLetterHandlerEx("*default");
			if(dl!=null)return dl;
			throw new System.Exception("*default が登録されていません");
		}
		/// <summary>
		/// 要求された名前を持つ文字処理関数を返します。
		/// 見つからない場合は null を返します。
		/// </summary>
		protected System.Delegate GetLetterHandlerEx(string key){
			//--自分の所から呼び出し
			System.Delegate dlg=this.hLttr[key] as System.Delegate;
			if(dlg!=null){
				LetterHandler2Txt[] ary=(LetterHandler2Txt[])dlg.GetInvocationList();
				return ary[ary.Length-1];
			}
			//--継承元からの呼び出し
			foreach(Context2Txt c in this.implements){
				dlg=c.GetLetterHandlerEx(key);
				if(dlg!=null)return dlg;
			}
			return null;
		}
	}
}
