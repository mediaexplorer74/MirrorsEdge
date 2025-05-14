// Decompiled with JetBrains decompiler
// Type: UI.TitledWindow
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
  public class TitledWindow : Window
  {
    public const int TITLE_FONT = 14;
    public const int SUBTITLE_FONT = 15;
    public const int TITLE_PADDING = 4;
    public const int BUTTON_X_PADDING = 8;
    public const int BUTTON_Y_PADDING = 5;
    protected string m_title;
    protected string m_subTitle;
    protected BorderedElement m_backgroundBorder;
    protected MajorButton m_backButton;
    protected bool m_showBackground;
    protected bool m_useFS_render_for_Background;

    public TitledWindow(int title, int subTitle)
    {
      this.m_title = (string) null;
      this.m_subTitle = (string) null;
      this.m_backgroundBorder = new BorderedElement(0, 0, 0, 0);
      this.m_backButton = new MajorButton(2095, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_NEGATIVE"));
      this.m_showBackground = true;
      this.setTitles(title, subTitle);
      this.m_backButton.setPosition(this.m_width - this.m_backButton.getWidth() - 8, this.m_height - this.m_backButton.getHeight() - 5);
    }

    public override void Destructor()
    {
      this.m_backgroundBorder.Destructor();
      this.m_backgroundBorder = (BorderedElement) null;
      this.m_backButton.Destructor();
      this.m_backButton = (MajorButton) null;
      this.m_title = (string) null;
      this.m_subTitle = (string) null;
      base.Destructor();
    }

    public void setTitles(int title, int subTitle)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_title = textManager.getString(title).ToUpper();
      if (subTitle == -1)
        return;
      this.m_subTitle = textManager.getString(subTitle).ToUpper();
    }

    public void setShowBackground(bool show) => this.m_showBackground = show;

    public override void update(int timeStep)
    {
      if (!this.m_backButton.isActivated())
        return;
      this.m_backButton.reset();
      this.close(WindowResult.WINDOW_RESULT_POSITIVE);
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_showBackground)
      {
        if (this.m_useFS_render_for_Background)
          this.m_backgroundBorder.specialFS_Render(g, top, left);
        else
          this.m_backgroundBorder.render(g, top, left);
      }
      this.m_backButton.render(g, top, left);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int lineHeight = textManager.getLineHeight(14);
      int x = this.m_backgroundBorder.getX();
      int y1 = this.m_backgroundBorder.getY() - 4 + 4;
      int y2 = y1 - lineHeight - 6;
      if (y2 < -1)
        y2 = -3;
      StringRenderer stringRenderer1 = textManager.getStringRenderer(14);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(0);
      textManager.drawString(g, this.m_title, 14, x + 1, y1 + 1, 33);
      stringRenderer1.setColor(color1);
      textManager.drawString(g, this.m_title, 14, x, y1, 33);
      if (this.m_subTitle == null)
        return;
      StringRenderer stringRenderer2 = textManager.getStringRenderer(15);
      int color2 = stringRenderer2.getColor();
      stringRenderer2.setColor(0);
      textManager.drawString(g, this.m_subTitle, 15, x + 1, y2 + 1, 65);
      stringRenderer2.setColor(color2);
      textManager.drawString(g, this.m_subTitle, 15, x, y2, 65);
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      x = this.m_backButton.getX() + (this.m_backButton.getWidth() >> 1);
      y = this.m_backButton.getY() + (this.m_backButton.getHeight() >> 1);
      return true;
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (!this.m_backButton.contains(x, y))
        return false;
      this.m_backButton.pointerPressed(this.m_backButton.toRelativeX(x), this.m_backButton.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_backButton.contains(x, y))
      {
        this.m_backButton.pointerReleased(this.m_backButton.toRelativeX(x), this.m_backButton.toRelativeY(y), pointerNum);
        return true;
      }
      if (this.m_backButton.isPressed())
        this.m_backButton.unpress();
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_backButton.isPressed() && !this.m_backButton.contains(x, y))
        this.m_backButton.unpress();
      return false;
    }
  }
}
