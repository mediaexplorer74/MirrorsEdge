// Decompiled with JetBrains decompiler
// Type: game.MathLine
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game
{
  public struct MathLine
  {
    public MathVector origin;
    public MathVector direction;

    public MathLine(MathLine other)
    {
      this.origin = other.origin;
      this.direction = other.direction;
    }

    public MathLine(
      float originX,
      float originY,
      float originZ,
      float dirX,
      float dirY,
      float dirZ)
    {
      this.origin = new MathVector(originX, originY, originZ);
      this.direction = new MathVector(dirX, dirY, dirZ);
    }

    public MathLine(MathVector startOrigin, float dirX, float dirY, float dirZ)
    {
      this.origin = new MathVector(startOrigin);
      this.direction = new MathVector(dirX, dirY, dirZ);
    }

    public MathLine(MathVector startOrigin, MathVector startDirection)
    {
      this.origin = startOrigin;
      this.direction = startDirection;
    }

    public MathLine CopyFrom(MathLine other)
    {
      this.origin = other.origin;
      this.direction = other.direction;
      return this;
    }

    public void set(
      float originX,
      float originY,
      float originZ,
      float dirX,
      float dirY,
      float dirZ)
    {
      this.origin.set(originX, originY, originZ);
      this.direction.set(dirX, dirY, dirZ);
    }

    public void set(MathVector newOrigin, MathVector newDirection)
    {
      this.origin = newOrigin;
      this.direction = newDirection;
    }

    public void set(MathLine other)
    {
      this.origin = other.origin;
      this.direction = other.direction;
    }

    public float calculateXatT(float t) => this.origin.x + t * this.direction.x;

    public float calculateYatT(float t) => this.origin.y + t * this.direction.y;

    public float calculateZatT(float t) => this.origin.z + t * this.direction.z;

    public float calculateTatX(float x)
    {
      return GameCommon.compareFloats(this.origin.x, x) ? 0.0f : (x - this.origin.x) / this.direction.x;
    }

    public float calculateTatY(float y)
    {
      return GameCommon.compareFloats(this.origin.y, y) ? 0.0f : (y - this.origin.y) / this.direction.y;
    }

    public float calculateTatZ(float z)
    {
      return GameCommon.compareFloats(this.origin.z, z) ? 0.0f : (z - this.origin.z) / this.direction.z;
    }

    public void calculatePointAtT(float t, ref MathVector point)
    {
      point.x = this.origin.x + t * this.direction.x;
      point.y = this.origin.y + t * this.direction.y;
      point.z = this.origin.z + t * this.direction.z;
    }

    public static float calculateClosestTToPoint(MathVector direction, MathVector point)
    {
      float num = (float) ((double) direction.x * (double) direction.x + (double) direction.y * (double) direction.y + (double) direction.z * (double) direction.z);
      return (double) num == 0.0 ? 0.0f : (float) ((double) direction.x * (double) point.x + (double) direction.y * (double) point.y + (double) direction.z * (double) point.z) / num;
    }

    public float calculateClosestTToPoint(MathVector point)
    {
      return MathLine.calculateClosestTToPoint(new MathVector(this.direction), new MathVector(new MathVector(point.x - this.origin.x, point.y - this.origin.y, point.z - this.origin.z)));
    }
  }
}
