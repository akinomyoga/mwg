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


	public partial class AElement:DatabindingElement{
		protected AElement(object instance):base(instance){}
		public static new AElement FromObj(object obj){
			if(obj==null)return null;
			return new AElement(obj);
		}
	}
	public partial class AElement{
		//------------------------------------------------------------
		//	IHTMLAnchorElement
		//------------------------------------------------------------
		// override #PROP<string,accessKey>
		public string hash{
			get{return (string)base["hash"];}
			set{base["hash"]=value;}
		}
		public string host{
			get{return (string)base["host"];}
			set{base["host"]=value;}
		}
		public string hostname{
			get{return (string)base["hostname"];}
			set{base["hostname"]=value;}
		}
		public string href{
			get{return (string)base["href"];}
			set{base["href"]=value;}
		}
		public string Methods{
			get{return (string)base["Methods"];}
			set{base["Methods"]=value;}
		}
		public string mimeType{
			get{return (string)base["mimeType"];}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public string nameProp{
			get{return (string)base["nameProp"];}
		}
		public string pathname{
			get{return (string)base["pathname"];}
			set{base["pathname"]=value;}
		}
		public string port{
			get{return (string)base["port"];}
			set{base["port"]=value;}
		}
		public string protocol{
			get{return (string)base["protocol"];}
			set{base["protocol"]=value;}
		}
		public string protocolLong{
			get{return (string)base["protocolLong"];}
		}
		public string rel{
			get{return (string)base["rel"];}
			set{base["rel"]=value;}
		}
		public string rev{
			get{return (string)base["rev"];}
			set{base["rev"]=value;}
		}
		public string search{
			get{return (string)base["search"];}
			set{base["search"]=value;}
		}
		// override #PROP<short,tabIndex>
		public string target{
			get{return (string)base["target"];}
			set{base["target"]=value;}
		}
		public string urn{
			get{return (string)base["urn"];}
			set{base["urn"]=value;}
		}
		//------------------------------------------------------------
		//	IHTMLAnchorElement2
		//------------------------------------------------------------
		public string charset{
			get{return (string)base["charset"];}
			set{base["charset"]=value;}
		}
		public string coords{
			get{return (string)base["coords"];}
			set{base["coords"]=value;}
		}
		public string hreflang{
			get{return (string)base["hreflang"];}
			set{base["hreflang"]=value;}
		}
		public string shape{
			get{return (string)base["shape"];}
			set{base["shape"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
	}
	public partial class AreaElement:Element{
		protected AreaElement(object instance):base(instance){}
		public static new AreaElement FromObj(object obj){
			if(obj==null)return null;
			return new AreaElement(obj);
		}
	}
	public partial class AreaElement{
		//------------------------------------------------------------
		//	IHTMLAreaElement
		//------------------------------------------------------------
		public string alt{
			get{return (string)base["alt"];}
			set{base["alt"]=value;}
		}
		public string coords{
			get{return (string)base["coords"];}
			set{base["coords"]=value;}
		}
		public string hash{
			get{return (string)base["hash"];}
			set{base["hash"]=value;}
		}
		public string host{
			get{return (string)base["host"];}
			set{base["host"]=value;}
		}
		public string hostname{
			get{return (string)base["hostname"];}
			set{base["hostname"]=value;}
		}
		public string href{
			get{return (string)base["href"];}
			set{base["href"]=value;}
		}
		public bool noHref{
			get{return (bool)base["noHref"];}
			set{base["noHref"]=value;}
		}
		public string pathname{
			get{return (string)base["pathname"];}
			set{base["pathname"]=value;}
		}
		public string port{
			get{return (string)base["port"];}
			set{base["port"]=value;}
		}
		public string protocol{
			get{return (string)base["protocol"];}
			set{base["protocol"]=value;}
		}
		public string search{
			get{return (string)base["search"];}
			set{base["search"]=value;}
		}
		public string shape{
			get{return (string)base["shape"];}
			set{base["shape"]=value;}
		}
		public string target{
			get{return (string)base["target"];}
			set{base["target"]=value;}
		}
	}
	public partial class BaseElement:Element{
		protected BaseElement(object instance):base(instance){}
		public static new BaseElement FromObj(object obj){
			if(obj==null)return null;
			return new BaseElement(obj);
		}
	}
	public partial class BaseElement{
		//------------------------------------------------------------
		//	IHTMLBaseElement
		//------------------------------------------------------------
		public string href{
			get{return (string)base["href"];}
			set{base["href"]=value;}
		}
		public string target{
			get{return (string)base["target"];}
			set{base["target"]=value;}
		}
	}
	public partial class BaseFontElement:Element{
		protected BaseFontElement(object instance):base(instance){}
		public static new BaseFontElement FromObj(object obj){
			if(obj==null)return null;
			return new BaseFontElement(obj);
		}
	}
	public partial class BaseFontElement{
		//------------------------------------------------------------
		//	IHTMLBaseFontElement
		//------------------------------------------------------------
		public object color{
			get{return (object)base["color"];}
			set{base["color"]=value;}
		}
		public string face{
			get{return (string)base["face"];}
			set{base["face"]=value;}
		}
		public int size{
			get{return (int)base["size"];}
			set{base["size"]=value;}
		}
	}
	public partial class BgsoundElement:Element{
		protected BgsoundElement(object instance):base(instance){}
		public static new BgsoundElement FromObj(object obj){
			if(obj==null)return null;
			return new BgsoundElement(obj);
		}
	}
	public partial class BgsoundElement{
		//------------------------------------------------------------
		//	IHTMLBGsoundElement
		//------------------------------------------------------------
		public object balance{
			get{return (object)base["balance"];}
			set{base["balance"]=value;}
		}
		public object loop{
			get{return (object)base["loop"];}
			set{base["loop"]=value;}
		}
		public string src{
			get{return (string)base["src"];}
			set{base["src"]=value;}
		}
		public object volume{
			get{return (object)base["volume"];}
			set{base["volume"]=value;}
		}
	}
	public partial class BlockElement:Element{
		protected BlockElement(object instance):base(instance){}
		public static new BlockElement FromObj(object obj){
			if(obj==null)return null;
			return new BlockElement(obj);
		}
	}
	public partial class BlockElement{
		//------------------------------------------------------------
		//	IHTMLBlockElement
		//------------------------------------------------------------
		public string clear{
			get{return (string)base["clear"];}
			set{base["clear"]=value;}
		}
		//------------------------------------------------------------
		//	IHTMLBlockElement2
		//------------------------------------------------------------
		public string cite{
			get{return (string)base["cite"];}
			set{base["cite"]=value;}
		}
		public string width{
			get{return (string)base["width"];}
			set{base["width"]=value;}
		}
	}

	public partial class BodyElement:Element{
		protected BodyElement(object instance):base(instance){}
		public static new BodyElement FromObj(object obj){
			if(obj==null)return null;
			return new BodyElement(obj);
		}
	}
	/// <summary>
	/// Body 要素を表現するクラスです。
	/// </summary>
	public partial class BodyElement{

		//--------------------------------------------------
		//		IHTMLBodyElement
		//--------------------------------------------------
		public object aLink{
			get{return (object)base["aLink"];}
			set{base["aLink"]=value;}
		}
		public object link{
			get{return (object)base["link"];}
			set{base["link"]=value;}
		}
		public object vLink{
			get{return (object)base["vLink"];}
			set{base["vLink"]=value;}
		}
		public string background{
			get{return (string)base["background"];}
			set{base["background"]=value;}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public string bgProperties{
			get{return (string)base["bgProperties"];}
			set{base["bgProperties"]=value;}
		}
		public object bottomMargin{
			get{return (object)base["bottomMargin"];}
			set{base["bottomMargin"]=value;}
		}
		public object leftMargin{
			get{return (object)base["leftMargin"];}
			set{base["leftMargin"]=value;}
		}
		public object rightMargin{
			get{return (object)base["rightMargin"];}
			set{base["rightMargin"]=value;}
		}
		public object topMargin{
			get{return (object)base["topMargin"];}
			set{base["topMargin"]=value;}
		}
		public bool noWrap{
			get{return (bool)base["noWrap"];}
			set{base["noWrap"]=value;}
		}
		public string scroll{
			get{return (string)base["scroll"];}
			set{base["scroll"]=value;}
		}
		public string text{
			get{return (string)base["text"];}
			set{base["text"]=value;}
		}
		public event EHVoid onbeforeunload{
			add{attachEvent("onbeforeunload",value);}
			remove{detachEvent("onbeforeunload",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		public event EHVoid onunload{
			add{attachEvent("onunload",value);}
			remove{detachEvent("onunload",value);}
		}
		//--------------------------------------------------
		//		IHTMLBodyElement2
		//--------------------------------------------------
		public event EHVoid onafterprint{
			add{attachEvent("onafterprint",value);}
			remove{detachEvent("onafterprint",value);}
		}
		public event EHVoid onbeforeprint{
			add{attachEvent("onbeforeprint",value);}
			remove{detachEvent("onbeforeprint",value);}
		}
		//--------------------------------------------------
		//		IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
	}

	public partial class BrElement:Element{
		protected BrElement(object instance):base(instance){}
		public static new BrElement FromObj(object obj){
			if(obj==null)return null;
			return new BrElement(obj);
		}
	}
	public partial class BrElement{
		public string clear{
			get{return (string)base["clear"];}
			set{base["clear"]=value;}
		}
	}

	public partial class ButtonElement:DatabindingElement{
		protected ButtonElement(object instance):base(instance){}
		public static new ButtonElement FromObj(object obj){
			if(obj==null)return null;
			return new ButtonElement(obj);
		}
	}
	public partial class ButtonElement{
		//------------------------------------------------------------
		//	IHTMLButtonElement
		//------------------------------------------------------------

		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public object status{
			get{return (object)base["status"];}
			set{base["status"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}
	}

	public partial class CommentElement:Element{
		protected CommentElement(object instance):base(instance){}
		public static new CommentElement FromObj(object obj){
			if(obj==null)return null;
			return new CommentElement(obj);
		}
	}
	public partial class CommentElement{
		//------------------------------------------------------------
		//	IHTMLCommentElement
		//------------------------------------------------------------
		public int atomic{
			get{return (int)base["atomic"];}
			set{base["atomic"]=value;}
		}
		public string text{
			get{return (string)base["text"];}
			set{base["text"]=value;}
		}
		//------------------------------------------------------------
		//	IHTMLCommentElement2
		//------------------------------------------------------------
		public string data{
			get{return (string)base["data"];}
			set{base["data"]=value;}
		}
		public int length{
			get{return (int)base["length"];}
		}
	}

	public partial class DdElement:Element{
		protected DdElement(object instance):base(instance){}
		public static new DdElement FromObj(object obj){
			if(obj==null)return null;
			return new DdElement(obj);
		}
	}
	public partial class DdElement{
		public bool noWrap{
			get{return (bool)base["noWrap"];}
			set{base["noWrap"]=value;}
		}
	}

	public partial class DivElement:DatabindingElement{
		protected DivElement(object instance):base(instance){}
		public static new DivElement FromObj(object obj){
			if(obj==null)return null;
			return new DivElement(obj);
		}
	}
	public partial class DivElement{
		//------------------------------------------------------------
		//	IHTMLDivElement
		//------------------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public bool noWrap{
			get{return (bool)base["noWrap"];}
			set{base["noWrap"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
	}

	public partial class DlElement:Element{
		protected DlElement(object instance):base(instance){}
		public static new DlElement FromObj(object obj){
			if(obj==null)return null;
			return new DlElement(obj);
		}
	}
	public partial class DlElement{
		public bool compact{
			get{return (bool)base["compact"];}
			set{base["compact"]=value;}
		}
	}

	public partial class DtElement:Element{
		protected DtElement(object instance):base(instance){}
		public static new DtElement FromObj(object obj){
			if(obj==null)return null;
			return new DtElement(obj);
		}
	}
	public partial class DtElement{
		public bool noWrap{
			get{return (bool)base["noWrap"];}
			set{base["noWrap"]=value;}
		}
	}

	public partial class EmbedElement:Element{
		protected EmbedElement(object instance):base(instance){}
		public static new EmbedElement FromObj(object obj){
			if(obj==null)return null;
			return new EmbedElement(obj);
		}
	}
	public partial class EmbedElement{
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		public string hidden{
			get{return (string)base["hidden"];}
			set{base["hidden"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public string palette{
			get{return (string)base["palette"];}
		}
		public string pluginspage{
			get{return (string)base["pluginspage"];}
		}
		public string src{
			get{return (string)base["src"];}
			set{base["src"]=value;}
		}
		public string units{
			get{return (string)base["units"];}
			set{base["units"]=value;}
		}
	}

	public partial class FieldsetElement:Element{
		protected FieldsetElement(object instance):base(instance){}
		public static new FieldsetElement FromObj(object obj){
			if(obj==null)return null;
			return new FieldsetElement(obj);
		}
	}
	public partial class FieldsetElement{
		//------------------------------------------------------------
		//	IHTMLFieldsetElement
		//------------------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		//------------------------------------------------------------
		//	IHTMLFieldsetElement2
		//------------------------------------------------------------
		public FormElement form{
			get{return (FormElement)base["form"];}
			set{base["form"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
	}

	public partial class FontElement:Element{
		protected FontElement(object instance):base(instance){}
		public static new FontElement FromObj(object obj){
			if(obj==null)return null;
			return new FontElement(obj);
		}
	}
	public partial class FontElement{
		//--------------------------------------------------
		//	IHTMLFontElement
		//--------------------------------------------------
		public object color{
			get{return (object)base["color"];}
			set{base["color"]=value;}
		}
		public string face{
			get{return (string)base["face"];}
			set{base["face"]=value;}
		}
		public object size{
			get{return (object)base["size"];}
			set{base["size"]=value;}
		}
	}

	public partial class FormElement:Element{
		protected FormElement(object instance):base(instance){}
		public static new FormElement FromObj(object obj){
			if(obj==null)return null;
			return new FormElement(obj);
		}
	}
	public partial class FormElement:Gen::IEnumerable<Element>{
		//--------------------------------------------------
		//	IHTMLFormElement
		//--------------------------------------------------

		public int length{
			get{return (int)base["length"];}
			set{base["length"]=value;}
		}
		public string action{
			get{return (string)base["action"];}
			set{base["action"]=value;}
		}
		public ElementCollection elements{
			get{return ElementCollection.FromObj(base["elements"]);}
		}
		public string encoding{
			get{return (string)base["encoding"];}
			set{base["encoding"]=value;}
		}
		public string method{
			get{return (string)base["method"];}
			set{base["method"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public string target{
			get{return (string)base["target"];}
			set{base["target"]=value;}
		}
		public event EHCancel onreset{
			add{attachEvent("onreset",value);}
			remove{detachEvent("onreset",value);}
		}
		public event EHCancel onsubmit{
			add{attachEvent("onsubmit",value);}
			remove{detachEvent("onsubmit",value);}
		}
		//--------------------------------------------------
		//	IHTMLFormElement2
		//--------------------------------------------------

		public string acceptCharset{
			get{return (string)base["acceptCharset"];}
			set{base["acceptCharset"]=value;}
		}
		//------------------------------------------------------------
		// IHTMLFormElement3
		//------------------------------------------------------------

		//--------------------------------------------------
		//	IHTMLSubmitData
		//--------------------------------------------------

	}

	// IHTMLFrameBase
	public partial class FrameBase:Element{
		protected FrameBase(object instance):base(instance){}
		public static new FrameBase FromObj(object obj){
			if(obj==null)return null;
			return new FrameBase(obj);
		}
	}
	public partial class FrameBase{
		//--------------------------------------------------
		//	IHTMLFrameBase
		//--------------------------------------------------
		public object border{
			get{return (object)base["border"];}
			set{base["border"]=value;}
		}
		public string frameBorder{
			get{return (string)base["frameBorder"];}
			set{base["frameBorder"]=value;}
		}
		public object frameSpacing{
			get{return (object)base["frameSpacing"];}
			set{base["frameSpacing"]=value;}
		}
		public object marginHeight{
			get{return (object)base["marginHeight"];}
			set{base["marginHeight"]=value;}
		}
		public object marginWidth{
			get{return (object)base["marginWidth"];}
			set{base["marginWidth"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public bool noResize{
			get{return (bool)base["noResize"];}
			set{base["noResize"]=value;}
		}
		public string scrolling{
			get{return (string)base["scrolling"];}
			set{base["scrolling"]=value;}
		}
		public string src{
			get{return (string)base["src"];}
			set{base["src"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLFrameBase2
		//--------------------------------------------------
		public bool allowTransparency{
			get{return (bool)base["allowTransparency"];}
			set{base["allowTransparency"]=value;}
		}
		public Window contentWindow{
			get{return Window.FromObj(base["contentWindow"]);}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		//--------------------------------------------------
		//	IHTMLFrameBase3
		//--------------------------------------------------
		public string longDesc{
			get{return (string)base["longDesc"];}
			set{base["longDesc"]=value;}
		}
	}

	public partial class FrameElement:FrameBase{
		protected FrameElement(object instance):base(instance){}
		public static new FrameElement FromObj(object obj){
			if(obj==null)return null;
			return new FrameElement(obj);
		}
	}
	public partial class FrameElement{
		//------------------------------------------------------------
		//	IHTMLDatabinding
		//------------------------------------------------------------
		public string dataFld{
			get{return (string)base["dataFld"];}
			set{base["dataFld"]=value;}
		}
		public string dataFormatAs{
			get{return (string)base["dataFormatAs"];}
			set{base["dataFormatAs"]=value;}
		}
		public string dataSrc{
			get{return (string)base["dataSrc"];}
			set{base["dataSrc"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLFrameElement
		//--------------------------------------------------
		public object borderColor{
			get{return (object)base["borderColor"];}
			set{base["borderColor"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLFrameElement
		//--------------------------------------------------
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
	}

	public partial class FramesetElement:Element{
		protected FramesetElement(object instance):base(instance){}
		public static new FramesetElement FromObj(object obj){
			if(obj==null)return null;
			return new FramesetElement(obj);
		}
	}
	public partial class FramesetElement{
		//--------------------------------------------------
		//	IHTMLFrameSetElement
		//--------------------------------------------------
		public object border{
			get{return (object)base["border"];}
			set{base["border"]=value;}
		}
		public object borderColor{
			get{return (object)base["borderColor"];}
			set{base["borderColor"]=value;}
		}
		public string cols{
			get{return (string)base["cols"];}
			set{base["cols"]=value;}
		}
		public string rows{
			get{return (string)base["rows"];}
			set{base["rows"]=value;}
		}
		public string frameBorder{
			get{return (string)base["frameBorder"];}
			set{base["frameBorder"]=value;}
		}
		public string frameSpacing{
			get{return (string)base["frameSpacing"];}
			set{base["frameSpacing"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public event EHVoid onbeforeunload{
			add{attachEvent("onbeforeunload",value);}
			remove{detachEvent("onbeforeunload",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		public event EHVoid onunload{
			add{attachEvent("onunload",value);}
			remove{detachEvent("onunload",value);}
		}
		//--------------------------------------------------
		//	IHTMLFrameSetElement2
		//--------------------------------------------------
		public event EHVoid onafterprint{
			add{attachEvent("onafterprint",value);}
			remove{detachEvent("onafterprint",value);}
		}
		public event EHVoid onbeforeprint{
			add{attachEvent("onbeforeprint",value);}
			remove{detachEvent("onbeforeprint",value);}
		}
	}

	public partial class GenericElement:Element{
		protected GenericElement(object instance):base(instance){}
		public static new GenericElement FromObj(object obj){
			if(obj==null)return null;
			return new GenericElement(obj);
		}
	}
	public partial class GenericElement{
		//--------------------------------------------------
		//	IHTMLGenericElement
		//--------------------------------------------------
		public object recordset{
			get{return (object)base["recordset"];}
		}
	}

	public partial class HeadElement:Element{
		protected HeadElement(object instance):base(instance){}
		public static new HeadElement FromObj(object obj){
			if(obj==null)return null;
			return new HeadElement(obj);
		}
	}
	public partial class HeadElement{
		//--------------------------------------------------
		//	IHTMLHeadElement
		//--------------------------------------------------
		public string profile{
			get{return (string)base["profile"];}
			set{base["profile"]=value;}
		}
	}

	public partial class HnElement:Element{
		protected HnElement(object instance):base(instance){}
		public static new HnElement FromObj(object obj){
			if(obj==null)return null;
			return new HnElement(obj);
		}
	}
	public partial class HnElement{
		//--------------------------------------------------
		//	IHTMLBlockElement
		//--------------------------------------------------
		public string clear{
			get{return (string)base["clear"];}
			set{base["clear"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLHeaderElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
	}

	public partial class HrElement:Element{
		protected HrElement(object instance):base(instance){}
		public static new HrElement FromObj(object obj){
			if(obj==null)return null;
			return new HrElement(obj);
		}
	}
	public partial class HrElement{
		//--------------------------------------------------
		//	IHTMLHRElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public object color{
			get{return (object)base["color"];}
			set{base["color"]=value;}
		}
		public bool noShade{
			get{return (bool)base["noShade"];}
			set{base["noShade"]=value;}
		}
		public object size{
			get{return (object)base["size"];}
			set{base["size"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
	}

	public partial class HtmlElement:Element{
		protected HtmlElement(object instance):base(instance){}
		public static new HtmlElement FromObj(object obj){
			if(obj==null)return null;
			return new HtmlElement(obj);
		}
	}
	public partial class HtmlElement{
		//--------------------------------------------------
		//	IHTMLHtmlElement
		//--------------------------------------------------
		public string version{
			get{return (string)base["version"];}
			set{base["version"]=value;}
		}
	}

	public partial class IframeElement:FrameBase{
		protected IframeElement(object instance):base(instance){}
		public static new IframeElement FromObj(object obj){
			if(obj==null)return null;
			return new IframeElement(obj);
		}
	}
	public partial class IframeElement{
		//------------------------------------------------------------
		//	IHTMLDatabinding
		//------------------------------------------------------------
		public string dataFld{
			get{return (string)base["dataFld"];}
			set{base["dataFld"]=value;}
		}
		public string dataFormatAs{
			get{return (string)base["dataFormatAs"];}
			set{base["dataFormatAs"]=value;}
		}
		public string dataSrc{
			get{return (string)base["dataSrc"];}
			set{base["dataSrc"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLIFrameElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public int hspace{
			get{return (int)base["hspace"];}
			set{base["hspace"]=value;}
		}
		public int vspace{
			get{return (int)base["vspace"];}
			set{base["vspace"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLIFrameElement2
		//--------------------------------------------------
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
	}

	public partial class ImgElement:DatabindingElement{
		protected ImgElement(object instance):base(instance){}
		public static new ImgElement FromObj(object obj){
			if(obj==null)return null;
			return new ImgElement(obj);
		}
	}
	public partial class ImgElement{
		//--------------------------------------------------
		//	IHTMLImgElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string alt{
			get{return (string)base["alt"];}
			set{base["alt"]=value;}
		}
		public object border{
			get{return (object)base["border"];}
			set{base["border"]=value;}
		}
		public bool complete{
			get{return (bool)base["complete"];}
		}
		public string dynsrc{
			get{return (string)base["dynsrc"];}
			set{base["dynsrc"]=value;}
		}
		public string fileCreatedDate{
			get{return (string)base["fileCreatedDate"];}
		}
		public string fileModifiedDate{
			get{return (string)base["fileModifiedDate"];}
		}
		public string fileSize{
			get{return (string)base["fileSize"];}
		}
		public string fileUpdatedDate{
			get{return (string)base["fileUpdatedDate"];}
		}
		public int height{
			get{return (int)base["height"];}
			set{base["height"]=value;}
		}
		public int width{
			get{return (int)base["width"];}
			set{base["width"]=value;}
		}
		public int hspace{
			get{return (int)base["hspace"];}
			set{base["hspace"]=value;}
		}
		public int vspace{
			get{return (int)base["vspace"];}
			set{base["vspace"]=value;}
		}
		public string href{
			get{return (string)base["href"];}
		}
		public bool isMap{
			get{return (bool)base["isMap"];}
			set{base["isMap"]=value;}
		}
		public object loop{
			get{return (object)base["loop"];}
			set{base["loop"]=value;}
		}
		public string lowsrc{
			get{return (string)base["lowsrc"];}
			set{base["lowsrc"]=value;}
		}
		public string mimeType{
			get{return (string)base["mimeType"];}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public string nameProp{
			get{return (string)base["nameProp"];}
		}
		public string protocol{
			get{return (string)base["protocol"];}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public string src{
			get{return (string)base["src"];}
			set{base["src"]=value;}
		}
		public string Start{
			get{return (string)base["Start"];}
			set{base["Start"]=value;}
		}
		public string useMap{
			get{return (string)base["useMap"];}
			set{base["useMap"]=value;}
		}
		public string vrml{
			get{return (string)base["vrml"];}
			set{base["vrml"]=value;}
		}
		public event EHVoid onabort{
			add{attachEvent("onabort",value);}
			remove{detachEvent("onabort",value);}
		}
		public event EHVoid onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		//--------------------------------------------------
		//	IHTMLIFrameElement2
		//--------------------------------------------------
		public string longDesc{
			get{return (string)base["longDesc"];}
			set{base["longDesc"]=value;}
		}
	}

	public partial class InputElement:DatabindingElement{
		protected InputElement(object instance):base(instance){}
		public static new InputElement FromObj(object obj){
			if(obj==null)return null;
			return new InputElement(obj);
		}
	}
	public partial class InputElement{
		//--------------------------------------------------
		//	IHTMLInputElement
		//	IHTMLInput***Element
		//--------------------------------------------------

		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string alt{
			get{return (string)base["alt"];}
			set{base["alt"]=value;}
		}
		public object border{
			get{return (object)base["border"];}
			set{base["border"]=value;}
		}
		public bool @checked{
			get{return (bool)base["@checked"];}
			set{base["@checked"]=value;}
		}
		public bool complete{
			get{return (bool)base["complete"];}
		}
		public bool defaultChecked{
			get{return (bool)base["defaultChecked"];}
			set{base["defaultChecked"]=value;}
		}
		public string defaultValue{
			get{return (string)base["defaultValue"];}
			set{base["defaultValue"]=value;}
		}
		// #PROP<bool,disabled>
		public string dynsrc{
			get{return (string)base["dynsrc"];}
			set{base["dynsrc"]=value;}
		}
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public int height{
			get{return (int)base["height"];}
			set{base["height"]=value;}
		}
		public int width{
			get{return (int)base["width"];}
			set{base["width"]=value;}
		}
		public int hspace{
			get{return (int)base["hspace"];}
			set{base["hspace"]=value;}
		}
		public int vspace{
			get{return (int)base["vspace"];}
			set{base["vspace"]=value;}
		}
		public bool indeterminate{
			get{return (bool)base["indeterminate"];}
			set{base["indeterminate"]=value;}
		}
		public string href{
			get{return (string)base["href"];}
		}
		public object loop{
			get{return (object)base["loop"];}
			set{base["loop"]=value;}
		}
		public string lowsrc{
			get{return (string)base["lowsrc"];}
			set{base["lowsrc"]=value;}
		}
		public int maxlength{
			get{return (int)base["maxlength"];}
			set{base["maxlength"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public bool readOnly{
			get{return (bool)base["readOnly"];}
			set{base["readOnly"]=value;}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public int size{
			get{return (int)base["size"];}
			set{base["size"]=value;}
		}
		public string src{
			get{return (string)base["src"];}
			set{base["src"]=value;}
		}
		public string Start{
			get{return (string)base["Start"];}
			set{base["Start"]=value;}
		}
		public bool status{
			get{return (bool)base["status"];}
			set{base["status"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}
		public string vrml{
			get{return (string)base["vrml"];}
			set{base["vrml"]=value;}
		}
		public event EHVoid onabort{
			add{attachEvent("onabort",value);}
			remove{detachEvent("onabort",value);}
		}
		public event EHVoid onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		public event EHCancel onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
		//--------------------------------------------------
		//	IHTMLInputElement2
		//--------------------------------------------------
		public string accept{
			get{return (string)base["accept"];}
			set{base["accept"]=value;}
		}
		public string useMap{
			get{return (string)base["useMap"];}
			set{base["useMap"]=value;}
		}
	}

	public partial class IsindexElement:Element{
		protected IsindexElement(object instance):base(instance){}
		public static new IsindexElement FromObj(object obj){
			if(obj==null)return null;
			return new IsindexElement(obj);
		}
	}
	public partial class IsindexElement{
		//--------------------------------------------------
		//	IHTMLIsIndexElement
		//--------------------------------------------------
		public string action{
			get{return (string)base["action"];}
			set{base["action"]=value;}
		}
		public string prompt{
			get{return (string)base["prompt"];}
			set{base["prompt"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLIsIndexElement2
		//--------------------------------------------------
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
	}

	public partial class LabelElement:DatabindingElement{
		protected LabelElement(object instance):base(instance){}
		public static new LabelElement FromObj(object obj){
			if(obj==null)return null;
			return new LabelElement(obj);
		}
	}
	public partial class LabelElement{
		//--------------------------------------------------
		//	IHTMLLabelElement
		//--------------------------------------------------
		public string htmlFor{
			get{return (string)base["htmlFor"];}
			set{base["htmlFor"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLLabelElement2
		//--------------------------------------------------
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
	}

	public partial class LegendElement:DatabindingElement{
		protected LegendElement(object instance):base(instance){}
		public static new LegendElement FromObj(object obj){
			if(obj==null)return null;
			return new LegendElement(obj);
		}
	}
	public partial class LegendElement{
		//--------------------------------------------------
		//	IHTMLLegendElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLLegendElement2
		//--------------------------------------------------
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		//--------------------------------------------------
		//	IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
	}

	public partial class LiElement:Element{
		protected LiElement(object instance):base(instance){}
		public static new LiElement FromObj(object obj){
			if(obj==null)return null;
			return new LiElement(obj);
		}
	}
	public partial class LiElement{
		//--------------------------------------------------
		//	IHTMLLIElement
		//--------------------------------------------------
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public int value{
			get{return (int)base["value"];}
			set{base["value"]=value;}
		}
	}

	public partial class LinkElement:Element{
		protected LinkElement(object instance):base(instance){}
		public static new LinkElement FromObj(object obj){
			if(obj==null)return null;
			return new LinkElement(obj);
		}
	}
	public partial class LinkElement{
		//--------------------------------------------------
		//	IHTMLLinkElement
		//--------------------------------------------------
		public string href{
			get{return (string)base["href"];}
			set{base["href"]=value;}
		}
		public string media{
			get{return (string)base["media"];}
			set{base["media"]=value;}
		}
		public string readyState{
			get{return (string)base["readyState"];}
			set{base["readyState"]=value;}
		}
		public string rel{
			get{return (string)base["rel"];}
			set{base["rel"]=value;}
		}
		public string rev{
			get{return (string)base["rev"];}
			set{base["rev"]=value;}
		}
		public StyleSheet styleSheet{
			get{return StyleSheet.FromObj(base["styleSheet"]);}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public event EHVoid onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		//--------------------------------------------------
		//	IHTMLLinkElement2
		//--------------------------------------------------
		public string target{
			get{return (string)base["target"];}
			set{base["target"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLLinkElement3
		//--------------------------------------------------
		public string charset{
			get{return (string)base["charset"];}
			set{base["charset"]=value;}
		}
		public string hreflang{
			get{return (string)base["hreflang"];}
			set{base["hreflang"]=value;}
		}
	}

	public partial class ListElement:Element{
		protected ListElement(object instance):base(instance){}
		public static new ListElement FromObj(object obj){
			if(obj==null)return null;
			return new ListElement(obj);
		}
	}
	public partial class ListElement{
		//--------------------------------------------------
		//	IHTMLListElement2
		//--------------------------------------------------
		public bool compact{
			get{return (bool)base["compact"];}
			set{base["compact"]=value;}
		}
	}

	public partial class MapElement:Element{
		protected MapElement(object instance):base(instance){}
		public static new MapElement FromObj(object obj){
			if(obj==null)return null;
			return new MapElement(obj);
		}
	}
	public partial class MapElement{
		//--------------------------------------------------
		//	IHTMLMapElement
		//--------------------------------------------------
		public AreaElementCollection areas{
			get{return AreaElementCollection.FromObj(base["areas"]);}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
	}

	public partial class MarqueeElement:DatabindingElement{
		protected MarqueeElement(object instance):base(instance){}
		public static new MarqueeElement FromObj(object obj){
			if(obj==null)return null;
			return new MarqueeElement(obj);
		}
	}
	public partial class MarqueeElement{
		//--------------------------------------------------
		//	IHTMLMarqueeElement
		//--------------------------------------------------
		public AreaElementCollection areas{
			get{return AreaElementCollection.FromObj(base["areas"]);}
		}
		public string behavior{
			get{return (string)base["behavior"];}
			set{base["behavior"]=value;}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public string direction{
			get{return (string)base["direction"];}
			set{base["direction"]=value;}
		}
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		public int hspace{
			get{return (int)base["hspace"];}
			set{base["hspace"]=value;}
		}
		public int vspace{
			get{return (int)base["vspace"];}
			set{base["vspace"]=value;}
		}
		public int loop{
			get{return (int)base["loop"];}
			set{base["loop"]=value;}
		}
		public int scrollAmount{
			get{return (int)base["scrollAmount"];}
			set{base["scrollAmount"]=value;}
		}
		public int scrollDelay{
			get{return (int)base["scrollDelay"];}
			set{base["scrollDelay"]=value;}
		}
		public bool trueSpeed{
			get{return (bool)base["trueSpeed"];}
			set{base["trueSpeed"]=value;}
		}
		public event EHVoid onbounce{
			add{attachEvent("onbounce",value);}
			remove{detachEvent("onbounce",value);}
		}
		public event EHVoid onfinish{
			add{attachEvent("onfinish",value);}
			remove{detachEvent("onfinish",value);}
		}
		public event EHVoid onstart{
			add{attachEvent("onstart",value);}
			remove{detachEvent("onstart",value);}
		}
		//--------------------------------------------------
		//	IHTMLTextContainer
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
	}

	public partial class MetaElement:Element{
		protected MetaElement(object instance):base(instance){}
		public static new MetaElement FromObj(object obj){
			if(obj==null)return null;
			return new MetaElement(obj);
		}
	}
	public partial class MetaElement{
		//--------------------------------------------------
		//	IHTMLMetaElement
		//--------------------------------------------------
		public string charset{
			get{return (string)base["charset"];}
			set{base["charset"]=value;}
		}
		public string content{
			get{return (string)base["content"];}
			set{base["content"]=value;}
		}
		public string httpEquiv{
			get{return (string)base["httpEquiv"];}
			set{base["httpEquiv"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public string url{
			get{return (string)base["url"];}
			set{base["url"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLMetaElement2
		//--------------------------------------------------
		public string scheme{
			get{return (string)base["scheme"];}
			set{base["scheme"]=value;}
		}
	}

	public partial class NextidElement:Element{
		protected NextidElement(object instance):base(instance){}
		public static new NextidElement FromObj(object obj){
			if(obj==null)return null;
			return new NextidElement(obj);
		}
	}
	public partial class NextidElement{
		public string n{
			get{return (string)base["n"];}
			set{base["n"]=value;}
		}
	}

	public partial class NoshowElement:Element{
		protected NoshowElement(object instance):base(instance){}
		public static new NoshowElement FromObj(object obj){
			if(obj==null)return null;
			return new NoshowElement(obj);
		}
	}

	public partial class ObjectElement:DatabindingElement{
		protected ObjectElement(object instance):base(instance){}
		public static new ObjectElement FromObj(object obj){
			if(obj==null)return null;
			return new ObjectElement(obj);
		}
	}
	public partial class ObjectElement{
		//--------------------------------------------------
		//	IHTMLObjectElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string altHtml{
			get{return (string)base["altHtml"];}
			set{base["altHtml"]=value;}
		}
		public string BaseHref{
			get{return (string)base["BaseHref"];}
		}
		public string code{
			get{return (string)base["code"];}
			set{base["code"]=value;}
		}
		public string codeBase{
			get{return (string)base["codeBase"];}
			set{base["codeBase"]=value;}
		}
		public string codeType{
			get{return (string)base["codeType"];}
			set{base["codeType"]=value;}
		}
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		public int hspace{
			get{return (int)base["hspace"];}
			set{base["hspace"]=value;}
		}
		public int vspace{
			get{return (int)base["vspace"];}
			set{base["vspace"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public object @object{
			get{return (object)base["@object"];}
		}
		public int readyState{
			get{return (int)base["readyState"];}
		}
		public object recordset{
			get{return (object)base["recordset"];}
			set{base["recordset"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public event EHVoid onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		//--------------------------------------------------
		//	IHTMLObjectElement2
		//--------------------------------------------------
		public string classid{
			get{return (string)base["classid"];}
			set{base["classid"]=value;}
		}
		public string data{
			get{return (string)base["data"];}
			set{base["data"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLObjectElement3
		//--------------------------------------------------
		public string alt{
			get{return (string)base["alt"];}
			set{base["alt"]=value;}
		}
		public string archive{
			get{return (string)base["archive"];}
			set{base["archive"]=value;}
		}
		public object border{
			get{return (object)base["border"];}
			set{base["border"]=value;}
		}
		public bool declare{
			get{return (bool)base["declare"];}
			set{base["declare"]=value;}
		}
		public string standby{
			get{return (string)base["standby"];}
			set{base["standby"]=value;}
		}
		public string useMap{
			get{return (string)base["useMap"];}
			set{base["useMap"]=value;}
		}
	}

	public partial class OlElement:ListElement{
		protected OlElement(object instance):base(instance){}
		public static new OlElement FromObj(object obj){
			if(obj==null)return null;
			return new OlElement(obj);
		}
	}
	public partial class OlElement{
		//--------------------------------------------------
		//	IHTMLOlistElement3
		//--------------------------------------------------
		// #PROP<bool,compact>
		public int Start{
			get{return (int)base["Start"];}
			set{base["Start"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
	}

	public partial class OptionbuttonElement:DatabindingElement{
		protected OptionbuttonElement(object instance):base(instance){}
		public static new OptionbuttonElement FromObj(object obj){
			if(obj==null)return null;
			return new OptionbuttonElement(obj);
		}
	}
	public partial class OptionbuttonElement{
		//--------------------------------------------------
		//	IHTMLOptionButtonElement
		//--------------------------------------------------
		public bool @checked{
			get{return (bool)base["@checked"];}
			set{base["@checked"]=value;}
		}
		public bool defaultChecked{
			get{return (bool)base["defaultChecked"];}
			set{base["defaultChecked"]=value;}
		}
		// #PROP<bool,disabled>
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public bool intermediate{
			get{return (bool)base["intermediate"];}
			set{base["intermediate"]=value;}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public bool status{
			get{return (bool)base["status"];}
			set{base["status"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}
		public event EHCancel onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
	}

	public partial class OptionElement:DatabindingElement{
		protected OptionElement(object instance):base(instance){}
		public static new OptionElement FromObj(object obj){
			if(obj==null)return null;
			return new OptionElement(obj);
		}
	}
	public partial class OptionElement{
		//--------------------------------------------------
		//	IHTMLOptionElement
		//--------------------------------------------------
		public bool defaultSelected{
			get{return (bool)base["defaultSelected"];}
			set{base["defaultSelected"]=value;}
		}
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public int index{
			get{return (int)base["index"];}
			set{base["index"]=value;}
		}
		public bool selected{
			get{return (bool)base["selected"];}
			set{base["selected"]=value;}
		}
		public string text{
			get{return (string)base["text"];}
			set{base["text"]=value;}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLOptionElement3
		//--------------------------------------------------
		public string label{
			get{return (string)base["label"];}
			set{base["label"]=value;}
		}
	}

	public partial class PElement:BlockElement{
		protected PElement(object instance):base(instance){}
		public static new PElement FromObj(object obj){
			if(obj==null)return null;
			return new PElement(obj);
		}
	}
	public partial class PElement{
		//--------------------------------------------------
		//	IHTMLParaElement
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
	}

	public partial class ParamElement:Element{
		protected ParamElement(object instance):base(instance){}
		public static new ParamElement FromObj(object obj){
			if(obj==null)return null;
			return new ParamElement(obj);
		}
	}
	public partial class ParamElement{
		//--------------------------------------------------
		//	IHTMLParamElement
		//--------------------------------------------------
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}
		public string valueType{
			get{return (string)base["valueType"];}
			set{base["valueType"]=value;}
		}
	}

	public partial class PhraseElement:Element{
		protected PhraseElement(object instance):base(instance){}
		public static new PhraseElement FromObj(object obj){
			if(obj==null)return null;
			return new PhraseElement(obj);
		}
	}
	public partial class PhraseElement{
		//--------------------------------------------------
		//	IHTMLPhraseElement
		//--------------------------------------------------
		public string cite{
			get{return (string)base["cite"];}
			set{base["cite"]=value;}
		}
		public string dateTime{
			get{return (string)base["dateTime"];}
			set{base["dateTime"]=value;}
		}
	}

	public partial class RichtextElement:TextAreaElement{
		protected RichtextElement(object instance):base(instance){}
		public static new RichtextElement FromObj(object obj){
			if(obj==null)return null;
			return new RichtextElement(obj);
		}
	}

	public partial class ScriptElement:Element{
		protected ScriptElement(object instance):base(instance){}
		public static new ScriptElement FromObj(object obj){
			if(obj==null)return null;
			return new ScriptElement(obj);
		}
	}
	public partial class ScriptElement{
		//--------------------------------------------------
		//	IHTMLScriptElement
		//--------------------------------------------------
		public bool defer{
			get{return (bool)base["defer"];}
			set{base["defer"]=value;}
		}
		public string @event{
			get{return (string)base["@event"];}
			set{base["@event"]=value;}
		}
		public string htmlFor{
			get{return (string)base["htmlFor"];}
			set{base["htmlFor"]=value;}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public string src{
			get{return (string)base["src"];}
			set{base["src"]=value;}
		}
		public string text{
			get{return (string)base["text"];}
			set{base["text"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public event EHVoid onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		//--------------------------------------------------
		//	IHTMLScriptElement2
		//--------------------------------------------------
		public string charset{
			get{return (string)base["charset"];}
			set{base["charset"]=value;}
		}
	}

	public partial class SelectElement:DatabindingElement{
		protected SelectElement(object instance):base(instance){}
		public static new SelectElement FromObj(object obj){
			if(obj==null)return null;
			return new SelectElement(obj);
		}
	}
	public partial class SelectElement:Gen::IEnumerable<Element>,System.Collections.IEnumerable{
		//--------------------------------------------------
		//	IHTMLSelectElement - Collection
		//--------------------------------------------------

		//--------------------------------------------------
		//	IHTMLSelectElement
		//--------------------------------------------------
		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public int type{
			get{return (int)base["type"];}
			set{base["type"]=value;}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}

		public ElementCollection options{
			get{return ElementCollection.FromObj(base["options"]);}
		}
		public bool multiple{
			get{return (bool)base["multiple"];}
			set{base["multiple"]=value;}
		}
		public int selectedIndex{
			get{return (int)base["selectedIndex"];}
			set{base["selectedIndex"]=value;}
		}
		public int size{
			get{return (int)base["size"];}
		}
		public event EHCancel onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		//--------------------------------------------------
		//	IHTMLSelectElement2
		//--------------------------------------------------

		//--------------------------------------------------
		//	IHTMLSelectElement2
		//--------------------------------------------------

	}

	public partial class SpanElement:DatabindingElement{
		protected SpanElement(object instance):base(instance){}
		public static new SpanElement FromObj(object obj){
			if(obj==null)return null;
			return new SpanElement(obj);
		}
	}
	public partial class SpanElement{
		//--------------------------------------------------
		//	IHTMLPhraseElement
		//--------------------------------------------------
		public string cite{
			get{return (string)base["cite"];}
			set{base["cite"]=value;}
		}
		public string dateTime{
			get{return (string)base["dateTime"];}
			set{base["dateTime"]=value;}
		}
	}

	public partial class SpanFlow:DatabindingElement{
		protected SpanFlow(object instance):base(instance){}
		public static new SpanFlow FromObj(object obj){
			if(obj==null)return null;
			return new SpanFlow(obj);
		}
	}
	public partial class SpanFlow{
		//--------------------------------------------------
		//	IHTMLPhraseElement
		//--------------------------------------------------
		public string cite{
			get{return (string)base["cite"];}
			set{base["cite"]=value;}
		}
		public string dateTime{
			get{return (string)base["dateTime"];}
			set{base["dateTime"]=value;}
		}
		//--------------------------------------------------
		//	HTMLTextContainerEvents
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
		//--------------------------------------------------
		//	IHTMLSpanFlow
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
	}

	public partial class StyleElement:Element{
		protected StyleElement(object instance):base(instance){}
		public static new StyleElement FromObj(object obj){
			if(obj==null)return null;
			return new StyleElement(obj);
		}
	}
	public partial class StyleElement{
		//--------------------------------------------------
		//	IHTMLStyleElement
		//--------------------------------------------------
		public string media{
			get{return (string)base["media"];}
			set{base["media"]=value;}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public StyleSheet styleSheet{
			get{return StyleSheet.FromObj(base["styleSheet"]);}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public event EHVoid onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
	}

	public partial class CaptionElement:Element{
		protected CaptionElement(object instance):base(instance){}
		public static new CaptionElement FromObj(object obj){
			if(obj==null)return null;
			return new CaptionElement(obj);
		}
	}
	public partial class CaptionElement{
		//--------------------------------------------------
		//	HTMLTextContainerEvents
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
		//--------------------------------------------------
		//	IHTMLTableCaption
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string vAlign{
			get{return (string)base["vAlign"];}
			set{base["vAlign"]=value;}
		}
	}

	public partial class TableCellElement:Element{
		protected TableCellElement(object instance):base(instance){}
		public static new TableCellElement FromObj(object obj){
			if(obj==null)return null;
			return new TableCellElement(obj);
		}
	}
	public partial class TableCellElement{
		//--------------------------------------------------
		//	HTMLTextContainerEvents
		//--------------------------------------------------
		public event EHVoid onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
		//--------------------------------------------------
		//	IHTMLTableCell
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string vAlign{
			get{return (string)base["vAlign"];}
			set{base["vAlign"]=value;}
		}
		public string background{
			get{return (string)base["background"];}
			set{base["background"]=value;}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public object borderColor{
			get{return (object)base["borderColor"];}
			set{base["borderColor"]=value;}
		}
		public object borderColorDark{
			get{return (object)base["borderColorDark"];}
			set{base["borderColorDark"]=value;}
		}
		public object borderColorLight{
			get{return (object)base["borderColorLight"];}
			set{base["borderColorLight"]=value;}
		}
		public int cellIndex{
			get{return (int)base["cellIndex"];}
		}
		public int colSpan{
			get{return (int)base["colSpan"];}
			set{base["colSpan"]=value;}
		}
		public int rowSpan{
			get{return (int)base["rowSpan"];}
			set{base["rowSpan"]=value;}
		}
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		public bool noWrap{
			get{return (bool)base["noWrap"];}
			set{base["noWrap"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLTableCell2
		//--------------------------------------------------
		public string abbr{
			get{return (string)base["abbr"];}
			set{base["abbr"]=value;}
		}
		public string axis{
			get{return (string)base["axis"];}
			set{base["axis"]=value;}
		}
		public string ch{
			get{return (string)base["ch"];}
			set{base["ch"]=value;}
		}
		public string chOff{
			get{return (string)base["chOff"];}
			set{base["chOff"]=value;}
		}
		public string headers{
			get{return (string)base["headers"];}
			set{base["headers"]=value;}
		}
		public string scope{
			get{return (string)base["scope"];}
			set{base["scope"]=value;}
		}
	}

	public partial class TableElement:DatabindingElement{
		protected TableElement(object instance):base(instance){}
		public static new TableElement FromObj(object obj){
			if(obj==null)return null;
			return new TableElement(obj);
		}
	}
	public partial class TableElement{
		//--------------------------------------------------
		//	IHTMLTable
		//--------------------------------------------------

		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string background{
			get{return (string)base["background"];}
			set{base["background"]=value;}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public object border{
			get{return (object)base["border"];}
			set{base["border"]=value;}
		}
		public object borderColor{
			get{return (object)base["borderColor"];}
			set{base["borderColor"]=value;}
		}
		public object borderColorDark{
			get{return (object)base["borderColorDark"];}
			set{base["borderColorDark"]=value;}
		}
		public object borderColorLight{
			get{return (object)base["borderColorLight"];}
			set{base["borderColorLight"]=value;}
		}
		public CaptionElement caption{
			get{return CaptionElement.FromObj(base["caption"]);}
		}
		public object cellPadding{
			get{return (object)base["cellPadding"];}
			set{base["cellPadding"]=value;}
		}
		public object cellSpacing{
			get{return (object)base["cellSpacing"];}
			set{base["cellSpacing"]=value;}
		}
		public int cols{
			get{return (int)base["cols"];}
			set{base["cols"]=value;}
		}
		public int dataPageSize{
			get{return (int)base["dataPageSize"];}
			set{base["dataPageSize"]=value;}
		}
		public string frame{
			get{return (string)base["frame"];}
			set{base["frame"]=value;}
		}
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public ElementCollection<TrElement> rows{
			get{return ElementCollection<TrElement>.FromObj(base["rows"]);}
		}
		public string rules{
			get{return (string)base["rules"];}
			set{base["rules"]=value;}
		}
		public ElementCollection<TableSectionElement> tBodies{
			get{return ElementCollection<TableSectionElement>.FromObj(base["tBodies"]);}
		}
		public TableSectionElement tFoot{
			get{return TableSectionElement.FromObj(base["tFoot"]);}
		}
		public TableSectionElement tHead{
			get{return TableSectionElement.FromObj(base["tHead"]);}
		}
		//--------------------------------------------------
		//	IHTMLTable2
		//--------------------------------------------------

		public ElementCollection<TableCellElement> cells{
			get{return ElementCollection<TableCellElement>.FromObj(base["cells"]);}
		}
		//--------------------------------------------------
		//	IHTMLTable2
		//--------------------------------------------------
		public string summary{
			get{return (string)base["summary"];}
			set{base["summary"]=value;}
		}
	}


	public partial class ColElement:Element{
		protected ColElement(object instance):base(instance){}
		public static new ColElement FromObj(object obj){
			if(obj==null)return null;
			return new ColElement(obj);
		}
	}
	public partial class ColElement{
		//--------------------------------------------------
		//	IHTMLTableCol
		//--------------------------------------------------
		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string vAlign{
			get{return (string)base["vAlign"];}
			set{base["vAlign"]=value;}
		}
		public string span{
			get{return (string)base["span"];}
			set{base["span"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLTableCol2
		//--------------------------------------------------
		public string ch{
			get{return (string)base["ch"];}
			set{base["ch"]=value;}
		}
		public string chOff{
			get{return (string)base["chOff"];}
			set{base["chOff"]=value;}
		}
	}

	public partial class TrElement:Element{
		protected TrElement(object instance):base(instance){}
		public static new TrElement FromObj(object obj){
			if(obj==null)return null;
			return new TrElement(obj);
		}
	}
	public partial class TrElement{
		//--------------------------------------------------
		//	IHTMLTableRow
		//--------------------------------------------------

		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string vAlign{
			get{return (string)base["vAlign"];}
			set{base["vAlign"]=value;}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public object borderColor{
			get{return (object)base["borderColor"];}
			set{base["borderColor"]=value;}
		}
		public object borderColorDark{
			get{return (object)base["borderColorDark"];}
			set{base["borderColorDark"]=value;}
		}
		public object borderColorLight{
			get{return (object)base["borderColorLight"];}
			set{base["borderColorLight"]=value;}
		}
		public ElementCollection<TableCellElement> cells{
			get{return ElementCollection<TableCellElement>.FromObj(base["cells"]);}
		}
		public int rowIndex{
			get{return (int)base["rowIndex"];}
		}
		public int sectionRowIndex{
			get{return (int)base["sectionRowIndex"];}
		}
		//--------------------------------------------------
		//	IHTMLTableRow2
		//--------------------------------------------------
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		//--------------------------------------------------
		//	IHTMLTableRow3
		//--------------------------------------------------
		public string ch{
			get{return (string)base["ch"];}
			set{base["ch"]=value;}
		}
		public string chOff{
			get{return (string)base["chOff"];}
			set{base["chOff"]=value;}
		}
	}

	public partial class TableSectionElement:Element{
		protected TableSectionElement(object instance):base(instance){}
		public static new TableSectionElement FromObj(object obj){
			if(obj==null)return null;
			return new TableSectionElement(obj);
		}
	}
	public partial class TableSectionElement{
		//--------------------------------------------------
		//	IHTMLTableSection
		//--------------------------------------------------

		public string align{
			get{return (string)base["align"];}
			set{base["align"]=value;}
		}
		public string vAlign{
			get{return (string)base["vAlign"];}
			set{base["vAlign"]=value;}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public ElementCollection<TrElement> rows{
			get{return ElementCollection<TrElement>.FromObj(base["rows"]);}
		}
		//--------------------------------------------------
		//	IHTMLTableSection2
		//--------------------------------------------------

		//--------------------------------------------------
		//	IHTMLTableSection3
		//--------------------------------------------------
		public string ch{
			get{return (string)base["ch"];}
			set{base["ch"]=value;}
		}
		public string chOff{
			get{return (string)base["chOff"];}
			set{base["chOff"]=value;}
		}
	}

	public partial class TextElement:Element{
		protected TextElement(object instance):base(instance){}
		public static new TextElement FromObj(object obj){
			if(obj==null)return null;
			return new TextElement(obj);
		}
	}

	public partial class TextAreaElement:DatabindingElement{
		protected TextAreaElement(object instance):base(instance){}
		public static new TextAreaElement FromObj(object obj){
			if(obj==null)return null;
			return new TextAreaElement(obj);
		}
	}
	public partial class TextAreaElement{
		//--------------------------------------------------
		//	IHTMLTextAreaElement
		//--------------------------------------------------

		public int cols{
			get{return (int)base["cols"];}
			set{base["cols"]=value;}
		}
		public int rows{
			get{return (int)base["rows"];}
			set{base["rows"]=value;}
		}
		public string defaultValue{
			get{return (string)base["defaultValue"];}
			set{base["defaultValue"]=value;}
		}
		public bool readOnly{
			get{return (bool)base["readOnly"];}
			set{base["readOnly"]=value;}
		}
		public string wrap{
			get{return (string)base["wrap"];}
		}

		public FormElement form{
			get{return FormElement.FromObj(base["form"]);}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public object status{
			get{return (object)base["status"];}
		}
		public int type{
			get{return (int)base["type"];}
			set{base["type"]=value;}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}

		public event EHCancel onchange{
			add{attachEvent("onchange",value);}
			remove{detachEvent("onchange",value);}
		}
		public event EHVoid onselect{
			add{attachEvent("onselect",value);}
			remove{detachEvent("onselect",value);}
		}
	}

	public partial class TitleElement:Element{
		protected TitleElement(object instance):base(instance){}
		public static new TitleElement FromObj(object obj){
			if(obj==null)return null;
			return new TitleElement(obj);
		}
	}
	public partial class TitleElement{
		//--------------------------------------------------
		//	IHTMLTitleElement
		//--------------------------------------------------
		public string text{
			get{return (string)base["text"];}
			set{base["text"]=value;}
		}
	}

	public partial class UlElement:ListElement{
		protected UlElement(object instance):base(instance){}
		public static new UlElement FromObj(object obj){
			if(obj==null)return null;
			return new UlElement(obj);
		}
	}
	public partial class UlElement{
		//--------------------------------------------------
		//	IHTMLUlistElement3
		//--------------------------------------------------
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
	}

	public partial class UnknownElement:Element{
		protected UnknownElement(object instance):base(instance){}
		public static new UnknownElement FromObj(object obj){
			if(obj==null)return null;
			return new UnknownElement(obj);
		}
	}

	// ※ IHTMLTextContainer ⊂ Element
	// ※ IHTMLControlElement ⊂ Element
	// ※ HTMLTextContainerEvents → onchange onselect
	// ※ HTMLControlElementEvents ⊂ Element
}