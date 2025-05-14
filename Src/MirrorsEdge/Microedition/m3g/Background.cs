
// Type: microedition.m3g.Background
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class Background : Object3D
  {
    public new const int M3G_UNIQUE_CLASS_ID = 4;
    private uint m_Color;
    private bool m_ColorClearEnabled;
    private bool m_DepthClearEnabled;

    public Background()
    {
      this.m_Color = 0U;
      this.m_ColorClearEnabled = true;
      this.m_DepthClearEnabled = true;
    }

    public uint getColor() => this.m_Color;

    public bool isColorClearEnabled() => this.m_ColorClearEnabled;

    public bool isDepthClearEnabled() => this.m_DepthClearEnabled;

    public void setColor(uint argb) => this.m_Color = argb;

    public void setColorClearEnable(bool onoff) => this.m_ColorClearEnabled = onoff;

    public void setDepthClearEnable(bool onoff) => this.m_DepthClearEnabled = onoff;

    public override int getM3GUniqueClassID() => 4;
  }
}
