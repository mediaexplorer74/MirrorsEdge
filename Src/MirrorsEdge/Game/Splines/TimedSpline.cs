// Decompiled with JetBrains decompiler
// Type: game.Splines.TimedSpline
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game.Splines
{
  public class TimedSpline
  {
    private const int MAX_NODES = 50;
    private MathVector[] m_nodePosition = new MathVector[50];
    private MathVector[] m_nodeVelocity = new MathVector[50];
    private int[] m_nodeTime = new int[50];
    private float[] m_nodeLength = new float[50];
    private float[] m_nodeTimeInv = new float[50];
    private int[] m_nodeFullTime = new int[50];
    private int m_maxTime;
    private int m_nodeCount;

    public TimedSpline()
    {
      this.m_maxTime = 0;
      this.m_nodeCount = 0;
    }

    public void addNode(MathVector pos, int time)
    {
      if (this.m_nodeCount == 0)
      {
        this.m_maxTime = 0;
      }
      else
      {
        int index = this.m_nodeCount - 1;
        this.m_nodeTime[index] = time;
        this.m_nodeLength[index] = (pos - this.m_nodePosition[index]).getLength();
        this.m_nodeTimeInv[index] = time <= 0 ? 0.0f : 1f / (float) time;
        this.m_maxTime += time;
      }
      this.m_nodePosition[this.m_nodeCount] = pos;
      this.m_nodeFullTime[this.m_nodeCount] = this.m_maxTime;
      ++this.m_nodeCount;
    }

    public void buildSpline()
    {
      if (this.m_nodeCount <= 1)
        return;
      for (int index1 = 1; index1 < this.m_nodeCount - 1; ++index1)
      {
        int index2 = index1;
        int index3 = index1 + 1;
        int index4 = index1 - 1;
        MathVector mathVector1 = (this.m_nodePosition[index3] - this.m_nodePosition[index2]).normalise();
        MathVector mathVector2 = (this.m_nodePosition[index4] - this.m_nodePosition[index2]).normalise();
        this.m_nodeVelocity[index2] = (mathVector1 - mathVector2).normalise();
      }
      int index5 = this.m_nodeCount - 1;
      int index6 = index5 - 1;
      this.m_nodeVelocity[0] = ((this.m_nodePosition[1] - this.m_nodePosition[0]) * 3f / this.m_nodeLength[0] - this.m_nodeVelocity[1]) * 0.5f;
      MathVector mathVector = (this.m_nodePosition[index5] - this.m_nodePosition[index6]) * 3f / this.m_nodeLength[index6];
      this.m_nodeVelocity[index5] = (mathVector - this.m_nodeVelocity[index6]) * 0.5f;
    }

    public void clear() => this.m_nodeCount = 0;

    public int getNodeAtTime(int time)
    {
      int num = 0;
      int nodeAtTime = 0;
      while (num + this.m_nodeTime[nodeAtTime] < time)
      {
        num += this.m_nodeTime[nodeAtTime];
        if (++nodeAtTime == this.m_nodeCount)
          nodeAtTime = 0;
      }
      return nodeAtTime;
    }

    public MathVector getNodePosition(int node) => this.m_nodePosition[node];

    public MathVector getPosition(int time)
    {
      int nodeAtTime = this.getNodeAtTime(time);
      int index = nodeAtTime + 1 >= this.m_nodeCount ? 0 : nodeAtTime + 1;
      float pos = (float) (time - this.m_nodeFullTime[nodeAtTime]) * this.m_nodeTimeInv[nodeAtTime];
      MathVector endVel = new MathVector(this.m_nodeVelocity[index] * this.m_nodeLength[index]);
      MathVector endPos = new MathVector(this.m_nodePosition[index]);
      return GlobalMembersNonUniformSpline.getPositionOnCubic(new MathVector(this.m_nodePosition[nodeAtTime]), new MathVector(this.m_nodeVelocity[nodeAtTime] * this.m_nodeLength[nodeAtTime]), endPos, endVel, pos);
    }

    public int getMaxTime() => this.m_maxTime;

    public void Destructor()
    {
      this.m_nodePosition = (MathVector[]) null;
      this.m_nodeVelocity = (MathVector[]) null;
      this.m_nodeTime = (int[]) null;
      this.m_nodeLength = (float[]) null;
      this.m_nodeTimeInv = (float[]) null;
      this.m_nodeFullTime = (int[]) null;
    }
  }
}
