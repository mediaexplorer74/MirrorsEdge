// Decompiled with JetBrains decompiler
// Type: generic.AnimPlayer
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using System;

#nullable disable
namespace generic
{
  public class AnimPlayer
  {
    public const int ANIMFLAG_NONE = 0;
    public const int ANIMFLAG_INITIALISED = 1;
    public const int ANIMFLAG_ANIMATING = 2;
    public const int ANIMFLAG_LOOP = 4;
    public const int ANIMFLAG_REVERSE = 8;
    public const int ANIMFLAG_RESTART = 16;
    public const int ANIMFLAG_RESTART_LOOP = 20;
    private int m_animID;
    private int m_currentFrame;
    private int m_currentFrameTime;
    private int m_flags;

    public AnimPlayer()
    {
      this.m_animID = -1;
      this.m_currentFrame = 0;
      this.m_currentFrameTime = 0;
      this.m_flags = 0;
    }

    public bool updateAnim(int interval)
    {
      if ((this.m_flags & 2) == 0)
        return false;
      this.m_currentFrameTime += interval;
      if (this.m_currentFrameTime <= (int) AnimationManager.getAnimFrameDuration(this.m_animID, this.m_currentFrame))
        return false;
      if ((this.m_flags & 8) != 0)
        this.prevFrame();
      else
        this.nextFrame();
      return true;
    }

    public void nextFrame()
    {
      this.m_currentFrameTime -= (int) AnimationManager.getAnimFrameDuration(this.m_animID, this.m_currentFrame);
      if (this.m_currentFrameTime < 0)
        this.m_currentFrameTime = 0;
      ++this.m_currentFrame;
      if (this.m_currentFrame < AnimationManager.getAnimFrameCount(this.m_animID))
        return;
      if ((this.m_flags & 4) != 0)
      {
        this.m_currentFrame = 0;
      }
      else
      {
        this.m_currentFrame = AnimationManager.getAnimFrameCount(this.m_animID) - 1;
        this.m_flags &= -3;
      }
    }

    public void prevFrame()
    {
      this.m_currentFrameTime -= (int) AnimationManager.getAnimFrameDuration(this.m_animID, this.m_currentFrame);
      if (this.m_currentFrameTime < 0)
        this.m_currentFrameTime = 0;
      --this.m_currentFrame;
      if (this.m_currentFrame > 0)
        return;
      if ((this.m_flags & 4) != 0)
      {
        this.m_currentFrame = AnimationManager.getAnimFrameCount(this.m_animID) - 1;
      }
      else
      {
        this.m_currentFrame = 0;
        this.m_flags &= -3;
      }
    }

    public void startAnim(int animID, int bitflag)
    {
      if ((bitflag & 16) != 0 || this.m_animID != animID)
      {
        this.m_animID = animID;
        this.m_currentFrame = (this.m_flags & 8) == 0 ? 0 : AnimationManager.getAnimFrameCount(this.m_animID) - 1;
        this.m_currentFrameTime = 0;
      }
      else if (this.m_currentFrame >= AnimationManager.getAnimFrameCount(this.m_animID))
        this.m_currentFrame = 0;
      else if (this.m_currentFrame < 0)
        this.m_currentFrame = AnimationManager.getAnimFrameCount(this.m_animID) - 1;
      this.m_flags = 3 | bitflag & 4;
    }

    public void drawAnim(Graphics g, int x, int y)
    {
      if (this.m_animID < 0)
        return;
      AnimationManager.drawAnimFrame(g, this.m_animID, this.m_currentFrame, x, y);
    }

    public void setReverse(bool reverse)
    {
      if (reverse)
        this.m_flags |= 8;
      else
        this.m_flags &= -9;
    }

    public int getAnimID() => this.m_animID;

    public int getCurrAnimFrame() => this.m_currentFrame;

    public int getNumAnimFrames()
    {
      return this.m_animID == -1 ? 0 : AnimationManager.getAnimFrameCount(this.m_animID);
    }

    public int getAnimDuration()
    {
      if (this.m_animID == -1)
        return 0;
      int animDuration = 0;
      for (int frameID = 0; frameID < AnimationManager.getAnimFrameCount(this.m_animID); ++frameID)
        animDuration += (int) AnimationManager.getAnimFrameDuration(this.m_animID, frameID);
      return animDuration;
    }

    public int getNumFirePointsCurrFrame()
    {
      return AnimationManager.getAnimFrameFirePointCount(this.m_animID, this.m_currentFrame);
    }

    public bool getFirePointCurrFrame(ref int[] result, int firePointID)
    {
      return AnimationManager.getAnimFrameFirePoint(ref result, this.m_animID, this.m_currentFrame, firePointID);
    }

    public bool getCollBoxCurrFrame(ref int[] result, int boxID)
    {
      return AnimationManager.getAnimFrameCollisionBox(ref result, this.m_animID, this.m_currentFrame, boxID);
    }

    public bool isAnimating() => (this.m_flags & 2) != 0;

    public bool isLooping() => (this.m_flags & 4) != 0;

    public bool isInitialised() => (this.m_flags & 1) != 0;

    public void setAnimating(bool b)
    {
      if (b)
        this.m_flags |= 2;
      else
        this.m_flags &= -3;
    }

    public void setFrame(int f) => this.m_currentFrame = f;

    public void setLastFrame()
    {
      this.m_currentFrame = Math.Max(0, this.getNumAnimFrames() - 1);
      if (this.isLooping())
        return;
      this.setAnimating(false);
    }
  }
}
