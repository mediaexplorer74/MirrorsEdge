
// Type: midp.DataOutputStream
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.IO;
using System.Text;

#nullable disable
namespace midp
{
  public class DataOutputStream : OutputStream
  {
    private OutputStream m_out;
    private static byte[] writingFloat = new byte[4];
    private static byte[] writingFloatInverted = new byte[4];
    private static MemoryStream m_MemoryStream = new MemoryStream(DataOutputStream.writingFloat, 0, 4);
    private static BinaryWriter m_Writer = new BinaryWriter((Stream) DataOutputStream.m_MemoryStream);
    private static byte[] writingInt = new byte[4];
    private static byte[] writingLong = new byte[8];
    private static byte[] writingShort = new byte[2];
    private static Encoding enc = (Encoding) new UTF8Encoding(true, true);

    public override meClass getClass() => (meClass) new DataOutputStreamClass();

    public DataOutputStream(OutputStream @out) => this.m_out = @out;

    public override void Destructor()
    {
      this.close();
      base.Destructor();
    }

    public override bool close()
    {
      if (this.m_out == null)
        return false;
      bool flag = this.m_out.close();
      this.m_out = (OutputStream) null;
      return flag;
    }

    public override void write(byte b) => this.m_out.write(b);

    public override void write(sbyte[] b) => this.m_out.write(b);

    public override void write(sbyte[] b, int off, int len) => this.m_out.write(b, off, len);

    public override void write(byte[] b, int len) => this.m_out.write(b, len);

    public void writeBoolean(bool b) => this.write(b ? (byte) 1 : (byte) 0);

    public void writeByte(byte c) => this.write(c);

    public void writeChar(short c) => this.writeShort(c);

    public void writeFloat(float f)
    {
      DataOutputStream.m_MemoryStream.Position = 0L;
      DataOutputStream.m_Writer.Write(f);
      DataOutputStream.writingFloatInverted[0] = DataOutputStream.writingFloat[3];
      DataOutputStream.writingFloatInverted[1] = DataOutputStream.writingFloat[2];
      DataOutputStream.writingFloatInverted[2] = DataOutputStream.writingFloat[1];
      DataOutputStream.writingFloatInverted[3] = DataOutputStream.writingFloat[0];
      this.write(DataOutputStream.writingFloatInverted, 4);
    }

    public void writeInt(int t)
    {
      DataOutputStream.writingInt[3] = (byte) (t & (int) byte.MaxValue);
      DataOutputStream.writingInt[2] = (byte) (t >> 8 & (int) byte.MaxValue);
      DataOutputStream.writingInt[1] = (byte) (t >> 16 & (int) byte.MaxValue);
      DataOutputStream.writingInt[0] = (byte) (t >> 24 & (int) byte.MaxValue);
      this.write(DataOutputStream.writingInt, 4);
    }

    public void writeLong(long t)
    {
      DataOutputStream.writingLong[7] = (byte) ((ulong) t & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[6] = (byte) ((ulong) (t >> 8) & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[5] = (byte) ((ulong) (t >> 16) & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[4] = (byte) ((ulong) (t >> 24) & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[3] = (byte) ((ulong) (t >> 32) & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[2] = (byte) ((ulong) (t >> 40) & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[1] = (byte) ((ulong) (t >> 48) & (ulong) byte.MaxValue);
      DataOutputStream.writingLong[0] = (byte) ((ulong) (t >> 56) & (ulong) byte.MaxValue);
      this.write(DataOutputStream.writingLong, 8);
    }

    public void writeShort(short t)
    {
      DataOutputStream.writingShort[1] = (byte) ((uint) t & (uint) byte.MaxValue);
      DataOutputStream.writingShort[0] = (byte) ((int) t >> 8 & (int) byte.MaxValue);
      this.write(DataOutputStream.writingShort, 2);
    }

    public void writeUnsignedByte(byte c) => this.write(c);

    public void writeUnsignedShort(ushort t)
    {
      DataOutputStream.writingShort[1] = (byte) ((uint) t & (uint) byte.MaxValue);
      DataOutputStream.writingShort[0] = (byte) ((int) t >> 8 & (int) byte.MaxValue);
      this.write(DataOutputStream.writingShort, 2);
    }

    public void writeUnsignedInt(uint t)
    {
      DataOutputStream.writingInt[3] = (byte) (t & (uint) byte.MaxValue);
      DataOutputStream.writingInt[2] = (byte) (t >> 8 & (uint) byte.MaxValue);
      DataOutputStream.writingInt[1] = (byte) (t >> 16 & (uint) byte.MaxValue);
      DataOutputStream.writingInt[0] = (byte) (t >> 24 & (uint) byte.MaxValue);
      this.write(DataOutputStream.writingInt, 4);
    }

    public void writeUTF(string str)
    {
      if (str != null)
      {
        byte[] bytes = DataOutputStream.enc.GetBytes(str);
        int length = bytes.Length;
        this.writeUnsignedShort((ushort) length);
        if (length <= 0)
          return;
        this.write(bytes, length);
      }
      else
        this.writeUnsignedShort((ushort) 0);
    }
  }
}
