using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	/// <summary>
	/// mshtml.dll �Œ�`����Ă���I�u�W�F�N�g�́A��{���b�p�N���X�ł��B
	/// </summary>
	public class MshtmlObject:IWrapper{
		private readonly static System.Type mshtml=System.Type.GetTypeFromProgID("mhtmlfile");
		protected readonly object instance;
		object IWrapper.Value{get{return this.instance;}}
		internal MshtmlObject(object inst){
			this.instance=inst;
		}
		public override bool Equals(object obj){
			IWrapper wrapper=obj as IWrapper;
			if(wrapper!=null)return this.instance==wrapper.Value;

			Forms::HtmlDocument doc=obj as Forms::HtmlDocument;
			if(obj!=null)return this.instance==doc.DomDocument;

			Forms::HtmlWindow win=obj as Forms::HtmlWindow;
			if(win!=null)return this.instance==win.DomWindow;

			Forms::HtmlElement elem=obj as Forms::HtmlElement;
			if(elem!=null)return this.instance==elem.DomElement;

			Forms::HtmlHistory hist=obj as Forms::HtmlHistory;
			if(hist!=null)return this.instance==hist.DomHistory;

			return this.instance==obj;
		}
		public override int GetHashCode(){
			return this.instance.GetHashCode();
		}
		//============================================================
		//		�p����ŗ��p�o���郁���o
		//============================================================
		protected internal object this[string name]{
			get{
				return mshtml.InvokeMember(name,Ref::BindingFlags.GetProperty,null,this.instance,null);
			}
			set{
				MshtmlObject mshObj=value as MshtmlObject;
				if(mshObj!=null)value=mshObj.instance;
				mshtml.InvokeMember(name,Ref::BindingFlags.SetProperty,null,this.instance,new object[]{value});
			}
		}
		protected internal object Invoke(string name,params object[] args){
			// �����̕ϊ�
			object[] args2=new object[args.Length];
			for(int i=0;i<args.Length;i++){
				if(args[i] is MshtmlObject)
					args2[i]=((MshtmlObject)args[i]).instance;
				else
					args2[i]=args[i];
			}

			return mshtml.InvokeMember(name,Ref::BindingFlags.InvokeMethod,null,this.instance,args2);
		}
		protected internal static object GetProperty(object obj,string propName){
			return mshtml.InvokeMember(propName,Ref::BindingFlags.GetProperty,null,obj,null);
		}
		/// <summary>
		/// �w�肵���I�u�W�F�N�g�̎�ނ𔻒肵�āA�K�؂� MshtmlObject �𐶐����܂��B
		/// </summary>
		/// <param name="instance">�v�f���v�f�̏W����������Ȃ��I�u�W�F�N�g���w�肵�܂��B</param>
		/// <returns>�v�f���v�f�W���̃��b�p�C���X�^���X��Ԃ��܂��B</returns>
		protected static MshtmlObject GetElementOrCollection(object instance){
			if(instance==null)return null;

			try{
				if(GetProperty(instance,"nodeType")!=null)
					return DomNode.FromObj(instance);
			}catch{}

			return ElementCollection.FromObj(instance);
		}
		/// <summary>
		/// �w�肵���^�̃I�u�W�F�N�g�𐶐����܂��B
		/// </summary>
		/// <param name="t">��������I�u�W�F�N�g�̎�ނ��w�肵�܂��B</param>
		/// <param name="instance">�w�肵���I�u�W�F�N�g�̎�ނ̎��̂��w�肵�܂��B</param>
		/// <returns>instance �Ɏw�肵���I�u�W�F�N�g�����ɍ쐬�����C���X�^���X��Ԃ��܂��B</returns>
		protected static MshtmlObject FromObj(System.Type t,object instance){
			return (MshtmlObject)t.InvokeMember("FromObj",Ref::BindingFlags.InvokeMethod,null,null,new object[]{instance});
		}
		/// <summary>
		/// �w�肵���^�̃I�u�W�F�N�g�𐶐����܂��B
		/// </summary>
		/// <typeparam name="T">��������I�u�W�F�N�g�̎�ނ��w�肵�܂��B</typeparam>
		/// <param name="instance">�w�肵���I�u�W�F�N�g�̎�ނ̎��̂��w�肵�܂��B</param>
		/// <returns>instance �Ɏw�肵���I�u�W�F�N�g�����ɍ쐬�����C���X�^���X��Ԃ��܂��B</returns>
		protected static T FromObj<T>(object instance) where T:MshtmlObject{
			return (T)typeof(T).InvokeMember("FromObj",Ref::BindingFlags.InvokeMethod,null,null,new object[]{instance});
		}
	}
	/// <summary>
	/// length �y�� item(int) ��ێ����� Collection �̊�{�����o��񋟂��܂��B
	/// </summary>
	/// <typeparam name="T">�R���N�V�����ň����l�̌^���w�肵�܂��B</typeparam>
	public class CollectionBase<T>:MshtmlObject,Gen::IEnumerable<T>,System.Collections.IEnumerable{
		protected CollectionBase(object obj):base(obj){}
		protected virtual T CreateInstance(object obj){
			return (T)typeof(T).InvokeMember("FromObj",Ref::BindingFlags.InvokeMethod,null,null,new object[]{obj});
		}
		public static CollectionBase<T> FromObj(object instance){
			if(instance==null)return null;
			return new CollectionBase<T>(instance);
		}

		public T item(int index){
			return CreateInstance(this.Invoke("item",index));
		}
		public int length{
			get{return (int)this["length"];}
		}
		//--------------------------------------------------
		public T this[int index]{
			get{return this.item(index);}
		}
		public Gen::IEnumerator<T> GetEnumerator(){
			int len=this.length;
			for(int i=0;i<len;i++)yield return this.item(i);
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return this.GetEnumerator();
		}
	}
}
