
// Type: UI.LanguageItem
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;

#nullable disable
namespace UI
{
  public class LanguageItem : WindowElement
  {
    public const int WIDTH = 188;
    public int FONT = 2;
    private readonly int m_langId;
    private string m_str;

    public LanguageItem(int langId, string str)
    {
      this.m_langId = langId;
      this.m_str = str;
      int lineHeight = AppEngine.getCanvas().getTextManager().getLineHeight(this.FONT);
      this.setWidth(188);
      this.setHeight(lineHeight);
    }

    public int getLangId() => this.m_langId;

    public override void render(Graphics g, int top, int left)
    {
      AppEngine.getCanvas().getTextManager().drawString(g, this.m_str, this.FONT, left + this.m_x + (this.m_width >> 1), top + this.m_y + (this.m_height >> 1), 18);
    }
  }
}
