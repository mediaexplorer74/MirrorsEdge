// Decompiled with JetBrains decompiler
// Type: UI.HelpWindow
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace UI
{
  public class HelpWindow : TitledWindow
  {
    public const int WINDOW_PADDING_X = 45;
    public const int WINDOW_HEIGHT = 220;
    private Window m_helpTextArea;

    public HelpWindow()
      : base(2051, 2051)
    {
      this.m_helpTextArea = new Window(45, this.m_height - 220 >> 1, this.m_width - 90, 220);
      this.m_backgroundBorder.setPosition(this.m_helpTextArea.getX(), this.m_helpTextArea.getY());
      this.m_backgroundBorder.setDimensions(this.m_helpTextArea.getWidth(), this.m_helpTextArea.getHeight());
      this.m_helpTextArea.addElement((WindowElement) new HelpText(this.m_helpTextArea));
    }

    public override void Destructor()
    {
      this.m_helpTextArea.Destructor();
      this.m_helpTextArea = (Window) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      this.m_helpTextArea.update(timeStep);
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      this.m_helpTextArea.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      base.pointerPressed(x, y, pointerNum);
      if (!this.m_helpTextArea.contains(x, y))
        return false;
      this.m_helpTextArea.pointerPressed(this.m_helpTextArea.toRelativeX(x), this.m_helpTextArea.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      base.pointerReleased(x, y, pointerNum);
      if (!this.m_helpTextArea.contains(x, y))
        return false;
      this.m_helpTextArea.pointerReleased(this.m_helpTextArea.toRelativeX(x), this.m_helpTextArea.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      base.pointerDragged(x, y, pointerNum);
      if (!this.m_helpTextArea.contains(x, y))
        return false;
      this.m_helpTextArea.pointerDragged(this.m_helpTextArea.toRelativeX(x), this.m_helpTextArea.toRelativeY(y), pointerNum);
      return true;
    }
  }
}
