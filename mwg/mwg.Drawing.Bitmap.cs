//Copyright, 村瀬功一, 2006.3

namespace mwg.Drawing{
	public class Bitmap{
		public System.Drawing.Bitmap data;
		public Bitmap(string filename){
			this.data=new System.Drawing.Bitmap(filename);
		}
		public Bitmap(int width,int height){
			this.data=new System.Drawing.Bitmap(width,height);//深さは24bitか32bitを指定するようにする
		}
	}
	
}