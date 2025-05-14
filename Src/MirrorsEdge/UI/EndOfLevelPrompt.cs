
// Type: UI.EndOfLevelPrompt
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using mirrorsedge_wp7;
using text;

#nullable disable
namespace UI
{
  public abstract class EndOfLevelPrompt : Window
  {
    protected const int TITLE_X_POS = 20;
    protected const int TITLE_Y_POS = 15;
    protected const int SUBTITLE_Y_ADJUST = 5;
    protected MajorButton m_restart;
    protected MajorButton m_menu;
    protected MajorButton m_next;
    protected bool m_gameComplete;
    protected bool m_buttonClicked;
    private readonly bool m_MenuButtonHidden;
    protected int LEVEL_COMPLETE_TITLE_FONT = 26;
    protected int LEVEL_COMPLETE_BODY_FONT = 27;

    public EndOfLevelPrompt()
      : base(0, 0, 533, 320)
    {
      this.m_restart = new MajorButton(2301);
      this.m_next = new MajorButton(2069);
      this.m_menu = new MajorButton(2068);
      this.m_gameComplete = false;
      this.m_buttonClicked = false;
      int y = this.m_height - this.m_restart.getHeight() - 25;
      int x1 = this.m_width - this.m_next.getWidth() - 20;
      int x2 = x1 - 12 - this.m_menu.getWidth();
      this.m_next.setPosition(x1, y);
      if (AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN || AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_CHALLENGE && !MirrorsEdge.TrialMode)
        this.m_restart.setPosition(20, y);
      else
        this.m_restart.setPosition(-500, -500);
      if (this.m_gameComplete && !MirrorsEdge.TrialMode || AppEngine.getLevelData().getCurrentLevelIndex() == AppEngine.getLevelData().getLevelNum() - 1 && MirrorsEdge.TrialMode)
      {
        this.m_MenuButtonHidden = true;
        this.m_menu.setPosition(-500, -500);
      }
      else
      {
        this.m_MenuButtonHidden = false;
        this.m_menu.setPosition(x2, y);
      }
    }

    public override void Destructor()
    {
      this.m_restart.Destructor();
      this.m_restart = (MajorButton) null;
      this.m_next.Destructor();
      this.m_next = (MajorButton) null;
      this.m_menu.Destructor();
      this.m_menu = (MajorButton) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      if (!this.m_buttonClicked)
        return;
      AppEngine canvas = AppEngine.getCanvas();
      if (!MirrorsEdge.TrialMode && MirrorsEdge.GS_Supported && (AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN || AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_CHALLENGE))
        canvas.getSceneGame().stateTransition(SceneGame.GameState.STATE_UPLOAD_SCORE_CONFIRM);
      else
        canvas.getSceneGame().stateTransition(SceneGame.GameState.STATE_FADE_EXIT);
      this.close(WindowResult.WINDOW_RESULT_NONE);
    }

    public override void render(Graphics g, int top, int left)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
      this.m_menu.render(g, top, left);
      this.m_next.render(g, top, left);
      this.m_restart.render(g, top, left);
      StringRenderer stringRenderer1 = textManager.getStringRenderer(this.LEVEL_COMPLETE_TITLE_FONT);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(0);
      textManager.drawString(g, 2226, this.LEVEL_COMPLETE_TITLE_FONT, 21, 16, 9);
      stringRenderer1.setColor(color1);
      textManager.drawString(g, 2226, this.LEVEL_COMPLETE_TITLE_FONT, 20, 15, 9);
      int y = 15 + textManager.getLineHeight(this.LEVEL_COMPLETE_TITLE_FONT) + 5;
      int name = currentLevelObject.getName();
      StringRenderer stringRenderer2 = textManager.getStringRenderer(this.LEVEL_COMPLETE_BODY_FONT);
      int color2 = stringRenderer2.getColor();
      stringRenderer2.setColor(0);
      textManager.drawString(g, name, this.LEVEL_COMPLETE_BODY_FONT, 21, y + 1, 9);
      stringRenderer2.setColor(color2);
      textManager.drawString(g, name, this.LEVEL_COMPLETE_BODY_FONT, 20, y, 9);
      int x = 20 + textManager.getStringWidth(name, this.LEVEL_COMPLETE_BODY_FONT) + textManager.getStringWidth("  ", this.LEVEL_COMPLETE_BODY_FONT);
      stringRenderer2.setColor(0);
      textManager.drawString(g, 2227, this.LEVEL_COMPLETE_BODY_FONT, x + 1, y + 1, 9);
      stringRenderer2.setColor(color2);
      textManager.drawString(g, 2227, this.LEVEL_COMPLETE_BODY_FONT, x, y, 9);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_restart.contains(x, y))
        this.m_restart.pointerPressed(this.m_restart.toRelativeX(x), this.m_restart.toRelativeY(y), pointerNum);
      else if (this.m_restart.isPressed())
        this.m_restart.unpress();
      if (this.m_next.contains(x, y))
        this.m_next.pointerPressed(this.m_next.toRelativeX(x), this.m_next.toRelativeY(y), pointerNum);
      else if (this.m_next.isPressed())
        this.m_next.unpress();
      if (this.m_menu.contains(x, y))
        this.m_menu.pointerPressed(this.m_menu.toRelativeX(x), this.m_menu.toRelativeY(y), pointerNum);
      else if (this.m_menu.isPressed())
        this.m_menu.unpress();
      return false;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      AppEngine canvas = AppEngine.getCanvas();
      if (this.m_restart.contains(x, y))
      {
        this.m_restart.pointerReleased(this.m_restart.toRelativeX(x), this.m_restart.toRelativeY(y), pointerNum);
        canvas.getSceneGame().setExitState(SceneGame.GameState.STATE_FADE_TO_RESTART);
        this.m_buttonClicked = true;
      }
      else if (this.m_next.contains(x, y))
      {
        this.m_next.pointerReleased(this.m_next.toRelativeX(x), this.m_next.toRelativeY(y), pointerNum);
        canvas.getSceneGame().setExitState(SceneGame.GameState.STATE_TRANS_TO_NEXT);
        this.m_buttonClicked = true;
      }
      else if (this.m_menu.contains(x, y))
      {
        this.m_menu.pointerReleased(this.m_menu.toRelativeX(x), this.m_menu.toRelativeY(y), pointerNum);
        canvas.getSceneGame().setExitState(SceneGame.GameState.STATE_FADE_TO_MENU);
        this.m_buttonClicked = true;
      }
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_restart.isPressed() && !this.m_restart.contains(x, y))
        this.m_restart.unpress();
      if (this.m_next.isPressed() && !this.m_next.contains(x, y))
        this.m_next.unpress();
      if (this.m_menu.isPressed() && !this.m_menu.contains(x, y))
        this.m_menu.unpress();
      return false;
    }

    public override bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      if (!this.m_MenuButtonHidden)
      {
        x = this.m_menu.getX() + (this.m_menu.getWidth() >> 1);
        y = this.m_menu.getY() + (this.m_menu.getHeight() >> 1);
      }
      else
      {
        x = this.m_next.getX() + (this.m_next.getWidth() >> 1);
        y = this.m_next.getY() + (this.m_next.getHeight() >> 1);
      }
      return true;
    }

    public void setBackgroundQuads(int quadGroupId) => this.m_quadManager.loadQuads(quadGroupId);

    public abstract void UnHide();
  }
}
