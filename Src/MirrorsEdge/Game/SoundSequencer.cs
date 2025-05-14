// Decompiled with JetBrains decompiler
// Type: game.SoundSequencer
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using midp;

#nullable disable
namespace game
{
  public class SoundSequencer
  {
    private const int MAX_SEQUENCES = 10;
    private SceneGame m_sceneGame;
    private SoundManager m_soundManager;
    private SoundEventPoolManager m_soundPoolManager;
    private SequencerTrack[] m_sequencerTracks;
    private SequencerTrackPlayer[] m_sequencerPlayers = new SequencerTrackPlayer[10];

    public SoundSequencer(
      SceneGame sceneGame,
      SoundManager soundManager,
      SoundEventPoolManager soundPoolManager)
    {
      this.m_sceneGame = sceneGame;
      this.m_soundManager = soundManager;
      this.m_soundPoolManager = soundPoolManager;
      for (int index = 0; index < 10; ++index)
        this.m_sequencerPlayers[index] = new SequencerTrackPlayer(this);
    }

    public void Destructor()
    {
      for (int index = 0; index < this.m_sequencerTracks.Length; ++index)
      {
        if (this.m_sequencerTracks[index] != null)
          this.m_sequencerTracks[index].Destructor();
      }
      for (int index = 0; index < 10; ++index)
      {
        if (this.m_sequencerPlayers[index] != null)
          this.m_sequencerPlayers[index].Destructor();
      }
      this.m_sequencerTracks = (SequencerTrack[]) null;
      this.m_sequencerPlayers = (SequencerTrackPlayer[]) null;
      this.m_sceneGame = (SceneGame) null;
      this.m_soundManager = (SoundManager) null;
      this.m_soundPoolManager = (SoundEventPoolManager) null;
    }

    public void loadData()
    {
      DataInputStream dis = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_SOUND_SEQUENCER_BIN")));
      short length = dis.readShort();
      this.m_sequencerTracks = new SequencerTrack[(int) length];
      for (int index = 0; index < (int) length; ++index)
        this.m_sequencerTracks[index] = new SequencerTrack(dis);
      dis.close();
      for (int index = 0; index < 10; ++index)
        this.m_sequencerPlayers[index].reset();
    }

    public void update(int timeStep)
    {
      for (int index = 0; index < 10; ++index)
      {
        if (this.m_sequencerPlayers[index].isPlaying())
          this.m_sequencerPlayers[index].update(timeStep);
      }
    }

    public int playSequence(int sequence) => this.playSequence(sequence, (GameObject) null);

    public int playSequence(int sequence, GameObject @object)
    {
      int num = -1;
      for (int index = 0; index < 10; ++index)
      {
        if (!this.m_sequencerPlayers[index].isPlaying())
        {
          this.m_sequencerPlayers[index].play(this.m_sequencerTracks[sequence], @object);
          num = index;
          break;
        }
      }
      return num;
    }

    public void stopSequence(int handle)
    {
      if (handle == -1)
        return;
      this.m_sequencerPlayers[handle].stop();
    }

    public bool isSequencePlaying(int handle)
    {
      return handle != -1 && this.m_sequencerPlayers[handle].isPlaying();
    }

    public void stopAllSequences()
    {
      for (int index = 0; index < 10; ++index)
        this.m_sequencerPlayers[index].stop();
    }

    public SceneGame getSceneGame() => this.m_sceneGame;

    public SoundManager getSoundManager() => this.m_soundManager;

    public SoundEventPoolManager getSoundPoolManager() => this.m_soundPoolManager;
  }
}
