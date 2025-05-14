// Decompiled with JetBrains decompiler
// Type: generic.GMath
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace generic
{
  public class GMath
  {
    public static float clamp(float value, float minValue, float maxValue)
    {
      return Math.Min(Math.Max(value, minValue), maxValue);
    }

    public static int clamp(int value, int minValue, int maxValue)
    {
      return Math.Min(Math.Max(value, minValue), maxValue);
    }

    public static float lerpAndClamp(float t, float xMin, float xMax)
    {
      return GMath.clamp(xMin + t * (xMax - xMin), xMin, xMax);
    }

    public static int lerpAndClamp(int t, int xMin, int xMax)
    {
      return GMath.clamp(xMin + t * (xMax - xMin), xMin, xMax);
    }

    public static float inverseLerp(float x, float xMin, float xMax)
    {
      return (float) (((double) x - (double) xMin) / ((double) xMax - (double) xMin));
    }

    public static int inverseLerp(int x, int xMin, int xMax) => (x - xMin) / (xMax - xMin);

    public static float linearRelerp(float x, float xMin, float xMax, float yMin, float yMax)
    {
      return yMin + GMath.inverseLerp(x, xMin, xMax) * (yMax - yMin);
    }

    public static float linearRelerpAndClamp(
      float x,
      float xMin,
      float xMax,
      float yMin,
      float yMax)
    {
      return GMath.lerpAndClamp(GMath.inverseLerp(x, xMin, xMax), yMin, yMax);
    }

    public static float dot2(float[] v1, float[] v2)
    {
      return (float) ((double) v1[0] * (double) v2[0] + (double) v1[1] * (double) v2[1]);
    }

    public static float magnitude2(float[] v) => (float) Math.Sqrt((double) GMath.dot2(v, v));

    public static void normalise2(ref float[] v)
    {
      float num = GMath.magnitude2(v);
      v[0] /= num;
      v[1] /= num;
    }

    public static float dot3(float[] v1, float[] v2)
    {
      return (float) ((double) v1[0] * (double) v2[0] + (double) v1[1] * (double) v2[1] + (double) v1[2] * (double) v2[2]);
    }

    public static float magnitude3(float[] v) => (float) Math.Sqrt((double) GMath.dot3(v, v));

    public static void normalise3(float[] v)
    {
      float num = GMath.magnitude3(v);
      v[0] /= num;
      v[1] /= num;
      v[2] /= num;
    }

    public static float signedAngleBetweenTwoVectors2(float[] v1, float[] v2)
    {
      return (float) Math.Atan2((double) v1[0] * (double) v2[1] - (double) v1[1] * (double) v2[0], (double) GMath.dot2(v1, v2));
    }

    public static float signedAngleBetweenTwoVectors2(float v1x, float v1y, float v2x, float v2y)
    {
      return (float) Math.Atan2((double) v1x * (double) v2y - (double) v1y * (double) v2x, (double) v1x * (double) v2x + (double) v1y * (double) v2y);
    }

    public static float saturate(float x) => GMath.clamp(x, 0.0f, 1f);

    public static void cross3(ref float[] vOut, float[] v1, float[] v2)
    {
      vOut[0] = (float) ((double) v1[1] * (double) v2[2] - (double) v1[2] * (double) v2[1]);
      vOut[1] = (float) ((double) v1[2] * (double) v2[0] - (double) v1[0] * (double) v2[2]);
      vOut[2] = (float) ((double) v1[0] * (double) v2[1] - (double) v1[1] * (double) v2[0]);
    }

    public static void subtract3(ref float[] vOut, float[] v1, float[] v2)
    {
      vOut[0] = v1[0] - v2[0];
      vOut[1] = v1[1] - v2[1];
      vOut[2] = v1[2] - v2[2];
    }

    public static void constructMatrix(ref float[] vOut, float[] v1, float[] v2, float[] v3)
    {
      vOut[0] = v1[0];
      vOut[1] = v2[0];
      vOut[2] = v3[0];
      vOut[3] = 0.0f;
      vOut[4] = v1[1];
      vOut[5] = v2[1];
      vOut[6] = v3[1];
      vOut[7] = 0.0f;
      vOut[8] = v1[2];
      vOut[9] = v2[2];
      vOut[10] = v3[2];
      vOut[11] = 0.0f;
      vOut[12] = 0.0f;
      vOut[13] = 0.0f;
      vOut[14] = 0.0f;
      vOut[15] = 1f;
    }

    public static void slerp(ref float[] qOut, float[] q1, float[] q2, float t)
    {
      float d = (float) ((double) q1[3] * (double) q2[3] + (double) q1[0] * (double) q2[0] + (double) q1[1] * (double) q2[1] + (double) q1[2] * (double) q2[2]);
      if ((double) Math.Abs(d) >= 1.0)
      {
        qOut[3] = q1[3];
        qOut[0] = q1[0];
        qOut[1] = q1[1];
        qOut[2] = q1[2];
      }
      else
      {
        float num1 = (float) Math.Acos((double) d);
        float num2 = (float) Math.Sqrt(1.0 - (double) d * (double) d);
        if ((double) Math.Abs(num2) < 1.0 / 1000.0)
        {
          qOut[3] = (float) ((double) q1[3] * 0.5 + (double) q2[3] * 0.5);
          qOut[0] = (float) ((double) q1[0] * 0.5 + (double) q2[0] * 0.5);
          qOut[1] = (float) ((double) q1[1] * 0.5 + (double) q2[1] * 0.5);
          qOut[2] = (float) ((double) q1[2] * 0.5 + (double) q2[2] * 0.5);
        }
        float num3 = (float) Math.Sin((1.0 - (double) t) * (double) num1) / num2;
        float num4 = (float) Math.Sin((double) t * (double) num1) / num2;
        qOut[3] = (float) ((double) q1[3] * (double) num3 + (double) q2[3] * (double) num4);
        qOut[0] = (float) ((double) q1[0] * (double) num3 + (double) q2[0] * (double) num4);
        qOut[1] = (float) ((double) q1[1] * (double) num3 + (double) q2[1] * (double) num4);
        qOut[2] = (float) ((double) q1[2] * (double) num3 + (double) q2[2] * (double) num4);
      }
    }

    public static float lerp(float t, float xMin, float xMax) => xMin + (xMax - xMin) * t;
  }
}
