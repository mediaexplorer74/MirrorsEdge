
// Type: generic.AnimationManagerData
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace generic
{
  public class AnimationManagerData
  {
    public AnimPlayer[] m_animPlayerPool;
    public sbyte[] colourData;
    public sbyte[] animNumFrames;
    public short[] animFrameOffset;
    public short[] frameDuration;
    public sbyte[] frameNumPrimitives;
    public int[] framePrimitiveOffset;
    public short[] primitiveData;
    public short[][] m_subImages = new short[12][];
    public Image[][] m_animImageArray;
    public int m_curBank;

    public AnimationManagerData()
    {
      this.m_animPlayerPool = new AnimPlayer[48];
      for (int index = 0; index < 12; ++index)
        this.m_subImages[index] = new short[5];
      this.colourData = (sbyte[]) null;
      this.animNumFrames = (sbyte[]) null;
      this.animFrameOffset = (short[]) null;
      this.frameDuration = (short[]) null;
      this.frameNumPrimitives = (sbyte[]) null;
      this.framePrimitiveOffset = (int[]) null;
      this.primitiveData = (short[]) null;
      this.m_animImageArray = (Image[][]) null;
      this.m_curBank = 0;
    }

    public void Destructor()
    {
      for (int index = 0; index < 48; ++index)
        this.m_animPlayerPool[index] = (AnimPlayer) null;
      for (int index1 = 0; index1 < 1; ++index1)
      {
        for (int index2 = 0; index2 < this.m_animImageArray[index1].Length; ++index2)
        {
          if (this.m_animImageArray[index1][index2] != null)
          {
            this.m_animImageArray[index1][index2].Destructor();
            this.m_animImageArray[index1][index2] = (Image) null;
          }
        }
      }
      this.m_subImages = (short[][]) null;
      this.colourData = (sbyte[]) null;
      this.animNumFrames = (sbyte[]) null;
      this.animFrameOffset = (short[]) null;
      this.frameDuration = (short[]) null;
      this.frameNumPrimitives = (sbyte[]) null;
      this.framePrimitiveOffset = (int[]) null;
      this.primitiveData = (short[]) null;
      this.m_animImageArray = (Image[][]) null;
    }
  }
}
