namespace mwg.File{
	public class Bytes:System.IO.Stream,System.ICloneable{
		public byte[] data;
		private long position;
		
		public Bytes(){
			this.data=new byte[]{};
			this.position=0;
			this.buffer=new System.Collections.ArrayList();
		}
		public Bytes(byte[] data){
			this.data=data;
			this.position=0;
			this.buffer=new System.Collections.ArrayList();
		}
		//public Bytes(System.IO.BinaryReader br);

		#region System.IO.Stream
		private System.Collections.ArrayList buffer;
		public override bool CanRead{get{return true;}}
		public override bool CanWrite{get{return true;}}
		public override bool CanSeek{get{return true;}}
		public override void Close(){this.Flush();base.Close();}
		public override void Flush(){
			if(this.buffer.Count==0)return;
			byte[] a=this.data;
			byte[] b=(byte[])this.buffer.ToArray(typeof(byte));
			this.data=new byte[a.Length+b.Length];
			a.CopyTo(this.data,0);
			a.CopyTo(this.data,a.Length);
		}
		public override long Length{get{return this.data.Length+this.buffer.Count;}}
		public override long Position{
			get{return this.position;}
			set{
				this.Flush();
				if(value<0||value>this.data.Length)value=this.data.Length;
				this.position=(int)value;
			}
		}
		public override int Read(byte[] buffer, int offset, int count){
			if(buffer==null)throw new System.ArgumentNullException("buffer","null参照です");
			if(offset<0)throw new System.ArgumentOutOfRangeException("offset",offset,"負の値です");
			if(count<=0)throw new System.ArgumentOutOfRangeException("count",count,"正の値ではありません");
			if(buffer.Length<offset)throw new System.ArgumentException("buffer 長より大きな値を指しています","offset");
			if(buffer.Length<offset+count)throw new System.ArgumentException("offset+count が buffer 長を越えています","count");
			this.Flush();
			int i=0;
			for(;i<count;i++){
				buffer[offset+i]=this.data[this.position++];
				if(this.position==this.data.Length)return i+1;
			}
			return i;
		}
		public override int ReadByte(){
			this.Flush();
			if(this.position>=this.data.Length)throw Exceptions.EOF;//return -1;
			return this.data[this.position++];
		}
		public override long Seek(long offset, System.IO.SeekOrigin origin){
			this.Flush();
			switch(origin){
				case System.IO.SeekOrigin.Begin:this.position=offset;break;
				case System.IO.SeekOrigin.Current:this.position+=offset;break;
				case System.IO.SeekOrigin.End:this.position=this.data.Length+offset;break;
			}
			if(this.position<0){
				this.position=0;
			}else if(this.position>this.data.Length){
				this.position=this.data.Length;
			}
			return this.position;
		}
		public override void SetLength(long value){
			this.Flush();
			byte[] newarray=new byte[value];
			if(this.data.Length>value){
				for(int i=0;i<value;i++)newarray[i]=this.data[i];
			}else{
				this.data.CopyTo(newarray,0);
			}
			this.data=newarray;
		}
		public override void Write(byte[] buffer,int offset,int count){
			//--error
			if(buffer==null)throw new System.ArgumentNullException("buffer","null参照です");
			if(offset<0)throw new System.ArgumentOutOfRangeException("offset",offset,"負の値です");
			if(count<=0)throw new System.ArgumentOutOfRangeException("count",count,"正の値ではありません");
			if(buffer.Length<offset)throw new System.ArgumentException("buffer 長より大きな値を指しています","offset");
			if(buffer.Length<offset+count)throw new System.ArgumentException("offset+count が buffer 長を越えています","count");
			//--write
			int iM=offset+count;
			if(this.position<this.data.Length){
				int iM_=offset+(int)(this.data.Length-this.position);
				// 書き込みが this.data に収まる場合
				if(iM<=iM_){for(;offset<iM;this.data[this.position++]=buffer[offset++]);return;}
				// 書き込みが this.data を越える場合
				for(;offset<iM_;this.data[this.position++]=buffer[offset++]);
			}
			// まだ書き込んでいない部分を Bytes の buffer に書き込む
			for(;offset<iM;offset++){this.buffer.Add(buffer[offset]);this.position++;}
			if(this.buffer.Count>=4096)this.Flush();
		}
		public override void WriteByte(byte value){
			if(this.position>=this.data.Length/*||this.buffer.Length>0 * 位置が変更される時は必ずthis.Flush()を呼び出す */){
				this.buffer.Add(value);
				if(this.buffer.Count>=4096)this.Flush();
				this.position++;
			}else{
				this.data[this.position]=value;
				this.position++;
			}
		}
		#endregion

		#region その他の入出力関数
		/// <summary>
		/// 格納されているデータを先読みする。
		/// </summary>
		/// <param name="buffer">
		/// null を指定した場合、要素数が count の配列が作成されます。 offset は無視されます。
		/// 読み込んだ情報を格納するのに領域が狭すぎる時は、延長します。
		/// </param>
		/// <param name="offset">負の値が指定された場合、0 と解釈します。</param>
		/// <param name="count">負の値が指定された場合何もしません。</param>
		/// <returns>
		/// 読み取る事の出来た byte 数を返す。
		/// 要求されたとおり読み取る事が出来たかは、 
		/// <code>count==MyBytes.PreRead(buffer,offset,count)</code>
		/// などで確かめる事が出来る。
		/// </returns>
		public int PreRead(byte[] buffer,int offset,int count){
			if(count<=0)return 0;
			if(buffer==null){buffer=new byte[count];offset=0;}
			if(offset<0)offset=0;
			if(buffer.Length<offset+count){
				byte[] na=new byte[offset+count];
				buffer.CopyTo(na,0);
				buffer=na;
			}
			this.Flush();
			int i=0;
			for(;i<count;i++){
				buffer[offset+i]=this.data[this.position+i];
				if(this.position==this.data.Length)return i+1;
			}
			return i;
		}
		public int RestLength{get{return (int)(this.Length-this.Position);}}
		public void Write(byte[] buffer){
			//--error
			if(buffer==null)throw new System.ArgumentNullException("buffer","null参照です");
			//--write
			int offset=0;
			int iM=buffer.Length;
			if(this.position<this.data.Length){
				int iM_=offset+(int)(this.data.Length-this.position);
				// 書き込みが this.data に収まる場合
				if(iM<=iM_){for(;offset<iM;this.data[this.position++]=buffer[offset++]);return;}
				// 書き込みが this.data を越える場合
				for(;offset<iM_;this.data[this.position++]=buffer[offset++]);
			}
			// まだ書き込んでいない部分を Bytes の buffer に書き込む
			for(;offset<iM;offset++){this.buffer.Add(buffer[offset]);this.position++;}
			if(this.buffer.Count>=4096)this.Flush();
		}
		#endregion

		#region System.ICloneable
		public object Clone(){
			this.Flush();
			mwg.File.Bytes r=new mwg.File.Bytes((byte[])this.data.Clone());
			r.position=this.position;
			return r;
		}
		#endregion
		
		public static mwg.File.Bytes operator +(Bytes a,Bytes b){
			a.Flush();b.Flush();
			byte[] r=new byte[a.Length+b.Length];
			a.data.CopyTo(r,0);
			b.data.CopyTo(r,a.Length);
			return new mwg.File.Bytes(r);
		}
		//public static mwg.File.Bytes operator +(Bytes a,byte[] b){}
		public static explicit operator mwg.File.Bytes(byte[] dat){return new mwg.File.Bytes(dat);}
		public static explicit operator byte[](mwg.File.Bytes dat){dat.Flush();return dat.data;}

		#region Object 入出力
		public mwg.File.Bytes R(ref object obj){
			System.Type type=obj.GetType();
			if(type.IsSubclassOf(typeof(mwg.File.RW))){((mwg.File.RW)obj).ReadFrom(this);return this;}
			if(type==typeof(byte)){obj=(byte)this.ReadByte();return this;}
			if(type==typeof(sbyte)){obj=this.ReadSByte();return this;}
			if(type==typeof(short)){obj=this.ReadInt16();return this;}
			if(type==typeof(int)){obj=this.ReadInt32();return this;}
			if(type==typeof(long)){obj=this.ReadInt64();return this;}
			if(type==typeof(ushort)){obj=this.ReadUInt16();return this;}
			if(type==typeof(uint)){obj=this.ReadUInt32();return this;}
			if(type==typeof(ulong)){obj=this.ReadUInt64();return this;}
			if(type==typeof(bool)){obj=this.ReadBool();return this;}
			if(type==typeof(string)){obj=this.ReadString();return this;}
			if(type==typeof(byte[])){obj=this.ReadByteArray();return this;}
			throw Exceptions.NotSupportedType;//decimal float double
		}
		public mwg.File.Bytes W(object obj){
			System.Type type=obj.GetType();
			if(type.IsSubclassOf(typeof(mwg.File.RW))){((mwg.File.RW)obj).WriteTo(this);return this;}
			if(type==typeof(byte)){this.WriteByte((byte)obj);return this;}
			if(type==typeof(sbyte)){this.WriteSByte((sbyte)obj);return this;}
			if(type==typeof(short)){this.WriteInt16((short)obj);return this;}
			if(type==typeof(int)){this.WriteInt32((int)obj);return this;}
			if(type==typeof(long)){this.WriteInt64((long)obj);return this;}
			if(type==typeof(ushort)){this.WriteUInt16((ushort)obj);return this;}
			if(type==typeof(uint)){this.WriteUInt32((uint)obj);return this;}
			if(type==typeof(ulong)){this.WriteUInt64((ulong)obj);return this;}
			if(type==typeof(bool)){this.WriteBool((bool)obj);return this;}
			if(type==typeof(string)){this.WriteString((string)obj);return this;}
			if(type==typeof(byte[])){this.WriteByteArray((byte[])obj);return this;}
			throw Exceptions.NotSupportedType;//decimal float double
		}
		public bool ReadBool(){
			int r=this.ReadByte();
			if(r<0)throw Exceptions.EOF;
			return (r&1)==1;
		}
		public void WriteBool(bool val){
			this.WriteByte((byte)(val?1:0));
		}
		public string ReadFourcc(){
			byte[] buffer=new byte[4];
			this.Read(buffer,0,4);
			return System.Text.Encoding.ASCII.GetString(buffer);
		}
		public void WriteFourcc(string str){
			while(str.Length<4)str+=" ";
			this.Write(System.Text.Encoding.ASCII.GetBytes(str));
		}
		#endregion

		#region Object 入出力 - 整数型
		public sbyte ReadSByte(){
			int r=this.ReadByte();
			if(r<0)throw Exceptions.EOF;
			if(r>127)return (sbyte)(r-0x80+sbyte.MinValue);//負の数
			return (sbyte)r;
		}
		public void WriteSByte(sbyte val){
			if(val>=0)this.WriteByte((byte)val);
			else this.WriteByte((byte)(val-sbyte.MinValue+0x80));
		}
		public short ReadInt16(){
			short r=0;
			if(this.RestLength<2)throw Exceptions.EOF;
			r=(short)this.ReadByte();
			if(r>127){//負の数
				return (short)((r-128)*256+this.ReadByte()+System.Int16.MinValue);
			}else{/*正の数*/
				return (short)(r<<8|this.ReadByte());
			}
		}
		public void WriteInt16(short val){
			if(val>=0){//val 正の時
				this.WriteByte((byte)(val>>8));
				this.WriteByte((byte)(val&0xff));
			}else{//val 負の時
				val-=System.Int16.MinValue;
				this.WriteByte((byte)((val>>8)+0x80));
				this.WriteByte((byte)(val&0xff));
			}
		}
		public int ReadInt32(){
			int r=0;
			if(this.RestLength<4)throw Exceptions.EOF;
			r=this.ReadByte();
			if(r>127){//負の数
				r-=128;
				for(int i=0;i<3;i++)r=r*256+this.ReadByte();
				r+=System.Int32.MinValue;
			}else{/*正の数*/for(int i=0;i<3;i++)r=r*256+this.ReadByte();}
			return r;
		}
		public void WriteInt32(int val){
			byte[] r=new byte[4];
			if(val>=0){//val 正の時
				for(int i=0;i<4;i++){r[3-i]=(byte)(val%256);val/=256;}
			}else{//val 負の時
				val-=System.Int32.MinValue;
				for(int i=0;i<4;i++){r[3-i]=(byte)(val%256);val/=256;}
				r[0]+=128;
			}
			this.Write(r,0,4);
		}
		public long ReadInt64(){
			if(this.RestLength<4)throw Exceptions.EOF;
			long r=this.ReadByte();
			if(r>127){//負の数
				r-=128;
				for(int i=0;i<7;i++)r=r*256+this.ReadByte();
				r+=System.Int64.MinValue;
			}else{/*正の数*/for(int i=0;i<7;i++)r=r*256+this.ReadByte();}
			return r;
		}
		public void WriteInt64(long val){
			byte[] r=new byte[8];
			if(val>=0){//val 正の時
				for(int i=0;i<8;i++){r[7-i]=(byte)val;val>>=8;}
			}else{//val 負の時
				val-=System.Int64.MinValue;
				for(int i=0;i<8;i++){r[7-i]=(byte)val;val>>=8;}
				r[0]+=128;
			}
			this.Write(r,0,8);
		}
		public ushort ReadUInt16(){
			if(this.RestLength<2)throw Exceptions.EOF;
			return (ushort)(this.ReadByte()<<8|this.ReadByte());
		}
		public void WriteUInt16(ushort val){
			this.WriteByte((byte)(val>>8));
			this.WriteByte((byte)(val&0xff));
		}
		public uint ReadUInt32(){
			if(this.RestLength<4)throw Exceptions.EOF;
			int r=this.ReadByte();
			for(int i=1;i<4;i++)r=r<<8|this.ReadByte();
			return (uint)r;
		}
		public void WriteUInt32(uint val){
			byte[] r=new byte[4];
			for(int i=3;i>0;i--){r[i]=(byte)val;val>>=8;}
			r[0]=(byte)val;
			this.Write(r,0,4);
		}
		public ulong ReadUInt64(){
			if(this.RestLength<8)throw Exceptions.EOF;
			int r=this.ReadByte();
			for(int i=1;i<8;i++)r=r<<8|this.ReadByte();
			return (ulong)r;
		}
		public void WriteUInt64(ulong val){
			byte[] r=new byte[8];
			for(int i=7;i>0;i--){r[i]=(byte)val;val>>=8;}
			r[0]=(byte)val;
			this.Write(r,0,8);
		}
		#endregion

		#region Object 入出力 - 可変長
		public byte[] ReadByteArray(){
			int i=this.ReadInt32();
			if(this.RestLength<i)throw Exceptions.EOF;
			byte[] r=new byte[i];
			this.Read(r,0,i);
			return r;
		}
		public void WriteByteArray(byte[] val){
			this.WriteInt32(val.Length);
			this.Write(val);
		}
		public string ReadString(){
			return System.Text.Encoding.UTF8.GetString(this.ReadByteArray());
		}
		public void WriteString(string val){
			this.WriteByteArray(System.Text.Encoding.UTF8.GetBytes(val));
		}
		#endregion
	}

	public interface RW{
		void ReadFrom(mwg.File.Bytes bin);
		void WriteTo(mwg.File.Bytes bin);
	}
	public class RWDateTime:RW{
		int year;
		int month;
		int day;
		int hour;
		int minute;
		int second;
		int milli;
		public RWDateTime(mwg.File.Bytes bin){this.ReadFrom(bin);}
		public RWDateTime(System.DateTime time){this.Set(time);}
		public RWDateTime(){this.SetNow();}

		#region member of RW
		private static int[] dayOmon={31,29,31,30,31,30,31,31,30,31,30,31};
		public void ReadFrom(Bytes bin){
			if(bin.RestLength<6)throw Exceptions.EOF;
			long dat=bin.ReadByte();
			int i;
			for(i=1;i<6;i++)dat=dat<<8|(byte)bin.ReadByte();
			this.milli=(int)dat%1000;dat/=1000;
			this.second=(int)dat%60;dat/=60;
			this.minute=(int)dat%60;dat/=60;
			this.hour=(int)dat%24;dat/=24;
			int md=(int)dat%366;dat/=366;
			for(i=0;md>RWDateTime.dayOmon[i];i++)md-=RWDateTime.dayOmon[i];
			this.month=i+1;
			this.day=md+1;
			//年
			dat-=4451;if(dat<=0)dat--;
			this.year=(int)dat;
		}
		public void WriteTo(Bytes bin){
			//年
			long dat=this.year+4451;
			if(this.year<0){
				dat++;
				if(dat<0)dat=0;
			}else if(dat>8901)dat=8901;
			//月日
			int month=this.month-1;
			int d=this.day-1;
			for(int i=0;i<month;i++)d+=RWDateTime.dayOmon[i];
			dat=dat*366+d;
			//時分秒
			dat=dat*24+this.hour;
			dat=dat*60+this.minute;
			dat=dat*60+this.second;
			dat=dat*1000+this.milli;
			//書き込み
			byte[] buffer=new byte[6];
			for(int i=5;i>0;i++){buffer[i]=(byte)dat;dat>>=8;}
			buffer[0]=(byte)dat;
			bin.Write(buffer,0,6);
		}
		#endregion

		public void SetNow(){this.Set(System.DateTime.Now);}
		public void Set(System.DateTime time){
			this.year=time.Year;
			this.month=time.Month;
			this.day=time.Day;
			this.hour=time.Hour;
			this.minute=time.Minute;
			this.second=time.Second;
			this.milli=time.Millisecond;
		}
		public static explicit operator System.DateTime(RWDateTime dt){
			return new System.DateTime(dt.year,dt.month,dt.day,dt.hour,dt.minute,dt.second,dt.milli);
		}
		public static explicit operator RWDateTime(System.DateTime dt){
			return new RWDateTime(dt);
		}
	}

	public class Exceptions{
		public static System.Exception EOF;
		public static System.Exception InvalidData;
		public static System.Exception NotSupportedType;
		static Exceptions(){
			EOF=new System.Exception("Stream の最後に達しています。これ以上読み取る事は出来ません");
			InvalidData=new System.Exception("無効なデータです。データの種類が想定している物と異なる可能性があります");
			NotSupportedType=new System.Exception("入出力に対応していない型のデータです。");
		}
	}
}