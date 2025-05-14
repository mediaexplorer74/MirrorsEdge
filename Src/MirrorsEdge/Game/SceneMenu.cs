
// Type: game.SceneMenu
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using ea;
using generic;
using microedition.m3g;
//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using midp;
using GameManager;
using support;
using System;
using System.Threading;
using text;
using UI;

#nullable disable
namespace game
{
  public class SceneMenu : Scene
  {
    public const int LOADING_STATE_INVALID = 0;
    public const int LOADING_STATE_INIT = 1;
    public const int LOADING_STATE_SCENEMENU_ANIMS = 2;
    public const int LOADING_STATE_CITYSCAPE = 3;
    public const int LOADING_STATE_FINISHED = 4;
    public const int GAME_INTRO_ANIM_START = 0;
    public const int GAME_INTRO_ANIM_WAIT = 1;
    public const int GAME_INTRO_ANIM_END = 5;
    private const int UPSELL_SCREEN_NUM = 5;
    private const float TICKER_SCROLL_SPEED = 50f;
    private const int TICKER_STRING_SEPARATION_OFFSET = -100;
    private const int TICKER_BAR_HEIGHT = 15;
    private const int TICKER_Y_POS = 2;
    private MenuMain m_menuMain;
    private MenuStringChoice m_menuLevelSelectStory;
    private MenuStringChoice m_menuLevelSelectSpeedRun;
    private SceneMenu.StatePhase m_statePhase;
    private SceneMenu.MenuState m_state;
    private SceneMenu.MenuState m_nextState;
    private SceneMenu.MenuState m_prevState;
    private int m_statePhaseTime;
    private bool m_stateTransitionFade;
    private bool m_needToStartTutorial;
    private int m_loadingState;
    private SceneMenu.MenuState m_postLoadingState;
    private static bool IsItTheFirstEnter = true;
    //private Thread m_loadingThread;
    private SceneMenu.LoadingThreadState m_loadingThreadState;
    private World m_world;
    private Camera m_m3gCamera;
    private Transform m_cityscapeCameraTransform;
    private Node m_cityscapeCameraLookFromNode;
    private Node m_cityscapeCameraLookAtNode;
    private AnimPlayer3D m_cityscapeCameraLookFromAnim;
    private AnimPlayer3D m_cityscapeCameraLookAtAnim;
    private bool m_cityscapeCameraInterp;
    private float m_cityscapeCameraInterpolationProgress;
    private MathVector m_cityscapeCameraInterpolationFromStart;
    private MathVector m_cityscapeCameraInterpolationFromEnd;
    private float m_cityscapeCameraInterpolationAzimuthStartDeg;
    private float m_cityscapeCameraInterpolationElevationStartDeg;
    private float m_cityscapeCameraInterpolationAzimuthEndDeg;
    private float m_cityscapeCameraInterpolationElevationEndDeg;
    private float[] lookFrom = new float[3];
    private float[] lookAt = new float[3];
    private bool m_titleDone;
    private ChapterSelectWindow m_levelSelectWindow;
    private WrappedString m_introString;
    private SoundOptions m_soundOptionsWindow;
    private GameOptions m_gameOptionsWindow;
    private AchievementWindow m_achievementsWindow;
    private AboutMenu m_aboutMenu;
    private static readonly int[] UPSELL_QUAD_BACK_MESH_ARRAY = new int[5]
    {
      16,
      18,
      20,
      22,
      24
    };
    private static readonly int[] UPSELL_QUAD_FRONT_MESH_ARRAY = new int[5]
    {
      17,
      19,
      21,
      23,
      25
    };
    private static readonly int[] UPSELL_QUAD_REVEAL_GROUP_ARRAY = new int[5]
    {
      11,
      12,
      13,
      14,
      15
    };
    private static readonly int[][] UPSELL_QUAD_REVEAL_MESH_ARRAY = new int[5][]
    {
      new int[2]{ 26, 27 },
      new int[2]{ 28, 29 },
      new int[2]{ 30, 31 },
      new int[2]{ 32, 33 },
      new int[2]{ 34, 35 }
    };
    private static readonly int[][] UPSELL_FONT_ARRAY = new int[5][]
    {
      new int[3]{ 35, 7, 6 },
      new int[3]{ 35, 7, 6 },
      new int[3]{ 35, 7, 6 },
      new int[3]{ 35, 7, 6 },
      new int[3]{ 35, 7, 6 }
    };
    private static readonly int[] UPSELL_STRING_TITLE_ARRAY = new int[5]
    {
      2398,
      2400,
      2402,
      2404,
      2406
    };
    private static readonly int[] UPSELL_STRING_BODY_ARRAY = new int[5]
    {
      2399,
      2401,
      2403,
      2405,
      2407
    };
    private SceneMenu.UpsellAnimState m_liteUpsellState;
    private int m_listUpsellScreenIndex;
    private WrappedString m_liteUpsellTitleString;
    private WrappedString m_liteUpsellBodyString;
    private int m_prevSubMenu;
    private int m_prevSubMenuItem;
    private int m_currentTicker;
    private int TICKER_FONT = 5;
    private int m_tickerStringSeparation;
    private float m_currentTickerXPos;
    private float m_nextTickerXPos;
    private string m_tickerURL;
    private LeaderboardWindow m_leaderboardWindow;

    public SceneMenu(AppEngine engine)
      : base(engine)
    {
      this.m_menuMain = new MenuMain();
      this.m_menuLevelSelectStory = new MenuStringChoice();
      this.m_menuLevelSelectSpeedRun = new MenuStringChoice();
      this.m_statePhase = SceneMenu.StatePhase.STATE_PHASE_PRE;
      this.m_state = SceneMenu.MenuState.STATE_INVALID;
      this.m_nextState = SceneMenu.MenuState.STATE_INVALID;
      this.m_prevState = SceneMenu.MenuState.STATE_INVALID;
      this.m_statePhaseTime = 0;
      this.m_stateTransitionFade = false;
      this.m_loadingState = 0;
      //this.m_loadingThread = (Thread) null;
      this.m_loadingThreadState = SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_IDLE;
      this.m_world = (World) null;
      this.m_m3gCamera = (Camera) null;
      this.m_cityscapeCameraTransform = (Transform) null;
      this.m_cityscapeCameraLookFromNode = (Node) null;
      this.m_cityscapeCameraLookAtNode = (Node) null;
      this.m_cityscapeCameraLookFromAnim = (AnimPlayer3D) null;
      this.m_cityscapeCameraLookAtAnim = (AnimPlayer3D) null;
      this.m_cityscapeCameraInterp = false;
      this.m_cityscapeCameraInterpolationProgress = 0.0f;
      this.m_cityscapeCameraInterpolationAzimuthStartDeg = 0.0f;
      this.m_cityscapeCameraInterpolationElevationStartDeg = 0.0f;
      this.m_cityscapeCameraInterpolationAzimuthEndDeg = 0.0f;
      this.m_cityscapeCameraInterpolationElevationEndDeg = 0.0f;
      this.m_postLoadingState = SceneMenu.MenuState.STATE_LOADING;
      this.m_achievementsWindow = (AchievementWindow) null;
      this.m_introString = new WrappedString();
      this.m_soundOptionsWindow = (SoundOptions) null;
      if (MirrorsEdge.TrialMode)
      {
        this.m_liteUpsellState = SceneMenu.UpsellAnimState.UPSELL_IDLE;
        this.m_listUpsellScreenIndex = 0;
        this.m_liteUpsellTitleString = new WrappedString();
        this.m_liteUpsellBodyString = new WrappedString();
      }
      this.m_gameOptionsWindow = (GameOptions) null;
      this.m_aboutMenu = (AboutMenu) null;
      this.m_levelSelectWindow = (ChapterSelectWindow) null;
      this.m_prevSubMenu = -1;
      this.m_prevSubMenuItem = -1;
      if (SceneMenu.IsItTheFirstEnter && MirrorsEdge.GS_Supported)
      {
        SceneMenu.IsItTheFirstEnter = false;
        this.m_prevSubMenu = 2;
        this.m_prevSubMenuItem = MirrorsEdge.TrialMode ? 1 : 2;
      }
      this.m_currentTicker = 0;
      this.m_tickerStringSeparation = 0;
      this.m_currentTickerXPos = 0.0f;
      this.m_nextTickerXPos = 0.0f;
      this.m_tickerURL = (string) null;
      this.m_titleDone = false;
      this.m_leaderboardWindow = (LeaderboardWindow) null;
    }

    public override void Destructor()
    {
      AppEngine.getM3GAssets().freeCaches(2);
      this.m_menuMain = (MenuMain) null;
      this.m_menuLevelSelectStory = (MenuStringChoice) null;
      this.m_menuLevelSelectSpeedRun = (MenuStringChoice) null;
      this.m_achievementsWindow = (AchievementWindow) null;
      base.Destructor();
    }

    public override void start(int initialState)
    {
      GC.Collect();
      this.m_engine.saveRMSAppSettings();
      this.m_loadingState = 1;
      this.m_loadingProgress = 0;
      bool flag = false;
      this.m_postLoadingState = AppEngine.getLevelData().isGameNewlyCompleted() || flag ? (MirrorsEdge.TrialMode ? (SceneMenu.MenuState) initialState : SceneMenu.MenuState.STATE_GAME_COMPLETE_RESULTS) : (SceneMenu.MenuState) initialState;
      this.m_state = SceneMenu.MenuState.STATE_LOADING;
      SoundManager soundManager = this.m_engine.getSoundManager();
      soundManager.setVolumeGlobal(SoundManager.MAX_VOLUME);
      soundManager.setListenerPosition(0.0f, 0.0f, 0.0f);
      soundManager.setListenerVelocity(0.0f, 0.0f, 0.0f);
      soundManager.setVolumeGroup(1, SoundManager.MAX_VOLUME);
      this.m_engine.getBGMusic().playMusic((int) ResourceManager.get("SOUNDEVENT_BGM_MENU"), 2);
      this.initLoading();
    }

    public int getState() => (int) this.m_state;

    public void updateLoading(int timeStep)
    {
      this.m_engine.updateLoading(timeStep);
      this.m_engine.getLoadingRunAnimPlayer().updateAnim(timeStep);

        if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        {
            this.stateActivate();
        }
        else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
        {
            if (/*this.m_loadingThread == null &&*/ this.m_loadingThreadState == SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_IDLE)
            {
                if (this.m_engine.isFading())
                    return;
                //this.m_loadingThread = new Thread(new ParameterizedThreadStart(ThreadImplSceneMenu.Start));
                ThreadImplSceneMenu.Start((object)this);
                //this.m_loadingThreadState = SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_IDLE;
                //this.m_loadingThread.Start((object) this);

                //TEST
                //this.m_loadingProgress = 100;
            }
            else
            {
                //Thread.Sleep(40); 
            }
        }
        else
        {
            if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
                return;
            this.stateDeactivate();
        }
    }

    public void updateLoadingState(int timeStep)
    {
      switch (this.m_loadingState)
      {
        case 1:
          this.m_loadingProgress = 0;
          ++this.m_loadingState;
          break;
        case 2:
          this.m_engine.getQuadManager().loadQuads((int) QuadManager.get("GROUP_SCENEMENU"));
          this.m_loadingProgress = 20;
          ++this.m_loadingState;
          break;
        case 3:
          M3GAssets m3Gassets = AppEngine.getM3GAssets();
          AnimationManager3D animationManager3D = AppEngine.getCanvas().getAnimationManager3D();
          World parent = new World();
          this.m_world = parent;
          float aspectRatio = (float) this.m_engine.getWidth() / (float) this.m_engine.getHeight();
          Camera camera = new Camera();
          this.m_m3gCamera = camera;
          camera.setPerspective(90f, aspectRatio, 5f, 500f);
          parent.addChild((Node) camera);
          parent.setActiveCamera(camera);
          Background background = new Background();
          background.setColorClearEnable(true);
          background.setColor(uint.MaxValue);
          background.setDepthClearEnable(true);
          parent.setBackground(background);
          Node child = m3Gassets.loadModel((int) M3GAssets.get("MODEL_CITYSCAPE"), 2);
          M3GAssets.addNode((Group) parent, child);
          this.m_cityscapeCameraTransform = new Transform();
          this.m_cityscapeCameraLookFromNode = m3Gassets.loadNodeCached((int) M3GAssets.get("NODE_CITYSCAPE_CAMERA_LOOK_FROM"), 2);
          this.m_cityscapeCameraLookFromAnim = new AnimPlayer3D(animationManager3D);
          this.m_cityscapeCameraLookFromAnim.setNode(this.m_cityscapeCameraLookFromNode);
          this.m_cityscapeCameraLookFromAnim.startAnim((int) ResourceManager.get("ANIM3D_CAMERA_UI"), 20);
          this.m_cityscapeCameraLookAtNode = m3Gassets.loadNodeCached((int) M3GAssets.get("NODE_CITYSCAPE_CAMERA_LOOK_AT"), 2);
          this.m_cityscapeCameraLookAtAnim = new AnimPlayer3D(animationManager3D);
          this.m_cityscapeCameraLookAtAnim.setNode(this.m_cityscapeCameraLookAtNode);
          this.m_cityscapeCameraLookAtAnim.startAnim((int) ResourceManager.get("ANIM3D_CAMERA_UI"), 20);
          this.m_loadingProgress = 50;
          ++this.m_loadingState;
          break;
        case 4:
          this.precacheAchievementsWindow();
          this.m_loadingThreadState = SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_QUIT;
          //this.m_loadingThread = (Thread) null;
          if (this.m_engine.getSaveFileError())
            this.stateTransition(SceneMenu.MenuState.STATE_FILESAVEERROR);
          else
            this.stateTransitionFade(this.m_postLoadingState);
          this.m_loadingProgress = 100;
          break;
      }
    }

    public void Run()
    {
      while (this.m_loadingThreadState != SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_QUIT)
      {
        if (this.m_loadingThreadState != SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_IDLE)
        {
            //Thread.Sleep(1000);
        }
        else
            this.updateLoadingState(100);
      }
    }

    public override void pause()
    {
      if (this.m_menuMain.getSelectedSubMenu() != null)
        this.m_menuMain.getSelectedSubMenu().transitionToIdle();
      if (this.m_engine.getBGMusic() != null)
        this.m_engine.getBGMusic().suspend();
      if (this.m_state == SceneMenu.MenuState.STATE_LOADING && this.m_loadingThreadState != SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_QUIT)
        this.m_loadingThreadState = SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_WAIT;
      EASpywareManager.getInstance().MTXpause();
    }

    public override void resume()
    {
      if (this.m_engine.getBGMusic() != null && (!MirrorsEdge.TrialMode || this.m_state != SceneMenu.MenuState.STATE_MEDIAPICKER))
        this.m_engine.getBGMusic().resume();
      if (this.m_state == SceneMenu.MenuState.STATE_LOADING && this.m_loadingThreadState == SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_WAIT)
        this.m_loadingThreadState = SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_IDLE;
      EASpywareManager.getInstance().MTXresume();
    }

    public override void end()
    {
      //if (this.m_loadingThread != null)
      //{
        this.m_loadingThreadState = SceneMenu.LoadingThreadState.LOADINGTHREAD_STATE_QUIT;
      //  this.m_loadingThread = (Thread) null;
      //}
      this.m_engine.getQuadManager().freeQuads((int) QuadManager.get("GROUP_SCENEMENU"));
    }

    private void updateCityscape(int timeStepMillis)
    {
      if (this.m_cityscapeCameraInterp)
      {
        this.m_cityscapeCameraInterpolationProgress -= (float) timeStepMillis * (1f / 500f);
        if ((double) this.m_cityscapeCameraInterpolationProgress <= 0.0)
          this.m_cityscapeCameraInterp = false;
      }
      else
      {
        int interval = timeStepMillis >> 2;
        this.m_cityscapeCameraLookFromAnim.updateAnim(interval);
        this.m_cityscapeCameraLookAtAnim.updateAnim(interval);
      }
      if (this.m_cityscapeCameraInterp)
      {
        float progress = 1f - this.m_cityscapeCameraInterpolationProgress;
        float degrees1 = (float) ((double) this.m_cityscapeCameraInterpolationProgress
                    * (double) this.m_cityscapeCameraInterpolationAzimuthStartDeg + (double) progress * (double) this.m_cityscapeCameraInterpolationAzimuthEndDeg);
        float degrees2 = (float) ((double) this.m_cityscapeCameraInterpolationProgress
                    * (double) this.m_cityscapeCameraInterpolationElevationStartDeg + (double) progress * (double) this.m_cityscapeCameraInterpolationElevationEndDeg);
        MathVector mathVector = new MathVector();
        mathVector.setAsLinearInterpolation(this.m_cityscapeCameraInterpolationFromStart, this.m_cityscapeCameraInterpolationFromEnd, progress);
        this.m_cityscapeCameraTransform.setIdentity();
        this.m_cityscapeCameraTransform.postTranslate(mathVector.x, mathVector.y, mathVector.z);
        this.m_cityscapeCameraTransform.postRotate(degrees1, 0.0f, 1f, 0.0f);
        this.m_cityscapeCameraTransform.postRotate(degrees2, 1f, 0.0f, 0.0f);
      }
      else
      {
        this.m_cityscapeCameraLookFromNode.getTranslation(ref this.lookFrom);
        this.m_cityscapeCameraLookAtNode.getTranslation(ref this.lookAt);
        GameCamera.createLookAtTransform(this.m_cityscapeCameraTransform, this.lookFrom[0], this.lookFrom[1], 
            this.lookFrom[2], this.lookAt[0], this.lookAt[1], this.lookAt[2], 0.0f, 1f, 0.0f);
      }
      this.m_cityscapeCameraTransform.postRotate(0.0f, 0.0f, 0.0f, 1f);
      this.m_m3gCamera.setTransform(ref this.m_cityscapeCameraTransform);
    }

    private void renderCityscape(Graphics g)
    {
      if (this.m_world == null)
        return;
      Graphics3D graphics3D = AppEngine.getCanvas().getGraphics3D();
      graphics3D.bindTarget((object) g);
      graphics3D.setViewport(0, 0, this.m_engine.getWidth(), this.m_engine.getHeight());
      graphics3D.render(this.m_world);
      graphics3D.releaseTarget();
    }

    private void jerkCityscape()
    {
      if (this.m_cityscapeCameraInterp)
        return;
      int animTime = this.m_cityscapeCameraLookFromAnim.getAnimTime();
      int animDuration = this.m_cityscapeCameraLookFromAnim.getAnimDuration();
      int num1 = animDuration >> 2;
      int range = animDuration >> 1;
      int num2 = (animTime + num1 + this.m_engine.rand(range)) % animDuration;
      this.m_cityscapeCameraLookFromAnim.setAnimTime(num2);
      this.m_cityscapeCameraLookAtAnim.setAnimTime(num2);
      this.m_cityscapeCameraInterp = true;
      this.m_cityscapeCameraInterpolationProgress = 1f;
      this.calculateCameraOrientationAtTime(animTime, ref this.m_cityscapeCameraInterpolationFromStart, 
          ref this.m_cityscapeCameraInterpolationAzimuthStartDeg, ref this.m_cityscapeCameraInterpolationElevationStartDeg);
      this.calculateCameraOrientationAtTime(num2, ref this.m_cityscapeCameraInterpolationFromEnd,
          ref this.m_cityscapeCameraInterpolationAzimuthEndDeg, ref this.m_cityscapeCameraInterpolationElevationEndDeg);
    }

    private void calculateCameraOrientationAtTime(
      int animTime,
      ref MathVector position,
      ref float azimuth,
      ref float elevation)
    {
      this.m_cityscapeCameraLookFromAnim.setAnimTime(animTime);
      this.m_cityscapeCameraLookAtAnim.setAnimTime(animTime);
      this.m_cityscapeCameraLookFromNode.getTranslation(ref this.lookFrom);
      this.m_cityscapeCameraLookAtNode.getTranslation(ref this.lookAt);
      position.set(this.lookFrom[0], this.lookFrom[1], this.lookFrom[2]);
      MathTrig.convertLookVectorToEulerRotationsDeg(this.lookAt[0] - this.lookFrom[0], this.lookAt[1] - this.lookFrom[1],
          this.lookAt[2] - this.lookFrom[2], ref azimuth, ref elevation);
    }

    private void initState()
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_TITLE:
          if (!MirrorsEdge.m_ReturnFromTombstone)
          {
            this.initTitle();
            break;
          }
          this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
          break;
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.initMainMenu();
          break;
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_STORY:
          this.initLevelSelectStory();
          break;
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_SPEEDRUN:
          this.initLevelSelectSpeedRun();
          break;
        case SceneMenu.MenuState.STATE_GAME_INTRO:
          this.initGameIntro();
          break;
        case SceneMenu.MenuState.STATE_SOUND_OPTIONS:
          this.initSoundOptions();
          break;
        case SceneMenu.MenuState.STATE_GAME_OPTIONS:
          this.initGameOptions();
          break;
        case SceneMenu.MenuState.STATE_ACHIEVEMENTS:
          this.initAchievements();
          break;
        case SceneMenu.MenuState.STATE_HELP:
          this.initHelp();
          break;
        case SceneMenu.MenuState.STATE_ABOUT_MENU:
          this.initAboutMenu();
          break;
        case SceneMenu.MenuState.STATE_ABOUT:
          this.initAbout();
          break;
        case SceneMenu.MenuState.STATE_PRE_LEADERBOARD:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.PLEASE_WAIT);
          break;
        case SceneMenu.MenuState.STATE_LEADERBOARD:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_leaderboardWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.LEADERBOARD) as LeaderboardWindow;
          break;
        case SceneMenu.MenuState.STATE_MOREGAMES:
          this.initMoreGames();
          break;
        case SceneMenu.MenuState.STATE_UNLOCKABLES:
          if (MirrorsEdge.TrialMode)
            break;
          this.initUnlockables();
          break;
        case SceneMenu.MenuState.STATE_LANGUAGE:
          this.initLanguage();
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_EULA:
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_PRIVACY:
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_TOS:
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.EXIT_TO_BROWSER_CONFIRMATION);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_NEW_GAME:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.NEW_GAME_CONFIRMATION);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_TUTORIAL:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.TUTORIAL_CONFIRMATION);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_ENABLE_SHARE_DATA:
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.ENABLE_SHARE_DATA_CONFIRM);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_DISABLE_SHARE_DATA:
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.DISABLE_SHARE_DATA_CONFIRM);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_ENABLED:
          SpywareManager.getInstance().setEnabled(true);
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.SHARE_DATA_ENABLED);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_DISABLED:
          SpywareManager.getInstance().trackOptOut();
          SpywareManager.getInstance().setEnabled(false);
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.SHARE_DATA_DISABLED);
          break;
        case SceneMenu.MenuState.STATE_GAME_COMPLETE_RESULTS:
          if (MirrorsEdge.TrialMode)
            break;
          this.initGameCompleteResults();
          break;
        case SceneMenu.MenuState.STATE_CHALLENGE_NONETWORK:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.CHALLENGE_NONETWORK);
          break;
        case SceneMenu.MenuState.STATE_LITE_UPSELL:
          if (!MirrorsEdge.TrialMode)
            break;
          this.initLiteUpsell();
          break;
        case SceneMenu.MenuState.STATE_NO_STORAGE:
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.NO_STORAGE);
          break;
        case SceneMenu.MenuState.STATE_BUY_FROM_LEADERBOARDS:
          if (!MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.BUY_FROM_LEADERBOARDS);
          break;
      }
    }

    public override void update(int timeStepMillis)
    {
      this.m_statePhaseTime += timeStepMillis;
      int num = MirrorsEdge.TrialMode ? 1 : 0;
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_LOADING:
          this.updateLoading(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_TITLE:
          this.updateCityscape(timeStepMillis);
          this.updateTitle(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.updateCityscape(timeStepMillis);
          this.updateMainMenu(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_STORY:
          this.updateCityscape(timeStepMillis);
          this.updateLevelSelectStory(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_SPEEDRUN:
          this.updateCityscape(timeStepMillis);
          this.updateLevelSelectSpeedRun(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_GAME_INTRO:
          this.updateGameIntro(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_SOUND_OPTIONS:
          this.updateCityscape(timeStepMillis);
          this.updateSoundOptions(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_GAME_OPTIONS:
          this.updateCityscape(timeStepMillis);
          this.updateGameOptions(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_ACHIEVEMENTS:
          this.updateCityscape(timeStepMillis);
          this.updateAchievements(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_HELP:
          this.updateCityscape(timeStepMillis);
          this.updateHelp(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_ABOUT_MENU:
          this.updateCityscape(timeStepMillis);
          this.updateAboutMenu(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_ABOUT:
          this.updateCityscape(timeStepMillis);
          this.updateAbout(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_PRE_LEADERBOARD:
          if (MirrorsEdge.TrialMode || this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
            break;
          this.m_engine.getWindowStore().clearWindowResult();
          this.stateTransition(SceneMenu.MenuState.STATE_LEADERBOARD, false);
          this.stateDeactivate();
          break;
        case SceneMenu.MenuState.STATE_LEADERBOARD:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateCityscape(timeStepMillis);
          this.updateLeaderboard(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_UNLOCKABLES:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateUnlockables(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_LANGUAGE:
          this.updateLanguage(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_EXITGAME:
          if (this.m_engine.isFading())
            break;
          this.m_engine.endGame();
          break;
        case SceneMenu.MenuState.STATE_TRANSITION_TO_GAME:
          this.updateTransitionToGame(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_EULA:
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_PRIVACY:
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_TOS:
          this.updateExitConf(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_NEW_GAME:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateNewGameConf(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_TUTORIAL:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateTutorialConf(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_ENABLE_SHARE_DATA:
          this.updateEnableShareDataConf(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_DISABLE_SHARE_DATA:
          this.updateDisableShareDataConf(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_ENABLED:
          this.updateShareDataEnabled(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_DISABLED:
          this.updateShareDataDisabled(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_GAME_COMPLETE_RESULTS:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateCityscape(timeStepMillis);
          this.updateGameCompleteResults(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_PENDING_CHALLENGES:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateCityscape(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_LAUNCHING_CHALLENGE:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateCityscape(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_CHALLENGE_NONETWORK:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateCityscape(timeStepMillis);
          if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
            break;
          this.m_engine.getWindowStore().clearWindowResult();
          this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
          this.stateDeactivate();
          break;
        case SceneMenu.MenuState.STATE_LITE_UPSELL:
          if (!MirrorsEdge.TrialMode)
            break;
          this.updateCityscape(timeStepMillis);
          this.updateLiteUpsell(timeStepMillis);
          break;
        case SceneMenu.MenuState.STATE_NO_STORAGE:
          if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
            break;
          this.m_engine.getWindowStore().clearWindowResult();
          this.stateTransition(SceneMenu.MenuState.STATE_TITLE);
          this.stateDeactivate();
          break;
        case SceneMenu.MenuState.STATE_BUY_FROM_LEADERBOARDS:
          if (!MirrorsEdge.TrialMode)
            break;
          this.updateBuyFromLeaderboards(timeStepMillis);
          break;
      }
    }

    public override void render(Graphics g)
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_LOADING:
          this.renderLoading(g);
          break;
        case SceneMenu.MenuState.STATE_TITLE:
          this.renderCityscape(g);
          this.renderTitle(g);
          break;
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.renderCityscape(g);
          this.renderMainMenu(g);
          break;
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_STORY:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_SPEEDRUN:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_GAME_INTRO:
          this.renderGameIntro(g);
          break;
        case SceneMenu.MenuState.STATE_SOUND_OPTIONS:
        case SceneMenu.MenuState.STATE_GAME_OPTIONS:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_ACHIEVEMENTS:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_HELP:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_ABOUT_MENU:
        case SceneMenu.MenuState.STATE_ABOUT:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_PRE_LEADERBOARD:
        case SceneMenu.MenuState.STATE_LEADERBOARD:
        case SceneMenu.MenuState.STATE_CONFIRM_NEW_GAME:
        case SceneMenu.MenuState.STATE_CONFIRM_TUTORIAL:
        case SceneMenu.MenuState.STATE_GAME_COMPLETE_RESULTS:
        case SceneMenu.MenuState.STATE_PENDING_CHALLENGES:
        case SceneMenu.MenuState.STATE_LAUNCHING_CHALLENGE:
        case SceneMenu.MenuState.STATE_CHALLENGE_NONETWORK:
          if (MirrorsEdge.TrialMode)
            break;
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_UNLOCKABLES:
          if (MirrorsEdge.TrialMode)
            break;
          this.renderUnlockables(g);
          break;
        case SceneMenu.MenuState.STATE_LANGUAGE:
          this.renderLanguage(g);
          break;
        case SceneMenu.MenuState.STATE_PROMPT_MEDIA_PICKER_UNAVAILABLE:
          if (!MirrorsEdge.TrialMode)
            break;
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_EULA:
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_PRIVACY:
        case SceneMenu.MenuState.STATE_CONFIRM_EXIT_TOS:
        case SceneMenu.MenuState.STATE_CONFIRM_ENABLE_SHARE_DATA:
        case SceneMenu.MenuState.STATE_CONFIRM_DISABLE_SHARE_DATA:
        case SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_ENABLED:
        case SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_DISABLED:
        case SceneMenu.MenuState.STATE_BUY_FROM_LEADERBOARDS:
          this.renderCityscape(g);
          break;
        case SceneMenu.MenuState.STATE_LITE_UPSELL:
          if (!MirrorsEdge.TrialMode)
            break;
          this.renderLiteUpsell(g);
          break;
        case SceneMenu.MenuState.STATE_NO_STORAGE:
          g.setColor(16777215);
          g.fillRect(0, 0, this.m_engine.getWidth(), this.m_engine.getHeight());
          break;
      }
    }

    public override void pointerPressed(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.pointerPressedMainMenu(x, y, pointerNum);
          break;
      }
    }

    public override void pointerDragged(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.pointerDraggedMainMenu(x, y, pointerNum);
          break;
      }
    }

    public override void pointerReleased(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_TITLE:
          if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE || this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_POST)
            break;
          this.pointerReleaseTitle(x, y, pointerNum);
          break;
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.pointerReleaseMainMenu(x, y, pointerNum);
          break;
        case SceneMenu.MenuState.STATE_GAME_INTRO:
          this.pointerReleaseGameIntro(x, y, pointerNum);
          break;
        case SceneMenu.MenuState.STATE_LITE_UPSELL:
          if (!MirrorsEdge.TrialMode)
            break;
          this.pointerReleaseLiteUpsell(x, y, pointerNum);
          break;
      }
    }

    private void stateTransOut()
    {
    }

    private void deinitState()
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_LOADING:
          this.deinitLoading();
          break;
        case SceneMenu.MenuState.STATE_TITLE:
          this.deinitTitle();
          break;
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          this.deinitMainMenu();
          break;
        case SceneMenu.MenuState.STATE_GAME_INTRO:
          this.deinitGameIntro();
          break;
        case SceneMenu.MenuState.STATE_LANGUAGE:
          this.deinitLanguage();
          break;
        case SceneMenu.MenuState.STATE_LITE_UPSELL:
          if (!MirrorsEdge.TrialMode)
            break;
          this.deinitLiteUpsell();
          break;
      }
    }

    private void initLoading()
    {
      AnimationManager.loadImage(this.m_engine.getResourceManager(), (int) ResourceManager.get("IDI_LOADING_RUN_PNG"));
    }

    private void renderLoading(Graphics g)
    {
      int width = this.m_engine.getWidth();
      int height = this.m_engine.getHeight();
      AnimationManager.setColor(g, 1);
      g.fillRect(0, 0, width, height);
      this.m_engine.getLoadingRunAnimPlayer().drawAnim(g, 7 * (width >> 3), 7 * (height >> 3));
    }

    private void deinitLoading()
    {
      AnimationManager.unloadImage((int) ResourceManager.get("IDI_LOADING_RUN_PNG"));
    }

    private void initTitle()
    {
      this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_MENU_INTRO_TITLE"), true);
      this.m_engine.startFadeIn(false);
    }

    private void updateTitle(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
      {
        if (this.m_engine.isFading())
          return;
        this.stateActivate();
      }
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.m_titleDone = true;
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || this.m_engine.isFading())
          return;
        this.stateDeactivate();
      }
    }

    private void renderTitle(Graphics g)
    {
      this.m_engine.getQuadManager().setSplashCamera();
      this.m_engine.getQuadManager().render(g);
      this.m_engine.getQuadManager().resetCamera();
      if (!this.m_titleDone)
        return;
      this.m_engine.getTextManager().drawString(g, 2067, 8, this.m_engine.getWidth() >> 1, this.m_engine.getHeight() - 28, 66);
    }

    private void pointerReleaseTitle(int x, int y, int pointerNum)
    {
      if (!this.m_titleDone)
        return;
      this.stateTransitionFade(SceneMenu.MenuState.STATE_MAINMENU);
    }

    private void deinitTitle()
    {
      this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_MENU_INTRO_TITLE"), false);
    }

    private void initMainMenu()
    {
      LevelData levelData = AppEngine.getLevelData();
      if (MirrorsEdge.TrialMode)
        this.m_engine.getBGMusic().playMusic((int) ResourceManager.get("SOUNDEVENT_BGM_MENU"), 2);
      this.m_menuMain.create();
      int numUnlockedLevels = levelData.getNumUnlockedLevels();
      MenuStringChoice subMenu = (MenuStringChoice) this.m_menuMain.getSubMenu(0);
      int selectedItem = subMenu.getSelectedItem();
      subMenu.create(4, 2310);
      if (MirrorsEdge.TrialMode)
      {
        if (numUnlockedLevels == 0)
          subMenu.append(2074);
        else
          subMenu.append(2073);
      }
      else
      {
        subMenu.append(2074);
        if (numUnlockedLevels != 0)
          subMenu.append(2073);
        if (0 < numUnlockedLevels && numUnlockedLevels < levelData.getLevelNum())
          subMenu.append(2072);
      }
      if (MirrorsEdge.TrialMode && levelData.getLevel(0).isLevelComplete() && numUnlockedLevels > 0)
        subMenu.append(2076);
      if (!MirrorsEdge.TrialMode && levelData.isLevelComplete() && numUnlockedLevels > 0)
        subMenu.append(2076);
      subMenu.setSelectedItem(selectedItem);
      this.m_menuMain.wrapStrings(true);
      this.m_menuMain.reset();
      if (this.m_prevSubMenu != -1)
        this.m_menuMain.setSubMenuOpen(this.m_prevSubMenu, this.m_prevSubMenuItem);
      this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_MENU_MAIN"), true);
      this.initTickers();
    }

    private void updateMainMenu(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode && this.m_engine.getPendingChallengeCount() > 0)
      {
        this.stateTransition(SceneMenu.MenuState.STATE_PENDING_CHALLENGES);
        this.stateDeactivate();
      }
      else
      {
        this.m_menuMain.update(timeStepMillis);
        if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        {
          if (this.m_engine.isFading())
            return;
          this.stateActivate();
        }
        else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
        {
          if (!this.m_engine.isFading() && this.m_menuMain.isItemSelected())
            this.processMenuSelect();
          if (this.m_currentTicker != -1)
          {
            EASpywareManager instance = EASpywareManager.getInstance();
            int tickerCount = instance.getTickerCount();
            int index = (this.m_currentTicker + 1) % tickerCount;
            string tickerString1 = instance.getTickerString(this.m_currentTicker);
            string tickerString2 = instance.getTickerString(index);
            TextManager textManager = this.m_engine.getTextManager();
            int stringWidth1 = textManager.getStringWidth(tickerString1, this.TICKER_FONT);
            int stringWidth2 = textManager.getStringWidth(tickerString2, this.TICKER_FONT);
            this.m_currentTickerXPos -= (float) (50.0 * ((double) timeStepMillis / 1000.0));
            this.m_nextTickerXPos -= (float) (50.0 * ((double) timeStepMillis / 1000.0));
            if ((double) this.m_currentTickerXPos >= (double) -stringWidth1)
              return;
            this.m_currentTickerXPos = this.m_nextTickerXPos;
            int num = stringWidth2;
            this.m_currentTicker = (this.m_currentTicker + 1) % tickerCount;
            this.m_nextTickerXPos = this.m_currentTickerXPos + (float) num + (float) this.m_tickerStringSeparation;
            this.m_tickerURL = instance.getTickerURL(this.m_currentTicker);
          }
          else
            this.initTickers();
        }
        else
        {
          if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || this.m_engine.isFading())
            return;
          this.stateDeactivate();
        }
      }
    }

    private void renderMainMenu(Graphics g)
    {
      this.m_menuMain.render(g);
      if (this.m_currentTicker == -1)
        return;
      EASpywareManager instance = EASpywareManager.getInstance();
      int index = (this.m_currentTicker + 1) % instance.getTickerCount();
      string tickerString1 = instance.getTickerString(this.m_currentTicker);
      string tickerString2 = instance.getTickerString(index);
      TextManager textManager = this.m_engine.getTextManager();
      textManager.drawString(g, tickerString1, this.TICKER_FONT, (int) this.m_currentTickerXPos, 2, 9);
      textManager.drawString(g, tickerString2, this.TICKER_FONT, (int) this.m_nextTickerXPos, 2, 9);
    }

    private void pointerPressedMainMenu(int x, int y, int pointerNum)
    {
      this.m_menuMain.pointerPressed(x, y);
    }

    private void pointerDraggedMainMenu(int x, int y, int pointerNum)
    {
      this.m_menuMain.pointerDragged(x, y);
    }

    private void pointerReleaseMainMenu(int x, int y, int pointerNum)
    {
      if (this.m_menuMain.pointerReleased(x, y))
        this.jerkCityscape();
      MenuStringChoice selectedSubMenu = (MenuStringChoice) this.m_menuMain.getSelectedSubMenu();
      this.m_prevSubMenu = this.m_menuMain.getSelectionIndex();
      if (selectedSubMenu != null)
        this.m_prevSubMenuItem = selectedSubMenu.getSelectedIndex();
      if (y >= 15 || this.m_currentTicker == -1 || this.m_tickerURL == null || this.m_tickerURL.Length <= 0)
        return;
      EASpywareManager.getInstance().logEvent(30012);
      try
      {
        //new WebBrowserTask() { Uri = new Uri(this.m_tickerURL) }.Show();
        //TODO
      }
      catch (InvalidOperationException ex)
      {
      }
    }

    private void deinitMainMenu()
    {
      this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_MENU_MAIN"), false);
      this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_TICKER_BAR"), false);
    }

    private void initLevelSelectStory()
    {
      this.m_levelSelectWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.CHAPTER_SELECT_STORY) as ChapterSelectWindow;
    }

    private void updateLevelSelectStory(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || this.m_engine.isFading())
          return;
        this.stateDeactivate();
      }
    }

    private void initLevelSelectSpeedRun()
    {
      this.m_levelSelectWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.CHAPTER_SELECT_SPEEDRUN) as ChapterSelectWindow;
    }

    private void updateLevelSelectSpeedRun(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || this.m_engine.isFading())
          return;
        this.stateDeactivate();
      }
    }

    private void initGameIntro()
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_MENU_INTRO_GAME"), true);
      quadManager.playAnimAcross((int) QuadManager.get("ANIM_MENU_INTRO_GAME"), 2, 0, 1);
      this.m_introString.wrapString(2325, 7, this.m_engine.getWidth() - 20, false);
      this.m_engine.startFadeIn(true);
    }

    private void updateGameIntro(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
      {
        if (!this.m_engine.isFadedIn())
          return;
        this.stateActivate();
      }
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        QuadManager quadManager = this.m_engine.getQuadManager();
        if (quadManager.getAnimCurrentFrameIndex((int) QuadManager.get("ANIM_MENU_INTRO_GAME")) == 5 && !quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_INTRO_GAME")))
        {
          this.m_engine.startFadeOut(false, 16777215);
          this.m_statePhase = SceneMenu.StatePhase.STATE_PHASE_POST;
        }
        else
          quadManager.updateAnim((int) QuadManager.get("ANIM_MENU_INTRO_GAME"), timeStepMillis);
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || !this.m_engine.isFadedOut())
          return;
        SpywareManager.getInstance().trackNewGame();
        LevelData levelData = AppEngine.getLevelData();
        levelData.resetLevelData();
        levelData.setCurrentLevelByIndex(LevelData.GameMode.GAME_MODE_STORY, MirrorsEdge.TrialMode || this.m_needToStartTutorial ? 0 : 2);
        this.stateTransition(SceneMenu.MenuState.STATE_TRANSITION_TO_GAME);
        this.stateDeactivate();
      }
    }

    private void renderGameIntro(Graphics g)
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      TextManager textManager = this.m_engine.getTextManager();
      quadManager.render(g);
      this.m_introString.draw(g, 20, 40, 65);
      textManager.drawString(g, 2326, 7, 160, 155, 65);
      textManager.drawString(g, 2327, 7, 190, 175, 65);
      quadManager.render(g, 64);
      if (quadManager.getAnimCurrentFrameIndex((int) QuadManager.get("ANIM_MENU_INTRO_GAME")) == 5 || quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_INTRO_GAME")))
        return;
      int x = this.m_engine.getWidth() - 50;
      int y = 40 + this.m_introString.getWrappedTextHeight() + 10;
      textManager.drawString(g, 2067, 9, x, y, 12);
    }

    private void pointerReleaseGameIntro(int x, int y, int pointerNum)
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      if (quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_INTRO_GAME")) || quadManager.getAnimCurrentFrameIndex((int) QuadManager.get("ANIM_MENU_INTRO_GAME")) != 1)
        return;
      quadManager.playAnimAcross((int) QuadManager.get("ANIM_MENU_INTRO_GAME"), 2, 1, 5);
    }

    private void deinitGameIntro()
    {
      AppEngine.getCanvas().getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_MENU_INTRO_GAME"), false);
    }

    private void initSoundOptions()
    {
      this.m_soundOptionsWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.SOUND_OPTIONS) as SoundOptions;
    }

    private void updateSoundOptions(int timeStep)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() != WindowResult.WINDOW_RESULT_NONE)
        {
          this.m_engine.getWindowStore().clearWindowResult();
          this.processMenuBack();
        }
        else
        {
          SoundManager soundManager = this.m_engine.getSoundManager();
          float musicLevel = this.m_soundOptionsWindow.getMusicLevel();
          float effectsLevel = this.m_soundOptionsWindow.getEffectsLevel();
          float volumeMusic = soundManager.getVolumeMusic();
          float volumeSfx = soundManager.getVolumeSFX();
          if ((double) effectsLevel != (double) volumeSfx)
          {
            soundManager.setVolumeSFX(effectsLevel);
            soundManager.playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_POSITIVE"));
          }
          if ((double) musicLevel == (double) volumeMusic)
            return;
          soundManager.setVolumeMusic(musicLevel);
        }
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.m_engine.saveRMSAppSettings();
        this.stateDeactivate();
      }
    }

    private void initGameOptions()
    {
      this.m_gameOptionsWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.GAME_OPTIONS) as GameOptions;
    }

    private void updateGameOptions(int timeStep)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
        this.m_engine.saveGameOptions();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void precacheAchievementsWindow()
    {
      if (this.m_achievementsWindow != null)
        this.m_achievementsWindow = (AchievementWindow) null;
      this.m_achievementsWindow = new AchievementWindow();
      this.m_achievementsWindow.setDeleteOnClosed(false);
    }

    private void initAchievements()
    {
      this.m_achievementsWindow.clearClosed();
      this.m_engine.getWindowStore().pushWindow((Window) this.m_achievementsWindow);
    }

    private void updateAchievements(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void initHelp()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.HELP);
    }

    private void updateHelp(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void initAboutMenu()
    {
      if (this.m_aboutMenu == null)
        this.m_aboutMenu = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.ABOUT_MENU) as AboutMenu;
      else
        this.m_aboutMenu.setHidden(false);
    }

    private void updateAboutMenu(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.m_aboutMenu = (AboutMenu) null;
        this.m_engine.saveGameOptions();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void initAbout()
    {
      SpywareManager.getInstance().trackViewAbout();
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.ABOUT);
    }

    private void updateAbout(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateLeaderboard(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE || this.m_leaderboardWindow.IsHidden())
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void initMoreGames()
    {
      EASpywareManager.getInstance().showMoreGames();
      try
      {
        //new WebBrowserTask()
        //{
        //  Uri = new Uri("http://mg.eamobile.com/?rId=1560")
        //}.Show();
      }
      catch (InvalidOperationException ex)
      {
      }
      this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
      this.stateDeactivate();
    }

    private void initUnlockables()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.UNLOCKABLES);
    }

    private void updateUnlockables(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void renderUnlockables(Graphics g) => this.renderCityscape(g);

    private void initLanguage()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.LANGUAGE);
    }

    private void updateLanguage(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.processMenuBack();
        this.m_engine.saveGameOptions();
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void renderLanguage(Graphics g) => this.renderCityscape(g);

    private void deinitLanguage() => this.m_engine.clearAboutString();

    private void updateTransitionToGame(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.m_engine.changeScene(3);
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateExitConf(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        switch (this.m_engine.getWindowStore().getWindowResult())
        {
          case WindowResult.WINDOW_RESULT_POSITIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            TextManager textManager = this.m_engine.getTextManager();
            string uriString = "";
            switch (this.m_state)
            {
              case SceneMenu.MenuState.STATE_CONFIRM_EXIT_EULA:
                uriString = textManager.getString(2328);
                break;
              case SceneMenu.MenuState.STATE_CONFIRM_EXIT_PRIVACY:
                uriString = textManager.getString(2330);
                break;
              case SceneMenu.MenuState.STATE_CONFIRM_EXIT_TOS:
                uriString = textManager.getString(2329);
                break;
            }
            try
            {
              //new WebBrowserTask() { Uri = new Uri(uriString) }.Show();
              break;
            }
            catch (InvalidOperationException ex)
            {
              break;
            }
          case WindowResult.WINDOW_RESULT_NEGATIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
            break;
        }
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateNewGameConf(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        switch (this.m_engine.getWindowStore().getWindowResult())
        {
          case WindowResult.WINDOW_RESULT_POSITIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_CONFIRM_TUTORIAL);
            break;
          case WindowResult.WINDOW_RESULT_NEGATIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
            break;
        }
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateBuyFromLeaderboards(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        if (this.m_engine.getWindowStore().getWindowResult() != WindowResult.WINDOW_RESULT_NEGATIVE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateTutorialConf(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        if (this.m_engine.isFadedOut())
        {
          this.m_needToStartTutorial = true;
          this.stateTransition(SceneMenu.MenuState.STATE_GAME_INTRO);
        }
        this.updateCityscape(timeStepMillis);
        switch (this.m_engine.getWindowStore().getWindowResult())
        {
          case WindowResult.WINDOW_RESULT_POSITIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.m_needToStartTutorial = true;
            this.stateTransitionFade(SceneMenu.MenuState.STATE_GAME_INTRO);
            break;
          case WindowResult.WINDOW_RESULT_NEGATIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.m_needToStartTutorial = false;
            this.stateTransitionFade(SceneMenu.MenuState.STATE_GAME_INTRO);
            break;
        }
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || !this.m_engine.isFadedOut())
          return;
        this.stateDeactivate();
      }
    }

    private void updateEnableShareDataConf(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        switch (this.m_engine.getWindowStore().getWindowResult())
        {
          case WindowResult.WINDOW_RESULT_POSITIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_ENABLED);
            break;
          case WindowResult.WINDOW_RESULT_NEGATIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
            break;
        }
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateDisableShareDataConf(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        switch (this.m_engine.getWindowStore().getWindowResult())
        {
          case WindowResult.WINDOW_RESULT_POSITIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_CONFIRM_SHARE_DATA_DISABLED);
            break;
          case WindowResult.WINDOW_RESULT_NEGATIVE:
            this.m_engine.getWindowStore().clearWindowResult();
            this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
            break;
        }
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateShareDataEnabled(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void updateShareDataDisabled(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
        this.stateActivate();
      else if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
      {
        this.updateCityscape(timeStepMillis);
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
      }
      else
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST)
          return;
        this.stateDeactivate();
      }
    }

    private void initLiteUpsell()
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      TextManager textManager = this.m_engine.getTextManager();
      this.m_liteUpsellState = SceneMenu.UpsellAnimState.UPSELL_TRANS_TEXT_STATE;
      this.m_listUpsellScreenIndex = 0;
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_MENU_UPSELL"), true);
      quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_BACK_MESH_ARRAY[this.m_listUpsellScreenIndex], true);
      quadManager.setGroupVisible(SceneMenu.UPSELL_QUAD_REVEAL_GROUP_ARRAY[this.m_listUpsellScreenIndex], true);
      quadManager.playAnim((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_TEXT"), 2);
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_MENU_MAIN"), false);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_TITLE"), true);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_HUD_BAG_ICON_MENU"), false);
      this.m_liteUpsellTitleString.wrapString(textManager.getString(SceneMenu.UPSELL_STRING_TITLE_ARRAY[this.m_listUpsellScreenIndex]).ToUpper(), SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][0], this.m_engine.getWidth() - 10, true);
      this.m_liteUpsellBodyString.wrapString(SceneMenu.UPSELL_STRING_BODY_ARRAY[this.m_listUpsellScreenIndex], SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][1], this.m_engine.getWidth() - 10, false);
      this.m_engine.getBGMusic().playMusic((int) ResourceManager.get("SOUNDEVENT_BGM_GAME"), 2);
      SpywareManager.getInstance().trackEnterUpsellScreen();
    }

    private void updateLiteUpsell(int timeStepMillis)
    {
      if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_PRE)
      {
        QuadManager quadManager = this.m_engine.getQuadManager();
        switch (this.m_liteUpsellState)
        {
          case SceneMenu.UpsellAnimState.UPSELL_NEW_SCREEN_STATE:
            quadManager.updateAnim((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_SCREENS"), timeStepMillis);
            if (!quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_SCREENS")))
            {
              TextManager textManager = this.m_engine.getTextManager();
              this.m_liteUpsellState = SceneMenu.UpsellAnimState.UPSELL_TRANS_TEXT_STATE;
              quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_BACK_MESH_ARRAY[this.m_listUpsellScreenIndex], false);
              this.m_listUpsellScreenIndex = (this.m_listUpsellScreenIndex + 1) % 5;
              quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_BACK_MESH_ARRAY[this.m_listUpsellScreenIndex], true);
              quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_FRONT_MESH_ARRAY[this.m_listUpsellScreenIndex], false);
              quadManager.setGroupVisible(SceneMenu.UPSELL_QUAD_REVEAL_GROUP_ARRAY[this.m_listUpsellScreenIndex], true);
              quadManager.playAnim((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_TEXT"), 2);
              this.m_liteUpsellTitleString.wrapString(textManager.getString(SceneMenu.UPSELL_STRING_TITLE_ARRAY[this.m_listUpsellScreenIndex]).ToUpper(), SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][0], this.m_engine.getWidth() - 10, true);
              this.m_liteUpsellBodyString.wrapString(SceneMenu.UPSELL_STRING_BODY_ARRAY[this.m_listUpsellScreenIndex], SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][1], this.m_engine.getWidth() - 10, false);
              break;
            }
            break;
          case SceneMenu.UpsellAnimState.UPSELL_TRANS_TEXT_STATE:
            quadManager.updateAnim((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_TEXT"), timeStepMillis);
            if (!quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_TEXT")))
            {
              this.m_liteUpsellState = SceneMenu.UpsellAnimState.UPSELL_IDLE;
              quadManager.setGroupVisible(SceneMenu.UPSELL_QUAD_REVEAL_GROUP_ARRAY[this.m_listUpsellScreenIndex], false);
              quadManager.playAnim((int) QuadManager.get("ANIM_MENU_UPSELL_IDLE"), 2);
              break;
            }
            break;
          case SceneMenu.UpsellAnimState.UPSELL_IDLE:
            quadManager.updateAnim((int) QuadManager.get("ANIM_MENU_UPSELL_IDLE"), timeStepMillis);
            if (!quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_UPSELL_IDLE")))
            {
              this.m_liteUpsellState = SceneMenu.UpsellAnimState.UPSELL_NEW_SCREEN_STATE;
              quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_FRONT_MESH_ARRAY[(this.m_listUpsellScreenIndex + 1) % 5], true);
              quadManager.playAnim((int) QuadManager.get("ANIM_MENU_UPSELL_TRANS_SCREENS"), 2);
              break;
            }
            break;
        }
      }
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE)
      {
        if (this.m_engine.isFading())
          return;
        this.stateActivate();
      }
      else
      {
        if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_ACTIVE || this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_POST || this.m_engine.isFading())
          return;
        this.stateDeactivate();
      }
    }

    private void renderLiteUpsell(Graphics g)
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      TextManager textManager = this.m_engine.getTextManager();
      quadManager.render(g);
      int num1 = 0;
      int num2 = 0;
      int meshIndex1 = SceneMenu.UPSELL_QUAD_REVEAL_MESH_ARRAY[this.m_listUpsellScreenIndex][0];
      int meshIndex2 = SceneMenu.UPSELL_QUAD_REVEAL_MESH_ARRAY[this.m_listUpsellScreenIndex][1];
      if (this.m_liteUpsellState == SceneMenu.UpsellAnimState.UPSELL_TRANS_TEXT_STATE)
      {
        float alphaFactor1 = quadManager.getMesh(meshIndex1).mesh.getAlphaFactor();
        num1 = (int) ((double) alphaFactor1 * (double) alphaFactor1 * (double) alphaFactor1 * 40.0);
        float alphaFactor2 = quadManager.getMesh(meshIndex2).mesh.getAlphaFactor();
        num2 = (int) ((double) alphaFactor2 * (double) alphaFactor2 * (double) alphaFactor2 * 40.0);
      }
      int x1 = quadManager.getMeshLeft(meshIndex1) + 5 - num1;
      int y1 = quadManager.getMeshTop(meshIndex1) + 5;
      this.m_liteUpsellTitleString.drawWithFont(g, SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][0], x1, y1, 9, false, SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][0] - 1);
      int x2 = quadManager.getMeshLeft(meshIndex2) + 5 + num2;
      int y2 = y1 + this.m_liteUpsellTitleString.getWrappedTextHeight();
      this.m_liteUpsellBodyString.drawWithFont(g, SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][1], x2, y2, 9, false, SceneMenu.UPSELL_FONT_ARRAY[this.m_listUpsellScreenIndex][2]);
      quadManager.render(g, 64);
      int meshX1 = quadManager.getMeshX((int) QuadManager.get("MESH_MENU_UPSELL_MENU"));
      int meshY1 = quadManager.getMeshY((int) QuadManager.get("MESH_MENU_UPSELL_MENU"));
      int font = 7;
      StringRenderer stringRenderer = textManager.getStringRenderer(font);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(0);
      textManager.drawString(g, 2408, font, meshX1 + 1, meshY1 + 1, 66);
      stringRenderer.setColor(color);
      textManager.drawString(g, 2408, font, meshX1, meshY1, 66);
      int meshX2 = quadManager.getMeshX((int) QuadManager.get("MESH_MENU_UPSELL_BUY"));
      int meshY2 = quadManager.getMeshY((int) QuadManager.get("MESH_MENU_UPSELL_BUY"));
      textManager.drawString(g, 2409, 7, meshX2, meshY2, 66);
    }

    private void pointerReleaseLiteUpsell(int x, int y, int pointerNum)
    {
      if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
        return;
      QuadManager quadManager = this.m_engine.getQuadManager();
      if (quadManager.isPointWithinMesh((int) QuadManager.get("MESH_MENU_UPSELL_MENU"), x, y))
      {
        this.stateTransitionFade(SceneMenu.MenuState.STATE_MAINMENU);
        this.m_engine.getWindowStore().getButtonEffect().play(quadManager.getMeshX((int) QuadManager.get("MESH_MENU_UPSELL_MENU")), quadManager.getMeshY((int) QuadManager.get("MESH_MENU_UPSELL_MENU")));
      }
      else if (quadManager.isPointWithinMesh((int) QuadManager.get("MESH_MENU_UPSELL_BUY"), x, y))
      {
        SpywareManager.getInstance().trackOptToBuyFullVersion();
        //Guide.ShowMarketplace(PlayerIndex.One);
      }
      else
      {
        if (this.m_liteUpsellState != SceneMenu.UpsellAnimState.UPSELL_IDLE)
          return;
        this.m_engine.getQuadManager().stopAnim((int) QuadManager.get("ANIM_MENU_UPSELL_IDLE"));
      }
    }

    private void deinitLiteUpsell()
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_MENU_UPSELL"), false);
      this.m_liteUpsellTitleString.clearString();
      this.m_liteUpsellBodyString.clearString();
      switch (this.m_liteUpsellState)
      {
        case SceneMenu.UpsellAnimState.UPSELL_NEW_SCREEN_STATE:
          quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_BACK_MESH_ARRAY[this.m_listUpsellScreenIndex], false);
          quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_FRONT_MESH_ARRAY[(this.m_listUpsellScreenIndex + 1) % 5], false);
          break;
        case SceneMenu.UpsellAnimState.UPSELL_TRANS_TEXT_STATE:
          quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_BACK_MESH_ARRAY[this.m_listUpsellScreenIndex], false);
          quadManager.setGroupVisible(SceneMenu.UPSELL_QUAD_REVEAL_GROUP_ARRAY[this.m_listUpsellScreenIndex], false);
          break;
        case SceneMenu.UpsellAnimState.UPSELL_IDLE:
          quadManager.setMeshVisible(SceneMenu.UPSELL_QUAD_BACK_MESH_ARRAY[this.m_listUpsellScreenIndex], false);
          break;
      }
    }

    private void initGameCompleteResults()
    {
      AppEngine.getAchievementData().registerGameEnd();
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.END_OF_GAME_RESULTS);
    }

    private void updateGameCompleteResults(int timeStepMillis)
    {
      if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_POST && this.m_engine.isFadedOut())
      {
        this.stateDeactivate();
      }
      else
      {
        if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
          return;
        this.m_engine.getWindowStore().clearWindowResult();
        this.stateTransitionFade(SceneMenu.MenuState.STATE_MAINMENU);
      }
    }

    private void startNewGame(bool playTutorial)
    {
      SpywareManager.getInstance().trackNewGame();
      LevelData levelData = AppEngine.getLevelData();
      levelData.resetLevelData();
      levelData.setCurrentLevelByIndex(LevelData.GameMode.GAME_MODE_STORY, MirrorsEdge.TrialMode || playTutorial ? 0 : 2);
      this.stateTransitionFade(SceneMenu.MenuState.STATE_TRANSITION_TO_GAME);
    }

    private void processMenuSelect()
    {
      MenuStringChoice selectedSubMenu = (MenuStringChoice) this.m_menuMain.getSelectedSubMenu();
      this.m_prevSubMenu = this.m_menuMain.getSelectionIndex();
      this.m_prevSubMenuItem = selectedSubMenu.getSelectedIndex();
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_MAINMENU:
        case SceneMenu.MenuState.STATE_RETURNED_TO_MENU:
          switch (this.m_menuMain.getSelectedSubMenu().getSelectedItem())
          {
            case 2049:
              this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
              this.jerkCityscape();
              break;
            case 2051:
              this.stateTransition(SceneMenu.MenuState.STATE_HELP);
              this.jerkCityscape();
              break;
            case 2072:
              SpywareManager.getInstance().trackContinueGame();
              LevelData levelData = AppEngine.getLevelData();
              int levelIndex = Math.Min(levelData.getNumUnlockedLevels(), levelData.getLevelNum() - 1);
              AppEngine.getLevelData().setCurrentLevelByIndex(LevelData.GameMode.GAME_MODE_STORY, levelIndex);
              this.stateTransitionFade(SceneMenu.MenuState.STATE_TRANSITION_TO_GAME);
              break;
            case 2073:
              this.stateTransitionFade(SceneMenu.MenuState.STATE_LEVEL_SELECT_STORY);
              break;
            case 2074:
              if (MirrorsEdge.TrialMode)
              {
                this.m_needToStartTutorial = true;
                this.stateTransitionFade(SceneMenu.MenuState.STATE_GAME_INTRO);
                break;
              }
              if (AppEngine.getLevelData().getNumUnlockedLevels() != 0)
              {
                this.stateTransition(SceneMenu.MenuState.STATE_CONFIRM_NEW_GAME);
                break;
              }
              this.m_needToStartTutorial = true;
              this.stateTransition(SceneMenu.MenuState.STATE_GAME_INTRO);
              break;
            case 2076:
              this.stateTransitionFade(SceneMenu.MenuState.STATE_LEVEL_SELECT_SPEEDRUN);
              break;
            case 2077:
              if (!MirrorsEdge.TrialMode)
              {
                this.stateTransition(SceneMenu.MenuState.STATE_LEADERBOARD);
                this.jerkCityscape();
                break;
              }
              this.stateTransition(SceneMenu.MenuState.STATE_BUY_FROM_LEADERBOARDS);
              break;
            case 2082:
              this.stateTransition(SceneMenu.MenuState.STATE_GAME_OPTIONS);
              this.jerkCityscape();
              break;
            case 2085:
              SpywareManager.getInstance().trackViewBadges();
              this.stateTransition(SceneMenu.MenuState.STATE_ACHIEVEMENTS);
              this.jerkCityscape();
              break;
            case 2087:
              if (!MirrorsEdge.TrialMode)
              {
                this.m_menuMain.reset();
                this.jerkCityscape();
                break;
              }
              break;
            case 2263:
              SpywareManager.getInstance().trackOptionsSound();
              this.stateTransition(SceneMenu.MenuState.STATE_SOUND_OPTIONS);
              this.jerkCityscape();
              break;
            case 2305:
              if (!MirrorsEdge.TrialMode)
              {
                this.stateTransition(SceneMenu.MenuState.STATE_UNLOCKABLES);
                this.jerkCityscape();
                break;
              }
              break;
            case 2307:
              this.stateTransition(SceneMenu.MenuState.STATE_LANGUAGE);
              this.jerkCityscape();
              break;
          }
          break;
      }
      this.m_engine.getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_POSITIVE"));
    }

    public override void OnHardBackKeyEvent()
    {
      if (this.m_state == SceneMenu.MenuState.STATE_MAINMENU || this.m_state == SceneMenu.MenuState.STATE_RETURNED_TO_MENU)
        this.m_menuMain.OnHardBackKeyEvent();
      else if (this.m_state == SceneMenu.MenuState.STATE_LITE_UPSELL)
      {
        if (this.m_statePhase != SceneMenu.StatePhase.STATE_PHASE_ACTIVE)
          return;
        this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
        this.jerkCityscape();
      }
      else
      {
        if (this.m_state == SceneMenu.MenuState.STATE_ABOUT)
          return;
        if (this.m_state == SceneMenu.MenuState.STATE_TITLE)
        {
          if (this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE || this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_POST || !this.m_titleDone)
            return;
          MirrorsEdge.m_MirrorsEdge.Exit();
        }
        else
        {
          if (this.m_state != SceneMenu.MenuState.STATE_GAME_INTRO)
            return;
          QuadManager quadManager = this.m_engine.getQuadManager();
          if (quadManager.getAnimCurrentFrameIndex((int) QuadManager.get("ANIM_MENU_INTRO_GAME")) == 5 || quadManager.isAnimating((int) QuadManager.get("ANIM_MENU_INTRO_GAME")))
            return;
          this.stateTransition(SceneMenu.MenuState.STATE_RETURNED_TO_MENU);
          this.stateDeactivate();
        }
      }
    }

    private void processMenuBack()
    {
      switch (this.m_state)
      {
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_STORY:
        case SceneMenu.MenuState.STATE_LEVEL_SELECT_SPEEDRUN:
          this.stateTransitionFade(SceneMenu.MenuState.STATE_MAINMENU);
          break;
        case SceneMenu.MenuState.STATE_SOUND_OPTIONS:
        case SceneMenu.MenuState.STATE_GAME_OPTIONS:
        case SceneMenu.MenuState.STATE_HELP:
        case SceneMenu.MenuState.STATE_ABOUT_MENU:
        case SceneMenu.MenuState.STATE_LANGUAGE:
        case SceneMenu.MenuState.STATE_LITE_UPSELL:
          if (this.m_state == SceneMenu.MenuState.STATE_LANGUAGE)
          {
            if (this.m_engine.freeLoadingScreens(false))
              this.m_engine.loadLoadingScreens();
            this.precacheAchievementsWindow();
          }
          this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
          this.jerkCityscape();
          break;
        case SceneMenu.MenuState.STATE_ACHIEVEMENTS:
        case SceneMenu.MenuState.STATE_LEADERBOARD:
        case SceneMenu.MenuState.STATE_UNLOCKABLES:
          this.stateTransition(SceneMenu.MenuState.STATE_MAINMENU);
          this.jerkCityscape();
          break;
        case SceneMenu.MenuState.STATE_ABOUT:
          this.stateTransition(SceneMenu.MenuState.STATE_ABOUT_MENU);
          this.jerkCityscape();
          break;
      }
    }

    public void stateTransition(SceneMenu.MenuState newState)
    {
      this.stateTransition(newState, false);
    }

    public void stateTransitionFade(SceneMenu.MenuState newState)
    {
      this.stateTransition(newState, true);
    }

    private void stateTransition(SceneMenu.MenuState newState, bool fade)
    {
      if (this.m_state == newState)
        return;
      this.m_statePhaseTime = 0;
      this.m_nextState = newState;
      if (this.m_state == SceneMenu.MenuState.STATE_INVALID)
      {
        this.stateDeactivate();
      }
      else
      {
        this.m_stateTransitionFade = fade;
        if (fade)
          this.m_engine.startFadeOut(true);
        this.m_statePhase = SceneMenu.StatePhase.STATE_PHASE_POST;
        this.stateTransOut();
      }
    }

    private bool inStateTransition()
    {
      return this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_PRE || this.m_statePhase == SceneMenu.StatePhase.STATE_PHASE_POST;
    }

    private void stateActivate()
    {
      this.m_statePhase = SceneMenu.StatePhase.STATE_PHASE_ACTIVE;
      this.m_statePhaseTime = 0;
    }

    private void stateDeactivate()
    {
      this.deinitState();
      this.m_prevState = this.m_state;
      this.m_state = this.m_nextState;
      this.m_statePhase = SceneMenu.StatePhase.STATE_PHASE_PRE;
      this.m_statePhaseTime = 0;
      if (this.m_stateTransitionFade)
        this.m_engine.startFadeIn(true);
      this.initState();
    }

    private void initTickers()
    {
      EASpywareManager instance = EASpywareManager.getInstance();
      if (instance.getTickerCount() != 0)
      {
        this.m_currentTicker = 0;
        int stringWidth = this.m_engine.getTextManager().getStringWidth(instance.getTickerString(this.m_currentTicker), this.TICKER_FONT);
        this.m_tickerStringSeparation = this.m_engine.getWidth() - 100;
        this.m_currentTickerXPos = (float) this.m_engine.getWidth();
        this.m_nextTickerXPos = this.m_currentTickerXPos + (float) stringWidth + (float) this.m_tickerStringSeparation;
        this.m_tickerURL = instance.getTickerURL(this.m_currentTicker);
        this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_TICKER_BAR"), true);
        this.m_engine.getQuadManager().setMeshVisible((int) QuadManager.get("MESH_TICKER_BAR"), true);
      }
      else
        this.m_currentTicker = -1;
    }

    private enum StatePhase
    {
      STATE_PHASE_INVALID,
      STATE_PHASE_PRE,
      STATE_PHASE_ACTIVE,
      STATE_PHASE_POST,
    }

    public enum MenuState
    {
      STATE_INVALID = -1, // 0xFFFFFFFF
      STATE_LOADING = 0,
      STATE_FILESAVEERROR = 1,
      STATE_TITLE = 2,
      STATE_MAINMENU = 3,
      STATE_LEVEL_SELECT_STORY = 4,
      STATE_LEVEL_SELECT_SPEEDRUN = 5,
      STATE_GAME_INTRO = 6,
      STATE_SOUND_OPTIONS = 7,
      STATE_GAME_OPTIONS = 8,
      STATE_ACHIEVEMENTS = 9,
      STATE_MEDIAPICKER = 10, // 0x0000000A
      STATE_HELP = 11, // 0x0000000B
      STATE_ABOUT_MENU = 12, // 0x0000000C
      STATE_ABOUT = 13, // 0x0000000D
      STATE_PRE_LEADERBOARD = 14, // 0x0000000E
      STATE_LEADERBOARD = 15, // 0x0000000F
      STATE_J2PLAY_NOT_LOGGED_IN = 16, // 0x00000010
      STATE_J2PLAY_LEADERBOARD = 17, // 0x00000011
      STATE_MOREGAMES = 18, // 0x00000012
      STATE_UNLOCKABLES = 19, // 0x00000013
      STATE_LANGUAGE = 20, // 0x00000014
      STATE_EXITGAME = 21, // 0x00000015
      STATE_RETURNED_TO_MENU = 22, // 0x00000016
      STATE_TRANSITION_TO_GAME = 23, // 0x00000017
      STATE_PROMPT_MEDIA_PICKER_UNAVAILABLE = 24, // 0x00000018
      STATE_CONFIRM_EXIT_EULA = 25, // 0x00000019
      STATE_CONFIRM_EXIT_PRIVACY = 26, // 0x0000001A
      STATE_CONFIRM_EXIT_TOS = 27, // 0x0000001B
      STATE_CONFIRM_NEW_GAME = 28, // 0x0000001C
      STATE_CONFIRM_TUTORIAL = 29, // 0x0000001D
      STATE_CONFIRM_ENABLE_SHARE_DATA = 30, // 0x0000001E
      STATE_CONFIRM_DISABLE_SHARE_DATA = 31, // 0x0000001F
      STATE_CONFIRM_SHARE_DATA_ENABLED = 32, // 0x00000020
      STATE_CONFIRM_SHARE_DATA_DISABLED = 33, // 0x00000021
      STATE_GAME_COMPLETE_RESULTS = 34, // 0x00000022
      STATE_PENDING_CHALLENGES = 35, // 0x00000023
      STATE_LAUNCHING_CHALLENGE = 36, // 0x00000024
      STATE_CHALLENGE_NONETWORK = 37, // 0x00000025
      STATE_LAUNCH_CHALLENGE_FAILED = 38, // 0x00000026
      STATE_LITE_UPSELL = 39, // 0x00000027
      STATE_NO_STORAGE = 40, // 0x00000028
      STATE_BUY_FROM_LEADERBOARDS = 41, // 0x00000029
    }

    private enum LoadingThreadState
    {
      LOADINGTHREAD_STATE_IDLE,
      LOADINGTHREAD_STATE_WAIT,
      LOADINGTHREAD_STATE_QUIT,
    }

    private enum ChallengeGetPhase
    {
      CHALLENGEPHASE_INITIAL,
      CHALLENGEPHASE_FACEBOOK_LOGIN,
      CHALLENGEPHASE_GET_USER,
      CHALLENGEPHASE_GET_TOKEN,
      CHALLENGEPHASE_GET_CHALLENGE,
      CHALLENGEPHASE_ERROR,
    }

    private enum UpsellAnimState
    {
      UPSELL_NEW_SCREEN_STATE,
      UPSELL_TRANS_TEXT_STATE,
      UPSELL_IDLE,
    }
  }
}
