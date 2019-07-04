using System.Diagnostics;
using Interop=System.Runtime.InteropServices;

namespace mwg.InterProcess{
	/// <summary>
	/// ���̃v���Z�X���̃������ɑ΂���|�C���^��\�����܂��B
	/// </summary>
	/// <typeparam name="T">�|�C���^�̌^���w�肵�܂��B</typeparam>
	public unsafe struct RemotePtr<T> where T:struct{
		private readonly ProcessMemory mem;
		private readonly byte* _base;

		internal RemotePtr(ProcessMemory mem):this(mem,(byte*)0){}
		internal RemotePtr(ProcessMemory mem,byte* _base){
			this.mem=mem;
			this._base=_base;
		}
		/// <summary>
		/// ���݂̈ʒu�ɂ���f�[�^���擾���͐ݒ肵�܂��B
		/// </summary>
		public T Value{
			get{return Read(mem,_base);}
			set{Write(mem,_base,value);}
		}
		/// <summary>
		/// �w�肵���ʒu�ɂ���f�[�^���擾���͐ݒ肵�܂��B
		/// </summary>
		/// <param name="index">�ǂݏ�������f�[�^�̑��Έʒu���w�肵�܂��B</param>
		/// <returns>�ǂݎ�����f�[�^��Ԃ��܂��B</returns>
		public T this[int index]{
			get{return Read(mem,_base+size*index);}
			set{Write(mem,_base+size*index,value);}
		}
		/// <summary>
		/// ���̃v���Z�X�̃��������Ǘ����� ProcessMemory �C���X�^���X���擾���܂��B
		/// </summary>
		public ProcessMemory ProcessMemory{
			get{return this.mem;}
		}
		/// <summary>
		/// ���̃C���X�^���X�����ݎw�������Ă����̉��z�A�h���X���擾���܂��B
		/// </summary>
		public System.IntPtr Address{
			get{return (System.IntPtr)this._base;}
		}
		/// <summary>
		/// ���̃C���X�^���X�� NULL �|�C���^�ł��邩�ۂ����擾���܂��B
		/// </summary>
		public bool IsNull{
			get{return this._base==(byte*)0;}
		}
		/// <summary>
		/// ���̃C���X�^���X�̎w��������̐������擾���܂��B
		/// </summary>
		/// <returns>�v���Z�X���y�уA�h���X��������镶�����Ԃ��܂��B</returns>
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
		/// �v���Z�X�̎w�肵�����z�A�h���X�ɂ�����e��ǂݎ��܂��B
		/// </summary>
		/// <param name="mem">�ǂݎ�茳�̃v���Z�X�ւ̃������A�N�Z�X��񋟂��� ProcessMemory ���w�肵�܂��B</param>
		/// <param name="addr">�ǂݎ�茳�́A�w�肵���v���Z�X���ɉ����鉼�z�A�h���X���w�肵�܂��B</param>
		/// <returns>�ǂݎ�������e��Ԃ��܂��B</returns>
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
		//		���̌^�̓ǂݎ��
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
					// prem �� pbuff
					mem.ReadMemory(prem,pbuff,SZ_BUFF);prem+=SZ_BUFF;
					byte* scn=pbuff;while(*scn!=0)scn++; // null �����̈ʒu��
					cByts=(int)(scn-pbuff);
					
					// pbuff �� pchar
					cChrs=dec.GetChars(pbuff,cByts,pchar,SZ_BUFF,false);

					// pchar �� build
					build.Append(new string(pchar,0,cChrs));
				}while(cByts==SZ_BUFF);
			}
			return build.ToString();
		}
		//============================================================
		//		�ϊ� / �ړ�
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
		/// �w�肵�����̗v�f�������A�ʒu������Ɉړ����܂��B
		/// </summary>
		/// <param name="acc">�ړ�����O�� RemotePtr ���w�肵�܂��B</param>
		/// <param name="index">�ړ���̈ʒu���w�肵�܂��B</param>
		/// <returns>�ړ�������̈ʒu������Ƃ��� RemotePtr ��Ԃ��܂��B</returns>
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
				throw new System.ArithmeticException("�قȂ郁������ԏ�̃|�C���^���m�Ō��Z�͏o���܂���B");
			return l._base-r._base;
		}
		public static RemotePtr<T> operator++(RemotePtr<T> val){
			return new RemotePtr<T>(val.mem,val._base+size);
		}
		public static RemotePtr<T> operator--(RemotePtr<T> val){
			return new RemotePtr<T>(val.mem,val._base-size);
		}
		//============================================================
		//		��r
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