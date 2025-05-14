// Decompiled with JetBrains decompiler
// Type: game.TimedEventsAchievement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using text;

#nullable disable
namespace game
{
  public class TimedEventsAchievement : Achievement
  {
    private int m_timeLimitSecs;
    private int[] m_eventTimeArray;

    public TimedEventsAchievement(
      int idx,
      int name,
      int description,
      int numEvents,
      int timeSecs)
      : base(idx, name, description)
    {
      this.m_timeLimitSecs = timeSecs;
      this.m_eventTimeArray = new int[numEvents];
    }

    public void levelStarted()
    {
      int length = this.m_eventTimeArray.Length;
      for (int index = 0; index != length; ++index)
        this.m_eventTimeArray[index] = -1;
    }

    public void eventHappended(int raceTimeSecs)
    {
      if (this.isComplete())
        return;
      int index1 = this.m_eventTimeArray.Length - 1;
      for (int index2 = 0; index2 != index1; ++index2)
        this.m_eventTimeArray[index2] = this.m_eventTimeArray[index2 + 1];
      this.m_eventTimeArray[index1] = raceTimeSecs;
      if (this.m_eventTimeArray[0] == -1 || raceTimeSecs - this.m_eventTimeArray[0] > this.m_timeLimitSecs)
        return;
      AppEngine.getAchievementData().registerAchievementComplete(this.m_idx);
    }

    public override StringBuffer getNameStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string0 = string.Concat((object) this.m_eventTimeArray.Length);
      string string1 = string.Concat((object) this.m_timeLimitSecs);
      textManager.dynamicString(-12, this.m_name, string0, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string0 = string.Concat((object) this.m_eventTimeArray.Length);
      string string1 = string.Concat((object) this.m_timeLimitSecs);
      textManager.dynamicString(-12, this.m_description, string0, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }

    public override StringBuffer getCompletedDescriptionStringBuffer()
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      string string0 = string.Concat((object) this.m_eventTimeArray.Length);
      string string1 = string.Concat((object) this.m_timeLimitSecs);
      textManager.dynamicString(-12, this.m_CompletedDescription, string0, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      return stringBuffer;
    }
  }
}
