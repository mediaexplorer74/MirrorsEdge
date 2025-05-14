// Decompiled with JetBrains decompiler
// Type: midp.ByteArrayInputStream
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;
using System.IO;

#nullable disable
namespace midp
{
  public class ByteArrayInputStream : InputStream
  {
    protected byte[] m_buf;
    private int m_offset;
    protected int m_count;
    protected int m_mark;
    protected int m_pos;

    public override meClass getClass() => (meClass) new ByteArrayInputStreamClass();

    public ByteArrayInputStream(byte[] buf)
    {
      this.m_buf = buf;
      this.m_offset = 0;
      this.m_count = buf.Length;
      this.m_mark = 0;
      this.m_pos = 0;
    }

    public ByteArrayInputStream(byte[] buf, int bufLength)
    {
      this.m_buf = new byte[bufLength];
      Array.Copy((Array) buf, (Array) this.m_buf, bufLength);
      this.m_offset = 0;
      this.m_count = bufLength;
      this.m_mark = 0;
      this.m_pos = 0;
    }

    public ByteArrayInputStream(byte[] buf, int offset, int length)
    {
      this.m_buf = buf;
      this.m_offset = offset;
      this.m_count = length;
      this.m_mark = 0;
      this.m_pos = 0;
    }

    public override void Destructor()
    {
      this.m_buf = (byte[]) null;
      base.Destructor();
    }

    public override int available() => this.m_count - this.m_pos;

    public override void mark(int readAheadLimit) => this.m_mark = this.m_pos;

    public override bool markSupported() => true;

    public override int read()
    {
      return this.m_pos == this.m_count ? -1 : (int) this.m_buf[this.m_offset + this.m_pos++] & (int) byte.MaxValue;
    }

    public override int read(ref sbyte[] b, int len)
    {
      int length = len;
      if (length > this.m_count - this.m_pos)
        length = this.m_count - this.m_pos;
      Array.Copy((Array) this.m_buf, this.m_offset + this.m_pos, (Array) b, 0, length);
      this.m_pos += length;
      return length;
    }

    public override void reset() => this.m_pos = this.m_mark;

    public override int skip(int n)
    {
      int num = n;
      if (num > this.m_count - this.m_pos)
        num = this.m_count - this.m_pos;
      this.m_pos += num;
      return num;
    }

    public override int getPosition() => this.m_pos;

    public override bool seek(int offset, int from)
    {
      int num;
      switch (from)
      {
        case 0:
          num = offset;
          break;
        case 1:
          num = this.m_pos + offset;
          break;
        case 2:
          num = this.m_count + offset;
          break;
        default:
          return false;
      }
      bool flag = true;
      if (num < 0)
      {
        num = 0;
        flag = false;
      }
      if (num > this.m_count)
      {
        num = this.m_count;
        flag = false;
      }
      this.m_pos = num;
      return flag;
    }

    public override int size() => this.m_count;

    public override Stream getWP7Stream() => (Stream) null;
  }
}
