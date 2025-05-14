// Decompiled with JetBrains decompiler
// Type: midp.DisplayWP7
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public class DisplayWP7 : Display
  {
    private static DisplayWP7 s_display;

    public static DisplayWP7 getStaticDisplay() => DisplayWP7.s_display;

    public DisplayWP7()
      : base(533, 320)
    {
      this.setGraphics((Graphics) new GraphicsWP7(this));
      DisplayWP7.s_display = this;
    }
  }
}
