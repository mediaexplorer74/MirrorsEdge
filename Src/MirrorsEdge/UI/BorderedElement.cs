
// Type: UI.BorderedElement
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using support;

#nullable disable
namespace UI
{
  public class BorderedElement(int x, int y, int width, int height) : WindowElement(x, y, width, height)
  {
    public override void render(Graphics g, int top, int left)
    {
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BACKING"), true);
      this.m_quadManager.setGroupPosition((int) QuadManager.get("GROUP_WINDOW_BACKING"), (float) (left + this.m_x), (float) (top + this.m_y));
      this.m_quadManager.setAnimTime((int) QuadManager.get("ANIM_WINDOW_BACKING_WIDTH"), this.m_width);
      this.m_quadManager.setAnimTime((int) QuadManager.get("ANIM_WINDOW_BACKING_HEIGHT"), this.m_height);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BACKING"), false);
    }

    public void specialFS_Render(Graphics g, int top, int left)
    {
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BACKING"), true);
      this.m_quadManager.setGroupPosition((int) QuadManager.get("GROUP_WINDOW_BACKING"), (float) (left + this.m_x), (float) (top + this.m_y));
      this.m_quadManager.setAnimTimeFS((int) QuadManager.get("ANIM_WINDOW_BACKING_WIDTH"), this.m_width);
      this.m_quadManager.setAnimTime((int) QuadManager.get("ANIM_WINDOW_BACKING_HEIGHT"), this.m_height);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BACKING"), false);
    }
  }
}
