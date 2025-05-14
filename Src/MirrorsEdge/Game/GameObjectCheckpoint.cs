// Decompiled with JetBrains decompiler
// Type: game.GameObjectCheckpoint
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;
using System;

#nullable disable
namespace game
{
  public class GameObjectCheckpoint : GameObject
  {
    public const int DISH_ANIM_DUR = 750;
    private int m_rotateUserId;
    private Node m_rotateNode;
    private float m_dishDeactivatedAngleDeg;
    private float m_dishActivatedAngeDeg;
    private GameObjectCheckpoint.DishAnimState m_animState;
    private int m_animTime;
    private GameObjectRunner.FacingDir m_playerFacingDir;

    public GameObjectRunner.FacingDir getPlayerFacingDir() => this.m_playerFacingDir;

    public GameObjectCheckpoint(
      MEdgeMap map,
      ModelSet modelSet,
      float min_x,
      float min_y,
      float max_x,
      float max_y,
      Node objectNode,
      bool isFacingRight)
      : base(map, 14, 0.0f, 0.0f, 0.0f)
    {
      this.m_rotateUserId = modelSet.getUserId(1);
      this.m_rotateNode = (Node) null;
      this.m_dishDeactivatedAngleDeg = 0.0f;
      this.m_dishActivatedAngeDeg = 0.0f;
      this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_INACTIVE;
      this.m_animTime = 0;
      this.m_playerFacingDir = GameObjectRunner.FacingDir.FACING_LEFT;
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(min_x, min_y, -1f, max_x, max_y, 1f);
      if (isFacingRight)
        this.m_playerFacingDir = GameObjectRunner.FacingDir.FACING_RIGHT;
      if (this.m_rotateUserId == 202)
      {
        if (isFacingRight)
        {
          this.m_dishDeactivatedAngleDeg = 0.0f;
          this.m_dishActivatedAngeDeg = 136f;
        }
        else
        {
          this.m_dishDeactivatedAngleDeg = 136f;
          this.m_dishActivatedAngeDeg = 0.0f;
        }
      }
      else if (isFacingRight)
      {
        this.m_dishDeactivatedAngleDeg = -45f;
        this.m_dishActivatedAngeDeg = 45f;
      }
      else
      {
        this.m_dishDeactivatedAngleDeg = 45f;
        this.m_dishActivatedAngeDeg = -45f;
      }
      this.m_rotateNode = this.m_rotateUserId != -1 ? (Node) objectNode.find(this.m_rotateUserId) : objectNode;
      this.setActiveAngleFactor(0.0f);
    }

    public override void Destructor()
    {
      if (this.m_globalShape == null)
        return;
      this.m_globalShape.Destructor();
      this.m_globalShape = (CollShape) null;
      this.m_rotateNode = (Node) null;
      base.Destructor();
    }

    public override void resetCheckpoint()
    {
      if (this.m_map.getCheckpointObject() == this)
      {
        this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_ACTIVE;
        this.setActiveAngleFactor(1f);
      }
      else
      {
        this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_INACTIVE;
        this.setActiveAngleFactor(0.0f);
      }
    }

    public override void collidedWith(GameObject other)
    {
      if (this.m_map.getCheckpointObject() == this)
        return;
      this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_ACTIVATING;
      this.m_animTime = 750;
      this.m_map.setCheckpointObject(this);
    }

    public new void deactivate()
    {
      this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_DEACTIVATING;
      this.m_animTime = 750;
    }

    public override void update(int timeStepMillis)
    {
      switch (this.m_animState)
      {
        case GameObjectCheckpoint.DishAnimState.DISH_ANIM_ACTIVATING:
          this.m_animTime = Math.Max(0, this.m_animTime - timeStepMillis);
          if (this.m_animTime == 0)
          {
            this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_ACTIVE;
            this.setActiveAngleFactor(1f);
            break;
          }
          this.setActiveAngleFactor((float) (750 - this.m_animTime) / 750f);
          break;
        case GameObjectCheckpoint.DishAnimState.DISH_ANIM_DEACTIVATING:
          this.m_animTime = Math.Max(0, this.m_animTime - timeStepMillis);
          if (this.m_animTime == 0)
          {
            this.m_animState = GameObjectCheckpoint.DishAnimState.DISH_ANIM_INACTIVE;
            this.setActiveAngleFactor(0.0f);
            break;
          }
          this.setActiveAngleFactor((float) this.m_animTime / 750f);
          break;
      }
      this.testVFC();
      if (this.m_passedVFC)
        this.m_rotateNode.setRenderingEnable(true);
      else
        this.m_rotateNode.setRenderingEnable(false);
    }

    private void setActiveAngleFactor(float activeAngleProgress)
    {
      float num1;
      if ((double) activeAngleProgress < 0.5)
      {
        float num2 = activeAngleProgress * 2f;
        num1 = num2 * num2 * num2 * 0.5f;
      }
      else
      {
        float num3 = (float) (2.0 * (1.0 - (double) activeAngleProgress));
        num1 = (float) (1.0 - 0.5 * (double) (num3 * num3 * num3));
      }
      float degrees = (float) ((double) this.m_dishDeactivatedAngleDeg * (1.0 - (double) num1) + (double) this.m_dishActivatedAngeDeg * (double) num1);
      ModelSet modelSet = this.m_map.getModelSet();
      this.m_rotateNode.setOrientation(degrees, 0.0f, -modelSet.getRotateY(), -modelSet.getRotateZ());
    }

    private enum DishAnimState
    {
      DISH_ANIM_INACTIVE,
      DISH_ANIM_ACTIVATING,
      DISH_ANIM_ACTIVE,
      DISH_ANIM_DEACTIVATING,
    }
  }
}
