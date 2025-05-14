
// Type: midp.ByteArrayOutputStream
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace midp
{
  public class ByteArrayOutputStream : OutputStream
  {
    public const int DEFAULT_INITIAL_SIZE = 32;
    private sbyte[] m_buffer;
    private int m_count;

    public override meClass getClass() => (meClass) new ByteArrayOutputStreamClass();

    public ByteArrayOutputStream()
    {
      this.m_buffer = new sbyte[32];
      this.m_count = 0;
    }

    public ByteArrayOutputStream(int initialSize)
    {
      this.m_buffer = new sbyte[initialSize];
      this.m_count = 0;
    }

    public override void Destructor()
    {
      this.m_buffer = (sbyte[]) null;
      base.Destructor();
    }

    public override void write(byte writeByte)
    {
      if (this.m_count == this.m_buffer.Length)
        this.ensureCapacity(this.m_buffer.Length << 1);
      this.m_buffer[this.m_count++] = (sbyte) writeByte;
    }

    public override void write(sbyte[] b, int len)
    {
      this.ensureCapacity(this.m_count + len);
      Array.Copy((Array) b, 0, (Array) this.m_buffer, this.m_count, len);
      this.m_count += len;
    }

    public override bool close() => true;

    public void writeTo(OutputStream outStream) => outStream.write(this.m_buffer, 0, this.m_count);

    private void ensureCapacity(int newSize)
    {
      int length = this.m_buffer.Length;
      if (newSize <= length)
        return;
      Array.Resize<sbyte>(ref this.m_buffer, newSize);
    }

    public void reset() => this.m_count = 0;

    public int size() => this.m_count;

    public sbyte[] toByteArray()
    {
      sbyte[] destinationArray = new sbyte[this.m_count];
      Array.Copy((Array) this.m_buffer, (Array) destinationArray, this.m_count);
      return destinationArray;
    }
  }
}
