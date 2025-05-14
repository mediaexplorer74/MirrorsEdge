// Decompiled with JetBrains decompiler
// Type: game.DataCameraAnimStep
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace game
{
  public class DataCameraAnimStep
  {
    public int m_moveTime;
    public int m_pauseTime;
    public int m_beginTime;
    public int m_pauseAt;
    public int m_endTime;
    public float[] m_tLookFrom = new float[3];
    public float[] m_tLookAt = new float[3];

    public DataCameraAnimStep(DataInputStream dis, int realTime)
    {
      this.m_moveTime = 0;
      this.m_pauseTime = 0;
      this.m_beginTime = 0;
      this.m_pauseAt = 0;
      this.m_endTime = 0;
      this.m_moveTime = dis.readInt();
      this.m_pauseTime = dis.readInt();
      this.m_tLookFrom[0] = dis.readFloat();
      this.m_tLookFrom[1] = dis.readFloat();
      this.m_tLookFrom[2] = dis.readFloat();
      this.m_tLookAt[0] = dis.readFloat();
      this.m_tLookAt[1] = dis.readFloat();
      this.m_tLookAt[2] = dis.readFloat();
      this.m_beginTime = realTime;
      this.m_pauseAt = this.m_beginTime + this.m_moveTime;
      this.m_endTime = this.m_pauseAt + this.m_pauseTime;
    }

    public void Destructor()
    {
      this.m_tLookFrom = (float[]) null;
      this.m_tLookAt = (float[]) null;
    }
  }
}
