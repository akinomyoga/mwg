using System.Diagnostics;
using Interop=System.Runtime.InteropServices;

namespace mwg.InterProcess{
	/// <summary>
	/// 他のプロセス内のメモリに対するポインタを表現します。
	/// </summary>
	/// <typeparam name="T">ポインタの型を指定します。</typeparam>
	public unsafe struct RemotePtr<T> where T:struct{
		private readonly ProcessMemory mem;
		private readonly byte* _base;

		internal RemotePtr(ProcessMemory mem):this(mem,(byte*)0){}
		internal RemotePtr(ProcessMemory mem,byte* _base){
			this.mem=mem;
			this._base=_base;
		}
		/// <summary>
		/// 現在の位置にあるデータを取得又は設定します。
		/// </summary>
		public T Value{
			get{return Read(mem,_base);}
			set{Write(mem,_base,value);}
		}
		/// <summary>
		/// 指定した位置にあるデータを取得又は設定します。
		/// </summary>
		/// <param name="index">読み書きするデータの相対位置を指定します。</param>
		/// <returns>読み取ったデータを返します。</returns>
		public T this[int index]{
			get{return Read(mem,_base+size*index);}
			set{Write(mem,_base+size*index,value);}
		}
		/// <summary>
		/// 他のプロセスのメモリを管理する ProcessMemory インスタンスを取得します。
		/// </summary>
		public ProcessMemory ProcessMemory{
			get{return this.mem;}
		}
		/// <summary>
		/// このインスタンスが現在指し示している先の仮想アドレスを取得します。
		/// </summary>
		public System.IntPtr Address{
			get{return (System.IntPtr)this._base;}
		}
		/// <summary>
		/// このインスタンスが NULL ポインタであるか否かを取得します。
		/// </summary>
		public bool IsNull{
			get{return this._base==(byte*)0;}
		}
		/// <summary>
		/// このインスタンスの指し示す先の説明を取得します。
		/// </summary>
		/// <returns>プロセス名及びアドレスを説明する文字列を返します。</returns>
		public override string ToString(){
			string addr;
			if((System.IntPtr)this._base==System.IntPtr.Zero){
				addr="nullptr";
			}else{
				addr=((long)this._base).ToString(System.IntPtr.Size==8?"X16":"X8");
			}
			return string.Format(
				"{2} @ \"{0}\" [pid:{1}]",
				this.mem.Process.ProcessName,
				this.mem.Process.Id,
				addr
				);
		}
		private static readonly bool isEnum=typeof(T).IsEnum;
		private static readonly System.Type read_type=isEnum?System.Enum.GetUnderlyingType(typeof(T)):typeof(T);
		private static readonly int size=Interop::Marshal.SizeOf(read_type);
		/// <summary>
		/// プロセスの指定した仮想アドレスにある内容を読み取ります。
		/// </summary>
		/// <param name="mem">読み取り元のプロセスへのメモリアクセスを提供する ProcessMemory を指定します。</param>
		/// <param name="addr">読み取り元の、指定したプロセス内に於ける仮想アドレスを指定します。</param>
		/// <returns>読み取った内容を返します。</returns>
		public static T Read(ProcessMemory mem,void* addr){
			byte* p=stackalloc byte[size];
			mem.ReadMemory(addr,p,size);
			return (T)Interop::Marshal.PtrToStructure((System.IntPtr)p,read_type);
		}
		public static void Write(ProcessMemory mem,void* addr,T value){
			byte* p=stackalloc byte[size];
			Interop::Marshal.StructureToPtr(value,(System.IntPtr)p,false);
			mem.WriteMemory(addr,p,size);
		}

		//============================================================
		//		他の型の読み取り
		//============================================================
		public RemotePtr<U> ReadPtr<U>() where U:struct{
			return new RemotePtr<U>(this.mem,(byte*)this.Read<System.IntPtr>());
		}
		public RemotePtr<U> ReadPtr32<U>() where U:struct{
			return new RemotePtr<U>(this.mem,(byte*)this.Read<uint>());
		}
		public U Read<U>() where U:struct{
			return RemotePtr<U>.Read(this.mem,this._base);
		}
		public void Write<U>(U value) where U:struct{
			RemotePtr<U>.Write(this.mem,this._base,value);
		}
		static System.Text.Decoder dec=System.Text.Encoding.GetEncoding("shift_jis").GetDecoder();
		public string ReadAnsiString(){
			const int SZ_BUFF=0x100;
			byte* prem=_base;
			byte* pbuff=stackalloc byte[SZ_BUFF+1];
			char* pchar=stackalloc char[SZ_BUFF];
			System.Text.StringBuilder build=new System.Text.StringBuilder();

			pbuff[SZ_BUFF]=0; // sentinel
			lock(dec){
				dec.Reset();
				int cByts,cChrs;
				do{
					// prem → pbuff
					mem.ReadMemory(prem,pbuff,SZ_BUFF);prem+=SZ_BUFF;
					byte* scn=pbuff;while(*scn!=0)scn++; // null 文字の位置迄
					cByts=(int)(scn-pbuff);
					
					// pbuff → pchar
					cChrs=dec.GetChars(pbuff,cByts,pchar,SZ_BUFF,false);

					// pchar → build
					build.Append(new string(pchar,0,cChrs));
				}while(cByts==SZ_BUFF);
			}
			return build.ToString();
		}
		//============================================================
		//		変換 / 移動
		//============================================================
		public static explicit operator RemotePtr<T>(ProcessMemory mem) {
			return new RemotePtr<T>(mem,(byte*)0);
		}
		public RemotePtr<U> Reinterpret<U>() where U:struct{
			return new RemotePtr<U>(this.mem,this._base);
		}
		public RemotePtr<T> Advance(int offset){
			return new RemotePtr<T>(this.mem,this._base+offset);
		}
		public RemotePtr<T> Recede(int bytes){
			return new RemotePtr<T>(this.mem,this._base-bytes);
		}
		public RemotePtr<T> Advance(System.IntPtr offset){
			return new RemotePtr<T>(this.mem,this._base+(long)offset);
		}
		public RemotePtr<T> Recede(System.IntPtr bytes){
			return new RemotePtr<T>(this.mem,this._base-(long)bytes);
		}
		/// <summary>
		/// 指定した個数の要素分だけ、位置を後方に移動します。
		/// </summary>
		/// <param name="acc">移動する前の RemotePtr を指定します。</param>
		/// <param name="index">移動先の位置を指定します。</param>
		/// <returns>移動した先の位置を既定とする RemotePtr を返します。</returns>
		public static RemotePtr<T> operator+(RemotePtr<T> acc,int index) {
			return new RemotePtr<T>(acc.mem,acc._base+size*index);
		}
		public static RemotePtr<T> operator-(RemotePtr<T> acc,int index) {
			return new RemotePtr<T>(acc.mem,acc._base-size*index);
		}
		public static RemotePtr<T> operator+(RemotePtr<T> acc,uint index) {
			return new RemotePtr<T>(acc.mem,acc._base+size*index);
		}
		public static RemotePtr<T> operator-(RemotePtr<T> acc,uint index) {
			return new RemotePtr<T>(acc.mem,acc._base-size*index);
		}
		public static RemotePtr<T> operator+(RemotePtr<T> acc,long index){
			return new RemotePtr<T>(acc.mem,acc._base+size*index);
		}
		public static RemotePtr<T> operator-(RemotePtr<T> acc,long index){
			return new RemotePtr<T>(acc.mem,acc._base-size*index);
		}
		public static RemotePtr<T> operator+(RemotePtr<T> acc,ulong index){
			return new RemotePtr<T>(acc.mem,acc._base+(ulong)size*index);
		}
		public static RemotePtr<T> operator-(RemotePtr<T> acc,ulong index){
			return new RemotePtr<T>(acc.mem,acc._base-(ulong)size*index);
		}
		public static RemotePtr<T> operator+(RemotePtr<T> acc,System.IntPtr index){
			return new RemotePtr<T>(acc.mem,acc._base+size*(long)index);
		}
		public static RemotePtr<T> operator-(RemotePtr<T> acc,System.IntPtr index) {
			return new RemotePtr<T>(acc.mem,acc._base-size*(long)index);
		}
		public static long operator-(RemotePtr<T> l,RemotePtr<T> r){
			if(l.mem!=r.mem)
				throw new System.ArithmeticException("異なるメモリ空間上のポインタ同士で減算は出来ません。");
			return l._base-r._base;
		}
		public static RemotePtr<T> operator++(RemotePtr<T> val){
			return new RemotePtr<T>(val.mem,val._base+size);
		}
		public static RemotePtr<T> operator--(RemotePtr<T> val){
			return new RemotePtr<T>(val.mem,val._base-size);
		}
		//============================================================
		//		比較
		//============================================================
		public override bool Equals(object obj){
			return obj is RemotePtr<T>&&this==(RemotePtr<T>)obj;
		}
		public override int GetHashCode() {
			return this.mem.GetHashCode()^((System.IntPtr)this._base).GetHashCode();
		}
		public static bool operator==(RemotePtr<T> l,RemotePtr<T> r){
			return l.mem==r.mem&&l._base==r._base;
		}
		public static bool operator!=(RemotePtr<T> l,RemotePtr<T> r){
			return !(l==r);
		}
		public static bool operator<(RemotePtr<T> l,RemotePtr<T> r){
			return l.mem==r.mem&&l._base<r._base;
		}
		public static bool operator>(RemotePtr<T> l,RemotePtr<T> r){
			return l.mem==r.mem&&l._base>r._base;
		}
		public static bool operator<=(RemotePtr<T> l,RemotePtr<T> r){
			return l.mem==r.mem&&l._base<=r._base;
		}
		public static bool operator>=(RemotePtr<T> l,RemotePtr<T> r){
			return l.mem==r.mem&&l._base>=r._base;
		}
	}
}