// Decompiled with JetBrains decompiler
// Type: UI.LoadingScreenStringElement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;
using text;

#nullable disable
namespace UI
{
  public class LoadingScreenStringElement : WindowElement
  {
    private int m_stringId;
    private int m_fontId;
    private int m_align;
    private WrappedString m_wrappedString;

    public LoadingScreenStringElement(DataInputStream dis, int yOffset)
      : base(0, 0, 0, 0)
    {
      this.m_stringId = 0;
      this.m_fontId = 0;
      this.m_align = 0;
      this.m_wrappedString = new WrappedString();
      this.m_stringId = ResourceManager.LOADING_STRING_LOOKUP[(int) dis.readShort()];
      int num = (int) dis.readShort();
      this.m_fontId = ResourceManager.LOADING_FONT_LOOKUP[(int) dis.readShort()];
      this.m_align = (int) dis.readShort();
      this.m_x = dis.readInt();
      this.m_y = dis.readInt();
      this.m_width = dis.readInt();
      this.m_height = dis.readInt();
      if ((this.m_align & 2) != 0)
      {
        this.m_width = 206;
        this.m_x = 25;
      }
      else
      {
        this.m_width = 206;
        this.m_x = 25;
      }
      this.m_y = yOffset;
      this.m_wrappedString.wrapString(this.m_stringId, this.m_fontId, this.m_width, false);
      this.m_height = this.m_wrappedString.getWrappedTextHeight();
    }

    public override void Destructor()
    {
      this.m_wrappedString.Destructor();
      this.m_wrappedString = (WrappedString) null;
      base.Destructor();
    }

    public bool usesLargeFont()
    {
      return this.m_fontId == 20 || this.m_fontId == 22 || this.m_fontId == 24;
    }

    public bool isCentered() => (this.m_align & 2) != 0;

    public override void render(Graphics g, int top, int left)
    {
      int x = this.m_x;
      int y = this.m_y;
      if ((this.m_align & 16) != 0)
        y = this.m_y + (this.m_height >> 1);
      else if ((this.m_align & 32) != 0)
        y = this.m_y + this.m_height;
      if ((this.m_align & 2) != 0)
        x = this.m_x + (this.m_width >> 1);
      else if ((this.m_align & 4) != 0)
        x = this.m_x + this.m_width;
      this.m_wrappedString.draw(g, x, y, this.m_align);
    }
  }
}
