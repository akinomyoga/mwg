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
using CM=System.ComponentModel;

namespace mwg.Controls.WebBrowser{
//#define RAW(x)	x


		//#define COLLECTION(x)		CollectionBase<x>
		//#define NAMED_COLLEC(x)	NamedCollection<x>

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

		//============================================================
		//		Events
		//============================================================
		public event EHVoid onafterprint{
			add{attachEvent("onafterprint",value);}
			remove{detachEvent("onafterprint",value);}
		}
		public event EHVoid onbeforeprint{
			add{attachEvent("onbeforeprint",value);}
			remove{detachEvent("onbeforeprint",value);}
		}
		public event EHVoid onbeforeunload{
			add{attachEvent("onbeforeunload",value);}
			remove{detachEvent("onbeforeunload",value);}
		}
		public event EHVoid onblur{
			add{attachEvent("onblur",value);}
			remove{detachEvent("onblur",value);}
		}
		public event EHError onerror{
			add{attachEvent("onerror",value);}
			remove{detachEvent("onerror",value);}
		}
		public event EHVoid onfocus{
			add{attachEvent("onfocus",value);}
			remove{detachEvent("onfocus",value);}
		}
		public event EHCancel onhelp{
			add{attachEvent("onhelp",value);}
			remove{detachEvent("onhelp",value);}
		}
		public event EHVoid onload{
			add{attachEvent("onload",value);}
			remove{detachEvent("onload",value);}
		}
		public event EHVoid onresize{
			add{attachEvent("onresize",value);}
			remove{detachEvent("onresize",value);}
		}
		public event EHVoid onscroll{
			add{attachEvent("onscroll",value);}
			remove{detachEvent("onscroll",value);}
		}
		public event EHVoid onunload{
			add{attachEvent("onunload",value);}
			remove{detachEvent("onunload",value);}
		}
		//============================================================
		//		Methods
		//============================================================

		//============================================================
		//		Property
		//============================================================
		public ImageElementFactory Image{
			get{return ImageElementFactory.FromObj(base["Image"]);}
		}
		public OptionElementFactory Option{
			get{return OptionElementFactory.FromObj(base["Option"]);}
		}
		public History history{
			get{return History.FromObj(base["history"]);}
		}
		public Location location{
			get{return Location.FromObj(base["location"]);}
		}
		public Screen screen{
			get{return Screen.FromObj(base["screen"]);}
		}
		public Navigator clientInformation{
			get{return Navigator.FromObj(base["clientInformation"]);}
		}
		public DataTransfer clipboardData{
			get{return DataTransfer.FromObj(base["clipboardData"]);}
		}
		public bool closed{
			get{return (bool)base["closed"];}
			set{base["closed"]=value;}
		}
		public string defaultStatus{
			get{return (string)base["defaultStatus"];}
			set{base["defaultStatus"]=value;}
		}
		public Document document{
			get{return Document.FromObj(base["document"]);}
		}
		public Event @event{
			get{return Event.FromObj(base["@event"]);}
		}
		public object external{
			get{return (object)base["external"];}
		}
		public FrameBase frameElement{
			get{return FrameBase.FromObj(base["frameElement"]);}
		}
		public NamedCollection<Window> frames{
			get{return NamedCollection<Window>.FromObj(base["frames"]);}
		}
		public int length{
			get{return (int)base["length"];}
		}
		public string name{
			get{return (string)base["name"];}
			set{base["name"]=value;}
		}
		public object _newEnum{
			get{return (object)base["_newEnum"];}
		}
		public object offscreenBuffering{
			get{return (object)base["offscreenBuffering"];}
			set{base["offscreenBuffering"]=value;}
		}
		public object opener{
			get{return (object)base["opener"];}
			set{base["opener"]=value;}
		}
		public Window parent{
			get{return Window.FromObj(base["parent"]);}
		}
		public int screenLeft{
			get{return (int)base["screenLeft"];}
			set{base["screenLeft"]=value;}
		}
		public int screenTop{
			get{return (int)base["screenTop"];}
			set{base["screenTop"]=value;}
		}
		public Window self{
			get{return Window.FromObj(base["self"]);}
		}
		public string status{
			get{return (string)base["status"];}
			set{base["status"]=value;}
		}
		public Window top{
			get{return Window.FromObj(base["top"]);}
		}
		public Window window{
			get{return Window.FromObj(base["window"]);}
		}
	}

	//================================================================
	//	cls:Event
	//================================================================
	// IHTMLEventObj
	// IHTMLEventObj2
	// IHTMLEventObj3
	// IHTMLEventObj4
	public partial class Event:MshtmlObject{
		protected Event(object instance):base(instance){}
		public static Event FromObj(object obj){
			if(obj==null)return null;
			return new Event(obj);
		}
	}
	public partial class Event{

		//--------------------------------------------------
		//		IHTMLEventObj
		//--------------------------------------------------
		public bool altKey{
			get{return (bool)base["altKey"];}
			set{base["altKey"]=value;}
		}
		public int button{
			get{return (int)base["button"];}
			set{base["button"]=value;}
		}
		public bool cancelBubble{
			get{return (bool)base["cancelBubble"];}
			set{base["cancelBubble"]=value;}
		}
		public int clientX{
			get{return (int)base["clientX"];}
			set{base["clientX"]=value;}
		}
		public int clientY{
			get{return (int)base["clientY"];}
			set{base["clientY"]=value;}
		}
		public bool ctrlKey{
			get{return (bool)base["ctrlKey"];}
			set{base["ctrlKey"]=value;}
		}
		public Element fromElement{
			get{return Element.FromObj(base["fromElement"]);}
			set{base["fromElement"]=((IWrapper)value).Value;}
		}
		public int keyCode{
			get{return (int)base["keyCode"];}
			set{base["keyCode"]=value;}
		}
		public int offsetX{
			get{return (int)base["offsetX"];}
			set{base["offsetX"]=value;}
		}
		public int offsetY{
			get{return (int)base["offsetY"];}
			set{base["offsetY"]=value;}
		}
		public string qualifier{
			get{return (string)base["qualifier"];}
			set{base["qualifier"]=value;}
		}
		public int reason{
			get{return (int)base["reason"];}
			set{base["reason"]=value;}
		}
		public object returnValue{
			get{return (object)base["returnValue"];}
			set{base["returnValue"]=value;}
		}
		public int screenX{
			get{return (int)base["screenX"];}
			set{base["screenX"]=value;}
		}
		public int screenY{
			get{return (int)base["screenY"];}
			set{base["screenY"]=value;}
		}
		public bool shiftKey{
			get{return (bool)base["shiftKey"];}
			set{base["shiftKey"]=value;}
		}
		public Element srcElement{
			get{return Element.FromObj(base["srcElement"]);}
			set{base["srcElement"]=((IWrapper)value).Value;}
		}
		public object srcFilter{
			get{return (object)base["srcFilter"];}
			set{base["srcFilter"]=value;}
		}
		public Element toElement{
			get{return Element.FromObj(base["toElement"]);}
			set{base["toElement"]=((IWrapper)value).Value;}
		}
		public string type{
			get{return (string)base["type"];}
			set{base["type"]=value;}
		}
		public int x{
			get{return (int)base["x"];}
			set{base["x"]=value;}
		}
		public int y{
			get{return (int)base["y"];}
			set{base["y"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLEventObj2
		//--------------------------------------------------
		public CollectionBase<object> bookmarks{
			get{return CollectionBase<object>.FromObj(base["bookmarks"]);}
			set{base["bookmarks"]=((IWrapper)value).Value;}
		}
		public ElementCollection boundElements{
			get{return ElementCollection.FromObj(base["boundElements"]);}
			set{base["boundElements"]=((IWrapper)value).Value;}
		}
		public string dataFld{
			get{return (string)base["dataFld"];}
			set{base["dataFld"]=value;}
		}
		public DataTransfer dataTransfer{
			get{return DataTransfer.FromObj(base["dataTransfer"]);}
		}
		public string propertyName{
			get{return (string)base["propertyName"];}
			set{base["propertyName"]=value;}
		}
		public object recordset{
			get{return (object)base["recordset"];}
			set{base["recordset"]=value;}
		}
		public bool repeat{
			get{return (bool)base["repeat"];}
			set{base["repeat"]=value;}
		}
		public string srcUrn{
			get{return (string)base["srcUrn"];}
			set{base["srcUrn"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLEventObj3
		//--------------------------------------------------
		public bool altLeft{
			get{return (bool)base["altLeft"];}
			set{base["altLeft"]=value;}
		}
		public bool ctrlLeft{
			get{return (bool)base["ctrlLeft"];}
			set{base["ctrlLeft"]=value;}
		}
		public bool shiftLeft{
			get{return (bool)base["shiftLeft"];}
			set{base["shiftLeft"]=value;}
		}
		public int behaviorCookie{
			get{return (int)base["behaviorCookie"];}
		}
		public int behaviorPart{
			get{return (int)base["behaviorPart"];}
		}
		public bool contentOverflow{
			get{return (bool)base["contentOverflow"];}
		}
		public int imeCompositionChange{
			get{return (int)base["imeCompositionChange"];}
		}
		public int imeNotifyCommand{
			get{return (int)base["imeNotifyCommand"];}
		}
		public int imeNotifyData{
			get{return (int)base["imeNotifyData"];}
		}
		public int imeRequest{
			get{return (int)base["imeRequest"];}
		}
		public int imeRequestData{
			get{return (int)base["imeRequestData"];}
		}
		public int keyboardLayout{
			get{return (int)base["keyboardLayout"];}
		}
		public string nextPage{
			get{return (string)base["nextPage"];}
		}
		//--------------------------------------------------
		//		IHTMLEventObj4
		//--------------------------------------------------
		public int wheelDelta{
			get{return (int)base["wheelDelta"];}
		}
	}

	//================================================================
	//	cls:Navigator
	//================================================================
	// HTMLNavigator
	// IOmNavigator
	public partial class Navigator:MshtmlObject{
		protected Navigator(object instance):base(instance){}
		public static Navigator FromObj(object obj){
			if(obj==null)return null;
			return new Navigator(obj);
		}
	}
	public partial class Navigator{
		public string appCodeName{
			get{return (string)base["appCodeName"];}
		}
		public string appMinorVersion{
			get{return (string)base["appMinorVersion"];}
		}
		public string appName{
			get{return (string)base["appName"];}
		}
		public string appVersion{
			get{return (string)base["appVersion"];}
		}
		public string browserLanguage{
			get{return (string)base["browserLanguage"];}
		}
		public int connectionSpeed{
			get{return (int)base["connectionSpeed"];}
		}
		public bool cookieEnabled{
			get{return (bool)base["cookieEnabled"];}
		}
		public string cpuClass{
			get{return (string)base["cpuClass"];}
		}
		public MimeTypes mimeTypes{
			get{return MimeTypes.FromObj(base["mimeTypes"]);}
		}
		public bool onLine{
			get{return (bool)base["onLine"];}
		}
	[System.Obsolete]
		public Profile opsProfile{
			get{return Profile.FromObj(base["opsProfile"]);}
		}
		public string platform{
			get{return (string)base["platform"];}
		}
		public Plugins plugins{
			get{return Plugins.FromObj(base["plugins"]);}
		}
		public string systemLanguage{
			get{return (string)base["systemLanguage"];}
		}
		public string userAgent{
			get{return (string)base["userAgent"];}
		}
		public string userLanguage{
			get{return (string)base["userLanguage"];}
		}
	[System.Obsolete]
		public Profile userProfile{
			get{return Profile.FromObj(base["userProfile"]);}
		}
	}

	//================================================================
	//	cls:History
	//================================================================
	// HTMLHistory
	// IOmHistory
	public partial class History:MshtmlObject{
		protected History(object instance):base(instance){}
		public static History FromObj(object obj){
			if(obj==null)return null;
			return new History(obj);
		}
	}
	public partial class History{

		public short length{
			get{return (short)base["length"];}
		}
	}

	//================================================================
	//	cls:Profile
	//================================================================
	// COpsProfile
	// IHTMLOpsProfile

	public partial class Profile:MshtmlObject{
		protected Profile(object instance):base(instance){}
		public static Profile FromObj(object obj){
			if(obj==null)return null;
			return new Profile(obj);
		}
	}
	public partial class Profile{

	}

	//================================================================
	//	cls:Location
	//================================================================
	// HTMLLocation
	// IHTMLLocation
	public partial class Location:MshtmlObject{
		protected Location(object instance):base(instance){}
		public static Location FromObj(object obj){
			if(obj==null)return null;
			return new Location(obj);
		}
	}
	public partial class Location{

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
	}

	//================================================================
	//	cls:Screen
	//================================================================
	// HTMLScreen
	// DispHTMLScreen
	// IHTMLScreen
	// IHTMLScreen2
	public partial class Screen:MshtmlObject{
		protected Screen(object instance):base(instance){}
		public static Screen FromObj(object obj){
			if(obj==null)return null;
			return new Screen(obj);
		}
	}
	public partial class Screen{
		// IHTMLScreen
		public int availHeight{
			get{return (int)base["availHeight"];}
		}
		public int availWidth{
			get{return (int)base["availWidth"];}
		}
		public int bufferDepth{
			get{return (int)base["bufferDepth"];}
			set{base["bufferDepth"]=value;}
		}
		public int colorDepth{
			get{return (int)base["colorDepth"];}
		}
		public bool fontSmoothingEnabled{
			get{return (bool)base["fontSmoothingEnabled"];}
		}
		public int height{
			get{return (int)base["height"];}
		}
		public int updateInterval{
			get{return (int)base["updateInterval"];}
			set{base["updateInterval"]=value;}
		}
		public int width{
			get{return (int)base["width"];}
		}

		// IHTMLScreen2
		public int deviceXDPI{
			get{return (int)base["deviceXDPI"];}
		}
		public int deviceYDPI{
			get{return (int)base["deviceYDPI"];}
		}
		public int logicalXDPI{
			get{return (int)base["logicalXDPI"];}
		}
		public int logicalYDPI{
			get{return (int)base["logicalYDPI"];}
		}
	}
}