//Copyright, ��������, 2006.3

namespace mwg.Drawing{
	public class Bitmap{
		public System.Drawing.Bitmap data;
		public Bitmap(string filename){
			this.data=new System.Drawing.Bitmap(filename);
		}
		public Bitmap(int width,int height){
			this.data=new System.Drawing.Bitmap(width,height);//�[����24bit��32bit���w�肷��悤�ɂ���
		}
	}
	
}