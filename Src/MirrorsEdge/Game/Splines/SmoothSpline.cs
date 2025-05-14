
// Type: game.Splines.SmoothSpline
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game.Splines
{
  public class SmoothSpline
  {
    private const int MAX_NODES = 50;
    private MathVector[] m_nodePosition = new MathVector[50];
    private MathVector[] m_nodeVelocity = new MathVector[50];
    private float[] m_nodeLength = new float[50];
    private float[] m_nodeLengthInv = new float[50];
    private float[] m_nodeFullLength = new float[50];
    private float m_maxLength;
    private int m_nodeCount;

    public SmoothSpline()
    {
      this.m_maxLength = 0.0f;
      this.m_nodeCount = 0;
    }

    public void addNode(MathVector pos)
    {
      if (this.m_nodeCount == 0)
      {
        this.m_maxLength = 0.0f;
      }
      else
      {
        int index = this.m_nodeCount - 1;
        float length = (this.m_nodePosition[index] - pos).getLength();
        this.m_nodeLength[index] = length;
        this.m_nodeLengthInv[index] = (double) length <= 0.0 ? 0.0f : 1f / length;
        this.m_maxLength += length;
      }
      this.m_nodePosition[this.m_nodeCount] = pos;
      this.m_nodeFullLength[this.m_nodeCount] = this.m_maxLength;
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

    public int getNodeAtLength(float length)
    {
      float num = 0.0f;
      int nodeAtLength = 0;
      while ((double) num + (double) this.m_nodeLength[nodeAtLength] < (double) length)
      {
        num += this.m_nodeLength[nodeAtLength];
        if (++nodeAtLength == this.m_nodeCount)
          nodeAtLength = 0;
      }
      return nodeAtLength;
    }

    public MathVector getNodePosition(int node) => this.m_nodePosition[node];

    public MathVector getPosition(float length)
    {
      int nodeAtLength = this.getNodeAtLength(length);
      int index = nodeAtLength + 1 >= this.m_nodeCount ? 0 : nodeAtLength + 1;
      float pos = (length - this.m_nodeFullLength[nodeAtLength]) * this.m_nodeLengthInv[nodeAtLength];
      MathVector endVel = new MathVector(this.m_nodeVelocity[index] * this.m_nodeLength[index]);
      MathVector endPos = new MathVector(this.m_nodePosition[index]);
      return GlobalMembersNonUniformSpline.getPositionOnCubic(new MathVector(this.m_nodePosition[nodeAtLength]), new MathVector(this.m_nodeVelocity[nodeAtLength] * this.m_nodeLength[nodeAtLength]), endPos, endVel, pos);
    }

    public float getMaxLength() => this.m_maxLength;

    public void Destructor()
    {
      this.m_nodePosition = (MathVector[]) null;
      this.m_nodeVelocity = (MathVector[]) null;
      this.m_nodeLength = (float[]) null;
      this.m_nodeLengthInv = (float[]) null;
      this.m_nodeFullLength = (float[]) null;
    }
  }
}
