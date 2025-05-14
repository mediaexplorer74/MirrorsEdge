// Decompiled with JetBrains decompiler
// Type: game.LevelAchievement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using text;

#nullable disable
namespace game
{
  public class LevelAchievement : Achievement
  {
    protected readonly int m_level;

    public LevelAchievement(int idx, int name, int description, int level)
      : base(idx, name, description)
    {
      this.m_level = level;
    }

    public int getLevel() => this.m_level;

    public override StringBuffer getNameStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level level = AppEngine.getLevelData().getLevel(this.m_level);
      textManager.dynamicString(-12, this.m_name, textManager.getString(level.getName()));
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level level = AppEngine.getLevelData().getLevel(this.m_level);
      textManager.dynamicString(-12, this.m_description, textManager.getString(level.getName()));
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getCompletedDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level level = AppEngine.getLevelData().getLevel(this.m_level);
      textManager.dynamicString(-12, this.m_CompletedDescription, textManager.getString(level.getName()));
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }
  }
}
