#define treeBase
namespace mwg.Collection{

#if treeBase
	// これを基本にして、オブジェクトの大小関係を決める方法を設定すればよい。
	/// <summary>
	/// 二分木の要素となるオブジェクトを定義しています。
	/// </summary>
	public class binTreeNode{
		//fields
		/**<summary>自分の親</summary>*/internal binTreeNode p;
		/**<summary>自分の左側の子</summary>*/internal binTreeNode l;
		/**<summary>自分の左側の子</summary>*/internal binTreeNode r;
		/**<summary>自分の左側にある子孫の数</summary>*/internal uint lL;
		/**<summary>自分の右側にある子孫の数</summary>*/internal uint rL;
		/**<summary>自分が、親にとって左側の子かそうでないかを設定</summary>*/internal bool w;
		/**<summary>実際に保持する値です。</summary>*/public System.IComparable v;
		// w,p を使用するロジックは、binTree で override されなければなりません。
		
		//constructor
		/// <summary>
		/// mwg.Collection.binTreeNode のコンストラクタ
		/// </summary>
		/// <param name="val">保持する情報を設定します</param>
		/// <param name="parent">親となる binTreeNode を設定します</param>
		/// <param name="which">自分が左側の子かどうかを設定します。</param>
		public binTreeNode(System.IComparable val,binTreeNode parent,bool which){
			v=val;
			p=parent;
			w=which;
			l=r=null;
			lL=rL=0;
		}
		//=====================================
		//          関数
		//-------------------------------------
		/// <summary>
		/// 新しい要素を木に追加します
		/// </summary>
		/// <param name="val">追加したい要素</param>
		public virtual void Add(System.IComparable val){
			int cc=val.CompareTo(this.v);
			if(cc<0){
				if(this.lL==0){
					this.lL++;
					this.l=new binTreeNode(val,this,true);
				}else l.Add(val);
			}else{
				if(this.rL==0){
					this.rL++;
					this.r=new binTreeNode(val,this,false);
				}else this.r.Add(val);
			}
		}
		/// <summary>
		/// indexで指定された ノードを返します。
		/// </summary>
		/// <param name="index">左から何番目を表すかの数を指定します。一番左は index==0 です。</param>
		public binTreeNode Node(uint index){
			if(index<lL)return l.Node(index);
			if(index>lL)return l.Node((uint)(index-lL-1));
			return this;
		}
		/// <summary>
		/// このノードを木から削除します。このノードの下にある子孫は、木に残します。
		/// </summary>
		public virtual void Remove(){
			if(lL+rL==0){//子孫がない時
				this.p.Decreased(1,w);
				if(this.w)this.p.l=null;else this.p.r=null;//自ら削除
			}else if(lL>rL){//左側の子孫の方が多い時
				v=l.lastNode.v;
				l.lastNode.Remove();
			}else{//右側の子孫の方が多い時
				v=r.firstNode.v;
				r.lastNode.Remove();
			}
		}
		/// <summary>
		/// ノードの数がすくなった事を上へ通知します。
		/// </summary>
		internal virtual void Decreased(uint n,bool which){
			if(which)lL-=n;else rL-=n;
			this.p.Decreased(n,w);
		}
		/// <summary>
		/// 一致する物があるかどうかを検索します
		/// </summary>
		/// <param name="val">何を検索するかを設定します。</param>
		/// <returns>一致した情報を持っている binTreeNode を返します。</returns>
		public object Search(System.IComparable val){
			int cc=val.CompareTo(v);
			if(cc<0){
				if(lL==0)return null; else return l.Search(val);
			}else if(cc>0){
				if(rL==0)return null; else return r.Search(val);
			}else return v;
		}
		//=====================================
		//          properties
		//-------------------------------------
		/// <summary>
		/// 自分が木の中で、左から数えてどの位置にあるかを返します。一番左は Index 0 です。
		/// </summary>
		public virtual uint Index{
			get{
				if(w)return p.Index-rL;
				return lL+p.Index;
			}
		}
		/// <summary>
		/// 自分とその子孫で、一番左側にある物を返します。
		/// </summary>
		public binTreeNode firstNode{
			get{return (lL==0)?this:this.l.firstNode;}
		}
		/// <summary>
		/// 自分とその子孫で、一番右側にある物を返します。
		/// </summary>
		public binTreeNode lastNode{
			get{return (rL==0)?this:this.r.firstNode;}
		}
		/// <summary>
		/// 自分の直径の先祖で、自分より右側にある物を返します
		/// </summary>
		public virtual binTreeNode rightAncestor{
			get{return (this.w)?this.p:this.p.rightAncestor;}
		}
		/// <summary>
		/// 自分の直径の先祖で、自分より左側にある物を返します。
		/// </summary>
		public virtual binTreeNode leftAncestor{
			get{return (!this.w)?this.p:this.p.leftAncestor;}
		}
		/// <summary>
		/// 全体のノードの中で自分の直ぐ右側にある物を返します。
		/// </summary>
		public binTreeNode nextNode{
			get{return (rL==0)?this.rightAncestor:this.r.firstNode;}
		}
		/// <summary>
		/// 全体のノードの中で自分の直ぐ左側にある物を返します。
		/// </summary>
		public binTreeNode previousNode{
			get{return (lL==0)?this.leftAncestor:this.r.lastNode;}
		}
		
	}
	/// <summary>
	/// 二分木
	/// </summary>
	public class binTree:binTreeNode{
		//field
		//このノードに於いて w はこのノードがデータを持つかどうかを示す物として扱います。

		//constructor
		public binTree():base("",null,false){}
		public binTree(System.IComparable val):base(val,null,true){
			//which=true は、このノードが要素を持っている事を示す。
			//ToDo:コンストラクションロジックは未だ書きおわっていない。
		}
		//=====================================
		//          関数
		//-------------------------------------
		/// <summary>
		/// 新しい要素を木に追加します
		/// </summary>
		/// <param name="val">追加したい要素</param>
		public override void Add(System.IComparable val){
			if(w)base.Add(val);else{
				v=val;
				w=true;
			}
		}
		/// <summary>
		/// ノードの数が減った通知を受け取ります。
		/// </summary>
		internal override void Decreased(uint n,bool which){
			if(which)lL-=n;else rL-=n;
		}
		/// <summary>
		/// ノードを削除します。
		/// </summary>
		public override void Remove(){
			if(lL+rL==0){//子孫がない時
				w=false;//自らはデータを持たない事にする。
				v=null;
			}else if(lL>rL){//左側の子孫の方が多い時
				v=l.lastNode.v;
				l.lastNode.Remove();
			}else{//右側の子孫の方が多い時
				v=r.firstNode.v;
				r.lastNode.Remove();
			}
		}
		
		//=====================================
		//          properties
		//-------------------------------------
		/// <summary>
		/// 自分が木の中で、左から数えてどの位置にあるかを返します。一番左は Index 0 です。
		/// </summary>
		public override uint Index
		{
			get{return (uint)((w)?lL:0);}
		}
		/// <summary>
		/// 保持しているノードの数を返します。
		/// </summary>
		public int Length{
			get{
				if(w)return (int)(lL+rL+1);//自分が情報を持っている時。
				return 0;
			}
		}
		/// <summary>
		/// null値が返された時は、自分が一番左側にあるノードである事を示しています。
		/// </summary>
		public override binTreeNode leftAncestor{get{return null;}}
		/// <summary>
		/// null値が返された時は、自分が一番右にあるノードである事を示しています。
		/// </summary>
		public override binTreeNode rightAncestor{get{return null;}}

	}
#endif

}