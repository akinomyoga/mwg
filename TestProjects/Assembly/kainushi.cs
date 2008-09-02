using System;
using Ref=System.Reflection;

namespace ‰Æ{
	public static class Ž”‚¢Žå‚³‚ñ{
		public static void Main(){
			AppDomain.CurrentDomain.AssemblyResolve
				+=new ResolveEventHandler(CurrentDomain_AssemblyResolve);
			System.Console.WriteLine("--- Ž”‚¢Žå‚ªŒ¢‚ð–Â‚©‚¹‚æ‚¤‚Æ‚µ‚Ä‚¢‚Ü‚· ---");

			–Â‚©‚¹‚é();
		}

		public static void –Â‚©‚¹‚é(){
			System.Console.WriteLine("Ž”‚¢Žå>–Â‚¯!");
			MyDll.Dog dog=new MyDll.Dog();
			dog.–Â‚­();
		}

		static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender,ResolveEventArgs args) {
			System.Console.WriteLine("Ž”‚¢Žå>Œ¢‚ª‚¢‚È‚¢? (Š¾");
			System.IO.DirectoryInfo dinfo=new System.IO.DirectoryInfo("lib");
			foreach(System.IO.FileInfo finfo in dinfo.GetFiles("*.dll")){
				Ref::AssemblyName name=Ref::AssemblyName.GetAssemblyName(finfo.FullName);
				if(name.FullName==args.Name){
					System.Console.WriteLine("Ž”‚¢Žå>Œ¢‚ªŒ©‚Â‚©‚Á‚½!");
					return Ref::Assembly.LoadFrom(finfo.FullName);
				}
			}
			Console.WriteLine(args.Name);
			return null;
		}
	}
}