
// Type: UI.AboutWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace UI
{
  public class AboutWindow : TitledWindow
  {
    public const int WINDOW_PADDING_X = 45;
    public const int WINDOW_HEIGHT = 220;
    private Window m_aboutTextArea;

    public AboutWindow()
      : base(2049, 2051)
    {
      this.m_aboutTextArea = new Window(45, this.m_height - 220 >> 1, this.m_width - 90, 220);
      this.m_backgroundBorder.setPosition(this.m_aboutTextArea.getX(), this.m_aboutTextArea.getY());
      this.m_backgroundBorder.setDimensions(this.m_aboutTextArea.getWidth(), this.m_aboutTextArea.getHeight());
      this.m_aboutTextArea.addElement((WindowElement) new AboutText(this.m_aboutTextArea));
    }

    public override void Destructor()
    {
      this.m_aboutTextArea.Destructor();
      this.m_aboutTextArea = (Window) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      this.m_aboutTextArea.update(timeStep);
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      this.m_aboutTextArea.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (base.pointerPressed(x, y, pointerNum))
        return true;
      if (!this.m_aboutTextArea.contains(x, y))
        return false;
      this.m_aboutTextArea.pointerPressed(this.m_aboutTextArea.toRelativeX(x), this.m_aboutTextArea.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (base.pointerReleased(x, y, pointerNum))
        return true;
      if (!this.m_aboutTextArea.contains(x, y))
        return false;
      this.m_aboutTextArea.pointerReleased(this.m_aboutTextArea.toRelativeX(x), this.m_aboutTextArea.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (!this.m_aboutTextArea.contains(x, y))
        return false;
      this.m_aboutTextArea.pointerDragged(this.m_aboutTextArea.toRelativeX(x), this.m_aboutTextArea.toRelativeY(y), pointerNum);
      return true;
    }
  }
}
