#define treeBase
namespace mwg.Collection{

#if treeBase
	// �������{�ɂ��āA�I�u�W�F�N�g�̑召�֌W�����߂���@��ݒ肷��΂悢�B
	/// <summary>
	/// �񕪖؂̗v�f�ƂȂ�I�u�W�F�N�g���`���Ă��܂��B
	/// </summary>
	public class binTreeNode{
		//fields
		/**<summary>�����̐e</summary>*/internal binTreeNode p;
		/**<summary>�����̍����̎q</summary>*/internal binTreeNode l;
		/**<summary>�����̍����̎q</summary>*/internal binTreeNode r;
		/**<summary>�����̍����ɂ���q���̐�</summary>*/internal uint lL;
		/**<summary>�����̉E���ɂ���q���̐�</summary>*/internal uint rL;
		/**<summary>�������A�e�ɂƂ��č����̎q�������łȂ�����ݒ�</summary>*/internal bool w;
		/**<summary>���ۂɕێ�����l�ł��B</summary>*/public System.IComparable v;
		// w,p ���g�p���郍�W�b�N�́AbinTree �� override ����Ȃ���΂Ȃ�܂���B
		
		//constructor
		/// <summary>
		/// mwg.Collection.binTreeNode �̃R���X�g���N�^
		/// </summary>
		/// <param name="val">�ێ��������ݒ肵�܂�</param>
		/// <param name="parent">�e�ƂȂ� binTreeNode ��ݒ肵�܂�</param>
		/// <param name="which">�����������̎q���ǂ�����ݒ肵�܂��B</param>
		public binTreeNode(System.IComparable val,binTreeNode parent,bool which){
			v=val;
			p=parent;
			w=which;
			l=r=null;
			lL=rL=0;
		}
		//=====================================
		//          �֐�
		//-------------------------------------
		/// <summary>
		/// �V�����v�f��؂ɒǉ����܂�
		/// </summary>
		/// <param name="val">�ǉ��������v�f</param>
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
		/// index�Ŏw�肳�ꂽ �m�[�h��Ԃ��܂��B
		/// </summary>
		/// <param name="index">�����牽�Ԗڂ�\�����̐����w�肵�܂��B��ԍ��� index==0 �ł��B</param>
		public binTreeNode Node(uint index){
			if(index<lL)return l.Node(index);
			if(index>lL)return l.Node((uint)(index-lL-1));
			return this;
		}
		/// <summary>
		/// ���̃m�[�h��؂���폜���܂��B���̃m�[�h�̉��ɂ���q���́A�؂Ɏc���܂��B
		/// </summary>
		public virtual void Remove(){
			if(lL+rL==0){//�q�����Ȃ���
				this.p.Decreased(1,w);
				if(this.w)this.p.l=null;else this.p.r=null;//����폜
			}else if(lL>rL){//�����̎q���̕���������
				v=l.lastNode.v;
				l.lastNode.Remove();
			}else{//�E���̎q���̕���������
				v=r.firstNode.v;
				r.lastNode.Remove();
			}
		}
		/// <summary>
		/// �m�[�h�̐��������Ȃ���������֒ʒm���܂��B
		/// </summary>
		internal virtual void Decreased(uint n,bool which){
			if(which)lL-=n;else rL-=n;
			this.p.Decreased(n,w);
		}
		/// <summary>
		/// ��v���镨�����邩�ǂ������������܂�
		/// </summary>
		/// <param name="val">�����������邩��ݒ肵�܂��B</param>
		/// <returns>��v�������������Ă��� binTreeNode ��Ԃ��܂��B</returns>
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
		/// �������؂̒��ŁA�����琔���Ăǂ̈ʒu�ɂ��邩��Ԃ��܂��B��ԍ��� Index 0 �ł��B
		/// </summary>
		public virtual uint Index{
			get{
				if(w)return p.Index-rL;
				return lL+p.Index;
			}
		}
		/// <summary>
		/// �����Ƃ��̎q���ŁA��ԍ����ɂ��镨��Ԃ��܂��B
		/// </summary>
		public binTreeNode firstNode{
			get{return (lL==0)?this:this.l.firstNode;}
		}
		/// <summary>
		/// �����Ƃ��̎q���ŁA��ԉE���ɂ��镨��Ԃ��܂��B
		/// </summary>
		public binTreeNode lastNode{
			get{return (rL==0)?this:this.r.firstNode;}
		}
		/// <summary>
		/// �����̒��a�̐�c�ŁA�������E���ɂ��镨��Ԃ��܂�
		/// </summary>
		public virtual binTreeNode rightAncestor{
			get{return (this.w)?this.p:this.p.rightAncestor;}
		}
		/// <summary>
		/// �����̒��a�̐�c�ŁA������荶���ɂ��镨��Ԃ��܂��B
		/// </summary>
		public virtual binTreeNode leftAncestor{
			get{return (!this.w)?this.p:this.p.leftAncestor;}
		}
		/// <summary>
		/// �S�̂̃m�[�h�̒��Ŏ����̒����E���ɂ��镨��Ԃ��܂��B
		/// </summary>
		public binTreeNode nextNode{
			get{return (rL==0)?this.rightAncestor:this.r.firstNode;}
		}
		/// <summary>
		/// �S�̂̃m�[�h�̒��Ŏ����̒��������ɂ��镨��Ԃ��܂��B
		/// </summary>
		public binTreeNode previousNode{
			get{return (lL==0)?this.leftAncestor:this.r.lastNode;}
		}
		
	}
	/// <summary>
	/// �񕪖�
	/// </summary>
	public class binTree:binTreeNode{
		//field
		//���̃m�[�h�ɉ����� w �͂��̃m�[�h���f�[�^�������ǂ������������Ƃ��Ĉ����܂��B

		//constructor
		public binTree():base("",null,false){}
		public binTree(System.IComparable val):base(val,null,true){
			//which=true �́A���̃m�[�h���v�f�������Ă��鎖�������B
			//ToDo:�R���X�g���N�V�������W�b�N�͖�������������Ă��Ȃ��B
		}
		//=====================================
		//          �֐�
		//-------------------------------------
		/// <summary>
		/// �V�����v�f��؂ɒǉ����܂�
		/// </summary>
		/// <param name="val">�ǉ��������v�f</param>
		public override void Add(System.IComparable val){
			if(w)base.Add(val);else{
				v=val;
				w=true;
			}
		}
		/// <summary>
		/// �m�[�h�̐����������ʒm���󂯎��܂��B
		/// </summary>
		internal override void Decreased(uint n,bool which){
			if(which)lL-=n;else rL-=n;
		}
		/// <summary>
		/// �m�[�h���폜���܂��B
		/// </summary>
		public override void Remove(){
			if(lL+rL==0){//�q�����Ȃ���
				w=false;//����̓f�[�^�������Ȃ����ɂ���B
				v=null;
			}else if(lL>rL){//�����̎q���̕���������
				v=l.lastNode.v;
				l.lastNode.Remove();
			}else{//�E���̎q���̕���������
				v=r.firstNode.v;
				r.lastNode.Remove();
			}
		}
		
		//=====================================
		//          properties
		//-------------------------------------
		/// <summary>
		/// �������؂̒��ŁA�����琔���Ăǂ̈ʒu�ɂ��邩��Ԃ��܂��B��ԍ��� Index 0 �ł��B
		/// </summary>
		public override uint Index
		{
			get{return (uint)((w)?lL:0);}
		}
		/// <summary>
		/// �ێ����Ă���m�[�h�̐���Ԃ��܂��B
		/// </summary>
		public int Length{
			get{
				if(w)return (int)(lL+rL+1);//���������������Ă��鎞�B
				return 0;
			}
		}
		/// <summary>
		/// null�l���Ԃ��ꂽ���́A��������ԍ����ɂ���m�[�h�ł��鎖�������Ă��܂��B
		/// </summary>
		public override binTreeNode leftAncestor{get{return null;}}
		/// <summary>
		/// null�l���Ԃ��ꂽ���́A��������ԉE�ɂ���m�[�h�ł��鎖�������Ă��܂��B
		/// </summary>
		public override binTreeNode rightAncestor{get{return null;}}

	}
#endif

}