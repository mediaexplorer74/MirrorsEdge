// Decompiled with JetBrains decompiler
// Type: text.SystemStringRenderer
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;

#nullable disable
namespace text
{
  public class SystemStringRenderer : StringRenderer
  {
    protected Font m_font;
    protected int m_color;

    public SystemStringRenderer()
    {
      this.m_font = (Font) null;
      this.m_color = 16777215;
      this.m_font = Font.getDefaultFont();
    }

    public SystemStringRenderer(Font font)
    {
      this.m_font = font;
      this.m_color = 16777215;
    }

    public override void Destructor()
    {
      this.m_font = (Font) null;
      base.Destructor();
    }

    public override meClass getClass() => (meClass) null;

    public override void setColor(int color) => this.m_color = color;

    public override int getColor() => this.m_color;

    public override void drawString(Graphics g, string str, int x, int y, int anchor)
    {
      g.setFont(this.m_font);
      g.setColor(this.m_color);
      g.drawString(str, x, y, anchor);
    }

    public override void drawString(Graphics g, StringBuffer str, int x, int y, int anchor)
    {
      g.setFont(this.m_font);
      g.setColor(this.m_color);
      g.drawString(str.ToString(), x, y, anchor);
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
      g.setFont(this.m_font);
      g.setColor(this.m_color);
      g.drawSubstring(str, offset, len, x, y, anchor);
    }

    public override int charsWidth(char[] ch, int offset, int length)
    {
      return this.m_font.charsWidth(ch, offset, length);
    }

    public override int charWidth(char chr) => this.m_font.charWidth(chr);

    public override int getBaselinePosition() => this.m_font.getBaselinePosition();

    public override int getHeight() => this.m_font.getHeight();

    public override int getLeading() => this.m_font.getLeading();

    public override int stringWidth(string str) => this.m_font.stringWidth(str);

    public override int substringWidth(string str, int offset, int length)
    {
      return this.m_font.substringWidth(str, offset, length);
    }
  }
}
