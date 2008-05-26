namespace mwg.Math{

	public class complex{
	//=====================================
	//          Field
	//-------------------------------------
		public double Re;
		public double Im;
	//=====================================
	//          Constructers
	//-------------------------------------
		public complex(double Re,double Im){this.Re=Re;this.Im=Im;}
		public complex(double Re){this.Re=Re;this.Im=0;}
		public complex(double rad,angle arg,string type){
			if(type=="angle"){this.Re=rad*System.Math.Cos(arg);this.Im=rad*System.Math.Sin(arg);}
		}
	//=====================================
	//          Operators
	//-------------------------------------
		/**<summary>public static complex operator +(complex a)
		</summary>*/public static complex operator +(complex a){return a;}
		/**<summary>public static complex operator -(complex a)
		</summary>*/public static complex operator -(complex a){return new complex(-a.Re,-a.Im);}
		/**<summary>public static complex operator +(complex a,complex b)
		</summary>*/public static complex operator +(complex a,complex b){return new complex(a.Re+b.Re,a.Im+b.Im);}
		/**<summary>public static complex operator -(complex a,complex b)
		</summary>*/public static complex operator -(complex a,complex b){return new complex(a.Re-b.Re,a.Im-b.Im);}
		public static complex operator *(complex a,complex b){return new complex(a.Re*b.Re-a.Im*b.Im,a.Re*b.Im+a.Im*b.Re);}
		public static complex operator /(complex a,complex b){double r=b.Re*b.Re+b.Im*b.Im;if(r==0)return a;
			return new complex(a.Re*b.Re+a.Im*b.Im,a.Im*b.Re-a.Re*b.Im)/r;}
		public static complex operator ++(complex a){return new complex(a.Re+1,a.Im);}
		public static complex operator --(complex a){return new complex(a.Re-1,a.Im);}
		public static complex operator ^(complex a,complex b){complex n=b*new complex(a.abs,a.arg);
			return System.Math.Exp(n.Re)*new complex(System.Math.Cos(n.Im),System.Math.Sin(n.Im));}
		public static implicit operator complex(double a){return new complex(a);} 
		public static implicit operator double(complex a){return a.Re;}
		public static implicit operator complex(int a){return new complex((double)a);}
		public static implicit operator int(complex a){return (int)a.Re;}
		/**<summary>‹¤–ğ•¡‘f”‰‰Zq</summary>*/public static complex operator ~(complex a){return new complex(a.Re,-a.Im);}
	//=====================================
	//          Properties
	//-------------------------------------
		/**<summary>‹¤–ğ•¡‘f”</summary>*/public complex cnj{get{
			return new complex(this.Re,-this.Im);}}
		/**<summary>â‘Î’l</summary>*/public complex abs{get{
			return new complex(System.Math.Sqrt(this.Re*this.Re+this.Im*this.Im),0);}}
		/**<summary>‹t”</summary>*/public complex rec{get{double r=this.Re*this.Re+this.Im*this.Im;if(r==0)return 0;
			return new complex(this.Re,-this.Im)/r;}}
		/**<summary>•ÎŠp</summary>*/public complex arg{get{
			if(this.Re==0)return (this.Im<0) ? -constant.CpiRE2 : constant.CpiRE2;
			if(this.Im<0 | this.Re<0 & this.Im==0)return new complex(System.Math.Atan(this.Im/this.Re)+constant.Cpi);
			return new complex(System.Math.Atan(this.Im/this.Re));
		}}
		/**<summary>•½•ûª</summary>*/public complex sqr{get{double r=this.arg/2;
			return System.Math.Exp(this.abs/2)*new complex(System.Math.Cos(r),System.Math.Sin(r));
		}}
		/**<summary>•¶š—ñŒ^string‚É•ÏŠ·</summary>*/public string tostring{get{
			if(this.Re==0){
				if(this.Im==0)return "0"; else return this.Im.ToString()+"i";
			}
			if(this.Im==0)return this.Re.ToString();
			return this.Re + "+" + this.Im + "i";
		}}
	//=====================================
	//          Functions
	//-------------------------------------
	/*‰“™ŠÖ”*/
		/**<summary>•½•ûª(•¡‘f”)</summary>*/public static complex sqrt(complex x){double r=x.arg/2;
			return System.Math.Exp(x.abs/2)*new complex(System.Math.Cos(r),System.Math.Sin(r));}
		/**<summary>³Œ·(•¡‘f”)</summary>*/public static complex sin(complex x){
			return new complex(System.Math.Sin(x.Re)*System.Math.Cosh(x.Im),System.Math.Cos(x.Re)*System.Math.Sinh(x.Im));}
		/**<summary>—]Œ·(•¡‘f”)</summary>*/public static complex cos(complex x){
			return new complex(System.Math.Cos(x.Re)*System.Math.Cosh(x.Im),-System.Math.Sin(x.Re)*System.Math.Sinh(x.Im));}
		/**<summary>³Ú(•¡‘f”)</summary>*/public static complex tan(complex x){
			return new complex(System.Math.Sin(x.Re*2),System.Math.Sinh(x.Im*2))/(System.Math.Cos(x.Re*2)+System.Math.Cosh(x.Im*2));}
		/**<summary>—]Ú(•¡‘f”)</summary>*/public static complex cot(complex x){
			return new complex(System.Math.Sin(x.Re*2),System.Math.Sinh(x.Im*2))/(System.Math.Cosh(x.Im*2))-System.Math.Cos(x.Re*2);}
		/**<summary>³Š„(•¡‘f”)</summary>*/public static complex sec(complex x){double a=System.Math.Cosh(x.Im);double b=System.Math.Cos(x.Re);
			return new complex(System.Math.Sin(x.Re)*System.Math.Cosh(x.Im),System.Math.Cos(x.Re)*System.Math.Sinh(x.Im))/((a+b)*(a-b));}
		/**<summary>—]Š„(•¡‘f”)</summary>*/public static complex cosec(complex x){double a=System.Math.Cosh(x.Im);double b=System.Math.Sin(x.Re);
			return new complex(System.Math.Cos(x.Re) * System.Math.Cosh(x.Im),System.Math.Sin(x.Re) * System.Math.Sinh(x.Im))/((a+b)*(a-b));}
		/**<summary>³–î(•¡‘f”)</summary>*/public static complex vers(complex x){return 1-cos(x);}
		/**<summary>—]–î(•¡‘f”)</summary>*/public static complex covers(complex x){return 1-sin(x);}
		public static complex hav(complex x){return (1-cos(x))/2;}
		public static complex cohav(complex x){return (1-sin(x))/2;}
		public static complex exsec(complex x){return sec(x)-1;}
		public static complex excosec(complex x){return cosec(x)-1;}
		/**<summary>‘o‹Èü³Œ·(•¡‘f”)</summary>*/public static complex sinh(complex x){
			return new complex(System.Math.Sinh(x.Re)*System.Math.Cos(x.Im),System.Math.Cosh(x.Re)*System.Math.Sin(x.Im));}
		/**<summary>‘o‹Èü—]Œ·(•¡‘f”)</summary>*/public static complex cosh(complex x){
			return new complex(System.Math.Cosh(x.Re)*System.Math.Cos(x.Im),System.Math.Sinh(x.Re)*System.Math.Sin(x.Im));}
		/**<summary>‘o‹Èü³Ú(•¡‘f”)</summary>*/public static complex tanh(complex x){
			return new complex(System.Math.Sinh(2*x.Re),System.Math.Sin(2*x.Im))/(System.Math.Cosh(2*x.Re)+System.Math.Cos(x.Im));}
		/**<summary>‘o‹Èü—]Ú(•¡‘f”)</summary>*/public static complex coth(complex x){
			return new complex(System.Math.Sinh(2*x.Re),System.Math.Sin(2*x.Im))/(System.Math.Cosh(2*x.Re)-System.Math.Cos(x.Im));}
		/**<summary>‘o‹Èü³Š„(•¡‘f”)</summary>*/public static complex sech(complex x){return cosh(x).rec;}
		/**<summary>‘o‹Èü—]Š„(•¡‘f”)</summary>*/public static complex cosech(complex x){return sinh(x).rec;}
		/**<summary>‘o‹Èü³–î(•¡‘f”)</summary>*/public static complex versh(complex x){return 1-cosh(x);}
		/**<summary>‘o‹Èü—]–î(•¡‘f”)</summary>*/public static complex coversh(complex x){return 1-sinh(x);}
		public static complex havh(complex x){return (1-cosh(x))/2;}
		public static complex cohavh(complex x){return (1-sinh(x))/2;}
		public static complex exsech(complex x){return cosh(x).rec-1;}
		public static complex excosech(complex x){return sinh(x).rec-1;}
		/**<summary>©‘R‘Î”(•¡‘f”)</summary>*/public static complex ln(complex x){
			return new complex(System.Math.Log(x.abs),x.arg);}
		/**<summary>í—p‘Î”(•¡‘f”)</summary>*/public static complex log(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN10;}
		/**<summary>‘Î”(’ê2)(•¡‘f”)</summary>*/public static complex log2(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN2;}
		/**<summary>‘Î”(’ê4)(•¡‘f”)</summary>*/public static complex log4(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN4;}
		/**<summary>‘Î”(’ê8)(•¡‘f”)</summary>*/public static complex log8(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN8;}
		/**<summary>‘Î”(’ê12)(•¡‘f”)</summary>*/public static complex log12(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN12;}
		/**<summary>‘Î”(’ê16)(•¡‘f”)</summary>*/public static complex log16(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN16;}
		/**<summary>‘Î”(’ê60)(•¡‘f”)</summary>*/public static complex log60(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN60;}
		/**<summary>‘Î”(’ê360)(•¡‘f”)</summary>*/public static complex log360(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CLN360;}
		/**<summary>‘Î”(’êi)(•¡‘f”)</summary>*/public static complex logi(complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/constant.CMPLNi;}
		/// <summary>‘Î”(•¡‘f”)</summary>
		/// <param name="a">’ê</param><param name="x">”</param><returns>‘Î”log_a(x)ya^log=xz</returns>
		public static complex log(complex a,complex x){
			return new complex(System.Math.Log(x.abs),x.arg)/new complex(System.Math.Log(a.abs),a.arg);}
		/**<summary>Gudermann”Ÿ”</summary>*/public static complex gd(complex x){return Arc.tan(sinh(x));}
		/**<summary>‘æˆêíTschebyscheff(Chebychef)‘½€® Tn(x)=cos(n arccos x)</summary>*/
		public static complex Tn(complex n,complex x){return cos(n*Arc.cos(x));}
		/**<summary>‘æ“ñíTschebyscheff(Chebychef)‘½€® U*n(x)=sin(n arccos x)/ã(1-x^2)</summary>*/
		public static complex Un_a(complex n,complex x){return sin(n*Arc.cos(x))/sqrt(1-x*x);}
		/**<summary>‘æ“ñíTschebyscheff(Chebychef)”Ÿ” Un(x)=sin(n arccos x)</summary>*/
		public static complex Un(complex n,complex x){return sin(n*Arc.cos(x));}
		/**<summary>ƒCƒ“ƒ{ƒŠƒ…[ƒg”Ÿ”;involute function</summary>*/
		public static complex inv(complex x){return tan(x)-x;}
		/**<summary>‹ÉƒCƒ“ƒ{ƒŠƒ…[ƒg”Ÿ”;polar involute function</summary>*/
		public static complex invr(complex x){return inv(Arc.sec(x));}
		public class Arc{
			/**<summary>‹t³Œ·(•¡‘f”)</summary>*/public static complex sin(complex x){return tan(x/complex.sqrt(1-x*x));}
			/**<summary>‹t—]Œ·(•¡‘f”)</summary>*/public static complex cos(complex x){return tan(complex.sqrt(1-x*x)/x);}
			/**<summary>‹t³Ú(•¡‘f”)</summary>*/public static complex tan(complex x){complex ix=constant.CMPi*x;
				return -0.5*constant.CMPi*complex.ln((1+ix)/(1-ix));}
			/**<summary>‹t—]Ú(•¡‘f”)</summary>*/public static complex cot(complex x){return tan(x.rec);}
			/**<summary>‹t³Š„(•¡‘f”)</summary>*/public static complex sec(complex x){return tan(complex.sqrt(x*x-1));}
			/**<summary>‹t—]Š„(•¡‘f”)</summary>*/public static complex cosec(complex x){return tan(complex.sqrt(x*x-1).rec);}
			/**<summary>‹t³–î(•¡‘f”)</summary>*/public static complex vers(complex x){return tan(complex.sqrt(x*(2-x))/(1-x));}
			/**<summary>‹t—]–î(•¡‘f”)</summary>*/public static complex covers(complex x){return tan((1-x)/complex.sqrt(x*(2-x)));}
			public static complex hav(complex x){return tan(complex.sqrt(x*(1-x))/(0.5-x));}
			public static complex cohav(complex x){return tan((0.5-x)/complex.sqrt(x*(1-x)));}
			public static complex exsec(complex x){return tan(complex.sqrt(x*(x+2)));}
			public static complex excosec(complex x){return tan(complex.sqrt(x*(x+2)).rec);}
			/**<summary>‹t‘o‹Èü³Œ·(•¡‘f”)</summary>*/public static complex sinh(complex x){return ln(x+sqrt(x*x+1));}
			/**<summary>‹t‘o‹Èü—]Œ·(•¡‘f”)</summary>*/public static complex cosh(complex x){return ln(x+sqrt(x*x-1));}
			/**<summary>‹t‘o‹Èü³Ú(•¡‘f”)</summary>*/public static complex tanh(complex x){return 0.5*ln((1+x)/(1-x));}
			/**<summary>‹t‘o‹Èü—]Ú(•¡‘f”)</summary>*/public static complex coth(complex x){return 0.5*ln((1-x)/(1+x));}
			/**<summary>‹t‘o‹Èü³Š„(•¡‘f”)</summary>*/public static complex sech(complex x){return ln((sqrt(1-x*x)+1)/x);}
			/**<summary>‹t‘o‹Èü—]Š„(•¡‘f”)</summary>*/public static complex cosech(complex x){return ln((sqrt(1+x*x)+1)/x);}
			/**<summary>‹t‘o‹Èü³–î(•¡‘f”)</summary>*/public static complex versh(complex x){return ln(1-x+sqrt(x*(x-2)));}
			/**<summary>‹t‘o‹Èü—]–î(•¡‘f”)</summary>*/public static complex coversh(complex x){return ln(1-x+sqrt(x*(x-2)+2));}
			public static complex havh(complex x){return ln(2*(sqrt(x*(x-1))-x)+1);}
			public static complex cohavh(complex x){return ln(1-2*x+sqrt(4*x*(x-1)+2));}
			public static complex exsech(complex x){return ln((sqrt(-x*(x+2))+1)/(x+1));}
			public static complex excosech(complex x){return ln((sqrt(x*(x+2)+2)+1)/(x+1));}
			/**<summary>‹tGudermann”Ÿ”ELambert”Ÿ”</summary>*/public static complex gd(complex x){complex s=sin(x);
				return 0.5*ln((1+s)/(1-s));}
		}
	}
}