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


	// IHTMLDataTransfer
	public partial class DataTransfer:MshtmlObject{
		protected DataTransfer(object instance):base(instance){}
		public static DataTransfer FromObj(object obj){
			if(obj==null)return null;
			return new DataTransfer(obj);
		}
	}
	public partial class DataTransfer{

		public string dropEffect{
			get{return (string)base["dropEffect"];}
			set{base["dropEffect"]=value;}
		}
		public string effectAllowed{
			get{return (string)base["effectAllowed"];}
			set{base["effectAllowed"]=value;}
		}
	}

	// IHTMLRect
	public partial class Rect:MshtmlObject{
		protected Rect(object instance):base(instance){}
		public static Rect FromObj(object obj){
			if(obj==null)return null;
			return new Rect(obj);
		}
	}
	public partial class Rect{
		public int bottom{
			get{return (int)base["bottom"];}
			set{base["bottom"]=value;}
		}
		public int left{
			get{return (int)base["left"];}
			set{base["left"]=value;}
		}
		public int right{
			get{return (int)base["right"];}
			set{base["right"]=value;}
		}
		public int top{
			get{return (int)base["top"];}
			set{base["top"]=value;}
		}
	}


	public partial class TextRange:MshtmlObject{
		protected TextRange(object instance):base(instance){}
		public static TextRange FromObj(object obj){
			if(obj==null)return null;
			return new TextRange(obj);
		}
	}
	public partial class TextRange{
		//------------------------------------------------------------
		//		IHTMLTxtRange
		//------------------------------------------------------------

		public string htmlText{
			get{return (string)base["htmlText"];}
		}
		public string text{
			get{return (string)base["text"];}
			set{base["text"]=value;}
		}
		//------------------------------------------------------------
		//		IHTMLTextRangeMetrics
		//------------------------------------------------------------
		public int boudingHeight{
			get{return (int)base["boudingHeight"];}
		}
		public int boudingLeft{
			get{return (int)base["boudingLeft"];}
		}
		public int boudingTop{
			get{return (int)base["boudingTop"];}
		}
		public int boudingWidth{
			get{return (int)base["boudingWidth"];}
		}
		public int offsetLeft{
			get{return (int)base["offsetLeft"];}
		}
		public int offsetTop{
			get{return (int)base["offsetTop"];}
		}
		//------------------------------------------------------------
		//		IHTMLTextRangeMetrics2
		//------------------------------------------------------------

	}

	public partial class Selection:MshtmlObject{
		protected Selection(object instance):base(instance){}
		public static Selection FromObj(object obj){
			if(obj==null)return null;
			return new Selection(obj);
		}
	}
	public partial class Selection{
		//------------------------------------------------------------
		//		IHTMLSelectionObject
		//------------------------------------------------------------

		public string type{
			get{return (string)base["type"];}
		}
		//------------------------------------------------------------
		//		IHTMLSelectionObject2
		//------------------------------------------------------------

		public string typeDetail{
			get{return (string)base["typeDetail"];}
		}
	}

	public partial class DomImplementation:MshtmlObject{
		protected DomImplementation(object instance):base(instance){}
		public static DomImplementation FromObj(object obj){
			if(obj==null)return null;
			return new DomImplementation(obj);
		}
	}
	public partial class DomImplementation{

	}

	public partial class ImageElementFactory:MshtmlObject{
		protected ImageElementFactory(object instance):base(instance){}
		public static ImageElementFactory FromObj(object obj){
			if(obj==null)return null;
			return new ImageElementFactory(obj);
		}
	}
	public partial class ImageElementFactory{

	}

	public partial class OptionElementFactory:MshtmlObject{
		protected OptionElementFactory(object instance):base(instance){}
		public static OptionElementFactory FromObj(object obj){
			if(obj==null)return null;
			return new OptionElementFactory(obj);
		}
	}
	public partial class OptionElementFactory{

	}

	/// <summary>
	/// NN との互換性の為の、空のクラスです。
	/// </summary>
	public partial class MimeTypes:MshtmlObject{
		protected MimeTypes(object instance):base(instance){}
		public static MimeTypes FromObj(object obj){
			if(obj==null)return null;
			return new MimeTypes(obj);
		}
	}
	public partial class MimeTypes{
		public int length{
			get{return (int)base["length"];}
		}
	}

	/// <summary>
	/// NN との互換性の為の、空のクラスです。
	/// </summary>
	public partial class Plugins:MshtmlObject{
		protected Plugins(object instance):base(instance){}
		public static Plugins FromObj(object obj){
			if(obj==null)return null;
			return new Plugins(obj);
		}
	}
	public partial class Plugins{
		public int length{
			get{return (int)base["length"];}
		}
	}

	// HTMLDefaults ?
	// HTMLDialog ?
	// HTMLDivPosition ? ← DivElement から noWrap を除いた物?
	// HtmlDlgSafeHelper
	// HTMLImageElementFactory ?
	// HTMLNamespace ?
	// HTMLNamespaceCollection ?
	// HTMLOptionElementFactory ?
	// HTMLPopup
	// HTMLStyleFontFace ?
	// HTMLWindowProxy ?
}