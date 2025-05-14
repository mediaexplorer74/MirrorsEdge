// Decompiled with JetBrains decompiler
// Type: UI.ConfirmationWindow
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;
using text;

#nullable disable
namespace UI
{
  public class ConfirmationWindow : Window
  {
    public const int BORDER_PADDING = 46;
    public const int TEXT_X_PADDING = 20;
    public const int TEXT_Y_PADDING = 25;
    public const int BUTTON_X_PADDING = 8;
    public const int BUTTON_Y_PADDING = 5;
    private BorderedElement m_border;
    private int m_fontId;
    private WrappedString m_string;
    private MajorButton m_positive;
    private MajorButton m_negative;

    public ConfirmationWindow(int stringId, int positiveId, int negativeId)
    {
      this.m_border = new BorderedElement(46, 0, this.m_width - 92, 0);
      this.m_fontId = 2;
      this.m_string = new WrappedString();
      this.m_positive = new MajorButton(positiveId);
      this.m_negative = new MajorButton(negativeId, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.sharedConstruction(stringId);
    }

    public ConfirmationWindow(int stringId, int buttonId)
    {
      this.m_border = new BorderedElement(46, 0, this.m_width - 92, 0);
      this.m_fontId = 2;
      this.m_string = new WrappedString();
      this.m_positive = new MajorButton(-1);
      this.m_negative = new MajorButton(buttonId, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.sharedConstruction(stringId);
    }

    public override void Destructor()
    {
      this.m_border.Destructor();
      this.m_border = (BorderedElement) null;
      this.m_string.Destructor();
      this.m_string = (WrappedString) null;
      this.m_positive.Destructor();
      this.m_positive = (MajorButton) null;
      this.m_negative.Destructor();
      this.m_negative = (MajorButton) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_border.render(g, top, left);
      int x = this.m_border.getX() + 20;
      int y = this.m_border.getY() + 25;
      this.m_string.draw(g, x, y, 9);
      if (this.m_positive.getStringId() != -1)
        this.m_positive.render(g, top, left);
      if (this.m_negative.getStringId() == -1)
        return;
      this.m_negative.render(g, top, left);
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      x = this.m_negative.getX() + (this.m_negative.getWidth() >> 1);
      y = this.m_negative.getY() + (this.m_negative.getHeight() >> 1);
      return true;
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_positive.contains(x, y) && this.m_positive.getStringId() != -1)
      {
        this.m_positive.pointerPressed(this.m_positive.toRelativeX(x), this.m_positive.toRelativeY(y), pointerNum);
        return true;
      }
      if (!this.m_negative.contains(x, y) || this.m_negative.getStringId() == -1)
        return false;
      this.m_negative.pointerPressed(this.m_negative.toRelativeX(x), this.m_negative.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_positive.contains(x, y) && this.m_positive.getStringId() != -1)
      {
        this.m_positive.pointerReleased(this.m_positive.toRelativeX(x), this.m_positive.toRelativeY(y), pointerNum);
        this.close(WindowResult.WINDOW_RESULT_POSITIVE);
        return true;
      }
      if (this.m_positive.isPressed())
        this.m_positive.unpress();
      if (this.m_negative.contains(x, y) && this.m_negative.getStringId() != -1)
      {
        this.m_negative.pointerReleased(this.m_negative.toRelativeX(x), this.m_negative.toRelativeY(y), pointerNum);
        this.close(WindowResult.WINDOW_RESULT_NEGATIVE);
        return true;
      }
      if (this.m_negative.isPressed())
        this.m_negative.unpress();
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_positive.isPressed() && !this.m_positive.contains(x, y))
        this.m_positive.unpress();
      if (this.m_negative.isPressed() && !this.m_negative.contains(x, y))
        this.m_negative.unpress();
      return false;
    }

    private void sharedConstruction(int stringId)
    {
      int lineWidth = this.m_width - 92 - 50;
      this.m_string.wrapString(stringId, this.m_fontId, lineWidth, false);
      int height = this.m_string.getWrappedTextHeight() + 50;
      this.m_border.setY(this.m_height - height >> 1);
      this.m_border.setHeight(height);
      int x = this.m_width - this.m_negative.getWidth() - 8;
      int y = this.m_height - this.m_negative.getHeight() - 5;
      this.m_negative.setPosition(x, y);
      this.m_positive.setPosition(x - this.m_positive.getWidth() - 8, y);
    }
  }
}
