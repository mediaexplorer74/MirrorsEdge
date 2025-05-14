// Decompiled with JetBrains decompiler
// Type: midp.MIDlet
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System.Collections.Generic;

#nullable disable
namespace midp
{
  public abstract class MIDlet : meObject
  {
    private Display m_display;

    public Display getDisplay() => this.m_display;

    public void setDisplay(Display display) => this.m_display = display;

    protected MIDlet() => this.m_display = (Display) null;

    public void notifyDestroyed() => Runtime.getRuntime().destroyMIDlet(this);

    public void notifyPaused()
    {
    }

    public void resumeRequest()
    {
    }

    public abstract void destroyApp(bool unconditional);

    public abstract void pauseApp();

    public abstract void startApp();

    public abstract void receivedNotification(Dictionary<string, string> notificationOptions);

    public override void Destructor()
    {
      this.m_display = (Display) null;
      base.Destructor();
    }

    public override meClass getClass() => (meClass) new MIDletClass();
  }
}
