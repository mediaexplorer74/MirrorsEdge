
// Type: SevenZip.Compression.RangeCoder.BitTreeEncoder
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace SevenZip.Compression.RangeCoder
{
  internal struct BitTreeEncoder(int numBitLevels)
  {
    private BitEncoder[] Models = new BitEncoder[1 << numBitLevels];
    private int NumBitLevels = numBitLevels;

    public void Init()
    {
      for (uint index = 1; (long) index < (long) (1 << this.NumBitLevels); ++index)
        this.Models[(int) index].Init();
    }

    public void Encode(Encoder rangeEncoder, uint symbol)
    {
      uint index = 1;
      int numBitLevels = this.NumBitLevels;
      while (numBitLevels > 0)
      {
        --numBitLevels;
        uint symbol1 = symbol >> numBitLevels & 1U;
        this.Models[(int) index].Encode(rangeEncoder, symbol1);
        index = index << 1 | symbol1;
      }
    }

    public void ReverseEncode(Encoder rangeEncoder, uint symbol)
    {
      uint index1 = 1;
      for (uint index2 = 0; (long) index2 < (long) this.NumBitLevels; ++index2)
      {
        uint symbol1 = symbol & 1U;
        this.Models[(int) index1].Encode(rangeEncoder, symbol1);
        index1 = index1 << 1 | symbol1;
        symbol >>= 1;
      }
    }

    public uint GetPrice(uint symbol)
    {
      uint price = 0;
      uint index = 1;
      int numBitLevels = this.NumBitLevels;
      while (numBitLevels > 0)
      {
        --numBitLevels;
        uint symbol1 = symbol >> numBitLevels & 1U;
        price += this.Models[(int) index].GetPrice(symbol1);
        index = (index << 1) + symbol1;
      }
      return price;
    }

    public uint ReverseGetPrice(uint symbol)
    {
      uint price = 0;
      uint index = 1;
      for (int numBitLevels = this.NumBitLevels; numBitLevels > 0; --numBitLevels)
      {
        uint symbol1 = symbol & 1U;
        symbol >>= 1;
        price += this.Models[(int) index].GetPrice(symbol1);
        index = index << 1 | symbol1;
      }
      return price;
    }

    public static uint ReverseGetPrice(
      BitEncoder[] Models,
      uint startIndex,
      int NumBitLevels,
      uint symbol)
    {
      uint price = 0;
      uint num = 1;
      for (int index = NumBitLevels; index > 0; --index)
      {
        uint symbol1 = symbol & 1U;
        symbol >>= 1;
        price += Models[(int) (startIndex + num)].GetPrice(symbol1);
        num = num << 1 | symbol1;
      }
      return price;
    }

    public static void ReverseEncode(
      BitEncoder[] Models,
      uint startIndex,
      Encoder rangeEncoder,
      int NumBitLevels,
      uint symbol)
    {
      uint num = 1;
      for (int index = 0; index < NumBitLevels; ++index)
      {
        uint symbol1 = symbol & 1U;
        Models[(int) (startIndex + num)].Encode(rangeEncoder, symbol1);
        num = num << 1 | symbol1;
        symbol >>= 1;
      }
    }
  }
}
