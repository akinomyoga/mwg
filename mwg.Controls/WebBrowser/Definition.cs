using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"

	// IHTMLDataTransfer
	//#CLASS<DataTransfer,MshtmlObject>
	public partial class DataTransfer{
		//#>>delete
		public void clearData(string format)	{this.Invoke("clearData",format);}
		public object getData(string format)	{return this.Invoke("getData",format);}
		public bool setData(string format,object value){
			return (bool)this.Invoke("setData",format,value);
		}
		//#<<delete
		//#PROP<string,dropEffect>
		//#PROP<string,effectAllowed>
	}

	// IHTMLRect
	//#CLASS<Rect,MshtmlObject>
	public partial class Rect{
		//#PROP<int,bottom>
		//#PROP<int,left>
		//#PROP<int,right>
		//#PROP<int,top>
	}


	//#CLASS<TextRange,MshtmlObject>
	public partial class TextRange{
		//------------------------------------------------------------
		//		IHTMLTxtRange
		//------------------------------------------------------------
		//#>>delete
		public void collapse(bool start){
			this.Invoke("collapse",start);
		}
		public int compareEndPoints(string how,TextRange range){
			return (int)this.Invoke("compareEndPoints",how,range);
		}
		public TextRange duplicate(){
			return TextRange.FromObj(this.Invoke("duplicate"));
		}
		public bool execCommand(string cmdId,bool showUi,object value){
			return (bool)this.Invoke("execCommand",cmdId,showUi,value);
		}
		public bool execCommandShowHelp(string cmdId){
			return (bool)this.Invoke("execCommandShowHelp",cmdId);
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
		public bool expand(string unit){
			return (bool)this.Invoke("expand",unit);
		}
		public bool findText(string str,int count,int flags){
			return (bool)this.Invoke("findText",str,count,flags);
		}
		public string getBookmark(){
			return (string)this.Invoke("getBookmark");
		}
		public bool inRange(TextRange range){
			return (bool)this.Invoke("inRange",range);
		}
		public bool isEqual(TextRange range){
			return (bool)this.Invoke("isEqual",range);
		}
		public int move(string unit,int count){
			return (int)this.Invoke("move",unit,count);
		}
		public int moveEnd(string unit,int count){
			return (int)this.Invoke("moveEnd",unit,count);
		}
		public int moveStart(string unit,int count){
			return (int)this.Invoke("moveStart",unit,count);
		}
		public bool moveToBookmark(string bookmark){
			return (bool)this.Invoke("moveToBookmark",bookmark);
		}
		public void moveToElementText(Element elem){
			this.Invoke("moveToElementText",elem);
		}
		public void moveToPoint(int x,int y){
			this.Invoke("moveToPoint",x,y);
		}
		public Element parentElement(){	
			return Element.FromObj(this.Invoke("parentElement"));
		}
		public bool pasteHTML(string html){
			return (bool)this.Invoke("paseHTML",html);
		}
		public void scrollIntoView(bool start){
			this.Invoke("scrollIntoView",start);
		}
		public void select(){
			this.Invoke("select");
		}
		public void setEndPoint(string how,Element range){
			this.Invoke("setEndPoint",how,range);
		}
		//#<<delete
		//#PROP_R<string,htmlText>
		//#PROP<string,text>
		//------------------------------------------------------------
		//		IHTMLTextRangeMetrics
		//------------------------------------------------------------
		//#PROP_R<int,boudingHeight>
		//#PROP_R<int,boudingLeft>
		//#PROP_R<int,boudingTop>
		//#PROP_R<int,boudingWidth>
		//#PROP_R<int,offsetLeft>
		//#PROP_R<int,offsetTop>
		//------------------------------------------------------------
		//		IHTMLTextRangeMetrics2
		//------------------------------------------------------------
		//#>>delete
		public Rect getBoundingClientRect(){
			return Rect.FromObj(this.Invoke("getBoundingClientRect"));
		}
		public CollectionBase<Rect> getClientRecst(){
			return CollectionBase<Rect>.FromObj(this.Invoke("getClientRecst"));
		}
		//#<<delete
	}

	//#CLASS<Selection,MshtmlObject>
	public partial class Selection{
		//------------------------------------------------------------
		//		IHTMLSelectionObject
		//------------------------------------------------------------
		//#>>delete
		public void clear(){
			this.Invoke("clear");
		}
		public TextRange createRange(){
			return TextRange.FromObj(this.Invoke("createRange"));
		}
		public void empty(){
			this.Invoke("empty");
		}
		//#<<delete
		//#PROP_R<string,type>
		//------------------------------------------------------------
		//		IHTMLSelectionObject2
		//------------------------------------------------------------
		//#>>delete
		public CollectionBase<TextRange> createRangeCollection(){
			return CollectionBase<TextRange>.FromObj(this.Invoke("createRangeCollection"));
		}
		//#<<delete
		//#PROP_R<string,typeDetail>
	}

	//#CLASS<DomImplementation,MshtmlObject>
	public partial class DomImplementation{
		//#>>delete
		public bool hasFeature(string feature,object version){
			return (bool)this.Invoke("hasFeature",feature,version);
		}
		//#<<delete
	}

	//#CLASS<ImageElementFactory,MshtmlObject>
	public partial class ImageElementFactory{
		//#>>delete
		public ImgElement create(object width,object height){
			return ImgElement.FromObj(this.Invoke("create",width,height));
		}
		//#<<delete
	}

	//#CLASS<OptionElementFactory,MshtmlObject>
	public partial class OptionElementFactory{
		//#>>delete
		public OptionElement create(string text,string value,bool defaultSelected,bool selected){
			return OptionElement.FromObj(this.Invoke("create",text,value,defaultSelected,selected));
		}
		//#<<delete
	}

	/// <summary>
	/// NN との互換性の為の、空のクラスです。
	/// </summary>
	//#CLASS<MimeTypes,MshtmlObject>
	public partial class MimeTypes{
		//#PROP_R<int,length>
	}

	/// <summary>
	/// NN との互換性の為の、空のクラスです。
	/// </summary>
	//#CLASS<Plugins,MshtmlObject>
	public partial class Plugins{
		//#PROP_R<int,length>
		//#>>delete
		public void refresh(bool reload){
			this.Invoke("refresh",reload);
		}
		//#<<delete
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