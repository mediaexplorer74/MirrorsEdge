
// Type: UI.ChapterSelectStoryWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using System;

#nullable disable
namespace UI
{
  public class ChapterSelectStoryWindow : ChapterSelectWindow
  {
    public ChapterSelectStoryWindow()
      : base(false)
    {
      LevelData levelData = AppEngine.getLevelData();
      int num = Math.Min(levelData.getLevelNum(), levelData.getNumUnlockedLevels() + 1);
      for (int levelIndex = 0; levelIndex < num; ++levelIndex)
        this.m_chapterPanel.addItem((WindowElement) new ChapterSelectItemStory(levelData.getLevel(levelIndex)));
    }

    public override void onSelected()
    {
      int name = (this.m_chapterPanel.getSelectedItem() as ChapterSelectItem).getLevelObject().getName();
      AppEngine.getLevelData().setCurrentLevelByName(LevelData.GameMode.GAME_MODE_STORY, name);
      AppEngine.getCanvas().getSceneMenu().stateTransitionFade(SceneMenu.MenuState.STATE_TRANSITION_TO_GAME);
    }
  }
}
