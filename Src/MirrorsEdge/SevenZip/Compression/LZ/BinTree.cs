
// Type: SevenZip.Compression.LZ.BinTree
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;
using System.IO;

#nullable disable
namespace SevenZip.Compression.LZ
{
  public class BinTree : InWindow, IMatchFinder, IInWindowStream
  {
    private const uint kHash2Size = 1024;
    private const uint kHash3Size = 65536;
    private const uint kBT2HashSize = 65536;
    private const uint kStartMaxLen = 1;
    private const uint kHash3Offset = 1024;
    private const uint kEmptyHashValue = 0;
    private const uint kMaxValForNormalize = 2147483647;
    private uint _cyclicBufferPos;
    private uint _cyclicBufferSize;
    private uint _matchMaxLen;
    private uint[] _son;
    private uint[] _hash;
    private uint _cutValue = (uint) byte.MaxValue;
    private uint _hashMask;
    private uint _hashSizeSum;
    private bool HASH_ARRAY = true;
    private uint kNumHashDirectBytes;
    private uint kMinMatchCheck = 4;
    private uint kFixHashSize = 66560;

    public void SetType(int numHashBytes)
    {
      this.HASH_ARRAY = numHashBytes > 2;
      if (this.HASH_ARRAY)
      {
        this.kNumHashDirectBytes = 0U;
        this.kMinMatchCheck = 4U;
        this.kFixHashSize = 66560U;
      }
      else
      {
        this.kNumHashDirectBytes = 2U;
        this.kMinMatchCheck = 3U;
        this.kFixHashSize = 0U;
      }
    }

    public new void SetStream(Stream stream) => base.SetStream(stream);

    public new void ReleaseStream() => base.ReleaseStream();

    public new void Init()
    {
      base.Init();
      for (uint index = 0; index < this._hashSizeSum; ++index)
        this._hash[(int) index] = 0U;
      this._cyclicBufferPos = 0U;
      this.ReduceOffsets(-1);
    }

    public new void MovePos()
    {
      if (++this._cyclicBufferPos >= this._cyclicBufferSize)
        this._cyclicBufferPos = 0U;
      base.MovePos();
      if (this._pos != (uint) int.MaxValue)
        return;
      this.Normalize();
    }

    public new byte GetIndexByte(int index) => base.GetIndexByte(index);

    public new uint GetMatchLen(int index, uint distance, uint limit)
    {
      return base.GetMatchLen(index, distance, limit);
    }

    public new uint GetNumAvailableBytes() => base.GetNumAvailableBytes();

    public void Create(
      uint historySize,
      uint keepAddBufferBefore,
      uint matchMaxLen,
      uint keepAddBufferAfter)
    {
      if (historySize > 2147483391U)
        throw new Exception();
      this._cutValue = 16U + (matchMaxLen >> 1);
      uint keepSizeReserv = (historySize + keepAddBufferBefore + matchMaxLen + keepAddBufferAfter) / 2U + 256U;
      this.Create(historySize + keepAddBufferBefore, matchMaxLen + keepAddBufferAfter, keepSizeReserv);
      this._matchMaxLen = matchMaxLen;
      uint num1 = historySize + 1U;
      if ((int) this._cyclicBufferSize != (int) num1)
        this._son = new uint[(int) ((this._cyclicBufferSize = num1) * 2U)];
      uint num2 = 65536;
      if (this.HASH_ARRAY)
      {
        uint num3 = historySize - 1U;
        uint num4 = num3 | num3 >> 1;
        uint num5 = num4 | num4 >> 2;
        uint num6 = num5 | num5 >> 4;
        uint num7 = (num6 | num6 >> 8) >> 1 | (uint) ushort.MaxValue;
        if (num7 > 16777216U)
          num7 >>= 1;
        this._hashMask = num7;
        num2 = num7 + 1U + this.kFixHashSize;
      }
      if ((int) num2 == (int) this._hashSizeSum)
        return;
      this._hash = new uint[(int) (this._hashSizeSum = num2)];
    }

    public uint GetMatches(uint[] distances)
    {
      uint num1;
      if (this._pos + this._matchMaxLen <= this._streamPos)
      {
        num1 = this._matchMaxLen;
      }
      else
      {
        num1 = this._streamPos - this._pos;
        if (num1 < this.kMinMatchCheck)
        {
          this.MovePos();
          return 0;
        }
      }
      uint matches = 0;
      uint num2 = this._pos > this._cyclicBufferSize ? this._pos - this._cyclicBufferSize : 0U;
      uint index1 = this._bufferOffset + this._pos;
      uint num3 = 1;
      uint index2 = 0;
      uint num4 = 0;
      uint num5;
      if (this.HASH_ARRAY)
      {
        uint num6 = CRC.Table[(int) this._bufferBase[(int) index1]] ^ (uint) this._bufferBase[(int) (index1 + 1U)];
        index2 = num6 & 1023U;
        uint num7 = num6 ^ (uint) this._bufferBase[(int) (index1 + 2U)] << 8;
        num4 = num7 & (uint) ushort.MaxValue;
        num5 = (num7 ^ CRC.Table[(int) this._bufferBase[(int) (index1 + 3U)]] << 5) & this._hashMask;
      }
      else
        num5 = (uint) this._bufferBase[(int) index1] ^ (uint) this._bufferBase[(int) (index1 + 1U)] << 8;
      uint num8 = this._hash[(int) (this.kFixHashSize + num5)];
      if (this.HASH_ARRAY)
      {
        uint num9 = this._hash[(int) index2];
        uint num10 = this._hash[(int) (1024U + num4)];
        this._hash[(int) index2] = this._pos;
        this._hash[(int) (1024U + num4)] = this._pos;
        if (num9 > num2 && (int) this._bufferBase[(int) (this._bufferOffset + num9)] == (int) this._bufferBase[(int) index1])
        {
          uint[] numArray1 = distances;
          int num11 = (int) matches;
          uint num12 = (uint) (num11 + 1);
          uint index3 = (uint) num11;
          int num13;
          num3 = (uint) (num13 = 2);
          numArray1[(int) index3] = (uint) num13;
          uint[] numArray2 = distances;
          int num14 = (int) num12;
          matches = (uint) (num14 + 1);
          uint index4 = (uint) num14;
          int num15 = (int) this._pos - (int) num9 - 1;
          numArray2[(int) index4] = (uint) num15;
        }
        if (num10 > num2 && (int) this._bufferBase[(int) (this._bufferOffset + num10)] == (int) this._bufferBase[(int) index1])
        {
          if ((int) num10 == (int) num9)
            matches -= 2U;
          uint[] numArray3 = distances;
          int num16 = (int) matches;
          uint num17 = (uint) (num16 + 1);
          uint index5 = (uint) num16;
          int num18;
          num3 = (uint) (num18 = 3);
          numArray3[(int) index5] = (uint) num18;
          uint[] numArray4 = distances;
          int num19 = (int) num17;
          matches = (uint) (num19 + 1);
          uint index6 = (uint) num19;
          int num20 = (int) this._pos - (int) num10 - 1;
          numArray4[(int) index6] = (uint) num20;
          num9 = num10;
        }
        if (matches != 0U && (int) num9 == (int) num8)
        {
          matches -= 2U;
          num3 = 1U;
        }
      }
      this._hash[(int) (this.kFixHashSize + num5)] = this._pos;
      uint index7 = (uint) (((int) this._cyclicBufferPos << 1) + 1);
      uint index8 = this._cyclicBufferPos << 1;
      uint val2;
      uint val1 = val2 = this.kNumHashDirectBytes;
      if (this.kNumHashDirectBytes != 0U && num8 > num2 && (int) this._bufferBase[(int) (this._bufferOffset + num8 + this.kNumHashDirectBytes)] != (int) this._bufferBase[(int) (index1 + this.kNumHashDirectBytes)])
      {
        uint[] numArray5 = distances;
        int num21 = (int) matches;
        uint num22 = (uint) (num21 + 1);
        uint index9 = (uint) num21;
        int numHashDirectBytes;
        num3 = (uint) (numHashDirectBytes = (int) this.kNumHashDirectBytes);
        numArray5[(int) index9] = (uint) numHashDirectBytes;
        uint[] numArray6 = distances;
        int num23 = (int) num22;
        matches = (uint) (num23 + 1);
        uint index10 = (uint) num23;
        int num24 = (int) this._pos - (int) num8 - 1;
        numArray6[(int) index10] = (uint) num24;
      }
      uint cutValue = this._cutValue;
      while (num8 > num2 && cutValue-- != 0U)
      {
        uint num25 = this._pos - num8;
        uint index11 = (uint) ((num25 <= this._cyclicBufferPos ? (int) this._cyclicBufferPos - (int) num25 : (int) this._cyclicBufferPos - (int) num25 + (int) this._cyclicBufferSize) << 1);
        uint num26 = this._bufferOffset + num8;
        uint num27 = Math.Min(val1, val2);
        if ((int) this._bufferBase[(int) (num26 + num27)] == (int) this._bufferBase[(int) (index1 + num27)])
        {
          do
            ;
          while ((int) ++num27 != (int) num1 && (int) this._bufferBase[(int) (num26 + num27)] == (int) this._bufferBase[(int) (index1 + num27)]);
          if (num3 < num27)
          {
            uint[] numArray7 = distances;
            int num28 = (int) matches;
            uint num29 = (uint) (num28 + 1);
            uint index12 = (uint) num28;
            int num30;
            num3 = (uint) (num30 = (int) num27);
            numArray7[(int) index12] = (uint) num30;
            uint[] numArray8 = distances;
            int num31 = (int) num29;
            matches = (uint) (num31 + 1);
            uint index13 = (uint) num31;
            int num32 = (int) num25 - 1;
            numArray8[(int) index13] = (uint) num32;
            if ((int) num27 == (int) num1)
            {
              this._son[(int) index8] = this._son[(int) index11];
              this._son[(int) index7] = this._son[(int) (index11 + 1U)];
              goto label_29;
            }
          }
        }
        if ((int) this._bufferBase[(int) (num26 + num27)] < (int) this._bufferBase[(int) (index1 + num27)])
        {
          this._son[(int) index8] = num8;
          index8 = index11 + 1U;
          num8 = this._son[(int) index8];
          val2 = num27;
        }
        else
        {
          this._son[(int) index7] = num8;
          index7 = index11;
          num8 = this._son[(int) index7];
          val1 = num27;
        }
      }
      this._son[(int) index7] = this._son[(int) index8] = 0U;
label_29:
      this.MovePos();
      return matches;
    }

    public void Skip(uint num)
    {
      do
      {
        uint num1;
        if (this._pos + this._matchMaxLen <= this._streamPos)
        {
          num1 = this._matchMaxLen;
        }
        else
        {
          num1 = this._streamPos - this._pos;
          if (num1 < this.kMinMatchCheck)
          {
            this.MovePos();
            goto label_19;
          }
        }
        uint num2 = this._pos > this._cyclicBufferSize ? this._pos - this._cyclicBufferSize : 0U;
        uint index1 = this._bufferOffset + this._pos;
        uint num3;
        if (this.HASH_ARRAY)
        {
          uint num4 = CRC.Table[(int) this._bufferBase[(int) index1]] ^ (uint) this._bufferBase[(int) (index1 + 1U)];
          this._hash[(int) (num4 & 1023U)] = this._pos;
          uint num5 = num4 ^ (uint) this._bufferBase[(int) (index1 + 2U)] << 8;
          this._hash[(int) (1024U + (num5 & (uint) ushort.MaxValue))] = this._pos;
          num3 = (num5 ^ CRC.Table[(int) this._bufferBase[(int) (index1 + 3U)]] << 5) & this._hashMask;
        }
        else
          num3 = (uint) this._bufferBase[(int) index1] ^ (uint) this._bufferBase[(int) (index1 + 1U)] << 8;
        uint num6 = this._hash[(int) (this.kFixHashSize + num3)];
        this._hash[(int) (this.kFixHashSize + num3)] = this._pos;
        uint index2 = (uint) (((int) this._cyclicBufferPos << 1) + 1);
        uint index3 = this._cyclicBufferPos << 1;
        uint val2;
        uint val1 = val2 = this.kNumHashDirectBytes;
        uint cutValue = this._cutValue;
        while (num6 > num2 && cutValue-- != 0U)
        {
          uint num7 = this._pos - num6;
          uint index4 = (uint) ((num7 <= this._cyclicBufferPos ? (int) this._cyclicBufferPos - (int) num7 : (int) this._cyclicBufferPos - (int) num7 + (int) this._cyclicBufferSize) << 1);
          uint num8 = this._bufferOffset + num6;
          uint num9 = Math.Min(val1, val2);
          if ((int) this._bufferBase[(int) (num8 + num9)] == (int) this._bufferBase[(int) (index1 + num9)])
          {
            do
              ;
            while ((int) ++num9 != (int) num1 && (int) this._bufferBase[(int) (num8 + num9)] == (int) this._bufferBase[(int) (index1 + num9)]);
            if ((int) num9 == (int) num1)
            {
              this._son[(int) index3] = this._son[(int) index4];
              this._son[(int) index2] = this._son[(int) (index4 + 1U)];
              goto label_18;
            }
          }
          if ((int) this._bufferBase[(int) (num8 + num9)] < (int) this._bufferBase[(int) (index1 + num9)])
          {
            this._son[(int) index3] = num6;
            index3 = index4 + 1U;
            num6 = this._son[(int) index3];
            val2 = num9;
          }
          else
          {
            this._son[(int) index2] = num6;
            index2 = index4;
            num6 = this._son[(int) index2];
            val1 = num9;
          }
        }
        this._son[(int) index2] = this._son[(int) index3] = 0U;
label_18:
        this.MovePos();
label_19:;
      }
      while (--num != 0U);
    }

    private void NormalizeLinks(uint[] items, uint numItems, uint subValue)
    {
      for (uint index = 0; index < numItems; ++index)
      {
        uint num1 = items[(int) index];
        uint num2 = num1 > subValue ? num1 - subValue : 0U;
        items[(int) index] = num2;
      }
    }

    private void Normalize()
    {
      uint subValue = this._pos - this._cyclicBufferSize;
      this.NormalizeLinks(this._son, this._cyclicBufferSize * 2U, subValue);
      this.NormalizeLinks(this._hash, this._hashSizeSum, subValue);
      this.ReduceOffsets((int) subValue);
    }

    public void SetCutValue(uint cutValue) => this._cutValue = cutValue;
  }
}
