// Decompiled with JetBrains decompiler
// Type: SevenZip.ICodeProgress
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace SevenZip
{
  public interface ICodeProgress
  {
    void SetProgress(long inSize, long outSize);
  }
}
