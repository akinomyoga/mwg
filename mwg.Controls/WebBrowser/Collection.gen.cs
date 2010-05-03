/*
	このソースコードは [afh.Design.dll] afh.Design.TemplateProcessor によって自動的に生成された物です。
	このソースコードを変更しても、このソースコードの元になったファイルを変更しないと変更は適用されません。

	This source code was generated automatically by a file-generator, '[afh.Design.dll] afh.Design.TemplateProcessor'.
	Changes to this source code may not be applied to the binary file, which will cause inconsistency of the whole project.
	If you want to modify any logics in this file, you should change THE SOURCE OF THIS FILE. 
*/
using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;
namespace mwg.Controls.WebBrowser{
//#define RAW(x)	x


		//#define COLLECTION(x)		CollectionBase<x>
		//#define NAMED_COLLEC(x)	NamedCollection<x>

	//#define CollectionBase<x>		CollectionBase<x>
	//#define x			x
	//================================================================
	//		cls:DomNodeCollection
	//================================================================
	// IHTMLDOMChildrenCollection
	public partial class DomNodeCollection:CollectionBase<DomNode>{
		protected DomNodeCollection(object instance):base(instance){}
		public static new DomNodeCollection FromObj(object obj){
			if(obj==null)return null;
			return new DomNodeCollection(obj);
		}
	}
	//================================================================
	//		cls:NamedCollection
	//================================================================

	//================================================================
	//		cls:ElementCollection<T:Element>
	//================================================================
	// IHTMLElementCollection
	// IHTMLElementCollection2
	// IHTMLElementCollection3

	//================================================================
	//		cls:ElementCollection
	//================================================================
	// IHTMLElementCollection
	// IHTMLElementCollection2
	// IHTMLElementCollection3
	public partial class ElementCollection:ElementCollection<Element>{
		protected ElementCollection(object instance):base(instance){}
		public static new ElementCollection FromObj(object obj){
			if(obj==null)return null;
			return new ElementCollection(obj);
		}
	}
	//================================================================
	//		cls:AreaElementCollection
	//================================================================
	public partial class AreaElementCollection:ElementCollection<AreaElement>{
		protected AreaElementCollection(object instance):base(instance){}
		public static new AreaElementCollection FromObj(object obj){
			if(obj==null)return null;
			return new AreaElementCollection(obj);
		}
	}
	//================================================================
	//		cls:BehaviorUrns
	//================================================================
	public partial class BehaviorUrns:CollectionBase<string>{
		protected BehaviorUrns(object instance):base(instance){}
		public static new BehaviorUrns FromObj(object obj){
			if(obj==null)return null;
			return new BehaviorUrns(obj);
		}
	}
}
