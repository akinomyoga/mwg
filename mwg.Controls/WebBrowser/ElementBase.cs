using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"
	//================================================================
	//	cls:DomNode
	//================================================================
	// IHTMLDOMNode
	// IHTMLDOMNode2
	public partial class DomNode:MshtmlObject{
		//#>>delete
		protected DomNode(object instance):base(instance){}
		public static DomNode FromObj(object instance){
			if(instance==null)return null;
			try{
				switch((int)GetProperty(instance,"nodeType")){
					case 1:		return Element.FromObj(instance);
					case 3:		return DomText.FromObj(instance);
					default:	return new DomNode(instance);
				}
			}catch(System.Exception e){
				throw new System.ArgumentException("指定したオブジェクトは DomNode ではありません。","instance",e);
			}
		}
		//#<<delete
		//------------------------------------------------------------
		//	IHTMLDOMNode
		//------------------------------------------------------------
		//#>>delete
		public DomNode appendChild(DomNode node)	{return DomNode.FromObj(this.Invoke("appendChild",node));}
		public DomNode cloneNode(bool deep)			{return DomNode.FromObj(this.Invoke("cloneNode",deep));}
		public bool hasChildNodes()					{return (bool)this.Invoke("hasChildNodes");}
		public DomNode insertBefore(DomNode node,DomNode refChild){
			return DomNode.FromObj(this.Invoke("insertBefore",node,refChild));
		}
		public DomNode removeChild(DomNode node)	{return DomNode.FromObj(this.Invoke("removeChild",node));}
		public DomNode removeNode(bool deep)		{return DomNode.FromObj(this.Invoke("removeNode",deep));}
		public DomNode replaceChild(DomNode newChild,DomNode refChild){
			return DomNode.FromObj(this.Invoke("replaceChild",newChild,refChild));
		}
		public DomNode replaceNode(DomNode node)	{return DomNode.FromObj(this.Invoke("replaceNode",node));}
		public DomNode swapNode(DomNode node)		{return DomNode.FromObj(this.Invoke("swapNode",node));}
		public Array attributes{
			get{return new Array(this.ownerDocument.parentWindow,this["attributes"]);}
		}
		//#<<delete
		//#PROPO_R<DomNodeCollection,childNodes>
		//#PROPO_R<DomNode,firstChild>
		//#PROPO_R<DomNode,lastChild>
		//#PROPO_R<DomNode,nextSibling>
		//#PROP_R<string,nodeName>
		//#PROP_R<int,nodeType>
		//#PROP_R<object,nodeValue>
		//#PROPO_R<DomNode,parentNode>
		//#PROPO_R<DomNode,previousSibling>
		//------------------------------------------------------------
		//	IHTMLDOMNode2
		//------------------------------------------------------------
		//#PROPO_R<Document,ownerDocument>
	}

	//================================================================
	//	cls:DomAttribute
	//================================================================
	// HTMLDOMAttribute
	// DispHTMLDOMAttribute
	// IHTMLDOMAttribute
	// IHTMLDOMAttribute2
	//#CLASS_NEW<DomAttribute,DomNode>
	public partial class DomAttribute{
		//#PROP_R<bool,specified>
		//#PROP_R<bool,expando>
		//#PROP_R<string,name>
		//#PROP<string,value>
	}

	//================================================================
	//	cls:DomText
	//================================================================
	// IHTMLDOMTextNode
	// IHTMLDOMTextNode2
	//#CLASS_NEW<DomText,DomNode>
	public partial class DomText{
		//#>>delete
		//------------------------------------------------------------
		// IHTMLDOMTextNode
		//------------------------------------------------------------
		public DomNode splitText(int offset){
			return DomNode.FromObj(this.Invoke("splitText",offset));
		}
		public string toString(){
			return (string)this.Invoke("toString");
		}
		public string data{
			get{return (string)this["data"];}
			set{this["data"]=value;}
		}
		public int length{
			get{return (int)this["length"];}
		}
		//------------------------------------------------------------
		// IHTMLDOMTextNode2
		//------------------------------------------------------------
		public void appendData(string data)				{this.Invoke("appendData",data);}
		public void deleteData(int offset,int len)		{this.Invoke("deleteData",offset,len);}
		public void insertData(int offset,string data)	{this.Invoke("insertData",offset,data);}
		public void replaceData(int offset,int len,string newdata){
			this.Invoke("replaceData",offset,len,newdata);
		}
		public string substringData(int offset,int len)	{return (string)this.Invoke("substringData",offset,len);}
		//#<<delete
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
		//#>>delete
		protected Element(object instance):base(instance){}
		public static new Element FromObj(object instance){
			if(instance==null)return null;
			try{
				string tagUrn=(string)GetProperty(instance,"tagUrn");
				string tagName=(string)GetProperty(instance,"tagName");
				if(tagUrn==null||tagUrn=="")switch(tagName.ToLower()){
					case "!":			return CommentElement.FromObj(instance);
					case "a":			return AElement.FromObj(instance);
					case "area":		return AreaElement.FromObj(instance);
					case "base":		return BaseElement.FromObj(instance);
					case "basefont":	return BaseFontElement.FromObj(instance);
					case "bgsound":		return BgsoundElement.FromObj(instance);
					case "address":case "pre":case "center":
					case "listing":case "xmp":case "plaintext":
					case "blockquote":	return BlockElement.FromObj(instance);
					case "body":		return BodyElement.FromObj(instance);
					case "br":			return BrElement.FromObj(instance);
					case "button":		return ButtonElement.FromObj(instance);
					case "dd":			return DdElement.FromObj(instance);
					case "div":			return DivElement.FromObj(instance);
					case "dl":			return DlElement.FromObj(instance);
					case "dt":			return DtElement.FromObj(instance);
					case "embed":		return EmbedElement.FromObj(instance);
					case "fieldset":	return FieldsetElement.FromObj(instance);
					case "font":		return FontElement.FromObj(instance);
					case "form":		return FormElement.FromObj(instance);
					case "frame":		return FrameElement.FromObj(instance);
					case "frameset":	return FramesetElement.FromObj(instance);
					case "head":		return HeadElement.FromObj(instance);
					case "h1":case "h2":case "h3":
					case "h4":case "h5":case "h6":
										return HnElement.FromObj(instance);
					case "hr":			return HrElement.FromObj(instance);
					case "html":		return HtmlElement.FromObj(instance);
					case "iframe":		return IframeElement.FromObj(instance);
					case "img":			return ImgElement.FromObj(instance);
					case "input":		return InputElement.FromObj(instance);
					case "isindex":		return IsindexElement.FromObj(instance);
					case "legend":		return LegendElement.FromObj(instance);
					case "label":		return LabelElement.FromObj(instance);
					case "li":			return LiElement.FromObj(instance);
					case "link":		return LinkElement.FromObj(instance);
					case "map":			return MapElement.FromObj(instance);
					case "marquee":		return MarqueeElement.FromObj(instance);
					case "meta":		return MetaElement.FromObj(instance);
					case "nextid":		return NextidElement.FromObj(instance);
					case "noembed":
					case "noframes":
					case "nolayer":
					case "noscript":	return NoshowElement.FromObj(instance);
					case "applet":
					case "object":		return ObjectElement.FromObj(instance);
					case "ol":			return OlElement.FromObj(instance);
					case "optgroup":
					case "option":		return OptionElement.FromObj(instance);
					case "p":			return PElement.FromObj(instance);
					case "param":		return ParamElement.FromObj(instance);
					case "i":case "u":case "b":case "q":case "s":
					case "strong":case "del":case "strike":case "em":case "small":case "big":
					case "ruby":case "rp":case "sub":case "sup":
					case "acronym":case "bdo":case "cite":case "dfn":case "ins":
					case "code":case "kbd":case "samp":case "var":
					case "nobr":		return PhraseElement.FromObj(instance);
					case "script":		return ScriptElement.FromObj(instance);
					case "select":		return SelectElement.FromObj(instance);
					case "span":		return SpanElement.FromObj(instance);
					case "style":		return StyleElement.FromObj(instance);
					case "caption":		return CaptionElement.FromObj(instance);
					case "td":
					case "th":			return TableCellElement.FromObj(instance);
					case "table":		return TableElement.FromObj(instance);
					case "colgroup":
					case "col":			return ColElement.FromObj(instance);
					case "tr":			return TrElement.FromObj(instance);
					case "thead":
					case "tbody":
					case "tfoot":		return TableSectionElement.FromObj(instance);
					case "textarea":	return TextAreaElement.FromObj(instance);
					case "wbr": return TextElement.FromObj(instance);
					case "title":		return TitleElement.FromObj(instance);
					case "dir":case "menu":
					case "ul":			return UlElement.FromObj(instance);
					// optionbutton ?
					// spanflow ?
					// default:			return UnknownElement.FromObj(instance);
				}
			}catch{
				// IHTMLElement でない可能性
			}

			return new Element(instance);
		}
		//============================================================
		//		Methods
		//============================================================
		// IHTMLElement
		//------------------------------------------------------------
		public void scrollIntoView(bool top)	{this.Invoke("scrollIntoView",top);}
		public void click()						{this.Invoke("click");}
		public bool contains(Element child)		{return (bool)this.Invoke("contains",child.instance);}
		public object getAttribute(string attrName,int flags){
			return this.Invoke("getAttribute",attrName,flags);
		}
		public void removeAttribute(string attrName,int flags){
			this.Invoke("removeAttribute",attrName,flags);
		}
		public void setAttribute(string attrName,object value,int flags){
			this.Invoke("setAttribute",attrName,value,flags);
		}
		public void insertAdjacentHTML(string where,string html){
			this.Invoke("insertAdjacentHTML",where,html);
		}
		public void insertAdjacentText(string where,string text){
			this.Invoke("insertAdjacentText",where,text);
		}
		public string toString(){
			return (string)this.Invoke("toString");
		}
		//------------------------------------------------------------
		// IHTMLElement2
		//------------------------------------------------------------
		// addBehavior
		public CollectionBase<Rect> getClientRects()	{return CollectionBase<Rect>.FromObj(this.Invoke("getClientRects"));}
		public void addFilter(object pUnk)				{this.Invoke("addFilter",pUnk);}
		public Element applyElement(Element elem,string where){
			return Element.FromObj(this.Invoke("applyElement",elem.instance,where));
		}
		public bool attachEvent(string eventName,ScriptObject proc){
			return (bool)this.Invoke("attachEvent",eventName,proc.Value);
		}
		public void blur()								{this.Invoke("blur");}
		public void clearAttributes()					{this.Invoke("clearAttributes");}
		public string componentFromPoint(int x,int y)	{return (string)this.Invoke("componentFromPoint",x,y);}
		public object createControlRange()				{return this.Invoke("createControlRange");}
		public void detachEvent(string eventName,ScriptObject proc){
			this.Invoke("detachEvent",eventName,proc.Value);
		}
		public void doScroll(object component)			{this.Invoke("doScroll",component);}
		public void focus()								{this.Invoke("focus");}
		public string getAdjacentText(string where)		{return (string)this.Invoke("getAdjacentText",where);}
		public Rect getBoundingClientRect()				{return Rect.FromObj(this.Invoke("getBoundingClientRect"));}
		public ElementCollection getElementsByTagName(string tagName){
			return ElementCollection.FromObj(this.Invoke("getElementsByTagName",tagName));
		}
		public object getExpression(string prop)		{return (string)this.Invoke("getExpression",prop);}
		public Element insertAdjacentElement(string where,Element elem){
			return Element.FromObj(this.Invoke("insertAdjacentElement",where,elem));
		}
		public void mergeAttributes(Element elem)		{this.Invoke("mergeAttributes",elem);}
		public void releaseCapture()					{this.Invoke("releaseCapture");}
		public bool removeBehavior(int cookie)			{return (bool)this.Invoke("removeBehavior",cookie);}
		public bool removeExpression(string prop)		{return (bool)this.Invoke("removeExpression",prop);}
		public void removeFilter(object pUnk)			{this.Invoke("removeFilter",pUnk);}
		public string replaceAdjacentText(string where,string newText){
			return (string)this.Invoke("replaceAdjacentText",where,newText);
		}
		public void setCapture(bool containerCapt)		{this.Invoke("setCapture",containerCapt);}
		public void setExpression(string prop,string expression,string lang){
			this.Invoke("setExpression",prop,expression,lang);
		}
		//------------------------------------------------------------
		public bool attachEvent(string eventName,System.Delegate proc){
			return this.attachEvent(eventName,this.document.parentWindow.ToScriptObject(proc));
		}
		public void detachEvent(string eventName,System.Delegate proc){
			this.detachEvent(eventName,this.document.parentWindow.ToScriptObject(proc));
		}
		//------------------------------------------------------------
		// IHTMLElement3
		//------------------------------------------------------------
		public bool dragDrop()							{return (bool)this.Invoke("dragDrop");}
		/// <summary>
		/// 指定したイベントを発生させます。
		/// </summary>
		/// <param name="eventName">発火させるイベントの名前を指定します。</param>
		/// <param name="e">document.createEventObject によって作成された Event インスタンスを指定します。</param>
		/// <returns>イベントの発火に成功したか否かを返します。成功した場合に true を、それ以外の場合に false を返します。</returns>
		public bool FireEvent(string eventName,Event e){
			return (bool)this.Invoke("FireEvent",eventName,e);
		}
		public void mergeAttributes(Element merge,object Flags){
			this.Invoke("mergeAttributes",merge,Flags);
		}
		public void setActive()							{this.Invoke("setActive");}
		//------------------------------------------------------------
		// IHTMLElement3
		//------------------------------------------------------------
		public DomAttribute getAttributeNode(string name){
			return DomAttribute.FromObj(this.Invoke("getAttributeNode",name));
		}
		public void normalize()							{this.Invoke("normalize");}
		public DomAttribute removeAttributeNode(DomAttribute attr){
			return DomAttribute.FromObj(this.Invoke("removeAttributeNode",attr));
		}
		public DomAttribute setAttributeNode(DomAttribute attr){
			return DomAttribute.FromObj(this.Invoke("setAttributeNode",attr));
		}
		//============================================================
		//		Properties
		//============================================================
		public Document document{
			get{return Document.FromObj(this["document"]);}
		}
		//#<<delete
		//==================================================
		//		Properties
		//==================================================
		//		IHTMLElement
		//--------------------------------------------------
		//#PROP<ElementCollection,all>
		//#PROP<ElementCollection,children>
		//#PROP<string,className>
		//#PROPO_R<NAMED_COLLEC(MshtmlObject),filters>
		//#PROP<string,id>
		//#PROP<string,innerHTML>
		//#PROP<string,innerText>
		//#PROP_R<bool,isTextEdit>
		//#PROP<string,lang>
		//#PROP<string,language>
		//#PROP_R<int,offsetHeight>
		//#PROP_R<int,offsetLeft>
		//#PROPO_R<Element,offsetParent>
		//#PROP_R<int,offsetTop>
		//#PROP_R<int,offsetWidth>
		//#PROP<string,outerHTML>
		//#PROP<string,outerText>
		//#PROPO_R<Element,parentElement>
		//#PROPO_R<Element,parentTextEdit>
		// recordNumber
		//#PROP_R<int,sourceIndex>
		//#PROPO_R<Style,style>
		//#PROP_R<string,tagName>
		//#PROP<string,title>
		//--------------------------------------------------
		//		IHTMLElement2
		//--------------------------------------------------
		//#PROP<string,accessKey>
		//#PROP_R<BehaviorUrns,behaviorUrns>
		//#PROP_R<bool,canHaveChildren>
		//#PROP_R<int,clientHeight>
		//#PROP_R<int,clientLeft>
		//#PROP_R<int,clientTop>
		//#PROP_R<int,clientWidth>
		//#PROP_R<CurrentStyle,currentStyle>
		//#PROP<string,dir>
		// readyState
		//#PROP_R<int,readyStateValue>
		//#PROPO_R<Style,runtimeStyle>
		//#PROP_R<string,scopeName>
		//#PROP_R<int,scrollHeight>
		//#PROP_R<int,scrollLeft>
		//#PROP_R<int,scrollTop>
		//#PROP_R<int,scrollWidth>
		//#PROP<short,tabIndex>
		//#PROP<string,tagUrn>
		//--------------------------------------------------
		//		IHTMLElement3
		//--------------------------------------------------
		//#PROP_R<bool,canHaveHTML>
		//#PROP<string,contentEditable>
		//#PROP<bool,disabled>
		//#PROP_R<int,glyphMode>
		//#PROP<bool,hideFocus>
		//#PROP<bool,inflateBlock>
		//#PROP_R<bool,isContentEditable>
		//#PROP_R<bool,isDisabled>
		//#PROP_R<bool,isMultiLine>
		//--------------------------------------------------
		//		IHTMLElement4
		//--------------------------------------------------
		// --
		//--------------------------------------------------
		//		IHTMLElementDefaults
		//--------------------------------------------------
		//#PROP<bool,frozen>
		//#PROP<int,scrollSegmentX>
		//#PROP<int,scrollSegmentY>
		//#PROP<bool,tabStop>
		//#PROP<bool,viewInheritStyle>
		//#PROPO<Document,viewLink>
		//#PROP<bool,viewMasterTab>
		//--------------------------------------------------
		//		IHTMLUniqueName
		//--------------------------------------------------
		//#PROP_R<string,uniqueID>
		//#PROP_R<int,uniqueNumber>
		//==================================================
		//		Events
		//==================================================
		//#EVENT<EHVoid,onactivate>
		//#EVENT<EHVoid,onafterupdate>
		//#EVENT<EHCancel,onbeforeactivate>
		//#EVENT<EHCancel,onbeforecopy>
		//#EVENT<EHCancel,onbeforecut>
		//#EVENT<EHCancel,onbeforedeactivate>
		//#EVENT<EHVoid,onbeforeeditfocus>
		//#EVENT<EHCancel,onbeforepaste>
		//#EVENT<EHCancel,onbeforeupdate>
		//#EVENT<EHVoid,onblur>
		//#EVENT<EHVoid,oncellchange>
		//#EVENT<EHCancel,onclick>
		//#EVENT<EHCancel,oncontextmenu>
		//#EVENT<EHCancel,oncopy>
		//#EVENT<EHCancel,oncut>
		//#EVENT<EHVoid,ondataavailable>
		//#EVENT<EHVoid,ondatasetchanged>
		//#EVENT<EHVoid,ondatasetcompleted>
		//#EVENT<EHCancel,ondblclick>
		//#EVENT<EHVoid,ondeactivate>
		//#EVENT<EHCancel,ondrag>
		//#EVENT<EHVoid,ondragend>
		//#EVENT<EHCancel,ondragenter>
		//#EVENT<EHVoid,ondragleave>
		//#EVENT<EHCancel,ondragover>
		//#EVENT<EHCancel,ondragstart>
		//#EVENT<EHCancel,ondrop>
		//#EVENT<EHCancel,onerrorupdate>
		//#EVENT<EHVoid,onfilterchange>
		//#EVENT<EHVoid,onfocus>
		//#EVENT<EHVoid,onfocusin>
		//#EVENT<EHVoid,onfocusout>
		//#EVENT<EHCancel,onhelp>
		//#EVENT<EHVoid,onkeydown>
		//#EVENT<EHCancel,onkeypress>
		//#EVENT<EHVoid,onkeyup>
		//#EVENT<EHVoid,onlayoutcomplete>
		//#EVENT<EHVoid,onlosecapture>
		//#EVENT<EHVoid,onmousedown>
		//#EVENT<EHVoid,onmouseenter>
		//#EVENT<EHVoid,onmouseleave>
		//#EVENT<EHVoid,onmousemove>
		//#EVENT<EHVoid,onmouseout>
		//#EVENT<EHVoid,onmouseover>
		//#EVENT<EHVoid,onmouseup>
		//#EVENT<EHCancel,onmousewheel>
		//#EVENT<EHVoid,onmove>
		//#EVENT<EHVoid,onmoveend>
		//#EVENT<EHCancel,onmovestart>
		//#EVENT<EHVoid,onpage>
		//#EVENT<EHCancel,onpaste>
		//#EVENT<EHVoid,onpropertychange>
		//#EVENT<EHVoid,onreadystatechange>
		//#EVENT<EHVoid,onresizeend>
		//#EVENT<EHVoid,onresize>
		//#EVENT<EHCancel,onresizestart>
		//#EVENT<EHVoid,onrowenter>
		//#EVENT<EHCancel,onrowexit>
		//#EVENT<EHVoid,onrowsdelete>
		//#EVENT<EHVoid,onrowsinserted>
		//#EVENT<EHVoid,onscroll>
		//#EVENT<EHCancel,onselectstart>

		// HTMLElementEvents2
		//#EVENT<EHCancel,oncontrolselect>
	}
	#endregion

	//================================================================
	//	cls:DatabindingElement
	//================================================================
	//#CLASS_NEW<DatabindingElement,Element>
	public partial class DatabindingElement{
		//------------------------------------------------------------
		//	IHTMLDatabinding
		//------------------------------------------------------------
		//#PROP<string,dataFld>
		//#PROP<string,dataFormatAs>
		//#PROP<string,dataSrc>
	}
}