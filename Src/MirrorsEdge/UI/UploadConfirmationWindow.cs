// Decompiled with JetBrains decompiler
// Type: UI.UploadConfirmationWindow
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using generic;
using midp;
using text;

#nullable disable
namespace UI
{
  public class UploadConfirmationWindow : Window
  {
    public const int BORDER_PADDING = 46;
    public const int TEXT_X_PADDING = 20;
    public const int TEXT_Y_PADDING = 25;
    public const int BUTTON_X_PADDING = 8;
    public const int BUTTON_Y_PADDING = 5;
    private BorderedElement m_border;
    private int m_fontId;
    private WrappedString m_string;
    private MajorButton m_negative;
    private MajorButton m_yesButton;
    private UploadConfirmationWindow.UserChoice m_userChoice;

    public UploadConfirmationWindow()
    {
      this.m_border = new BorderedElement(46, 0, this.m_width - 92, 0);
      this.m_fontId = 2;
      this.m_string = new WrappedString();
      this.m_negative = new MajorButton(2054, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.m_yesButton = new MajorButton(2053);
      this.m_userChoice = UploadConfirmationWindow.UserChoice.CHOICE_NONE;
      this.m_string.wrapString(2341, this.m_fontId, this.m_width - 92 - 50, false);
      int height = this.m_string.getWrappedTextHeight() + 50;
      this.m_border.setY(this.m_height - height >> 1);
      this.m_border.setHeight(height);
      int x = this.m_width - this.m_negative.getWidth() - 8;
      int y = this.m_height - this.m_negative.getHeight() - 5;
      this.m_negative.setPosition(x, y);
      this.m_yesButton.setPosition(this.m_negative.getX() - this.m_yesButton.getWidth() - 16, y);
    }

    public override void Destructor()
    {
      this.m_border.Destructor();
      this.m_border = (BorderedElement) null;
      this.m_string.Destructor();
      this.m_string = (WrappedString) null;
      this.m_negative.Destructor();
      this.m_negative = (MajorButton) null;
      this.m_yesButton.Destructor();
      this.m_yesButton = (MajorButton) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_userChoice == UploadConfirmationWindow.UserChoice.CHOICE_NONE)
      {
        this.m_border.render(g, top, left);
        int x = this.m_border.getX() + 20;
        int y = this.m_border.getY() + 25;
        this.m_string.draw(g, x, y, 9);
        this.m_negative.render(g, top, left);
        this.m_yesButton.render(g, top, left);
      }
      else
      {
        this.m_string.wrapString(2346, this.m_fontId, this.m_width - 92 - 50, false);
        int height = this.m_string.getWrappedTextHeight() + 50;
        this.m_border.setY(this.m_height - height >> 1);
        this.m_border.setHeight(height);
        this.m_border.render(g, top, left);
        int x = this.m_width >> 1;
        int y = this.m_border.getY() + 25;
        this.m_string.draw(g, x, y, 10);
        AppEngine.getCanvas();
        if (this.m_userChoice != UploadConfirmationWindow.UserChoice.CHOICE_DEFAULT)
          return;
        this.close(WindowResult.WINDOW_RESULT_POSITIVE);
      }
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      if (this.m_negative.getStringId() != -1)
      {
        x = this.m_negative.getX() + (this.m_negative.getWidth() >> 1);
        y = this.m_negative.getY() + (this.m_negative.getHeight() >> 1);
        return true;
      }
      x = 0;
      y = 0;
      return false;
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_yesButton.contains(x, y) && this.m_yesButton.getStringId() != -1)
      {
        this.m_yesButton.pointerPressed(this.m_yesButton.toRelativeX(x), this.m_yesButton.toRelativeY(y), pointerNum);
        return true;
      }
      if (!this.m_negative.contains(x, y) || this.m_negative.getStringId() == -1)
        return false;
      this.m_negative.pointerPressed(this.m_negative.toRelativeX(x), this.m_negative.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_yesButton.contains(x, y) && this.m_yesButton.getStringId() != -1)
      {
        this.m_userChoice = UploadConfirmationWindow.UserChoice.CHOICE_DEFAULT;
        this.m_yesButton.pointerReleased(this.m_yesButton.toRelativeX(x), this.m_yesButton.toRelativeY(y), pointerNum);
        return true;
      }
      if (this.m_yesButton.isPressed())
        this.m_yesButton.unpress();
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
      if (this.m_yesButton.isPressed() && !this.m_yesButton.contains(x, y))
        this.m_yesButton.unpress();
      if (this.m_negative.isPressed() && !this.m_negative.contains(x, y))
        this.m_negative.unpress();
      return false;
    }

    public void setBackgroundQuads(int quadGroupId) => this.m_quadManager.loadQuads(quadGroupId);

    private enum UserChoice
    {
      CHOICE_NONE,
      CHOICE_DEFAULT,
    }
  }
}
