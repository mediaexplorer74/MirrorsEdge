
// Type: game.ModelSet
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace game
{
  public class ModelSet
  {
    public int[] m_modelArray;
    public int[] m_userIdArray;
    public uint m_backgroundFillColor;
    public float m_rotateY;
    public float m_rotateZ;

    public ModelSet()
    {
      this.m_modelArray = new int[2];
      this.m_userIdArray = new int[2];
      this.m_backgroundFillColor = 0U;
      this.m_rotateY = 0.0f;
      this.m_rotateZ = 0.0f;
    }

    public ModelSet(ModelSet other)
    {
      this.m_modelArray = new int[other.m_modelArray.Length];
      Array.Copy((Array) other.m_modelArray, (Array) this.m_modelArray, other.m_modelArray.Length);
      this.m_userIdArray = new int[other.m_userIdArray.Length];
      Array.Copy((Array) other.m_userIdArray, (Array) this.m_userIdArray, other.m_userIdArray.Length);
      this.m_backgroundFillColor = other.m_backgroundFillColor;
      this.m_rotateY = other.m_rotateY;
      this.m_rotateZ = other.m_rotateZ;
    }

    public void Destructor()
    {
      this.m_modelArray = (int[]) null;
      this.m_userIdArray = (int[]) null;
    }

    public int getModelId(int index) => this.m_modelArray[index];

    public int getUserId(int index) => this.m_userIdArray[index];

    public float getRotateY() => this.m_rotateY;

    public float getRotateZ() => this.m_rotateZ;

    public uint getBackgroundFillColor() => this.m_backgroundFillColor;
  }
}
