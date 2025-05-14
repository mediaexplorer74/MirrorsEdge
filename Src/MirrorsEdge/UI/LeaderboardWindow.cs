
// Type: UI.LeaderboardWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;
using System;
using text;

#nullable disable
namespace UI
{
  public class LeaderboardWindow : Window
  {
    public const int LABEL_Y_PADDING = 5;
    public const int BORDER_TOP = 95;
    public const int BORDER_HEIGHT = 188;
    public int LABEL_FONT = 2;
    private MajorButton m_backButton;
    private string m_title;
    private string m_subTitle;
    private BorderedElement m_border;
    private LeaderboardListWindow m_leaderboardList;
    private NotchedSlider m_chapterSelect;
    private LeftArrowButton m_prevButton;
    private RightArrowButton m_nextButton;
    private bool m_dragging;
    private int m_lastDragY;
    private int m_lastDragX;
    private int m_curLevel;
    private bool m_hidden;

    public LeaderboardWindow()
    {
      this.m_backButton = new MajorButton(2095, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.m_title = (string) null;
      this.m_subTitle = (string) null;
      this.m_chapterSelect = new NotchedSlider();
      this.m_leaderboardList = new LeaderboardListWindow(this, 5, 102, this.m_width - 10, 181);
      this.m_border = new BorderedElement(5, 95, this.m_width - 10, 188);
      this.m_prevButton = new LeftArrowButton();
      this.m_nextButton = new RightArrowButton();
      this.m_dragging = false;
      this.m_curLevel = 0;
      this.m_hidden = false;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_title = textManager.getString(2077).ToUpper();
      this.m_subTitle = textManager.getString(2083).ToUpper();
      this.m_backButton.setPosition(this.m_width - this.m_backButton.getWidth() - 5, this.m_height - this.m_backButton.getHeight() - 5);
      this.m_chapterSelect.setLooped(false);
      this.m_chapterSelect.setNotchWidth(303);
      this.m_chapterSelect.setRenderExtra(1);
      this.m_chapterSelect.setDimensions(303, 40);
      this.m_chapterSelect.setPosition(this.m_width - this.m_chapterSelect.getWidth() >> 1, 35);
      int y = this.m_chapterSelect.getY() + (this.m_chapterSelect.getHeight() - this.m_prevButton.getHeight() >> 1);
      this.m_prevButton.setPosition(this.m_chapterSelect.getX() - this.m_prevButton.getWidth() - 15, y);
      this.m_nextButton.setPosition(this.m_chapterSelect.getX() + this.m_chapterSelect.getWidth() + 15, y);
      this.m_nextButton.setEnabled(this.m_chapterSelect.canNext());
      this.m_prevButton.setEnabled(this.m_chapterSelect.canPrev());
      LevelData levelData = AppEngine.getLevelData();
      int numUnlockedLevels = levelData.getNumUnlockedLevels();
      int levelNum = levelData.getLevelNum();
      for (int levelIndex = 0; levelIndex < levelNum; ++levelIndex)
      {
        Level level = levelData.getLevel(levelIndex);
        if (levelIndex == 0 || levelIndex < numUnlockedLevels || level.isLevelComplete())
          this.m_chapterSelect.addItem((WindowElement) new ChapterSelectItemSpeedrun(level));
      }
      this.m_curLevel = 0;
    }

    public override void Destructor()
    {
      this.m_backButton.Destructor();
      this.m_backButton = (MajorButton) null;
      this.m_chapterSelect.Destructor();
      this.m_chapterSelect = (NotchedSlider) null;
      this.m_leaderboardList.Destructor();
      this.m_leaderboardList = (LeaderboardListWindow) null;
      this.m_border.Destructor();
      this.m_border = (BorderedElement) null;
      this.m_prevButton.Destructor();
      this.m_prevButton = (LeftArrowButton) null;
      this.m_nextButton.Destructor();
      this.m_nextButton = (RightArrowButton) null;
      this.m_title = (string) null;
      this.m_subTitle = (string) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      this.m_chapterSelect.update(timeStep);
      this.m_nextButton.setEnabled(this.m_chapterSelect.canNext());
      this.m_prevButton.setEnabled(this.m_chapterSelect.canPrev());
      this.m_leaderboardList.update(timeStep);
      if (this.m_curLevel != this.m_chapterSelect.getSelectedNotch() && this.m_chapterSelect.isIdle())
      {
        this.m_curLevel = this.m_chapterSelect.getSelectedNotch();
        this.m_leaderboardList.SetListIdx(this.m_curLevel);
      }
      if (AppEngine.getCanvas().getWindowStore().getNetworkWaitEffect().isAnimating())
        this.m_chapterSelect.setEnabled(false);
      else
        this.m_chapterSelect.setEnabled(true);
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_hidden)
        return;
      this.m_backButton.render(g, top, left);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int lineHeight = textManager.getLineHeight(15);
      int y1 = 2;
      int y2 = y1 + lineHeight;
      StringRenderer stringRenderer1 = textManager.getStringRenderer(14);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(0);
      textManager.drawString(g, this.m_title, 14, 6, y2 + 1, 9);
      stringRenderer1.setColor(color1);
      textManager.drawString(g, this.m_title, 14, 5, y2, 9);
      if (this.m_subTitle != null)
      {
        StringRenderer stringRenderer2 = textManager.getStringRenderer(15);
        int color2 = stringRenderer2.getColor();
        stringRenderer2.setColor(0);
        textManager.drawString(g, this.m_subTitle, 15, 6, y1 + 1, 9);
        stringRenderer2.setColor(color2);
        textManager.drawString(g, this.m_subTitle, 15, 5, y1, 9);
      }
      this.m_chapterSelect.render(g, top, left);
      this.m_prevButton.render(g, top, left);
      this.m_nextButton.render(g, top, left);
      this.m_border.specialFS_Render(g, top, left);
      textManager.drawString(g, 2371, 1, this.m_width >> 1, this.m_border.getY() - 4, 34);
      if (!this.m_chapterSelect.isDraggingButNotAtAnEdgeToKeepQAHappy())
        this.m_leaderboardList.render(g, top, left);
      int y3 = this.m_border.getY() + 5;
      int num = this.m_leaderboardList.getX() + 10;
      textManager.drawString(g, 2347, this.LABEL_FONT, num + 10, y3, 9);
      textManager.drawString(g, 2348, this.LABEL_FONT, num + 70, y3, 9);
      textManager.drawString(g, 2096, this.LABEL_FONT, num + 380, y3, 9);
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      if (this.m_hidden)
      {
        x = y = 0;
        return false;
      }
      x = this.m_backButton.getX() + (this.m_backButton.getWidth() >> 1);
      y = this.m_backButton.getY() + (this.m_backButton.getHeight() >> 1);
      return true;
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      if (this.m_chapterSelect.contains(x, y))
        this.m_chapterSelect.pointerPressed(this.m_chapterSelect.toRelativeX(x), this.m_chapterSelect.toRelativeY(y), pointerNum);
      else if (this.m_prevButton.contains(x, y))
        this.m_prevButton.pointerPressed(this.m_prevButton.toRelativeX(x), this.m_prevButton.toRelativeY(y), pointerNum);
      else if (this.m_nextButton.contains(x, y))
        this.m_nextButton.pointerPressed(this.m_nextButton.toRelativeX(x), this.m_nextButton.toRelativeY(y), pointerNum);
      else if (this.m_backButton.contains(x, y))
        this.m_backButton.pointerPressed(this.m_backButton.toRelativeX(x), this.m_backButton.toRelativeY(y), pointerNum);
      if (this.m_leaderboardList.contains(x, y))
        this.m_leaderboardList.pointerPressed(this.m_leaderboardList.toRelativeX(x), this.m_leaderboardList.toRelativeY(y), pointerNum);
      this.m_lastDragY = y;
      this.m_lastDragX = x;
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      if (this.m_chapterSelect.contains(x, y))
        this.m_chapterSelect.pointerReleased(this.m_chapterSelect.toRelativeX(x), this.m_chapterSelect.toRelativeY(y), pointerNum);
      if (this.m_prevButton.contains(x, y) && this.m_prevButton.isPressed())
      {
        this.m_chapterSelect.prev();
        this.m_prevButton.pointerReleased(this.m_prevButton.toRelativeX(x), this.m_prevButton.toRelativeY(y), pointerNum);
      }
      else if (this.m_prevButton.isPressed())
        this.m_prevButton.unpress();
      if (this.m_nextButton.contains(x, y) && this.m_nextButton.isPressed())
      {
        this.m_chapterSelect.next();
        this.m_nextButton.pointerReleased(this.m_nextButton.toRelativeX(x), this.m_nextButton.toRelativeY(y), pointerNum);
      }
      else if (this.m_nextButton.isPressed())
        this.m_nextButton.unpress();
      if (this.m_backButton.contains(x, y))
      {
        if (AppEngine.getCanvas().getWindowStore().getNetworkWaitEffect().isAnimating())
          AppEngine.getCanvas().getWindowStore().getNetworkWaitEffect().stop();
        this.m_backButton.pointerReleased(this.m_backButton.toRelativeX(x), this.m_backButton.toRelativeY(y), pointerNum);
        this.close(WindowResult.WINDOW_RESULT_EXIT);
      }
      else if (this.m_backButton.isPressed())
        this.m_backButton.unpress();
      if (this.m_leaderboardList.contains(x, y))
        this.m_leaderboardList.pointerReleased(this.m_leaderboardList.toRelativeX(x), this.m_leaderboardList.toRelativeY(y), pointerNum);
      this.m_dragging = false;
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_hidden || Math.Abs(this.m_lastDragY - y) < 10 && Math.Abs(this.m_lastDragX - x) < 10)
        return false;
      this.m_dragging = true;
      if (this.m_chapterSelect.contains(x, y))
        this.m_chapterSelect.pointerDragged(this.m_chapterSelect.toRelativeX(x), this.m_chapterSelect.toRelativeY(y), pointerNum);
      else if (this.m_chapterSelect.isDragging())
        this.m_chapterSelect.pointerReleased(this.m_chapterSelect.toRelativeX(x), this.m_chapterSelect.toRelativeY(y), pointerNum);
      if (!this.m_prevButton.contains(x, y) && this.m_nextButton.isPressed())
        this.m_prevButton.unpress();
      else if (!this.m_nextButton.contains(x, y) && this.m_nextButton.isPressed())
        this.m_nextButton.unpress();
      else if (!this.m_backButton.contains(x, y) && this.m_backButton.isPressed())
        this.m_backButton.unpress();
      if (this.m_leaderboardList.contains(x, y))
        this.m_leaderboardList.pointerDragged(this.m_leaderboardList.toRelativeX(x), this.m_leaderboardList.toRelativeY(y), pointerNum);
      return true;
    }

    public void Hide() => this.m_hidden = true;

    public void Show() => this.m_hidden = false;

    public bool IsHidden() => this.m_hidden;

    public bool IsDragging() => this.m_dragging;
  }
}
