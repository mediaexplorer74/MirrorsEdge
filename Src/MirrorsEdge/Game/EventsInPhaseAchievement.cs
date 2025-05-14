// Decompiled with JetBrains decompiler
// Type: game.EventsInPhaseAchievement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game
{
  public class EventsInPhaseAchievement : Achievement
  {
    public const int PHASE_OFF = -1;
    public const int PHASE_ON = 0;
    private int m_numEventsInPhase;
    private int m_numEventsRequirement;

    public EventsInPhaseAchievement(int idx, int name, int description, int numEvents)
      : base(idx, name, description)
    {
      this.m_numEventsInPhase = -1;
      this.m_numEventsRequirement = numEvents;
    }

    public void phaseOn()
    {
      if (this.m_numEventsInPhase != -1)
        return;
      this.m_numEventsInPhase = 0;
    }

    public void phaseOff() => this.m_numEventsInPhase = -1;

    public void eventHappended()
    {
      if (this.isComplete() || this.m_numEventsInPhase == -1 || ++this.m_numEventsInPhase != this.m_numEventsRequirement)
        return;
      AppEngine.getAchievementData().registerAchievementComplete(this.m_idx);
    }
  }
}
