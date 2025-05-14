
// Type: UI.VerticalScrollBar
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using support;

#nullable disable
namespace UI
{
  public class VerticalScrollBar : ScrollBar
  {
    public const int DEFAULT_WIDTH = 10;

    public VerticalScrollBar(Window window)
      : base(window)
    {
      this.setWidth(10);
    }

    public override void Destructor() => base.Destructor();

    public override void render(Graphics g, int top, int left)
    {
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_SCROLLBAR"), true);
      this.m_quadManager.setGroupPosition((int) QuadManager.get("GROUP_WINDOW_SCROLLBAR"), (float) (left + this.m_x), (float) (top + this.m_y));
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_SCROLLBAR_BACKING"), 0.0f, 0.0f, (float) this.m_width, (float) this.m_height, 9);
      float num1 = (float) -this.m_window.getClientOffsetY();
      float clientMaxY = (float) this.m_window.getClientMaxY();
      float num2 = num1 / clientMaxY;
      float h = (float) this.m_window.getClientHeight() / ((float) this.m_window.getClientHeight() + clientMaxY) * (float) this.m_height;
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_SCROLLBAR_VISIBLE"), 0.0f, ((float) this.m_height - h) * num2, (float) this.m_width, h, 9);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_SCROLLBAR"), false);
    }

    protected override int getOffset() => this.m_window.getClientOffsetY();

    protected override void setOffset(int offset) => this.m_window.setClientOffsetY(offset);
  }
}
