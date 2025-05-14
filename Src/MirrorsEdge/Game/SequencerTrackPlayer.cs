
// Type: game.SequencerTrackPlayer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class SequencerTrackPlayer
  {
    private SoundSequencer m_sequencer;
    private SequencerTrack m_track;
    private GameObject m_object;
    private bool m_playing;
    private int m_currentTime;
    private int m_nextEvent;

    public SequencerTrackPlayer(SoundSequencer sequencer)
    {
      this.m_sequencer = sequencer;
      this.m_track = (SequencerTrack) null;
      this.m_playing = false;
      this.m_currentTime = 0;
      this.m_nextEvent = -1;
      this.m_object = (GameObject) null;
    }

    public void Destructor()
    {
      this.m_sequencer = (SoundSequencer) null;
      this.m_track = (SequencerTrack) null;
      this.m_object = (GameObject) null;
    }

    public void reset()
    {
      this.m_track = (SequencerTrack) null;
      this.m_playing = false;
      this.m_currentTime = 0;
      this.m_nextEvent = -1;
    }

    public void play(SequencerTrack track, GameObject @object)
    {
      this.m_track = track;
      this.m_playing = true;
      this.m_currentTime = 0;
      this.m_nextEvent = 0;
      this.m_object = @object;
    }

    public void stop() => this.reset();

    public bool isPlaying() => this.m_playing;

    public void update(int timeStep)
    {
      if (!this.m_playing)
        return;
      for (this.m_currentTime += timeStep; this.m_nextEvent < this.m_track.m_events.Length && this.m_track.m_events[this.m_nextEvent].m_time <= this.m_currentTime; ++this.m_nextEvent)
        this.m_track.m_events[this.m_nextEvent].play(this.m_sequencer, this.m_object);
      if (this.m_currentTime < this.m_track.m_duration)
        return;
      if ((this.m_track.m_flags & 1) == 0)
      {
        this.reset();
      }
      else
      {
        this.m_currentTime -= this.m_track.m_duration;
        this.m_nextEvent = 0;
      }
    }
  }
}
