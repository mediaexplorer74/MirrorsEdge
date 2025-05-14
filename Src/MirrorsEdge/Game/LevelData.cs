// Decompiled with JetBrains decompiler
// Type: game.LevelData
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;
using mirrorsedge_wp7;
using support;
using System;
using System.Collections.Generic;

#nullable disable
namespace game
{
  public class LevelData
  {
    public const int LEVEL_TYPE_TUTORIAL = 0;
    public const int LEVEL_TYPE_STORY = 1;
    public const int MODEL_SET_ELEMENT_MAP_TEXTURE = 0;
    public const int MODEL_SET_ELEMENT_FAITH = 1;
    public const int MODEL_SET_ELEMENT_NUM = 2;
    public const int MODEL_SET_USERID_ELEMENT_REFLECTION_MAP = 0;
    public const int MODEL_SET_USERID_ELEMENT_CHECKPOINT_ROTATE = 1;
    public const int MODEL_SET_USERID_ELEMENT_NUM = 2;
    public const int LEVEL_TUTORIAL = 0;
    public const int LEVEL_TUTORIAL_ADV = 1;
    public const int LEVEL_CHAPTER_01 = 2;
    public const int LEVEL_CHAPTER_01_TRIAL = 1;
    public const int LEVEL_CHAPTER_01_02 = 3;
    public const int LEVEL_CHAPTER_02 = 4;
    public const int LEVEL_CHAPTER_02_02 = 5;
    public const int LEVEL_CHAPTER_03 = 6;
    public const int LEVEL_CHAPTER_03_02 = 7;
    public const int LEVEL_CHAPTER_04 = 8;
    public const int LEVEL_CHAPTER_04_02 = 9;
    public const int LEVEL_CHAPTER_05 = 10;
    public const int LEVEL_CHAPTER_05_02 = 11;
    public const int LEVEL_CHAPTER_06 = 12;
    public const int LEVEL_CHAPTER_06_02 = 13;
    public const int MODEL_SET_EXTERNAL_MIDDAY = 0;
    public const int MODEL_SET_EXTERNAL_DUSK = 1;
    public const int MODEL_SET_EXTERNAL_NIGHT = 2;
    public const int MODEL_SET_INTERNAL_GEN = 3;
    public const int MODEL_SET_INTERNAL_JAIL = 4;
    public const int MODEL_SET_INTERNAL_UNDERGROUND = 5;
    public const int RMS_VERSION = 2;
    private const string LEVEL_RMS_FILENAME = "LevelProgress5";
    private const string PROGRESSION_CHUNK_TYPE = "PROG";
    private const string LEVEL_CHUNK_TYPE = "LEVL";
    public static short[] STRING_ARRAY;
    private static readonly short[] STRING_ARRAY_FULL = new short[39]
    {
      (short) 2048,
      (short) 2097,
      (short) 2098,
      (short) 2228,
      (short) 2240,
      (short) 2099,
      (short) 2229,
      (short) 2241,
      (short) 2100,
      (short) 2230,
      (short) 2242,
      (short) 2101,
      (short) 2231,
      (short) 2243,
      (short) 2102,
      (short) 2232,
      (short) 2244,
      (short) 2103,
      (short) 2233,
      (short) 2245,
      (short) 2104,
      (short) 2234,
      (short) 2246,
      (short) 2105,
      (short) 2235,
      (short) 2247,
      (short) 2106,
      (short) 2236,
      (short) 2248,
      (short) 2107,
      (short) 2237,
      (short) 2249,
      (short) 2108,
      (short) 2238,
      (short) 2250,
      (short) 2109,
      (short) 2239,
      (short) 2251,
      (short) 2110
    };
    private static readonly short[] STRING_ARRAY_TRIAL = new short[5]
    {
      (short) 2048,
      (short) 2097,
      (short) 2228,
      (short) 2240,
      (short) 2099
    };
    public static int[] RESOURCE_ARRAY;
    public static readonly int[] RESOURCE_ARRAY_FULL = new int[50]
    {
      202,
      88,
      87,
      89,
      -1,
      6,
      135,
      0,
      166,
      7,
      1,
      167,
      4,
      2,
      142,
      20,
      136,
      3,
      143,
      31,
      137,
      4,
      144,
      9,
      139,
      5,
      145,
      6,
      146,
      27,
      138,
      7,
      147,
      33,
      8,
      148,
      1,
      9,
      149,
      32,
      10,
      150,
      11,
      151,
      24,
      12,
      152,
      0,
      13,
      153
    };
    private static readonly int[] RESOURCE_ARRAY_TRIAL = new int[9]
    {
      202,
      33,
      2,
      70,
      0,
      80,
      1,
      1,
      67
    };
    public static short[] MODEL_ARRAY;
    private static readonly short[] MODEL_ARRAY_FULL = new short[12]
    {
      (short) 11,
      (short) 7,
      (short) 12,
      (short) 8,
      (short) 13,
      (short) 9,
      (short) 14,
      (short) 10,
      (short) 15,
      (short) 11,
      (short) 16,
      (short) 12
    };
    private static readonly short[] MODEL_ARRAY_TRIAL = new short[2]
    {
      (short) 6,
      (short) 7
    };
    public static short[] RUNNER_ANIM_ARRAY;
    private static readonly short[] RUNNER_ANIM_ARRAY_FULL = new short[4]
    {
      (short) 39,
      (short) 40,
      (short) 41,
      (short) 42
    };
    private static readonly short[] RUNNER_ANIM_ARRAY_TRIAL = new short[1]
    {
      (short) 36
    };
    public static short[] QUAD_ARRAY;
    private static readonly short[] QUAD_ARRAY_FULL = new short[14]
    {
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 18
    };
    private static readonly short[] QUAD_ARRAY_TRIAL = new short[2]
    {
      (short) 5,
      (short) 6
    };
    private ModelSet[] m_modelSetArray;
    private Level[] m_levelArray;
    private int m_numUnlockedLevels;
    private LevelData.GameMode m_gameMode;
    private int m_currentLevelIndex;
    private bool m_gameCompleteFirstTime;

    public static void SetResources()
    {
      if (MirrorsEdge.TrialMode)
      {
        LevelData.STRING_ARRAY = LevelData.STRING_ARRAY_TRIAL;
        LevelData.RESOURCE_ARRAY = LevelData.RESOURCE_ARRAY_TRIAL;
        LevelData.MODEL_ARRAY = LevelData.MODEL_ARRAY_TRIAL;
        LevelData.RUNNER_ANIM_ARRAY = LevelData.RUNNER_ANIM_ARRAY_TRIAL;
        LevelData.QUAD_ARRAY = LevelData.QUAD_ARRAY_TRIAL;
      }
      else
      {
        LevelData.STRING_ARRAY = LevelData.STRING_ARRAY_FULL;
        LevelData.RESOURCE_ARRAY = LevelData.RESOURCE_ARRAY_FULL;
        LevelData.MODEL_ARRAY = LevelData.MODEL_ARRAY_FULL;
        LevelData.RUNNER_ANIM_ARRAY = LevelData.RUNNER_ANIM_ARRAY_FULL;
        LevelData.QUAD_ARRAY = LevelData.QUAD_ARRAY_FULL;
      }
    }

    public int getNumUnlockedLevels() => this.m_numUnlockedLevels;

    public LevelData.GameMode getGameMode() => this.m_gameMode;

    public LevelData()
    {
      this.m_levelArray = (Level[]) null;
      this.m_modelSetArray = (ModelSet[]) null;
      this.m_numUnlockedLevels = 0;
      this.m_gameMode = LevelData.GameMode.GAME_MODE_STORY;
      this.m_currentLevelIndex = 0;
      this.m_gameCompleteFirstTime = false;
    }

    public void Destructor()
    {
      if (this.m_levelArray != null)
      {
        int length = this.m_levelArray.Length;
        for (int index = 0; index != length; ++index)
        {
          this.m_levelArray[index].Destructor();
          this.m_levelArray[index] = (Level) null;
        }
        this.m_levelArray = (Level[]) null;
      }
      if (this.m_modelSetArray == null)
        return;
      int length1 = this.m_modelSetArray.Length;
      for (int index = 0; index < length1; ++index)
      {
        this.m_modelSetArray[index].Destructor();
        this.m_modelSetArray[index] = (ModelSet) null;
      }
      this.m_modelSetArray = (ModelSet[]) null;
    }

    public void loadData()
    {
      if (this.m_levelArray != null)
        return;
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_LEVEL_DATA_BIN")));
      StringBuffer stringBuffer = AppEngine.getCanvas().getTextManager().clearStringBuffer();
      int length1 = (int) dataInputStream.readShort();
      this.m_levelArray = new Level[length1];
      for (int index1 = 0; index1 != length1; ++index1)
      {
        this.m_levelArray[index1] = new Level();
        Level level = this.m_levelArray[index1];
        stringBuffer.setLength(0);
        int num1 = (int) dataInputStream.readByte();
        for (int index2 = 0; index2 != num1; ++index2)
          stringBuffer.append(dataInputStream.readByte());
        level.m_uniqueName = stringBuffer.toString();
        level.m_type = (int) dataInputStream.readByte();
        level.m_nameStringId = (int) LevelData.STRING_ARRAY[(int) dataInputStream.readShort()];
        level.m_objectiveStringId = (int) LevelData.STRING_ARRAY[(int) dataInputStream.readShort()];
        level.m_dateTimeStringId = (int) LevelData.STRING_ARRAY[(int) dataInputStream.readShort()];
        level.m_mapResId = LevelData.RESOURCE_ARRAY[(int) dataInputStream.readShort()];
        for (int index3 = 0; index3 != 3; ++index3)
        {
          int num2 = (int) dataInputStream.readByte() * 60 + (int) dataInputStream.readByte();
          level.m_speedRunRequirementMillis[index3] = num2 * 1000;
        }
        level.m_loadingScreen = LevelData.RESOURCE_ARRAY[(int) dataInputStream.readShort()];
        level.m_startMusicId = LevelData.RESOURCE_ARRAY[(int) dataInputStream.readShort()];
        level.m_introFaithAnim = (int) LevelData.RUNNER_ANIM_ARRAY[(int) dataInputStream.readShort()];
        level.m_introCamAnim = LevelData.RESOURCE_ARRAY[(int) dataInputStream.readShort()];
        level.m_completionBackground = (int) LevelData.QUAD_ARRAY[(int) dataInputStream.readShort()];
      }
      int length2 = (int) dataInputStream.readShort();
      this.m_modelSetArray = new ModelSet[length2];
      for (int index4 = 0; index4 != length2; ++index4)
      {
        this.m_modelSetArray[index4] = new ModelSet();
        ModelSet modelSet = this.m_modelSetArray[index4];
        for (int index5 = 0; index5 != 2; ++index5)
          modelSet.m_modelArray[index5] = (int) LevelData.MODEL_ARRAY[(int) dataInputStream.readShort()];
        for (int index6 = 0; index6 != 2; ++index6)
          modelSet.m_userIdArray[index6] = LevelData.RESOURCE_ARRAY[(int) dataInputStream.readShort()];
        modelSet.m_rotateY = (float) dataInputStream.readInt() * 1.52587891E-05f;
        modelSet.m_rotateZ = (float) dataInputStream.readInt() * 1.52587891E-05f;
        modelSet.m_backgroundFillColor = (uint) dataInputStream.readInt();
      }
      dataInputStream.close();
      this.loadRmsData();
    }

    private void resetRmsData()
    {
      this.m_numUnlockedLevels = 0;
      for (int index = 0; index != this.m_levelArray.Length; ++index)
        this.m_levelArray[index].resetRms(false);
    }

    private void loadRmsData()
    {
      this.resetRmsData();
      InputStream resourceAsStream = (InputStream) WP7InputStreamIsolatedStorage.getResourceAsStream("LevelProgress5" + (MirrorsEdge.TrialMode ? "_trial" : ""));
      if (resourceAsStream == null)
        return;
      IFFReader iffReader = new IFFReader(new DataInputStream(resourceAsStream));
      while (!iffReader.isReadComplete())
      {
        if (iffReader.isIdOfCurrectChunk("VERS"))
        {
          InputStream @in = iffReader.readChunk();
          if (new DataInputStream(@in).readInt() != 2)
          {
            @in.close();
            return;
          }
        }
        else if (iffReader.isIdOfCurrectChunk("PROG"))
        {
          InputStream @in = iffReader.readChunk();
          DataInputStream dataInputStream = new DataInputStream(@in);
          if (2 <= @in.available())
          {
            int num = (int) dataInputStream.readShort();
            if (0 <= num && num <= this.m_levelArray.Length)
              this.m_numUnlockedLevels = num;
          }
        }
        else if (iffReader.isIdOfCurrectChunk("LEVL"))
        {
          InputStream @in = iffReader.readChunk();
          if (2 <= @in.available())
          {
            DataInputStream dis = new DataInputStream(@in);
            int index = (int) dis.readShort();
            if (0 <= index && index < this.m_levelArray.Length && !this.m_levelArray[index].readRms(dis))
              this.m_levelArray[index].resetRms(true);
          }
        }
        else
          iffReader.skipChunk();
      }
      resourceAsStream.close();
    }

    private void saveRmsData()
    {
      if (AppEngine.getCanvas().storageFull())
        return;
      IFFWriter iffWriter = new IFFWriter(new DataOutputStream((OutputStream) OutputStream.getResourceAsStream("LevelProgress5" + (MirrorsEdge.TrialMode ? "_trial" : ""))));
      iffWriter.writeChunk("VERS").writeInt(2);
      iffWriter.writeChunk("PROG").writeShort((short) this.m_numUnlockedLevels);
      for (int t = 0; t != this.m_levelArray.Length; ++t)
      {
        DataOutputStream dos = iffWriter.writeChunk("LEVL");
        dos.writeShort((short) t);
        this.m_levelArray[t].writeRms(dos);
      }
      iffWriter.Destructor();
    }

    public void resetLevelData()
    {
      this.m_numUnlockedLevels = 0;
      for (int index = 0; index != this.m_levelArray.Length; ++index)
        this.m_levelArray[index].levelReset();
      this.saveRmsData();
    }

    public ModelSet getModelSet(int modelSetIndex) => this.m_modelSetArray[modelSetIndex];

    public Level getLevel(int levelIndex)
    {
      if (levelIndex > 0)
        this.m_levelArray[levelIndex].getNumTotalBags();
      return this.m_levelArray[levelIndex];
    }

    public int getLevelNum() => this.m_levelArray.Length;

    public int getLevelIndexByStringId(int stringId)
    {
      for (int levelIndexByStringId = 0; levelIndexByStringId != this.m_levelArray.Length; ++levelIndexByStringId)
      {
        if (this.m_levelArray[levelIndexByStringId].getName() == stringId)
          return levelIndexByStringId;
      }
      return -1;
    }

    public Level getLevelByStringId(int stringId)
    {
      for (int index = 0; index != this.m_levelArray.Length; ++index)
      {
        Level level = this.m_levelArray[index];
        if (level.getName() == stringId)
          return level;
      }
      return (Level) null;
    }

    public int getCurrentLevelIndex() => this.m_currentLevelIndex;

    public Level getCurrentLevelObject() => this.m_levelArray[this.m_currentLevelIndex];

    public void setCurrentLevelByIndex(LevelData.GameMode gameMode, int levelIndex)
    {
      this.m_gameMode = gameMode;
      this.m_currentLevelIndex = levelIndex;
    }

    public void setCurrentLevelByName(LevelData.GameMode gameMode, int levelNameStringId)
    {
      this.m_gameMode = gameMode;
      this.m_currentLevelIndex = this.getLevelIndexByStringId(levelNameStringId);
    }

    public bool isCurrentLevelLast() => this.m_currentLevelIndex == this.m_levelArray.Length - 1;

    public void nextLevel() => ++this.m_currentLevelIndex;

    public void JumpToLastLevel() => this.m_currentLevelIndex = this.m_levelArray.Length - 1;

    public bool isLevelComplete()
    {
      for (int index = 0; index != this.m_levelArray.Length; ++index)
      {
        if (this.m_levelArray[index].isLevelComplete())
          return true;
      }
      return false;
    }

    public bool setLevelComplete(int completeTimeMillis, List<GameObject> bagFoundList)
    {
      bool flag = false;
      Level level = this.m_levelArray[this.m_currentLevelIndex];
      if (this.m_currentLevelIndex == this.m_levelArray.Length - 1 && !level.m_levelComplete)
        this.m_gameCompleteFirstTime = true;
      if (MirrorsEdge.TrialMode)
        level.m_levelComplete = true;
      switch (this.m_gameMode)
      {
        case LevelData.GameMode.GAME_MODE_STORY:
          int count = bagFoundList.Count;
          if (level.m_collectableFoundArray.Length == count)
          {
            for (int index = 0; index != count; ++index)
              level.m_collectableFoundArray[index] = level.m_collectableFoundArray[index] || bagFoundList[index] == null;
          }
          else
          {
            level.m_collectableFoundArray = new bool[count];
            for (int index = 0; index != count; ++index)
              level.m_collectableFoundArray[index] = bagFoundList[index] == null;
          }
          if (!MirrorsEdge.TrialMode)
            level.m_levelComplete = true;
          this.m_numUnlockedLevels = Math.Max(this.m_numUnlockedLevels, this.m_currentLevelIndex + 1);
          break;
        case LevelData.GameMode.GAME_MODE_SPEEDRUN:
        case LevelData.GameMode.GAME_MODE_CHALLENGE:
          if ((level.m_bestSpeedRunTimeMillis == -1 || completeTimeMillis < level.m_bestSpeedRunTimeMillis) && completeTimeMillis >= level.getSpeedRunRequirementMillis(3) / 2)
          {
            level.m_bestSpeedRunTimeMillis = completeTimeMillis;
            flag = true;
            if (!MirrorsEdge.TrialMode)
            {
              AppEngine.getAchievementData().registerStars();
              break;
            }
            break;
          }
          break;
      }
      this.saveRmsData();
      return flag;
    }

    public bool isGameNewlyCompleted()
    {
      bool completeFirstTime = this.m_gameCompleteFirstTime;
      this.m_gameCompleteFirstTime = false;
      return completeFirstTime;
    }

    public void unlockAllLevels() => this.m_numUnlockedLevels = this.m_levelArray.Length;

    public int calculateNumCollectedStars()
    {
      int numCollectedStars = 0;
      for (int index = 0; index != this.m_levelArray.Length; ++index)
        numCollectedStars += this.m_levelArray[index].getNumStarsAchieved();
      return numCollectedStars;
    }

    public enum GameMode
    {
      GAME_MODE_STORY,
      GAME_MODE_SPEEDRUN,
      GAME_MODE_CHALLENGE,
    }
  }
}
