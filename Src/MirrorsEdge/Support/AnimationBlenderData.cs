// Decompiled with JetBrains decompiler
// Type: support.AnimationBlenderData
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using generic;
using midp;

#nullable disable
namespace support
{
  public class AnimationBlenderData
  {
    private int[][] m_animationControllerUserIDs;
    private short[][] m_animationChannelInterpTimes;
    private bool m_isDataLoaded;

    public AnimationBlenderData()
    {
      this.m_isDataLoaded = false;
      this.m_animationControllerUserIDs = (int[][]) null;
      this.m_animationChannelInterpTimes = (short[][]) null;
    }

    public void Destructor()
    {
      this.m_animationControllerUserIDs = (int[][]) null;
      this.m_animationChannelInterpTimes = (short[][]) null;
    }

    public void loadData()
    {
      if (this.m_isDataLoaded)
        return;
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_ANIMATION_BLENDERS_BIN")));
      int length1 = (int) dataInputStream.readShort();
      this.m_animationControllerUserIDs = new int[length1][];
      this.m_animationChannelInterpTimes = new short[length1][];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        int length2 = (int) dataInputStream.readShort();
        this.m_animationControllerUserIDs[index1] = new int[length2];
        this.m_animationChannelInterpTimes[index1] = new short[length2];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          this.m_animationControllerUserIDs[index1][index2] = ResourceManager.ANIMATION_CONTROLLER_LOOKUP[(int) dataInputStream.readShort()];
          this.m_animationChannelInterpTimes[index1][index2] = dataInputStream.readShort();
        }
      }
      dataInputStream.close();
      this.m_isDataLoaded = true;
    }

    public int[] getAnimationControllerUserIDs(int blenderID)
    {
      return this.m_animationControllerUserIDs[blenderID];
    }

    public short[] getAnimationChannelInterpTimes(int blenderID)
    {
      return this.m_animationChannelInterpTimes[blenderID];
    }
  }
}
