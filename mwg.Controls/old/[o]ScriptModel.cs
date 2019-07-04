using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using Gen=System.Collections.Generic;

using Interop=System.Runtime.InteropServices;

namespace mwg.Controls.WebBrowserOld{
	/// <summary>
	/// �w�肵�� Window �� script �����s����ׂ̃N���X�ł��B
	/// </summary>
	public class ScriptExecutor{
		internal HTMLWindow2 win;
		private readonly ScriptObject root;
		/// <summary>
		/// �w�肵�� Window �� ScriptExecutor �̃C���X�^���X���쐬���܂��B
		/// </summary>
		/// <param name="win">script �̎��s����ƂȂ� window ���w�肵�܂��B</param>
		public ScriptExecutor(HTMLWindow2 win){//Interop::Marshal.
			this.win=win;

			// execFunc �p�̏�����
			this.exec("document.body['<mwg:root>']={creatingArrays:[]};");
			this._arg=new ExecArguments(this);
			this.root=new ScriptObject(this,win.document.body.getAttribute("<mwg:root>",0));

			this.root["executor"]=this.FromManaged(this);
		}
		/// <summary>
		/// javascript �ŋL�q���ꂽ�X�N���v�g�����s���܂��B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		public void exec(string code){
			try{
				win.execScript(code,"javascript");
			}catch{
				System.Windows.Forms.MessageBox.Show("�X�N���v�g�̎��s���ɃG���[���������܂����B");
			}
		}
		/// <summary>
		/// .NET �̃I�u�W�F�N�g�� ScriptObject �ɕϊ� (�{�b�N�X��) ���܂��B
		/// </summary>
		/// <param name="obj">�ϊ��O�� object ���w�肵�܂��B</param>
		/// <returns>�ϊ���� ScriptObject ���w�肵�܂��B</returns>
		public ScriptObject FromManaged(object obj){
			return ScriptObject.FromManaged(this,obj);
		}
		//=================================================================
		//		�l�̂��Ƃ�
		//=================================================================
		private ScriptObject RetVal{
			get{return this.FromManaged(this.win.document.body.getAttribute("<mwg:retval>",0));}
			set{this.win.document.body.setAttribute("<mwg:retval>",value.obj,0);}
		}
		//=================================================================
		//		�֐��Ƃ��Ď��s
		//=================================================================
		private ExecArguments _arg;
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <returns>���s�������ʕԂ��ꂽ�l��Ԃ��܂��B�܂� return ���ɂ���ĕԂ��ꂽ�l�������܂��B</returns>
		public ScriptObject execFunc(string code) {
			this.exec("document.body['<mwg:retval>']=(function($){"+code+"})(document.body['<mwg:root>'].args);");
			return RetVal;
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="arg0">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[0] �ŎQ�Ƃ��ĉ������B</param>
		/// <returns>���s�������ʕԂ��ꂽ�l��Ԃ��܂��B�܂� return ���ɂ���ĕԂ��ꂽ�l�������܂��B</returns>
		public ScriptObject execFunc(string code,ScriptObject arg0){
			_arg[0]=arg0;
			return execFunc(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="arg0">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[0] �ŎQ�Ƃ��ĉ������B</param>
		/// <param name="arg1">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[1] �ŎQ�Ƃ��ĉ������B</param>
		/// <returns>���s�������ʕԂ��ꂽ�l��Ԃ��܂��B�܂� return ���ɂ���ĕԂ��ꂽ�l�������܂��B</returns>
		public ScriptObject execFunc(string code,ScriptObject arg0,ScriptObject arg1){
			_arg[0]=arg0;
			_arg[1]=arg1;
			return execFunc(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="arg0">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[0] �ŎQ�Ƃ��ĉ������B</param>
		/// <param name="arg1">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[1] �ŎQ�Ƃ��ĉ������B</param>
		/// <param name="arg2">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[2] �ŎQ�Ƃ��ĉ������B</param>
		/// <returns>���s�������ʕԂ��ꂽ�l��Ԃ��܂��B�܂� return ���ɂ���ĕԂ��ꂽ�l�������܂��B</returns>
		public ScriptObject execFunc(string code,ScriptObject arg0,ScriptObject arg1,ScriptObject arg2){
			_arg[0]=arg0;
			_arg[1]=arg1;
			_arg[2]=arg2;
			return execFunc(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="args">
		/// �R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B
		/// �R�[�h����� $[n] �ŎQ�Ƃ��ĉ������Bn �� 0 �Ŏn�܂鏘���ł��B
		/// </param>
		/// <returns>���s�������ʕԂ��ꂽ�l��Ԃ��܂��B�܂� return ���ɂ���ĕԂ��ꂽ�l�������܂��B</returns>
		public ScriptObject execFunc(string code,params ScriptObject[] args){
			for(int i=0;i<args.Length;i++)_arg[i]=args[i];
			return execFunc(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B�l�͕Ԃ��܂���B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		public void execSub(string code){
			this.exec("document.body['<mwg:retval>']=function($){"+code+"}(document.body['<mwg:root>'].args);");
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B�l�͕Ԃ��܂���B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="arg0">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[0] �ŎQ�Ƃ��ĉ������B</param>
		public void execSub(string code,ScriptObject arg0){
			_arg[0]=arg0;
			execSub(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B�l�͕Ԃ��܂���B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="arg0">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[0] �ŎQ�Ƃ��ĉ������B</param>
		/// <param name="arg1">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[1] �ŎQ�Ƃ��ĉ������B</param>
		public void execSub(string code,ScriptObject arg0,ScriptObject arg1){
			_arg[0]=arg0;
			_arg[1]=arg1;
			execSub(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B�l�͕Ԃ��܂���B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="arg0">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[0] �ŎQ�Ƃ��ĉ������B</param>
		/// <param name="arg1">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[1] �ŎQ�Ƃ��ĉ������B</param>
		/// <param name="arg2">�R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B�R�[�h����� $[2] �ŎQ�Ƃ��ĉ������B</param>
		public void execSub(string code,ScriptObject arg0,ScriptObject arg1,ScriptObject arg2){
			_arg[0]=arg0;
			_arg[1]=arg1;
			_arg[2]=arg2;
			execSub(code);
		}
		/// <summary>
		/// �w�肵���X�N���v�g���֐��̒��g�Ƃ��Ď��s���܂��B�l�͕Ԃ��܂���B
		/// </summary>
		/// <param name="code">���s����R�[�h���w�肵�܂��B</param>
		/// <param name="args">
		/// �R�[�h���ŎQ�Ƃ����� ScriptObject ���w�肵�܂��B
		/// �R�[�h����� $[n] �ŎQ�Ƃ��ĉ������Bn �� 0 �Ŏn�܂鏘���ł��B
		/// </param>
		public void execSub(string code,params ScriptObject[] args){
			for(int i=0;i<args.Length;i++)_arg[i]=args[i];
			execSub(code);
		}
		//=================================================================
		//		�֐��̐ݒ�
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
		/// �w�肵���֐����w�肵�������Ŏ��s���܂��B
		/// �֐��� System.Delegate �� document.body['&lt;mwg:root&gt;'].delegate �Ɏw�肵�܂��B
		/// ������ System.Delegate �� document.body['&lt;mwg:root&gt;'].arguments �Ɏw�肵�܂��B
		/// </summary>
		/// <return>
		/// �Ԓl�� System.Delegate �� document.body['&lt;mwg:root&gt;'].retval �ɕԂ��܂��B</arg>
		/// </return>
		public void InvokeFunction(){
			System.Delegate dlg=(System.Delegate)root["delegate"].obj;

			// �����̏���
			ScriptObject arguments=root["arguments"];
			ScriptObject param;
			System.Reflection.ParameterInfo[] paraminfos=dlg.Method.GetParameters();
			object[] parameters=new object[paraminfos.Length];
			for(int i=0;i<paraminfos.Length;i++){
				param=arguments[i.ToString()];
				parameters[i]=param.ToManaged();
				if(parameters[i]!=null){
					// ����l�̗̍p
					object[] attrs=paraminfos[i].GetCustomAttributes(typeof(System.ComponentModel.DefaultValueAttribute),false);
					if(attrs.Length==0)continue;
					parameters[i]=((System.ComponentModel.DefaultValueAttribute)attrs[0]).Value;
				}
			}

			// ���s�y�ѕԒl�̐ݒ�
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
	/// �X�N���v�g�̃I�u�W�F�N�g�ɑ΂���A�N�Z�X��񋟂���N���X�ł��B
	/// �ȒP�Ɍ����΁A�X�N���v�g�Ŏg�p�����I�u�W�F�N�g��\���܂����A
	/// ���ۂ͒P�Ȃ�{�b�N�X���Ŏ��̂� obj �t�B�[���h�ɂ���܂��B
	/// ���̂��擾����ɂ� Value �v���p�e�B���g�p���ĉ������B
	/// </summary>
	public class ScriptObject{
		//=================================================
		//		fld:x
		//=================================================
		/// <summary>
		/// ���̃I�u�W�F�N�g�Ɋ֘A�t����ꂽ ScriptExecutor ��ێ����܂��B
		/// </summary>
		protected ScriptExecutor x;
		//=================================================
		//		fld:obj
		//=================================================
		/// <summary>
		/// ���̃I�u�W�F�N�g�̎��̂�ێ����܂��B
		/// </summary>
		internal object obj;
		/// <summary>
		/// ���̃I�u�W�F�N�g�̎Q�Ɛ�̎��̂��擾���܂��B
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
		/// �����o�ɃA�N�Z�X���܂��B
		/// </summary>
		/// <param name="name">�����o�̖��O���w�肵�܂��B</param>
		/// <returns>�w�肵�����O�̃����o��Ԃ��܂��B</returns>
		public ScriptObject this[string name]{
			get{return x.execFunc("return $[0]['"+resolveName(name)+"'];",this);}
			set{x.execSub("$[0]['"+resolveName(name)+"']=$[1];",this,value);}
		}
		//=================================================
		//		��{���Z
		//=================================================
		/// <summary>
		/// script �� toString() �֐����Ăяo���܂��B�w�肵���I�u�W�F�N�g�𕶎���ɂ��ĕ\���܂��B
		/// </summary>
		/// <returns>toString �̎��s���ʂ�Ԃ��܂��B</returns>
		public string toString(){return x.execFunc("return $[0].toString();",this).obj as string;}
		/// <summary>
		/// ���̃I�u�W�F�N�g���A�w�肵���֐��ɂ���č쐬���ꂽ�C���X�^���X�ł��邩�ǂ������擾���܂��B
		/// </summary>
		/// <param name="ctor">�R���X�g���N�^�֐����w�肵�܂��B</param>
		/// <returns>�w�肵���֐��ɂ��C���X�^���X�ł������ꍇ�� true ��Ԃ��܂��B
		/// ����ȊO�̎��ɂ� false ��Ԃ��܂��B</returns>
		public bool instanceof(string ctor){
			return (bool)x.execFunc("return $[0] instanceof "+ctor,this).obj;
		}
		/// <summary>
		/// typeof �ɑ΂��錋�ʂ�Ԃ��܂��B���̃I�u�W�F�N�g�̎�ނ��擾����̂Ɏg�p���܂��B
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
		//		��
		//=================================================
		private static string resolveName(string name){
			return name
				.Replace(@"\",@"\\").Replace("'",@"\'")
				.Replace("\n",@"\n").Replace("\t",@"\t");
		}
		internal static ScriptObject FromManaged(ScriptExecutor x,object obj){
			if(obj==null)return new ScriptObject(x,obj);

			System.Type t=obj.GetType();

			// �z��ɕϊ�
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

			// ��̉�
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
		/// ScriptObject �� .NET �̃I�u�W�F�N�g�ɕϊ����܂��B
		/// </summary>
		/// <returns>�ϊ����ꂽ�I�u�W�F�N�g��Ԃ��܂��B</returns>
		public object ToManaged(){
			return this.ToManaged(new Gen::Dictionary<Array,object[]>());
		}
		private object ToManaged(Gen::Dictionary<Array,object[]> nest){
			if(this.isNull||this.isUndefined)return null;
			if(this.GetType()==typeof(Array)) {
				Array thisArr=(Array)this;

				// ���g���܂ނ悤�Ȕz��Ŗ������[�v�ɂȂ�̂�h��
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
	/// javascript �̔z��ɃA�N�Z�X����ׂ̃N���X�ł��B
	/// </summary>
	public class Array:ScriptObject,System.Collections.Generic.IEnumerable<ScriptObject>,System.Collections.IEnumerable{
		internal Array(ScriptExecutor x,object obj):base(x,obj){
			if(!this.instanceof("Array")){
				throw new System.InvalidCastException("�w�肵�� object �� javascript:Array �ɕϊ��o���܂���B");
			}
		}
		/// <summary>
		/// �z��̗v�f�Ɏ擾���͐ݒ肵�܂��B
		/// </summary>
		/// <param name="index">�擾���͐ݒ肷��z��̗v�f�̔ԍ����w�肵�܂��B</param>
		/// <returns>�z��̗v�f��Ԃ��܂��B</returns>
		public ScriptObject this[int index]{
			get{return this[index.ToString()];}
			set{this[index.ToString()]=value;}
		}
		/// <summary>
		/// �z��̒�����Ԃ��܂��B
		/// </summary>
		public int length{
			get{
				try{
					return (int)x.execFunc("return $[0].length",this).obj;
				}catch{return 0;}
			}
		}
		//=======================================
		//		�񋓎q
		//=======================================
		/// <summary>
		/// �z��̗v�f��񋓂��܂��B
		/// </summary>
		/// <returns>�z��̗v�f��񋓂���񋓎q��Ԃ��܂��B</returns>
		public System.Collections.Generic.IEnumerator<ScriptObject> GetEnumerator(){
			int m=this.length;
			for(int i=0;i<m;i++)yield return this[i];
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
	}
	/// <summary>
	/// �ʂ� HTML �����̃X�N���v�g�I�u�W�F�N�g���X�N���v�g�ň����ׂ̕�
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
