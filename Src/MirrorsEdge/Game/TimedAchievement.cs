
// Type: game.TimedAchievement
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using text;

#nullable disable
namespace game
{
  public class TimedAchievement : Achievement
  {
    public const int NOT_TIMING = -1;
    private float m_startTimeSecs;
    private int m_targetTimeSecs;

    public TimedAchievement(int idx, int name, int description, int targetTimeSecs)
      : base(idx, name, description)
    {
      this.m_startTimeSecs = -1f;
      this.m_targetTimeSecs = targetTimeSecs;
    }

    public void runTimer(int raceTimeSecs)
    {
      if ((double) this.m_startTimeSecs == -1.0)
      {
        this.m_startTimeSecs = (float) raceTimeSecs;
      }
      else
      {
        if (this.isComplete() || (double) this.m_targetTimeSecs > (double) raceTimeSecs - (double) this.m_startTimeSecs)
          return;
        AppEngine.getAchievementData().registerAchievementComplete(this.m_idx);
      }
    }

    public void endTimer() => this.m_startTimeSecs = -1f;

    public override StringBuffer getNameStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string1 = string.Concat((object) this.m_targetTimeSecs);
      textManager.dynamicString(-12, this.m_name, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string1 = string.Concat((object) this.m_targetTimeSecs);
      textManager.dynamicString(-12, this.m_description, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getCompletedDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string1 = string.Concat((object) this.m_targetTimeSecs);
      textManager.dynamicString(-12, this.m_CompletedDescription, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }
  }
}
