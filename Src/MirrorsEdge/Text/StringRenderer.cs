// Decompiled with JetBrains decompiler
// Type: text.StringRenderer
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace text
{
  public abstract class StringRenderer : meObject
  {
    public override void Destructor() => base.Destructor();

    public abstract void drawString(Graphics g, string str, int x, int y, int anchor);

    public abstract void drawString(Graphics g, StringBuffer str, int x, int y, int anchor);

    public abstract void drawSubstring(
      Graphics g,
      string str,
      int offset,
      int len,
      int x,
      int y,
      int anchor);

    public abstract int charsWidth(char[] ch, int offset, int length);

    public abstract int charWidth(char chr);

    public abstract int getBaselinePosition();

    public abstract int getHeight();

    public abstract int getLeading();

    public abstract int stringWidth(string str);

    public abstract int substringWidth(string str, int offset, int length);

    public abstract void setColor(int color);

    public abstract int getColor();

    public virtual void getStringTexturePadding(out int x0, out int x1, out int y0, out int y1)
    {
      x0 = 0;
      x1 = 0;
      y0 = 0;
      y1 = 0;
    }
  }
}
