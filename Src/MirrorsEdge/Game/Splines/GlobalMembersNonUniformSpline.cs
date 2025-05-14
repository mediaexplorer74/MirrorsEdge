
// Type: game.Splines.GlobalMembersNonUniformSpline
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game.Splines
{
  public static class GlobalMembersNonUniformSpline
  {
    internal const float H00 = 2f;
    internal const float H01 = -2f;
    internal const float H10 = -3f;
    internal const float H11 = 3f;
    internal const float H12 = -2f;

    public static MathVector getPositionOnCubic(
      MathVector startPos,
      MathVector startVel,
      MathVector endPos,
      MathVector endVel,
      float pos)
    {
      float x1 = startPos.x;
      float y1 = startPos.y;
      float z1 = startPos.z;
      float x2 = endPos.x;
      float y2 = endPos.y;
      float z2 = endPos.z;
      float x3 = startVel.x;
      float y3 = startVel.y;
      float z3 = startVel.z;
      float x4 = endVel.x;
      float y4 = endVel.y;
      float z4 = endVel.z;
      float num1 = (float) (2.0 * (double) x1 + -2.0 * (double) x2) + x3 + x4;
      float num2 = (float) (2.0 * (double) y1 + -2.0 * (double) y2) + y3 + y4;
      float num3 = (float) (2.0 * (double) z1 + -2.0 * (double) z2) + z3 + z4;
      float num4 = (float) (-3.0 * (double) x1 + 3.0 * (double) x2 + -2.0 * (double) x3) - x4;
      float num5 = (float) (-3.0 * (double) y1 + 3.0 * (double) y2 + -2.0 * (double) y3) - y4;
      float num6 = (float) (-3.0 * (double) z1 + 3.0 * (double) z2 + -2.0 * (double) z3) - z4;
      float num7 = x3;
      float num8 = y3;
      float num9 = z3;
      float num10 = x1;
      float num11 = y1;
      float num12 = z1;
      float num13 = pos * pos;
      float num14 = pos * num13;
      float num15 = pos;
      return new MathVector((float) ((double) num14 * (double) num1 + (double) num13 * (double) num4 + (double) num15 * (double) num7) + num10, (float) ((double) num14 * (double) num2 + (double) num13 * (double) num5 + (double) num15 * (double) num8) + num11, (float) ((double) num14 * (double) num3 + (double) num13 * (double) num6 + (double) num15 * (double) num9) + num12);
    }
  }
}
