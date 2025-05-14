
// Type: game.SequencerEvent
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace game
{
  public abstract class SequencerEvent
  {
    public int m_time;

    public SequencerEvent(DataInputStream dis)
    {
      this.m_time = 0;
      this.m_time = dis.readInt();
    }

    public virtual void Destructor()
    {
    }

    public abstract void play(SoundSequencer sequencer, GameObject @object);
  }
}
