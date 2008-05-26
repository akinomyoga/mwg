using System;

namespace mwg.Math{
	public class integer{
		/**<summary>最大公約数g.c.m.(greatest common measure)</summary>*/
		public static int GCM(int a,int b){
			if(a==0) return b; if(b==0) return a; if(a<b){ a+=b; b=a-b; a=a-b;}
		loop1: a%=b; if(a==0) return b; b%=a; if(b==0) return a; goto loop1;
		}
		public static decimal GCM(decimal a,decimal b){
			if(a==0) return b; 
			if(b==0) return a;
			if(a<b){ 
				decimal c=a;
				a=b;
				b=c;
			}
		loop1: a%=b; 
			if(a==0) return b; 
			b%=a;
			if(b==0) return a;
			goto loop1;
		}
		/**<summary>最小公倍数l.c.m.(least common multiple/lowest common multiple)</summary>*/
		public static int LCM(int a,int b){return a*b/GCM(a,b);}
		public static decimal LCM(decimal a,decimal b){return a*b/GCM(a,b);}
		/**<summary>約分</summary>*/
		public static void reduction(ref decimal a,ref decimal b){ decimal n=integer.GCM(a,b); a/=n; b/=n;}
		/**<summary>天井ceiling</summary>*/
		public static double CEI(double a){return System.Math.Ceiling(a);}
		/**<summary>天井ceiling</summary>*/
		public static double CEI(double a,double exp){return System.Math.Ceiling(a/exp)*exp;}
		/**<summary>天井ceiling</summary>*/
		public static double CEI(double a,double exp,double number){return System.Math.Ceiling((a-number)/exp)*exp+number;}
		/**<summary>天井ceiling</summary>*/
		public static double CEIn(double a,double number){return System.Math.Ceiling(a-number)+number;}
		/**<summary>床floor</summary>*/
		public static double FLR(double a){return System.Math.Floor(a);}
		/**<summary>床floor</summary>*/
		public static double FLR(double a,double exp){return System.Math.Floor(a/exp)*exp;}
		/**<summary>床floor</summary>*/
		public static double FLR(double a,double exp,double number){return System.Math.Floor((a-number)/exp)*exp+number;}
		/**<summary>床floor</summary>*/
		public static double FLRn(double a,double number){return System.Math.Ceiling(a-number)+number;}
		/**<summary>四捨五入round</summary>*/
		public static double ROU(double a){return System.Math.Floor(a+0.5);}
		/**<summary>四捨五入round</summary>*/
		public static double ROU(double a,double exp){return System.Math.Floor(a/exp+0.5)*exp;}
		/**<summary>四捨五入round</summary>*/
		public static double ROU(double a,double exp,double num){return System.Math.Floor(a/exp+num)*exp;}
		/**<summary>四捨五入round</summary>*/
		public static double ROUn(double a,double num){return System.Math.Floor(a+num);}
		/**<summary>切り捨てcutdown</summary>*/
		public static double ROUd(double a){return (a>=0)?System.Math.Floor(a):System.Math.Ceiling(a);}
		/**<summary>切り捨てcutdown</summary>*/
		public static double ROUd(double a,double exp){return exp*((a>=0)?System.Math.Floor(a/exp):System.Math.Ceiling(a/exp));}
		/**<summary>切り捨てcutdown</summary>*/
		public static double ROUd(double a,double exp,double number){return number+exp*((a>=0)?System.Math.Floor((a-number)/exp):System.Math.Ceiling((a-number)/exp));}
		/**<summary>切り捨てcutdown</summary>*/
		public static double ROUdn(double a,double number){return number+((a>=0)?System.Math.Floor(a-number):System.Math.Ceiling(a-number));}
		/**<summary>切り上げcutup</summary>*/
		public static double ROUu(double a){return (a>=0)?System.Math.Floor(a):System.Math.Ceiling(a);}
		/**<summary>切り上げcutup</summary>*/
		public static double ROUu(double a,double exp){return exp*((a>=0)?System.Math.Ceiling(a/exp):System.Math.Floor(a/exp));}
		/**<summary>切り上げcutup</summary>*/
		public static double ROUu(double a,double exp,double number){return number+exp*((a>=0)?System.Math.Ceiling((a-number)/exp):System.Math.Floor((a-number)/exp));}
		/**<summary>切り上げcutup</summary>*/
		public static double ROUun(double a,double number){return number+((a>=0)?System.Math.Ceiling(a-number):System.Math.Floor(a-number));}
	}
	/// <summary>
	/// 定数の集まり
	/// </summary>
	public class constant{
		public const double CpiRE2=1.5707963267948966192313216916398;
		public const double CpiMU3RE2=4.7123889803846898576939650749193;
		public const double CpiRE180=0.017453292519943295769236907684886;
		public const double C180REpi=57.295779513082320876798154814105;
		public const double Cpi=3.1415926535897932384626433832795;
		public const double CLN2=0.69314718055994530941723212145818;
		public const double CLN4=1.3862943611198906188344642429164;
		public const double CLN8=2.0794415416798359282516963643745;
		public const double CLN10=2.3025850929940456840179914546844;
		public const double CLN12=2.4849066497880003102297094798389;
		public const double CLN16=2.7725887222397812376689284858327;
		public const double CLN60=4.0943445622221006848304688130651;
		public const double CLN360=5.8861040314501556856429461714458;
		public const double Ce=2.7182818284590452353602874713527;
		public static complex CMPi=new complex(0,1);
		public static complex CMPLNi=new complex(0,1.5707963267948966192313216916398);
		public const double CRTST2REpiEN=0.79788456080286541;
		public const double CRTSTRE2piEN=0.3989422804014327;
		public const double C2REpi=0.63661977236758138;
		public const double C2MUpi=6.2831853071795862;
		//RTST...EN ="√(...)"
		//RE="/"
		//PL="+"
		//MU="*"
		//MI="-"
		//ST...EN="(...)"


		/**<summary>Gauss Constant</summary>*/
		public const double k=0.0172029895;
		/**<summary>Euler Constant</summary>*/
		public const double EGamma=0.577215664901533;
		/**<summary>万有引力定数cm3/gs2</summary>*/
		public const double GGamma=0.000000066732;
		/**<summary>天文単位km</summary>*/
		public const double astroA=149600000;
		/**<summary>光速m/s</summary>*/
		public const double c=299792458;
		/**<summary>地球赤道半径km</summary>*/
		public const double ae=6378.16;
		/**<summary>地球扁平率1/298</summary>*/
		public const double Jz=0.0033557046979865771;
		/**<summary>地心重力定数m3/s2</summary>*/
		public const double GE=398603000000000.0;
		public struct moon{
			/**<summary>月対地質量比1/81.3</summary>*/
			public const double mu=0.012300123001230012;
			/**<summary>月対恒平均運動/s</summary>*/
			public const double n=0.000002661699489;
			/**<summary>月歳差運動 sec.</summary>*/
			public const double p=5025.64;
		}
		/**<summary>黄道傾斜 deg</summary>*/
		public const double eta1900=23.452294444444444;
		/**<summary>衝動定数 sec.</summary>*/
		public const double N=9.21;
		/**<summary>太陽定数cal/min.cm2</summary>*/
		public const double sun=2.0;
		/**<summary>Boltsmann Constant J/K</summary>*/
		public const double Boltsmannk=1.380662E-23;
		/**<summary>Stefan=Boltsman Constant</summary>*/
		public const double Stefan_Boltsmann_sigma=0.0000000567032;
		/**<summary>プランク定数J/Hz</summary>*/
		public const double Plankh=6.626176E-34;
		/**<summary>プランク定数h_bar</summary>*/
		public const double Plankh_=1.054589E-34;
		/**<summary>第一放射定数Wm2</summary>*/
		public const double c1=0.0000000000000003741832;
		/**<summary>第二放射定数mK</summary>*/
		public const double c2=0.01438786;
		/**<summary>微細構造定数</summary>*/
		public const double alpha=0.0072973506;
		/**<summary>微細構造定数</summary>*/
		public const double alpha_recip=137.03604;
		/**<summary>Rydberg Constant</summary>*/
		public const double R8=1.097373177;
		/**<summary>Avogadro Number /mol</summary>*/
		public const double NA=6.022045E+23;
		/**<summary>Falad Constant C/mol</summary>*/
		public const double F=96484.56;
		/**<summary>気体定数 J/mol K</summary>*/
		public const double R=8.31441;
		/**<summary>第一宇宙速度km/s</summary>*/
		public const double v1=7.9;
		/**<summary>第二宇宙速度km/s</summary>*/
		public const double v2=11.2;
		/**<summary>第三宇宙速度km/s</summary>*/
		public const double v3=16.7;
		/**<summary>質量kg</summary>*/
		public struct m{
			public const double mu=1.883566E-28;
			public const double p=1.6726485E-27;
			public const double n=1.6749543E-27;
			public const double e=9.109534E-31;
		}
		/**<summary>原子量kg</summary>*/
		public const double u=1.6605655E-27;
		/**<summary>電気素量C</summary>*/
		public const double e=1.6021892E-19;
		/**<summary>電子比電荷 me C/kg</summary>*/
		public const double e2=175880470000.0;
		/**<summary>磁気 磁子 J/K</summary>*/
		public struct mu{
			public const double p=1.4106171E-26;
			public const double Mu=4.490474E-26;
			public const double N=5.050824E-27;
			public const double e=9.284832E-24;
			public const double B=9.274078E-24;
		}
		/**<summary>ボーア半径</summary>*/
		public const double a0=0.000000000052917706;
		/**<summary>磁子角運動量比 /sT</summary>*/
		public struct gamma{
			public const double p=267519870.0;
		}
		/**<summary>コンプトン波長</summary>*/
		public struct labmda{
			public const double c=2.4263089;
			public const double cp=1.3214099;
			public const double cn=1.3195909;
		}
		/**<summary>理想気体標準体積 m3/mol</summary>*/
		public const double Vm=0.02241383;
		/**<summary>磁束量子 Wb</summary>*/
		public const double Phi0=0.0000000000000020678506;
		/**<summary>真空誘電率 F/m</summary>*/
		public const double eta0=0.00000000000885418782;
	}

	/// <summary>
	/// <para>public class tri</para>
	/// <para>三角関数 逆三角関数</para>
	/// <para>双曲線関数 逆双曲線関数</para>
	/// <para>角度・弧度の変換</para>
	/// </summary>
	public class trigonomic{
		public static double DegRad(double deg){return deg*constant.CpiRE180;}
		public static double RadDeg(double rad){return rad*constant.C180REpi;}
		/**<summary>正弦</summary>*/
		public static double sin(double x){return System.Math.Sin(x);}
		/**<summary>余弦</summary>*/
		public static double cos(double x){return System.Math.Cos(x);}
		/**<summary>正接</summary>*/
		public static double tan(double x){return System.Math.Tan(x);}
		/**<summary>余接</summary>*/
		public static double cot(double x){return 1/System.Math.Tan(x);}
		/**<summary>正割</summary>*/
		public static double sec(double x){return 1/System.Math.Cos(x);}
		/**<summary>余割</summary>*/
		public static double cosec(double x){return 1/System.Math.Sin(x);}
		/**<summary>正矢</summary>*/
		public static double vers(double x){return 1-System.Math.Cos(x);}
		/**<summary>余矢</summary>*/
		public static double covers(double x){return 1-System.Math.Sin(x);}
		public static double hav(double x){return (1-System.Math.Cos(x))/2;}
		public static double cohav(double x){return (1-System.Math.Sin(x))/2;}
		public static double exsec(double x){return 1/System.Math.Cos(x)-1;}
		public static double excosec(double x){return 1/System.Math.Sin(x)-1;}
		public static double sinh(double x){return System.Math.Sinh(x)-1;}
		public static double cosh(double x){return System.Math.Cosh(x);}
		public static double tanh(double x){return System.Math.Tanh(x);}
		public static double coth(double x){return 1/System.Math.Tanh(x);}
		public static double sech(double x){return 1/System.Math.Cosh(x);}
		public static double cosech(double x){return 1/System.Math.Sinh(x);}
		public static double versh(double x){return 1-System.Math.Cosh(x);}
		public static double coversh(double x){return 1-System.Math.Sinh(x);}
		public static double havh(double x){return (1-System.Math.Cosh(x))/2;}
		public static double cohavh(double x){return (1-System.Math.Sinh(x))/2;}
		public static double exsech(double x){return 1/System.Math.Cosh(x)-1;}
		public static double excosech(double x){return 1/System.Math.Sinh(x)-1;}
		public class Arc{
			public static double sin(double x){return System.Math.Asin(x);}
			public static double cos(double x){return System.Math.Acos(x);}
			public static double tan(double x){return System.Math.Atan(x);}
			public static double cot(double x){return System.Math.Atan(x)+constant.CpiRE2;}
			public static double sec(double x){return constant.CpiRE2-System.Math.Atan(System.Math.Sign(x)/System.Math.Sqrt(x*x-1));}
			public static double cosec(double x){return System.Math.Atan(System.Math.Sign(x)/System.Math.Sqrt(x*x-1));}
			public static double vers(double x){return System.Math.Acos(1-x);}
			public static double covers(double x){return System.Math.Asin(1-x);}
			public static double hav(double x){return System.Math.Acos(1-2*x);}
			public static double cohav(double x){return System.Math.Asin(1-2*x);}
			public static double exsec(double x){return constant.CpiRE2-System.Math.Atan(System.Math.Sign(x+1)/System.Math.Sqrt(x*(x+2)));}
			public static double excosec(double x){return System.Math.Atan(System.Math.Sign(x+1)/System.Math.Sqrt(x*(x+2)));}
			public static double sinh(double x){return System.Math.Log(x+System.Math.Sqrt(x*x+1));}
			public static double cosh(double x){return System.Math.Log(x+System.Math.Sqrt(x*x-1));}
			public static double tanh(double x){return System.Math.Log((1+x)/(1-x))/2;}
			public static double coth(double x){return System.Math.Log((x+1)/(x-1))/2;}
			public static double sech(double x){return System.Math.Log((System.Math.Sqrt(1-x*x)+1)/x);}
			public static double cosech(double x){return System.Math.Log((System.Math.Sign(x)*System.Math.Sqrt(x*x+1)+1)/x);}
			public static double versh(double x){return System.Math.Log(1-x+System.Math.Sqrt(x*(x-2)));}
			public static double coversh(double x){return System.Math.Log(1-x+System.Math.Sqrt(x*(x-2)+2));}
			public static double havh(double x){return System.Math.Log(2-2*(x+System.Math.Sqrt(x*(x-1))));}
			public static double cohavh(double x){return System.Math.Log(2-2*x+System.Math.Sqrt(4*x*(x-1)+2));}
			public static double exsech(double x){return System.Math.Log((System.Math.Sqrt(1-x*(x+2))+1)/(x+1));}
			public static double excosech(double x){return System.Math.Log((System.Math.Sign(x+1)*System.Math.Sqrt(x*(x+2)+2)+1)/x);}
		}
	}

	#region cls:rational 有理数
	public class rational{
		/*field*/
		public decimal denom;
		public decimal numer;
		public decimal expon;
		public sbyte sign;
		/*constructer*/
		public rational(decimal denominator,decimal numerator,decimal exponent,string sig){
			switch(sig){
				case "0": sign=0; return;
				case "+": sign=1; return;
				case "-": sign=-1; return;
				case "1": sign=1; return;
				case "-1": sign=-1; return;
				case "∞": sign=18; return;
				case "+∞": sign=8; return;
				case "-∞": sign=-8; return;
				case "18": sign=18; return;
				case "8": sign=8; return;
				case "-8": sign=-8; return;
				case "indifinite": sign=10; return;
				case "10": sign=10; return;
				case "impossible": sign=11; return;
				case "11": sign=11; return;
				case "any": sign=12; return;
				case "12": sign=12; return;
			}
			if(denominator==0){
				numer=0;
				denom=1;
				expon=0;
				sign=(numerator==0)?(sbyte)12:(sbyte)11;
				return;
			}
			if(denominator<0){
				denominator=-denominator; sign=(sbyte)-sign;
			}
			if(numerator<0){
				numerator=-numerator; sign=(sbyte)-sign;
			}
			char[] dot=new char[] { '.' };
			string[] a=denominator.ToString().Split(dot,2);
			if(a.Length==2){
				exponent+=a[1].Length;
				denominator=System.Decimal.Parse(a[0]+a[1]);
			}/*小数の分母を整数に*/
			a=numerator.ToString().Split(dot,2); if(a.Length==2){
				exponent-=a[1].Length; numerator=System.Decimal.Parse(a[0]+a[1]);
			}/*小数の分子を整数に*/
			string b=denominator.ToString();
			while(b.Substring(b.Length,1)=="0"){
				b=b.Substring(1,b.Length-1);
				denominator/=10;
				exponent--;
			}/*10の倍数の分母を10で除す*/
			b=numerator.ToString();
			while(b.Substring(b.Length,1)=="0"){
				b=b.Substring(1,b.Length-1);
				numerator/=10;
				exponent++;
			}/*10の倍数の分子を10で除す*/
			decimal c=integer.GCM(denominator,numerator);
			numerator/=c;
			denom=denominator/c;
			numer=numerator/c;
			expon=exponent;
		}
		public rational(decimal denominator,decimal numerator,decimal exponent){
			rational a=new rational(denominator,numerator,exponent,"+");
			numer=a.numer; denom=a.denom; expon=a.expon; sign=a.sign;
		}
		public rational(decimal denominator,decimal numerator){
			rational a=new rational(denominator,numerator,0,"+");
			numer=a.numer; denom=a.denom; expon=a.expon; sign=a.sign;
		}
		/*operator*/
		public static rational operator+(rational a,rational b){
			if(a.sign==10|b.sign==10) return new rational(1,1,0,"10");
			if(a.sign==11|b.sign==11) return new rational(1,1,0,"11");
			if(a.sign==12|b.sign==12) return new rational(1,1,0,"12");
			if(a.sign==18&b.sign==18|a.sign==8&b.sign==-8|a.sign==-8&b.sign==8) return new rational(1,1,0,"10");
			if(a.sign==8|b.sign==8) return new rational(1,1,0,"8");
			if(a.sign==-8|b.sign==-8) return new rational(1,1,0,"-8"); decimal exp;
			if(a.expon>b.expon){
				a.numer=multi10(a.numer,a.expon-b.expon);
				exp=b.expon;
				integer.reduction(ref a.numer,ref a.denom);
			}else{
				b.numer=multi10(b.numer,b.expon-a.expon);
				exp=a.expon;
				integer.reduction(ref b.numer,ref b.denom);
			}
			decimal l=integer.GCM(a.denom,b.denom);
			decimal den=a.denom*b.denom/l;
			a.denom/=l;
			b.denom/=l;
			if(a.sign==-1) a.numer=-a.numer;
			if(b.sign==-1) b.numer=-b.numer;
			return new rational(a.numer*b.denom+a.denom*b.numer,den,a.expon+b.expon,"+");
		}
		public static rational operator-(rational a,rational b){
			if(a.sign==10|b.sign==10) return new rational(1,1,0,"10");
			if(a.sign==11|b.sign==11) return new rational(1,1,0,"11");
			if(a.sign==12|b.sign==12) return new rational(1,1,0,"12");
			if(a.sign==18&b.sign==18|a.sign==8&b.sign==8|a.sign==-8&b.sign==-8) return new rational(1,1,0,"10");
			if(a.sign==8|b.sign==-8) return new rational(1,1,0,"8");
			if(a.sign==-8|b.sign==8) return new rational(1,1,0,"-8"); decimal exp;
			if(a.expon>b.expon){
				a.numer=multi10(a.numer,a.expon-b.expon);
				exp=b.expon;
				integer.reduction(ref a.numer,ref a.denom);
			}else{
				b.numer=multi10(b.numer,b.expon-a.expon);
				exp=a.expon;
				integer.reduction(ref b.numer,ref b.denom);
			}
			decimal l=integer.GCM(a.denom,b.denom);
			decimal den=a.denom*b.denom/l;
			a.denom/=l;
			b.denom/=l;
			if(a.sign==-1) a.numer=-a.numer;
			if(b.sign==1) b.numer=-b.numer;
			return new rational(a.numer*b.denom+a.denom*b.numer,den,a.expon+b.expon,"+");
		}
		public static rational operator+(rational a){return a;}
		public static rational operator-(rational a){
			if(a.sign==1|a.sign==-1|a.sign==8|a.sign==-8) a.sign=(sbyte)-a.sign;
			return a;
		}
		public static rational operator*(rational a,rational b){
			if(a.sign==10|b.sign==10) return new rational(0,1,0,"10");
			if(a.sign==11|b.sign==11) return new rational(0,1,0,"11");
			if(a.sign==12|b.sign==12) return new rational(0,1,0,"12");
			if(a.sign==0|b.sign==0) return new rational(0,1,0,"0");
			if(a.sign==18|b.sign==18) return new rational(0,1,0,"18"); string sig;
			if(a.sign*b.sign<0){
				if(a.sign==8|b.sign==8|a.sign==-8|b.sign==-8) return new rational(0,1,0,"-8");
				sig="-1";
			}else{
				if(a.sign==8|b.sign==8|a.sign==-8|b.sign==-8) return new rational(0,1,0,"8");
				sig="1";
			}
			integer.reduction(ref a.numer,ref b.denom);
			integer.reduction(ref a.denom,ref b.numer);
			return new rational(a.numer*b.numer,a.denom*b.denom,a.expon+b.expon,sig);
		}
		public static rational operator/(rational a,rational b){
			if(a.sign==10|b.sign==10) return new rational(0,1,0,"19");
			if(a.sign==11|b.sign==11) return new rational(0,1,0,"11");
			if(a.sign==12|b.sign==12) return new rational(0,1,0,"12");
			if(a.sign==0) return new rational(0,1,0,(b.sign==0)?"12":"0");
			if(b.sign==0) return new rational(0,1,0,"11");
			if(b.sign==18|b.sign==8|b.sign==-8) return new rational(0,1,0,(a.sign==8|a.sign==-8|a.sign==18)?"10":"0");
			if(a.sign==18) return new rational(0,1,0,"18");
			if(a.sign==8) return new rational(0,1,0,(b.sign==1)?"8":"-8");
			if(a.sign==-8) return new rational(0,1,0,(b.sign==1)?"-8":"8");
			integer.reduction(ref a.denom,ref b.denom);
			integer.reduction(ref a.numer,ref b.numer);
			return new rational(a.numer*b.denom,a.denom*b.numer,a.expon-b.expon,(a.sign==1^b.sign==1)?"-1":"1");
		}
		public static implicit operator string(rational a){return tostring(a);}
		/*function*/
		public override string ToString(){return tostring(this);}
		public string ToHTML(){
			if(sign==10) return "indifinite(未定義)";
			if(sign==11) return "impossible(不可)";
			if(sign==12) return "any(不定)";
			if(sign==8) return "+∞";
			if(sign==-8) return "-∞";
			if(sign==18) return "±∞";
			if(sign==2) return "+0(無限小)";/*未設定*/
			if(sign==-2) return "-0(無限小)";/*未設定*/
			if(sign==0) return "0";
			string den1,den,sig,exp="";
			if(denom!=1){
				den1="<table cellpadding=0><tr><td style='border-bottom:1 solid #000'>";
				den="</td></tr><tr><td>"+denom.ToString()+"</td></tr></table>";
				exp=(expon==0)?"":"</td><td rowspan=2>×10<sup>"+expon.ToString()+"</sup>";
				sig=(sign==1)?"":"</td><td rowspan=2>-";
				return den1+sig+numer.ToString()+exp+den;
			}else return ((sign==1)?"":"-")+numer.ToString()+((expon==0)?"":"×10<sup>"+expon.ToString()+"</sup>");
		}
		/*static function*/
		public static string tostring(rational a){
			if(a.sign==10) return "indifinite(未定義)";
			if(a.sign==11) return "impossible(不可)";
			if(a.sign==12) return "any(不定)";
			if(a.sign==8) return "+∞";
			if(a.sign==-8) return "-∞";
			if(a.sign==18) return "±∞";
			if(a.sign==2) return "+0(無限小)";/*未設定*/
			if(a.sign==-2) return "-0(無限小)";/*未設定*/
			if(a.sign==0) return "0";
			string den="";
			if(a.denom!=1)den="/"+a.denom.ToString();
			string exp="";
			if(a.expon!=0) exp="×10^"+a.expon.ToString();
			return ((a.sign==1)?"":"-")+a.numer.ToString()+den+exp;
		}
		private static decimal multi10(decimal a,decimal e){
			decimal r=a;
			int e2=(int)e;
			for(int i=0;i<e2;i++) r*=10;
			return r;
			//System.Decimal.Parse(a.ToString()+str1.stringmultiply("0",(int)e));
		}
	}
	#endregion

	public class angle{
		public double ang;
		public angle(double a){ this.ang=a;}
		public angle(double x,double y){
			if(y==0) this.ang=(x>=0)?0:constant.Cpi;
			else if(x==0) this.ang=(y>0)?constant.CpiRE2:constant.CpiMU3RE2;
			else if(x>0) this.ang=(System.Math.Atan(x/y)+constant.C2MUpi)%constant.C2MUpi;
			else this.ang=(System.Math.Atan(x/y)+constant.Cpi)%constant.C2MUpi;
		}
		public static angle operator+(angle a){return a;}
		public static angle operator+(angle a,angle b){return (a+b)%constant.C2MUpi;}
		public static angle operator-(angle a){return (constant.C2MUpi-a)%constant.C2MUpi;}
		public static angle operator-(angle a,angle b){return (constant.C2MUpi+a-b)%constant.C2MUpi;}
		public static implicit operator double(angle a){return a.ang;}
		public static implicit operator angle(double a){return new angle(a);}
		public double degree{get{return this.ang*constant.C180REpi;}}
		public double radian{get{return this.ang*constant.CpiRE180;}}
	}

	/*	public class str
			{
				public int num;
				public static string DblStr(double number){
					string num0=number.ToString();
					int len=num0.Length;
					while(len>0){
						if(num0.Substring(len,1)=="e"){
							string a0=num0.Substring(1,len-1).Remove(2,1);
							str a1=(str)num0.Substring(len+1);
							if(a1.num<0)return "0." + ("0" * (str)(-a1.num-1)) + a0;
							if(a0.Length<=1+a1.num)return a0+("0"* (str)(1+a1.num-a0.Length));
							return a0.Insert(a1.num+2,".");
						}
						len--;
					}
					return num0;
				}
				public static string operator *(string a,str n)
				{
					string o="";
					int nn=n.num;
					while(nn>0){o+=a;nn--;}
					return o;
				}
				public static implicit operator str(int a){return new str(a);}
				public static implicit operator str(string a){return (str)(int)(System.Data.SqlTypes.SqlInt16)(System.Data.SqlTypes.SqlString)a;}
				public str(int a){this.num=a;}
			}
	*/
}

