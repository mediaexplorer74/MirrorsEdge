
// Type: UI.AchievementNotification
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using System;
using System.Collections.Generic;
using text;

#nullable disable
namespace UI
{
  public class AchievementNotification : BorderedElement
  {
    private const int NOTIFICATION_X_PADDING = 20;
    private const int NOTIFICATION_CONTENT_PADDING = 5;
    private const int BADGE_WIDTH = 28;
    private const int NOTIFICATION_SLIDE_SPEED = 40;
    private const int NOTIFICATION_HOLD_TIME = 2000;
    private const int NOTIFICATION_SEPARATE_TIME = 500;
    private const int TITLE_Y_POS = 8;
    private const int DESC_Y_POS = -5;
    private AchievementNotification.State m_state;
    private int m_stateTime;
    private float m_windowOffset;
    private float m_badgeAlpha;
    private Image iconImage;
    private Queue<Achievement> m_achievementQueue;
    private string m_title;
    private string m_description;
    private int FONT_TITLE = 18;
    private int FONT_DESCRIPTION = 19;

    public AchievementNotification()
      : base(0, 0, 200, 43)
    {
      this.m_state = AchievementNotification.State.STATE_NONE;
      this.m_stateTime = 0;
      this.m_windowOffset = 0.0f;
      this.m_badgeAlpha = 0.0f;
      this.m_achievementQueue = new Queue<Achievement>();
      this.m_title = (string) null;
      this.m_description = (string) null;
    }

    public override void Destructor()
    {
      this.m_achievementQueue.Clear();
      this.m_achievementQueue = (Queue<Achievement>) null;
      this.m_title = (string) null;
      this.m_description = (string) null;
      base.Destructor();
    }

    public void addAchievement(Achievement achievement)
    {
      this.m_achievementQueue.Enqueue(achievement);
    }

    public override void update(int timeStep)
    {
      float num1 = (float) timeStep / 1000f;
      switch (this.m_state)
      {
        case AchievementNotification.State.STATE_OUT:
          Achievement achievement = this.m_achievementQueue.Peek();
          this.m_title = achievement.getNameStringBuffer().toString();
          this.m_description = achievement.getCompletedDescriptionStringBuffer().toString();
          this.iconImage = achievement.iconOpened;
          TextManager textManager = AppEngine.getCanvas().getTextManager();
          this.setWidth(10 + (Math.Max(textManager.getStringWidth(this.m_title, this.FONT_TITLE), 
              textManager.getStringWidth(this.m_description, this.FONT_DESCRIPTION)) + 28 + 5));
          this.m_state = AchievementNotification.State.STATE_SLIDE_IN;
          this.m_stateTime = 0;
          this.m_badgeAlpha = 0.0f;
          break;
        case AchievementNotification.State.STATE_SLIDE_IN:
          this.m_windowOffset += 40f * num1;
          if ((double) this.m_windowOffset >= (double) this.m_height)
          {
            this.m_windowOffset = (float) this.m_height;
            this.m_state = AchievementNotification.State.STATE_HOLD;
            this.m_stateTime = 2000;
            break;
          }
          break;
        case AchievementNotification.State.STATE_HOLD:
          this.m_stateTime -= timeStep;
          int num2 = 1000;
          this.m_badgeAlpha = (float) (1.0 - (double) (this.m_stateTime - num2) / (double) num2);
          this.m_badgeAlpha = Math.Min(this.m_badgeAlpha, 1f);
          if (this.m_stateTime <= 0)
          {
            this.m_state = AchievementNotification.State.STATE_SLIDE_OUT;
            break;
          }
          break;
        case AchievementNotification.State.STATE_SLIDE_OUT:
          this.m_windowOffset -= 40f * num1;
          if ((double) this.m_windowOffset <= 0.0)
          {
            this.m_achievementQueue.Dequeue();
            this.m_state = AchievementNotification.State.STATE_NONE;
            this.m_stateTime = 500;
            this.m_windowOffset = 0.0f;
            this.iconImage = (Image) null;
            break;
          }
          break;
        case AchievementNotification.State.STATE_NONE:
          if (this.m_stateTime > 0)
            this.m_stateTime -= timeStep;
          if (this.m_stateTime <= 0 && this.m_achievementQueue.Count > 0)
          {
            this.m_state = AchievementNotification.State.STATE_OUT;
            this.m_stateTime = 0;
            break;
          }
          break;
      }
      int y = (int) ((double) AppEngine.getCanvas().getHeight() - (double) this.m_windowOffset);
      this.setPosition(AppEngine.getCanvas().getWidth() - 20 - this.m_width, y);
    }

    public override void render(Graphics g, int top, int left)
    {
      if (this.m_state == AchievementNotification.State.STATE_NONE || this.m_state == AchievementNotification.State.STATE_OUT)
        return;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      base.render(g, top, left);
      g.drawScaledRegion( 
          this.iconImage, 0, 0, this.iconImage.getWidth(), this.iconImage.getHeight(),
          left + this.m_x + 5, 
          top + this.m_y, 
          left + this.m_x + 5 + (int)(28 / Runtime.pixelScale), 
          top + this.m_y + (int)(28 / Runtime.pixelScale)  );
      int num1 = 33;
      int num2 = num1 + (this.m_width - num1 >> 1);
      textManager.drawString(g, this.m_title, this.FONT_TITLE, left + this.m_x + num2, top + this.m_y + 8, 10);
      textManager.drawString(g, this.m_description, this.FONT_DESCRIPTION, left + this.m_x + num2, top + this.m_y + this.m_height - 5, 34);
    }

    private enum State
    {
      STATE_OUT,
      STATE_SLIDE_IN,
      STATE_HOLD,
      STATE_SLIDE_OUT,
      STATE_NONE,
    }
  }
}
