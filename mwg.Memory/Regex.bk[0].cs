//using System;
using Gen=System.Collections.Generic;
using System.Text;
using Store=System.Collections.Generic.Dictionary<string,object>;
// store info args (captures)

namespace afh.RegularExpressions {
#if VER1
	/// <summary>
	/// 特定の型の配列に対する正規表現を扱います。
	/// </summary>
	/// <typeparam name="T">正規表現の対象となる型を指定します。</typeparam>
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
			/// この node の持つ条件に適合するかどうかの試験を行います。
			/// </summary>
			/// <param name="target">試験の対象となる配列を指定します。</param>
			/// <param name="index">
			/// 現在見ている位置を指定します。試験を終了した後の位置を返します。
			/// 試験が失敗した場合には、失敗した所の次の位置を返します。
			/// </param>
			/// <param name="dic">
			/// 試験の最中に得られた情報を格納しておく為のオブジェクトです。
			/// 試験が失敗した場合には、基本的に dic に変化が残らない様に設計して下さい。
			/// </param>
			/// <returns>
			/// 試験が成功した場合―則ち、条件を満たす場合―に true を返します。
			/// それ以外の場合に false を返します。
			/// </returns>
			bool Test(T[] target,ref int index,Capture dic);
		}
		//============================================================
		//		関数による判定
		//============================================================
		public delegate bool DTest(T[] target,ref int index,Capture dic);
		/// <summary>
		/// 単一の関数を以て判定を行います。
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
		//		他
		//============================================================
		public struct OrNode:INode{
			public OrNode(INode[] candidates){
				this.cands=candidates;
			}

			INode[] cands; // 例: /(xx|x)x/ に "xx" を入れたとき等
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
				dic.AddCapture(capt); // 全体が成功してから確定
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
				if(!test1.Result)break; // 次の match pattern に挑戦

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
				// a. 失敗
				//-------------------
					//TODO: OtherPossibility
					return false;
				}else if(test!=null){
				// b. 入れ子 Node
				//-------------------
					s.Push(test);
				}else{
				// c. 成功
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
			/// 現在の処理の対象となっている tester を取得します。
			/// これは、tester スタックの一番上にある tester です。
			/// </summary>
			public ITester Tester{
				get{return this.st.Peek();}
			}
			/// <summary>
			/// 処理すべき tester を一つ加えます。
			/// </summary>
			/// <param name="test">追加する tester を指定します。</param>
			internal void Push(ITester test){
				this.st.Push(test);
			}
			/// <summary>
			/// 処理の終わった tester (スタックの一番上にある) を、スタックから取り除きます。
			/// </summary>
			/// <returns>
			/// 残りの ITester が無くなった時、則ち、全ての Node が一致したときに true を返します。
			/// それ以外の場合、則ち、未だ処理が終わっていない Node が残っているときに false を返します。
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
			/// 現在の状態を保持するもう一つのインスタンスを作成します。
			/// </summary>
			/// <returns>作成したインスタンスを返します。</returns>
			ITester Clone();
			/// <summary>
			/// 読み取りを行います。
			/// </summary>
			/// <param name="s">読み取りに必要な情報を保持するオブジェクトを指定します。</param>
			/// <returns>
			/// 次の子読み取りに使用する ITester を返します。
			/// 子読み取りを実行した後に再度この関数が呼ばれます。
			/// ※ 子読み取りをこれ以上行わない場合は null を返します。
			/// この場合には、この ITester の読み取りは終了した物と見做されます。
			/// </returns>
			ITester Read(Status s);
			/// <summary>
			/// 読み取った結果を返します。
			/// </summary>
			bool Result{get;}
			/// <summary>
			/// 非決定性を示します。
			/// 全ての考え得る候補を列挙し終えた場合に false を返します。
			/// 未だ、他の読み取り方法が考えられる場合に true を返します。
			/// </summary>
			/// <remarks>
			/// これは "(Reg1|Reg2|...)" や "Reg*", "Reg+" 等で、複数の match の仕方が考えられる場合に使用します。
			/// (使用しない場合には常に false を返す様にしておけば問題在りません。)
			/// <ol>
			/// <li>一回目の Read では取り敢えず、最優先の match の仕方で行います。
			/// Indefinite は true を返す様にします。
			/// その match の仕方で失敗したときには、再び Read が呼び出されます。</li>
			/// <li>二回目の Read では二つ目の match の仕方で結果を返します。
			/// 三つ目以降の match の仕方が考えられる時には、Indefinite を true にします。
			/// 考えられる match の仕方が二つしかない場合には Indefinite は false を返す様にします。</li>
			/// <li>「正規表現全体が match する」か、「match のパターンが尽きて Indefinite=false を返す」まで
			/// "2." の動作が続きます。</li>
			/// </ol>
			/// </remarks>
			/// <example>
			/// 例えば、"(abcd|abc)..." という正規表現には、"'abcd' or 'abc'" という Node (NodeA とする) が含まれています。
			/// 初め NodeA で abcd に match し、残りの "..." の部分で失敗したとします。
			/// この時 NodeA-Tester が Indefinite==true の場合には、判定は再び NodeA の部分からやり直しになります。
			/// (この例では、abc に match します。)
			/// </example>
			bool Indefinite{get;}
		}
		/// <summary>
		/// 正規表現の構成要素を示します。
		/// </summary>
		public interface INode{
			/// <summary>
			/// 新しい Tester を取得します。
			/// </summary>
			/// <returns>新しく作成した ITester インスタンスを返します。</returns>
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
		/// 単に判定を行う関数を以て、Node を定義します。
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

				#region ITester メンバ

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


			#region INode メンバ

			public ITester GetTester() {
				return new Tester(this.h);
			}

			#endregion
		}
		#endregion

#if OBSOLETE
		// → new FunctionNode(delegate(Status s){...}); で実装
		//-----------------------------------------------------
		/// <summary>
		/// どんな要素でも受容する Node です。
		/// </summary>
		public sealed class AnythingNode:INode{

			#region INode メンバ

			public ITester GetTester() {
				return new Tester();
			}

			#endregion

			public class Tester:ITester{
				private bool result=false;

				#region ITester メンバ

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
