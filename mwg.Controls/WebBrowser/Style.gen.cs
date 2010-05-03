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


	public partial class RenderStyle:MshtmlObject{
		protected RenderStyle(object instance):base(instance){}
		public static RenderStyle FromObj(object obj){
			if(obj==null)return null;
			return new RenderStyle(obj);
		}
	}
	/// <summary>
	/// 文字列の描画方法に関する設定を保持します。
	/// </summary>
	public partial class RenderStyle{
		//--------------------------------------------------
		//		IHTMLStyleSheet
		//--------------------------------------------------
		public string defaultTextSelection{
			get{return (string)base["defaultTextSelection"];}
			set{base["defaultTextSelection"]=value;}
		}
		public int renderingPriority{
			get{return (int)base["renderingPriority"];}
			set{base["renderingPriority"]=value;}
		}
		public object textBackgroundColor{
			get{return (object)base["textBackgroundColor"];}
			set{base["textBackgroundColor"]=value;}
		}
		public object textColor{
			get{return (object)base["textColor"];}
			set{base["textColor"]=value;}
		}
		public string textDecoration{
			get{return (string)base["textDecoration"];}
			set{base["textDecoration"]=value;}
		}
		public object textDecorationColor{
			get{return (object)base["textDecorationColor"];}
			set{base["textDecorationColor"]=value;}
		}
		public string textEffect{
			get{return (string)base["textEffect"];}
			set{base["textEffect"]=value;}
		}
		public string textLineThroughStyle{
			get{return (string)base["textLineThroughStyle"];}
			set{base["textLineThroughStyle"]=value;}
		}
		public string textUnderlineStyle{
			get{return (string)base["textUnderlineStyle"];}
			set{base["textUnderlineStyle"]=value;}
		}
	}

	public partial class StyleSheet:MshtmlObject{
		protected StyleSheet(object instance):base(instance){}
		public static StyleSheet FromObj(object obj){
			if(obj==null)return null;
			return new StyleSheet(obj);
		}
	}
	public partial class StyleSheet{
		//--------------------------------------------------
		//		IHTMLStyleSheet
		//--------------------------------------------------

		public string cssText{
			get{return (string)base["cssText"];}
			set{base["cssText"]=value;}
		}
		public string href{
			get{return (string)base["href"];}
			set{base["href"]=value;}
		}
		public string id{
			get{return (string)base["id"];}
			set{base["id"]=value;}
		}
		public CollectionBase<StyleSheet> imports{
			get{return CollectionBase<StyleSheet>.FromObj(base["imports"]);}
		}
		public string media{
			get{return (string)base["media"];}
			set{base["media"]=value;}
		}
		public Element owningElement{
			get{return Element.FromObj(base["owningElement"]);}
		}
		public StyleSheet parentStyleSheet{
			get{return StyleSheet.FromObj(base["parentStyleSheet"]);}
		}
		public bool readOnly{
			get{return (bool)base["readOnly"];}
		}
		public CollectionBase<StyleRule> rules{
			get{return CollectionBase<StyleRule>.FromObj(base["rules"]);}
		}
		public string title{
			get{return (string)base["title"];}
			set{base["title"]=value;}
		}
		public string type{
			get{return (string)base["type"];}
		}
		//--------------------------------------------------
		//		IHTMLStyleSheet
		//--------------------------------------------------

		public CollectionBase<StylePage> pages{
			get{return CollectionBase<StylePage>.FromObj(base["pages"]);}
		}
	}

	public partial class StylePage:MshtmlObject{
		protected StylePage(object instance):base(instance){}
		public static StylePage FromObj(object obj){
			if(obj==null)return null;
			return new StylePage(obj);
		}
	}
	public partial class StylePage{
		//--------------------------------------------------
		//		IHTMLStyleSheetPage
		//--------------------------------------------------
		public string pseudoClass{
			get{return (string)base["pseudoClass"];}
		}
		public string selector{
			get{return (string)base["selector"];}
		}
	}

	public partial class StyleRule:MshtmlObject{
		protected StyleRule(object instance):base(instance){}
		public static StyleRule FromObj(object obj){
			if(obj==null)return null;
			return new StyleRule(obj);
		}
	}
	public partial class StyleRule{
		//--------------------------------------------------
		//		IHTMLStyleSheetRule
		//--------------------------------------------------
		public bool readOnly{
			get{return (bool)base["readOnly"];}
		}
		public string selectorText{
			get{return (string)base["selectorText"];}
			set{base["selectorText"]=value;}
		}
		public RuleStyle style{
			get{return RuleStyle.FromObj(base["style"]);}
		}
	}

	public partial class CurrentStyle:MshtmlObject{
		protected CurrentStyle(object instance):base(instance){}
		public static CurrentStyle FromObj(object obj){
			if(obj==null)return null;
			return new CurrentStyle(obj);
		}
	}
	public partial class RuleStyle:MshtmlObject{
		protected RuleStyle(object instance):base(instance){}
		public static RuleStyle FromObj(object obj){
			if(obj==null)return null;
			return new RuleStyle(obj);
		}
	}
	public partial class Style:RuleStyle{
		protected Style(object instance):base(instance){}
		public static new Style FromObj(object obj){
			if(obj==null)return null;
			return new Style(obj);
		}
	}

	public partial class RuleStyle{
		//--------------------------------------------------
		//		IHTMLRuleStyle
		//--------------------------------------------------

		public string background{
			get{return (string)base["background"];}
			set{base["background"]=value;}
		}
		public string backgroundAttachment{
			get{return (string)base["backgroundAttachment"];}
			set{base["backgroundAttachment"]=value;}
		}
		public object backgroundColor{
			get{return (object)base["backgroundColor"];}
			set{base["backgroundColor"]=value;}
		}
		public string backgroundImage{
			get{return (string)base["backgroundImage"];}
			set{base["backgroundImage"]=value;}
		}
		public string backgroundPosition{
			get{return (string)base["backgroundPosition"];}
			set{base["backgroundPosition"]=value;}
		}
		public object backgroundPositionX{
			get{return (object)base["backgroundPositionX"];}
			set{base["backgroundPositionX"]=value;}
		}
		public object backgroundPositionY{
			get{return (object)base["backgroundPositionY"];}
			set{base["backgroundPositionY"]=value;}
		}
		public string backgroundRepeat{
			get{return (string)base["backgroundRepeat"];}
			set{base["backgroundRepeat"]=value;}
		}
		public string border{
			get{return (string)base["border"];}
			set{base["border"]=value;}
		}
		public string borderColor{
			get{return (string)base["borderColor"];}
			set{base["borderColor"]=value;}
		}
		public string borderStyle{
			get{return (string)base["borderStyle"];}
			set{base["borderStyle"]=value;}
		}
		public string borderWidth{
			get{return (string)base["borderWidth"];}
			set{base["borderWidth"]=value;}
		}
		public string borderBottom{
			get{return (string)base["borderBottom"];}
			set{base["borderBottom"]=value;}
		}
		public object borderBottomColor{
			get{return (object)base["borderBottomColor"];}
			set{base["borderBottomColor"]=value;}
		}
		public string borderBottomStyle{
			get{return (string)base["borderBottomStyle"];}
			set{base["borderBottomStyle"]=value;}
		}
		public object borderBottomWidth{
			get{return (object)base["borderBottomWidth"];}
			set{base["borderBottomWidth"]=value;}
		}
		public string borderLeft{
			get{return (string)base["borderLeft"];}
			set{base["borderLeft"]=value;}
		}
		public object borderLeftColor{
			get{return (object)base["borderLeftColor"];}
			set{base["borderLeftColor"]=value;}
		}
		public string borderLeftStyle{
			get{return (string)base["borderLeftStyle"];}
			set{base["borderLeftStyle"]=value;}
		}
		public object borderLeftWidth{
			get{return (object)base["borderLeftWidth"];}
			set{base["borderLeftWidth"]=value;}
		}
		public string borderRight{
			get{return (string)base["borderRight"];}
			set{base["borderRight"]=value;}
		}
		public object borderRightColor{
			get{return (object)base["borderRightColor"];}
			set{base["borderRightColor"]=value;}
		}
		public string borderRightStyle{
			get{return (string)base["borderRightStyle"];}
			set{base["borderRightStyle"]=value;}
		}
		public object borderRightWidth{
			get{return (object)base["borderRightWidth"];}
			set{base["borderRightWidth"]=value;}
		}
		public string borderTop{
			get{return (string)base["borderTop"];}
			set{base["borderTop"]=value;}
		}
		public object borderTopColor{
			get{return (object)base["borderTopColor"];}
			set{base["borderTopColor"]=value;}
		}
		public string borderTopStyle{
			get{return (string)base["borderTopStyle"];}
			set{base["borderTopStyle"]=value;}
		}
		public object borderTopWidth{
			get{return (object)base["borderTopWidth"];}
			set{base["borderTopWidth"]=value;}
		}
		public string clear{
			get{return (string)base["clear"];}
			set{base["clear"]=value;}
		}
		public string clip{
			get{return (string)base["clip"];}
			set{base["clip"]=value;}
		}
		public object color{
			get{return (object)base["color"];}
			set{base["color"]=value;}
		}
		public string cssText{
			get{return (string)base["cssText"];}
			set{base["cssText"]=value;}
		}
		public string cursor{
			get{return (string)base["cursor"];}
			set{base["cursor"]=value;}
		}
		public string display{
			get{return (string)base["display"];}
			set{base["display"]=value;}
		}
		public string filter{
			get{return (string)base["filter"];}
			set{base["filter"]=value;}
		}
		public string font{
			get{return (string)base["font"];}
			set{base["font"]=value;}
		}
		public string fontFamily{
			get{return (string)base["fontFamily"];}
			set{base["fontFamily"]=value;}
		}
		public object fontSize{
			get{return (object)base["fontSize"];}
			set{base["fontSize"]=value;}
		}
		public string fontStyle{
			get{return (string)base["fontStyle"];}
			set{base["fontStyle"]=value;}
		}
		public string fontVariant{
			get{return (string)base["fontVariant"];}
			set{base["fontVariant"]=value;}
		}
		public string fontWeight{
			get{return (string)base["fontWeight"];}
			set{base["fontWeight"]=value;}
		}
		public object height{
			get{return (object)base["height"];}
			set{base["height"]=value;}
		}
		public object left{
			get{return (object)base["left"];}
			set{base["left"]=value;}
		}
		public object top{
			get{return (object)base["top"];}
			set{base["top"]=value;}
		}
		public object width{
			get{return (object)base["width"];}
			set{base["width"]=value;}
		}
		public object letterSpacing{
			get{return (object)base["letterSpacing"];}
			set{base["letterSpacing"]=value;}
		}
		public object lineHeight{
			get{return (object)base["lineHeight"];}
			set{base["lineHeight"]=value;}
		}
		public string listStyle{
			get{return (string)base["listStyle"];}
			set{base["listStyle"]=value;}
		}
		public string listStyleImage{
			get{return (string)base["listStyleImage"];}
			set{base["listStyleImage"]=value;}
		}
		public string listStylePosition{
			get{return (string)base["listStylePosition"];}
			set{base["listStylePosition"]=value;}
		}
		public string listStyleType{
			get{return (string)base["listStyleType"];}
			set{base["listStyleType"]=value;}
		}
		public string margin{
			get{return (string)base["margin"];}
			set{base["margin"]=value;}
		}
		public object marginBottom{
			get{return (object)base["marginBottom"];}
			set{base["marginBottom"]=value;}
		}
		public object marginLeft{
			get{return (object)base["marginLeft"];}
			set{base["marginLeft"]=value;}
		}
		public object marginRight{
			get{return (object)base["marginRight"];}
			set{base["marginRight"]=value;}
		}
		public object marginTop{
			get{return (object)base["marginTop"];}
			set{base["marginTop"]=value;}
		}
		public string overflow{
			get{return (string)base["overflow"];}
			set{base["overflow"]=value;}
		}
		public string padding{
			get{return (string)base["padding"];}
			set{base["padding"]=value;}
		}
		public object paddingBottom{
			get{return (object)base["paddingBottom"];}
			set{base["paddingBottom"]=value;}
		}
		public object paddingLeft{
			get{return (object)base["paddingLeft"];}
			set{base["paddingLeft"]=value;}
		}
		public object paddingRight{
			get{return (object)base["paddingRight"];}
			set{base["paddingRight"]=value;}
		}
		public object paddingTop{
			get{return (object)base["paddingTop"];}
			set{base["paddingTop"]=value;}
		}
		public object pageBreakAfter{
			get{return (object)base["pageBreakAfter"];}
			set{base["pageBreakAfter"]=value;}
		}
		public object pageBreakBefore{
			get{return (object)base["pageBreakBefore"];}
			set{base["pageBreakBefore"]=value;}
		}
		public string position{
			get{return (string)base["position"];}
			set{base["position"]=value;}
		}
		public string styleFloat{
			get{return (string)base["styleFloat"];}
			set{base["styleFloat"]=value;}
		}
		public string textAlign{
			get{return (string)base["textAlign"];}
			set{base["textAlign"]=value;}
		}
		public string textDecoration{
			get{return (string)base["textDecoration"];}
			set{base["textDecoration"]=value;}
		}
		public bool textDecorationBlink{
			get{return (bool)base["textDecorationBlink"];}
			set{base["textDecorationBlink"]=value;}
		}
		public bool textDecorationLineThrough{
			get{return (bool)base["textDecorationLineThrough"];}
			set{base["textDecorationLineThrough"]=value;}
		}
		public bool textDecorationNone{
			get{return (bool)base["textDecorationNone"];}
			set{base["textDecorationNone"]=value;}
		}
		public bool textDecorationOverline{
			get{return (bool)base["textDecorationOverline"];}
			set{base["textDecorationOverline"]=value;}
		}
		public bool textDecorationUnderline{
			get{return (bool)base["textDecorationUnderline"];}
			set{base["textDecorationUnderline"]=value;}
		}
		public object textIndent{
			get{return (object)base["textIndent"];}
			set{base["textIndent"]=value;}
		}
		public string textTransform{
			get{return (string)base["textTransform"];}
			set{base["textTransform"]=value;}
		}
		public object verticalAlign{
			get{return (object)base["verticalAlign"];}
			set{base["verticalAlign"]=value;}
		}
		public string visibility{
			get{return (string)base["visibility"];}
			set{base["visibility"]=value;}
		}
		public string whiteSpace{
			get{return (string)base["whiteSpace"];}
			set{base["whiteSpace"]=value;}
		}
		public object wordSpacing{
			get{return (object)base["wordSpacing"];}
			set{base["wordSpacing"]=value;}
		}
		public object zIndex{
			get{return (object)base["zIndex"];}
			set{base["zIndex"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLRuleStyle2
		//--------------------------------------------------
		public string accelerator{
			get{return (string)base["accelerator"];}
			set{base["accelerator"]=value;}
		}
		public string behavior{
			get{return (string)base["behavior"];}
			set{base["behavior"]=value;}
		}
		public string borderCollapse{
			get{return (string)base["borderCollapse"];}
			set{base["borderCollapse"]=value;}
		}
		public object bottom{
			get{return (object)base["bottom"];}
			set{base["bottom"]=value;}
		}
		public object right{
			get{return (object)base["right"];}
			set{base["right"]=value;}
		}
		public string direction{
			get{return (string)base["direction"];}
			set{base["direction"]=value;}
		}
		public string imeMode{
			get{return (string)base["imeMode"];}
			set{base["imeMode"]=value;}
		}
		public string layoutGrid{
			get{return (string)base["layoutGrid"];}
			set{base["layoutGrid"]=value;}
		}
		public object layoutGridChar{
			get{return (object)base["layoutGridChar"];}
			set{base["layoutGridChar"]=value;}
		}
		public object layoutGridLine{
			get{return (object)base["layoutGridLine"];}
			set{base["layoutGridLine"]=value;}
		}
		public string layoutGridMode{
			get{return (string)base["layoutGridMode"];}
			set{base["layoutGridMode"]=value;}
		}
		public string layoutGridType{
			get{return (string)base["layoutGridType"];}
			set{base["layoutGridType"]=value;}
		}
		public string lineBreak{
			get{return (string)base["lineBreak"];}
			set{base["lineBreak"]=value;}
		}
		public string overflowX{
			get{return (string)base["overflowX"];}
			set{base["overflowX"]=value;}
		}
		public string overflowY{
			get{return (string)base["overflowY"];}
			set{base["overflowY"]=value;}
		}
		public int pixelBottom{
			get{return (int)base["pixelBottom"];}
			set{base["pixelBottom"]=value;}
		}
		public int pixelRight{
			get{return (int)base["pixelRight"];}
			set{base["pixelRight"]=value;}
		}
		public float posBottom{
			get{return (float)base["posBottom"];}
			set{base["posBottom"]=value;}
		}
		public float posRight{
			get{return (float)base["posRight"];}
			set{base["posRight"]=value;}
		}
		public string rubyAlign{
			get{return (string)base["rubyAlign"];}
			set{base["rubyAlign"]=value;}
		}
		public string rubyOverhang{
			get{return (string)base["rubyOverhang"];}
			set{base["rubyOverhang"]=value;}
		}
		public string rubyPosition{
			get{return (string)base["rubyPosition"];}
			set{base["rubyPosition"]=value;}
		}
		public string tableLayout{
			get{return (string)base["tableLayout"];}
			set{base["tableLayout"]=value;}
		}
		public string textAutospace{
			get{return (string)base["textAutospace"];}
			set{base["textAutospace"]=value;}
		}
		public string textJustify{
			get{return (string)base["textJustify"];}
			set{base["textJustify"]=value;}
		}
		public string textJustifyTrim{
			get{return (string)base["textJustifyTrim"];}
			set{base["textJustifyTrim"]=value;}
		}
		public object textKashida{
			get{return (object)base["textKashida"];}
			set{base["textKashida"]=value;}
		}
		public string unicodeBidi{
			get{return (string)base["unicodeBidi"];}
			set{base["unicodeBidi"]=value;}
		}
		public string wordBreak{
			get{return (string)base["wordBreak"];}
			set{base["wordBreak"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLRuleStyle3
		//--------------------------------------------------
		public string layoutFlow{
			get{return (string)base["layoutFlow"];}
			set{base["layoutFlow"]=value;}
		}
		public object scrollbar3dLightColor{
			get{return (object)base["scrollbar3dLightColor"];}
			set{base["scrollbar3dLightColor"]=value;}
		}
		public object scrollbarArrowColor{
			get{return (object)base["scrollbarArrowColor"];}
			set{base["scrollbarArrowColor"]=value;}
		}
		public object scrollbarBaseColor{
			get{return (object)base["scrollbarBaseColor"];}
			set{base["scrollbarBaseColor"]=value;}
		}
		public object scrollbarDarkShadowColor{
			get{return (object)base["scrollbarDarkShadowColor"];}
			set{base["scrollbarDarkShadowColor"]=value;}
		}
		public object scrollbarFaceColor{
			get{return (object)base["scrollbarFaceColor"];}
			set{base["scrollbarFaceColor"]=value;}
		}
		public object scrollbarHighlightColor{
			get{return (object)base["scrollbarHighlightColor"];}
			set{base["scrollbarHighlightColor"]=value;}
		}
		public object scrollbarShadowColor{
			get{return (object)base["scrollbarShadowColor"];}
			set{base["scrollbarShadowColor"]=value;}
		}
		public object scrollbarTrackColor{
			get{return (object)base["scrollbarTrackColor"];}
			set{base["scrollbarTrackColor"]=value;}
		}
		public string textAlignLast{
			get{return (string)base["textAlignLast"];}
			set{base["textAlignLast"]=value;}
		}
		public object textKashidaSpace{
			get{return (object)base["textKashidaSpace"];}
			set{base["textKashidaSpace"]=value;}
		}
		public string textUnderlinePosition{
			get{return (string)base["textUnderlinePosition"];}
			set{base["textUnderlinePosition"]=value;}
		}
		public string wordWrap{
			get{return (string)base["wordWrap"];}
			set{base["wordWrap"]=value;}
		}
		public string writingMode{
			get{return (string)base["writingMode"];}
			set{base["writingMode"]=value;}
		}
		public object zoom{
			get{return (object)base["zoom"];}
			set{base["zoom"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLRuleStyle4
		//--------------------------------------------------
		public object minHeight{
			get{return (object)base["minHeight"];}
			set{base["minHeight"]=value;}
		}
		public string textOverflow{
			get{return (string)base["textOverflow"];}
			set{base["textOverflow"]=value;}
		}
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

		public int pixelHeight{
			get{return (int)base["pixelHeight"];}
			set{base["pixelHeight"]=value;}
		}
		public int pixelLeft{
			get{return (int)base["pixelLeft"];}
			set{base["pixelLeft"]=value;}
		}
		public int pixelTop{
			get{return (int)base["pixelTop"];}
			set{base["pixelTop"]=value;}
		}
		public int pixelWidth{
			get{return (int)base["pixelWidth"];}
			set{base["pixelWidth"]=value;}
		}
		public float posHeight{
			get{return (float)base["posHeight"];}
			set{base["posHeight"]=value;}
		}
		public float posLeft{
			get{return (float)base["posLeft"];}
			set{base["posLeft"]=value;}
		}
		public float posTop{
			get{return (float)base["posTop"];}
			set{base["posTop"]=value;}
		}
		public float posWidth{
			get{return (float)base["posWidth"];}
			set{base["posWidth"]=value;}
		}
		//--------------------------------------------------
		//		IHTMLStyle2
		//--------------------------------------------------
		// inherits from IHTMLRuleStyle

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

		public string accelerator{
			get{return (string)base["accelerator"];}
		}
		public string backgroundAttachment{
			get{return (string)base["backgroundAttachment"];}
		}
		public object backgroundColor{
			get{return (object)base["backgroundColor"];}
		}
		public string backgroundImage{
			get{return (string)base["backgroundImage"];}
		}
		public string backgroundPosition{
			get{return (string)base["backgroundPosition"];}
		}
		public object backgroundPositionX{
			get{return (object)base["backgroundPositionX"];}
		}
		public object backgroundPositionY{
			get{return (object)base["backgroundPositionY"];}
		}
		public string backgroundRepeat{
			get{return (string)base["backgroundRepeat"];}
		}
		public string behavior{
			get{return (string)base["behavior"];}
		}
		public string blockDirection{
			get{return (string)base["blockDirection"];}
		}
		public string borderColor{
			get{return (string)base["borderColor"];}
		}
		public string borderStyle{
			get{return (string)base["borderStyle"];}
		}
		public string borderWidth{
			get{return (string)base["borderWidth"];}
		}
		public object borderBottomColor{
			get{return (object)base["borderBottomColor"];}
		}
		public string borderBottomStyle{
			get{return (string)base["borderBottomStyle"];}
		}
		public object borderBottomWidth{
			get{return (object)base["borderBottomWidth"];}
		}
		public object borderLeftColor{
			get{return (object)base["borderLeftColor"];}
		}
		public string borderLeftStyle{
			get{return (string)base["borderLeftStyle"];}
		}
		public object borderLeftWidth{
			get{return (object)base["borderLeftWidth"];}
		}
		public object borderRightColor{
			get{return (object)base["borderRightColor"];}
		}
		public string borderRightStyle{
			get{return (string)base["borderRightStyle"];}
		}
		public object borderRightWidth{
			get{return (object)base["borderRightWidth"];}
		}
		public object borderTopColor{
			get{return (object)base["borderTopColor"];}
		}
		public string borderTopStyle{
			get{return (string)base["borderTopStyle"];}
		}
		public object borderTopWidth{
			get{return (object)base["borderTopWidth"];}
		}
		public string borderCollapse{
			get{return (string)base["borderCollapse"];}
		}
		public object bottom{
			get{return (object)base["bottom"];}
		}
		public string clear{
			get{return (string)base["clear"];}
		}
		public object clipBottom{
			get{return (object)base["clipBottom"];}
		}
		public object clipLeft{
			get{return (object)base["clipLeft"];}
		}
		public object clipRight{
			get{return (object)base["clipRight"];}
		}
		public object clipTop{
			get{return (object)base["clipTop"];}
		}
		public object color{
			get{return (object)base["color"];}
		}
		public string cursor{
			get{return (string)base["cursor"];}
		}
		public string direction{
			get{return (string)base["direction"];}
		}
		public string display{
			get{return (string)base["display"];}
		}
		public string fontFamily{
			get{return (string)base["fontFamily"];}
		}
		public object fontSize{
			get{return (object)base["fontSize"];}
		}
		public string fontStyle{
			get{return (string)base["fontStyle"];}
		}
		public string fontVariant{
			get{return (string)base["fontVariant"];}
		}
		public string fontWeight{
			get{return (string)base["fontWeight"];}
		}
		public object height{
			get{return (object)base["height"];}
		}
		public string imeMode{
			get{return (string)base["imeMode"];}
		}
		public object layoutGridChar{
			get{return (object)base["layoutGridChar"];}
		}
		public object layoutGridLine{
			get{return (object)base["layoutGridLine"];}
		}
		public string layoutGridMode{
			get{return (string)base["layoutGridMode"];}
		}
		public string layoutGridType{
			get{return (string)base["layoutGridType"];}
		}
		public object left{
			get{return (object)base["left"];}
		}
		public object letterSpacing{
			get{return (object)base["letterSpacing"];}
		}
		public string lineBreak{
			get{return (string)base["lineBreak"];}
		}
		public object lineHeight{
			get{return (object)base["lineHeight"];}
		}
		public string listStyleImage{
			get{return (string)base["listStyleImage"];}
		}
		public string listStylePosition{
			get{return (string)base["listStylePosition"];}
		}
		public string listStyleType{
			get{return (string)base["listStyleType"];}
		}
		public string margin{
			get{return (string)base["margin"];}
		}
		public object marginBottom{
			get{return (object)base["marginBottom"];}
		}
		public object marginLeft{
			get{return (object)base["marginLeft"];}
		}
		public object marginRight{
			get{return (object)base["marginRight"];}
		}
		public object marginTop{
			get{return (object)base["marginTop"];}
		}
		public string overflow{
			get{return (string)base["overflow"];}
		}
		public string overflowX{
			get{return (string)base["overflowX"];}
		}
		public string overflowY{
			get{return (string)base["overflowY"];}
		}
		public string padding{
			get{return (string)base["padding"];}
		}
		public object paddingBottom{
			get{return (object)base["paddingBottom"];}
		}
		public object paddingLeft{
			get{return (object)base["paddingLeft"];}
		}
		public object paddingRight{
			get{return (object)base["paddingRight"];}
		}
		public object paddingTop{
			get{return (object)base["paddingTop"];}
		}
		public object pageBreakAfter{
			get{return (object)base["pageBreakAfter"];}
		}
		public object pageBreakBefore{
			get{return (object)base["pageBreakBefore"];}
		}
		public string position{
			get{return (string)base["position"];}
		}
		public object right{
			get{return (object)base["right"];}
		}
		public string rubyAlign{
			get{return (string)base["rubyAlign"];}
		}
		public string rubyOverhang{
			get{return (string)base["rubyOverhang"];}
		}
		public string rubyPosition{
			get{return (string)base["rubyPosition"];}
		}
		public string styleFloat{
			get{return (string)base["styleFloat"];}
		}
		public string tableLayout{
			get{return (string)base["tableLayout"];}
		}
		public string textAlign{
			get{return (string)base["textAlign"];}
		}
		public string textAutospace{
			get{return (string)base["textAutospace"];}
		}
		public string textDecoration{
			get{return (string)base["textDecoration"];}
		}
		public object textIndent{
			get{return (object)base["textIndent"];}
		}
		public string textJustify{
			get{return (string)base["textJustify"];}
		}
		public string textJustifyTrim{
			get{return (string)base["textJustifyTrim"];}
		}
		public object textKashida{
			get{return (object)base["textKashida"];}
		}
		public string textTransform{
			get{return (string)base["textTransform"];}
		}
		public object top{
			get{return (object)base["top"];}
		}
		public string unicodeBidi{
			get{return (string)base["unicodeBidi"];}
		}
		public object verticalAlign{
			get{return (object)base["verticalAlign"];}
		}
		public string visibility{
			get{return (string)base["visibility"];}
		}
		public object width{
			get{return (object)base["width"];}
		}
		public string wordBreak{
			get{return (string)base["wordBreak"];}
		}
		public object zIndex{
			get{return (object)base["zIndex"];}
		}
		//--------------------------------------------------
		//		IHTMLCurrentStyle2
		//--------------------------------------------------
		public string filter{
			get{return (string)base["filter"];}
		}
		public bool hasLayout{
			get{return (bool)base["hasLayout"];}
		}
		public bool isBlock{
			get{return (bool)base["isBlock"];}
		}
		public string layoutFlow{
			get{return (string)base["layoutFlow"];}
		}
		public object scrollbar3dLightColor{
			get{return (object)base["scrollbar3dLightColor"];}
		}
		public object scrollbarArrowColor{
			get{return (object)base["scrollbarArrowColor"];}
		}
		public object scrollbarBaseColor{
			get{return (object)base["scrollbarBaseColor"];}
		}
		public object scrollbarDarkShadowColor{
			get{return (object)base["scrollbarDarkShadowColor"];}
		}
		public object scrollbarFaceColor{
			get{return (object)base["scrollbarFaceColor"];}
		}
		public object scrollbarHighlightColor{
			get{return (object)base["scrollbarHighlightColor"];}
		}
		public object scrollbarShadowColor{
			get{return (object)base["scrollbarShadowColor"];}
		}
		public object scrollbarTrackColor{
			get{return (object)base["scrollbarTrackColor"];}
		}
		public string textAlignLast{
			get{return (string)base["textAlignLast"];}
		}
		public object textKashidaSpace{
			get{return (object)base["textKashidaSpace"];}
		}
		public string textUnderlinePosition{
			get{return (string)base["textUnderlinePosition"];}
		}
		public string wordWrap{
			get{return (string)base["wordWrap"];}
		}
		public string writingMode{
			get{return (string)base["writingMode"];}
		}
		public object zoom{
			get{return (object)base["zoom"];}
		}
		//--------------------------------------------------
		//		IHTMLCurrentStyle3
		//--------------------------------------------------
		public object minHeight{
			get{return (object)base["minHeight"];}
		}
		public string textOverflow{
			get{return (string)base["textOverflow"];}
		}
		public string whiteSpace{
			get{return (string)base["whiteSpace"];}
		}
		public object wordSpacing{
			get{return (object)base["wordSpacing"];}
		}
	}
}