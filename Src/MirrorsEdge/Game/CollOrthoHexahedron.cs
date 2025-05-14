
// Type: game.CollOrthoHexahedron
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace game
{
  public class CollOrthoHexahedron : CollShape
  {
    public CollOrthoHexahedron()
      : base(CollShape.ShapeType.SHAPE_TYPE_ORTHO_HEXAHEDRON, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f)
    {
    }

    public CollOrthoHexahedron(MathOrthoBox orthoBounds)
      : base(CollShape.ShapeType.SHAPE_TYPE_ORTHO_HEXAHEDRON, orthoBounds)
    {
    }

    public CollOrthoHexahedron(float x1, float y1, float z1, float x2, float y2, float z2)
      : base(CollShape.ShapeType.SHAPE_TYPE_ORTHO_HEXAHEDRON, x1, y1, z1, x2, y2, z2)
    {
    }

    public override void Destructor() => base.Destructor();

    public override bool intersects(MathLine line, ref float minT, ref float maxT)
    {
      MathOrthoBox mathOrthoBox = new MathOrthoBox(this.m_globalOrthoBounds) - line.origin;
      float val1_1 = -1E+09f;
      float val1_2 = 1E+09f;
      if ((double) line.direction.x != 0.0)
      {
        float val1_3 = mathOrthoBox.min.x / line.direction.x;
        float val2_1 = mathOrthoBox.max.x / line.direction.x;
        float val2_2 = Math.Min(val1_3, val2_1);
        float val2_3 = Math.Max(val1_3, val2_1);
        if ((double) val2_3 < (double) val1_1 || (double) val1_2 < (double) val2_2)
          return false;
        val1_1 = Math.Max(val1_1, val2_2);
        val1_2 = Math.Min(val1_2, val2_3);
      }
      else if ((double) line.origin.x < (double) this.m_globalOrthoBounds.min.x || (double) this.m_globalOrthoBounds.max.x < (double) line.origin.x)
        return false;
      if ((double) line.direction.y != 0.0)
      {
        float val1_4 = mathOrthoBox.min.y / line.direction.y;
        float val2_4 = mathOrthoBox.max.y / line.direction.y;
        float val2_5 = Math.Min(val1_4, val2_4);
        float val2_6 = Math.Max(val1_4, val2_4);
        if ((double) val2_6 < (double) val1_1 || (double) val1_2 < (double) val2_5)
          return false;
        val1_1 = Math.Max(val1_1, val2_5);
        val1_2 = Math.Min(val1_2, val2_6);
      }
      else if ((double) line.origin.y < (double) this.m_globalOrthoBounds.min.y || (double) this.m_globalOrthoBounds.max.y < (double) line.origin.y)
        return false;
      if ((double) line.direction.z != 0.0)
      {
        float val1_5 = mathOrthoBox.min.z / line.direction.z;
        float val2_7 = mathOrthoBox.max.z / line.direction.z;
        float val2_8 = Math.Min(val1_5, val2_7);
        float val2_9 = Math.Max(val1_5, val2_7);
        if ((double) val2_9 < (double) val1_1 || (double) val1_2 < (double) val2_8)
          return false;
        val1_1 = Math.Max(val1_1, val2_8);
        val1_2 = Math.Min(val1_2, val2_9);
      }
      else if ((double) line.origin.z < (double) this.m_globalOrthoBounds.min.z || (double) this.m_globalOrthoBounds.max.z < (double) line.origin.z)
        return false;
      minT = val1_1;
      maxT = val1_2;
      return true;
    }

    public override void addNonOrthogonalAxesTo(SeperatedAxesList sepAxesList, int shapeIndex)
    {
    }

    public override void setNonOrthogonalRangeOnAxis(SeperatedAxis axis, int shapeIndex)
    {
      MathVector direction = axis.getDirection();
      MathVector min = this.m_globalOrthoBounds.min;
      MathVector max = this.m_globalOrthoBounds.max;
      if ((double) direction.x <= -0.0099999997764825821)
      {
        min.x = this.m_globalOrthoBounds.max.x;
        max.x = this.m_globalOrthoBounds.min.x;
      }
      if ((double) direction.y <= -0.0099999997764825821)
      {
        min.y = this.m_globalOrthoBounds.max.y;
        max.y = this.m_globalOrthoBounds.min.y;
      }
      if ((double) direction.z <= -0.0099999997764825821)
      {
        min.z = this.m_globalOrthoBounds.max.z;
        max.z = this.m_globalOrthoBounds.min.z;
      }
      float distanceOfPoint1 = axis.getDistanceOfPoint(min);
      float distanceOfPoint2 = axis.getDistanceOfPoint(max);
      axis.setTValues(shapeIndex, distanceOfPoint1, distanceOfPoint2);
    }
  }
}
