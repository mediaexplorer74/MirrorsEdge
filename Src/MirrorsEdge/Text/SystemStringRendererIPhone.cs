
// Type: text.SystemStringRendererIPhone
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace text
{
  public class SystemStringRendererIPhone : SystemStringRenderer
  {
    public const int IPHONE_FONT_RENDERING_STYLE_FILL = 1;
    public const int IPHONE_FONT_RENDERING_STYLE_OUTLINE = 2;
    public const int IPHONE_FONT_RENDERING_STYLE_STROKE = 3;
    private int m_dropShadowX;
    private int m_dropShadowY;
    private int m_dropShadowRadius;
    private int m_strokeColor;
    private int m_dropShadowColor;
    private bool m_enableDropShadow;

    public SystemStringRendererIPhone()
    {
      this.m_strokeColor = 16777215;
      this.m_dropShadowX = 0;
      this.m_dropShadowY = 0;
      this.m_dropShadowRadius = 0;
      this.m_enableDropShadow = false;
      this.m_dropShadowColor = 805306368;
    }

    public SystemStringRendererIPhone(Font font)
      : base(font)
    {
      this.m_strokeColor = 16777215;
      this.m_dropShadowX = 0;
      this.m_dropShadowY = 0;
      this.m_dropShadowRadius = 0;
      this.m_enableDropShadow = false;
      this.m_dropShadowColor = 805306368;
    }

    public override void Destructor() => base.Destructor();

    public override void drawString(Graphics g, string str, int x, int y, int anchor)
    {
      g.setFont(this.m_font);
      g.setColor(this.m_color);
      int flags = 0;
      if (this.m_enableDropShadow)
      {
        g.setFontDropShadowParameters(this.m_dropShadowX, this.m_dropShadowY, this.m_dropShadowRadius, this.m_dropShadowColor);
        flags |= 1;
      }
      g.drawString(str, x, y, anchor, flags);
    }

    public override void drawString(Graphics g, StringBuffer str, int x, int y, int anchor)
    {
      str.toString();
      g.setFont(this.m_font);
      g.setColor(this.m_color);
      int flags = 0;
      if (this.m_enableDropShadow)
      {
        g.setFontDropShadowParameters(this.m_dropShadowX, this.m_dropShadowY, this.m_dropShadowRadius, this.m_dropShadowColor);
        flags |= 1;
      }
      g.drawString(str.toString(), x, y, anchor, flags);
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
      int flags = 0;
      if (this.m_enableDropShadow)
      {
        g.setFontDropShadowParameters(this.m_dropShadowX, this.m_dropShadowY, this.m_dropShadowRadius, this.m_dropShadowColor);
        flags |= 1;
      }
      g.drawSubstring(str, offset, len, x, y, anchor, flags);
    }

    public override int stringWidth(string str) => this.m_font.stringWidth(str);

    public override int substringWidth(string str, int offset, int length)
    {
      return this.m_font.stringWidth(str.Substring(offset, length));
    }

    public override int getHeight() => this.m_font.getHeight() + 2;

    public new virtual void getStringTexturePadding(
      ref int x0,
      ref int x1,
      ref int y0,
      ref int y1)
    {
      this.m_font.getLayoutPadding(ref x0, ref x1, ref y0, ref y1);
      if (!this.m_enableDropShadow)
        return;
      int num1;
      int num2;
      if (this.m_dropShadowX > 0)
      {
        num1 = this.m_dropShadowRadius - this.m_dropShadowX;
        if (num1 < 0)
          num1 = 0;
        num2 = this.m_dropShadowX + this.m_dropShadowRadius;
      }
      else
      {
        num1 = this.m_dropShadowRadius - this.m_dropShadowX;
        num2 = this.m_dropShadowRadius + this.m_dropShadowX;
        if (num2 < 0)
          num2 = 0;
      }
      int num3;
      int num4;
      if (this.m_dropShadowY > 0)
      {
        num3 = this.m_dropShadowRadius - this.m_dropShadowY;
        num4 = this.m_dropShadowY + this.m_dropShadowRadius;
        if (num3 < 0)
          num3 = 0;
      }
      else
      {
        num3 = this.m_dropShadowRadius - this.m_dropShadowY;
        num4 = this.m_dropShadowRadius + this.m_dropShadowY;
        if (num4 < 0)
          num4 = 0;
      }
      x0 += num1 + 2;
      x1 += num2 + 2;
      y0 += num4 + 2;
      y1 += num3 + 2;
    }

    public virtual void useIPhoneFontDropShadow(bool enabled) => this.m_enableDropShadow = enabled;

    public virtual void setIPhoneDropShadowParameters(int xoffset, int yoffset, int blurradius)
    {
      this.m_dropShadowX = xoffset * Runtime.pixelScale;
      this.m_dropShadowY = -yoffset * Runtime.pixelScale;
      this.m_dropShadowRadius = blurradius * Runtime.pixelScale;
    }

    public virtual void setIPhoneDropShadowColor(int colour) => this.m_dropShadowColor = colour;

    public void setStrokeColor(int color) => this.m_strokeColor = color;

    public int getStrokeColor() => this.m_strokeColor;
  }
}
