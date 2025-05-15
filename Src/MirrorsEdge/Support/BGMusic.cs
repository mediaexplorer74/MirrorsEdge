
// Type: support.BGMusic
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using GameManager;
using generic;
using Microsoft.Xna.Framework.Media;
using midp;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace support
{
  public class BGMusic
  {
    private const int OTHER_AUDIO_CHECK_INTERVAL = 250;
    private const int TIMER_CLOSED_PAUSE = 250;
    public const int RESUME = 0;
    public const int RESTART = 1;
    public const int LOOP = 2;
    private SoundManager m_soundManager;
    private volatile bool m_playing;
    private volatile bool m_enabled;
    private volatile bool m_suspended;
    private volatile bool m_otherAudioPlaying;
    private volatile int m_otherAudioPollTime;
    private volatile bool m_restarting;
    private volatile bool m_closed = true; //!
    private volatile bool m_updated;
    public static object musicLockObject = new object();
    private volatile BGMusic.PlayState m_state;
    private volatile int m_timer;
    private volatile int m_eventPlaying;
    private volatile int m_eventToPlay;
    private volatile bool m_looped;
    private long m_startTime;
    private Song m_eventMusic;
    private static float volumeToSet = 0.0f;
    private static bool volumeChanged = false;
    private static long volumeChangeTime = DateTime.Now.Ticks / 10000L;
    private static BGMusic m_musicInstance;

    private void refreshVolume()
    {
      float num = !this.m_enabled || !this.m_playing ? SoundManager.MIN_VOLUME : this.m_soundManager.getVolumeMusic();
      if ((double) BGMusic.volumeToSet == (double) num)
        return;
      BGMusic.volumeToSet = (double) num < 0.10000000149011612 ? 0.0001f : num;
      BGMusic.volumeChanged = true;
    }

    private void restartMusic()
    {
      if (this.m_state != BGMusic.PlayState.STATE_PLAYING)
        return;
      this.m_state = BGMusic.PlayState.STATE_STOPPING;
      MediaPlayer.Stop();
    }

    public virtual meClass getClass() => (meClass) null;

    public static BGMusic getInstance() => BGMusic.m_musicInstance;

    public BGMusic(SoundManager soundManager)
    {
      this.m_soundManager = soundManager;
      this.m_playing = false;
      this.m_enabled = true;
      this.m_suspended = false;
      this.m_otherAudioPlaying = true;
      this.m_otherAudioPollTime = 0;
      this.m_state = BGMusic.PlayState.STATE_IDLE;
      this.m_timer = 0;
      this.m_eventPlaying = -1;
      this.m_eventToPlay = -1;
      this.m_looped = false;
      this.m_restarting = false;
      this.m_closed = false;
      this.m_updated = false;
      this.m_startTime = DateTime.Now.Ticks / 10000L;
      this.m_eventMusic = (Song) null;
      BGMusic.m_musicInstance = this;
      MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(this.playerUpdate);
    }

    public virtual void Destructor()
    {
    }

    public void playMusic(int eventId, int flags)
    {
      this.m_playing = true;
      bool flag = this.m_eventToPlay == eventId;
      this.m_eventToPlay = eventId;
      this.m_looped = (flags & 2) != 0;
      this.m_restarting = (flags & 1) != 0;
      switch (this.m_state)
      {
        case BGMusic.PlayState.STATE_IDLE:
          this.m_state = BGMusic.PlayState.STATE_READY;
          break;
        case BGMusic.PlayState.STATE_PLAYING:
          if (!this.m_restarting && flag)
            break;
          this.restartMusic();
          break;
      }
    }

    public void beQuiet()
    {
      this.m_playing = false;
      this.refreshVolume();
    }

    public static void setVolume(float volume)
    {
      if ((double) BGMusic.volumeToSet == (double) volume)
        return;
      BGMusic.volumeToSet = (double) volume < 0.10000000149011612 ? 0.0001f : volume;
      BGMusic.volumeChanged = true;
    }

    public void update(int timeStep)
    {
      if (this.m_suspended)
        return;
      lock (BGMusic.musicLockObject)
      {
        if (this.m_otherAudioPlaying)
        {
          this.m_otherAudioPollTime -= timeStep;
          if (this.m_otherAudioPollTime <= 0)
          {
            if (MediaPlayer.State == MediaState.Playing)
            {
              this.m_otherAudioPollTime = 250;
              if (!this.m_looped)
                this.m_playing = false;
            }
            else
              this.m_otherAudioPlaying = false;
          }
        }
        switch (this.m_state)
        {
          case BGMusic.PlayState.STATE_STOPPED:
            if (this.m_otherAudioPlaying || this.m_suspended)
              break;
            this.m_state = BGMusic.PlayState.STATE_CLOSING;
            this.m_eventMusic = (Song) null;
            break;
          case BGMusic.PlayState.STATE_READY:
            if (this.m_otherAudioPlaying || this.m_suspended || this.m_eventToPlay == -1)
              break;
            if (this.m_timer <= 0)
            {
              this.m_playing = this.m_playing && (this.m_looped || this.m_eventPlaying != this.m_eventToPlay || this.m_restarting);
              this.m_restarting = false;
              this.refreshVolume();
              this.m_eventPlaying = this.m_eventToPlay;
              string assetName = "music/" + this.m_soundManager.getEventName(this.m_eventPlaying);
              this.m_eventMusic = MirrorsEdge.content.Load<Song>(assetName);
              MediaPlayer.IsRepeating = this.m_looped;
              MediaPlayer.Play(this.m_eventMusic);
              this.m_state = BGMusic.PlayState.STATE_PLAYING;
              break;
            }
            this.m_timer -= timeStep;
            break;
          case BGMusic.PlayState.STATE_PAUSED:
            MediaPlayer.Resume();
            this.m_state = BGMusic.PlayState.STATE_PLAYING;
            break;
        }
      }
    }

    public async void suspend()
    {
      lock (BGMusic.musicLockObject)
      {
        if (this.m_suspended)
          return;
        this.m_suspended = true;
        if (this.m_state != BGMusic.PlayState.STATE_PLAYING)
          return;
        this.m_state = BGMusic.PlayState.STATE_STOPPING;
        MediaPlayer.Stop();
        //Thread.Sleep(200);
        Task.Delay(200);
        this.m_state = BGMusic.PlayState.STATE_CLOSING;
        this.m_eventMusic = (Song) null;
      }
    }

    public void resume()
    {
      lock (BGMusic.musicLockObject)
      {
        if (!this.m_suspended)
          return;
        this.m_suspended = false;
        this.m_otherAudioPlaying = true;
        this.m_otherAudioPollTime = 0;
        this.m_eventPlaying = -1;
      }
    }

    public void close() => this.m_closed = true;

    private void playerUpdate(object sender, EventArgs e)
    {
      switch (MediaPlayer.State)
      {
        case MediaState.Stopped:
          if (this.m_state == BGMusic.PlayState.STATE_STOPPED)
            break;
          this.m_state = BGMusic.PlayState.STATE_READY;
          break;
        case MediaState.Paused:
          if (this.m_suspended)
            break;
          this.m_state = BGMusic.PlayState.STATE_PAUSED;
          break;
      }
    }

    public bool isUpdated() => this.m_updated;

    public async virtual void run()
    {
      //while (!this.m_closed)
      {
        if (!MirrorsEdge.externalMusic)
        {
          long num = DateTime.Now.Ticks / 10000L;
          int timeStep = (int) Math.Max(1L, num - this.m_startTime);
          this.m_startTime = num;
          this.update(timeStep);
          if (BGMusic.volumeChanged && DateTime.Now.Ticks / 10000L - BGMusic.volumeChangeTime > 500L)
          {
            MediaPlayer.Volume = BGMusic.volumeToSet;
            BGMusic.volumeChanged = false;
            BGMusic.volumeChangeTime = DateTime.Now.Ticks / 10000L;
          }
        }
        this.m_updated = true;
        //Thread.Sleep(250);
        await Task.Delay(250);
      }
    }

    public static void Process() => BGMusic.getInstance().run();

    private enum PlayState
    {
      STATE_IDLE,
      STATE_PLAYING,
      STATE_STOPPING,
      STATE_STOPPED,
      STATE_CLOSING,
      STATE_READY,
      STATE_PAUSED,
    }
  }
}
