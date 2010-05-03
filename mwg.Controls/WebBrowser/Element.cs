using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"

	//#CLASS_NEW<AElement,DatabindingElement>
	public partial class AElement{
		//------------------------------------------------------------
		//	IHTMLAnchorElement
		//------------------------------------------------------------
		// override #PROP<string,accessKey>
		//#PROP<string,hash>
		//#PROP<string,host>
		//#PROP<string,hostname>
		//#PROP<string,href>
		//#PROP<string,Methods>
		//#PROP_R<string,mimeType>
		//#PROP<string,name>
		//#PROP_R<string,nameProp>
		//#PROP<string,pathname>
		//#PROP<string,port>
		//#PROP<string,protocol>
		//#PROP_R<string,protocolLong>
		//#PROP<string,rel>
		//#PROP<string,rev>
		//#PROP<string,search>
		// override #PROP<short,tabIndex>
		//#PROP<string,target>
		//#PROP<string,urn>
		//------------------------------------------------------------
		//	IHTMLAnchorElement2
		//------------------------------------------------------------
		//#PROP<string,charset>
		//#PROP<string,coords>
		//#PROP<string,hreflang>
		//#PROP<string,shape>
		//#PROP<string,type>
	}
	//#CLASS_NEW<AreaElement,Element>
	public partial class AreaElement{
		//------------------------------------------------------------
		//	IHTMLAreaElement
		//------------------------------------------------------------
		//#PROP<string,alt>
		//#PROP<string,coords>
		//#PROP<string,hash>
		//#PROP<string,host>
		//#PROP<string,hostname>
		//#PROP<string,href>
		//#PROP<bool,noHref>
		//#PROP<string,pathname>
		//#PROP<string,port>
		//#PROP<string,protocol>
		//#PROP<string,search>
		//#PROP<string,shape>
		//#PROP<string,target>
	}
	//#CLASS_NEW<BaseElement,Element>
	public partial class BaseElement{
		//------------------------------------------------------------
		//	IHTMLBaseElement
		//------------------------------------------------------------
		//#PROP<string,href>
		//#PROP<string,target>
	}
	//#CLASS_NEW<BaseFontElement,Element>
	public partial class BaseFontElement{
		//------------------------------------------------------------
		//	IHTMLBaseFontElement
		//------------------------------------------------------------
		//#PROP<object,color>
		//#PROP<string,face>
		//#PROP<int,size>
	}
	//#CLASS_NEW<BgsoundElement,Element>
	public partial class BgsoundElement{
		//------------------------------------------------------------
		//	IHTMLBGsoundElement
		//------------------------------------------------------------
		//#PROP<object,balance>
		//#PROP<object,loop>
		//#PROP<string,src>
		//#PROP<object,volume>
	}
	//#CLASS_NEW<BlockElement,Element>
	public partial class BlockElement{
		//------------------------------------------------------------
		//	IHTMLBlockElement
		//------------------------------------------------------------
		//#PROP<string,clear>
		//------------------------------------------------------------
		//	IHTMLBlockElement2
		//------------------------------------------------------------
		//#PROP<string,cite>
		//#PROP<string,width>
	}

	//#CLASS_NEW<BodyElement,Element>
	/// <summary>
	/// Body 要素を表現するクラスです。
	/// </summary>
	public partial class BodyElement{
		//#>>delete
		internal BodyElement(Forms::HtmlElement elem)
			: base(elem.DomElement) {
			if(elem.TagName.ToLower()!="body")
				throw new System.ArgumentException("指定した要素は body 要素ではありません。","elem");
		}
		//#<<delete
		//--------------------------------------------------
		//		IHTMLBodyElement
		//--------------------------------------------------
		//#PROP<object,aLink>
		//#PROP<object,link>
		//#PROP<object,vLink>
		//#PROP<string,background>
		//#PROP<object,bgColor>
		//#PROP<string,bgProperties>
		//#PROP<object,bottomMargin>
		//#PROP<object,leftMargin>
		//#PROP<object,rightMargin>
		//#PROP<object,topMargin>
		//#PROP<bool,noWrap>
		//#PROP<string,scroll>
		//#PROP<string,text>
		//#EVENT<EHVoid,onbeforeunload>
		//#EVENT<EHVoid,onload>
		//#EVENT<EHVoid,onunload>
		//--------------------------------------------------
		//		IHTMLBodyElement2
		//--------------------------------------------------
		//#EVENT<EHVoid,onafterprint>
		//#EVENT<EHVoid,onbeforeprint>
		//--------------------------------------------------
		//		IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
	}

	//#CLASS_NEW<BrElement,Element>
	public partial class BrElement{
		//#PROP<string,clear>
	}

	//#CLASS_NEW<ButtonElement,DatabindingElement>
	public partial class ButtonElement{
		//------------------------------------------------------------
		//	IHTMLButtonElement
		//------------------------------------------------------------
		//#>>delete
		public TextRange createTextRange(){
			return (TextRange)this.Invoke("createTextRange");
		}
		//#<<delete
		//#PROPO_R<FormElement,form>
		//#PROP<string,name>
		//#PROP<object,status>
		//#PROP_R<string,type>
		//#PROP<string,value>
	}

	//#CLASS_NEW<CommentElement,Element>
	public partial class CommentElement{
		//------------------------------------------------------------
		//	IHTMLCommentElement
		//------------------------------------------------------------
		//#PROP<int,atomic>
		//#PROP<string,text>
		//------------------------------------------------------------
		//	IHTMLCommentElement2
		//------------------------------------------------------------
		//#PROP<string,data>
		//#PROP_R<int,length>
		//#>>delete
		public void appendData(string data)				{this.Invoke("appendData",data);}
		public void deleteData(int offset,int len)		{this.Invoke("deleteData",offset,len);}
		public void insertData(int offset,string data)	{this.Invoke("insertData",offset,data);}
		public void replaceData(int offset,int len,string newdata){
			this.Invoke("replaceData",offset,len,newdata);
		}
		public string substringData(int offset,int len)	{return (string)this.Invoke("substringData",offset,len);}
		//#<<delete
	}

	//#CLASS_NEW<DdElement,Element>
	public partial class DdElement{
		//#PROP<bool,noWrap>
	}

	//#CLASS_NEW<DivElement,DatabindingElement>
	public partial class DivElement{
		//------------------------------------------------------------
		//	IHTMLDivElement
		//------------------------------------------------------------
		//#PROP<string,align>
		//#PROP<bool,noWrap>
		//--------------------------------------------------
		//	IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
	}

	//#CLASS_NEW<DlElement,Element>
	public partial class DlElement{
		//#PROP<bool,compact>
	}

	//#CLASS_NEW<DtElement,Element>
	public partial class DtElement{
		//#PROP<bool,noWrap>
	}

	//#CLASS_NEW<EmbedElement,Element>
	public partial class EmbedElement{
		//#PROP<object,height>
		//#PROP<object,width>
		//#PROP<string,hidden>
		//#PROP<string,name>
		//#PROP_R<string,palette>
		//#PROP_R<string,pluginspage>
		//#PROP<string,src>
		//#PROP<string,units>
	}

	//#CLASS_NEW<FieldsetElement,Element>
	public partial class FieldsetElement{
		//------------------------------------------------------------
		//	IHTMLFieldsetElement
		//------------------------------------------------------------
		//#PROP<string,align>
		//------------------------------------------------------------
		//	IHTMLFieldsetElement2
		//------------------------------------------------------------
		//#PROP<FormElement,form>
		//--------------------------------------------------
		//	IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
	}

	//#CLASS_NEW<FontElement,Element>
	public partial class FontElement{
		//--------------------------------------------------
		//	IHTMLFontElement
		//--------------------------------------------------
		//#PROP<object,color>
		//#PROP<string,face>
		//#PROP<object,size>
	}

	//#CLASS_NEW<FormElement,Element>
	public partial class FormElement:Gen::IEnumerable<Element>{
		//--------------------------------------------------
		//	IHTMLFormElement
		//--------------------------------------------------
		//#>>delete
		public MshtmlObject item(string name){
			return GetElementOrCollection(this.Invoke("item",name));
		}
		public Element item(string name,int index){
			return Element.FromObj(this.Invoke("item",name,index));
		}
		public Element item(int index){
			return Element.FromObj(this.Invoke("item",index));
		}
		//public new MshtmlObject this[string name]{
		//	get{return this.item(name);}
		//}
		public Element this[string name,int index]{
			get{return this.item(name,index);}
		}
		public Element this[int index]{
			get{return this.item(index);}
		}
		public Gen::IEnumerator<Element> GetEnumerator(){
			for(int i=0;i<this.length;i++)
				yield return this[i];
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return this.GetEnumerator();
		}
		public void reset(){
			this.Invoke("reset");
		}
		public void submit(){
			this.Invoke("submit");
		}
		// tags(MshtmlObject)
		public ElementCollection tags(string tagName) {
			return ElementCollection.FromObj(this.Invoke("tags",tagName));
		}
		//#<<delete
		//#PROP<int,length>
		//#PROP<string,action>
		//#PROPO_R<ElementCollection,elements>
		//#PROP<string,encoding>
		//#PROP<string,method>
		//#PROP<string,name>
		//#PROP<string,target>
		//#EVENT<EHCancel,onreset>
		//#EVENT<EHCancel,onsubmit>
		//--------------------------------------------------
		//	IHTMLFormElement2
		//--------------------------------------------------
		//#>>delete
		public ElementCollection urns(string behaviorUrn) {
			return ElementCollection.FromObj(this.Invoke("urns",behaviorUrn));
		}
		//#<<delete
		//#PROP<string,acceptCharset>
		//------------------------------------------------------------
		// IHTMLFormElement3
		//------------------------------------------------------------
		//#>>delete
		/// <summary>
		/// 指定した名前を持つ要素を取得します。
		/// </summary>
		/// <param name="name">検索する要素に付けられた名前を指定します。</param>
		/// <returns>
		/// 指定した名前の要素が一つの場合にはその要素を返します。
		/// 指定した名前の要素が複数ある場合にはその集合を返します。
		/// </returns>
		public MshtmlObject namedItem(string name) {
			return GetElementOrCollection(this.Invoke("namedItem",name));
		}
		//#<<delete
		//--------------------------------------------------
		//	IHTMLSubmitData
		//--------------------------------------------------
		//#>>delete
		public void appenditemSeparator(){
			this.Invoke("appenditemSeparator");
		}
		public void appendNameFilePair(string name,string filename){
			this.Invoke("appendNameFilePair",name,filename);
		}
		public void appendNameValuePair(string name,string value){
			this.Invoke("appendNameValuePair",name,value);
		}
		//#<<delete
	}

	// IHTMLFrameBase
	//#CLASS_NEW<FrameBase,Element>
	public partial class FrameBase{
		//--------------------------------------------------
		//	IHTMLFrameBase
		//--------------------------------------------------
		//#PROP<object,border>
		//#PROP<string,frameBorder>
		//#PROP<object,frameSpacing>
		//#PROP<object,marginHeight>
		//#PROP<object,marginWidth>
		//#PROP<string,name>
		//#PROP<bool,noResize>
		//#PROP<string,scrolling>
		//#PROP<string,src>
		//--------------------------------------------------
		//	IHTMLFrameBase2
		//--------------------------------------------------
		//#PROP<bool,allowTransparency>
		//#PROPO_R<Window,contentWindow>
		//#PROP_R<string,readyState>
		//#EVENT<EHVoid,onload>
		//--------------------------------------------------
		//	IHTMLFrameBase3
		//--------------------------------------------------
		//#PROP<string,longDesc>
	}

	//#CLASS_NEW<FrameElement,FrameBase>
	public partial class FrameElement{
		//------------------------------------------------------------
		//	IHTMLDatabinding
		//------------------------------------------------------------
		//#PROP<string,dataFld>
		//#PROP<string,dataFormatAs>
		//#PROP<string,dataSrc>
		//--------------------------------------------------
		//	IHTMLFrameElement
		//--------------------------------------------------
		//#PROP<object,borderColor>
		//--------------------------------------------------
		//	IHTMLFrameElement
		//--------------------------------------------------
		//#PROP<object,height>
		//#PROP<object,width>
	}

	//#CLASS_NEW<FramesetElement,Element>
	public partial class FramesetElement{
		//--------------------------------------------------
		//	IHTMLFrameSetElement
		//--------------------------------------------------
		//#PROP<object,border>
		//#PROP<object,borderColor>
		//#PROP<string,cols>
		//#PROP<string,rows>
		//#PROP<string,frameBorder>
		//#PROP<string,frameSpacing>
		//#PROP<string,name>
		//#EVENT<EHVoid,onbeforeunload>
		//#EVENT<EHVoid,onload>
		//#EVENT<EHVoid,onunload>
		//--------------------------------------------------
		//	IHTMLFrameSetElement2
		//--------------------------------------------------
		//#EVENT<EHVoid,onafterprint>
		//#EVENT<EHVoid,onbeforeprint>
	}

	//#CLASS_NEW<GenericElement,Element>
	public partial class GenericElement{
		//--------------------------------------------------
		//	IHTMLGenericElement
		//--------------------------------------------------
		//#PROP_R<object,recordset>
		//#>>delete
		public object namedRecordset(string name,object hieralchy){
			return this.Invoke("namedRecordset",name,hieralchy);
		}
		//#<<delete
	}

	//#CLASS_NEW<HeadElement,Element>
	public partial class HeadElement{
		//--------------------------------------------------
		//	IHTMLHeadElement
		//--------------------------------------------------
		//#PROP<string,profile>
	}

	//#CLASS_NEW<HnElement,Element>
	public partial class HnElement{
		//--------------------------------------------------
		//	IHTMLBlockElement
		//--------------------------------------------------
		//#PROP<string,clear>
		//--------------------------------------------------
		//	IHTMLHeaderElement
		//--------------------------------------------------
		//#PROP<string,align>
	}

	//#CLASS_NEW<HrElement,Element>
	public partial class HrElement{
		//--------------------------------------------------
		//	IHTMLHRElement
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<object,color>
		//#PROP<bool,noShade>
		//#PROP<object,size>
		//#PROP<object,width>
	}

	//#CLASS_NEW<HtmlElement,Element>
	public partial class HtmlElement{
		//--------------------------------------------------
		//	IHTMLHtmlElement
		//--------------------------------------------------
		//#PROP<string,version>
	}

	//#CLASS_NEW<IframeElement,FrameBase>
	public partial class IframeElement{
		//------------------------------------------------------------
		//	IHTMLDatabinding
		//------------------------------------------------------------
		//#PROP<string,dataFld>
		//#PROP<string,dataFormatAs>
		//#PROP<string,dataSrc>
		//--------------------------------------------------
		//	IHTMLIFrameElement
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<int,hspace>
		//#PROP<int,vspace>
		//--------------------------------------------------
		//	IHTMLIFrameElement2
		//--------------------------------------------------
		//#PROP<object,height>
		//#PROP<object,width>
	}

	//#CLASS_NEW<ImgElement,DatabindingElement>
	public partial class ImgElement{
		//--------------------------------------------------
		//	IHTMLImgElement
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<string,alt>
		//#PROP<object,border>
		//#PROP_R<bool,complete>
		//#PROP<string,dynsrc>
		//#PROP_R<string,fileCreatedDate>
		//#PROP_R<string,fileModifiedDate>
		//#PROP_R<string,fileSize>
		//#PROP_R<string,fileUpdatedDate>
		//#PROP<int,height>
		//#PROP<int,width>
		//#PROP<int,hspace>
		//#PROP<int,vspace>
		//#PROP_R<string,href>
		//#PROP<bool,isMap>
		//#PROP<object,loop>
		//#PROP<string,lowsrc>
		//#PROP_R<string,mimeType>
		//#PROP<string,name>
		//#PROP_R<string,nameProp>
		//#PROP_R<string,protocol>
		//#PROP_R<string,readyState>
		//#PROP<string,src>
		//#PROP<string,Start>
		//#PROP<string,useMap>
		//#PROP<string,vrml>
		//#EVENT<EHVoid,onabort>
		//#EVENT<EHVoid,onerror>
		//#EVENT<EHVoid,onload>
		//--------------------------------------------------
		//	IHTMLIFrameElement2
		//--------------------------------------------------
		//#PROP<string,longDesc>
	}

	//#CLASS_NEW<InputElement,DatabindingElement>
	public partial class InputElement{
		//--------------------------------------------------
		//	IHTMLInputElement
		//	IHTMLInput***Element
		//--------------------------------------------------
		//#>>delete
		public TextRange createTextRange(){
			return (TextRange)this.Invoke("createTextRange");
		}
		public void select(){
			this.Invoke("select");
		}
		//#<<delete
		//#PROP<string,align>
		//#PROP<string,alt>
		//#PROP<object,border>
		//#PROP<bool,@checked>
		//#PROP_R<bool,complete>
		//#PROP<bool,defaultChecked>
		//#PROP<string,defaultValue>
		// #PROP<bool,disabled>
		//#PROP<string,dynsrc>
		//#PROPO_R<FormElement,form>
		//#PROP<int,height>
		//#PROP<int,width>
		//#PROP<int,hspace>
		//#PROP<int,vspace>
		//#PROP<bool,indeterminate>
		//#PROP_R<string,href>
		//#PROP<object,loop>
		//#PROP<string,lowsrc>
		//#PROP<int,maxlength>
		//#PROP<string,name>
		//#PROP<bool,readOnly>
		//#PROP_R<string,readyState>
		//#PROP<int,size>
		//#PROP<string,src>
		//#PROP<string,Start>
		//#PROP<bool,status>
		//#PROP<string,type>
		//#PROP<string,value>
		//#PROP<string,vrml>
		//#EVENT<EHVoid,onabort>
		//#EVENT<EHVoid,onerror>
		//#EVENT<EHVoid,onload>
		//#EVENT<EHCancel,onchange>
		//#EVENT<EHVoid,onselect>
		//--------------------------------------------------
		//	IHTMLInputElement2
		//--------------------------------------------------
		//#PROP<string,accept>
		//#PROP<string,useMap>
	}

	//#CLASS_NEW<IsindexElement,Element>
	public partial class IsindexElement{
		//--------------------------------------------------
		//	IHTMLIsIndexElement
		//--------------------------------------------------
		//#PROP<string,action>
		//#PROP<string,prompt>
		//--------------------------------------------------
		//	IHTMLIsIndexElement2
		//--------------------------------------------------
		//#PROPO_R<FormElement,form>
	}

	//#CLASS_NEW<LabelElement,DatabindingElement>
	public partial class LabelElement{
		//--------------------------------------------------
		//	IHTMLLabelElement
		//--------------------------------------------------
		//#PROP<string,htmlFor>
		//--------------------------------------------------
		//	IHTMLLabelElement2
		//--------------------------------------------------
		//#PROPO_R<FormElement,form>
	}

	//#CLASS_NEW<LegendElement,DatabindingElement>
	public partial class LegendElement{
		//--------------------------------------------------
		//	IHTMLLegendElement
		//--------------------------------------------------
		//#PROP<string,align>
		//--------------------------------------------------
		//	IHTMLLegendElement2
		//--------------------------------------------------
		//#PROPO_R<FormElement,form>
		//--------------------------------------------------
		//	IHTMLTextContainerEvents_Event
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
	}

	//#CLASS_NEW<LiElement,Element>
	public partial class LiElement{
		//--------------------------------------------------
		//	IHTMLLIElement
		//--------------------------------------------------
		//#PROP<string,type>
		//#PROP<int,value>
	}

	//#CLASS_NEW<LinkElement,Element>
	public partial class LinkElement{
		//--------------------------------------------------
		//	IHTMLLinkElement
		//--------------------------------------------------
		//#PROP<string,href>
		//#PROP<string,media>
		//#PROP<string,readyState>
		//#PROP<string,rel>
		//#PROP<string,rev>
		//#PROPO_R<StyleSheet,styleSheet>
		//#PROP<string,type>
		//#EVENT<EHVoid,onerror>
		//#EVENT<EHVoid,onload>
		//--------------------------------------------------
		//	IHTMLLinkElement2
		//--------------------------------------------------
		//#PROP<string,target>
		//--------------------------------------------------
		//	IHTMLLinkElement3
		//--------------------------------------------------
		//#PROP<string,charset>
		//#PROP<string,hreflang>
	}

	//#CLASS_NEW<ListElement,Element>
	public partial class ListElement{
		//--------------------------------------------------
		//	IHTMLListElement2
		//--------------------------------------------------
		//#PROP<bool,compact>
	}

	//#CLASS_NEW<MapElement,Element>
	public partial class MapElement{
		//--------------------------------------------------
		//	IHTMLMapElement
		//--------------------------------------------------
		//#PROPO_R<AreaElementCollection,areas>
		//#PROP<string,name>
	}

	//#CLASS_NEW<MarqueeElement,DatabindingElement>
	public partial class MarqueeElement{
		//--------------------------------------------------
		//	IHTMLMarqueeElement
		//--------------------------------------------------
		//#PROPO_R<AreaElementCollection,areas>
		//#PROP<string,behavior>
		//#PROP<object,bgColor>
		//#PROP<string,direction>
		//#PROP<object,height>
		//#PROP<object,width>
		//#PROP<int,hspace>
		//#PROP<int,vspace>
		//#PROP<int,loop>
		//#PROP<int,scrollAmount>
		//#PROP<int,scrollDelay>
		//#PROP<bool,trueSpeed>
		//#EVENT<EHVoid,onbounce>
		//#EVENT<EHVoid,onfinish>
		//#EVENT<EHVoid,onstart>
		//--------------------------------------------------
		//	IHTMLTextContainer
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
	}

	//#CLASS_NEW<MetaElement,Element>
	public partial class MetaElement{
		//--------------------------------------------------
		//	IHTMLMetaElement
		//--------------------------------------------------
		//#PROP<string,charset>
		//#PROP<string,content>
		//#PROP<string,httpEquiv>
		//#PROP<string,name>
		//#PROP<string,url>
		//--------------------------------------------------
		//	IHTMLMetaElement2
		//--------------------------------------------------
		//#PROP<string,scheme>
	}

	//#CLASS_NEW<NextidElement,Element>
	public partial class NextidElement{
		//#PROP<string,n>
	}

	//#CLASS_NEW<NoshowElement,Element>

	//#CLASS_NEW<ObjectElement,DatabindingElement>
	public partial class ObjectElement{
		//--------------------------------------------------
		//	IHTMLObjectElement
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<string,altHtml>
		//#PROP_R<string,BaseHref>
		//#PROP<string,code>
		//#PROP<string,codeBase>
		//#PROP<string,codeType>
		//#PROPO_R<FormElement,form>
		//#PROP<object,height>
		//#PROP<object,width>
		//#PROP<int,hspace>
		//#PROP<int,vspace>
		//#PROP<string,name>
		//#PROP_R<object,@object>
		//#PROP_R<int,readyState>
		//#PROP<object,recordset>
		//#PROP<string,type>
		//#EVENT<EHVoid,onerror>
		//--------------------------------------------------
		//	IHTMLObjectElement2
		//--------------------------------------------------
		//#PROP<string,classid>
		//#PROP<string,data>
		//#>>delete
		public object namedRecordset(string name,object hierarchy){
			return this.Invoke("namedRecordset",name,hierarchy);
		}
		//#<<delete
		//--------------------------------------------------
		//	IHTMLObjectElement3
		//--------------------------------------------------
		//#PROP<string,alt>
		//#PROP<string,archive>
		//#PROP<object,border>
		//#PROP<bool,declare>
		//#PROP<string,standby>
		//#PROP<string,useMap>
	}

	//#CLASS_NEW<OlElement,ListElement>
	public partial class OlElement{
		//--------------------------------------------------
		//	IHTMLOlistElement3
		//--------------------------------------------------
		// #PROP<bool,compact>
		//#PROP<int,Start>
		//#PROP<string,type>
	}

	//#CLASS_NEW<OptionbuttonElement,DatabindingElement>
	public partial class OptionbuttonElement{
		//--------------------------------------------------
		//	IHTMLOptionButtonElement
		//--------------------------------------------------
		//#PROP<bool,@checked>
		//#PROP<bool,defaultChecked>
		// #PROP<bool,disabled>
		//#PROPO_R<FormElement,form>
		//#PROP<bool,intermediate>
		//#PROP<string,name>
		//#PROP<bool,status>
		//#PROP_R<string,type>
		//#PROP<string,value>
		//#EVENT<EHCancel,onchange>
	}

	//#CLASS_NEW<OptionElement,DatabindingElement>
	public partial class OptionElement{
		//--------------------------------------------------
		//	IHTMLOptionElement
		//--------------------------------------------------
		//#PROP<bool,defaultSelected>
		//#PROPO_R<FormElement,form>
		//#PROP<int,index>
		//#PROP<bool,selected>
		//#PROP<string,text>
		//#PROP<string,value>
		//--------------------------------------------------
		//	IHTMLOptionElement3
		//--------------------------------------------------
		//#PROP<string,label>
	}

	//#CLASS_NEW<PElement,BlockElement>
	public partial class PElement{
		//--------------------------------------------------
		//	IHTMLParaElement
		//--------------------------------------------------
		//#PROP<string,align>
	}

	//#CLASS_NEW<ParamElement,Element>
	public partial class ParamElement{
		//--------------------------------------------------
		//	IHTMLParamElement
		//--------------------------------------------------
		//#PROP<string,name>
		//#PROP<string,type>
		//#PROP<string,value>
		//#PROP<string,valueType>
	}

	//#CLASS_NEW<PhraseElement,Element>
	public partial class PhraseElement{
		//--------------------------------------------------
		//	IHTMLPhraseElement
		//--------------------------------------------------
		//#PROP<string,cite>
		//#PROP<string,dateTime>
	}

	//#CLASS_NEW<RichtextElement,TextAreaElement>

	//#CLASS_NEW<ScriptElement,Element>
	public partial class ScriptElement{
		//--------------------------------------------------
		//	IHTMLScriptElement
		//--------------------------------------------------
		//#PROP<bool,defer>
		//#PROP<string,@event>
		//#PROP<string,htmlFor>
		//#PROP_R<string,readyState>
		//#PROP<string,src>
		//#PROP<string,text>
		//#PROP<string,type>
		//#EVENT<EHVoid,onerror>
		//--------------------------------------------------
		//	IHTMLScriptElement2
		//--------------------------------------------------
		//#PROP<string,charset>
	}

	//#CLASS_NEW<SelectElement,DatabindingElement>
	public partial class SelectElement:Gen::IEnumerable<Element>,System.Collections.IEnumerable{
		//--------------------------------------------------
		//	IHTMLSelectElement - Collection
		//--------------------------------------------------
		//#>>delete
		//	copied from AreaElementCollection
		//--------------------------------------------------
		public void add(Element elem,object before){
			this.Invoke("add",elem,before);
		}
		public void remove(int index){
			this.Invoke("remove",index);
		}
		//	copied from ElementCollection
		//--------------------------------------------------
		public ElementCollection tags(string tagName){
			return ElementCollection.FromObj(this.Invoke("tags",tagName));
		}
		//	copied from NamedCollection
		//--------------------------------------------------
		public Element this[string name,int index]{
			get{return this.item(name,index);}
		}
		public Element item(string name,int index){
			return FromObj<Element>(this.Invoke("item",name,index));
		}
		//	copied from CollectionBase
		//--------------------------------------------------
		public Element item(int index){
			return Element.FromObj(this.Invoke("item",index));
		}
		public int length{
			get{return (int)this["length"];}
		}
		public Element this[int index]{
			get{return this.item(index);}
		}
		public Gen::IEnumerator<Element> GetEnumerator(){
			int len=this.length;
			for(int i=0;i<len;i++)yield return this.item(i);
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return this.GetEnumerator();
		}
		//#<<delete
		//--------------------------------------------------
		//	IHTMLSelectElement
		//--------------------------------------------------
		//#PROPO_R<FormElement,form>
		//#PROP<string,name>
		//#PROP<int,type>
		//#PROP<string,value>

		//#PROPO_R<ElementCollection,options>
		//#PROP<bool,multiple>
		//#PROP<int,selectedIndex>
		//#PROP_R<int,size>
		//#EVENT<EHCancel,onchange>
		//--------------------------------------------------
		//	IHTMLSelectElement2
		//--------------------------------------------------
		//#>>delete
		public ElementCollection urns(string behaviorUrn){
			return ElementCollection.FromObj(this.Invoke("urns",behaviorUrn));
		}
		//#<<delete
		//--------------------------------------------------
		//	IHTMLSelectElement2
		//--------------------------------------------------
		//#>>delete
		public MshtmlObject namedItem(string name){
			return GetElementOrCollection(this.Invoke("namedItem",name));
		}
		//#<<delete
	}

	//#CLASS_NEW<SpanElement,DatabindingElement>
	public partial class SpanElement{
		//--------------------------------------------------
		//	IHTMLPhraseElement
		//--------------------------------------------------
		//#PROP<string,cite>
		//#PROP<string,dateTime>
	}

	//#CLASS_NEW<SpanFlow,DatabindingElement>
	public partial class SpanFlow{
		//--------------------------------------------------
		//	IHTMLPhraseElement
		//--------------------------------------------------
		//#PROP<string,cite>
		//#PROP<string,dateTime>
		//--------------------------------------------------
		//	HTMLTextContainerEvents
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
		//--------------------------------------------------
		//	IHTMLSpanFlow
		//--------------------------------------------------
		//#PROP<string,align>
	}

	//#CLASS_NEW<StyleElement,Element>
	public partial class StyleElement{
		//--------------------------------------------------
		//	IHTMLStyleElement
		//--------------------------------------------------
		//#PROP<string,media>
		//#PROP_R<string,readyState>
		//#PROPO_R<StyleSheet,styleSheet>
		//#PROP<string,type>
		//#EVENT<EHVoid,onerror>
		//#EVENT<EHVoid,onload>
	}

	//#CLASS_NEW<CaptionElement,Element>
	public partial class CaptionElement{
		//--------------------------------------------------
		//	HTMLTextContainerEvents
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
		//--------------------------------------------------
		//	IHTMLTableCaption
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<string,vAlign>
	}

	//#CLASS_NEW<TableCellElement,Element>
	public partial class TableCellElement{
		//--------------------------------------------------
		//	HTMLTextContainerEvents
		//--------------------------------------------------
		//#EVENT<EHVoid,onchange>
		//#EVENT<EHVoid,onselect>
		//--------------------------------------------------
		//	IHTMLTableCell
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<string,vAlign>
		//#PROP<string,background>
		//#PROP<object,bgColor>
		//#PROP<object,borderColor>
		//#PROP<object,borderColorDark>
		//#PROP<object,borderColorLight>
		//#PROP_R<int,cellIndex>
		//#PROP<int,colSpan>
		//#PROP<int,rowSpan>
		//#PROP<object,height>
		//#PROP<object,width>
		//#PROP<bool,noWrap>
		//--------------------------------------------------
		//	IHTMLTableCell2
		//--------------------------------------------------
		//#PROP<string,abbr>
		//#PROP<string,axis>
		//#PROP<string,ch>
		//#PROP<string,chOff>
		//#PROP<string,headers>
		//#PROP<string,scope>
	}

	//#CLASS_NEW<TableElement,DatabindingElement>
	public partial class TableElement{
		//--------------------------------------------------
		//	IHTMLTable
		//--------------------------------------------------
		//#>>delete
		public CaptionElement createCaption(){
			return CaptionElement.FromObj(this.Invoke("createCaption"));
		}
		public TableSectionElement createTFoot(){
			return TableSectionElement.FromObj(this.Invoke("createTFoot"));
		}
		public TableSectionElement createTHead(){
			return TableSectionElement.FromObj(this.Invoke("createTHead"));
		}
		public void deleteCaption(){
			this.Invoke("deleteCaption");
		}
		public void deleteTFoot(){
			this.Invoke("deleteTFoot");
		}
		public void deleteTHead(){
			this.Invoke("deleteTHead");
		}
		public TrElement insertRow(int index){
			return TrElement.FromObj(this.Invoke("insertRow",index));
		}
		public void deleteRow(int index){
			this.Invoke("deleteRow",index);
		}
		public void nextPage(){
			this.Invoke("nextPage");
		}
		public void previousPage(){
			this.Invoke("previousPage");
		}
		public void refresh(){
			this.Invoke("refresh");
		}
		//#<<delete
		//#PROP<string,align>
		//#PROP<string,background>
		//#PROP<object,bgColor>
		//#PROP<object,border>
		//#PROP<object,borderColor>
		//#PROP<object,borderColorDark>
		//#PROP<object,borderColorLight>
		//#PROPO_R<CaptionElement,caption>
		//#PROP<object,cellPadding>
		//#PROP<object,cellSpacing>
		//#PROP<int,cols>
		//#PROP<int,dataPageSize>
		//#PROP<string,frame>
		//#PROP<object,height>
		//#PROP<object,width>
		//#PROP_R<string,readyState>
		//#PROPO_R<RAW("#ElementCollection<TrElement>#"),rows>
		//#PROP<string,rules>
		//#PROPO_R<RAW("#ElementCollection<TableSectionElement>#"),tBodies>
		//#PROPO_R<TableSectionElement,tFoot>
		//#PROPO_R<TableSectionElement,tHead>
		//--------------------------------------------------
		//	IHTMLTable2
		//--------------------------------------------------
		//#>>delete
		public void firstPage(){
			this.Invoke("firstPage");
		}
		public void lastPage(){
			this.Invoke("lastPage");
		}
		public TrElement moveRow(int fromIndex,int toIndex){
			return TrElement.FromObj(this.Invoke("moveRow",fromIndex,toIndex));
		}
		//#<<delete
		//#PROPO_R<RAW("#ElementCollection<TableCellElement>#"),cells>
		//--------------------------------------------------
		//	IHTMLTable2
		//--------------------------------------------------
		//#PROP<string,summary>
	}


	//#CLASS_NEW<ColElement,Element>
	public partial class ColElement{
		//--------------------------------------------------
		//	IHTMLTableCol
		//--------------------------------------------------
		//#PROP<string,align>
		//#PROP<string,vAlign>
		//#PROP<string,span>
		//#PROP<object,width>
		//--------------------------------------------------
		//	IHTMLTableCol2
		//--------------------------------------------------
		//#PROP<string,ch>
		//#PROP<string,chOff>
	}

	//#CLASS_NEW<TrElement,Element>
	public partial class TrElement{
		//--------------------------------------------------
		//	IHTMLTableRow
		//--------------------------------------------------
		//#>>delete
		public void deleteCell(int index){
			this.Invoke("deleteCell",index);
		}
		public TableCellElement insertCell(int index){
			return TableCellElement.FromObj(this.Invoke("insertCell",index));
		}
		//#<<delete
		//#PROP<string,align>
		//#PROP<string,vAlign>
		//#PROP<object,bgColor>
		//#PROP<object,borderColor>
		//#PROP<object,borderColorDark>
		//#PROP<object,borderColorLight>
		//#PROPO_R<RAW("#ElementCollection<TableCellElement>#"),cells>
		//#PROP_R<int,rowIndex>
		//#PROP_R<int,sectionRowIndex>
		//--------------------------------------------------
		//	IHTMLTableRow2
		//--------------------------------------------------
		//#PROP<object,height>
		//--------------------------------------------------
		//	IHTMLTableRow3
		//--------------------------------------------------
		//#PROP<string,ch>
		//#PROP<string,chOff>
	}

	//#CLASS_NEW<TableSectionElement,Element>
	public partial class TableSectionElement{
		//--------------------------------------------------
		//	IHTMLTableSection
		//--------------------------------------------------
		//#>>delete
		public TrElement insertRow(int index){
			return TrElement.FromObj(this.Invoke("insertRow",index));
		}
		public void deleteRow(int index){
			this.Invoke("deleteRow",index);
		}
		//#<<delete
		//#PROP<string,align>
		//#PROP<string,vAlign>
		//#PROP<object,bgColor>
		//#PROPO_R<RAW("#ElementCollection<TrElement>#"),rows>
		//--------------------------------------------------
		//	IHTMLTableSection2
		//--------------------------------------------------
		//#>>delete
		public TrElement moveRow(int fromIndex,int toIndex){
			return TrElement.FromObj(this.Invoke("moveRow",fromIndex,toIndex));
		}
		//#<<delete
		//--------------------------------------------------
		//	IHTMLTableSection3
		//--------------------------------------------------
		//#PROP<string,ch>
		//#PROP<string,chOff>
	}

	//#CLASS_NEW<TextElement,Element>

	//#CLASS_NEW<TextAreaElement,DatabindingElement>
	public partial class TextAreaElement{
		//--------------------------------------------------
		//	IHTMLTextAreaElement
		//--------------------------------------------------
		//#>>delete
		public TextRange createTextRange(){
			return (TextRange)this.Invoke("createTextRange");
		}
		public void select(){
			this.Invoke("select");
		}
		//#<<delete
		//#PROP<int,cols>
		//#PROP<int,rows>
		//#PROP<string,defaultValue>
		//#PROP<bool,readOnly>
		//#PROP_R<string,wrap>

		//#PROPO_R<FormElement,form>
		//#PROP<string,name>
		//#PROP_R<object,status>
		//#PROP<int,type>
		//#PROP<string,value>

		//#EVENT<EHCancel,onchange>
		//#EVENT<EHVoid,onselect>
	}

	//#CLASS_NEW<TitleElement,Element>
	public partial class TitleElement{
		//--------------------------------------------------
		//	IHTMLTitleElement
		//--------------------------------------------------
		//#PROP<string,text>
	}

	//#CLASS_NEW<UlElement,ListElement>
	public partial class UlElement{
		//--------------------------------------------------
		//	IHTMLUlistElement3
		//--------------------------------------------------
		//#PROP<string,type>
	}

	//#CLASS_NEW<UnknownElement,Element>

	// ※ IHTMLTextContainer ⊂ Element
	// ※ IHTMLControlElement ⊂ Element
	// ※ HTMLTextContainerEvents → onchange onselect
	// ※ HTMLControlElementEvents ⊂ Element
}