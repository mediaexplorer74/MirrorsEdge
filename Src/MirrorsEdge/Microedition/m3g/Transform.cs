// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Transform
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace microedition.m3g
{
  public class Transform
  {
    private static readonly float[] s_Identity = new float[16]
    {
      1f,
      0.0f,
      0.0f,
      0.0f,
      0.0f,
      1f,
      0.0f,
      0.0f,
      0.0f,
      0.0f,
      1f,
      0.0f,
      0.0f,
      0.0f,
      0.0f,
      1f
    };
    private float[] m_Matrix = new float[16];
    private static float[] q = new float[4];
    private static Transform m_temp = new Transform();

    private static float fdot4(int bstride, float[] a, int apos, float[] b, int bpos)
    {
      return (float) ((double) a[apos] * (double) b[bpos] + (double) a[1 + apos] * (double) b[bstride + bpos] + (double) a[2 + apos] * (double) b[2 * bstride + bpos] + (double) a[3 + apos] * (double) b[3 * bstride + bpos]);
    }

    private static float fdot44(float[] a, int apos, float[] b, int bpos)
    {
      return (float) ((double) a[apos] * (double) b[bpos] + (double) a[1 + apos] * (double) b[4 + bpos] + (double) a[2 + apos] * (double) b[8 + bpos] + (double) a[3 + apos] * (double) b[12 + bpos]);
    }

    private static float fdot3(int bstride, float[] a, int apos, float[] b, int bpos)
    {
      return (float) ((double) a[apos] * (double) b[bpos] + (double) a[1 + apos] * (double) b[bstride + bpos] + (double) a[2 + apos] * (double) b[2 * bstride + bpos]);
    }

    public Transform(Transform rhs) => this.m_Matrix = rhs.m_Matrix;

    public Transform() => this.setIdentity();

    public float determinant3x3()
    {
      return (float) ((double) this.m_Matrix[0] * (double) Transform.cofactor2(1, 2, 1, 2, this.m_Matrix) + (double) this.m_Matrix[1] * (double) Transform.cofactor2(1, 2, 2, 0, this.m_Matrix) + (double) this.m_Matrix[2] * (double) Transform.cofactor2(1, 2, 0, 1, this.m_Matrix));
    }

    private static float cofactor2(int r1, int r2, int c1, int c2, float[] m)
    {
      float num1 = m[r1 * 4 + c1];
      float num2 = m[r1 * 4 + c2];
      float num3 = m[r2 * 4 + c1];
      float num4 = m[r2 * 4 + c2];
      return (float) ((double) num1 * (double) num4 - (double) num2 * (double) num3);
    }

    public void setIdentity() => this.set(Transform.s_Identity);

    public void set(Transform t)
    {
      Array.Copy((Array) t.m_Matrix, (Array) this.m_Matrix, this.m_Matrix.Length);
    }

    public void set(float[] matrix)
    {
      Array.Copy((Array) matrix, (Array) this.m_Matrix, this.m_Matrix.Length);
    }

    public void set(Matrix matrix)
    {
      this.m_Matrix[0] = matrix.M11;
      this.m_Matrix[1] = matrix.M21;
      this.m_Matrix[2] = matrix.M31;
      this.m_Matrix[3] = matrix.M41;
      this.m_Matrix[4] = matrix.M12;
      this.m_Matrix[5] = matrix.M22;
      this.m_Matrix[6] = matrix.M32;
      this.m_Matrix[7] = matrix.M42;
      this.m_Matrix[8] = matrix.M13;
      this.m_Matrix[9] = matrix.M23;
      this.m_Matrix[10] = matrix.M33;
      this.m_Matrix[11] = matrix.M43;
      this.m_Matrix[12] = matrix.M14;
      this.m_Matrix[13] = matrix.M24;
      this.m_Matrix[14] = matrix.M34;
      this.m_Matrix[15] = matrix.M44;
    }

    public void setx(int[] matrix)
    {
      float num = 1f / 65536f;
      for (int index = 0; index < 16; ++index)
        this.m_Matrix[index] = (float) matrix[index] * num;
    }

    public void get(ref float[] matrix)
    {
      Array.Copy((Array) this.m_Matrix, (Array) matrix, this.m_Matrix.Length);
    }

    public void get(ref Matrix matrix)
    {
      matrix.M11 = this.m_Matrix[0];
      matrix.M21 = this.m_Matrix[1];
      matrix.M31 = this.m_Matrix[2];
      matrix.M41 = this.m_Matrix[3];
      matrix.M12 = this.m_Matrix[4];
      matrix.M22 = this.m_Matrix[5];
      matrix.M32 = this.m_Matrix[6];
      matrix.M42 = this.m_Matrix[7];
      matrix.M13 = this.m_Matrix[8];
      matrix.M23 = this.m_Matrix[9];
      matrix.M33 = this.m_Matrix[10];
      matrix.M43 = this.m_Matrix[11];
      matrix.M14 = this.m_Matrix[12];
      matrix.M24 = this.m_Matrix[13];
      matrix.M34 = this.m_Matrix[14];
      matrix.M44 = this.m_Matrix[15];
    }

    public Matrix getMatrix()
    {
      Matrix matrix = new Matrix();
      this.get(ref matrix);
      return matrix;
    }

    public void getx(ref int[] matrix)
    {
      int num = 65536;
      for (int index = 0; index < 16; ++index)
        matrix[index] = (int) ((double) this.m_Matrix[index] * (double) num + 0.5);
    }

    public void getTranslate(ref float tx, ref float ty, ref float tz)
    {
      tx = this.m_Matrix[3];
      ty = this.m_Matrix[7];
      tz = this.m_Matrix[11];
    }

    public void getTranslatex(ref int tx, ref int ty, ref int tz)
    {
      tx = (int) ((double) this.m_Matrix[3] * 65536.0);
      ty = (int) ((double) this.m_Matrix[7] * 65536.0);
      tz = (int) ((double) this.m_Matrix[11] * 65536.0);
    }

    public void postTranslate(float tx, float ty, float tz)
    {
      this.m_Matrix[3] += (float) ((double) tx * (double) this.m_Matrix[0] + (double) ty * (double) this.m_Matrix[1] + (double) tz * (double) this.m_Matrix[2]);
      this.m_Matrix[7] += (float) ((double) tx * (double) this.m_Matrix[4] + (double) ty * (double) this.m_Matrix[5] + (double) tz * (double) this.m_Matrix[6]);
      this.m_Matrix[11] += (float) ((double) tx * (double) this.m_Matrix[8] + (double) ty * (double) this.m_Matrix[9] + (double) tz * (double) this.m_Matrix[10]);
    }

    public void postTranslatex(int tx, int ty, int tz)
    {
      float num = 1.52587891E-05f;
      this.postTranslate((float) tx * num, (float) ty * num, (float) tz * num);
    }

    public void postScale(float sx, float sy, float sz)
    {
      float num1 = sx;
      float num2 = sy;
      float num3 = sz;
      this.m_Matrix[0] *= num1;
      this.m_Matrix[1] *= num2;
      this.m_Matrix[2] *= num3;
      this.m_Matrix[4] *= num1;
      this.m_Matrix[5] *= num2;
      this.m_Matrix[6] *= num3;
      this.m_Matrix[8] *= num1;
      this.m_Matrix[9] *= num2;
      this.m_Matrix[10] *= num3;
      this.m_Matrix[12] *= num1;
      this.m_Matrix[13] *= num2;
      this.m_Matrix[14] *= num3;
    }

    public void postScalex(int sx, int sy, int sz)
    {
      float num = 1.52587891E-05f;
      this.postScale((float) sx * num, (float) sy * num, (float) sz * num);
    }

    public void postRotate(float degrees, float ax, float ay, float az)
    {
      Transform.angleAxisToQuat(degrees, ax, ay, az, Transform.q);
      this.postRotateQuat(Transform.q[0], Transform.q[1], Transform.q[2], Transform.q[3]);
    }

    public void postRotatex(int degrees, int ax, int ay, int az)
    {
      float num = 1.52587891E-05f;
      Transform.angleAxisToQuat((float) degrees * num, (float) ax * num, (float) ay * num, (float) az * num, Transform.q);
      this.postRotateQuat(Transform.q[0], Transform.q[1], Transform.q[2], Transform.q[3]);
    }

    public void postRotateQuat(float qx, float qy, float qz, float qw)
    {
      float num1 = 1f / (float) Math.Sqrt((double) qx * (double) qx + (double) qy * (double) qy + (double) qz * (double) qz + (double) qw * (double) qw);
      float num2 = num1 * qx;
      float num3 = num1 * qy;
      float num4 = num1 * qz;
      float num5 = num1 * qw;
      float num6 = (float) ((double) num2 * (double) num2 * 2.0);
      float num7 = (float) ((double) num2 * (double) num3 * 2.0);
      float num8 = (float) ((double) num2 * (double) num4 * 2.0);
      float num9 = (float) ((double) num2 * (double) num5 * 2.0);
      float num10 = (float) ((double) num3 * (double) num3 * 2.0);
      float num11 = (float) ((double) num3 * (double) num4 * 2.0);
      float num12 = (float) ((double) num3 * (double) num5 * 2.0);
      float num13 = (float) ((double) num4 * (double) num4 * 2.0);
      float num14 = (float) ((double) num4 * (double) num5 * 2.0);
      Transform.m_temp.setIdentity();
      float[] matrix = Transform.m_temp.m_Matrix;
      matrix[0] = (float) (1.0 - ((double) num10 + (double) num13));
      matrix[1] = num7 - num14;
      matrix[2] = num8 + num12;
      matrix[4] = num7 + num14;
      matrix[5] = (float) (1.0 - ((double) num6 + (double) num13));
      matrix[6] = num11 - num9;
      matrix[8] = num8 - num12;
      matrix[9] = num11 + num9;
      matrix[10] = (float) (1.0 - ((double) num6 + (double) num10));
      this.postMultiply_34(Transform.m_temp);
    }

    public void postRotateQuatx(int qx, int qy, int qz, int qw)
    {
      float num = 1.52587891E-05f;
      this.postRotateQuat((float) qx * num, (float) qy * num, (float) qz * num, (float) qw * num);
    }

    public void transformx(int[] vector)
    {
      float num1 = 1.52587891E-05f;
      float[] vector1 = new float[vector.Length];
      for (int index = 0; index < ((IEnumerable<int>) vector).Count<int>(); ++index)
        vector1[index] = (float) (int) ((double) vector[index] * (double) num1);
      this.transform(vector1);
      int num2 = 65536;
      for (int index = 0; index < vector.Length; ++index)
        vector[index] = (int) ((double) vector1[index] * (double) num2 + 0.5);
    }

    public void transform(float[] vector) => this.transform(vector, vector.Length);

    public void transform(float[] vector, int length)
    {
      float[] numArray = vector;
      for (int offset = 0; offset < length; offset += 4)
        Transform.transform4(this.m_Matrix, numArray, offset);
    }

    private static void transform4(float[] _a, float[] _value, int offset)
    {
      float[] numArray1 = _a;
      float[] numArray2 = _value;
      float num1 = numArray2[offset];
      float num2 = numArray2[1 + offset];
      float num3 = numArray2[2 + offset];
      float num4 = numArray2[3 + offset];
      numArray2[offset] = (float) ((double) numArray1[0] * (double) num1 + (double) numArray1[1] * (double) num2 + (double) numArray1[2] * (double) num3 + (double) numArray1[3] * (double) num4);
      numArray2[1 + offset] = (float) ((double) numArray1[4] * (double) num1 + (double) numArray1[5] * (double) num2 + (double) numArray1[6] * (double) num3 + (double) numArray1[7] * (double) num4);
      numArray2[2 + offset] = (float) ((double) numArray1[8] * (double) num1 + (double) numArray1[9] * (double) num2 + (double) numArray1[10] * (double) num3 + (double) numArray1[11] * (double) num4);
      numArray2[3 + offset] = (float) ((double) numArray1[12] * (double) num1 + (double) numArray1[13] * (double) num2 + (double) numArray1[14] * (double) num3 + (double) numArray1[15] * (double) num4);
    }

    private static void transpose(float[] m)
    {
      float num1 = m[1];
      m[1] = m[4];
      m[4] = num1;
      float num2 = m[2];
      m[2] = m[8];
      m[8] = num2;
      float num3 = m[3];
      m[3] = m[12];
      m[12] = num3;
      float num4 = m[6];
      m[6] = m[9];
      m[9] = num4;
      float num5 = m[7];
      m[7] = m[13];
      m[13] = num5;
      float num6 = m[11];
      m[11] = m[14];
      m[14] = num6;
    }

    public void postMultiply(Transform trans)
    {
      float[] matrix1 = trans.m_Matrix;
      float[] matrix2 = this.m_Matrix;
      float num1 = Transform.fdot44(matrix2, 0, matrix1, 0);
      float num2 = Transform.fdot44(matrix2, 0, matrix1, 1);
      float num3 = Transform.fdot44(matrix2, 0, matrix1, 2);
      float num4 = Transform.fdot44(matrix2, 0, matrix1, 3);
      matrix2[0] = num1;
      matrix2[1] = num2;
      matrix2[2] = num3;
      matrix2[3] = num4;
      float num5 = Transform.fdot44(matrix2, 4, matrix1, 0);
      float num6 = Transform.fdot44(matrix2, 4, matrix1, 1);
      float num7 = Transform.fdot44(matrix2, 4, matrix1, 2);
      float num8 = Transform.fdot44(matrix2, 4, matrix1, 3);
      matrix2[4] = num5;
      matrix2[5] = num6;
      matrix2[6] = num7;
      matrix2[7] = num8;
      float num9 = Transform.fdot44(matrix2, 8, matrix1, 0);
      float num10 = Transform.fdot44(matrix2, 8, matrix1, 1);
      float num11 = Transform.fdot44(matrix2, 8, matrix1, 2);
      float num12 = Transform.fdot44(matrix2, 8, matrix1, 3);
      matrix2[8] = num9;
      matrix2[9] = num10;
      matrix2[10] = num11;
      matrix2[11] = num12;
      float num13 = Transform.fdot44(matrix2, 12, matrix1, 0);
      float num14 = Transform.fdot44(matrix2, 12, matrix1, 1);
      float num15 = Transform.fdot44(matrix2, 12, matrix1, 2);
      float num16 = Transform.fdot44(matrix2, 12, matrix1, 3);
      matrix2[12] = num13;
      matrix2[13] = num14;
      matrix2[14] = num15;
      matrix2[15] = num16;
    }

    public void postMultiply_34(Transform trans)
    {
      float[] matrix1 = trans.m_Matrix;
      float[] matrix2 = this.m_Matrix;
      float num1 = Transform.fdot3(4, matrix2, 0, matrix1, 0);
      float num2 = Transform.fdot3(4, matrix2, 0, matrix1, 1);
      float num3 = Transform.fdot3(4, matrix2, 0, matrix1, 2);
      float num4 = Transform.fdot3(4, matrix2, 0, matrix1, 3) + matrix2[3];
      matrix2[0] = num1;
      matrix2[1] = num2;
      matrix2[2] = num3;
      matrix2[3] = num4;
      float num5 = Transform.fdot3(4, matrix2, 4, matrix1, 0);
      float num6 = Transform.fdot3(4, matrix2, 4, matrix1, 1);
      float num7 = Transform.fdot3(4, matrix2, 4, matrix1, 2);
      float num8 = Transform.fdot3(4, matrix2, 4, matrix1, 3) + matrix2[7];
      matrix2[4] = num5;
      matrix2[5] = num6;
      matrix2[6] = num7;
      matrix2[7] = num8;
      float num9 = Transform.fdot3(4, matrix2, 8, matrix1, 0);
      float num10 = Transform.fdot3(4, matrix2, 8, matrix1, 1);
      float num11 = Transform.fdot3(4, matrix2, 8, matrix1, 2);
      float num12 = Transform.fdot3(4, matrix2, 8, matrix1, 3) + matrix2[11];
      matrix2[8] = num9;
      matrix2[9] = num10;
      matrix2[10] = num11;
      matrix2[11] = num12;
    }

    public void invert()
    {
      float num1 = this.m_Matrix[3];
      float num2 = this.m_Matrix[7];
      float num3 = this.m_Matrix[11];
      this.m_Matrix[3] = 0.0f;
      this.m_Matrix[7] = 0.0f;
      this.m_Matrix[11] = 0.0f;
      this.m_Matrix[12] = 0.0f;
      this.m_Matrix[13] = 0.0f;
      this.m_Matrix[14] = 0.0f;
      this.m_Matrix[15] = 1f;
      this.invert3x3();
      this.postTranslate(-num1, -num2, -num3);
    }

    public static void angleAxisToQuatx(int degrees, int x, int y, int z, int[] quat)
    {
      if (degrees == 0 || x == 0 && y == 0 && z == 0)
      {
        quat[0] = 0;
        quat[1] = 0;
        quat[2] = 0;
        quat[3] = 65536;
      }
      else
      {
        float num1 = (float) ((double) degrees * Math.PI / 23592960.0);
        float num2 = (float) Math.Sin((double) num1);
        float num3 = (float) Math.Cos((double) num1);
        float num4 = 1.52587891E-05f;
        float num5 = (float) x * num4;
        float num6 = (float) y * num4;
        float num7 = (float) z * num4;
        float num8 = 1f / (float) Math.Sqrt((double) num5 * (double) num5 + (double) num6 * (double) num6 + (double) num7 * (double) num7);
        quat[0] = (int) ((double) num2 * (double) x * (double) num8 + 0.5);
        quat[1] = (int) ((double) num2 * (double) y * (double) num8 + 0.5);
        quat[2] = (int) ((double) num2 * (double) z * (double) num8 + 0.5);
        quat[3] = (int) ((double) num3 * 65536.0 + 0.5);
      }
    }

    public static void angleAxisToQuat(float degrees, float x, float y, float z, float[] quat)
    {
      if ((double) degrees != 0.0 && (double) x == 0.0 && (double) y == 0.0 && (double) z == 0.0)
      {
        quat[0] = 0.0f;
        quat[1] = 0.0f;
        quat[2] = 0.0f;
        quat[3] = 0.0f;
      }
      else
      {
        float num1 = (float) ((double) degrees * Math.PI / 360.0);
        float num2 = (float) Math.Sin((double) num1);
        float num3 = (float) Math.Cos((double) num1);
        float num4 = 1f / (float) Math.Sqrt((double) x * (double) x + (double) y * (double) y + (double) z * (double) z);
        quat[0] = num2 * x * num4;
        quat[1] = num2 * y * num4;
        quat[2] = num2 * z * num4;
        quat[3] = num3;
      }
    }

    private void invert3x3()
    {
      float[] matrix1 = this.m_Matrix;
      float[] matrix2 = this.m_Matrix;
      float num1 = 1f / this.determinant3x3();
      float num2 = Transform.cofactor2(1, 2, 1, 2, matrix1);
      float num3 = Transform.cofactor2(1, 2, 2, 0, matrix1);
      float num4 = Transform.cofactor2(1, 2, 0, 1, matrix1);
      float num5 = Transform.cofactor2(2, 0, 1, 2, matrix1);
      float num6 = Transform.cofactor2(2, 0, 2, 0, matrix1);
      float num7 = Transform.cofactor2(2, 0, 0, 1, matrix1);
      float num8 = Transform.cofactor2(0, 1, 1, 2, matrix1);
      float num9 = Transform.cofactor2(0, 1, 2, 0, matrix1);
      float num10 = Transform.cofactor2(0, 1, 0, 1, matrix1);
      matrix2[0] = num1 * num2;
      matrix2[1] = num1 * num5;
      matrix2[2] = num1 * num8;
      matrix2[4] = num1 * num3;
      matrix2[5] = num1 * num6;
      matrix2[6] = num1 * num9;
      matrix2[8] = num1 * num4;
      matrix2[9] = num1 * num7;
      matrix2[10] = num1 * num10;
    }
  }
}
