// Decompiled with JetBrains decompiler
// Type: game.SoundEventPoolManager
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;

#nullable disable
namespace game
{
  public class SoundEventPoolManager
  {
    private int[][][] m_soundPoolData;

    public SoundEventPoolManager()
    {
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_SOUNDPOOLS_BIN")));
      int length1 = dataInputStream.readUnsignedShort();
      this.m_soundPoolData = new int[length1][][];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        int length2 = dataInputStream.readUnsignedShort();
        this.m_soundPoolData[index1] = new int[length2][];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          int length3 = dataInputStream.readUnsignedShort();
          this.m_soundPoolData[index1][index2] = new int[length3];
          for (int index3 = 0; index3 < length3; ++index3)
          {
            int eventID = ResourceManager.SOUND_EVENT_LOOKUP[dataInputStream.readUnsignedShort()];
            this.m_soundPoolData[index1][index2][index3] = eventID;
            soundManager.loadEvent(eventID);
          }
        }
      }
      dataInputStream.close();
    }

    public void Destructor() => this.unloadSounds();

    private void unloadSounds()
    {
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      int length1 = this.m_soundPoolData.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        int length2 = this.m_soundPoolData[index1].Length;
        for (int index2 = 0; index2 < length2; ++index2)
        {
          int length3 = this.m_soundPoolData[index1][index2].Length;
          for (int index3 = 0; index3 < length3; ++index3)
          {
            int eventID = this.m_soundPoolData[index1][index2][index3];
            soundManager.unloadEvent(eventID);
          }
        }
      }
    }

    public int getRandomSoundEventID(int poolSetId, int poolId)
    {
      return this.getRandomSoundEventID(poolSetId, poolId, -1);
    }

    public int getRandomSoundEventID(int poolSetId, int poolId, int excludeId)
    {
      int length = this.m_soundPoolData[poolSetId][poolId].Length;
      int index1 = AppEngine.getCanvas().rand(0, length - 1);
      int randomSoundEventId = this.m_soundPoolData[poolSetId][poolId][index1];
      if (excludeId == randomSoundEventId)
      {
        int index2 = (index1 + 1) % length;
        randomSoundEventId = this.m_soundPoolData[poolSetId][poolId][index2];
      }
      return randomSoundEventId;
    }
  }
}
