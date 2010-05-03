using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"

	/// <summary>
	/// HtmlDocument オブジェクトをラップします。
	/// </summary>
	//#CLASS<Document,MshtmlObject>
	public partial class Document{
		//#>>delete
		public Document(Forms::HtmlDocument doc):base(doc.DomDocument){}
		//#<<delete
		//--------------------------------------------------
		//		IHTMLDocument
		//--------------------------------------------------
		//#PROP_R<object,Script>
		//--------------------------------------------------
		//		IHTMLDocument2
		//--------------------------------------------------
		//#>>delete
		public void clear(){
			this.Invoke("clear");
		}
		public void close(){
			this.Invoke("close");
		}
		public Element createElement(string tagName){
			return Element.FromObj(this.Invoke("createElement",tagName));
		}
		public StyleSheet createStyleSheet(string href,int index){
			return StyleSheet.FromObj(this.Invoke("createStyleSheet",href,index));
		}
		public Element elementFromPoint(int x,int y){
			return Element.FromObj(this.Invoke("elementFromPoint",x,y));
		}
		public bool execCommand(string cmdId,bool showUi,object value){
			return (bool)this.Invoke("execCommand",cmdId,showUi,value);
		}
		public bool execCommandShowHelp(string cmdId){
			return (bool)this.Invoke("execCommandShowHelp",cmdId);
		}
		public MshtmlObject open(string url,string target,string features,bool replace){
			object ret=this.Invoke("open",url,target,features,replace);
			try{
				if(null!=ret.GetType().GetInterface("DispHTMLDocument"))
					return Document.FromObj(url);
			}catch{}

			try{
				if(null!=ret.GetType().GetInterface("DispHTMLWindow2"))
					return Window.FromObj(url);
			}catch{}

			return new MshtmlObject(ret);
		}
		public bool queryCommndEnabled(string cmdId){
			return (bool)this.Invoke("queryCommndEnabled",cmdId);
		}
		public bool queryCommndIndeterm(string cmdId){
			return (bool)this.Invoke("queryCommndIndeterm",cmdId);
		}
		public bool queryCommndState(string cmdId){
			return (bool)this.Invoke("queryCommndState",cmdId);
		}
		public bool queryCommndSupported(string cmdId){
			return (bool)this.Invoke("queryCommndSupported",cmdId);
		}
		public string queryCommndText(string cmdId){
			return (string)this.Invoke("queryCommndText",cmdId);
		}
		public object queryCommndValue(string cmdId){
			return (object)this.Invoke("queryCommndValue",cmdId);
		}
		public string toString(){
			return (string)this.Invoke("toString");
		}
		public void write(params object[] array){
			this.Invoke("write",array);
		}
		public void writeln(params object[] array){
			this.Invoke("writeln",array);
		}
		//#<<delete
		//#PROPO_R<Element,activeElement>
		//#PROP<object,alinkColor>
		//#PROPO_R<ElementCollection,all>
		//#PROPO_R<ElementCollection,anchors>
		//#PROPO_R<ElementCollection,applets>
		//#PROP<object,bgColor>
		//#PROPO_R<BodyElement,body>
		//#PROP<string,charset>
		//#PROP<string,cookie>
		//#PROP<string,defaultCharset>
		//#PROP<string,designMode>
		//#PROP<string,domain>
		//#PROPO_R<ElementCollection,embeds>
		//#PROP<bool,expando>
		//#PROP<object,fgColor>
		//#PROP_R<string,fileCreatedDate>
		//#PROP_R<string,fileModifiedData>
		//#PROP_R<string,fileSize>
		//#PROP_R<string,fileUpdatedDate>
		//#PROPO_R<ElementCollection,forms>
		//#PROPO_R<RAW("#CollectionBase<FrameElement>#"),frames>
		//#PROPO_R<ElementCollection,images>
		//#PROP_R<string,lastModified>
		//#PROP<object,linkColor>
		//#PROPO_R<ElementCollection,links>
		//#PROPO_R<Location,location>
		//#PROP_R<string,mimeType>
		//#PROP_R<string,nameProp>
		//#PROPO_R<Window,parentWindow>
		//#PROPO_R<ElementCollection,plugins>
		//#PROP_R<string,protocol>
		//#PROP_R<string,readyState>
		//#PROP_R<string,referer>
		//#PROPO_R<ElementCollection,scripts>
		//#PROP_R<string,security>
		//#PROPO_R<Selection,selection>
		//#PROPO_R<RAW("#CollectionBase<StyleSheet>#"),styleSheets>
		//#PROP<string,title>
		//#PROP<string,url>
		//#PROP<object,vlinkColor>
		//--------------------------------------------------
		//		IHTMLDocument3
		//--------------------------------------------------
		//#>>delete
		public bool attachEvent(string eventName,ScriptObject proc){
			return (bool)this.Invoke("attachEvent",eventName,proc.Value);
		}
		public void detachEvent(string eventName,ScriptObject proc){
			this.Invoke("detachEvent",eventName,proc.Value);
		}
		public bool attachEvent(string eventName,System.Delegate proc){
			return this.attachEvent(eventName,this.parentWindow.ToScriptObject(proc));
		}
		public void detachEvent(string eventName,System.Delegate proc){
			this.detachEvent(eventName,this.parentWindow.ToScriptObject(proc));
		}
		public Document createDocumentFragment(){
			return Document.FromObj(this.Invoke("createDocumentFragment"));
		}
		public DomText createTextNode(string text){
			return DomText.FromObj(this.Invoke("createTextNode",text));
		}
		public Element getElementById(string id){
			return Element.FromObj(this.Invoke("getElementById",id));
		}
		public ElementCollection getElementsByName(string name){
			return ElementCollection.FromObj(this.Invoke("getElementsByName",name));
		}
		public ElementCollection getElementsByTagName(string tagName){
			return ElementCollection.FromObj(this.Invoke("getElementsByTagName",tagName));
		}
		public void recalc(bool force){
			this.Invoke("recalc",force);
		}
		public void releaseCapture(){
			this.Invoke("releaseCapture");
		}
		//#<<delete
		//#PROP<string,baseUrl>
		//#PROP_R<object,childNodes>
		//#PROP<string,dir>
		//#PROPO_R<Element,documentElement>
		//#PROP<bool,enableDownload>
		//#PROP<bool,inheritStyleSheets>
		//#PROPO_R<Document,parentDocument>
		//#PROP_R<string,uniqueID>
		//--------------------------------------------------
		//		IHTMLDocument4
		//--------------------------------------------------
		//#>>delete
		public Document createDocumentFromUrl(string url,string options){
			return Document.FromObj(this.Invoke("createDocumentFromUrl",url,options));
		}
		public Event CreateEventObject(Event e){
			return Event.FromObj(this.Invoke("CreateEventObject",e));
		}
		public RenderStyle createRenderStyle(string v){
			return RenderStyle.FromObj(this.Invoke("createRenderStyle",v));
		}
		public bool FireEvent(string eventName,Event e){
			return (bool)this.Invoke("FireEvent",eventName,e);
		}
		public void focus(){
			this.Invoke("focus");
		}
		public bool hasFocus(){
			return (bool)this.Invoke("focus");
		}
		//#<<delete
		//#PROP<string,media>
		//#PROP_R<object,namespaces>
		//#PROP_R<string,URLUnencoded>
		//--------------------------------------------------
		//		IHTMLDocument5
		//--------------------------------------------------
		//#>>delete
		public DomAttribute createAttribute(string name){
			return DomAttribute.FromObj(this.Invoke("createAttribute",name));
		}
		public CommentElement createComment(string text){
			return CommentElement.FromObj(this.Invoke("createComment",text));
		}
		//#<<delete
		//#PROP_R<string,compatMode>
		//#PROPO_R<DomNode,doctype>
		//#PROPO_R<DomImplementation,implementation>
		//--------------------------------------------------
		//		Events
		//--------------------------------------------------
		//#EVENT<EHVoid,onactivate>
		//#EVENT<EHVoid,onafterupdate>
		//#EVENT<EHCancel,onbeforeactivate>
		//#EVENT<EHCancel,onbeforedeactivate>
		//#EVENT<EHVoid,onbeforeeditfocus>
		//#EVENT<EHCancel,onbeforeupdate>
		//#EVENT<EHVoid,oncellchange>
		//#EVENT<EHCancel,onclick>
		//#EVENT<EHCancel,oncontextmenu>
		//#EVENT<EHCancel,oncontrolselect>
		//#EVENT<EHVoid,ondataavailable>
		//#EVENT<EHVoid,ondatasetchanged>
		//#EVENT<EHVoid,ondatasetcompleted>
		//#EVENT<EHCancel,ondblclick>
		//#EVENT<EHVoid,ondeactivate>
		//#EVENT<EHCancel,ondragstart>
		//#EVENT<EHCancel,onerrorupdate>
		//#EVENT<EHVoid,onfocusin>
		//#EVENT<EHVoid,onfocusout>
		//#EVENT<EHCancel,onhelp>
		//#EVENT<EHVoid,onkeydown>
		//#EVENT<EHCancel,onkeypress>
		//#EVENT<EHVoid,onkeyup>
		//#EVENT<EHVoid,onmousedown>
		//#EVENT<EHVoid,onmousemove>
		//#EVENT<EHVoid,onmouseout>
		//#EVENT<EHVoid,onmouseover>
		//#EVENT<EHVoid,onmouseup>
		//#EVENT<EHCancel,onmousewheel>
		//#EVENT<EHVoid,onpropertychange>
		//#EVENT<EHVoid,onreadystatechange>
		//#EVENT<EHVoid,onrowenter>
		//#EVENT<EHCancel,onrowexit>
		//#EVENT<EHVoid,onrowsdelete>
		//#EVENT<EHVoid,onrowsinserted>
		//#EVENT<EHVoid,onselectionchange>
		//#EVENT<EHCancel,onselectstart>
		//#EVENT<EHCancel,onstop>
	}
}