// Decompiled with JetBrains decompiler
// Type: util.TimerTask
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace util
{
  public class TimerTask : Runnable
  {
    private long m_lastScheduledTime;
    private bool m_repeatScheduled;
    private bool m_hasRun;
    private bool m_cancelFlag;

    protected TimerTask()
    {
      this.m_lastScheduledTime = 0L;
      this.m_repeatScheduled = false;
      this.m_hasRun = false;
      this.m_cancelFlag = false;
    }

    public override void Destructor()
    {
    }

    public override meClass getClass() => (meClass) new TimerTaskClass();

    public bool cancel()
    {
      if (this.m_cancelFlag)
        return false;
      this.m_cancelFlag = true;
      return !this.m_repeatScheduled && !this.m_hasRun || this.m_repeatScheduled;
    }

    public override void run() => this.m_hasRun = true;

    public long scheduledExecutionTime() => this.m_lastScheduledTime;

    public bool wantsToCancel() => this.m_cancelFlag;

    public void setLastScheduledExecutionTime(long time) => this.m_lastScheduledTime = time;
  }
}
