// Decompiled with JetBrains decompiler
// Type: game.SequencerSound
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;

#nullable disable
namespace game
{
  public class SequencerSound : SequencerEvent
  {
    protected int m_soundId;

    public SequencerSound(DataInputStream dis)
      : base(dis)
    {
      this.m_soundId = 0;
      this.m_soundId = ResourceManager.SOUND_TRACK_SOUNDS[(int) dis.readShort()];
    }

    public override void Destructor() => base.Destructor();

    public override void play(SoundSequencer sequencer, GameObject @object)
    {
      if (@object != null)
        sequencer.getSoundManager().playEventAt(this.m_soundId, @object.m_position.x, @object.m_position.y, @object.m_position.z);
      else
        sequencer.getSoundManager().playEvent(this.m_soundId);
    }
  }
}
