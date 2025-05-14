// Decompiled with JetBrains decompiler
// Type: MathTrig
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using System;

#nullable disable
public static class MathTrig
{
  public const float ATAN2_1_0 = 1.57079637f;

  public static void convertLookVectorToEulerRotationsRad(
    float lookX,
    float lookY,
    float lookZ,
    ref float azimuth,
    ref float elevation)
  {
    float x = (float) Math.Sqrt((double) lookX * (double) lookX + (double) lookZ * (double) lookZ);
    azimuth = (float) Math.Atan2(-(double) lookX, -(double) lookZ);
    elevation = (float) Math.Atan2((double) lookY, (double) x);
  }

  public static void convertLookVectorToEulerRotationsDeg(
    float lookX,
    float lookY,
    float lookZ,
    ref float azimuth,
    ref float elevation)
  {
    float x = (float) Math.Sqrt((double) lookX * (double) lookX + (double) lookZ * (double) lookZ);
    azimuth = JMath.toDegrees((float) Math.Atan2(-(double) lookX, -(double) lookZ));
    elevation = JMath.toDegrees((float) Math.Atan2((double) lookY, (double) x));
  }
}
