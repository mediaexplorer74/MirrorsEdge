// Decompiled with JetBrains decompiler
// Type: UI.AboutMenu
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace UI
{
  public class AboutMenu : TitledWindow
  {
    public const int BUTTON_PADDING = 10;
    private bool m_hidden;
    private AboutMenuPanel m_menuPanel;
    private LeftArrowButton m_prevButton;
    private RightArrowButton m_nextButton;

    public AboutMenu()
      : base(2049, 2051)
    {
      this.m_hidden = false;
      this.m_menuPanel = new AboutMenuPanel(this);
      this.m_nextButton = new RightArrowButton();
      this.m_prevButton = new LeftArrowButton();
      this.m_backgroundBorder.setPosition(52, 93);
      this.m_backgroundBorder.setDimensions(433, 133);
      this.m_menuPanel.setPosition(this.m_width - this.m_menuPanel.getWidth() >> 1, this.m_backgroundBorder.getY() + (this.m_backgroundBorder.getHeight() - this.m_menuPanel.getHeight() >> 1));
      this.m_prevButton.setPosition(this.m_backgroundBorder.getX() + 10, this.m_menuPanel.getY() + (this.m_menuPanel.getHeight() - this.m_prevButton.getHeight() >> 1));
      this.m_nextButton.setPosition(this.m_backgroundBorder.getX() + this.m_backgroundBorder.getWidth() - this.m_prevButton.getWidth() - 10, this.m_menuPanel.getY() + (this.m_menuPanel.getHeight() - this.m_nextButton.getHeight() >> 1));
    }

    public override void Destructor()
    {
      this.m_menuPanel.Destructor();
      this.m_menuPanel = (AboutMenuPanel) null;
      this.m_nextButton.Destructor();
      this.m_nextButton = (RightArrowButton) null;
      this.m_prevButton.Destructor();
      this.m_prevButton = (LeftArrowButton) null;
      base.Destructor();
    }

    public void setHidden(bool hide) => this.m_hidden = hide;

    public override void update(int timeStep)
    {
      if (this.m_hidden)
        return;
      base.update(timeStep);
      this.m_menuPanel.update(timeStep);
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_hidden)
        return;
      base.render(g, top, left);
      this.m_menuPanel.render(g, top, left);
      this.m_nextButton.render(g, top, left);
      this.m_prevButton.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerPressed(x, y, pointerNum);
      if (this.m_menuPanel.contains(x, y))
        this.m_menuPanel.pointerPressed(this.m_menuPanel.toRelativeX(x), this.m_menuPanel.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerReleased(x, y, pointerNum);
      if (this.m_nextButton.contains(x, y))
      {
        this.m_menuPanel.next();
        this.m_nextButton.pointerReleased(x, y, pointerNum);
      }
      else if (this.m_prevButton.contains(x, y))
      {
        this.m_menuPanel.prev();
        this.m_prevButton.pointerReleased(x, y, pointerNum);
      }
      else if (this.m_menuPanel.contains(x, y))
        this.m_menuPanel.pointerReleased(x, y, pointerNum);
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerDragged(x, y, pointerNum);
      if (this.m_menuPanel.contains(x, y))
        this.m_menuPanel.pointerDragged(this.m_menuPanel.toRelativeX(x), this.m_menuPanel.toRelativeY(y), pointerNum);
      else if (this.m_menuPanel.isDragging())
        this.m_menuPanel.pointerReleased(this.m_menuPanel.toRelativeX(x), this.m_menuPanel.toRelativeY(y), pointerNum);
      return true;
    }
  }
}
