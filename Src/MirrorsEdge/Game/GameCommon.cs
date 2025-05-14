// Decompiled with JetBrains decompiler
// Type: game.GameCommon
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game
{
  public static class GameCommon
  {
    public const int TOP = 8;
    public const int BOTTOM = 32;
    public const int BASELINE = 64;
    public const int VCENTER = 16;
    public const int LEFT = 1;
    public const int RIGHT = 4;
    public const int HCENTER = 2;
    public const int TOP_LEFT = 9;
    public const int TOP_RIGHT = 12;
    public const int TOP_HCENTER = 10;
    public const int BOTTOM_LEFT = 33;
    public const int BOTTOM_RIGHT = 36;
    public const int BOTTOM_HCENTER = 34;
    public const int VCENTER_LEFT = 17;
    public const int VCENTER_RIGHT = 20;
    public const int VCENTER_HCENTER = 18;
    public const int BASELINE_LEFT = 65;
    public const int BASELINE_RIGHT = 68;
    public const int BASELINE_HCENTER = 66;
    public const int POINTER_PRESSED = 0;
    public const int POINTER_DRAGGED = 1;
    public const int POINTER_RELEASED = 2;
    public const int POINTER_ID_NULL = -1;
    public const float FLOAT_COMPARE_TOLERANCE = 0.01f;
    public const float FLOAT_TO_FIXED = 65536f;
    public const float FIXED_TO_FLOAT = 1.52587891E-05f;
    public const float INVERSE_MILLIS = 0.001f;

    public static void fillArray(ref bool[] arrayToFill, bool value)
    {
      for (int index = arrayToFill.Length - 1; index != -1; --index)
        arrayToFill[index] = value;
    }

    public static void fillArray(ref sbyte[] arrayToFill, sbyte value)
    {
      for (int index = arrayToFill.Length - 1; index != -1; --index)
        arrayToFill[index] = value;
    }

    public static void fillArray(ref byte[] arrayToFill, byte value)
    {
      for (int index = arrayToFill.Length - 1; index != -1; --index)
        arrayToFill[index] = value;
    }

    public static void fillArray(ref short[] arrayToFill, int value)
    {
      for (int index = arrayToFill.Length - 1; index != -1; --index)
        arrayToFill[index] = (short) value;
    }

    public static void fillArray(ref int[] arrayToFill, int value)
    {
      for (int index = arrayToFill.Length - 1; index != -1; --index)
        arrayToFill[index] = value;
    }

    public static void fillArray(ref long[] arrayToFill, long value)
    {
      for (int index = arrayToFill.Length - 1; index != -1; --index)
        arrayToFill[index] = value;
    }

    public static int indexOf(int value, int[] arrayToSearch)
    {
      return GameCommon.indexOf(value, arrayToSearch, 0);
    }

    public static int indexOf(int value, short[] arrayToSearch)
    {
      return GameCommon.indexOf(value, arrayToSearch, 0);
    }

    public static int indexOf(int value, int[] arrayToSearch, int startIndex)
    {
      for (int index = startIndex; index < arrayToSearch.Length; ++index)
      {
        if (arrayToSearch[index] == value)
          return index;
      }
      return -1;
    }

    public static int indexOf(int value, short[] arrayToSearch, int startIndex)
    {
      for (int index = startIndex; index < arrayToSearch.Length; ++index)
      {
        if ((int) arrayToSearch[index] == value)
          return index;
      }
      return -1;
    }

    public static bool isZero(float value)
    {
      return -0.0099999997764825821 <= (double) value && (double) value <= 0.0099999997764825821;
    }

    public static bool compareFloats(float lhs, float rhs)
    {
      return (double) rhs - 0.0099999997764825821 <= (double) lhs && (double) lhs <= (double) rhs + 0.0099999997764825821;
    }

    public static bool inBounds(float minimum, float value, float maximum)
    {
      return (double) minimum - 0.0099999997764825821 <= (double) value && (double) value <= (double) maximum + 0.0099999997764825821;
    }

    public static bool boundsIntersect(float min1, float max1, float min2, float max2)
    {
      return (double) max1 >= (double) min2 - 0.0099999997764825821 && (double) max2 + 0.0099999997764825821 >= (double) min1;
    }
  }
}
