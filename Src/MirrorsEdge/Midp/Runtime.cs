
// Type: midp.Runtime
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace midp
{
  public class Runtime : meObject
  {
    private int[] m_pointerStatus0 = new int[3];
    private int[] m_pointerStatus1 = new int[3];
    private int[] m_pointerStatus2 = new int[3];
    protected List<MIDlet> m_midlets;
    public static Runtime m_runtime = new Runtime();
    public static int pixelScale = 1;

    protected Runtime()
    {
      this.m_midlets = new List<MIDlet>();
      this.m_pointerStatus0[0] = 0;
      this.m_pointerStatus1[0] = 0;
      this.m_pointerStatus2[0] = 0;
    }

    public override void Destructor()
    {
      for (int count = this.m_midlets.Count; count > 0; count = this.m_midlets.Count)
        this.destroyMIDlet(true);
      base.Destructor();
    }

    public override meClass getClass() => (meClass) new RuntimeClass();

    public static Runtime getRuntime() => Runtime.m_runtime;

    public void setMIDlet(MIDlet midlet, Display display)
    {
      this.getCurrentMIDlet()?.pauseApp();
      midlet.setDisplay(display);
      this.addMIDlet(midlet);
    }

        public void startMIDlet()
        {
            try
            {
              MIDlet curMidlet = this.getCurrentMIDlet();
              if (curMidlet != null)
                curMidlet.startApp();
            }
            catch { }
        }

        public void pauseMIDlet() => this.getCurrentMIDlet().pauseApp();

    public void destroyMIDlet(bool unconditional)
    {
      MIDlet currentMiDlet = this.getCurrentMIDlet();
      if (currentMiDlet == null)
        return;
      currentMiDlet.destroyApp(unconditional);
      this.removeMIDlet(currentMiDlet);
    }

    public virtual bool eventLoop() => this.m_midlets.Count != 0;

    public void keyPressed(int keyCode) => this.getCurrentDisplayable()?.keyPressed(keyCode);

    public void keyReleased(int keyCode) => this.getCurrentDisplayable()?.keyReleased(keyCode);

    public void OnHardBackKeyEvent() => this.getCurrentDisplayable()?.OnHardBackKeyEvent();

    public void pointerPressed(int x, int y, int pointerNum)
    {
      int[] pointerStatus = this.getPointerStatus(pointerNum);
      pointerStatus[0] = 1;
      pointerStatus[1] = x;
      pointerStatus[2] = y;
      this.getCurrentDisplayable()?.pointerPressed(x, y, pointerNum);
    }

    public void pointerDragged(int x, int y, int pointerNum)
    {
      int[] pointerStatus = this.getPointerStatus(pointerNum);
      pointerStatus[1] = x;
      pointerStatus[2] = y;
      this.getCurrentDisplayable()?.pointerDragged(x, y, pointerNum);
    }

    public void pointerReleased(int x, int y, int pointerNum)
    {
      int[] pointerStatus = this.getPointerStatus(pointerNum);
      pointerStatus[0] = 0;
      pointerStatus[1] = x;
      pointerStatus[2] = y;
      this.getCurrentDisplayable()?.pointerReleased(x, y, pointerNum);
    }

    public void pointerClearAll()
    {
      for (int pointerNum = 0; pointerNum < 3; ++pointerNum)
        this.getPointerStatus(pointerNum)[0] = 0;
    }

    public int[] getPointerStatus(int pointerNum)
    {
      switch (pointerNum)
      {
        case 0:
          return this.m_pointerStatus0;
        case 1:
          return this.m_pointerStatus1;
        case 2:
          return this.m_pointerStatus2;
        default:
          return this.m_pointerStatus0;
      }
    }

    protected Displayable getCurrentDisplayable()
    {
      MIDlet currentMiDlet = this.getCurrentMIDlet();
      if (currentMiDlet != null)
      {
        Display display = currentMiDlet.getDisplay();
        if (display != null)
          return display.getCurrent();
      }
      return (Displayable) null;
    }

    public void destroyMIDlet(MIDlet midlet)
    {
      if (midlet == null)
        return;
      this.removeMIDlet(midlet);
    }

    protected MIDlet getCurrentMIDlet()
    {
        MIDlet res = (MIDlet)null;

        if (this.m_midlets != null)
        {
            if (this.m_midlets.Count > 0)
                res = this.m_midlets.Last<MIDlet>();
        }
      return res;
    }

    private MIDlet Graphics3D_getMIDlet() => Runtime.getRuntime().getCurrentMIDlet();

    protected void addMIDlet(MIDlet midlet) => this.m_midlets.Add(midlet);

    protected virtual void removeMIDlet(MIDlet midlet) => this.m_midlets.Remove(midlet);
  }
}
