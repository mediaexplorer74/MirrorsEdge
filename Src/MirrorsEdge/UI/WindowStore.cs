
// Type: UI.WindowStore
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using mirrorsedge_wp7;
using System.Collections.Generic;

#nullable disable
namespace UI
{
  public class WindowStore
  {
    private WindowResult m_lastResult;
    private List<Window> m_windows;
    private List<Window> m_newWindows;
    private ButtonEffect m_buttonEffect;
    private NetworkWaitEffect m_networkWaitEffect;
    private object m_syncMutex;
    private object m_syncNewWindowsMutex;
    private int m_waitingForMainThread;

    public WindowStore()
    {
      this.m_lastResult = WindowResult.WINDOW_RESULT_NONE;
      this.m_windows = new List<Window>();
      this.m_newWindows = new List<Window>();
      this.m_buttonEffect = new ButtonEffect();
      this.m_networkWaitEffect = new NetworkWaitEffect();
      this.m_syncMutex = new object();
      this.m_syncNewWindowsMutex = new object();
      this.m_waitingForMainThread = 0;
    }

    public void Destructor()
    {
      foreach (WindowElement window in this.m_windows)
        window.Destructor();
      this.m_windows.Clear();
      this.m_newWindows.Clear();
      this.m_buttonEffect = (ButtonEffect) null;
      this.m_networkWaitEffect = (NetworkWaitEffect) null;
      this.m_syncMutex = (object) null;
      this.m_syncNewWindowsMutex = (object) null;
    }

    public Window createWindow(WindowStore.WindowType type, int x, int y, int width)
    {
      return this.createWindow(type, x, y, width, 0);
    }

    public Window createWindow(WindowStore.WindowType type, int x, int y)
    {
      return this.createWindow(type, x, y, 0, 0);
    }

    public Window createWindow(WindowStore.WindowType type, int x)
    {
      return this.createWindow(type, x, 0, 0, 0);
    }

    public Window createWindow(WindowStore.WindowType type) => this.createWindow(type, 0, 0, 0, 0);

    public Window createWindow(WindowStore.WindowType type, int x, int y, int width, int height)
    {
      Window window = (Window) null;
      switch (type)
      {
        case WindowStore.WindowType.GENERIC_WINDOW:
          window = new Window(x, y, width, height);
          break;
        case WindowStore.WindowType.WINDOW_WITH_BACKGROUND:
          window = (Window) new WindowWithBackground(x, y, width, height);
          break;
        case WindowStore.WindowType.GAME_OPTIONS:
          window = (Window) new GameOptions();
          break;
        case WindowStore.WindowType.SOUND_OPTIONS:
          window = (Window) new SoundOptions();
          break;
        case WindowStore.WindowType.ACHIEVEMENTS_WINDOW:
          window = (Window) new AchievementWindow();
          break;
        case WindowStore.WindowType.STORY_END_OF_LEVEL:
          window = (Window) new StoryEndOfLevelPrompt();
          break;
        case WindowStore.WindowType.SPEEDRUN_END_OF_LEVEL:
          window = (Window) new SpeedRunEndOfLevelPrompt();
          break;
        case WindowStore.WindowType.CHALLENGE_END_OF_LEVEL:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new ChallengeEndOfLevelPrompt();
            break;
          }
          break;
        case WindowStore.WindowType.END_OF_GAME_RESULTS:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new EndOfGameResultsWindow();
            break;
          }
          break;
        case WindowStore.WindowType.ABOUT_MENU:
          window = (Window) new AboutMenu();
          break;
        case WindowStore.WindowType.ABOUT:
          window = (Window) new AboutWindow();
          break;
        case WindowStore.WindowType.HELP:
          window = (Window) new HelpWindow();
          break;
        case WindowStore.WindowType.UNLOCKABLES:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new UnlockableWindow();
            break;
          }
          break;
        case WindowStore.WindowType.LANGUAGE:
          window = (Window) new LanguageWindow();
          break;
        case WindowStore.WindowType.RESTART_CONFIRMATION:
          window = (Window) new RestartConfirm();
          break;
        case WindowStore.WindowType.RETURN_TO_MENU_CONFIRMATION:
          window = (Window) new ReturnToMenuConfirm();
          break;
        case WindowStore.WindowType.EXIT_TO_BROWSER_CONFIRMATION:
          window = (Window) new ExitToBrowserConfirm();
          break;
        case WindowStore.WindowType.CHAPTER_SELECT_STORY:
          window = (Window) new ChapterSelectStoryWindow();
          break;
        case WindowStore.WindowType.CHAPTER_SELECT_SPEEDRUN:
          window = (Window) new ChapterSelectSpeedrunWindow();
          break;
        case WindowStore.WindowType.NEW_GAME_CONFIRMATION:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new NewGameConfirm();
            break;
          }
          break;
        case WindowStore.WindowType.TUTORIAL_CONFIRMATION:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new TutorialConfirm();
            break;
          }
          break;
        case WindowStore.WindowType.ENABLE_SHARE_DATA_CONFIRM:
          window = (Window) new EnableShareDataConfirm();
          break;
        case WindowStore.WindowType.DISABLE_SHARE_DATA_CONFIRM:
          window = (Window) new DisableShareDataConfirm();
          break;
        case WindowStore.WindowType.SHARE_DATA_ENABLED:
          window = (Window) new ShareDataEnabledConfirm();
          break;
        case WindowStore.WindowType.SHARE_DATA_DISABLED:
          window = (Window) new ShareDataDisabledConfirm();
          break;
        case WindowStore.WindowType.LEADERBOARD:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new LeaderboardWindow();
            break;
          }
          break;
        case WindowStore.WindowType.UPLOAD_SCORE_CONFIRM:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new UploadConfirmationWindow();
            break;
          }
          break;
        case WindowStore.WindowType.DISPLAY_NAME_ENTRY:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new DisplayNameEntry();
            break;
          }
          break;
        case WindowStore.WindowType.PLEASE_WAIT:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new PleaseWaitWindow();
            break;
          }
          break;
        case WindowStore.WindowType.CHALLENGE_NONETWORK:
          if (!MirrorsEdge.TrialMode)
          {
            window = (Window) new ChallengeNoNetwork();
            break;
          }
          break;
        case WindowStore.WindowType.NO_STORAGE:
          window = (Window) new NoStorage();
          break;
        case WindowStore.WindowType.BUY_FROM_LEADERBOARDS:
          window = (Window) new BuyFromLeaderboardsMessage();
          break;
      }
      if (window != null)
        this.pushWindow(window);
      return window;
    }

    public Window pushWindow(Window window)
    {
      lock (this.m_syncNewWindowsMutex)
        this.m_newWindows.Add(window);
      return window;
    }

    public void update(int timeStep)
    {
      lock (this.m_syncMutex)
      {
        foreach (WindowElement window in this.m_windows)
          window.update(timeStep);
        for (int index = this.m_windows.Count - 1; index >= 0; --index)
        {
          Window window = this.m_windows[index];
          if (this.onDeleteWindow(window))
            this.m_windows.Remove(window);
        }
        lock (this.m_syncNewWindowsMutex)
        {
          foreach (Window newWindow in this.m_newWindows)
            this.m_windows.Add(newWindow);
          this.m_newWindows.Clear();
        }
        this.m_buttonEffect.update(timeStep);
        if (MirrorsEdge.TrialMode)
          return;
        this.m_networkWaitEffect.update(timeStep);
      }
    }

    public void render(Graphics g)
    {
      lock (this.m_syncMutex)
      {
        foreach (WindowElement window in this.m_windows)
          window.render(g);
      }
    }

    public bool OnHardBackKeyEvent()
    {
      if (this.m_waitingForMainThread != 0)
        return false;
      int x1 = -1;
      int y1 = -1;
      foreach (Window window in this.m_windows)
      {
        if (window.GetBackKeyCenterIfAny(out x1, out y1))
        {
          int x2 = x1 - window.getX();
          int y2 = y1 - window.getY();
          bool flag = window.pointerPressed(x2, y2, 0) | window.pointerReleased(x2, y2, 0);
          if (flag)
            return flag;
        }
      }
      return false;
    }

    public void pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_waitingForMainThread != 0)
        return;
      lock (this.m_syncMutex)
      {
        foreach (Window window in this.m_windows)
        {
          if (window.contains(x, y))
          {
            int x1 = x - window.getX();
            int y1 = y - window.getY();
            window.pointerPressed(x1, y1, pointerNum);
          }
        }
      }
    }

    public void pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_waitingForMainThread != 0)
        return;
      lock (this.m_syncMutex)
      {
        foreach (Window window in this.m_windows)
        {
          if (window.contains(x, y))
          {
            int x1 = x - window.getX();
            int y1 = y - window.getY();
            window.pointerReleased(x1, y1, pointerNum);
          }
        }
      }
    }

    public void pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_waitingForMainThread != 0)
        return;
      lock (this.m_syncMutex)
      {
        foreach (Window window in this.m_windows)
        {
          if (window.contains(x, y))
          {
            int x1 = x - window.getX();
            int y1 = y - window.getY();
            window.pointerDragged(x1, y1, pointerNum);
          }
        }
      }
    }

    public WindowResult getWindowResult() => this.m_lastResult;

    public void clearWindowResult() => this.m_lastResult = WindowResult.WINDOW_RESULT_NONE;

    public ButtonEffect getButtonEffect() => this.m_buttonEffect;

    public NetworkWaitEffect getNetworkWaitEffect() => this.m_networkWaitEffect;

    public void setWaitingForMainThread()
    {
      if (this.m_waitingForMainThread != 0)
        return;
      this.m_waitingForMainThread = 1;
    }

    public void unsetWaitingForMainThread()
    {
      if (this.m_waitingForMainThread == 0)
        return;
      this.m_waitingForMainThread = 0;
    }

    private bool onDeleteWindow(Window win)
    {
      if (!win.isClosed())
        return false;
      this.m_lastResult = win.getWindowResult();
      if (win.deleteOnClosed() && win != null)
        win.Destructor();
      return true;
    }

    public enum WindowType
    {
      GENERIC_WINDOW,
      WINDOW_WITH_BACKGROUND,
      GAME_OPTIONS,
      SOUND_OPTIONS,
      ACHIEVEMENTS_WINDOW,
      TIME_UPLOAD_CONFIRMATION,
      J2PLAY_CONFIRMATION,
      STORY_END_OF_LEVEL,
      SPEEDRUN_END_OF_LEVEL,
      CHALLENGE_END_OF_LEVEL,
      END_OF_GAME_RESULTS,
      ABOUT_MENU,
      ABOUT,
      HELP,
      UNLOCKABLES,
      LANGUAGE,
      MEDIA_PICKER_UNAVAILABLE_PROMPT,
      RESTART_CONFIRMATION,
      RETURN_TO_MENU_CONFIRMATION,
      EXIT_TO_BROWSER_CONFIRMATION,
      CHAPTER_SELECT_STORY,
      CHAPTER_SELECT_SPEEDRUN,
      NEW_GAME_CONFIRMATION,
      TUTORIAL_CONFIRMATION,
      ENABLE_SHARE_DATA_CONFIRM,
      DISABLE_SHARE_DATA_CONFIRM,
      SHARE_DATA_ENABLED,
      SHARE_DATA_DISABLED,
      LEADERBOARD,
      UPLOAD_SCORE_CONFIRM,
      DISPLAY_NAME_ENTRY,
      PLEASE_WAIT,
      CHALLENGEE_SELECT,
      CHALLENGE_COMMENT_ENTRY,
      CHALLENGE_NONETWORK,
      NO_STORAGE,
      BUY_FROM_LEADERBOARDS,
    }
  }
}
