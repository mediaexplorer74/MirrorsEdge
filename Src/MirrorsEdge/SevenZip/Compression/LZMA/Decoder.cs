﻿
// Type: SevenZip.Compression.LZMA.Decoder
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using SevenZip.Compression.LZ;
using SevenZip.Compression.RangeCoder;
using System;
using System.IO;

#nullable disable
namespace SevenZip.Compression.LZMA
{
  public class Decoder : ICoder, ISetDecoderProperties
  {
    private OutWindow m_OutWindow = new OutWindow();
    private SevenZip.Compression.RangeCoder.Decoder m_RangeDecoder = new SevenZip.Compression.RangeCoder.Decoder();
    private BitDecoder[] m_IsMatchDecoders = new BitDecoder[192];
    private BitDecoder[] m_IsRepDecoders = new BitDecoder[12];
    private BitDecoder[] m_IsRepG0Decoders = new BitDecoder[12];
    private BitDecoder[] m_IsRepG1Decoders = new BitDecoder[12];
    private BitDecoder[] m_IsRepG2Decoders = new BitDecoder[12];
    private BitDecoder[] m_IsRep0LongDecoders = new BitDecoder[192];
    private BitTreeDecoder[] m_PosSlotDecoder = new BitTreeDecoder[4];
    private BitDecoder[] m_PosDecoders = new BitDecoder[114];
    private BitTreeDecoder m_PosAlignDecoder = new BitTreeDecoder(4);
    private Decoder.LenDecoder m_LenDecoder = new Decoder.LenDecoder();
    private Decoder.LenDecoder m_RepLenDecoder = new Decoder.LenDecoder();
    private Decoder.LiteralDecoder m_LiteralDecoder = new Decoder.LiteralDecoder();
    private uint m_DictionarySize;
    private uint m_DictionarySizeCheck;
    private uint m_PosStateMask;
    private bool _solid;

    public Decoder()
    {
      this.m_DictionarySize = uint.MaxValue;
      for (int index = 0; index < 4; ++index)
        this.m_PosSlotDecoder[index] = new BitTreeDecoder(6);
    }

    private void SetDictionarySize(uint dictionarySize)
    {
      if ((int) this.m_DictionarySize == (int) dictionarySize)
        return;
      this.m_DictionarySize = dictionarySize;
      this.m_DictionarySizeCheck = Math.Max(this.m_DictionarySize, 1U);
      this.m_OutWindow.Create(Math.Max(this.m_DictionarySizeCheck, 4096U));
    }

    private void SetLiteralProperties(int lp, int lc)
    {
      if (lp > 8)
        throw new InvalidParamException();
      if (lc > 8)
        throw new InvalidParamException();
      this.m_LiteralDecoder.Create(lp, lc);
    }

    private void SetPosBitsProperties(int pb)
    {
      if (pb > 4)
        throw new InvalidParamException();
      uint numPosStates = (uint) (1 << pb);
      this.m_LenDecoder.Create(numPosStates);
      this.m_RepLenDecoder.Create(numPosStates);
      this.m_PosStateMask = numPosStates - 1U;
    }

    private void Init(Stream inStream, Stream outStream)
    {
      this.m_RangeDecoder.Init(inStream);
      this.m_OutWindow.Init(outStream, this._solid);
      for (uint index1 = 0; index1 < 12U; ++index1)
      {
        for (uint index2 = 0; index2 <= this.m_PosStateMask; ++index2)
        {
          uint index3 = (index1 << 4) + index2;
          this.m_IsMatchDecoders[(int) index3].Init();
          this.m_IsRep0LongDecoders[(int) index3].Init();
        }
        this.m_IsRepDecoders[(int) index1].Init();
        this.m_IsRepG0Decoders[(int) index1].Init();
        this.m_IsRepG1Decoders[(int) index1].Init();
        this.m_IsRepG2Decoders[(int) index1].Init();
      }
      this.m_LiteralDecoder.Init();
      for (uint index = 0; index < 4U; ++index)
        this.m_PosSlotDecoder[(int) index].Init();
      for (uint index = 0; index < 114U; ++index)
        this.m_PosDecoders[(int) index].Init();
      this.m_LenDecoder.Init();
      this.m_RepLenDecoder.Init();
      this.m_PosAlignDecoder.Init();
    }

    public void Code(
      Stream inStream,
      Stream outStream,
      long inSize,
      long outSize,
      ICodeProgress progress)
    {
      this.Init(inStream, outStream);
      Base.State state = new Base.State();
      state.Init();
      uint distance = 0;
      uint num1 = 0;
      uint num2 = 0;
      uint num3 = 0;
      ulong pos = 0;
      ulong num4 = (ulong) outSize;
      if (pos < num4)
      {
        if (this.m_IsMatchDecoders[(int) (state.Index << 4)].Decode(this.m_RangeDecoder) != 0U)
          throw new DataErrorException();
        state.UpdateChar();
        this.m_OutWindow.PutByte(this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, 0U, (byte) 0));
        ++pos;
      }
      while (pos < num4)
      {
        uint posState = (uint) pos & this.m_PosStateMask;
        if (this.m_IsMatchDecoders[(int) ((state.Index << 4) + posState)].Decode(this.m_RangeDecoder) == 0U)
        {
          byte prevByte = this.m_OutWindow.GetByte(0U);
          this.m_OutWindow.PutByte(state.IsCharState() ? this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, (uint) pos, prevByte) : this.m_LiteralDecoder.DecodeWithMatchByte(this.m_RangeDecoder, (uint) pos, prevByte, this.m_OutWindow.GetByte(distance)));
          state.UpdateChar();
          ++pos;
        }
        else
        {
          uint len;
          if (this.m_IsRepDecoders[(int) state.Index].Decode(this.m_RangeDecoder) == 1U)
          {
            if (this.m_IsRepG0Decoders[(int) state.Index].Decode(this.m_RangeDecoder) == 0U)
            {
              if (this.m_IsRep0LongDecoders[(int) ((state.Index << 4) + posState)].Decode(this.m_RangeDecoder) == 0U)
              {
                state.UpdateShortRep();
                this.m_OutWindow.PutByte(this.m_OutWindow.GetByte(distance));
                ++pos;
                continue;
              }
            }
            else
            {
              uint num5;
              if (this.m_IsRepG1Decoders[(int) state.Index].Decode(this.m_RangeDecoder) == 0U)
              {
                num5 = num1;
              }
              else
              {
                if (this.m_IsRepG2Decoders[(int) state.Index].Decode(this.m_RangeDecoder) == 0U)
                {
                  num5 = num2;
                }
                else
                {
                  num5 = num3;
                  num3 = num2;
                }
                num2 = num1;
              }
              num1 = distance;
              distance = num5;
            }
            len = this.m_RepLenDecoder.Decode(this.m_RangeDecoder, posState) + 2U;
            state.UpdateRep();
          }
          else
          {
            num3 = num2;
            num2 = num1;
            num1 = distance;
            len = 2U + this.m_LenDecoder.Decode(this.m_RangeDecoder, posState);
            state.UpdateMatch();
            uint num6 = this.m_PosSlotDecoder[(int) Base.GetLenToPosState(len)].Decode(this.m_RangeDecoder);
            if (num6 >= 4U)
            {
              int NumBitLevels = (int) (num6 >> 1) - 1;
              uint num7 = (uint) ((2 | (int) num6 & 1) << NumBitLevels);
              distance = num6 >= 14U ? num7 + (this.m_RangeDecoder.DecodeDirectBits(NumBitLevels - 4) << 4) + this.m_PosAlignDecoder.ReverseDecode(this.m_RangeDecoder) : num7 + BitTreeDecoder.ReverseDecode(this.m_PosDecoders, (uint) ((int) num7 - (int) num6 - 1), this.m_RangeDecoder, NumBitLevels);
            }
            else
              distance = num6;
          }
          if ((ulong) distance >= (ulong) this.m_OutWindow.TrainSize + pos || distance >= this.m_DictionarySizeCheck)
          {
            if (distance != uint.MaxValue)
              throw new DataErrorException();
            break;
          }
          this.m_OutWindow.CopyBlock(distance, len);
          pos += (ulong) len;
        }
      }
      this.m_OutWindow.Flush();
      this.m_OutWindow.ReleaseStream();
      this.m_RangeDecoder.ReleaseStream();
    }

    public void SetDecoderProperties(byte[] properties)
    {
      if (properties.Length < 5)
        throw new InvalidParamException();
      int lc = (int) properties[0] % 9;
      int num = (int) properties[0] / 9;
      int lp = num % 5;
      int pb = num / 5;
      if (pb > 4)
        throw new InvalidParamException();
      uint dictionarySize = 0;
      for (int index = 0; index < 4; ++index)
        dictionarySize += (uint) properties[1 + index] << index * 8;
      this.SetDictionarySize(dictionarySize);
      this.SetLiteralProperties(lp, lc);
      this.SetPosBitsProperties(pb);
    }

    public bool Train(Stream stream)
    {
      this._solid = true;
      return this.m_OutWindow.Train(stream);
    }

    private class LenDecoder
    {
      private BitDecoder m_Choice = new BitDecoder();
      private BitDecoder m_Choice2 = new BitDecoder();
      private BitTreeDecoder[] m_LowCoder = new BitTreeDecoder[16];
      private BitTreeDecoder[] m_MidCoder = new BitTreeDecoder[16];
      private BitTreeDecoder m_HighCoder = new BitTreeDecoder(8);
      private uint m_NumPosStates;

      public void Create(uint numPosStates)
      {
        for (uint numPosStates1 = this.m_NumPosStates; numPosStates1 < numPosStates; ++numPosStates1)
        {
          this.m_LowCoder[(int) numPosStates1] = new BitTreeDecoder(3);
          this.m_MidCoder[(int) numPosStates1] = new BitTreeDecoder(3);
        }
        this.m_NumPosStates = numPosStates;
      }

      public void Init()
      {
        this.m_Choice.Init();
        for (uint index = 0; index < this.m_NumPosStates; ++index)
        {
          this.m_LowCoder[(int) index].Init();
          this.m_MidCoder[(int) index].Init();
        }
        this.m_Choice2.Init();
        this.m_HighCoder.Init();
      }

      public uint Decode(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, uint posState)
      {
        if (this.m_Choice.Decode(rangeDecoder) == 0U)
          return this.m_LowCoder[(int) posState].Decode(rangeDecoder);
        uint num = 8;
        return this.m_Choice2.Decode(rangeDecoder) != 0U ? num + 8U + this.m_HighCoder.Decode(rangeDecoder) : num + this.m_MidCoder[(int) posState].Decode(rangeDecoder);
      }
    }

    private class LiteralDecoder
    {
      private Decoder.LiteralDecoder.Decoder2[] m_Coders;
      private int m_NumPrevBits;
      private int m_NumPosBits;
      private uint m_PosMask;

      public void Create(int numPosBits, int numPrevBits)
      {
        if (this.m_Coders != null && this.m_NumPrevBits == numPrevBits && this.m_NumPosBits == numPosBits)
          return;
        this.m_NumPosBits = numPosBits;
        this.m_PosMask = (uint) ((1 << numPosBits) - 1);
        this.m_NumPrevBits = numPrevBits;
        uint length = (uint) (1 << this.m_NumPrevBits + this.m_NumPosBits);
        this.m_Coders = new Decoder.LiteralDecoder.Decoder2[(int) length];
        for (uint index = 0; index < length; ++index)
          this.m_Coders[(int) index].Create();
      }

      public void Init()
      {
        uint num = (uint) (1 << this.m_NumPrevBits + this.m_NumPosBits);
        for (uint index = 0; index < num; ++index)
          this.m_Coders[(int) index].Init();
      }

      private uint GetState(uint pos, byte prevByte)
      {
        return (uint) ((((int) pos & (int) this.m_PosMask) << this.m_NumPrevBits) + ((int) prevByte >> 8 - this.m_NumPrevBits));
      }

      public byte DecodeNormal(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, uint pos, byte prevByte)
      {
        return this.m_Coders[(int) this.GetState(pos, prevByte)].DecodeNormal(rangeDecoder);
      }

      public byte DecodeWithMatchByte(
        SevenZip.Compression.RangeCoder.Decoder rangeDecoder,
        uint pos,
        byte prevByte,
        byte matchByte)
      {
        return this.m_Coders[(int) this.GetState(pos, prevByte)].DecodeWithMatchByte(rangeDecoder, matchByte);
      }

      private struct Decoder2
      {
        private BitDecoder[] m_Decoders;

        public void Create() => this.m_Decoders = new BitDecoder[768];

        public void Init()
        {
          for (int index = 0; index < 768; ++index)
            this.m_Decoders[index].Init();
        }

        public byte DecodeNormal(SevenZip.Compression.RangeCoder.Decoder rangeDecoder)
        {
          uint index = 1;
          do
          {
            index = index << 1 | this.m_Decoders[(int) index].Decode(rangeDecoder);
          }
          while (index < 256U);
          return (byte) index;
        }

        public byte DecodeWithMatchByte(SevenZip.Compression.RangeCoder.Decoder rangeDecoder, byte matchByte)
        {
          uint index = 1;
          do
          {
            uint num1 = (uint) ((int) matchByte >> 7 & 1);
            matchByte <<= 1;
            uint num2 = this.m_Decoders[(int) ((uint) (1 + (int) num1 << 8) + index)].Decode(rangeDecoder);
            index = index << 1 | num2;
            if ((int) num1 != (int) num2)
            {
              while (index < 256U)
                index = index << 1 | this.m_Decoders[(int) index].Decode(rangeDecoder);
              break;
            }
          }
          while (index < 256U);
          return (byte) index;
        }
      }
    }
  }
}
