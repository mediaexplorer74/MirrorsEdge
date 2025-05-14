// Decompiled with JetBrains decompiler
// Type: game.SequencerPoolSound
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;

#nullable disable
namespace game
{
  public class SequencerPoolSound : SequencerEvent
  {
    protected int m_poolsetId;
    protected int m_poolId;

    public SequencerPoolSound(DataInputStream dis)
      : base(dis)
    {
      this.m_poolsetId = 0;
      this.m_poolId = 0;
      this.m_poolsetId = ResourceManager.SOUND_TRACK_POOL_LOOKUPS[(int) dis.readShort()];
      this.m_poolId = ResourceManager.SOUND_TRACK_POOL_LOOKUPS[(int) dis.readShort()];
    }

    public override void Destructor() => base.Destructor();

    public override void play(SoundSequencer sequencer, GameObject @object)
    {
      int randomSoundEventId = sequencer.getSoundPoolManager().getRandomSoundEventID(this.m_poolsetId, this.m_poolId);
      if (@object != null)
        sequencer.getSoundManager().playEventAt(randomSoundEventId, @object.m_position.x, @object.m_position.y, @object.m_position.z);
      else
        sequencer.getSoundManager().playEvent(randomSoundEventId);
    }
  }
}
