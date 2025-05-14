// Decompiled with JetBrains decompiler
// Type: generic.BoundingRectangle
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace generic
{
  public class BoundingRectangle
  {
    public int m_x1;
    public int m_z1;
    public int m_x2;
    public int m_z2;

    public BoundingRectangle()
    {
      this.m_x1 = 0;
      this.m_z1 = 0;
      this.m_x2 = 0;
      this.m_z2 = 0;
    }

    public BoundingRectangle(int x1, int z1, int x2, int z2)
    {
      this.m_x1 = x1;
      this.m_z1 = z1;
      this.m_x2 = x2;
      this.m_z2 = z2;
    }

    public void set(int x1, int z1, int x2, int z2)
    {
      this.m_x1 = x1;
      this.m_z1 = z1;
      this.m_x2 = x2;
      this.m_z2 = z2;
    }

    public int getX1() => this.m_x1;

    public int getZ1() => this.m_z1;

    public int getX2() => this.m_x2;

    public int getZ2() => this.m_z2;

    public bool isPointWithin(int x, int z) => this.isPointWithin(x, z, 0);

    public bool isPointWithin(int x, int z, int radius)
    {
      return x - radius >= this.m_x1 && x + radius <= this.m_x2 && z - radius >= this.m_z1 && z + radius <= this.m_z2;
    }
  }
}
