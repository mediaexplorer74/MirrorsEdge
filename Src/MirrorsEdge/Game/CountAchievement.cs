
// Type: game.CountAchievement
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using text;

#nullable disable
namespace game
{
  public class CountAchievement : Achievement
  {
    protected int m_count;

    public CountAchievement(int idx, int name, int description, int count)
      : base(idx, name, description)
    {
      this.m_count = count;
    }

    public void setCount(int count) => this.m_count = count;

    public int getCount() => this.m_count;

    public override StringBuffer getNameStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string1 = string.Concat((object) this.m_count);
      textManager.dynamicString(-12, this.m_name, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string stringWithThousandSep = textManager.convertIntToStringWithThousandSep(this.m_count);
      textManager.dynamicString(-12, this.m_description, stringWithThousandSep);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getCompletedDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string stringWithThousandSep = textManager.convertIntToStringWithThousandSep(this.m_count);
      textManager.dynamicString(-12, this.m_CompletedDescription, stringWithThousandSep);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }
  }
}
