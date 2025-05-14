// Decompiled with JetBrains decompiler
// Type: microedition.m3g.VertexArrayTextureUnit
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class VertexArrayTextureUnit
  {
    public VertexArray texcoords;
    public float texcoordScale;
    public float texcoordBiasU;
    public float texcoordBiasV;

    public VertexArrayTextureUnit(VertexArray arr, float scale, float[] bias)
    {
      this.texcoords = arr;
      this.texcoordScale = 1f;
      this.texcoordBiasU = 0.0f;
      this.texcoordBiasV = 0.0f;
      this.texcoordScale = scale;
      if (bias != null)
      {
        this.texcoordBiasU = bias[0];
        this.texcoordBiasV = bias[1];
      }
      else
      {
        this.texcoordBiasU = 0.0f;
        this.texcoordBiasV = 0.0f;
      }
    }

    public VertexArrayTextureUnit(VertexArray arr, float scale, float[] bias, int sindex)
    {
      this.texcoords = arr;
      this.texcoordScale = 1f;
      this.texcoordBiasU = 0.0f;
      this.texcoordBiasV = 0.0f;
      this.texcoordScale = scale;
      if (bias != null)
      {
        this.texcoordBiasU = bias[sindex];
        this.texcoordBiasV = bias[sindex + 1];
      }
      else
      {
        this.texcoordBiasU = 0.0f;
        this.texcoordBiasV = 0.0f;
      }
    }

    public int getVertexCount() => this.texcoords.getVertexCount();
  }
}
