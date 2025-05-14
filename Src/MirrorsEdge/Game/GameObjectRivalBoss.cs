// Decompiled with JetBrains decompiler
// Type: game.GameObjectRivalBoss
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using support;
using System;

#nullable disable
namespace game
{
  public class GameObjectRivalBoss : GameObjectRival
  {
    public const float RIVAL_BOSS_TRIGGER_RANGE_SQ = 81f;
    private const float ATTACK_RANGE_SQ = 36f;
    private bool m_passing;
    private int m_attackTime;
    private bool m_triggered;

    public GameObjectRivalBoss(MEdgeMap map, float posX, float posY, float posZ, int pathId)
      : base(map, 5, (int) M3GAssets.get("MODEL_RIVAL_BOSS"), 0, posX, posY, posZ, pathId)
    {
      this.m_passing = false;
      this.m_attackTime = 0;
      this.m_triggered = false;
    }

    public override void resetLevel()
    {
      base.resetLevel();
      this.m_passing = false;
      this.m_attackTime = 0;
      this.m_triggered = false;
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      if (!AppEngine.getLevelData().isCurrentLevelLast())
        return;
      GameObjectPlayer playerObject = this.m_map.getPlayerObject();
      if ((double) this.m_distanceToPlayerSq < 36.0 && (double) Math.Abs(playerObject.m_position.y - this.m_position.y) < 0.5 && this.m_brainState == GameObjectRival.BrainState.BRAINSTATE_FOLLOW)
      {
        if ((double) (this.m_position - playerObject.m_position).x * (double) this.m_velocity.x < 0.0)
        {
          if (!this.m_passing && (double) AppEngine.getCanvas().randPercent() < 50.0)
          {
            this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_SLIDE);
            this.m_attackTime = 1000;
          }
          this.m_passing = true;
        }
        else
          this.m_passing = false;
      }
      else if (this.m_attackTime > 0)
      {
        this.m_attackTime -= timeStep;
        if (this.m_attackTime <= 0)
        {
          this.brainStateTransition(GameObjectRival.BrainState.BRAINSTATE_FOLLOW);
          this.m_passing = false;
        }
      }
      if (!this.m_triggeredCombat || 36.0 >= (double) this.m_distanceToPlayerSq)
        return;
      this.m_triggeredCombat = false;
    }

    protected override bool shouldUpdate()
    {
      if (!this.m_triggered)
        this.m_triggered = (double) this.m_distanceToPlayerSq < 81.0;
      return AppEngine.getLevelData().isCurrentLevelLast() ? this.m_triggered : base.shouldUpdate();
    }
  }
}
