using System;
using System.Collections.Generic;
using System.Text;
using mwg.InterProcess;
//using afh.RegularExpressions;

namespace RemoteClient {
	class Client {
		static void Main(){
			WinEvent win=WinEvent.Open("����ɂ��́`�`�`");
			if(win==null){
				System.Console.WriteLine("�܂�ȁ[���B����[�Ȃ�");
				return;
			}

			win.Raised+=new Action<WinEvent>(win_Raised);
			while(true){
				System.Console.WriteLine(">�������͂��Ă� q");
				string s=System.Console.ReadLine();
				switch(s){
					case "q":
						System.Console.WriteLine("�����Ȃ�");
						win.Close();
						return;
					default:
						System.Console.WriteLine("�����̌䚠�̌��t�ł���?");
						break;
				}
			}
		}

		static void win_Raised(WinEvent obj){
			System.Console.WriteLine("�ǂ��������Łu�ǂ���v�ĉ�������!");
		}
	}
}
