// Decompiled with JetBrains decompiler
// Type: SevenZip.Compression.RangeCoder.BitTreeDecoder
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace SevenZip.Compression.RangeCoder
{
  internal struct BitTreeDecoder(int numBitLevels)
  {
    private BitDecoder[] Models = new BitDecoder[1 << numBitLevels];
    private int NumBitLevels = numBitLevels;

    public void Init()
    {
      for (uint index = 1; (long) index < (long) (1 << this.NumBitLevels); ++index)
        this.Models[(int) index].Init();
    }

    public uint Decode(Decoder rangeDecoder)
    {
      uint index = 1;
      for (int numBitLevels = this.NumBitLevels; numBitLevels > 0; --numBitLevels)
        index = (index << 1) + this.Models[(int) index].Decode(rangeDecoder);
      return index - (uint) (1 << this.NumBitLevels);
    }

    public uint ReverseDecode(Decoder rangeDecoder)
    {
      uint index1 = 1;
      uint num1 = 0;
      for (int index2 = 0; index2 < this.NumBitLevels; ++index2)
      {
        uint num2 = this.Models[(int) index1].Decode(rangeDecoder);
        index1 = (index1 << 1) + num2;
        num1 |= num2 << index2;
      }
      return num1;
    }

    public static uint ReverseDecode(
      BitDecoder[] Models,
      uint startIndex,
      Decoder rangeDecoder,
      int NumBitLevels)
    {
      uint num1 = 1;
      uint num2 = 0;
      for (int index = 0; index < NumBitLevels; ++index)
      {
        uint num3 = Models[(int) (startIndex + num1)].Decode(rangeDecoder);
        num1 = (num1 << 1) + num3;
        num2 |= num3 << index;
      }
      return num2;
    }
  }
}
