
// Type: midp.Font
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace midp
{
  public abstract class Font : meObject
  {
    public const int FACE_SYSTEM = 0;
    public const int FACE_MONOSPACE = 32;
    public const int FACE_PROPORTIONAL = 64;
    public const int FONT_STATIC_TEXT = 0;
    public const int FONT_INPUT_TEXT = 1;
    public const int SIZE_MEDIUM = 0;
    public const int SIZE_SMALL = 8;
    public const int SIZE_LARGE = 16;
    public const int STYLE_PLAIN = 0;
    public const int STYLE_BOLD = 1;
    public const int STYLE_ITALIC = 2;
    public const int STYLE_UNDERLINED = 4;

    public override void Destructor() => base.Destructor();

    public override meClass getClass() => (meClass) new FontClass();

    public static Font getDefaultFont() => (Font) null;

    public static Font getFont(int face, int style, float size) => (Font) null;

    public static Font getFont(InputStream stream, float size) => (Font) null;

    public static Font getFont(string face, float size) => (Font) new FontWP7Font(face, (int) size);

    public static Font getFontFromFile(string filename, float size)
    {
      return (Font) new FontWP7Font(filename, (int) size);
    }

    public static void releaseCachedData()
    {
    }

    public abstract int charsWidth(char[] ch, int offset, int length);

    public abstract int charWidth(char chr);

    public abstract int getBaselinePosition();

    public abstract int getHeight();

    public abstract int stringWidth(string str);

    public abstract int substringWidth(string str, int offset, int length);

    public abstract int getLeading();

    public virtual void getLayoutPadding(ref int x0, ref int x1, ref int y0, ref int y1)
    {
      x0 = 0;
      x1 = 0;
      y0 = 0;
      y1 = 0;
    }
  }
}
