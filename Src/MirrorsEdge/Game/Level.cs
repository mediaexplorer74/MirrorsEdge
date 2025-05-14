// Decompiled with JetBrains decompiler
// Type: game.Level
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using System;

#nullable disable
namespace game
{
  public class Level
  {
    public const int SPEED_RUN_NUM_STARS = 3;
    public const int SPEEDRUN_INCOMPLETE_TIME = -1;
    public string m_uniqueName;
    public int m_type;
    public int m_nameStringId;
    public int m_objectiveStringId;
    public int m_dateTimeStringId;
    public int m_mapResId;
    public int[] m_speedRunRequirementMillis = new int[3];
    public int m_loadingScreen;
    public int m_startMusicId;
    public int m_introFaithAnim;
    public int m_introCamAnim;
    public int m_completionBackground;
    public bool m_levelComplete;
    public int m_bestSpeedRunTimeMillis;
    public bool[] m_collectableFoundArray;

    public string getUniqueName() => this.m_uniqueName;

    public int getType() => this.m_type;

    public int getName() => this.m_nameStringId;

    public int getObjective() => this.m_objectiveStringId;

    public int getDateTime() => this.m_dateTimeStringId;

    public int getMapResId() => this.m_mapResId;

    public int getLoadingScreen() => this.m_loadingScreen;

    public int getStartMusicId() => this.m_startMusicId;

    public int getIntroFaithAnim() => this.m_introFaithAnim;

    public int getIntroCamAnim() => this.m_introCamAnim;

    public int getCompletionBackground() => this.m_completionBackground;

    public int getSpeedRunRequirementMillis(int starIndex)
    {
      return this.m_speedRunRequirementMillis[starIndex - 1];
    }

    public bool isLevelComplete() => this.m_levelComplete;

    public int getBestSpeedRunTimeMillis() => this.m_bestSpeedRunTimeMillis;

    public Level()
    {
      this.m_uniqueName = (string) null;
      this.m_type = 0;
      this.m_nameStringId = 2048;
      this.m_objectiveStringId = 2048;
      this.m_dateTimeStringId = 2048;
      this.m_mapResId = -1;
      this.m_levelComplete = false;
      this.m_bestSpeedRunTimeMillis = -1;
      this.m_collectableFoundArray = new bool[0];
      this.m_loadingScreen = 0;
      this.m_startMusicId = 0;
      this.m_introFaithAnim = (int) GameObjectRunner.get("VISUAL_SOLO_IDLE");
      this.m_introCamAnim = -1;
      this.m_completionBackground = -1;
    }

    public Level(Level other)
    {
      this.m_uniqueName = other.m_uniqueName;
      this.m_type = other.m_type;
      this.m_nameStringId = other.m_nameStringId;
      this.m_objectiveStringId = other.m_objectiveStringId;
      this.m_dateTimeStringId = other.m_dateTimeStringId;
      this.m_mapResId = other.m_mapResId;
      this.m_levelComplete = other.m_levelComplete;
      this.m_bestSpeedRunTimeMillis = other.m_bestSpeedRunTimeMillis;
      this.m_collectableFoundArray = other.m_collectableFoundArray;
      this.m_loadingScreen = other.m_loadingScreen;
      this.m_startMusicId = other.m_startMusicId;
      this.m_introFaithAnim = other.m_introFaithAnim;
      this.m_introCamAnim = other.m_introCamAnim;
      this.m_completionBackground = -1;
    }

    public void Destructor()
    {
      this.m_collectableFoundArray = (bool[]) null;
      this.m_uniqueName = (string) null;
    }

    public int getNumBagsFound()
    {
      int numBagsFound = 0;
      int length = this.m_collectableFoundArray.Length;
      for (int index = 0; index != length; ++index)
      {
        if (this.m_collectableFoundArray[index])
          ++numBagsFound;
      }
      return numBagsFound;
    }

    public int getNumTotalBags() => this.m_collectableFoundArray.Length;

    public int getNumStarsAchieved() => this.getMinStarsWithTime(this.getBestSpeedRunTimeMillis());

    public int getMinStarsWithTime(int raceTimeMillis)
    {
      if (raceTimeMillis != -1)
      {
        for (int minStarsWithTime = 3; minStarsWithTime != 0; --minStarsWithTime)
        {
          if (raceTimeMillis <= this.m_speedRunRequirementMillis[minStarsWithTime - 1])
            return minStarsWithTime;
        }
      }
      return 0;
    }

    public bool isBagFound(int index)
    {
      return 0 <= index && index < this.m_collectableFoundArray.Length && this.m_collectableFoundArray[index];
    }

    public void resetRms(bool fullReset)
    {
      if (this.m_collectableFoundArray != null)
        Array.Clear((Array) this.m_collectableFoundArray, 0, this.m_collectableFoundArray.Length);
      this.m_bestSpeedRunTimeMillis = -1;
      if (!fullReset)
        return;
      this.m_levelComplete = false;
    }

    public bool readRms(DataInputStream dis)
    {
      if (dis.available() < 6)
        return false;
      this.m_levelComplete = dis.readBoolean();
      this.m_bestSpeedRunTimeMillis = dis.readInt();
      int length = (int) dis.readByte();
      if (length < 0 || 20 <= length || dis.available() < length)
        return false;
      this.m_collectableFoundArray = new bool[length];
      for (int index = 0; index != length; ++index)
        this.m_collectableFoundArray[index] = dis.readBoolean();
      return true;
    }

    public void writeRms(DataOutputStream dos)
    {
      dos.writeBoolean(this.m_levelComplete);
      dos.writeInt(this.m_bestSpeedRunTimeMillis);
      int length = this.m_collectableFoundArray.Length;
      dos.writeByte((byte) length);
      for (int index = 0; index != length; ++index)
        dos.writeBoolean(this.m_collectableFoundArray[index]);
    }

    public void levelReset()
    {
      if (this.m_collectableFoundArray == null)
        return;
      Array.Clear((Array) this.m_collectableFoundArray, 0, this.m_collectableFoundArray.Length);
    }
  }
}
