
// Type: game.GameObjectPoliceLight
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using support;

#nullable disable
namespace game
{
  public class GameObjectPoliceLight : GameObjectPolice
  {
    public GameObjectPoliceLight(MEdgeMap map, float posX, float posY, float posZ, int pathId)
      : base(map, 2, (int) M3GAssets.get("MODEL_POLICE_LIGHT"), 1, posX, posY, posZ, pathId)
    {
      this.FIRE_DELAY = 300;
      this.FIRE_DECISION_TIME = 300;
      this.AIM_DURATION = 300;
      this.SHOT_DELAY = 550;
      this.SHOT_REPEAT_CHANCE = 50;
      this.POSTURE_DECISION_TIME = 1000;
      this.POSTURE_CHANGE_CHANCE = 10;
      this.POSTURE_CHANGE_DELAY = 3000;
      this.FIRE_CHANCE = 80;
      this.TRIGGER_RANGE_FRONT = 7;
      this.TRIGGER_RANGE_REAR = 1;
    }

    public override void Destructor() => base.Destructor();
  }
}
