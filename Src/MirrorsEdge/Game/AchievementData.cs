// Decompiled with JetBrains decompiler
// Type: game.AchievementData
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;
using mirrorsedge_wp7;
using support;
using System.Collections.Generic;

#nullable disable
namespace game
{
  public class AchievementData
  {
    public const int ACHIEVEMENT_TYPE_COMPLETE_CHAPTER = 0;
    public const int ACHIEVEMENT_TYPE_COMPLETE_TIMED_CHAPTER = 1;
    public const int ACHIEVEMENT_TYPE_COLLECT_BAGS_CHAPTER = 2;
    public const int ACHIEVEMENT_TYPE_COLLECT_BAGS = 3;
    public const int ACHIEVEMENT_TYPE_COLLECT_STARS = 4;
    public const int ACHIEVEMENT_TYPE_COMPLETE_GAME_DYING = 5;
    public const int ACHIEVEMENT_TYPE_COMPLETE_GAME_BADLANDING = 6;
    public const int ACHIEVEMENT_TYPE_ADVANCED_MOVE = 7;
    public const int ACHIEVEMENT_TYPE_RUN_DISTANCE = 8;
    public const int ACHIEVEMENT_TYPE_WALL_RUN_DISTANCE = 9;
    public const int ACHIEVEMENT_TYPE_CLIMB_DISTANCE = 10;
    public const int ACHIEVEMENT_TYPE_ZIPLINE_DISTANCE = 11;
    public const int ACHIEVEMENT_TYPE_SLIDE_DISTANCE = 12;
    public const int ACHIEVEMENT_TYPE_BALANCE_DISTANCE = 13;
    public const int ACHIEVEMENT_TYPE_FALL_DISTANCE = 14;
    public const int ACHIEVEMENT_TYPE_SPRINT_TIME = 15;
    public const int ACHIEVEMENT_TYPE_DEFEAT_ENEMY = 16;
    public const int ACHIEVEMENT_TYPE_DISARM_ENEMY = 17;
    public const int ACHIEVEMENT_TYPE_FALL_ENEMY = 18;
    public const int ACHIEVEMENT_TYPE_DEFEAT_ENEMIES_IN_ONE_MOVE = 19;
    public const int ACHIEVEMENT_TYPE_DEFEAT_ENEMIES_IN_TIME = 20;
    public const int ACHIEVEMENT_TYPE_DEFEAT_RIVAL = 21;
    public const int ACHIEVEMENT_TYPE_ZIPLINE_DROP_KICK = 22;
    public const int ACHIEVEMENT_TYPE_BOUDING_BOX = 23;
    public const int ACHIEVEMENT_TYPE_UNIQUE = 24;
    public const int ACHIEVEMENT_COLLECT_STARS_1 = 0;
    public const int ACHIEVEMENT_COLLECT_STARS_14 = 1;
    public const int ACHIEVEMENT_COLLECT_STARS_ALL = 2;
    public const int ACHIEVEMENT_COMPLETE_GAME = 3;
    public const int ACHIEVEMENT_COLLECT_BAG = 4;
    public const int ACHIEVEMENT_COLLECT_BAGS = 5;
    public const int ACHIEVEMENT_BALANCE_DISTANCE = 6;
    public const int ACHIEVEMENT_FALL_DISTANCE = 7;
    public const int ACHIEVEMENT_DEFEAT_ENEMY = 8;
    public const int ACHIEVEMENT_DEFEAT_ENEMY_IN_TIME = 9;
    public const int ACHIEVEMENT_DISARM_ENEMY = 10;
    public const int ACHIEVEMENT_DISARM_ENEMY_10 = 11;
    public const int ACHIEVEMENT_DEFEAT_RIVAL = 12;
    public const int ACHIEVEMENT_DEFEAT_RIVAL_N = 13;
    public const int ACHIEVEMENT_ENEMY_FALL = 13;
    public const int ACHIEVEMENT_ENEMY_FALL_N = 15;
    public const int ACHIEVEMENT_DISARM_2_ENEMIES_ONE_MOVE = 16;
    public const int ACHIEVEMENT_ZIPLINE_DROP_ATTACK = 17;
    public const int ACHIEVEMENT_BOUNDING_BOX_SCRUFFY = 18;
    public const int ACHIEVEMENT_BOUNDING_BOX_RADIO = 19;
    private const string ACHIEVEMENT_FILENAME = "achievements_data3";
    public static readonly short[] STRING_ARRAY = new short[56]
    {
      (short) 2336,
      (short) 2335,
      (short) 2340,
      (short) 2339,
      (short) 2338,
      (short) 2337,
      (short) 2167,
      (short) 2166,
      (short) 2169,
      (short) 2168,
      (short) 2171,
      (short) 2170,
      (short) 2173,
      (short) 2172,
      (short) 2185,
      (short) 2184,
      (short) 2187,
      (short) 2186,
      (short) 2189,
      (short) 2188,
      (short) 2191,
      (short) 2190,
      (short) 2193,
      (short) 2192,
      (short) 2195,
      (short) 2194,
      (short) 2197,
      (short) 2196,
      (short) 2217,
      (short) 2216,
      (short) 2199,
      (short) 2198,
      (short) 2201,
      (short) 2200,
      (short) 2203,
      (short) 2202,
      (short) 2207,
      (short) 2206,
      (short) 2209,
      (short) 2208,
      (short) 2223,
      (short) 2222,
      (short) 2225,
      (short) 2224,
      (short) 2211,
      (short) 2210,
      (short) 2213,
      (short) 2212,
      (short) 2205,
      (short) 2204,
      (short) 2215,
      (short) 2214,
      (short) 2219,
      (short) 2218,
      (short) 2221,
      (short) 2220
    };
    private static readonly bool[] AVAILABLE_ACHIEVEMENTS = new bool[28]
    {
      true,
      true,
      true,
      true,
      true,
      true,
      false,
      false,
      false,
      false,
      false,
      false,
      true,
      true,
      false,
      true,
      false,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true,
      true
    };
    public static readonly short[] COMPLETED_STRING_ARRAY = new short[20]
    {
      (short) 2424,
      (short) 2425,
      (short) 2426,
      (short) 2427,
      (short) 2428,
      (short) 2429,
      (short) 2430,
      (short) 2431,
      (short) 2432,
      (short) 2433,
      (short) 2434,
      (short) 2435,
      (short) 2436,
      (short) 2437,
      (short) 2438,
      (short) 2439,
      (short) 2440,
      (short) 2441,
      (short) 2442,
      (short) 2443
    };
    public static readonly string[] ICONS_FILE_NAME_MASK = new string[20]
    {
      "achiev_00_on_clock",
      "achiev_01_still_counting",
      "achiev_02_superstar",
      "achiev_03_that_s_a_wrap",
      "achiev_04_bag_lady",
      "achiev_05_hoarder",
      "achiev_06_balancing_act",
      "achiev_07_free_falling",
      "achiev_08_tango_down",
      "achiev_09_rampage",
      "achiev_10_i_ll_take_that",
      "achiev_11_neutralizer",
      "achiev_12_tag_you_re_it",
      "achiev_13_thrill_of_the_chase",
      "achiev_14_falling_down",
      "achiev_15_scream_for_me",
      "achiev_16_economist",
      "achiev_17_death_from_above",
      "achiev_18_scruffy",
      "achiev_19_i_heart_88_7"
    };
    public static readonly int[] GAME_POINTS_ARRAY = new int[20]
    {
      5,
      10,
      15,
      15,
      5,
      10,
      10,
      10,
      5,
      15,
      5,
      10,
      5,
      15,
      5,
      10,
      10,
      10,
      15,
      15
    };
    public static readonly string[] ACHIEVEMENT_SERVER_KEYS = new string[20]
    {
      "ACHIEVEMENT_COLLECT_STARS",
      "ACHIEVEMENT_COLLECT_NUM_STARS",
      "ACHIEVEMENT_COLLECT_ALL_STARS",
      nameof (ACHIEVEMENT_COMPLETE_GAME),
      nameof (ACHIEVEMENT_COLLECT_BAG),
      nameof (ACHIEVEMENT_COLLECT_BAGS),
      nameof (ACHIEVEMENT_BALANCE_DISTANCE),
      nameof (ACHIEVEMENT_FALL_DISTANCE),
      nameof (ACHIEVEMENT_DEFEAT_ENEMY),
      "ACHIEVEMENT_RAMPAGE",
      nameof (ACHIEVEMENT_DISARM_ENEMY),
      "ACHIEVEMENT_DISARM_ENEMY_N",
      "ACHIEVEMENT_RUNNER",
      "ACHIEVEMENT_RUNNER_N",
      "ACHIEVEMENT_FALL_ENEMY",
      "ACHIEVEMENT_FALL_ENEMY_N",
      "ACHIEVEMENT_TWO_ENEMIES",
      "ACHIEVEMENT_ZIPLINE_KICK",
      "ACHIEVEMENT_SCRUFFY",
      "ACHIEVEMENT_RADIO"
    };
    public static int m_totalGamePoints;
    private Achievement[] m_allAchievements;
    private List<Achievement> m_uniqueAchievements;
    private List<LevelAchievement> m_levelAchievements;
    private List<TimedLevelAchievement> m_timedLevelAchievements;
    private List<BagsPerLevelAchievement> m_bagsPerLevelAchievements;
    private List<CountAchievement> m_bagsAchievements;
    private List<CountAchievement> m_starsAchievements;
    private List<CountAchievement> m_dyingAchievements;
    private List<CountAchievement> m_badLandingAchievements;
    private List<CountAchievement> m_advancedMoveAchievements;
    private List<CountAchievement> m_runDistanceAchievements;
    private List<CountAchievement> m_wallRunDistanceAchievements;
    private List<CountAchievement> m_climbDistanceAchievements;
    private List<CountAchievement> m_ziplineDistanceAchievements;
    private List<CountAchievement> m_balanceDistanceAchievements;
    private List<CountAchievement> m_slideDistanceAchievements;
    private List<CountAchievement> m_fallDistanceAchievements;
    private List<CountAchievement> m_defeatEnemyAchievements;
    private List<CountAchievement> m_disarmEnemyAchievements;
    private List<CountAchievement> m_defeatRivalAchievements;
    private List<CountAchievement> m_fallEnemyAchievements;
    private List<EventsInPhaseAchievement> m_singleAttackAchievements;
    private List<TimedEventsAchievement> m_attacksInTimeAchievements;
    private List<TimedAchievement> m_sprintDurationAchievements;
    private List<Achievement> m_boundingBoxAchievements;
    private bool m_inLevel;
    private int m_currentLevel;
    private int m_gameCompletes;
    private AchievementMetrics m_levelData;
    private AchievementMetrics m_overallData;

    public AchievementData()
    {
      this.m_allAchievements = (Achievement[]) null;
      this.m_uniqueAchievements = new List<Achievement>();
      this.m_levelAchievements = new List<LevelAchievement>();
      this.m_timedLevelAchievements = new List<TimedLevelAchievement>();
      this.m_bagsPerLevelAchievements = new List<BagsPerLevelAchievement>();
      this.m_bagsAchievements = new List<CountAchievement>();
      this.m_starsAchievements = new List<CountAchievement>();
      this.m_dyingAchievements = new List<CountAchievement>();
      this.m_badLandingAchievements = new List<CountAchievement>();
      this.m_advancedMoveAchievements = new List<CountAchievement>();
      this.m_runDistanceAchievements = new List<CountAchievement>();
      this.m_wallRunDistanceAchievements = new List<CountAchievement>();
      this.m_climbDistanceAchievements = new List<CountAchievement>();
      this.m_ziplineDistanceAchievements = new List<CountAchievement>();
      this.m_slideDistanceAchievements = new List<CountAchievement>();
      this.m_balanceDistanceAchievements = new List<CountAchievement>();
      this.m_fallDistanceAchievements = new List<CountAchievement>();
      this.m_defeatEnemyAchievements = new List<CountAchievement>();
      this.m_disarmEnemyAchievements = new List<CountAchievement>();
      this.m_defeatRivalAchievements = new List<CountAchievement>();
      this.m_fallEnemyAchievements = new List<CountAchievement>();
      this.m_singleAttackAchievements = new List<EventsInPhaseAchievement>();
      this.m_attacksInTimeAchievements = new List<TimedEventsAchievement>();
      this.m_sprintDurationAchievements = new List<TimedAchievement>();
      this.m_boundingBoxAchievements = new List<Achievement>();
      this.m_inLevel = false;
      this.m_currentLevel = 0;
      this.m_gameCompletes = 0;
      this.m_levelData = new AchievementMetrics();
      this.m_overallData = new AchievementMetrics();
    }

    public void Destructor()
    {
      this.saveRmsData();
      this.m_uniqueAchievements.Clear();
      this.m_levelAchievements.Clear();
      this.m_timedLevelAchievements.Clear();
      this.m_bagsPerLevelAchievements.Clear();
      this.m_bagsAchievements.Clear();
      this.m_starsAchievements.Clear();
      this.m_dyingAchievements.Clear();
      this.m_badLandingAchievements.Clear();
      this.m_advancedMoveAchievements.Clear();
      this.m_runDistanceAchievements.Clear();
      this.m_wallRunDistanceAchievements.Clear();
      this.m_climbDistanceAchievements.Clear();
      this.m_ziplineDistanceAchievements.Clear();
      this.m_slideDistanceAchievements.Clear();
      this.m_balanceDistanceAchievements.Clear();
      this.m_fallDistanceAchievements.Clear();
      this.m_defeatEnemyAchievements.Clear();
      this.m_disarmEnemyAchievements.Clear();
      this.m_defeatRivalAchievements.Clear();
      this.m_fallEnemyAchievements.Clear();
      this.m_singleAttackAchievements.Clear();
      this.m_attacksInTimeAchievements.Clear();
      this.m_sprintDurationAchievements.Clear();
      this.m_boundingBoxAchievements.Clear();
      this.m_allAchievements = (Achievement[]) null;
      this.m_levelData = (AchievementMetrics) null;
      this.m_overallData = (AchievementMetrics) null;
    }

    public void loadData()
    {
      if (this.m_allAchievements != null)
        return;
      AchievementData.m_totalGamePoints = 0;
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_ACHIEVEMENT_DATA_BIN")));
      int num1 = (int) dataInputStream.readShort();
      this.m_allAchievements = new Achievement[20];
      int idx = 0;
      for (int index = 0; index != num1; ++index)
      {
        int num2 = (int) dataInputStream.readByte();
        int name = (int) AchievementData.STRING_ARRAY[(int) dataInputStream.readShort()];
        int description = (int) AchievementData.STRING_ARRAY[(int) dataInputStream.readShort()];
        int num3 = (int) dataInputStream.readShort();
        int num4 = (int) dataInputStream.readShort();
        if (AchievementData.AVAILABLE_ACHIEVEMENTS[index])
        {
          Achievement achievement1;
          switch (num2)
          {
            case 0:
              LevelAchievement levelAchievement1 = new LevelAchievement(idx, name, description, num3);
              this.m_levelAchievements.Add(levelAchievement1);
              achievement1 = (Achievement) levelAchievement1;
              break;
            case 1:
              TimedLevelAchievement levelAchievement2 = new TimedLevelAchievement(idx, name, description, num3, num4);
              this.m_timedLevelAchievements.Add(levelAchievement2);
              achievement1 = (Achievement) levelAchievement2;
              break;
            case 2:
              BagsPerLevelAchievement levelAchievement3 = new BagsPerLevelAchievement(idx, name, description, num3, num4);
              this.m_bagsPerLevelAchievements.Add(levelAchievement3);
              achievement1 = (Achievement) levelAchievement3;
              break;
            case 3:
              CountAchievement countAchievement1 = new CountAchievement(idx, name, description, num3);
              this.m_bagsAchievements.Add(countAchievement1);
              achievement1 = (Achievement) countAchievement1;
              break;
            case 4:
              CountAchievement countAchievement2 = new CountAchievement(idx, name, description, num3);
              this.m_starsAchievements.Add(countAchievement2);
              achievement1 = (Achievement) countAchievement2;
              break;
            case 5:
              CountAchievement countAchievement3 = new CountAchievement(idx, name, description, num3);
              this.m_dyingAchievements.Add(countAchievement3);
              achievement1 = (Achievement) countAchievement3;
              break;
            case 6:
              CountAchievement countAchievement4 = new CountAchievement(idx, name, description, num3);
              this.m_badLandingAchievements.Add(countAchievement4);
              achievement1 = (Achievement) countAchievement4;
              break;
            case 7:
              CountAchievement countAchievement5 = new CountAchievement(idx, name, description, num3);
              this.m_advancedMoveAchievements.Add(countAchievement5);
              achievement1 = (Achievement) countAchievement5;
              break;
            case 8:
              CountAchievement countAchievement6 = new CountAchievement(idx, name, description, num3);
              this.m_runDistanceAchievements.Add(countAchievement6);
              achievement1 = (Achievement) countAchievement6;
              break;
            case 9:
              CountAchievement countAchievement7 = new CountAchievement(idx, name, description, num3);
              this.m_wallRunDistanceAchievements.Add(countAchievement7);
              achievement1 = (Achievement) countAchievement7;
              break;
            case 10:
              CountAchievement countAchievement8 = new CountAchievement(idx, name, description, num3);
              this.m_climbDistanceAchievements.Add(countAchievement8);
              achievement1 = (Achievement) countAchievement8;
              break;
            case 11:
              CountAchievement countAchievement9 = new CountAchievement(idx, name, description, num3);
              this.m_ziplineDistanceAchievements.Add(countAchievement9);
              achievement1 = (Achievement) countAchievement9;
              break;
            case 12:
              CountAchievement countAchievement10 = new CountAchievement(idx, name, description, num3);
              this.m_slideDistanceAchievements.Add(countAchievement10);
              achievement1 = (Achievement) countAchievement10;
              break;
            case 13:
              CountAchievement countAchievement11 = new CountAchievement(idx, name, description, num3);
              this.m_balanceDistanceAchievements.Add(countAchievement11);
              achievement1 = (Achievement) countAchievement11;
              break;
            case 14:
              CountAchievement countAchievement12 = new CountAchievement(idx, name, description, num3);
              this.m_fallDistanceAchievements.Add(countAchievement12);
              achievement1 = (Achievement) countAchievement12;
              break;
            case 15:
              TimedAchievement timedAchievement = new TimedAchievement(idx, name, description, num3);
              this.m_sprintDurationAchievements.Add(timedAchievement);
              achievement1 = (Achievement) timedAchievement;
              break;
            case 16:
              CountAchievement countAchievement13 = new CountAchievement(idx, name, description, num3);
              this.m_defeatEnemyAchievements.Add(countAchievement13);
              achievement1 = (Achievement) countAchievement13;
              break;
            case 17:
              CountAchievement countAchievement14 = new CountAchievement(idx, name, description, num3);
              this.m_disarmEnemyAchievements.Add(countAchievement14);
              achievement1 = (Achievement) countAchievement14;
              break;
            case 18:
              CountAchievement countAchievement15 = new CountAchievement(idx, name, description, num3);
              this.m_fallEnemyAchievements.Add(countAchievement15);
              achievement1 = (Achievement) countAchievement15;
              break;
            case 19:
              EventsInPhaseAchievement phaseAchievement = new EventsInPhaseAchievement(idx, name, description, num3);
              this.m_singleAttackAchievements.Add(phaseAchievement);
              achievement1 = (Achievement) phaseAchievement;
              break;
            case 20:
              TimedEventsAchievement eventsAchievement = new TimedEventsAchievement(idx, name, description, num3, num4);
              this.m_attacksInTimeAchievements.Add(eventsAchievement);
              achievement1 = (Achievement) eventsAchievement;
              break;
            case 21:
              CountAchievement countAchievement16 = new CountAchievement(idx, name, description, num3);
              this.m_defeatRivalAchievements.Add(countAchievement16);
              achievement1 = (Achievement) countAchievement16;
              break;
            case 22:
              achievement1 = (Achievement) new EventsInPhaseAchievement(idx, name, description, 1);
              break;
            case 23:
              Achievement achievement2 = new Achievement(idx, name, description);
              this.m_boundingBoxAchievements.Add(achievement2);
              achievement1 = achievement2;
              break;
            default:
              Achievement achievement3 = new Achievement(idx, name, description);
              this.m_uniqueAchievements.Add(achievement3);
              achievement1 = achievement3;
              break;
          }
          achievement1.m_CompletedDescription = (int) AchievementData.COMPLETED_STRING_ARRAY[idx];
          achievement1.m_GamePoints = AchievementData.GAME_POINTS_ARRAY[idx];
          achievement1.iconLocked = Image.createImage("res/" + AchievementData.ICONS_FILE_NAME_MASK[idx] + "_lock");
          achievement1.iconOpened = Image.createImage("res/" + AchievementData.ICONS_FILE_NAME_MASK[idx] + "_open");
          achievement1.m_ServerKey = AchievementData.ACHIEVEMENT_SERVER_KEYS[idx];
          AchievementData.m_totalGamePoints += achievement1.m_GamePoints;
          this.m_allAchievements[idx++] = achievement1;
        }
      }
      dataInputStream.close();
      this.loadRmsData();
      if (LiveProcessor.m_Achievements == null || LiveProcessor.m_Achievements.Count <= 0)
        return;
      List<string> stringList = new List<string>();
      foreach (Microsoft.Xna.Framework.GamerServices.Achievement achievement4 in LiveProcessor.m_Achievements)
      {
        Achievement achievement5 = this.FindAchievement(achievement4.Key);
        if (achievement5 != null)
        {
          AchievementData.m_totalGamePoints -= achievement5.m_GamePoints;
          achievement5.m_GamePoints = achievement4.GamerScore;
          AchievementData.m_totalGamePoints += achievement4.GamerScore;
          if (achievement4.IsEarned)
            achievement5.complete();
          else if (achievement5.isComplete())
            stringList.Add(achievement5.m_ServerKey);
        }
      }
      foreach (string achievementKey in stringList)
        LiveProcessor.AwardAchievement(achievementKey);
      this.saveRmsData();
    }

    public int getAchievementNum() => this.m_allAchievements.Length;

    public Achievement getAchievement(int index) => this.m_allAchievements[index];

    public Achievement FindAchievement(string serverKey)
    {
      for (int index = 0; index < this.m_allAchievements.Length; ++index)
      {
        if (this.m_allAchievements[index].m_ServerKey == serverKey)
          return this.m_allAchievements[index];
      }
      return (Achievement) null;
    }

    public int getBadgeCount()
    {
      int badgeCount = 0;
      for (int index = 0; index < this.m_allAchievements.Length; ++index)
      {
        if (this.m_allAchievements[index].isComplete())
          ++badgeCount;
      }
      return badgeCount;
    }

    public int getGamePointsEarned()
    {
      int gamePointsEarned = 0;
      for (int index = 0; index < this.m_allAchievements.Length; ++index)
      {
        if (this.m_allAchievements[index].isComplete())
          gamePointsEarned += this.m_allAchievements[index].m_GamePoints;
      }
      return gamePointsEarned;
    }

    public void update(int timeStep)
    {
      if (!this.m_inLevel)
        return;
      this.m_levelData.time += timeStep;
    }

    public void registerLevelStart(int levelNum)
    {
      this.m_currentLevel = levelNum;
      this.m_inLevel = true;
      this.m_levelData = new AchievementMetrics();
      foreach (TimedEventsAchievement inTimeAchievement in this.m_attacksInTimeAchievements)
        inTimeAchievement.levelStarted();
    }

    public void registerLevelCancel()
    {
      this.m_currentLevel = 0;
      this.m_inLevel = false;
    }

    public bool registerLevelComplete()
    {
      bool flag = false;
      LevelAchievement levelAchievement1 = this.getLevelAchievement(this.m_currentLevel);
      if (levelAchievement1 != null)
        flag = this.registerAchievementComplete(levelAchievement1.getIdx());
      if (AppEngine.getLevelData().getGameMode() == LevelData.GameMode.GAME_MODE_SPEEDRUN)
      {
        TimedLevelAchievement levelAchievement2 = this.getTimedLevelAchievement(this.m_currentLevel);
        if (levelAchievement2 != null && this.m_levelData.time < levelAchievement2.getTime())
          this.registerAchievementComplete(levelAchievement2.getIdx());
      }
      return flag;
    }

    public void registerLevelEnd()
    {
      this.m_overallData += this.m_levelData;
      this.saveRmsData();
      this.m_currentLevel = 0;
      this.m_inLevel = false;
    }

    public void registerGameEnd()
    {
      ++this.m_gameCompletes;
      this.registerAchievementComplete(3);
      foreach (CountAchievement dyingAchievement in this.m_dyingAchievements)
      {
        if (dyingAchievement.getCount() < this.m_overallData.deaths)
          this.registerAchievementComplete(dyingAchievement.getIdx());
      }
      foreach (CountAchievement landingAchievement in this.m_badLandingAchievements)
      {
        if (landingAchievement.getCount() < this.m_overallData.badLandings)
          this.registerAchievementComplete(landingAchievement.getIdx());
      }
    }

    public void registerDeath() => ++this.m_levelData.deaths;

    public void registerKill(int raceTimeSecs)
    {
      ++this.m_levelData.kills;
      int num = this.m_levelData.kills + this.m_overallData.kills;
      foreach (CountAchievement enemyAchievement in this.m_defeatEnemyAchievements)
      {
        if (enemyAchievement.getCount() <= num)
          this.registerAchievementComplete(enemyAchievement.getIdx());
      }
      foreach (EventsInPhaseAchievement attackAchievement in this.m_singleAttackAchievements)
        attackAchievement.eventHappended();
      foreach (TimedEventsAchievement inTimeAchievement in this.m_attacksInTimeAchievements)
        inTimeAchievement.eventHappended(raceTimeSecs);
      ((EventsInPhaseAchievement) this.m_allAchievements[17]).eventHappended();
      this.saveRmsData();
    }

    public void registerRivalKill(int raceTimeSecs)
    {
      this.registerKill(raceTimeSecs);
      ++this.m_levelData.rivalKills;
      int num = this.m_levelData.rivalKills + this.m_overallData.rivalKills;
      foreach (CountAchievement rivalAchievement in this.m_defeatRivalAchievements)
      {
        if (rivalAchievement.getCount() <= num)
          this.registerAchievementComplete(rivalAchievement.getIdx());
      }
      this.saveRmsData();
    }

    public void registerEnemyFall()
    {
      ++this.m_levelData.enemyFalls;
      int num = this.m_levelData.enemyFalls + this.m_overallData.enemyFalls;
      foreach (CountAchievement enemyAchievement in this.m_fallEnemyAchievements)
      {
        if (enemyAchievement.getCount() <= num)
          this.registerAchievementComplete(enemyAchievement.getIdx());
      }
      this.saveRmsData();
    }

    public void registerDisarm()
    {
      ++this.m_levelData.disarms;
      int num = this.m_levelData.disarms + this.m_overallData.disarms;
      foreach (CountAchievement enemyAchievement in this.m_disarmEnemyAchievements)
      {
        if (enemyAchievement.getCount() <= num)
          this.registerAchievementComplete(enemyAchievement.getIdx());
      }
      this.saveRmsData();
    }

    public void registerBag()
    {
      ++this.m_levelData.bags;
      foreach (BagsPerLevelAchievement levelAchievement in this.m_bagsPerLevelAchievements)
      {
        if (levelAchievement.getLevel() == this.m_currentLevel && levelAchievement.getBags() <= this.m_levelData.bags)
          this.registerAchievementComplete(levelAchievement.getIdx());
      }
      int num1 = 0;
      bool flag = true;
      LevelData levelData = AppEngine.getLevelData();
      int currentLevelIndex = levelData.getCurrentLevelIndex();
      int levelNum = levelData.getLevelNum();
      for (int levelIndex = 0; levelIndex != levelNum; ++levelIndex)
      {
        if (currentLevelIndex == levelIndex)
        {
          MEdgeMap map = AppEngine.getCanvas().getSceneGame().getMap();
          int collectablesCollected = map.getNumCollectablesCollected();
          int collectablesTotal = map.getNumCollectablesTotal();
          num1 += collectablesCollected;
          if (collectablesCollected != collectablesTotal)
            flag = false;
        }
        else
        {
          Level level = levelData.getLevel(levelIndex);
          if (!level.isLevelComplete())
          {
            flag = false;
          }
          else
          {
            int numBagsFound = level.getNumBagsFound();
            num1 += numBagsFound;
            if (numBagsFound != level.getNumTotalBags())
              flag = false;
          }
        }
      }
      int num2 = flag ? 1 : 0;
      foreach (CountAchievement bagsAchievement in this.m_bagsAchievements)
      {
        if (bagsAchievement.getCount() <= num1)
          this.registerAchievementComplete(bagsAchievement.getIdx());
      }
    }

    public void registerStars()
    {
      int numCollectedStars = AppEngine.getLevelData().calculateNumCollectedStars();
      foreach (CountAchievement starsAchievement in this.m_starsAchievements)
      {
        if (starsAchievement.getCount() <= numCollectedStars)
          this.registerAchievementComplete(starsAchievement.getIdx());
      }
    }

    public void registerAttackStart()
    {
      foreach (EventsInPhaseAchievement attackAchievement in this.m_singleAttackAchievements)
        attackAchievement.phaseOn();
    }

    public void registerAttackEnd()
    {
      foreach (EventsInPhaseAchievement attackAchievement in this.m_singleAttackAchievements)
        attackAchievement.phaseOff();
    }

    public void registerZiplineDrop()
    {
      ((EventsInPhaseAchievement) this.m_allAchievements[17]).phaseOn();
    }

    public void registerSprintOn(int raceTimeMillis)
    {
      int raceTimeSecs = raceTimeMillis >> 10;
      foreach (TimedAchievement durationAchievement in this.m_sprintDurationAchievements)
        durationAchievement.runTimer(raceTimeSecs);
    }

    public void registerSprintOff()
    {
      foreach (TimedAchievement durationAchievement in this.m_sprintDurationAchievements)
        durationAchievement.endTimer();
    }

    public void registerMapCollision()
    {
      ((EventsInPhaseAchievement) this.m_allAchievements[17]).phaseOff();
    }

    public void registerBadLanding() => ++this.m_levelData.badLandings;

    public void registerRunDist(float dist)
    {
      this.m_levelData.runDistance += dist;
      int num = (int) ((double) this.m_levelData.runDistance + (double) this.m_overallData.runDistance);
      foreach (CountAchievement distanceAchievement in this.m_runDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public void registerWallRunDist(float dist)
    {
      this.m_levelData.wallRunDistance += dist;
      int num = (int) ((double) this.m_levelData.wallRunDistance + (double) this.m_overallData.wallRunDistance);
      foreach (CountAchievement distanceAchievement in this.m_wallRunDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public void registerClimbDist(float dist)
    {
      this.m_levelData.climbDistance += dist;
      int num = (int) ((double) this.m_levelData.climbDistance + (double) this.m_overallData.climbDistance);
      foreach (CountAchievement distanceAchievement in this.m_climbDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public void registerZiplineDist(float dist)
    {
      this.m_levelData.zipLineDistance += dist;
      int num = (int) ((double) this.m_levelData.zipLineDistance + (double) this.m_overallData.zipLineDistance);
      foreach (CountAchievement distanceAchievement in this.m_ziplineDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public void registerBalanceDist(float dist)
    {
      this.m_levelData.balanceDistance += dist;
      int num = (int) ((double) this.m_levelData.balanceDistance + (double) this.m_overallData.balanceDistance);
      foreach (CountAchievement distanceAchievement in this.m_balanceDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public void registerSlideDist(float dist)
    {
      this.m_levelData.slideDistance += dist;
      int num = (int) ((double) this.m_levelData.slideDistance + (double) this.m_overallData.slideDistance);
      foreach (CountAchievement distanceAchievement in this.m_slideDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public void registerFallDist(float dist)
    {
      this.m_levelData.fallDistance += dist;
      int num = (int) ((double) this.m_levelData.fallDistance + (double) this.m_overallData.fallDistance);
      foreach (CountAchievement distanceAchievement in this.m_fallDistanceAchievements)
      {
        if (distanceAchievement.getCount() <= num)
          this.registerAchievementComplete(distanceAchievement.getIdx());
      }
    }

    public bool registerPopupBox(int stringId)
    {
      foreach (Achievement boundingBoxAchievement in this.m_boundingBoxAchievements)
      {
        if (boundingBoxAchievement.getNameId() == stringId)
        {
          this.registerAchievementComplete(boundingBoxAchievement.getIdx());
          return true;
        }
      }
      return false;
    }

    public bool registerAchievementComplete(int achievementId)
    {
      if (MirrorsEdge.TrialMode)
        return false;
      Achievement achievement = this.getAchievement(achievementId);
      if (achievement.isComplete())
        return false;
      achievement.complete();
      SpywareManager.getInstance().trackAchievedBadge(achievementId);
      AppEngine.getCanvas().notifyAchievementComplete(achievement);
      LiveProcessor.AwardAchievement(achievement.m_ServerKey);
      this.saveRmsData();
      return true;
    }

    private void loadRmsData()
    {
      InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream("achievements_data3" + (MirrorsEdge.TrialMode ? "_trial" : ""));
      if (resourceAsStream == null)
        return;
      DataInputStream dis = new DataInputStream(resourceAsStream);
      if (dis.available() < 4)
      {
        dis.close();
      }
      else
      {
        if (dis.readInt() == this.m_allAchievements.Length)
        {
          this.m_overallData.read(dis);
          if (dis.available() < this.m_allAchievements.Length)
          {
            dis.close();
            return;
          }
          for (int index = 0; index < this.m_allAchievements.Length; ++index)
          {
            if (dis.readBoolean())
              this.m_allAchievements[index].complete();
          }
        }
        resourceAsStream.close();
      }
    }

    private void saveRmsData()
    {
      if (AppEngine.getCanvas().storageFull())
        return;
      DataOutputStream dos = new DataOutputStream((OutputStream) OutputStream.getResourceAsStream("achievements_data3" + (MirrorsEdge.TrialMode ? "_trial" : "")));
      dos.writeInt(this.m_allAchievements.Length);
      this.m_overallData.write(dos);
      for (int index = 0; index < this.m_allAchievements.Length; ++index)
        dos.writeBoolean(this.m_allAchievements[index].isComplete());
      dos.close();
    }

    private LevelAchievement getLevelAchievement(int level)
    {
      foreach (LevelAchievement levelAchievement in this.m_levelAchievements)
      {
        if (levelAchievement.getLevel() == level)
          return levelAchievement;
      }
      return (LevelAchievement) null;
    }

    private TimedLevelAchievement getTimedLevelAchievement(int level)
    {
      foreach (TimedLevelAchievement levelAchievement in this.m_timedLevelAchievements)
      {
        if (levelAchievement.getLevel() == level)
          return levelAchievement;
      }
      return (TimedLevelAchievement) null;
    }
  }
}
