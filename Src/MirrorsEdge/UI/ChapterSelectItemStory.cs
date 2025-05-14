
// Type: UI.ChapterSelectItemStory
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;

#nullable disable
namespace UI
{
  public class ChapterSelectItemStory(Level level) : ChapterSelectItem(level)
  {
    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      int x = left + this.m_x + 4;
      int y = top + this.m_y + this.m_height - 5;
      AppEngine canvas = AppEngine.getCanvas();
      if (this.m_level == AppEngine.getLevelData().getLevel(0))
        return;
      if (this.m_level.isLevelComplete())
        canvas.drawOfStatString(g, this.STAT_FONT, 2113, this.m_level.getNumBagsFound(), this.m_level.getNumTotalBags(), x, y, 65, false);
      else
        canvas.drawStatString(g, this.STAT_FONT, 2113, AppEngine.StatType.STAT_TYPE_OF, -1, x, y, 65, false);
    }
  }
}
