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


	/// <summary>
	/// HtmlDocument オブジェクトをラップします。
	/// </summary>
	public partial class Document:MshtmlObject{
		protected Document(object instance):base(instance){}
		public static Document FromObj(object obj){
			if(obj==null)return null;
			return new Document(obj);
		}
	}
	public partial class Document{

		//--------------------------------------------------
		//		IHTMLDocument
		//--------------------------------------------------
		public object Script{
			get{return (object)base["Script"];}
		}
		//--------------------------------------------------
		//		IHTMLDocument2
		//--------------------------------------------------

		public Element activeElement{
			get{return Element.FromObj(base["activeElement"]);}
		}
		public object alinkColor{
			get{return (object)base["alinkColor"];}
			set{base["alinkColor"]=value;}
		}
		public ElementCollection all{
			get{return ElementCollection.FromObj(base["all"]);}
		}
		public ElementCollection anchors{
			get{return ElementCollection.FromObj(base["anchors"]);}
		}
		public ElementCollection applets{
			get{return ElementCollection.FromObj(base["applets"]);}
		}
		public object bgColor{
			get{return (object)base["bgColor"];}
			set{base["bgColor"]=value;}
		}
		public BodyElement body{
			get{return BodyElement.FromObj(base["body"]);}
		}
		public string charset{
			get{return (string)base["charset"];}
			set{base["charset"]=value;}
		}
		public string cookie{
			get{return (string)base["cookie"];}
			set{base["cookie"]=value;}
		}
		public string defaultCharset{
			get{return (string)base["defaultCharset"];}
			set{base["defaultCharset"]=value;}
		}
		public string designMode{
			get{return (string)base["designMode"];}
			set{base["designMode"]=value;}
		}
		public string domain{
			get{return (string)base["domain"];}
			set{base["domain"]=value;}
		}
		public ElementCollection embeds{
			get{return ElementCollection.FromObj(base["embeds"]);}
		}
		public bool expando{
			get{return (bool)base["expando"];}
			set{base["expando"]=value;}
		}
		public object fgColor{
			get{return (object)base["fgColor"];}
			set{base["fgColor"]=value;}
		}
		public string fileCreatedDate{
			get{return (string)base["fileCreatedDate"];}
		}
		public string fileModifiedData{
			get{return (string)base["fileModifiedData"];}
		}
		public string fileSize{
			get{return (string)base["fileSize"];}
		}
		public string fileUpdatedDate{
			get{return (string)base["fileUpdatedDate"];}
		}
		public ElementCollection forms{
			get{return ElementCollection.FromObj(base["forms"]);}
		}
		public CollectionBase<FrameElement> frames{
			get{return CollectionBase<FrameElement>.FromObj(base["frames"]);}
		}
		public ElementCollection images{
			get{return ElementCollection.FromObj(base["images"]);}
		}
		public string lastModified{
			get{return (string)base["lastModified"];}
		}
		public object linkColor{
			get{return (object)base["linkColor"];}
			set{base["linkColor"]=value;}
		}
		public ElementCollection links{
			get{return ElementCollection.FromObj(base["links"]);}
		}
		public Location location{
			get{return Location.FromObj(base["location"]);}
		}
		public string mimeType{
			get{return (string)base["mimeType"];}
		}
		public string nameProp{
			get{return (string)base["nameProp"];}
		}
		public Window parentWindow{
			get{return Window.FromObj(base["parentWindow"]);}
		}
		public ElementCollection plugins{
			get{return ElementCollection.FromObj(base["plugins"]);}
		}
		public string protocol{
			get{return (string)base["protocol"];}
		}
		public string readyState{
			get{return (string)base["readyState"];}
		}
		public string referer{
			get{return (string)base["referer"];}
		}
		public ElementCollection scripts{
			get{return ElementCollection.FromObj(base["scripts"]);}
		}
		public string security{
			get{return (string)base["security"];}
		}
		public Selection selection{
			get{return Selection.FromObj(base["selection"]);}
		}
		public CollectionBase<StyleSheet> styleSheets{
			get{return CollectionBase<StyleSheet>.FromObj(base["styleSheets"]);}
		}
		public string title{
			get{return (string)base["title"];}
			set{base["title"]=value;}
		}
		public string url{
			get{return (string)base["url"];}
			set{base["url"]=value;}
		}
		public object vlinkColor{
			get{return (object)base["vlinkColor"];}
			set{base["vlinkColor"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLDocument3
		//--------------------------------------------------

		public string baseUrl{
			get{return (string)base["baseUrl"];}
			set{base["baseUrl"]=value;}
		}
		public object childNodes{
			get{return (object)base["childNodes"];}
		}
		public string dir{
			get{return (string)base["dir"];}
			set{base["dir"]=value;}
		}
		public Element documentElement{
			get{return Element.FromObj(base["documentElement"]);}
		}
		public bool enableDownload{
			get{return (bool)base["enableDownload"];}
			set{base["enableDownload"]=value;}
		}
		public bool inheritStyleSheets{
			get{return (bool)base["inheritStyleSheets"];}
			set{base["inheritStyleSheets"]=value;}
		}
		public Document parentDocument{
			get{return Document.FromObj(base["parentDocument"]);}
		}
		public string uniqueID{
			get{return (string)base["uniqueID"];}
		}
		//--------------------------------------------------
		//		IHTMLDocument4
		//--------------------------------------------------

		public string media{
			get{return (string)base["media"];}
			set{base["media"]=value;}
		}
		public object namespaces{
			get{return (object)base["namespaces"];}
		}
		public string URLUnencoded{
			get{return (string)base["URLUnencoded"];}
		}
		//--------------------------------------------------
		//		IHTMLDocument5
		//--------------------------------------------------

		public string compatMode{
			get{return (string)base["compatMode"];}
		}
		public DomNode doctype{
			get{return DomNode.FromObj(base["doctype"]);}
		}
		public DomImplementation implementation{
			get{return DomImplementation.FromObj(base["implementation"]);}
		}
		//--------------------------------------------------
		//		Events
		//--------------------------------------------------
		public event EHVoid onactivate{
			add{attachEvent("onactivate",value);}
			remove{detachEvent("onactivate",value);}
		}
		public event EHVoid onafterupdate{
			add{attachEvent("onafterupdate",value);}
			remove{detachEvent("onafterupdate",value);}
		}
		public event EHCancel onbeforeactivate{
			add{attachEvent("onbeforeactivate",value);}
			remove{detachEvent("onbeforeactivate",value);}
		}
		public event EHCancel onbeforedeactivate{
			add{attachEvent("onbeforedeactivate",value);}
			remove{detachEvent("onbeforedeactivate",value);}
		}
		public event EHVoid onbeforeeditfocus{
			add{attachEvent("onbeforeeditfocus",value);}
			remove{detachEvent("onbeforeeditfocus",value);}
		}
		public event EHCancel onbeforeupdate{
			add{attachEvent("onbeforeupdate",value);}
			remove{detachEvent("onbeforeupdate",value);}
		}
		public event EHVoid oncellchange{
			add{attachEvent("oncellchange",value);}
			remove{detachEvent("oncellchange",value);}
		}
		public event EHCancel onclick{
			add{attachEvent("onclick",value);}
			remove{detachEvent("onclick",value);}
		}
		public event EHCancel oncontextmenu{
			add{attachEvent("oncontextmenu",value);}
			remove{detachEvent("oncontextmenu",value);}
		}
		public event EHCancel oncontrolselect{
			add{attachEvent("oncontrolselect",value);}
			remove{detachEvent("oncontrolselect",value);}
		}
		public event EHVoid ondataavailable{
			add{attachEvent("ondataavailable",value);}
			remove{detachEvent("ondataavailable",value);}
		}
		public event EHVoid ondatasetchanged{
			add{attachEvent("ondatasetchanged",value);}
			remove{detachEvent("ondatasetchanged",value);}
		}
		public event EHVoid ondatasetcompleted{
			add{attachEvent("ondatasetcompleted",value);}
			remove{detachEvent("ondatasetcompleted",value);}
		}
		public event EHCancel ondblclick{
			add{attachEvent("ondblclick",value);}
			remove{detachEvent("ondblclick",value);}
		}
		public event EHVoid ondeactivate{
			add{attachEvent("ondeactivate",value);}
			remove{detachEvent("ondeactivate",value);}
		}
		public event EHCancel ondragstart{
			add{attachEvent("ondragstart",value);}
			remove{detachEvent("ondragstart",value);}
		}
		public event EHCancel onerrorupdate{
			add{attachEvent("onerrorupdate",value);}
			remove{detachEvent("onerrorupdate",value);}
		}
		public event EHVoid onfocusin{
			add{attachEvent("onfocusin",value);}
			remove{detachEvent("onfocusin",value);}
		}
		public event EHVoid onfocusout{
			add{attachEvent("onfocusout",value);}
			remove{detachEvent("onfocusout",value);}
		}
		public event EHCancel onhelp{
			add{attachEvent("onhelp",value);}
			remove{detachEvent("onhelp",value);}
		}
		public event EHVoid onkeydown{
			add{attachEvent("onkeydown",value);}
			remove{detachEvent("onkeydown",value);}
		}
		public event EHCancel onkeypress{
			add{attachEvent("onkeypress",value);}
			remove{detachEvent("onkeypress",value);}
		}
		public event EHVoid onkeyup{
			add{attachEvent("onkeyup",value);}
			remove{detachEvent("onkeyup",value);}
		}
		public event EHVoid onmousedown{
			add{attachEvent("onmousedown",value);}
			remove{detachEvent("onmousedown",value);}
		}
		public event EHVoid onmousemove{
			add{attachEvent("onmousemove",value);}
			remove{detachEvent("onmousemove",value);}
		}
		public event EHVoid onmouseout{
			add{attachEvent("onmouseout",value);}
			remove{detachEvent("onmouseout",value);}
		}
		public event EHVoid onmouseover{
			add{attachEvent("onmouseover",value);}
			remove{detachEvent("onmouseover",value);}
		}
		public event EHVoid onmouseup{
			add{attachEvent("onmouseup",value);}
			remove{detachEvent("onmouseup",value);}
		}
		public event EHCancel onmousewheel{
			add{attachEvent("onmousewheel",value);}
			remove{detachEvent("onmousewheel",value);}
		}
		public event EHVoid onpropertychange{
			add{attachEvent("onpropertychange",value);}
			remove{detachEvent("onpropertychange",value);}
		}
		public event EHVoid onreadystatechange{
			add{attachEvent("onreadystatechange",value);}
			remove{detachEvent("onreadystatechange",value);}
		}
		public event EHVoid onrowenter{
			add{attachEvent("onrowenter",value);}
			remove{detachEvent("onrowenter",value);}
		}
		public event EHCancel onrowexit{
			add{attachEvent("onrowexit",value);}
			remove{detachEvent("onrowexit",value);}
		}
		public event EHVoid onrowsdelete{
			add{attachEvent("onrowsdelete",value);}
			remove{detachEvent("onrowsdelete",value);}
		}
		public event EHVoid onrowsinserted{
			add{attachEvent("onrowsinserted",value);}
			remove{detachEvent("onrowsinserted",value);}
		}
		public event EHVoid onselectionchange{
			add{attachEvent("onselectionchange",value);}
			remove{detachEvent("onselectionchange",value);}
		}
		public event EHCancel onselectstart{
			add{attachEvent("onselectstart",value);}
			remove{detachEvent("onselectstart",value);}
		}
		public event EHCancel onstop{
			add{attachEvent("onstop",value);}
			remove{detachEvent("onstop",value);}
		}
	}
}