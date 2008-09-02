using System;
using System.Collections.Generic;
using System.Text;
using mwg.InterProcess;

namespace RemoteServer {
	class RemoteServer {
		static void Main() {
			WinEvent ev=WinEvent.CreateNew("こんにちは～～～",true);
			if(ev==null){
				System.Console.WriteLine("既に誰かがイベントを作っているよ。");
				return;
			}

			while(true){
				System.Console.WriteLine(">何か入力してね r or q");
				string s=System.Console.ReadLine();
				switch(s){
					case "r":
						System.Console.WriteLine("どかーん");
						ev.Pulse();
						break;
					case "q":
						System.Console.WriteLine("さいなら");
						ev.Close();
						return;
					default:
						System.Console.WriteLine("何処の御國の言葉ですか?");
						break;
				}
			}
		}
	}
}
