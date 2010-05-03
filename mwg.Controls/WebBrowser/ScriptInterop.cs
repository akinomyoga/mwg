using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;
using Interop=System.Runtime.InteropServices;
using Rgx=System.Text.RegularExpressions;
using CM=System.ComponentModel;

namespace mwg.Controls.WebBrowser{
	[Interop::ComVisible(true)]
	public class ScriptChannel{
		private Window win;
		internal ScriptChannel(Window win){
			this.win=win;
		}

		private object retval=null;
		public object retVal{
			get{return this.retval;}
			set{this.retval=value;}
		}

		public object _args;
		public object args{
			get{return this._args;}
			set{this._args=value;}
		}

		public object this_ptr;
		public object This{
			get{return this_ptr;}
			set{this.this_ptr=value;}
		}

		public object InvokeD(System.Delegate deleg,object _arguments){
			ScriptObject arguments=win.ToScriptObject(_arguments);

			Ref::ParameterInfo[] pinfos=deleg.Method.GetParameters();
			object[] args=new object[pinfos.Length];
			for(int i=0;i<pinfos.Length;i++){
				System.Type ptype=pinfos[i].ParameterType;
				args[i]=arguments[i.ToString()].Value;

				if(args[i]==null){
					// 既定値の採用
					object[] attrs=pinfos[i].GetCustomAttributes(typeof(CM::DefaultValueAttribute),false);
					if(attrs.Length!=0){
						args[i]=((CM::DefaultValueAttribute)attrs[0]).Value;
					}else if(ptype==typeof(Event)){
						// Event Object の場合
						args[i]=win.@event;
					}
				}else if(!ptype.IsAssignableFrom(args[i].GetType())){
					// 自動型変換
					if(typeof(MshtmlObject).IsAssignableFrom(ptype)){
						try{
							Ref::MethodInfo fromObj=ptype.GetMethod("FromObj",Ref::BindingFlags.Static|Ref::BindingFlags.Public);
							args[i]=fromObj.Invoke(null,new object[]{args[i]});
						}catch{}
					}else if(typeof(ScriptObject)==ptype){
						args[i]=new ScriptObject(win,args[i]);
					}
				}
			}

			object ret;
			try{
				ret=deleg.Method.Invoke(deleg.Target,args);
			}catch(System.Reflection.TargetInvocationException e){
				if(e.InnerException==null)throw;
				throw e.InnerException;
			}
			return ret;
		}
	}

	public partial class Window{
		const string ROOT_NAME="<mwg::root>";
		const string ROOT="document.body['"+ROOT_NAME+"']";
		internal ScriptChannel root;

		private void InitExecution(){
			this.root=this.document.body.getAttribute(ROOT_NAME,0) as ScriptChannel;
			if(this.root==null){
				this.document.body.setAttribute(ROOT_NAME,this.root=new ScriptChannel(this),0);
			}
		}
		//============================================================
		//		配列変換処理
		//============================================================
		internal object ArrayFromClr(object[] array){
			this.root.retVal=new ClrArrayAccessor(array);
			this.execScript(@"(function(){
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
				object ret=this.array[index];
				return ret is IWrapper?((IWrapper)ret).Value:ret;
			}
		}
		//============================================================
		//		変換処理
		//============================================================
		public ScriptObject ToScriptObject(object value){
			if(value==null)return new ScriptObject(this,null);

			IWrapper wrapper=value as IWrapper;
			if(wrapper!=null){
				value=wrapper.Value;
			}

			System.Type t=value.GetType();
			if(t.IsArray&&t.GetArrayRank()==1){
				value=ArrayFromClr((object[])value);
			}else if(value is System.Delegate){
				System.Delegate dlg=(System.Delegate)value;
				ScriptObject ret;
				if(!funcs.TryGetValue(dlg,out ret)){
					ret=new ScriptObject(this,this.exec(@"var ret=function(){return "+ROOT+@".InvokeD(args[0],arguments);};
ret.toString=function(){return 'function "+dlg.Method.Name+@"(){\r\n\t[native code]\r\n}';};
return ret;",dlg));
					funcs.Add(dlg,ret);
				}
				return ret;
			}

			return new ScriptObject(this,value);
		}
		private Gen::Dictionary<System.Delegate,ScriptObject> funcs=new Gen::Dictionary<System.Delegate,ScriptObject>();
		//============================================================
		//		スクリプト実行
		//============================================================
		public object exec(string code,params object[] args){
			this.root.args=ArrayFromClr(args);
			this.execScript(ROOT+@".retVal=(function(){
	var args="+ROOT+@".args;
	"+code+@"
})();");
			return this.root.retVal;
		}
		public object eval(string value,params object[] args){
			this.root.args=ArrayFromClr(args);
			this.execScript(ROOT+@".retVal=(function(){
	var args="+ROOT+@".args;
	return "+value+@";
})();");
			return this.root.retVal;
		}
		public object apply(string code,object obj,params object[] args){
			this.root.This=obj;
			this.root.args=ArrayFromClr(args);
			this.execScript(ROOT+@".retVal=(function(){
	var args="+ROOT+@".args;
	var _this="+ROOT+@".This;
	"+code+@"
})();");
			return this.root.retVal;
		
		}
	}

	public interface IWrapper{
		object Value{get;}
	}
	public class ScriptObject:IWrapper{
		protected readonly Window win;
		protected readonly object obj;
		/// <summary>
		/// オブジェクトの実体を取得します。
		/// </summary>
		public object Value{get{return this.obj;}}

		internal ScriptObject(Window win,object raw){
			this.win=win;
			this.obj=raw;
		}
		//============================================================
		//		メンバアクセス
		//============================================================
		public ScriptObject Invoke(string name,params object[] args){
			return win.ToScriptObject(
				win.apply("return _this['"+escape_quote(name)+"'].apply(_this,args);",this.obj,args)
			);
		}
		public ScriptObject this[string key]{
			get{return win.ToScriptObject(win.eval("args[0][args[1]]",this.obj,key));}
			set{win.eval("args[0][args[1]]=args[2]",this.obj,key,value.Value);}
			//get{return win.ToScriptObject(win.eval("args[0]['"+escape_quote(key)+"']",this.obj));}
			//set{win.eval("args[0]['"+escape_quote(key)+"']=args[1]",this.obj,value.Value);}
		}
		static Rgx::Regex reg_escquote=new Rgx::Regex("[\\\\\'\"\\n\\r\\t]",Rgx::RegexOptions.Compiled);
		static string escape_quote(string name){
			return reg_escquote.Replace(name,delegate(Rgx::Match m){return "\\"+m.Value;});
		}
		//============================================================
		//		特殊演算
		//============================================================
		public bool InstanceOf(string typename){
			return (bool)win.apply("return _this instanceof typename;",this.obj);
		}
		public string TypeOf{
			get{return (string)win.apply("return typeof(_this)",this.obj);}
		}
		public bool In(string memberName){
			return (bool)win.apply("return args[0] in _this;",this.obj,memberName);
		}
		public bool IsUndefined{
			get{return (bool)win.apply("return _this==undefined",this.obj);}
		}
		public bool IsNull{
			get{return (bool)win.apply("return _this==null",this.obj);}
		}
	}
	public class Array:ScriptObject,Gen::IEnumerable<ScriptObject>,System.Collections.IEnumerable{
		internal Array(Window win,object raw):base(win,raw){}

		public int length{
			get{return (int)this["length"].Value;}
		}
		public ScriptObject this[int index]{
			get{return this[index.ToString()];}
			set{this[index.ToString()]=value;}
		}

		public Gen::IEnumerator<ScriptObject> GetEnumerator(){
			int len=this.length;
			for(int i=0;i<len;i++)yield return this[i];
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return this.GetEnumerator();
		}
	}
}