// Decompiled with JetBrains decompiler
// Type: game.PathPoint
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace game
{
  public class PathPoint
  {
    public MathVector m_position;
    public int m_type;
    public int m_time;

    public PathPoint(DataInputStream dis)
    {
      this.m_type = -1;
      this.m_time = -1;
      this.m_position.set(dis);
      this.m_type = dis.readInt();
      this.m_time = dis.readInt();
    }

    public PathPoint(PathPoint pp)
    {
      this.m_type = pp.m_type;
      this.m_time = pp.m_time;
      this.m_position = pp.m_position;
    }
  }
}
