
// Type: game.SceneGame
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using midp;
using GameManager;
using support;
using System;
using System.Threading;
using text;
using UI;
using System.Threading.Tasks;

#nullable disable
namespace game
{
  public class SceneGame : Scene
  {
    private const int INTRO_DURATION = 5000;
    public const int NO_GESTURES = 0;
    public const int GESTURE_LEFT = 1;
    public const int GESTURE_RIGHT = 2;
    public const int GESTURE_UP = 4;
    public const int GESTURE_DOWN = 8;
    public const int GESTURE_TAP = 16;
    public const int GESTURE_HOLD = 32;
    public const int GESTURE_ALL = 63;
    public const int GESTURE_FIRST_POINTER = 1;
    public const int GESTURE_SECOND_POINTER = 64;
    public const int GESTURE_BOTH_POINTERS = 65;
    public const int GESTURE_FIRST_LEFT = 1;
    public const int GESTURE_FIRST_RIGHT = 2;
    public const int GESTURE_FIRST_UP = 4;
    public const int GESTURE_FIRST_DOWN = 8;
    public const int GESTURE_FIRST_TAP = 16;
    public const int GESTURE_FIRST_HOLD = 32;
    public const int GESTURE_FIRST_ALL = 63;
    public const int GESTURE_SECOND_LEFT = 64;
    public const int GESTURE_SECOND_RIGHT = 128;
    public const int GESTURE_SECOND_UP = 256;
    public const int GESTURE_SECOND_DOWN = 512;
    public const int GESTURE_SECOND_TAP = 1024;
    public const int GESTURE_SECOND_HOLD = 2048;
    public const int GESTURE_SECOND_ALL = 4032;
    public const int GESTURE_BOTH_LEFT = 65;
    public const int GESTURE_BOTH_RIGHT = 130;
    public const int GESTURE_BOTH_UP = 260;
    public const int GESTURE_BOTH_DOWN = 520;
    public const int GESTURE_BOTH_TAP = 1040;
    public const int GESTURE_BOTH_HOLD = 2080;
    public const int GESTURE_BOTH_ALL = 4095;
    private const int ACC_TILT_INTERP_TIME = 300;
    private const string SAVE_STATE_FILENAME = "SceneGameState";
    private const float NEAR_CLIP_GAME = 3.5f;
    private const float NEAR_CLIP_CUTSCENE = 1f;
    private const float FAR_CLIP = 250f;
    public const int FREELOOK_BUTTON_SIZE = 45;
    public const int LOADINGTHREAD_STATE_IDLE = 0;
    public const int LOADINGTHREAD_STATE_WAIT = 1;
    public const int LOADINGTHREAD_STATE_QUIT = 2;
    public const int LOADING_STATE_INIT = 0;
    public const int LOADING_STATE_ANIMS = 1;
    public const int LOADING_STATE_3D = 2;
    public const int LOADING_STATE_OBJECTS = 3;
    public const int LOADING_STATE_MAP = 4;
    public const int LOADING_STATE_SOUNDS = 5;
    public const int LOADING_STATE_CLEAR_CACHE = 6;
    public const int LOADING_STATE_RENDERING_FIRST_PASS = 7;
    public const int LOADING_STATE_WARMING_SHADER_CACHE = 8;
    public const int LOADING_STATE_WARMING_SHADER_CACHE_2 = 9;
    public const int LOADING_STATE_WARMING_SHADER_CACHE_3 = 10;
    public const int LOADING_STATE_FIRST_PASS_QUADS = 11;
    public const int LOADING_STATE_FINISHING_FIRST_PASS = 12;
    public const int LOADING_STATE_FINISHED = 13;
    public const int BLOOM_INTERP_TIME = 500;
    public const float BLOOM_MIX_NORMAL = 1f;
    public const float BLOOM_MIX_BLURRY = 0.5f;
    public const float BLOOM_MIX_BLURRY_DUST = 0.75f;
    public const float BLOOM_BLEND_NORMAL = 0.0f;
    public const float BLOOM_BLEND_BLURRY = 0.9f;
    public const int BAG_COUNT_DISPLAY_TIME = 4000;
    private const float LOADING_SCREEN_DRAG_SCALE = 0.002f;
    private const int COUNTDOWN_X = 355;
    private const int COUNTDOWN_Y = 50;
    private const int MAX_GO_TIME = 2000;
    private World m_world;
    private Camera m_m3gCamera;
    private GameCamera m_gameCamera;
    private MathFrustum m_gameCameraFrustum;
    private MEdgeMap m_map;
    private int m_raceTimeMillis;
    private int m_gestures;
    private SceneGamePointer m_firstPointer;
    private SceneGamePointer m_secondPointer;
    private SignalFilter m_accTiltFilter;
    private float m_accTiltVelocity;
    private int ACC_TILT_INTERP_TYPE = 1;
    private bool m_freelookEnabled;
    private Transform m_freelookTransform = new Transform();
    private MathVector m_freelookPosition;
    private float m_freelookRotationAzimuthRad;
    private float m_freelookRotationElevationRad;
    private MathQuaternion m_freelookRotation;
    private int m_freelookXYPosId;
    private int m_freelookXYPosX;
    private int m_freelookXYPosY;
    private int m_freelookZPosId;
    private int m_freelookZPosY;
    private int m_freelookRotationId;
    private int m_freelookRotationX;
    private int m_freelookRotationY;
    private bool m_freelookHideFlag;
    private float[] v_for_updateCamera = new float[3];
    private SceneGame.GameState m_state;
    private SceneGame.GameState m_nextState;
    private SceneGame.GameState m_prevState;
    private SceneGame.GameState m_preMenuState;
    private int m_stateTime;
    private bool m_gameRunning;
    private Thread m_loadingThread;
    private int m_loadingThreadState;
    private int m_loadingState;
    private SceneGame.GameState m_postLoadingState;
    private int BLOOM_INTERP_TYPE;
    private SignalFilter m_bloomMixFilter;
    private SignalFilter m_bloomBlendFilter;
    private static Microsoft.Xna.Framework.Graphics.Texture2D painTexture 
            = (Microsoft.Xna.Framework.Graphics.Texture2D) null;
    private static byte[] painTextureData0 = new byte[65536];
    private static byte[] painTextureData = new byte[65536];
    private static BlendState blendStateAdd;
    private SoundOptions m_soundOptionsWindow;
    private SignalFilter m_sfxVolumeFilter;
    private SceneGame.GameState m_prePopupState;
    private int m_finishedGameTimer;
    private MenuPause m_menuPaused;
    private int m_bagCountDisplayTimer;
    private int m_playerHurtSoundHandle;
    private int m_topMessageStringId;
    private int m_topMessageSubStringId;
    private int m_topMessageSection;
    private InterpolationFloatTimed m_topMessageInterpolation;
    private InterpolationFloatTimed m_topSubMessageInterpolation;
    private SoundSequencer m_soundSequencer;
    private SoundEventPoolManager m_soundPoolManager;
    private SignalFilter m_gameTimeFactorFilter;
    private int m_combatTimeDuration;
    public AchievementData m_achievementData;
    private float m_prevDragY;
    private float m_prevStartX;
    private float m_prevStartY;
    private bool m_draggingLoadScreen;
    private GameObjectGhost m_ghostObject;
    private int m_lastSpeedRunTime;
    private int m_lastSpeedRunRecord;
    private bool m_recordWasBroken;
    private bool m_firstComplete;
    private SceneGame.GameState m_exitState;
    public static string LAST_GHOST_ANIMATION_FILENAME = "lastGhostAnimation";
    public string m_displayName;
    public string m_challengeOpponentID;
    public string m_challengeComment;
    private PleaseWaitWindow m_pleaseWaitWindow;
    private SceneGame.MayhemWaitPhase m_mayhemWaitPhase;
    private byte[] localBuffer = new byte[10240];
    private int COUNTDOWN_FONT = 4;
    private int m_goTime;

    public World getM3GWorld() => this.m_world;

    public GameCamera getCamera() => this.m_gameCamera;

    public MathFrustum getCameraFrustum() => this.m_gameCameraFrustum;

    public MEdgeMap getMap() => this.m_map;

    public int getRaceTime() => this.m_raceTimeMillis;

    public float getAccelerometerTilt() => this.m_accTiltFilter.getFilteredValue();

    public float getAccelerometerTiltVelocity() => this.m_accTiltVelocity;

    public SceneGame(AppEngine app)
      : base(app)
    {
      this.m_world = (World) null;
      this.m_m3gCamera = (Camera) null;
      this.m_gameCamera = (GameCamera) null;
      this.m_gameCameraFrustum = new MathFrustum();
      this.m_map = new MEdgeMap();
      this.m_raceTimeMillis = 0;
      this.m_gestures = 0;
      this.m_firstPointer = new SceneGamePointer();
      this.m_secondPointer = new SceneGamePointer();
      this.m_accTiltFilter = new SignalFilter(this.ACC_TILT_INTERP_TYPE, 300f, 0.0f);
      this.m_accTiltVelocity = 0.0f;
      this.m_freelookEnabled = false;
      this.m_freelookRotationAzimuthRad = 0.0f;
      this.m_freelookRotationElevationRad = 0.0f;
      this.m_freelookXYPosId = -1;
      this.m_freelookXYPosX = 0;
      this.m_freelookXYPosY = 0;
      this.m_freelookZPosId = -1;
      this.m_freelookZPosY = 0;
      this.m_freelookRotationId = -1;
      this.m_freelookRotationX = 0;
      this.m_freelookRotationY = 0;
      this.m_freelookHideFlag = false;
      this.m_state = SceneGame.GameState.STATE_INVALID;
      this.m_nextState = SceneGame.GameState.STATE_INVALID;
      this.m_prevState = SceneGame.GameState.STATE_INVALID;
      this.m_preMenuState = SceneGame.GameState.STATE_INVALID;
      this.m_stateTime = 0;
      this.m_gameRunning = false;
      this.m_loadingThread = (Thread) null;
      this.m_loadingThreadState = 0;
      this.m_loadingState = 0;
      this.m_postLoadingState = SceneGame.GameState.STATE_INTRO;
      this.m_bloomMixFilter = new SignalFilter(this.BLOOM_INTERP_TYPE, 500f, 1f);
      this.m_bloomBlendFilter = new SignalFilter(this.BLOOM_INTERP_TYPE, 500f, 0.0f);
      this.m_soundPoolManager = new SoundEventPoolManager();
      this.m_soundSequencer = new SoundSequencer(this, app.getSoundManager(), this.m_soundPoolManager);
      this.m_topMessageStringId = 2048;
      this.m_topMessageSubStringId = 2048;
      this.m_topMessageSection = 0;
      this.m_topMessageInterpolation = new InterpolationFloatTimed();
      this.m_topSubMessageInterpolation = new InterpolationFloatTimed();
      this.m_gameTimeFactorFilter = new SignalFilter(0, 500f, 1f);
      this.m_combatTimeDuration = 0;
      this.m_prePopupState = SceneGame.GameState.STATE_GAME;
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData = AppEngine.getAchievementData();
      this.m_bagCountDisplayTimer = 0;
      this.m_playerHurtSoundHandle = -1;
      this.m_soundOptionsWindow = (SoundOptions) null;
      this.m_menuPaused = new MenuPause();
      this.m_prevDragY = 0.0f;
      this.m_prevStartX = 0.0f;
      this.m_prevStartY = 0.0f;
      this.m_draggingLoadScreen = false;
      this.m_finishedGameTimer = 0;
      this.m_sfxVolumeFilter = new SignalFilter(0, 800f, 0.0f);
      this.m_ghostObject = (GameObjectGhost) null;
      this.m_lastSpeedRunTime = -1;
      this.m_lastSpeedRunRecord = -1;
      this.m_recordWasBroken = false;
      this.m_firstComplete = false;
      this.m_exitState = SceneGame.GameState.STATE_INVALID;
      this.m_displayName = (string) null;
      this.m_mayhemWaitPhase = SceneGame.MayhemWaitPhase.PHASE_NONE;
      this.m_pleaseWaitWindow = (PleaseWaitWindow) null;
      this.m_goTime = -1;
      this.m_challengeOpponentID = (string) null;
      this.m_challengeComment = (string) null;
      this.m_freelookRotation.Constructor();
      this.m_soundSequencer.loadData();
    }

    public override void Destructor()
    {
      this.m_map.freeMap();
      this.m_map.Destructor();
      this.m_map = (MEdgeMap) null;
      this.m_gameCameraFrustum.Destructor();
      this.m_gameCameraFrustum = (MathFrustum) null;
      if (this.m_world != null)
      {
        this.m_world.Destructor();
        this.m_world = (World) null;
        this.m_m3gCamera.Destructor();
        this.m_m3gCamera = (Camera) null;
        this.m_gameCamera.Destructor();
        this.m_gameCamera = (GameCamera) null;
      }
      this.m_accTiltFilter.Destructor();
      this.m_accTiltFilter = (SignalFilter) null;
      this.m_bloomMixFilter.Destructor();
      this.m_bloomMixFilter = (SignalFilter) null;
      this.m_bloomBlendFilter.Destructor();
      this.m_bloomBlendFilter = (SignalFilter) null;
      this.m_soundPoolManager.Destructor();
      this.m_soundPoolManager = (SoundEventPoolManager) null;
      this.m_soundSequencer.Destructor();
      this.m_soundSequencer = (SoundSequencer) null;
      AppEngine.getM3GAssets().freeCaches(4);
      this.m_topMessageInterpolation.Destructor();
      this.m_topMessageInterpolation = (InterpolationFloatTimed) null;
      this.m_topSubMessageInterpolation.Destructor();
      this.m_topSubMessageInterpolation = (InterpolationFloatTimed) null;
      this.m_gameTimeFactorFilter.Destructor();
      this.m_gameTimeFactorFilter = (SignalFilter) null;
      if (this.m_soundOptionsWindow != null)
      {
        this.m_soundOptionsWindow.Destructor();
        this.m_soundOptionsWindow = (SoundOptions) null;
      }
      this.m_menuPaused.Destructor();
      this.m_menuPaused = (MenuPause) null;
      this.m_sfxVolumeFilter.Destructor();
      this.m_sfxVolumeFilter = (SignalFilter) null;
      if (this.m_ghostObject != null)
      {
        this.m_ghostObject.Destructor();
        this.m_ghostObject = (GameObjectGhost) null;
      }
      if (this.m_pleaseWaitWindow != null)
      {
        this.m_pleaseWaitWindow.Destructor();
        this.m_pleaseWaitWindow = (PleaseWaitWindow) null;
      }
      if (!MirrorsEdge.TrialMode)
      {
        this.m_achievementData.registerLevelEnd();
        this.m_achievementData = (AchievementData) null;
      }
      base.Destructor();
    }

    public override void start(int initialState)
    {
      this.m_state = SceneGame.GameState.STATE_LOADING;
      this.m_engine.initLoadingScreen(AppEngine.getLevelData().getCurrentLevelObject().getLoadingScreen());
      this.m_engine.startFadeIn(false);
      if (MirrorsEdge.TrialMode)
        this.m_engine.getBGMusic().playMusic((int) ResourceManager.get("SOUNDEVENT_BGM_AMBIENCE_03"), 2);
      else
        this.m_engine.getBGMusic().playMusic((int) ResourceManager.get("SOUNDEVENT_BGM_AMBIENCE_01"), 2);
      LevelData levelData = AppEngine.getLevelData();
      int gameMode = (int) levelData.getGameMode();
      int currentLevelIndex = levelData.getCurrentLevelIndex();
      switch (gameMode)
      {
        case 0:
          SpywareManager.getInstance().trackStoryLevelStarted(currentLevelIndex);
          break;
        case 1:
          SpywareManager.getInstance().trackRaceLevelStarted(currentLevelIndex);
          break;
        case 2:
          if (MirrorsEdge.TrialMode)
            break;
          goto case 1;
      }
      if (!MirrorsEdge.TrialMode)
        return;
      SpywareManager.getInstance().trackDemoStart();
    }

    public override void pause()
    {
      if (this.m_engine.getBGMusic() != null)
        this.m_engine.getBGMusic().suspend();
      if (this.m_state != SceneGame.GameState.STATE_INTRO && this.m_state != SceneGame.GameState.STATE_GAME && this.m_state != SceneGame.GameState.STATE_PLAYER_DIED && this.m_state != SceneGame.GameState.STATE_CUTSCENE && this.m_state != SceneGame.GameState.STATE_TRANS_TO_NEXT)
        return;
      this.m_engine.setFadeColor(16777215);
      this.m_engine.startFadeIn(true);
      this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
      this.m_sfxVolumeFilter.setSteadyState(0.0f);
      this.m_engine.getSoundManager().setVolumeGroup(2, 0.0f);
      this.m_engine.getSoundManager().setVolumeGroup(5, 0.0f);
    }

    public override void resume()
    {
      if (this.m_engine.getBGMusic() == null || MirrorsEdge.TrialMode && this.m_state == SceneGame.GameState.STATE_MEDIAPICKER)
        return;
      this.m_engine.getBGMusic().resume();
    }

    public override void end()
    {
      this.unloadSounds();
      QuadManager quadManager = this.m_engine.getQuadManager();
      quadManager.freeQuads((int) QuadManager.get("GROUP_SCENEGAME"));
      quadManager.freeQuads((int) QuadManager.get("GROUP_LEVEL_COMPLETE_BACKGROUNDS"));
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_SPEEDRUN_STARS_LEVEL_COMPLETE"), false);
      this.stateTransition(SceneGame.GameState.STATE_INVALID);
      int num = this.m_gameRunning ? 1 : 0;
      if (!MirrorsEdge.TrialMode)
        return;
      SpywareManager.getInstance().trackDemoEnd();
    }

    private void loadState()
    {
    }

    private void saveState() => SceneStartup.setCurrentLevelToAutoStart();

    public void updateCamera(int timeStepMillis)
    {
      this.m_freelookEnabled = false;
      this.m_gameCamera.update(timeStepMillis);
      this.updateCameraFrustum();
      this.m_gameCamera.getM3GCamera().getTranslation(ref this.v_for_updateCamera);
    }

    public void updateCameraFreeLook(int timeStepMillis)
    {
      if (this.m_freelookEnabled)
      {
        float num1 = (float) timeStepMillis * (1f / 1000f);
        if (this.m_freelookRotationId != -1)
        {
          int num2 = this.m_engine.getWidth() >> 1;
          int num3 = this.m_engine.getHeight() >> 1;
          int val1_1 = num2 - 45 >> 1;
          int val1_2 = num3 - 45 << 1;
          int val2_1 = this.m_freelookRotationX - (num2 + val1_1);
          int num4 = Math.Max(-val1_1, Math.Min(val1_1, val2_1));
          int val2_2 = this.m_freelookRotationY - num3;
          int num5 = Math.Max(-val1_2, Math.Min(val1_2, val2_2));
          this.m_freelookRotationAzimuthRad += (float) num4 * 0.02f * num1;
          this.m_freelookRotationElevationRad += (float) num5 * 0.02f * num1;
          while ((double) this.m_freelookRotationAzimuthRad < -1.0 * Math.PI)
            this.m_freelookRotationAzimuthRad += 5.141593f;
          while (Math.PI < (double) this.m_freelookRotationAzimuthRad)
            this.m_freelookRotationAzimuthRad -= 5.141593f;
          float val1_3 = 1.2566371f;
          this.m_freelookRotationElevationRad = 
                        Math.Max(-val1_3, Math.Min(val1_3, this.m_freelookRotationElevationRad));
          this.m_freelookRotation.setIdentity();
          this.m_freelookRotation.addToElevation(this.m_freelookRotationElevationRad);
          this.m_freelookRotation.addToAzimuth(this.m_freelookRotationAzimuthRad);
        }
        if (this.m_freelookXYPosId != -1 || this.m_freelookZPosId != -1)
        {
          MathVector vec = new MathVector();
          if (this.m_freelookXYPosId != -1)
          {
            int num6 = this.m_engine.getWidth() >> 1;
            int num7 = this.m_engine.getHeight() >> 1;
            int val1_4 = num6 - 45 >> 1;
            int val1_5 = num7 - 45 << 1;
            int val2_3 = this.m_freelookXYPosX - (num6 - val1_4);
            int num8 = Math.Max(-val1_4, Math.Min(val1_4, val2_3));
            int val2_4 = num7 - this.m_freelookXYPosY;
            int num9 = Math.Max(-val1_5, Math.Min(val1_5, val2_4));
            vec.x = (float) num8 * 0.1f * num1;
            vec.y = (float) num9 * 0.1f * num1;
          }
          if (this.m_freelookZPosId != -1)
          {
            int num10 = this.m_engine.getHeight() >> 1;
            int val1 = num10 - 45 << 1;
            int val2 = this.m_freelookZPosY - num10;
            int num11 = Math.Max(-val1, Math.Min(val1, val2));
            vec.z = (float) num11 * 0.05f * num1;
          }
          this.m_freelookRotation.applyToVector(ref vec);
          this.m_freelookPosition += vec;
        }
        this.m_freelookTransform.setIdentity();
        this.m_freelookTransform.postTranslate(this.m_freelookPosition.x, 
            this.m_freelookPosition.y, this.m_freelookPosition.z);
        this.m_freelookRotation.applyToTransform(ref this.m_freelookTransform);
        this.m_freelookTransform.postRotate(90f, 0.0f, 0.0f, 1f);
        this.m_m3gCamera.setTransform(ref this.m_freelookTransform);
      }
      else
      {
        this.m_freelookEnabled = true;
        float lookFromX = this.m_gameCamera.getLookFromX();
        float lookFromY = this.m_gameCamera.getLookFromY();
        float lookFromZ = this.m_gameCamera.getLookFromZ();
        float lookAtX = this.m_gameCamera.getLookAtX();
        float lookAtY = this.m_gameCamera.getLookAtY();
        float lookAtZ = this.m_gameCamera.getLookAtZ();
        this.m_freelookPosition.set(lookFromX, lookFromY, lookFromZ);
        MathTrig.convertLookVectorToEulerRotationsRad(lookAtX - lookFromX, 
            lookAtY - lookFromY, lookAtZ - lookFromZ, ref this.m_freelookRotationAzimuthRad, 
            ref this.m_freelookRotationElevationRad);
        this.m_freelookRotationElevationRad = -this.m_freelookRotationElevationRad;
        this.m_freelookRotationAzimuthRad = -this.m_freelookRotationAzimuthRad;
        this.m_freelookRotation.setIdentity();
        this.m_freelookRotation.addToElevation(this.m_freelookRotationElevationRad);
        this.m_freelookRotation.addToAzimuth(this.m_freelookRotationAzimuthRad);
      }
    }

    public void updateCameraFrustum()
    {
      this.m_gameCameraFrustum.set(new MathVector(this.m_gameCamera.getLookFromX(), 
          this.m_gameCamera.getLookFromY(), this.m_gameCamera.getLookFromZ()), 
          new MathVector(this.m_gameCamera.getLookAtX(), this.m_gameCamera.getLookAtY(), 
          this.m_gameCamera.getLookAtZ()), this.m_gameCamera.getNearClip(), 
          this.m_gameCamera.getFarClip(), this.m_gameCamera.getFOV());
    }

    public void startVerticalCameraBump(float mag) => this.m_gameCamera.startVerticalBump(mag);

    public void startCameraShake(float mag, int duration)
    {
      this.m_gameCamera.startShake(mag, duration);
    }

    private void renderGameFreelook(midp.Graphics g)
    {
      if (this.m_freelookHideFlag)
        return;
      int width = this.m_engine.getWidth();
      int height1 = this.m_engine.getHeight();
      int x = width >> 1;
      int y = height1 >> 1;
      int num1 = x - 45;
      int height2 = height1 - 90;
      int num2 = num1 >> 1;
      g.setColor(0, 0, 0);
      g.fillRect(0, 45, width, 2);
      g.fillRect(0, height1 - 45, width, 2);
      g.fillRect(45, 45, 2, height2);
      g.fillRect(x, 45, 2, height2);
      g.fillRect(width - 45, 45, 2, height2);
      g.fillRect(width - 45, 90, 45, 2);
      g.fillRect(width - 45, height1 - 90, 45, 2);
      g.fillRect(x - num2, 45, 1, height2);
      g.fillRect(x + num2, 45, 1, height2);
      g.fillRect(0, y, width - 45, 1);
    }

    public override void OnHardBackKeyEvent()
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_LOADING:
          if (this.m_loadingProgress != 100)
            break;
          this.m_engine.deinitLoadingScreen();
          this.deinitState();
          this.m_gameRunning = false;
          this.m_engine.changeScene(2, 3);
          break;
        case SceneGame.GameState.STATE_INTRO:
        case SceneGame.GameState.STATE_GAME:
          SpywareManager.getInstance().trackGamePaused();
          this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          if (!this.m_menuPaused.OnHardBackKeyEvent())
            break;
          SpywareManager.getInstance().trackResumeLevel(AppEngine.getLevelData().getCurrentLevelIndex());
          this.stateTransition(this.m_preMenuState);
          this.m_preMenuState = SceneGame.GameState.STATE_INVALID;
          break;
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          this.m_gameRunning = false;
          this.m_engine.changeScene(2, 3);
          break;
      }
    }

    public void pointerPressedFreelook(int x, int y, int pointerNum)
    {
      if (this.m_freelookHideFlag)
        return;
      int width = this.m_engine.getWidth();
      int height = this.m_engine.getHeight();
      int num1 = width >> 1;
      if (y < 45 || height - 45 < y)
        return;
      if (width - 45 < x)
      {
        int num2 = 90;
        if (height - num2 >= y)
          return;
        this.m_freelookPosition.set(this.m_gameCamera.getLookFromX(), 
            this.m_gameCamera.getLookFromY(), this.m_gameCamera.getLookFromZ());
        this.m_freelookRotationAzimuthRad = 0.0f;
        this.m_freelookRotationElevationRad = 0.0f;
        this.m_freelookRotation.setIdentity();
      }
      else if (45 < x && x < num1)
      {
        this.m_freelookXYPosId = pointerNum;
        this.m_freelookXYPosX = x;
        this.m_freelookXYPosY = y;
      }
      else if (x < 45)
      {
        this.m_freelookZPosId = pointerNum;
        this.m_freelookZPosY = y;
      }
      else
      {
        if (num1 >= x || x >= width - 45)
          return;
        this.m_freelookRotationId = pointerNum;
        this.m_freelookRotationX = x;
        this.m_freelookRotationY = y;
      }
    }

    public void pointerDraggedFreelook(int x, int y, int pointerNum)
    {
      if (this.m_freelookHideFlag)
        return;
      if (this.m_freelookXYPosId == pointerNum)
      {
        this.m_freelookXYPosX = x;
        this.m_freelookXYPosY = y;
      }
      else if (this.m_freelookZPosId == pointerNum)
      {
        this.m_freelookZPosY = y;
      }
      else
      {
        if (this.m_freelookRotationId != pointerNum)
          return;
        this.m_freelookRotationX = x;
        this.m_freelookRotationY = y;
      }
    }

    public void pointerReleasedFreelook(int x, int y, int pointerNum)
    {
      if (this.m_freelookHideFlag)
      {
        this.m_freelookHideFlag = false;
      }
      else
      {
        if (this.m_freelookXYPosId == pointerNum)
          this.m_freelookXYPosId = -1;
        else if (this.m_freelookZPosId == pointerNum)
          this.m_freelookZPosId = -1;
        else if (this.m_freelookRotationId == pointerNum)
        {
          this.m_freelookRotationId = -1;
          this.m_freelookRotation.normalise();
        }
        if (this.m_engine.getWidth() - 45 >= x || y >= 90)
          return;
        this.m_freelookHideFlag = true;
      }
    }

    public SceneGame.GameState getState() => this.m_state;

    public void stateTransition(SceneGame.GameState newState)
    {
      if (this.m_state == newState || this.m_state == SceneGame.GameState.STATE_GAME_FADE_OUT)
        return;
      if (newState == SceneGame.GameState.STATE_PLAYER_DEATH_FALLING 
                || newState == SceneGame.GameState.STATE_PLAYER_DEATH_FALLING_NO_DEATH_SOUND 
                || newState == SceneGame.GameState.STATE_PLAYER_DIED)
        SpywareManager.getInstance().trackFaithKilled();
      if (this.m_preMenuState == SceneGame.GameState.STATE_INVALID 
                && newState == SceneGame.GameState.STATE_MENU_PAUSED 
                && (MirrorsEdge.TrialMode 
                || this.m_state != SceneGame.GameState.STATE_RESTART_CONFIRM 
                && this.m_state != SceneGame.GameState.STATE_QUIT_CONFIRM))
        this.m_preMenuState = this.m_state;
      this.m_prevState = this.m_state;
      this.deinitState();
      this.m_state = newState;
      this.initState();
      this.m_stateTime = 0;
    }

    public void stateTransitionFade(SceneGame.GameState newState)
    {
      if (this.m_state == SceneGame.GameState.STATE_GAME_FADE_OUT)
        return;
      this.m_nextState = newState;
      this.stateTransition(SceneGame.GameState.STATE_GAME_FADE_OUT);
    }

    public async void Run()
    {
      while (this.m_loadingThreadState != 2)
      {
        if (this.m_loadingThreadState != 0)
        {
            //Thread.Sleep(1000);
            await Task.Delay(1000);
        }
        else
            this.updateLoadingState(100);
      }
    }

    public async void updateLoading(int timeStep)
    {
      this.m_engine.updateLoading(timeStep);
      if (this.m_loadingProgress == 100)
        return;
      if (this.m_loadingThread == null)
      {
        if (this.m_engine.isFading())
            return;
        this.m_loadingThread = new Thread(new ParameterizedThreadStart(ThreadImplSceneGame.Start));
        //ThreadImplSceneGame.Start((object) this);
        this.m_loadingThreadState = 0;
        this.m_loadingThread.Start((object)this);
      }
      else
      {
        //Thread.Sleep(40);
        await Task.Delay(40);
      }
      //TEST
      //this.m_loadingProgress = 100;
    }

    public void updateLoadingState(int timeStep)
    {
      switch (this.m_loadingState)
      {
        case 0:
          ++this.m_loadingState;
          break;
        case 1:
          QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
          quadManager.loadQuads((int) QuadManager.get("GROUP_SCENEGAME"));
          quadManager.setAnimFrame((int) QuadManager.get("ANIM_GAME_PAIN_EFFECT"), 1);
          quadManager.setAnimFrame((int) QuadManager.get("ANIM_SPEEDRUN_STARS_HUD"), 3);
          ++this.m_loadingState;
          break;
        case 2:
          AppEngine.getAnimationBlenderData().loadData();
          if (this.m_world == null)
          {
            this.m_world = new World();
            this.m_m3gCamera = new Camera();
            this.m_world.addChild((Node) this.m_m3gCamera);
            this.m_world.setActiveCamera(this.m_m3gCamera);
            this.m_gameCamera = new GameCamera();
            this.m_gameCamera.setM3GCamera(this.m_m3gCamera);
          }
          this.m_gameCamera.loadM3GAnims();
          AppEngine.getM3GAssets().loadModel((int) M3GAssets.get("MODEL_EFFECT_BULLET"), 4);
          AppEngine.getM3GAssets().loadTexturedAppearance((int) M3GAssets.get("TEX_EFFECT_PARTICLES_ALPHA"), 4);
          ++this.m_loadingState;
          break;
        case 3:
          AppEngine.getGameObjectData().loadData();
          AppEngine.getGameObjectRunnerData().loadData();
          ++this.m_loadingState;
          break;
        case 4:
          LevelData levelData = AppEngine.getLevelData();
          int currentLevelIndex = levelData.getCurrentLevelIndex();
          this.m_map.loadMap(levelData.getLevel(currentLevelIndex).getMapResId(), this.m_world);
          Background background = new Background();
          background.setColorClearEnable(true);
          background.setColor(this.m_map.getModelSet().getBackgroundFillColor());
          background.setDepthClearEnable(true);
          this.m_world.setBackground(background);
          ++this.m_loadingState;
          break;
        case 5:
          this.loadSounds();
          ++this.m_loadingState;
          break;
        case 6:
          AppEngine.getM3GAssets().freeCaches(8);
          ++this.m_loadingState;
          break;
        case 13:
          this.m_loadingProgress = 100;
          this.m_loadingThreadState = 2;
          this.m_loadingThread = (Thread) null;
          break;
      }
    }

    private void loadSounds()
    {
      SoundManager soundManager = this.m_engine.getSoundManager();
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_DEATH_01"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_PIGEON_WINGS"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_CHECKPOINT"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_IN"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_LOOP"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_OUT"));
      if (MirrorsEdge.TrialMode)
        return;
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_IN"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_LOOP"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_OUT"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_1"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_2"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_3"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_4"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_5"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_1"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_2"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_3"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_4"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_1"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_2"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_3"));
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_4"));
    }

    private void unloadSounds()
    {
      SoundManager soundManager = this.m_engine.getSoundManager();
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_DEATH_01"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_PIGEON_WINGS"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_CHECKPOINT"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_IN"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_LOOP"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_OUT"));
      if (MirrorsEdge.TrialMode)
        return;
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_IN"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_LOOP"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_OUT"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_1"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_2"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_3"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_4"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_5"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_1"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_2"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_3"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_4"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_1"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_2"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_3"));
      soundManager.unloadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_4"));
    }

    private void extractUploadQueue(Node node) => this.extractUploadQueueScoped(node, 1);

    private void extractUploadQueueScoped(Node node, int scope)
    {
    }

    private void processUploadQueue()
    {
    }

    private bool uploadQueueExhausted() => true;

    public void bumpBloomBlurryness()
    {
      if (!MirrorsEdge.TrialMode)
        this.m_bloomMixFilter.setSteadyState(this.m_map.getTheme() == 1 ? 0.75f : 0.5f);
      else
        this.m_bloomMixFilter.setSteadyState(0.5f);
      this.m_bloomMixFilter.setTargetValue(1f);
      this.m_bloomBlendFilter.setSteadyState(0.9f);
      this.m_bloomBlendFilter.setTargetValue(0.0f);
    }

    private void initState()
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_INTRO:
          GC.Collect();
          this.m_gameCamera.setClipping(1f, 250f);
          break;
        case SceneGame.GameState.STATE_GAME:
          this.initGame();
          break;
        case SceneGame.GameState.STATE_GAME_FADE_OUT:
          this.m_engine.startFadeOut(true, 16777215);
          break;
        case SceneGame.GameState.STATE_PLAYER_DEATH_FALLING:
          this.m_engine.getSoundManager().playEvent((int) 
              ResourceManager.get("SOUNDEVENT_SFX_DEATH_01"), 1f);
          this.m_engine.startFadeOut(true, 0);
          break;
        case SceneGame.GameState.STATE_PLAYER_DEATH_FALLING_NO_DEATH_SOUND:
          this.m_engine.startFadeOut(true, 0);
          break;
        case SceneGame.GameState.STATE_PLAYER_DIED:
          this.m_engine.startFadeOut(true, 0);
          break;
        case SceneGame.GameState.STATE_LEVEL_COMPLETE:
          this.initLevelComplete();
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          this.initMenuPaused();
          break;
        case SceneGame.GameState.STATE_GAME_OPTIONS:
          this.initGameOptions();
          break;
        case SceneGame.GameState.STATE_SOUND_OPTIONS:
          this.initSoundOptions();
          break;
        case SceneGame.GameState.STATE_HELP:
          this.initHelp();
          break;
        case SceneGame.GameState.STATE_ABOUT:
          this.initAbout();
          break;
        case SceneGame.GameState.STATE_RESTART_CONFIRM:
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.RESTART_CONFIRMATION);
          break;
        case SceneGame.GameState.STATE_QUIT_CONFIRM:
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.RETURN_TO_MENU_CONFIRMATION);
          break;
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          if (MirrorsEdge.TrialMode)
            break;
          this.initFinishedGameStory();
          break;
        case SceneGame.GameState.STATE_UPLOAD_SCORE_CONFIRM:
          if (MirrorsEdge.TrialMode)
            break;
          if (this.m_engine.getUploadScores() && this.m_recordWasBroken && MirrorsEdge.GS_Supported)
          {
            this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.UPLOAD_SCORE_CONFIRM);
            Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
            this.m_engine.getQuadManager().loadQuads(currentLevelObject.getCompletionBackground());
            break;
          }
          this.m_engine.startFadeOut(true, 16777215);
          break;
        case SceneGame.GameState.STATE_MAYHEM_DISPLAYNAME:
        case SceneGame.GameState.STATE_MAYHEM_ADD_DISPLAYNAME:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.DISPLAY_NAME_ENTRY);
          break;
        case SceneGame.GameState.STATE_MAYHEM_WAIT:
          if (MirrorsEdge.TrialMode)
            break;
          this.initMayhemWait();
          break;
        case SceneGame.GameState.STATE_FADE_EXIT:
          this.m_engine.startFadeOut(true, 16777215);
          break;
        case SceneGame.GameState.STATE_SELECT_CHALLENGEE:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.CHALLENGEE_SELECT);
          break;
        case SceneGame.GameState.STATE_CHALLENGE_COMMENT_ENTRY:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.CHALLENGE_COMMENT_ENTRY);
          break;
      }
    }

    public override void update(int timeStepMillis)
    {
      if (0 < this.m_bagCountDisplayTimer)
      {
        this.m_bagCountDisplayTimer -= timeStepMillis;
        if (this.m_bagCountDisplayTimer <= 0)
          this.m_engine.getQuadManager().setMeshVisible((int) QuadManager.get("MESH_HUD_BAG_ICON_GAME"), 
              false);
      }
      this.m_sfxVolumeFilter.setTargetValue(0.0f);
      this.m_stateTime += timeStepMillis;
      if (this.m_goTime > 0)
        this.m_goTime -= timeStepMillis;
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_LOADING:
          if (this.m_engine.isFadedOut() && this.m_loadingProgress == 100)
          {
            this.resetLevel(SceneGame.GameState.STATE_INTRO);
            this.m_state = this.m_postLoadingState;
            this.updateCameraFrustum();
            this.m_engine.deinitLoadingScreen();
            break;
          }
          this.updateLoading(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_INTRO:
          this.updateGame(timeStepMillis);
          if (!this.m_gameCamera.isAnimating())
          {
            this.stateTransition(SceneGame.GameState.STATE_GAME);
            this.m_goTime = 2000;
            break;
          }
          break;
        case SceneGame.GameState.STATE_GAME:
          this.m_raceTimeMillis += (int) ((double) timeStepMillis * (double) this.getGameTimeFactor());
          this.updateGame(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_GAME_FADE_OUT:
          this.updateGame(timeStepMillis);
          if (this.m_engine.isFadedOut())
          {
            this.m_state = SceneGame.GameState.STATE_INVALID;
            this.stateTransition(this.m_nextState);
            break;
          }
          break;
        case SceneGame.GameState.STATE_PLAYER_DEATH_FALLING:
        case SceneGame.GameState.STATE_PLAYER_DEATH_FALLING_NO_DEATH_SOUND:
          if (this.m_engine.isFadedOut())
          {
            this.resetCheckpoint(SceneGame.GameState.STATE_GAME);
            break;
          }
          this.updateGame(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_PLAYER_DIED:
          if (this.m_engine.isFadedOut())
          {
            this.resetCheckpoint(SceneGame.GameState.STATE_GAME);
            break;
          }
          this.updateGame(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          this.updateMenuPaused(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_GAME_OPTIONS:
          this.updateGameOptions(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_SOUND_OPTIONS:
          this.updateSoundOptions(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_POPUP_BOX:
          this.updatePopupBox(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_CUTSCENE:
          this.updateCamera(timeStepMillis);
          if (!this.m_gameCamera.isAnimating())
          {
            this.stateTransition(SceneGame.GameState.STATE_GAME);
            break;
          }
          break;
        case SceneGame.GameState.STATE_HELP:
          this.updateHelp(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_ABOUT:
          this.updateAbout(timeStepMillis);
          break;
        case SceneGame.GameState.STATE_RESTART_CONFIRM:
          if (this.m_engine.isFadedOut())
          {
            this.resetLevel(SceneGame.GameState.STATE_INTRO);
            break;
          }
          switch (this.m_engine.getWindowStore().getWindowResult())
          {
            case WindowResult.WINDOW_RESULT_POSITIVE:
              this.m_engine.getWindowStore().clearWindowResult();
              SpywareManager.getInstance().trackRestartLevel(
                  AppEngine.getLevelData().getCurrentLevelIndex());
              this.m_engine.startFadeOut(true, 16777215);
              break;
            case WindowResult.WINDOW_RESULT_NEGATIVE:
              this.m_engine.getWindowStore().clearWindowResult();
              this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
              break;
          }
          break;
        case SceneGame.GameState.STATE_QUIT_CONFIRM:
          if (this.m_engine.isFadedOut())
          {
            this.m_gameRunning = false;
            if (!MirrorsEdge.TrialMode)
              this.m_achievementData.registerLevelCancel();
            this.m_engine.changeScene(2, 3);
          }
          switch (this.m_engine.getWindowStore().getWindowResult())
          {
            case WindowResult.WINDOW_RESULT_POSITIVE:
              this.m_engine.getWindowStore().clearWindowResult();
              SpywareManager.getInstance().trackQuitLevel(
                  AppEngine.getLevelData().getCurrentLevelIndex());
              this.m_engine.startFadeOut(true, 16777215);
              break;
            case WindowResult.WINDOW_RESULT_NEGATIVE:
              this.m_engine.getWindowStore().clearWindowResult();
              this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
              break;
          }
          break;
        case SceneGame.GameState.STATE_FADE_TO_RESTART:
          if (this.m_engine.isFadedOut())
          {
            this.resetLevel(SceneGame.GameState.STATE_INTRO);
            break;
          }
          break;
        case SceneGame.GameState.STATE_TRANS_TO_NEXT:
          if (AppEngine.getLevelData().isCurrentLevelLast())
          {
            if (MirrorsEdge.TrialMode)
            {
              this.m_gameRunning = false;
              this.m_engine.changeScene(2, 39);
              break;
            }
            this.stateTransition(SceneGame.GameState.STATE_FINISHED_GAME_STORY);
            this.m_engine.startFadeIn(true);
            break;
          }
          AppEngine.getLevelData().nextLevel();
          this.m_gameRunning = false;
          this.m_engine.changeScene(3);
          break;
        case SceneGame.GameState.STATE_FADE_TO_MENU:
          if (this.m_engine.isFadedOut())
          {
            this.m_gameRunning = false;
            this.m_engine.changeScene(2, 22);
            break;
          }
          break;
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          if (!MirrorsEdge.TrialMode)
          {
            this.updateFinishedGameStory(timeStepMillis);
            break;
          }
          break;
        case SceneGame.GameState.STATE_UPLOAD_SCORE_CONFIRM:
          if (!MirrorsEdge.TrialMode)
          {
            if (this.m_engine.isFadedOut())
            {
              this.stateTransition(this.m_exitState);
              break;
            }
            WindowResult windowResult = this.m_engine.getWindowStore().getWindowResult();
            if (windowResult != WindowResult.WINDOW_RESULT_NONE)
            {
              this.m_engine.getWindowStore().clearWindowResult();
              if (windowResult == WindowResult.WINDOW_RESULT_NEGATIVE)
              {
                this.m_engine.getWindowStore().clearWindowResult();
                this.m_engine.startFadeOut(true, 16777215);
                break;
              }
              this.stateTransition(SceneGame.GameState.STATE_MAYHEM_WAIT);
              InputStream resourceAsStream = 
                                (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream(
                                    SceneGame.LAST_GHOST_ANIMATION_FILENAME 
                                    + (MirrorsEdge.TrialMode ? "_trial" : "") + "_7zip");
              if (resourceAsStream == null)
              {
                this.m_engine.getWindowStore().clearWindowResult();
                this.m_engine.startFadeOut(true, 16777215);
                return;
              }
              int length = resourceAsStream.size();
              if (this.localBuffer.Length < length)
                this.localBuffer = new byte[length];
              resourceAsStream.read(ref this.localBuffer, 0, length);
              resourceAsStream.close();
              int raceTimeMillis = this.m_raceTimeMillis;
              if (!LiveProcessor.NewLeaderboardRecord(AppEngine.getLevelData().getCurrentLevelIndex(), 
                  raceTimeMillis, this.localBuffer, length))
                this.m_pleaseWaitWindow.Succeeded(2357);
              else
                this.m_pleaseWaitWindow.Succeeded(2356);
              this.m_mayhemWaitPhase = SceneGame.MayhemWaitPhase.PHASE_FADE;
              this.m_engine.getWindowStore().getNetworkWaitEffect().stop();
              break;
            }
            break;
          }
          break;
        case SceneGame.GameState.STATE_MAYHEM_WAIT:
          if (!MirrorsEdge.TrialMode)
          {
            this.updateMayhemWait(timeStepMillis);
            break;
          }
          break;
        case SceneGame.GameState.STATE_FADE_EXIT:
          if (this.m_engine.isFadedOut())
          {
            this.stateTransition(this.m_exitState);
            break;
          }
          break;
        case SceneGame.GameState.STATE_SELECT_CHALLENGEE:
          if (!MirrorsEdge.TrialMode)
          {
            switch (this.m_engine.getWindowStore().getWindowResult())
            {
              case WindowResult.WINDOW_RESULT_POSITIVE:
                this.m_engine.getWindowStore().clearWindowResult();
                this.stateTransition(SceneGame.GameState.STATE_CHALLENGE_COMMENT_ENTRY);
                this.deinitState();
                break;
              case WindowResult.WINDOW_RESULT_NEGATIVE:
                this.m_engine.getWindowStore().clearWindowResult();
                this.stateTransition(SceneGame.GameState.STATE_LEVEL_COMPLETE);
                this.deinitState();
                break;
            }
          }
          else
            break;
          break;
      }
      this.updateTopMessage(timeStepMillis);
      this.m_sfxVolumeFilter.update(timeStepMillis);
      this.m_engine.getSoundManager().setVolumeGroup(2, this.m_sfxVolumeFilter.getFilteredValue());
      this.m_engine.getSoundManager().setVolumeGroup(5, this.m_sfxVolumeFilter.getFilteredValue());
      if (this.m_playerHurtSoundHandle == -1 
                || this.m_engine.getSoundManager().isHandlePlaying(this.m_playerHurtSoundHandle))
        return;
      this.m_playerHurtSoundHandle = -1;
    }

    public override void render(midp.Graphics g)
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_LOADING:
          this.renderLoading(g);
          break;
        case SceneGame.GameState.STATE_INTRO:
        case SceneGame.GameState.STATE_GAME:
        case SceneGame.GameState.STATE_GAME_FADE_OUT:
        case SceneGame.GameState.STATE_PLAYER_DEATH_FALLING:
        case SceneGame.GameState.STATE_PLAYER_DEATH_FALLING_NO_DEATH_SOUND:
        case SceneGame.GameState.STATE_PLAYER_DIED:
        case SceneGame.GameState.STATE_CUTSCENE:
          this.renderGame(g);
          this.renderTopMessage(g);
          break;
        case SceneGame.GameState.STATE_LEVEL_COMPLETE:
        case SceneGame.GameState.STATE_UPLOAD_SCORE_CONFIRM:
        case SceneGame.GameState.STATE_MAYHEM_DISPLAYNAME:
        case SceneGame.GameState.STATE_MAYHEM_ADD_DISPLAYNAME:
          if (MirrorsEdge.TrialMode && this.m_state != SceneGame.GameState.STATE_LEVEL_COMPLETE)
            break;
          this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_LEVEL_COMPLETE_BACKGROUNDS"), true);
          this.m_engine.getQuadManager().render(g, 2);
          this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_LEVEL_COMPLETE_BACKGROUNDS"), false);
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          this.renderGame(g);
          this.renderMenuPaused(g);
          break;
        case SceneGame.GameState.STATE_GAME_OPTIONS:
          this.renderGameOptions(g);
          break;
        case SceneGame.GameState.STATE_SOUND_OPTIONS:
          this.renderSoundOptions(g);
          break;
        case SceneGame.GameState.STATE_POPUP_BOX:
          this.renderGame(g);
          this.renderPopupBox(g);
          break;
        case SceneGame.GameState.STATE_HELP:
          this.renderHelp(g);
          break;
        case SceneGame.GameState.STATE_ABOUT:
          this.renderAbout(g);
          break;
        case SceneGame.GameState.STATE_RESTART_CONFIRM:
        case SceneGame.GameState.STATE_QUIT_CONFIRM:
          this.renderGame(g);
          this.m_engine.renderBgFade(g);
          break;
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          if (MirrorsEdge.TrialMode)
            break;
          this.renderFinishedGameStory(g);
          break;
        case SceneGame.GameState.STATE_MAYHEM_WAIT:
        case SceneGame.GameState.STATE_SELECT_CHALLENGEE:
        case SceneGame.GameState.STATE_CHALLENGE_COMMENT_ENTRY:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_LEVEL_COMPLETE_BACKGROUNDS"), true);
          this.m_engine.getQuadManager().render(g, 2);
          this.m_engine.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_LEVEL_COMPLETE_BACKGROUNDS"), false);
          break;
      }
    }

    private void deinitState()
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_LEVEL_COMPLETE:
          if (MirrorsEdge.TrialMode || this.m_firstComplete)
            break;
          int num = this.m_recordWasBroken ? 1 : 0;
          break;
        case SceneGame.GameState.STATE_POPUP_BOX:
          this.deinitPopupBox();
          break;
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          if (MirrorsEdge.TrialMode)
            break;
          this.deinitFinishedGameStory();
          break;
      }
    }

    private void renderLoading(midp.Graphics g)
    {
      if (this.m_loadingState == 7)
      {
        QuadManager quadManager = this.m_engine.getQuadManager();
        this.extractUploadQueue((Node) this.m_world);
        this.extractUploadQueueScoped(quadManager.getWorld(), 16);
        ++this.m_loadingState;
      }
      else if (this.m_loadingState == 8)
      {
        Transform transform = new Transform();
        ++this.m_loadingState;
      }
      else if (this.m_loadingState == 9)
        ++this.m_loadingState;
      else if (this.m_loadingState == 10)
        ++this.m_loadingState;
      else if (this.m_loadingState == 11)
      {
        QuadManager quadManager = this.m_engine.getQuadManager();
        Graphics3D graphics3D = this.m_engine.getGraphics3D();
        graphics3D.bindTarget((object) g);
        quadManager.render(graphics3D, 16);
        graphics3D.releaseTarget();
        ++this.m_loadingState;
      }
      else if (this.m_loadingState == 12)
      {
        this.processUploadQueue();
        if (this.uploadQueueExhausted())
          ++this.m_loadingState;
      }
      this.m_engine.renderLoading(g);
      TextManager textManager = this.m_engine.getTextManager();
      StringRenderer stringRenderer = textManager.getStringRenderer(6);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(8421504);
      if (this.m_loadingProgress == 100)
      {
        textManager.drawString(g, 2067, 6, (this.m_engine.getWidth() >> 1) + 1, this.m_engine.getHeight() - textManager.getLineHeight(6) - 6 + 1, 10);
        stringRenderer.setColor(color);
        textManager.drawString(g, 2067, 6, this.m_engine.getWidth() >> 1, this.m_engine.getHeight() - textManager.getLineHeight(6) - 6, 10);
      }
      else
      {
        textManager.drawString(g, 2066, 6, (this.m_engine.getWidth() >> 1) + 1, this.m_engine.getHeight() - textManager.getLineHeight(6) - 6 + 1, 10);
        stringRenderer.setColor(color);
        textManager.drawString(g, 2066, 6, this.m_engine.getWidth() >> 1, this.m_engine.getHeight() - textManager.getLineHeight(6) - 6, 10);
      }
    }

    private void renderLoadingFirstPass(midp.Graphics g)
    {
      if (this.m_world == null)
        return;
      Graphics3D graphics3D = this.m_engine.getGraphics3D();
      graphics3D.bindTarget((object) g);
      graphics3D.render(this.m_world);
      graphics3D.releaseTarget();
    }

    private void initGame()
    {
      this.m_gameRunning = true;
      this.m_gameCamera.setClipping(3.5f, 250f);
      if (MirrorsEdge.TrialMode)
        return;
      this.m_achievementData.registerLevelStart(AppEngine.getLevelData().getCurrentLevelIndex());
    }

    private void updateGame(int timeStepMillis)
    {
      this.m_sfxVolumeFilter.setTargetValue(this.m_engine.getSoundManager().getVolumeSFX());
      QuadManager quadManager = this.m_engine.getQuadManager();
      this.m_gameTimeFactorFilter.update(timeStepMillis);
      int num1 = timeStepMillis;
      int num2 = (int) ((double) timeStepMillis * (double) this.getGameTimeFactor());
      this.updateSound(num2);
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.update(num2);
      quadManager.updateAnim((int) QuadManager.get("ANIM_GAME_PAIN_EFFECT"), num1);
      quadManager.updateAnim((int) QuadManager.get("ANIM_SPEEDRUN_STARS_HUD"), num1);
      this.updateAccelerometer(num1);
      this.m_bloomMixFilter.update(num1);
      this.m_bloomBlendFilter.update(num1);
      if (this.m_gestures != 0)
        this.updateGameGestures();
      this.updateCamera(num1);
      this.m_map.updateObjects(num2);
      this.m_map.updateObjectCollisions();
      this.m_map.updateChunkVisibility(num2, this.m_gameCamera);
      if (this.m_state == SceneGame.GameState.STATE_LEVEL_COMPLETE)
        return;
      if (this.m_firstPointer.checkHold(num1))
        this.m_gestures |= 32;
      if (this.m_combatTimeDuration <= 0)
        return;
      this.m_combatTimeDuration -= timeStepMillis;
      if (this.m_combatTimeDuration <= 0)
        this.setGameTimeFactor(1f);
      else
        this.setGameTimeFactor(0.3f);
    }

    private void updateSound(int timeStepMillis)
    {
      this.m_soundSequencer.update(timeStepMillis);
      this.m_engine.getSoundManager().setGroupPitch(5, this.getGameTimeFactor());
    }

    private void updateAccelerometer(int timeStepMillis)
    {
      WP7_Accelerometer accelerometerWp7 = WP7_Accelerometer.getAccelerometerWP7();
      float rawX = 0.0f;
      float rawY = 0.0f;
      float rawZ = 0.0f;
      accelerometerWp7.getRawXYZ(ref rawX, ref rawY, ref rawZ);
      this.m_accTiltFilter.setTargetValue(-(float) Math.Atan2((double) rawY, Math.Sqrt((double) rawX * (double) rawX + (double) rawZ * (double) rawZ)) / 1.570796f);
      float filteredValue = this.m_accTiltFilter.getFilteredValue();
      this.m_accTiltFilter.update(timeStepMillis);
      if (timeStepMillis <= 0)
        return;
      this.m_accTiltVelocity = (float) (1000.0 * ((double) this.m_accTiltFilter.getFilteredValue() - (double) filteredValue)) / (float) timeStepMillis;
    }

    private void updateGameGestures()
    {
      if (this.m_state == SceneGame.GameState.STATE_PLAYER_DEATH_FALLING)
        return;
      if ((this.m_gestures & 65) == 65)
        this.m_map.getPlayerObject().addGesture(2);
      if ((this.m_gestures & 1) != 0 && !this.m_secondPointer.isPressed())
        this.m_map.getPlayerObject().addGesture(1);
      if ((this.m_gestures & 130) == 130)
        this.m_map.getPlayerObject().addGesture(8);
      if ((this.m_gestures & 2) != 0 && !this.m_secondPointer.isPressed())
        this.m_map.getPlayerObject().addGesture(4);
      if ((this.m_gestures & 4) != 0)
        this.m_map.getPlayerObject().addGesture(128);
      if ((this.m_gestures & 8) != 0 && !this.m_secondPointer.isPressed())
        this.m_map.getPlayerObject().addGesture(32);
      if ((this.m_gestures & 16) != 0)
      {
        GameObjectPlayer playerObject = this.m_map.getPlayerObject();
        playerObject.addGesture(playerObject.getTapGesture());
      }
      if ((this.m_gestures & 32) != 0)
        this.m_map.getPlayerObject().addGesture(16);
      if ((this.m_gestures & 8) != 0 && (this.m_gestures & 512) != 0)
      {
        SpywareManager.getInstance().trackGamePaused();
        this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
        this.m_gestures = 0;
      }
      if ((this.m_gestures & 8) != 0 && this.m_secondPointer.isPressed() || (this.m_gestures & 512) != 0 && this.m_firstPointer.isPressed())
        return;
      this.m_gestures = 0;
    }

    private void renderGame(midp.Graphics g)
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      Node skyDomeNode = this.m_map.getSkyDomeNode();
      if (skyDomeNode != null)
      {
        MathVector origin = this.m_gameCameraFrustum.middleLine.origin;
        skyDomeNode.setTranslation(origin.x, origin.y, origin.z);
      }
      quadManager.setMeshVisible((int) QuadManager.get("MESH_GAME_HEALTH_EFFECT"), false);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_GAME_PAIN_EFFECT"), false);
      if (this.m_world != null)
      {
        Graphics3D graphics3D = this.m_engine.getGraphics3D();
        graphics3D.bindTarget((object) g);
        graphics3D.setViewport(0, 0, this.m_engine.getWidth(), this.m_engine.getHeight());
        graphics3D.render(this.m_world);
        quadManager.render(graphics3D, 17);
        graphics3D.releaseTarget();
        this.renderHealthPain();
      }
      if (this.m_freelookEnabled)
        this.renderGameFreelook(g);
      else
        this.renderGameHUD(g);
    }

    private void renderHealthPain()
    {
      if (SceneGame.painTexture == null)
      {
        SceneGame.painTexture = MirrorsEdge.content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("res/texture_pain_effect");
        SceneGame.painTexture.GetData<byte>(SceneGame.painTextureData0);
        for (int index = 0; index < 65536; index += 4)
          SceneGame.painTextureData[index + 1] = SceneGame.painTextureData[index + 2] = SceneGame.painTextureData[index + 3] = (byte) 0;
        SceneGame.blendStateAdd = new BlendState();
        SceneGame.blendStateAdd.AlphaSourceBlend = Blend.One;
        SceneGame.blendStateAdd.AlphaDestinationBlend = Blend.One;
        SceneGame.blendStateAdd.AlphaBlendFunction = BlendFunction.Add;
        SceneGame.blendStateAdd.ColorSourceBlend = Blend.One;
        SceneGame.blendStateAdd.ColorDestinationBlend = Blend.One;
      }
      float num1 = 1f - this.m_map.getPlayerObject().getHealthFraction() + this.m_engine.getQuadManager().getMeshAlpha((int) QuadManager.get("MESH_GAME_PAIN_EFFECT"));
      if ((double) num1 <= 0.0)
        return;
      int num2 = (int) (256.0 * (double) num1);
      for (int index = 0; index < 65536; index += 4)
      {
        int num3 = (int) SceneGame.painTextureData0[index] * num2 >> 8;
        SceneGame.painTextureData[index] = num3 < 256 ? (byte) num3 : byte.MaxValue;
      }
      SceneGame.painTexture.SetData<byte>(SceneGame.painTextureData);
      MirrorsEdge.spriteBatch.Begin(SpriteSortMode.Deferred, SceneGame.blendStateAdd);
      MirrorsEdge.spriteBatch.Draw(SceneGame.painTexture, new Rectangle(0, 0, 800, 480), Color.White);
      MirrorsEdge.spriteBatch.End();
    }

    private void renderGameHUD(midp.Graphics g)
    {
      LevelData levelData = AppEngine.getLevelData();
      QuadManager quadManager = this.m_engine.getQuadManager();
      switch (levelData.getGameMode())
      {
        case LevelData.GameMode.GAME_MODE_STORY:
          if (this.m_bagCountDisplayTimer > 0)
          {
            this.m_engine.drawOfStatString(g, 7, 2048, this.m_map.getNumCollectablesCollected(), this.m_map.getNumCollectablesTotal(), 508, 54, 10, true);
            break;
          }
          break;
        case LevelData.GameMode.GAME_MODE_SPEEDRUN:
          Level currentLevelObject = levelData.getCurrentLevelObject();
          this.m_engine.drawStatString(g, 10, 2048, AppEngine.StatType.STAT_TYPE_TIME_MILLIS, this.m_raceTimeMillis, 393, 6, 9, true);
          if (currentLevelObject.getNumStarsAchieved() == 3)
          {
            this.m_engine.drawStatString(g, 7, 2048, AppEngine.StatType.STAT_TYPE_TIME_MILLIS, currentLevelObject.getBestSpeedRunTimeMillis(), 393, 24, 9, true);
            break;
          }
          int minStarsWithTime = currentLevelObject.getMinStarsWithTime(this.m_raceTimeMillis);
          int requirementMillis = currentLevelObject.getSpeedRunRequirementMillis(Math.Max(1, minStarsWithTime));
          this.m_engine.drawStatString(g, 7, 2048, AppEngine.StatType.STAT_TYPE_TIME_MILLIS, requirementMillis, 393, 24, 9, true);
          quadManager.render(g, 32);
          quadManager.playAnimTo((int) QuadManager.get("ANIM_SPEEDRUN_STARS_HUD"), minStarsWithTime);
          break;
        case LevelData.GameMode.GAME_MODE_CHALLENGE:
          if (!MirrorsEdge.TrialMode)
          {
            this.m_engine.drawStatString(g, 10, 2048, AppEngine.StatType.STAT_TYPE_TIME_MILLIS, this.m_raceTimeMillis, 393, 6, 9, true);
            this.m_engine.drawStatString(g, 7, 2048, AppEngine.StatType.STAT_TYPE_TIME_MILLIS, this.m_engine.getChallengeTime(), 393, 24, 9, true);
            break;
          }
          break;
      }
      if (levelData.getGameMode() != LevelData.GameMode.GAME_MODE_SPEEDRUN && (MirrorsEdge.TrialMode || levelData.getGameMode() != LevelData.GameMode.GAME_MODE_CHALLENGE))
        return;
      TextManager textManager = this.m_engine.getTextManager();
      if (this.m_state == SceneGame.GameState.STATE_INTRO)
      {
        int integer = this.m_gameCamera.getAnimTimeLeft() / 1000 + 1;
        if (integer > 3)
          return;
        StringBuffer stringBuffer = textManager.clearStringBuffer();
        textManager.appendIntToBuffer(stringBuffer, integer);
        string str = stringBuffer.toString();
        StringRenderer stringRenderer = textManager.getStringRenderer(this.COUNTDOWN_FONT);
        int color = stringRenderer.getColor();
        stringRenderer.setColor(0);
        textManager.drawString(g, str, this.COUNTDOWN_FONT, 356, 51, 9);
        stringRenderer.setColor(color);
        textManager.drawString(g, str, this.COUNTDOWN_FONT, 355, 50, 9);
      }
      else
      {
        if (this.m_goTime <= 0)
          return;
        StringRenderer stringRenderer = textManager.getStringRenderer(this.COUNTDOWN_FONT);
        int color = stringRenderer.getColor();
        stringRenderer.setColor(0);
        textManager.drawString(g, 2359, this.COUNTDOWN_FONT, 356, 51, 9);
        stringRenderer.setColor(color);
        textManager.drawString(g, 2359, this.COUNTDOWN_FONT, 355, 50, 9);
      }
    }

    private void pointerDraggedGame(
      int pointerMultiplier,
      ref SceneGamePointer pointer,
      int x,
      int y)
    {
      int num1 = 15;
      int num2 = x - pointer.pressX;
      int num3 = y - pointer.pressY;
      int num4 = Math.Abs(num2) > Math.Abs(num3) ? num2 : 0;
      int num5 = Math.Abs(num3) > Math.Abs(num4) ? num3 : 0;
      if (num4 <= -30)
      {
        if (-num1 <= num5 && num5 <= num1 && pointer.swipeGestureMade != 1 && pointer.swipeGestureMade != 4)
        {
          this.m_gestures |= pointerMultiplier;
          pointer.swipe(1, x, y);
        }
        else
          pointer.swipe(pointer.swipeGestureMade, x, y);
      }
      else if (30 <= num4)
      {
        if (-num1 <= num5 && num5 <= num1 && pointer.swipeGestureMade != 2 && pointer.swipeGestureMade != 4)
        {
          this.m_gestures |= pointerMultiplier * 2;
          pointer.swipe(2, x, y);
        }
        else
          pointer.swipe(pointer.swipeGestureMade, x, y);
      }
      else if (num5 <= -30)
      {
        float num6 = (float) num1 * 2f;
        if (-(double) num6 <= (double) num4 && (double) num4 <= (double) num6 && pointer.swipeGestureMade != 4)
        {
          this.m_gestures |= pointerMultiplier * 4;
          pointer.swipe(4, x, y);
        }
        else
          pointer.swipe(pointer.swipeGestureMade, x, y);
      }
      else
      {
        if (30 > num5)
          return;
        float num7 = (float) num1 * 2f;
        if (-(double) num7 <= (double) num4 && (double) num4 <= (double) num7 && pointer.swipeGestureMade != 8)
        {
          if (pointer.swipeGestureMade != 4)
            this.m_gestures |= pointerMultiplier * 8;
          pointer.swipe(8, x, y);
        }
        else
          pointer.swipe(pointer.swipeGestureMade, x, y);
      }
    }

    private void initMenuPaused()
    {
      QuadManager quadManager = this.m_engine.getQuadManager();
      if (!this.m_menuPaused.isCreated())
      {
        this.m_menuPaused.create();
        this.m_menuPaused.setSelectionIndex(-1);
      }
      this.m_menuPaused.wrapStrings(true);
      this.m_menuPaused.reset();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_MENU_MAIN"), true);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_TITLE"), false);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_HUD_BAG_ICON_MENU"), true);
      if (this.m_prevState != SceneGame.GameState.STATE_GAME)
        return;
      quadManager.playAnim((int) QuadManager.get("ANIM_BG_FADE"), 2);
    }

    private void updateMenuPaused(int timeStep)
    {
      this.m_menuPaused.update(timeStep);
      if (!this.m_menuPaused.isItemSelected())
        return;
      this.menuSelect(this.m_menuPaused.getSelectedSubMenu().getSelectedItem());
      if (this.m_state == SceneGame.GameState.STATE_MEDIAPICKER)
        return;
      QuadManager quadManager = this.m_engine.getQuadManager();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_MENU_MAIN"), false);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_TITLE"), true);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_HUD_BAG_ICON_MENU"), false);
    }

    private void renderMenuPaused(midp.Graphics g)
    {
      this.m_engine.renderBgFade(g);
      this.m_menuPaused.render(g);
      MenuMainSubMenu subMenu = this.m_menuPaused.getSubMenu(3);
      if (subMenu.isActive() || subMenu.isAnimating())
        return;
      this.m_engine.drawOfStatString(g, 7, 2048, this.m_map.getNumCollectablesCollected(), this.m_map.getNumCollectablesTotal(), 508, 54, 10, true);
    }

    private void pointerPressedMenuPaused(int x, int y) => this.m_menuPaused.pointerPressed(x, y);

    private void pointerReleasedMenuPaused(int x, int y) => this.m_menuPaused.pointerReleased(x, y);

    private void pointerDraggedMenuPaused(int x, int y) => this.m_menuPaused.pointerDragged(x, y);

    private void initGameOptions()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.GAME_OPTIONS);
    }

    private void updateGameOptions(int timeStep)
    {
      if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
        return;
      this.m_engine.getWindowStore().clearWindowResult();
      this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
      this.m_engine.saveGameOptions();
    }

    private void renderGameOptions(midp.Graphics g)
    {
      this.renderGame(g);
      this.m_engine.renderBgFade(g);
    }

    private void initSoundOptions()
    {
      this.m_soundOptionsWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.SOUND_OPTIONS) as SoundOptions;
    }

    private void updateSoundOptions(int timeStep)
    {
      if (this.m_engine.getWindowStore().getWindowResult() != WindowResult.WINDOW_RESULT_NONE)
      {
        this.m_engine.getWindowStore().clearWindowResult();
        this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
      }
      if (this.m_soundOptionsWindow != null && !this.m_soundOptionsWindow.isClosed())
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
      else
      {
        if (this.m_soundOptionsWindow != null)
          this.m_engine.saveRMSAppSettings();
        this.m_soundOptionsWindow = (SoundOptions) null;
      }
    }

    private void renderSoundOptions(midp.Graphics g)
    {
      this.renderGame(g);
      this.m_engine.renderBgFade(g);
    }

    private void initLevelComplete()
    {
      LevelData levelData = AppEngine.getLevelData();
      EndOfLevelPrompt endOfLevelPrompt = (EndOfLevelPrompt) null;
      switch (levelData.getGameMode())
      {
        case LevelData.GameMode.GAME_MODE_STORY:
          endOfLevelPrompt = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.STORY_END_OF_LEVEL) as EndOfLevelPrompt;
          break;
        case LevelData.GameMode.GAME_MODE_SPEEDRUN:
          endOfLevelPrompt = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.SPEEDRUN_END_OF_LEVEL) as EndOfLevelPrompt;
          break;
        case LevelData.GameMode.GAME_MODE_CHALLENGE:
          if (!MirrorsEdge.TrialMode)
          {
            endOfLevelPrompt = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.CHALLENGE_END_OF_LEVEL) as EndOfLevelPrompt;
            break;
          }
          break;
      }
      Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
      if (!MirrorsEdge.TrialMode)
        endOfLevelPrompt.setBackgroundQuads(currentLevelObject.getCompletionBackground());
      else
        this.m_engine.getQuadManager().loadQuads(currentLevelObject.getCompletionBackground());
      this.m_engine.startFadeIn(true);
    }

    private void initGameCompleteResults()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.END_OF_GAME_RESULTS);
      this.m_engine.startFadeIn(true);
    }

    private void updateGameCompleteResults(int timeStepMillis)
    {
      if (this.m_engine.getWindowStore().getWindowResult() != WindowResult.WINDOW_RESULT_POSITIVE)
        return;
      this.m_engine.getWindowStore().clearWindowResult();
      this.stateTransition(SceneGame.GameState.STATE_FADE_TO_MENU);
      this.m_engine.startFadeOut(true, 16777215);
    }

    private void updatePopupBox(int timeStepMillis)
    {
      this.m_bloomMixFilter.update(timeStepMillis);
      this.m_bloomBlendFilter.update(timeStepMillis);
      if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
        return;
      this.m_engine.getWindowStore().clearWindowResult();
      this.stateTransition(this.m_prePopupState);
    }

    private void renderPopupBox(midp.Graphics g)
    {
    }

    private void deinitPopupBox()
    {
      this.m_bloomMixFilter.setSteadyState(1f);
      this.m_bloomBlendFilter.setSteadyState(0.0f);
    }

    private void initHelp()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.HELP);
    }

    private void updateHelp(int timeStep)
    {
      if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
        return;
      this.m_engine.getWindowStore().clearWindowResult();
      this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
    }

    private void renderHelp(midp.Graphics g)
    {
      this.renderGame(g);
      this.m_engine.renderBgFade(g);
    }

    private void initAbout()
    {
      this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.ABOUT);
    }

    private void updateAbout(int timeStep)
    {
      if (this.m_engine.getWindowStore().getWindowResult() == WindowResult.WINDOW_RESULT_NONE)
        return;
      this.m_engine.getWindowStore().clearWindowResult();
      this.stateTransition(SceneGame.GameState.STATE_MENU_PAUSED);
    }

    private void renderAbout(midp.Graphics g) => this.renderGame(g);

    private void initFinishedGameStory()
    {
      this.m_engine.initLoadingScreen(14);
      this.m_finishedGameTimer = 3000;
    }

    private void updateFinishedGameStory(int timeStep)
    {
      if (this.m_engine.isFadedOut())
      {
        this.m_gameRunning = false;
        this.m_engine.changeScene(2, 3);
      }
      this.m_finishedGameTimer -= timeStep;
      this.m_engine.updateLoading(timeStep);
    }

    private void renderFinishedGameStory(midp.Graphics g)
    {
      this.m_engine.renderLoading(g);
      if (this.m_finishedGameTimer > 0)
        return;
      TextManager textManager = this.m_engine.getTextManager();
      textManager.drawString(g, 2067, 6, this.m_engine.getWidth() >> 1, this.m_engine.getHeight() - textManager.getLineHeight(6) - 6, 10);
    }

    private void deinitFinishedGameStory() => this.m_engine.deinitLoadingScreen();

    public override void pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_freelookEnabled)
        this.pointerPressedFreelook(x, y, pointerNum);
      else
        this.pointerPressedGame(x, y, pointerNum);
    }

    public override void pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_freelookEnabled)
        this.pointerDraggedFreelook(x, y, pointerNum);
      else
        this.pointerDraggedGame(x, y, pointerNum);
    }

    public override void pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_state == SceneGame.GameState.STATE_LOADING)
      {
        this.m_engine.setScrollingLoad(true);
        if (this.m_loadingProgress != 100 || this.m_engine.isFading() || this.m_engine.isFadedOut() || this.m_draggingLoadScreen || (double) Math.Abs(this.m_prevStartX - (float) x) >= 10.0 || (double) Math.Abs(this.m_prevStartY - (float) y) >= 10.0)
          return;
        this.m_engine.startFadeOut(true, 16777215);
      }
      else
      {
        if (this.m_state == SceneGame.GameState.STATE_FINISHED_GAME_STORY && !MirrorsEdge.TrialMode)
        {
          this.m_engine.setScrollingLoad(true);
          if (this.m_finishedGameTimer <= 0 && !this.m_engine.isFading() && !this.m_engine.isFadedOut() && !this.m_draggingLoadScreen)
            this.m_engine.startFadeOut(true, 16777215);
        }
        if (this.m_freelookEnabled)
          this.pointerReleasedFreelook(x, y, pointerNum);
        else
          this.pointerReleasedGame(x, y, pointerNum);
      }
    }

    public void pointerPressedGame(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_LOADING:
          this.m_draggingLoadScreen = false;
          this.m_prevDragY = (float) y;
          this.m_prevStartX = (float) x;
          this.m_prevStartY = (float) y;
          break;
        case SceneGame.GameState.STATE_GAME:
          if (!this.m_firstPointer.isPressed())
          {
            this.m_firstPointer.press(pointerNum, x, y);
            this.m_secondPointer.release();
            break;
          }
          if (this.m_secondPointer.isPressed())
            break;
          this.m_secondPointer.press(pointerNum, x, y);
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          this.pointerPressedMenuPaused(x, y);
          break;
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          if (MirrorsEdge.TrialMode)
            break;
          this.m_draggingLoadScreen = false;
          this.m_prevDragY = (float) y;
          this.m_prevStartX = (float) x;
          this.m_prevStartY = (float) y;
          break;
      }
    }

    public void pointerDraggedGame(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_LOADING:
        case SceneGame.GameState.STATE_FINISHED_GAME_STORY:
          if (this.m_state != SceneGame.GameState.STATE_LOADING && MirrorsEdge.TrialMode)
            break;
          if (!this.m_engine.isFading() && (double) Math.Abs(this.m_prevStartY - (float) y) > 10.0)
          {
            this.m_draggingLoadScreen = true;
            float num = (float) (((double) y - (double) this.m_prevDragY) * (1.0 / 500.0));
            float loadingScrollValue = this.m_engine.getLoadingScrollValue();
            this.m_engine.setScrollingLoad(false);
            this.m_engine.setLoadingScrollValue(loadingScrollValue + num);
          }
          this.m_prevDragY = (float) y;
          break;
        case SceneGame.GameState.STATE_GAME:
          if (this.m_firstPointer == pointerNum)
            this.pointerDraggedGame(1, ref this.m_firstPointer, x, y);
          if (!(this.m_secondPointer == pointerNum))
            break;
          this.pointerDraggedGame(64, ref this.m_secondPointer, x, y);
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          this.pointerDraggedMenuPaused(x, y);
          break;
      }
    }

    public void pointerReleasedGame(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case SceneGame.GameState.STATE_GAME:
          if (this.m_firstPointer == pointerNum)
          {
            if (this.m_firstPointer.pressTime < 750 && !this.m_firstPointer.swiped)
              this.m_gestures |= 16;
            this.m_firstPointer.release();
            break;
          }
          if (!(this.m_secondPointer == pointerNum))
            break;
          this.m_secondPointer.release();
          break;
        case SceneGame.GameState.STATE_MENU_PAUSED:
          this.m_firstPointer.release();
          this.m_secondPointer.release();
          this.pointerReleasedMenuPaused(x, y);
          break;
      }
    }

    private void menuSelect(int stringId)
    {
      switch (stringId)
      {
        case 2049:
          this.stateTransition(SceneGame.GameState.STATE_ABOUT);
          break;
        case 2051:
          this.stateTransition(SceneGame.GameState.STATE_HELP);
          break;
        case 2082:
          this.stateTransition(SceneGame.GameState.STATE_GAME_OPTIONS);
          break;
        case 2089:
          SpywareManager.getInstance().trackResumeLevel(AppEngine.getLevelData().getCurrentLevelIndex());
          this.stateTransition(this.m_preMenuState);
          this.m_preMenuState = SceneGame.GameState.STATE_INVALID;
          break;
        case 2090:
          this.stateTransition(SceneGame.GameState.STATE_RESTART_CONFIRM);
          break;
        case 2091:
          this.stateTransition(SceneGame.GameState.STATE_QUIT_CONFIRM);
          break;
        case 2263:
          this.stateTransition(SceneGame.GameState.STATE_SOUND_OPTIONS);
          break;
      }
      this.m_engine.getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_UI_POSITIVE"));
    }

    private void resetCheckpoint(SceneGame.GameState startState)
    {
      this.m_map.resetCheckpoint();
      this.m_gameCamera.startSmoothTracking((GameObject) this.m_map.getPlayerObject(), true);
      this.stateTransition(startState);
      this.m_engine.startFadeIn(true);
      if (this.m_ghostObject != null)
        this.m_map.addObject((GameObject) this.m_ghostObject);
      this.clearGameTimeFactor();
      this.m_combatTimeDuration = 0;
    }

    private void resetLevel(SceneGame.GameState startState)
    {
      LevelData levelData = AppEngine.getLevelData();
      Level currentLevelObject = levelData.getCurrentLevelObject();
      this.m_sfxVolumeFilter.setSteadyState(this.m_engine.getSoundManager().getVolumeSFX());
      this.m_engine.getBGMusic().playMusic(currentLevelObject.getStartMusicId(), 2);
      this.m_map.resetLevel();
      this.m_raceTimeMillis = 0;
      this.m_gameCamera.startSmoothTracking((GameObject) this.m_map.getPlayerObject(), true);
      this.stateTransition(startState);
      this.m_engine.startFadeIn(true);
      this.startIntroCam();
      if (levelData.getGameMode() == LevelData.GameMode.GAME_MODE_STORY)
        this.startTopMessage(currentLevelObject.getObjective(), currentLevelObject.getDateTime());
      else if (levelData.getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN && currentLevelObject.isLevelComplete())
      {
        if (this.m_ghostObject == null)
        {
          GhostAnimationPlayback forCurrentLevel = GhostAnimationPlayback.createForCurrentLevel();
          if (forCurrentLevel != null)
            this.m_ghostObject = new GameObjectGhost(this.m_map, forCurrentLevel);
        }
        else
          this.m_ghostObject.resetLevel();
        if (this.m_ghostObject != null)
          this.m_map.addObject((GameObject) this.m_ghostObject);
      }
      else if (levelData.getGameMode() == LevelData.GameMode.GAME_MODE_CHALLENGE && !MirrorsEdge.TrialMode)
      {
        if (this.m_ghostObject == null)
        {
          GhostAnimationPlayback fromChallenge = GhostAnimationPlayback.createFromChallenge();
          if (fromChallenge != null)
            this.m_ghostObject = new GameObjectGhost(this.m_map, fromChallenge);
        }
        else
          this.m_ghostObject.resetLevel();
        if (this.m_ghostObject != null)
          this.m_map.addObject((GameObject) this.m_ghostObject);
      }
      this.clearGameTimeFactor();
      this.m_combatTimeDuration = 0;
      this.updateGame(0);
      this.m_preMenuState = SceneGame.GameState.STATE_INVALID;
    }

    public void completeLevel()
    {
      this.m_map.updateCollectableList();
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerLevelComplete();
      SpywareManager.getInstance().trackLevelFinished(AppEngine.getLevelData().getCurrentLevelIndex());
      bool flag1 = false;
      if (!MirrorsEdge.TrialMode)
      {
        Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
        if (!currentLevelObject.isLevelComplete())
          this.m_firstComplete = true;
        this.m_lastSpeedRunTime = this.m_raceTimeMillis;
        this.m_lastSpeedRunRecord = currentLevelObject.getBestSpeedRunTimeMillis();
        flag1 = this.m_lastSpeedRunTime < currentLevelObject.getSpeedRunRequirementMillis(3) / 2;
      }
      bool flag2 = AppEngine.getLevelData().setLevelComplete(this.m_raceTimeMillis, this.m_map.getCollectableList());
      GhostAnimationRecorder ghostAnimation = this.m_map.getPlayerObject().getGhostAnimation();
      if (!MirrorsEdge.TrialMode && ghostAnimation != null)
      {
        ghostAnimation.write(SceneGame.LAST_GHOST_ANIMATION_FILENAME);
        if (ghostAnimation.getPackedDataLength() >= 10240)
          flag2 = false;
      }
      if (flag2)
      {
        if (!MirrorsEdge.TrialMode)
          this.m_recordWasBroken = !flag1;
        this.m_ghostObject = (GameObjectGhost) null;
        ghostAnimation?.write();
        this.stateTransitionFade(SceneGame.GameState.STATE_LEVEL_COMPLETE);
      }
      else
        this.stateTransitionFade(SceneGame.GameState.STATE_LEVEL_COMPLETE);
    }

    public void checkpointReached()
    {
      this.m_engine.getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_CHECKPOINT"));
    }

    public void notifyBagCollected()
    {
      this.m_bagCountDisplayTimer = 4000;
      this.m_engine.getQuadManager().setMeshVisible((int) QuadManager.get("MESH_HUD_BAG_ICON_GAME"), true);
    }

    public void playerHurt(bool playSFX)
    {
      this.bumpBloomBlurryness();
      this.m_engine.getQuadManager().playAnim((int) QuadManager.get("ANIM_GAME_PAIN_EFFECT"), 2);
      if (!playSFX || this.m_playerHurtSoundHandle != -1)
        return;
      int randomSoundEventId = this.m_soundPoolManager.getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_VOCAL"), (int) ResourceManager.get("SOUNDEVENTPOOL_VOCAL_MED_IMPACTS"));
      this.m_playerHurtSoundHandle = this.m_engine.getSoundManager().playEvent(randomSoundEventId);
    }

    public bool triggerPopupBox(int stringId)
    {
      if (this.m_state == SceneGame.GameState.STATE_PLAYER_DEATH_FALLING)
        return false;
      PopupBox popupBox = new PopupBox(stringId);
      this.m_engine.getWindowStore().pushWindow((Window) popupBox);
      popupBox.setDeleteOnClosed(true);
      if (MirrorsEdge.TrialMode)
        this.m_bloomMixFilter.setSteadyState(0.5f);
      else
        this.m_bloomMixFilter.setSteadyState(this.m_map.getTheme() == 1 ? 0.75f : 0.5f);
      this.m_bloomBlendFilter.setSteadyState(0.9f);
      this.m_prePopupState = this.m_state;
      this.stateTransition(SceneGame.GameState.STATE_POPUP_BOX);
      return true;
    }

    public void playCutscene(int cutsceneId)
    {
      DataCameraAnim cutscene = this.m_map.getCutscene(cutsceneId);
      int targetId = cutscene.getTargetID();
      GameObject target = (GameObject) null;
      if (targetId == 1)
        target = (GameObject) this.m_map.getPlayerObject();
      this.m_gameCamera.playDataAnim(cutscene, target);
    }

    private void startIntroCam()
    {
      int introCamAnim = AppEngine.getLevelData().getCurrentLevelObject().getIntroCamAnim();
      if (introCamAnim == -1)
        return;
      this.m_gameCamera.startCameraAnimation(introCamAnim, true);
    }

    private void startTopMessage(int messageStringId, int subMessageStringId)
    {
      this.m_topMessageStringId = messageStringId;
      this.m_topMessageSubStringId = subMessageStringId;
      this.m_topMessageSection = 0;
      this.m_topMessageInterpolation.start(720f, 266f, 1000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_AHEAD);
      this.m_topSubMessageInterpolation.start(-200f, -200f, 1000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
    }

    private void updateTopMessage(int timeStepMillis)
    {
      if (this.m_topMessageStringId == 2048)
        return;
      this.m_topMessageInterpolation.update(timeStepMillis);
      this.m_topSubMessageInterpolation.update(timeStepMillis);
      if (!this.m_topMessageInterpolation.isFinished())
        return;
      switch (this.m_topMessageSection)
      {
        case 0:
          this.m_topMessageInterpolation.start(266f, 266f, 5000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
          this.m_topSubMessageInterpolation.start(-200f, -200f, 5000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
          break;
        case 1:
          this.m_topMessageInterpolation.start(266f, -266f, 1000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_BEHIND);
          this.m_topSubMessageInterpolation.start(-200f, 10f, 1000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_AHEAD);
          break;
        case 2:
          this.m_topMessageInterpolation.start(-266f, -266f, 5000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
          this.m_topSubMessageInterpolation.start(10f, 10f, 5000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
          break;
        case 3:
          this.m_topMessageInterpolation.start(-266f, -266f, 1000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
          this.m_topSubMessageInterpolation.start(10f, -200f, 1000, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_BEHIND);
          break;
        case 4:
          this.m_topMessageStringId = 2048;
          this.m_topMessageSubStringId = 2048;
          break;
      }
      ++this.m_topMessageSection;
    }

    private void renderTopMessage(midp.Graphics g)
    {
      if (this.m_topMessageStringId != 2048)
      {
        int currentValue = (int) this.m_topMessageInterpolation.getCurrentValue();
        int font = 14;
        TextManager textManager = AppEngine.getCanvas().getTextManager();
        if (textManager.getStringWidth(this.m_topMessageStringId, font) > 500)
        {
          font = 28;
          if (textManager.getStringWidth(this.m_topMessageStringId, font) > 500)
            font = 30;
        }
        StringRenderer stringRenderer = this.m_engine.getTextManager().getStringRenderer(font);
        int color = stringRenderer.getColor();
        stringRenderer.setColor(0);
        this.m_engine.getTextManager().drawString(g, this.m_topMessageStringId, font, currentValue + 1, 51, 66);
        stringRenderer.setColor(-3875073);
        this.m_engine.getTextManager().drawString(g, this.m_topMessageStringId, font, currentValue, 50, 66);
        stringRenderer.setColor(color);
      }
      if (this.m_topMessageSubStringId == 2048)
        return;
      int currentValue1 = (int) this.m_topSubMessageInterpolation.getCurrentValue();
      int y = this.m_engine.getHeight() - 20;
      StringRenderer stringRenderer1 = this.m_engine.getTextManager().getStringRenderer(15);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(0);
      this.m_engine.getTextManager().drawString(g, this.m_topMessageSubStringId, 15, currentValue1 + 1, y + 1, 65);
      stringRenderer1.setColor(color1);
      this.m_engine.getTextManager().drawString(g, this.m_topMessageSubStringId, 15, currentValue1, y, 65);
    }

    public SoundSequencer getSoundSequencer() => this.m_soundSequencer;

    public SoundEventPoolManager getSoundPoolManager() => this.m_soundPoolManager;

    public void setGameTimeFactor(float timeFactor)
    {
      this.m_gameTimeFactorFilter.setTargetValue(timeFactor);
    }

    public float getGameTimeFactor() => this.m_gameTimeFactorFilter.getFilteredValue();

    public void clearGameTimeFactor()
    {
      this.m_gameTimeFactorFilter.setTargetValue(1f);
      this.m_gameTimeFactorFilter.setSteadyState(1f);
    }

    public void startCombatTime() => this.m_combatTimeDuration = 1750;

    public bool isCombatTime() => this.m_combatTimeDuration > 0;

    public void setExitState(SceneGame.GameState state) => this.m_exitState = state;

    private void initMayhemWait()
    {
      this.m_pleaseWaitWindow = this.m_engine.getWindowStore().createWindow(WindowStore.WindowType.PLEASE_WAIT) as PleaseWaitWindow;
    }

    private void updateMayhemWait(int timeStep)
    {
      if (this.m_mayhemWaitPhase != SceneGame.MayhemWaitPhase.PHASE_FADE)
        return;
      if (this.m_engine.isFadedOut())
      {
        if (this.m_pleaseWaitWindow != null)
          this.m_pleaseWaitWindow.close(WindowResult.WINDOW_RESULT_NONE);
        this.stateTransition(this.m_exitState);
      }
      WindowResult windowResult = this.m_engine.getWindowStore().getWindowResult();
      if (windowResult == WindowResult.WINDOW_RESULT_NONE)
        return;
      this.m_engine.getWindowStore().clearWindowResult();
      if (windowResult == WindowResult.WINDOW_RESULT_EXIT)
      {
        this.m_engine.startFadeOut(true, 16777215);
      }
      else
      {
        if (windowResult == WindowResult.WINDOW_RESULT_POSITIVE || windowResult != WindowResult.WINDOW_RESULT_NEGATIVE)
          return;
        this.stateTransition(SceneGame.GameState.STATE_LEVEL_COMPLETE);
      }
    }

    public enum GameState
    {
      STATE_INVALID,
      STATE_LOADING,
      STATE_INTRO,
      STATE_GAME,
      STATE_GAME_FADE_OUT,
      STATE_PLAYER_DEATH_FALLING,
      STATE_PLAYER_DEATH_FALLING_NO_DEATH_SOUND,
      STATE_PLAYER_DIED,
      STATE_LEVEL_COMPLETE,
      STATE_MENU_PAUSED,
      STATE_GAME_OPTIONS,
      STATE_SOUND_OPTIONS,
      STATE_POPUP_BOX,
      STATE_CUTSCENE,
      STATE_HELP,
      STATE_ABOUT,
      STATE_RESTART_CONFIRM,
      STATE_QUIT_CONFIRM,
      STATE_FADE_TO_RESTART,
      STATE_TRANS_TO_NEXT,
      STATE_FADE_TO_MENU,
      STATE_FINISHED_GAME_STORY,
      STATE_MEDIAPICKER,
      STATE_UPLOAD_SCORE_CONFIRM,
      STATE_MAYHEM_DISPLAYNAME,
      STATE_MAYHEM_ADD_DISPLAYNAME,
      STATE_MAYHEM_WAIT,
      STATE_MAYHEM_POST_USER_FAILED,
      STATE_MAYHEM_POST_STAT_FAILED,
      STATE_FADE_EXIT,
      STATE_SELECT_CHALLENGEE,
      STATE_CHALLENGE_COMMENT_ENTRY,
    }

    private enum MayhemWaitPhase
    {
      PHASE_NONE,
      PHASE_FADE,
    }
  }
}
