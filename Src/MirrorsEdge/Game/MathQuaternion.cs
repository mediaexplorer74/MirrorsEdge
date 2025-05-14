
// Type: game.MathQuaternion
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using System;

#nullable disable
namespace game
{
  public struct MathQuaternion
  {
    private float m_c;
    private float m_i;
    private float m_j;
    private float m_k;

    public void Constructor()
    {
      this.m_c = 1f;
      this.m_i = 0.0f;
      this.m_j = 0.0f;
      this.m_k = 0.0f;
    }

    public MathQuaternion(MathQuaternion other)
    {
      this.m_c = other.m_c;
      this.m_i = other.m_i;
      this.m_j = other.m_j;
      this.m_k = other.m_k;
    }

    public MathQuaternion CopyFrom(MathQuaternion other)
    {
      this.m_c = other.m_c;
      this.m_i = other.m_i;
      this.m_j = other.m_j;
      this.m_k = other.m_k;
      return this;
    }

    public void setIdentity()
    {
      this.m_c = 1f;
      this.m_i = 0.0f;
      this.m_j = 0.0f;
      this.m_k = 0.0f;
    }

    public void normalise()
    {
      float num = 1f / (float) Math.Sqrt((double) this.m_c * (double) this.m_c + (double) this.m_i * (double) this.m_i + (double) this.m_j * (double) this.m_j + (double) this.m_k * (double) this.m_k);
      this.m_c *= num;
      this.m_i *= num;
      this.m_j *= num;
      this.m_k *= num;
    }

    public void worldRotate(MathQuaternion rotation)
    {
      this.worldRotate(rotation.m_c, rotation.m_i, rotation.m_j, rotation.m_k);
    }

    public void worldRotate(float c, float i, float j, float k)
    {
      float num1 = this.m_c * c;
      float num2 = this.m_c * i;
      float num3 = this.m_c * j;
      float num4 = this.m_c * k;
      float num5 = this.m_i * c;
      float num6 = this.m_i * i;
      float num7 = this.m_i * j;
      float num8 = this.m_i * k;
      float num9 = this.m_j * c;
      float num10 = this.m_j * i;
      float num11 = this.m_j * j;
      float num12 = this.m_j * k;
      float num13 = this.m_k * c;
      float num14 = this.m_k * i;
      float num15 = this.m_k * j;
      float num16 = this.m_k * k;
      this.m_c = num1 - num6 - num11 - num16;
      this.m_i = num2 + num5 + num12 - num15;
      this.m_j = num3 - num8 + num9 + num14;
      this.m_k = num4 + num7 - num10 + num13;
    }

    public void localRotate(MathQuaternion rotation)
    {
      this.localRotate(rotation.m_c, rotation.m_i, rotation.m_j, rotation.m_k);
    }

    public void localRotate(float c, float i, float j, float k)
    {
      float num1 = c * this.m_c;
      float num2 = c * this.m_i;
      float num3 = c * this.m_j;
      float num4 = c * this.m_k;
      float num5 = i * this.m_c;
      float num6 = i * this.m_i;
      float num7 = i * this.m_j;
      float num8 = i * this.m_k;
      float num9 = j * this.m_c;
      float num10 = j * this.m_i;
      float num11 = j * this.m_j;
      float num12 = j * this.m_k;
      float num13 = k * this.m_c;
      float num14 = k * this.m_i;
      float num15 = k * this.m_j;
      float num16 = k * this.m_k;
      this.m_c = num1 - num6 - num11 - num16;
      this.m_i = num2 + num5 + num12 - num15;
      this.m_j = num3 - num8 + num9 + num14;
      this.m_k = num4 + num7 - num10 + num13;
    }

    public void addToAzimuth(float radians)
    {
      float num1 = radians * 0.5f;
      float num2 = -(float) Math.Sin((double) num1);
      float num3 = (float) Math.Cos((double) num1);
      float c = this.m_c;
      float i = this.m_i;
      float j = this.m_j;
      float k = this.m_k;
      this.m_c = (float) ((double) num3 * (double) c - (double) num2 * (double) j);
      this.m_i = (float) ((double) num3 * (double) i + (double) num2 * (double) k);
      this.m_j = (float) ((double) num3 * (double) j + (double) num2 * (double) c);
      this.m_k = (float) ((double) num3 * (double) k - (double) num2 * (double) i);
    }

    public void addToElevation(float radians)
    {
      float num1 = radians * 0.5f;
      float num2 = -(float) Math.Sin((double) num1);
      float num3 = (float) Math.Cos((double) num1);
      float c = this.m_c;
      float i = this.m_i;
      float j = this.m_j;
      float k = this.m_k;
      this.m_c = (float) ((double) num3 * (double) c - (double) num2 * (double) i);
      this.m_i = (float) ((double) num3 * (double) i + (double) num2 * (double) c);
      this.m_j = (float) ((double) num3 * (double) j - (double) num2 * (double) k);
      this.m_k = (float) ((double) num3 * (double) k + (double) num2 * (double) j);
    }

    public void addToTilt(float radians)
    {
      float num1 = radians * 0.5f;
      float num2 = -(float) Math.Sin((double) num1);
      float num3 = (float) Math.Cos((double) num1);
      float c = this.m_c;
      float i = this.m_i;
      float j = this.m_j;
      float k = this.m_k;
      this.m_c = (float) ((double) num3 * (double) c - (double) num2 * (double) k);
      this.m_i = (float) ((double) num3 * (double) i - (double) num2 * (double) j);
      this.m_j = (float) ((double) num3 * (double) j + (double) num2 * (double) i);
      this.m_k = (float) ((double) num3 * (double) k + (double) num2 * (double) c);
    }

    public void addToPitch(float radians)
    {
      float num1 = radians * 0.5f;
      float num2 = -(float) Math.Sin((double) num1);
      float num3 = (float) Math.Cos((double) num1);
      float c = this.m_c;
      float i = this.m_i;
      float j = this.m_j;
      float k = this.m_k;
      this.m_c = (float) ((double) num3 * (double) c - (double) num2 * (double) i);
      this.m_i = (float) ((double) num3 * (double) i + (double) num2 * (double) c);
      this.m_j = (float) ((double) num3 * (double) j + (double) num2 * (double) k);
      this.m_k = (float) ((double) num3 * (double) k - (double) num2 * (double) j);
    }

    public void addToYaw(float radians)
    {
      float num1 = radians * 0.5f;
      float num2 = -(float) Math.Sin((double) num1);
      float num3 = (float) Math.Cos((double) num1);
      float c = this.m_c;
      float i = this.m_i;
      float j = this.m_j;
      float k = this.m_k;
      this.m_c = (float) ((double) num3 * (double) c - (double) num2 * (double) j);
      this.m_i = (float) ((double) num3 * (double) i - (double) num2 * (double) k);
      this.m_j = (float) ((double) num3 * (double) j + (double) num2 * (double) c);
      this.m_k = (float) ((double) num3 * (double) k + (double) num2 * (double) i);
    }

    public void addToRoll(float radians)
    {
      float num1 = radians * 0.5f;
      float num2 = -(float) Math.Sin((double) num1);
      float num3 = (float) Math.Cos((double) num1);
      float c = this.m_c;
      float i = this.m_i;
      float j = this.m_j;
      float k = this.m_k;
      this.m_c = (float) ((double) num3 * (double) c - (double) num2 * (double) k);
      this.m_i = (float) ((double) num3 * (double) i + (double) num2 * (double) j);
      this.m_j = (float) ((double) num3 * (double) j - (double) num2 * (double) i);
      this.m_k = (float) ((double) num3 * (double) k + (double) num2 * (double) c);
    }

    public void applyToTransform(ref Transform trans)
    {
      trans.postRotateQuat(this.m_i, this.m_j, this.m_k, this.m_c);
    }

    public void applyToVector(ref MathVector vec)
    {
      MathVector mathVector = new MathVector(vec);
      float num1 = this.m_c * this.m_i;
      float num2 = this.m_c * this.m_j;
      float num3 = this.m_c * this.m_k;
      float num4 = this.m_i * this.m_i;
      float num5 = this.m_i * this.m_j;
      float num6 = this.m_i * this.m_k;
      float num7 = this.m_j * this.m_j;
      float num8 = this.m_j * this.m_k;
      float num9 = this.m_k * this.m_k;
      vec.x = (float) ((1.0 - 2.0 * ((double) num7 + (double) num9)) * (double) mathVector.x + 2.0 * ((double) num5 - (double) num3) * (double) mathVector.y + 2.0 * ((double) num6 + (double) num2) * (double) mathVector.z);
      vec.y = (float) (2.0 * ((double) num5 + (double) num3) * (double) mathVector.x + (1.0 - 2.0 * ((double) num4 + (double) num9)) * (double) mathVector.y + 2.0 * ((double) num8 - (double) num1) * (double) mathVector.z);
      vec.z = (float) (2.0 * ((double) num6 - (double) num2) * (double) mathVector.x + 2.0 * ((double) num8 + (double) num1) * (double) mathVector.y + (1.0 - 2.0 * ((double) num4 + (double) num7)) * (double) mathVector.z);
    }
  }
}
