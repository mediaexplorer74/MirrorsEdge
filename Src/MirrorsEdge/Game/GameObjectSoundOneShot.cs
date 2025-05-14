
// Type: game.GameObjectSoundOneShot
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;

#nullable disable
namespace game
{
  public class GameObjectSoundOneShot : GameObject
  {
    private const int TRIGGER_DIST = 10;
    private int m_soundID;
    private bool m_fired;
    private int TRIGGER_DIST_SQ = 100;

    public GameObjectSoundOneShot(MEdgeMap map, int soundId, float x, float y, float z)
      : base(map, 13, x, y, z)
    {
      this.m_soundID = soundId;
      this.m_fired = false;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (soundManager.isEventLoaded(this.m_soundID))
        return;
      soundManager.loadEvent(this.m_soundID);
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      GameObject playerObject = (GameObject) this.m_map.getPlayerObject();
      if (this.m_fired || (double) (playerObject.m_position - this.m_position).getLengthSq() > (double) this.TRIGGER_DIST_SQ)
        return;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      int handle = soundManager.playEvent(this.m_soundID);
      soundManager.setLoopCountEvent(handle, 1);
      this.m_fired = true;
    }

    public override void resetLevel() => this.m_fired = false;
  }
}
