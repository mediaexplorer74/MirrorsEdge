
// Type: game.GameObjectPlayer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using midp;
using GameManager;
using particles;
using support;
using System;

#nullable disable
namespace game
{
  public class GameObjectPlayer : GameObjectRunner
  {
    private const float SWINGING_PIVOT_POINT = 1.6f;
    private const int ROLL_STATE_ROLL_TIME = 500;
    public const int GESTURE_NONE = 0;
    public const int GESTURE_SINGLE_LEFT = 1;
    public const int GESTURE_DOUBLE_LEFT = 2;
    public const int GESTURE_SINGLE_RIGHT = 4;
    public const int GESTURE_DOUBLE_RIGHT = 8;
    public const int GESTURE_STOP = 16;
    public const int GESTURE_DOWN = 32;
    public const int GESTURE_JUMP = 64;
    public const int GESTURE_UP = 128;
    public const int GESTURE_ALL = 255;
    public const int GESTURE_ANY_LEFT = 3;
    public const int GESTURE_ANY_RIGHT = 12;
    public const int GESTURE_SINGLE_SIDE = 5;
    public const int GESTURE_DOUBLE_SIDE = 10;
    public const int GESTURE_ANY_SIDE = 15;
    public const int GHOST_MAX_KEYFRAME_DUR = 500;
    public const int WALL_ACTION_NONE = 0;
    public const int WALL_ACTION_CLAMBER_EXACT = 1;
    public const int WALL_ACTION_HALF_METRE_CLAMBER_SNAP = 2;
    public const int WALL_ACTION_LEDGE_PASS = 4;
    public const int WALL_ACTION_HANG = 8;
    public const int WALL_ACTION_CLIMB_SLIDE = 16;
    public const int WALL_RUN_IMMUNE_DUR = 750;
    private const float PLAYER_ZIP_OFFSET = 1.58f;
    private MathVector m_frameStartPos;
    private float m_swingingVelocity;
    private float m_swingingProgress;
    private float m_initialSwingPhase;
    private float m_swingingAngleDeg;
    private int m_swingingDirection;
    private GameObject m_swingingObject;
    private int m_swingImmunity;
    private SignalFilter m_accelerometerNoiseFilter;
    private int m_accelerometerNoiseTimer;
    private GameObjectRunner.FacingDir m_mapPlacementFacingDir;
    private MathVector m_lastCheckpointPosition;
    private GameObjectRunner.FacingDir m_lastCheckpointFacingDir;
    private GameObjectPlayer.PlayerState m_state;
    private int m_stateTime;
    private int m_disarmTime;
    private int m_disarmCooldown;
    private CollShape m_standingShape;
    private CollShape m_crouchingShape;
    private CollShape m_rampShape;
    private int m_controlGestures;
    private int m_rollGestureTimer;
    private GameObjectPlayer.AnimationState m_animState;
    private GameObjectPlayer.PlayerState m_animObjectState;
    private bool m_forceRefreshAnimation;
    private GhostAnimationRecorder m_ghostRecorder;
    private GhostKeyframe m_lastGhostKeyframe;
    private int m_ghostRecorderTime;
    private int m_regenDelay;
    private GameObjectPlayer.PlayerState m_soundState;
    private GameObjectPlayer.AnimationState m_soundAnimState;
    private int m_climbSequenceHandle;
    private int m_footstepsSequenceHandle;
    private bool m_ignoreGestures;
    private float m_targetForwardVelocity;
    private float m_targetForwardVelocityAcceleration;
    private int m_targetForwardVelocityAccelerationPause;
    private InterpolationFloatTimed m_velocityInterpolation;
    private InterpolationVectorTimed m_positionInterpolation;
    private bool m_positionInterpolationClip;
    private bool m_skipMovement;
    private bool m_noClip;
    private bool m_lineMovementEnabled;
    private float m_lineMovementSpeed;
    private MathVector m_lineMovementDirection = new MathVector();
    private GameObjectPlayer.LineClip m_lineMovementClip;
    private ParticleEffect m_zipLineSparkEffect;
    private ParticleEffect m_rampSlideEffect1;
    private ParticleEffect m_rampSlideEffect2;
    private ParticleEffect m_wallSlideEffectHand;
    private ParticleEffect m_wallSlideEffectFoot;
    private Node m_faithLeftFootLocator;
    private Node m_faithRightFootLocator;
    private Node m_faithHandFootLocator;
    private static Transform temp = new Transform();
    private static float[] position = new float[4];
    private int m_clamberOriginVisualStart;
    private int m_clamberOriginVisualEnd;
    private float m_clamberExitSpeed;
    private float m_clamberYOffset;
    private GameObjectPlayer.IgnoreCode m_ignoreClimbUp;
    private GameObjectPlayer.IgnoreCode m_ignoreWallSlide;
    private bool m_risingStateIsFromJump;
    private int m_postClamberBoostJumpTime;
    private bool m_slideAfterRoll;
    private int slidingTime;
    private int m_wallRunImmuneTimer;
    private int m_ledgeScaleHaulAnim;
    private GameObjectPlayer.PlayerState m_ledgeScalePostState;
    private GameObjectPlayer.AnimationState m_ledgeScalePostAnimState;
    private CollShape m_rampCollisionShape;
    private GameObjectZipLine m_attachedZipLine;
    private float m_attachedZiplineEndOffset;
    private MathVector m_zipLineJumpDestination = new MathVector();
    private int m_currentMaterial;
    private float m_health;
    public AchievementData m_achievementData;
    private int m_downedTime;

    public GameObjectPlayer(
      MEdgeMap map,
      float posX,
      float posY,
      float posZ,
      GameObjectRunner.FacingDir facingDir)
      : base(map, 0, posX, posY, posZ)
    {
      this.m_frameStartPos = new MathVector();
      this.m_swingingVelocity = 0.0f;
      this.m_swingingProgress = 0.0f;
      this.m_initialSwingPhase = 0.0f;
      this.m_swingingAngleDeg = 0.0f;
      this.m_swingingDirection = 1;
      this.m_swingingObject = (GameObject) null;
      this.m_swingImmunity = 0;
      this.m_accelerometerNoiseFilter = new SignalFilter(0, 2500f, 0.0f);
      this.m_accelerometerNoiseTimer = 0;
      this.m_state = GameObjectPlayer.PlayerState.STATE_INACTIVE;
      this.m_stateTime = 0;
      this.m_controlGestures = 0;
      this.m_rollGestureTimer = 0;
      this.m_animState = GameObjectPlayer.AnimationState.ANIM_STATE_NONE;
      this.m_animObjectState = GameObjectPlayer.PlayerState.STATE_INACTIVE;
      this.m_forceRefreshAnimation = false;
      this.m_mapPlacementFacingDir = facingDir;
      this.m_lastCheckpointPosition = new MathVector(posX, posY, posZ);
      this.m_lastCheckpointFacingDir = facingDir;
      this.m_ghostRecorder = (GhostAnimationRecorder) null;
      this.m_lastGhostKeyframe = (GhostKeyframe) null;
      this.m_ghostRecorderTime = 0;
      this.m_soundState = GameObjectPlayer.PlayerState.STATE_INACTIVE;
      this.m_soundAnimState = GameObjectPlayer.AnimationState.ANIM_STATE_NONE;
      this.m_ignoreGestures = false;
      this.m_targetForwardVelocity = 0.0f;
      this.m_targetForwardVelocityAcceleration = 0.0f;
      this.m_targetForwardVelocityAccelerationPause = 0;
      this.m_velocityInterpolation = new InterpolationFloatTimed();
      this.m_positionInterpolation = new InterpolationVectorTimed();
      this.m_positionInterpolationClip = false;
      this.m_skipMovement = false;
      this.m_noClip = false;
      this.m_lineMovementEnabled = false;
      this.m_lineMovementSpeed = 0.0f;
      this.m_lineMovementDirection = new MathVector();
      this.m_lineMovementClip = GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_NONE;
      this.m_zipLineSparkEffect = (ParticleEffect) null;
      this.m_rampSlideEffect1 = (ParticleEffect) null;
      this.m_rampSlideEffect2 = (ParticleEffect) null;
      this.m_wallSlideEffectHand = (ParticleEffect) null;
      this.m_wallSlideEffectFoot = (ParticleEffect) null;
      this.m_clamberOriginVisualStart = -1;
      this.m_clamberOriginVisualEnd = -1;
      this.m_clamberExitSpeed = 0.0f;
      this.m_clamberYOffset = 0.0f;
      this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
      this.m_ignoreWallSlide = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
      this.m_risingStateIsFromJump = false;
      this.m_postClamberBoostJumpTime = -1;
      this.m_slideAfterRoll = false;
      this.m_wallRunImmuneTimer = -1;
      this.m_ledgeScaleHaulAnim = 0;
      this.m_ledgeScalePostState = GameObjectPlayer.PlayerState.STATE_IDLE;
      this.m_ledgeScalePostAnimState = GameObjectPlayer.AnimationState.ANIM_STATE_NONE;
      this.m_rampCollisionShape = (CollShape) null;
      this.m_faithLeftFootLocator = (Node) null;
      this.m_faithRightFootLocator = (Node) null;
      this.m_faithHandFootLocator = (Node) null;
      this.m_attachedZipLine = (GameObjectZipLine) null;
      this.m_attachedZiplineEndOffset = 0.0f;
      this.m_zipLineJumpDestination = new MathVector();
      this.m_currentMaterial = 0;
      this.m_footstepsSequenceHandle = -1;
      this.m_climbSequenceHandle = -1;
      this.m_health = 10f;
      this.m_regenDelay = 0;
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData = AppEngine.getAchievementData();
      this.m_disarmTime = 0;
      this.m_disarmCooldown = 0;
      this.m_downedTime = 0;
      this.setVisualAssets(map.getModelId(1), 0);
      this.setFacingDir(facingDir);
      if (!MirrorsEdge.TrialMode)
      {
        M3GAssets.orphanNode((Node) this.m_objectNode.find(103));
        M3GAssets.orphanNode((Node) this.m_objectNode.find(104));
      }
      AnimationManager3D animationManager3D = AppEngine.getCanvas().getAnimationManager3D();
      this.m_originNode = AppEngine.getM3GAssets().loadNode((int) M3GAssets.get("NODE_FAITH_ORIGIN"));
      this.m_originAnimPlayer = new AnimPlayer3D(animationManager3D);
      this.m_originAnimPlayer.setNode(this.m_originNode);
      this.m_standingShape = (CollShape) new CollOctahedron(-0.5f, 0.0f, -0.5f, 0.5f, 1.8f, 0.5f);
      this.m_standingShape.setBounce(0.0f);
      this.m_crouchingShape = (CollShape) new CollOctahedron(-0.5f, 0.0f, -0.5f, 0.5f, 0.5f, 0.5f);
      this.m_crouchingShape.setBounce(0.0f);
      this.m_rampShape = (CollShape) new CollOrthoHexahedron(-0.5f, 0.9f, -0.5f, 0.5f, 0.9f, 0.5f);
      this.m_rampShape.setBounce(0.0f);
      this.setFacingDir(this.getFacingDir());
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_INTRO_ANIM);
      if (AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN || AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_CHALLENGE && !MirrorsEdge.TrialMode)
      {
        this.m_ghostRecorder = new GhostAnimationRecorder();
        this.m_lastGhostKeyframe = new GhostKeyframe(this.m_ghostRecorderTime, this.m_position, GameObjectRunner.hashVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_IDLE_SNAP"), (int) this.getFacingDir()), this.get3WayAnimationBlendWeights());
        this.m_ghostRecorder.addKeyframe(this.m_lastGhostKeyframe);
        this.m_ghostRecorderTime = 0;
      }
      this.m_faithLeftFootLocator = (Node) this.m_objectNode.find(106);
      this.m_faithRightFootLocator = (Node) this.m_objectNode.find(105);
      this.m_faithHandFootLocator = (Node) this.m_objectNode.find(110);
    }

    public override void Destructor()
    {
      if (this.m_accelerometerNoiseFilter == null)
        return;
      this.m_accelerometerNoiseFilter.Destructor();
      this.m_accelerometerNoiseFilter = (SignalFilter) null;
      this.m_originAnimPlayer.Destructor();
      this.m_originAnimPlayer = (AnimPlayer3D) null;
      if (this.m_ghostRecorder != null)
      {
        this.m_ghostRecorder.Destructor();
        this.m_ghostRecorder = (GhostAnimationRecorder) null;
      }
      this.m_lastGhostKeyframe = (GhostKeyframe) null;
      this.m_faithLeftFootLocator = (Node) null;
      this.m_faithRightFootLocator = (Node) null;
      this.m_faithHandFootLocator = (Node) null;
      this.m_standingShape.Destructor();
      this.m_standingShape = (CollShape) null;
      this.m_crouchingShape.Destructor();
      this.m_crouchingShape = (CollShape) null;
      this.m_rampShape.Destructor();
      this.m_rampShape = (CollShape) null;
      this.m_velocityInterpolation.Destructor();
      this.m_velocityInterpolation = (InterpolationFloatTimed) null;
      this.m_positionInterpolation.Destructor();
      this.m_positionInterpolation = (InterpolationVectorTimed) null;
      if (this.m_zipLineSparkEffect != null)
      {
        this.m_zipLineSparkEffect.Destructor();
        this.m_zipLineSparkEffect = (ParticleEffect) null;
      }
      if (this.m_rampSlideEffect1 != null)
      {
        this.m_rampSlideEffect1.Destructor();
        this.m_rampSlideEffect1 = (ParticleEffect) null;
      }
      if (this.m_rampSlideEffect2 != null)
      {
        this.m_rampSlideEffect2.Destructor();
        this.m_rampSlideEffect2 = (ParticleEffect) null;
      }
      if (this.m_wallSlideEffectHand != null)
      {
        this.m_wallSlideEffectHand.Destructor();
        this.m_wallSlideEffectHand = (ParticleEffect) null;
      }
      if (this.m_wallSlideEffectFoot != null)
      {
        this.m_wallSlideEffectFoot.Destructor();
        this.m_wallSlideEffectFoot = (ParticleEffect) null;
      }
      this.m_achievementData = (AchievementData) null;
      base.Destructor();
    }

    public override void checkpointActivated()
    {
      GameObjectCheckpoint checkpointObject = this.m_map.getCheckpointObject();
      MathOrthoBox globalBounds = checkpointObject.getGlobalBounds();
      MathLine line = new MathLine((float) (((double) globalBounds.min.x + (double) globalBounds.max.x) * 0.5), (float) (((double) globalBounds.min.y + (double) globalBounds.max.y) * 0.5), (float) (((double) globalBounds.min.z + (double) globalBounds.max.z) * 0.5), 0.0f, -1f, 0.0f);
      float tValue = 0.0f;
      if (!this.m_map.calculateCollision(line, ref tValue, 0))
        return;
      this.m_lastCheckpointPosition = line.origin;
      this.m_lastCheckpointPosition.y -= tValue;
      this.m_lastCheckpointFacingDir = checkpointObject.getPlayerFacingDir();
    }

    private void resetCommon()
    {
      this.m_controlGestures = 0;
      this.m_swingingObject = (GameObject) null;
      this.m_swingImmunity = 0;
      this.m_health = 10f;
      this.m_regenDelay = 0;
      this.setNoVelocity();
      this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_IDLE_SNAP"));
    }

    public override void resetCheckpoint()
    {
      this.resetCommon();
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
      this.m_position = this.m_lastCheckpointPosition;
      this.setFacingDir(this.m_lastCheckpointFacingDir);
    }

    public override void resetLevel()
    {
      base.resetLevel();
      this.setNoVelocity();
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_INACTIVE);
      this.resetCommon();
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_INTRO_ANIM);
      this.setFacingDir(this.m_mapPlacementFacingDir);
      this.m_lastCheckpointPosition = this.m_position;
      this.m_lastCheckpointFacingDir = this.m_mapPlacementFacingDir;
      this.m_forceRefreshAnimation = true;
      this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_IDLE_SNAP"));
      if (this.m_ghostRecorder == null)
        return;
      this.m_ghostRecorder.reset();
      this.m_ghostRecorderTime = 0;
    }

    public override void collidedWith(GameObject other)
    {
      switch (other.getType())
      {
        case 16:
          if (this.m_state == GameObjectPlayer.PlayerState.STATE_ZIP_LINE)
            break;
          MathLine mathLine = new MathLine(((GameObjectZipLine) other).getZipLineLine());
          if (0.0 < (double) mathLine.direction.x != (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_RIGHT))
            break;
          float tatX = mathLine.calculateTatX(this.m_position.x);
          if ((double) tatX < 0.0 || 0.89999997615814209 < (double) tatX)
            break;
          float yatT1 = mathLine.calculateYatT(mathLine.calculateTatX(this.m_frameStartPos.x));
          float yatT2 = mathLine.calculateYatT(tatX);
          if ((double) yatT1 >= (double) this.m_frameStartPos.y + 1.5800000429153442 || (double) this.m_position.y + 1.5800000429153442 > (double) yatT2)
            break;
          this.m_position.y = (float) ((double) yatT2 - 1.5800000429153442 - 0.0099999997764825821);
          this.playerZipLine((GameObjectZipLine) other);
          break;
        case 17:
          if (this.m_swingImmunity != 0 && this.m_swingingObject == other)
          {
            this.m_swingImmunity = 2;
            break;
          }
          if (this.m_state == GameObjectPlayer.PlayerState.STATE_SWINGING)
            break;
          this.m_position.x += other.m_position.x - this.m_position.x;
          this.m_position.y += other.m_position.y - (this.m_position.y + 1.6f);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_SWINGING);
          this.m_swingingObject = other;
          this.m_swingImmunity = 2;
          break;
      }
    }

    private float getAccelerometerTilt(bool noisy)
    {
      return Math.Max(Math.Min(1f, 4f * AppEngine.getCanvas().getSceneGame().getAccelerometerTilt() + (noisy ? 1.1f * this.m_accelerometerNoiseFilter.getFilteredValue() : 0.0f)), -1f);
    }

    private GameObjectPlayer.PlayerState getState() => this.m_state;

    public bool isKicking() => this.m_state == GameObjectPlayer.PlayerState.STATE_FLYKICK;

    public bool isSliding() => this.m_state == GameObjectPlayer.PlayerState.STATE_SLIDING;

    public bool isRolling()
    {
      return this.m_state == GameObjectPlayer.PlayerState.STATE_ROLLING && this.m_objectAnimPlayer.getAnimTime() < 500;
    }

    public bool isLying() => this.m_state == GameObjectPlayer.PlayerState.STATE_PRONE;

    public bool isMeleed() => this.m_state == GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE;

    public bool isGetupMeleeing() => this.m_state == GameObjectPlayer.PlayerState.STATE_MELEE_GETUP;

    public bool isGettingUp() => this.m_state == GameObjectPlayer.PlayerState.STATE_GETUP;

    public bool isDisarming() => this.m_disarmTime > 0;

    public bool isProne() => this.m_state == GameObjectPlayer.PlayerState.STATE_PRONE;

    public bool isInIntroAnim() => this.m_state == GameObjectPlayer.PlayerState.STATE_INTRO_ANIM;

    public void stateTransition(GameObjectPlayer.PlayerState newState)
    {
      if (this.m_state == newState)
        return;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      bool flag1 = newState == GameObjectPlayer.PlayerState.STATE_SLIDING || newState == GameObjectPlayer.PlayerState.STATE_WALLSLIDE || newState == GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE;
      bool flag2 = this.m_state == GameObjectPlayer.PlayerState.STATE_SLIDING || this.m_state == GameObjectPlayer.PlayerState.STATE_WALLSLIDE || this.m_state == GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE;
      if (!flag1 && flag2)
      {
        soundManager.stopEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_LOOP"));
        soundManager.playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_OUT"), 0.8f);
      }
      else if (!flag2 && flag1)
      {
        soundManager.playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_IN"), 0.8f);
        soundManager.playEventLooped((int) ResourceManager.get("SOUNDEVENT_SFX_SLIDE_LOOP"), 0.8f);
      }
      bool flag3 = newState == GameObjectPlayer.PlayerState.STATE_ZIP_LINE && this.getState() != GameObjectPlayer.PlayerState.STATE_ZIP_LINE;
      bool flag4 = newState != GameObjectPlayer.PlayerState.STATE_ZIP_LINE && this.getState() == GameObjectPlayer.PlayerState.STATE_ZIP_LINE;
      if (flag3)
      {
        soundManager.playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_IN"), 1f);
        int handle = soundManager.playEventLooped((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_LOOP"), 1f);
        soundManager.setVolumeEvent(handle, 0.1f);
      }
      else if (flag4)
      {
        soundManager.stopEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_LOOP"));
        soundManager.playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_ZIP_OUT"), 1f);
      }
      GameObjectPlayer.PlayerState state = this.m_state;
      this.m_state = newState;
      this.m_stateTime = 0;
      if (GameObjectPlayer.isCrouchState(newState))
        this.m_globalShape = this.m_crouchingShape;
      else if (newState == GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP && this.m_globalShape != this.m_rampShape)
        this.m_globalShape = this.m_rampShape;
      else if (this.m_globalShape != this.m_standingShape)
      {
        this.m_standingShape.setPosition(this.m_position);
        if (state != GameObjectPlayer.PlayerState.STATE_INACTIVE && state != GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP && this.m_map.intersects(this.m_standingShape, 0))
          this.m_state = state;
        else
          this.m_globalShape = this.m_standingShape;
      }
      switch (state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          this.deinitStateIdle();
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          this.deinitStateRunning();
          break;
        case GameObjectPlayer.PlayerState.STATE_BALANCE_FELL:
          this.deinitStateBalanceFell();
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
          this.deinitStateFallingVertically();
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          this.deinitStateFallingForwards();
          break;
        case GameObjectPlayer.PlayerState.STATE_CRASH:
          this.deinitStateCrash();
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          this.deinitStateWallClimb();
          break;
        case GameObjectPlayer.PlayerState.STATE_CLAMBER:
          this.deinitStateClamber();
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          this.deinitStateHang();
          break;
        case GameObjectPlayer.PlayerState.STATE_SLIDING:
          this.deinitStateSliding();
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
          this.deinitStateWallRun();
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.deinitStateWallSlide();
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          this.deinitStateSwinging();
          break;
        case GameObjectPlayer.PlayerState.STATE_SCALE_JUMP:
        case GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP:
          this.deinitStateScale();
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          this.deinitStateRampAny();
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_JUMP_TO:
          this.deinitStateZipLineJumpTo();
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
          this.deinitStateZipLine();
          break;
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          this.deinitStateFlykick();
          break;
      }
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_BALANCE_FELL:
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
        case GameObjectPlayer.PlayerState.STATE_HANG:
        case GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER:
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
        case GameObjectPlayer.PlayerState.STATE_SCALE_JUMP:
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_JUMP_TO:
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_FAIL:
        case GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE:
        case GameObjectPlayer.PlayerState.STATE_DYING:
          this.setVerticalAcceleration(0.0f);
          break;
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          this.setVerticalAcceleration(-6f);
          break;
        default:
          this.setVerticalAcceleration(-9.8f);
          break;
      }
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          this.initStateIdle(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          this.initStateRunning(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING_TURNING:
          this.initStateRunningTurning(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING:
          this.initStateBalanceRunning(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_BALANCE_FELL:
          this.initStateBalanceFell(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_STOPPING:
          this.initStateStopping(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
          this.initStateFalling(state);
          this.initStateFallingVertically(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          this.initStateFalling(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_ROLLING:
          this.initStateRolling(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_CRASH:
          this.initStateCrash(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          this.initStateWallClimb(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_CLAMBER:
          this.initStateClamber(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          this.initStateHang(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER:
          this.initStateHangClamber(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_SLIDING:
          this.initStateSliding(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.initStateWallSlide(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          this.initStateSwinging(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_SCALE_JUMP:
          this.initStateScaleJump(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          this.initStateRampRunUp(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          this.initStateRampSlide(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_JUMP_TO:
          this.initStateZipLineJumpTo(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
          this.initStateZipLine(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_FAIL:
          this.initStateZipLineFail(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE:
          if (MirrorsEdge.TrialMode)
            break;
          this.initStateMeleedInTheFace(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_DISARMING_COP:
          if (MirrorsEdge.TrialMode)
            break;
          this.initStateDisarmingCop(state);
          break;
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          this.initStateFlykick(state);
          break;
      }
    }

    private static bool isCrouchState(GameObjectPlayer.PlayerState state)
    {
      return state == GameObjectPlayer.PlayerState.STATE_ROLLING || state == GameObjectPlayer.PlayerState.STATE_PRONE;
    }

    public bool isAnyGestureSet(int gesture) => (this.m_controlGestures & gesture) != 0;

    public bool isAllGesturesSet(int gesture) => (this.m_controlGestures & gesture) == gesture;

    public void addGesture(int gesture) => this.m_controlGestures |= gesture;

    public void clearGesture(int gesture) => this.m_controlGestures &= ~gesture;

    public void clearAllGestures() => this.m_controlGestures = 0;

    public void clearAllGesturesExcept(int gestures) => this.m_controlGestures &= gestures;

    public int getTapGesture()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          return 16;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          return 64;
        default:
          return 16;
      }
    }

    protected void animStateTransition(GameObjectPlayer.AnimationState newState)
    {
      this.m_animState = newState;
      this.m_forceRefreshAnimation = true;
    }

    protected void refreshAnimation() => this.m_forceRefreshAnimation = true;

    public GameObjectRunner.FacingDir getCameraFacingDir()
    {
      return (GameObjectRunner.FacingDir) ((int) this.getFacingDir() * (this.m_state == GameObjectPlayer.PlayerState.STATE_WALLSLIDE ? -1 : 1));
    }

    public float getForwardSpeed() => this.getForwardVelocity() * (float) this.getFacingDir();

    public void setForwardSpeed(float newSpeed)
    {
      this.setForwardVelocity((float) this.getFacingDir() * newSpeed);
    }

    public GhostAnimationRecorder getGhostAnimation() => this.m_ghostRecorder;

    private void updateGhost(int timeStepMillis)
    {
      int visual = this.getVisual();
      this.m_ghostRecorderTime += timeStepMillis;
      if (500 <= this.m_ghostRecorderTime)
      {
        this.m_lastGhostKeyframe = new GhostKeyframe(this.m_ghostRecorderTime, this.m_position, visual, this.get3WayAnimationBlendWeights());
        this.m_ghostRecorder.addKeyframe(this.m_lastGhostKeyframe);
        this.m_ghostRecorderTime = 0;
      }
      else
      {
        if ((int) this.m_lastGhostKeyframe.visualCode == visual)
          return;
        this.m_lastGhostKeyframe = new GhostKeyframe(this.m_ghostRecorderTime, this.m_position, visual, this.get3WayAnimationBlendWeights());
        this.m_ghostRecorder.addKeyframe(this.m_lastGhostKeyframe);
        this.m_ghostRecorderTime = 0;
      }
    }

    public override void update(int timeStepMillis)
    {
      if (this.m_state == GameObjectPlayer.PlayerState.STATE_INACTIVE)
        return;
      if (this.m_disarmTime > 0)
        this.m_disarmTime -= timeStepMillis;
      if (this.m_disarmCooldown > 0)
        this.m_disarmCooldown -= timeStepMillis;
      if (0 < this.m_postClamberBoostJumpTime)
        this.m_postClamberBoostJumpTime -= timeStepMillis;
      this.m_stateTime += timeStepMillis;
      this.m_frameStartPos = this.m_position;
      if (this.m_state == GameObjectPlayer.PlayerState.STATE_DEAD)
      {
        if (this.m_stateTime < 1000)
          return;
        AppEngine.getCanvas().getSceneGame().stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING_NO_DEATH_SOUND);
      }
      else
      {
        this.updateHealth(timeStepMillis);
        this.applyGestures();
        this.setupAnimation();
        this.updateVelocity(timeStepMillis);
        base.update(timeStepMillis);
        this.setupAnimation();
        this.updateMovement(timeStepMillis);
        this.clearInvalidGestures();
        this.setupAnimation();
        this.updateState(timeStepMillis);
        this.setupAnimation();
        this.updateSound(timeStepMillis);
        if (this.m_ghostRecorder != null)
          this.updateGhost(timeStepMillis);
        if (this.m_wallRunImmuneTimer != -1)
          this.m_wallRunImmuneTimer -= timeStepMillis;
        if (this.m_ignoreClimbUp == GameObjectPlayer.IgnoreCode.IGNORE_ONE_FRAME)
          this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
        if (this.m_ignoreWallSlide != GameObjectPlayer.IgnoreCode.IGNORE_ONE_FRAME)
          return;
        this.m_ignoreWallSlide = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
      }
    }

    private void updateHealth(int timeStepMillis)
    {
      if (this.m_regenDelay <= 0 && (double) this.m_health < 10.0)
      {
        this.m_health += 3.3f * ((float) timeStepMillis / 1000f);
        if ((double) this.m_health <= 10.0)
          return;
        this.m_health = 10f;
      }
      else
        this.m_regenDelay -= timeStepMillis;
    }

    private void updateSound(int timeStepMillis)
    {
      AppEngine.getCanvas().getSoundManager().setListenerPosition(this.m_position.x, this.m_position.y, this.m_position.z);
      if (this.m_state == this.m_soundState && this.m_animState == this.m_soundAnimState)
        return;
      this.m_soundState = this.m_state;
      this.m_soundAnimState = this.m_animState;
      this.setupSoundSequence();
    }

    private void setupSoundSequence()
    {
      SoundSequencer soundSequencer = AppEngine.getCanvas().getSceneGame().getSoundSequencer();
      soundSequencer.stopSequence(this.m_footstepsSequenceHandle);
      this.m_footstepsSequenceHandle = -1;
      if (this.m_animState == GameObjectPlayer.AnimationState.ANIM_STATE_LANDING)
      {
        soundSequencer.playSequence(6);
      }
      else
      {
        switch (this.m_state)
        {
          case GameObjectPlayer.PlayerState.STATE_RUNNING:
          case GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING:
          case GameObjectPlayer.PlayerState.STATE_WALLRUN:
          case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
            this.m_footstepsSequenceHandle = soundSequencer.playSequence(0);
            break;
          case GameObjectPlayer.PlayerState.STATE_ROLLING:
            soundSequencer.playSequence(7);
            break;
          case GameObjectPlayer.PlayerState.STATE_CRASH:
            soundSequencer.playSequence(8);
            break;
          case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          case GameObjectPlayer.PlayerState.STATE_CLAMBER:
            if (soundSequencer.isSequencePlaying(this.m_climbSequenceHandle))
              break;
            this.m_climbSequenceHandle = soundSequencer.playSequence(1);
            break;
        }
      }
    }

    private void setupAnimation()
    {
      if (!this.m_forceRefreshAnimation && this.m_animObjectState == this.m_state)
        return;
      this.m_forceRefreshAnimation = false;
      this.m_animObjectState = this.m_state;
      this.m_originAnimPlayer.setAnimating(false);
      if (this.m_objectAnimPlayer == null)
        return;
      if (this.m_animState != GameObjectPlayer.AnimationState.ANIM_STATE_NONE)
      {
        switch (this.m_animState)
        {
          case GameObjectPlayer.AnimationState.ANIM_STATE_LANDING:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_JUMP_LANDING"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_BOOST_JUMP:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_JUMP_FROM_CLAMBER"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_POWER_JUMP_START:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_POWER_JUMP_START"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_POWER_JUMP_LOOP:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_POWER_JUMP_LOOP"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_SCALE_HALF_STEP:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_SCALE_HALF_STEP"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_HANG_TO_SLIDE:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_TRANS_HANG_TO_WALLSLIDE"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_WALLSLIDE_TO_JUMP:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_WALLSLIDE_JUMP"));
            break;
          case GameObjectPlayer.AnimationState.ANIM_STATE_FLYING_KICK_IMPACT:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_FLYING_KICK_IMPACT"));
            break;
        }
      }
      else
      {
        switch (this.m_state)
        {
          case GameObjectPlayer.PlayerState.STATE_INTRO_ANIM:
            this.setVisual(0, AppEngine.getLevelData().getCurrentLevelObject().getIntroFaithAnim());
            break;
          case GameObjectPlayer.PlayerState.STATE_IDLE:
            if (this.getVisualType() == 0 && this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING"))
            {
              this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING_END"));
              break;
            }
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_IDLE"));
            break;
          case GameObjectPlayer.PlayerState.STATE_RUNNING:
            if (this.getVisualType() == 0 && this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING"))
              this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING_END"));
            else
              this.setVisual(2, 0);
            this.updateAnimationStateRunning();
            break;
          case GameObjectPlayer.PlayerState.STATE_RUNNING_TURNING:
            break;
          case GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING:
            this.setVisual(2, 1);
            break;
          case GameObjectPlayer.PlayerState.STATE_BALANCE_FELL:
            break;
          case GameObjectPlayer.PlayerState.STATE_STOPPING:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_STOPPING"));
            break;
          case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
          case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          case GameObjectPlayer.PlayerState.STATE_FALLING_CRASHING:
          case GameObjectPlayer.PlayerState.STATE_SCALE_JUMP:
            if ((double) this.getUpVelocity() > 0.0)
            {
              if (this.m_risingStateIsFromJump)
              {
                this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_RISING_SNAP"));
                break;
              }
              if (this.getVisualType() == 0 && this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_RISING_SNAP"))
                break;
              this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_RISING"));
              break;
            }
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_FALLING"));
            break;
          case GameObjectPlayer.PlayerState.STATE_ROLLING:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_ROLLING"));
            break;
          case GameObjectPlayer.PlayerState.STATE_CRASH:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_CRASH"));
            break;
          case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_WALLCLIMB"));
            break;
          case GameObjectPlayer.PlayerState.STATE_CLAMBER:
            if (this.m_clamberOriginVisualEnd != -1 && this.getVisualType() == 1 && this.getVisualIndex() == this.m_clamberOriginVisualStart)
            {
              this.setVisual(1, this.m_clamberOriginVisualEnd);
              break;
            }
            this.setOffsetVisual(this.m_clamberOriginVisualStart, this.m_clamberYOffset);
            break;
          case GameObjectPlayer.PlayerState.STATE_HANG:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_HANG"));
            break;
          case GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER:
            this.setVisual(1, 5);
            break;
          case GameObjectPlayer.PlayerState.STATE_SLIDING:
            int visualIndex = this.getVisualIndex();
            if (this.getVisualType() == 0 && (visualIndex == (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING_START") || visualIndex == (int) GameObjectRunner.get("VISUAL_SOLO_ROLLING")))
            {
              this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING"));
              break;
            }
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING_START"));
            break;
          case GameObjectPlayer.PlayerState.STATE_WALLRUN:
            if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
            {
              this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_WALLRUN_LEFT"));
              break;
            }
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_WALLRUN_RIGHT"));
            break;
          case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_WALLSLIDE"));
            break;
          case GameObjectPlayer.PlayerState.STATE_SWINGING:
            this.setVisual(2, 2);
            break;
          case GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP:
            if (GameCommon.isZero(this.getForwardVelocity()))
            {
              if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
              {
                this.setVisual(1, 6);
                break;
              }
              this.setVisual(1, 7);
              break;
            }
            if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
            {
              this.setVisual(1, 8);
              break;
            }
            this.setVisual(1, 9);
            break;
          case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_RUNNING_UPHILL"));
            break;
          case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
            this.setVisual(2, 3);
            break;
          case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_JUMP_TO:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_ZIP_LINE"));
            break;
          case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
            this.setVisual(2, 4);
            break;
          case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_FAIL:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_ZIPLINE_FAIL"));
            break;
          case GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE:
            if (MirrorsEdge.TrialMode)
              break;
            this.setVisual(1, 10);
            break;
          case GameObjectPlayer.PlayerState.STATE_DISARMING_COP:
            if (MirrorsEdge.TrialMode)
              break;
            this.setNoVelocity();
            this.setVisual(1, 11);
            break;
          case GameObjectPlayer.PlayerState.STATE_PRONE:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_PRONE_IDLE"));
            break;
          case GameObjectPlayer.PlayerState.STATE_GETUP:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_PRONE_RECOVER"));
            break;
          case GameObjectPlayer.PlayerState.STATE_MELEE_GETUP:
            if (MirrorsEdge.TrialMode)
              break;
            this.setVisual(1, 12);
            break;
          case GameObjectPlayer.PlayerState.STATE_FLYKICK:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_FLYING_KICK_LOOP"));
            break;
          case GameObjectPlayer.PlayerState.STATE_DYING:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_DYING_RUNNING"));
            break;
          case GameObjectPlayer.PlayerState.STATE_DEAD:
            break;
          default:
            this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_IDLE"));
            break;
        }
      }
    }

    protected override void animationStopped()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_INTRO_ANIM:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
          break;
        case GameObjectPlayer.PlayerState.STATE_CLAMBER:
          this.animStoppedStateClamber();
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER:
          this.animStoppedStateHangClamber();
          break;
        case GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP:
          this.animStoppedStateScaleHaulUp();
          break;
      }
      this.refreshAnimation();
      this.setupAnimation();
    }

    private void applyGestures()
    {
      if (this.m_ignoreGestures)
        this.m_controlGestures = 0;
      if (this.isAnyGestureSet(3) ^ this.isAnyGestureSet(12))
        this.applyGestureSide();
      if (this.isAnyGestureSet(16))
        this.applyGestureStop();
      if (this.isAnyGestureSet(64))
        this.applyGestureJump();
      if (this.isAnyGestureSet(128))
        this.applyGestureUp();
      if (!this.isAnyGestureSet(32))
        return;
      this.applyGestureDown();
    }

    private void applyGestureSide()
    {
      int facingDir = (int) this.getFacingDir();
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
        case GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING:
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
        case GameObjectPlayer.PlayerState.STATE_HANG:
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          this.setFacingDir(this.isAnyGestureSet(3) ? GameObjectRunner.FacingDir.FACING_LEFT : GameObjectRunner.FacingDir.FACING_RIGHT);
          break;
      }
      bool flag = this.getFacingDir() != (GameObjectRunner.FacingDir) facingDir;
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          if (this.checkWallAction(this.getFacingDir(), false, 1))
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          if (flag)
          {
            if (7.0 <= (double) this.getForwardSpeed())
            {
              this.setForwardVelocity((float) this.getFacingDir() * 5f);
              break;
            }
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING_TURNING);
            this.reverseFacingDir();
            break;
          }
          if (this.m_disarmCooldown > 0)
            break;
          this.m_disarmTime = 1000;
          this.m_disarmCooldown = 2000;
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
          this.setForwardVelocity((float) this.getFacingDir() * 1f);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          if (flag)
          {
            float num = Math.Min(Math.Abs(this.getForwardVelocity()), 1f);
            this.setForwardVelocity((float) this.getFacingDir() * num);
            break;
          }
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FLYKICK);
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          if (this.intersectsMap())
          {
            if (!flag)
              break;
            this.reverseFacingDir();
            break;
          }
          if (flag)
          {
            this.setForwardVelocity((float) this.getFacingDir() * 7f);
            this.setUpVelocity(5f);
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
            break;
          }
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          if (flag)
          {
            this.reverseFacingDir();
            break;
          }
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          this.setForwardVelocity(0.01f * (float) this.getFacingDir());
          this.m_ignoreWallSlide = GameObjectPlayer.IgnoreCode.IGNORE_CONSTANT;
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          if (flag)
            break;
          this.playerSwingLaunch();
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          if (!flag)
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE);
          break;
      }
    }

    private void applyGestureStop()
    {
      if (this.m_state != GameObjectPlayer.PlayerState.STATE_RUNNING)
        return;
      if (7.0 <= (double) this.getForwardSpeed())
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_STOPPING);
      else
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
    }

    private void applyGestureJump()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          if (this.checkWallAction(this.getFacingDir(), false, 1))
          {
            this.clearGesture(128);
            break;
          }
          this.playerJump(false);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          this.playerJump(false);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          int num1 = -1 * (int) this.getFacingDir();
          this.setFacingDir((GameObjectRunner.FacingDir) num1);
          this.setForwardVelocity((float) num1 * 7f);
          this.setUpVelocity(5f);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          int num2 = -(int) this.getFacingDir();
          this.setFacingDir((GameObjectRunner.FacingDir) num2);
          this.setForwardVelocity((float) num2 * 7f);
          this.setUpVelocity(5f);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_WALLSLIDE_TO_JUMP);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          this.setForwardVelocity((float) this.getFacingDir() * 10f);
          this.setUpVelocity(5f);
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          this.playerSwingLaunch();
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          this.setForwardVelocity((float) this.getFacingDir() * 7f);
          this.setUpVelocity(5f);
          break;
      }
    }

    private void applyGestureUp()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          if (this.checkWallAction(this.getFacingDir(), false, 1) || this.checkJumpToZipLine())
            break;
          if (this.checkLedgeScale())
          {
            this.m_skipMovement = true;
            break;
          }
          this.playerJump(false);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          if (this.checkJumpToZipLine())
            break;
          if (this.checkLedgeScale())
          {
            this.m_skipMovement = true;
            break;
          }
          if (this.checkWallrun())
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLRUN);
            break;
          }
          if (0 < this.m_postClamberBoostJumpTime)
          {
            this.playerJump(true);
            break;
          }
          this.playerJump(false);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          if (!this.checkWallrun() || this.m_wallRunImmuneTimer > 0)
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLRUN);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          int num = -1 * (int) this.getFacingDir();
          this.setFacingDir((GameObjectRunner.FacingDir) num);
          this.setForwardVelocity((float) num * 7f);
          this.setUpVelocity(5f);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          this.clearGesture(128);
          break;
        case GameObjectPlayer.PlayerState.STATE_CLAMBER:
          if (this.getVisualIndex() != this.m_clamberOriginVisualEnd)
            break;
          this.snapPlayerToGround();
          this.playerJump(true);
          this.clearGesture(128);
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER);
          this.clearGesture(192);
          break;
        case GameObjectPlayer.PlayerState.STATE_SLIDING:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          this.playerJump(false);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_WALLSLIDE_TO_JUMP);
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          this.setForwardVelocity((float) this.getFacingDir() * 7f);
          this.setUpVelocity(5f);
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          this.playerSwingLaunch();
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          this.setForwardVelocity((float) this.getFacingDir() * 7f);
          this.setUpVelocity(5f);
          break;
        case GameObjectPlayer.PlayerState.STATE_PRONE:
          if (MirrorsEdge.TrialMode)
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_GETUP);
            break;
          }
          MathLine line = new MathLine(this.m_position, -1f * (float) this.getFacingDir(), 0.0f, 0.0f);
          line.origin.y += this.m_globalShape.getLocalBounds().max.y;
          GameObject gameObjectAt = this.m_map.getGameObjectAt(line, (GameObject) this);
          if (gameObjectAt != null && gameObjectAt.isFlagSet(4))
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_MELEE_GETUP);
            (gameObjectAt as GameObjectPolice).meleedFromPlayerJumpingFromProne();
            break;
          }
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_GETUP);
          break;
      }
    }

    private void applyGestureDown()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_SLIDING);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          this.m_rollGestureTimer = 500;
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLSLIDE);
          this.clearGesture(32);
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          if (this.intersectsMap())
            break;
          MathLine line = new MathLine(this.getForwardDirection(), 0.0f, 1f, 0.0f);
          line.origin *= 0.51f * (float) this.getFacingDir();
          line.origin += this.m_position;
          MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
          float y1 = localBounds.min.y;
          float y2 = localBounds.max.y;
          this.m_map.intersects(line, 0, ref y1, ref y2);
          if ((double) y1 < 0.0)
          {
            this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_HANG_TO_SLIDE);
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLSLIDE);
          }
          else
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
          this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.IGNORE_CONSTANT;
          this.clearGesture(32);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
          this.m_ignoreWallSlide = GameObjectPlayer.IgnoreCode.IGNORE_CONSTANT;
          this.clearGesture(32);
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          if ((double) this.m_swingingVelocity == 0.0)
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
          }
          else
          {
            this.setForwardVelocity((float) ((double) this.m_swingingVelocity * (double) this.getFacingDir() * 0.5));
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          }
          this.clearGesture(32);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
          if (!MirrorsEdge.TrialMode)
            AppEngine.getAchievementData().registerZiplineDrop();
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
      }
    }

    private void updateVelocity(int timeStepMillis)
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          this.setForwardVelocity(0.0f);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
        case GameObjectPlayer.PlayerState.STATE_ROLLING:
        case GameObjectPlayer.PlayerState.STATE_SLIDING:
          float num1 = (float) timeStepMillis * (1f / 1000f);
          float num2 = (float) this.getFacingDir() * this.getForwardVelocity();
          if ((double) num2 < (double) this.m_targetForwardVelocity)
          {
            num2 += num1 * this.m_targetForwardVelocityAcceleration;
            if ((double) this.m_targetForwardVelocity <= (double) num2)
              num2 = this.m_targetForwardVelocity;
          }
          else if ((double) this.m_targetForwardVelocity < (double) num2)
          {
            num2 -= num1 * this.m_targetForwardVelocityAcceleration;
            if ((double) num2 <= (double) this.m_targetForwardVelocity)
              num2 = this.m_targetForwardVelocity;
          }
          this.setForwardVelocity((float) this.getFacingDir() * num2);
          break;
        case GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING:
          float accelerometerTilt1 = this.getAccelerometerTilt(true);
          this.set3WayAnimationBlendWeights((float) this.getFacingDir() * -accelerometerTilt1);
          this.m_targetForwardVelocity = (float) (7.0 - 1.0 * (double) Math.Abs(accelerometerTilt1));
          float num3 = (float) timeStepMillis * (1f / 1000f);
          float num4 = (float) this.getFacingDir() * this.getForwardVelocity();
          if ((double) num4 < (double) this.m_targetForwardVelocity)
          {
            num4 += num3 * this.m_targetForwardVelocityAcceleration;
            if ((double) this.m_targetForwardVelocity <= (double) num4)
              num4 = this.m_targetForwardVelocity;
          }
          else if ((double) this.m_targetForwardVelocity < (double) num4)
          {
            num4 -= num3 * this.m_targetForwardVelocityAcceleration;
            if ((double) num4 <= (double) this.m_targetForwardVelocity)
              num4 = this.m_targetForwardVelocity;
          }
          this.setForwardVelocity((float) this.getFacingDir() * num4);
          break;
        case GameObjectPlayer.PlayerState.STATE_STOPPING:
          this.setForwardSpeed(this.m_velocityInterpolation.getCurrentValue());
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          this.setUpVelocity(4.7f);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
          this.setUpVelocity(1.5f);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.setUpVelocity(-5f);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          float accelerometerTilt2 = this.getAccelerometerTilt(true);
          this.set3WayAnimationBlendWeights((float) this.getFacingDir() * -accelerometerTilt2);
          this.m_lineMovementSpeed = (float) (12.0 - 2.0 * (double) Math.Abs(accelerometerTilt2));
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
          float accelerometerTilt3 = this.getAccelerometerTilt(true);
          this.set3WayAnimationBlendWeights((float) this.getFacingDir() * -accelerometerTilt3);
          this.m_lineMovementSpeed = (float) (13.0 - 2.0 * (double) Math.Abs(accelerometerTilt3));
          break;
      }
    }

    private void startPositionLinearInterpolation(
      MathVector start,
      MathVector end,
      int duration,
      bool clip)
    {
      this.m_positionInterpolation.start(start, end, duration, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
      this.m_positionInterpolationClip = clip;
    }

    private void startPositionInterpolation(
      MathVector start,
      MathVector end,
      int duration,
      InterpolationTimed.InterpolationType interpolationType,
      bool clip)
    {
      this.m_positionInterpolation.start(start, end, duration, interpolationType);
      this.m_positionInterpolationClip = clip;
    }

    private void updateMovement(int timeStepMillis)
    {
      if (this.m_animState != GameObjectPlayer.AnimationState.ANIM_STATE_NONE && !this.m_objectAnimPlayer.isAnimating())
      {
        if (this.m_animState == GameObjectPlayer.AnimationState.ANIM_STATE_POWER_JUMP_START)
          this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_POWER_JUMP_LOOP);
        else if (this.m_animState != GameObjectPlayer.AnimationState.ANIM_STATE_BOOST_JUMP)
          this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_NONE);
      }
      if (this.m_skipMovement)
      {
        this.m_skipMovement = false;
      }
      else
      {
        if (this.isInOriginAnimation())
          this.updateMovementOriginAnimation((int) this.getFacingDir(), timeStepMillis);
        else if (!this.m_positionInterpolation.isFinished())
          this.updateMovementLinearInterpolation(timeStepMillis);
        else if (this.m_lineMovementEnabled)
          this.updateLineMovement((float) timeStepMillis * (1f / 1000f));
        else if (this.m_noClip)
          this.updateMovementNoClip((float) timeStepMillis * (1f / 1000f));
        else
          this.updateMovementPhysics((float) timeStepMillis * (1f / 1000f));
        if (this.m_state == GameObjectPlayer.PlayerState.STATE_SWINGING)
          this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y + 1.6f, this.m_position.z);
        else
          this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      }
    }

    private void updateMovementLinearInterpolation(int timeStepMillis)
    {
      this.m_positionInterpolation.update(timeStepMillis);
      this.m_position = this.m_positionInterpolation.getCurrentValue();
      if (!this.m_positionInterpolationClip)
        return;
      this.m_globalShape.setPosition(this.m_position);
      if (!this.m_map.intersects(this.m_globalShape, 0))
        return;
      if ((double) this.m_frameStartPos.y < (double) this.m_position.y)
        this.hitHead();
      this.m_position = this.m_frameStartPos;
    }

    private void updateMovementPhysics(float timeStepSecs)
    {
      float forwardVelocity = this.getForwardVelocity();
      MathVector collisionNormal = new MathVector();
      CollShape collidedShape = (CollShape) null;
      int num = this.m_map.moveObjectPhysics(ref this.m_position, ref this.m_velocity, timeStepSecs, this.m_globalShape, ref collidedShape, ref collisionNormal);
      if (this.m_map.isInFallBox((GameObject) this))
        AppEngine.getCanvas().getSceneGame().stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING);
      if (this.m_map.isInPainBox((GameObject) this))
        this.hurt(6.9f * timeStepSecs, true);
      if ((num & 192) != 0)
      {
        if ((double) this.getForwardVelocity() * (double) this.getFacingDir() < 0.0)
          this.m_velocity.reverse();
        if ((double) this.getUpVelocity() < 0.0)
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE);
        else
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP);
      }
      else
      {
        if (!GameCommon.isZero(forwardVelocity) && GameCommon.isZero(this.getForwardVelocity()))
          this.hitWall();
        if ((num & 4) != 0)
        {
          this.groundPlayer();
          this.m_currentMaterial = collidedShape.m_materialId;
        }
        else if (!GameCommon.isZero(this.m_velocity.y))
          this.fallPlayer();
        if ((num & 8) == 0)
          return;
        this.hitHead();
      }
    }

    private void updateMovementNoClip(float timeStepSecs)
    {
      MathVector mathVector = new MathVector(this.m_velocity) * timeStepSecs;
      GameObjectPlayer gameObjectPlayer = this;
      gameObjectPlayer.m_position = gameObjectPlayer.m_position + mathVector;
    }

    private void updateAccelerometerNoise(int timeStepMillis)
    {
      this.m_accelerometerNoiseTimer -= timeStepMillis;
      if (this.m_accelerometerNoiseTimer < 0)
      {
        this.m_accelerometerNoiseFilter.setTargetValue(AppEngine.getCanvas().randFloat(-1f, 1f));
        this.m_accelerometerNoiseTimer = 2100;
      }
      this.m_accelerometerNoiseFilter.update(timeStepMillis);
    }

    private void clearInvalidGestures()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_BALANCE_FELL:
        case GameObjectPlayer.PlayerState.STATE_CLAMBER:
        case GameObjectPlayer.PlayerState.STATE_HANG_CLAMBER:
          this.clearAllGesturesExcept(128);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          this.clearAllGesturesExcept(32);
          break;
        case GameObjectPlayer.PlayerState.STATE_ROLLING:
        case GameObjectPlayer.PlayerState.STATE_SCALE_JUMP:
        case GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP:
          this.clearAllGesturesExcept(143);
          break;
        default:
          this.clearAllGestures();
          break;
      }
    }

    private void updateState(int timeStepMillis)
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          this.updateStateIdle(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
          this.updateStateRunning(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING_TURNING:
          this.updateStateRunningTurning(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING:
          this.updateStateBalanceRunning(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_BALANCE_FELL:
          this.updateStateBalanceFell(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_STOPPING:
          this.updateStateStopping(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
          this.updateStateFallingVertically(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          this.updateStateFallingForwards(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_ROLLING:
          this.updateStateRolling(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_CRASH:
          this.updateStateCrash(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          this.updateStateWallClimb(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_SLIDING:
          this.updateStateSliding(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
          this.updateStateWallRun(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.updateStateWallSlide(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          this.updateStateSwinging(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_SCALE_JUMP:
          this.updateStateScaleJump(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP:
          this.updateStateScaleHaulUp(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
          this.updateStateRampRunUp(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          this.updateStateRampSlide(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_JUMP_TO:
          this.updateStateZipLineJumpTo(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
          this.updateStateZipLine(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE_FAIL:
          this.updateStateZipLineFail(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateStateMeleedInTheFace(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_DISARMING_COP:
          if (MirrorsEdge.TrialMode)
            break;
          this.updateStateDisarmingCop(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_PRONE:
          if ((double) this.getUpVelocity() <= -18.0)
            AppEngine.getCanvas().getSceneGame().stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING);
          this.m_downedTime -= timeStepMillis;
          if (this.m_downedTime > 0)
            break;
          this.playerGetUpAggressive();
          break;
        case GameObjectPlayer.PlayerState.STATE_GETUP:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
          break;
        case GameObjectPlayer.PlayerState.STATE_MELEE_GETUP:
          if (this.m_objectAnimPlayer.isAnimating() || MirrorsEdge.TrialMode)
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
          break;
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          this.updateStateFlykick(timeStepMillis);
          break;
        case GameObjectPlayer.PlayerState.STATE_DYING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_DEAD);
          break;
      }
    }

    private void initLineMovement(MathVector velocity, bool clip)
    {
      this.m_lineMovementEnabled = true;
      this.m_lineMovementSpeed = velocity.getLength();
      this.m_lineMovementDirection = velocity;
      this.m_lineMovementDirection *= 1f / this.m_lineMovementSpeed;
      this.m_lineMovementClip = !clip ? GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_NONE : GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_SET;
      this.m_velocity = velocity;
    }

    private void initLineMovement(MathVector direction, float speed, bool clip)
    {
      this.m_lineMovementEnabled = true;
      this.m_lineMovementSpeed = speed;
      this.m_lineMovementDirection = direction;
      this.m_lineMovementDirection.normalise();
      this.m_lineMovementClip = !clip ? GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_NONE : GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_SET;
      this.m_velocity = this.m_lineMovementDirection;
      GameObjectPlayer gameObjectPlayer = this;
      gameObjectPlayer.m_velocity = gameObjectPlayer.m_velocity * speed;
    }

    private void updateLineMovement(float timeStepSecs)
    {
      MathVector mathVector1 = new MathVector(this.m_lineMovementDirection) * this.m_lineMovementSpeed;
      this.m_velocity = mathVector1;
      MathVector mathVector2 = mathVector1 * timeStepSecs;
      if (this.m_lineMovementClip == GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_NONE)
      {
        GameObjectPlayer gameObjectPlayer = this;
        gameObjectPlayer.m_position = gameObjectPlayer.m_position + mathVector2;
      }
      else
      {
        if (this.m_lineMovementClip != GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_SET || this.m_map.moveObject(ref this.m_position, ref this.m_velocity, timeStepSecs, this.m_globalShape) == 0)
          return;
        this.m_lineMovementClip = GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_CLIPPED;
      }
    }

    private void deinitLineMovement() => this.m_lineMovementEnabled = false;

    private void loadParticleEffectsForRampAndWallSlides()
    {
      if (this.m_rampSlideEffect1 != null)
        return;
      ParticleMode particleMode = new ParticleMode((AppearanceBase) AppEngine.getM3GAssets().loadTexturedAppearance((int) M3GAssets.get("TEX_EFFECT_PARTICLES_ALPHA_ADD"), 4));
      particleMode.setMeanTimeToLive(300f, 150f);
      KeyframeSequence sequence1 = new KeyframeSequence(3, 1, 176);
      float[] numArray1 = new float[3]{ 0.5f, 1f, 0.5f };
      sequence1.setKeyframe(0, 0, numArray1, 0);
      sequence1.setKeyframe(1, 100, numArray1, 1);
      sequence1.setKeyframe(2, 500, numArray1, 2);
      sequence1.setDuration(501);
      particleMode.setScale(sequence1);
      KeyframeSequence sequence2 = new KeyframeSequence(2, 1, 176);
      float[] numArray2 = new float[2];
      sequence2.setKeyframe(0, 0, numArray2, 0);
      sequence2.setKeyframe(1, 500, numArray2, 1);
      sequence2.setDuration(501);
      particleMode.setRotation(sequence2);
      KeyframeSequence sequence3 = new KeyframeSequence(1, 4, 180);
      float num = 1f / 256f;
      float[] numArray3 = new float[4]
      {
        0.0f * num,
        0.0f * num,
        64f * num,
        64f * num
      };
      sequence3.setKeyframe(0, 0, numArray3, 0);
      sequence3.setDuration(501);
      particleMode.setCrop(sequence3);
      KeyframeSequence sequence4 = new KeyframeSequence(2, 4, 176);
      float[] numArray4 = new float[8]
      {
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        0.0f
      };
      sequence4.setKeyframe(0, 0, numArray4, 0);
      sequence4.setKeyframe(1, 500, numArray4, 4);
      sequence4.setDuration(501);
      particleMode.setColor(sequence4);
      EmissionMode emissionMode = new EmissionMode();
      emissionMode.setRate(10f);
      emissionMode.setSpeed(1f, 1f);
      emissionMode.setSpreadAngle(10f, 10f);
      this.m_rampSlideEffect1 = new ParticleEffect(Emitter.createEmitter(1, 60, particleMode, emissionMode));
      this.m_rampSlideEffect2 = new ParticleEffect(Emitter.createEmitter(1, 60, particleMode, emissionMode));
      this.m_wallSlideEffectHand = new ParticleEffect(Emitter.createEmitter(1, 60, particleMode, emissionMode));
      this.m_wallSlideEffectFoot = new ParticleEffect(Emitter.createEmitter(1, 60, particleMode, emissionMode));
    }

    private void updateParticleEffectForRampAndWallSlides(
      int timeStepMillis,
      float rate,
      float rateFactor,
      ParticleEffect effect,
      Node locator,
      float offsetX,
      float offsetY)
    {
      if (effect == null)
        return;
      World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
      locator.getTransformTo((Node) m3Gworld, GameObjectPlayer.temp);
      GameObjectPlayer.position[0] = GameObjectPlayer.position[1] = GameObjectPlayer.position[2] = 0.0f;
      GameObjectPlayer.position[3] = 1f;
      GameObjectPlayer.temp.transform(GameObjectPlayer.position, 4);
      effect.setTranslation(GameObjectPlayer.position[0] + offsetX, GameObjectPlayer.position[1] + offsetY, GameObjectPlayer.position[2] + 0.1f);
      int emitterCount = effect.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
      {
        EmissionMode emissionMode = effect.getEmitter(index).getEmissionMode();
        emissionMode.setRate(rate);
        emissionMode.setSpeed(1f * rateFactor, 0.5f * rateFactor);
      }
      effect.updateStep(timeStepMillis, (Transform) null, (Transform) null);
    }

    private void hitWall()
    {
      if (!MirrorsEdge.TrialMode)
        AppEngine.getAchievementData().registerMapCollision();
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          if ((double) this.getUpVelocity() <= -18.0)
            break;
          this.checkWallAction(this.getFacingDir(), false, 30);
          break;
        default:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
          break;
      }
    }

    private void groundPlayer()
    {
      if (!MirrorsEdge.TrialMode)
        AppEngine.getAchievementData().registerMapCollision();
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          if (0 < this.m_rollGestureTimer)
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_ROLLING);
            break;
          }
          if (-(double) this.getPrevUpVelocity() > 8.0)
          {
            if (!MirrorsEdge.TrialMode)
              this.m_achievementData.registerBadLanding();
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_CRASH);
            break;
          }
          this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_LANDING);
          this.stateTransition(this.m_state == GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY ? GameObjectPlayer.PlayerState.STATE_IDLE : GameObjectPlayer.PlayerState.STATE_RUNNING);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_CRASHING:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_CRASH);
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
          break;
        case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
        case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
          break;
      }
    }

    private void fallPlayer()
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_IDLE:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
          break;
        case GameObjectPlayer.PlayerState.STATE_RUNNING:
        case GameObjectPlayer.PlayerState.STATE_SLIDING:
          CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(new MathLine(this.m_position.x, this.m_position.y, this.m_position.z, 0.0f, -1f, 0.0f), 0);
          if (attributeShapeAt == null)
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
            break;
          }
          float y1 = this.m_position.y;
          float y2 = attributeShapeAt.getBounds().max.y;
          GameObjectRunner.FacingDir facingDir = this.getFacingDir();
          int shapeType = (int) attributeShapeAt.getShapeType();
          bool flag1 = (double) y1 - 0.5 <= (double) y2 && (double) y2 <= (double) y1 + 0.5;
          bool flag2 = facingDir == GameObjectRunner.FacingDir.FACING_LEFT && shapeType == 2 || facingDir == GameObjectRunner.FacingDir.FACING_RIGHT && shapeType == 1;
          if (attributeShapeAt.isRamp() && flag1 && flag2)
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE);
            break;
          }
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          if (this.m_objectAnimPlayer.getAnimID() != (int) ResourceManager.get("ANIM3D_FAITH_JUMP_RISING") || (double) this.getUpVelocity() > 0.0)
            break;
          this.refreshAnimation();
          break;
        case GameObjectPlayer.PlayerState.STATE_FALLING_CRASHING:
          break;
        case GameObjectPlayer.PlayerState.STATE_CRASH:
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLCLIMB:
          break;
        case GameObjectPlayer.PlayerState.STATE_CLAMBER:
          break;
        case GameObjectPlayer.PlayerState.STATE_HANG:
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLRUN:
          break;
        case GameObjectPlayer.PlayerState.STATE_WALLSLIDE:
          break;
        case GameObjectPlayer.PlayerState.STATE_SWINGING:
          break;
        case GameObjectPlayer.PlayerState.STATE_ZIP_LINE:
          break;
        case GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE:
          break;
        case GameObjectPlayer.PlayerState.STATE_PRONE:
          break;
        case GameObjectPlayer.PlayerState.STATE_FLYKICK:
          break;
        default:
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
          break;
      }
    }

    private void hitHead()
    {
      if (this.m_state != GameObjectPlayer.PlayerState.STATE_WALLCLIMB)
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
    }

    public void meleeByCop(GameObjectNPC from)
    {
      switch (this.m_state)
      {
        case GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY:
        case GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS:
          if ((double) this.m_position.y >= (double) from.m_position.y + 1.2000000476837158)
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE);
          break;
        case GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP:
          break;
        case GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE:
          break;
        default:
          if (this.m_state == GameObjectPlayer.PlayerState.STATE_DYING || this.m_state == GameObjectPlayer.PlayerState.STATE_DEAD)
            break;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_MELEED_IN_THE_FACE);
          break;
      }
    }

    public void disarmCop(GameObjectNPC cop)
    {
      this.m_disarmTime = 0;
      this.m_disarmCooldown = 0;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_DISARMING_COP);
    }

    public void kickSomething()
    {
      this.m_map.addObject((GameObject) new GameObjectEffectHit(this.m_map, this.m_faithLeftFootLocator));
    }

    private void playerJump(bool clamberBoostJump) => this.playerJump(clamberBoostJump, true);

    private void playerJump(bool clamberBoostJump, bool snap)
    {
      GameObjectPlayer.PlayerState state = this.m_state;
      this.stateTransition(this.m_state == GameObjectPlayer.PlayerState.STATE_IDLE ? GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY : GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
      if (clamberBoostJump)
      {
        this.setForwardSpeed(Math.Max(this.getForwardSpeed(), 5.5f));
        this.setUpVelocity(8.15f);
        this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_BOOST_JUMP);
        this.clearGesture(128);
      }
      else if (this.m_map.intersects(this.m_position, 3))
      {
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
        this.setForwardSpeed(12f);
        this.setUpVelocity(7f);
        this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_POWER_JUMP_START);
      }
      else
      {
        switch (state)
        {
          case GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP:
            this.setForwardVelocity((float) this.getFacingDir() * 6.5f);
            this.setUpVelocity(6.5f);
            break;
          case GameObjectPlayer.PlayerState.STATE_RAMP_SLIDE:
            this.setForwardVelocity((float) this.getFacingDir() * 10f);
            this.setUpVelocity(5f);
            break;
          default:
            this.setUpVelocity(5f);
            break;
        }
      }
      this.clearGesture(64);
      this.m_risingStateIsFromJump = snap;
      SoundSequencer soundSequencer = AppEngine.getCanvas().getSceneGame().getSoundSequencer();
      if (clamberBoostJump)
        soundSequencer.playSequence(3);
      else
        soundSequencer.playSequence(2);
    }

    private void playerZipLine(GameObjectZipLine zipLine)
    {
      this.m_attachedZipLine = zipLine;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_ZIP_LINE);
    }

    private void playerSwingLaunch()
    {
      if ((GameObjectRunner.FacingDir) this.m_swingingDirection != this.getFacingDir())
        return;
      this.m_position.x += 0.640000045f * (float) Math.Sin((double) this.m_swingingAngleDeg * 3.1415927410125732 / 180.0);
      this.setForwardVelocity(7.5f * (float) this.getFacingDir());
      this.setUpVelocity(5f);
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
      AppEngine.getCanvas().getSceneGame().getSoundSequencer().playSequence(5);
    }

    private void playerHalfClamber(GameObjectRunner.FacingDir checkSide, CollShape ledgeShape)
    {
      if (this.m_state == GameObjectPlayer.PlayerState.STATE_WALLCLIMB || this.m_state == GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS)
      {
        if (checkSide == GameObjectRunner.FacingDir.FACING_RIGHT && ledgeShape.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_NEG_X || checkSide == GameObjectRunner.FacingDir.FACING_LEFT && ledgeShape.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X)
          return;
        this.playerClimbLedgeAfterCheck(checkSide, 0, -1, 7f);
      }
      else
        this.playerClimbLedgeAfterCheck(checkSide, 1, -1, 5f);
    }

    private void playerFullClamber(GameObjectRunner.FacingDir checkSide, CollShape ledgeShape)
    {
      if ((double) this.getUpVelocity() < 0.0)
        this.playerClimbLedgeAfterCheck(checkSide, 4, -1, 5f);
      else if (this.m_state == GameObjectPlayer.PlayerState.STATE_WALLCLIMB || this.m_state == GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS)
        this.playerClimbLedgeAfterCheck(checkSide, 2, -1, 7f);
      else
        this.playerClimbLedgeAfterCheck(checkSide, 3, -1, 5f);
    }

    private void playerClimbLedgeAfterCheck(
      GameObjectRunner.FacingDir checkSide,
      int clamberOriginVisualStart,
      int clamberOriginVisualEnd,
      float exitSpeed)
    {
      this.m_clamberOriginVisualStart = clamberOriginVisualStart;
      this.m_clamberOriginVisualEnd = clamberOriginVisualEnd;
      this.m_clamberExitSpeed = exitSpeed;
      if (checkSide != this.getFacingDir())
        this.reverseFacingDir();
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_CLAMBER);
    }

    private void playerGetUpAggressive()
    {
      MathLine line = new MathLine(this.m_position, -1f * (float) this.getFacingDir(), 0.0f, 0.0f);
      line.origin.y += this.m_globalShape.getLocalBounds().max.y;
      GameObject gameObjectAt = this.m_map.getGameObjectAt(line, (GameObject) this);
      if (gameObjectAt != null && gameObjectAt.isFlagSet(4))
      {
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_MELEE_GETUP);
        (gameObjectAt as GameObjectPolice).meleedFromPlayerJumpingFromProne();
      }
      else
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_GETUP);
    }

    private bool checkWallAction(
      GameObjectRunner.FacingDir checkSide,
      bool checkMoveForward,
      int wallActionChecks)
    {
      MathLine mathLine = new MathLine(new MathVector(this.getForwardDirection()) * (0.51f * (float) checkSide) + this.m_position, 0.0f, 1f, 0.0f);
      MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
      float y1 = localBounds.min.y;
      float y2 = localBounds.max.y;
      if (this.m_map.intersects(mathLine, 0, ref y1, ref y2))
      {
        if ((wallActionChecks & 1) != 0)
        {
          if (GameCommon.compareFloats(y2, 0.5f) && (double) this.getUpVelocity() >= 0.0)
          {
            MathLine line = new MathLine(mathLine);
            line.origin.y += y2 + 0.01f;
            line.direction.y = -1f;
            CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(line, 0);
            this.playerHalfClamber(checkSide, attributeShapeAt);
            return true;
          }
          if (GameCommon.compareFloats(y2, 1f))
          {
            MathLine line = new MathLine(mathLine);
            line.origin.y += y2 + 0.01f;
            line.direction.y = -1f;
            CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(line, 0);
            this.playerFullClamber(checkSide, attributeShapeAt);
            return true;
          }
        }
        if ((wallActionChecks & 2) != 0 && 0.0 < (double) y2 && (double) y2 <= 0.5)
        {
          MathLine line = new MathLine(mathLine);
          line.origin.y += y2 + 0.01f;
          line.direction.y = -1f;
          CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(line, 0);
          this.playerHalfClamber(checkSide, attributeShapeAt);
          return true;
        }
        if ((wallActionChecks & 4) != 0)
        {
          float num1 = y2;
          float num2 = num1 - (this.m_position.y - this.m_frameStartPos.y);
          if ((double) num2 <= 0.5 && 0.5 <= (double) num1)
          {
            MathLine line = new MathLine(mathLine);
            line.origin.y += num1 + 0.01f;
            line.direction.y = -1f;
            CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(line, 0);
            this.playerHalfClamber(checkSide, attributeShapeAt);
            return true;
          }
          if ((double) num2 <= 1.0 != (double) num1 <= 1.0)
          {
            MathLine line = new MathLine(mathLine);
            line.origin.y += num1 + 0.01f;
            line.direction.y = -1f;
            CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(line, 0);
            this.playerFullClamber(checkSide, attributeShapeAt);
            return true;
          }
        }
        if ((wallActionChecks & 8) != 0)
        {
          float num3 = y2;
          float num4 = num3 - (this.m_position.y - this.m_frameStartPos.y);
          if ((double) this.getUpVelocity() < 0.0)
          {
            bool flag1 = 1.0 <= (double) num3 && (double) num3 <= 1.8999999761581421;
            bool flag2 = (double) num4 <= 1.0 && 1.0 <= (double) num3;
            if (flag1 || flag2)
            {
              MathLine line = new MathLine(this.m_position, 0.0f, -1f, 0.0f);
              float minT = 0.0f;
              float maxT = 2.3f;
              if (!this.m_map.intersects(line, 0, ref minT, ref maxT))
              {
                this.startPositionInterpolation(this.m_position, new MathVector(this.m_position.x, this.m_position.y + (y2 - 2f), this.m_position.z), 100, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR, false);
                this.stateTransition(GameObjectPlayer.PlayerState.STATE_HANG);
                return true;
              }
            }
          }
        }
        if ((wallActionChecks & 16) != 0)
        {
          if (0.5 < (double) y2 && 1.0 <= (double) y2 - (double) y1 && 5.0 < (double) this.m_prevVelocity.x * (double) this.getFacingDir() && 0.0 < (double) this.getUpVelocity())
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLCLIMB);
            return true;
          }
          if ((double) y1 < (double) localBounds.min.y && (double) localBounds.max.y <= (double) y2 && (double) this.getUpVelocity() <= 0.0)
          {
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLSLIDE);
            return true;
          }
          if (this.m_state != GameObjectPlayer.PlayerState.STATE_WALLSLIDE || (double) y1 >= (double) localBounds.min.y)
            this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
        }
      }
      else if (checkMoveForward)
      {
        MathVector mathVector = new MathVector(this.getForwardDirection()) * 0.1f;
        GameObjectPlayer gameObjectPlayer = this;
        gameObjectPlayer.m_position = gameObjectPlayer.m_position + mathVector;
        this.setUpVelocity(0.0f);
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
      }
      return false;
    }

    private bool checkJumpToZipLine()
    {
      MathVector mathVector1 = this.getForwardDirection() * (2f * (float) this.getFacingDir());
      MathVector mathVector2 = this.getRightDirection() * 0.5f;
      MathOrthoBox mathOrthoBox = new MathOrthoBox(-mathVector2.x, 0.0f, -mathVector2.z, mathVector1.x + mathVector2.x, 2.5f, mathVector1.z + mathVector2.z) + this.m_position;
      int playerCheckObjects = this.m_map.getNumPlayerCheckObjects();
      for (int index = 0; index != playerCheckObjects; ++index)
      {
        GameObject playerCheckObject = this.m_map.getPlayerCheckObject(index);
        if (playerCheckObject.getType() == 16)
        {
          GameObjectZipLine gameObjectZipLine = (GameObjectZipLine) playerCheckObject;
          MathLine zipLineLine = gameObjectZipLine.getZipLineLine();
          if (0.0 < (double) zipLineLine.direction.x == (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_RIGHT))
          {
            float minTValue = 0.0f;
            float maxTValue = 0.0f;
            if (mathOrthoBox.intersects(zipLineLine, out minTValue, out maxTValue) && GameCommon.boundsIntersect(0.0f, 0.9f, minTValue, maxTValue))
            {
              this.m_attachedZipLine = gameObjectZipLine;
              zipLineLine.calculatePointAtT(Math.Max(0.0f, minTValue), ref this.m_zipLineJumpDestination);
              this.m_zipLineJumpDestination.y -= 1.59f;
              this.m_velocity.set(this.m_zipLineJumpDestination.x - this.m_position.x, this.m_zipLineJumpDestination.y - this.m_position.y, this.m_zipLineJumpDestination.z - this.m_position.z);
              this.m_velocity.setLength(13f);
              this.stateTransition(GameObjectPlayer.PlayerState.STATE_ZIP_LINE_JUMP_TO);
              return true;
            }
          }
        }
      }
      return false;
    }

    private bool checkLedgeScale()
    {
      MathVector mathVector1 = new MathVector(this.getForwardDirection()) * (this.getForwardVelocity() * 0.1f);
      MathLine line1 = new MathLine(this.m_position.x, this.m_position.y + this.m_globalShape.getLocalBounds().max.y, this.m_position.z, 0.0f, 1f, 0.0f);
      line1.origin += mathVector1;
      float minT1 = 0.0f;
      float maxT1 = 1.5f;
      if (this.m_map.intersects(line1, 0, ref minT1, ref maxT1) && 0.0 < (double) minT1 && 0.20000000298023224 <= (double) maxT1 && (double) maxT1 <= 1.5)
      {
        if (GameCommon.isZero(this.getForwardVelocity()))
        {
          this.m_ledgeScaleHaulAnim = this.getFacingDir() != GameObjectRunner.FacingDir.FACING_LEFT ? 7 : 6;
          this.m_ledgeScalePostAnimState = GameObjectPlayer.AnimationState.ANIM_STATE_NONE;
          this.m_ledgeScalePostState = GameObjectPlayer.PlayerState.STATE_IDLE;
        }
        else
        {
          if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
          {
            this.m_ledgeScaleHaulAnim = 8;
            this.m_ledgeScalePostAnimState = GameObjectPlayer.AnimationState.ANIM_STATE_SCALE_HALF_STEP;
          }
          else
          {
            this.m_ledgeScaleHaulAnim = 9;
            this.m_ledgeScalePostAnimState = GameObjectPlayer.AnimationState.ANIM_STATE_NONE;
          }
          this.m_ledgeScalePostState = GameObjectPlayer.PlayerState.STATE_RUNNING;
        }
        MathVector mathVector2 = (new MathVector(this.m_position) + mathVector1) with
        {
          y = line1.origin.y + maxT1
        };
        this.m_globalShape.setPosition(mathVector2);
        if (!this.m_map.intersects(this.m_globalShape, 0))
        {
          MathVector jumpPoint = new MathVector();
          MathVector scalePoint = new MathVector();
          this.checkLedgeScale_CalculateJumpAndScale(mathVector2, ref jumpPoint, ref scalePoint);
          this.m_globalShape.setPosition(scalePoint);
          if (!this.m_map.intersects(this.m_globalShape, 0))
          {
            CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(new MathLine(scalePoint, 0.0f, -1f, 0.0f), 0);
            if (attributeShapeAt != null && GameCommon.compareFloats(scalePoint.y, attributeShapeAt.getBounds().max.y))
            {
              MathLine line2 = new MathLine(this.m_position.x, scalePoint.y + this.m_globalShape.getLocalBounds().max.y * 0.5f, 0.0f, scalePoint.x - this.m_position.x, 0.0f, 0.0f);
              MathLine line3 = new MathLine(this.m_position.x, this.m_position.y + this.m_globalShape.getLocalBounds().max.y * 0.5f, 0.0f, scalePoint.x - this.m_position.x, 0.0f, 0.0f);
              float minT2 = 0.0f;
              float maxT2 = 1f;
              float minT3 = 0.0f;
              float maxT3 = 1f;
              if (!this.m_map.intersects(line2, 0, ref minT2, ref maxT2) && !this.m_map.intersects(line3, 0, ref minT3, ref maxT3))
              {
                this.startPositionLinearInterpolation(this.m_position, jumpPoint, 100, false);
                this.stateTransition(GameObjectPlayer.PlayerState.STATE_SCALE_JUMP);
                return true;
              }
            }
          }
        }
      }
      return false;
    }

    private void checkLedgeScale_CalculateJumpAndScale(
      MathVector intersectPoint,
      ref MathVector jumpPoint,
      ref MathVector scalePoint)
    {
      MathVector offset = new MathVector();
      this.calculateOffsetAnimationOverallOffset(this.m_ledgeScaleHaulAnim, ref offset);
      jumpPoint.set(intersectPoint.x, intersectPoint.y - offset.y, intersectPoint.z + this.getRightDirection().z);
      scalePoint.set(intersectPoint.x + offset.x, intersectPoint.y, intersectPoint.z);
    }

    private bool checkWallrun()
    {
      CollShape colShape = (CollShape) null;
      if (this.m_map.intersects(this.m_position, 1, ref colShape))
      {
        this.m_currentMaterial = colShape.m_materialId;
        return true;
      }
      MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
      if (!this.m_map.intersects(new MathVector(this.m_position.x, (float) ((double) this.m_position.y + (double) localBounds.min.y + (double) localBounds.max.y * 0.5), this.m_position.z), 1, ref colShape))
        return false;
      this.m_currentMaterial = colShape.m_materialId;
      return true;
    }

    private void initStateIdle(GameObjectPlayer.PlayerState oldState)
    {
      this.setForwardVelocity(0.0f);
      MathVector mathVector = new MathVector(this.m_position.x, this.m_position.y + 2f, this.m_position.z);
    }

    private void updateStateIdle(int timeStepMillis)
    {
    }

    private void deinitStateIdle()
    {
    }

    private void initStateRunning(GameObjectPlayer.PlayerState oldState)
    {
      float val1 = this.getForwardVelocity() * (float) this.getFacingDir();
      if (7.0 <= (double) val1)
      {
        this.m_targetForwardVelocity = 10f;
        this.m_targetForwardVelocityAcceleration = 3f;
      }
      else
      {
        this.m_targetForwardVelocity = 7f;
        this.m_targetForwardVelocityAcceleration = 3f;
        this.m_targetForwardVelocityAccelerationPause = -1;
        this.setForwardVelocity((float) this.getFacingDir() * Math.Max(val1, 5f));
      }
    }

    private void updateStateRunning(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerRunDist(Math.Abs(this.m_frameStartPos.x - this.m_position.x));
      if (this.m_map.intersects(this.m_position, 2))
      {
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING);
      }
      else
      {
        float num = this.getForwardVelocity() * (float) this.getFacingDir();
        if ((double) num == 7.0)
        {
          if (this.m_targetForwardVelocityAccelerationPause == -1)
          {
            this.m_targetForwardVelocityAccelerationPause = 500;
          }
          else
          {
            this.m_targetForwardVelocityAccelerationPause -= timeStepMillis;
            if (this.m_targetForwardVelocityAccelerationPause < 0)
              this.m_targetForwardVelocity = 10f;
          }
        }
        else if ((double) num == 10.0 && !MirrorsEdge.TrialMode)
          AppEngine.getAchievementData().registerSprintOn(AppEngine.getCanvas().getSceneGame().getRaceTime());
        this.updateAnimationStateRunning();
      }
    }

    private void updateAnimationStateRunning()
    {
      if (this.m_animState != GameObjectPlayer.AnimationState.ANIM_STATE_NONE || this.m_visualIndex == (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING_END") || this.m_visualIndex == (int) GameObjectRunner.get("VISUAL_SOLO_ROLLING"))
        return;
      float num = this.getForwardVelocity() * (float) this.getFacingDir();
      if ((double) num < 6.0)
        this.set3WayAnimationBlendWeights((float) (((double) num - 6.0) / 1.0));
      else
        this.set3WayAnimationBlendWeights((float) (((double) num - 6.0) / 4.0));
    }

    private void deinitStateRunning()
    {
      this.m_disarmTime = 0;
      if (!MirrorsEdge.TrialMode)
        AppEngine.getAchievementData().registerSprintOff();
      if (this.m_animState != GameObjectPlayer.AnimationState.ANIM_STATE_SCALE_HALF_STEP)
        return;
      this.m_animState = GameObjectPlayer.AnimationState.ANIM_STATE_NONE;
    }

    private void initStateRunningTurning(GameObjectPlayer.PlayerState oldState)
    {
      this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_RUNNING_TURNING_START"));
      this.m_targetForwardVelocity = this.getForwardVelocity();
    }

    private void updateStateRunningTurning(int timeStepMillis)
    {
      if (this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_RUNNING_TURNING_START"))
      {
        if (this.isAnimating())
        {
          this.setForwardVelocity(this.m_targetForwardVelocity * (1f - this.calcAnimProgress()));
        }
        else
        {
          this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_RUNNING_TURNING_END"));
          this.reverseFacingDir();
        }
      }
      else if (this.isAnimating())
        this.setForwardVelocity((float) this.getFacingDir() * 7f * this.calcAnimProgress());
      else
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
    }

    private void initStateBalanceRunning(GameObjectPlayer.PlayerState oldState)
    {
      this.m_targetForwardVelocityAcceleration = 10f;
      this.m_accelerometerNoiseFilter.setSteadyState(0.0f);
    }

    private void updateStateBalanceRunning(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerBalanceDist(Math.Abs(this.m_frameStartPos.x - this.m_position.x));
      if (!this.m_map.intersects(this.m_position, 2))
      {
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
      }
      else
      {
        this.updateAccelerometerNoise(timeStepMillis);
        if (0.99000000953674316 >= (double) Math.Abs(this.getAccelerometerTilt(true)) || 500 > this.m_stateTime)
          return;
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_BALANCE_FELL);
      }
    }

    private void initStateBalanceFell(GameObjectPlayer.PlayerState oldState)
    {
      this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_BALANCE_FALL"));
      this.setNoVelocity();
      this.m_noClip = true;
      this.clearGesture(128);
    }

    private void updateStateBalanceFell(int timeStepMillis)
    {
      if (this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_BALANCE_FALL"))
      {
        if (this.isAnimating())
          return;
        this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_BALANCE_FALLEN"));
      }
      else if (this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_BALANCE_FALLEN"))
      {
        if (!this.isAnyGestureSet(128))
          return;
        this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_BALANCE_RECLIMB"));
      }
      else
      {
        if (this.getVisualIndex() != (int) GameObjectRunner.get("VISUAL_SOLO_BALANCE_RECLIMB") || this.isAnimating())
          return;
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_BALANCE_RUNNING);
      }
    }

    private void deinitStateBalanceFell() => this.m_noClip = false;

    private void initStateStopping(GameObjectPlayer.PlayerState oldState)
    {
      int animationDuration = AppEngine.getCanvas().getAnimationManager3D().getAnimationDuration((int) ResourceManager.get("ANIM3D_FAITH_RUN_STOP_SLIDE"));
      this.m_velocityInterpolation.start(this.getForwardSpeed(), 0.0f, animationDuration, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR);
    }

    private void updateStateStopping(int timeStepMillis)
    {
      this.m_velocityInterpolation.update(timeStepMillis);
      if (!this.m_velocityInterpolation.isFinished())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
    }

    private void initStateFalling(GameObjectPlayer.PlayerState oldState)
    {
      this.m_rollGestureTimer = 0;
      this.m_risingStateIsFromJump = false;
    }

    private void initStateFallingVertically(GameObjectPlayer.PlayerState oldState)
    {
      this.setForwardVelocity(0.0f);
      this.m_ignoreWallSlide = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
    }

    private void updateStateFallingVertically(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
      {
        float dist = this.m_frameStartPos.y - this.m_position.y;
        if (0.0 < (double) dist)
          this.m_achievementData.registerFallDist(dist);
      }
      if ((double) this.getUpVelocity() <= -18.0)
        AppEngine.getCanvas().getSceneGame().stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING);
      int wallActionChecks = 0;
      if (this.m_ignoreClimbUp == GameObjectPlayer.IgnoreCode.DONT_IGNORE)
      {
        wallActionChecks |= 6;
        if ((double) this.getUpVelocity() < 0.0)
          wallActionChecks |= 8;
      }
      if (this.m_ignoreWallSlide == GameObjectPlayer.IgnoreCode.DONT_IGNORE)
        wallActionChecks |= 16;
      if (wallActionChecks != 0)
        this.checkWallAction(this.getFacingDir(), false, wallActionChecks);
      if (0 < this.m_rollGestureTimer)
        this.m_rollGestureTimer -= timeStepMillis;
      if (this.m_swingImmunity == 0)
        return;
      --this.m_swingImmunity;
    }

    private void deinitStateFallingVertically()
    {
      this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
      this.m_ignoreWallSlide = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
    }

    private void updateStateFallingForwards(int timeStepMillis)
    {
      SceneGame sceneGame = AppEngine.getCanvas().getSceneGame();
      if (!MirrorsEdge.TrialMode)
      {
        float dist = this.m_frameStartPos.y - this.m_position.y;
        if (0.0 < (double) dist)
          this.m_achievementData.registerFallDist(dist);
      }
      if ((double) this.getUpVelocity() <= -18.0)
        sceneGame.stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING);
      this.checkWallAction(this.getFacingDir(), false, 4);
      if (0 < this.m_rollGestureTimer)
        this.m_rollGestureTimer -= timeStepMillis;
      if (this.m_swingImmunity == 0)
        return;
      --this.m_swingImmunity;
      if (this.m_swingImmunity != 0)
        return;
      this.m_swingingObject = (GameObject) null;
    }

    private void deinitStateFallingForwards()
    {
      this.animStateTransition(GameObjectPlayer.AnimationState.ANIM_STATE_NONE);
    }

    private void initStateRolling(GameObjectPlayer.PlayerState oldState)
    {
      this.m_targetForwardVelocity = Math.Max(8f, (float) this.getFacingDir() * this.getForwardVelocity());
      this.m_targetForwardVelocityAcceleration = 60f;
      MathVector position = new MathVector(this.m_position);
      position.x += (float) ((double) this.getFacingDir() * (double) this.m_targetForwardVelocity * ((double) this.getVisualDuration(0, (int) GameObjectRunner.get("VISUAL_SOLO_ROLLING")) * (1.0 / 1000.0)));
      this.m_standingShape.setPosition(position);
      this.m_crouchingShape.setPosition(position);
      this.m_slideAfterRoll = this.m_map.intersects(this.m_standingShape, 0) && !this.m_map.intersects(this.m_crouchingShape, 0);
      this.m_crouchingShape.setPosition(this.m_position);
    }

    private void updateStateRolling(int timeStepMillis)
    {
      if (this.m_slideAfterRoll)
      {
        if (300 >= this.m_objectAnimPlayer.getAnimTime())
          return;
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_SLIDING);
      }
      else if (!this.m_objectAnimPlayer.isAnimating())
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
      else if (600 < this.m_objectAnimPlayer.getAnimTime() && this.isAnyGestureSet(128))
      {
        this.snapPlayerToGround();
        this.playerJump(false, false);
        this.clearGesture(128);
      }
      else
      {
        if (600 >= this.m_objectAnimPlayer.getAnimTime() || !this.isAnyGestureSet(15) || this.m_disarmCooldown > 0)
          return;
        this.m_disarmTime = 1000;
        this.m_disarmCooldown = 2000;
      }
    }

    private void initStateCrash(GameObjectPlayer.PlayerState oldState)
    {
      SceneGame sceneGame = AppEngine.getCanvas().getSceneGame();
      this.m_ignoreGestures = true;
      this.setForwardVelocity(0.0f);
      sceneGame.startCameraShake(0.5f, 300);
      sceneGame.playerHurt(true);
    }

    private void updateStateCrash(int timeStepMillis)
    {
      if (this.m_objectAnimPlayer.isAnimating())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
    }

    private void deinitStateCrash() => this.m_ignoreGestures = false;

    private void initStateWallClimb(GameObjectPlayer.PlayerState oldState)
    {
      int animationDuration = AppEngine.getCanvas().getAnimationManager3D().getAnimationDuration(AppEngine.getGameObjectRunnerData().getSoloAnim((int) GameObjectRunner.get("VISUAL_SOLO_WALLCLIMB")).animId);
      MathVector start = new MathVector(this.m_position);
      MathVector end = new MathVector(this.m_position);
      end.y += 3.5f;
      this.startPositionInterpolation(start, end, animationDuration, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_AHEAD, true);
    }

    private void updateStateWallClimb(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerClimbDist(Math.Abs(this.m_frameStartPos.y - this.m_position.y));
      if (this.m_positionInterpolation.isFinished())
      {
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_VERTICALLY);
        this.setUpVelocity(0.0f);
      }
      else
        this.checkWallAction(this.getFacingDir(), true, 4);
    }

    private void deinitStateWallClimb()
    {
      this.m_positionInterpolation.stop();
      this.clearGesture(128);
    }

    private void initStateClamber(GameObjectPlayer.PlayerState oldState)
    {
      this.clearGesture(128);
      MathVector offset = new MathVector();
      this.calculateOffsetAnimationOverallOffset(this.m_clamberOriginVisualStart, ref offset);
      MathLine line = new MathLine(this.m_position.x + offset.x, this.m_position.y, this.m_position.z, 0.0f, 1f, 0.0f);
      float animationYoffset = this.getOriginAnimationYOffset(this.m_clamberOriginVisualStart);
      this.m_clamberYOffset = animationYoffset * 2f;
      float minT = 0.0f;
      if (this.m_map.intersects(line, 0, ref minT, ref this.m_clamberYOffset))
        return;
      this.m_clamberYOffset = animationYoffset;
    }

    private void animStoppedStateClamber()
    {
      if ((this.getVisualType() != 1 || this.getVisualIndex() != this.m_clamberOriginVisualEnd) && this.isAnyGestureSet(128))
      {
        this.snapPlayerToGround();
        this.playerJump(true);
        this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.IGNORE_ONE_FRAME;
      }
      else
      {
        if (this.m_clamberOriginVisualEnd != -1 && this.getVisualType() == 1 && this.getVisualIndex() == this.m_clamberOriginVisualStart)
          return;
        MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
        float originY = this.m_position.y + localBounds.max.y;
        MathLine line = new MathLine(this.m_position.x, originY, this.m_position.z, 0.0f, -1f, 0.0f);
        float minT = 0.0f;
        float maxT = localBounds.max.y - localBounds.min.y;
        if (this.m_map.intersects(line, 0, ref minT, ref maxT))
          this.m_position.y = originY - minT;
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
      }
    }

    private void deinitStateClamber()
    {
      this.m_globalShape.setPosition(this.m_position);
      if (this.m_map.intersects(this.m_globalShape, 0))
      {
        MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
        MathLine line = new MathLine(this.m_position.x, this.m_position.y + 0.5f * localBounds.max.y, this.m_position.z, -1f, 0.0f, 0.0f);
        float minT = 0.0f;
        float maxT = -localBounds.min.x;
        if (this.m_map.intersects(line, 0, ref minT, ref maxT))
        {
          this.m_position.x += -localBounds.min.x - minT;
        }
        else
        {
          line.direction.x = 1f;
          float x = localBounds.max.x;
          if (this.m_map.intersects(line, 0, ref minT, ref x))
            this.m_position.x -= localBounds.max.x - minT;
        }
      }
      MathVector mathVector = (new MathVector(this.m_position) - this.getPreOriginAnimPosNoOffset()) with
      {
        y = 0.0f
      };
      if (this.m_originAnimPlayer.isAnimating())
      {
        this.m_originAnimPlayer.setAnimating(false);
      }
      else
      {
        float num = mathVector.getLength() / ((float) this.m_originAnimPlayer.getAnimDuration() * (1f / 1000f));
        if (!this.isAnyGestureSet(128))
          num = Math.Max(this.m_clamberExitSpeed, num);
        this.m_velocity = mathVector;
        if (!this.m_velocity.isZero())
          this.m_velocity.setLength(num);
      }
      this.m_clamberOriginVisualStart = -1;
      this.m_clamberOriginVisualEnd = -1;
      this.m_postClamberBoostJumpTime = 300;
    }

    private void initStateHang(GameObjectPlayer.PlayerState oldState)
    {
      this.setNoVelocity();
      if (oldState != GameObjectPlayer.PlayerState.STATE_WALLSLIDE)
        return;
      this.reverseFacingDir();
    }

    private void deinitStateHang() => this.m_positionInterpolation.stop();

    private void initStateHangClamber(GameObjectPlayer.PlayerState oldState)
    {
      MathVector mathVector = new MathVector(this.getForwardDirection()) * ((float) this.getFacingDir() * 0.368f);
      GameObjectPlayer gameObjectPlayer = this;
      gameObjectPlayer.m_position = gameObjectPlayer.m_position + mathVector;
    }

    private void animStoppedStateHangClamber()
    {
      if (this.isAnyGestureSet(128))
      {
        this.snapPlayerToGround();
        this.playerJump(true);
        this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.IGNORE_ONE_FRAME;
      }
      else
      {
        this.setForwardSpeed(5f);
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
      }
    }

    private void initStateSliding(GameObjectPlayer.PlayerState oldState)
    {
      this.m_targetForwardVelocity = 5f;
      this.m_targetForwardVelocityAcceleration = 2f;
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerAttackStart();
      this.slidingTime = 0;
    }

    private void updateStateSliding(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerSlideDist(Math.Abs(this.m_frameStartPos.x - this.m_position.x));
      if ((double) this.getForwardVelocity() * (double) this.getFacingDir() == (double) this.m_targetForwardVelocity)
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
      else if (this.m_globalShape != this.m_crouchingShape && (this.slidingTime > 100 || this.getVisualType() == 0 && this.getVisualIndex() == (int) GameObjectRunner.get("VISUAL_SOLO_SLIDING")))
        this.m_globalShape = this.m_crouchingShape;
      else
        this.slidingTime += timeStepMillis;
    }

    private void deinitStateSliding()
    {
      if (MirrorsEdge.TrialMode)
        return;
      this.m_achievementData.registerAttackEnd();
    }

    private void updateStateWallRun(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerWallRunDist(Math.Abs(this.m_frameStartPos.x - this.m_position.x));
      bool flag = this.isAnyGestureSet(32) || this.isAnyGestureSet(3) && this.getFacingDir() == GameObjectRunner.FacingDir.FACING_RIGHT || this.isAnyGestureSet(12) && this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT;
      if (this.m_stateTime < 1000 && !flag && this.checkWallrun())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
    }

    private void deinitStateWallRun()
    {
      this.m_wallRunImmuneTimer = 750;
      this.clearGesture(160);
    }

    private void initStateWallSlide(GameObjectPlayer.PlayerState oldState)
    {
      this.loadParticleEffectsForRampAndWallSlides();
      if (this.m_wallSlideEffectHand.getParent() == null)
      {
        World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
        m3Gworld.addChild((Node) this.m_wallSlideEffectHand);
        m3Gworld.addChild((Node) this.m_wallSlideEffectFoot);
      }
      this.reverseFacingDir();
    }

    private void updateStateWallSlide(int timeStepMillis)
    {
      int wallActionChecks = 16;
      if (this.m_ignoreClimbUp == GameObjectPlayer.IgnoreCode.DONT_IGNORE)
        wallActionChecks |= 14;
           
      this.checkWallAction((GameObjectRunner.FacingDir)(-(int)this.getFacingDir()), false, wallActionChecks);
      this.updateParticleEffectForRampAndWallSlides(timeStepMillis, 25f, 1f, this.m_wallSlideEffectHand, this.m_faithHandFootLocator, 0.0f, 0.2f);
      this.updateParticleEffectForRampAndWallSlides(timeStepMillis, 25f, 1f, this.m_wallSlideEffectFoot, this.m_faithLeftFootLocator, 0.0f, 0.2f);
      this.m_wallSlideEffectHand.setOrientation((float) this.getFacingDir() * -10f, 0.0f, 0.0f, 1f);
      this.m_wallSlideEffectFoot.setOrientation((float) this.getFacingDir() * -10f, 0.0f, 0.0f, 1f);
    }

    private void deinitStateWallSlide()
    {
      this.m_ignoreClimbUp = GameObjectPlayer.IgnoreCode.DONT_IGNORE;
      if (this.m_wallSlideEffectHand == null || this.m_wallSlideEffectHand.getParent() == null)
        return;
      World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
      m3Gworld.removeChild((Node) this.m_wallSlideEffectHand);
      m3Gworld.removeChild((Node) this.m_wallSlideEffectFoot);
    }

    private void initStateSwinging(GameObjectPlayer.PlayerState oldState)
    {
      this.m_swingingVelocity = 1f;
      this.m_initialSwingPhase = (double) this.getFacingDir() > 0.0 ? 0.0f : 3.14159274f;
      this.m_swingingProgress = 0.0f;
      this.m_swingingDirection = (double) this.getForwardVelocity() < 0.0 ? -1 : 1;
      this.setNoVelocity();
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y + 1.6f, this.m_position.z);
      this.m_localTransformNode.setTranslation(0.0f, -1.6f, 0.0f);
      this.m_swingImmunity = 2;
      this.clearGesture(128);
    }

    private void updateStateSwinging(int timeStepMillis)
    {
      float num1 = (float) timeStepMillis * (1f / 1000f);
      this.m_swingingProgress += num1;
      this.m_swingingAngleDeg = this.m_swingingVelocity * (float) Math.Sin((double) this.m_initialSwingPhase + (double) this.m_swingingProgress * 2.0 * Math.PI / 1.3200000524520874);
      this.m_swingingDirection = Math.Cos((double) this.m_initialSwingPhase + (double) this.m_swingingProgress * 2.0 * Math.PI / 1.3200000524520874) > 0.0 ? 1 : -1;
      float num2 = Math.Max(0.0f, Math.Abs((float) ((double) this.m_swingingDirection * (double) AppEngine.getCanvas().getSceneGame().getAccelerometerTiltVelocity() * 0.699999988079071)));
      this.m_swingingVelocity = Math.Min(1f, Math.Max(0.05f, this.m_swingingVelocity - num1 * (0.3f - num2)));
      this.m_objectNode.setOrientation(70f * this.m_swingingAngleDeg, 0.0f, 0.0f, 1f);
      this.set3WayAnimationBlendWeights((float) this.getFacingDir() * this.m_swingingAngleDeg);
    }

    private void deinitStateSwinging()
    {
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      this.m_localTransformNode.setTranslation(0.0f, 0.0f, 0.0f);
      this.m_objectNode.setOrientation(0.0f, 0.0f, 0.0f, 1f);
    }

    private void initStateScaleJump(GameObjectPlayer.PlayerState oldState)
    {
      this.clearGesture(143);
    }

    private void updateStateScaleJump(int timeStepMillis)
    {
      if (!this.m_positionInterpolation.isFinished())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_SCALE_HAUL_UP);
    }

    private void updateStateScaleHaulUp(int timeStepMillis)
    {
      if (!this.isAnimating())
        return;
      float num1 = this.calcAnimProgress();
      float num2 = 1f - num1;
      this.m_position.z = (float) ((double) this.m_positionInterpolation.getStartValue().z * (double) num1 + (double) this.m_positionInterpolation.getEndValue().z * (double) num2);
    }

    private void animStoppedStateScaleHaulUp()
    {
      if (this.isAnyGestureSet(128))
      {
        if (!this.checkLedgeScale())
        {
          this.stateTransition(this.m_ledgeScalePostState);
          if (this.m_ledgeScalePostState == GameObjectPlayer.PlayerState.STATE_RUNNING)
            this.setForwardVelocity((float) this.getFacingDir() * 8.5f);
        }
      }
      else if (this.isAnyGestureSet(3))
      {
        this.setFacingDir(GameObjectRunner.FacingDir.FACING_LEFT);
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
        this.setForwardVelocity((float) this.getFacingDir() * 8.5f);
      }
      else if (this.isAnyGestureSet(12))
      {
        this.setFacingDir(GameObjectRunner.FacingDir.FACING_RIGHT);
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
        this.setForwardVelocity((float) this.getFacingDir() * 8.5f);
      }
      else
      {
        this.stateTransition(this.m_ledgeScalePostState);
        if (this.m_ledgeScalePostState == GameObjectPlayer.PlayerState.STATE_RUNNING)
          this.setForwardVelocity((float) this.getFacingDir() * 8.5f);
      }
      if (this.m_ledgeScalePostAnimState == GameObjectPlayer.AnimationState.ANIM_STATE_NONE)
        return;
      this.animStateTransition(this.m_ledgeScalePostAnimState);
    }

    private void deinitStateScale()
    {
      this.m_position.z = this.m_positionInterpolation.getStartValue().z;
    }

    private void initStateRampRunUp(GameObjectPlayer.PlayerState oldState)
    {
      if ((double) this.m_velocity.y < 0.0)
        this.m_velocity.reverse();
      if ((double) this.getForwardVelocity() < 0.0 != (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT))
        this.reverseFacingDir();
      MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
      MathLine line = new MathLine(this.m_position, 0.0f, -1f, 0.0f);
      line.origin.y += localBounds.max.y * 0.5f;
      this.m_rampCollisionShape = this.m_map.getAttributeShapeAt(line, 0);
      if (this.m_rampCollisionShape == null || !this.m_rampCollisionShape.isRamp())
      {
        if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
          line.origin.x += localBounds.min.x;
        else
          line.origin.x += localBounds.max.x;
        this.m_rampCollisionShape = this.m_map.getAttributeShapeAt(line, 0);
      }
      MathOrthoBox bounds = this.m_rampCollisionShape.getBounds();
      MathVector direction = new MathVector(bounds.max);
      direction -= bounds.min;
      direction.z = 0.0f;
      if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
        direction.x = -direction.x;
      this.initLineMovement(direction, Math.Max(7f, this.m_velocity.getLength()), true);
    }

    private void initStateRampSlide(GameObjectPlayer.PlayerState oldState)
    {
      this.loadParticleEffectsForRampAndWallSlides();
      if (this.m_rampSlideEffect1.getParent() == null)
      {
        World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
        m3Gworld.addChild((Node) this.m_rampSlideEffect1);
        m3Gworld.addChild((Node) this.m_rampSlideEffect2);
      }
      if (0.0 < (double) this.m_velocity.y)
        this.m_velocity.reverse();
      if ((double) this.getForwardVelocity() < 0.0 != (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT))
        this.reverseFacingDir();
      MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
      MathLine line = new MathLine(this.m_position, 0.0f, -1f, 0.0f);
      line.origin.y += localBounds.max.y * 0.5f;
      this.m_rampCollisionShape = this.m_map.getAttributeShapeAt(line, 0);
      if (this.m_rampCollisionShape == null || !this.m_rampCollisionShape.isRamp())
      {
        if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
          line.origin.x += localBounds.max.x;
        else
          line.origin.x += localBounds.min.x;
        this.m_rampCollisionShape = this.m_map.getAttributeShapeAt(line, 0);
      }
      MathOrthoBox bounds = this.m_rampCollisionShape.getBounds();
      MathLine mathLine = new MathLine();
      if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_RIGHT)
      {
        mathLine.origin.x = bounds.min.x;
        mathLine.origin.y = bounds.max.y;
        mathLine.direction.x = bounds.max.x - mathLine.origin.x;
        mathLine.direction.y = bounds.min.y - mathLine.origin.y;
      }
      else
      {
        mathLine.origin.x = bounds.max.x;
        mathLine.origin.y = bounds.max.y;
        mathLine.direction.x = bounds.min.x - mathLine.origin.x;
        mathLine.direction.y = bounds.min.y - mathLine.origin.y;
      }
      this.initLineMovement(mathLine.direction, 10.5f, false);
      this.m_position.y = mathLine.calculateYatT(mathLine.calculateTatX(this.m_position.x)) + 0.01f;
      if ((double) this.getForwardVelocity() < 0.0)
      {
        float degrees = JMath.toDegrees((float) (Math.Atan2((double) this.m_velocity.y, (double) this.m_velocity.x) + Math.PI));
        this.m_objectNode.setOrientation(degrees, 0.0f, 0.0f, 1f);
        this.m_rampSlideEffect1.setOrientation(degrees - 80f, 0.0f, 0.0f, 1f);
        this.m_rampSlideEffect2.setOrientation(degrees - 80f, 0.0f, 0.0f, 1f);
      }
      else
      {
        float degrees = JMath.toDegrees((float) Math.Atan2((double) this.m_velocity.y, (double) this.m_velocity.x));
        this.m_objectNode.setOrientation(degrees, 0.0f, 0.0f, 1f);
        this.m_rampSlideEffect1.setOrientation(80f + degrees, 0.0f, 0.0f, 1f);
        this.m_rampSlideEffect2.setOrientation(80f + degrees, 0.0f, 0.0f, 1f);
      }
      this.m_accelerometerNoiseFilter.setSteadyState(0.0f);
    }

    private void updateStateRampRunUp(int timeStepMillis)
    {
      if (this.m_lineMovementClip == GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_CLIPPED)
      {
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_WALLCLIMB);
      }
      else
      {
        MathOrthoBox bounds = this.m_rampCollisionShape.getBounds();
        if ((double) bounds.max.y >= (double) this.m_position.y)
          return;
        this.m_position.y = bounds.max.y;
        this.setUpVelocity(0.0f);
        this.groundPlayer();
      }
    }

    private void updateStateRampSlide(int timeStepMillis)
    {
      if ((double) this.m_position.y < (double) this.m_rampCollisionShape.getBounds().min.y)
      {
        this.m_position.y += this.m_map.calculateCollisionMoveOut(new MathLine(this.m_position, 0.0f, 1f, 0.0f), 0);
        this.groundPlayer();
      }
      float rateFactor = Math.Abs(this.getAccelerometerTilt(true));
      float rate = (float) (4.0 + 28.0 * (double) rateFactor * (double) rateFactor);
      if (this.getFacingDir() == GameObjectRunner.FacingDir.FACING_LEFT)
      {
        this.updateParticleEffectForRampAndWallSlides(timeStepMillis, rate, rateFactor, this.m_rampSlideEffect1, this.m_faithLeftFootLocator, 0.25f, 0.0f);
        this.updateParticleEffectForRampAndWallSlides(timeStepMillis, rate, rateFactor, this.m_rampSlideEffect2, this.m_faithRightFootLocator, 0.25f, 0.0f);
      }
      else
      {
        this.updateParticleEffectForRampAndWallSlides(timeStepMillis, rate, rateFactor, this.m_rampSlideEffect1, this.m_faithLeftFootLocator, 0.1f, 0.3f);
        this.updateParticleEffectForRampAndWallSlides(timeStepMillis, rate, rateFactor, this.m_rampSlideEffect2, this.m_faithRightFootLocator, 0.1f, 0.3f);
      }
      this.updateAccelerometerNoise(timeStepMillis);
    }

    private void deinitStateRampAny()
    {
      this.m_rampCollisionShape = (CollShape) null;
      this.m_objectNode.setOrientation(0.0f, 0.0f, 0.0f, 1f);
      this.m_accelerometerNoiseFilter.setSteadyState(0.0f);
      this.deinitLineMovement();
      if (this.m_rampSlideEffect1 == null || this.m_rampSlideEffect1.getParent() == null)
        return;
      World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
      m3Gworld.removeChild((Node) this.m_rampSlideEffect1);
      m3Gworld.removeChild((Node) this.m_rampSlideEffect2);
    }

    private void loadStateZipLineParticleEffect()
    {
      if (this.m_zipLineSparkEffect != null)
        return;
      ParticleMode particleMode = new ParticleMode((AppearanceBase) AppEngine.getM3GAssets().loadTexturedAppearance((int) M3GAssets.get("TEX_EFFECT_PARTICLES_ALPHA_ADD"), 4));
      particleMode.setMeanTimeToLive(200f, 100f);
      KeyframeSequence sequence1 = new KeyframeSequence(3, 2, 176);
      float[] numArray1 = new float[6]
      {
        0.0f,
        0.0f,
        0.25f,
        0.8f,
        0.0f,
        0.0f
      };
      sequence1.setKeyframe(0, 0, numArray1, 0);
      sequence1.setKeyframe(1, 200, numArray1, 2);
      sequence1.setKeyframe(2, 1000, numArray1, 4);
      sequence1.setDuration(1001);
      particleMode.setScale(sequence1);
      KeyframeSequence sequence2 = new KeyframeSequence(2, 1, 176);
      float[] numArray2 = new float[2]{ 0.0f, 180f };
      sequence2.setKeyframe(0, 0, numArray2, 0);
      sequence2.setKeyframe(1, 1000, numArray2, 1);
      sequence2.setDuration(1001);
      particleMode.setRotation(sequence2);
      KeyframeSequence sequence3 = new KeyframeSequence(1, 4, 180);
      float num = 1f / 256f;
      float[] numArray3 = new float[4]
      {
        0.0f * num,
        192f * num,
        64f * num,
        64f * num
      };
      sequence3.setKeyframe(0, 0, numArray3, 0);
      sequence3.setDuration(1001);
      particleMode.setCrop(sequence3);
      KeyframeSequence sequence4 = new KeyframeSequence(4, 4, 176);
      float[] numArray4 = new float[16]
      {
        1f,
        0.8f,
        0.0f,
        0.0f,
        1f,
        0.8f,
        0.0f,
        1f,
        1f,
        0.5f,
        0.0f,
        1f,
        1f,
        0.5f,
        0.0f,
        0.0f
      };
      sequence4.setKeyframe(0, 0, numArray4, 0);
      sequence4.setKeyframe(1, 150, numArray4, 4);
      sequence4.setKeyframe(2, 300, numArray4, 8);
      sequence4.setKeyframe(3, 1000, numArray4, 12);
      sequence4.setDuration(1001);
      particleMode.setColor(sequence4);
      EmissionMode emissionMode = new EmissionMode();
      emissionMode.setSpeed(7f, 1f);
      emissionMode.setSpreadAngle(10f, 10f);
      ParticleEffect child = new ParticleEffect(Emitter.createEmitter(2, 60, particleMode, emissionMode));
      this.m_zipLineSparkEffect = child;
      AppEngine.getCanvas().getSceneGame().getM3GWorld().addChild((Node) child);
    }

    private void unloadStateZipLineParticleEffect()
    {
      if (this.m_zipLineSparkEffect == null)
        return;
      AppEngine.getCanvas().getSceneGame().getM3GWorld().removeChild((Node) this.m_zipLineSparkEffect);
      this.m_zipLineSparkEffect = (ParticleEffect) null;
    }

    private void initStateZipLineJumpTo(GameObjectPlayer.PlayerState oldState)
    {
      this.m_noClip = true;
    }

    private void updateStateZipLineJumpTo(int timeStepMillis)
    {
      if ((double) this.m_frameStartPos.y < (double) this.m_zipLineJumpDestination.y == (double) this.m_position.y < (double) this.m_zipLineJumpDestination.y)
        return;
      this.m_position = this.m_zipLineJumpDestination;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_ZIP_LINE);
    }

    private void deinitStateZipLineJumpTo() => this.m_noClip = false;

    private void initStateZipLine(GameObjectPlayer.PlayerState oldState)
    {
      this.loadStateZipLineParticleEffect();
      this.initLineMovement(this.m_attachedZipLine.getZipLineLine().direction, this.m_velocity.getLength(), true);
      this.m_accelerometerNoiseFilter.setSteadyState(0.0f);
    }

    private void updateStateZipLine(int timeStepMillis)
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerZiplineDist(new MathVector(this.m_frameStartPos.x - this.m_position.x, this.m_frameStartPos.y - this.m_position.y, this.m_frameStartPos.z - this.m_position.z).getLength());
      MathLine mathLine = new MathLine(this.m_attachedZipLine.getZipLineLine());
      float x = mathLine.origin.x;
      float num1 = x + mathLine.direction.x;
      this.m_attachedZiplineEndOffset = this.m_attachedZipLine.getEndOffset();
      if (this.m_lineMovementClip == GameObjectPlayer.LineClip.LINE_MOVEMENT_CLIP_CLIPPED)
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_FALLING_FORWARDS);
      else if ((double) x < (double) num1)
      {
        if ((double) num1 - (double) this.m_attachedZiplineEndOffset < (double) this.m_position.x)
        {
          this.m_position.x = num1 - this.m_attachedZiplineEndOffset;
          this.stateTransition(GameObjectPlayer.PlayerState.STATE_ZIP_LINE_FAIL);
        }
      }
      else if ((double) this.m_position.x < (double) num1 + (double) this.m_attachedZiplineEndOffset)
      {
        this.m_position.x = num1 + this.m_attachedZiplineEndOffset;
        this.stateTransition(GameObjectPlayer.PlayerState.STATE_ZIP_LINE_FAIL);
      }
      this.updateAccelerometerNoise(timeStepMillis);
      if (this.m_zipLineSparkEffect == null)
        return;
      float num2 = Math.Abs(this.getAccelerometerTilt(true));
      float rate = (float) (2.0 + 100.0 * (double) num2);
      ParticleEffect zipLineSparkEffect = this.m_zipLineSparkEffect;
      int emitterCount = zipLineSparkEffect.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
      {
        Emitter emitter = zipLineSparkEffect.getEmitter(index);
        emitter.setOrientation((float) (70 * (int) this.getFacingDir()), 0.0f, 0.0f, 1f);
        EmissionMode emissionMode = emitter.getEmissionMode();
        emissionMode.setRate(rate);
        emissionMode.setSpeed(7f * num2, 1f * num2);
      }
      zipLineSparkEffect.setTranslation(this.m_position.x, this.m_position.y + 1.58f, this.m_position.z);
      zipLineSparkEffect.updateStep(timeStepMillis, (Transform) null, (Transform) null);
    }

    private void deinitStateZipLine()
    {
      this.deinitLineMovement();
      this.m_attachedZipLine = (GameObjectZipLine) null;
      this.clearGesture(32);
      this.unloadStateZipLineParticleEffect();
    }

    private void initStateZipLineFail(GameObjectPlayer.PlayerState oldState)
    {
      this.m_velocity.set(0.0f, 0.0f, 0.0f);
      MathLine line = new MathLine(this.m_position, 0.0f, -1f, 0.0f);
      line.origin.x += (float) this.getFacingDir() * this.m_attachedZiplineEndOffset;
      float tValue = 0.0f;
      if (!this.m_map.calculateCollision(line, ref tValue, 0))
        return;
      this.m_position.y -= tValue;
    }

    private void updateStateZipLineFail(int timeStepMillis)
    {
      if (this.isAnimating())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_IDLE);
    }

    private void initStateMeleedInTheFace(GameObjectPlayer.PlayerState oldState)
    {
      this.m_downedTime = 3000;
      this.setNoVelocity();
    }

    private void updateStateMeleedInTheFace(int timeStepMillis)
    {
      if ((double) this.getUpVelocity() <= -18.0)
        AppEngine.getCanvas().getSceneGame().stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING);
      if (this.m_objectAnimPlayer.isAnimating())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_PRONE);
    }

    private void initStateDisarmingCop(GameObjectPlayer.PlayerState oldState)
    {
      this.m_achievementData.registerDisarm();
    }

    private void updateStateDisarmingCop(int timeStepMillis)
    {
      if (this.m_objectAnimPlayer.isAnimating())
        return;
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_RUNNING);
    }

    private void initStateFlykick(GameObjectPlayer.PlayerState oldState)
    {
      if (MirrorsEdge.TrialMode)
        return;
      this.m_achievementData.registerAttackStart();
    }

    private void updateStateFlykick(int timeStepMillis)
    {
      if (!this.m_objectAnimPlayer.isAnimating())
        this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_FLYING_KICK_LOOP"));
      if ((double) this.getUpVelocity() > -18.0)
        return;
      AppEngine.getCanvas().getSceneGame().stateTransition(SceneGame.GameState.STATE_PLAYER_DEATH_FALLING);
    }

    private void deinitStateFlykick()
    {
      if (MirrorsEdge.TrialMode)
        return;
      AppEngine.getAchievementData().registerAttackEnd();
    }

    public int getCurrentMaterial()
    {
      return this.m_state == GameObjectPlayer.PlayerState.STATE_RAMP_RUN_UP ? this.m_rampCollisionShape.m_materialId : this.m_currentMaterial;
    }

    public void hurt(float amount, bool playSFX)
    {
      if (this.m_state == GameObjectPlayer.PlayerState.STATE_DEAD || this.m_state == GameObjectPlayer.PlayerState.STATE_DYING || this.m_state == GameObjectPlayer.PlayerState.STATE_INACTIVE)
        return;
      AppEngine canvas = AppEngine.getCanvas();
      this.m_health -= amount;
      this.m_regenDelay = 3000;
      if ((double) this.m_health <= 0.0)
        this.die();
      canvas.getSceneGame().playerHurt(playSFX);
    }

    public void die()
    {
      if (!MirrorsEdge.TrialMode)
        this.m_achievementData.registerDeath();
      AppEngine.getCanvas().getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_DEATH_01"), 1f);
      this.stateTransition(GameObjectPlayer.PlayerState.STATE_DYING);
    }

    public bool isDead()
    {
      return this.m_state == GameObjectPlayer.PlayerState.STATE_DEAD || this.m_state == GameObjectPlayer.PlayerState.STATE_DYING;
    }

    public float getHealthFraction() => Math.Max(0.0f, this.m_health) / 10f;

    private void snapPlayerToGround()
    {
      float collisionMoveOut = this.m_map.calculateCollisionMoveOut(new MathLine(this.m_position, 0.0f, 1f, 0.0f), 0);
      if ((double) collisionMoveOut == 0.0)
        return;
      this.setUpVelocity(0.0f);
      this.m_position.y += collisionMoveOut;
    }

    public enum PlayerState
    {
      STATE_INACTIVE,
      STATE_INTRO_ANIM,
      STATE_IDLE,
      STATE_RUNNING,
      STATE_RUNNING_TURNING,
      STATE_BALANCE_RUNNING,
      STATE_BALANCE_FELL,
      STATE_STOPPING,
      STATE_FALLING_VERTICALLY,
      STATE_FALLING_FORWARDS,
      STATE_FALLING_CRASHING,
      STATE_ROLLING,
      STATE_CRASH,
      STATE_WALLCLIMB,
      STATE_CLAMBER,
      STATE_HANG,
      STATE_HANG_CLAMBER,
      STATE_SLIDING,
      STATE_WALLRUN,
      STATE_WALLSLIDE,
      STATE_SWINGING,
      STATE_SCALE_JUMP,
      STATE_SCALE_HAUL_UP,
      STATE_RAMP_RUN_UP,
      STATE_RAMP_SLIDE,
      STATE_ZIP_LINE_JUMP_TO,
      STATE_ZIP_LINE,
      STATE_ZIP_LINE_FAIL,
      STATE_MELEED_IN_THE_FACE,
      STATE_DISARMING_COP,
      STATE_PRONE,
      STATE_GETUP,
      STATE_MELEE_GETUP,
      STATE_FLYKICK,
      STATE_DYING,
      STATE_DEAD,
    }

    protected enum AnimationState
    {
      ANIM_STATE_NONE,
      ANIM_STATE_LANDING,
      ANIM_STATE_BOOST_JUMP,
      ANIM_STATE_POWER_JUMP_START,
      ANIM_STATE_POWER_JUMP_LOOP,
      ANIM_STATE_SCALE_HALF_STEP,
      ANIM_STATE_HANG_TO_SLIDE,
      ANIM_STATE_WALLSLIDE_TO_JUMP,
      ANIM_STATE_FLYING_KICK_IMPACT,
    }

    private enum LineClip
    {
      LINE_MOVEMENT_CLIP_NONE,
      LINE_MOVEMENT_CLIP_SET,
      LINE_MOVEMENT_CLIP_CLIPPED,
    }

    private enum IgnoreCode
    {
      DONT_IGNORE,
      IGNORE_ONE_FRAME,
      IGNORE_CONSTANT,
    }
  }
}
