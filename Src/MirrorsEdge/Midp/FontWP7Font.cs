
// Type: midp.FontWP7Font
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;
using System.Text;

#nullable disable
namespace midp
{
  public class FontWP7Font : Font
  {
    private float ascent = -1f;
    private float descent = -1f;
    private string content_name;
    private float scale = 0.6666667f;
    private static bool bitmap = false;
    private static bool dontScaleBitmapGraphics = false;
    private StringBuilder sb = new StringBuilder(256);
    private static bool m_shadowForHieroglyphic = false;
    private static StringBuilder DebugStringBuilder = new StringBuilder();
    private static string lastString = (string) null;
    private static float lastx;
    private static float lasty;
    private SpriteFont m_UIFont;

    public static void SetShadowForHieroglyphic(bool shadowForHieroglyphic)
    {
      FontWP7Font.m_shadowForHieroglyphic = shadowForHieroglyphic;
    }

    public static void SetBitmapGraphics(bool bitmap_) => FontWP7Font.bitmap = bitmap_;

    public static void SetDontScaleBitmapGraphics(bool dontScale)
    {
      FontWP7Font.dontScaleBitmapGraphics = dontScale;
    }

    public FontWP7Font(string stream, int size)
    {
      int length = stream.IndexOf(".otf");
      if (length == -1)
        length = stream.Length;
      this.content_name = "fonts/" + stream.Substring(0, length) + "_" + (object) size;
      this.m_UIFont = MirrorsEdge.content.Load<SpriteFont>(this.content_name);
    }

    public static FontWP7Font Load(string stream, int size) => new FontWP7Font(stream, size);

    public override int charsWidth(char[] ch, int offset, int length)
    {
      this.sb.Length = 0;
      this.sb.Append(ch, offset, length);
      return (int) this.measureStringAdvance(this.sb);
    }

    public override int charWidth(char chr)
    {
      this.sb.Length = 0;
      this.sb.Append(chr);
      return (int) this.measureStringAdvance(this.sb);
    }

    public override int getBaselinePosition() => (int) this.getAscent();

    public override int getHeight()
    {
      return (int) ((double) this.m_UIFont.LineSpacing * (double) this.scale);
    }

    public override int stringWidth(string str) => (int) this.measureStringAdvance(str);

    public override int substringWidth(string str, int offset, int length)
    {
      return (int) this.measureStringAdvance(str.Substring(offset, length));
    }

    public override int getLeading() => 0;

    public void drawChar(SpriteBatch sb, char c, float x, float y, Color currentColor)
    {
      string s = string.Concat((object) c);
      this.drawString(sb, s, x, y, currentColor);
    }

    public void drawString(SpriteBatch sb, string s, float x, float y, Color currentColor)
    {
      if (FontWP7Font.bitmap)
      {
        if (FontWP7Font.dontScaleBitmapGraphics)
          sb.DrawString(this.m_UIFont, s, new Vector2((float) (int) x, (float) (int) y), currentColor, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
        else
          sb.DrawString(this.m_UIFont, s, new Vector2((float) (2 * (int) x), (float) (2 * (int) y)), currentColor, 0.0f, new Vector2(), 2f * this.scale, SpriteEffects.None, 0.0f);
      }
      else
      {
        int x1;
        int num;
        if (FontWP7Font.lastString == s && (double) x + 1.0 == (double) FontWP7Font.lastx && (double) y + 1.0 == (double) FontWP7Font.lasty)
        {
          x1 = (int) ((double) FontWP7Font.lastx / (double) this.scale + 0.5) - (FontWP7Font.m_shadowForHieroglyphic ? 1 : 1);
          num = (int) ((double) FontWP7Font.lasty / (double) this.scale + 0.5) - (FontWP7Font.m_shadowForHieroglyphic ? 1 : 1);
          FontWP7Font.lastString = (string) null;
        }
        else
        {
          x1 = (int) ((double) x / (double) this.scale + 0.5);
          num = (int) ((double) y / (double) this.scale + 0.5);
          FontWP7Font.lastString = s;
          FontWP7Font.lastx = x;
          FontWP7Font.lasty = y;
        }
        sb.DrawString(this.m_UIFont, s, new Vector2((float) x1, (float) (num + 1)), currentColor, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
      }
    }

    private float measureStringAdvance(string s) => this.m_UIFont.MeasureString(s).X * this.scale;

    private float measureStringAdvance(StringBuilder sb)
    {
      return this.m_UIFont.MeasureString(sb).X * this.scale;
    }

    private Rectangle measureCharBoundingBox(char c)
    {
      this.sb.Length = 0;
      this.sb.Append(c);
      return this.measureStringBoundingBox(this.sb);
    }

    private Rectangle measureStringBoundingBox(StringBuilder sb)
    {
      Vector2 vector2 = this.m_UIFont.MeasureString(sb);
      int num = 2;
      return new Rectangle(-num, 0, (int) ((double) vector2.X * (double) this.scale + (double) (2 * num)), (int) ((double) vector2.Y * (double) this.scale));
    }

    private Rectangle measureStringBoundingBox(string s)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      foreach (char ch in s)
      {
        this.sb.Length = 0;
        this.sb.Append(ch);
        if ((double) num1 == 0.0)
          num2 = this.m_UIFont.MeasureString(this.sb).Y;
        num1 += this.m_UIFont.MeasureString(this.sb).X;
      }
      int num3 = 2;
      return new Rectangle(-num3, 0, (int) ((double) num1 * (double) this.scale + (double) (2 * num3)), (int) ((double) num2 * (double) this.scale));
    }

    public float getAscent()
    {
      if ((double) this.ascent < 0.0)
        this.ascent = (float) (int) ((double) this.getHeight() * 2.0 / 3.0);
      return this.ascent;
    }

    public float getDescent()
    {
      if ((double) this.descent < 0.0)
        this.descent = (float) (this.getHeight() - (int) ((double) this.getHeight() * 2.0 / 3.0));
      return this.descent;
    }

    public SpriteFont GetSpriteFont() => this.m_UIFont;
  }
}
