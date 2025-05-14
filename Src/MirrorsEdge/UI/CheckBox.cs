
// Type: UI.CheckBox
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace UI
{
  public class CheckBox : WindowElement
  {
    public const int DEFAULT_WIDTH = 24;
    public const int DEFAULT_HEIGHT = 24;
    public const int INTERIOR_BORDER = 3;
    private bool m_checked;

    public CheckBox()
      : base(0, 0, 24, 24)
    {
      this.m_checked = false;
    }

    public override void update(int timeStep)
    {
    }

    public override void render(Graphics g, int top, int left)
    {
      g.setColor(0);
      g.drawRect(left + this.m_x, top + this.m_y, this.m_width - 1, this.m_height - 1);
      if (!this.m_checked)
        return;
      g.setColor(16711680);
      g.fillRect(left + this.m_x + 3, top + this.m_y + 3, this.m_width - 6 - 1, this.m_height - 6 - 1);
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      this.m_checked = !this.m_checked;
      return true;
    }

    public void setChecked(bool check) => this.m_checked = check;

    public bool getChecked() => this.m_checked;
  }
}
