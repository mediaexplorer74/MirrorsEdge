
// Type: text.CachedStringRenderer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace text
{
  public class CachedStringRenderer : StringRenderer
  {
    private StringRenderer m_stringRenderer;

    public CachedStringRenderer(StringRenderer sr) => this.m_stringRenderer = sr;

    public override void Destructor() => base.Destructor();

    public override meClass getClass() => (meClass) null;

    public override void drawString(Graphics g, string str, int x, int y, int anchor)
    {
      this.bufferedDrawString(g, str, x, y, anchor);
    }

    public override void drawString(Graphics g, StringBuffer str, int x, int y, int anchor)
    {
      this.drawString(g, str, x, y, anchor);
    }

    public override void drawSubstring(
      Graphics g,
      string str,
      int offset,
      int len,
      int x,
      int y,
      int anchor)
    {
      string str1 = str.Substring(offset, len);
      this.bufferedDrawString(g, str1, x, y, anchor);
    }

    public override int charsWidth(char[] ch, int offset, int length)
    {
      return this.m_stringRenderer.charsWidth(ch, offset, length);
    }

    public override int charWidth(char chr) => this.m_stringRenderer.charWidth(chr);

    public override int getBaselinePosition() => this.m_stringRenderer.getBaselinePosition();

    public override int getHeight() => this.m_stringRenderer.getHeight();

    public override int getLeading() => this.m_stringRenderer.getLeading();

    public override int stringWidth(string str) => this.m_stringRenderer.stringWidth(str);

    public override int substringWidth(string str, int offset, int length)
    {
      return this.m_stringRenderer.substringWidth(str, offset, length);
    }

    public override void setColor(int color) => this.m_stringRenderer.setColor(color);

    public override int getColor() => this.m_stringRenderer.getColor();

    private void bufferedDrawString(Graphics g, string str, int x_, int y_, int anchor)
    {
      int num1 = x_ * Runtime.pixelScale;
      int num2 = y_ * Runtime.pixelScale;
      int num3 = this.m_stringRenderer.stringWidth(str);
      int num4 = this.m_stringRenderer.getHeight() - this.m_stringRenderer.getLeading();
      int x0;
      int x1;
      int y0;
      int y1;
      this.m_stringRenderer.getStringTexturePadding(out x0, out x1, out y0, out y1);
      int num5 = x1 + 2;
      int num6 = x0 + 2;
      int num7 = y1 + 2;
      int num8 = y0 + 2;
      if (num3 <= 0 || num4 <= 0)
        return;
      int x = num1 - num6;
      int y = num2 - num8;
      if ((anchor & 2) != 0)
        x -= num3 >> 1;
      else if ((anchor & 4) != 0)
        x -= num3;
      if ((anchor & 16) != 0)
        y -= num4 >> 1;
      else if ((anchor & 32) != 0)
        y -= num4;
      else if ((anchor & 64) != 0)
        y -= this.m_stringRenderer.getBaselinePosition();
      this.m_stringRenderer.drawString(g, str, x, y, 9);
    }
  }
}
