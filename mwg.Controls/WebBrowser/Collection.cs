using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;
namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"
	//#define COLLECTION(x)		CollectionBase<x>
	//#define RAW(x)			x
	//================================================================
	//		cls:DomNodeCollection
	//================================================================
	// IHTMLDOMChildrenCollection
	//#CLASS_NEW<DomNodeCollection,COLLECTION(DomNode)>
	//#>>delete
	public partial class DomNodeCollection:Gen::IEnumerable<DomNode>,System.Collections.IEnumerable{
		protected override DomNode CreateInstance(object obj){
			return DomNode.FromObj(obj);
		}
		public DomNodeCollection urns(string behaviorUrn){
			return DomNodeCollection.FromObj(this.Invoke("urns",behaviorUrn));
		}
	}
	//#<<delete
	//================================================================
	//		cls:NamedCollection
	//================================================================
	//#>>delete
	public partial class NamedCollection<T>:CollectionBase<T> where T:MshtmlObject{
		protected NamedCollection(object inst):base(inst){}
		public static new NamedCollection<T> FromObj(object obj){
			return new NamedCollection<T>(obj);
		}
		//private static readonly bool isMshtmlObject=typeof(T).IsSubclassOf(typeof(MshtmlObject));
		//------------------------------------------------------------
		public MshtmlObject item(string name) {
			return this.namedItem(name);
		}
		public T item(string name,int index){
			return FromObj<T>(this.Invoke("item",name,index));
		}
		public MshtmlObject namedItem(string name){
			object ret=this.Invoke("namedItem",name);
			try{
				if(GetProperty(ret,"length")!=null)
					return FromObj(this.GetType(),ret);
			}catch{}

			try{
				return FromObj(typeof(T),ret);
			}catch{}
			
			return new MshtmlObject(ret);
		}
		//------------------------------------------------------------
		public T this[string name,int index]{
			get{return this.item(name,index);}
		}
	}
	//#<<delete
	//================================================================
	//		cls:ElementCollection<T:Element>
	//================================================================
	// IHTMLElementCollection
	// IHTMLElementCollection2
	// IHTMLElementCollection3
	//#>>delete
	public partial class ElementCollection<T>:NamedCollection<T> where T:Element{
		protected ElementCollection(object instance):base(instance){}
		public static new ElementCollection<T> FromObj(object obj){
			if(obj==null)return null;
			return new ElementCollection<T>(obj);
		}
		protected override T CreateInstance(object obj){
			return (T)Element.FromObj(obj);
		}
		//------------------------------------------------------------
		// IHTMLElementCollection
		//------------------------------------------------------------
		public string toString(){
			return (string)this.Invoke("toString");
		}
		// tags(MshtmlObject)
		public ElementCollection<T> tags(string tagName){
			return ElementCollection<T>.FromObj(this.Invoke("tags",tagName));
		}
		//------------------------------------------------------------
		// IHTMLElementCollection2
		//------------------------------------------------------------
		public ElementCollection<T> urns(string behaviorUrn){
			return ElementCollection<T>.FromObj(this.Invoke("urns",behaviorUrn));
		}
		//------------------------------------------------------------
		// IHTMLElementCollection3
		//------------------------------------------------------------
		/// <summary>
		/// 指定した名前を持つ要素を取得します。
		/// </summary>
		/// <param name="name">検索する要素に付けられた名前を指定します。</param>
		/// <returns>
		/// 指定した名前の要素が一つの場合にはその要素を返します。
		/// 指定した名前の要素が複数ある場合にはその集合を返します。
		/// </returns>
		public new MshtmlObject namedItem(string name){
			return GetElementOrCollection(this.Invoke("namedItem",name));
		}
	}
	//#<<delete
	//================================================================
	//		cls:ElementCollection
	//================================================================
	// IHTMLElementCollection
	// IHTMLElementCollection2
	// IHTMLElementCollection3
	//#CLASS_NEW<ElementCollection,RAW("#ElementCollection<Element>#")>
	//#>>delete
	public partial class ElementCollection:ElementCollection<Element>{}
	//#<<delete
	//================================================================
	//		cls:AreaElementCollection
	//================================================================
	//#CLASS_NEW<AreaElementCollection,RAW("#ElementCollection<AreaElement>#")>
	//#>>delete
	public partial class AreaElementCollection{
		public void add(AreaElement elem,object before){
			this.Invoke("add",elem,before);
		}
		public void remove(int index){
			this.Invoke("remove",index);
		}
	}
	//#<<delete
	//================================================================
	//		cls:BehaviorUrns
	//================================================================
	//#CLASS_NEW<BehaviorUrns,COLLECTION(string)>
	//#>>delete
	public partial class BehaviorUrns{
		public BehaviorUrns item(string name){
			return BehaviorUrns.FromObj(this.Invoke("item",name));
		}
		public new BehaviorUrns this[string name]{
			get{return this.item(name);}
		}
	}
	//#<<delete
}
