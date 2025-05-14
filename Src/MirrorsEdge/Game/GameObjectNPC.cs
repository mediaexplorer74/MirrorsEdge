// Decompiled with JetBrains decompiler
// Type: game.GameObjectNPC
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace game
{
  public abstract class GameObjectNPC : GameObjectRunner
  {
    protected const float DEFAULT_TURN_SPEED = 6.28318548f;
    protected GameObjectNPC.NPCState m_state;
    protected int m_stateTime;
    protected float m_currentFacing;
    protected float m_facingDest;
    protected float m_facingRotateSpeed;

    public GameObjectNPC(MEdgeMap map, int objectType, float posX, float posY, float posZ)
      : base(map, objectType, posX, posY, posZ)
    {
      this.m_state = GameObjectNPC.NPCState.NPCSTATE_INACTIVE;
      this.m_stateTime = 0;
      this.m_currentFacing = 0.0f;
      this.m_facingDest = 0.0f;
      this.m_facingRotateSpeed = 6.28318548f;
    }

    public override void Destructor() => base.Destructor();

    public override void collidedWith(GameObject other)
    {
    }

    public override void update(int timeStepMillis)
    {
      this.m_stateTime += timeStepMillis;
      base.update(timeStepMillis);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
    }

    protected abstract void stateTransition(GameObjectNPC.NPCState newState);

    protected void startBlendedAnim(int animId, int channelId, int flags)
    {
      this.m_objectAnimPlayer.startAnim(animId, flags);
      this.m_animationBlender.setChannelWeight(channelId, 1f, 3);
    }

    protected void updateFacePlayer(int timeStepMillis)
    {
      GameObject playerObject = (GameObject) this.m_map.getPlayerObject();
      MathVector mathVector = new MathVector(playerObject.m_position.x - this.m_position.x, 0.0f, playerObject.m_position.z - this.m_position.z);
      float num1 = (float) Math.Atan2((double) mathVector.z, (double) mathVector.x) - 1.57079637f;
      float num2 = 6.28318548f;
      if ((double) num1 < 0.0)
        num1 += num2;
      else if ((double) num1 > (double) num2)
        num1 -= num2;
      this.m_facingDest = num1;
    }

    protected void updateFacing(int timeStepMillis)
    {
      float num1 = 6.28318548f;
      float num2 = 3.14159274f;
      if ((double) this.m_facingDest == (double) this.m_currentFacing)
        return;
      float num3 = 1f;
      float num4 = this.m_facingDest - this.m_currentFacing;
      if ((double) num4 < 0.0)
        num4 += num1;
      else if ((double) num4 > (double) num1)
        num4 -= num1;
      if ((double) num4 > (double) num2)
        num3 = -1f;
      float num5 = (float) timeStepMillis / 1000f;
      this.m_currentFacing += this.m_facingRotateSpeed * num3 * num5;
      if ((double) this.m_currentFacing < 0.0)
        this.m_currentFacing += num1;
      else if ((double) this.m_currentFacing > (double) num1)
        this.m_currentFacing -= num1;
      float num6 = this.m_currentFacing - this.m_facingDest;
      if ((double) num6 < 0.0)
        num6 += num1;
      else if ((double) num6 > (double) num1)
        num6 -= num1;
      if ((double) num3 > 0.0 && (double) num6 < (double) num2)
        this.m_currentFacing = this.m_facingDest;
      else if ((double) num3 < 0.0 && (double) num6 > (double) num2)
        this.m_currentFacing = this.m_facingDest;
      float num7 = 57.2957764f;
      if (this.m_localTransformNode == null)
        return;
      this.m_localTransformNode.setOrientation(this.m_currentFacing * num7, 0.0f, -1f, 0.0f);
    }

    public enum NPCState
    {
      NPCSTATE_INITIAL,
      NPCSTATE_TESTANIMS,
      NPCSTATE_STANDING,
      NPCSTATE_CROUCHING,
      NPCSTATE_WALKING,
      NPCSTATE_RUNNING,
      NPCSTATE_JUMPING,
      NPCSTATE_FALLING,
      NPCSTATE_RAMP_RUN_UP,
      NPCSTATE_RAMP_SLIDE_DOWN,
      NPCSTATE_INACTIVE,
    }
  }
}
