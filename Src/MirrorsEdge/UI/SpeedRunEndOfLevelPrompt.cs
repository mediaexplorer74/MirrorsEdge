
// Type: UI.SpeedRunEndOfLevelPrompt
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using mirrorsedge_wp7;
using support;
using text;

#nullable disable
namespace UI
{
  public class SpeedRunEndOfLevelPrompt : EndOfLevelPrompt
  {
    protected const int TIME_X = 15;
    protected const int TIME_Y = 36;
    protected const int LABEL_X_PADDING = 4;
    protected const int TIME_Y_PADDING = 2;
    protected string m_timeString;
    protected string m_bestTimeString;
    protected bool m_hidden;
    protected int LEVEL_COMPLETE_LABEL_FONT = 27;
    protected int LEVEL_COMPLETE_TIME_FONT = 30;
    protected int LEVEL_COMPLETE_BEST_TIME_LABEL_FONT = 32;
    protected int LEVEL_COMPLETE_BEST_TIME_FONT = 31;

    public SpeedRunEndOfLevelPrompt()
    {
      this.m_timeString = (string) null;
      this.m_bestTimeString = (string) null;
      this.m_hidden = false;
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      TextManager textManager = canvas.getTextManager();
      Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
      int raceTime = canvas.getSceneGame().getRaceTime();
      StringBuffer stringBuffer1 = textManager.clearStringBuffer();
      textManager.appendMillisTimeToBuffer(stringBuffer1, raceTime, 2);
      this.m_timeString = stringBuffer1.toString();
      int speedRunTimeMillis = currentLevelObject.getBestSpeedRunTimeMillis();
      StringBuffer stringBuffer2 = textManager.clearStringBuffer();
      textManager.appendMillisTimeToBuffer(stringBuffer2, speedRunTimeMillis, 2);
      this.m_bestTimeString = stringBuffer2.toString();
      int numStarsAchieved = currentLevelObject.getNumStarsAchieved();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_SPEEDRUN_STARS_LEVEL_COMPLETE"), true);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_1"), 1 <= numStarsAchieved);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_2"), 2 <= numStarsAchieved);
      quadManager.setMeshVisible((int) QuadManager.get("MESH_SPEEDRUN_STARS_LEVEL_COMPLETE_3"), 3 <= numStarsAchieved);
      if (MirrorsEdge.TrialMode)
      {
        int levelIndex = AppEngine.getLevelData().getCurrentLevelIndex() + 1;
        if (levelIndex == AppEngine.getLevelData().getLevelNum() || AppEngine.getLevelData().getLevel(levelIndex).isLevelComplete())
          return;
        this.m_next.setPosition(-500, -500);
      }
      else
      {
        int currentLevelIndex = AppEngine.getLevelData().getCurrentLevelIndex();
        int numUnlockedLevels = AppEngine.getLevelData().getNumUnlockedLevels();
        int levelNum = AppEngine.getLevelData().getLevelNum();
        if (currentLevelIndex < levelNum - 1 && currentLevelIndex < numUnlockedLevels - 1)
          return;
        this.m_next.setPosition(-this.m_next.getWidth(), -this.m_next.getHeight());
      }
    }

    public override void Destructor()
    {
      AppEngine.getCanvas().getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_SPEEDRUN_STARS_LEVEL_COMPLETE"), false);
      this.m_timeString = (string) null;
      this.m_bestTimeString = (string) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_hidden)
        return;
      base.render(g, top, left);
      AppEngine canvas = AppEngine.getCanvas();
      TextManager textManager = canvas.getTextManager();
      StringRenderer stringRenderer1 = textManager.getStringRenderer(this.LEVEL_COMPLETE_TIME_FONT);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(16777215);
      textManager.drawString(g, this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT, this.m_width - 15 + 1, 37, 68);
      stringRenderer1.setColor(color1);
      textManager.drawString(g, this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT, this.m_width - 15, 36, 68);
      int x1 = this.m_width - 15 - textManager.getStringWidth(this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT) - 4;
      StringRenderer stringRenderer2 = textManager.getStringRenderer(this.LEVEL_COMPLETE_LABEL_FONT);
      int color2 = stringRenderer2.getColor();
      stringRenderer2.setColor(0);
      textManager.drawString(g, 2317, this.LEVEL_COMPLETE_LABEL_FONT, x1 + 1, 37, 68);
      stringRenderer2.setColor(color2);
      textManager.drawString(g, 2317, this.LEVEL_COMPLETE_LABEL_FONT, x1, 36, 68);
      int x2 = this.m_width - 15 - textManager.getStringWidth(this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT);
      int y = 36 + textManager.getLineHeight(this.LEVEL_COMPLETE_BEST_TIME_FONT) + 2;
      StringRenderer stringRenderer3 = textManager.getStringRenderer(this.LEVEL_COMPLETE_BEST_TIME_FONT);
      int color3 = stringRenderer3.getColor();
      stringRenderer3.setColor(13684944);
      textManager.drawString(g, this.m_bestTimeString, this.LEVEL_COMPLETE_BEST_TIME_FONT, x2 + 1, y + 1, 65);
      stringRenderer3.setColor(color3);
      textManager.drawString(g, this.m_bestTimeString, this.LEVEL_COMPLETE_BEST_TIME_FONT, x2, y, 65);
      int x3 = x2 - 4;
      StringRenderer stringRenderer4 = textManager.getStringRenderer(this.LEVEL_COMPLETE_BEST_TIME_LABEL_FONT);
      int color4 = stringRenderer4.getColor();
      stringRenderer4.setColor(13684944);
      textManager.drawString(g, 2318, this.LEVEL_COMPLETE_BEST_TIME_LABEL_FONT, x3 + 1, y + 1, 68);
      stringRenderer4.setColor(color4);
      textManager.drawString(g, 2318, this.LEVEL_COMPLETE_BEST_TIME_LABEL_FONT, x3, y, 68);
      canvas.getQuadManager().render(g);
    }

    public override void update(int timestamp) => base.update(timestamp);

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      if (MirrorsEdge.TrialMode)
        base.pointerPressed(x, y, pointerNum);
      else
        base.pointerPressed(x, y, pointerNum);
      return false;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      if (MirrorsEdge.TrialMode)
        base.pointerReleased(x, y, pointerNum);
      else
        base.pointerReleased(x, y, pointerNum);
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerDragged(x, y, pointerNum);
      int num = MirrorsEdge.TrialMode ? 1 : 0;
      return false;
    }

    public override void UnHide() => this.m_hidden = false;
  }
}
