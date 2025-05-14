// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Blender
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class Blender : Object3D
  {
    public const int ADD = 88;
    public const int CONSTANT_ALPHA = 125;
    public const int CONSTANT_COLOR = 123;
    public const int DST_ALPHA = 118;
    public const int DST_COLOR = 120;
    public const int ONE = 113;
    public const int ONE_MINUS_CONSTANT_ALPHA = 126;
    public const int ONE_MINUS_CONSTANT_COLOR = 124;
    public const int ONE_MINUS_DST_ALPHA = 119;
    public const int ONE_MINUS_DST_COLOR = 121;
    public const int ONE_MINUS_SRC_ALPHA = 117;
    public const int ONE_MINUS_SRC_COLOR = 115;
    public const int REVERSE_SUBTRACT = 90;
    public const int SRC_ALPHA = 116;
    public const int SRC_ALPHA_SATURATE = 122;
    public const int SRC_COLOR = 114;
    public const int SUBTRACT = 89;
    public const int ZERO = 112;
    public new const int M3G_UNIQUE_CLASS_ID = 32;
    private int m_BlendColor;
    private int m_SrcColorBlendFactor;
    private int m_SrcAlphaBlendFactor;
    private int m_DstColorBlendFactor;
    private int m_DstAlphaBlendFactor;
    private int m_ColorBlendFunc;
    private int m_AlphaBlendFunc;

    public Blender()
    {
      this.m_BlendColor = 0;
      this.m_SrcColorBlendFactor = 113;
      this.m_SrcAlphaBlendFactor = 113;
      this.m_DstColorBlendFactor = 112;
      this.m_DstAlphaBlendFactor = 112;
      this.m_ColorBlendFunc = 88;
      this.m_AlphaBlendFunc = 88;
    }

    public int getBlendColor() => this.m_BlendColor;

    public int getBlendFactor(int component)
    {
      switch (component)
      {
        case 114:
          return this.m_SrcColorBlendFactor;
        case 116:
          return this.m_SrcAlphaBlendFactor;
        case 118:
          return this.m_DstAlphaBlendFactor;
        case 120:
          return this.m_DstColorBlendFactor;
        default:
          return -1;
      }
    }

    public int getBlendFunction(int channel)
    {
      switch (channel)
      {
        case 114:
          return this.m_ColorBlendFunc;
        case 116:
          return this.m_AlphaBlendFunc;
        default:
          return -1;
      }
    }

    public void setBlendColor(int ARGB) => this.m_BlendColor = ARGB;

    public void setBlendFactors(int srcColor, int srcAlpha, int dstColor, int dstAlpha)
    {
      this.m_SrcColorBlendFactor = srcColor;
      this.m_SrcAlphaBlendFactor = srcAlpha;
      this.m_DstColorBlendFactor = dstColor;
      this.m_DstAlphaBlendFactor = dstAlpha;
    }

    public void setBlendFunctions(int funcColor, int funcAlpha)
    {
      this.m_ColorBlendFunc = funcColor;
      this.m_AlphaBlendFunc = funcAlpha;
    }

    public override int getM3GUniqueClassID() => 32;
  }
}
