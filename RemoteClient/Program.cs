using System;
using System.Collections.Generic;
using System.Text;
using mwg.InterProcess;
//using afh.RegularExpressions;

namespace RemoteClient {
	class Client {
		static void Main(){
			WinEvent win=WinEvent.Open("こんにちは～～～");
			if(win==null){
				System.Console.WriteLine("つまんなーい。さよーなら");
				return;
			}

			win.Raised+=new Action<WinEvent>(win_Raised);
			while(true){
				System.Console.WriteLine(">何か入力してね q");
				string s=System.Console.ReadLine();
				switch(s){
					case "q":
						System.Console.WriteLine("さいなら");
						win.Close();
						return;
					default:
						System.Console.WriteLine("何処の御國の言葉ですか?");
						break;
				}
			}
		}

		static void win_Raised(WinEvent obj){
			System.Console.WriteLine("どこか遠くで「どかん」て音がした!");
		}
	}
}
