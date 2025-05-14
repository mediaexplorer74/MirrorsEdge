// Decompiled with JetBrains decompiler
// Type: midp.Player
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System.Collections.Generic;

#nullable disable
namespace midp
{
  public abstract class Player : meObject, Controllable
  {
    public const int CLOSED = 0;
    public const int UNREALIZED = 100;
    public const int REALIZED = 200;
    public const int PREFETCHED = 300;
    public const int STARTED = 400;
    public const int PAUSED = 500;
    public const int TIME_UNKNOWN = -1;
    protected int m_state;
    private int m_loopCount;
    private string m_contentType;
    private List<PlayerListener> m_listeners;

    protected Player()
    {
      this.m_state = 100;
      this.m_loopCount = 0;
      this.m_contentType = (string) null;
      this.m_listeners = new List<PlayerListener>();
    }

    protected void stateTransition(int state) => this.m_state = state;

    protected void setContentType(string contentType) => this.m_contentType = contentType;

    protected abstract bool isExclusive();

    protected abstract void update(bool exclusiveResourceNeedsLock);

    protected abstract Player duplicate();

    protected int getPlayerListenerCount() => this.m_listeners.Count;

    protected PlayerListener getPlayerListener(int index) => this.m_listeners[index];

    private void notifyListeners(string _event, object eventData)
    {
      int count = this.m_listeners.Count;
      for (int index = 0; index < count; ++index)
        this.m_listeners[index]?.playerUpdate(this, _event, eventData);
    }

    public override void Destructor()
    {
    }

    public override meClass getClass() => (meClass) new PlayerClass();

    public void addPlayerListener(PlayerListener playerListener)
    {
      this.m_listeners.Add(playerListener);
    }

    public abstract void close();

    public abstract void deallocate();

    public string getContentType() => this.m_contentType;

    public abstract long getDuration();

    public abstract long getMediaTime();

    public virtual void prefetch()
    {
      if (this.m_state != 100)
        return;
      this.realize();
    }

    public abstract void realize();

    public virtual int getState() => this.m_state;

    public void removePlayerListener(PlayerListener playerListener)
    {
      this.m_listeners.Remove(playerListener);
    }

    public virtual void setLoopCount(int count) => this.m_loopCount = count;

    public virtual int getLoopCount() => this.m_loopCount;

    public abstract long setMediaTime(long now);

    public virtual void start()
    {
      if (this.m_state != 100 && this.m_state != 200)
        return;
      this.prefetch();
    }

    public virtual void stop()
    {
      this.realize();
      if (this.getState() == 200)
        return;
      this.stateTransition(200);
    }

    public virtual void pause()
    {
    }

    public virtual void unpause()
    {
    }

    public virtual Control getControl(string controlType) => (Control) null;

    public virtual VolumeControl getVolumeControl() => (VolumeControl) null;

    public virtual PitchControl getPitchControl() => (PitchControl) null;

    public virtual PositionControl getPositionControl() => (PositionControl) null;
  }
}
