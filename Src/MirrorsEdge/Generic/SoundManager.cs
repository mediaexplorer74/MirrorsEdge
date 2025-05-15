
// Type: generic.SoundManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using support;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace generic
{
  public class SoundManager
  {
    private byte[] d_groupFlags;
    private short[] d_eventResources;
    private byte[] d_eventGroups;
    private byte[] d_eventInstances;
    private byte[] d_eventFlags;
    private float[] d_eventRanges;
    private float m_globalVolume;
    private float m_musicVolume;
    private float m_sfxVolume;
    private Player[][] m_players;
    private float[][] m_volumes;
    private float[] m_groupVolumes;
    public static float MAX_VOLUME = 1f;
    public static float MIN_VOLUME = 0.0f;

    private Player loadPlayer(int eventID)
    {
      return (Player) new PlayerWP7CoreAudio(this.getEventName(eventID));
    }

    public string getEventName(int eventID)
    {
      return ResourceManager.ID_TO_FILENAME((int) this.d_eventResources[eventID]);
    }

    private int calcEventVolume(int eventID, int instance)
    {
      float num = this.m_globalVolume * this.m_volumes[eventID][instance];
      int dEventGroup = (int) this.d_eventGroups[eventID];
      int dGroupFlag = (int) this.d_groupFlags[dEventGroup];
      if ((dGroupFlag & 2) != 0)
        num *= this.m_sfxVolume;
      if ((dGroupFlag & 1) != 0)
        num *= this.m_musicVolume;
      return (int) ((double) (num * this.m_groupVolumes[dEventGroup]) * 100.0);
    }

    private void applyVolume(int eventID, int instance)
    {
      Player player = this.m_players[eventID][instance];
      if (player == null)
        return;
      VolumeControl volumeControl = player.getVolumeControl();
      if (volumeControl == null)
        return;
      int volume = this.calcEventVolume(eventID, instance);
      volumeControl.setVolume(volume);
    }

    private int getHandle(int eventID, int instance) => eventID | instance << 16;

    private int getHandleEventId(int handle) => handle & (int) ushort.MaxValue;

    private int getHandleInstanceId(int handle) => handle >> 16 & (int) ushort.MaxValue;

    public int getEventGroup(int eventID) => 0;

    public SoundManager()
    {
      this.d_groupFlags = (byte[]) null;
      this.d_eventResources = (short[]) null;
      this.d_eventGroups = (byte[]) null;
      this.d_eventInstances = (byte[]) null;
      this.d_eventFlags = (byte[]) null;
      this.d_eventRanges = (float[]) null;
      this.m_globalVolume = SoundManager.MAX_VOLUME;
      this.m_musicVolume = SoundManager.MAX_VOLUME;
      this.m_sfxVolume = SoundManager.MAX_VOLUME;
      this.m_players = (Player[][]) null;
      this.m_volumes = (float[][]) null;
      this.m_groupVolumes = (float[]) null;
    }

    public virtual void Destructor()
    {
      int length = this.d_eventResources.Length;
      for (int eventID = 0; eventID < length; ++eventID)
        this.unloadEvent(eventID);
    }

    public void loadData()
    {
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_SOUNDEVENTS_BIN")));
      int length1 = (int) dataInputStream.readByte();
      byte[] numArray1 = new byte[length1];
      for (int index = 0; index < length1; ++index)
        numArray1[index] = (byte) dataInputStream.readByte();
      int length2 = (int) dataInputStream.readShort();
      short[] numArray2 = new short[length2];
      byte[] numArray3 = new byte[length2];
      byte[] numArray4 = new byte[length2];
      byte[] numArray5 = new byte[length2];
      float[] numArray6 = new float[length2];
      for (int index = 0; index < length2; ++index)
      {
        numArray2[index] = (short) ResourceManager.SOUND_DATA_SETS[(int) dataInputStream.readShort()];
        numArray3[index] = (byte) dataInputStream.readByte();
        numArray4[index] = (byte) dataInputStream.readByte();
        numArray5[index] = (byte) dataInputStream.readByte();
        numArray6[index] = dataInputStream.readFloat();
      }
      this.d_groupFlags = numArray1;
      this.d_eventResources = numArray2;
      this.d_eventGroups = numArray3;
      this.d_eventInstances = numArray4;
      this.d_eventFlags = numArray5;
      this.d_eventRanges = numArray6;
      this.m_players = new Player[length2][];
      this.m_volumes = new float[length2][];
      this.m_groupVolumes = new float[length1];
      for (int index = 0; index < length1; ++index)
        this.m_groupVolumes[index] = SoundManager.MAX_VOLUME;
    }

    public void loadEvent(int eventID)
    {
      int dEventInstance = (int) this.d_eventInstances[eventID];
      if (this.m_players[eventID] != null && this.m_players[eventID].Length == dEventInstance)
      {
        Player[] player1 = this.m_players[eventID];
        bool flag = true;
        for (int index = 0; index < dEventInstance; ++index)
        {
          Player player2 = player1[index];
          if (player2 == null || player2.getState() == 0)
          {
            flag = false;
            break;
          }
        }
        if (flag)
          return;
      }
      this.unloadEvent(eventID);
      Player[] playerArray = new Player[dEventInstance];
      float[] numArray = new float[dEventInstance];
      this.m_players[eventID] = playerArray;
      this.m_volumes[eventID] = numArray;
      for (int instance = 0; instance < dEventInstance; ++instance)
      {
        Player player = this.loadPlayer(eventID);
        playerArray[instance] = player;
        numArray[instance] = SoundManager.MAX_VOLUME;
        this.applyVolume(eventID, instance);
      }
    }

    public void unloadEvent(int eventID)
    {
      if (eventID < 0)
        return;
      Player[] player1 = this.m_players[eventID];
      if (player1 != null)
      {
        for (int index = 0; index < player1.Length; ++index)
        {
          Player player2 = player1[index];
          if (player2 != null)
          {
            if (player2.getState() == 400)
            {
              player2.stop();
              player2.setMediaTime(0L);
              Task.Delay(500);
            }
            player2.close();
          }
        }
      }
      this.m_players[eventID] = new Player[0];
      this.m_volumes[eventID] = new float[0];
    }

    public int playEventLooped(int eventID) => this.playEvent(eventID, 1f, true);

    public int playEvent(int eventID) => this.playEvent(eventID, 1f, false);

    public int playEventLooped(int eventID, float volume) => this.playEvent(eventID, volume, true);

    public int playEvent(int eventID, float volume) => this.playEvent(eventID, volume, false);

    public int playEvent(int eventID, float volume, bool looped)
    {
      Player[] player1 = this.m_players[eventID];
      for (int instance = 0; instance < player1.Length; ++instance)
      {
        Player player2 = player1[instance];
        if (player2.getState() != 400)
        {
          int handle = this.getHandle(eventID, instance);
          player2.prefetch();
          player2.setLoopCount(looped ? -1 : 0);
          this.setVolumeEvent(handle, volume);
          PositionControl positionControl = player2.getPositionControl();
          if (positionControl != null)
          {
            if (((int) this.d_eventFlags[eventID] & 1) != 0)
            {
              positionControl.setSourceRelative(true);
              positionControl.setPosition(0.0f, 0.0f, 0.0f);
            }
            else
            {
              positionControl.setSourceRelative(false);
              positionControl.setReferenceDistance(this.d_eventRanges[eventID]);
            }
          }
          player2.start();
          return handle;
        }
      }
      return -1;
    }

    public int playEventAt(int eventID, float posX, float posY, float posZ)
    {
      return this.playEventAt(eventID, posX, posY, posZ, 1f);
    }

    public int playEventAt(int eventID, float posX, float posY, float posZ, float volume)
    {
      int handle = this.playEvent(eventID, volume);
      if (handle != -1)
        this.setEventPosition(handle, posX, posY, posZ);
      return handle;
    }

    public void pauseEvent(int handle)
    {
      if (handle < 0)
        return;
      Player player = this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)];
      if (player == null)
        return;
      player.stop();
      this.setVolumeEvent(handle, SoundManager.MIN_VOLUME);
    }

    public void stopEvent(int handle)
    {
      if (handle < 0)
        return;
      Player player = this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)];
      if (player == null)
        return;
      player.stop();
      this.setVolumeEvent(handle, SoundManager.MIN_VOLUME);
      player.setMediaTime(0L);
    }

    public void addPlayerListener(int eventId, PlayerListener listener)
    {
      foreach (Player player in this.m_players[eventId])
        player.addPlayerListener(listener);
    }

    public void removePlayerListener(int eventId, PlayerListener listener)
    {
      if (eventId < 0)
        return;
      foreach (Player player in this.m_players[eventId])
        player.removePlayerListener(listener);
    }

    public int getDurationMicros(int eventID, int instance)
    {
      if (eventID < 0 || this.m_players.Length <= eventID || instance < 0 || this.m_players[eventID].Length <= instance)
        return 0;
      Player player = this.m_players[eventID][instance];
      return player == null ? 0 : (int) player.getDuration();
    }

    public long getEventTimeMicros(int eventID, int instance)
    {
      if (eventID < 0 || this.m_players.Length <= eventID || instance < 0 || this.m_players[eventID].Length <= instance)
        return 0;
      Player player = this.m_players[eventID][instance];
      return player == null ? 0L : player.getMediaTime();
    }

    public void setEventTimeMicros(int eventID, int instance, long time)
    {
      if (0 > eventID && eventID >= this.m_players.Length && 0 > instance && instance >= this.m_players[eventID].Length)
        return;
      this.m_players[eventID][instance]?.setMediaTime(time);
    }

    public bool isEventLoaded(int eventID)
    {
      Player[] player = this.m_players[eventID];
      return player != null && player.Length > 0;
    }

    public bool isHandlePlaying(int handle)
    {
      if (handle < 0)
        return false;
      Player player = this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)];
      return player != null && player.getState() == 400;
    }

    public bool isEventPlaying(int eventID) => this.getEventPlayingHandle(eventID) != -1;

    public int getEventPlayingHandle(int eventID)
    {
      Player[] player = this.m_players[eventID];
      for (int eventPlayingHandle = 0; eventPlayingHandle < player.Length; ++eventPlayingHandle)
      {
        if (player[eventPlayingHandle].getState() == 400)
          return eventPlayingHandle;
      }
      return -1;
    }

    public void setVolumeGlobal(float volume)
    {
      this.m_globalVolume = volume;
      for (int eventID = 0; eventID < this.m_players.Length; ++eventID)
      {
        if (this.m_players[eventID] != null)
        {
          for (int instance = 0; instance < this.m_players[eventID].Length; ++instance)
            this.applyVolume(eventID, instance);
        }
      }
    }

    public float getVolumeGlobal() => this.m_globalVolume;

    public void setVolumeMusic(float volume)
    {
      this.m_musicVolume = volume;
      BGMusic.setVolume(this.m_musicVolume);
    }

    public float getVolumeMusic() => this.m_musicVolume;

    public void setVolumeSFX(float volumeConst)
    {
      float num = volumeConst;
      if ((double) num > 1.0)
        num = 1f;
      else if ((double) num < 0.059999998658895493)
        num = 0.0f;
      this.m_sfxVolume = num;
      if (this.m_players == null)
        return;
      for (int eventID = 0; eventID < this.m_players.Length; ++eventID)
      {
        if (((int) this.d_groupFlags[(int) this.d_eventGroups[eventID]] & 2) != 0 && this.m_players[eventID] != null)
        {
          for (int instance = 0; instance < this.m_players[eventID].Length; ++instance)
            this.applyVolume(eventID, instance);
        }
      }
    }

    public float getVolumeSFX() => this.m_sfxVolume;

    public void setVolumeEvent(int handle, float volume)
    {
      if (handle < 0)
        return;
      this.applyVolume(this.getHandleEventId(handle), this.getHandleInstanceId(handle));
    }

    public void setLoopCountEvent(int handle, int loopCount)
    {
      if (handle < 0)
        return;
      this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)]?.setLoopCount(loopCount);
    }

    public void setVolumeGroup(int group, float volume)
    {
      if (group >= this.m_groupVolumes.Length)
        return;
      this.m_groupVolumes[group] = volume;
      for (int eventID = 0; eventID < this.m_players.Length; ++eventID)
      {
        if ((int) this.d_eventGroups[eventID] == group && this.m_players[eventID] != null)
        {
          for (int instance = 0; instance < this.m_players[eventID].Length; ++instance)
            this.applyVolume(eventID, instance);
        }
      }
    }

    public void setEventReferenceDistance(int handle, float rD)
    {
      this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)].getPositionControl()?.setReferenceDistance(rD);
    }

    public void setEventRolloffFactor(int handle, float rF)
    {
      this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)].getPositionControl()?.setRolloffFactor(rF);
    }

    public void setListenerPosition(float posX, float posY, float posZ)
    {
      PlayerWP7CoreAudio.setListenerPosition(posX, posY, posZ);
    }

    public void setListenerVelocity(float velX, float velY, float velZ)
    {
      PlayerWP7CoreAudio.setListenerVelocity(velX, velY, velZ);
    }

    public void setListenerOrientation(
      float atX,
      float atY,
      float atZ,
      float upX,
      float upY,
      float upZ)
    {
      PlayerWP7CoreAudio.setListenerOrientation(atX, atY, atZ, upX, upY, upZ);
    }

    public void setEventPosition(int handle, float posX, float posY, float posZ)
    {
      this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)].getPositionControl()?.setPosition(posX, posY, posZ);
    }

    public void setEventVelocity(int handle, float velX, float velY, float velZ)
    {
      this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)].getPositionControl()?.setVelocity(velX, velY, velZ);
    }

    public void setEventPitch(int handle, float pitchFactor)
    {
      this.m_players[this.getHandleEventId(handle)][this.getHandleInstanceId(handle)].getPitchControl()?.setPitchFactor(pitchFactor);
    }

    public void setGroupPitch(int group, float pitchFactor)
    {
      for (int index1 = 0; index1 < this.m_players.Length; ++index1)
      {
        if ((int) this.d_eventGroups[index1] == group && this.m_players[index1] != null)
        {
          for (int index2 = 0; index2 < this.m_players[index1].Length; ++index2)
            this.m_players[index1][index2].getPitchControl()?.setPitchFactor(pitchFactor);
        }
      }
    }

    public void pause()
    {
    }

    public void resume()
    {
    }

    public void update(int timeStep)
    {
    }
  }
}
