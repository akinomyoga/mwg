using afh.RegularExpressions;
using Rgx=System.Text.RegularExpressions;

#if true||DEBUG
namespace UnitTest{
	[afh.Tester.TestTarget]
	public static class TestRegex{
		public static void TestRegexParse1(afh.Application.Log log){
			using(log.Lock()){
				TestRegexMatch(log,"x","x",true);
				TestRegexMatch(log,"y","x",false);
				TestRegexMatch(log,"x|y|z","y",true);
				TestRegexMatch(log,"xyz","xyz",true);
				TestRegexMatch(log,"xyz","x",false);
				TestRegexMatch(log,"xyz","yxz",false);
				TestRegexMatch(log,"x(?:x|y)z","xyz",true);
				TestRegexMatch(log,"x(?:x|y)z","xxz",true);
				TestRegexMatch(log,"x(?:x|y)z","xxzb",true);
				TestRegexMatch(log,"x(?:x|y)z","xzz",false);
				TestRegexMatch(log,"(?:x|y)z*(?:x|y)","xzzzzzzzzzzzzzy",true);
				TestRegexMatch(log,"(?:x|y)z*zzzz(?:x|y)","xzzzzzzzzzzzzzy",true);
				TestRegexMatch(log,"(?:x|y)z+?z(?:x|y)","xzzzzzzzzzzzzzy",true);
				TestRegexMatch(log,"(?:x|y)z*?z(?:x|y)","xzzy",true);

				TestRegexMatch(log,"z{1,?}","zzz",true);
				TestRegexMatch(log,"z{1,?}z","zzz",true);
				TestRegexMatch(log,"z{1,+}","zzz",true);
				TestRegexMatch(log,"z{1,+}z","zzz",false);

				TestRegexMatch(log,"[a-z]","e",true);
				TestRegexMatch(log,"[a-z-[b-o]]","e",false);
				TestRegexMatch(log,"[a-z-[b-o]d-f]","e",true);
				TestRegexMatch(log,"[a-z-[b-o]d-f-[e]]","e",false);

				TestRegexMatch(log,@"x\p{xdigit}+y","x08FFABy",true);
				TestRegexMatch(log,@"x\P{xdigit}+y","x08FFABy",false);
				TestRegexMatch(log,@"x\p{xdigit}+y","xBvy",false);
				TestRegexMatch(log,@"x\P{xdigit}+y","xAvy",false);
				TestRegexMatch(log,"x:\"y","ddx\"Hello World!\"yss",true);
				TestRegexMatch(log,"x:\"y","ddx\"Hello World!\\\"yss",false);
				TestRegexMatch(log,"x:\"y","ddx\"how are you\"ss",false);
			}
		}
		private static void TestRegexMatch(afh.Application.Log log,string regex,string text,bool expected){
			StringRegex.RegLan reg=new StringRegex.RegLan(regex);
			bool r=reg.IsMatching(text);
			log.WriteLine("'{1}'∈/{0}/ : {2}",reg,text,r);
			if(r!=expected){
				log.WriteError("***ASSERTION FAILED*** 評価結果が予期した物と異なります!!!");
			}
		}

		public static void TestRegexMatch1(afh.Application.Log log){
			using(log.Lock()){
				StringRegex.RegLan reglan=new StringRegex.RegLan("こんにちは|さようなら");
				log.WriteLine("Regular Expression: {0}",reglan);
				foreach(StringRegex.Capture c in reglan.Matches("こんにちは さようなら こんにちは こんにちは さようなら")){
					log.WriteLine("Match! : {0}",c.Value);
				}

				reglan=new StringRegex.RegLan(@"(?<l>\d+)(?<o>(?:\+|\-))(?<r>\d+)");
				log.WriteLine("Regular Expression: {0}",reglan);
				foreach(StringRegex.Capture c in reglan.Matches("3+2+4+5 1+2+3 2+3 4+7 123+321")) {
					int l=int.Parse(c.Groups["l"].Last);
					int r=int.Parse(c.Groups["r"].Last);
					bool o=c.Groups["o"].Last.Value=="+";
					log.WriteLine("{0} を計算すると→: {1}",c,o?l+r:l-r);
				}

				log.WriteLine("== System.Text.RegularExpressions の場合 ==");
				Rgx::Regex rgx=new Rgx::Regex(@"(?<l>\d+)(?<o>(?:\+|\-))(?<r>\d+)");
				log.WriteLine("Regular Expression: {0}",rgx.ToString());
				foreach(Rgx::Match m in rgx.Matches("3+2+4+5 1+2+3 2+3 4+7 123+321")) {
					int l=int.Parse(m.Groups["l"].Value);
					int r=int.Parse(m.Groups["r"].Value);
					bool o=m.Groups["o"].Value=="+";
					log.WriteLine("{0} を計算すると→: {1}",m.Value,o?l+r:l-r);
				}
			}
		}

		static StringRegex.RegLan reglan=new StringRegex.RegLan(@"(?<l>\d+)(?<o>[\+\-])(?<r>\d+)");
		[afh.Tester.BenchMethod]
		public static int benchRegexMatchAfh1(){
			int s=0;

			foreach(StringRegex.Capture c in reglan.Matches("3+2+4+5 1+2+3 2+3 4+7 123+321")) {
				int l=int.Parse(c.Groups["l"].Last);
				int r=int.Parse(c.Groups["r"].Last);
				bool o=c.Groups["o"].Last.Value=="+";
				s+=o?l+r:l-r;
			}

			return s;
		}
		static Rgx::Regex rgx=new Rgx::Regex(@"(?<l>\d+)(?<o>[\+\-])(?<r>\d+)");
		[afh.Tester.BenchMethod]
		public static int benchRegexMatchClr1(){
			int s=0;

			foreach(Rgx::Match m in rgx.Matches("3+2+4+5 1+2+3 2+3 4+7 123+321")){
				int l=int.Parse(m.Groups["l"].Value);
				int r=int.Parse(m.Groups["r"].Value);
				bool o=m.Groups["o"].Value=="+";
				s+=o?l+r:l-r;
			}

			return s;
		}

		[afh.Tester.BenchMethod]
		public static int benchRegexMatchEmpty1(){
			int s=0;

			for(int i=0;i<6;i++){
				int l=int.Parse("123");
				int r=int.Parse("321");
				bool o=i%2==0;
				s+=o?l+r:l-r;
			}

			return s;
		}
		//------------------------------------------------------------
		static StringRegex.RegLan reglan2=new StringRegex.RegLan("(:\")");//@"(""(?:[^""\\\r\n\f]|\\.)*"")"
		[afh.Tester.BenchMethod]
		public static int benchRegexMatchAfh2(){
			int s=0;

			foreach(StringRegex.Capture c in reglan2.Matches(@" adsasda ""還元"" dadas ""Hello!"" dasda22 ""充君"" adaaee ""三軒茶屋"" adad")){
				s+=c.Groups[0].Last.Value.Length;
			}

			return s;
		}
		static Rgx::Regex rgx2=new Rgx::Regex(@"(""(?:[^""\\\r\n\f]|\\.)*"")");
		[afh.Tester.BenchMethod]
		public static int benchRegexMatchClr2(){
			int s=0;

			foreach(Rgx::Match m in rgx2.Matches(@" adsasda ""還元"" dadas ""Hello!"" dasda22 ""充君"" adaaee ""三軒茶屋"" adad")){
				s+=m.Groups[0].Value.Length;
			}

			return s;
		}
		//------------------------------------------------------------
		[afh.Tester.BenchMethod]
		public static void benchRegexCompileAfh1(){
			StringRegex.RegLan reglan=new StringRegex.RegLan(@"(?<l>\d+)(?<o>(?:\+|\-))(?<r>\d+)");		
		}
		[afh.Tester.BenchMethod]
		public static void benchRegexCompileClr1(){
			Rgx::Regex rgx=new Rgx::Regex(@"(?<l>\d+)(?<o>(?:\+|\-))(?<r>\d+)");
		}

		public static void 列挙体確認(afh.Application.Log log){
			log.WriteLine(afh.Text.GeneralCategory.C.ToString());
			log.WriteLine(afh.Text.UnicodeBlock.CJKCompatibilityIdeographs.ToString());
		}

		delegate string D仮想関数とデリゲート();
		public static void 仮想関数とデリゲート(afh.Application.Log log){
			D仮想関数とデリゲート d=((object)reglan).ToString;
			log.WriteLine(d.Method.DeclaringType);
			log.WriteLine(d.Method);
		}
	}
}
#endif
