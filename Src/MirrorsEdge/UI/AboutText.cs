
// Type: UI.AboutText
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class AboutText : WindowElement
  {
    public const int TEXT_X_PADDING = 5;
    public int FONT_ABOUT = 2;
    private WrappedString m_aboutString;

    public AboutText(Window container)
      : base(0, 0, container.getClientWidth() - 10 - 5, 0)
    {
      this.m_aboutString = new WrappedString();
      AppEngine.getCanvas().initAboutString();
      this.m_aboutString.wrapString(-11, this.FONT_ABOUT, this.m_width - 10, false);
      this.setHeight(this.m_aboutString.getWrappedTextHeight());
    }

    public override void Destructor()
    {
      this.m_aboutString.Destructor();
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_aboutString.draw(g, 5 + this.m_x + left, this.m_y + top + 2, 9);
    }
  }
}
