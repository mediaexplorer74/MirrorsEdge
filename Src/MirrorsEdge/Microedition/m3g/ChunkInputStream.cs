// Decompiled with JetBrains decompiler
// Type: microedition.m3g.ChunkInputStream
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;
using System.IO;

#nullable disable
namespace microedition.m3g
{
  internal class ChunkInputStream
  {
    private BinaryReader m_Stream;
    private int m_Length;
    private int m_Pos;
    private int m_StreamPos;

    public ChunkInputStream(BinaryReader stream, int length)
    {
      this.m_Stream = stream;
      this.m_Length = length;
      this.m_Pos = 0;
      this.m_StreamPos = (int) this.m_Stream.BaseStream.Position;
    }

    public int available()
    {
      this.m_Pos = (int) this.m_Stream.BaseStream.Position - this.m_StreamPos;
      return this.m_Length - this.m_Pos;
    }

    public int read()
    {
      this.m_Pos = (int) this.m_Stream.BaseStream.Position - this.m_StreamPos;
      if (this.m_Pos >= this.m_Length)
        return -1;
      ++this.m_Pos;
      return (int) this.m_Stream.ReadByte();
    }

    public int read(byte[] b, int len)
    {
      this.m_Pos = (int) this.m_Stream.BaseStream.Position - this.m_StreamPos;
      if (this.m_Pos >= this.m_Length)
        return -1;
      int count = Math.Min(len, this.m_Length - this.m_Pos);
      int num = this.m_Stream.Read(b, 0, count);
      if (num > 0)
        this.m_Pos += num;
      return num;
    }

    public long skip(long n)
    {
      this.m_Pos = (int) this.m_Stream.BaseStream.Position - this.m_StreamPos;
      int offset = Math.Min((int) n, this.m_Length - this.m_Pos);
      this.m_Stream.BaseStream.Seek((long) offset, SeekOrigin.Current);
      this.m_Pos += offset;
      return (long) offset;
    }

    public static implicit operator BinaryReader(ChunkInputStream s) => s.m_Stream;
  }
}
