using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	//#include "mwg.Controls.WB.hs"

	//#CLASS<RenderStyle,MshtmlObject>
	/// <summary>
	/// ï∂éöóÒÇÃï`âÊï˚ñ@Ç…ä÷Ç∑ÇÈê›íËÇï€éùÇµÇ‹Ç∑ÅB
	/// </summary>
	public partial class RenderStyle{
		//--------------------------------------------------
		//		IHTMLStyleSheet
		//--------------------------------------------------
		//#PROP<string,defaultTextSelection>
		//#PROP<int,renderingPriority>
		//#PROP<object,textBackgroundColor>
		//#PROP<object,textColor>
		//#PROP<string,textDecoration>
		//#PROP<object,textDecorationColor>
		//#PROP<string,textEffect>
		//#PROP<string,textLineThroughStyle>
		//#PROP<string,textUnderlineStyle>
	}

	//#CLASS<StyleSheet,MshtmlObject>
	public partial class StyleSheet{
		//--------------------------------------------------
		//		IHTMLStyleSheet
		//--------------------------------------------------
		//#>>delete
		public int addImport(string url,int index){
			return (int)this.Invoke("addImport",url,index);
		}
		public int addRule(string selector,string style,int index){
			return (int)this.Invoke("addRule",selector,style,index);
		}
		public void removeImport(int index){
			this.Invoke("removeImport",index);
		}
		public void removeRule(int index){
			this.Invoke("removeRule",index);
		}
		//#<<delete
		//#PROP<string,cssText>
		//#PROP<string,href>
		//#PROP<string,id>
		//#PROPO_R<RAW("#CollectionBase<StyleSheet>#"),imports>
		//#PROP<string,media>
		//#PROPO_R<Element,owningElement>
		//#PROPO_R<StyleSheet,parentStyleSheet>
		//#PROP_R<bool,readOnly>
		//#PROPO_R<RAW("#CollectionBase<StyleRule>#"),rules>
		//#PROP<string,title>
		//#PROP_R<string,type>
		//--------------------------------------------------
		//		IHTMLStyleSheet
		//--------------------------------------------------
		//#>>delete
		public int addPageRule(string selector,string style,int index){
			return (int)this.Invoke("addPageRule",selector,style,index);
		}
		//#<<delete
		//#PROPO_R<RAW("#CollectionBase<StylePage>#"),pages>
	}

	//#CLASS<StylePage,MshtmlObject>
	public partial class StylePage{
		//--------------------------------------------------
		//		IHTMLStyleSheetPage
		//--------------------------------------------------
		//#PROP_R<string,pseudoClass>
		//#PROP_R<string,selector>
	}

	//#CLASS<StyleRule,MshtmlObject>
	public partial class StyleRule{
		//--------------------------------------------------
		//		IHTMLStyleSheetRule
		//--------------------------------------------------
		//#PROP_R<bool,readOnly>
		//#PROP<string,selectorText>
		//#PROPO_R<RuleStyle,style>
	}

	//#CLASS<CurrentStyle,MshtmlObject>
	//#CLASS<RuleStyle,MshtmlObject>
	//#CLASS_NEW<Style,RuleStyle>

	public partial class RuleStyle{
		//--------------------------------------------------
		//		IHTMLRuleStyle
		//--------------------------------------------------
		//#>>delete
		public object getAttribute(string attrName,int flags){
			return this.Invoke("getAttribute",attrName,flags);
		}
		public bool removeAttribute(string attrName,int flags){
			return (bool)this.Invoke("removeAttribute",attrName,flags);
		}
		public object setAttribute(string attrName,object value,int flags){
			return this.Invoke("setAttribute",attrName,value,flags);
		}
		//#<<delete
		//#PROP<string,background>
		//#PROP<string,backgroundAttachment>
		//#PROP<object,backgroundColor>
		//#PROP<string,backgroundImage>
		//#PROP<string,backgroundPosition>
		//#PROP<object,backgroundPositionX>
		//#PROP<object,backgroundPositionY>
		//#PROP<string,backgroundRepeat>
		//#PROP<string,border>
		//#PROP<string,borderColor>
		//#PROP<string,borderStyle>
		//#PROP<string,borderWidth>
		//#PROP<string,borderBottom>
		//#PROP<object,borderBottomColor>
		//#PROP<string,borderBottomStyle>
		//#PROP<object,borderBottomWidth>
		//#PROP<string,borderLeft>
		//#PROP<object,borderLeftColor>
		//#PROP<string,borderLeftStyle>
		//#PROP<object,borderLeftWidth>
		//#PROP<string,borderRight>
		//#PROP<object,borderRightColor>
		//#PROP<string,borderRightStyle>
		//#PROP<object,borderRightWidth>
		//#PROP<string,borderTop>
		//#PROP<object,borderTopColor>
		//#PROP<string,borderTopStyle>
		//#PROP<object,borderTopWidth>
		//#PROP<string,clear>
		//#PROP<string,clip>
		//#PROP<object,color>
		//#PROP<string,cssText>
		//#PROP<string,cursor>
		//#PROP<string,display>
		//#PROP<string,filter>
		//#PROP<string,font>
		//#PROP<string,fontFamily>
		//#PROP<object,fontSize>
		//#PROP<string,fontStyle>
		//#PROP<string,fontVariant>
		//#PROP<string,fontWeight>
		//#PROP<object,height>
		//#PROP<object,left>
		//#PROP<object,top>
		//#PROP<object,width>
		//#PROP<object,letterSpacing>
		//#PROP<object,lineHeight>
		//#PROP<string,listStyle>
		//#PROP<string,listStyleImage>
		//#PROP<string,listStylePosition>
		//#PROP<string,listStyleType>
		//#PROP<string,margin>
		//#PROP<object,marginBottom>
		//#PROP<object,marginLeft>
		//#PROP<object,marginRight>
		//#PROP<object,marginTop>
		//#PROP<string,overflow>
		//#PROP<string,padding>
		//#PROP<object,paddingBottom>
		//#PROP<object,paddingLeft>
		//#PROP<object,paddingRight>
		//#PROP<object,paddingTop>
		//#PROP<object,pageBreakAfter>
		//#PROP<object,pageBreakBefore>
		//#PROP<string,position>
		//#PROP<string,styleFloat>
		//#PROP<string,textAlign>
		//#PROP<string,textDecoration>
		//#PROP<bool,textDecorationBlink>
		//#PROP<bool,textDecorationLineThrough>
		//#PROP<bool,textDecorationNone>
		//#PROP<bool,textDecorationOverline>
		//#PROP<bool,textDecorationUnderline>
		//#PROP<object,textIndent>
		//#PROP<string,textTransform>
		//#PROP<object,verticalAlign>
		//#PROP<string,visibility>
		//#PROP<string,whiteSpace>
		//#PROP<object,wordSpacing>
		//#PROP<object,zIndex>
		//--------------------------------------------------
		//		IHTMLRuleStyle2
		//--------------------------------------------------
		//#PROP<string,accelerator>
		//#PROP<string,behavior>
		//#PROP<string,borderCollapse>
		//#PROP<object,bottom>
		//#PROP<object,right>
		//#PROP<string,direction>
		//#PROP<string,imeMode>
		//#PROP<string,layoutGrid>
		//#PROP<object,layoutGridChar>
		//#PROP<object,layoutGridLine>
		//#PROP<string,layoutGridMode>
		//#PROP<string,layoutGridType>
		//#PROP<string,lineBreak>
		//#PROP<string,overflowX>
		//#PROP<string,overflowY>
		//#PROP<int,pixelBottom>
		//#PROP<int,pixelRight>
		//#PROP<float,posBottom>
		//#PROP<float,posRight>
		//#PROP<string,rubyAlign>
		//#PROP<string,rubyOverhang>
		//#PROP<string,rubyPosition>
		//#PROP<string,tableLayout>
		//#PROP<string,textAutospace>
		//#PROP<string,textJustify>
		//#PROP<string,textJustifyTrim>
		//#PROP<object,textKashida>
		//#PROP<string,unicodeBidi>
		//#PROP<string,wordBreak>
		//--------------------------------------------------
		//		IHTMLRuleStyle3
		//--------------------------------------------------
		//#PROP<string,layoutFlow>
		//#PROP<object,scrollbar3dLightColor>
		//#PROP<object,scrollbarArrowColor>
		//#PROP<object,scrollbarBaseColor>
		//#PROP<object,scrollbarDarkShadowColor>
		//#PROP<object,scrollbarFaceColor>
		//#PROP<object,scrollbarHighlightColor>
		//#PROP<object,scrollbarShadowColor>
		//#PROP<object,scrollbarTrackColor>
		//#PROP<string,textAlignLast>
		//#PROP<object,textKashidaSpace>
		//#PROP<string,textUnderlinePosition>
		//#PROP<string,wordWrap>
		//#PROP<string,writingMode>
		//#PROP<object,zoom>
		//--------------------------------------------------
		//		IHTMLRuleStyle4
		//--------------------------------------------------
		//#PROP<object,minHeight>
		//#PROP<string,textOverflow>
	}

	// IHTMLStyle
	// IHTMLStyle2
	// IHTMLStyle3
	// IHTMLStyle4
	public partial class Style:RuleStyle{
		//--------------------------------------------------
		//		IHTMLStyle
		//--------------------------------------------------
		// inherits from IHTMLRuleStyle
		//#>>delete
		public string toString(){
			return (string)this.Invoke("toString");
		}
		//#<<delete
		//#PROP<int,pixelHeight>
		//#PROP<int,pixelLeft>
		//#PROP<int,pixelTop>
		//#PROP<int,pixelWidth>
		//#PROP<float,posHeight>
		//#PROP<float,posLeft>
		//#PROP<float,posTop>
		//#PROP<float,posWidth>
		//--------------------------------------------------
		//		IHTMLStyle2
		//--------------------------------------------------
		// inherits from IHTMLRuleStyle
		//#>>delete
		public object getExpression(string propName)	{return this.Invoke("getExpression",propName);}
		public bool removeExpression(string propName)	{return (bool)this.Invoke("removeExpression",propName);}
		public void setExpression(string propName,string expression,string lang){
			this.Invoke("setExpression",propName,expression,lang);
		}
		//#<<delete
		//--------------------------------------------------
		//		IHTMLStyle3
		//--------------------------------------------------
		// inherits from IHTMLRuleStyle
		//--------------------------------------------------
		//		IHTMLStyle4
		//--------------------------------------------------
		// inherits from IHTMLRuleStyle
	}

	// IHTMLCurrentStyle
	// IHTMLCurrentStyle2
	// IHTMLCurrentStyle3
	public partial class CurrentStyle{
		//--------------------------------------------------
		//		IHTMLCurrentStyle
		//--------------------------------------------------
		//#>>delete
		public object getAttribute(string attrName,int flags){
			return this.Invoke("getAttribute",attrName,flags);
		}
		//#<<delete
		//#PROP_R<string,accelerator>
		//#PROP_R<string,backgroundAttachment>
		//#PROP_R<object,backgroundColor>
		//#PROP_R<string,backgroundImage>
		//#PROP_R<string,backgroundPosition>
		//#PROP_R<object,backgroundPositionX>
		//#PROP_R<object,backgroundPositionY>
		//#PROP_R<string,backgroundRepeat>
		//#PROP_R<string,behavior>
		//#PROP_R<string,blockDirection>
		//#PROP_R<string,borderColor>
		//#PROP_R<string,borderStyle>
		//#PROP_R<string,borderWidth>
		//#PROP_R<object,borderBottomColor>
		//#PROP_R<string,borderBottomStyle>
		//#PROP_R<object,borderBottomWidth>
		//#PROP_R<object,borderLeftColor>
		//#PROP_R<string,borderLeftStyle>
		//#PROP_R<object,borderLeftWidth>
		//#PROP_R<object,borderRightColor>
		//#PROP_R<string,borderRightStyle>
		//#PROP_R<object,borderRightWidth>
		//#PROP_R<object,borderTopColor>
		//#PROP_R<string,borderTopStyle>
		//#PROP_R<object,borderTopWidth>
		//#PROP_R<string,borderCollapse>
		//#PROP_R<object,bottom>
		//#PROP_R<string,clear>
		//#PROP_R<object,clipBottom>
		//#PROP_R<object,clipLeft>
		//#PROP_R<object,clipRight>
		//#PROP_R<object,clipTop>
		//#PROP_R<object,color>
		//#PROP_R<string,cursor>
		//#PROP_R<string,direction>
		//#PROP_R<string,display>
		//#PROP_R<string,fontFamily>
		//#PROP_R<object,fontSize>
		//#PROP_R<string,fontStyle>
		//#PROP_R<string,fontVariant>
		//#PROP_R<string,fontWeight>
		//#PROP_R<object,height>
		//#PROP_R<string,imeMode>
		//#PROP_R<object,layoutGridChar>
		//#PROP_R<object,layoutGridLine>
		//#PROP_R<string,layoutGridMode>
		//#PROP_R<string,layoutGridType>
		//#PROP_R<object,left>
		//#PROP_R<object,letterSpacing>
		//#PROP_R<string,lineBreak>
		//#PROP_R<object,lineHeight>
		//#PROP_R<string,listStyleImage>
		//#PROP_R<string,listStylePosition>
		//#PROP_R<string,listStyleType>
		//#PROP_R<string,margin>
		//#PROP_R<object,marginBottom>
		//#PROP_R<object,marginLeft>
		//#PROP_R<object,marginRight>
		//#PROP_R<object,marginTop>
		//#PROP_R<string,overflow>
		//#PROP_R<string,overflowX>
		//#PROP_R<string,overflowY>
		//#PROP_R<string,padding>
		//#PROP_R<object,paddingBottom>
		//#PROP_R<object,paddingLeft>
		//#PROP_R<object,paddingRight>
		//#PROP_R<object,paddingTop>
		//#PROP_R<object,pageBreakAfter>
		//#PROP_R<object,pageBreakBefore>
		//#PROP_R<string,position>
		//#PROP_R<object,right>
		//#PROP_R<string,rubyAlign>
		//#PROP_R<string,rubyOverhang>
		//#PROP_R<string,rubyPosition>
		//#PROP_R<string,styleFloat>
		//#PROP_R<string,tableLayout>
		//#PROP_R<string,textAlign>
		//#PROP_R<string,textAutospace>
		//#PROP_R<string,textDecoration>
		//#PROP_R<object,textIndent>
		//#PROP_R<string,textJustify>
		//#PROP_R<string,textJustifyTrim>
		//#PROP_R<object,textKashida>
		//#PROP_R<string,textTransform>
		//#PROP_R<object,top>
		//#PROP_R<string,unicodeBidi>
		//#PROP_R<object,verticalAlign>
		//#PROP_R<string,visibility>
		//#PROP_R<object,width>
		//#PROP_R<string,wordBreak>
		//#PROP_R<object,zIndex>
		//--------------------------------------------------
		//		IHTMLCurrentStyle2
		//--------------------------------------------------
		//#PROP_R<string,filter>
		//#PROP_R<bool,hasLayout>
		//#PROP_R<bool,isBlock>
		//#PROP_R<string,layoutFlow>
		//#PROP_R<object,scrollbar3dLightColor>
		//#PROP_R<object,scrollbarArrowColor>
		//#PROP_R<object,scrollbarBaseColor>
		//#PROP_R<object,scrollbarDarkShadowColor>
		//#PROP_R<object,scrollbarFaceColor>
		//#PROP_R<object,scrollbarHighlightColor>
		//#PROP_R<object,scrollbarShadowColor>
		//#PROP_R<object,scrollbarTrackColor>
		//#PROP_R<string,textAlignLast>
		//#PROP_R<object,textKashidaSpace>
		//#PROP_R<string,textUnderlinePosition>
		//#PROP_R<string,wordWrap>
		//#PROP_R<string,writingMode>
		//#PROP_R<object,zoom>
		//--------------------------------------------------
		//		IHTMLCurrentStyle3
		//--------------------------------------------------
		//#PROP_R<object,minHeight>
		//#PROP_R<string,textOverflow>
		//#PROP_R<string,whiteSpace>
		//#PROP_R<object,wordSpacing>
	}
}