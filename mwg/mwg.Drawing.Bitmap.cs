//Copyright, ‘º£Œ÷ˆê, 2006.3

namespace mwg.Drawing{
	public class Bitmap{
		public System.Drawing.Bitmap data;
		public Bitmap(string filename){
			this.data=new System.Drawing.Bitmap(filename);
		}
		public Bitmap(int width,int height){
			this.data=new System.Drawing.Bitmap(width,height);//[‚³‚Í24bit‚©32bit‚ğw’è‚·‚é‚æ‚¤‚É‚·‚é
		}
	}
	
}