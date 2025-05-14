
// Type: microedition.m3g.Fog
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class Fog : Object3D
  {
    public const int EXPONENTIAL = 80;
    public const int LINEAR = 81;
    public const int NONE = -1;
    public new const int M3G_UNIQUE_CLASS_ID = 7;
    private int m_Mode;
    private float m_Density;
    private float m_Near;
    private float m_Far;
    private int m_Color;

    public Fog()
    {
      this.m_Mode = 81;
      this.m_Density = 1f;
      this.m_Near = 0.0f;
      this.m_Far = 1f;
      this.m_Color = 0;
    }

    public int getColor() => this.m_Color;

    public float getDensity() => this.m_Density;

    public int getDensityx() => (int) ((double) this.m_Density * 65536.0);

    public float getFarDistance() => this.m_Far;

    public int getFarDistancex() => (int) ((double) this.m_Far * 65536.0);

    public int getMode() => this.m_Mode;

    public float getNearDistance() => this.m_Near;

    public int getNearDistancex() => (int) ((double) this.m_Near * 65536.0);

    public void setColor(int RGB) => this.m_Color = RGB;

    public void setDensity(float density) => this.m_Density = density;

    public void setLinear(float _near, float _far)
    {
      this.m_Near = _near;
      this.m_Far = _far;
    }

    public void setMode(int mode) => this.m_Mode = mode;

    public override int getM3GUniqueClassID() => 7;

    public static Fog m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 7 ? (Fog) obj : (Fog) null;
    }
  }
}
