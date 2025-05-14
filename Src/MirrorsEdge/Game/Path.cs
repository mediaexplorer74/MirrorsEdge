// Decompiled with JetBrains decompiler
// Type: game.Path
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace game
{
  public class Path
  {
    private PathPoint[] m_points;

    public Path(DataInputStream dis)
    {
      int length = (int) dis.readShort();
      this.m_points = new PathPoint[length];
      for (int index = 0; index < length; ++index)
        this.m_points[index] = new PathPoint(dis);
    }

    public void Destructor() => this.m_points = (PathPoint[]) null;

    public int getPointCount() => this.m_points.Length;

    public PathPoint getPoint(int idx) => this.m_points[idx];
  }
}
