// Decompiled with JetBrains decompiler
// Type: UI.EndOfGameResultsWindow
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class EndOfGameResultsWindow : Window
  {
    public const int TOP_MARGIN = 20;
    public const int TITLE_Y = 29;
    public const int TABLE_CHAPTER_X = 48;
    public const int TABLE_BAGS_X = 300;
    public const int TABLE_TOTAL_X = 410;
    public const int TABLE_HEADING_Y = 55;
    public const int TITLE_LINE_FIRST_Y = 75;
    public const int TITLE_LINE_OFFSET_Y = 17;
    public const int TITLE_TOTAL_BAGS_Y = 77;
    public const int FONT_TITLE = 26;
    public const int FONT_TABLE_HEADER = 15;
    public const int FONT_TABLE_BODY = 2;
    public const int FONT_TOTAL_VALUE = 26;
    private MajorButton m_next;
    private string m_titleString;
    private string m_chapterString;
    private string m_bagsString;

    public EndOfGameResultsWindow()
    {
      this.m_next = new MajorButton(2308);
      AppEngine canvas = AppEngine.getCanvas();
      int width = canvas.getWidth();
      int height = canvas.getHeight();
      this.m_next.setPosition(width - 5 - this.m_next.getWidth(), height - 5 - this.m_next.getHeight());
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_titleString = textManager.getString(2332).ToUpper();
      this.m_chapterString = textManager.getString(2331).ToUpper();
      this.m_bagsString = textManager.getString(2114).ToUpper();
    }

    public override void Destructor()
    {
      this.m_next.Destructor();
      this.m_next = (MajorButton) null;
      this.m_titleString = (string) null;
      this.m_chapterString = (string) null;
      this.m_bagsString = (string) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      AppEngine canvas = AppEngine.getCanvas();
      int width = canvas.getWidth();
      LevelData levelData = AppEngine.getLevelData();
      TextManager textManager = canvas.getTextManager();
      textManager.drawString(g, this.m_titleString, 26, width >> 1, 29, 66);
      textManager.drawString(g, this.m_chapterString, 15, 48, 55, 65);
      textManager.drawString(g, this.m_bagsString, 15, 300, 55, 66);
      int numerator = 0;
      int denominator = 0;
      int levelNum = levelData.getLevelNum();
      int y = 75;
      for (int levelIndex = 0; levelIndex != levelNum; ++levelIndex)
      {
        Level level = levelData.getLevel(levelIndex);
        textManager.drawString(g, level.getName(), 2, 48, y, 65);
        int numBagsFound = level.getNumBagsFound();
        int numTotalBags = level.getNumTotalBags();
        numerator += numBagsFound;
        denominator += numTotalBags;
        canvas.drawOfStatString(g, 2, 2048, numBagsFound, numTotalBags, 300, y, 66, false);
        y += 17;
      }
      textManager.drawString(g, 2333, 2, 410, 55, 66);
      canvas.drawOfStatString(g, 26, 2048, numerator, denominator, 410, 77, 66, false);
      this.m_next.render(g, 0, 0);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (!this.m_next.contains(x, y) || this.m_next.getStringId() == -1)
        return false;
      this.m_next.pointerPressed(this.m_next.toRelativeX(x), this.m_next.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (!this.m_next.contains(x, y) || this.m_next.getStringId() == -1)
        return false;
      this.m_next.pointerReleased(this.m_next.toRelativeX(x), this.m_next.toRelativeY(y), pointerNum);
      this.close(WindowResult.WINDOW_RESULT_POSITIVE);
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_next.isPressed() && !this.m_next.contains(x, y))
        this.m_next.unpress();
      return false;
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      x = this.m_next.getX() + (this.m_next.getWidth() >> 1);
      y = this.m_next.getY() + (this.m_next.getHeight() >> 1);
      return true;
    }
  }
}
