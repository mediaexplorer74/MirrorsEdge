
// Type: UI.HelpText
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using GameManager;
using text;

#nullable disable
namespace UI
{
  public class HelpText : WindowElement
  {
    public const int TEXT_X_PADDING = 5;
    public int FONT_HELP = 2;
    private WrappedString m_helpString;

    public HelpText(Window container)
      : base(0, 0, container.getClientWidth() - 10 - 5, 0)
    {
      this.m_helpString = new WrappedString();
      if (MirrorsEdge.TrialMode)
      {
        this.m_helpString.wrapString(2396, this.FONT_HELP, this.m_width - 10, false);
      }
      else
      {
        TextManager textManager = AppEngine.getCanvas().getTextManager();
        string str = textManager.getString(2052) + textManager.getString(2444);
        if (MirrorsEdge.GS_Supported)
          str += textManager.getString(2448);
        this.m_helpString.wrapString(str, this.FONT_HELP, this.m_width - 10, false);
      }
      this.setHeight(this.m_helpString.getWrappedTextHeight());
    }

    public override void Destructor()
    {
      this.m_helpString.Destructor();
      this.m_helpString = (WrappedString) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_helpString.draw(g, 5 + this.m_x + left, this.m_y + top + 2, 9);
    }
  }
}
