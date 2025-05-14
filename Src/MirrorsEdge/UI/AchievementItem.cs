
// Type: UI.AchievementItem
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using System;
using text;

#nullable disable
namespace UI
{
  public class AchievementItem : WindowElement
  {
    public const int ITEM_WIDTH = 463;
    public const int DESCRIPTION_BG_WIDTH = 418;
    public const int DESCRIPTION_PADDING_X = 5;
    public const int DESCRIPTION_PADDING_Y = 3;
    public const int BADGE_OFFSET_X = 441;
    public int FONT_TITLE = 18;
    public int FONT_DESCRIPTION = 19;
    private int m_achievementId;
    private string m_StringGamePoints;
    private string m_titleString;
    private WrappedString m_descriptionString;

    public AchievementItem(AchievementsList parent, int id)
      : base(0, 0, 463, 0)
    {
      this.m_achievementId = id;
      this.m_titleString = (string) null;
      this.m_descriptionString = new WrappedString();
      this.m_parent = (WindowElement) parent;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Achievement achievement = parent.getAchievementData().getAchievement(id);
      this.m_StringGamePoints = string.Concat((object) achievement.m_GamePoints);
      this.m_titleString = achievement.getNameStringBuffer().toString();
      this.m_descriptionString.wrapString((achievement.isComplete() ? achievement.getCompletedDescriptionStringBuffer() : achievement.getDescriptionStringBuffer()).toString(), this.FONT_DESCRIPTION, 408, false);
      int numWrappedLines = this.m_descriptionString.getNumWrappedLines();
      this.setHeight(textManager.getLineHeight(this.FONT_TITLE) + textManager.getLineHeight(this.FONT_DESCRIPTION) * numWrappedLines + 3);
    }

    public override void Destructor()
    {
      this.m_titleString = (string) null;
      this.m_descriptionString.Destructor();
      this.m_descriptionString = (WrappedString) null;
      base.Destructor();
    }

    public override void render(Graphics g, int top, int left)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int num1 = top + this.m_y;
      int num2 = num1 + this.m_height;
      int num3 = left + this.m_x;
      int num4 = num3 + this.m_width;
      int clipX = g.getClipX();
      int clipY = g.getClipY();
      int num5 = clipX + g.getClipWidth();
      int num6 = clipY + g.getClipHeight();
      if (num3 > num5 || num4 < clipX || num1 > num6 || num2 < clipY)
        return;
      g.setColor(16777215);
      g.fillRect(left + this.m_x, top + this.m_y, 418, this.m_height);
      textManager.drawString(g, this.m_titleString, this.FONT_TITLE, left + this.m_x + 5, top + this.m_y + 3, 9);
      this.m_descriptionString.draw(g, left + this.m_x + 5, top + this.m_y + 3 + textManager.getLineHeight(this.FONT_TITLE), 9);
      textManager.drawString(g, this.m_StringGamePoints, this.FONT_TITLE, left + this.m_x + 441 + 50, top + this.m_y + 3 + this.m_height, 36);
      Achievement achievement = (this.m_parent as AchievementsList).getAchievementData().getAchievement(this.m_achievementId);
      Image src = achievement.isComplete() ? achievement.iconOpened : achievement.iconLocked;
      g.drawScaledRegion(src, 0, 0, src.getWidth(), src.getHeight(), left + this.m_x + 441, top + this.m_y, left + this.m_x + 441 + Math.Min(src.getWidth(), this.m_height) / Runtime.pixelScale, top + this.m_y + Math.Min(src.getHeight(), this.m_height) / Runtime.pixelScale);
    }
  }
}
