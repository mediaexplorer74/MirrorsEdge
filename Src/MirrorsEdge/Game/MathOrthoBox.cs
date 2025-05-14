// Decompiled with JetBrains decompiler
// Type: game.MathOrthoBox
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using System;

#nullable disable
namespace game
{
  public struct MathOrthoBox
  {
    public MathVector min;
    public MathVector max;
    private bool m_active;

    public MathOrthoBox(MathOrthoBox other)
    {
      this.min = other.min;
      this.max = other.max;
      this.m_active = other.m_active;
    }

    public MathOrthoBox(float x1, float y1, float z1, float x2, float y2, float z2)
    {
      this.min = new MathVector();
      this.max = new MathVector();
      this.m_active = false;
      this.setCoordinates(x1, y1, z1, x2, y2, z2);
    }

    public MathOrthoBox(MathVector vec1, MathVector vec2)
    {
      this.min = new MathVector();
      this.max = new MathVector();
      this.m_active = false;
      this.setCoordinates(vec1.x, vec1.y, vec1.z, vec2.x, vec2.y, vec2.z);
    }

    public MathOrthoBox(MathOrthoBox box, MathVector offset)
    {
      this.min = box.min;
      this.max = box.max;
      this.m_active = true;
      this.min += offset;
      this.max += offset;
    }

    public MathOrthoBox CopyFrom(MathOrthoBox other)
    {
      this.min = other.min;
      this.max = other.max;
      return this;
    }

    public static MathOrthoBox operator +(MathOrthoBox current, MathVector vector)
    {
      MathOrthoBox mathOrthoBox = new MathOrthoBox(current);
      mathOrthoBox.min += vector;
      mathOrthoBox.max += vector;
      return mathOrthoBox;
    }

    public static MathOrthoBox operator -(MathOrthoBox current, MathVector vector)
    {
      MathOrthoBox mathOrthoBox = new MathOrthoBox(current);
      mathOrthoBox.min -= vector;
      mathOrthoBox.max -= vector;
      return mathOrthoBox;
    }

    public void deactivate() => this.m_active = false;

    public void setCoordinates(float x1, float y1, float z1, float x2, float y2, float z2)
    {
      this.min.x = Math.Min(x1, x2);
      this.min.y = Math.Min(y1, y2);
      this.min.z = Math.Min(z1, z2);
      this.max.x = Math.Max(x1, x2);
      this.max.y = Math.Max(y1, y2);
      this.max.z = Math.Max(z1, z2);
      this.m_active = true;
    }

    public void setCoordinates(MathOrthoBox other)
    {
      this.min = other.min;
      this.max = other.max;
      this.m_active = true;
    }

    public void setF(DataInputStream dis)
    {
      this.min.setF(dis);
      this.max.setF(dis);
      this.m_active = true;
    }

    public void setUnion(MathOrthoBox box1, MathOrthoBox box2)
    {
      this.min.x = Math.Min(box1.min.x, box2.min.x);
      this.min.y = Math.Min(box1.min.y, box2.min.y);
      this.min.z = Math.Min(box1.min.z, box2.min.z);
      this.max.x = Math.Max(box1.max.x, box2.max.x);
      this.max.y = Math.Max(box1.max.y, box2.max.y);
      this.max.z = Math.Max(box1.max.z, box2.max.z);
      this.m_active = true;
    }

    public bool intersects(MathVector other)
    {
      return this.m_active && (double) this.min.x < (double) other.x && (double) other.x < (double) this.max.x && (double) this.min.y < (double) other.y && (double) other.y < (double) this.max.y && (double) this.min.z < (double) other.z && (double) other.z < (double) this.max.z;
    }

    public bool intersects(MathLine line) => this.intersects(line, out float _, out float _);

    public bool intersects(MathLine line, out float tValue)
    {
      return this.intersects(line, out tValue, out float _);
    }

    public bool intersects(MathLine line, out float minTValue, out float maxTValue)
    {
      bool flag = false;
      float val1_1 = 0.0f;
      float val1_2 = 0.0f;
      if (!GameCommon.isZero(line.direction.x))
      {
        float tatX1 = line.calculateTatX(this.min.x);
        float tatX2 = line.calculateTatX(this.max.x);
        val1_1 = Math.Min(tatX1, tatX2);
        val1_2 = Math.Max(tatX1, tatX2);
        flag = true;
      }
      if (!GameCommon.isZero(line.direction.y))
      {
        float tatY1 = line.calculateTatY(this.min.y);
        float tatY2 = line.calculateTatY(this.max.y);
        float val2_1 = Math.Min(tatY1, tatY2);
        float val2_2 = Math.Max(tatY1, tatY2);
        if (flag)
        {
          val1_1 = Math.Max(val1_1, val2_1);
          val1_2 = Math.Min(val1_2, val2_2);
        }
        else
        {
          val1_1 = val2_1;
          val1_2 = val2_2;
          flag = true;
        }
      }
      if (!GameCommon.isZero(line.direction.z))
      {
        float tatZ1 = line.calculateTatZ(this.min.z);
        float tatZ2 = line.calculateTatZ(this.max.z);
        float val2_3 = Math.Min(tatZ1, tatZ2);
        float val2_4 = Math.Max(tatZ1, tatZ2);
        if (flag)
        {
          val1_1 = Math.Max(val1_1, val2_3);
          val1_2 = Math.Min(val1_2, val2_4);
        }
        else
        {
          val1_1 = val2_3;
          val1_2 = val2_4;
        }
      }
      if ((double) val1_2 < (double) val1_1)
      {
        minTValue = 0.0f;
        maxTValue = 0.0f;
        return false;
      }
      minTValue = val1_1;
      maxTValue = val1_2;
      return true;
    }

    public bool intersects(MathOrthoBox other)
    {
      return this.m_active && (double) other.max.x > (double) this.min.x && (double) this.max.x > (double) other.min.x && (double) other.max.y > (double) this.min.y && (double) this.max.y > (double) other.min.y && (double) other.max.z > (double) this.min.z && (double) this.max.z > (double) other.min.z;
    }

    public bool intersectsX(MathOrthoBox other)
    {
      return this.m_active && (double) other.max.x > (double) this.min.x && (double) this.max.x > (double) other.min.x;
    }

    public bool intersectsY(MathOrthoBox other)
    {
      return this.m_active && (double) other.max.y > (double) this.min.y && (double) this.max.y > (double) other.min.y;
    }

    public bool intersectsZ(MathOrthoBox other)
    {
      return this.m_active && (double) other.max.z > (double) this.min.z && (double) this.max.z > (double) other.min.z;
    }
  }
}
