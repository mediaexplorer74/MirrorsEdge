
// Type: UI.Button
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;

#nullable disable
namespace UI
{
  public abstract class Button : WindowElement
  {
    public const int TEXT_PADDING = 20;
    public int DEFAULT_FONT = 7;
    public int DEFAULT_HILIGHT_FONT = 6;
    protected int m_stringId;
    protected int m_fontId;
    protected int m_highlightFontId;
    protected bool m_pressed;
    protected bool m_activated;
    protected bool m_enabled;

    public Button()
    {
      this.m_activated = false;
      this.m_stringId = 2048;
      this.m_fontId = -1;
      this.m_highlightFontId = -1;
      this.m_pressed = false;
      this.m_enabled = true;
    }

    public Button(int stringId)
    {
      this.m_activated = false;
      this.m_stringId = stringId;
      this.m_fontId = this.DEFAULT_FONT;
      this.m_highlightFontId = this.DEFAULT_HILIGHT_FONT;
      this.m_pressed = false;
      this.m_enabled = true;
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_enabled)
        this.m_pressed = true;
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_enabled)
      {
        AppEngine.getCanvas().getWindowStore().getButtonEffect().play(this.m_x + (this.m_width >> 1), this.m_y + (this.m_height >> 1));
        this.m_activated = true;
        this.m_pressed = false;
      }
      return true;
    }

    public bool isPressed() => this.m_pressed;

    public bool isActivated() => this.m_activated;

    public bool isEnabled() => this.m_enabled;

    public void setEnabled(bool enabled) => this.m_enabled = enabled;

    public void unpress() => this.m_pressed = false;

    public void reset() => this.m_activated = false;

    public int getStringId() => this.m_stringId;
  }
}
