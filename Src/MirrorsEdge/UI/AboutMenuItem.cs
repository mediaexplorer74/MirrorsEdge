// Decompiled with JetBrains decompiler
// Type: UI.AboutMenuItem
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class AboutMenuItem : WindowElement
  {
    public const int WIDTH = 199;
    public int FONT = 2;
    private readonly int m_stringId;
    private WrappedString m_string;

    public AboutMenuItem(int stringId)
    {
      this.m_stringId = stringId;
      this.m_string = new WrappedString();
      this.m_string.wrapString(stringId, this.FONT, 199, false);
      int wrappedTextHeight = this.m_string.getWrappedTextHeight();
      this.setWidth(199);
      this.setHeight(wrappedTextHeight);
    }

    public override void Destructor()
    {
      this.m_string.Destructor();
      this.m_string = (WrappedString) null;
    }

    public int getStringId() => this.m_stringId;

    public override void render(Graphics g, int top, int left)
    {
      AppEngine.getCanvas().getTextManager();
      this.m_string.draw(g, left + this.m_x + (this.m_width >> 1), top + this.m_y + (this.m_height >> 1), 18);
    }
  }
}
