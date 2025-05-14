
// Type: game.GameObjectMusicCue
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class GameObjectMusicCue : GameObject
  {
    private const int TRIGGER_DIST = 10;
    private int m_musicId;
    private bool m_triggered;
    private int TRIGGER_DIST_SQ = 100;

    public GameObjectMusicCue(MEdgeMap map, int musicId, float x, float y, float z)
      : base(map, 11, x, y, z)
    {
      this.m_musicId = musicId;
      this.m_triggered = false;
    }

    public override void update(int timeStepMillis)
    {
      base.update(timeStepMillis);
      if (this.m_triggered)
        return;
      GameObject playerObject = (GameObject) this.m_map.getPlayerObject();
      float num1 = playerObject.m_position.x - this.m_position.x;
      float num2 = playerObject.m_position.y - this.m_position.y;
      float num3 = playerObject.m_position.z - this.m_position.z;
      if ((double) num1 * (double) num1 + (double) num2 * (double) num2 + (double) num3 * (double) num3 >= (double) this.TRIGGER_DIST_SQ)
        return;
      AppEngine.getCanvas().getBGMusic().playMusic(this.m_musicId, 2);
      this.m_triggered = true;
    }

    public override void resetLevel() => this.m_triggered = false;
  }
}
