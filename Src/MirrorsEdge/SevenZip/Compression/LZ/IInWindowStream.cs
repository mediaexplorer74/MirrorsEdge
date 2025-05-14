
// Type: SevenZip.Compression.LZ.IInWindowStream
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.IO;

#nullable disable
namespace SevenZip.Compression.LZ
{
  internal interface IInWindowStream
  {
    void SetStream(Stream inStream);

    void Init();

    void ReleaseStream();

    byte GetIndexByte(int index);

    uint GetMatchLen(int index, uint distance, uint limit);

    uint GetNumAvailableBytes();
  }
}
