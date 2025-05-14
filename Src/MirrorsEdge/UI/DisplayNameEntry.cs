// Decompiled with JetBrains decompiler
// Type: UI.DisplayNameEntry
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
  public class DisplayNameEntry : Window
  {
    public const int BORDER_PADDING = 46;
    public const int X_PADDING = 20;
    public const int Y_PADDING = 25;
    public const int TEXT_PADDING = 20;
    public const int BUTTON_X_PADDING = 8;
    public const int BUTTON_Y_PADDING = 5;
    public const int INPUT_PADDING = 3;
    public const int MAX_NAME_CHARS = 25;
    private BorderedElement m_border;
    private WrappedString m_message;
    private string m_name;
    private MajorButton m_negative;
    private MajorButton m_positive;
    private bool m_getInput;

    public DisplayNameEntry()
    {
      this.m_border = new BorderedElement(0, 0, 1, 1);
      this.m_name = (string) null;
      this.m_negative = new MajorButton(2095, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.m_positive = new MajorButton(2094);
      this.m_getInput = false;
      this.m_message = new WrappedString();
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int width = this.m_width - 92;
      this.m_message.wrapString(2344, 2, width - 40, false);
      int height = 25 + this.m_message.getWrappedTextHeight() + 20 + (textManager.getLineHeight(2) + 6) + 25;
      this.m_border.setPosition(46, this.m_height - height >> 1);
      this.m_border.setDimensions(width, height);
      this.m_negative.setPosition(this.m_width - this.m_negative.getWidth() - 8, this.m_height - this.m_negative.getHeight() - 5);
      this.m_positive.setPosition(-3000, -3000);
      this.m_name = "";
    }

    public override void Destructor()
    {
      this.m_border.Destructor();
      this.m_border = (BorderedElement) null;
      this.m_negative.Destructor();
      this.m_negative = (MajorButton) null;
      this.m_positive.Destructor();
      this.m_positive = (MajorButton) null;
      this.m_message.Destructor();
      this.m_message = (WrappedString) null;
      this.m_name = (string) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      if (!this.m_getInput)
        return;
      string title = AppEngine.getCanvas().getTextManager().getString(2345);
      AppEngine.getCanvas().getWindowStore().setWaitingForMainThread();
      this.m_name = AppEngine.getCanvas().getMIDlet().getInputString(title, 25);
      AppEngine.getCanvas().getWindowStore().unsetWaitingForMainThread();
      if (this.m_name.Length > 25)
        this.m_name = this.m_name.Substring(0, 25);
      if (this.m_name.Length > 0)
      {
        int num = this.m_width - this.m_negative.getWidth() - 8;
        int y = this.m_height - this.m_negative.getHeight() - 5;
        this.m_positive.setPosition(num - this.m_positive.getWidth() - 8, y);
      }
      this.m_getInput = false;
    }

    public override void render(Graphics g, int top, int left)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_border.render(g, top, left);
      this.m_message.draw(g, this.m_border.getX() + 20, this.m_border.getY() + 25, 9);
      int wrappedTextHeight = this.m_message.getWrappedTextHeight();
      int x = this.m_border.getX() + 20;
      int y = this.m_border.getY() + 25 + wrappedTextHeight + 20;
      int width = this.m_border.getWidth() - 40;
      int height = textManager.getLineHeight(2) + 6;
      g.setColor(0);
      g.drawRect(x, y, width, height);
      textManager.drawString(g, this.m_name, 2, this.m_width >> 1, y + 3, 10);
      this.m_positive.render(g, top, left);
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
      if (this.m_positive.contains(x, y))
      {
        this.m_positive.pointerPressed(this.m_positive.toRelativeX(x), this.m_positive.toRelativeY(y), pointerNum);
        return true;
      }
      if (!this.m_negative.contains(x, y))
        return false;
      this.m_negative.pointerPressed(this.m_negative.toRelativeX(x), this.m_negative.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int wrappedTextHeight = this.m_message.getWrappedTextHeight();
      int num1 = this.m_border.getX() + 20;
      int num2 = this.m_border.getY() + 25 + wrappedTextHeight + 20;
      int num3 = this.m_border.getWidth() - 40;
      int num4 = textManager.getLineHeight(2) + 6;
      if (x > num1 && x < num1 + num3 && y > num2 && y < num2 + num4)
        this.m_getInput = true;
      if (this.m_positive.contains(x, y))
      {
        this.m_positive.pointerReleased(this.m_positive.toRelativeX(x), this.m_positive.toRelativeY(y), pointerNum);
        AppEngine.getCanvas().getSceneGame().m_displayName = this.m_name;
        this.close(WindowResult.WINDOW_RESULT_POSITIVE);
        return true;
      }
      if (this.m_positive.isPressed())
        this.m_positive.unpress();
      if (this.m_negative.contains(x, y))
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
  }
}
