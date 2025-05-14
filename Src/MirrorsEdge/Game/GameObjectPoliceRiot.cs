// Decompiled with JetBrains decompiler
// Type: game.GameObjectPoliceRiot
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using support;

#nullable disable
namespace game
{
  public class GameObjectPoliceRiot : GameObjectPolice
  {
    public GameObjectPoliceRiot(MEdgeMap map, float posX, float posY, float posZ, int pathId)
      : base(map, 3, (int) M3GAssets.get("MODEL_POLICE_RIOT"), 1, posX, posY, posZ, pathId)
    {
      this.FIRE_DELAY = 200;
      this.FIRE_DECISION_TIME = 200;
      this.AIM_DURATION = 200;
      this.SHOT_DELAY = 450;
      this.SHOT_REPEAT_CHANCE = 75;
      this.POSTURE_DECISION_TIME = 1000;
      this.POSTURE_CHANGE_CHANCE = 10;
      this.POSTURE_CHANGE_DELAY = 3000;
      this.FIRE_CHANCE = 90;
      this.TRIGGER_RANGE_FRONT = 15;
      this.TRIGGER_RANGE_REAR = 7;
    }

    public override void Destructor() => base.Destructor();
  }
}
