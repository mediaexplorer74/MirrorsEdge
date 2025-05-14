// Decompiled with JetBrains decompiler
// Type: UI.AchievementsList
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;

#nullable disable
namespace UI
{
  public class AchievementsList : Window
  {
    public const int LIST_YPOS_OFFSET = 20;
    public const int LIST_X_PADDING = 2;
    public const int LIST_BOTTOM_PADDING = 5;
    public const int ITEM_PADDING = 2;
    public const int ITEM_SPACING = 8;
    private AchievementData m_ad;

    public AchievementsList(int x, int y, int width, int height)
      : base(x, y, width, height)
    {
      this.m_ad = AppEngine.getAchievementData();
      this.m_clientPaddingX = 0;
      this.m_clientPaddingY = 0;
      this.adjustClientArea();
      int y1 = 0;
      int achievementNum = this.m_ad.getAchievementNum();
      for (int id = 0; id < achievementNum; ++id)
      {
        AchievementItem achievementItem = new AchievementItem(this, id);
        achievementItem.setPosition(2, y1);
        this.addElement((WindowElement) achievementItem);
        y1 += achievementItem.getHeight() + 8;
      }
    }

    public override void Destructor()
    {
      this.m_ad = (AchievementData) null;
      base.Destructor();
    }

    public AchievementData getAchievementData() => this.m_ad;
  }
}
