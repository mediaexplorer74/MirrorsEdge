
// Type: game.GameObjectGhost
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using support;
using System;

#nullable disable
namespace game
{
  public class GameObjectGhost : GameObjectRunner
  {
    public const int FADE_IN_DUR = 1000;
    public const int FADE_OUT_DUR = 1000;
    private GhostAnimationPlayback m_playback;
    private int m_keyframeIndex;
    private int m_keyframeTime;
    private bool m_fadeIn;
    private int m_fadeInTime;
    private int m_elapsedTime;
    private int m_overallTime;

    public GameObjectGhost(MEdgeMap map, GhostAnimationPlayback playback)
      : base(map, 1)
    {
      this.m_playback = playback;
      this.m_keyframeIndex = 0;
      this.m_keyframeTime = 0;
      this.m_fadeIn = false;
      this.m_fadeInTime = 0;
      this.m_elapsedTime = 0;
      this.m_overallTime = 0;
      this.setVisualAssets((int) M3GAssets.get("MODEL_FAITH_GHOST"), 0);
      M3GAssets.orphanNode((Node) this.m_objectNode.find(103));
      M3GAssets.orphanNode((Node) this.m_objectNode.find(104));
      M3GAssets.applyAlphaFactor(this.m_objectNode, 0.0f);
      int keyframeNum = this.m_playback.getKeyframeNum();
      for (int index = 0; index != keyframeNum; ++index)
        this.m_overallTime += this.m_playback.getKeyframe(index).duration;
    }

    public override void Destructor()
    {
      if (this.m_playback == null)
        return;
      this.m_playback.Destructor();
      this.m_playback = (GhostAnimationPlayback) null;
      base.Destructor();
    }

    public GhostAnimationPlayback getPlayback() => this.m_playback;

    public override void checkpointActivated()
    {
    }

    public override void resetCheckpoint()
    {
    }

    public override void resetLevel()
    {
      this.m_fadeIn = false;
      this.m_fadeInTime = 0;
      this.m_elapsedTime = 0;
      this.m_keyframeIndex = 0;
      this.m_keyframeTime = 0;
      M3GAssets.applyAlphaFactor(this.m_objectNode, 0.0f);
      this.resetCheckpoint();
      GhostKeyframe keyframe = this.m_playback.getKeyframe(0);
      this.setVisual((int) keyframe.visualCode);
      this.m_position.set(keyframe.position);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
    }

    public override void update(int timeStepMillis)
    {
      GameObjectPlayer playerObject = this.m_map.getPlayerObject();
      base.update(timeStepMillis);
      this.m_keyframeTime += timeStepMillis;
      this.m_elapsedTime += timeStepMillis;
      int num = this.m_playback.getKeyframeNum() - 1;
      bool flag = false;
      GhostKeyframe keyframe1 = this.m_playback.getKeyframe(this.m_keyframeIndex);
      while (this.m_keyframeIndex != num && keyframe1.duration <= this.m_keyframeTime)
      {
        this.m_keyframeTime -= keyframe1.duration;
        ++this.m_keyframeIndex;
        keyframe1 = this.m_playback.getKeyframe(this.m_keyframeIndex);
        flag = true;
      }
      if (flag)
      {
        this.setVisual((int) keyframe1.visualCode);
        if (this.getVisualType() == 2)
          this.set3WayAnimationBlendWeights(keyframe1.blend3WayValue);
      }
      if (this.m_keyframeIndex == num || this.m_keyframeTime == 0)
      {
        this.m_position = keyframe1.position;
      }
      else
      {
        GhostKeyframe keyframe2 = this.m_playback.getKeyframe(this.m_keyframeIndex + 1);
        float progress = (float) this.m_keyframeTime / (float) keyframe1.duration;
        this.m_position.setAsLinearInterpolation(keyframe1.position, keyframe2.position, progress);
      }
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      if (!this.m_fadeIn)
      {
        MathVector mathVector = new MathVector(playerObject.m_position);
        if (!GameCommon.compareFloats(this.m_position.x, mathVector.x) || !GameCommon.compareFloats(this.m_position.y, mathVector.y) || !GameCommon.compareFloats(this.m_position.z, mathVector.z))
          this.m_fadeIn = true;
      }
      else if (this.m_fadeInTime < 1000)
      {
        this.m_fadeInTime = Math.Min(this.m_fadeInTime + timeStepMillis, 1000);
        M3GAssets.applyAlphaFactor(this.m_objectNode, (float) this.m_fadeInTime / 1000f);
      }
      if (this.m_overallTime - this.m_elapsedTime > 1000)
        return;
      M3GAssets.applyAlphaFactor(this.m_objectNode, (float) Math.Max(0, this.m_overallTime - this.m_elapsedTime) / 1000f);
    }
  }
}
