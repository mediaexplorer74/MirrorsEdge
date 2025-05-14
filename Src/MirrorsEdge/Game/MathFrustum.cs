// Decompiled with JetBrains decompiler
// Type: game.MathFrustum
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using System;

#nullable disable
namespace game
{
  public class MathFrustum
  {
    public MathLine middleLine;
    public MathPlane farPlane;
    private MathFrustum.CullingPlane[] m_cullingPlanes = new MathFrustum.CullingPlane[6];

    public int intersectAABB(MathVector min, MathVector max)
    {
      int num = 1;
      for (int index = 2; index < 6; ++index)
      {
        MathVector other1 = new MathVector((double) this.m_cullingPlanes[index].m_normal.x > 0.0 ? max.x : min.x, (double) this.m_cullingPlanes[index].m_normal.y > 0.0 ? max.y : min.y, (double) this.m_cullingPlanes[index].m_normal.z > 0.0 ? max.z : min.z);
        MathVector other2 = new MathVector((double) this.m_cullingPlanes[index].m_normal.x < 0.0 ? max.x : min.x, (double) this.m_cullingPlanes[index].m_normal.y < 0.0 ? max.y : min.y, (double) this.m_cullingPlanes[index].m_normal.z < 0.0 ? max.z : min.z);
        if ((double) (this.m_cullingPlanes[index].m_normal.dot(other2) + this.m_cullingPlanes[index].m_distance) > 0.0)
          return -1;
        if ((double) (this.m_cullingPlanes[index].m_normal.dot(other1) + this.m_cullingPlanes[index].m_distance) >= 0.0)
          num = 0;
      }
      return num;
    }

    public int intersectAABBCoherency(MathVector min, MathVector max, ref uint index)
    {
      int num = 1;
      for (int index1 = 1; index1 < 6; ++index1)
      {
        uint index2 = (uint) index1;
        if ((long) index1 != (long) index)
        {
          if (index1 == 1 && index >= 0U && index < 6U)
            index2 = index;
          MathVector other = new MathVector((double) this.m_cullingPlanes[(int) index2].m_normal.x < 0.0 ? max.x : min.x, (double) this.m_cullingPlanes[(int) index2].m_normal.y < 0.0 ? max.y : min.y, (double) this.m_cullingPlanes[(int) index2].m_normal.z < 0.0 ? max.z : min.z);
          if ((double) (this.m_cullingPlanes[(int) index2].m_normal.dot(other) + this.m_cullingPlanes[(int) index2].m_distance) > 0.05000000074505806)
          {
            index = index2;
            return -1;
          }
        }
      }
      return num;
    }

    public int intersectXAABBCoherency(MathVector min, MathVector max, ref uint index)
    {
      int num = 1;
      for (int index1 = 1; index1 < 4; ++index1)
      {
        uint index2 = (uint) index1;
        if ((long) index1 != (long) index)
        {
          if (index1 == 1 && index >= 2U && index < 4U)
            index2 = index;
          MathVector other = new MathVector((double) this.m_cullingPlanes[(int) index2].m_normal.x < 0.0 ? max.x : min.x, (double) this.m_cullingPlanes[(int) index2].m_normal.y < 0.0 ? max.y : min.y, (double) this.m_cullingPlanes[(int) index2].m_normal.z < 0.0 ? max.z : min.z);
          if ((double) (this.m_cullingPlanes[(int) index2].m_normal.dot(other) + this.m_cullingPlanes[(int) index2].m_distance) > 0.05000000074505806)
          {
            index = index2;
            return -1;
          }
        }
      }
      return num;
    }

    public int intersectYAABBCoherency(MathVector min, MathVector max, ref uint index)
    {
      int num = 1;
      for (int index1 = 3; index1 < 6; ++index1)
      {
        uint index2 = (uint) index1;
        if ((long) index1 != (long) index)
        {
          if (index1 == 3 && index >= 4U && index < 6U)
            index2 = index;
          MathVector mathVector = new MathVector((double) this.m_cullingPlanes[(int) index2].m_normal.x > 0.0 ? max.x : min.x, (double) this.m_cullingPlanes[(int) index2].m_normal.y > 0.0 ? max.y : min.y, (double) this.m_cullingPlanes[(int) index2].m_normal.z > 0.0 ? max.z : min.z);
          MathVector other = new MathVector((double) this.m_cullingPlanes[(int) index2].m_normal.x < 0.0 ? max.x : min.x, (double) this.m_cullingPlanes[(int) index2].m_normal.y < 0.0 ? max.y : min.y, (double) this.m_cullingPlanes[(int) index2].m_normal.z < 0.0 ? max.z : min.z);
          if ((double) (this.m_cullingPlanes[(int) index2].m_normal.dot(other) + this.m_cullingPlanes[(int) index2].m_distance) > 0.05000000074505806)
          {
            index = index2;
            return -1;
          }
        }
      }
      return num;
    }

    public MathFrustum()
    {
      this.middleLine = new MathLine();
      this.farPlane = new MathPlane();
    }

    public void Destructor() => this.m_cullingPlanes = (MathFrustum.CullingPlane[]) null;

    public void set(
      MathVector lookFrom,
      MathVector lookAt,
      float nearDist,
      float farDist,
      float fov)
    {
      fov /= 1.5f;
      float num1 = (float) Math.Tan((double) JMath.toRadians(fov) * 0.5) * 1.1f;
      float num2 = (float) Math.Tan((double) JMath.toRadians(fov) * 0.5 / (320.0 / 533.0));
      float newMagnitude1 = farDist * num1;
      float newMagnitude2 = farDist * num2;
      MathVector other = new MathVector(lookAt.x - lookFrom.x, lookAt.y - lookFrom.y, lookAt.z - lookFrom.z);
      other.normalise();
      this.middleLine.origin = lookFrom;
      this.middleLine.direction.set(other);
      this.middleLine.direction.setLength(farDist);
      this.farPlane.origin.set(lookFrom.x + this.middleLine.direction.x, lookFrom.y + this.middleLine.direction.y, lookFrom.z + this.middleLine.direction.z);
      this.farPlane.basis1.set(0.0f, 1f, 0.0f);
      this.middleLine.direction.cross(this.farPlane.basis1, ref this.farPlane.basis2);
      this.farPlane.basis2.cross(this.middleLine.direction, ref this.farPlane.basis1);
      this.farPlane.basis1.setLength(newMagnitude1);
      this.farPlane.basis2.setLength(newMagnitude2);
      MathVector mathVector1 = lookFrom + other * nearDist;
      MathVector mathVector2 = lookFrom + other * farDist;
      MathVector basis2 = this.farPlane.basis2;
      MathVector basis1 = this.farPlane.basis1;
      basis2.normalise();
      basis1.normalise();
      MathVector mathVector3 = mathVector1 + basis1 * num1 * nearDist + basis2 * num2 * nearDist;
      MathVector mathVector4 = mathVector1 + basis1 * num1 * nearDist - basis2 * num2 * nearDist;
      MathVector mathVector5 = mathVector1 - basis1 * num1 * nearDist + basis2 * num2 * nearDist;
      MathVector mathVector6 = mathVector1 - basis1 * num1 * nearDist - basis2 * num2 * nearDist;
      MathVector mathVector7 = mathVector2 + basis1 * num1 * farDist + basis2 * num2 * farDist;
      MathVector mathVector8 = mathVector2 + basis1 * num1 * farDist - basis2 * num2 * farDist;
      MathVector mathVector9 = mathVector2 - basis1 * num1 * farDist + basis2 * num2 * farDist;
      MathVector mathVector10 = mathVector2 - basis1 * num1 * farDist - basis2 * num2 * farDist;
      this.m_cullingPlanes[2].m_normal = (mathVector5 - mathVector3).cross(mathVector9 - mathVector3);
      this.m_cullingPlanes[2].m_normal.normalise();
      this.m_cullingPlanes[2].m_distance = -mathVector3.dot(this.m_cullingPlanes[2].m_normal);
      this.m_cullingPlanes[3].m_normal = (mathVector4 - mathVector6).cross(mathVector10 - mathVector6);
      this.m_cullingPlanes[3].m_normal.normalise();
      this.m_cullingPlanes[3].m_distance = -mathVector6.dot(this.m_cullingPlanes[3].m_normal);
      this.m_cullingPlanes[4].m_normal = (mathVector3 - mathVector4).cross(mathVector7 - mathVector4);
      this.m_cullingPlanes[4].m_normal.normalise();
      this.m_cullingPlanes[4].m_distance = -mathVector4.dot(this.m_cullingPlanes[4].m_normal);
      this.m_cullingPlanes[5].m_normal = (mathVector6 - mathVector5).cross(mathVector10 - mathVector5);
      this.m_cullingPlanes[5].m_normal.normalise();
      this.m_cullingPlanes[5].m_distance = -mathVector5.dot(this.m_cullingPlanes[5].m_normal);
    }

    public bool getScreenPosition(MathVector worldPos, ref int screenX, ref int screenY)
    {
      double closestTtoPoint1 = (double) this.middleLine.calculateClosestTToPoint(worldPos);
      if (closestTtoPoint1 < 0.0 || 1.0 < closestTtoPoint1)
        return false;
      MathLine mathLine = new MathLine(this.farPlane.origin, this.farPlane.basis1);
      mathLine.direction *= (float) closestTtoPoint1;
      double closestTtoPoint2 = (double) mathLine.calculateClosestTToPoint(new MathVector(worldPos));
      screenY = 320 - (int) ((closestTtoPoint2 + 1.0) * 160.0);
      mathLine.direction.CopyFrom(this.farPlane.basis2);
      mathLine.direction *= (float) closestTtoPoint1;
      double closestTtoPoint3 = (double) mathLine.calculateClosestTToPoint(worldPos);
      screenX = (int) ((closestTtoPoint3 + 1.0) * 267.0);
      return true;
    }

    public bool intersectsOnX(MathOrthoBox orthoBox)
    {
      MathLine middleLine = this.middleLine;
      middleLine.direction -= this.farPlane.basis2;
      float xatT1 = middleLine.calculateXatT(middleLine.calculateTatZ(orthoBox.min.z));
      float xatT2 = middleLine.calculateXatT(middleLine.calculateTatZ(orthoBox.max.z));
      if ((double) orthoBox.max.x < (double) xatT1 && (double) orthoBox.max.x < (double) xatT2)
        return false;
      MathLine mathLine = new MathLine(this.middleLine);
      mathLine.direction += this.farPlane.basis2;
      float xatT3 = mathLine.calculateXatT(mathLine.calculateTatZ(orthoBox.min.z));
      float xatT4 = mathLine.calculateXatT(mathLine.calculateTatZ(orthoBox.max.z));
      return (double) xatT3 >= (double) orthoBox.min.x || (double) xatT4 >= (double) orthoBox.min.x;
    }

    public bool intersectsOnY(MathOrthoBox orthoBox)
    {
      MathLine middleLine = this.middleLine;
      middleLine.direction -= this.farPlane.basis1;
      MathLine mathLine1 = middleLine;
      middleLine.direction -= this.farPlane.basis2;
      mathLine1.direction += this.farPlane.basis2;
      float yatT1 = middleLine.calculateYatT(middleLine.calculateTatZ(orthoBox.min.z));
      float yatT2 = middleLine.calculateYatT(middleLine.calculateTatZ(orthoBox.max.z));
      float yatT3 = mathLine1.calculateYatT(mathLine1.calculateTatZ(orthoBox.min.z));
      float yatT4 = mathLine1.calculateYatT(mathLine1.calculateTatZ(orthoBox.max.z));
      if ((double) orthoBox.max.y < (double) yatT1 && (double) orthoBox.max.y < (double) yatT2 && (double) orthoBox.max.y < (double) yatT3 && (double) orthoBox.max.y < (double) yatT4)
        return false;
      MathLine other = new MathLine(this.middleLine);
      other.direction += this.farPlane.basis1;
      MathLine mathLine2 = new MathLine(other);
      other.direction -= this.farPlane.basis2;
      mathLine2.direction += this.farPlane.basis2;
      float yatT5 = other.calculateYatT(other.calculateTatZ(orthoBox.min.z));
      float yatT6 = other.calculateYatT(other.calculateTatZ(orthoBox.max.z));
      float yatT7 = mathLine2.calculateYatT(mathLine2.calculateTatZ(orthoBox.min.z));
      float yatT8 = mathLine2.calculateYatT(mathLine2.calculateTatZ(orthoBox.max.z));
      return (double) yatT5 >= (double) orthoBox.min.y || (double) yatT6 >= (double) orthoBox.min.y || (double) yatT7 >= (double) orthoBox.min.y || (double) yatT8 >= (double) orthoBox.min.y;
    }

    private struct CullingPlane
    {
      public MathVector m_normal;
      public float m_distance;
    }
  }
}
