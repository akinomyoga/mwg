using System;
using Ref=System.Reflection;

namespace 家{
	public static class 飼い主さん{
		public static void Main(){
			AppDomain.CurrentDomain.AssemblyResolve
				+=new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			System.Console.WriteLine("--- 飼い主が犬を鳴かせようとしています ---");

			鳴かせる();
		}

		public static void 鳴かせる(){
			System.Console.WriteLine("飼い主>鳴け!");
			MyDll.Dog dog=new MyDll.Dog();
			dog.鳴く();
		}

		static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender,ResolveEventArgs args) {
			System.Console.WriteLine("飼い主>犬がいない? (汗");
			System.IO.DirectoryInfo dinfo=new System.IO.DirectoryInfo("lib");
			foreach(System.IO.FileInfo finfo in dinfo.GetFiles("*.dll")){
				Ref::AssemblyName name=Ref::AssemblyName.GetAssemblyName(finfo.FullName);
				if(name.FullName==args.Name){
					System.Console.WriteLine("飼い主>犬が見つかった!");
					return Ref::Assembly.LoadFrom(finfo.FullName);
				}
			}
			Console.WriteLine(args.Name);
			return null;
		}
	}
}