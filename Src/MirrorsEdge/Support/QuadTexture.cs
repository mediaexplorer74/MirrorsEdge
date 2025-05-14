// Decompiled with JetBrains decompiler
// Type: support.QuadTexture
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace support
{
  public class QuadTexture
  {
    public Texture2D texture;
    public VertexArray vertexArray;
    public int imageIndex;
    public int refCount;
    public int blendMode;
    public int filtering;
    public int texX;
    public int texY;
    public int texW;
    public int texH;

    public QuadTexture()
    {
      this.texture = (Texture2D) null;
      this.vertexArray = (VertexArray) null;
      this.imageIndex = 0;
      this.refCount = 0;
      this.blendMode = 0;
      this.filtering = 0;
      this.texX = 0;
      this.texY = 0;
      this.texW = 0;
      this.texH = 0;
    }

    public QuadTexture(QuadTexture quadTex)
    {
      this.texture = quadTex.texture;
      this.vertexArray = quadTex.vertexArray;
      this.imageIndex = quadTex.imageIndex;
      this.refCount = quadTex.refCount;
      this.blendMode = quadTex.blendMode;
      this.filtering = quadTex.filtering;
      this.texX = quadTex.texX;
      this.texY = quadTex.texY;
      this.texW = quadTex.texW;
      this.texH = quadTex.texH;
    }

    public QuadTexture CopyFrom(QuadTexture quadTex)
    {
      this.texture = quadTex.texture;
      this.vertexArray = quadTex.vertexArray;
      this.imageIndex = quadTex.imageIndex;
      this.refCount = quadTex.refCount;
      this.blendMode = quadTex.blendMode;
      this.filtering = quadTex.filtering;
      this.texX = quadTex.texX;
      this.texY = quadTex.texY;
      this.texW = quadTex.texW;
      this.texH = quadTex.texH;
      return this;
    }

    public void Destructor()
    {
      this.texture = (Texture2D) null;
      this.vertexArray = (VertexArray) null;
    }
  }
}
