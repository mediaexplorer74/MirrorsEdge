// Decompiled with JetBrains decompiler
// Type: UI.GameOptions
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using mirrorsedge_wp7;
using text;

#nullable disable
namespace UI
{
  public class GameOptions : TitledWindow
  {
    public const int BACKGROUND_WIDTH = 380;
    public const int CHECKBOX_X_POS = 285;
    public new const int BUTTON_X_PADDING = 10;
    public new const int BUTTON_Y_PADDING = 20;
    private const int LABEL_Y_PADDING = 20;
    public const int COLUMN_SEPARATOR_X = 280;
    public const int ITEM_Y_PADDING = 20;
    public const int ITEM_X_PADDING = 40;
    public const int LABEL_X_PADDING = 15;
    public int LABEL_FONT = 6;
    private CheckBox m_tutorialPrompts;
    private CheckBox m_uploadScores;
    private WrappedString m_tutorialLabel;
    private WrappedString m_uploadsLabel;

    public GameOptions()
      : base(2082, 2078)
    {
      this.m_tutorialPrompts = new CheckBox();
      if (MirrorsEdge.TrialMode || !MirrorsEdge.GS_Supported)
      {
        int height = this.m_tutorialPrompts.getHeight() + 40;
        this.m_backgroundBorder.setPosition(this.m_width - 380 >> 1, this.m_height - height >> 1);
        this.m_backgroundBorder.setDimensions(380, height);
        this.m_tutorialPrompts.setPosition(285, this.m_backgroundBorder.getY() + 20);
        this.m_tutorialPrompts.setChecked(AppEngine.getCanvas().getTutorialBoxes());
      }
      else
      {
        this.m_uploadScores = new CheckBox();
        this.m_tutorialLabel = new WrappedString();
        this.m_uploadsLabel = new WrappedString();
        int height = 2 * this.m_tutorialPrompts.getHeight() + 60;
        this.m_backgroundBorder.setPosition(this.m_width - 380 >> 1, this.m_height - height >> 1);
        this.m_backgroundBorder.setDimensions(380, height);
        int num = this.m_backgroundBorder.getX() + this.m_backgroundBorder.getWidth() - 40;
        this.m_tutorialPrompts.setPosition(num - 40 - this.m_tutorialPrompts.getWidth(), this.m_backgroundBorder.getY() + 20);
        this.m_uploadScores.setPosition(num - 40 - this.m_uploadScores.getWidth(), this.m_tutorialPrompts.getY() + this.m_tutorialPrompts.getHeight() + 20);
        this.m_tutorialPrompts.setChecked(AppEngine.getCanvas().getTutorialBoxes());
        this.m_uploadScores.setChecked(AppEngine.getCanvas().getUploadScores());
        int lineWidth = this.m_tutorialPrompts.getX() - 15 - (this.m_backgroundBorder.getX() + 15);
        this.m_tutorialLabel.wrapString(2303, this.LABEL_FONT, lineWidth, false);
        this.m_uploadsLabel.wrapString(2370, this.LABEL_FONT, lineWidth, false);
      }
    }

    public override void Destructor()
    {
      this.m_tutorialPrompts.Destructor();
      this.m_tutorialPrompts = (CheckBox) null;
      if (!MirrorsEdge.TrialMode && MirrorsEdge.GS_Supported)
      {
        this.m_uploadScores.Destructor();
        this.m_uploadScores = (CheckBox) null;
        this.m_tutorialLabel.Destructor();
        this.m_tutorialLabel = (WrappedString) null;
        this.m_uploadsLabel.Destructor();
        this.m_uploadsLabel = (WrappedString) null;
      }
      base.Destructor();
    }

    public override void update(int timeStep) => base.update(timeStep);

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      if (MirrorsEdge.TrialMode || !MirrorsEdge.GS_Supported)
      {
        TextManager textManager = AppEngine.getCanvas().getTextManager();
        this.m_tutorialPrompts.render(g, top, left);
        int x = this.m_tutorialPrompts.getX() - 15;
        int y = this.m_tutorialPrompts.getY() + (this.m_tutorialPrompts.getHeight() >> 1);
        textManager.drawString(g, 2303, this.LABEL_FONT, x, y, 20);
      }
      else
      {
        int x1 = this.m_tutorialPrompts.getX() - 15;
        int y1 = this.m_tutorialPrompts.getY() + (this.m_tutorialPrompts.getHeight() >> 1);
        this.m_tutorialLabel.draw(g, x1, y1, 20);
        int x2 = this.m_uploadScores.getX() - 15;
        int y2 = this.m_uploadScores.getY() + (this.m_uploadScores.getHeight() >> 1);
        this.m_uploadsLabel.draw(g, x2, y2, 20);
        this.m_tutorialPrompts.render(g, top, left);
        this.m_uploadScores.render(g, top, left);
      }
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      base.pointerReleased(x, y, pointerNum);
      if (this.m_tutorialPrompts.contains(x, y))
      {
        this.m_tutorialPrompts.pointerReleased(this.m_tutorialPrompts.toRelativeX(x), this.m_tutorialPrompts.toRelativeY(y), pointerNum);
        AppEngine.getCanvas().setTutorialBoxes(this.m_tutorialPrompts.getChecked());
        AppEngine.getCanvas().saveGameOptions();
        return true;
      }
      if (MirrorsEdge.TrialMode || !MirrorsEdge.GS_Supported || !this.m_uploadScores.contains(x, y))
        return false;
      this.m_uploadScores.pointerReleased(this.m_uploadScores.toRelativeX(x), this.m_uploadScores.toRelativeY(y), pointerNum);
      AppEngine.getCanvas().setUploadScores(this.m_uploadScores.getChecked());
      AppEngine.getCanvas().saveGameOptions();
      return true;
    }
  }
}
