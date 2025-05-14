// Decompiled with JetBrains decompiler
// Type: UI.PleaseWaitWindow
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
  public class PleaseWaitWindow : Window
  {
    private const int X_PADDING = 20;
    private const int Y_PADDING = 20;
    private const int BORDER_PADDING = 46;
    private BorderedElement m_border;
    private WrappedString m_message;
    private bool m_succeedVariant;
    private MajorButton m_okayButton;
    private MajorButton m_retryButton;
    private MajorButton m_cancelButton;

    public PleaseWaitWindow()
    {
      this.m_border = new BorderedElement(46, 0, this.m_width - 92, 0);
      this.m_message = new WrappedString();
      this.m_okayButton = new MajorButton(2094);
      this.m_retryButton = new MajorButton(2353);
      this.m_cancelButton = new MajorButton(2095, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.m_message.wrapString(2346, 2, this.m_width - 92 - 40, false);
      int height = this.m_message.getWrappedTextHeight() + 40 + 50;
      int y = this.m_height - height >> 1;
      this.m_border.setY(y);
      this.m_border.setHeight(height);
      this.m_okayButton.setPosition(-3000, -3000);
      this.m_cancelButton.setPosition(-3000, -3000);
      this.m_retryButton.setPosition(-3000, -3000);
      AppEngine.getCanvas().getWindowStore().getNetworkWaitEffect().play(this.m_width >> 1, y + height - 50 + 13);
    }

    public override void Destructor()
    {
      if (this.m_border == null)
        return;
      this.m_border.Destructor();
      this.m_border = (BorderedElement) null;
      this.m_message.Destructor();
      this.m_message = (WrappedString) null;
      this.m_okayButton.Destructor();
      this.m_okayButton = (MajorButton) null;
      this.m_retryButton.Destructor();
      this.m_retryButton = (MajorButton) null;
      this.m_cancelButton.Destructor();
      this.m_cancelButton = (MajorButton) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_border.render(g, top, left);
      int x = this.m_width >> 1;
      int y = this.m_border.getY() + 20;
      this.m_message.draw(g, x, y, 10);
      this.m_okayButton.render(g, top, left);
      this.m_cancelButton.render(g, top, left);
      this.m_retryButton.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_okayButton.contains(x, y))
      {
        this.m_okayButton.pointerPressed(this.m_okayButton.toRelativeX(x), this.m_okayButton.toRelativeY(y), pointerNum);
        return true;
      }
      if (this.m_cancelButton.contains(x, y))
      {
        this.m_cancelButton.pointerPressed(this.m_cancelButton.toRelativeX(x), this.m_cancelButton.toRelativeY(y), pointerNum);
        return true;
      }
      if (!this.m_retryButton.contains(x, y))
        return false;
      this.m_retryButton.pointerPressed(this.m_retryButton.toRelativeX(x), this.m_retryButton.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      if (this.m_succeedVariant)
      {
        x = this.m_okayButton.getX() + (this.m_okayButton.getWidth() >> 1);
        y = this.m_okayButton.getY() + (this.m_okayButton.getHeight() >> 1);
        return true;
      }
      x = this.m_cancelButton.getX() + (this.m_cancelButton.getWidth() >> 1);
      y = this.m_cancelButton.getY() + (this.m_cancelButton.getHeight() >> 1);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_okayButton.contains(x, y))
      {
        this.m_okayButton.pointerReleased(this.m_okayButton.toRelativeX(x), this.m_okayButton.toRelativeY(y), pointerNum);
        this.close(WindowResult.WINDOW_RESULT_EXIT);
        return true;
      }
      if (this.m_okayButton.isPressed())
        this.m_okayButton.unpress();
      if (this.m_cancelButton.contains(x, y))
      {
        this.m_cancelButton.pointerReleased(this.m_cancelButton.toRelativeX(x), this.m_cancelButton.toRelativeY(y), pointerNum);
        this.close(WindowResult.WINDOW_RESULT_NEGATIVE);
        return true;
      }
      if (this.m_cancelButton.isPressed())
        this.m_cancelButton.unpress();
      if (this.m_retryButton.contains(x, y))
      {
        this.m_retryButton.pointerReleased(this.m_retryButton.toRelativeX(x), this.m_retryButton.toRelativeY(y), pointerNum);
        this.close(WindowResult.WINDOW_RESULT_POSITIVE);
        return true;
      }
      if (this.m_retryButton.isPressed())
        this.m_retryButton.unpress();
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_okayButton.isPressed() && !this.m_okayButton.contains(x, y))
        this.m_okayButton.unpress();
      if (this.m_cancelButton.isPressed() && !this.m_cancelButton.contains(x, y))
        this.m_cancelButton.unpress();
      if (this.m_retryButton.isPressed() && !this.m_retryButton.contains(x, y))
        this.m_retryButton.unpress();
      return false;
    }

    public void Succeeded(int stringID)
    {
      this.m_succeedVariant = true;
      int lineWidth = this.m_width - 92 - 40;
      this.m_message.wrapString(stringID, 2, lineWidth, false);
      int height = this.m_message.getWrappedTextHeight() + 40;
      this.m_border.setY(this.m_height - height >> 1);
      this.m_border.setHeight(height);
      this.m_okayButton.setPosition(this.m_width - this.m_okayButton.getWidth() - 5, this.m_height - this.m_okayButton.getHeight() - 5);
    }

    public void Failed(int stringID)
    {
      this.m_succeedVariant = false;
      int lineWidth = this.m_width - 92 - 40;
      this.m_message.wrapString(stringID, 2, lineWidth, false);
      int height = this.m_message.getWrappedTextHeight() + 40;
      this.m_border.setY(this.m_height - height >> 1);
      this.m_border.setHeight(height);
      this.m_cancelButton.setPosition(this.m_width - this.m_cancelButton.getWidth() - 5, this.m_height - this.m_cancelButton.getHeight() - 5);
      this.m_retryButton.setPosition(this.m_cancelButton.getX() - this.m_retryButton.getWidth() - 5, this.m_cancelButton.getY());
    }
  }
}
