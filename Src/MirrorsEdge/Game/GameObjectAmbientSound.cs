
// Type: game.GameObjectAmbientSound
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;

#nullable disable
namespace game
{
  public class GameObjectAmbientSound : GameObject
  {
    private int m_soundID;
    private int m_sndHandle;

    public GameObjectAmbientSound(MEdgeMap map, int soundId, float x, float y, float z)
      : base(map, 11, x, y, z)
    {
      this.m_soundID = soundId;
      this.m_sndHandle = -1;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (soundManager.isEventLoaded(this.m_soundID))
        return;
      soundManager.loadEvent(this.m_soundID);
    }

    public override void Destructor()
    {
      if (this.m_sndHandle != -1)
        AppEngine.getCanvas().getSoundManager().stopEvent(this.m_sndHandle);
      base.Destructor();
    }

    public override void update(int timeStepMillis)
    {
      if (this.m_sndHandle == -1)
        this.m_sndHandle = AppEngine.getCanvas().getSoundManager().playEventLooped(this.m_soundID);
      base.update(timeStepMillis);
      this.updateSoundPos();
    }

    private void updateSoundPos()
    {
      if (this.m_sndHandle == -1)
        return;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (soundManager.isHandlePlaying(this.m_sndHandle))
        soundManager.setEventPosition(this.m_sndHandle, this.m_position.x, this.m_position.y, this.m_position.z);
      else
        this.m_sndHandle = -1;
    }
  }
}
