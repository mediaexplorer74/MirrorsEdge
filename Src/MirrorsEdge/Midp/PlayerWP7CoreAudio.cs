// Decompiled with JetBrains decompiler
// Type: midp.PlayerWP7CoreAudio
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using mirrorsedge_wp7;
using System;

#nullable disable
namespace midp
{
  internal class PlayerWP7CoreAudio : Player, VolumeControl, PitchControl, PositionControl, Control
  {
    private SoundEffect m_effect;
    private SoundEffectInstance m_effectInstance;
    private bool m_isLooped;
    private bool m_is3D;
    private bool m_sourceRelative;
    private string m_audioFileID;
    private long m_duration;
    private int m_volume;
    private Vector3 m_pos;
    private Vector3 m_vel;
    private AudioEmitter m_emitter;
    private static Vector3 m_listenerPosition;
    private static Vector3 m_listenerVelocity;
    private static Vector3 m_listenerUp;
    private static Vector3 m_listenerDir;
    private static AudioListener[] m_listener = new AudioListener[1];

    public PlayerWP7CoreAudio(string fileID)
    {
      this.m_duration = -1L;
      this.m_volume = 100;
      this.m_audioFileID = "sounds/" + fileID;
      this.m_effect = MirrorsEdge.content.Load<SoundEffect>(this.m_audioFileID);
      this.m_effectInstance = (SoundEffectInstance) null;
      this.m_isLooped = false;
      this.m_is3D = false;
      this.m_sourceRelative = true;
      this.m_pos = new Vector3();
      this.m_vel = new Vector3();
      this.m_emitter = new AudioEmitter();
      this.m_emitter.Up = Vector3.Up;
    }

    protected override bool isExclusive() => false;

    protected override void update(bool exclusiveResourceNeedsLock)
    {
    }

    protected override Player duplicate() => (Player) null;

    public override Control getControl(string controlType)
    {
      Control control = (Control) null;
      switch (controlType)
      {
        case "PitchControl":
          control = (Control) this;
          break;
        case "VolumeControl":
          control = (Control) this;
          break;
        case "PositionControl":
          control = (Control) this;
          break;
      }
      return control;
    }

    public override VolumeControl getVolumeControl() => (VolumeControl) this;

    public override PitchControl getPitchControl() => (PitchControl) this;

    public override PositionControl getPositionControl() => (PositionControl) this;

    public override void close()
    {
      if (this.getState() == 0)
        return;
      this.deallocate();
      this.stateTransition(0);
    }

    public override void deallocate()
    {
      switch (this.getState())
      {
        case 400:
        case 500:
          this.stop();
          this.stateTransition(200);
          break;
      }
      if (this.m_effectInstance == null)
        return;
      this.m_effectInstance = (SoundEffectInstance) null;
      this.m_is3D = false;
    }

    public override long getDuration()
    {
      if (this.m_duration == -1L)
        this.m_duration = (long) this.m_effect.Duration.TotalMilliseconds;
      return this.m_duration;
    }

    public override long getMediaTime() => 0;

    public override void prefetch()
    {
      base.prefetch();
      if (this.getState() == 400 || this.getState() == 500 || this.getState() != 200)
        return;
      this.m_effectInstance = this.m_effect.CreateInstance();
      this.m_effectInstance.IsLooped = this.m_isLooped;
      this.updatePosition();
      this.updateVelocity();
      this.update3D();
      this.stateTransition(300);
    }

    public override void realize()
    {
      if (this.getState() != 100)
        return;
      this.m_effectInstance = (SoundEffectInstance) null;
      this.m_is3D = false;
      this.stateTransition(200);
    }

    public override void setLoopCount(int count)
    {
      if (this.m_effectInstance == null)
        return;
      this.m_isLooped = count < 0;
      if (this.getState() == 400 && this.m_isLooped != this.m_effectInstance.IsLooped)
      {
        this.m_effectInstance.Stop();
        this.m_effectInstance = (SoundEffectInstance) null;
        this.m_effectInstance = this.m_effect.CreateInstance();
        this.m_effectInstance.IsLooped = this.m_isLooped;
        if (this.m_is3D)
          this.m_effectInstance.Apply3D(PlayerWP7CoreAudio.m_listener, this.m_emitter);
        this.m_effectInstance.Play();
      }
      else
      {
        if (this.getState() != 300 || this.m_isLooped == this.m_effectInstance.IsLooped)
          return;
        this.m_effectInstance = (SoundEffectInstance) null;
        this.m_is3D = false;
        this.m_effectInstance = this.m_effect.CreateInstance();
        this.m_effectInstance.IsLooped = this.m_isLooped;
      }
    }

    public override long setMediaTime(long nowMicroSeconds) => 0;

    public override void start()
    {
      base.start();
      if (this.getState() == 400)
        return;
      if (this.getState() == 500)
        this.stop();
      this.m_effectInstance.Volume = (float) this.m_volume * 0.01f;
      this.updatePosition();
      this.updateVelocity();
      this.update3D();
      this.m_effectInstance.Play();
      this.stateTransition(400);
    }

    public override void stop()
    {
      switch (this.getState())
      {
        case 400:
        case 500:
          this.m_effectInstance.Stop();
          this.stateTransition(300);
          break;
      }
    }

    public void setPitchFactor(float factor)
    {
      if (this.m_effectInstance == null)
        return;
      float num = (float) Math.Log((double) factor, 2.0);
      if ((double) num < -1.0)
        num = -1f;
      else if ((double) num > 1.0)
        num = 1f;
      this.m_effectInstance.Pitch = num;
    }

    public float getPitchFactor()
    {
      float pitchFactor = 1f;
      if (this.m_effectInstance != null)
        pitchFactor = (float) Math.Pow(2.0, (double) this.m_effectInstance.Pitch);
      return pitchFactor;
    }

    public void setVolume(int volume)
    {
      if (this.m_volume == volume)
        return;
      this.m_volume = volume;
      if (this.m_effectInstance == null)
        return;
      if (this.m_is3D)
        this.updateVolume3D();
      else
        this.m_effectInstance.Volume = (float) volume * 0.01f;
    }

    public int getVolume() => this.m_effectInstance != null ? this.m_volume : 0;

    private void update3D()
    {
      if (this.m_sourceRelative)
        return;
      if (this.m_pos == Vector3.Zero)
      {
        this.m_pos = PlayerWP7CoreAudio.m_listenerPosition;
        this.updatePosition();
      }
      if (!this.m_is3D)
      {
        bool flag = false;
        if (this.getState() == 400)
          flag = true;
        float volume = this.m_effectInstance.Volume;
        float pitch = this.m_effectInstance.Pitch;
        this.m_effectInstance.Stop();
        this.m_effectInstance = (SoundEffectInstance) null;
        this.m_effectInstance = this.m_effect.CreateInstance();
        this.m_effectInstance.Volume = volume;
        this.m_effectInstance.Pitch = pitch;
        this.m_effectInstance.IsLooped = this.m_isLooped;
        this.m_effectInstance.Apply3D(PlayerWP7CoreAudio.m_listener, this.m_emitter);
        this.updateVolume3D();
        if (flag)
          this.m_effectInstance.Play();
        this.m_is3D = true;
      }
      else
      {
        this.m_effectInstance.Apply3D(PlayerWP7CoreAudio.m_listener, this.m_emitter);
        this.updateVolume3D();
      }
    }

    private void updateVolume3D()
    {
      float num1 = (this.m_emitter.Position - PlayerWP7CoreAudio.m_listener[0].Position).Length() - SoundEffect.DistanceScale;
      if ((double) num1 < (double) SoundEffect.DistanceScale)
        num1 = SoundEffect.DistanceScale;
      float num2 = (float) this.m_volume / 100f * SoundEffect.DistanceScale / num1;
      if ((double) num2 < 0.10000000149011612)
        num2 = 0.0f;
      this.m_effectInstance.Volume = num2;
    }

    private void updatePosition()
    {
      if (this.m_effectInstance == null)
        return;
      this.m_emitter.Position = this.m_pos;
    }

    private void updateVelocity()
    {
      if (this.m_effectInstance == null)
        return;
      this.m_emitter.Velocity = this.m_vel;
    }

    public void setPosition(float x, float y, float z)
    {
      this.m_pos = new Vector3(x, y, z);
      this.updatePosition();
      this.update3D();
    }

    public void setVelocity(float x, float y, float z)
    {
      this.m_vel = new Vector3(x, y, z);
      this.updateVelocity();
      this.update3D();
    }

    public void setReferenceDistance(float dist)
    {
      if (this.m_effectInstance == null)
        return;
      SoundEffect.DistanceScale = dist;
    }

    public void setRolloffFactor(float factor)
    {
      SoundEffectInstance effectInstance = this.m_effectInstance;
    }

    public void setSourceRelative(bool set) => this.m_sourceRelative = set;

    public override int getState()
    {
      if (this.m_effectInstance == null)
        return base.getState();
      switch (this.m_effectInstance.State)
      {
        case SoundState.Playing:
          this.m_state = 400;
          break;
        case SoundState.Paused:
          this.m_state = 500;
          break;
        case SoundState.Stopped:
          this.m_state = 300;
          break;
      }
      return this.m_state;
    }

    public override void pause()
    {
      this.m_effectInstance.Pause();
      this.stateTransition(500);
    }

    public override void unpause()
    {
      this.m_effectInstance.Resume();
      this.stateTransition(400);
    }

    public static void setListenerPosition(float x, float y, float z)
    {
      PlayerWP7CoreAudio.m_listenerPosition = new Vector3(x, y, z);
      if (PlayerWP7CoreAudio.m_listener[0] == null)
        PlayerWP7CoreAudio.m_listener[0] = new AudioListener();
      PlayerWP7CoreAudio.m_listener[0].Position = PlayerWP7CoreAudio.m_listenerPosition;
    }

    public static void setListenerOrientation(
      float atx,
      float aty,
      float atz,
      float upx,
      float upy,
      float upz)
    {
      PlayerWP7CoreAudio.m_listenerDir = new Vector3(atx, aty, atz);
      PlayerWP7CoreAudio.m_listenerUp = new Vector3(upx, upy, upz);
      if (PlayerWP7CoreAudio.m_listener[0] == null)
        PlayerWP7CoreAudio.m_listener[0] = new AudioListener();
      PlayerWP7CoreAudio.m_listener[0].Forward = PlayerWP7CoreAudio.m_listenerDir;
      PlayerWP7CoreAudio.m_listener[0].Up = PlayerWP7CoreAudio.m_listenerUp;
    }

    public static void setListenerVelocity(float x, float y, float z)
    {
      PlayerWP7CoreAudio.m_listenerVelocity = new Vector3(x, y, z);
      if (PlayerWP7CoreAudio.m_listener[0] == null)
        PlayerWP7CoreAudio.m_listener[0] = new AudioListener();
      PlayerWP7CoreAudio.m_listener[0].Velocity = PlayerWP7CoreAudio.m_listenerVelocity;
    }
  }
}
