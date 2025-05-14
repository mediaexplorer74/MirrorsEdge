
// Type: microedition.m3g.World
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class World : Group
  {
    public new const int M3G_UNIQUE_CLASS_ID = 22;
    private Background m_Background;
    private Camera m_ActiveCamera;

    public World()
    {
      this.m_Background = (Background) null;
      this.m_ActiveCamera = (Camera) null;
    }

    public override void Destructor()
    {
      if (this.m_Background != null)
        this.m_Background.Destructor();
      this.m_Background = (Background) null;
      if (this.m_ActiveCamera != null)
        this.m_ActiveCamera.Destructor();
      this.m_ActiveCamera = (Camera) null;
      base.Destructor();
    }

    public void setActiveCamera(Camera camera) => this.m_ActiveCamera = camera;

    public void setBackground(Background background) => this.m_Background = background;

    public Camera getActiveCamera() => this.m_ActiveCamera;

    public Background getBackground() => this.m_Background;

    public override int getM3GUniqueClassID() => 22;

    public static World m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 22 ? (World) obj : (World) null;
    }
  }
}
