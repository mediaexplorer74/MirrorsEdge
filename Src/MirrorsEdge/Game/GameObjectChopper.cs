// Decompiled with JetBrains decompiler
// Type: game.GameObjectChopper
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game.Splines;
using generic;
using microedition.m3g;
using support;
using System;

#nullable disable
namespace game
{
  public class GameObjectChopper : GameObject
  {
    public const float SCALE_SPEED_RANGE_SQ = 25f;
    private const int UPDATE_DIST_SQ = 625;
    private const float PATH_NODE_NEAR = 0.4f;
    private const float PATH_NODE_NEAR_SQ = 0.160000011f;
    private const float HEADING_LOOKAHEAD_DIST = 5f;
    private const float SHOOT_RANGE_SQ = 400f;
    private readonly int m_pathId;
    private Node m_visualNode;
    private ChunkDynamic m_dynamicNode;
    private Node m_largeRotor;
    private Node m_smallRotor;
    private Node m_leftMuzzle;
    private Node m_rightMuzzle;
    private Node m_light01;
    private Node m_light02;
    private Node m_light03;
    private Node m_light04;
    private float m_rotorRotate;
    private int m_distToPlayerSq;
    private Path m_path;
    private int m_lifeTime;
    private SmoothSpline m_spline = new SmoothSpline();
    private float m_splinePos;
    private float m_splineLength;
    private MathVector m_posOffset = new MathVector();
    private float m_lastSpeed;
    private float m_lastYaw;
    private SignalFilter m_rollFilter;
    private SignalFilter m_pitchFilter;
    private SignalFilter m_speedFilter;
    private int m_burstTimer;
    private int m_burstDelay;
    private int m_shotTimer;
    private bool m_rotorVis;
    private int m_lightTimer;
    private int m_farRotorSound;
    private int m_nearRotorSound;

    public GameObjectChopper(MEdgeMap map, float posX, float posY, float posZ, int pathId)
      : base(map, 6, posX, posY, posZ)
    {
      this.m_distToPlayerSq = 0;
      this.m_path = (Path) null;
      this.m_pathId = pathId;
      this.m_visualNode = (Node) null;
      this.m_dynamicNode = (ChunkDynamic) null;
      this.m_largeRotor = (Node) null;
      this.m_smallRotor = (Node) null;
      this.m_leftMuzzle = (Node) null;
      this.m_rightMuzzle = (Node) null;
      this.m_light01 = (Node) null;
      this.m_light02 = (Node) null;
      this.m_light03 = (Node) null;
      this.m_light04 = (Node) null;
      this.m_rotorRotate = 0.0f;
      this.m_spline = new SmoothSpline();
      this.m_splinePos = 0.0f;
      this.m_splineLength = 0.0f;
      this.m_lastSpeed = 0.0f;
      this.m_rollFilter = new SignalFilter(0, 1000f, 0.0f);
      this.m_pitchFilter = new SignalFilter(0, 1000f, 0.0f);
      this.m_speedFilter = new SignalFilter(0, 1000f, 0.0f);
      this.m_shotTimer = 0;
      this.m_burstTimer = 0;
      this.m_burstDelay = 2000;
      this.m_rotorVis = true;
      this.m_lastYaw = 0.0f;
      this.m_lightTimer = 1000;
      this.m_lifeTime = 0;
      this.m_posOffset = new MathVector();
      this.m_farRotorSound = -1;
      this.m_nearRotorSound = -1;
      this.setModel();
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(-9f, -1f, -6f, 9f, 6f, 6f);
      if (pathId != -1)
      {
        this.m_path = this.m_map.getPath(pathId);
        this.setupSpline();
      }
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (!soundManager.isEventLoaded((int) ResourceManager.get("SOUNDEVENT_SFX_BLADES_FAR")))
        soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_BLADES_FAR"));
      if (soundManager.isEventLoaded((int) ResourceManager.get("SOUNDEVENT_SFX_BLADES_NEAR")))
        return;
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_BLADES_NEAR"));
    }

    public override void Destructor()
    {
      if (this.m_rollFilter == null)
        return;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (this.m_farRotorSound != -1)
      {
        soundManager.stopEvent(this.m_farRotorSound);
        this.m_farRotorSound = -1;
      }
      if (this.m_nearRotorSound != -1)
      {
        soundManager.stopEvent(this.m_nearRotorSound);
        this.m_nearRotorSound = -1;
      }
      if (this.m_dynamicNode != null)
      {
        this.m_dynamicNode.Destructor();
        this.m_dynamicNode = (ChunkDynamic) null;
      }
      this.m_rollFilter.Destructor();
      this.m_rollFilter = (SignalFilter) null;
      this.m_pitchFilter.Destructor();
      this.m_pitchFilter = (SignalFilter) null;
      this.m_speedFilter.Destructor();
      this.m_speedFilter = (SignalFilter) null;
      this.m_path = (Path) null;
      this.m_visualNode = (Node) null;
      this.m_largeRotor = (Node) null;
      this.m_smallRotor = (Node) null;
      this.m_leftMuzzle = (Node) null;
      this.m_rightMuzzle = (Node) null;
      this.m_light01 = (Node) null;
      this.m_light02 = (Node) null;
      this.m_light03 = (Node) null;
      base.Destructor();
    }

    public override void resetLevel()
    {
      this.m_position = this.m_mapPlacementPosition;
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (this.m_farRotorSound != -1)
      {
        soundManager.stopEvent(this.m_farRotorSound);
        this.m_farRotorSound = -1;
      }
      if (this.m_nearRotorSound != -1)
      {
        soundManager.stopEvent(this.m_nearRotorSound);
        this.m_nearRotorSound = -1;
      }
      this.m_splinePos = 0.0f;
      this.m_lastSpeed = 0.0f;
      this.m_rollFilter.setSteadyState(0.0f);
      this.m_pitchFilter.setSteadyState(0.0f);
      this.m_speedFilter.setSteadyState(0.0f);
      this.m_shotTimer = 0;
      this.m_burstTimer = 0;
      this.m_rotorVis = true;
      this.m_lastYaw = 0.0f;
      this.m_lifeTime = 0;
    }

    public override void update(int timeStep)
    {
      this.m_distToPlayerSq = (int) this.getDistanceToSq((GameObject) this.m_map.getPlayerObject());
      if (this.shouldUpdate())
      {
        this.m_lifeTime += timeStep;
        if (this.m_path != null)
        {
          this.updateMovement(timeStep);
          this.updateOrientation(timeStep);
        }
        this.updateSound(timeStep);
        this.m_leftMuzzle.setRenderingEnable(false);
        this.m_rightMuzzle.setRenderingEnable(false);
        if (this.shouldShoot(timeStep))
          this.shootPlayer();
      }
      this.m_lightTimer -= timeStep;
      if (this.m_lightTimer < 0)
        this.m_lightTimer += 1000;
      bool enable = this.m_lightTimer <= 200;
      this.testVFC();
      if (this.m_passedVFC)
      {
        this.m_light01.setRenderingEnable(enable);
        this.m_light02.setRenderingEnable(enable);
        this.m_light03.setRenderingEnable(enable);
        this.m_light04.setRenderingEnable(enable);
        this.m_objectNode.setRenderingEnable(true);
      }
      else
        this.m_objectNode.setRenderingEnable(false);
    }

    private void setModel()
    {
      AppEngine canvas = AppEngine.getCanvas();
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      World m3Gworld = canvas.getSceneGame().getM3GWorld();
      this.m_visualNode = m3Gassets.loadModel((int) M3GAssets.get("MODEL_CHOPPER"), 4);
      Group child = new Group();
      child.addChild(this.m_visualNode);
      m3Gworld.addChild((Node) child);
      this.m_objectNode = (Node) child;
      this.m_largeRotor = (Node) this.m_visualNode.find(401);
      this.m_smallRotor = (Node) this.m_visualNode.find(402);
      this.m_leftMuzzle = (Node) this.m_visualNode.find(403);
      this.m_rightMuzzle = (Node) this.m_visualNode.find(404);
      this.m_light01 = (Node) this.m_visualNode.find(405);
      this.m_light02 = (Node) this.m_visualNode.find(406);
      this.m_light03 = (Node) this.m_visualNode.find(407);
      this.m_light04 = (Node) this.m_visualNode.find(408);
      this.m_leftMuzzle.setRenderingEnable(false);
      this.m_rightMuzzle.setRenderingEnable(false);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
    }

    private void setupSpline()
    {
      this.m_spline.addNode(new MathVector(this.m_mapPlacementPosition));
      int num = 0;
      int pointCount = this.m_path.getPointCount();
      for (int idx = 0; idx < pointCount; ++idx)
      {
        PathPoint pathPoint = new PathPoint(this.m_path.getPoint(idx));
        MathVector other = new MathVector(pathPoint.m_position);
        int time = pathPoint.m_time;
        this.m_spline.addNode(new MathVector(other));
        num += time;
      }
      this.m_spline.buildSpline();
      this.m_splineLength = this.m_spline.getMaxLength();
    }

    private bool shouldUpdate() => this.m_distToPlayerSq <= 625;

    private bool shouldShoot(int timeStep)
    {
      if (this.m_path == null)
        return false;
      if (this.m_burstTimer <= 0)
      {
        this.m_burstDelay -= timeStep;
        if (this.m_burstDelay <= 0)
          this.m_burstTimer = 2000;
      }
      if (this.m_burstDelay > 0 && this.m_burstTimer <= 0)
        return false;
      this.m_burstDelay = 2000;
      if (!this.playerInRange())
      {
        this.m_burstTimer = 0;
        this.m_burstDelay = 0;
      }
      if (this.m_path.getPoint(this.m_spline.getNodeAtLength(this.m_splinePos)).m_type != 0 || this.m_burstTimer <= 0)
        return false;
      this.m_burstTimer -= timeStep;
      this.m_shotTimer -= timeStep;
      return this.m_shotTimer <= 0;
    }

    private bool playerInRange() => (double) this.m_distToPlayerSq <= 400.0;

    private void updateMovement(int timeStep)
    {
      float num1 = (float) timeStep / 1000f;
      this.m_speedFilter.setTargetValue((float) this.m_path.getPoint(this.m_spline.getNodeAtLength(this.m_splinePos)).m_time);
      this.m_speedFilter.update(timeStep);
      float filteredValue = this.m_speedFilter.getFilteredValue();
      float num2 = 1f;
      GameObjectPlayer playerObject = this.m_map.getPlayerObject();
      MathVector position = playerObject.m_position;
      MathVector mathVector1 = playerObject.getForwardDirection().normalise() * (float) playerObject.getFacingDir();
      MathVector mathVector2 = this.m_spline.getPosition(this.m_splinePos + 1f / 1000f).normalise();
      if ((double) mathVector1.x * (double) mathVector2.x > 0.0)
      {
        MathVector mathVector3 = (this.m_position - position) with
        {
          z = 0.0f
        };
        float lengthSq = mathVector3.getLengthSq();
        if ((double) lengthSq > 25.0)
          num2 = (double) mathVector3.x * (double) mathVector2.x <= 0.0 ? Math.Max(0.0f, (float) (1.0 + (Math.Sqrt((double) lengthSq) - 5.0) / 20.0)) : Math.Max(0.0f, (float) (1.0 - (Math.Sqrt((double) lengthSq) - 5.0) / 20.0));
      }
      this.m_splinePos += filteredValue * num2 * num1;
      if ((double) this.m_splinePos >= (double) this.m_splineLength)
        this.m_splinePos -= this.m_splineLength;
      this.m_position = this.m_spline.getPosition(this.m_splinePos);
      float num3 = (float) this.m_lifeTime / 1000f;
      this.m_posOffset.x = (float) Math.Cos((double) num3 * 1.0) * 0.5f;
      this.m_posOffset.y = (float) Math.Sin((double) num3 * 2.0) * 0.25f;
      GameObjectChopper gameObjectChopper = this;
      gameObjectChopper.m_position = gameObjectChopper.m_position + this.m_posOffset;
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
    }

    private void updateOrientation(int timeStep)
    {
      float num1 = (float) timeStep / 1000f;
      MathVector mathVector = new MathVector(new MathVector(this.m_spline.getPosition(this.m_splinePos + 5f)) - this.m_position);
      float num2 = (float) Math.Atan2((double) mathVector.z, (double) mathVector.x) - 1.57079637f;
      while ((double) num2 < 0.0)
        num2 += 6.28318548f;
      while ((double) num2 > 2.0 * Math.PI)
        num2 -= 6.28318548f;
      this.m_rollFilter.setTargetValue((num2 - this.m_lastYaw) / num1 * 0.5f);
      this.m_rollFilter.update(timeStep);
      float filteredValue1 = this.m_rollFilter.getFilteredValue();
      this.m_lastYaw = num2;
      float filteredValue2 = this.m_speedFilter.getFilteredValue();
      this.m_pitchFilter.setTargetValue(((filteredValue2 - this.m_lastSpeed) / num1 + filteredValue2 * 1f) * 2f);
      this.m_pitchFilter.update(timeStep);
      float filteredValue3 = this.m_pitchFilter.getFilteredValue();
      this.m_lastSpeed = filteredValue2;
      float num3 = 57.2957764f;
      Transform transform = new Transform();
      transform.postRotate(num2 * num3, 0.0f, -1f, 0.0f);
      transform.postRotate(filteredValue1 * num3, 0.0f, 0.0f, 1f);
      transform.postRotate(filteredValue3, 1f, 0.0f, 0.0f);
      this.m_objectNode.setTransform(ref transform);
      this.m_rotorRotate += 2f * num1;
      if ((double) this.m_rotorRotate >= 2.0 * Math.PI)
        this.m_rotorRotate -= 6.28318548f;
      this.m_largeRotor.setOrientation(this.m_rotorRotate * num3, 0.0f, 1f, 0.0f);
      this.m_smallRotor.setOrientation(-this.m_rotorRotate * num3, 1f, 0.0f, 0.0f);
      this.m_rotorVis = !this.m_rotorVis;
      this.m_largeRotor.setRenderingEnable(this.m_rotorVis);
      this.m_smallRotor.setRenderingEnable(this.m_rotorVis);
    }

    private void updateSound(int timeStep)
    {
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (this.m_farRotorSound == -1 || !soundManager.isHandlePlaying(this.m_farRotorSound))
        this.m_farRotorSound = soundManager.playEventLooped((int) ResourceManager.get("SOUNDEVENT_SFX_BLADES_FAR"));
      if (this.m_nearRotorSound == -1 || !soundManager.isHandlePlaying(this.m_nearRotorSound))
        this.m_nearRotorSound = soundManager.playEventLooped((int) ResourceManager.get("SOUNDEVENT_SFX_BLADES_NEAR"));
      if (this.m_farRotorSound != -1)
        soundManager.setEventPosition(this.m_farRotorSound, this.m_position.x, this.m_position.y, this.m_position.z);
      if (this.m_nearRotorSound == -1)
        return;
      soundManager.setEventPosition(this.m_nearRotorSound, this.m_position.x, this.m_position.y, this.m_position.z);
    }

    private void shootPlayer()
    {
      GameObject playerObject = (GameObject) this.m_map.getPlayerObject();
      MathVector mathVector1 = new MathVector(this.m_position);
      MathVector mathVector2 = new MathVector(playerObject.m_position);
      MathVector mathVector3 = mathVector1 + TweakConstants.CHOPPER_GUN_OFFSET;
      MathVector velocity = new MathVector(mathVector2 + TweakConstants.CHOPPER_AIM_OFFSET - mathVector3);
      velocity.setLength(40f);
      this.m_map.addObject((GameObject) new GameObjectBullet(this.m_map, mathVector3.x, mathVector3.y, mathVector3.z, velocity));
      this.m_shotTimer = 150;
      velocity.rotateYAxis(-this.m_lastYaw);
      if ((double) velocity.x > 0.0)
        this.m_leftMuzzle.setRenderingEnable(true);
      else
        this.m_rightMuzzle.setRenderingEnable(true);
      AppEngine canvas = AppEngine.getCanvas();
      canvas.getSoundManager().playEventAt(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_GUNSHOTS"), (int) ResourceManager.get("SOUNDEVENTPOOL_GUNSHOTS_MINIGUN")), this.m_position.x, this.m_position.y, this.m_position.z);
    }
  }
}
