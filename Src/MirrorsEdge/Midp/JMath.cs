
// Type: midp.JMath
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace midp
{
  public static class JMath
  {
    private const float E = 2.71828175f;
    private const float PI = 3.14159274f;

    public static float toDegrees(float angrad)
    {
      return (float) ((double) angrad * 180.0 / 3.1415927410125732);
    }

    public static float toRadians(float angdeg)
    {
      return (float) ((double) angdeg * 3.1415927410125732 / 180.0);
    }
  }
}
