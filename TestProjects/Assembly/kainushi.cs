using System;
using Ref=System.Reflection;

namespace ��{
	public static class �����傳��{
		public static void Main(){
			AppDomain.CurrentDomain.AssemblyResolve
				+=new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			System.Console.WriteLine("--- �����傪��������悤�Ƃ��Ă��܂� ---");

			������();
		}

		public static void ������(){
			System.Console.WriteLine("������>��!");
			MyDll.Dog dog=new MyDll.Dog();
			dog.��();
		}

		static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender,ResolveEventArgs args) {
			System.Console.WriteLine("������>�������Ȃ�? (��");
			System.IO.DirectoryInfo dinfo=new System.IO.DirectoryInfo("lib");
			foreach(System.IO.FileInfo finfo in dinfo.GetFiles("*.dll")){
				Ref::AssemblyName name=Ref::AssemblyName.GetAssemblyName(finfo.FullName);
				if(name.FullName==args.Name){
					System.Console.WriteLine("������>������������!");
					return Ref::Assembly.LoadFrom(finfo.FullName);
				}
			}
			Console.WriteLine(args.Name);
			return null;
		}
	}
}