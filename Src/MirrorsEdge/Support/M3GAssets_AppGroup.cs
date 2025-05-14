
// Type: support.M3GAssets_AppGroup
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_AppGroup
  {
    private int[] m_userIdArray;
    private Appearance[] m_appearanceArray;
    private Appearance m_defaultAppearance;
    private int m_size;

    public M3GAssets_AppGroup(int numElements, Appearance defaultAppearance)
    {
      this.m_userIdArray = new int[numElements];
      this.m_appearanceArray = new Appearance[numElements];
      for (int index = 0; index < numElements; ++index)
        this.m_appearanceArray[index] = (Appearance) null;
      this.m_defaultAppearance = defaultAppearance;
      this.m_size = 0;
    }

    public void Destructor()
    {
      this.clearAppearances();
      this.m_appearanceArray = (Appearance[]) null;
      this.m_defaultAppearance = (Appearance) null;
      this.m_size = 0;
    }

    public int getNumMappings() => this.m_size;

    public void addElement(int userId, Appearance appearance)
    {
      this.m_userIdArray[this.m_size] = userId;
      this.m_appearanceArray[this.m_size] = appearance;
      ++this.m_size;
    }

    public Appearance getDefaultAppearance() => this.m_defaultAppearance;

    public void setDefaultAppearance(Appearance appearance)
    {
      this.m_defaultAppearance = appearance;
    }

    public void setMappedAppearance(Appearance appearance, int index)
    {
      this.m_appearanceArray[index] = appearance;
    }

    public void clearAppearances()
    {
      this.m_defaultAppearance = (Appearance) null;
      for (int index = 0; index != this.m_size; ++index)
        this.m_appearanceArray[index] = (Appearance) null;
    }

    public Appearance getAppearance(int userId)
    {
      for (int index = 0; index != this.m_size; ++index)
      {
        if (this.m_userIdArray[index] == userId)
          return this.m_appearanceArray[index];
      }
      return this.m_defaultAppearance;
    }
  }
}
