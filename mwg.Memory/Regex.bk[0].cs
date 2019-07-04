//using System;
using Gen=System.Collections.Generic;
using System.Text;
using Store=System.Collections.Generic.Dictionary<string,object>;
// store info args (captures)

namespace afh.RegularExpressions {
#if VER1
	/// <summary>
	/// ����̌^�̔z��ɑ΂��鐳�K�\���������܂��B
	/// </summary>
	/// <typeparam name="T">���K�\���̑ΏۂƂȂ�^���w�肵�܂��B</typeparam>
	internal class Regex<T>{
		//
		
		// '.'
		public static bool Anything(T[] target,ref int index,Capture dic){
			return index++<target.Length;
		}
		public static bool op_Plus(T[] target,ref int index,Capture dic){
			return false;
		}

		public class Capture{
			Gen::Dictionary<string,object> data=new Gen::Dictionary<string,object>();
			public Capture(){}

			public void AddCapture(Capture capt){
				foreach(Gen::KeyValuePair<string,object> pair in capt.data){
					this.Add(pair.Key,pair.Value);
				}
			}

			public void Add(string key,object value){
				this.data.Add(key,value);
			}
		}
		//============================================================
		public interface INode{
			/// <summary>
			/// ���� node �̎������ɓK�����邩�ǂ����̎������s���܂��B
			/// </summary>
			/// <param name="target">�����̑ΏۂƂȂ�z����w�肵�܂��B</param>
			/// <param name="index">
			/// ���݌��Ă���ʒu���w�肵�܂��B�������I��������̈ʒu��Ԃ��܂��B
			/// ���������s�����ꍇ�ɂ́A���s�������̎��̈ʒu��Ԃ��܂��B
			/// </param>
			/// <param name="dic">
			/// �����̍Œ��ɓ���ꂽ�����i�[���Ă����ׂ̃I�u�W�F�N�g�ł��B
			/// ���������s�����ꍇ�ɂ́A��{�I�� dic �ɕω����c��Ȃ��l�ɐ݌v���ĉ������B
			/// </param>
			/// <returns>
			/// ���������������ꍇ�\�����A�����𖞂����ꍇ�\�� true ��Ԃ��܂��B
			/// ����ȊO�̏ꍇ�� false ��Ԃ��܂��B
			/// </returns>
			bool Test(T[] target,ref int index,Capture dic);
		}
		//============================================================
		//		�֐��ɂ�锻��
		//============================================================
		public delegate bool DTest(T[] target,ref int index,Capture dic);
		/// <summary>
		/// �P��̊֐����ȂĔ�����s���܂��B
		/// </summary>
		public struct FuncNode:INode{
			DTest proc;
			public FuncNode(DTest proc){
				this.proc=proc;
			}
			public static implicit operator FuncNode(DTest proc){
				return new FuncNode(proc);
			}
			public bool Test(T[] target,ref int index,Capture dic){
				return this.proc(target,ref index,dic);
			}
		}
		//============================================================
		//		��
		//============================================================
		public struct OrNode:INode{
			public OrNode(INode[] candidates){
				this.cands=candidates;
			}

			INode[] cands; // ��: /(xx|x)x/ �� "xx" ����ꂽ�Ƃ���
			public bool Test(T[] target,ref int index,Capture dic){
				int index0=index;
				foreach(INode node in cands){
					index=index0;
					if(node.Test(target,ref index,dic))
						return true;
				}
				return false;
			}
		}
		public struct SequenceNode:INode{
			public SequenceNode(INode[] sequence){
				this.seqs=sequence;
			}

			INode[] seqs;
			public bool Test(T[] target,ref int index,Capture dic){
				Capture capt=new Capture();
				foreach(INode node in seqs){
					if(!node.Test(target,ref index,capt))return false;
				}
				dic.AddCapture(capt); // �S�̂��������Ă���m��
				return true;
			}
		}
		public struct RepeatNode:INode{
			public bool Test(T[] target,ref int index,Capture dic){
				return false;
			}
		}
	}
#endif

	public class Regex<T>{
#if OBSOLETE
		public static Gen::IEnumerable<INode> Anything(Status s){
			s.Frame.result=++s.Index<s.Target.Length;
			yield break;
		}
#endif
		public static bool Match(T[] target,INode rootNode){
			Status s=new Status(target);
			
#if OLD
			Gen::Stack<ITester> st=new Gen::Stack<ITester>();
			ITester test1=initialNode.GetTester();
			while(true){
				ITester test2=test1.Read(s);
				if(!test1.Result)break; // ���� match pattern �ɒ���

				if(test2!=null){
					st.Push(test1);
					test1=test2;
					continue;
				}
				
				if(test1.Indefinite){
					//SaveStatus();
				}

				if(st.Count>0){
					test1=st.Pop();
				}else{
					return true;
				}
			}
#endif

			s.Push(rootNode.GetTester());
			while(true){
				ITester test=s.Tester.Read(s);

				if(!s.Tester.Result){
				// a. ���s
				//-------------------
					//TODO: OtherPossibility
					return false;
				}else if(test!=null){
				// b. ����q Node
				//-------------------
					s.Push(test);
				}else{
				// c. ����
				//-------------------
					if(s.Tester.Indefinite){
						//TODO: SaveStatus();
					}

					if(s.Pop())return true;
				}
			}
		}

		public class Status{
			T[] target;
			int index;
			
			Gen::Stack<ITester> st=new Gen::Stack<ITester>();

			public Status(T[] target):this(target,0){}
			public Status(T[] target,int index){
				this.target=target;
				this.index=index;
			}

#if OBSOLETE_FRAME
			[System.Obsolete]
			Gen::Stack<Frame> stk=new Gen::Stack<Frame>();
			[System.Obsolete]
			public Frame Frame{
				get{return this.stk.Peek();}
			}
			[System.Obsolete]
			public ITester Read(){
				return this.Tester.Read(this);
			}
#endif

			public T[] Target{
				get{return this.target;}
			}
			public int Index{
				get{return this.index;}
				set{this.index=value;}
			}

			/// <summary>
			/// ���݂̏����̑ΏۂƂȂ��Ă��� tester ���擾���܂��B
			/// ����́Atester �X�^�b�N�̈�ԏ�ɂ��� tester �ł��B
			/// </summary>
			public ITester Tester{
				get{return this.st.Peek();}
			}
			/// <summary>
			/// �������ׂ� tester ��������܂��B
			/// </summary>
			/// <param name="test">�ǉ����� tester ���w�肵�܂��B</param>
			internal void Push(ITester test){
				this.st.Push(test);
			}
			/// <summary>
			/// �����̏I����� tester (�X�^�b�N�̈�ԏ�ɂ���) ���A�X�^�b�N�����菜���܂��B
			/// </summary>
			/// <returns>
			/// �c��� ITester �������Ȃ������A�����A�S�Ă� Node ����v�����Ƃ��� true ��Ԃ��܂��B
			/// ����ȊO�̏ꍇ�A�����A�����������I����Ă��Ȃ� Node ���c���Ă���Ƃ��� false ��Ԃ��܂��B
			/// </returns>
			internal bool Pop(){
				this.st.Pop();
				return this.st.Count==0;
			}
		}

#if OBSOLETE_FRAME
		[System.Obsolete]
		public class Frame{
			internal Frame(){}

			public ITester node;
		}
#endif

		public interface ITester{
			/// <summary>
			/// ���݂̏�Ԃ�ێ����������̃C���X�^���X���쐬���܂��B
			/// </summary>
			/// <returns>�쐬�����C���X�^���X��Ԃ��܂��B</returns>
			ITester Clone();
			/// <summary>
			/// �ǂݎ����s���܂��B
			/// </summary>
			/// <param name="s">�ǂݎ��ɕK�v�ȏ���ێ�����I�u�W�F�N�g���w�肵�܂��B</param>
			/// <returns>
			/// ���̎q�ǂݎ��Ɏg�p���� ITester ��Ԃ��܂��B
			/// �q�ǂݎ������s������ɍēx���̊֐����Ă΂�܂��B
			/// �� �q�ǂݎ�������ȏ�s��Ȃ��ꍇ�� null ��Ԃ��܂��B
			/// ���̏ꍇ�ɂ́A���� ITester �̓ǂݎ��͏I���������ƌ��􂳂�܂��B
			/// </returns>
			ITester Read(Status s);
			/// <summary>
			/// �ǂݎ�������ʂ�Ԃ��܂��B
			/// </summary>
			bool Result{get;}
			/// <summary>
			/// �񌈒萫�������܂��B
			/// �S�Ă̍l���������񋓂��I�����ꍇ�� false ��Ԃ��܂��B
			/// �����A���̓ǂݎ����@���l������ꍇ�� true ��Ԃ��܂��B
			/// </summary>
			/// <remarks>
			/// ����� "(Reg1|Reg2|...)" �� "Reg*", "Reg+" ���ŁA������ match �̎d�����l������ꍇ�Ɏg�p���܂��B
			/// (�g�p���Ȃ��ꍇ�ɂ͏�� false ��Ԃ��l�ɂ��Ă����Ζ��݂�܂���B)
			/// <ol>
			/// <li>���ڂ� Read �ł͎�芸�����A�ŗD��� match �̎d���ōs���܂��B
			/// Indefinite �� true ��Ԃ��l�ɂ��܂��B
			/// ���� match �̎d���Ŏ��s�����Ƃ��ɂ́A�Ă� Read ���Ăяo����܂��B</li>
			/// <li>���ڂ� Read �ł͓�ڂ� match �̎d���Ō��ʂ�Ԃ��܂��B
			/// �O�ڈȍ~�� match �̎d�����l�����鎞�ɂ́AIndefinite �� true �ɂ��܂��B
			/// �l������ match �̎d����������Ȃ��ꍇ�ɂ� Indefinite �� false ��Ԃ��l�ɂ��܂��B</li>
			/// <li>�u���K�\���S�̂� match ����v���A�umatch �̃p�^�[�����s���� Indefinite=false ��Ԃ��v�܂�
			/// "2." �̓��삪�����܂��B</li>
			/// </ol>
			/// </remarks>
			/// <example>
			/// �Ⴆ�΁A"(abcd|abc)..." �Ƃ������K�\���ɂ́A"'abcd' or 'abc'" �Ƃ��� Node (NodeA �Ƃ���) ���܂܂�Ă��܂��B
			/// ���� NodeA �� abcd �� match ���A�c��� "..." �̕����Ŏ��s�����Ƃ��܂��B
			/// ���̎� NodeA-Tester �� Indefinite==true �̏ꍇ�ɂ́A����͍Ă� NodeA �̕��������蒼���ɂȂ�܂��B
			/// (���̗�ł́Aabc �� match ���܂��B)
			/// </example>
			bool Indefinite{get;}
		}
		/// <summary>
		/// ���K�\���̍\���v�f�������܂��B
		/// </summary>
		public interface INode{
			/// <summary>
			/// �V���� Tester ���擾���܂��B
			/// </summary>
			/// <returns>�V�����쐬���� ITester �C���X�^���X��Ԃ��܂��B</returns>
			ITester GetTester();
		}

#if UNUSED
		public abstract class TesterBase:ITester{
			/*
			public ITester Clone(){
				TesterBase ret=new TesterBase();
				this.Clone_copy(ret);
				return ret;
			}
			//*/
			public abstract ITester Clone();
			protected void Clone_copy(TesterBase val){
				val.result=this.result;
			}

			public abstract ITester Read(Status s);

			protected bool result=false;
			public virtual bool Result{
				get{return this.result;}
			}
		}
#endif

		#region FunctionNode
		internal static INode CreateFunctionNode(FunctionNode.Handler handler){
			return new FunctionNode(handler);
		}
		internal static INode CreateAnythingNode(){
			return new FunctionNode(delegate(Status s){
				return s.Index++<s.Target.Length;
			});
		}
		/// <summary>
		/// �P�ɔ�����s���֐����ȂāANode ���`���܂��B
		/// </summary>
		public class FunctionNode:INode{
			public delegate bool Handler(Status s);

			Handler h;
			internal FunctionNode(Handler handler){
				this.h=handler;
			}

			private class Tester:ITester{
				private bool result=false;
				private readonly Handler h;
				public Tester(Handler handler){
					this.h=handler;
				}

				#region ITester �����o

				public ITester Clone() {
					return new Tester(this.h);
				}

				public ITester Read(Status s) {
					int index0=s.Index;
					if(!(this.result=this.h(s)))s.Index=index0;
					return null;
				}

				public bool Result {
					get {return this.result; }
				}

				public bool Indefinite{
					get{return false;}
				}
				#endregion
			}


			#region INode �����o

			public ITester GetTester() {
				return new Tester(this.h);
			}

			#endregion
		}
		#endregion

#if OBSOLETE
		// �� new FunctionNode(delegate(Status s){...}); �Ŏ���
		//-----------------------------------------------------
		/// <summary>
		/// �ǂ�ȗv�f�ł���e���� Node �ł��B
		/// </summary>
		public sealed class AnythingNode:INode{

			#region INode �����o

			public ITester GetTester() {
				return new Tester();
			}

			#endregion

			public class Tester:ITester{
				private bool result=false;

				#region ITester �����o

				public ITester Clone(){
					throw new System.Exception("The method or operation is not implemented.");
				}

				public ITester Read(Status s) {
					this.result=++s.Index<s.Target.Length;
					return null;
				}

				public bool Result {
					get {return this.result;}
				}

				#endregion
			}
		}
#endif

	}
}
