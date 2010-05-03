using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using Gen=System.Collections.Generic;

using Interop=System.Runtime.InteropServices;

namespace mwg.Controls.WebBrowserOld{
	/// <summary>
	/// 指定した Window で script を実行する為のクラスです。
	/// </summary>
	public class ScriptExecutor{
		internal HTMLWindow2 win;
		private readonly ScriptObject root;
		/// <summary>
		/// 指定した Window で ScriptExecutor のインスタンスを作成します。
		/// </summary>
		/// <param name="win">script の実行舞台となる window を指定します。</param>
		public ScriptExecutor(HTMLWindow2 win){//Interop::Marshal.
			this.win=win;

			// execFunc 用の初期化
			this.exec("document.body['<mwg:root>']={creatingArrays:[]};");
			this._arg=new ExecArguments(this);
			this.root=new ScriptObject(this,win.document.body.getAttribute("<mwg:root>",0));

			this.root["executor"]=this.FromManaged(this);
		}
		/// <summary>
		/// javascript で記述されたスクリプトを実行します。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		public void exec(string code){
			try{
				win.execScript(code,"javascript");
			}catch{
				System.Windows.Forms.MessageBox.Show("スクリプトの実行中にエラーが発生しました。");
			}
		}
		/// <summary>
		/// .NET のオブジェクトを ScriptObject に変換 (ボックス化) します。
		/// </summary>
		/// <param name="obj">変換前の object を指定します。</param>
		/// <returns>変換後の ScriptObject を指定します。</returns>
		public ScriptObject FromManaged(object obj){
			return ScriptObject.FromManaged(this,obj);
		}
		//=================================================================
		//		値のやりとり
		//=================================================================
		private ScriptObject RetVal{
			get{return this.FromManaged(this.win.document.body.getAttribute("<mwg:retval>",0));}
			set{this.win.document.body.setAttribute("<mwg:retval>",value.obj,0);}
		}
		//=================================================================
		//		関数として実行
		//=================================================================
		private ExecArguments _arg;
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <returns>実行した結果返された値を返します。つまり return 文によって返された値を示します。</returns>
		public ScriptObject execFunc(string code) {
			this.exec("document.body['<mwg:retval>']=(function($){"+code+"})(document.body['<mwg:root>'].args);");
			return RetVal;
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="arg0">コード内で参照したい ScriptObject を指定します。コードからは $[0] で参照して下さい。</param>
		/// <returns>実行した結果返された値を返します。つまり return 文によって返された値を示します。</returns>
		public ScriptObject execFunc(string code,ScriptObject arg0){
			_arg[0]=arg0;
			return execFunc(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="arg0">コード内で参照したい ScriptObject を指定します。コードからは $[0] で参照して下さい。</param>
		/// <param name="arg1">コード内で参照したい ScriptObject を指定します。コードからは $[1] で参照して下さい。</param>
		/// <returns>実行した結果返された値を返します。つまり return 文によって返された値を示します。</returns>
		public ScriptObject execFunc(string code,ScriptObject arg0,ScriptObject arg1){
			_arg[0]=arg0;
			_arg[1]=arg1;
			return execFunc(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="arg0">コード内で参照したい ScriptObject を指定します。コードからは $[0] で参照して下さい。</param>
		/// <param name="arg1">コード内で参照したい ScriptObject を指定します。コードからは $[1] で参照して下さい。</param>
		/// <param name="arg2">コード内で参照したい ScriptObject を指定します。コードからは $[2] で参照して下さい。</param>
		/// <returns>実行した結果返された値を返します。つまり return 文によって返された値を示します。</returns>
		public ScriptObject execFunc(string code,ScriptObject arg0,ScriptObject arg1,ScriptObject arg2){
			_arg[0]=arg0;
			_arg[1]=arg1;
			_arg[2]=arg2;
			return execFunc(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="args">
		/// コード内で参照したい ScriptObject を指定します。
		/// コードからは $[n] で参照して下さい。n は 0 で始まる序数です。
		/// </param>
		/// <returns>実行した結果返された値を返します。つまり return 文によって返された値を示します。</returns>
		public ScriptObject execFunc(string code,params ScriptObject[] args){
			for(int i=0;i<args.Length;i++)_arg[i]=args[i];
			return execFunc(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。値は返しません。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		public void execSub(string code){
			this.exec("document.body['<mwg:retval>']=function($){"+code+"}(document.body['<mwg:root>'].args);");
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。値は返しません。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="arg0">コード内で参照したい ScriptObject を指定します。コードからは $[0] で参照して下さい。</param>
		public void execSub(string code,ScriptObject arg0){
			_arg[0]=arg0;
			execSub(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。値は返しません。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="arg0">コード内で参照したい ScriptObject を指定します。コードからは $[0] で参照して下さい。</param>
		/// <param name="arg1">コード内で参照したい ScriptObject を指定します。コードからは $[1] で参照して下さい。</param>
		public void execSub(string code,ScriptObject arg0,ScriptObject arg1){
			_arg[0]=arg0;
			_arg[1]=arg1;
			execSub(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。値は返しません。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="arg0">コード内で参照したい ScriptObject を指定します。コードからは $[0] で参照して下さい。</param>
		/// <param name="arg1">コード内で参照したい ScriptObject を指定します。コードからは $[1] で参照して下さい。</param>
		/// <param name="arg2">コード内で参照したい ScriptObject を指定します。コードからは $[2] で参照して下さい。</param>
		public void execSub(string code,ScriptObject arg0,ScriptObject arg1,ScriptObject arg2){
			_arg[0]=arg0;
			_arg[1]=arg1;
			_arg[2]=arg2;
			execSub(code);
		}
		/// <summary>
		/// 指定したスクリプトを関数の中身として実行します。値は返しません。
		/// </summary>
		/// <param name="code">実行するコードを指定します。</param>
		/// <param name="args">
		/// コード内で参照したい ScriptObject を指定します。
		/// コードからは $[n] で参照して下さい。n は 0 で始まる序数です。
		/// </param>
		public void execSub(string code,params ScriptObject[] args){
			for(int i=0;i<args.Length;i++)_arg[i]=args[i];
			execSub(code);
		}
		//=================================================================
		//		関数の設定
		//=================================================================
		public delegate object StringCbRetval(string text);
		public delegate void VoidCb();
		public void SetFunction(string target,StringCbRetval function){
			this.execSub(@"var deleg=$[0];
"+target+@"=function(text){
	document.body['<mwg:root>'].delegate=deleg;
	document.body['<mwg:root>'].arguments=arguments;
	document.body['<mwg:root>'].executor.InvokeFunction();
	return document.body['<mwg:root>'].retval;
};",this.FromManaged(function));
		}
		/// <summary>
		/// 指定した関数を指定した引数で実行します。
		/// 関数は System.Delegate で document.body['&lt;mwg:root&gt;'].delegate に指定します。
		/// 引数は System.Delegate で document.body['&lt;mwg:root&gt;'].arguments に指定します。
		/// </summary>
		/// <return>
		/// 返値は System.Delegate で document.body['&lt;mwg:root&gt;'].retval に返します。</arg>
		/// </return>
		public void InvokeFunction(){
			System.Delegate dlg=(System.Delegate)root["delegate"].obj;

			// 引数の準備
			ScriptObject arguments=root["arguments"];
			ScriptObject param;
			System.Reflection.ParameterInfo[] paraminfos=dlg.Method.GetParameters();
			object[] parameters=new object[paraminfos.Length];
			for(int i=0;i<paraminfos.Length;i++){
				param=arguments[i.ToString()];
				parameters[i]=param.ToManaged();
				if(parameters[i]!=null){
					// 既定値の採用
					object[] attrs=paraminfos[i].GetCustomAttributes(typeof(System.ComponentModel.DefaultValueAttribute),false);
					if(attrs.Length==0)continue;
					parameters[i]=((System.ComponentModel.DefaultValueAttribute)attrs[0]).Value;
				}
			}

			// 実行及び返値の設定
			object ret;
			try{
				ret=dlg.Method.Invoke(dlg.Target,parameters);
			}catch(System.Reflection.TargetInvocationException e){
				if(e.InnerException==null)throw;
				throw new System.Exception(e.InnerException.ToString());
			}
			this.root["retval"]=this.FromManaged(ret);
		}
	}
	internal sealed class ExecArguments{
		ScriptExecutor x;
		internal ExecArguments(ScriptExecutor x) {
			this.x=x;
			x.exec("document.body['<mwg:root>'].args=[];");
		}
		public ScriptObject this[int index]{
			get{
				x.exec("document.body['<msg:argument>']=document.body['<mwg:root>'].args["+index.ToString()+"];");
				return x.FromManaged(x.win.document.body.getAttribute("<mwg:argument>",0));
			}
			set{
				x.win.document.body.setAttribute("<mwg:argument>",value.obj,0);
				x.exec("document.body['<mwg:root>'].args["+index.ToString()+"]=document.body['<mwg:argument>'];");
			}
		}
	}
	/// <summary>
	/// スクリプトのオブジェクトに対するアクセスを提供するクラスです。
	/// 簡単に言えば、スクリプトで使用されるオブジェクトを表しますが、
	/// 実際は単なるボックス化で実体は obj フィールドにあります。
	/// 実体を取得するには Value プロパティを使用して下さい。
	/// </summary>
	public class ScriptObject{
		//=================================================
		//		fld:x
		//=================================================
		/// <summary>
		/// このオブジェクトに関連付けられた ScriptExecutor を保持します。
		/// </summary>
		protected ScriptExecutor x;
		//=================================================
		//		fld:obj
		//=================================================
		/// <summary>
		/// このオブジェクトの実体を保持します。
		/// </summary>
		internal object obj;
		/// <summary>
		/// このオブジェクトの参照先の実体を取得します。
		/// </summary>
		public object Value{get{return obj;}}
		internal ScriptObject(ScriptExecutor x,object obj){
			this.x=x;
			this.obj=obj;
		}
		public ScriptObject InvokeMember(string name){
			return x.execFunc("return $[0]['"+resolveName(name)+"']();",this);
		}
		/// <summary>
		/// メンバにアクセスします。
		/// </summary>
		/// <param name="name">メンバの名前を指定します。</param>
		/// <returns>指定した名前のメンバを返します。</returns>
		public ScriptObject this[string name]{
			get{return x.execFunc("return $[0]['"+resolveName(name)+"'];",this);}
			set{x.execSub("$[0]['"+resolveName(name)+"']=$[1];",this,value);}
		}
		//=================================================
		//		基本演算
		//=================================================
		/// <summary>
		/// script の toString() 関数を呼び出します。指定したオブジェクトを文字列にして表します。
		/// </summary>
		/// <returns>toString の実行結果を返します。</returns>
		public string toString(){return x.execFunc("return $[0].toString();",this).obj as string;}
		/// <summary>
		/// このオブジェクトが、指定した関数によって作成されたインスタンスであるかどうかを取得します。
		/// </summary>
		/// <param name="ctor">コンストラクタ関数を指定します。</param>
		/// <returns>指定した関数によるインスタンスであった場合に true を返します。
		/// それ以外の時には false を返します。</returns>
		public bool instanceof(string ctor){
			return (bool)x.execFunc("return $[0] instanceof "+ctor,this).obj;
		}
		/// <summary>
		/// typeof に対する結果を返します。このオブジェクトの種類を取得するのに使用します。
		/// </summary>
		public string @typeof{
			get{return x.execFunc("return typeof($[0]);",this).obj as string;}
		}
		public bool @in(string memberName){
			return (bool)x.execFunc("return '"+resolveName(memberName)+"' in $[0];",this).obj;
		}
		public bool isNull{get{return (bool)x.execFunc("return $[0]==null;",this).obj;}}
		public bool isUndefined{get{return (bool)x.execFunc("return $[0]==undefined;",this).obj;}}
		public string[] Keys{
			get{
				System.Collections.ArrayList list=new System.Collections.ArrayList();
				ScriptObject arr=x.execFunc("for(key in $[0])$[1].Add(key);",this,x.FromManaged(list));
				return (string[])list.ToArray(typeof(string));
			}
		}
		//=================================================
		//		他
		//=================================================
		private static string resolveName(string name){
			return name
				.Replace(@"\",@"\\").Replace("'",@"\'")
				.Replace("\n",@"\n").Replace("\t",@"\t");
		}
		internal static ScriptObject FromManaged(ScriptExecutor x,object obj){
			if(obj==null)return new ScriptObject(x,obj);

			System.Type t=obj.GetType();

			// 配列に変換
			if(t.IsArray&&((System.Array)obj).Rank==1) {
				object[] arr=(object[])obj;
				Array a=(Array)x.execFunc("var a=[];document.body['<mwg:root>'].creatingArrays.push(a);return a;");
				for(int i=0,m=arr.Length;i<m;i++){
					x.execSub("$[0].push($[1]);",a,x.FromManaged(arr[i]));
				}
				return a;
			}

			ScriptObject r=new ScriptObject(x,obj);
			if(t==typeof(string)||t.IsValueType)return r;

			// 具体化
			if(r.instanceof("Array")){
				return new Array(x,obj);
			}else switch(r.@typeof){
				case "object":
				case "unknown":
				case "number":
				case "string":
				case "null":
				default:
					return r;
			}
		}
		/// <summary>
		/// ScriptObject を .NET のオブジェクトに変換します。
		/// </summary>
		/// <returns>変換されたオブジェクトを返します。</returns>
		public object ToManaged(){
			return this.ToManaged(new Gen::Dictionary<Array,object[]>());
		}
		private object ToManaged(Gen::Dictionary<Array,object[]> nest){
			if(this.isNull||this.isUndefined)return null;
			if(this.GetType()==typeof(Array)) {
				Array thisArr=(Array)this;

				// 自身を含むような配列で無限ループになるのを防ぐ
				if(nest.ContainsKey(thisArr))return nest[thisArr];

				int m=thisArr.length;
				object[] retArr=new object[m];
				nest[thisArr]=retArr;
				for(int i=0;i<m;i++){
					retArr[i]=thisArr[i].ToManaged(nest);
				}
				return retArr;
			}
			return this.obj;
		}
	}
	/// <summary>
	/// javascript の配列にアクセスする為のクラスです。
	/// </summary>
	public class Array:ScriptObject,System.Collections.Generic.IEnumerable<ScriptObject>,System.Collections.IEnumerable{
		internal Array(ScriptExecutor x,object obj):base(x,obj){
			if(!this.instanceof("Array")){
				throw new System.InvalidCastException("指定した object を javascript:Array に変換出来ません。");
			}
		}
		/// <summary>
		/// 配列の要素に取得亦は設定します。
		/// </summary>
		/// <param name="index">取得亦は設定する配列の要素の番号を指定します。</param>
		/// <returns>配列の要素を返します。</returns>
		public ScriptObject this[int index]{
			get{return this[index.ToString()];}
			set{this[index.ToString()]=value;}
		}
		/// <summary>
		/// 配列の長さを返します。
		/// </summary>
		public int length{
			get{
				try{
					return (int)x.execFunc("return $[0].length",this).obj;
				}catch{return 0;}
			}
		}
		//=======================================
		//		列挙子
		//=======================================
		/// <summary>
		/// 配列の要素を列挙します。
		/// </summary>
		/// <returns>配列の要素を列挙する列挙子を返します。</returns>
		public System.Collections.Generic.IEnumerator<ScriptObject> GetEnumerator(){
			int m=this.length;
			for(int i=0;i<m;i++)yield return this[i];
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
	}
	/// <summary>
	/// 別の HTML 文書のスクリプトオブジェクトをスクリプトで扱う為の物
	/// </summary>
	public class ObjectProxy:ScriptObject{
		private ObjectProxy(ScriptExecutor x,object obj):base(x,obj){}
		public object getMember(string name){return new ObjectProxy(x,this[name].obj);}
		public void setMember(string name,object value){this[name]=x.FromManaged(value);}
		public object keys(){
			return x.execFunc("var arr=[];for(key in $[0])arr.push(key);return arr;",this).obj;
		}
	}
}
