using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;
using Ref=System.Reflection;

namespace mwg.Controls.WebBrowser{
	public delegate void EHVoid();
	public delegate void EHError(string desc,string url,int line);
	public delegate bool EHCancel();
}