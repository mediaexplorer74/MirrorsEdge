
// Type: game.GameObjectEffectPigeonTakeoff_Pigeon
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using midp;
using support;
using System;

#nullable disable
namespace game
{
  public class GameObjectEffectPigeonTakeoff_Pigeon
  {
    private GameObjectEffectPigeonTakeoff m_pigeonGroup;
    private Node m_pigeonNode;
    private AnimPlayer3D m_animPlayer;
    private MathVector m_startPosition;
    private MathVector m_currentPosition;
    private MathVector m_flightDirection;
    private float m_startSpeed;
    private float m_endSpeed;
    private float m_idleRotation;
    private int m_rotationProgress;
    private float m_flightRotation;
    private GameObjectEffectPigeonTakeoff_Pigeon.SpookState m_spookState;
    private int m_stateTime;
    private int m_totalStateTime;
    public readonly int m_index;

    public GameObjectEffectPigeonTakeoff_Pigeon(
      int index,
      GameObjectEffectPigeonTakeoff pigeonGroup,
      Group parentGroup,
      float posX,
      float posY,
      float posZ)
    {
      this.m_index = index;
      this.m_pigeonGroup = pigeonGroup;
      this.m_startPosition = new MathVector(posX, posY, posZ);
      this.m_currentPosition = new MathVector(posX, posY, posZ);
      this.m_flightDirection = new MathVector();
      this.m_endSpeed = 0.0f;
      this.m_startSpeed = 0.0f;
      this.m_idleRotation = 0.0f;
      this.m_rotationProgress = 0;
      this.m_flightRotation = 0.0f;
      this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_NONE;
      this.m_stateTime = 0;
      this.m_totalStateTime = 0;
      AppEngine canvas = AppEngine.getCanvas();
      this.m_pigeonNode = AppEngine.getM3GAssets().loadModel((int) M3GAssets.get("MODEL_PIGEON"), 4);
      M3GAssets.addNode(parentGroup, this.m_pigeonNode);
      this.m_animPlayer = new AnimPlayer3D(canvas.getAnimationManager3D());
      this.m_animPlayer.setNode(this.m_pigeonNode);
      this.m_animPlayer.startAnim((int) ResourceManager.get("ANIM3D_PIGEON_IDLE_01"), 20);
      int animDuration = this.m_animPlayer.getAnimDuration();
      this.m_animPlayer.setAnimTime(canvas.rand(0, animDuration - 1));
      this.m_startPosition.x += canvas.randFloat(-0.2f, 0.2f);
      this.m_startPosition.z += canvas.randFloat(-0.2f, 0.2f);
      this.m_currentPosition = this.m_startPosition;
      this.m_pigeonNode.setTranslation(this.m_startPosition.x, this.m_startPosition.y, this.m_startPosition.z);
      this.m_pigeonNode.setScale(0.01f, 0.01f, 0.01f);
      this.m_idleRotation = (float) canvas.rand(0, 350);
      this.m_pigeonNode.setOrientation(this.m_idleRotation, 0.0f, 1f, 0.0f);
    }

    public virtual void Destructor()
    {
      if (this.m_pigeonNode == null)
        return;
      this.m_pigeonGroup = (GameObjectEffectPigeonTakeoff) null;
      if (this.m_animPlayer != null)
      {
        this.m_animPlayer.Destructor();
        this.m_animPlayer = (AnimPlayer3D) null;
      }
      this.m_pigeonNode.Destructor();
      this.m_pigeonNode = (Node) null;
    }

    public void reset()
    {
      this.m_currentPosition = this.m_startPosition;
      this.m_rotationProgress = 0;
      this.m_pigeonNode.setRenderingEnable(true);
      this.m_pigeonNode.setTranslation(this.m_startPosition.x, this.m_startPosition.y, this.m_startPosition.z);
      this.m_pigeonNode.setOrientation(this.m_idleRotation, 0.0f, 1f, 0.0f);
      this.m_animPlayer.startAnim((int) ResourceManager.get("ANIM3D_PIGEON_IDLE_01"), 20);
      this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_NONE;
    }

    public void takeoff(MathVector playerPosition)
    {
      AppEngine canvas = AppEngine.getCanvas();
      MathVector mathVector = this.m_startPosition - playerPosition;
      float length = mathVector.getLength();
      this.m_flightDirection.x = mathVector.x * canvas.randFloat(1f, 4f);
      this.m_flightDirection.y = length * canvas.randFloat(0.1f, 1f);
      this.m_flightDirection.z = mathVector.z * canvas.randFloat(1f, 4f);
      this.m_flightDirection.normalise();
      this.m_totalStateTime = canvas.rand(0, 500);
      this.m_startSpeed = canvas.randFloat(3f, 6f);
      this.m_endSpeed = canvas.randFloat(8f, 12f);
      this.m_totalStateTime = (int) (0.0 + (double) Math.Max(0.0f, Math.Min(1f, (float) (((double) mathVector.getLengthSq() - 30.0) / 15.0))) * 200.0);
      this.m_stateTime = 0;
      this.m_flightRotation = JMath.toDegrees((float) Math.Atan2((double) this.m_flightDirection.x, (double) this.m_flightDirection.z));
      if (180.0 < (double) Math.Abs(this.m_idleRotation - this.m_flightRotation))
      {
        if ((double) this.m_idleRotation < (double) this.m_flightRotation)
          this.m_flightRotation -= 360f;
        else
          this.m_flightRotation += 360f;
      }
      this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_UNAWARE;
    }

    public void update(int timeStepMillis)
    {
      if (this.m_spookState == GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_DISABLED)
        return;
      this.m_stateTime += timeStepMillis;
      switch (this.m_spookState)
      {
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_TAKEOFF:
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_ACCELERATING:
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_CRUISING:
          if (this.m_rotationProgress < 1000)
          {
            this.m_rotationProgress = Math.Min(1000, this.m_rotationProgress + timeStepMillis);
            float num = (float) this.m_rotationProgress / 1000f;
            this.m_pigeonNode.setOrientation(this.m_idleRotation + (float) ((double) num * (double) num * ((double) this.m_flightRotation - (double) this.m_idleRotation)), 0.0f, 1f, 0.0f);
            break;
          }
          break;
      }
      switch (this.m_spookState)
      {
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_UNAWARE:
          if (this.m_totalStateTime <= this.m_stateTime)
          {
            this.m_animPlayer.startAnim((int) ResourceManager.get("ANIM3D_PIGEON_TAKEOFF"), 16);
            this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_TAKEOFF;
            break;
          }
          break;
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_TAKEOFF:
          if (!this.m_animPlayer.isAnimating())
          {
            this.m_animPlayer.startAnim((int) ResourceManager.get("ANIM3D_PIGEON_FLYING_LOOP"), 20);
            this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_ACCELERATING;
            this.m_stateTime = 0;
            this.m_pigeonGroup.pigeonTakenOff();
          }
          MathVector mathVector = new MathVector(this.m_flightDirection) * (float) (1.0 * (double) timeStepMillis * (1.0 / 1000.0));
          this.m_currentPosition.x += mathVector.x;
          this.m_currentPosition.z += mathVector.z;
          this.m_pigeonNode.setTranslation(this.m_currentPosition.x, this.m_currentPosition.y, this.m_currentPosition.z);
          break;
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_ACCELERATING:
          if (this.m_totalStateTime <= this.m_stateTime)
          {
            this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_CRUISING;
            this.m_stateTime = 0;
            break;
          }
          float num1 = (float) this.m_stateTime / (float) this.m_totalStateTime;
          this.m_currentPosition += new MathVector(this.m_flightDirection) * (float) ((double) (this.m_startSpeed + (float) ((double) num1 * (double) num1 * ((double) this.m_endSpeed - (double) this.m_startSpeed))) * (double) timeStepMillis * (1.0 / 1000.0));
          this.m_pigeonNode.setTranslation(this.m_currentPosition.x, this.m_currentPosition.y, this.m_currentPosition.z);
          break;
        case GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_CRUISING:
          if (60000.0 < (double) this.m_stateTime)
          {
            this.m_spookState = GameObjectEffectPigeonTakeoff_Pigeon.SpookState.SPOOK_STATE_DISABLED;
            this.m_pigeonNode.setRenderingEnable(false);
          }
          this.m_currentPosition += new MathVector(this.m_flightDirection) * (float) ((double) this.m_endSpeed * (double) timeStepMillis * (1.0 / 1000.0));
          this.m_pigeonNode.setTranslation(this.m_currentPosition.x, this.m_currentPosition.y, this.m_currentPosition.z);
          break;
      }
      MathVector min = new MathVector(-2f, -2f, -2f) + this.m_currentPosition;
      MathVector max = new MathVector(2f, 2f, 2f) + this.m_currentPosition;
      if (AppEngine.getCanvas().getSceneGame().getCameraFrustum().intersectAABB(min, max) != -1)
      {
        this.m_animPlayer.updateAnim(timeStepMillis);
        this.m_pigeonNode.setRenderingEnable(true);
      }
      else
        this.m_pigeonNode.setRenderingEnable(false);
    }

    private enum SpookState
    {
      SPOOK_STATE_NONE,
      SPOOK_STATE_UNAWARE,
      SPOOK_STATE_TAKEOFF,
      SPOOK_STATE_ACCELERATING,
      SPOOK_STATE_CRUISING,
      SPOOK_STATE_DISABLED,
    }
  }
}
