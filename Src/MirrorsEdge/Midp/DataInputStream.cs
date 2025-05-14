// Decompiled with JetBrains decompiler
// Type: midp.DataInputStream
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System.IO;
using System.Text;

#nullable disable
namespace midp
{
  public class DataInputStream : InputStream
  {
    private readonly InputStream m_inputStream;
    private bool m_eofExceptionThrown;
    private static byte[] readingFloat = new byte[4];
    private static MemoryStream m_MemoryStream = new MemoryStream(DataInputStream.readingFloat, 0, 4);
    private static BinaryReader m_Reader = new BinaryReader((Stream) DataInputStream.m_MemoryStream);
    private static byte[] readingInt = new byte[4];
    private static byte[] readingLong = new byte[8];
    private static byte[] readingShort = new byte[2];
    private static byte[] utf8Bytes = new byte[1000];
    private static Encoding enc = (Encoding) new UTF8Encoding(true, true);

    public override meClass getClass() => (meClass) new DataInputStreamClass();

    public bool eofExceptionThrown() => this.m_eofExceptionThrown;

    public DataInputStream(InputStream @in)
    {
      this.m_inputStream = @in;
      this.m_eofExceptionThrown = false;
    }

    public override void Destructor() => base.Destructor();

    public override void close() => this.m_inputStream.close();

    public bool readBoolean() => this.readByte() != (sbyte) 0;

    public sbyte readByte()
    {
      int num = this.read();
      if (num == -1)
        this.m_eofExceptionThrown = true;
      return (sbyte) (num & (int) byte.MaxValue);
    }

    public short readChar() => this.readShort();

    public float readFloat()
    {
      this.readFully(ref DataInputStream.readingFloat, 0, 4);
      byte num1 = DataInputStream.readingFloat[0];
      DataInputStream.readingFloat[0] = DataInputStream.readingFloat[3];
      DataInputStream.readingFloat[3] = num1;
      byte num2 = DataInputStream.readingFloat[1];
      DataInputStream.readingFloat[1] = DataInputStream.readingFloat[2];
      DataInputStream.readingFloat[2] = num2;
      DataInputStream.m_MemoryStream.Position = 0L;
      return DataInputStream.m_Reader.ReadSingle();
    }

    public void readFully(ref byte[] b) => this.readFully(ref b, 0, b.Length);

    public void readFully(ref byte[] b, int off, int len)
    {
      this.verifyStream();
      int off1 = off;
      int num;
      for (int index = off + len; off1 < index; off1 += num)
      {
        num = this.m_inputStream.read(ref b, off1, index - off1);
        if (num <= 0)
        {
          this.m_eofExceptionThrown = true;
          break;
        }
      }
    }

    public int readInt()
    {
      this.readFully(ref DataInputStream.readingInt, 0, 4);
      return (int) DataInputStream.readingInt[3] & (int) byte.MaxValue | ((int) DataInputStream.readingInt[2] & (int) byte.MaxValue) << 8 | ((int) DataInputStream.readingInt[1] & (int) byte.MaxValue) << 16 | ((int) DataInputStream.readingInt[0] & (int) byte.MaxValue) << 24;
    }

    public long readLong()
    {
      this.readFully(ref DataInputStream.readingLong, 0, 8);
      return (long) DataInputStream.readingLong[7] & (long) byte.MaxValue | ((long) DataInputStream.readingLong[6] & (long) byte.MaxValue) << 8 | ((long) DataInputStream.readingLong[5] & (long) byte.MaxValue) << 16 | ((long) DataInputStream.readingLong[4] & (long) byte.MaxValue) << 24 | ((long) DataInputStream.readingLong[3] & (long) byte.MaxValue) << 32 | ((long) DataInputStream.readingLong[2] & (long) byte.MaxValue) << 40 | ((long) DataInputStream.readingLong[1] & (long) byte.MaxValue) << 48 | ((long) DataInputStream.readingLong[0] & (long) byte.MaxValue) << 56;
    }

    public short readShort()
    {
      this.readFully(ref DataInputStream.readingShort, 0, 2);
      return (short) ((int) DataInputStream.readingShort[1] & (int) byte.MaxValue | ((int) DataInputStream.readingShort[0] & (int) byte.MaxValue) << 8);
    }

    public int readUnsignedByte() => (int) this.readByte() & (int) byte.MaxValue;

    public int readUnsignedShort() => (int) this.readShort() & (int) ushort.MaxValue;

    public string readUTF()
    {
      if (this.m_inputStream.available() < 2)
        return "";
      int length = this.readUnsignedShort();
      if (length == 0)
        return "";
      int n = this.m_inputStream.available();
      if (n < length)
      {
        this.m_inputStream.skip(n);
        return "";
      }
      if (DataInputStream.utf8Bytes.Length < length)
        DataInputStream.utf8Bytes = new byte[length];
      this.readFully(ref DataInputStream.utf8Bytes, 0, length);
      return DataInputStream.enc.GetString(DataInputStream.utf8Bytes, 0, length);
    }

    private void verifyStream()
    {
    }

    public override int available()
    {
      this.verifyStream();
      return this.m_inputStream.available();
    }

    public override void mark(int readlimit)
    {
      this.verifyStream();
      this.m_inputStream.mark(readlimit);
    }

    public override bool markSupported()
    {
      this.verifyStream();
      return this.m_inputStream.markSupported();
    }

    public override int read()
    {
      this.verifyStream();
      return this.m_inputStream.read();
    }

    public override int read(ref byte[] b, int len)
    {
      this.verifyStream();
      return this.m_inputStream.read(ref b, 0, len);
    }

    public override void reset()
    {
      this.verifyStream();
      this.m_inputStream.reset();
    }

    public override int skip(int n)
    {
      this.verifyStream();
      return this.m_inputStream.skip(n);
    }

    public override int getPosition() => this.m_inputStream.getPosition();

    public override bool seek(int offset, int from) => this.m_inputStream.seek(offset, from);

    public override int size() => this.m_inputStream.size();

    public override Stream getWP7Stream() => this.m_inputStream.getWP7Stream();
  }
}
