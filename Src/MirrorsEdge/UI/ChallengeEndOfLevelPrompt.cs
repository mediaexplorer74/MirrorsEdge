
// Type: UI.ChallengeEndOfLevelPrompt
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class ChallengeEndOfLevelPrompt : EndOfLevelPrompt
  {
    protected const int TIME_X = 15;
    protected const int TIME_Y = 36;
    protected const int LABEL_X_PADDING = 4;
    protected const int TIME_Y_PADDING = 2;
    protected string m_timeString;
    protected string m_bestTimeString;
    protected bool m_hidden;
    protected bool m_success;
    protected int LEVEL_COMPLETE_LABEL_FONT = 27;
    protected int LEVEL_COMPLETE_TIME_FONT = 30;
    protected int LEVEL_COMPLETE_BEST_TIME_LABEL_FONT = 32;
    protected int LEVEL_COMPLETE_BEST_TIME_FONT = 31;

    public ChallengeEndOfLevelPrompt()
    {
      this.m_timeString = (string) null;
      this.m_bestTimeString = (string) null;
      this.m_hidden = false;
      this.m_success = false;
      AppEngine canvas = AppEngine.getCanvas();
      TextManager textManager = canvas.getTextManager();
      int raceTime = canvas.getSceneGame().getRaceTime();
      StringBuffer stringBuffer1 = textManager.clearStringBuffer();
      textManager.appendMillisTimeToBuffer(stringBuffer1, raceTime, 2);
      this.m_timeString = stringBuffer1.toString();
      int challengeTime = canvas.getChallengeTime();
      StringBuffer stringBuffer2 = textManager.clearStringBuffer();
      textManager.appendMillisTimeToBuffer(stringBuffer2, challengeTime, 2);
      this.m_bestTimeString = stringBuffer2.toString();
      this.m_success = raceTime < challengeTime;
      this.m_next.setPosition(-this.m_next.getWidth(), -this.m_next.getHeight());
    }

    public override void Destructor()
    {
      this.m_timeString = (string) null;
      this.m_bestTimeString = (string) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_hidden)
        return;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
      this.m_menu.render(g, top, left);
      this.m_restart.render(g, top, left);
      StringRenderer stringRenderer1 = textManager.getStringRenderer(this.LEVEL_COMPLETE_TITLE_FONT);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(0);
      textManager.drawString(g, 2349, this.LEVEL_COMPLETE_TITLE_FONT, 21, 16, 9);
      stringRenderer1.setColor(color1);
      textManager.drawString(g, 2349, this.LEVEL_COMPLETE_TITLE_FONT, 20, 15, 9);
      int y1 = 15 + textManager.getLineHeight(this.LEVEL_COMPLETE_TITLE_FONT) + 5;
      int name = currentLevelObject.getName();
      StringRenderer stringRenderer2 = textManager.getStringRenderer(this.LEVEL_COMPLETE_BODY_FONT);
      int color2 = stringRenderer2.getColor();
      stringRenderer2.setColor(0);
      textManager.drawString(g, name, this.LEVEL_COMPLETE_BODY_FONT, 21, y1 + 1, 9);
      stringRenderer2.setColor(color2);
      textManager.drawString(g, name, this.LEVEL_COMPLETE_BODY_FONT, 20, y1, 9);
      StringRenderer stringRenderer3 = textManager.getStringRenderer(this.LEVEL_COMPLETE_TIME_FONT);
      int color3 = stringRenderer3.getColor();
      stringRenderer3.setColor(8421504);
      textManager.drawString(g, this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT, this.m_width - 15 + 1, 37, 68);
      stringRenderer3.setColor(color3);
      textManager.drawString(g, this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT, this.m_width - 15, 36, 68);
      int x1 = this.m_width - 15 - textManager.getStringWidth(this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT) - 4;
      StringRenderer stringRenderer4 = textManager.getStringRenderer(this.LEVEL_COMPLETE_LABEL_FONT);
      int color4 = stringRenderer4.getColor();
      stringRenderer4.setColor(0);
      textManager.drawString(g, 2317, this.LEVEL_COMPLETE_LABEL_FONT, x1 + 1, 37, 68);
      stringRenderer4.setColor(color4);
      textManager.drawString(g, 2317, this.LEVEL_COMPLETE_LABEL_FONT, x1, 36, 68);
      int x2 = this.m_width - 15 - textManager.getStringWidth(this.m_timeString, this.LEVEL_COMPLETE_TIME_FONT);
      int y2 = 36 + textManager.getLineHeight(this.LEVEL_COMPLETE_BEST_TIME_FONT) + 2;
      StringRenderer stringRenderer5 = textManager.getStringRenderer(this.LEVEL_COMPLETE_BEST_TIME_FONT);
      int color5 = stringRenderer5.getColor();
      stringRenderer5.setColor(8421504);
      textManager.drawString(g, this.m_bestTimeString, this.LEVEL_COMPLETE_BEST_TIME_FONT, x2 + 1, y2 + 1, 65);
      stringRenderer5.setColor(color5);
      textManager.drawString(g, this.m_bestTimeString, this.LEVEL_COMPLETE_BEST_TIME_FONT, x2, y2, 65);
      int x3 = x2 - 4;
      StringRenderer stringRenderer6 = textManager.getStringRenderer(this.LEVEL_COMPLETE_BEST_TIME_LABEL_FONT);
      int color6 = stringRenderer6.getColor();
      stringRenderer6.setColor(8421504);
      textManager.drawString(g, 2360, this.LEVEL_COMPLETE_BEST_TIME_LABEL_FONT, x3 + 1, y2 + 1, 68);
      stringRenderer6.setColor(color6);
      textManager.drawString(g, 2360, this.LEVEL_COMPLETE_BEST_TIME_LABEL_FONT, x3, y2, 68);
      int strId = this.m_success ? 2361 : 2362;
      StringRenderer stringRenderer7 = textManager.getStringRenderer(this.LEVEL_COMPLETE_TITLE_FONT);
      int color7 = stringRenderer7.getColor();
      stringRenderer7.setColor(0);
      textManager.drawString(g, strId, this.LEVEL_COMPLETE_TITLE_FONT, (this.m_width >> 1) + 1, (this.m_height >> 1) + 1, 18);
      stringRenderer7.setColor(color7);
      textManager.drawString(g, strId, this.LEVEL_COMPLETE_TITLE_FONT, this.m_width >> 1, this.m_height >> 1, 18);
    }

    public override void update(int timestamp) => base.update(timestamp);

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerPressed(x, y, pointerNum);
      return false;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerReleased(x, y, pointerNum);
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_hidden)
        return false;
      base.pointerDragged(x, y, pointerNum);
      return false;
    }

    public override void UnHide() => this.m_hidden = false;
  }
}
