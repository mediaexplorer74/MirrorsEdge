
// Type: game.BagsPerLevelAchievement
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using text;

#nullable disable
namespace game
{
  public class BagsPerLevelAchievement : LevelAchievement
  {
    protected readonly int m_bags;

    public BagsPerLevelAchievement(int idx, int name, int description, int level, int bags)
      : base(idx, name, description, level)
    {
      this.m_bags = bags;
    }

    public int getBags() => this.m_bags;

    public override StringBuffer getNameStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level level = AppEngine.getLevelData().getLevel(this.m_level);
      string string1 = string.Concat((object) this.m_bags);
      textManager.dynamicString(-12, this.m_name, textManager.getString(level.getName()), string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level level = AppEngine.getLevelData().getLevel(this.m_level);
      string string1 = string.Concat((object) this.m_bags);
      textManager.dynamicString(-12, this.m_description, textManager.getString(level.getName()), string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getCompletedDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level level = AppEngine.getLevelData().getLevel(this.m_level);
      string string1 = string.Concat((object) this.m_bags);
      textManager.dynamicString(-12, this.m_CompletedDescription, textManager.getString(level.getName()), string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }
  }
}
