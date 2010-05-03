using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;
using CM=System.ComponentModel;

namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"
	//================================================================
	//	cls:Window
	//================================================================
	// HTMLWindow2
	// DispHTMLWindow2
	// HTMLWindowEvent_Event
	// HTMLWindowEvent2_Event
	// IHTMLWindow
	// IHTMLWindow2
	// IHTMLWindow3
	// IHTMLWindow4
	public partial class Window:MshtmlObject{
		//#>>delete
		static Gen::Dictionary<object,Window> instances=new Gen::Dictionary<object,Window>();
		/// <summary>
		/// 実際の COM のインスタンスから Window クラスを生成します。
		/// </summary>
		/// <param name="obj">COM の HTMLWindow インスタンスを指定します。</param>
		/// <returns>引数に渡した obj に対応して作成した Window インスタンスを作成します。</returns>
		internal static Window FromObj(object obj){
			if(obj==null)return null;
			Window ret;
			if(!instances.TryGetValue(obj,out ret)){
				ret=new Window(obj);
				ret.onunload+=delegate(){instances.Remove(obj);};
				instances.Add(obj,ret);
			}
			return ret;
		}
		private Window(object instance):base(instance){
			InitExecution();
		}
		/// <summary>
		/// System.Windows.Forms.HtmlWindow インスタンスから Window インスタンスを初期化します。
		/// </summary>
		/// <param name="win">基本となる HtmlWindow インスタンスを指定します。</param>
		public Window(Forms::HtmlWindow win):base(win.DomWindow){
			InitExecution();
		}
		//#<<delete
		//============================================================
		//		Events
		//============================================================
		//#EVENT<EHVoid,onafterprint>
		//#EVENT<EHVoid,onbeforeprint>
		//#EVENT<EHVoid,onbeforeunload>
		//#EVENT<EHVoid,onblur>
		//#EVENT<EHError,onerror>
		//#EVENT<EHVoid,onfocus>
		//#EVENT<EHCancel,onhelp>
		//#EVENT<EHVoid,onload>
		//#EVENT<EHVoid,onresize>
		//#EVENT<EHVoid,onscroll>
		//#EVENT<EHVoid,onunload>
		//============================================================
		//		Methods
		//============================================================
		//#>>delete
		public bool attachEvent(string eventName,ScriptObject proc){
			return (bool)this.Invoke("attachEvent",eventName,proc.Value);
		}
		public bool attachEvent(string eventName,System.Delegate proc){
			return this.attachEvent(eventName,this.ToScriptObject(proc));
		}
		public void detachEvent(string eventName,ScriptObject proc){
			this.Invoke("detachEvent",eventName,proc.Value);
		}
		public void detachEvent(string eventName,System.Delegate proc){
			this.detachEvent(eventName,this.ToScriptObject(proc));
		}
		public int setInterval(ScriptObject proc,int interval_msec,string lang){
			return (int)this.Invoke("setInterval",proc.Value,interval_msec,lang);
		}
		public int setInterval(EHVoid proc,int interval_msec){
			return this.setInterval(this.ToScriptObject(proc),interval_msec,null);
		}
		public int setInterval(string code,int interval_msec,string lang){
			return (int)this.Invoke("setInterval",code,interval_msec,lang??"javascript");
		}
		public int setInterval(string code,int interval_msec){
			return this.setInterval(code,interval_msec,null);
		}
		public int setTimeout(ScriptObject proc,int time_msec,string lang) {
			return (int)this.Invoke("setTimeout",proc.Value,time_msec,lang);
		}
		public int setTimeout(EHVoid proc,int time_msec){
			return this.setTimeout(this.ToScriptObject(proc),time_msec,null);
		}
		public int setTimeout(string code,int time_msec,string lang){
			return (int)this.Invoke("setTimeout",code,time_msec,lang??"javascript");
		}
		public int setTimeout(string code,int time_msec){
			return this.setTimeout(code,time_msec,null);
		}
		//------------------------------------------------------------
		public void alert(string message)		{this.Invoke("alert",message);}
		public void blur()						{this.Invoke("blur");}
		public void clearInterval(int id)		{this.Invoke("clearInterval",id);}
		public void clearTimeout(int id)		{this.Invoke("clearTimeout",id);}
		public void close()						{this.Invoke("close");}
		public bool comfirm(string message)		{return (bool)this.Invoke("confirm",message);}
		public object createPopup(ref object val){
			throw new System.NotImplementedException();
		}
		public object execScript(string code,string lang){return this.Invoke("execScript",code,lang);}
		public object execScript(string code)	{return this.Invoke("execScript",code,"javascript");}
		public void focus()						{this.Invoke("focus");}
		public object item(ref object index){
			throw new System.NotImplementedException();
		}
		public void moveBy(int dx,int dy)		{this.Invoke("moveBy",dx,dy);}
		public void moveTo(int x,int y)			{this.Invoke("moveTo",x,y);}
		public void navigate(string url)		{this.Invoke("navigate",url);}
		public void open(string url,string name,string features,bool replace){
			this.Invoke("open",url,name,features,replace);
		}
		public void print()						{this.Invoke("print");}
		public object prompt(string message,string text){return this.Invoke("prompt",message,text);}
		public void resizeBy(int dx,int dy)		{this.Invoke("resizeBy",dx,dy);}
		public void resizeTo(int x,int y)		{this.Invoke("resizeTo",x,y);}
		public void scroll(int x,int y)			{this.Invoke("scroll",x,y);}
		public void scrollBy(int dx,int dy)		{this.Invoke("scrollBy",dx,dy);}
		public void scrollTo(int x,int y)		{this.Invoke("scrollTo",x,y);}
		public void showHelp(string url,object arg,string features){this.Invoke("showHelp",url,arg,features);}
		public object showModalDialog(string dialog,ref object varArgIn,ref object varOptions){
			throw new System.NotImplementedException();
		}
		public Window showModelessDialog(string url,ref object varArgIn,ref object options){
			throw new System.NotImplementedException();
		}
		public string toString()				{return (string)this.Invoke("toString");}
		//#<<delete
		//============================================================
		//		Property
		//============================================================
		//#PROPO_R<ImageElementFactory,Image>
		//#PROPO_R<OptionElementFactory,Option>
		//#PROPO_R<History,history>
		//#PROPO_R<Location,location>
		//#PROPO_R<Screen,screen>
		//#PROPO_R<Navigator,clientInformation>
		//#PROPO_R<DataTransfer,clipboardData>
		//#PROP<bool,closed>
		//#PROP<string,defaultStatus>
		//#PROPO_R<Document,document>
		//#PROPO_R<Event,@event>
		//#PROP_R<object,external>
		//#PROPO_R<FrameBase,frameElement>
		//#PROPO_R<NAMED_COLLEC(Window),frames>
		//#PROP_R<int,length>
		//#PROP<string,name>
		//#PROP_R<object,_newEnum>
		//#PROP<object,offscreenBuffering>
		//#PROP<object,opener>
		//#PROPO_R<Window,parent>
		//#PROP<int,screenLeft>
		//#PROP<int,screenTop>
		//#PROPO_R<Window,self>
		//#PROP<string,status>
		//#PROPO_R<Window,top>
		//#PROPO_R<Window,window>
	}

	//================================================================
	//	cls:Event
	//================================================================
	// IHTMLEventObj
	// IHTMLEventObj2
	// IHTMLEventObj3
	// IHTMLEventObj4
	//#CLASS<Event,MshtmlObject>
	public partial class Event{
		//#>>delete
		public object getAttribute(string attrName,int flags){
			return this.Invoke("getAttribute",attrName,flags);
		}
		public bool removeAttribute(string attrName,int flags){
			return (bool)this.Invoke("removeAttribute",attrName,flags);
		}
		public bool setAttribute(string attrName,object value,int flags){
			return (bool)this.Invoke("setAttribute",attrName,value,flags);
		}
		//#<<delete
		//--------------------------------------------------
		//		IHTMLEventObj
		//--------------------------------------------------
		//#PROP<bool,altKey>
		//#PROP<int,button>
		//#PROP<bool,cancelBubble>
		//#PROP<int,clientX>
		//#PROP<int,clientY>
		//#PROP<bool,ctrlKey>
		//#PROPO<Element,fromElement>
		//#PROP<int,keyCode>
		//#PROP<int,offsetX>
		//#PROP<int,offsetY>
		//#PROP<string,qualifier>
		//#PROP<int,reason>
		//#PROP<object,returnValue>
		//#PROP<int,screenX>
		//#PROP<int,screenY>
		//#PROP<bool,shiftKey>
		//#PROPO<Element,srcElement>
		//#PROP<object,srcFilter>
		//#PROPO<Element,toElement>
		//#PROP<string,type>
		//#PROP<int,x>
		//#PROP<int,y>
		//--------------------------------------------------
		//		IHTMLEventObj2
		//--------------------------------------------------
		//#PROPO<COLLECTION(object),bookmarks>
		//#PROPO<ElementCollection,boundElements>
		//#PROP<string,dataFld>
		//#PROPO_R<DataTransfer,dataTransfer>
		//#PROP<string,propertyName>
		//#PROP<object,recordset>
		//#PROP<bool,repeat>
		//#PROP<string,srcUrn>
		//--------------------------------------------------
		//		IHTMLEventObj3
		//--------------------------------------------------
		//#PROP<bool,altLeft>
		//#PROP<bool,ctrlLeft>
		//#PROP<bool,shiftLeft>
		//#PROP_R<int,behaviorCookie>
		//#PROP_R<int,behaviorPart>
		//#PROP_R<bool,contentOverflow>
		//#PROP_R<int,imeCompositionChange>
		//#PROP_R<int,imeNotifyCommand>
		//#PROP_R<int,imeNotifyData>
		//#PROP_R<int,imeRequest>
		//#PROP_R<int,imeRequestData>
		//#PROP_R<int,keyboardLayout>
		//#PROP_R<string,nextPage>
		//--------------------------------------------------
		//		IHTMLEventObj4
		//--------------------------------------------------
		//#PROP_R<int,wheelDelta>
	}

	//================================================================
	//	cls:Navigator
	//================================================================
	// HTMLNavigator
	// IOmNavigator
	//#CLASS<Navigator,MshtmlObject>
	public partial class Navigator{
		//#PROP_R<string,appCodeName>
		//#PROP_R<string,appMinorVersion>
		//#PROP_R<string,appName>
		//#PROP_R<string,appVersion>
		//#PROP_R<string,browserLanguage>
		//#PROP_R<int,connectionSpeed>
		//#PROP_R<bool,cookieEnabled>
		//#PROP_R<string,cpuClass>
		//#PROPO_R<MimeTypes,mimeTypes>
		//#PROP_R<bool,onLine>
		//#OBSOLETE_ATTR<x>
		//#PROPO_R<Profile,opsProfile>
		//#PROP_R<string,platform>
		//#PROPO_R<Plugins,plugins>
		//#PROP_R<string,systemLanguage>
		//#PROP_R<string,userAgent>
		//#PROP_R<string,userLanguage>
		//#OBSOLETE_ATTR<x>
		//#PROPO_R<Profile,userProfile>
		//#>>delete
		public bool javaEnabled()	{return (bool)this.Invoke("javaEnabled");}
		public bool taintEnabled()	{return (bool)this.Invoke("taintEnabled");}
		public string toString()	{return (string)this.Invoke("toString");}
		//#<<delete
	}

	//================================================================
	//	cls:History
	//================================================================
	// HTMLHistory
	// IOmHistory
	//#CLASS<History,MshtmlObject>
	public partial class History{
		//#>>delete
		public void back(object var) { this.Invoke("back",var); }
		public void forward(object var)	{this.Invoke("forward",var);}
		public void go(object var)		{this.Invoke("go",var);}
		//#<<delete
		//#PROP_R<short,length>
	}

	//================================================================
	//	cls:Profile
	//================================================================
	// COpsProfile
	// IHTMLOpsProfile
	//#>>delete
	[System.Obsolete]
	//#<<delete
	//#CLASS<Profile,MshtmlObject>
	public partial class Profile{
		//#>>delete
		public bool addReadRequest(string name,object reserved) {
			return (bool)this.Invoke("addReadRequest",name,reserved);
		}
		public bool addRequest(string name,object reserved){
			return (bool)this.Invoke("addRequest",name,reserved);
		}
		public void clearRequest(){
			this.Invoke("clearRequest");
		}
		public bool commitChanges(){
			return (bool)this.Invoke("commitChanges");
		}
		public void doReadRequest(object usage,object fname,object domain,object path,object expire,object reserved){
			this.Invoke("doReadRequest",usage,fname,path,expire,reserved);
		}
		public void doRequest(object usage,object fname,object domain,object path,object expire,object reserved){
			this.Invoke("doRequest",usage,fname,path,expire,reserved);
		}
		public bool doWriteRequest(){
			return (bool)this.Invoke("doWriteRequest");
		}
		public string getAttribute(string name){
			return (string)this.Invoke("getAttribute",name);
		}
		public bool setAttribute(string name,string value,object prefs){
			return (bool)this.Invoke("setAttribute",name,value,prefs);
		}
		//#<<delete
	}

	//================================================================
	//	cls:Location
	//================================================================
	// HTMLLocation
	// IHTMLLocation
	//#CLASS<Location,MshtmlObject>
	public partial class Location{
		//#>>delete
		public void assign(string str) { this.Invoke("assign",str); }
		public void reload(bool flag)	{this.Invoke("reload",flag);}
		public void replace(string url)	{this.Invoke("replace",url);}
		public string toString()		{return (string)this.Invoke("toString");}
		//#<<delete
		//#PROP<string,hash>
		//#PROP<string,host>
		//#PROP<string,hostname>
		//#PROP<string,href>
		//#PROP<string,pathname>
		//#PROP<string,port>
		//#PROP<string,protocol>
		//#PROP<string,search>
	}

	//================================================================
	//	cls:Screen
	//================================================================
	// HTMLScreen
	// DispHTMLScreen
	// IHTMLScreen
	// IHTMLScreen2
	//#CLASS<Screen,MshtmlObject>
	public partial class Screen{
		// IHTMLScreen
		//#PROP_R<int,availHeight>
		//#PROP_R<int,availWidth>
		//#PROP<int,bufferDepth>
		//#PROP_R<int,colorDepth>
		//#PROP_R<bool,fontSmoothingEnabled>
		//#PROP_R<int,height>
		//#PROP<int,updateInterval>
		//#PROP_R<int,width>

		// IHTMLScreen2
		//#PROP_R<int,deviceXDPI>
		//#PROP_R<int,deviceYDPI>
		//#PROP_R<int,logicalXDPI>
		//#PROP_R<int,logicalYDPI>
	}
}