
// Type: game.GameObjectPolice
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using System;

#nullable disable
namespace game
{
  public class GameObjectPolice : GameObjectNPC
  {
    private const int RAND_MAX = 65535;
    public const int UPDATE_RANGESQ = 625;
    public const float COMBAT_TIME_DIST_SQ = 36f;
    protected const float KICK_HEIGHT = 0.5f;
    protected const float KICK_HEIGHT_HIGH = 1f;
    protected const int numAnims = 15;
    private static Random rand = new Random();
    private ChunkDynamic m_dynamicNode;
    protected new GameObjectNPC.NPCState m_state;
    protected GameObjectPolice.BrainState m_brainState;
    protected GameObjectPolice.AnimState m_animState;
    protected new int m_stateTime;
    protected MathVector m_preFallPos = new MathVector();
    protected int m_deadTime;
    protected bool m_triggeredCombat;
    protected MathVector m_deceleration = new MathVector();
    protected static string[] DEBUG_ANIM_STATES;
    protected readonly int[] testAnims = new int[15]
    {
      (int) ResourceManager.get("ANIM3D_POLICE_IDLE"),
      (int) ResourceManager.get("ANIM3D_POLICE_DRAW_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_LOWER_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_LOWERED_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_AIM_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_AIM_HOLD"),
      (int) ResourceManager.get("ANIM3D_POLICE_FIRE_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_HOLSTER_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH_LOWER_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH_LOWERED_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH_AIM_PISTOL"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH_AIM_HOLD"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH_FIRE"),
      (int) ResourceManager.get("ANIM3D_POLICE_CROUCH_STAND")
    };
    protected readonly int[] testChannels = new int[15]
    {
      (int) ResourceManager.get("CHANNEL_POLICE_IDLE"),
      (int) ResourceManager.get("CHANNEL_POLICE_DRAW_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_LOWER_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_LOWERED_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_AIM_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_AIM_HOLD"),
      (int) ResourceManager.get("CHANNEL_POLICE_FIRE_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_HOLSTER_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_LOWER_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_LOWERED_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_AIM_PISTOL"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_AIM_HOLD"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_FIRE"),
      (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_STAND")
    };
    protected int m_curAnimIdx;
    protected int m_curAnimTime;
    protected int m_aimingTimer;
    protected int m_lastPostureChangeDelay;
    protected int m_postureChangeTimer;
    protected int m_fireDelay;
    protected int m_fireDecisionTimer;
    protected int m_shotDelay;
    protected int m_pathId;
    protected Path m_path;
    protected int m_nextPoint;
    protected float m_excessYTranslation;
    protected GameObjectPlayer m_player;
    protected int m_distanceToPlayerSq;
    protected bool m_facePlayer;
    protected int FIRE_DELAY;
    protected int FIRE_DECISION_TIME;
    protected int AIM_DURATION;
    protected int SHOT_DELAY;
    protected int SHOT_REPEAT_CHANCE;
    protected int POSTURE_DECISION_TIME;
    protected int POSTURE_CHANGE_CHANCE;
    protected int POSTURE_CHANGE_DELAY;
    protected int FIRE_CHANCE;
    protected int TRIGGER_RANGE_FRONT;
    protected int TRIGGER_RANGE_REAR;

    public GameObjectPolice(
      MEdgeMap map,
      int type,
      int modelId,
      int blenderId,
      float posX,
      float posY,
      float posZ,
      int pathId)
      : base(map, type, posX, posY, posZ)
    {
      this.m_state = GameObjectNPC.NPCState.NPCSTATE_INACTIVE;
      this.m_stateTime = 0;
      this.m_preFallPos = new MathVector();
      this.m_brainState = GameObjectPolice.BrainState.BRAINSTATE_IDLE;
      this.m_animState = GameObjectPolice.AnimState.ANIMSTATE_IDLE;
      this.m_curAnimIdx = -1;
      this.m_curAnimTime = -1;
      this.m_aimingTimer = -1;
      this.m_lastPostureChangeDelay = -1;
      this.m_postureChangeTimer = -1;
      this.m_fireDelay = -1;
      this.m_fireDecisionTimer = -1;
      this.m_shotDelay = -1;
      this.m_pathId = pathId;
      this.m_path = (Path) null;
      this.m_nextPoint = -1;
      this.m_facePlayer = false;
      this.m_distanceToPlayerSq = 0;
      this.m_deadTime = 0;
      this.m_triggeredCombat = false;
      this.m_deceleration = new MathVector(0.0f, 0.0f, 0.0f);
      this.m_player = (GameObjectPlayer) null;
      this.FIRE_DELAY = 0;
      this.FIRE_DECISION_TIME = 0;
      this.AIM_DURATION = 0;
      this.SHOT_DELAY = 0;
      this.SHOT_REPEAT_CHANCE = 0;
      this.POSTURE_DECISION_TIME = 0;
      this.POSTURE_CHANGE_CHANCE = 0;
      this.POSTURE_CHANGE_DELAY = 0;
      this.FIRE_CHANCE = 0;
      this.TRIGGER_RANGE_FRONT = 0;
      this.TRIGGER_RANGE_REAR = 0;
      this.m_excessYTranslation = 0.0f;
      this.setVisualAssets(modelId, blenderId);
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(-0.8f, 0.0f, -0.3f, 0.8f, 1.8f, 0.3f);
      this.m_globalShape.setBounce(0.0f);
      this.m_dynamicNode = this.m_map.getForegroundLayer().addDynamicNode(this.m_objectNode, (GameObject) this);
      this.resetLevel();
      this.m_facingRotateSpeed = 25.1327419f;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (soundManager.isEventLoaded((int) ResourceManager.get("SOUNDEVENT_SFX_WILHELM_SCREAM")))
        return;
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_WILHELM_SCREAM"));
    }

    public override void Destructor()
    {
      if (this.m_dynamicNode == null)
        return;
      this.m_map.getForegroundLayer().removeDynamicNode(this.m_dynamicNode);
      this.m_dynamicNode = (ChunkDynamic) null;
      base.Destructor();
    }

    public override void resetLevel()
    {
      this.m_triggeredCombat = false;
      this.m_deadTime = 0;
      this.m_player = this.m_map.getPlayerObject();
      this.m_excessYTranslation = 0.0f;
      base.resetLevel();
      this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_INITIAL);
      this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_IDLE);
      if (this.m_pathId == -1)
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_IDLE);
      else
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_PATROL);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      this.m_objectNode.setOrientation(0.0f, 0.0f, 0.0f, 1f);
    }

    public override void collidedWith(GameObject other)
    {
      if (!this.isActive() || other.getType() != 0)
        return;
      float lateralDistanceToSq = this.getLateralDistanceToSq((GameObject) this.m_player);
      bool flag1 = (double) lateralDistanceToSq <= 0.64000004529953;
      bool flag2 = (double) lateralDistanceToSq <= 1.6205290555953979;
      bool flag3 = this.m_player.isSliding() || this.m_player.isKicking() || this.m_player.isRolling();
      bool flag4 = this.m_player.isGettingUp();
      bool flag5 = this.m_player.isProne();
      if (flag3 && flag1)
      {
        SpywareManager.getInstance().trackGuardKilled(this.m_map.getUniqueGameObjectID((GameObject) this));
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_FALL);
        if (!this.m_player.isKicking())
          return;
        this.m_player.kickSomething();
      }
      else if (this.m_player.isDisarming() && flag1)
      {
        SpywareManager.getInstance().trackGuardKilled(this.m_map.getUniqueGameObjectID((GameObject) this));
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_DISARMED);
        this.m_player.disarmCop((GameObjectNPC) this);
      }
      else
      {
        if (this.m_brainState == GameObjectPolice.BrainState.BRAINSTATE_MELEE || flag3 || this.m_player.isDisarming() || this.m_player.isLying() || !flag2 || flag4 || flag5)
          return;
        this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_STANDING);
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_MELEE);
        this.m_player.meleeByCop((GameObjectNPC) this);
        this.setFacingDir((double) this.m_player.m_position.x < (double) this.m_position.x ? GameObjectRunner.FacingDir.FACING_LEFT : GameObjectRunner.FacingDir.FACING_RIGHT);
      }
    }

    public void meleedFromPlayerJumpingFromProne()
    {
      this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_HIT_FROM_PRONE_JUMPUP);
    }

    public override void update(int timeStepMillis)
    {
      this.m_distanceToPlayerSq = (int) this.getDistanceToSq((GameObject) this.m_player);
      this.updateDrawing();
      this.testVFC();
      if (this.m_passedVFC)
        this.m_objectNode.setRenderingEnable(true);
      else
        this.m_objectNode.setRenderingEnable(false);
      if (!this.shouldUpdate())
      {
        if (this.m_distanceToPlayerSq > 625)
          this.m_animateBlender = false;
        else
          this.m_firstAlwaysPass = false;
      }
      else
      {
        base.update(timeStepMillis);
        if (this.m_brainState == GameObjectPolice.BrainState.BRAINSTATE_FALL || this.m_brainState == GameObjectPolice.BrainState.BRAINSTATE_GETUP)
          this.m_passedVFC = true;
        this.updateBrain(timeStepMillis);
        this.updateState(timeStepMillis);
        this.updatePosition(timeStepMillis);
        this.updateAnimState(timeStepMillis);
        this.updateFacing(timeStepMillis);
        if (!this.m_triggeredCombat && (double) Math.Abs(this.m_position.z) <= 0.5 && (double) this.m_distanceToPlayerSq <= 36.0 && (double) Math.Abs(this.m_position.y - this.m_player.m_position.y) <= 4.0 && this.m_deadTime < 250 && this.checkLOSWithPlayer())
        {
          this.m_triggeredCombat = true;
          AppEngine.getCanvas().getSceneGame().startCombatTime();
        }
        MathVector mathVector = this.m_deceleration * ((float) timeStepMillis / 1000f);
        MathVector velocity = this.m_velocity;
        this.m_velocity.x -= (double) this.m_velocity.x > 0.0 ? mathVector.x : -mathVector.x;
        if ((double) velocity.x * (double) this.m_velocity.x <= 0.0)
        {
          this.m_velocity.x = 0.0f;
          this.m_deceleration.x = 0.0f;
        }
        this.m_velocity.y -= (double) this.m_velocity.y > 0.0 ? mathVector.y : -mathVector.y;
        if ((double) velocity.y * (double) this.m_velocity.y <= 0.0)
        {
          this.m_velocity.y = 0.0f;
          this.m_deceleration.y = 0.0f;
        }
        this.m_velocity.z -= (double) this.m_velocity.z > 0.0 ? mathVector.z : -mathVector.z;
        if ((double) velocity.z * (double) this.m_velocity.z > 0.0)
          return;
        this.m_velocity.z = 0.0f;
        this.m_deceleration.z = 0.0f;
      }
    }

    protected bool isAlive() => this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_DEAD;

    protected new bool isActive()
    {
      return this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_FALL && this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_DISARMED && this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_DEAD && this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_GETUP && this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_HIT_FROM_PRONE_JUMPUP;
    }

    protected void updateDrawing() => this.m_animateBlender = this.m_dynamicNode.isVisible();

    protected bool shouldUpdate()
    {
      return this.m_brainState == GameObjectPolice.BrainState.BRAINSTATE_FALL || this.m_brainState == GameObjectPolice.BrainState.BRAINSTATE_GETUP || this.m_state != GameObjectNPC.NPCState.NPCSTATE_INACTIVE && this.m_distanceToPlayerSq <= 625;
    }

    protected override void stateTransition(GameObjectNPC.NPCState newState)
    {
      if (this.m_state == newState)
        return;
      switch (newState)
      {
        case GameObjectNPC.NPCState.NPCSTATE_STANDING:
          if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_INITIAL)
          {
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_IDLE);
            break;
          }
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LOWERED_STANDING);
          break;
        case GameObjectNPC.NPCState.NPCSTATE_CROUCHING:
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LOWERED_CROUCHING);
          break;
        case GameObjectNPC.NPCState.NPCSTATE_WALKING:
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_WALKING);
          break;
        case GameObjectNPC.NPCState.NPCSTATE_RUNNING:
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_RUNNING);
          break;
      }
      this.m_state = newState;
      this.m_stateTime = 0;
    }

    protected void brainStateTransition(GameObjectPolice.BrainState newState)
    {
      this.m_facePlayer = false;
      switch (newState)
      {
        case GameObjectPolice.BrainState.BRAINSTATE_IDLE:
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_IDLE);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_PATROL:
          this.m_path = this.m_map.getPath(this.m_pathId);
          this.m_nextPoint = 0;
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_WALKING);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_ALERTED:
          if (AppEngine.getCanvas().rand(0, 100) < 20)
          {
            SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
            int num = AppEngine.getCanvas().rand(0, 100);
            int eventID = num >= 25 ? (num >= 50 ? (num >= 75 ? (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_4") : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_3")) : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_2")) : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_START_1");
            soundManager.playEventAt(eventID, this.m_position.x, this.m_position.y, this.m_position.z);
          }
          this.setNoVelocity();
          this.facePlayer();
          this.m_fireDelay = this.FIRE_DELAY;
          this.m_fireDecisionTimer = this.FIRE_DECISION_TIME;
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_ALERTED);
          this.m_facePlayer = true;
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_AIMING:
          this.facePlayer();
          if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_STANDING)
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_AIM_STANDING);
          else
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_AIM_CROUCHING);
          this.m_aimingTimer = this.AIM_DURATION;
          this.m_facePlayer = true;
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_FIRING:
          if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_STANDING)
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FIRING_STANDING);
          else
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FIRING_CROUCHING);
          this.shoot();
          this.m_shotDelay = this.SHOT_DELAY;
          this.m_facePlayer = true;
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_MELEE:
          this.m_stateTime = 0;
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_MELEE);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_FALL:
          this.m_stateTime = 0;
          this.m_preFallPos = this.m_position;
          this.setNoVelocity();
          AppEngine canvas = AppEngine.getCanvas();
          SoundEventPoolManager soundPoolManager = canvas.getSceneGame().getSoundPoolManager();
          SoundManager soundManager1 = canvas.getSoundManager();
          int raceTimeSecs = canvas.getSceneGame().getRaceTime() >> 10;
          AppEngine.getAchievementData().registerKill(raceTimeSecs);
          if (this.m_player.isSliding() || this.m_player.isRolling())
          {
            if (this.facingPlayer())
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD);
            else
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD);
            int randomSoundEventId = soundPoolManager.getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_MELEE"), 1);
            soundManager1.playEvent(randomSoundEventId);
            this.playRandomGruntSound();
            break;
          }
          MathVector mathVector = this.m_player.m_position - this.m_position;
          if (this.facingPlayer())
          {
            if ((double) mathVector.y < 0.5)
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD);
            else if ((double) mathVector.y < 1.0)
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD);
            else
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD_HIGH);
          }
          else if ((double) mathVector.y < 0.5)
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD);
          else if ((double) mathVector.y < 1.0)
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD);
          else
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD_HIGH);
          this.m_velocity.x = this.m_player.getVelocity().x * 1.3f;
          this.m_deceleration.x = 9f;
          int randomSoundEventId1 = soundPoolManager.getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_MELEE"), 1);
          soundManager1.playEvent(randomSoundEventId1);
          this.playRandomGruntSound();
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_GETUP:
          if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD)
          {
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_GETUP_FORWARD);
            break;
          }
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_GETUP_BACKWARD);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_DISARMED:
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_DISARMED);
          AppEngine.getCanvas().getSceneGame().getSoundSequencer().playSequence(10);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_HIT_FROM_PRONE_JUMPUP:
          this.m_stateTime = 0;
          this.m_preFallPos = this.m_position;
          this.setNoVelocity();
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_GETUP_MELEED);
          break;
      }
      this.m_brainState = newState;
    }

    protected void animStateTransition(GameObjectPolice.AnimState newState)
    {
      switch (newState)
      {
        case GameObjectPolice.AnimState.ANIMSTATE_IDLE:
          if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_ALERTED)
          {
            this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_HOLSTER_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_HOLSTER_PISTOL"), 16);
            break;
          }
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_IDLE"), (int) ResourceManager.get("CHANNEL_POLICE_IDLE"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_WALKING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_WALK"), (int) ResourceManager.get("CHANNEL_POLICE_WALK"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_RUNNING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_RUN"), (int) ResourceManager.get("CHANNEL_POLICE_RUN"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_ALERTED:
          if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_IDLE)
          {
            this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_DRAW_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_DRAW_PISTOL"), 16);
            break;
          }
          if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_STANDING)
          {
            this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LOWERED_STANDING);
            return;
          }
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LOWERED_CROUCHING);
          return;
        case GameObjectPolice.AnimState.ANIMSTATE_LOWERED_STANDING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_LOWER_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_LOWER_PISTOL"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_AIM_STANDING:
          if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_FIRING_STANDING)
          {
            this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_AIM_HOLD"), (int) ResourceManager.get("CHANNEL_POLICE_AIM_HOLD"), 4);
            break;
          }
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_AIM_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_AIM_PISTOL"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FIRING_STANDING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_FIRE_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_FIRE_PISTOL"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LOWERED_CROUCHING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_CROUCH_LOWER_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_LOWER_PISTOL"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_AIM_CROUCHING:
          if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_FIRING_CROUCHING)
          {
            this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_CROUCH_AIM_HOLD"), (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_AIM_HOLD"), 4);
            break;
          }
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_CROUCH_AIM_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_AIM_PISTOL"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FIRING_CROUCHING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_CROUCH_FIRE"), (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_FIRE"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_MELEE:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_MELEE_FAIL"), (int) ResourceManager.get("CHANNEL_POLICE_MELEE_FAIL"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_DISARMED:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_MELEE_SUCCESS"), (int) ResourceManager.get("CHANNEL_POLICE_MELEE_SUCCESS"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_FORWARDS_LOW"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_FORWARDS_LOW"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD_HIGH:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_FORWARDS_HIGH"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_FORWARDS_HIGH"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_BACKWARDS_LOW"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_BACKWARDS_LOW"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD_HIGH:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_BACKWARDS_HIGH"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_BACKWARDS_HIGH"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_GETUP_FORWARD:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_STAND_FORWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_STAND_FORWARDS"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_GETUP_BACKWARD:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_STAND_BACKWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_STAND_BACKWARDS"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_GETUP_MELEED:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_MELEE_GETUP"), (int) ResourceManager.get("CHANNEL_POLICE_MELEE_GETUP"), 16);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_FORWARDS:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_FALL_FORWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_FALL_FORWARDS"), 4);
          SoundManager soundManager1 = AppEngine.getCanvas().getSoundManager();
          int num1 = AppEngine.getCanvas().rand(0, 99);
          int eventID1 = num1 >= 20 ? (num1 >= 40 ? (num1 >= 60 ? (num1 >= 80 ? (int) ResourceManager.get("SOUNDEVENT_SFX_WILHELM_SCREAM") : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_4")) : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_3")) : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_2")) : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_1");
          soundManager1.playEventAt(eventID1, this.m_position.x, this.m_position.y, this.m_position.z);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_BACKWARDS:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_FALL_BACKWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_FALL_BACKWARDS"), 4);
          SoundManager soundManager2 = AppEngine.getCanvas().getSoundManager();
          int num2 = AppEngine.getCanvas().rand(0, 99);
          int eventID2 = num2 >= 20 ? (num2 >= 40 ? (num2 >= 60 ? (num2 >= 80 ? (int) ResourceManager.get("SOUNDEVENT_SFX_WILHELM_SCREAM") : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_4")) : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_3")) : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_2")) : (int) ResourceManager.get("SOUNDEVENT_SFX_SCREAM_SHORT_1");
          soundManager2.playEventAt(eventID2, this.m_position.x, this.m_position.y, this.m_position.z);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LAND:
          MathLine line = new MathLine(this.m_position.x, this.m_position.y, this.m_position.z, 0.0f, -1f, 0.0f);
          CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(line, 0);
          if (attributeShapeAt != null && attributeShapeAt.isRamp())
          {
            this.m_objectNode.setOrientation(attributeShapeAt.getShapeType() != CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X ? -30f : 30f, 0.0f, 0.0f, 1f);
            float minT = 0.0f;
            float maxT = 2f;
            this.m_map.intersects(line, 0, ref minT, ref maxT);
            this.m_excessYTranslation = minT * -1f;
          }
          if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_FORWARDS)
          {
            this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_LAND_FORWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_LAND_FORWARDS"), 16);
            break;
          }
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_LAND_BACKWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_LAND_BACKWARDS"), 16);
          break;
      }
      this.m_animState = newState;
    }

    protected void updateState(int timeStepMillis)
    {
      switch (this.m_state)
      {
        case GameObjectNPC.NPCState.NPCSTATE_INITIAL:
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_STANDING);
          break;
        case GameObjectNPC.NPCState.NPCSTATE_TESTANIMS:
          this.updateTestAnims(timeStepMillis);
          break;
      }
    }

    protected void updatePosition(int timeStepMillis)
    {
      CollShape collidedShape = (CollShape) null;
      MathVector collisionNormal = new MathVector();
      int num = this.m_map.moveObjectPhysics(ref this.m_position, ref this.m_velocity, (float) timeStepMillis / 1000f, this.m_globalShape, ref collidedShape, ref collisionNormal);
      if (this.m_brainState != GameObjectPolice.BrainState.BRAINSTATE_PATROL)
      {
        this.m_velocity.z = 0.0f;
        this.m_deceleration.x = 9f;
      }
      else
        this.m_deceleration.x = 0.0f;
      if ((this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_FORWARDS || this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_BACKWARDS) && (num & 4) != 0)
      {
        this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LAND);
        this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_INACTIVE);
      }
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y + this.m_excessYTranslation, this.m_position.z);
    }

    protected void updateBrain(int timeStepMillis)
    {
      switch (this.m_brainState)
      {
        case GameObjectPolice.BrainState.BRAINSTATE_IDLE:
          if (this.sightedPlayer())
          {
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_AIMING);
            break;
          }
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_PATROL:
          this.patrolBrainUpdate(timeStepMillis);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_ALERTED:
          this.alertBrainUpdate(timeStepMillis);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_AIMING:
          this.aimBrainUpdate(timeStepMillis);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_FIRING:
          this.fireBrainUpdate(timeStepMillis);
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_MELEE:
          int stateTime1 = this.m_stateTime;
          this.m_stateTime += timeStepMillis;
          if (stateTime1 < 280 && 280 <= this.m_stateTime)
          {
            AppEngine canvas = AppEngine.getCanvas();
            canvas.getSoundManager().playEvent(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_MELEE"), 0));
          }
          if (!this.m_objectAnimPlayer.isAnimating())
          {
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_ALERTED);
            break;
          }
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_FALL:
          if ((double) this.m_position.y - (double) this.m_preFallPos.y < -2.0)
          {
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_DEAD);
            if (this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD || this.m_animState == GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD_HIGH)
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_FORWARDS);
            else
              this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LONGFALL_BACKWARDS);
            AppEngine.getAchievementData().registerEnemyFall();
            break;
          }
          this.m_stateTime += timeStepMillis;
          if (this.m_stateTime >= 5000)
          {
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_GETUP);
            break;
          }
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_GETUP:
          if (!this.m_objectAnimPlayer.isAnimating())
          {
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_ALERTED);
            break;
          }
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_HIT_FROM_PRONE_JUMPUP:
          int stateTime2 = this.m_stateTime;
          this.m_stateTime += timeStepMillis;
          if (stateTime2 < 650 && 650 <= this.m_stateTime)
          {
            AppEngine canvas = AppEngine.getCanvas();
            canvas.getSoundManager().playEvent(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_MELEE"), 1));
            this.playRandomGruntSound();
          }
          if (this.m_stateTime < 650 && !this.m_map.getPlayerObject().isGetupMeleeing())
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_ALERTED);
          if (!this.m_objectAnimPlayer.isAnimating())
          {
            this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_GETUP);
            break;
          }
          break;
        case GameObjectPolice.BrainState.BRAINSTATE_DEAD:
          this.m_deadTime += timeStepMillis;
          break;
      }
      if (!this.m_facePlayer)
        return;
      this.updateFacePlayer(timeStepMillis);
    }

    protected void updateAnimState(int timeStepMillis)
    {
      switch (this.m_animState)
      {
        case GameObjectPolice.AnimState.ANIMSTATE_IDLE:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_IDLE"), (int) ResourceManager.get("CHANNEL_POLICE_IDLE"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_ALERTED:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_LOWERED_STANDING);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LOWERED_STANDING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_LOWERED_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_LOWERED_PISTOL"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_AIM_STANDING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_AIM_HOLD"), (int) ResourceManager.get("CHANNEL_POLICE_AIM_HOLD"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FIRING_STANDING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_AIM_STANDING);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LOWERED_CROUCHING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_CROUCH_LOWERED_PISTOL"), (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_LOWERED_PISTOL"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_AIM_CROUCHING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_CROUCH_AIM_HOLD"), (int) ResourceManager.get("CHANNEL_POLICE_CROUCH_AIM_HOLD"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FIRING_CROUCHING:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_AIM_CROUCHING);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_MELEE:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_ALERTED);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FALLING_FORWARD:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_IDLE_FORWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_IDLE_FORWARDS"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_FALLING_BACKWARD:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_POLICE_KNOCKDOWN_IDLE_BACKWARDS"), (int) ResourceManager.get("CHANNEL_POLICE_KNOCKDOWN_IDLE_BACKWARDS"), 4);
          break;
        case GameObjectPolice.AnimState.ANIMSTATE_LAND:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_INACTIVE);
          break;
      }
    }

    protected void updateTestAnims(int timeStepMillis)
    {
      this.m_curAnimTime -= timeStepMillis;
      if (this.m_curAnimTime > 0)
        return;
      this.m_curAnimIdx = (this.m_curAnimIdx + 1) % 15;
      this.startBlendedAnim(this.testAnims[this.m_curAnimIdx], this.testChannels[this.m_curAnimIdx], 4);
      this.m_curAnimTime = 5000;
    }

    protected void alertBrainUpdate(int timeStepMillis)
    {
      if (this.shouldSwitchPosture(timeStepMillis))
      {
        this.switchPosture();
      }
      else
      {
        if (!this.shouldTryShot(timeStepMillis))
          return;
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_AIMING);
      }
    }

    protected void aimBrainUpdate(int timeStepMillis)
    {
      if (this.m_player.isDead())
      {
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_IDLE);
      }
      else
      {
        if (this.m_player.isLying() || !this.checkLOSWithPlayer() || (double) Math.Abs(this.m_player.m_position.y - this.m_position.y) > 2.0)
          return;
        this.m_aimingTimer -= timeStepMillis;
        if (this.m_aimingTimer > 0)
          return;
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_FIRING);
      }
    }

    protected void fireBrainUpdate(int timeStepMillis)
    {
      this.m_shotDelay -= timeStepMillis;
      if (this.m_shotDelay > 0)
        return;
      if ((double) ((float) GameObjectPolice.rand.NextDouble() * 100f) < (double) this.SHOT_REPEAT_CHANCE)
      {
        if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_STANDING)
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FIRING_STANDING);
        else
          this.animStateTransition(GameObjectPolice.AnimState.ANIMSTATE_FIRING_CROUCHING);
        this.shoot();
        this.m_shotDelay = this.SHOT_DELAY;
      }
      else
        this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_ALERTED);
    }

    protected bool shouldSwitchPosture(int timeStepMillis)
    {
      this.m_lastPostureChangeDelay -= timeStepMillis;
      if (this.m_lastPostureChangeDelay <= 0)
      {
        this.m_postureChangeTimer -= timeStepMillis;
        if (this.m_postureChangeTimer <= 0)
        {
          this.m_postureChangeTimer += this.POSTURE_DECISION_TIME;
          bool flag = (double) ((float) GameObjectPolice.rand.NextDouble() * 100f) < (double) this.POSTURE_CHANGE_CHANCE;
          if (flag)
            this.m_lastPostureChangeDelay += this.POSTURE_CHANGE_DELAY;
          return flag;
        }
      }
      return false;
    }

    protected void switchPosture()
    {
      if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_STANDING)
      {
        if (16.0 > (double) this.getDistanceToSq((GameObject) this.m_map.getPlayerObject()))
          return;
        this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_CROUCHING);
      }
      else
        this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_STANDING);
    }

    protected bool shouldTryShot(int timeStepMillis)
    {
      this.m_fireDelay -= timeStepMillis;
      if (this.m_fireDelay <= 0)
      {
        this.m_fireDecisionTimer -= timeStepMillis;
        if (this.m_fireDecisionTimer <= 0)
        {
          this.m_fireDecisionTimer += this.FIRE_DECISION_TIME;
          bool flag = (double) ((float) GameObjectPolice.rand.NextDouble() * 100f) < (double) this.FIRE_CHANCE;
          if (flag)
            this.m_fireDelay += this.FIRE_DELAY;
          return flag;
        }
      }
      return false;
    }

    protected void patrolBrainUpdate(int timeStepMillis)
    {
      MathVector mathVector = (new MathVector(this.m_path.getPoint(this.m_nextPoint).m_position) - this.m_position) with
      {
        y = 0.0f
      };
      if ((double) mathVector.getLength() <= 0.40000000596046448)
      {
        this.m_nextPoint = (this.m_nextPoint + 1) % this.m_path.getPointCount();
        mathVector = this.m_path.getPoint(this.m_nextPoint).m_position - this.m_position;
      }
      mathVector.y = 0.0f;
      mathVector.setLength(1f);
      this.m_velocity.x = mathVector.x;
      this.m_velocity.z = mathVector.z;
      float num1 = (float) Math.Atan2((double) this.m_velocity.z, (double) this.m_velocity.x) - 1.57079637f;
      float num2 = 6.28318548f;
      if ((double) num1 < 0.0)
        num1 += num2;
      else if ((double) num1 > (double) num2)
        num1 -= num2;
      this.m_facingDest = num1;
      if (!this.sightedPlayer())
        return;
      this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_STANDING);
      this.brainStateTransition(GameObjectPolice.BrainState.BRAINSTATE_AIMING);
      this.setNoVelocity();
    }

    protected bool sightedPlayer()
    {
      bool flag1 = this.facingPlayer();
      bool flag2 = this.m_distanceToPlayerSq < GlobalMembersGameObjectPolice.CLOSE_PLAYER_DIST_SQ;
      bool flag3 = this.playerInRange();
      bool flag4 = this.checkLOSWithPlayer();
      bool flag5 = this.m_player.isDead();
      bool flag6 = this.m_player.isSliding();
      bool flag7 = this.m_player.isKicking();
      bool flag8 = this.m_player.isRolling();
      return flag2 && flag7 || flag3 && flag4 && !flag5 && (flag1 || !flag6 && !flag7 && !flag8);
    }

    public void playRandomGruntSound()
    {
      AppEngine.getCanvas().rand(0, 100);
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      int num = AppEngine.getCanvas().rand(0, 100);
      int eventID = num >= 20 ? (num >= 40 ? (num >= 60 ? (num >= 80 ? (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_5") : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_4")) : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_3")) : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_2")) : (int) ResourceManager.get("SOUNDEVENT_SFX_COMBAT_SUCCESS_1");
      soundManager.playEventAt(eventID, this.m_position.x, this.m_position.y, this.m_position.z);
    }

    protected bool playerInRange()
    {
      int num = this.facingPlayer() ? this.TRIGGER_RANGE_FRONT : this.TRIGGER_RANGE_REAR;
      return this.m_distanceToPlayerSq < num * num;
    }

    protected bool facingPlayer()
    {
      float num = 3.14159274f;
      if ((double) this.m_player.m_position.x < (double) this.m_position.x)
      {
        if ((double) this.m_currentFacing < (double) num)
          return true;
      }
      else if ((double) this.m_currentFacing > (double) num)
        return true;
      return false;
    }

    protected void shoot()
    {
      float num = this.m_state != GameObjectNPC.NPCState.NPCSTATE_STANDING ? 1.2f : 1.6f;
      MathVector velocity = new MathVector();
      velocity.z += 12f;
      velocity.rotateYAxis(this.m_currentFacing);
      this.m_map.addObject((GameObject) new GameObjectBullet(this.m_map, this.m_position.x, this.m_position.y + num, this.m_position.z, velocity));
      AppEngine canvas = AppEngine.getCanvas();
      canvas.getSoundManager().playEventAt(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_GUNSHOTS"), (int) ResourceManager.get("SOUNDEVENTPOOL_GUNSHOTS_GLOCK")), this.m_position.x, this.m_position.y, this.m_position.z);
    }

    protected void facePlayer()
    {
      if ((double) this.m_player.m_position.x < (double) this.m_position.x)
        this.setFacingDir(GameObjectRunner.FacingDir.FACING_LEFT);
      else
        this.setFacingDir(GameObjectRunner.FacingDir.FACING_RIGHT);
    }

    public enum BrainState
    {
      BRAINSTATE_IDLE,
      BRAINSTATE_PATROL,
      BRAINSTATE_ALERTED,
      BRAINSTATE_AIMING,
      BRAINSTATE_FIRING,
      BRAINSTATE_MELEE,
      BRAINSTATE_FALL,
      BRAINSTATE_GETUP,
      BRAINSTATE_DISARMED,
      BRAINSTATE_HIT_FROM_PRONE_JUMPUP,
      BRAINSTATE_DEAD,
    }

    public enum AnimState
    {
      ANIMSTATE_IDLE,
      ANIMSTATE_WALKING,
      ANIMSTATE_RUNNING,
      ANIMSTATE_ALERTED,
      ANIMSTATE_LOWERED_STANDING,
      ANIMSTATE_AIM_STANDING,
      ANIMSTATE_FIRING_STANDING,
      ANIMSTATE_LOWERED_CROUCHING,
      ANIMSTATE_AIM_CROUCHING,
      ANIMSTATE_FIRING_CROUCHING,
      ANIMSTATE_MELEE,
      ANIMSTATE_DISARMED,
      ANIMSTATE_FALLING_FORWARD,
      ANIMSTATE_FALLING_FORWARD_HIGH,
      ANIMSTATE_FALLING_BACKWARD,
      ANIMSTATE_FALLING_BACKWARD_HIGH,
      ANIMSTATE_GETUP_FORWARD,
      ANIMSTATE_GETUP_BACKWARD,
      ANIMSTATE_GETUP_MELEED,
      ANIMSTATE_LONGFALL_FORWARDS,
      ANIMSTATE_LONGFALL_BACKWARDS,
      ANIMSTATE_LAND,
    }
  }
}
