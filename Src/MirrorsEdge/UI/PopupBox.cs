
// Type: UI.PopupBox
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class PopupBox : Window
  {
    public const int POPUP_FONT = 2;
    public const int PROMPT_FONT = 3;
    public const int TEXT_X_PADDING = 8;
    public const int TEXT_Y_PADDING = 12;
    public const int PROMPT_X_PADDING = 5;
    public const int PROMPT_Y_PADDING = 5;
    private BorderedElement m_border;
    private WrappedString m_popupString;
    private int m_popupTimer;

    public PopupBox(int stringId)
    {
      this.m_border = new BorderedElement(0, 0, 0, 0);
      this.m_popupString = new WrappedString();
      this.m_popupTimer = 0;
      this.m_popupString.wrapString(stringId, 2, this.m_width - 16, false);
      int num = AppEngine.getCanvas().getTextManager().getLineHeight(2) + 5;
      this.m_border.setDimensions(this.m_width, this.m_popupString.getWrappedTextHeight() + 12 + num);
    }

    public override void Destructor()
    {
      this.m_border.Destructor();
      this.m_border = (BorderedElement) null;
      this.m_popupString.Destructor();
      this.m_popupString = (WrappedString) null;
      base.Destructor();
    }

    public override void update(int timeStepMillis)
    {
      if (this.m_popupTimer > 1000)
        return;
      this.m_popupTimer += timeStepMillis;
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_border.specialFS_Render(g, top, left);
      this.m_popupString.draw(g, 8, 12, 9);
      if (1000 > this.m_popupTimer)
        return;
      AppEngine.getCanvas().getTextManager().drawString(g, 2067, 3, this.m_width - 5, this.m_border.getY() + this.m_border.getHeight() - 5, 36);
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      x = this.m_width - 10;
      y = 10;
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (1000 > this.m_popupTimer)
        return false;
      this.close(WindowResult.WINDOW_RESULT_POSITIVE);
      return true;
    }
  }
}
