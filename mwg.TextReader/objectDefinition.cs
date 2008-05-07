
namespace mwg.Text{
#if 未だ完成していません
	/// <summary>
	/// class 宣言を読み取るクラスです。
	/// </summary>
	/**
	次の形式のファイルの読み取りを行います
	class object1//何のクラスかの説明
	{
		private void MyFunction(int a,int b);//何をする為の関数かの説明
		public int MyInt(string str1);//説明
		public static explicit operator object1(int a1);//説明
		public const int MyContant=1234;//説明
		private int MyProperty{get;set;};//説明
	}
	(注意)
	改行は許しません。
	一つの定義は一行にして下さい
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
				lineDat.Trim(new char[]{' ','　','\t','\n'});
				if(lineDat.Length==0)continue;
				//宣言本体とコメントの取得
				comment="";
				i=lineDat.IndexOf("//");
				comment=lineDat.Substring(i+2);
				lineDat=lineDat.Substring(0,i);
				j=lineDat.IndexOf(";");
				if(j!=-1)lineDat=lineDat.Substring(0,j);
				
				// '=' がある ==> 変数
				if(lineDat.IndexOf("=")!=-1){
					//HandleVariant(/*渡す引数を茲に記述*/);
					//
					//ToDo:ここにHTMLに書き出す操作なり何なりを追加
					//HandleVariantとcommentを使用
					continue;//この宣言の処理を次に回す
				}
				
				// '(' がある ==> 関数
				// * '='で変数に回している為、'('が変数のコンストラクタの可能性はない
				if(lineDat.IndexOf("(")!=-1){
					HandleFunction(/*渡す引数を茲に記述*/);
					//
					//ToDo:
					//
					continue;//この宣言の処理はこれで終わり。
				}
			}
			
			
		}
		private static string HandleFunction(string line){
			int a=line.IndexOf("(");
			int b=line.LastIndexOf(")");
			string declaration=line.Substring(0,a);
			string hikisuu=line.Substring(a+1,b);
			//getModifiers(/*渡す引数を茲に記述*/);
		}
		private static string HandleVariant(string line){
			return "";
			// TODO: 変数を解析して結果を返すようにする。
		}
		//=====================================
		//          修飾子の取得の為の関数
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
				// TODO: str の内容を解析してmodifier 構造体を初期化するようにする。
				AccessMod=accessMod.Private;
				InheritMod=inheritMod.None;
				Readonly=false;
				Extern=false;
			}
			
		}
		private static modifier getModifiers(string line){
			//line は identifier まで含めた形で受け取る。'private void int myFunction'など
			string[] line2=line.Split(new char[]{' ','　','\t'});
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
		//          クラス、デリゲートなど構造の種類を取得する
		//-------------------------------------------------------
		private enum structType{Default,Class,Struct,Interface,Enum,Delegate,Const,Event}
		private static structType getStructType(string line){
			string[] line2=line.Split(new char[]{' ','　','\t'});
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