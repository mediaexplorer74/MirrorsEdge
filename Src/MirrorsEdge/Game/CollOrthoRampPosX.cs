
// Type: game.CollOrthoRampPosX
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace game
{
  public class CollOrthoRampPosX : CollShape
  {
    public CollOrthoRampPosX(MathOrthoBox orthoBounds)
      : base(CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X, orthoBounds)
    {
    }

    public CollOrthoRampPosX(float x1, float y1, float z1, float x2, float y2, float z2)
      : base(CollShape.ShapeType.SHAPE_TYPE_RAMP_POS_X, x1, y1, z1, x2, y2, z2)
    {
    }

    public override void Destructor() => base.Destructor();

    public override bool intersects(MathLine line, ref float minT, ref float maxT)
    {
      bool flag1 = (double) line.direction.x == 0.0;
      bool flag2 = (double) line.direction.y == 0.0;
      bool flag3 = (double) line.direction.z == 0.0;
      if (!flag1 || flag2 || !flag3 || !GameCommon.inBounds(this.m_globalOrthoBounds.min.x, line.origin.x, this.m_globalOrthoBounds.max.x) || !GameCommon.inBounds(this.m_globalOrthoBounds.min.z, line.origin.z, this.m_globalOrthoBounds.max.z))
        return false;
      float y = this.m_globalOrthoBounds.min.y + (float) (((double) this.m_globalOrthoBounds.max.y - (double) this.m_globalOrthoBounds.min.y) * ((double) line.origin.x - (double) this.m_globalOrthoBounds.min.x) / ((double) this.m_globalOrthoBounds.max.x - (double) this.m_globalOrthoBounds.min.x));
      float tatY1 = line.calculateTatY(y);
      float tatY2 = line.calculateTatY(Math.Max(this.m_globalOrthoBounds.min.y, y - 0.05f));
      minT = Math.Min(tatY1, tatY2);
      maxT = Math.Max(tatY1, tatY2);
      return true;
    }

    public override void addNonOrthogonalAxesTo(SeperatedAxesList sepAxesList, int shapeIndex)
    {
      MathVector direction = new MathVector(this.m_globalOrthoBounds.min.y - this.m_globalOrthoBounds.max.y, this.m_globalOrthoBounds.max.x - this.m_globalOrthoBounds.min.x, 0.0f);
      direction.normalise();
      MathVector min = this.m_globalOrthoBounds.min;
      float closestTtoPoint = MathLine.calculateClosestTToPoint(direction, min);
      min.x = this.m_globalOrthoBounds.max.x;
      min.y = this.m_globalOrthoBounds.max.y;
      float maxT = Math.Max(closestTtoPoint, MathLine.calculateClosestTToPoint(direction, min));
      sepAxesList.addAxis(direction, shapeIndex, maxT - 0.05f, maxT);
    }

    public override void setNonOrthogonalRangeOnAxis(SeperatedAxis axis, int shapeIndex)
    {
      MathVector direction = axis.getDirection();
      float minT = 0.0f;
      float maxT = 0.0f;
      float z1;
      float z2;
      if ((double) direction.z < -0.0099999997764825821)
      {
        z1 = this.m_globalOrthoBounds.max.z;
        z2 = this.m_globalOrthoBounds.min.z;
      }
      else
      {
        z1 = this.m_globalOrthoBounds.min.z;
        z2 = this.m_globalOrthoBounds.max.z;
      }
      bool flag1 = GameCommon.isZero(direction.y);
      bool flag2 = GameCommon.isZero(direction.x);
      if (flag1 && flag2)
      {
        MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.min.y, z1);
        minT = axis.getDistanceOfPoint(point);
        point.z = z2;
        maxT = axis.getDistanceOfPoint(point);
      }
      else if (flag1)
      {
        if ((double) direction.x < 0.0)
        {
          MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.min.y, z1);
          minT = axis.getDistanceOfPoint(point);
          point.x = this.m_globalOrthoBounds.min.x;
          maxT = axis.getDistanceOfPoint(point);
        }
        else
        {
          MathVector point = new MathVector(this.m_globalOrthoBounds.min.x, this.m_globalOrthoBounds.min.y, z1);
          minT = axis.getDistanceOfPoint(point);
          point.x = this.m_globalOrthoBounds.max.x;
          maxT = axis.getDistanceOfPoint(point);
        }
      }
      else if (flag2)
      {
        if ((double) direction.y < 0.0)
        {
          MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.max.y, z1);
          minT = axis.getDistanceOfPoint(point);
          point.y = this.m_globalOrthoBounds.min.y;
          maxT = axis.getDistanceOfPoint(point);
        }
        else
        {
          MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.min.y, z1);
          minT = axis.getDistanceOfPoint(point);
          point.y = this.m_globalOrthoBounds.max.y;
          maxT = axis.getDistanceOfPoint(point);
        }
      }
      else if ((double) direction.x < -0.0099999997764825821 && 0.0099999997764825821 < (double) direction.y)
      {
        MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.min.y, z1);
        minT = axis.getDistanceOfPoint(point);
        point.x = this.m_globalOrthoBounds.min.x;
        point.z = z2;
        float distanceOfPoint = axis.getDistanceOfPoint(point);
        point.x = this.m_globalOrthoBounds.max.x;
        point.y = this.m_globalOrthoBounds.max.y;
        maxT = Math.Max(distanceOfPoint, axis.getDistanceOfPoint(point));
      }
      else if (0.0099999997764825821 < (double) direction.x && (double) direction.y < -0.0099999997764825821)
      {
        MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.min.y, z1);
        maxT = axis.getDistanceOfPoint(point);
        point.x = this.m_globalOrthoBounds.min.x;
        point.z = z2;
        float distanceOfPoint = axis.getDistanceOfPoint(point);
        point.x = this.m_globalOrthoBounds.max.x;
        point.y = this.m_globalOrthoBounds.max.y;
        minT = Math.Min(distanceOfPoint, axis.getDistanceOfPoint(point));
      }
      else if (0.0099999997764825821 < (double) direction.x && 0.0099999997764825821 < (double) direction.y)
      {
        MathVector point = new MathVector(this.m_globalOrthoBounds.min.x, this.m_globalOrthoBounds.min.y, z1);
        minT = axis.getDistanceOfPoint(point);
        point.set(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.max.y, z2);
        maxT = axis.getDistanceOfPoint(point);
      }
      else if ((double) direction.x < -0.0099999997764825821 && (double) direction.y < -0.0099999997764825821)
      {
        MathVector point = new MathVector(this.m_globalOrthoBounds.max.x, this.m_globalOrthoBounds.max.y, z1);
        minT = axis.getDistanceOfPoint(point);
        point.set(this.m_globalOrthoBounds.min.x, this.m_globalOrthoBounds.min.y, z2);
        maxT = axis.getDistanceOfPoint(point);
      }
      axis.setTValues(shapeIndex, minT, maxT);
    }
  }
}
