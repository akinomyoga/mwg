using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Interop=System.Runtime.InteropServices;
using Window=mwg.Controls.WebBrowser.Window;

namespace mwg.Controls.WebBrowserTest{
	public interface IWrapper{
		object RawObject{get;}
	}
	[Interop::ComVisible(true)]
	public class ScriptChannel{
		private object retval=null;
		public object retVal{
			get{return this.retval;}
			set{this.retval=value;}
		}

		//
		//	引数
		//
		public object _args;
		public object args{
			get{return this._args;}
			set{this._args=value;}
		}
#if TOARRAY
		public ScriptChannel(Window window){
			window.document.body.setAttribute(ScriptExecutor.ROOT_NAME,this,0);
			window.execScript(ScriptExecutor.ROOT+@".toArray=function(value){
	var ret=[];
	for(var i=0,m=value.length;i<m;i++){
		ret[i]=value.getVal(i);
	}
}");
		}

		public object _toArray;
		public object toArray{
			get{return this._toArray;}
			set{this._toArray=value;}
		}
#endif
	}
	public class ScriptExecutor{
		internal const string ROOT_NAME="<mwg::root>";
		internal const string ROOT="document.body['"+ROOT_NAME+"']";
		readonly Window window;
		readonly ScriptChannel root;

		public ScriptExecutor(Window window){
			this.window=window;

			this.root=window.document.body.getAttribute(ROOT_NAME,0) as ScriptChannel;
			if(this.root==null){
				window.document.body.setAttribute(ROOT_NAME,this.root=new ScriptChannel(),0);
			}
		}
		//============================================================
		//		配列変換処理
		//============================================================
		internal object ArrayFromClr(object[] array){
			this.root.retVal=new ClrArrayAccessor(array);
			window.execScript(@"(function(){
	var value="+ROOT+@".retVal;
	var ret=[];
	for(var i=0,m=value.length;i<m;i++){
		ret[i]=value.getVal(i);
	}
	"+ROOT+@".retVal=ret;
})();");
			return this.root.retVal;
		}
		[Interop::ComVisible(true)]
		public class ClrArrayAccessor{
			private object[] array;
			internal ClrArrayAccessor(object[] array){
				this.array=array;
			}

			public int length{
				get{return array.Length;}
			}
			public object getVal(int index){
				return this.array[index];
			}
		}
		//============================================================
		//		スクリプト実行
		//============================================================
		public object exec(string code,params object[] args){
			this.root.args=ArrayFromClr(args);
			window.execScript(ROOT+@".retVal=(function(){
	var args="+ROOT+@".args;
	"+code+@"
})();");
			return this.root.retVal;
		}
		public object eval(string value,params object[] args){
			this.root.args=ArrayFromClr(args);
			window.execScript(ROOT+@".retVal=(function(){
	var args="+ROOT+@".args;
	return "+value+@"
})();");
			return this.root.retVal;
		}
	}
	public class ScriptObject:IWrapper{
		protected ScriptExecutor exe;
		internal object obj;
		/// <summary>
		/// オブジェクトの実体を取得します。
		/// </summary>
		public object Value{get{return this.obj;}}
		object IWrapper.RawObject{get{return this.obj;}}

		internal ScriptObject(ScriptExecutor exe,object raw){
			this.exe=exe;
			this.obj=raw;
		}
	}
	/*
	public class Array:ScriptObject{
		public static object FromClrArray(ScriptExecutor exe,object[] array){
			return exe.eval(@"
var ret=[];
for(int i=0,m=args[0].length;i<m;i++)ret[i]=args[0].getVal(i);
return ret;
",new ClrArrayAccessor(array));
		}
		[Interop::ComVisible(true)]
		public class ClrArrayAccessor{
			private object[] array;
			internal ClrArrayAccessor(object[] array){
				this.array=array;
			}

			public int length{
				get{return array.Length;}
			}
			public object getVal(int index){
				return this.array[index];
			}
		}
	}
	//*/
}