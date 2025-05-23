﻿
// Type: SevenZip.Compression.RangeCoder.Encoder
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.IO;

#nullable disable
namespace SevenZip.Compression.RangeCoder
{
  internal class Encoder
  {
    public const uint kTopValue = 16777216;
    private Stream Stream;
    public ulong Low;
    public uint Range;
    private uint _cacheSize;
    private byte _cache;
    private long StartPosition;

    public void SetStream(Stream stream) => this.Stream = stream;

    public void ReleaseStream() => this.Stream = (Stream) null;

    public void Init()
    {
      this.StartPosition = this.Stream.Position;
      this.Low = 0UL;
      this.Range = uint.MaxValue;
      this._cacheSize = 1U;
      this._cache = (byte) 0;
    }

    public void FlushData()
    {
      for (int index = 0; index < 5; ++index)
        this.ShiftLow();
    }

    public void FlushStream() => this.Stream.Flush();

    public void CloseStream() => this.Stream.Dispose();

    public void Encode(uint start, uint size, uint total)
    {
      this.Low += (ulong) (start * (this.Range /= total));
      this.Range *= size;
      while (this.Range < 16777216U)
      {
        this.Range <<= 8;
        this.ShiftLow();
      }
    }

    public void ShiftLow()
    {
      if ((uint) this.Low < 4278190080U || (uint) (this.Low >> 32) == 1U)
      {
        byte num = this._cache;
        do
        {
          this.Stream.WriteByte((byte) ((ulong) num + (this.Low >> 32)));
          num = byte.MaxValue;
        }
        while (--this._cacheSize != 0U);
        this._cache = (byte) ((uint) this.Low >> 24);
      }
      ++this._cacheSize;
      this.Low = (ulong) ((uint) this.Low << 8);
    }

    public void EncodeDirectBits(uint v, int numTotalBits)
    {
      for (int index = numTotalBits - 1; index >= 0; --index)
      {
        this.Range >>= 1;
        if (((int) (v >> index) & 1) == 1)
          this.Low += (ulong) this.Range;
        if (this.Range < 16777216U)
        {
          this.Range <<= 8;
          this.ShiftLow();
        }
      }
    }

    public void EncodeBit(uint size0, int numTotalBits, uint symbol)
    {
      uint num = (this.Range >> numTotalBits) * size0;
      if (symbol == 0U)
      {
        this.Range = num;
      }
      else
      {
        this.Low += (ulong) num;
        this.Range -= num;
      }
      while (this.Range < 16777216U)
      {
        this.Range <<= 8;
        this.ShiftLow();
      }
    }

    public long GetProcessedSizeAdd()
    {
      return (long) this._cacheSize + this.Stream.Position - this.StartPosition + 4L;
    }
  }
}
