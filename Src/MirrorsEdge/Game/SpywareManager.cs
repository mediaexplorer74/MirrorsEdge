
// Type: game.SpywareManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using ea;

#nullable disable
namespace game
{
  public class SpywareManager
  {
    public const int EVT_CONTINUE_GAME = 346;
    public const int EVT_NEW_GAME = 347;
    public const int EVT_VIEW_ABOUT = 348;
    public const int EVT_VIEW_BADGES = 349;
    public const int EVT_VIEW_MORE_GAMES = 350;
    public const int EVT_SELECT_MEDIA_PICKER = 351;
    public const int EVT_ACHIEVED_BADGE_X = 352;
    public const int EVT_GAME_PAUSED = 353;
    public const int EVT_PAUSED_RESUME_LEVEL_X = 354;
    public const int EVT_PAUSED_RESTART_LEVEL_X = 355;
    public const int EVT_PAUSED_QUIT_LEVEL_X = 356;
    public const int EVT_STORY_LEVEL_X_STARTED = 357;
    public const int EVT_RACE_LEVEL_X_STARTED = 358;
    public const int EVT_LEVEL_X_ABANDONED = 359;
    public const int EVT_LEVEL_X_FINISHED = 360;
    public const int EVT_BAG_PICKED_UP = 361;
    public const int EVT_CHECKPOINT_TRIGGERED = 362;
    public const int EVT_GUARD_X_KILLED = 363;
    public const int EVT_GUARD_X_PASSED = 364;
    public const int EVT_FAITH_KILLED = 365;
    public const int EVT_OPTIONS_SOUND = 366;
    public const int EVT_OPT_FULL_PURCHASE = 30000;
    public const int EVT_ENTER_FULL_GAME_OVERVIEW_SCREEN = 30008;
    public const int EVT_LITE_ED_GAME_DEMO_START = 30009;
    public const int EVT_LITE_ED_GAME_DEMO_END = 30010;
    public const int EVT_TRACKING_OPT_OUT = 30024;
    private static SpywareManager getInstance_instance = new SpywareManager();
    private bool m_Enabled;

    public static SpywareManager getInstance() => SpywareManager.getInstance_instance;

    public bool isEnabled() => this.m_Enabled;

    public void setEnabled(bool enabled) => this.m_Enabled = enabled;

    public void trackContinueGame() => EASpywareManager.getInstance().logEvent(346);

    public void trackNewGame() => EASpywareManager.getInstance().logEvent(347);

    public void trackGamePaused() => EASpywareManager.getInstance().logEvent(353);

    public void trackViewAbout() => EASpywareManager.getInstance().logEvent(348);

    public void trackViewBadges() => EASpywareManager.getInstance().logEvent(349);

    public void trackViewMoreGames() => EASpywareManager.getInstance().logEvent(350);

    public void trackSelectMediaPicker() => EASpywareManager.getInstance().logEvent(351);

    public void trackAchievedBadge(int badgeId)
    {
      EASpywareManager.getInstance().logEvent(352, badgeId);
    }

    public void trackResumeLevel(int levelId)
    {
      EASpywareManager.getInstance().logEvent(354, levelId);
    }

    public void trackRestartLevel(int levelId)
    {
      EASpywareManager.getInstance().logEvent(355, levelId);
    }

    public void trackQuitLevel(int levelId)
    {
      EASpywareManager.getInstance().logEvent(356, levelId);
    }

    public void trackStoryLevelStarted(int levelId)
    {
      EASpywareManager.getInstance().logEvent(357, levelId);
    }

    public void trackRaceLevelStarted(int levelId)
    {
      EASpywareManager.getInstance().logEvent(358, levelId);
    }

    public void trackLevelAbandoned(int levelId)
    {
      EASpywareManager.getInstance().logEvent(359, levelId);
    }

    public void trackLevelFinished(int levelId)
    {
      EASpywareManager.getInstance().logEvent(360, levelId);
    }

    public void trackBagPickedUp(int bagId) => EASpywareManager.getInstance().logEvent(361, bagId);

    public void trackCheckpointTriggered(int checkpointId)
    {
      EASpywareManager.getInstance().logEvent(362, checkpointId);
    }

    public void trackGuardKilled(int guardId)
    {
      EASpywareManager.getInstance().logEvent(363, guardId);
    }

    public void trackGuardPassed(int guardId)
    {
      EASpywareManager.getInstance().logEvent(364, guardId);
    }

    public void trackFaithKilled() => EASpywareManager.getInstance().logEvent(365);

    public void trackOptionsSound() => EASpywareManager.getInstance().logEvent(366);

    public void trackEnterUpsellScreen() => EASpywareManager.getInstance().logEvent(30008);

    public void trackOptToBuyFullVersion() => EASpywareManager.getInstance().logEvent(30000);

    public void trackOptOut() => EASpywareManager.getInstance().logEvent(30024);

    public void trackDemoStart() => EASpywareManager.getInstance().logEvent(30009);

    public void trackDemoEnd() => EASpywareManager.getInstance().logEvent(30010);
  }
}
