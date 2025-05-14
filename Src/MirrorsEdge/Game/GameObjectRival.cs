// Decompiled with JetBrains decompiler
// Type: game.GameObjectRival
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using System;

#nullable disable
namespace game
{
  public class GameObjectRival : GameObjectNPC
  {
    public const int UPDATE_RANGESQ = 400;
    protected const int numAnims = 8;
    protected const float PATH_NODE_NEAR = 0.7f;
    protected const float RIVAL_RUN_SPEED = 9.5f;
    protected const float SCALE_SPEED_RANGE_SQ = 225f;
    private ChunkDynamic m_dynamicNode;
    protected new GameObjectNPC.NPCState m_state;
    protected GameObjectRival.BrainState m_brainState;
    protected GameObjectRival.BrainState m_prefallState;
    protected GameObjectRival.AnimState m_animState;
    protected new int m_stateTime;
    protected float m_speed;
    protected bool m_triggeredCombat;
    protected CollShape m_standingShape;
    protected CollShape m_crouchingShape;
    protected readonly int[] testAnims = new int[8]
    {
      (int) ResourceManager.get("ANIM3D_FAITH_IDLE"),
      (int) ResourceManager.get("ANIM3D_FAITH_RUN_SLOW"),
      (int) ResourceManager.get("ANIM3D_FAITH_RUN_MEDIUM"),
      (int) ResourceManager.get("ANIM3D_FAITH_RUN_FAST"),
      (int) ResourceManager.get("ANIM3D_FAITH_JUMP_PREJUMP"),
      (int) ResourceManager.get("ANIM3D_FAITH_JUMP_RISING"),
      (int) ResourceManager.get("ANIM3D_FAITH_JUMP_FALLING"),
      (int) ResourceManager.get("ANIM3D_FAITH_JUMP_LANDING")
    };
    protected readonly int[] testChannels = new int[8]
    {
      (int) ResourceManager.get("CHANNEL_FAITH_IDLE"),
      (int) ResourceManager.get("CHANNEL_FAITH_RUN_SLOW"),
      (int) ResourceManager.get("CHANNEL_FAITH_RUN_MEDIUM"),
      (int) ResourceManager.get("CHANNEL_FAITH_RUN_FAST"),
      (int) ResourceManager.get("CHANNEL_FAITH_JUMP_PREJUMP"),
      (int) ResourceManager.get("CHANNEL_FAITH_JUMP_RISING"),
      (int) ResourceManager.get("CHANNEL_FAITH_JUMP_FALLING"),
      (int) ResourceManager.get("CHANNEL_FAITH_JUMP_LANDING")
    };
    protected int m_curAnimIdx;
    protected int m_curAnimTime;
    protected Path m_path;
    protected int m_nextPathNode;
    protected CollShape m_rampShape;
    protected MathVector m_rampStart = new MathVector();
    protected MathVector m_rampEnd = new MathVector();
    protected int m_hitCount;
    protected int m_distanceToPlayerSq;

    public GameObjectRival(
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
      this.m_dynamicNode = (ChunkDynamic) null;
      this.m_state = GameObjectNPC.NPCState.NPCSTATE_INACTIVE;
      this.m_stateTime = 0;
      this.m_brainState = GameObjectRival.BrainState.BRAINSTATE_IDLE;
      this.m_animState = GameObjectRival.AnimState.ANIMSTATE_IDLE;
      this.m_standingShape = (CollShape) null;
      this.m_crouchingShape = (CollShape) null;
      this.m_curAnimIdx = -1;
      this.m_curAnimTime = -1;
      this.m_path = this.m_map.getPath(pathId);
      this.m_nextPathNode = 0;
      this.m_distanceToPlayerSq = 0;
      this.m_rampShape = (CollShape) null;
      this.m_rampStart = new MathVector();
      this.m_rampEnd = new MathVector();
      this.m_speed = 9.5f;
      this.m_triggeredCombat = false;
      this.m_prefallState = GameObjectRival.BrainState.BRAINSTATE_IDLE;
      this.m_hitCount = 0;
      this.setVisualAssets(modelId, blenderId);
      this.m_standingShape = (CollShape) new CollOctahedron(-0.5f, 0.0f, -0.5f, 0.5f, 1.8f, 0.5f);
      this.m_standingShape.setBounce(0.0f);
      this.m_crouchingShape = (CollShape) new CollOctahedron(-0.5f, 0.0f, -0.5f, 0.5f, 0.5f, 0.5f);
      this.m_crouchingShape.setBounce(0.0f);
      this.m_dynamicNode = this.m_map.getForegroundLayer().addDynamicNode(this.m_objectNode, (GameObject) this);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FOLLOW);
      this.m_facingRotateSpeed = 12.566371f;
    }

    public override void Destructor()
    {
      if (this.m_dynamicNode == null)
        return;
      this.m_map.getForegroundLayer().removeDynamicNode(this.m_dynamicNode);
      this.m_dynamicNode = (ChunkDynamic) null;
      this.m_standingShape.Destructor();
      this.m_standingShape = (CollShape) null;
      this.m_crouchingShape.Destructor();
      this.m_crouchingShape = (CollShape) null;
      this.m_rampShape = (CollShape) null;
      base.Destructor();
    }

    public override void resetLevel()
    {
      base.resetLevel();
      this.m_stateTime = 0;
      this.m_nextPathNode = 0;
      this.m_triggeredCombat = false;
      this.m_prefallState = GameObjectRival.BrainState.BRAINSTATE_IDLE;
      this.m_hitCount = 0;
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FOLLOW);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
    }

    public override void collidedWith(GameObject other)
    {
      if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_INACTIVE || this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_FALL || this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_GETUP || other.getType() != 0)
        return;
      GameObjectPlayer playerObject = this.m_map.getPlayerObject();
      bool flag = playerObject.isSliding() || playerObject.isKicking() || playerObject.isRolling();
      if (this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_FOLLOW && flag && this.m_state != GameObjectNPC.NPCState.NPCSTATE_JUMPING && this.m_state != GameObjectNPC.NPCState.NPCSTATE_FALLING && this.m_state != GameObjectNPC.NPCState.NPCSTATE_RAMP_SLIDE_DOWN)
      {
        this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FALL);
        ++this.m_hitCount;
        AppEngine.getAchievementData().registerRivalKill(AppEngine.getCanvas().getSceneGame().getRaceTime());
        AppEngine canvas = AppEngine.getCanvas();
        canvas.getSoundManager().playEvent(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_MELEE"), 1));
      }
      else
      {
        if (this.m_brainState != GameObjectRival.BrainState.BRAINSTATE_SLIDE || playerObject.isMeleed())
          return;
        playerObject.meleeByCop((GameObjectNPC) this);
        AppEngine canvas = AppEngine.getCanvas();
        canvas.getSoundManager().playEvent(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_MELEE"), 1));
      }
    }

    public override void update(int timeStepMillis)
    {
      GameObjectPlayer playerObject = this.m_map.getPlayerObject();
      this.m_distanceToPlayerSq = (int) this.getDistanceToSq((GameObject) playerObject);
      this.updateDrawing();
      this.testVFC();
      this.m_animateBlender = this.m_passedVFC;
      if (this.m_passedVFC)
      {
        this.m_animateBlender = this.m_passedVFC;
        this.m_objectNode.setRenderingEnable(true);
      }
      else
        this.m_objectNode.setRenderingEnable(false);
      if (!this.shouldUpdate())
      {
        this.m_animateBlender = false;
        this.m_objectNode.setRenderingEnable(this.m_firstAlwaysPass);
        this.m_firstAlwaysPass = false;
      }
      else
      {
        base.update(timeStepMillis);
        this.updateBrain(timeStepMillis);
        this.updateState(timeStepMillis);
        this.updatePosition(timeStepMillis);
        this.updateAnimState(timeStepMillis);
        this.updateFacing(timeStepMillis);
        bool flag = false;
        if ((double) playerObject.m_position.x < (double) this.m_position.x == (double) this.m_currentFacing < Math.PI)
          flag = (double) this.m_distanceToPlayerSq <= 144.0;
        else if (this.getType() != 5)
          flag = (double) this.m_distanceToPlayerSq <= 2.25;
        if (this.m_triggeredCombat || (double) Math.Abs(this.m_position.z) > 0.5 || !flag || (double) Math.Abs(this.m_position.y - playerObject.m_position.y) > 4.0 || !this.checkLOSWithPlayer())
          return;
        this.m_triggeredCombat = true;
        AppEngine.getCanvas().getSceneGame().startCombatTime();
      }
    }

    protected void updateDrawing() => this.m_animateBlender = this.m_dynamicNode.isVisible();

    protected virtual bool shouldUpdate()
    {
      return this.m_state != GameObjectNPC.NPCState.NPCSTATE_INACTIVE && AppEngine.getCanvas().getSceneGame().getState() != SceneGame.GameState.STATE_INTRO && this.m_distanceToPlayerSq <= 400;
    }

    protected void updateBrain(int timeStepMillis)
    {
      switch (this.m_brainState)
      {
        case GameObjectRival.BrainState.BRAINSTATE_FOLLOW:
        case GameObjectRival.BrainState.BRAINSTATE_SLIDE:
          this.brainUpdateFollowPath(timeStepMillis);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_JUMP_TO:
          this.brainUpdateOnRails(timeStepMillis);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_WALL_RUN:
          this.brainUpdateOnRails(timeStepMillis);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_WALL_CLIMB:
          this.brainUpdateClimb(timeStepMillis);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_CLAMBER:
          this.brainUpdateClamber(timeStepMillis);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_FALL:
          if (this.m_objectAnimPlayer.isAnimating() || this.getType() != 5)
            break;
          if (this.m_hitCount < 3)
          {
            this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_GETUP);
            break;
          }
          SceneGame sceneGame = AppEngine.getCanvas().getSceneGame();
          if (sceneGame.getState() != SceneGame.GameState.STATE_GAME)
            break;
          sceneGame.completeLevel();
          break;
        case GameObjectRival.BrainState.BRAINSTATE_GETUP:
          if (this.m_objectAnimPlayer.isAnimating())
            break;
          this.brainStateTransition(this.m_prefallState);
          break;
      }
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

    protected void updateAnimState(int timeStepMillis)
    {
      switch (this.m_animState)
      {
      }
    }

    protected void updatePosition(int timeStepMillis)
    {
      float timeStepSecs = (float) timeStepMillis / 1000f;
      float num1 = 1f;
      if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_RAMP_RUN_UP || this.m_state == GameObjectNPC.NPCState.NPCSTATE_RAMP_SLIDE_DOWN)
      {
        MathVector mathVector1 = (this.m_rampEnd - this.m_rampStart) with
        {
          z = 0.0f
        };
        mathVector1.setLength(this.m_speed * num1);
        GameObjectRival gameObjectRival = this;
        gameObjectRival.m_position = gameObjectRival.m_position + mathVector1 * timeStepSecs;
        MathVector mathVector2 = (this.m_rampEnd - this.m_rampStart) with
        {
          z = 0.0f
        };
        MathVector mathVector3 = (this.m_position - this.m_rampStart) with
        {
          z = 0.0f
        };
        if ((double) mathVector2.getLengthSq() < (double) mathVector3.getLengthSq())
        {
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_RUNNING);
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_RUNNING);
        }
        this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      }
      else
      {
        MathVector collisionNormal = new MathVector();
        CollShape collidedShape = (CollShape) null;
        MathVector velocity = new MathVector(this.m_velocity * num1);
        int num2 = this.m_map.moveObjectPhysics(ref this.m_position, ref velocity, timeStepSecs, this.m_globalShape, ref collidedShape, ref collisionNormal);
        this.m_velocity = velocity;
        this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
        if ((num2 & 4) != 0)
        {
          if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_FALLING)
          {
            this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_RUNNING);
            if (this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_JUMP_OVER)
              this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FOLLOW);
          }
          if (collidedShape.isRamp() && (collidedShape.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X && (double) this.m_velocity.x > 0.0 || collidedShape.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_NEG_X && (double) this.m_velocity.x < 0.0))
          {
            this.m_rampShape = collidedShape;
            this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_RAMP_RUN_UP);
            this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_RAMP_RUN_UP);
            return;
          }
        }
        else if ((double) this.m_velocity.y < 0.0)
        {
          CollShape attributeShapeAt = this.m_map.getAttributeShapeAt(new MathLine(this.m_position.x, this.m_position.y, this.m_position.z, 0.0f, -1f, 0.0f), 0);
          if (attributeShapeAt != null && attributeShapeAt.isRamp() && (double) Math.Abs(this.m_position.y - attributeShapeAt.getBounds().max.y) < 0.5)
          {
            if (attributeShapeAt.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X && (double) this.m_velocity.x < 0.0 || attributeShapeAt.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_NEG_X && (double) this.m_velocity.x > 0.0)
            {
              this.m_rampShape = attributeShapeAt;
              this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_RAMP_SLIDE_DOWN);
              this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_RAMP_SLIDE);
              return;
            }
          }
          else
            this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_FALLING);
        }
        bool flag1 = (num2 & 2) != 0;
        bool flag2 = (num2 & 1) != 0;
        if ((!flag1 || (double) this.m_currentFacing <= Math.PI) && (!flag2 || (double) this.m_currentFacing >= Math.PI))
          return;
        float num3 = Math.Abs((double) this.m_currentFacing - Math.PI / 2.0) >= 0.0099999997764825821 ? 1f : -1f;
        MathVector mathVector = new MathVector(this.getForwardDirection());
        MathVector startDirection = new MathVector(0.0f, 1f, 0.0f);
        MathLine line = new MathLine(mathVector * (0.51f * num3) + this.m_position, startDirection);
        MathOrthoBox localBounds = this.m_globalShape.getLocalBounds();
        float y1 = localBounds.min.y;
        float y2 = localBounds.max.y;
        if (!this.m_map.intersects(line, 0, ref y1, ref y2) || (double) y2 >= (double) localBounds.max.y)
          return;
        this.m_position.y += y2 + 0.2f;
        this.setNoVelocity();
        this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_CLAMBER);
      }
    }

    protected override void stateTransition(GameObjectNPC.NPCState newState)
    {
      if (this.m_state == newState)
        return;
      if (this.m_state == GameObjectNPC.NPCState.NPCSTATE_RAMP_RUN_UP || this.m_state == GameObjectNPC.NPCState.NPCSTATE_RAMP_SLIDE_DOWN)
        this.m_objectNode.setOrientation(0.0f, 0.0f, 0.0f, 1f);
      switch (newState)
      {
        case GameObjectNPC.NPCState.NPCSTATE_CROUCHING:
          this.m_globalShape = this.m_crouchingShape;
          break;
        case GameObjectNPC.NPCState.NPCSTATE_RUNNING:
          this.m_globalShape = this.m_standingShape;
          break;
        case GameObjectNPC.NPCState.NPCSTATE_RAMP_RUN_UP:
          this.m_rampStart = this.m_position;
          MathOrthoBox bounds1 = this.m_rampShape.getBounds();
          this.m_rampEnd = this.m_rampShape.getShapeType() != CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X ? new MathVector(bounds1.min.x, bounds1.max.y, 0.0f) : bounds1.max;
          this.m_rampShape = (CollShape) null;
          break;
        case GameObjectNPC.NPCState.NPCSTATE_RAMP_SLIDE_DOWN:
          this.m_rampStart = this.m_position;
          MathOrthoBox bounds2 = this.m_rampShape.getBounds();
          if (this.m_rampShape.getShapeType() == CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X)
          {
            this.m_rampEnd = bounds2.min;
            this.m_objectNode.setOrientation(30f, 0.0f, 0.0f, 1f);
          }
          else
          {
            this.m_rampEnd = new MathVector(bounds2.max.x, bounds2.min.y, 0.0f);
            this.m_objectNode.setOrientation(-30f, 0.0f, 0.0f, 1f);
          }
          this.m_rampEnd.y += 0.3f;
          this.m_rampShape = (CollShape) null;
          break;
      }
      this.m_state = newState;
      this.m_stateTime = 0;
    }

    protected void brainStateTransition(GameObjectRival.BrainState newState)
    {
      if (this.m_brainState == newState)
        return;
      switch (newState)
      {
        case GameObjectRival.BrainState.BRAINSTATE_FOLLOW:
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_RUNNING);
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_RUNNING);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_JUMP_OVER:
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_JUMPING);
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_JUMPING);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_JUMP_TO:
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_JUMPING);
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_JUMPING);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_SLIDE:
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_SLIDING);
          this.stateTransition(GameObjectNPC.NPCState.NPCSTATE_CROUCHING);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_WALL_RUN:
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_WALLRUNNING);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_WALL_CLIMB:
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_CLIMBING);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_CLAMBER:
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_CLAMBER);
          break;
        case GameObjectRival.BrainState.BRAINSTATE_FALL:
          this.setNoVelocity();
          this.m_prefallState = this.m_brainState;
          this.animStateTransition(GameObjectRival.AnimState.ANIMSTATE_FALL);
          break;
      }
      this.m_brainState = newState;
    }

    protected void animStateTransition(GameObjectRival.AnimState newState)
    {
      if (this.m_animState == newState)
        return;
      switch (newState)
      {
        case GameObjectRival.AnimState.ANIMSTATE_RUNNING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_RUN_MEDIUM"), (int) ResourceManager.get("CHANNEL_FAITH_RUN_MEDIUM"), 4);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_JUMPING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_JUMP_RISING"), (int) ResourceManager.get("CHANNEL_FAITH_JUMP_RISING"), 16);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_SLIDING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_SLIDE_LOOP"), (int) ResourceManager.get("CHANNEL_FAITH_SLIDE_LOOP"), 16);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_WALLRUNNING:
          if ((double) this.m_velocity.x > 0.0)
          {
            this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_WALLRUN_RIGHT_LOOP"), (int) ResourceManager.get("CHANNEL_FAITH_WALLRUN_RIGHT_LOOP"), 4);
            break;
          }
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_WALLRUN_LEFT_LOOP"), (int) ResourceManager.get("CHANNEL_FAITH_WALLRUN_LEFT_LOOP"), 4);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_CLIMBING:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_VERTICAL_RUN"), (int) ResourceManager.get("CHANNEL_FAITH_VERTICAL_RUN"), 4);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_CLAMBER:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_CLAMBER_HALF_METRE"), (int) ResourceManager.get("CHANNEL_FAITH_CLAMBER_HALF_METRE"), 16);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_RAMP_SLIDE:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_WALL_SLIDE_OPTIMAL"), (int) ResourceManager.get("CHANNEL_FAITH_WALLSLIDE_OPTIMAL"), 4);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_RAMP_RUN_UP:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_RUN_UPHILL"), (int) ResourceManager.get("CHANNEL_FAITH_RUN_UPHILL"), 4);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_FALL:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_MELEE_FAIL"), (int) ResourceManager.get("CHANNEL_FAITH_MELEE_FAIL"), 16);
          break;
        case GameObjectRival.AnimState.ANIMSTATE_GETUP:
          this.startBlendedAnim((int) ResourceManager.get("ANIM3D_FAITH_PRONE_RECOVER"), (int) ResourceManager.get("CHANNEL_FAITH_PRONE_RECOVER"), 16);
          break;
      }
      this.m_animState = newState;
    }

    protected void updateTestAnims(int timeStepMillis)
    {
      this.m_curAnimTime -= timeStepMillis;
      if (this.m_curAnimTime > 0)
        return;
      this.m_curAnimIdx = (this.m_curAnimIdx + 1) % 8;
      this.startBlendedAnim(this.testAnims[this.m_curAnimIdx], this.testChannels[this.m_curAnimIdx], 4);
      this.m_curAnimTime = 5000;
    }

    protected bool hasPastPathNode()
    {
      if ((double) new MathVector(this.getVectorToNextPathNode())
      {
        y = 0.0f
      }.getLength() <= 0.699999988079071)
        return true;
      if (this.m_nextPathNode > 0)
      {
        PathPoint point1 = this.m_path.getPoint(this.m_nextPathNode - 1);
        PathPoint point2 = this.m_path.getPoint(this.m_nextPathNode);
        MathVector mathVector1 = new MathVector(point1.m_position);
        MathVector mathVector2 = new MathVector(new MathVector(point2.m_position) - mathVector1);
        MathVector mathVector3 = new MathVector(this.m_position - mathVector1);
        if ((double) mathVector2.getLengthSq() < (double) mathVector3.getLengthSq())
          return true;
      }
      return false;
    }

    protected void brainUpdateFollowPath(int timeStepMillis)
    {
      if (!this.onGround())
        return;
      MathVector toNext = new MathVector(this.getVectorToNextPathNode());
      if (this.hasPastPathNode() && !this.onReachedNode(ref toNext))
        return;
      MathVector mathVector = new MathVector(toNext);
      mathVector.y = 0.0f;
      if ((double) mathVector.getLengthSq() > 0.0)
        mathVector.setLength(this.m_speed);
      this.m_velocity.x = mathVector.x;
      this.m_velocity.z = mathVector.z;
      float num1 = (float) Math.Atan2((double) this.m_velocity.z, (double) this.m_velocity.x) - 1.57079637f;
      float num2 = 6.28318548f;
      if ((double) num1 < 0.0)
        num1 += num2;
      else if ((double) num1 > (double) num2)
        num1 -= num2;
      this.m_facingDest = num1;
    }

    protected void brainUpdateClimb(int timeStepMillis)
    {
      MathVector toNext = new MathVector(this.getVectorToNextPathNode());
      if ((double) toNext.y < 0.0)
      {
        this.setUpVelocity(0.0f);
        this.onReachedNode(ref toNext);
      }
      else
      {
        this.setUpVelocity(this.m_speed);
        toNext.setLength(this.m_speed);
        this.m_velocity.x = toNext.x;
      }
    }

    protected void brainUpdateOnRails(int timeStepMillis)
    {
      MathVector toNext = new MathVector(this.getVectorToNextPathNode());
      if ((double) toNext.getLength() <= 0.699999988079071)
      {
        this.onReachedNode(ref toNext);
      }
      else
      {
        toNext.setLength(this.m_speed);
        this.m_velocity = toNext;
        float num1 = (float) Math.Atan2((double) this.m_velocity.z, (double) this.m_velocity.x) - 1.57079637f;
        float num2 = 6.28318548f;
        if ((double) num1 < 0.0)
          num1 += num2;
        else if ((double) num1 > (double) num2)
          num1 -= num2;
        this.m_facingDest = num1;
      }
    }

    protected void brainUpdateClamber(int timeStepMillis)
    {
      this.m_velocity.y = 0.0f;
      if (this.m_objectAnimPlayer.isAnimating())
        return;
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FOLLOW);
    }

    protected MathVector getVectorToNextPathNode()
    {
      return this.m_path.getPoint(this.m_nextPathNode).m_position - this.m_position;
    }

    protected bool onReachedNode(ref MathVector toNext)
    {
      PathPoint point = this.m_path.getPoint(this.m_nextPathNode);
      int type = point.m_type;
      this.m_speed = (float) point.m_time / 1000f;
      this.m_nextPathNode = (this.m_nextPathNode + 1) % this.m_path.getPointCount();
      toNext = this.getVectorToNextPathNode();
      if (this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_SLIDE || this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_WALL_CLIMB || this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_WALL_RUN || this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_JUMP_TO)
        this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FOLLOW);
      switch (type)
      {
        case 1:
          this.processJumpNode(ref toNext);
          return false;
        case 2:
          this.processJumpToNode(ref toNext);
          return false;
        case 3:
          this.processClimbNode(ref toNext);
          return true;
        case 4:
          this.processSlideNode(ref toNext);
          return true;
        case 5:
          this.processWallRunNode(ref toNext);
          return false;
        default:
          return true;
      }
    }

    public void processJumpNode(ref MathVector toNext)
    {
      this.setUpVelocity(-(float) (-9.8000001907348633 * (double) (Math.Abs(toNext.x) / Math.Abs(this.m_velocity.x)) / 2.0));
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_JUMP_OVER);
    }

    public void processJumpToNode(ref MathVector toNext)
    {
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_JUMP_TO);
    }

    public void processClimbNode(ref MathVector toNext)
    {
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_WALL_CLIMB);
    }

    public void processSlideNode(ref MathVector toNext)
    {
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_SLIDE);
    }

    public void processWallRunNode(ref MathVector toNext)
    {
      this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_WALL_RUN);
    }

    protected bool onGround()
    {
      return this.m_state != GameObjectNPC.NPCState.NPCSTATE_JUMPING && this.m_state != GameObjectNPC.NPCState.NPCSTATE_FALLING;
    }

    public enum BrainState
    {
      BRAINSTATE_IDLE,
      BRAINSTATE_FOLLOW,
      BRAINSTATE_JUMP_OVER,
      BRAINSTATE_JUMP_TO,
      BRAINSTATE_SLIDE,
      BRAINSTATE_WALL_RUN,
      BRAINSTATE_WALL_CLIMB,
      BRAINSTATE_CLAMBER,
      BRAINSTATE_FALL,
      BRAINSTATE_GETUP,
    }

    public enum AnimState
    {
      ANIMSTATE_IDLE,
      ANIMSTATE_RUNNING,
      ANIMSTATE_JUMPING,
      ANIMSTATE_LANDING,
      ANIMSTATE_SLIDING,
      ANIMSTATE_WALLRUNNING,
      ANIMSTATE_CLIMBING,
      ANIMSTATE_CLAMBER,
      ANIMSTATE_RAMP_SLIDE,
      ANIMSTATE_RAMP_RUN_UP,
      ANIMSTATE_FALL,
      ANIMSTATE_GETUP,
    }
  }
}
