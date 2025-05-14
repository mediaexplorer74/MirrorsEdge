
// Type: UI.ChapterSelectWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace UI
{
  public abstract class ChapterSelectWindow : TitledWindow
  {
    protected MajorButton m_okButton;
    protected LeftArrowButton m_prevButton;
    protected RightArrowButton m_nextButton;
    protected NotchedSlider m_chapterPanel;

    public ChapterSelectWindow(bool speedRun)
      : base(2073, 2071)
    {
      this.m_okButton = new MajorButton(2094);
      this.m_prevButton = new LeftArrowButton();
      this.m_nextButton = new RightArrowButton();
      this.m_chapterPanel = new NotchedSlider();
      this.setShowBackground(false);
      if (speedRun)
        this.setTitles(2073, 2076);
      this.m_backgroundBorder.setPosition(70, 56);
      this.m_backgroundBorder.setDimensions(0, 0);
      this.m_chapterPanel.setLooped(false);
      this.m_chapterPanel.setNotchWidth(303);
      this.m_chapterPanel.setRenderExtra(1);
      this.m_chapterPanel.setPosition(70, 60);
      this.m_chapterPanel.setDimensions(303, 40);
      int y = this.m_chapterPanel.getY() + (this.m_chapterPanel.getHeight() - this.m_prevButton.getHeight() >> 1);
      this.m_prevButton.setPosition(this.m_chapterPanel.getX() - this.m_prevButton.getWidth() - 15, y);
      this.m_nextButton.setPosition(this.m_chapterPanel.getX() + this.m_chapterPanel.getWidth() + 15, y);
      this.m_nextButton.setEnabled(this.m_chapterPanel.canNext());
      this.m_prevButton.setEnabled(this.m_chapterPanel.canPrev());
      this.m_okButton.setPosition(this.m_backButton.getX() - this.m_okButton.getWidth() - 8, this.m_backButton.getY());
    }

    public override void Destructor()
    {
      this.m_okButton.Destructor();
      this.m_okButton = (MajorButton) null;
      this.m_prevButton.Destructor();
      this.m_prevButton = (LeftArrowButton) null;
      this.m_nextButton.Destructor();
      this.m_nextButton = (RightArrowButton) null;
      this.m_chapterPanel.Destructor();
      this.m_chapterPanel = (NotchedSlider) null;
      base.Destructor();
    }

    public abstract void onSelected();

    public override void update(int timeStep)
    {
      base.update(timeStep);
      this.m_chapterPanel.update(timeStep);
      this.m_nextButton.setEnabled(this.m_chapterPanel.canNext());
      this.m_prevButton.setEnabled(this.m_chapterPanel.canPrev());
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      this.m_chapterPanel.render(g, top, left);
      this.m_prevButton.render(g, top, left);
      this.m_nextButton.render(g, top, left);
      this.m_okButton.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      base.pointerPressed(x, y, pointerNum);
      if (this.m_chapterPanel.contains(x, y))
        this.m_chapterPanel.pointerPressed(this.m_chapterPanel.toRelativeX(x), this.m_chapterPanel.toRelativeY(y), pointerNum);
      else if (this.m_prevButton.contains(x, y))
        this.m_prevButton.pointerPressed(this.m_prevButton.toRelativeX(x), this.m_prevButton.toRelativeY(y), pointerNum);
      else if (this.m_nextButton.contains(x, y))
        this.m_nextButton.pointerPressed(this.m_nextButton.toRelativeX(x), this.m_nextButton.toRelativeY(y), pointerNum);
      else if (this.m_okButton.contains(x, y))
        this.m_okButton.pointerPressed(this.m_okButton.toRelativeX(x), this.m_okButton.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      base.pointerReleased(x, y, pointerNum);
      if (this.m_chapterPanel.contains(x, y))
        this.m_chapterPanel.pointerReleased(this.m_chapterPanel.toRelativeX(x), this.m_chapterPanel.toRelativeY(y), pointerNum);
      else if (this.m_prevButton.contains(x, y))
      {
        this.m_chapterPanel.prev();
        this.m_prevButton.pointerReleased(this.m_prevButton.toRelativeX(x), this.m_prevButton.toRelativeY(y), pointerNum);
      }
      if (this.m_nextButton.contains(x, y))
      {
        this.m_chapterPanel.next();
        this.m_nextButton.pointerReleased(this.m_nextButton.toRelativeX(x), this.m_nextButton.toRelativeY(y), pointerNum);
      }
      else if (this.m_nextButton.isPressed())
        this.m_nextButton.unpress();
      if (this.m_okButton.contains(x, y))
      {
        this.m_okButton.pointerReleased(this.m_okButton.toRelativeX(x), this.m_okButton.toRelativeY(y), pointerNum);
        this.onSelected();
        this.close(WindowResult.WINDOW_RESULT_NONE);
      }
      else if (this.m_okButton.isPressed())
        this.m_okButton.unpress();
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      base.pointerDragged(x, y, pointerNum);
      if (this.m_chapterPanel.contains(x, y))
        this.m_chapterPanel.pointerDragged(this.m_chapterPanel.toRelativeX(x), this.m_chapterPanel.toRelativeY(y), pointerNum);
      else if (this.m_chapterPanel.isDragging())
        this.m_chapterPanel.pointerReleased(this.m_chapterPanel.toRelativeX(x), this.m_chapterPanel.toRelativeY(y), pointerNum);
      if (this.m_prevButton.contains(x, y))
        this.m_prevButton.pointerPressed(this.m_prevButton.toRelativeX(x), this.m_prevButton.toRelativeY(y), pointerNum);
      else if (this.m_nextButton.contains(x, y))
        this.m_nextButton.pointerPressed(this.m_nextButton.toRelativeX(x), this.m_nextButton.toRelativeY(y), pointerNum);
      else if (this.m_okButton.contains(x, y))
        this.m_okButton.pointerDragged(this.m_okButton.toRelativeX(x), this.m_okButton.toRelativeY(y), pointerNum);
      else if (!this.m_okButton.isPressed())
        this.m_okButton.unpress();
      return true;
    }
  }
}
