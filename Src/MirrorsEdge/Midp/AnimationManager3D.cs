
// Type: midp.AnimationManager3D
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;

#nullable disable
namespace midp
{
  public class AnimationManager3D
  {
    public const int HERMITE_SHIFT = 6;
    public const int NUM_HERMITES = 1025;
    private short[] m_animStartFrame;
    private short[] m_animEndFrame;
    private short[][] m_animWindowStartFrame;
    private short[][] m_animWindowEndFrame;
    private sbyte[][] m_animWindowFlags;

    public AnimationManager3D()
    {
      this.m_animStartFrame = (short[]) null;
      this.m_animEndFrame = (short[]) null;
      this.m_animWindowStartFrame = (short[][]) null;
      this.m_animWindowEndFrame = (short[][]) null;
      this.m_animWindowFlags = (sbyte[][]) null;
    }

    public void Destructor()
    {
      this.m_animStartFrame = (short[]) null;
      this.m_animEndFrame = (short[]) null;
      this.m_animWindowStartFrame = (short[][]) null;
      this.m_animWindowEndFrame = (short[][]) null;
      this.m_animWindowFlags = (sbyte[][]) null;
    }

    public bool loadAnimFile(ResourceManager resMgr)
    {
      DataInputStream dataInputStream = new DataInputStream(resMgr.loadBinaryFile((int) ResourceManager.get("IDI_ANIM3D_BIN")));
      int length1 = (int) dataInputStream.readShort();
      this.m_animStartFrame = new short[length1];
      this.m_animEndFrame = new short[length1];
      this.m_animWindowStartFrame = new short[length1][];
      this.m_animWindowEndFrame = new short[length1][];
      this.m_animWindowFlags = new sbyte[length1][];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        this.m_animStartFrame[index1] = dataInputStream.readShort();
        this.m_animEndFrame[index1] = dataInputStream.readShort();
        dataInputStream.readBoolean();
        int length2 = (int) dataInputStream.readShort();
        if (length2 > 0)
        {
          this.m_animWindowStartFrame[index1] = new short[length2];
          this.m_animWindowEndFrame[index1] = new short[length2];
          this.m_animWindowFlags[index1] = new sbyte[length2];
          for (int index2 = 0; index2 < length2; ++index2)
          {
            this.m_animWindowStartFrame[index1][index2] = dataInputStream.readShort();
            this.m_animWindowEndFrame[index1][index2] = dataInputStream.readShort();
            this.m_animWindowFlags[index1][index2] = dataInputStream.readByte();
          }
        }
      }
      return true;
    }

    public AnimPlayer3D createAnimPlayer3D() => new AnimPlayer3D(this);

    public int getNumAnimations() => this.m_animStartFrame.Length;

    public int getAnimStartFrame(int animIndex) => (int) this.m_animStartFrame[animIndex];

    public int getAnimEndFrame(int animIndex) => (int) this.m_animEndFrame[animIndex];

    public int getAnimationDuration(int animIndex)
    {
      int animStartFrame = this.getAnimStartFrame(animIndex);
      int animEndFrame = this.getAnimEndFrame(animIndex);
      int num = animEndFrame - animStartFrame;
      return num <= 0 || animStartFrame == 0 || animEndFrame == 0 ? 0 : num * 40;
    }

    public int getAnimNumWindows(int animIndex) => this.m_animWindowStartFrame[animIndex].Length;

    public int getAnimWindowStartFrame(int animIndex, int windowIndex)
    {
      return (int) this.m_animWindowStartFrame[animIndex][windowIndex];
    }

    public int getAnimWindowEndFrame(int animIndex, int windowIndex)
    {
      return (int) this.m_animWindowEndFrame[animIndex][windowIndex];
    }

    public int getAnimWindowFlags(int animIndex, int windowIndex)
    {
      return (int) this.m_animWindowFlags[animIndex][windowIndex];
    }
  }
}
