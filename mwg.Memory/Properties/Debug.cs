using Gen=System.Collections.Generic;
using afh.RegularExpressions;
using Diag=System.Diagnostics;

internal static partial class __dll__{

	private static void ASSERT(string name,object real,object ideal){
		if(real.Equals(ideal)){
			System.Console.WriteLine("ASSERT {0}: {1}",name,"OK");
		}else{
			System.Console.ForegroundColor=System.ConsoleColor.Red;
			System.Console.WriteLine("ASSERT {0}: {1}",name,"ERROR");
			System.Console.WriteLine("\tReal : {0}",real);
			System.Console.WriteLine("\tIdeal: {0}",ideal);
			System.Console.ForegroundColor=System.ConsoleColor.Gray;
		}
	}

	public static void Main(){
		//chkStack動作();
		testProcessMemory();

		chkArgEvalOrder();
		chkListBehaviour();
		mwg.InterProcess.ProcessMemory WR=new mwg.InterProcess.ProcessMemory("WarRock");

		System.Console.WriteLine("Press any key to exit this program.");
		System.Console.ReadLine();
	}
	private static void testProcessMemory(){
		System.Console.WriteLine("Press any key to stop the process.");
		System.Console.ReadKey();
		System.Console.WriteLine();

		mwg.InterProcess.ProcessMemory m=new mwg.InterProcess.ProcessMemory("notepad");
		if(!m.Available){
			System.Console.WriteLine("notepad.exe が実行されていません!");
			System.Console.WriteLine();
			return;
		}
		System.Console.WriteLine();
		m.StopProcess();

		System.Console.WriteLine("Press any key to restart the process.");
		System.Console.ReadKey();
		System.Console.WriteLine();
		m.RestartProcess();
	}
	//================================================================
	//		chk ListBehaviour
	//================================================================
	private static void chkListBehaviour(){
		System.Console.WriteLine("---- check List Behaviour on Removing Last Item ----");
		Gen::List<int> list=new System.Collections.Generic.List<int>();
		list.Add(2);
		list.Add(3);
		list.Add(5);
		list.Add(7);
		ASSERT("Appended 4 items",chkListBehaviour_List2Str(list),"2 3 5 7");
		list.RemoveAt(3);
		ASSERT("Removed 1 item",chkListBehaviour_List2Str(list),"2 3 5");
		list.RemoveAt(2);
		ASSERT("Removed 2 items",chkListBehaviour_List2Str(list),"2 3");
		list.RemoveAt(1);
		ASSERT("Removed 3 items",chkListBehaviour_List2Str(list),"2");
		System.Console.WriteLine();
	}
	private static string chkListBehaviour_List2Str(Gen::List<int> list){
		System.Text.StringBuilder b=new System.Text.StringBuilder();
		bool isfirst=true;
		foreach(int val in list){
			if(isfirst)isfirst=false;else b.Append(" ");
			b.Append(val);
		}
		return b.ToString();
	}
	//================================================================
	//		引数の評価の順番
	//================================================================
	private delegate string DChkAEO(int a,int b,int c);
	/// <summary>
	/// 引数を評価する順番が左から右である事を確認します。
	/// </summary>
	private static void chkArgEvalOrder(){
		System.Console.WriteLine("---- check Argument Evaluation Order ----");
		int i=0;
		ASSERT(
			"argument",
			((DChkAEO)delegate(int a,int b,int c){
				return string.Format("{0} {1} {2}",a,b,c);
			})(i,++i,++i),
			"0 1 2"
		);
		ASSERT(
			"params argument",
			string.Format("{0} {1} {2}",i,++i,++i),
			"2 3 4"
		);
		System.Console.WriteLine();
	}

	private static void chkStack動作(){
		Gen::Stack<int> st=new System.Collections.Generic.Stack<int>();
		st.Push(0);
		st.Push(1);
		st.Push(2);
		st.Push(3);

		PrintStack(st);

		Gen::Stack<int> st2=new System.Collections.Generic.Stack<int>(st.ToArray());
		PrintStack(st2);
	}
	private static void PrintStack(Gen::Stack<int> st){
		System.Console.Write("content of st: ");
		int[] starr=st.ToArray();
		for(int i=starr.Length-1;i>=0;i--){
			int val=starr[i];
			System.Console.Write("{0}, ",val);
		}
		//foreach(int val in st)System.Console.Write("{0}, ",val);
		System.Console.WriteLine("(top);");
		System.Console.WriteLine("count of st: {0}",st.Count);
	}
}
