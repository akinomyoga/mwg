using System;
using System.Collections.Generic;
using System.Text;
using mwg.InterProcess;

namespace RemoteServer {
	class RemoteServer {
		static void Main() {
			WinEvent ev=WinEvent.CreateNew("����ɂ��́`�`�`",true);
			if(ev==null){
				System.Console.WriteLine("���ɒN�����C�x���g������Ă����B");
				return;
			}

			while(true){
				System.Console.WriteLine(">�������͂��Ă� r or q");
				string s=System.Console.ReadLine();
				switch(s){
					case "r":
						System.Console.WriteLine("�ǂ��[��");
						ev.Pulse();
						break;
					case "q":
						System.Console.WriteLine("�����Ȃ�");
						ev.Close();
						return;
					default:
						System.Console.WriteLine("�����̌䚠�̌��t�ł���?");
						break;
				}
			}
		}
	}
}
