
// Type: UI.ChapterSelectItemSpeedrun
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;

#nullable disable
namespace UI
{
  public class ChapterSelectItemSpeedrun(Level level) : ChapterSelectItem(level)
  {
    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      int x = left + this.m_x + 4;
      int y = top + this.m_y + this.m_height - 5;
      AppEngine.getCanvas().drawStatString(g, this.STAT_FONT, 2111, AppEngine.StatType.STAT_TYPE_POSITIVE_TIME_MILLIS, this.m_level.getBestSpeedRunTimeMillis(), x, y, 65, false);
    }
  }
}
