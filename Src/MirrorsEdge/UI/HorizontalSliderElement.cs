// Decompiled with JetBrains decompiler
// Type: UI.HorizontalSliderElement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using support;

#nullable disable
namespace UI
{
  public class HorizontalSliderElement(int width) : SliderElement(width, 40)
  {
    public const int SLIDER_HEIGHT = 40;

    public override void render(Graphics g, int top, int left)
    {
      int num = this.m_y + (this.m_height >> 1);
      g.setColor(7575731);
      g.fillRect(left + this.m_x, top + num - 2, this.m_width, 4);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_SLIDER"), true);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_SLIDER_BUTTON"), true);
      this.m_quadManager.setMeshPosition((int) QuadManager.get("MESH_SLIDER_BUTTON"), (float) (left + this.m_x) + this.m_slidePos * (float) this.m_width, (float) (top + num), 18);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_SLIDER_BUTTON"), false);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_SLIDER"), false);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      this.m_slidePos = (float) x / (float) this.m_width;
      this.m_sliding = true;
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      this.m_slidePos = (float) x / (float) this.m_width;
      this.m_sliding = false;
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      this.m_slidePos = (float) x / (float) this.m_width;
      return true;
    }
  }
}
