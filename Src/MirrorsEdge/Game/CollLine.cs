
// Type: game.CollLine
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class CollLine : CollShape
  {
    private MathVector m_startPoint;
    private MathVector m_endPoint;
    private MathVector m_dir;
    private MathVector m_perp1;
    private MathVector m_perp2;
    private float m_dirMinT;
    private float m_dirMaxT;
    private float m_perp1MinT;
    private float m_perp1MaxT;
    private float m_perp2MinT;
    private float m_perp2MaxT;

    public CollLine(MathLine line)
      : base(CollShape.ShapeType.SHAPE_TYPE_LINE, line.origin.x, line.origin.y, line.origin.z, line.origin.x + line.direction.x, line.origin.y + line.direction.y, line.origin.z + line.direction.z)
    {
      this.m_startPoint = new MathVector(line.origin);
      this.m_endPoint = new MathVector(line.direction);
      this.m_dir = new MathVector(line.direction);
      this.m_perp1 = new MathVector();
      this.m_perp2 = new MathVector();
      this.m_dirMinT = 0.0f;
      this.m_dirMaxT = 0.0f;
      this.m_perp1MinT = 0.0f;
      this.m_perp1MaxT = 0.0f;
      this.m_perp2MinT = 0.0f;
      this.m_perp2MaxT = 0.0f;
      this.m_endPoint += this.m_startPoint;
      float num = 0.049999997f;
      MathVector point = new MathVector(line.origin);
      this.m_dir *= 1f / this.m_dir.getLength();
      this.m_dirMinT = MathLine.calculateClosestTToPoint(this.m_dir, point);
      this.m_dirMaxT = MathLine.calculateClosestTToPoint(this.m_dir, point + line.direction);
      this.m_dir.calculateArbitraryPerpendicular(ref this.m_perp1);
      this.m_perp1.normalise();
      this.m_perp1MinT = MathLine.calculateClosestTToPoint(this.m_perp1, line.origin);
      this.m_perp1MaxT = this.m_perp1MinT + num;
      this.m_perp1MinT -= num;
      this.m_dir.cross(this.m_perp1, ref this.m_perp2);
      this.m_perp2MinT = MathLine.calculateClosestTToPoint(this.m_perp2, line.origin);
      this.m_perp2MaxT = this.m_perp2MinT + num;
      this.m_perp2MinT -= num;
    }

    public override void Destructor() => base.Destructor();

    public override bool intersects(MathLine line, ref float minT, ref float maxT) => false;

    public override void addNonOrthogonalAxesTo(SeperatedAxesList sepAxesList, int shapeIndex)
    {
      sepAxesList.addAxis(this.m_dir, shapeIndex, this.m_dirMinT, this.m_dirMaxT);
      sepAxesList.addAxis(this.m_perp1, shapeIndex, this.m_perp1MinT, this.m_perp1MaxT);
      sepAxesList.addAxis(this.m_perp2, shapeIndex, this.m_perp2MinT, this.m_perp2MaxT);
    }

    public override void setNonOrthogonalRangeOnAxis(SeperatedAxis axis, int shapeIndex)
    {
      float distanceOfPoint1;
      float distanceOfPoint2;
      if ((double) this.m_dir.dot(axis.getDirection()) < 0.0)
      {
        distanceOfPoint1 = axis.getDistanceOfPoint(this.m_startPoint);
        distanceOfPoint2 = axis.getDistanceOfPoint(this.m_endPoint);
      }
      else
      {
        distanceOfPoint2 = axis.getDistanceOfPoint(this.m_startPoint);
        distanceOfPoint1 = axis.getDistanceOfPoint(this.m_endPoint);
      }
      axis.setTValues(shapeIndex, distanceOfPoint2, distanceOfPoint1);
    }
  }
}
