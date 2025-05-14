// Decompiled with JetBrains decompiler
// Type: game.SequencerTrack
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace game
{
  public class SequencerTrack
  {
    public int m_flags;
    public int m_duration;
    public SequencerEvent[] m_events;

    public SequencerTrack(DataInputStream dis)
    {
      this.m_flags = 0;
      this.m_duration = 0;
      byte num1 = (byte) dis.readByte();
      int num2 = dis.readInt();
      int length1 = (int) dis.readByte();
      SequencerFootstep[] sequencerFootstepArray = new SequencerFootstep[length1];
      for (int index = 0; index < length1; ++index)
        sequencerFootstepArray[index] = new SequencerFootstep(dis);
      int length2 = (int) dis.readByte();
      SequencerSound[] sequencerSoundArray = new SequencerSound[length2];
      for (int index = 0; index < length2; ++index)
        sequencerSoundArray[index] = new SequencerSound(dis);
      int length3 = (int) dis.readByte();
      SequencerPoolSound[] sequencerPoolSoundArray = new SequencerPoolSound[length3];
      for (int index = 0; index < length3; ++index)
        sequencerPoolSoundArray[index] = new SequencerPoolSound(dis);
      int length4 = length1 + length2 + length3;
      SequencerEvent[] sequencerEventArray = new SequencerEvent[length4];
      int index1 = 0;
      int index2 = 0;
      int index3 = 0;
      for (int index4 = 0; index4 < length4; ++index4)
      {
        int num3 = index1 < length1 ? sequencerFootstepArray[index1].m_time : int.MaxValue;
        int num4 = index2 < length2 ? sequencerSoundArray[index2].m_time : int.MaxValue;
        int num5 = index3 < length3 ? sequencerPoolSoundArray[index3].m_time : int.MaxValue;
        if (num3 < num4 && num3 < num5)
        {
          sequencerEventArray[index4] = (SequencerEvent) sequencerFootstepArray[index1];
          ++index1;
        }
        if (num4 < num3 && num4 < num5)
        {
          sequencerEventArray[index4] = (SequencerEvent) sequencerSoundArray[index2];
          ++index2;
        }
        if (num5 < num4 && num5 < num3)
        {
          sequencerEventArray[index4] = (SequencerEvent) sequencerPoolSoundArray[index3];
          ++index3;
        }
      }
      this.m_flags = (int) num1;
      this.m_duration = num2;
      this.m_events = sequencerEventArray;
    }

    public void Destructor()
    {
      for (int index = 0; index < this.m_events.Length; ++index)
      {
        if (this.m_events[index] != null)
          this.m_events[index].Destructor();
      }
      this.m_events = (SequencerEvent[]) null;
    }
  }
}
