// Decompiled with JetBrains decompiler
// Type: SevenZip.Compression.RangeCoder.Decoder
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System.IO;

#nullable disable
namespace SevenZip.Compression.RangeCoder
{
  internal class Decoder
  {
    public const uint kTopValue = 16777216;
    public uint Range;
    public uint Code;
    public Stream Stream;

    public void Init(Stream stream)
    {
      this.Stream = stream;
      this.Code = 0U;
      this.Range = uint.MaxValue;
      for (int index = 0; index < 5; ++index)
        this.Code = this.Code << 8 | (uint) (byte) this.Stream.ReadByte();
    }

    public void ReleaseStream() => this.Stream = (Stream) null;

    public void CloseStream() => this.Stream.Close();

    public void Normalize()
    {
      for (; this.Range < 16777216U; this.Range <<= 8)
        this.Code = this.Code << 8 | (uint) (byte) this.Stream.ReadByte();
    }

    public void Normalize2()
    {
      if (this.Range >= 16777216U)
        return;
      this.Code = this.Code << 8 | (uint) (byte) this.Stream.ReadByte();
      this.Range <<= 8;
    }

    public uint GetThreshold(uint total) => this.Code / (this.Range /= total);

    public void Decode(uint start, uint size, uint total)
    {
      this.Code -= start * this.Range;
      this.Range *= size;
      this.Normalize();
    }

    public uint DecodeDirectBits(int numTotalBits)
    {
      uint range = this.Range;
      uint num1 = this.Code;
      uint num2 = 0;
      for (int index = numTotalBits; index > 0; --index)
      {
        range >>= 1;
        uint num3 = num1 - range >> 31;
        num1 -= range & num3 - 1U;
        num2 = (uint) ((int) num2 << 1 | 1 - (int) num3);
        if (range < 16777216U)
        {
          num1 = num1 << 8 | (uint) (byte) this.Stream.ReadByte();
          range <<= 8;
        }
      }
      this.Range = range;
      this.Code = num1;
      return num2;
    }

    public uint DecodeBit(uint size0, int numTotalBits)
    {
      uint num1 = (this.Range >> numTotalBits) * size0;
      uint num2;
      if (this.Code < num1)
      {
        num2 = 0U;
        this.Range = num1;
      }
      else
      {
        num2 = 1U;
        this.Code -= num1;
        this.Range -= num1;
      }
      this.Normalize();
      return num2;
    }
  }
}
