// Decompiled with JetBrains decompiler
// Type: support.QuadAttrib
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace support
{
  public class QuadAttrib
  {
    public QuadMesh mesh;
    public int attribIndex;
    public int[] valueArray;

    public QuadAttrib()
    {
      this.mesh = (QuadMesh) null;
      this.attribIndex = 0;
      this.valueArray = (int[]) null;
    }

    public QuadAttrib(QuadAttrib quadAtt)
    {
      this.mesh = quadAtt.mesh;
      this.attribIndex = quadAtt.attribIndex;
      if (quadAtt.valueArray != null)
      {
        this.valueArray = new int[quadAtt.valueArray.Length];
        Array.Copy((Array) quadAtt.valueArray, (Array) this.valueArray, quadAtt.valueArray.Length);
      }
      else
        this.valueArray = (int[]) null;
    }

    public QuadAttrib CopyFrom(QuadAttrib quadAtt)
    {
      this.mesh = quadAtt.mesh;
      this.attribIndex = quadAtt.attribIndex;
      if (quadAtt.valueArray != null)
      {
        this.valueArray = new int[quadAtt.valueArray.Length];
        Array.Copy((Array) quadAtt.valueArray, (Array) this.valueArray, quadAtt.valueArray.Length);
      }
      else
        this.valueArray = (int[]) null;
      return this;
    }

    public void Destructor()
    {
      this.mesh = (QuadMesh) null;
      this.valueArray = (int[]) null;
    }
  }
}
