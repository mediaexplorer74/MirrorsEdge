
// Type: game.CollShape
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public abstract class CollShape
  {
    protected MathOrthoBox m_localOrthoBounds;
    protected MathOrthoBox m_globalOrthoBounds;
    public float m_bounce;
    public float m_inverseResistance;
    public int m_materialId;
    private CollShape.ShapeType m_shapeType;
    private static SeperatedAxesList[] s_sepAxisList = new SeperatedAxesList[3];

    public CollShape.ShapeType getShapeType() => this.m_shapeType;

    public bool isRamp()
    {
      return this.m_shapeType == CollShape.ShapeType.SHAPE_TYPE_RAMP_NEG_X || this.m_shapeType == CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X;
    }

    public CollShape(CollShape.ShapeType type, MathOrthoBox orthoBounds)
    {
      this.m_localOrthoBounds = new MathOrthoBox(orthoBounds);
      this.m_globalOrthoBounds = new MathOrthoBox(orthoBounds);
      this.m_bounce = 1f;
      this.m_inverseResistance = 1f;
      this.m_shapeType = type;
      this.m_materialId = -1;
    }

    public CollShape(
      CollShape.ShapeType type,
      float x1,
      float y1,
      float z1,
      float x2,
      float y2,
      float z2)
    {
      this.m_localOrthoBounds = new MathOrthoBox(x1, y1, z1, x2, y2, z2);
      this.m_globalOrthoBounds = new MathOrthoBox(x1, y1, z1, x2, y2, z2);
      this.m_bounce = 1f;
      this.m_inverseResistance = 1f;
      this.m_shapeType = type;
      this.m_materialId = -1;
    }

    public virtual void Destructor()
    {
    }

    public float getBounce() => this.m_bounce;

    public void setBounce(float bounce) => this.m_bounce = bounce;

    public float getInverseResistance() => this.m_inverseResistance;

    public void setResistance(float resistance) => this.m_inverseResistance = 1f - resistance;

    public MathOrthoBox getBounds() => this.m_globalOrthoBounds;

    public MathOrthoBox getLocalBounds() => this.m_localOrthoBounds;

    public void setLocalBounds(MathOrthoBox newBounds) => this.m_localOrthoBounds = newBounds;

    public void setGlobalBounds(MathOrthoBox newBounds) => this.m_globalOrthoBounds = newBounds;

    public void setPosition(MathVector position)
    {
      this.m_globalOrthoBounds = this.m_localOrthoBounds;
      this.m_globalOrthoBounds += position;
    }

    public abstract bool intersects(MathLine line, ref float minT, ref float maxT);

    public abstract void addNonOrthogonalAxesTo(SeperatedAxesList sepAxesList, int shapeIndex);

    public void setRangeOnAxis(SeperatedAxis axis, int shapeIndex)
    {
      switch (axis.getType())
      {
        case SeperatedAxis.AxisType.AXIS_X:
          axis.setTValues(shapeIndex, this.m_globalOrthoBounds.min.x, this.m_globalOrthoBounds.max.x);
          break;
        case SeperatedAxis.AxisType.AXIS_Y:
          axis.setTValues(shapeIndex, this.m_globalOrthoBounds.min.y, this.m_globalOrthoBounds.max.y);
          break;
        case SeperatedAxis.AxisType.AXIS_Z:
          axis.setTValues(shapeIndex, this.m_globalOrthoBounds.min.z, this.m_globalOrthoBounds.max.z);
          break;
        default:
          this.setNonOrthogonalRangeOnAxis(axis, shapeIndex);
          break;
      }
    }

    public abstract void setNonOrthogonalRangeOnAxis(SeperatedAxis axis, int shapeIndex);

    public bool intersects(MathVector point)
    {
      if ((double) point.x <= (double) this.m_globalOrthoBounds.min.x || (double) this.m_globalOrthoBounds.max.x <= (double) point.x || (double) point.y <= (double) this.m_globalOrthoBounds.min.y || (double) this.m_globalOrthoBounds.max.y <= (double) point.y || (double) point.z <= (double) this.m_globalOrthoBounds.min.z || (double) this.m_globalOrthoBounds.max.z <= (double) point.z)
        return false;
      SeperatedAxesList sepAxisList = CollShape.getSepAxisList(0);
      this.addNonOrthogonalAxesTo(sepAxisList, 0);
      int axisNum = sepAxisList.getAxisNum();
      for (int index = 0; index != axisNum; ++index)
      {
        SeperatedAxis axis = sepAxisList.getAxis(index);
        float distanceOfPoint = axis.getDistanceOfPoint(point);
        if ((double) distanceOfPoint < (double) axis.getMinT(0) || (double) axis.getMaxT(0) < (double) distanceOfPoint)
          return false;
      }
      return true;
    }

    public bool pointIntersects(MathVector point) => this.intersects(point);

    private static SeperatedAxesList getSepAxisList(int shapeIndex)
    {
      SeperatedAxesList sepAxisList = CollShape.s_sepAxisList[shapeIndex];
      if (sepAxisList == null)
      {
        sepAxisList = new SeperatedAxesList();
        CollShape.s_sepAxisList[shapeIndex] = sepAxisList;
      }
      sepAxisList.reset();
      return sepAxisList;
    }

    private static void freeSepAxisList()
    {
      for (int index = 0; index != 3; ++index)
        CollShape.s_sepAxisList[index] = (SeperatedAxesList) null;
    }

    public static bool testIntersection(CollShape shape1, CollShape shape2)
    {
      MathOrthoBox globalOrthoBounds1 = shape1.m_globalOrthoBounds;
      MathOrthoBox globalOrthoBounds2 = shape2.m_globalOrthoBounds;
      if ((double) globalOrthoBounds1.max.x <= (double) globalOrthoBounds2.min.x || (double) globalOrthoBounds2.max.x <= (double) globalOrthoBounds1.min.x || (double) globalOrthoBounds1.max.y <= (double) globalOrthoBounds2.min.y || (double) globalOrthoBounds2.max.y <= (double) globalOrthoBounds1.min.y || (double) globalOrthoBounds1.max.z <= (double) globalOrthoBounds2.min.z || (double) globalOrthoBounds2.max.z <= (double) globalOrthoBounds1.min.z)
        return false;
      SeperatedAxesList sepAxisList1 = CollShape.getSepAxisList(0);
      shape1.addNonOrthogonalAxesTo(sepAxisList1, 0);
      if (sepAxisList1.addRangeToAxesWithCheck(shape2, 1) != -1)
        return false;
      SeperatedAxesList sepAxisList2 = CollShape.getSepAxisList(1);
      shape2.addNonOrthogonalAxesTo(sepAxisList2, 1);
      return sepAxisList2.addRangeToAxesWithCheck(shape1, 0) == -1;
    }

    public static bool moveShape(
      CollShape movingShape,
      ref MathVector startPos,
      MathVector move,
      ref CollShape[] staticShapeArray,
      int staticShapeStartIndex,
      int staticShapeEndIndex,
      ref float adjustMoveMultiple,
      ref CollShape collidedShape,
      ref MathVector collisionNormal)
    {
      SeperatedAxesList sepAxisList = CollShape.getSepAxisList(0);
      sepAxisList.addOrthogonalAxes(movingShape);
      movingShape.addNonOrthogonalAxesTo(sepAxisList, 0);
      SeperatedAxesList seperatedAxesList1 = CollShape.getSepAxisList(1);
      SeperatedAxesList seperatedAxesList2 = (SeperatedAxesList) null;
      SeperatedAxis axisMove1 = (SeperatedAxis) null;
      for (int index = staticShapeStartIndex; index != staticShapeEndIndex; ++index)
      {
        CollShape shape = staticShapeArray[index];
        if (sepAxisList.addRangeToAxesWithCheck(shape, 1) == -1)
        {
          seperatedAxesList1.reset();
          shape.addNonOrthogonalAxesTo(seperatedAxesList1, 1);
          if (seperatedAxesList1.addRangeToAxesWithCheck(movingShape, 0) == -1)
          {
            if (seperatedAxesList2 == null)
            {
              SeperatedAxesList.getMoveOutMultiple(sepAxisList, seperatedAxesList1, move, 1, ref adjustMoveMultiple, ref axisMove1);
              SeperatedAxesList.storeStaticBounds(sepAxisList, sepAxisList);
              SeperatedAxesList.storeStaticBounds(seperatedAxesList1, seperatedAxesList1);
              seperatedAxesList2 = seperatedAxesList1;
              seperatedAxesList1 = CollShape.getSepAxisList(2);
              collidedShape = shape;
            }
            else
            {
              float moveOutMultiple = 0.0f;
              SeperatedAxis axisMove2 = (SeperatedAxis) null;
              SeperatedAxesList.getMoveOutMultiple(sepAxisList, seperatedAxesList1, move, 1, ref moveOutMultiple, ref axisMove2);
              if ((double) moveOutMultiple < (double) adjustMoveMultiple)
              {
                SeperatedAxesList.storeStaticBounds(sepAxisList, sepAxisList);
                SeperatedAxesList.storeStaticBounds(seperatedAxesList1, seperatedAxesList1);
                adjustMoveMultiple = moveOutMultiple;
                axisMove1 = axisMove2;
                collidedShape = shape;
                SeperatedAxesList seperatedAxesList3 = seperatedAxesList1;
                seperatedAxesList1 = seperatedAxesList2;
                seperatedAxesList2 = seperatedAxesList3;
              }
            }
          }
        }
      }
      if (seperatedAxesList2 == null)
      {
        startPos += move;
        return false;
      }
      bool flag = false;
      for (int index = staticShapeStartIndex; index != staticShapeEndIndex; ++index)
      {
        CollShape staticShape = staticShapeArray[index];
        SeperatedAxis combineAxis = (SeperatedAxis) null;
        if (collidedShape != staticShape && SeperatedAxesList.checkCombineAxes(sepAxisList, (CollShape) null, (CollShape) null, staticShape, ref combineAxis, true, move) && SeperatedAxesList.checkCombineAxes(seperatedAxesList2, (CollShape) null, (CollShape) null, staticShape, ref combineAxis, false, move))
        {
          seperatedAxesList1.reset();
          staticShape.addNonOrthogonalAxesTo(seperatedAxesList1, 1);
          if (SeperatedAxesList.checkCombineAxes(seperatedAxesList1, movingShape, collidedShape, (CollShape) null, ref combineAxis, false, move))
          {
            sepAxisList.combineTValues();
            seperatedAxesList2.combineTValues();
            seperatedAxesList1.combineTValues();
            seperatedAxesList2.addAxes(seperatedAxesList1);
            flag = true;
          }
        }
      }
      if (flag)
      {
        if (sepAxisList.noIntersection(0, 2) || seperatedAxesList2.noIntersection(0, 2))
        {
          startPos += move;
          return false;
        }
        SeperatedAxesList.getMoveOutMultiple(sepAxisList, seperatedAxesList2, move, 2, ref adjustMoveMultiple, ref axisMove1);
      }
      MathVector mathVector = move * adjustMoveMultiple;
      startPos += mathVector;
      axisMove1.ensureNoIntersection(ref startPos, movingShape.getLocalBounds(), move);
      axisMove1.getNormal(move, ref collisionNormal);
      return true;
    }

    public enum ShapeType
    {
      SHAPE_TYPE_ORTHO_HEXAHEDRON,
      SHAPE_TYPE_RAMP_NEG_X,
      SHAPE_TYPE_RAMP_POS_X,
      SHAPE_TYPE_OCTAHEDRON,
      SHAPE_TYPE_LINE,
    }
  }
}
