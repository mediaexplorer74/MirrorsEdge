
// Type: UI.AchievementWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class AchievementWindow : TitledWindow
  {
    public const int WINDOW_PADDING_X = 7;
    public const int WINDOW_HEIGHT = 240;
    public int FONT_COLUMN_TITLE = 17;
    public int FONT_COLUMN_COMPLETE = 17;
    private AchievementsList m_achievementsList;

    public AchievementWindow()
      : base(2085, 2083)
    {
      this.m_achievementsList = new AchievementsList(9, (this.m_height - 240 >> 1) + 20, this.m_width - 14 - 4, 215);
      this.m_backgroundBorder.setPosition(7, this.m_height - 240 >> 1);
      this.m_backgroundBorder.setDimensions(this.m_width - 14, 240);
      this.m_useFS_render_for_Background = true;
    }

    public override void Destructor()
    {
      this.m_achievementsList.Destructor();
      this.m_achievementsList = (AchievementsList) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      this.m_achievementsList.update(timeStep);
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      textManager.drawString(g, 2158, this.FONT_COLUMN_TITLE, 15, this.m_backgroundBorder.getY() + 3, 9);
      textManager.drawString(g, 2159, this.FONT_COLUMN_COMPLETE, 518, this.m_backgroundBorder.getY() + 3, 12);
      string str = AppEngine.getAchievementData().getGamePointsEarned().ToString() + "/" + (object) AchievementData.m_totalGamePoints;
      StringRenderer stringRenderer = textManager.getStringRenderer(14);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(0);
      textManager.drawString(g, str, 14, 526, 11, 12);
      stringRenderer.setColor(color);
      textManager.drawString(g, str, 14, 525, 10, 12);
      this.m_achievementsList.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      base.pointerPressed(x, y, pointerNum);
      if (!this.m_achievementsList.contains(x, y))
        return false;
      this.m_achievementsList.pointerPressed(x - this.m_achievementsList.getX(), y - this.m_achievementsList.getY(), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      base.pointerReleased(x, y, pointerNum);
      if (!this.m_achievementsList.contains(x, y))
        return false;
      this.m_achievementsList.pointerReleased(x - this.m_achievementsList.getX(), y - this.m_achievementsList.getY(), pointerNum);
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      base.pointerDragged(x, y, pointerNum);
      if (!this.m_achievementsList.contains(x, y))
        return false;
      this.m_achievementsList.pointerDragged(x - this.m_achievementsList.getX(), y - this.m_achievementsList.getY(), pointerNum);
      return true;
    }
  }
}
