
// Type: game.SceneStartup
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using GameManager;
using generic;
using midp;
using support;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace game
{
  public class SceneStartup : Scene
  {
    public const int LOADING_STATE_INVALID = 0;
    public const int LOADING_STATE_INIT = 1;
    public const int LOADING_STATE_APP_ENGINE = 2;
    public const int LOADING_STATE_LAST_STATE = 3;
    public const int LOADING_STATE_FINISHED = 4;
    public const int SPLASH_SCREEN_FIRST_RENDER = -1;
    public const int SPLASH_SCREEN_RENDERED = 0;
    private const string STARTUP_FILENAME = "LevelAutoStart";
    public const int DONT_STARTUP_VERSION = -1;
    public const int STARTUP_FILE_VERSION = 0;
    private bool m_autoStart;
    private int m_loadingState;
    private int m_loadingTime;
    private int m_splashTime;
    private Thread m_loadingThread;
    private SceneStartup.LoadingThreadState m_loadingThreadState;

    public SceneStartup(AppEngine engine)
      : base(engine)
    {
      this.m_autoStart = false;
      this.m_loadingState = 0;
      this.m_loadingTime = 0;
      this.m_splashTime = -1;
      this.m_loadingThread = (Thread) null;
      this.m_loadingThreadState = SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_IDLE;
    }

    public override void Destructor()
    {
      this.m_engine.getQuadManager().freeQuads((int) QuadManager.get("GROUP_SCENESTARTUP"));
      base.Destructor();
    }

    public override void start(int initialState)
    {
      this.m_loadingState = 1;
      this.m_loadingProgress = 0;
    }

    public async void updateLoading(int timeStep)
    {
      this.m_engine.updateLoading(timeStep);
      this.m_engine.getLoadingRunAnimPlayer().updateAnim(timeStep);

      if (0 <= this.m_splashTime)
        this.m_splashTime += timeStep;
       
        if (this.m_loadingThread == null && 
                this.m_loadingThreadState == SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_IDLE)
        {
            if (this.m_engine.isFading())
                return;
            this.m_loadingThread = new Thread(new ParameterizedThreadStart(ThreadImplSceneStartup.Start));
            //ThreadImplSceneStartup.Start((object)this);
            this.m_loadingThreadState = SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_IDLE;
            this.m_loadingThread.Start((object) this);

            //TEST
            //this.m_loadingProgress = 100;
            }
        else
        {
            //Thread.Sleep(40);
            await Task.Delay(40);
        }
    }

    public async void updateLoadingState(int timeStep)
    {
      switch (this.m_loadingState)
      {
        case 1:
          ++this.m_loadingState;
          break;
        case 2:
          this.m_engine.doStartupLoading();
          QuadManager quadManager = this.m_engine.getQuadManager();
          AnimationManager.loadImage(this.m_engine.getResourceManager(), 
              (int) ResourceManager.get("IDI_LOADING_RUN_PNG"));
          this.m_engine.getLoadingRunAnimPlayer().startAnim(0, 4);
          AppEngine.getM3GAssets().getAppearance(7).m_Mirror = true;
          AppEngine.getLevelData().loadData();
          AppEngine.getAchievementData().loadData();
          this.m_engine.loadLoadingAssets();
          this.m_engine.getBGMusic();
          this.m_engine.loadSounds();
          while (this.m_splashTime == -1)
          {
            //Thread.Sleep(1);
            await Task.Delay(1);
           }            
          quadManager.loadQuads((int) QuadManager.get("GROUP_APPENGINE"));
          quadManager.setAnimFrame((int) QuadManager.get("ANIM_FADE"), 1);
          ++this.m_loadingState;
          break;
        case 3:
          this.checkForLevelAutoStart();
          ++this.m_loadingState;
          break;
        case 4:
          if (3000 >= this.m_splashTime)
            break;
          this.m_loadingThreadState = SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_QUIT;
          this.m_loadingThread = (Thread) null;
          break;
      }
    }

    public async void Run()
    {
      while (this.m_loadingThreadState != SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_QUIT)
      {
        if (this.m_loadingThreadState != SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_IDLE)
        {
           //Thread.Sleep(1000);
            await Task.Delay(1000);
        }
        else
            this.updateLoadingState(100);
      }
    }

    public override void pause()
    {
      if (this.m_loadingThreadState == SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_QUIT)
        return;
      this.m_loadingThreadState = SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_WAIT;
    }

    public override void resume()
    {
      if (this.m_loadingThreadState != SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_WAIT)
        return;
      this.m_loadingThreadState = SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_IDLE;
    }

    public override void end()
    {
      if (this.m_loadingThread == null)
        return;
      this.m_loadingThreadState = SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_QUIT;
      this.m_loadingThread = (Thread) null;
    }

    public override void update(int timeStepMillis)
    {
      this.m_loadingTime += timeStepMillis;
      if (this.m_loadingState == 4 && 
                this.m_loadingThreadState == SceneStartup.LoadingThreadState.LOADINGTHREAD_STATE_QUIT)
      {
        if (3000 > this.m_loadingTime || this.m_engine.isFading())
          return;
        this.exitStartup();
      }
      else
        this.updateLoading(timeStepMillis);
    }

    private void exitStartup()
    {
      if (this.m_autoStart)
      {
        this.m_engine.changeScene(3);
      }
      else
      {
        if (this.m_engine.launchedForChallenge() && !MirrorsEdge.TrialMode)
          return;
        ulong num = 5242880;
        if (num * 2UL < num)
        {
          this.m_engine.setStorageFull(true);
          this.m_engine.changeScene(2, 40);
        }
        else
        {
          this.m_engine.setStorageFull(false);
          this.m_engine.changeScene(2, 2);
        }
      }
    }

    public override void render(Graphics g)
    {
      if (this.m_splashTime < 3000)
      {
        this.m_engine.getQuadManager().setSplashCamera();
        this.m_engine.getQuadManager().render(g);
        this.m_engine.getQuadManager().resetCamera();
        if (this.m_splashTime != -1)
          return;
        this.m_splashTime = 0;
      }
      else
      {
        int width = this.m_engine.getWidth();
        int height = this.m_engine.getHeight();
        g.setColor((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
        g.fillRect(0, 0, width, height);
        this.m_engine.getLoadingRunAnimPlayer().drawAnim(g, 7 * (width >> 3), 7 * (height >> 3));
      }
    }

    public override void OnHardBackKeyEvent()
    {
    }

    public override void pointerPressed(int x, int y, int pointerNum)
    {
    }

    public override void pointerDragged(int x, int y, int pointerNum)
    {
    }

    public override void pointerReleased(int x, int y, int pointerNum)
    {
    }

    public void checkForLevelAutoStart()
    {
      InputStream resourceAsStream1 = (InputStream) WP7InputStreamIsolatedStorage
                .getResourceAsStream("LevelAutoStart");
      if (resourceAsStream1 != null)
      {
        DataInputStream dataInputStream = new DataInputStream(resourceAsStream1);
        if (dataInputStream.readShort() == (short) 0)
        {
          this.m_autoStart = true;
          LevelData.GameMode gameMode = (LevelData.GameMode) dataInputStream.readByte();
          int levelIndex = (int) dataInputStream.readShort();
          AppEngine.getLevelData().setCurrentLevelByIndex(gameMode, levelIndex);
        }
        resourceAsStream1.close();
      }
      if (!this.m_autoStart)
        return;
      OutputStream resourceAsStream2 = (OutputStream) OutputStream.getResourceAsStream("LevelAutoStart");
      new DataOutputStream(resourceAsStream2).writeShort((short) -1);
      resourceAsStream2.close();
    }

    public static void setCurrentLevelToAutoStart()
    {
      DataOutputStream dataOutputStream = 
                new DataOutputStream((OutputStream) OutputStream.getResourceAsStream("LevelAutoStart"));
      LevelData levelData = AppEngine.getLevelData();
      dataOutputStream.writeShort((short) 0);
      dataOutputStream.writeByte((byte) levelData.getGameMode());
      dataOutputStream.writeShort((short) levelData.getCurrentLevelIndex());
      dataOutputStream.close();
    }

    private enum LoadingThreadState
    {
      LOADINGTHREAD_STATE_IDLE,
      LOADINGTHREAD_STATE_WAIT,
      LOADINGTHREAD_STATE_QUIT,
    }
  }
}
