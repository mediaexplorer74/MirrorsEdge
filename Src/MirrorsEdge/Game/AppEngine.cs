
// Type: game.AppEngine
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using ea;
using generic;
using microedition.m3g;
using midp;
using mirrorsedge_wp7;
using support;
using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;
using text;
using UI;

#nullable disable
namespace game
{
  public class AppEngine : Canvas
  {
    public const int MAX_POINTER_QUEUE = 50;
    public const int DYNAMIC_STRING_TEMP = -12;
    public const int DYNAMIC_STRING_ABOUT = -11;
    public const int FADE_ANIM_INDEX_FULL_FAST = 0;
    public const int FADE_ANIM_INDEX_NONE = 1;
    public const int FADE_ANIM_INDEX_FULL_SLOW = 2;
    public const int VIBRATION_SHORT = 250;
    public const int VIBRATION_MEDIUM = 400;
    public const int VIBRATION_LONG = 600;
    private const int SETTINGS_VERSION = 59;
    public const string GAME_OPTIONS_FILENAME = "gopt.bin";
    public const int GAME_OPTIONS_VERSION = 3;
    public sbyte NULL_VALUE = -1;
    private bool m_saveFileError;
    public volatile bool m_paintScheduled;
    public volatile bool m_updateScheduled;
    private MonkeyApp m_midlet;
    public bool m_gameRunning;
    private Scene m_currentScene;
    public bool m_paused;
    private Graphics3D m_graphics3D;
    private ResourceManager m_resourceManager = new ResourceManager();
    private AnimationManagerData m_animManData = new AnimationManagerData();
    private AnimationManager3D m_animationManager3D = new AnimationManager3D();
    private TextManager m_textManager = new TextManager();
    private QuadManager m_quadManager = new QuadManager();
    private SoundManager m_soundManager = new SoundManager();
    private BGMusic m_bgMusic;
    private Thread m_bgMusicThread;
    private Random m_randomInstance = new Random();
    public static AppEngine AppEngine_instance;
    private bool m_startupDone;
    private int m_pointerQueueBuffer;
    private int[] m_pointerQueueIndexes;
    private int[,] m_pointerQueueEvents;
    private int[,] m_pointerQueueXs;
    private int[,] m_pointerQueueYs;
    private int[,] m_pointerQueuePointerNums;
    private M3GAssets m_m3gAssets = new M3GAssets();
    private GameObjectData m_gameObjectData = new GameObjectData();
    private LevelData m_levelData = new LevelData();
    private AnimationBlenderData m_animationBlenderData = new AnimationBlenderData();
    private GameObjectRunnerData m_gameObjectRunnerData = new GameObjectRunnerData();
    private AchievementData m_achievementData = new AchievementData();
    private bool m_fadeComplete;
    private int m_currentSceneIndex;
    private int m_nextSceneIndex;
    private int m_nextSceneState;
    private int m_fpsFrameTimer;
    private int m_fpsFrameCount;
    private StringBuffer m_fpsString;
    private bool s_rmsVibrationEnabled;
    private string m_lastLaunchOSVersionString;
    private AnimPlayer m_loadingRunAnimPlayer = new AnimPlayer();
    private World m_world;
    private Camera m_m3gCamera;
    private Transform m_cameraTransform;
    private Node m_loadingNode;
    private Appearance m_scrollingLoadAppearance;
    private Texture2D m_scrollingLoadTexture;
    private float m_scrollingLoadProgress;
    private float m_minScrollingLoadProgress;
    private bool m_scrolling;
    private LoadingScreen[] m_loadingScreens;
    private int m_LastLanguage = -1;
    private WindowStore m_windowStore = new WindowStore();
    private AchievementNotification m_achievementNotification;
    private bool m_tutorialBoxes;
    private bool m_uploadScores;
    private bool m_runnerVision;
    private bool m_spyware;
    private int m_challengeTime;
    public bool m_launchedForChallenge;
    public string m_challengeID;
    public string m_challengerID;
    private bool m_storageFull;

    public override void Destructor()
    {
      this.m_currentScene = (Scene) null;
      this.m_midlet = (MonkeyApp) null;
      this.m_bgMusic.beQuiet();
      this.m_bgMusic.close();
      this.m_bgMusicThread = (Thread) null;
      if (!MirrorsEdge.TrialMode && this.m_achievementNotification != null)
        this.m_achievementNotification = (AchievementNotification) null;
      this.freeLoadingScreens(true);
      if (this.m_lastLaunchOSVersionString != null)
        this.m_lastLaunchOSVersionString = (string) null;
      int num = MirrorsEdge.TrialMode ? 1 : 0;
    }

    public bool getSaveFileError() => this.m_saveFileError;

    public bool isEngineIdle() => !this.m_paintScheduled && !this.m_updateScheduled;

    public MonkeyApp getMIDlet() => this.m_midlet;

    public Graphics3D getGraphics3D() => this.m_graphics3D;

    public AnimationManagerData getAnimManData() => this.m_animManData;

    public ResourceManager getResourceManager() => this.m_resourceManager;

    public AnimationManager3D getAnimationManager3D() => this.m_animationManager3D;

    public SoundManager getSoundManager() => this.m_soundManager;

    public BGMusic getBGMusic() => this.m_bgMusic;

    public TextManager getTextManager() => this.m_textManager;

    public QuadManager getQuadManager() => this.m_quadManager;

    public int getHalfWidth() => this.getWidth() >> 1;

    public int getHalfHeight() => this.getHeight() >> 1;

    public static void createAppEngine(MonkeyApp m)
    {
      AppEngine.AppEngine_instance = new AppEngine(m);
    }

    public static AppEngine getCanvas() => AppEngine.AppEngine_instance;

    public AppEngine(MonkeyApp m)
    {
      this.m_saveFileError = false;
      this.m_midlet = m;
      this.m_gameRunning = false;
      this.m_currentScene = (Scene) null;
      this.m_paused = true;
      this.m_graphics3D = (Graphics3D) null;
      this.m_startupDone = false;
      this.m_paintScheduled = false;
      this.m_updateScheduled = false;
      this.m_pointerQueueBuffer = 0;
      this.m_pointerQueueIndexes = new int[2];
      this.m_pointerQueueEvents = new int[2, 50];
      this.m_pointerQueueXs = new int[2, 50];
      this.m_pointerQueueYs = new int[2, 50];
      this.m_pointerQueuePointerNums = new int[2, 50];
      this.m_currentSceneIndex = 0;
      this.m_nextSceneIndex = 2;
      this.m_nextSceneState = 0;
      this.m_fpsFrameTimer = 0;
      this.m_fpsFrameCount = 0;
      this.m_fpsString = new StringBuffer(15);
      this.s_rmsVibrationEnabled = false;
      this.m_lastLaunchOSVersionString = (string) null;
      this.m_world = (World) null;
      this.m_m3gCamera = (Camera) null;
      this.m_cameraTransform = (Transform) null;
      this.m_loadingNode = (Node) null;
      this.m_scrollingLoadAppearance = (Appearance) null;
      this.m_scrollingLoadTexture = (Texture2D) null;
      this.m_scrollingLoadProgress = 0.0f;
      this.m_achievementNotification = (AchievementNotification) null;
      this.m_tutorialBoxes = false;
      this.m_runnerVision = false;
      this.m_uploadScores = false;
      this.m_spyware = true;
      this.m_scrolling = false;
      this.m_fadeComplete = false;
      this.m_challengeTime = 0;
      this.m_launchedForChallenge = false;
      this.m_storageFull = false;
      AnimationManager.constructAnimationManager(ref this.m_animManData);
      AnimationManager.loadSubimageFile(ref this.m_animManData, this.m_resourceManager);
      AnimationManager.loadAnimFile(ref this.m_animManData, this.m_resourceManager);
      AnimationManager.loadColorsFile(ref this.m_animManData, this.m_resourceManager);
      this.m_graphics3D = Graphics3D.getInstance();
      this.loadAnimManager3D();
      this.m_soundManager.setListenerOrientation(0.0f, 0.0f, -1f, 0.0f, 1f, 0.0f);
      this.loadGameOptions();
      EASpywareManager.getInstance().setLanguage(this.getTextManager().getCurrentLocale());
      EASpywareManager.getInstance().setTextManager(this.getTextManager());
      EASpywareManager.getInstance().setEnabled(this.m_spyware);
      int num = MirrorsEdge.TrialMode ? 1 : 0;
    }

    public void start()
    {
      this.loadRMSAppSettings();
      this.m_textManager.init();
      this.changeScene(1);
      this.m_bgMusic = new BGMusic(this.m_soundManager);
      this.m_bgMusicThread = new Thread(new ThreadStart(BGMusic.Process));
      this.m_bgMusicThread.Start();
      AppEngine.getM3GAssets().loadData();
      this.m_quadManager.loadQuadData(this.m_resourceManager.loadBinaryFile((int) ResourceManager.get("IDI_QUADS_BIN")), 533, 320);
      this.m_quadManager.loadQuads((int) QuadManager.get("GROUP_SCENESTARTUP"));
      this.m_paused = false;
    }

    public void doStartupLoading()
    {
      if (this.m_startupDone)
        return;
      this.m_startupDone = true;
      if (MirrorsEdge.TrialMode)
        return;
      this.m_achievementNotification = new AchievementNotification();
    }

    public void end()
    {
      this.m_gameRunning = false;
      this.stopThread();
      if (this.m_currentScene != null)
        this.m_currentScene.end();
      this.saveRMSAppSettings();
    }

    public override void showNotify()
    {
      if (!this.m_paused)
        return;
      this.resumeGame();
      this.startThread();
    }

    public override void hideNotify() => this.pauseGame();

    public void startThread() => this.m_gameRunning = true;

    public void stopThread()
    {
      while (this.m_updateScheduled || this.m_paintScheduled)
        Thread.Sleep(1);
    }

    public void runLoop(int frameTime) => this.update(frameTime);

    public void endGame() => this.m_gameRunning = false;

    public void pauseGame()
    {
      this.m_paused = true;
      while (this.m_paintScheduled)
        Thread.Sleep(1);
      if (this.m_currentScene != null)
        this.m_currentScene.pause();
      this.m_soundManager.pause();
    }

    public void resumeGame()
    {
      this.m_soundManager.resume();
      if (this.m_currentScene != null)
        this.m_currentScene.resume();
      this.m_paused = false;
    }

    public bool isPaused() => this.m_paused;

    public void update(int intervalConst)
    {
      int timeStep = intervalConst;
      if (!MirrorsEdge.TrialMode && this.m_achievementNotification != null)
        this.m_achievementNotification.update(timeStep);
      this.m_windowStore.update(timeStep);
      if (timeStep > 135)
        timeStep = 135;
      if (this.m_nextSceneIndex != -1)
      {
        this.performChangeScene(this.m_nextSceneIndex, this.m_nextSceneState);
        this.m_nextSceneIndex = -1;
        this.m_nextSceneState = -1;
      }
      if (this.m_currentScene == null)
        return;
      this.m_quadManager.updateAnim((int) QuadManager.get("ANIM_FADE"), timeStep);
      this.m_quadManager.updateAnim((int) QuadManager.get("ANIM_BG_FADE"), timeStep);
      this.processPointerEvents();
      this.m_currentScene.update(timeStep);
    }

    public override void paint(Graphics g)
    {
      Graphics g1 = g;
      if (this.m_gameRunning && this.m_currentScene != null)
        this.m_currentScene.render(g1);
      this.m_windowStore.render(g1);
      this.m_quadManager.render(g1, 4);
      if (!MirrorsEdge.TrialMode && this.m_achievementNotification != null)
        this.m_achievementNotification.render(g1);
      this.m_fadeComplete = !this.m_quadManager.isAnimating((int) QuadManager.get("ANIM_FADE"));
      this.m_paintScheduled = false;
    }

    public override bool OnHardBackKeyEvent()
    {
      if (this.m_windowStore.OnHardBackKeyEvent())
        return true;
      if (this.m_currentScene == null)
        return false;
      this.m_currentScene.OnHardBackKeyEvent();
      return true;
    }

    public override void pointerPressed(int x, int y, int pointerNum)
    {
      this.addPointerEvent(0, x, y, pointerNum);
      this.m_windowStore.pointerPressed(x, y, pointerNum);
    }

    public override void pointerDragged(int x, int y, int pointerNum)
    {
      this.addPointerEvent(1, x, y, pointerNum);
      this.m_windowStore.pointerDragged(x, y, pointerNum);
    }

    public override void pointerReleased(int x, int y, int pointerNum)
    {
      this.addPointerEvent(2, x, y, pointerNum);
      this.m_windowStore.pointerReleased(x, y, pointerNum);
    }

    private void addPointerEvent(int @event, int x, int y, int pointerNum)
    {
      int pointerQueueBuffer = this.m_pointerQueueBuffer;
      int pointerQueueIndex = this.m_pointerQueueIndexes[pointerQueueBuffer];
      if (pointerQueueIndex >= 50)
        return;
      ++this.m_pointerQueueIndexes[pointerQueueBuffer];
      this.m_pointerQueueEvents[pointerQueueBuffer, pointerQueueIndex] = @event;
      this.m_pointerQueueXs[pointerQueueBuffer, pointerQueueIndex] = x;
      this.m_pointerQueueYs[pointerQueueBuffer, pointerQueueIndex] = y;
      this.m_pointerQueuePointerNums[pointerQueueBuffer, pointerQueueIndex] = pointerNum;
    }

    public void clearPointerEvents()
    {
      this.m_pointerQueueIndexes[0] = 0;
      this.m_pointerQueueIndexes[1] = 0;
    }

    private void processPointerEvents()
    {
      if (this.m_currentScene == null)
        return;
      int pointerQueueBuffer = this.m_pointerQueueBuffer;
      this.m_pointerQueueBuffer = 1 - pointerQueueBuffer;
      for (int index = 0; index < this.m_pointerQueueIndexes[pointerQueueBuffer]; ++index)
      {
        int pointerQueueEvent = this.m_pointerQueueEvents[pointerQueueBuffer, index];
        int pointerQueueX = this.m_pointerQueueXs[pointerQueueBuffer, index];
        int pointerQueueY = this.m_pointerQueueYs[pointerQueueBuffer, index];
        int pointerQueuePointerNum = this.m_pointerQueuePointerNums[pointerQueueBuffer, index];
        switch (pointerQueueEvent)
        {
          case 0:
            this.m_currentScene.pointerPressed(pointerQueueX, pointerQueueY, pointerQueuePointerNum);
            break;
          case 1:
            this.m_currentScene.pointerDragged(pointerQueueX, pointerQueueY, pointerQueuePointerNum);
            break;
          case 2:
            this.m_currentScene.pointerReleased(pointerQueueX, pointerQueueY, pointerQueuePointerNum);
            break;
        }
      }
      this.m_pointerQueueIndexes[pointerQueueBuffer] = 0;
    }

    public override void keyPressed(int keyCode)
    {
    }

    public override void keyReleased(int keyCode)
    {
    }

    public static M3GAssets getM3GAssets() => AppEngine.getCanvas().m_m3gAssets;

    public static GameObjectData getGameObjectData() => AppEngine.getCanvas().m_gameObjectData;

    public static LevelData getLevelData() => AppEngine.getCanvas().m_levelData;

    public static bool isUnderground()
    {
      switch (AppEngine.getCanvas().m_levelData.getCurrentLevelIndex())
      {
        case 3:
        case 5:
        case 7:
        case 10:
        case 11:
          return true;
        default:
          return false;
      }
    }

    public static AnimationBlenderData getAnimationBlenderData()
    {
      return AppEngine.getCanvas().m_animationBlenderData;
    }

    public static GameObjectRunnerData getGameObjectRunnerData()
    {
      return AppEngine.getCanvas().m_gameObjectRunnerData;
    }

    public static AchievementData getAchievementData() => AppEngine.getCanvas().m_achievementData;

    public StringBuffer getStatLabel(int label)
    {
      StringBuffer stringBuffer = this.m_textManager.clearStringBuffer();
      this.m_textManager.appendStringIdToBuffer(stringBuffer, label);
      this.m_textManager.appendStringIdToBuffer(stringBuffer, 2061);
      return stringBuffer;
    }

    public void appendStatValue(StringBuffer buffer, AppEngine.StatType type, int number)
    {
      switch (type)
      {
        case AppEngine.StatType.STAT_TYPE_INT:
          this.m_textManager.appendIntToBuffer(buffer, number);
          break;
        case AppEngine.StatType.STAT_TYPE_OF:
          if (number < 0)
          {
            buffer.append('-');
            this.m_textManager.appendStringIdToBuffer(buffer, 2058);
            buffer.append('-');
            break;
          }
          this.m_textManager.appendIntToBuffer(buffer, number >> 16 & (int) byte.MaxValue);
          this.m_textManager.appendStringIdToBuffer(buffer, 2058);
          this.m_textManager.appendIntToBuffer(buffer, number & (int) byte.MaxValue);
          break;
        case AppEngine.StatType.STAT_TYPE_TIME_MILLIS:
          this.m_textManager.appendMillisTimeToBuffer(buffer, number, 2);
          break;
        case AppEngine.StatType.STAT_TYPE_POSITIVE_TIME_MILLIS:
          if (number < 0)
          {
            this.m_textManager.appendStringIdToBuffer(buffer, 2065);
            break;
          }
          this.m_textManager.appendMillisTimeToBuffer(buffer, number, 2);
          break;
      }
    }

    public void drawOfStatString(
      Graphics g,
      int fontIndex,
      int label,
      int numerator,
      int denominator,
      int x,
      int y,
      int anchor,
      bool drawShadow)
    {
      this.drawStatString(g, fontIndex, label, AppEngine.StatType.STAT_TYPE_OF, numerator << 16 | denominator, x, y, anchor, drawShadow);
    }

    public void drawStatString(
      Graphics g,
      int fontIndex,
      int label,
      AppEngine.StatType type,
      int number,
      int x,
      int y,
      int anchor,
      bool drawShadow)
    {
      if (label == 2048)
      {
        StringBuffer stringBuffer = this.m_textManager.clearStringBuffer();
        this.appendStatValue(stringBuffer, type, number);
        if (drawShadow)
        {
          StringRenderer stringRenderer = this.m_textManager.getStringRenderer(fontIndex);
          int color = stringRenderer.getColor();
          stringRenderer.setColor(0);
          this.m_textManager.drawString(g, stringBuffer, fontIndex, x + 1, y + 1, anchor);
          stringRenderer.setColor(color);
          this.m_textManager.drawString(g, stringBuffer, fontIndex, x, y, anchor);
        }
        else
          this.m_textManager.drawString(g, stringBuffer, fontIndex, x, y, anchor);
      }
      else
      {
        StringBuffer statLabel = this.getStatLabel(label);
        statLabel.append(' ');
        this.appendStatValue(statLabel, type, number);
        if (drawShadow)
        {
          StringRenderer stringRenderer = this.m_textManager.getStringRenderer(fontIndex);
          int color = stringRenderer.getColor();
          stringRenderer.setColor(0);
          this.m_textManager.drawString(g, statLabel, fontIndex, x + 1, y + 1, anchor);
          stringRenderer.setColor(color);
          this.m_textManager.drawString(g, statLabel, fontIndex, x, y, anchor);
        }
        else
          this.m_textManager.drawString(g, statLabel, fontIndex, x, y, anchor);
      }
    }

    private static string GetVersionNumber()
    {
      string versionNumber = Assembly.GetExecutingAssembly().FullName.Split(',')[1].Split('=')[1];
      int length = versionNumber.LastIndexOf(".");
      if (length > 0)
        versionNumber = versionNumber.Substring(0, length);
      return versionNumber;
    }

    public void initAboutString()
    {
      if (this.m_textManager.getDynamicString(-11) != null)
        return;
      this.m_textManager.dynamicString(-11, 2050, AppEngine.GetVersionNumber());
    }

    public void clearAboutString() => this.m_textManager.dynamicString(-11, (string) null);

    public void setFadeColor(int fadeColor)
    {
      this.m_quadManager.getMesh((int) QuadManager.get("MESH_FADE")).vertexBuffer.setDefaultColor((uint) (-16777216 | fadeColor));
    }

    public void startFadeOut(bool fast, int fadeColor)
    {
      this.setFadeColor(fadeColor);
      this.startFadeOut(fast);
    }

    public void startFadeOut(bool fast)
    {
      int endIndex = fast ? 0 : 2;
      this.m_quadManager.playAnimAcross((int) QuadManager.get("ANIM_FADE"), 2, 1, endIndex);
      this.m_fadeComplete = false;
    }

    public void startFadeIn(bool fast)
    {
      int startIndex = fast ? 0 : 2;
      this.m_quadManager.playAnimAcross((int) QuadManager.get("ANIM_FADE"), 2, startIndex, 1);
      this.m_fadeComplete = false;
    }

    public void stopFade()
    {
      AnimPlayerQuad animPlayer = this.m_quadManager.getAnimPlayer((int) QuadManager.get("ANIM_FADE"));
      animPlayer.stopAnim();
      animPlayer.snapToFrame(1);
      this.m_fadeComplete = true;
    }

    public bool isFading() => !this.m_fadeComplete;

    public bool isFadingOut()
    {
      AnimPlayerQuad animPlayer = this.m_quadManager.getAnimPlayer((int) QuadManager.get("ANIM_FADE"));
      return animPlayer.isAnimating() && animPlayer.getPlayingForward();
    }

    public bool isFadingIn()
    {
      AnimPlayerQuad animPlayer = this.m_quadManager.getAnimPlayer((int) QuadManager.get("ANIM_FADE"));
      return animPlayer.isAnimating() && !animPlayer.getPlayingForward();
    }

    public bool isFadedOut()
    {
      AnimPlayerQuad animPlayer = this.m_quadManager.getAnimPlayer((int) QuadManager.get("ANIM_FADE"));
      return this.m_fadeComplete && animPlayer.getCurrentKeyframe() != 1;
    }

    public bool isFadedIn()
    {
      AnimPlayerQuad animPlayer = this.m_quadManager.getAnimPlayer((int) QuadManager.get("ANIM_FADE"));
      return this.m_fadeComplete && animPlayer.getCurrentKeyframe() == 1;
    }

    public void renderBgFade(Graphics g) => this.m_quadManager.render(g, 8);

    public Scene getCurrentScene() => this.m_currentScene;

    public SceneGame getSceneGame()
    {
      return this.m_currentSceneIndex != 3 ? (SceneGame) null : this.m_currentScene as SceneGame;
    }

    public SceneMenu getSceneMenu()
    {
      return this.m_currentSceneIndex != 2 ? (SceneMenu) null : this.m_currentScene as SceneMenu;
    }

    public void changeScene(int sceneID) => this.changeScene(sceneID, -1);

    public void changeScene(int sceneID, int state)
    {
      this.m_nextSceneIndex = sceneID;
      this.m_nextSceneState = state;
    }

    private void performChangeScene(int sceneID, int state)
    {
      if (this.m_currentScene != null)
      {
        this.m_currentScene.end();
        this.m_currentScene.Destructor();
        this.m_currentScene = (Scene) null;
        this.stopFade();
      }
      this.m_currentSceneIndex = sceneID;
      switch (sceneID)
      {
        case 1:
          this.m_currentScene = (Scene) new SceneStartup(this);
          break;
        case 2:
          this.m_currentScene = (Scene) new SceneMenu(this);
          break;
        case 3:
          this.m_currentScene = (Scene) new SceneGame(this);
          break;
      }
      this.m_currentScene.start(state);
    }

    public int rand(int range) => (int) (this.m_randomInstance.NextDouble() * (double) range);

    public int rand(int randLow, int randHigh) => randLow + this.rand(randHigh + 1 - randLow);

    public int randPercent() => this.rand(100);

    public float randFloat(float min, float max)
    {
      return min + (float) (((double) max - (double) min) * ((double) this.rand(65536) * 1.52587890625E-05));
    }

    public float randPercentile() => (float) this.rand(65536) * 1.52587891E-05f;

    public void vibrate(int duration)
    {
    }

    public bool isVibrationEnabled() => this.s_rmsVibrationEnabled;

    public void setVibrationEnabled(bool enabled) => this.s_rmsVibrationEnabled = enabled;

    private void updateFPS(int timeStepMillis)
    {
      ++this.m_fpsFrameCount;
      this.m_fpsFrameTimer += timeStepMillis;
      if (1000 > this.m_fpsFrameTimer)
        return;
      this.m_fpsString.setLength(0);
      this.m_fpsString.append("FPS: ");
      this.m_textManager.appendIntToBuffer(this.m_fpsString, this.m_fpsFrameCount);
      this.m_fpsFrameCount = 0;
      this.m_fpsFrameTimer = 0;
    }

    public int getSoftKeyCommandIDAt(int x, int y) => 0;

    public void loadSounds()
    {
      SoundManager soundManager = this.m_soundManager;
      soundManager.loadData();
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_POSITIVE"));
    }

    public void unloadSounds()
    {
      SoundManager soundManager = this.m_soundManager;
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_POSITIVE"));
    }

    private void resetRMSAppSettings()
    {
      this.s_rmsVibrationEnabled = true;
      this.m_soundManager.setVolumeSFX(1f);
      this.m_soundManager.setVolumeMusic(1f);
      this.m_lastLaunchOSVersionString = (string) null;
    }

    private void loadRMSAppSettings()
    {
      bool flag = false;
      InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream("gamesett2");
      if (resourceAsStream != null)
      {
        DataInputStream dataInputStream = new DataInputStream(resourceAsStream);
        if (13 <= dataInputStream.available() && dataInputStream.readInt() == 59)
        {
          this.s_rmsVibrationEnabled = dataInputStream.readBoolean();
          float volumeConst = dataInputStream.readFloat();
          float volume = dataInputStream.readFloat();
          this.m_soundManager.setVolumeSFX(volumeConst);
          this.m_soundManager.setVolumeMusic(volume);
          this.m_lastLaunchOSVersionString = dataInputStream.readUTF();
          flag = !dataInputStream.eofExceptionThrown();
        }
        resourceAsStream.close();
      }
      if (flag)
        return;
      this.resetRMSAppSettings();
      this.saveRMSAppSettings();
    }

    public bool saveRMSAppSettings()
    {
      OutputStream resourceAsStream = (OutputStream) OutputStream.getResourceAsStream("gamesett2");
      if (resourceAsStream == null)
        return false;
      DataOutputStream dataOutputStream = new DataOutputStream(resourceAsStream);
      dataOutputStream.writeInt(59);
      dataOutputStream.writeBoolean(this.s_rmsVibrationEnabled);
      dataOutputStream.writeFloat(this.m_soundManager.getVolumeSFX());
      dataOutputStream.writeFloat(this.m_soundManager.getVolumeMusic());
      dataOutputStream.writeUTF(this.m_lastLaunchOSVersionString);
      dataOutputStream.close();
      return true;
    }

    public void loadAnimManager3D()
    {
      this.m_animationManager3D.loadAnimFile(this.m_resourceManager);
    }

    public AnimPlayer getLoadingRunAnimPlayer() => this.m_loadingRunAnimPlayer;

    public float getLoadingScrollValue() => this.m_scrollingLoadProgress;

    public void setLoadingScrollValue(float value) => this.m_scrollingLoadProgress = value;

    public void setScrollingLoad(bool scroll) => this.m_scrolling = scroll;

    public void loadLoadingScreens()
    {
      this.m_LastLanguage = this.getTextManager().getCurrentLanguage();
      InputStream @in = AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_LOADING_SCREEN_DATA_BIN"));
      DataInputStream dis = new DataInputStream(@in);
      int length = dis.readInt();
      this.m_loadingScreens = new LoadingScreen[length];
      for (int index = 0; index < length; ++index)
        this.m_loadingScreens[index] = new LoadingScreen(dis);
      @in.close();
    }

    public bool freeLoadingScreens(bool realRemove)
    {
      if (!realRemove && this.m_LastLanguage == this.getTextManager().getCurrentLanguage())
        return false;
      for (int index = 0; index < this.m_loadingScreens.Length; ++index)
      {
        this.m_loadingScreens[index].Destructor();
        this.m_loadingScreens[index] = (LoadingScreen) null;
      }
      return true;
    }

    public LoadingScreen getLoadingScreen(int id) => this.m_loadingScreens[id];

    public void initLoadingScreen(int screenId)
    {
      GC.Collect();
      LoadingScreen loadingScreen = this.getLoadingScreen(screenId);
      int modelIndex = 0;
      Image image1 = Image.createImage(256 * Runtime.pixelScale * 2, 1024 * Runtime.pixelScale * 2);
      Graphics graphics = image1.getGraphics();
      FontWP7Font.SetBitmapGraphics(true);
      graphics.setClip(0, 0, 256 * Runtime.pixelScale, 1024 * Runtime.pixelScale);
      loadingScreen.render(graphics);
      FontWP7Font.SetBitmapGraphics(false);
      Image2D image2 = new Image2D(32867, image1);
      switch (loadingScreen.getColour())
      {
        case 0:
          modelIndex = (int) M3GAssets.get("MODEL_LOADING_ORANGE");
          break;
        case 1:
          modelIndex = (int) M3GAssets.get("MODEL_LOADING_RED");
          break;
        case 2:
          modelIndex = (int) M3GAssets.get("MODEL_LOADING_BLUE");
          break;
      }
      GC.Collect();
      Node child = this.m_m3gAssets.loadModel(modelIndex, 16);
      M3GAssets.addNode((Group) this.m_world, child);
      this.m_loadingNode = child;
      ((Node) this.m_loadingNode.find(120)).getTransformTo((Node) this.m_world, this.m_cameraTransform);
      this.m_cameraTransform.postRotate(0.0f, 0.0f, 0.0f, 1f);
      this.m_m3gCamera.setTransform(ref this.m_cameraTransform);
      this.m_scrollingLoadAppearance = this.m_m3gAssets.loadTexturedAppearance((int) M3GAssets.get("TEX_LOADING_TEXT"), 16);
      this.m_scrollingLoadTexture = M3GAssets.createTexture2D(image2, 208, 209, 228);
      this.m_scrollingLoadTexture.setTranslation(0.0f, this.m_scrollingLoadProgress, 0.0f);
      this.m_scrollingLoadAppearance.setTexture(0, this.m_scrollingLoadTexture);
      this.m_scrollingLoadProgress = 1.6f;
      this.m_scrollingLoadTexture.setTranslation(0.0f, this.m_scrollingLoadProgress, 0.0f);
      this.m_minScrollingLoadProgress = (float) (1.4199999570846558 - (double) loadingScreen.getTotalHeight() / 1024.0);
      this.m_scrolling = true;
    }

    public void deinitLoadingScreen()
    {
      this.m_m3gAssets.freeCaches(16);
      this.m_world.removeChild(this.m_loadingNode);
      this.m_loadingNode = (Node) null;
      this.m_scrollingLoadAppearance = (Appearance) null;
      this.m_scrollingLoadTexture = (Texture2D) null;
    }

    public void loadLoadingAssets()
    {
      World world = new World();
      this.m_world = world;
      float aspectRatio = (float) this.getWidth() / (float) this.getHeight();
      Camera camera = new Camera();
      this.m_m3gCamera = camera;
      camera.setPerspective(54f, aspectRatio, 0.01f, 2f);
      world.addChild((Node) camera);
      world.setActiveCamera(camera);
      Background background = new Background();
      background.setColorClearEnable(true);
      background.setColor(uint.MaxValue);
      background.setDepthClearEnable(true);
      world.setBackground(background);
      this.m_cameraTransform = new Transform();
      this.m_scrollingLoadProgress = 0.0f;
      this.loadLoadingScreens();
    }

    public void updateLoading(int timeStep)
    {
      if (this.m_scrollingLoadTexture == null)
        return;
      float num = (float) timeStep / 1000f;
      if (this.m_scrolling)
        this.m_scrollingLoadProgress += -0.02f * num;
      while ((double) this.m_scrollingLoadProgress < 0.0)
        ++this.m_scrollingLoadProgress;
      while ((double) this.m_scrollingLoadProgress > 2.0)
        --this.m_scrollingLoadProgress;
      if ((double) this.m_scrollingLoadProgress < (double) this.m_minScrollingLoadProgress)
        this.m_scrollingLoadProgress = 1.72f;
      this.m_scrollingLoadTexture.setTranslation(0.0f, this.m_scrollingLoadProgress, 0.0f);
    }

    public void renderLoading(Graphics g)
    {
      int width = this.getWidth();
      int height = this.getHeight();
      if (this.m_world == null)
        return;
      Graphics3D graphics3D = this.getGraphics3D();
      graphics3D.bindTarget((object) g);
      graphics3D.setViewport(0, 0, width, height);
      graphics3D.render(this.m_world);
      graphics3D.releaseTarget();
    }

    public static bool networkIsReachable() => NetworkInterface.GetIsNetworkAvailable();

    public WindowStore getWindowStore() => this.m_windowStore;

    public void notifyAchievementComplete(Achievement achievement)
    {
      this.m_achievementNotification.addAchievement(achievement);
    }

    public void setTutorialBoxes(bool set) => this.m_tutorialBoxes = set;

    public bool getTutorialBoxes() => this.m_tutorialBoxes;

    public void setUploadScores(bool set) => this.m_uploadScores = set;

    public bool getUploadScores() => this.m_uploadScores;

    public void setRunnerVision(bool set) => this.m_runnerVision = set;

    public bool getRunnerVision() => this.m_runnerVision;

    public void resetGameOptions()
    {
      this.m_tutorialBoxes = true;
      this.m_uploadScores = !MirrorsEdge.TrialMode;
      this.m_runnerVision = true;
      this.m_spyware = true;
    }

    public void saveGameOptions()
    {
      this.m_spyware = EASpywareManager.getInstance().isEnabled();
      OutputStream resourceAsStream = (OutputStream) OutputStream.getResourceAsStream("gopt.bin");
      if (resourceAsStream == null)
        return;
      DataOutputStream dataOutputStream = new DataOutputStream(resourceAsStream);
      dataOutputStream.writeByte((byte) 3);
      dataOutputStream.writeBoolean(this.m_tutorialBoxes);
      dataOutputStream.writeByte(this.m_uploadScores ? (byte) 1 : (byte) 0);
      dataOutputStream.writeBoolean(this.m_runnerVision);
      dataOutputStream.writeBoolean(this.m_spyware);
      int currentLanguage = this.m_textManager.getCurrentLanguage();
      dataOutputStream.writeInt(currentLanguage);
      resourceAsStream.close();
    }

    public void loadGameOptions()
    {
      InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream("gopt.bin");
      if (resourceAsStream != null)
      {
        DataInputStream dataInputStream = new DataInputStream(resourceAsStream);
        if (9 <= dataInputStream.available())
        {
          if (dataInputStream.readByte() == (sbyte) 3)
          {
            this.m_tutorialBoxes = dataInputStream.readBoolean();
            this.m_uploadScores = dataInputStream.readBoolean();
            this.m_runnerVision = dataInputStream.readBoolean();
            this.m_spyware = dataInputStream.readBoolean();
            this.m_textManager.setCurrentLanguage(dataInputStream.readInt());
          }
          else
          {
            this.resetGameOptions();
            this.saveGameOptions();
          }
          resourceAsStream.close();
        }
        else
        {
          resourceAsStream.close();
          this.resetGameOptions();
          this.saveGameOptions();
        }
      }
      else
      {
        this.resetGameOptions();
        this.saveGameOptions();
      }
    }

    public int timeToMayhemScore(int time, int level)
    {
      return AppEngine.getLevelData().getLevel(level).getSpeedRunRequirementMillis(1) - time;
    }

    public int mayhemScoreToTime(int score, int level)
    {
      return AppEngine.getLevelData().getLevel(level).getSpeedRunRequirementMillis(1) - score;
    }

    public void setChallengeTime(int time) => this.m_challengeTime = time;

    public int getChallengeTime() => this.m_challengeTime;

    public bool launchedForChallenge() => this.m_launchedForChallenge;

    public string getChallengeID() => this.m_challengeID;

    public void setChallengeID(string challengeID) => this.m_challengeID = challengeID;

    public int getPendingChallengeCount() => 0;

    public void setStorageFull(bool full) => this.m_storageFull = full;

    public bool storageFull() => this.m_storageFull;

    public enum StatType
    {
      STAT_TYPE_NONE,
      STAT_TYPE_INT,
      STAT_TYPE_OF,
      STAT_TYPE_TIME_MILLIS,
      STAT_TYPE_POSITIVE_TIME_MILLIS,
    }
  }
}
