
namespace mwg{
	public delegate void BoolEventHandler(object sender,mwg.BoolEventArgs e);
	public class BoolEventArgs{
		private bool val;
		public bool Value{get{return this.val;}}
		public BoolEventArgs(bool val){
			this.val=val;
		}
	}
}
