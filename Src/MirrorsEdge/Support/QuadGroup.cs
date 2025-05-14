// Decompiled with JetBrains decompiler
// Type: support.QuadGroup
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace support
{
  public class QuadGroup
  {
    public Group group;
    public int parentIndex;
    public bool initVisible;
    public int childGroupStart;
    public int childGroupNum;
    public int childMeshStart;
    public int childMeshNum;
    public int offsetX;
    public int offsetY;

    public QuadGroup()
    {
      this.group = (Group) null;
      this.parentIndex = -1;
      this.initVisible = true;
      this.childGroupStart = 0;
      this.childGroupNum = 0;
      this.childMeshStart = 0;
      this.childMeshNum = 0;
      this.offsetX = 0;
      this.offsetY = 0;
    }

    public void Destructor() => this.group = (Group) null;
  }
}
