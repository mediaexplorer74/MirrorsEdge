
// Type: util.Timer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace util
{
  public class Timer : meObject
  {
    private TimerTask m_task;
    private int m_delay;
    private int m_period;
    private bool m_isFixedRate;

    public Timer()
    {
      this.m_task = (TimerTask) null;
      this.m_delay = 0;
      this.m_period = 0;
      this.m_isFixedRate = false;
    }

    public override void Destructor()
    {
      this.m_task = (TimerTask) null;
      base.Destructor();
    }

    public override meClass getClass() => (meClass) new TimerClass();

    protected void setTask(TimerTask task) => this.m_task = task;

    public TimerTask getTask() => this.m_task;

    public void cancel()
    {
      TimerTask task = this.m_task;
      this.setTask((TimerTask) null);
      task?.cancel();
    }

    public int getPeriod() => this.m_period;

    protected int getDelay() => this.m_delay;

    protected bool isFixedRate() => this.m_isFixedRate;

    public void timerCallback(Timer t)
    {
    }
  }
}
