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

	//================================================================
	//	cls:DomNode
	//================================================================
	// IHTMLDOMNode
	// IHTMLDOMNode2
	public partial class DomNode:MshtmlObject{

		//------------------------------------------------------------
		//	IHTMLDOMNode
		//------------------------------------------------------------

		public DomNodeCollection childNodes{
			get{return DomNodeCollection.FromObj(base["childNodes"]);}
		}
		public DomNode firstChild{
			get{return DomNode.FromObj(base["firstChild"]);}
		}
		public DomNode lastChild{
			get{return DomNode.FromObj(base["lastChild"]);}
		}
		public DomNode nextSibling{
			get{return DomNode.FromObj(base["nextSibling"]);}
		}
		public string nodeName{
			get{return (string)base["nodeName"];}
		}
		public int nodeType{
			get{return (int)base["nodeType"];}
		}
		public object nodeValue{
			get{return (object)base["nodeValue"];}
		}
		public DomNode parentNode{
			get{return DomNode.FromObj(base["parentNode"]);}
		}
		public DomNode previousSibling{
			get{return DomNode.FromObj(base["previousSibling"]);}
		}
		//------------------------------------------------------------
		//	IHTMLDOMNode2
		//------------------------------------------------------------
		public Document ownerDocument{
			get{return Document.FromObj(base["ownerDocument"]);}
		}
	}

	//================================================================
	//	cls:DomAttribute
	//================================================================
	// HTMLDOMAttribute
	// DispHTMLDOMAttribute
	// IHTMLDOMAttribute
	// IHTMLDOMAttribute2
	public partial class DomAttribute:DomNode{
		protected DomAttribute(object instance):base(instance){}
		public static new DomAttribute FromObj(object obj){
			if(obj==null)return null;
			return new DomAttribute(obj);
		}
	}
	public partial class DomAttribute{
		public bool specified{
			get{return (bool)base["specified"];}
		}
		public bool expando{
			get{return (bool)base["expando"];}
		}
		public string name{
			get{return (string)base["name"];}
		}
		public string value{
			get{return (string)base["value"];}
			set{base["value"]=value;}
		}
	}

	//================================================================
	//	cls:DomText
	//================================================================
	// IHTMLDOMTextNode
	// IHTMLDOMTextNode2
	public partial class DomText:DomNode{
		protected DomText(object instance):base(instance){}
		public static new DomText FromObj(object obj){
			if(obj==null)return null;
			return new DomText(obj);
		}
	}
	public partial class DomText{

	}

	//================================================================
	//	cls:Element
	//================================================================
	#region cls:Element
	// HTMLElementEvents
	// HTMLElementEvents_Event
	// IHTMLElement
	// IHTMLElement2
	// IHTMLElement3
	// IHTMLElement4
	// IHTMLElementDefaults
	public partial class Element:DomNode{

		//==================================================
		//		Properties
		//==================================================
		//		IHTMLElement
		//--------------------------------------------------
		public ElementCollection all{
			get{return (ElementCollection)base["all"];}
			set{base["all"]=value;}
		}
		public ElementCollection children{
			get{return (ElementCollection)base["children"];}
			set{base["children"]=value;}
		}
		public string className{
			get{return (string)base["className"];}
			set{base["className"]=value;}
		}
		public NamedCollection<MshtmlObject> filters{
			get{return NamedCollection<MshtmlObject>.FromObj(base["filters"]);}
		}
		public string id{
			get{return (string)base["id"];}
			set{base["id"]=value;}
		}
		public string innerHTML{
			get{return (string)base["innerHTML"];}
			set{base["innerHTML"]=value;}
		}
		public string innerText{
			get{return (string)base["innerText"];}
			set{base["innerText"]=value;}
		}
		public bool isTextEdit{
			get{return (bool)base["isTextEdit"];}
		}
		public string lang{
			get{return (string)base["lang"];}
			set{base["lang"]=value;}
		}
		public string language{
			get{return (string)base["language"];}
			set{base["language"]=value;}
		}
		public int offsetHeight{
			get{return (int)base["offsetHeight"];}
		}
		public int offsetLeft{
			get{return (int)base["offsetLeft"];}
		}
		public Element offsetParent{
			get{return Element.FromObj(base["offsetParent"]);}
		}
		public int offsetTop{
			get{return (int)base["offsetTop"];}
		}
		public int offsetWidth{
			get{return (int)base["offsetWidth"];}
		}
		public string outerHTML{
			get{return (string)base["outerHTML"];}
			set{base["outerHTML"]=value;}
		}
		public string outerText{
			get{return (string)base["outerText"];}
			set{base["outerText"]=value;}
		}
		public Element parentElement{
			get{return Element.FromObj(base["parentElement"]);}
		}
		public Element parentTextEdit{
			get{return Element.FromObj(base["parentTextEdit"]);}
		}
		// recordNumber
		public int sourceIndex{
			get{return (int)base["sourceIndex"];}
		}
		public Style style{
			get{return Style.FromObj(base["style"]);}
		}
		public string tagName{
			get{return (string)base["tagName"];}
		}
		public string title{
			get{return (string)base["title"];}
			set{base["title"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLElement2
		//--------------------------------------------------
		public string accessKey{
			get{return (string)base["accessKey"];}
			set{base["accessKey"]=value;}
		}
		public BehaviorUrns behaviorUrns{
			get{return (BehaviorUrns)base["behaviorUrns"];}
		}
		public bool canHaveChildren{
			get{return (bool)base["canHaveChildren"];}
		}
		public int clientHeight{
			get{return (int)base["clientHeight"];}
		}
		public int clientLeft{
			get{return (int)base["clientLeft"];}
		}
		public int clientTop{
			get{return (int)base["clientTop"];}
		}
		public int clientWidth{
			get{return (int)base["clientWidth"];}
		}
		public CurrentStyle currentStyle{
			get{return (CurrentStyle)base["currentStyle"];}
		}
		public string dir{
			get{return (string)base["dir"];}
			set{base["dir"]=value;}
		}
		// readyState
		public int readyStateValue{
			get{return (int)base["readyStateValue"];}
		}
		public Style runtimeStyle{
			get{return Style.FromObj(base["runtimeStyle"]);}
		}
		public string scopeName{
			get{return (string)base["scopeName"];}
		}
		public int scrollHeight{
			get{return (int)base["scrollHeight"];}
		}
		public int scrollLeft{
			get{return (int)base["scrollLeft"];}
		}
		public int scrollTop{
			get{return (int)base["scrollTop"];}
		}
		public int scrollWidth{
			get{return (int)base["scrollWidth"];}
		}
		public short tabIndex{
			get{return (short)base["tabIndex"];}
			set{base["tabIndex"]=value;}
		}
		public string tagUrn{
			get{return (string)base["tagUrn"];}
			set{base["tagUrn"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLElement3
		//--------------------------------------------------
		public bool canHaveHTML{
			get{return (bool)base["canHaveHTML"];}
		}
		public string contentEditable{
			get{return (string)base["contentEditable"];}
			set{base["contentEditable"]=value;}
		}
		public bool disabled{
			get{return (bool)base["disabled"];}
			set{base["disabled"]=value;}
		}
		public int glyphMode{
			get{return (int)base["glyphMode"];}
		}
		public bool hideFocus{
			get{return (bool)base["hideFocus"];}
			set{base["hideFocus"]=value;}
		}
		public bool inflateBlock{
			get{return (bool)base["inflateBlock"];}
			set{base["inflateBlock"]=value;}
		}
		public bool isContentEditable{
			get{return (bool)base["isContentEditable"];}
		}
		public bool isDisabled{
			get{return (bool)base["isDisabled"];}
		}
		public bool isMultiLine{
			get{return (bool)base["isMultiLine"];}
		}
		//--------------------------------------------------
		//		IHTMLElement4
		//--------------------------------------------------
		// --
		//--------------------------------------------------
		//		IHTMLElementDefaults
		//--------------------------------------------------
		public bool frozen{
			get{return (bool)base["frozen"];}
			set{base["frozen"]=value;}
		}
		public int scrollSegmentX{
			get{return (int)base["scrollSegmentX"];}
			set{base["scrollSegmentX"]=value;}
		}
		public int scrollSegmentY{
			get{return (int)base["scrollSegmentY"];}
			set{base["scrollSegmentY"]=value;}
		}
		public bool tabStop{
			get{return (bool)base["tabStop"];}
			set{base["tabStop"]=value;}
		}
		public bool viewInheritStyle{
			get{return (bool)base["viewInheritStyle"];}
			set{base["viewInheritStyle"]=value;}
		}
		public Document viewLink{
			get{return Document.FromObj(base["viewLink"]);}
			set{base["viewLink"]=((IWrapper)value).Value;}
		}
		public bool viewMasterTab{
			get{return (bool)base["viewMasterTab"];}
			set{base["viewMasterTab"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLUniqueName
		//--------------------------------------------------
		public string uniqueID{
			get{return (string)base["uniqueID"];}
		}
		public int uniqueNumber{
			get{return (int)base["uniqueNumber"];}
		}
		//==================================================
		//		Events
		//==================================================
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
		public event EHCancel onbeforecopy{
			add{attachEvent("onbeforecopy",value);}
			remove{detachEvent("onbeforecopy",value);}
		}
		public event EHCancel onbeforecut{
			add{attachEvent("onbeforecut",value);}
			remove{detachEvent("onbeforecut",value);}
		}
		public event EHCancel onbeforedeactivate{
			add{attachEvent("onbeforedeactivate",value);}
			remove{detachEvent("onbeforedeactivate",value);}
		}
		public event EHVoid onbeforeeditfocus{
			add{attachEvent("onbeforeeditfocus",value);}
			remove{detachEvent("onbeforeeditfocus",value);}
		}
		public event EHCancel onbeforepaste{
			add{attachEvent("onbeforepaste",value);}
			remove{detachEvent("onbeforepaste",value);}
		}
		public event EHCancel onbeforeupdate{
			add{attachEvent("onbeforeupdate",value);}
			remove{detachEvent("onbeforeupdate",value);}
		}
		public event EHVoid onblur{
			add{attachEvent("onblur",value);}
			remove{detachEvent("onblur",value);}
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
		public event EHCancel oncopy{
			add{attachEvent("oncopy",value);}
			remove{detachEvent("oncopy",value);}
		}
		public event EHCancel oncut{
			add{attachEvent("oncut",value);}
			remove{detachEvent("oncut",value);}
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
		public event EHCancel ondrag{
			add{attachEvent("ondrag",value);}
			remove{detachEvent("ondrag",value);}
		}
		public event EHVoid ondragend{
			add{attachEvent("ondragend",value);}
			remove{detachEvent("ondragend",value);}
		}
		public event EHCancel ondragenter{
			add{attachEvent("ondragenter",value);}
			remove{detachEvent("ondragenter",value);}
		}
		public event EHVoid ondragleave{
			add{attachEvent("ondragleave",value);}
			remove{detachEvent("ondragleave",value);}
		}
		public event EHCancel ondragover{
			add{attachEvent("ondragover",value);}
			remove{detachEvent("ondragover",value);}
		}
		public event EHCancel ondragstart{
			add{attachEvent("ondragstart",value);}
			remove{detachEvent("ondragstart",value);}
		}
		public event EHCancel ondrop{
			add{attachEvent("ondrop",value);}
			remove{detachEvent("ondrop",value);}
		}
		public event EHCancel onerrorupdate{
			add{attachEvent("onerrorupdate",value);}
			remove{detachEvent("onerrorupdate",value);}
		}
		public event EHVoid onfilterchange{
			add{attachEvent("onfilterchange",value);}
			remove{detachEvent("onfilterchange",value);}
		}
		public event EHVoid onfocus{
			add{attachEvent("onfocus",value);}
			remove{detachEvent("onfocus",value);}
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
		public event EHVoid onlayoutcomplete{
			add{attachEvent("onlayoutcomplete",value);}
			remove{detachEvent("onlayoutcomplete",value);}
		}
		public event EHVoid onlosecapture{
			add{attachEvent("onlosecapture",value);}
			remove{detachEvent("onlosecapture",value);}
		}
		public event EHVoid onmousedown{
			add{attachEvent("onmousedown",value);}
			remove{detachEvent("onmousedown",value);}
		}
		public event EHVoid onmouseenter{
			add{attachEvent("onmouseenter",value);}
			remove{detachEvent("onmouseenter",value);}
		}
		public event EHVoid onmouseleave{
			add{attachEvent("onmouseleave",value);}
			remove{detachEvent("onmouseleave",value);}
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
		public event EHVoid onmove{
			add{attachEvent("onmove",value);}
			remove{detachEvent("onmove",value);}
		}
		public event EHVoid onmoveend{
			add{attachEvent("onmoveend",value);}
			remove{detachEvent("onmoveend",value);}
		}
		public event EHCancel onmovestart{
			add{attachEvent("onmovestart",value);}
			remove{detachEvent("onmovestart",value);}
		}
		public event EHVoid onpage{
			add{attachEvent("onpage",value);}
			remove{detachEvent("onpage",value);}
		}
		public event EHCancel onpaste{
			add{attachEvent("onpaste",value);}
			remove{detachEvent("onpaste",value);}
		}
		public event EHVoid onpropertychange{
			add{attachEvent("onpropertychange",value);}
			remove{detachEvent("onpropertychange",value);}
		}
		public event EHVoid onreadystatechange{
			add{attachEvent("onreadystatechange",value);}
			remove{detachEvent("onreadystatechange",value);}
		}
		public event EHVoid onresizeend{
			add{attachEvent("onresizeend",value);}
			remove{detachEvent("onresizeend",value);}
		}
		public event EHVoid onresize{
			add{attachEvent("onresize",value);}
			remove{detachEvent("onresize",value);}
		}
		public event EHCancel onresizestart{
			add{attachEvent("onresizestart",value);}
			remove{detachEvent("onresizestart",value);}
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
		public event EHVoid onscroll{
			add{attachEvent("onscroll",value);}
			remove{detachEvent("onscroll",value);}
		}
		public event EHCancel onselectstart{
			add{attachEvent("onselectstart",value);}
			remove{detachEvent("onselectstart",value);}
		}

		// HTMLElementEvents2
		public event EHCancel oncontrolselect{
			add{attachEvent("oncontrolselect",value);}
			remove{detachEvent("oncontrolselect",value);}
		}
	}
	#endregion

	//================================================================
	//	cls:DatabindingElement
	//================================================================
	public partial class DatabindingElement:Element{
		protected DatabindingElement(object instance):base(instance){}
		public static new DatabindingElement FromObj(object obj){
			if(obj==null)return null;
			return new DatabindingElement(obj);
		}
	}
	public partial class DatabindingElement{
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
	}
}