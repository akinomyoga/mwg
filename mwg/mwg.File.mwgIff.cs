#define version0_2
#define version0_3

namespace mwg.File{

#if version0_1
	/// <summary>
	/// 独自のファイル形式の計画
	/// </summary>
	public class mwgIff{
		//=====================================
		//          fields
		//-------------------------------------
		public string path;//path windowsファイル名
		public uint version;
		public mfString name;//name 内部ファイル名
		public mfTime madetime,lasttime;
		public mfString annex;//付属文書
		public mwgIffNodeArray childs;
		//=====================================
		//          constructor
		//-------------------------------------
		public mwgIff(string filename){
			System.Text.UnicodeEncoding uni=new System.Text.UnicodeEncoding();
			uint i,imax;
			
			byte[] data=mwg.File.mwgBinary.WholeFileInBinary(filename);
			imax=(uint)data.Length;
			this.path=filename;
			
			//ファイル識別子の確認とversion の取得
			if((string)(new mwgFourcc(data,0))!="mIff")return;//◆
			this.version=(uint)(new mwgDword(data,4));i=8;
			//内部ファイル名(ディレクトリ名)の取得
			this.name=new mfString(data,i);i+=this.name.Length;
			//年月日の取得
			this.madetime=new mfTime(data,i);i+=5;
			this.lasttime=new mfTime(data,i);i+=5;
			//固有情報(annex)の取得
			this.annex=new mfString(data,i);i+=this.annex.Length;
			//子要素の取得
			uint x=i+(uint)(new mwgDword(data,i));i+=4;
			if(x<imax)imax=x;
			while(i<imax){
				switch((string)(new mwgFourcc(data,i))){
					case "DIR ":
						childs.Add(new mwgIffDir(data,i));
						break;
					case "FILE":
						childs.Add(new mwgIffFile(data,i));
						break;
					default:
						childs.Add(new mwgIffData(data,i));
						break;
				}
				i+=childs.Item(childs.Length-1).Length;
			}
		}
		//■Length
		//=====================================
		//          static members
		//-------------------------------------
		public static string readString(byte[] data,int offset,int bytelen){
			if(data.Length<offset+bytelen||offset<0)return "";return "■";
		}
		public static byte[] readByteData(byte[] data,uint offset,uint len){
			if(data.Length<offset+len||offset<0)return new byte[]{};
			byte[] r=new byte[len];
			for(int i=0;i<len;i++)r[i]=data[offset+i];
			return r;
		}
		//=====================================
		//          子(Node)クラスの定義
		//-------------------------------------
		public interface mwgIffNode{
			string ID{get;set;}
			byte[] ToBinary();
			uint Length{get;}
		}
		
		#region class mwgIffDir
		public class mwgIffDir:mwgIffNode{
			//fields
			mwgFourcc identifier;
			public mfString name;
			public mfTime madetime;
			public mfTime lasttime;
			public mfString annex;
			//postlenはとばし読みの時に使う。
			public mwgIffNodeArray childs;
			private uint selflen;//子要素を除く長さ
			
			//constructor
			public mwgIffDir(byte[] data,uint offset){
				uint i=offset;
				//id
				identifier=new mwgFourcc(data,i);
				if((string)(identifier)!="DIR "){
					throw new System.Exception("mwgIff データの読み取り中にエラーが発生しました。ディレクトリでない情報を誤ってディレクトリ'DIR 'として処理しようとしました。");
				}
				i+=4;
				//name
				name=new mfString(data,i);
				i+=this.name.Length;
				//made,lastmodified
				this.madetime=new mfTime(data,i);i+=5;
				this.lasttime=new mfTime(data,i);i+=5;
				//data
				annex=new mfString(data,i);
				i+=this.annex.Length;
				//子要素の最後
				uint imax=i+4+(uint)(new mwgDword(data,i));
				if(imax>data.Length)imax=(uint)data.Length;
				i+=4;
				this.selflen=i;
				//◆以上 runover の可能性在り
				
				//子要素
				while(i<imax){
					switch((string)(new mwgFourcc(data,i))){
						case "DIR ":
							childs.Add(new mwgIffDir(data,i));
							break;
						case "FILE":
							childs.Add(new mwgIffFile(data,i));
							break;
						default:
							childs.Add(new mwgIffData(data,i));
							break;
					}
					i+=(uint)childs.Item(childs.Length-1).Length;
				}
			}
			//inheritance of interface
			public byte[] ToBinary(){
				byte[][] childArray=new byte[this.childs.Length][];
				uint i,k;
				uint l=this.selflen;
				for(k=0;k<this.childs.Length;k++){
					childArray[k]=this.childs.Item(k).ToBinary();
					l+=(uint)childArray[k].Length;
				}
				byte[] r=new byte[l];
				i=0;
				((byte[])this.identifier).CopyTo(r,i);i+=4;
				((byte[])this.name).CopyTo(r,i);i+=this.name.Length;
				((byte[])this.madetime).CopyTo(r,i);i+=5;
				((byte[])this.lasttime).CopyTo(r,i);i+=5;
				((byte[])this.annex).CopyTo(r,i);i+=this.annex.Length;
				((byte[])new mwgDword(l-this.selflen)).CopyTo(r,i);i+=4;
				for(k=0;k<this.childs.Length;k++){
					childArray[k].CopyTo(r,i);
					i+=(uint)childArray[k].Length;
				}
				return r;
			}
			public string ID{
				get{return (string)this.identifier;}
				set{this.identifier=(mwgFourcc)value;}
			}
			public uint Length{
				get{
					uint r=this.selflen;
					//◆selflenはファイル名や保持データなどの改変によって再計算するようにしないと行けない。
					if(this.childs.Length!=0)for(int i=0;i<this.childs.Length;i++){
						r+=this.childs.Item((uint)i).Length;
					}
					return r;
				}
			}
		}
		#endregion
		
		#region class mwgIffFile
		public class mwgIffFile:mwgIffNode{
			//fields
			public mwgFourcc identifier;
			public mfString name;
			public mfTime madetime,lasttime;
			public mfBytes content;
			//public dword postLen
			public mwgIffDataArray childs;
			private uint selflen;
			
			//constructor
			public mwgIffFile(byte[] data,uint offset){
				uint i=offset;
				//id
				identifier=new mwgFourcc(data,i);
				if((string)(identifier)!="FILE"){
					throw new System.Exception("mwgIff データの読み取り中にエラーが発生しました。ファイルでない情報を誤ってファイル'FILE'として処理しようとしました。");
				}
				i+=4;
				//name
				name=new mfString(data,i);
				i+=this.name.Length;
				//made,lastmodified
				this.madetime=new mfTime(data,i);i+=5;
				this.lasttime=new mfTime(data,i);i+=5;
				//data
				this.content=new mfBytes(data,i);
				i+=this.content.Length;
				//子要素の最後(postLen)
				uint imax=i+4+(uint)(new mwgDword(data,i));
				if(imax>data.Length)imax=(uint)data.Length;
				i+=4;
				this.selflen=i;
				//◆以上 runover の可能性在り
				
				//子要素の登録
				while(i<imax){
					childs.Add(new mwgIffData(data,i));
					i+=childs.Item(childs.Length-1).Length;
				}
			}
			//inheritance from interface
			public byte[] ToBinary(){
				byte[][] childArray=new byte[this.childs.Length][];
				uint i,k;
				uint l=this.selflen;
				for(k=0;k<this.childs.Length;k++){
					childArray[k]=this.childs.Item(k).ToBinary();
					l+=(uint)childArray[k].Length;
				}
				byte[] r=new byte[l];
				i=0;
				((byte[])this.identifier).CopyTo(r,i);i+=4;
				((byte[])this.name).CopyTo(r,i);i+=this.name.Length;
				((byte[])this.madetime).CopyTo(r,i);i+=5;
				((byte[])this.lasttime).CopyTo(r,i);i+=5;
				((byte[])this.content).CopyTo(r,i);i+=this.content.Length;
				((byte[])new mwgDword(l-this.selflen)).CopyTo(r,i);i+=4;
				for(k=0;k<this.childs.Length;k++){
					childArray[k].CopyTo(r,i);
					i+=(uint)childArray[k].Length;
				}
				return r;
			}
			public string ID{
				get{return (string)this.identifier;}
				set{this.identifier=(mwgFourcc)value;}
			}
			public uint Length{
				get{
					uint r=this.selflen;
					//◆selflenはファイル名や保持データなどの改変によって再計算するようにしないと行けない。
					if(this.childs.Length!=0)for(uint i=0;i<this.childs.Length;i++){
						r+=this.childs.Item(i).Length;
					}
					return r;
				}
			}
		}
		#endregion
		
		#region class mwgIffData
		public class mwgIffData:mwgIffNode{
			mwgFourcc identifier;
			mwgDword len1;
			byte[] data1;
			//constructor
			public mwgIffData(byte[] data,uint offset){
				long im=data.Length;
				identifier=(offset+4>im)?new mwgFourcc("    "):new mwgFourcc(data,offset);
				if(offset+8>im){
					len1=new mwgDword(0);
					data1=new byte[0];
				}else{
					len1=new mwgDword(data,offset+4);
					data1=mwgIff.readByteData(data,offset+8,(uint)len1);
				}
			}
			//inheritance from interface
			public byte[] ToBinary(){
				byte[] r=new byte[this.data1.Length+8];
				((byte[])this.identifier).CopyTo(r,0);
				((byte[])this.len1).CopyTo(r,4);
				this.data1.CopyTo(r,8);
				return r;
			}
			public string ID{
				get{return (string)this.identifier;}
				set{this.identifier=(mwgFourcc)value;}
			}
			public uint Length{get{return (uint)(data1.Length+8);}}
		}
		#endregion
		
		//=====================================
		//          子クラス集合(list)の定義
		//-------------------------------------
		public class mwgIffNodeArray{
			private System.Collections.ArrayList data;
			public mwgIffNodeArray(){
				data=new System.Collections.ArrayList();
			}
			public void Add(mwgIffNode v){
				data.Add(v);
			}
			public void Remove(uint i){
				data.RemoveAt((int)i);
			}
			public mwgIffNode Item(uint i){
				return (mwgIffNode)data[(int)i];
			}
			public uint Length{
				get{return (uint)this.data.Count;}
			}
		}
		public class mwgIffDataArray{
			private System.Collections.ArrayList data;
			public mwgIffDataArray(){
				data=new System.Collections.ArrayList();
			}
			public void Add(mwgIffData v){
				data.Add(v);
			}
			public void Remove(uint i){
				data.RemoveAt((int)i);
			}
			public mwgIffData Item(uint i){
				return (mwgIffData)data[(int)i];
			}
			public uint Length{
				get{return (uint)this.data.Count;}
			}
		}
		
	}//endof-mwgIff
	
	#region struct mwgDword
	//=====================================
	//          mwgDword
	//-------------------------------------
	/// <summary>
	/// ファイル中のDword値を扱います。
	/// </summary>
	//■■エンディアンネスが逆かも知れない。問題があるようならあとで逆にし直す。
	public struct mwgDword{
		uint n;
		//constructor
		public mwgDword(ref mwg.File.mwgBinary mbin){
			n=0;
			if(mbin.RestLength<4)return;
			for(int i=0;i<4;i++)n=(uint)(n*256+(int)mbin.readBytes(1)[0]);
		}
		/// <summary>
		/// バイナリデータの途中にあるdword値を読み取ります。
		/// </summary>
		/// <param name="x">ソースとなるバイナリデータ</param>
		/// <param name="offset">ソースの中に於ける位置を設定します(先頭が0)。変な所に設定されている場合は、読み取らずに0とします。</param>
		public mwgDword(byte[] data,uint offset){
			n=0;
			if(data.Length<offset+4||offset<0)return;
			n=data[offset];
			n=n*256+(uint)data[offset+1];
			n=n*256+(uint)data[offset+2];
			n=n*256+(uint)data[offset+3];
		}
		/// <summary>
		/// byte[]をdword値に変換します。
		/// </summary>
		/// <param name="x">配列の長さが、4より小さな場合でも変換します。</param>
		public mwgDword(byte[] x){
			if(x.Length==0){n=0;return;}
			n=(uint)x[0];
			if(x.Length==1)return;
			int i=(x.Length>4)?x.Length:4;
			for(int j=1;j<i;j++){
				n=n*256+(uint)x[j];
			}
		}
		public mwgDword(uint num){this.n=num;}
		public mwgDword(int num):this((uint)num){}
		//operator
		public static explicit operator byte[](mwgDword a){
			byte[] r=new byte[4];
			uint b,c;
			c=a.n;
			for(int i=3;i>0;i--){
				b=c;c=(uint)(c/256);
				r[i]=(byte)(b-c*256);	
			}
			r[0]=(byte)c;
			return r;
		}
		public static explicit operator uint(mwgDword a){
			return a.n;
		}
		public static explicit operator mwgDword(byte[] a){return new mwgDword(a);}
		public static explicit operator mwgDword(uint a){return new mwgDword(a);}
		
	}
	#endregion
	
	#region struct mwgFourcc
	public struct mwgFourcc{
		byte[] dat;
		//constructor
		public mwgFourcc(ref mwg.File.mwgBinary mbin):this(mbin.readBytes(4),0){}
		public mwgFourcc(byte[] binary,uint offset){
			this.dat=new byte[4];
			if(binary.Length<offset+4||offset<0)return;
			for(int i=0;i<4;i++)this.dat[i]=binary[offset++];
		}
		public mwgFourcc(byte[] binary):this(binary,0){}
		public mwgFourcc(string str){
			while(str.Length<4)str+=" ";
			System.Text.ASCIIEncoding asc=new System.Text.ASCIIEncoding();
			this.dat=new byte[4];
			asc.GetBytes(str,0,4,this.dat,0);
		}
		//operator
		public static explicit operator string(mwgFourcc a){
			System.Text.ASCIIEncoding asc=new System.Text.ASCIIEncoding();
			return asc.GetString(a.dat);
		}
		public static explicit operator mwgFourcc(string a){return new mwgFourcc(a);}
		public static explicit operator byte[](mwgFourcc a){return a.dat;}
	}
	
	#endregion
	
	#region struct mfString
	/// <summary>
	/// byte[] 内にある、dword(長さ)とそれに続く文字列情報(utf-16)を読み取るための物。
	/// </summary>
	public struct mfString{
		public string dat;
		System.Text.UnicodeEncoding uni;
		//constructor
		public mfString(ref mwg.File.mwgBinary mbin){
			this.uni=new System.Text.UnicodeEncoding();
			this.dat=uni.GetString(mbin.readBytes((uint)(new mwg.File.mwgDword(ref mbin))));
		}
		public mfString(byte[] data,uint offset){
			this.uni=new System.Text.UnicodeEncoding();
			uint i=(uint)(new mwgDword(data,offset));
			this.dat=uni.GetString(data,(int)(offset+4),(int)i);
			//※問題点 intの文字数分までしか読み取る事が出来ない。
		}
		public mfString(string str){
			this.uni=new System.Text.UnicodeEncoding();
			this.dat=str;
		}
		//property
		public uint Length{
			get{return (uint)(this.uni.GetByteCount(this.dat)+4);}
		}
		
		//operator
		public static explicit operator string(mfString a){return a.dat;}
		public static explicit operator byte[](mfString a){
			uint l=a.Length;
			byte[] r0=new byte[l];
			((byte[])(mwgDword)(l-4)).CopyTo(r0,0);//長さの情報を書き込み
			a.uni.GetBytes(a.dat,0,a.dat.Length,r0,4);//実際の内容を書き込み
			return r0;
		}
	}
	#endregion
	
	#region struct mfBytes
	public struct mfBytes{
		public byte[] dat;
		public mfBytes(ref mwg.File.mwgBinary mbin){
			this.dat=mbin.readBytes((uint)(new mwgDword(ref mbin)));
		}
		public mfBytes(byte[] data,uint offset){
			uint l=(uint)(new mwgDword(data,offset));
			this.dat=new byte[l];
			uint iData=offset+4;
			if(l+iData>data.Length)return;//◆error:data配列の長さが足りません
			for(uint i=0;i<l;i++)this.dat[i]=data[iData++];//コピー
		}
		public mfBytes(byte[] data){this.dat=data;}
		//property
		/// <summary>
		/// バイト列に直した時の長さを取得します。
		/// </summary>
		public uint Length{
			get{return (uint)(4+this.dat.Length);}
		}
		//operator
		public static explicit operator byte[](mfBytes a){
			byte[] r=new byte[a.dat.Length+4];
			((byte[])new mwgDword(a.dat.Length)).CopyTo(r,0);
			a.dat.CopyTo(r,4);
			return r;
		}
	}
	#endregion
	
	#region struct mfTime
	/// <summary>
	/// ファイル内などバイト列に含まれる、日時情報を扱います。保持する情報は、年(西暦0-西暦3420(実際には西暦0は存在しません))月日時分秒、デシ秒です。
	/// </summary>
	public struct mfTime{
		long dat;
		//=====================================
		//          Constructors
		//-------------------------------------
		public mfTime(ref mwg.File.mwgBinary mbin){
			dat=0;
			if(mbin.RestLength<5)return;
			for(int i=0;i<5;i++)dat=dat*256+(int)mbin.readBytes(1)[0];
		}
		public mfTime(byte[] data,uint offset){
			dat=0;
			if(data.Length<offset+5||offset<0)return;
			dat=data[offset];
			dat=dat*256+(long)data[offset+1];
			dat=dat*256+(long)data[offset+2];
			dat=dat*256+(long)data[offset+3];
			dat=dat*256+(long)data[offset+4];
		}
		public mfTime(byte[] data){
			if(data.Length==0){dat=0;return;}
			dat=(uint)data[0];
			if(data.Length==1)return;
			int i=(data.Length>4)?data.Length:4;
			for(int j=1;j<i;j++){
				dat=dat*256+(uint)data[j];
			}
		}
		public mfTime(System.DateTime time){
			dat=(time.Year>3420)?3420:time.Year;
			dat=dat*12+time.Month-1;//Month は 1-12 だから 0-11 に変換
			dat=dat*31+time.Day-1;//0-30に変換
			dat=dat*24+time.Hour;
			dat=dat*60+time.Minute;
			dat=dat*60+time.Second;
			dat=dat*10+(int)(time.Millisecond/100);
		}
		/// <summary>
		/// mfTimeコンストラクタ
		/// </summary>
		/// <param name="time">年月日時分秒、デシ秒を含む配列からmfTimeを生成します。</param>
		public mfTime(int[] time){
			dat=(time[0]>3420)?3420:time[0];
			dat=dat*12+time[1]-1;//Month は 1-12 だから 0-11 に変換
			dat=dat*31+time[2]-1;//0-30に変換
			dat=dat*24+time[3];
			dat=dat*60+time[4];
			dat=dat*60+time[5];
			dat=dat*10+time[6];
		}
		//property
		public int Year{
			get{return ((int[])this)[0];}
			set{
				if(value>3420||value<0)return;
				int[] a=(int[])this;
				a[0]=value;
				this=new mfTime(a);
			}
		}
		public int Month{
			get{return ((int[])this)[1]+1;}
			set{
				if(value>12||value<1)return;
				int[] a=(int[])this;
				a[1]=value-1;
				this=new mfTime(a);
			}
		}
		public int Day{
			get{return ((int[])this)[2]+1;}
			set{
				if(value>31||value<1)return;
				int[] a=(int[])this;
				a[2]=value-1;
				this=new mfTime(a);
			}
		}
		public int Hour{
			get{return ((int[])this)[3];}
			set{
				if(value>23||value<0)return;
				int[] a=(int[])this;
				a[3]=value;
				this=new mfTime(a);
			}
		}
		public int Minute{
			get{return ((int[])this)[4];}
			set{
				if(value>59||value<0)return;
				int[] a=(int[])this;
				a[4]=value;
				this=new mfTime(a);
			}
		}
		public int Second{
			get{return ((int[])this)[5];}
			set{
				if(value>59||value<0)return;
				int[] a=(int[])this;
				a[5]=value;
				this=new mfTime(a);
			}
		}
		public int Desisecond{
			get{return ((int[])this)[6];}
			set{
				if(value>9||value<0)return;
				int[] a=(int[])this;
				a[6]=value;
				this=new mfTime(a);
			}
		}
		//operator
		public static explicit operator int[](mfTime a){
			int[] r=new int[7];
			int[] m=new int[]{12,31,24,60,60,10};
			long b,c;
			c=a.dat;
			for(int i=6;i>0;i--){
				b=c;c=(long)(c/m[i]);
				r[i]=(byte)(b-c*m[i]);	
			}
			r[0]=(int)c;r[1]++;r[2]++;
			return r;
		}
		public static explicit operator byte[](mfTime a){
			byte[] r=new byte[5];
			long b,c;
			c=a.dat;
			for(int i=4;i>0;i--){
				b=c;c=(long)(c/256);
				r[i]=(byte)(b-c*256);	
			}
			r[0]=(byte)c;
			return r;
		}
		public static explicit operator System.DateTime(mfTime a){
			int[] r1=(int[])a;
			r1[6]*=100;//デシ秒をミリ秒に変換
			return new System.DateTime(r1[0],r1[1],r1[2],r1[3],r1[4],r1[5],r1[6]);
		}
	}
	
	#endregion

#endif

#if version0_2
	public class mwgIff:mb{
		//Field
		mwg.File.mbFourcc id;
		mwg.File.mbUInt32 version;
		mwg.File.mbString name;
		mwg.File.mbDateTime made;
		mwg.File.mbDateTime lastModified;
		mwg.File.mbString annex;
		mwg.File.mwgIff.NodeArray nodes;
		//Constructor
		public mwgIff(mwg.File.mwgBinary mbin){
			this.id=(mwg.File.mbFourcc)mbin;
			this.version=(mwg.File.mbUInt32)mbin;
			this.name=(mwg.File.mbString)mbin;
			this.made=(mwg.File.mbDateTime)mbin;
			this.lastModified=(mwg.File.mbDateTime)mbin;
			this.annex=(mwg.File.mbString)mbin;
			this.nodes=(mwg.File.mwgIff.NodeArray)mbin;
		}
		public mwgIff(byte[] data):this((mwg.File.mwgBinary)data){}
		//Methods
		public override byte[] ToBinary(){
			return (byte[])this.ToMwgBinary();
		}
		public override mwg.File.mwgBinary ToMwgBinary(){
			return this.id.ToMwgBinary()+this.version.ToMwgBinary()+this.name.ToMwgBinary()
				+this.made.ToMwgBinary()+this.lastModified.ToMwgBinary()+this.annex.ToMwgBinary()
				+this.nodes.ToMwgBinary();
		}
		//Properties
		public override int Length{
			get{return 18+this.name.Length+this.annex.Length+this.nodes.Length;}
			//18=4(fourcc)+4(version)+...+5(made)+5(last)
		}
		//Operators
		public static explicit operator mwg.File.mwgIff(byte[] a){return new mwg.File.mwgIff((mwg.File.mwgBinary)a);}
		public static explicit operator mwg.File.mwgIff(mwg.File.mwgBinary a){return new mwg.File.mwgIff(a);}
		//=====================================
		//        child class : Node
		//-------------------------------------
		public abstract class Node:mwg.File.mb{
			public mwg.File.mbFourcc id;
			public mwg.File.mbString name;
			//public Node(){}
			public Node(mwg.File.mwgBinary mbin){
				this.id=(mwg.File.mbFourcc)mbin;
				this.name=(mwg.File.mbString)mbin;
			}
		}
		#region class NodeArray
		//=====================================
		//        child class : NodeArray
		//-------------------------------------
		public class NodeArray:mb{
			System.Collections.ArrayList list;
			//Constructors
			public NodeArray(){
				this.list=new System.Collections.ArrayList();
			}
			public NodeArray(mwg.File.mwgBinary mbin){
				uint postLen=(uint)(mwg.File.mbUInt32)mbin;
				if(postLen>mbin.RestLength)throw new System.Exception("mwg.File.mwgIff.NodeArray Construction from mwg.File.mwgBinary\n\tバイナリデータの長さが指定した数値に足りません。");
				int restmin=(int)(mbin.RestLength-postLen);
				while(mbin.RestLength>restmin){
					switch((string)(mwg.File.mbFourcc)mbin.preReadBytes(4)){
						case "DIR ":
							this.list.Add(new mwg.File.mwgIff.Dir(mbin));
							break;
						case "FILE":
							this.list.Add(new mwg.File.mwgIff.File(mbin));
							break;
						case "DATA":
						default:
							//TODO: DATA を登録
							break;
					}
				}
			}
			public NodeArray(params mwg.File.mwgIff.Node[] nodes){
				this.list=new System.Collections.ArrayList();
				for(int i=0;i<nodes.Length;i++)this.list.Add(nodes[i]);
			}
			//methods
			public override byte[] ToBinary(){
				return (byte[])this.ToMwgBinary();
			}
			public override mwg.File.mwgBinary ToMwgBinary(){
				mwg.File.mwgBinary r=(mwg.File.mwgBinary)new mwg.File.mbUInt32((uint)(this.Length-4));
				for(int i=0;i<this.list.Count;i++)r+=((mwg.File.mwgIff.Node)this.list[i]).ToMwgBinary();
				return r;
			}
			//properties
			public int Count{get{return this.list.Count;}}
			public mwg.File.mwgIff.Node this[int index]{
				get{
					return (mwg.File.mwgIff.Node)this.list[index];
				}
				set{
					this.list[index]=value;
				}
			}
			public override int Length{
				get{
					int r=4;//postLen は長さ 4
					for(int i=0;i<this.list.Count;i++)r+=((mwg.File.mwgIff.Node)this.list[i]).Length;
					return r;
				}
			}
			//operators
			public static implicit operator mwg.File.mwgIff.NodeArray(mwg.File.mwgIff.Node n){
				return new NodeArray(n);
			}
			public static mwg.File.mwgIff.NodeArray operator +(mwg.File.mwgIff.NodeArray a,mwg.File.mwgIff.NodeArray b){
				mwg.File.mwgIff.NodeArray r=new mwg.File.mwgIff.NodeArray();
				r.list.AddRange(a.list);
				r.list.AddRange(b.list);
				return r;
			}
			public static explicit operator mwg.File.mwgIff.NodeArray(mwg.File.mwgBinary mbin){
				return new mwg.File.mwgIff.NodeArray(mbin);
			}
		}
		#endregion

		#region class Dir
		//=====================================
		//        child class : Dir
		//-------------------------------------
		public class Dir:Node{
			//Fields
			mwg.File.mbDateTime made;
			mwg.File.mbDateTime lastModified;
			mwg.File.mbString annex;
			mwg.File.mwgIff.NodeArray nodes;
			//Constructors
			public Dir(mwg.File.mwgBinary mbin):base(mbin){
				this.made=(mwg.File.mbDateTime)mbin;
				this.lastModified=(mwg.File.mbDateTime)mbin;
				this.annex=(mwg.File.mbString)mbin;
				this.nodes=(mwg.File.mwgIff.NodeArray)mbin;
			}
			public Dir(byte[] bin):this((mwg.File.mwgBinary)bin){}
			//Methods
			public override byte[] ToBinary(){
				return (byte[])this.ToMwgBinary();
			}
			public override mwgBinary ToMwgBinary(){
				return this.id.ToMwgBinary()+this.name.ToMwgBinary()+this.made.ToMwgBinary()
					+this.lastModified.ToMwgBinary()+this.annex.ToMwgBinary()+this.nodes.ToMwgBinary();
			}
			public override int Length{
				get{return 14+this.name.Length+this.annex.Length+this.nodes.Length;}
				//4(fourcc)+[name]+5(made)+5(lastModeified)+[annex]+[NodeArray]
			}
			//operators
			public static explicit operator mwg.File.mwgIff.Dir(byte[] a){return new mwg.File.mwgIff.Dir((mwg.File.mwgBinary)a);}
			public static explicit operator mwg.File.mwgIff.Dir(mwg.File.mwgBinary a){return new mwg.File.mwgIff.Dir(a);}
		}
		#endregion

		#region class File
		//=====================================
		//        child class : File
		//-------------------------------------
		public class File :Node{
			//Fields
			mwg.File.mbDateTime made;
			mwg.File.mbDateTime lastModified;
			mwg.File.mbBytes content;
			mwg.File.mwgIff.NodeArray nodes;
			//Constructors
			public File(mwg.File.mwgBinary mbin):base(mbin){
				this.made=(mwg.File.mbDateTime)mbin;
				this.lastModified=(mwg.File.mbDateTime)mbin;
				this.content=(mwg.File.mbBytes)mbin;
				this.nodes=(mwg.File.mwgIff.NodeArray)mbin;
				//TODO: Data 以外を除去する。
			}
			public File(byte[] bin):this((mwg.File.mwgBinary)bin){}
			//Methods
			public override byte[] ToBinary(){
				return (byte[])this.ToMwgBinary();
			}
			public override mwgBinary ToMwgBinary(){
				return this.id.ToMwgBinary()+this.name.ToMwgBinary()+this.made.ToMwgBinary()
					+this.lastModified.ToMwgBinary()+this.content.ToMwgBinary()+this.nodes.ToMwgBinary();
			}
			public override int Length{
				get{return 14+this.name.Length+this.content.Length+this.nodes.Length;}
				//4(fourcc)+[name]+5(made)+5(lastModeified)+[annex]+[NodeArray]
			}
			//operators
			public static explicit operator mwg.File.mwgIff.File(byte[] a){return new mwg.File.mwgIff.File((mwg.File.mwgBinary)a);}
			public static explicit operator mwg.File.mwgIff.File(mwg.File.mwgBinary a){return new mwg.File.mwgIff.File(a);}
		}
		#endregion

		#region class Data
		public class Data:Node{
			//■了■
			mwg.File.mbBytes data;
			public Data(mwg.File.mwgBinary mbin):base(mbin){
				this.data=(mwg.File.mbBytes)mbin;
			}
			public Data(byte[] data):this((mwg.File.mwgBinary)data){}
			
			public override int Length{
				get{
					return 4+this.name.Length+this.data.Length;
				}
			}
			public override byte[] ToBinary(){
				return (byte[])(this.ToMwgBinary());
			}
			public override mwg.File.mwgBinary ToMwgBinary(){
				return this.id.ToMwgBinary()+this.name.ToMwgBinary()+this.data.ToMwgBinary();
			}
			public static explicit operator Data(byte[] a){return new mwg.File.mwgIff.Data(a);}
			public static explicit operator Data(mwg.File.mwgBinary a){return new mwg.File.mwgIff.Data(a);}
		}
		#endregion
	}
#endif

#if version0_3
	public class mwgIff3:RW{
		string type;
		uint version;
		string name;
		RWDateTime made;
		RWDateTime lastModified;
		string annex;
		NodeList nodes;
		//
		//		constructor
		//
		public mwgIff3(string type,uint version,string name){
			this.type=type;
			this.version=version;
			this.name=name;
			this.made=new RWDateTime();
			this.lastModified=new RWDateTime();
			this.annex="";
			this.nodes=new NodeList();
		}
		public mwgIff3(mwg.File.Bytes bin){this.ReadFrom(bin);}

		#region RW メンバ
		public void ReadFrom(mwg.File.Bytes bin){
			//bin.R(ref this.type).R(ref this.version).R(ref this.name).R(ref this.made);
			this.type=bin.ReadFourcc();
			this.version=bin.ReadUInt32();
			this.name=bin.ReadString();
			this.made=new RWDateTime(bin);
			this.lastModified=new RWDateTime(bin);
			this.annex=bin.ReadString();
		}
		public void WriteTo(mwg.File.Bytes bin){
			bin.WriteFourcc(this.type);
			bin.WriteUInt32(this.version);
			bin.WriteString(this.name);
			this.made.WriteTo(bin);
			this.lastModified.WriteTo(bin);
			bin.WriteString(this.annex);
			//this.nodes.WriteTo(bin);
		}
		#endregion

		public class NodeList:mwg.File.RW,System.Collections.IEnumerable{
			System.Collections.ArrayList data;
			public NodeList(){
				this.data=new System.Collections.ArrayList();
			}
			public NodeList(mwg.File.Bytes bin){
				this.data=new System.Collections.ArrayList();
				this.ReadFrom(bin);
			}
			public bool isOnlyAttr=false;
			public void ReadFrom(mwg.File.Bytes bin){
				int m=bin.ReadInt32();
				mwgIff3.Node node;
				if(this.isOnlyAttr)for(int i=0;i<m;i++){
					node=(mwgIff3.Node)bin;
					if(node.Type!="DIR "&&node.Type!="FILE")this.data.Add(node);
				}else for(int i=0;i<m;i++){
					this.data.Add((mwgIff3.Node)bin);
				}
			}
			public void WriteTo(mwg.File.Bytes bin){
				bin.WriteInt32(this.data.Count);
				for(int i=0;i<this.data.Count;i++)((mwgIff3.Node)data[i]).WriteTo(bin);
			}

			#region Indexer
			public mwgIff3.Node this[string type,string name]{
				get{
					if(name=="")return null;//複数ある為
					mwgIff3.Node node;
					for(int i=0;i<this.data.Count;i++){
						node=(Node)this.data[i];
						if(node.Type==type&&node.name==name)return node;
					}
					return null;
				}
				set{
					mwgIff3.Node node;
					if(name!="")for(int i=0;i<this.data.Count;i++){
						node=(Node)this.data[i];
						if(node.Type==type&&node.name==name){
							this.data.RemoveAt(i);
							break;
						}
					}
					if(value.Type!=type)throw new System.Exception("異なる Type のノードとして登録しようとしています");
					value.name=name;
					this.data.Add(value);
				}
			}
			public mwgIff3.NodeList this[string type]{
				get{
					mwgIff3.NodeList list=new NodeList();
					mwgIff3.Node node;
					for(int i=0;i<this.data.Count;i++){
						node=(Node)this.data[i];
						if(node.Type==type)list.data.Add(node);
					}
					return list;
				}
			}
			public mwgIff3.Node this[int index]{
				get{return (mwgIff3.Node)this.data[index];}
			}
			public int Count{get{return this.data.Count;}}
			#endregion

			#region Member of IEnumerable
			public System.Collections.IEnumerator GetEnumerator(int index,int count){
				return this.data.GetEnumerator(index,count);
			}
			public System.Collections.IEnumerator GetEnumerator(){return this.data.GetEnumerator();}
			#endregion

			public void Add(mwg.File.mwgIff3.Node newnode){
				if(this.isOnlyAttr&&(newnode.Type=="DIR "||newnode.Type=="FILE"))throw new System.Exception("Directory 及び File は登録出来ません");
				this[newnode.Type,newnode.name]=newnode;
			}
			public void RemoveAt(int index){this.data.RemoveAt(index);}
		}
		public abstract class Node:mwg.File.RW{
			public abstract string Type{get;}
			public string name;
			//TODO:親→field に Node parent; を追加して、NodeList に登録する時に設定する事にする。
			//	NodeList.Parent==null の時は再設定しない
			//解析遅延
			private byte[] content;//未だ読み込んでいない中身
			private bool isOpened=true;
			protected void Open(){
				if(!this.isOpened){
					this.ReadContent((mwg.File.Bytes)this.content);
					this.isOpened=true;
				}
			}
			//TODO:暗号化・圧縮→他に圧縮暗号化クラスを作成して呼び出す
			//読込・保存
			public virtual void ReadFrom(mwg.File.Bytes bin){
				this.name=bin.ReadString();
				this.content=bin.ReadByteArray();
				this.isOpened=false;
			}
			public virtual void WriteTo(mwg.File.Bytes bin){
				bin.WriteFourcc(this.Type);
				bin.WriteString(this.name);
				mwg.File.Bytes bytes=new mwg.File.Bytes();
				this.WriteContent(bytes);
				bin.Write((byte[])bytes);
			}
			protected virtual void WriteContent(mwg.File.Bytes bin){}
			protected virtual void ReadContent(mwg.File.Bytes bin){}
			public static explicit operator mwgIff3.Node(mwg.File.Bytes bin){
				switch(bin.ReadFourcc()){
					case "DIR ":return new mwgIff3.Dir(bin);
					case "FILE":return new mwgIff3.File(bin);
					case "@bin":return new mwgIff3.AttrBin(bin);
					case "@str":return new mwgIff3.AttrStr(bin);
					default:
						bin.Position-=4;
						throw new System.Exception("不明な種類のノードです。読み取れません");
				}
			}
		}
		public class Dir:Node{
			RWDateTime made;
			RWDateTime lastModified;
			string annex;
			NodeList nodes;
			public Dir(mwg.File.Bytes bin){
				this.ReadFrom(bin);
			}
			public override string Type{get{return "DIR ";}}
			protected override void ReadContent(Bytes bin){
				this.made=new RWDateTime(bin);
				this.lastModified=new RWDateTime(bin);
				this.annex=bin.ReadString();
				this.nodes=new NodeList(bin);
			}
			protected override void WriteContent(Bytes bin){
				this.made.WriteTo(bin);
				this.lastModified.WriteTo(bin);
				bin.WriteString(this.annex);
				this.nodes.WriteTo(bin);
			}
		}
		public class File:Node{
			RWDateTime made;
			RWDateTime lastModified;
			byte[] data;
			NodeList nodes;
			public File(mwg.File.Bytes bin){
				this.ReadFrom(bin);
			}
			public File(string name,byte[] data){
				this.name=name;
				this.made.SetNow();
				this.lastModified.SetNow();
				this.data=data;
				this.nodes=new NodeList();
				this.nodes.isOnlyAttr=true;
			}
			public override string Type{get{return "FILE";}}
			protected override void ReadContent(Bytes bin){
				this.made=new RWDateTime(bin);
				this.lastModified=new RWDateTime(bin);
				this.data=bin.ReadByteArray();
				this.nodes=new NodeList();
				this.nodes.isOnlyAttr=true;
				this.nodes.ReadFrom(bin);
			}
			protected override void WriteContent(Bytes bin){
				this.made.WriteTo(bin);
				this.lastModified.WriteTo(bin);
				bin.WriteByteArray(this.data);
				this.nodes.WriteTo(bin);
			}
			public byte[] Data{
				get{this.Open();return this.data;}
				set{this.Open();this.data=value;this.lastModified.SetNow();}
			}
			public mwgIff3.NodeList Childs{
				get{this.Open();return this.nodes;}
			}
		}
		public class AttrBin:Node{
			byte[] data;
			public AttrBin(mwg.File.Bytes bin){this.ReadFrom(bin);}
			public AttrBin(string name,byte[] data){
				this.name=name;
				this.data=data;
			}
			public override void ReadFrom(mwg.File.Bytes bin){
				this.name=bin.ReadString();
				this.data=bin.ReadByteArray();
			}
			public override void WriteTo(mwg.File.Bytes bin){
				bin.WriteFourcc(this.Type);
				bin.WriteString(this.name);
				bin.WriteByteArray(this.data);
			}
			public override string Type{get{return "@bin";}}
			public byte[] Data{get{return this.data;}set{this.data=value;}}
		}
		public class AttrStr:Node{
			string data;
			public AttrStr(mwg.File.Bytes bin){this.ReadFrom(bin);}
			public AttrStr(string name,string data){
				this.name=name;
				this.data=data;
			}
			public override void ReadFrom(mwg.File.Bytes bin){
				this.name=bin.ReadString();
				this.data=bin.ReadString();
			}
			public override void WriteTo(mwg.File.Bytes bin){
				bin.WriteFourcc(this.Type);
				bin.WriteString(this.name);
				bin.WriteString(this.data);
			}
			public override string Type{get{return "@str";}}
			public string Data{get{return this.data;}set{this.data=value;}}
		}
	}
#endif

}