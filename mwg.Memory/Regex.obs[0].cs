using Gen=System.Collections.Generic;
using System.Text;

namespace afh.RegularExpressions {
	public class Regex<T>{
		private static void testRegex1(){
			Regex<char>.INode node0=new Regex<char>.FunctionNode(delegate(Regex<char>.Status s){
				return 'x'==s.Target[s.Index++];
			});
			Regex<char>.INode node1=new Regex<char>.FunctionNode(delegate(Regex<char>.Status s){
				return 'y'==s.Target[s.Index++];
			});
			Regex<char>.INode node=new Regex<char>.OrNode(new Regex<char>.INode[]{node0,node1});

			ASSERT("x->/x/",Regex<char>.Match("x".ToCharArray(),node0),true);
			ASSERT("y->/x/",Regex<char>.Match("y".ToCharArray(),node0),false);
			ASSERT("y->/x|y/",Regex<char>.Match("y".ToCharArray(),node),true);

			System.Console.WriteLine();
		}

		#region node:Function
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

			public ITester GetTester(){
				return new Tester(this.h);
			}
			public NodeAssociativity Associativity{
				get{return NodeAssociativity.Strong;}
			}

			private class Tester:ITester{
				private bool result=false;
				private readonly Handler h;
				public Tester(Handler handler){
					this.h=handler;
				}

				public NodeAssociativity Associativity{
					get{return NodeAssociativity.Strong;}
				}

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

				public void NextPossibility(Status s) {
					throw new System.Exception("The method or operation is not implemented.");
				}
			}

		}
		#endregion

		public class OrNode:INode{
			private class Tester:OrNode,ITester{
#if TEST
				// __minor(instance continuous)
				// __minor(clear) return;
				// × Clone をするには Reflection が必要。著しい overhead
				// × s が別のインスタンスに変わっても、初めに指定したインスタンスを使い続けてしまう。
				private Gen::IEnumerator<ITester> _Read(Status s){
					int index0=s.Index;
					for(int iNode=0;iNode<nodes.Length;iNode++){
						s.Index=index0;
						ITester test=nodes[iNode].GetTester();
						yield return test;
						if(test.Result)yield return null;
					}
					this.result=false;
					yield return null;
				}
#endif
			}

		}

	}

	public class RegexFactory2<T>{
		public class Group{
			int iParentCapt=-1;
			Gen::List<ICaptureRange> captures;
			INode node;
			internal Group(Gen::List<ICaptureRange> captures,INode node):this(captures,node,-1){}
			internal Group(Gen::List<ICaptureRange> captures,INode node,ICaptureRange parent)
				:this(captures,node,captures.IndexOf(parent)){}
			internal Group(Gen::List<ICaptureRange> captures,INode node,int indexOfParentCapture){
				this.captures=captures;
				this.node=node;
				this.iParentCapt=indexOfParentCapture;
			}

			private Gen::IEnumerable<ICaptureRange> EnumCaptures(){
				int start,end;
				if(this.iParentCapt<0){
					start=0;
					end=captures.Count;
				}else{
					start=captures[iParentCapt].InitialCaptureCount;
					end=iParentCapt;
				}

				for(int i=start;i<end;i++){
					if(captures[i].Node==node)
						yield return captures[i];
				}
			}
		}
	}
}
