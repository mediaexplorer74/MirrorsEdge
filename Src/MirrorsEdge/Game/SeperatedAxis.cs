
// Type: game.SeperatedAxis
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class SeperatedAxis
  {
    public const int SHAPE_1 = 0;
    public const int SHAPE_MOVING = 0;
    public const int SHAPE_2 = 1;
    public const int SHAPE_STATIC = 1;
    public const int SHAPE_STORED = 2;
    public const int SHAPE_NUM = 3;
    public const int MIN_T_INDEX = 0;
    public const int MAX_T_INDEX = 1;
    public const int NUM_T_INDICES = 2;
    private SeperatedAxis.AxisType m_axisType;
    private MathVector m_axisDirection;
    private float[][] m_tRangeArray;

    public SeperatedAxis()
    {
      this.m_axisType = SeperatedAxis.AxisType.AXIS_NON_ORTHO;
      this.m_axisDirection = new MathVector();
      this.m_tRangeArray = new float[3][];
      for (int index = 0; index < 3; ++index)
        this.m_tRangeArray[index] = new float[2];
    }

    public void Destructor() => this.m_tRangeArray = (float[][]) null;

    public SeperatedAxis.AxisType getType() => this.m_axisType;

    public MathVector getDirection() => this.m_axisDirection;

    public void setAxis(MathVector direction, int shapeIndex, float minT, float maxT)
    {
      this.setAxis(direction, shapeIndex, minT, maxT, SeperatedAxis.AxisType.AXIS_NON_ORTHO);
    }

    public void setAxis(
      MathVector direction,
      int shapeIndex,
      float minT,
      float maxT,
      SeperatedAxis.AxisType axisType)
    {
      this.m_axisDirection = direction;
      this.setTValues(shapeIndex, minT, maxT);
      this.m_axisType = axisType;
    }

    public void setAxis(SeperatedAxis newAxis)
    {
      this.m_axisDirection = newAxis.m_axisDirection;
      this.m_tRangeArray[0][0] = newAxis.m_tRangeArray[0][0];
      this.m_tRangeArray[0][1] = newAxis.m_tRangeArray[0][1];
      this.m_tRangeArray[1][0] = newAxis.m_tRangeArray[1][0];
      this.m_tRangeArray[1][1] = newAxis.m_tRangeArray[1][1];
      this.m_tRangeArray[2][0] = newAxis.m_tRangeArray[2][0];
      this.m_tRangeArray[2][1] = newAxis.m_tRangeArray[2][1];
    }

    public float getMinT(int shapeIndex) => this.m_tRangeArray[shapeIndex][0];

    public float getMaxT(int shapeIndex) => this.m_tRangeArray[shapeIndex][1];

    public float[] getTValues(int shapeIndex) => this.m_tRangeArray[shapeIndex];

    public void setTValues(int shapeIndex, float minT, float maxT)
    {
      float[] tRange = this.m_tRangeArray[shapeIndex];
      tRange[0] = minT;
      tRange[1] = maxT;
    }

    public bool noIntersection()
    {
      return (double) this.m_tRangeArray[0][1] <= (double) this.m_tRangeArray[1][0] || (double) this.m_tRangeArray[1][1] <= (double) this.m_tRangeArray[0][0];
    }

    public bool noIntersection(int shape1, int shape2)
    {
      return (double) this.m_tRangeArray[shape1][1] <= (double) this.m_tRangeArray[shape2][0] || (double) this.m_tRangeArray[shape2][1] <= (double) this.m_tRangeArray[shape1][0];
    }

    public float getDistanceOfPoint(MathVector point)
    {
      return MathLine.calculateClosestTToPoint(this.m_axisDirection, point);
    }

    public bool getMoveOutVector(
      int staticShapeIndex,
      ref float moveDist,
      ref MathVector moveVector)
    {
      float num1 = -1f;
      float num2 = 0.0f;
      bool moveOutVector = false;
      float num3 = this.m_tRangeArray[staticShapeIndex][0] - this.m_tRangeArray[0][1];
      if ((double) num3 <= 0.0)
      {
        num1 = -num3;
        num2 = num3;
        moveOutVector = true;
      }
      float num4 = this.m_tRangeArray[staticShapeIndex][1] - this.m_tRangeArray[0][0];
      if (0.0 <= (double) num4 && (double) num4 <= (double) num1)
      {
        num1 = num4;
        num2 = num4;
        moveOutVector = true;
      }
      if (moveOutVector)
      {
        moveDist = num2;
        moveVector.set(this.m_axisDirection);
        moveVector *= num1;
      }
      return moveOutVector;
    }

    public float getMoveOutMultiple(MathVector move, int shapeIndex)
    {
      float num1 = 0.0f;
      if (this.m_axisDirection.isOrthogonal())
      {
        float num2 = 0.0f;
        if (!GameCommon.isZero(this.m_axisDirection.x))
        {
          num1 = move.x;
          num2 = this.m_axisDirection.x;
        }
        else if (!GameCommon.isZero(this.m_axisDirection.y))
        {
          num1 = move.y;
          num2 = this.m_axisDirection.y;
        }
        else if (!GameCommon.isZero(this.m_axisDirection.z))
        {
          num1 = move.z;
          num2 = this.m_axisDirection.z;
        }
        if ((double) num2 != 1.0)
          num1 /= num2;
      }
      else
        num1 = MathLine.calculateClosestTToPoint(this.m_axisDirection, move);
      if ((double) num1 < 0.0)
        return (float) (1.0 - (double) (this.m_tRangeArray[0][0] - this.m_tRangeArray[shapeIndex][1]) / (double) num1);
      return 0.0 < (double) num1 ? (float) (1.0 - (double) (this.m_tRangeArray[0][1] - this.m_tRangeArray[shapeIndex][0]) / (double) num1) : -999999f;
    }

    public void ensureNoIntersection(
      ref MathVector position,
      MathOrthoBox bounds,
      MathVector moveDir)
    {
      switch (this.m_axisType)
      {
        case SeperatedAxis.AxisType.AXIS_X:
          if ((double) moveDir.x < -0.0099999997764825821)
          {
            position.x = this.m_tRangeArray[2][1] - bounds.min.x;
            break;
          }
          if (0.0099999997764825821 >= (double) moveDir.x)
            break;
          position.x = this.m_tRangeArray[2][0] - bounds.max.x;
          break;
        case SeperatedAxis.AxisType.AXIS_Y:
          if ((double) moveDir.y < -0.0099999997764825821)
          {
            position.y = this.m_tRangeArray[2][1] - bounds.min.y;
            break;
          }
          if (0.0099999997764825821 >= (double) moveDir.y)
            break;
          position.y = this.m_tRangeArray[2][0] - bounds.max.y;
          break;
        case SeperatedAxis.AxisType.AXIS_Z:
          if ((double) moveDir.z < -0.0099999997764825821)
          {
            position.z = this.m_tRangeArray[2][1] - bounds.min.z;
            break;
          }
          if (0.0099999997764825821 >= (double) moveDir.z)
            break;
          position.z = this.m_tRangeArray[2][0] - bounds.max.z;
          break;
      }
    }

    public void getNormal(MathVector move, ref MathVector collisionNormal)
    {
      collisionNormal.set(this.m_axisDirection);
      if (0.0 > (double) collisionNormal.dot(move))
        return;
      collisionNormal *= -1f;
    }

    public enum AxisType
    {
      AXIS_X,
      AXIS_Y,
      AXIS_Z,
      AXIS_NON_ORTHO,
    }
  }
}
