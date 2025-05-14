
// Type: UI.ChapterSelectSpeedrunWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using mirrorsedge_wp7;
using support;
using System;
using text;

#nullable disable
namespace UI
{
  public class ChapterSelectSpeedrunWindow : ChapterSelectWindow
  {
    public ChapterSelectSpeedrunWindow()
      : base(true)
    {
      AppEngine canvas = AppEngine.getCanvas();
      LevelData levelData = AppEngine.getLevelData();
      if (MirrorsEdge.TrialMode)
      {
        int num = Math.Min(levelData.getNumUnlockedLevels(), levelData.getLevelNum());
        for (int levelIndex = 0; levelIndex < num; ++levelIndex)
          this.m_chapterPanel.addItem((WindowElement) new ChapterSelectItemSpeedrun(levelData.getLevel(levelIndex)));
      }
      else
      {
        int numUnlockedLevels = levelData.getNumUnlockedLevels();
        int levelNum = levelData.getLevelNum();
        for (int levelIndex = 0; levelIndex < levelNum; ++levelIndex)
        {
          Level level = levelData.getLevel(levelIndex);
          if (levelIndex < numUnlockedLevels || level.isLevelComplete())
            this.m_chapterPanel.addItem((WindowElement) new ChapterSelectItemSpeedrun(level));
        }
      }
      canvas.getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_SPEEDRUN_STARS_MENU"), true);
    }

    public override void Destructor()
    {
      AppEngine.getCanvas().getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_SPEEDRUN_STARS_MENU"), false);
      base.Destructor();
    }

    public override void onSelected()
    {
      int name = (this.m_chapterPanel.getSelectedItem() as ChapterSelectItem).getLevelObject().getName();
      AppEngine.getLevelData().setCurrentLevelByName(LevelData.GameMode.GAME_MODE_SPEEDRUN, name);
      AppEngine.getCanvas().getSceneMenu().stateTransitionFade(SceneMenu.MenuState.STATE_TRANSITION_TO_GAME);
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      AppEngine canvas = AppEngine.getCanvas();
      LevelData levelData = AppEngine.getLevelData();
      Level level = levelData.getLevel(this.m_chapterPanel.getSelectedNotch());
      TextManager textManager = canvas.getTextManager();
      int numCollectedStars = levelData.calculateNumCollectedStars();
      StringBuffer statLabel = canvas.getStatLabel(2322);
      statLabel.append(' ');
      canvas.appendStatValue(statLabel, AppEngine.StatType.STAT_TYPE_INT, numCollectedStars);
      textManager.drawString(g, statLabel, 6, 460, 18, 68);
      canvas.getQuadManager().setMeshPosition((int) QuadManager.get("MESH_SPEEDRUN_STARS_TOTAL"), (float) (460 - textManager.getStringWidth(statLabel, 6) - 5), 16f, 20);
      canvas.getQuadManager().render(g);
      textManager.drawString(g, 2112, 12, 74, 162, 65);
      for (int index = 0; index != 3; ++index)
      {
        int y = 185 + index * 24;
        int requirementMillis = level.getSpeedRunRequirementMillis(index + 1);
        canvas.drawStatString(g, 6, 2048, AppEngine.StatType.STAT_TYPE_TIME_MILLIS, requirementMillis, 170, y, 65, false);
      }
    }
  }
}
