
// Type: game.MathVector
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using System;

#nullable disable
namespace game
{
  public struct MathVector
  {
    public float x;
    public float y;
    public float z;

    public MathVector(MathVector other)
    {
      this.x = other.x;
      this.y = other.y;
      this.z = other.z;
    }

    public MathVector(float init_x, float init_y, float init_z)
    {
      this.x = init_x;
      this.y = init_y;
      this.z = init_z;
    }

    public MathVector(MathLine line, float t)
    {
      this.x = line.origin.x + t * line.direction.x;
      this.y = line.origin.y + t * line.direction.y;
      this.z = line.origin.z + t * line.direction.z;
    }

    public MathVector CopyFrom(MathVector other)
    {
      this.x = other.x;
      this.y = other.y;
      this.z = other.z;
      return this;
    }

    public static bool operator ==(MathVector ImpliedObject, MathVector other)
    {
      return GameCommon.compareFloats(ImpliedObject.x, other.x) && GameCommon.compareFloats(ImpliedObject.y, other.y) && GameCommon.compareFloats(ImpliedObject.z, other.z);
    }

    public override bool Equals(object obj) => base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();

    public static bool operator !=(MathVector ImpliedObject, MathVector other)
    {
      return !GameCommon.compareFloats(ImpliedObject.x, other.x) || !GameCommon.compareFloats(ImpliedObject.y, other.y) || !GameCommon.compareFloats(ImpliedObject.z, other.z);
    }

    public static MathVector operator +(MathVector ImpliedObject, MathVector other)
    {
      return new MathVector(ImpliedObject.x + other.x, ImpliedObject.y + other.y, ImpliedObject.z + other.z);
    }

    public static MathVector operator -(MathVector ImpliedObject, MathVector other)
    {
      return new MathVector(ImpliedObject.x - other.x, ImpliedObject.y - other.y, ImpliedObject.z - other.z);
    }

    public static MathVector operator *(MathVector ImpliedObject, float other)
    {
      return new MathVector(ImpliedObject.x * other, ImpliedObject.y * other, ImpliedObject.z * other);
    }

    public static MathVector operator /(MathVector ImpliedObject, float other)
    {
      return new MathVector(ImpliedObject.x / other, ImpliedObject.y / other, ImpliedObject.z / other);
    }

    public static MathVector operator *(float f, MathVector v)
    {
      return new MathVector(v.x * f, v.y * f, v.z * f);
    }

    public bool isZero()
    {
      return (double) this.x == 0.0 && (double) this.y == 0.0 && (double) this.z == 0.0;
    }

    public bool isOrthogonal()
    {
      return ((double) this.x == 0.0 || (double) this.y == 0.0 || (double) this.z == 0.0) && (double) this.x != 0.0 ^ (double) this.y != 0.0 ^ (double) this.z != 0.0;
    }

    public void set(float newX, float newY, float newZ)
    {
      this.x = newX;
      this.y = newY;
      this.z = newZ;
    }

    public void set(MathVector other)
    {
      this.x = other.x;
      this.y = other.y;
      this.z = other.z;
    }

    public void set(DataInputStream dis)
    {
      this.x = dis.readFloat();
      this.y = dis.readFloat();
      this.z = dis.readFloat();
    }

    public void setF(DataInputStream dis)
    {
      this.x = (float) dis.readInt() * 1.52587891E-05f;
      this.y = (float) dis.readInt() * 1.52587891E-05f;
      this.z = (float) dis.readInt() * 1.52587891E-05f;
    }

    public void reverse()
    {
      this.x = -this.x;
      this.y = -this.y;
      this.z = -this.z;
    }

    public float dot(MathVector other)
    {
      return (float) ((double) this.x * (double) other.x + (double) this.y * (double) other.y + (double) this.z * (double) other.z);
    }

    public void cross(MathVector other, ref MathVector result)
    {
      float newX = (float) ((double) this.y * (double) other.z - (double) this.z * (double) other.y);
      float newY = (float) ((double) this.z * (double) other.x - (double) this.x * (double) other.z);
      float newZ = (float) ((double) this.x * (double) other.y - (double) this.y * (double) other.x);
      result.set(newX, newY, newZ);
    }

    public MathVector cross(MathVector other)
    {
      return new MathVector((float) ((double) this.y * (double) other.z - (double) this.z * (double) other.y), (float) ((double) this.z * (double) other.x - (double) this.x * (double) other.z), (float) ((double) this.x * (double) other.y - (double) this.y * (double) other.x));
    }

    public float getLengthSq()
    {
      return (float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y + (double) this.z * (double) this.z);
    }

    public float getLength()
    {
      return (float) Math.Sqrt((double) this.x * (double) this.x + (double) this.y * (double) this.y + (double) this.z * (double) this.z);
    }

    public void setLength(float newMagnitude)
    {
      float num1 = (float) Math.Sqrt((double) this.x * (double) this.x + (double) this.y * (double) this.y + (double) this.z * (double) this.z);
      float num2 = newMagnitude / num1;
      this.x *= num2;
      this.y *= num2;
      this.z *= num2;
    }

    public void rotateXAxis(float angleRad)
    {
      float num1 = (float) Math.Sin((double) angleRad);
      float num2 = (float) Math.Cos((double) angleRad);
      float y = this.y;
      float z = this.z;
      this.y = (float) ((double) num2 * (double) y + (double) num1 * (double) z);
      this.z = (float) ((double) num2 * (double) z - (double) num1 * (double) y);
    }

    public void rotateYAxis(float angleRad)
    {
      float num1 = (float) Math.Sin((double) angleRad);
      float num2 = (float) Math.Cos((double) angleRad);
      float x = this.x;
      float z = this.z;
      this.x = (float) ((double) num2 * (double) x - (double) num1 * (double) z);
      this.z = (float) ((double) num2 * (double) z + (double) num1 * (double) x);
    }

    public MathVector normalise()
    {
      float num = 1f / (float) Math.Sqrt((double) this.x * (double) this.x + (double) this.y * (double) this.y + (double) this.z * (double) this.z);
      this.x *= num;
      this.y *= num;
      this.z *= num;
      return this;
    }

    public void setAsLinearInterpolation(MathVector low, MathVector hi, float progress)
    {
      float num = 1f - progress;
      this.x = (float) ((double) low.x * (double) num + (double) hi.x * (double) progress);
      this.y = (float) ((double) low.y * (double) num + (double) hi.y * (double) progress);
      this.z = (float) ((double) low.z * (double) num + (double) hi.z * (double) progress);
    }

    public void calculateArbitraryPerpendicular(ref MathVector vectorToSet)
    {
      if (!GameCommon.isZero(this.y))
      {
        float newY = (float) -((double) this.z / (double) this.y);
        vectorToSet.set(0.0f, newY, 1f);
      }
      else if (!GameCommon.isZero(this.x))
      {
        float newX = (float) -((double) this.z / (double) this.x);
        vectorToSet.set(newX, 0.0f, 1f);
      }
      else if (!GameCommon.isZero(this.z))
      {
        float newX = (float) -((double) this.y / (double) this.x);
        vectorToSet.set(newX, 1f, 0.0f);
      }
      else
        vectorToSet.set(0.0f, 0.0f, 0.0f);
    }
  }
}
