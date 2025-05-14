// Decompiled with JetBrains decompiler
// Type: SevenZip.Compression.LZ.IMatchFinder
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace SevenZip.Compression.LZ
{
  internal interface IMatchFinder : IInWindowStream
  {
    void Create(
      uint historySize,
      uint keepAddBufferBefore,
      uint matchMaxLen,
      uint keepAddBufferAfter);

    uint GetMatches(uint[] distances);

    void Skip(uint num);
  }
}
