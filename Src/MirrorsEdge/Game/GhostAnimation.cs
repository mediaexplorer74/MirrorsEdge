
// Type: game.GhostAnimation
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using GameManager;

#nullable disable
namespace game
{
  public static class GhostAnimation
  {
    public const int SETTING_VERSION = 3;
    public const int FILENAME_DIGIT_INDEX_TENS = 6;
    public const int FILENAME_DIGIT_INDEX_ONES = 7;
    public const string GHOST_FILENAME = "GhostS";

    public static string getCurrentLevelGhostFilename()
    {
      int currentLevelIndex = AppEngine.getLevelData().getCurrentLevelIndex();
      return "GhostS" + (object) (currentLevelIndex / 10) + (object) (currentLevelIndex % 10) + (MirrorsEdge.TrialMode ? (object) "_trial" : (object) "");
    }
  }
}
