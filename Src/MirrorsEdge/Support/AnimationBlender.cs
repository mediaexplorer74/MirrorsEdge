// Decompiled with JetBrains decompiler
// Type: support.AnimationBlender
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using generic;
using microedition.m3g;
using System.Collections.Generic;

#nullable disable
namespace support
{
  public class AnimationBlender
  {
    public const int NO_FLAGS = 0;
    public const int RESTART = 1;
    public const int RESET_OTHER_WEIGHTS = 2;
    public const int SNAP_TO_WEIGHT = 4;
    private microedition.m3g.Node m_node;
    private short[] m_interpolationTimes;
    private int m_numChannels;
    private AnimationBlender.AnimationControllerProxy[] m_animationControllers;
    private int[] m_animationControllerUserIDs;
    private float[] m_channelWeights;
    private SignalFilter[] m_channelWeightFilters;
    private long m_worldTime;

    public AnimationBlender(int animationBlenderID)
    {
      this.m_node = (microedition.m3g.Node) null;
      this.m_worldTime = 0L;
      this.m_animationControllers = (AnimationBlender.AnimationControllerProxy[]) null;
      this.m_animationControllerUserIDs = (int[]) null;
      this.m_channelWeightFilters = (SignalFilter[]) null;
      this.m_channelWeights = (float[]) null;
      this.m_numChannels = 0;
      this.m_interpolationTimes = AppEngine.getAnimationBlenderData().getAnimationChannelInterpTimes(animationBlenderID);
      int[] controllerUserIds = AppEngine.getAnimationBlenderData().getAnimationControllerUserIDs(animationBlenderID);
      this.m_numChannels = controllerUserIds.Length;
      this.m_animationControllers = new AnimationBlender.AnimationControllerProxy[this.m_numChannels];
      this.m_animationControllerUserIDs = new int[this.m_numChannels];
      this.m_channelWeights = new float[this.m_numChannels];
      this.m_channelWeightFilters = new SignalFilter[this.m_numChannels];
      for (int index = 0; index < this.m_numChannels; ++index)
      {
        this.m_channelWeightFilters[index] = (SignalFilter) null;
        this.m_channelWeights[index] = 0.0f;
        this.m_animationControllerUserIDs[index] = controllerUserIds[index];
      }
    }

    public virtual void Destructor()
    {
      for (int index = 0; index < this.m_numChannels; ++index)
      {
        if (this.m_channelWeightFilters[index] != null)
        {
          if (this.m_channelWeightFilters[index] != null)
            this.m_channelWeightFilters[index].Destructor();
          this.m_channelWeightFilters[index] = (SignalFilter) null;
        }
      }
      this.m_node = (microedition.m3g.Node) null;
      this.m_animationControllers = (AnimationBlender.AnimationControllerProxy[]) null;
      this.m_animationControllerUserIDs = (int[]) null;
      this.m_channelWeightFilters = (SignalFilter[]) null;
      this.m_channelWeights = (float[]) null;
    }

    public void setNode(microedition.m3g.Node node)
    {
      this.m_node = node;
      for (int index = 0; index != this.m_numChannels; ++index)
      {
        AnimationController animationController = AnimationController.m3g_cast(this.m_node.find(this.m_animationControllerUserIDs[index]));
        this.m_animationControllers[index] = new AnimationBlender.AnimationControllerProxy()
        {
          m_target = animationController,
          m_weight = animationController.getWeight(),
          m_referenceSequenceTime = (float) animationController.getRefSequenceTime(),
          m_referenceWorldTime = (float) animationController.getRefWorldTime()
        };
      }
    }

    private void addUniqueAnimationTracksToVector(
      Object3D curObject,
      ref List<AnimationTrack> trackList)
    {
      int animationTrackCount = curObject.getAnimationTrackCount();
      for (int index = 0; index != animationTrackCount; ++index)
      {
        AnimationTrack animationTrack = curObject.getAnimationTrack(index);
        if (animationTrack != null && !trackList.Contains(animationTrack))
          trackList.Add(animationTrack);
      }
      SkinnedMesh skinnedMesh = SkinnedMesh.m3g_cast(curObject);
      if (skinnedMesh != null)
        this.addUniqueAnimationTracksToVector((Object3D) skinnedMesh.getSkeleton(), ref trackList);
      Group group = Group.m3g_cast(curObject);
      if (group == null)
        return;
      int childCount = group.getChildCount();
      for (int index = 0; index != childCount; ++index)
        this.addUniqueAnimationTracksToVector((Object3D) group.getChild(index), ref trackList);
    }

    public float getChannelWeight(int channelId)
    {
      return this.m_channelWeightFilters[channelId].getFilteredValue();
    }

    private void setWorldTime(long worldTime) => this.m_worldTime = worldTime;

    public void setChannelWeight(int channelId, float weight)
    {
      this.setChannelWeight(channelId, weight, 0);
    }

    public void setChannelWeight(int channelId, float weight, int flags)
    {
      bool flag1 = (flags & 2) != 0;
      bool flag2 = (flags & 1) != 0;
      bool snap = (flags & 4) != 0;
      if (flag1)
        this.setAllChannelWeightsToZero(snap);
      if (flag2)
      {
        this.m_animationControllers[channelId].m_referenceSequenceTime = 0.0f;
        this.m_animationControllers[channelId].m_referenceWorldTime = (float) this.m_worldTime;
      }
      for (int index = 0; index < this.m_numChannels; ++index)
      {
        if (index == channelId)
        {
          this.m_channelWeights[index] = weight;
          if (this.m_channelWeightFilters[index] == null)
            this.m_channelWeightFilters[index] = new SignalFilter(1, (float) this.m_interpolationTimes[index], index == 0 ? 1f : 0.0f);
          if (snap)
          {
            this.m_channelWeightFilters[index].setSteadyState(this.m_channelWeights[index]);
            break;
          }
          this.m_channelWeightFilters[index].setTargetValue(this.m_channelWeights[index]);
          break;
        }
      }
    }

    private void setAllChannelWeightsToZero(bool snap)
    {
      for (int index = 0; index < this.m_numChannels; ++index)
      {
        this.m_channelWeights[index] = 0.0f;
        if (this.m_channelWeightFilters[index] != null)
        {
          if (snap)
            this.m_channelWeightFilters[index].setSteadyState(this.m_channelWeights[index]);
          else
            this.m_channelWeightFilters[index].setTargetValue(this.m_channelWeights[index]);
        }
      }
    }

    public void update(int timeStep, bool fullUpdate)
    {
      this.m_worldTime += (long) timeStep;
      if (!fullUpdate)
        return;
      float num1 = 0.0f;
      for (int index = 0; index < this.m_numChannels; ++index)
      {
        if (this.m_channelWeightFilters[index] != null)
        {
          this.m_channelWeightFilters[index].update(timeStep);
          num1 += this.m_channelWeightFilters[index].getFilteredValue();
        }
      }
      float num2 = (double) num1 != 0.0 ? 1f / num1 : num1;
      for (int index = 0; index < this.m_numChannels; ++index)
      {
        if (this.m_channelWeightFilters[index] != null)
        {
          this.m_animationControllers[index].m_weight = num2 * this.m_channelWeightFilters[index].getFilteredValue();
          this.m_animationControllers[index].m_target.setWeight(this.m_animationControllers[index].m_weight);
          this.m_animationControllers[index].m_target.setPosition(this.m_animationControllers[index].m_referenceSequenceTime, (int) this.m_animationControllers[index].m_referenceWorldTime);
        }
        else
          this.m_animationControllers[index].m_target.setWeight(0.0f);
      }
      if (this.m_node == null)
        return;
      this.m_node.animate((int) this.m_worldTime);
    }

    private class AnimationControllerProxy
    {
      public AnimationController m_target;
      public float m_referenceSequenceTime;
      public float m_referenceWorldTime;
      public float m_weight;
    }
  }
}
