
// Type: game.MonkeyApp
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using ea;
using midp;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace game
{
  public class MonkeyApp : MIDlet
  {
    private bool m_gameHasStarted;
    private AppEngine m_engine;
    private Display m_display;
    private volatile bool m_gettingInput;
    private long timeStartFrame;

    public MonkeyApp()
    {
      this.m_gameHasStarted = false;
      this.m_engine = (AppEngine) null;
      this.m_display = (Display) null;
      this.m_gettingInput = false;
      this.m_gameHasStarted = false;
      AppEngine.createAppEngine(this);
      this.m_engine = AppEngine.getCanvas();
      EASpywareManager.getInstance().appStarted();
    }

    public override void Destructor() => base.Destructor();

    public override void destroyApp(bool unconditional)
    {
      //while (this.m_gettingInput)
      //{
      //    //Thread.Sleep(1);
      //}
      EASpywareManager.getInstance().logEvent(20000);
      this.close();
      this.notifyDestroyed();
    }

    public override void pauseApp()
    {
        this.m_engine.pauseGame();
    }

    public override void startApp()
    {
      this.timeStartFrame = this.GetTime();
      this.m_display = Display.getDisplay((MIDlet) this);
      this.m_engine.setFullScreenMode(true);
      this.m_engine.setFullScreenMode(true);
      bool flag = false;
      if (!this.m_gameHasStarted)
      {
        this.m_engine.start();
        this.m_gameHasStarted = true;
        this.m_engine.startThread();
        flag = true;
      }
      if (this.m_engine.isPaused())
      {
        this.m_engine.resumeGame();
        this.m_engine.startThread();
      }
      if (!flag)
        return;
      this.m_display.setCurrent((Displayable) this.m_engine);
    }

    private void close()
    {
      if (this.m_engine != null)
        this.m_engine.end();
      if (this.m_display == null)
        return;
      Display display = this.m_display;
      this.m_display = (Display) null;
      display.setCurrent((Displayable) null);
    }

    public long GetTime() => DateTime.Now.Ticks / 10000L;

    public void update()
    {
      long time = this.GetTime();
      int frameTime = Math.Max(0, (int) (time - this.timeStartFrame));
      if (frameTime < 34)
      {
        //Thread.Sleep(34 - frameTime);
        this.timeStartFrame += 34L;
        frameTime = 34;
      }
      else
        this.timeStartFrame = time;
      if (!this.m_engine.m_gameRunning || this.m_engine.m_paused)
        return;
      this.m_engine.m_updateScheduled = true;
      this.m_engine.runLoop(frameTime);
      this.m_engine.m_updateScheduled = false;
    }

    public override void receivedNotification(Dictionary<string, string> notificationOptions)
    {
      AppEngine engine = this.m_engine;
    }

    public string getInputString(string title, int maxSize) => "Some debug name 1";
  }
}
