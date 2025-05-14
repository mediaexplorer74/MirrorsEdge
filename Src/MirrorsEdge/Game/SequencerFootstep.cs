// Decompiled with JetBrains decompiler
// Type: game.SequencerFootstep
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;

#nullable disable
namespace game
{
  public class SequencerFootstep(DataInputStream dis) : SequencerEvent(dis)
  {
    public override void Destructor() => base.Destructor();

    public override void play(SoundSequencer sequencer, GameObject @object)
    {
      int currentMaterial = sequencer.getSceneGame().getMap().getPlayerObject().getCurrentMaterial();
      int poolSetId = (int) ResourceManager.get("SOUNDEVENTPOOLSET_FOOTSTEPS");
      int poolId = ResourceManager.MATERIAL_LOOKUP[currentMaterial];
      int randomSoundEventId = sequencer.getSoundPoolManager().getRandomSoundEventID(poolSetId, poolId, -1);
      if (@object != null)
        sequencer.getSoundManager().playEventAt(randomSoundEventId, @object.m_position.x, @object.m_position.y, @object.m_position.z);
      else
        sequencer.getSoundManager().playEvent(randomSoundEventId);
    }
  }
}
