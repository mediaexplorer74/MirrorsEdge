// Decompiled with JetBrains decompiler
// Type: microedition.m3g.CompositingMode
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class CompositingMode : Object3D
  {
    private const float factor = 1.52587891E-05f;
    public const int ADD = 69;
    public const int ALPHA = 64;
    public const int ALPHA_ADD = 65;
    public const int ALPHA_DARKEN = 70;
    public const int ALPHA_PREMULTIPLIED = 71;
    public const int MODULATE = 66;
    public const int MODULATE_INV = 72;
    public const int MODULATE_X2 = 67;
    public const int REPLACE = 68;
    public new const int M3G_UNIQUE_CLASS_ID = 6;
    private int m_Blending;
    private Blender m_Blender;
    private int m_AlphaThreshold;
    private bool m_DepthTestEnabled;
    private bool m_DepthWriteEnabled;
    private bool m_ColorWriteEnabled;
    private bool m_AlphaWriteEnabled;
    private int m_DepthOffsetFactor;
    private int m_DepthOffsetUnits;

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      CompositingMode compositingMode = (CompositingMode) ret;
      compositingMode.setBlending(this.getBlending());
      compositingMode.setBlender(this.getBlender());
      compositingMode.setAlphaThreshold(this.getAlphaThreshold());
      compositingMode.setColorWriteEnable(this.isColorWriteEnabled());
      compositingMode.setDepthWriteEnable(this.isDepthWriteEnabled());
      compositingMode.setDepthTestEnable(this.isDepthTestEnabled());
      compositingMode.setDepthOffsetx(this.getDepthOffsetFactorx(), this.getDepthOffsetUnitsx());
    }

    public CompositingMode()
    {
      this.m_Blending = 68;
      this.m_Blender = (Blender) null;
      this.m_AlphaThreshold = 0;
      this.m_DepthTestEnabled = true;
      this.m_DepthWriteEnabled = true;
      this.m_ColorWriteEnabled = true;
      this.m_AlphaWriteEnabled = true;
      this.m_DepthOffsetFactor = 0;
      this.m_DepthOffsetUnits = 0;
    }

    public void setBlending(int mode) => this.m_Blending = mode;

    public int getBlending() => this.m_Blending;

    public void setBlender(Blender blender) => this.m_Blender = blender;

    public Blender getBlender() => this.m_Blender;

    public void setAlphaThreshold(float threshold)
    {
      this.setAlphaThresholdx((int) ((double) threshold * 65536.0));
    }

    public void setAlphaThresholdx(int threshold) => this.m_AlphaThreshold = threshold;

    public float getAlphaThreshold() => (float) this.getAlphaThresholdx() * 1.52587891E-05f;

    public int getAlphaThresholdx() => this.m_AlphaThreshold;

    public void setAlphaWriteEnable(bool enable) => this.m_AlphaWriteEnabled = enable;

    public bool isAlphaWriteEnabled() => this.m_AlphaWriteEnabled;

    public void setColorWriteEnable(bool enable) => this.m_ColorWriteEnabled = enable;

    public bool isColorWriteEnabled() => this.m_ColorWriteEnabled;

    public void setDepthWriteEnable(bool enable) => this.m_DepthWriteEnabled = enable;

    public bool isDepthWriteEnabled() => this.m_DepthWriteEnabled;

    public void setDepthTestEnable(bool enable) => this.m_DepthTestEnabled = enable;

    public bool isDepthTestEnabled() => this.m_DepthTestEnabled;

    public void setDepthOffset(float factor, float units)
    {
      this.setDepthOffsetx((int) ((double) factor * 65536.0), (int) ((double) units * 65536.0));
    }

    public void setDepthOffsetx(int factor, int units)
    {
      this.m_DepthOffsetFactor = factor;
      this.m_DepthOffsetUnits = units;
    }

    public float getDepthOffsetFactor() => (float) this.getDepthOffsetFactorx() * 1.52587891E-05f;

    public int getDepthOffsetFactorx() => this.m_DepthOffsetFactor;

    public float getDepthOffsetUnits() => (float) this.getDepthOffsetUnitsx() * 1.52587891E-05f;

    public int getDepthOffsetUnitsx() => this.m_DepthOffsetUnits;

    public override int getM3GUniqueClassID() => 6;

    public static CompositingMode m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 6 ? (CompositingMode) obj : (CompositingMode) null;
    }
  }
}
