// Decompiled with JetBrains decompiler
// Type: game.CollOctahedron
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace game
{
  public class CollOctahedron : CollShape
  {
    public CollOctahedron(MathOrthoBox orthoBounds)
      : base(CollShape.ShapeType.SHAPE_TYPE_OCTAHEDRON, orthoBounds)
    {
    }

    public CollOctahedron(float x1, float y1, float z1, float x2, float y2, float z2)
      : base(CollShape.ShapeType.SHAPE_TYPE_OCTAHEDRON, x1, y1, z1, x2, y2, z2)
    {
    }

    public override void Destructor() => base.Destructor();

    public override bool intersects(MathLine line, ref float minT, ref float maxT) => false;

    public override void addNonOrthogonalAxesTo(SeperatedAxesList sepAxesList, int shapeIndex)
    {
      MathVector point1 = new MathVector((float) (((double) this.m_globalOrthoBounds.min.x + (double) this.m_globalOrthoBounds.max.x) * 0.5), this.m_globalOrthoBounds.min.y, (float) (((double) this.m_globalOrthoBounds.min.z + (double) this.m_globalOrthoBounds.max.z) * 0.5));
      MathVector point2 = new MathVector(this.m_globalOrthoBounds.min.x, this.m_globalOrthoBounds.max.y, point1.z);
      MathVector direction = new MathVector((float) (((double) this.m_globalOrthoBounds.min.x - (double) this.m_globalOrthoBounds.max.x) * 0.5), (float) (((double) this.m_globalOrthoBounds.max.y - (double) this.m_globalOrthoBounds.min.y) * 0.5), 0.0f);
      direction.normalise();
      float closestTtoPoint1 = MathLine.calculateClosestTToPoint(direction, point1);
      float closestTtoPoint2 = MathLine.calculateClosestTToPoint(direction, point2);
      sepAxesList.addAxis(direction, shapeIndex, closestTtoPoint1, closestTtoPoint2);
      direction.x = -direction.x;
      point2.x = this.m_globalOrthoBounds.max.x;
      float closestTtoPoint3 = MathLine.calculateClosestTToPoint(direction, point1);
      float closestTtoPoint4 = MathLine.calculateClosestTToPoint(direction, point2);
      sepAxesList.addAxis(direction, shapeIndex, closestTtoPoint3, closestTtoPoint4);
    }

    public override void setNonOrthogonalRangeOnAxis(SeperatedAxis axis, int shapeIndex)
    {
      MathVector direction = axis.getDirection();
      float init_x = (float) (((double) this.m_globalOrthoBounds.min.x + (double) this.m_globalOrthoBounds.max.x) * 0.5);
      float init_y = (float) (((double) this.m_globalOrthoBounds.min.y + (double) this.m_globalOrthoBounds.max.y) * 0.5);
      float init_z = (float) (((double) this.m_globalOrthoBounds.min.z + (double) this.m_globalOrthoBounds.max.z) * 0.5);
      MathVector point1 = new MathVector(this.m_globalOrthoBounds.min.x, init_y, this.m_globalOrthoBounds.min.z);
      MathVector point2 = new MathVector(this.m_globalOrthoBounds.max.x, init_y, this.m_globalOrthoBounds.max.z);
      MathVector point3 = new MathVector(init_x, this.m_globalOrthoBounds.min.y, init_z);
      MathVector point4 = new MathVector(init_x, this.m_globalOrthoBounds.max.y, init_z);
      if ((double) direction.x < 0.0)
      {
        point1.x = this.m_globalOrthoBounds.max.x;
        point2.x = this.m_globalOrthoBounds.min.x;
      }
      if ((double) direction.y < 0.0)
      {
        point3.y = this.m_globalOrthoBounds.max.z;
        point4.y = this.m_globalOrthoBounds.min.z;
      }
      if ((double) direction.z < 0.0)
      {
        point1.z = this.m_globalOrthoBounds.max.z;
        point2.z = this.m_globalOrthoBounds.min.z;
      }
      float num1 = axis.getDistanceOfPoint(point1);
      float num2 = axis.getDistanceOfPoint(point2);
      if (!GameCommon.isZero(direction.y))
      {
        num1 = Math.Min(num1, axis.getDistanceOfPoint(point3));
        num2 = Math.Max(num2, axis.getDistanceOfPoint(point4));
      }
      axis.setTValues(shapeIndex, num1, num2);
    }
  }
}
