// Decompiled with JetBrains decompiler
// Type: microedition.MathExt
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace microedition
{
  public class MathExt
  {
    private const int PI_Q16_INV = 20861;
    private const int PI_Q16_180 = 1144;
    private const int PI_Q16_180_INV = 3754936;
    public const int ONE_FIXED = 65536;
    public const int HALF_FIXED = 32768;
    public const int PI = 205887;
    public const int PI_2 = 102944;
    public const int PI_3 = 68629;
    public const int PI_4 = 51472;
    public const int PI_6 = 34315;
    public const int PI_8 = 25736;
    public const int PI_16 = 12868;
    public const int DEG_180_FIXED = 11796480;
    public const int DEG_360_FIXED = 23592960;
    public const short ONE_FIXED_2_14 = 16384;
    public const float FIXED_TO_FLOAT = 1.52587891E-05f;

    public static int floatToFixed16(float f) => (int) ((double) f * 65536.0);

    public static float fixed16ToFloat(int f) => (float) f * 1.52587891E-05f;

    public static int Fmul(int a, int b) => (int) ((long) a * (long) b >> 16);

    public static int degreesToRadiansF(int degreesF) => MathExt.Fmul(degreesF, 1144);

    public static int radiansToDegreesF(int radiansF) => MathExt.Fmul(radiansF, 3754936);

    public static int normaliseAngleRadiansF(int radiansF)
    {
      if (radiansF > 205887)
        radiansF -= Math.Max(1, radiansF / 411774) * 411774;
      else if (radiansF < -205887)
        radiansF += Math.Max(1, -radiansF / 411774) * 411774;
      return radiansF;
    }

    public static int normaliseAngleDegreesF(int degreesF)
    {
      while (degreesF > 11796480)
        degreesF -= Math.Max(1, degreesF / 23592960) * 23592960;
      while (degreesF < -11796480)
        degreesF += Math.Max(1, -degreesF / 23592960) * 23592960;
      return degreesF;
    }

    public static int getDiffBetweenAnglesF(int angle1, int angle2)
    {
      return MathExt.normaliseAngleRadiansF(angle2 - angle1);
    }

    public static int getDiffBetweenAnglesDegF(int angle1, int angle2)
    {
      return MathExt.normaliseAngleDegreesF(angle2 - angle1);
    }

    public static int Fdiv(int a, int b) => (int) (((long) a << 16) / (long) b);
  }
}
