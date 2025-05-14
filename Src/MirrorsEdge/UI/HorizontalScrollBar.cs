
// Type: UI.HorizontalScrollBar
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using support;

#nullable disable
namespace UI
{
  public class HorizontalScrollBar : ScrollBar
  {
    public const int DEFAULT_HEIGHT = 10;

    public HorizontalScrollBar(Window window)
      : base(window)
    {
      this.setHeight(10);
    }

    public override void Destructor() => base.Destructor();

    public override void render(Graphics g, int top, int left)
    {
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_SCROLLBAR"), true);
      this.m_quadManager.setGroupPosition((int) QuadManager.get("GROUP_WINDOW_SCROLLBAR"), (float) (left + this.m_x), (float) (top + this.m_y));
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_SCROLLBAR_BACKING"), 0.0f, 0.0f, (float) this.m_width, (float) this.m_height, 9);
      float num1 = (float) -this.m_window.getClientOffsetX();
      float clientMaxX = (float) this.m_window.getClientMaxX();
      float num2 = num1 / clientMaxX;
      float w = (float) this.m_window.getClientWidth() / ((float) this.m_window.getClientWidth() + clientMaxX) * (float) this.m_width;
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_SCROLLBAR_VISIBLE"), ((float) this.m_width - w) * num2, 0.0f, w, (float) this.m_height, 9);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_SCROLLBAR"), false);
    }

    protected override int getOffset() => this.m_window.getClientOffsetX();

    protected override void setOffset(int offset) => this.m_window.setClientOffsetX(offset);
  }
}
