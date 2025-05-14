// Decompiled with JetBrains decompiler
// Type: midp.Graphics
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public abstract class Graphics : meObject
  {
    public const int IPHONE_FONT_RENDERING_DROPSHADOW = 1;
    public const int IPHONE_FONT_RENDERING_OUTLINE = 2;
    public const int BASELINE = 64;
    public const int SOLID = 0;
    public const int DOTTED = 1;
    public const int HCENTER = 2;
    public const int LEFT = 1;
    public const int RIGHT = 4;
    public const int VCENTER = 16;
    public const int TOP = 8;
    public const int BOTTOM = 32;
    private int m_colorR;
    private int m_colorG;
    private int m_colorB;
    private int m_colorA;
    private int m_translateX;
    private int m_translateY;
    private Font m_font;
    public int pixelScale;

    protected Graphics()
    {
      this.m_colorR = (int) byte.MaxValue;
      this.m_colorG = (int) byte.MaxValue;
      this.m_colorB = (int) byte.MaxValue;
      this.m_colorA = (int) byte.MaxValue;
      this.m_translateX = 0;
      this.m_translateY = 0;
      this.m_font = (Font) null;
      this.pixelScale = 1;
      this.setFont((Font) null);
    }

    public override void Destructor() => base.Destructor();

    public override meClass getClass() => (meClass) new GraphicsClass();

    public virtual void setFontDropShadowParameters(
      int xoffset,
      int yoffset,
      int radius,
      int colour)
    {
    }

    public abstract void clipRect(int x, int y, int width, int height);

    public abstract void copyArea(
      int x_src,
      int y_src,
      int width,
      int height,
      int x_dest,
      int y_dest,
      int anchor);

    public abstract void drawArc(
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int arcAngle);

    public abstract void drawChar(char character, int x, int y, int anchor);

    public abstract void drawChars(
      char[] data,
      int offset,
      int length,
      int x,
      int y,
      int anchor);

    public abstract void drawImage(Image img, int x, int y, int anchor);

    public abstract void drawLine(int x1, int y1, int x2, int y2);

    public abstract void drawRect(int x, int y, int width, int height);

    public abstract void drawRegion(
      Image src,
      int x_src,
      int y_src,
      int width,
      int height,
      int transform,
      int x_dest,
      int y_dest,
      int anchor);

    public virtual void drawScaledRegion(
      Image src,
      int srcLeft,
      int srcTop,
      int srcRight,
      int srcBottom,
      int destLeft,
      int destTop,
      int destRight,
      int destBottom)
    {
    }

    public abstract void drawRGB(
      int[] rgbData,
      int offset,
      int scanlength,
      int x,
      int y,
      int width,
      int height,
      bool processAlpha);

    public abstract void drawRoundRect(
      int x,
      int y,
      int width,
      int height,
      int arcWidth,
      int arcHeight);

    public virtual void drawString(string str, int x, int y, int anchor)
    {
      this.drawString(str, x, y, anchor, 0);
    }

    public abstract void drawString(string str, int x, int y, int anchor, int flags);

    public virtual void drawSubstring(string str, int offset, int len, int x, int y, int anchor)
    {
      this.drawSubstring(str, offset, len, x, y, anchor, 0);
    }

    public abstract void drawSubstring(
      string str,
      int offset,
      int len,
      int x,
      int y,
      int anchor,
      int flags);

    public abstract void fillArc(
      int x,
      int y,
      int width,
      int height,
      int startAngle,
      int arcAngle);

    public abstract void fillRect(int x, int y, int width, int height);

    public abstract void fillRoundRect(
      int x,
      int y,
      int width,
      int height,
      int arcWidth,
      int arcHeight);

    public abstract void fillTriangle(int x1, int y1, int x2, int y2, int x3, int y3);

    public virtual int getAlphaComponent() => this.m_colorA;

    public virtual int getBlueComponent() => this.m_colorB;

    public abstract int getClipHeight();

    public abstract int getClipWidth();

    public abstract int getClipX();

    public abstract int getClipY();

    public virtual int getColor()
    {
      return this.m_colorA << 24 | this.m_colorR << 16 | this.m_colorG << 8 | this.m_colorB;
    }

    public abstract int getDisplayColor(int color);

    public Font getFont() => this.m_font;

    public abstract int getGrayScale();

    public virtual int getGreenComponent() => this.m_colorG;

    public virtual int getRedComponent() => this.m_colorR;

    public abstract int getStrokeStyle();

    public virtual int getTranslateX() => this.m_translateX;

    public virtual int getTranslateY() => this.m_translateY;

    public abstract void setClip(int x, int y, int width, int height);

    public virtual void setColor(int RGB)
    {
      this.setColor(RGB >> 16 & (int) byte.MaxValue, RGB >> 8 & (int) byte.MaxValue, RGB & (int) byte.MaxValue);
    }

    public virtual void setColor(int red, int green, int blue)
    {
      this.setColor(red, green, blue, (int) byte.MaxValue);
    }

    public virtual void setFont(Font font)
    {
      if (font == null)
        font = Font.getDefaultFont();
      this.m_font = font;
    }

    public virtual void setGrayScale(int value)
    {
      this.setColor(value, value, value, (int) byte.MaxValue);
    }

    public abstract void setStrokeStyle(int style);

    public virtual void translate(int x, int y)
    {
      this.m_translateX += x;
      this.m_translateY += y;
    }

    public virtual void setColor(int red, int green, int blue, int alpha)
    {
      this.m_colorR = red & (int) byte.MaxValue;
      this.m_colorG = green & (int) byte.MaxValue;
      this.m_colorB = blue & (int) byte.MaxValue;
      this.m_colorA = alpha & (int) byte.MaxValue;
    }

    public abstract void bind2D();
  }
}
