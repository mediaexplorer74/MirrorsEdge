
// Type: game.GameObjectRunner
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using midp;
using mirrorsedge_wp7;
using support;
using System;
using System.Collections.Generic;

#nullable disable
namespace game
{
  public class GameObjectRunner : GameObject
  {
    public const int VISUAL_ORIGIN_CLAMBER_HALF_METRE = 0;
    public const int VISUAL_ORIGIN_CLAMBER_HALF_METRE_STATIC = 1;
    public const int VISUAL_ORIGIN_CLAMBER_ONE_METRE = 2;
    public const int VISUAL_ORIGIN_CLAMBER_ONE_METRE_STATIC = 3;
    public const int VISUAL_ORIGIN_VAULT = 4;
    public const int VISUAL_ORIGIN_HANG_CLAMBER = 5;
    public const int VISUAL_ORIGIN_SCALE_HAUL_UP_STILL_LEFT = 6;
    public const int VISUAL_ORIGIN_SCALE_HAUL_UP_STILL_RIGHT = 7;
    public const int VISUAL_ORIGIN_SCALE_HAUL_UP_MOVING_LEFT = 8;
    public const int VISUAL_ORIGIN_SCALE_HAUL_UP_MOVING_RIGHT = 9;
    public const int VISUAL_ORIGIN_MELEED_IN_THE_FACE = 10;
    public const int VISUAL_ORIGIN_MELEEING_IN_THE_FACE = 11;
    public const int VISUAL_ORIGIN_MELEEING_GETUP = 12;
    public const int VISUAL_3BLEND_RUNNING = 0;
    public const int VISUAL_3BLEND_BALANCE_RUNNING = 1;
    public const int VISUAL_3BLEND_SWINGING = 2;
    public const int VISUAL_3BLEND_RAMP_SLIDE = 3;
    public const int VISUAL_3BLEND_ZIP_LINE = 4;
    public const int ANIM_TYPE_SOLO = 0;
    public const int ANIM_TYPE_ORIGIN = 1;
    public const int ANIM_TYPE_3BLEND = 2;
    public const int VISUAL_NULL = -1;
    private static Type VisualSolo = (Type) null;
    private static Dictionary<string, short> Enums = new Dictionary<string, short>();
    public static short[] RESOURCE_ARRAY;
    public static readonly short[] RESOURCE_ARRAY_FULL = new short[135]
    {
      (short) 24,
      (short) 79,
      (short) 31,
      (short) 27,
      (short) 80,
      (short) 32,
      (short) 25,
      (short) 77,
      (short) 29,
      (short) 28,
      (short) 78,
      (short) 30,
      (short) 23,
      (short) 76,
      (short) 28,
      (short) 26,
      (short) 81,
      (short) 33,
      (short) 51,
      (short) 85,
      (short) 57,
      (short) 52,
      (short) 84,
      (short) 56,
      (short) 49,
      (short) 83,
      (short) 54,
      (short) 48,
      (short) 82,
      (short) 53,
      (short) 60,
      (short) 87,
      (short) 64,
      (short) 59,
      (short) 88,
      (short) 65,
      (short) 61,
      (short) 89,
      (short) 67,
      (short) 7,
      (short) 12,
      (short) 5,
      (short) 10,
      (short) 6,
      (short) 11,
      (short) 56,
      (short) 61,
      (short) 57,
      (short) 62,
      (short) 58,
      (short) 63,
      (short) 11,
      (short) 16,
      (short) 12,
      (short) 17,
      (short) 54,
      (short) 59,
      (short) 55,
      (short) 60,
      (short) 50,
      (short) 55,
      (short) 4,
      (short) 9,
      (short) 1,
      (short) 6,
      (short) 3,
      (short) 8,
      (short) 9,
      (short) 14,
      (short) 10,
      (short) 15,
      (short) 34,
      (short) 41,
      (short) 35,
      (short) 42,
      (short) 13,
      (short) 18,
      (short) 18,
      (short) 23,
      (short) 15,
      (short) 20,
      (short) 29,
      (short) 36,
      (short) 30,
      (short) 37,
      (short) 47,
      (short) 35,
      (short) 46,
      (short) 34,
      (short) 20,
      (short) 25,
      (short) 21,
      (short) 26,
      (short) 22,
      (short) 27,
      (short) 42,
      (short) 49,
      (short) 45,
      (short) 52,
      (short) 62,
      (short) 68,
      (short) 63,
      (short) 69,
      (short) 64,
      (short) 70,
      (short) 65,
      (short) 71,
      (short) 66,
      (short) 72,
      (short) 67,
      (short) 75,
      (short) 68,
      (short) 1,
      (short) 69,
      (short) 2,
      (short) 70,
      (short) 3,
      (short) 71,
      (short) 4,
      (short) 0,
      (short) 2,
      (short) 36,
      (short) 38,
      (short) 43,
      (short) 37,
      (short) 39,
      (short) 41,
      (short) 46,
      (short) 40,
      (short) 31,
      (short) 32,
      (short) 38,
      (short) 33,
      (short) 44,
      (short) 43
    };
    public static readonly short[] RESOURCE_ARRAY_TRIAL = new short[118]
    {
      (short) 19,
      (short) 63,
      (short) 23,
      (short) 22,
      (short) 64,
      (short) 24,
      (short) 20,
      (short) 61,
      (short) 21,
      (short) 23,
      (short) 62,
      (short) 22,
      (short) 18,
      (short) 60,
      (short) 20,
      (short) 21,
      (short) 65,
      (short) 25,
      (short) 46,
      (short) 69,
      (short) 49,
      (short) 47,
      (short) 68,
      (short) 48,
      (short) 44,
      (short) 67,
      (short) 46,
      (short) 43,
      (short) 66,
      (short) 45,
      (short) 7,
      (short) 9,
      (short) 5,
      (short) 7,
      (short) 6,
      (short) 8,
      (short) 50,
      (short) 52,
      (short) 51,
      (short) 53,
      (short) 52,
      (short) 54,
      (short) 10,
      (short) 12,
      (short) 11,
      (short) 13,
      (short) 48,
      (short) 50,
      (short) 49,
      (short) 51,
      (short) 45,
      (short) 47,
      (short) 4,
      (short) 6,
      (short) 3,
      (short) 5,
      (short) 8,
      (short) 10,
      (short) 9,
      (short) 11,
      (short) 29,
      (short) 33,
      (short) 30,
      (short) 34,
      (short) 12,
      (short) 14,
      (short) 14,
      (short) 16,
      (short) 13,
      (short) 15,
      (short) 24,
      (short) 28,
      (short) 25,
      (short) 29,
      (short) 42,
      (short) 27,
      (short) 41,
      (short) 26,
      (short) 15,
      (short) 17,
      (short) 16,
      (short) 18,
      (short) 17,
      (short) 19,
      (short) 37,
      (short) 41,
      (short) 40,
      (short) 44,
      (short) 53,
      (short) 55,
      (short) 54,
      (short) 56,
      (short) 55,
      (short) 57,
      (short) 56,
      (short) 58,
      (short) 57,
      (short) 59,
      (short) 58,
      (short) 1,
      (short) 1,
      (short) 0,
      (short) 3,
      (short) 2,
      (short) 31,
      (short) 33,
      (short) 35,
      (short) 32,
      (short) 34,
      (short) 36,
      (short) 38,
      (short) 35,
      (short) 26,
      (short) 27,
      (short) 30,
      (short) 28,
      (short) 39,
      (short) 38
    };
    public static readonly short[] ANIM_FLAG_ARRAY = new short[2]
    {
      (short) 4,
      (short) 16
    };
    protected microedition.m3g.Node m_localTransformNode;
    protected AnimationBlender m_animationBlender;
    protected bool m_animateBlender;
    protected microedition.m3g.Node m_originNode;
    protected AnimPlayer3D m_originAnimPlayer;
    private float m_originYStart;
    private float m_originYScale;
    private float m_originYTotal;
    protected int m_visualType;
    protected int m_visualIndex;
    private float m_3WayBlendValue;
    private MathVector m_preOriginAnimPosNoOffset;
    private MathVector m_preOriginAnimatePosition;
    private float[] translation = new float[3];
    private float[] startPosition = new float[3];
    private float[] endPosition = new float[3];
    private GameObjectRunner.FacingDir m_facingDir;

    public static void SetResources()
    {
      if (MirrorsEdge.TrialMode)
      {
        GameObjectRunner.RESOURCE_ARRAY = GameObjectRunner.RESOURCE_ARRAY_TRIAL;
        GameObjectRunner.VisualSolo = typeof (GameObjectRunner.VisualSolo_Trial);
      }
      else
      {
        GameObjectRunner.RESOURCE_ARRAY = GameObjectRunner.RESOURCE_ARRAY_FULL;
        GameObjectRunner.VisualSolo = typeof (GameObjectRunner.VisualSolo_Full);
      }
    }

    public static short get(string name)
    {
      short num = -1;
      if (GameObjectRunner.Enums.TryGetValue(name, out num))
        return num;
      if (name.StartsWith("VISUAL_SOLO_"))
        num = (short) (int) Enum.Parse(GameObjectRunner.VisualSolo, name, false);
      GameObjectRunner.Enums.Add(name, num);
      return num;
    }

    public GameObjectRunner(MEdgeMap map, int type)
      : base(map, type)
    {
      this.m_localTransformNode = (microedition.m3g.Node) null;
      this.m_animationBlender = (AnimationBlender) null;
      this.m_animateBlender = true;
      this.m_originNode = (microedition.m3g.Node) null;
      this.m_originAnimPlayer = (AnimPlayer3D) null;
      this.m_originYStart = 0.0f;
      this.m_originYScale = 0.0f;
      this.m_originYTotal = 0.0f;
      this.m_visualType = 0;
      this.m_visualIndex = 0;
      this.m_3WayBlendValue = 0.0f;
      this.m_preOriginAnimatePosition = new MathVector();
      this.m_preOriginAnimPosNoOffset = new MathVector();
      this.m_facingDir = GameObjectRunner.FacingDir.FACING_RIGHT;
    }

    public GameObjectRunner(MEdgeMap map, int type, float posX, float posY, float posZ)
      : base(map, type, posX, posY, posZ)
    {
      this.m_localTransformNode = (microedition.m3g.Node) null;
      this.m_animationBlender = (AnimationBlender) null;
      this.m_animateBlender = true;
      this.m_originNode = (microedition.m3g.Node) null;
      this.m_originAnimPlayer = (AnimPlayer3D) null;
      this.m_originYStart = 0.0f;
      this.m_originYScale = 0.0f;
      this.m_originYTotal = 0.0f;
      this.m_visualType = 0;
      this.m_visualIndex = -1;
      this.m_3WayBlendValue = 0.0f;
      this.m_preOriginAnimatePosition = new MathVector();
      this.m_preOriginAnimPosNoOffset = new MathVector();
      this.m_facingDir = GameObjectRunner.FacingDir.FACING_RIGHT;
      this.setVerticalAcceleration(-9.8f);
    }

    protected void setVisualAssets(int modelId, int blenderId)
    {
      AppEngine canvas = AppEngine.getCanvas();
      AnimationManager3D animationManager3D = canvas.getAnimationManager3D();
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      World m3Gworld = canvas.getSceneGame().getM3GWorld();
      Group child = new Group();
      this.m_objectNode = (microedition.m3g.Node) child;
      m3Gworld.addChild((microedition.m3g.Node) child);
      this.m_localTransformNode = m3Gassets.loadModel(modelId, 4);
      child.addChild(this.m_localTransformNode);
      this.m_objectAnimPlayer = new AnimPlayer3D(animationManager3D);
      this.m_animationBlender = new AnimationBlender(blenderId);
      this.m_animationBlender.setNode(this.m_objectNode);
      this.setVisual(0, (int) GameObjectRunner.get("VISUAL_SOLO_IDLE"));
      M3GAssets.cacheSkinTransforms(this.m_objectNode);
    }

    public override void Destructor()
    {
      this.m_localTransformNode = (microedition.m3g.Node) null;
      this.m_animationBlender = (AnimationBlender) null;
      this.m_animateBlender = true;
      this.m_originNode = (microedition.m3g.Node) null;
      this.m_originAnimPlayer = (AnimPlayer3D) null;
      base.Destructor();
    }

    public float calcAnimProgress()
    {
      return (float) this.m_objectAnimPlayer.getAnimTime() / (float) this.m_objectAnimPlayer.getAnimDuration();
    }

    protected MathVector getPreOriginAnimPosNoOffset() => this.m_preOriginAnimPosNoOffset;

    protected MathVector getPreOriginAnimPosition() => this.m_preOriginAnimatePosition;

    public int getVisualDuration(int animType, int animIndex)
    {
      AnimationManager3D animationManager3D = AppEngine.getCanvas().getAnimationManager3D();
      switch (animType)
      {
        case 0:
          GameObjectData_SoloVisual soloAnim = AppEngine.getGameObjectRunnerData().getSoloAnim(animIndex);
          return animationManager3D.getAnimationDuration(soloAnim.animId);
        case 1:
          GameObjectData_OriginAnimVisual originAnim = AppEngine.getGameObjectRunnerData().getOriginAnim(animIndex);
          return animationManager3D.getAnimationDuration(originAnim.animId);
        case 2:
          GameObjectData_3ChannelBlendVisual channelBlendVisual = AppEngine.getGameObjectRunnerData().get3ChannelBlendAnim(animIndex);
          return animationManager3D.getAnimationDuration(channelBlendVisual.animId);
        default:
          return -1;
      }
    }

    protected int getVisualType() => this.m_visualType;

    protected int getVisualIndex() => this.m_visualIndex;

    protected int getVisual()
    {
      return GameObjectRunner.hashVisual(this.m_visualType, this.m_visualIndex, (int) this.m_facingDir);
    }

    protected static int hashVisual(int visualType, int visualIndex, int facingDir)
    {
      return visualIndex << 3 | visualType << 1 | Math.Max(0, facingDir);
    }

    protected void setVisual(int animCode)
    {
      this.setVisual(animCode >> 1 & 3, animCode >> 3);
      this.setFacingDir((GameObjectRunner.FacingDir) (((animCode & 1) << 1) - 1));
    }

    protected virtual void setVisual(int animType, int animIndex)
    {
      if (this.m_visualType == animType && this.m_visualIndex == animIndex)
        return;
      switch (animType)
      {
        case 0:
          this.m_visualType = animType;
          this.m_visualIndex = animIndex;
          GameObjectData_SoloVisual soloAnim = AppEngine.getGameObjectRunnerData().getSoloAnim(animIndex);
          this.m_objectAnimPlayer.startAnim(soloAnim.animId, soloAnim.animFlags);
          this.m_animationBlender.setChannelWeight(soloAnim.blendId, 1f, 3 | soloAnim.blendFlags);
          break;
        case 1:
          GameObjectData_OriginAnimVisual originAnim = AppEngine.getGameObjectRunnerData().getOriginAnim(animIndex);
          if (this.m_originAnimPlayer == null)
          {
            this.m_objectAnimPlayer.startAnim(originAnim.animId, 16);
            this.m_animationBlender.setChannelWeight(originAnim.blendId, 1f, 3 | originAnim.blendFlags);
            break;
          }
          if ((double) originAnim.yDist == 0.0)
          {
            MathVector offset = new MathVector();
            this.calculateOffsetAnimationOverallOffset(animIndex, ref offset);
            this.setOffsetVisual(animIndex, offset.y);
            break;
          }
          this.setOffsetVisual(animIndex, originAnim.yDist);
          break;
        case 2:
          this.m_visualType = animType;
          this.m_visualIndex = animIndex;
          GameObjectData_3ChannelBlendVisual channelBlendVisual = AppEngine.getGameObjectRunnerData().get3ChannelBlendAnim(animIndex);
          this.m_objectAnimPlayer.startAnim(channelBlendVisual.animId, 4);
          this.m_animationBlender.setChannelWeight(channelBlendVisual.midBlendId, 1f, 3);
          this.m_animationBlender.setChannelWeight(channelBlendVisual.lowBlendId, 0.0f, 1);
          this.m_animationBlender.setChannelWeight(channelBlendVisual.hiBlendId, 0.0f, 1);
          break;
      }
      this.m_3WayBlendValue = 0.0f;
    }

    protected virtual void setOffsetVisual(int animIndex, float yOffset)
    {
      if (this.m_visualType == 1 && this.m_visualIndex == animIndex)
        return;
      this.m_visualType = 1;
      this.m_visualIndex = animIndex;
      GameObjectData_OriginAnimVisual originAnim = AppEngine.getGameObjectRunnerData().getOriginAnim(animIndex);
      this.m_objectAnimPlayer.startAnim(originAnim.animId, 16);
      this.m_animationBlender.setChannelWeight(originAnim.blendId, 1f, 3 | originAnim.blendFlags);
      if (this.m_originAnimPlayer == null)
        return;
      MathVector offset1 = new MathVector();
      MathVector offset2 = new MathVector();
      this.calculateOffsetAnimationStartOffset(animIndex, ref offset1);
      this.calculateOffsetAnimationOverallOffset(animIndex, ref offset2);
      this.m_originAnimPlayer.startAnim(originAnim.originAnimId, 16);
      this.m_preOriginAnimatePosition.set(this.m_position.x - offset1.x, this.m_position.y, this.m_position.z);
      this.m_preOriginAnimPosNoOffset.CopyFrom(this.m_position);
      this.m_originYStart = offset1.y;
      this.m_originYScale = (double) yOffset != (double) offset2.y ? yOffset / offset2.y : 1f;
      this.m_originYTotal = yOffset;
    }

    protected bool isInOriginAnimation() => this.m_visualType == 1;

    protected float getOriginAnimationYOffset(int originAnimIndex)
    {
      GameObjectData_OriginAnimVisual originAnim = AppEngine.getGameObjectRunnerData().getOriginAnim(originAnimIndex);
      if ((double) originAnim.yDist != 0.0)
        return originAnim.yDist;
      MathVector offset = new MathVector();
      this.calculateOffsetAnimationOverallOffset(originAnimIndex, ref offset);
      return offset.y;
    }

    protected void updateMovementOriginAnimation(int facingDir, int timeStepMillis)
    {
      if (this.m_originAnimPlayer == null || this.m_visualType != 1)
        return;
      GameObjectData_OriginAnimVisual originAnim = AppEngine.getGameObjectRunnerData().getOriginAnim(this.m_visualIndex);
      this.m_originNode.getTranslation(ref this.translation);
      MathVector position1 = (new MathVector(this.getForwardDirection()) * ((float) facingDir * this.translation[2])) with
      {
        y = (this.translation[1] - this.m_originYStart) * this.m_originYScale
      } + this.m_preOriginAnimatePosition;
      if (originAnim.clipping)
      {
        this.m_globalShape.setPosition(position1);
        if (!this.m_map.intersects(this.m_globalShape, 0))
          this.m_position = position1;
      }
      else
        this.m_position = position1;
      if (originAnim.gravity)
      {
        float timeStepSecs = (float) timeStepMillis * (1f / 1000f);
        this.setForwardVelocity(0.0f);
        this.m_velocity.y += timeStepSecs * -9.8f;
        MathVector position2 = new MathVector(this.m_position);
        if ((this.m_map.moveObject(ref position2, ref this.m_velocity, timeStepSecs, this.m_globalShape) & 4) != 0)
          this.m_velocity.y = 0.0f;
        this.m_preOriginAnimatePosition.y += position2.y - this.m_position.y;
        this.m_position.y = position2.y;
      }
      else
        this.setNoVelocity();
    }

    public float get3WayAnimationBlendWeights() => this.m_3WayBlendValue;

    protected void set3WayAnimationBlendWeights(float blendWeight)
    {
      if (this.m_visualType != 2)
        return;
      this.m_3WayBlendValue = blendWeight;
      GameObjectData_3ChannelBlendVisual channelBlendVisual = AppEngine.getGameObjectRunnerData().get3ChannelBlendAnim(this.m_visualIndex);
      this.m_animationBlender.setChannelWeight(channelBlendVisual.lowBlendId, Math.Max(0.0f, Math.Min(1f, -blendWeight)));
      this.m_animationBlender.setChannelWeight(channelBlendVisual.midBlendId, Math.Max(0.0f, 1f - Math.Abs(blendWeight)));
      this.m_animationBlender.setChannelWeight(channelBlendVisual.hiBlendId, Math.Max(0.0f, Math.Min(1f, blendWeight)));
    }

    public void calculateOffsetAnimationStartOffset(int offsetAnimIndex, ref MathVector offset)
    {
      this.m_originAnimPlayer.startAnim(AppEngine.getGameObjectRunnerData().getOriginAnim(offsetAnimIndex).animId, 16);
      this.m_originNode.getTranslation(ref this.startPosition);
      offset.set((float) this.m_facingDir * this.startPosition[2], this.startPosition[1], 0.0f);
    }

    public void calculateOffsetAnimationOverallOffset(int offsetAnimIndex, ref MathVector offset)
    {
      this.m_originAnimPlayer.startAnim(AppEngine.getGameObjectRunnerData().getOriginAnim(offsetAnimIndex).animId, 16);
      this.m_originNode.getTranslation(ref this.startPosition);
      this.m_originAnimPlayer.setAnimTime(this.m_originAnimPlayer.getAnimDuration());
      this.m_originNode.getTranslation(ref this.endPosition);
      offset.set((float) this.m_facingDir * (this.endPosition[2] - this.startPosition[2]), this.endPosition[1] - this.startPosition[1], 0.0f);
    }

    public GameObjectRunner.FacingDir getFacingDir() => this.m_facingDir;

    public void setFacingDir(GameObjectRunner.FacingDir facingDir)
    {
      this.m_facingDir = facingDir;
      if (this.m_localTransformNode == null)
        return;
      this.m_localTransformNode.setOrientation((float) this.m_facingDir * 90f, 0.0f, 1f, 0.0f);
    }

    public void reverseFacingDir()
    {
      this.setFacingDir(this.m_facingDir == GameObjectRunner.FacingDir.FACING_LEFT ? GameObjectRunner.FacingDir.FACING_RIGHT : GameObjectRunner.FacingDir.FACING_LEFT);
    }

    public override void update(int timeStepMillis)
    {
      base.update(timeStepMillis);
      this.testVFC();
      if (this.m_passedVFC)
        this.m_objectNode.setRenderingEnable(true);
      else
        this.m_objectNode.setRenderingEnable(false);
    }

    public override void updateAnimation(int timeStepMillis)
    {
      if (this.m_passedVFC)
        this.m_objectAnimPlayer.updateAnim(timeStepMillis);
      if (this.m_objectNode != null)
        this.m_animationBlender.update(timeStepMillis, this.m_animateBlender);
      if (this.m_originAnimPlayer != null && this.m_visualType == 1)
        this.m_originAnimPlayer.updateAnim(timeStepMillis);
      if (this.isAnimating())
        return;
      if (this.m_visualType == 1 && this.m_originAnimPlayer != null)
      {
        GameObjectData_OriginAnimVisual originAnim = AppEngine.getGameObjectRunnerData().getOriginAnim(this.m_visualIndex);
        this.m_originAnimPlayer.setAnimTime(this.m_originAnimPlayer.getAnimDuration());
        this.m_originNode.getTranslation(ref this.endPosition);
        MathVector position = (new MathVector(this.getForwardDirection()) * ((float) this.getFacingDir() * this.endPosition[2])) with
        {
          y = this.m_originYTotal
        } + this.m_preOriginAnimatePosition;
        if (originAnim.clipping)
        {
          this.m_globalShape.setPosition(position);
          if (!this.m_map.intersects(this.m_globalShape, 0))
            this.m_position = position;
        }
        else
          this.m_position = position;
      }
      this.animationStopped();
    }

    protected virtual void animationStopped()
    {
    }

    public enum VisualSolo_Full
    {
      VISUAL_SOLO_IDLE,
      VISUAL_SOLO_IDLE_SNAP,
      VISUAL_SOLO_RUNNING_TURNING_START,
      VISUAL_SOLO_RUNNING_TURNING_END,
      VISUAL_SOLO_BALANCE_FALL,
      VISUAL_SOLO_BALANCE_FALLEN,
      VISUAL_SOLO_BALANCE_RECLIMB,
      VISUAL_SOLO_JUMP_LANDING,
      VISUAL_SOLO_JUMP_FROM_CLAMBER,
      VISUAL_SOLO_POWER_JUMP_START,
      VISUAL_SOLO_POWER_JUMP_LOOP,
      VISUAL_SOLO_SCALE_HALF_STEP,
      VISUAL_SOLO_STOPPING,
      VISUAL_SOLO_RUNNING,
      VISUAL_SOLO_RUNNING_UPHILL,
      VISUAL_SOLO_RISING,
      VISUAL_SOLO_RISING_SNAP,
      VISUAL_SOLO_FALLING,
      VISUAL_SOLO_ROLLING,
      VISUAL_SOLO_CRASH,
      VISUAL_SOLO_WALLCLIMB,
      VISUAL_SOLO_WALLRUN_LEFT,
      VISUAL_SOLO_WALLRUN_RIGHT,
      VISUAL_SOLO_WALLSLIDE,
      VISUAL_SOLO_WALLSLIDE_JUMP,
      VISUAL_SOLO_TRANS_HANG_TO_WALLSLIDE,
      VISUAL_SOLO_HANG,
      VISUAL_SOLO_SLIDING_START,
      VISUAL_SOLO_SLIDING,
      VISUAL_SOLO_SLIDING_END,
      VISUAL_SOLO_ZIP_LINE,
      VISUAL_SOLO_ZIPLINE_FAIL,
      VISUAL_SOLO_PRONE_IDLE,
      VISUAL_SOLO_PRONE_RECOVER,
      VISUAL_SOLO_FLYING_KICK,
      VISUAL_SOLO_FLYING_KICK_LOOP,
      VISUAL_SOLO_FLYING_KICK_IMPACT,
      VISUAL_SOLO_DYING,
      VISUAL_SOLO_DYING_RUNNING,
      VISUAL_SOLO_INTRO_STRETCHING,
      VISUAL_SOLO_INTRO_LANDING,
      VISUAL_SOLO_INTRO_2_1CAUTIOUS,
      VISUAL_SOLO_INTRO_3_2DAZED,
    }

    public enum VisualSolo_Trial
    {
      VISUAL_SOLO_IDLE,
      VISUAL_SOLO_IDLE_SNAP,
      VISUAL_SOLO_RUNNING_TURNING_START,
      VISUAL_SOLO_RUNNING_TURNING_END,
      VISUAL_SOLO_BALANCE_FALL,
      VISUAL_SOLO_BALANCE_FALLEN,
      VISUAL_SOLO_BALANCE_RECLIMB,
      VISUAL_SOLO_JUMP_LANDING,
      VISUAL_SOLO_JUMP_FROM_CLAMBER,
      VISUAL_SOLO_POWER_JUMP_START,
      VISUAL_SOLO_POWER_JUMP_LOOP,
      VISUAL_SOLO_SCALE_HALF_STEP,
      VISUAL_SOLO_STOPPING,
      VISUAL_SOLO_RUNNING_UPHILL,
      VISUAL_SOLO_RISING,
      VISUAL_SOLO_RISING_SNAP,
      VISUAL_SOLO_FALLING,
      VISUAL_SOLO_ROLLING,
      VISUAL_SOLO_CRASH,
      VISUAL_SOLO_WALLCLIMB,
      VISUAL_SOLO_WALLRUN_LEFT,
      VISUAL_SOLO_WALLRUN_RIGHT,
      VISUAL_SOLO_WALLSLIDE,
      VISUAL_SOLO_WALLSLIDE_JUMP,
      VISUAL_SOLO_TRANS_HANG_TO_WALLSLIDE,
      VISUAL_SOLO_HANG,
      VISUAL_SOLO_SLIDING_START,
      VISUAL_SOLO_SLIDING,
      VISUAL_SOLO_SLIDING_END,
      VISUAL_SOLO_ZIP_LINE,
      VISUAL_SOLO_ZIPLINE_FAIL,
      VISUAL_SOLO_PRONE_IDLE,
      VISUAL_SOLO_PRONE_RECOVER,
      VISUAL_SOLO_FLYING_KICK_LOOP,
      VISUAL_SOLO_FLYING_KICK_IMPACT,
      VISUAL_SOLO_DYING_RUNNING,
      VISUAL_SOLO_INTRO_STRETCHING,
    }

    public enum FacingDir
    {
      FACING_LEFT = -1, // 0xFFFFFFFF
      FACING_RIGHT = 1,
    }
  }
}
