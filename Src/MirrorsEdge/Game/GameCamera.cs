
// Type: game.GameCamera
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using support;
using System;

#nullable disable
namespace game
{
  public class GameCamera
  {
    private const float MAX_SCALE_SPEED = 6f;
    private const float FOV_HIGH_SPEED = 75f;
    private const float FOV_ZERO_SPEED = 55f;
    private const float FOV_CUTSCENE = 55f;
    private const float UP_VECTOR_NOISE_FACTOR = 0.2f;
    private const float STIFFNESS = 25f;
    private const float DAMPENING = 12f;
    private const float MASS = 1f;
    private const float DEFAULT_UP_X = 0.0f;
    private const float DEFAULT_UP_Y = 1f;
    private const float DEFAULT_UP_Z = 0.0f;
    private const int INTERP_TIME_SHAKE = 350;
    private const int INTERP_TIME_SHAKE_SCALE = 400;
    private const int INTERP_TIME_LOOKAT = 400;
    private const int INTERP_TIME_LOOKFROM = 1000;
    private const int INTERP_TIME_VELOCITY_SCALE = 1900;
    private const int INTERP_TIME_ANIM_CROSSFADE = 1200;
    private const int INTERP_TIME_Y_BUMP_OFFSET = 1500;
    private const int INTERP_TIME_FOV = 500;
    private const int VERTICAL_BUMP_DURATION = 100;
    public const int STATE_NONE = -1;
    public const int STATE_KEYFRAMED = 0;
    public const int STATE_FOLLOW_GAMEOBJECT = 1;
    private int INTERP_TYPE_SHAKE;
    private int INTERP_TYPE_SHAKE_SCALE = 1;
    private int INTERP_TYPE_LOOKAT = 1;
    private int INTERP_TYPE_VELOCITY_SCALE = 1;
    private int INTERP_TYPE_ANIM_CROSSFADE;
    private int INTERP_TYPE_Y_BUMP_OFFSET = 1;
    private int INTERP_TYPE_FOV = 1;
    private float m_nearClip;
    private float m_farClip;
    public MathVector m_cameraPos;
    public MathVector m_desiredCameraPos;
    public MathVector m_lookAtPos;
    public MathVector m_velocity;
    private SignalFilter m_shakeXFilter;
    private SignalFilter m_shakeYFilter;
    private SignalFilter m_shakeZFilter;
    private SignalFilter m_shakeScaleFilter;
    private SignalFilter m_lookAtXFilter;
    private SignalFilter m_lookAtYFilter;
    private SignalFilter m_lookAtZFilter;
    private SignalFilter m_targetVelocityXFilter;
    private SignalFilter m_targetVelocityYFilter;
    private SignalFilter m_targetVelocityZFilter;
    private SignalFilter m_velocityScaleFilter;
    private SignalFilter m_animWeightFilter;
    private SignalFilter m_yBumpOffsetFilter;
    private SignalFilter m_fovFilter;
    private int m_state;
    private int m_shakeTimer;
    private int m_yBumpOffsetTimer;
    private GameObject m_targetGameObject;
    private Transform m_cameraTransform = new Transform();
    private float m_aspectRatio;
    private Camera m_m3gCamera;
    private AnimPlayer3D m_lookAtAnimPlayer;
    private AnimPlayer3D m_lookFromAnimPlayer;
    private Node m_cameraLookAtNode;
    private Node m_cameraLookFromNode;
    private static float[] translation = new float[3];
    public static float[] temp = new float[16];
    private bool m_dataAnim;
    private DataCameraAnim m_currentDataAnim;
    private float m_lastXPos;

    public void setClipping(float near, float far)
    {
      this.m_nearClip = near;
      this.m_farClip = far;
    }

    public float getFarClip() => this.m_farClip;

    public float getNearClip() => this.m_nearClip;

    public float getLookAtX() => this.m_lookAtPos.x;

    public float getLookAtY() => this.m_lookAtPos.y;

    public float getLookAtZ() => this.m_lookAtPos.z;

    public float getLookFromX() => this.m_cameraPos.x;

    public float getLookFromY() => this.m_cameraPos.y;

    public float getLookFromZ() => this.m_cameraPos.z;

    public float getFOV()
    {
      return this.m_fovFilter.getFilteredValue() + this.m_animWeightFilter.getFilteredValue() * (55f - this.m_fovFilter.getFilteredValue());
    }

    public GameCamera()
    {
      this.m_state = -1;
      this.m_targetGameObject = (GameObject) null;
      this.m_cameraTransform = new Transform();
      this.m_m3gCamera = (Camera) null;
      this.m_shakeTimer = 0;
      this.m_yBumpOffsetTimer = 0;
      this.m_lastXPos = 0.0f;
      this.m_aspectRatio = 0.0f;
      this.m_lookAtAnimPlayer = new AnimPlayer3D(AppEngine.getCanvas().getAnimationManager3D());
      this.m_lookFromAnimPlayer = new AnimPlayer3D(AppEngine.getCanvas().getAnimationManager3D());
      this.m_cameraLookAtNode = (Node) null;
      this.m_cameraLookFromNode = (Node) null;
      this.m_dataAnim = false;
      this.m_currentDataAnim = (DataCameraAnim) null;
      this.m_cameraPos = new MathVector(0.0f, 0.0f, 1f);
      this.m_desiredCameraPos = new MathVector(0.0f, 0.0f, 1f);
      this.m_lookAtPos = new MathVector(0.0f, 0.0f, 0.0f);
      this.m_velocity = new MathVector(0.0f, 0.0f, 0.0f);
      this.m_nearClip = 0.0f;
      this.m_farClip = 1f;
      this.m_lookAtXFilter = new SignalFilter(this.INTERP_TYPE_LOOKAT, 400f, 0.0f);
      this.m_lookAtYFilter = new SignalFilter(this.INTERP_TYPE_LOOKAT, 400f, 0.0f);
      this.m_lookAtZFilter = new SignalFilter(this.INTERP_TYPE_LOOKAT, 400f, 0.0f);
      this.m_targetVelocityXFilter = new SignalFilter(this.INTERP_TYPE_LOOKAT, 2000f, 0.0f);
      this.m_targetVelocityYFilter = new SignalFilter(this.INTERP_TYPE_LOOKAT, 2000f, 0.0f);
      this.m_targetVelocityZFilter = new SignalFilter(this.INTERP_TYPE_LOOKAT, 2000f, 0.0f);
      this.m_shakeXFilter = new SignalFilter(this.INTERP_TYPE_SHAKE, 350f, 0.0f);
      this.m_shakeYFilter = new SignalFilter(this.INTERP_TYPE_SHAKE, 350f, 0.0f);
      this.m_shakeZFilter = new SignalFilter(this.INTERP_TYPE_SHAKE, 350f, 0.0f);
      this.m_shakeScaleFilter = new SignalFilter(this.INTERP_TYPE_SHAKE_SCALE, 400f, 0.0f);
      this.m_velocityScaleFilter = new SignalFilter(this.INTERP_TYPE_VELOCITY_SCALE, 1900f, 0.0f);
      this.m_animWeightFilter = new SignalFilter(this.INTERP_TYPE_ANIM_CROSSFADE, 1200f, 0.0f);
      this.m_yBumpOffsetFilter = new SignalFilter(this.INTERP_TYPE_Y_BUMP_OFFSET, 1500f, 0.0f);
      this.m_fovFilter = new SignalFilter(this.INTERP_TYPE_FOV, 500f, 55f);
      this.m_aspectRatio = (float) AppEngine.getCanvas().getWidth() / (float) AppEngine.getCanvas().getHeight();
    }

    public void Destructor()
    {
      this.m_lookAtAnimPlayer.Destructor();
      this.m_lookFromAnimPlayer.Destructor();
      this.m_lookAtAnimPlayer = (AnimPlayer3D) null;
      this.m_lookFromAnimPlayer = (AnimPlayer3D) null;
      this.m_cameraLookAtNode = (Node) null;
      this.m_cameraLookFromNode = (Node) null;
      this.m_lookAtXFilter.Destructor();
      this.m_lookAtXFilter = (SignalFilter) null;
      this.m_lookAtYFilter.Destructor();
      this.m_lookAtYFilter = (SignalFilter) null;
      this.m_lookAtZFilter.Destructor();
      this.m_lookAtZFilter = (SignalFilter) null;
      this.m_targetVelocityXFilter.Destructor();
      this.m_targetVelocityXFilter = (SignalFilter) null;
      this.m_targetVelocityYFilter.Destructor();
      this.m_targetVelocityYFilter = (SignalFilter) null;
      this.m_targetVelocityZFilter.Destructor();
      this.m_targetVelocityZFilter = (SignalFilter) null;
      this.m_shakeXFilter.Destructor();
      this.m_shakeXFilter = (SignalFilter) null;
      this.m_shakeYFilter.Destructor();
      this.m_shakeYFilter = (SignalFilter) null;
      this.m_shakeZFilter.Destructor();
      this.m_shakeZFilter = (SignalFilter) null;
      this.m_shakeScaleFilter.Destructor();
      this.m_shakeScaleFilter = (SignalFilter) null;
      this.m_velocityScaleFilter.Destructor();
      this.m_velocityScaleFilter = (SignalFilter) null;
      this.m_animWeightFilter.Destructor();
      this.m_animWeightFilter = (SignalFilter) null;
      this.m_yBumpOffsetFilter.Destructor();
      this.m_yBumpOffsetFilter = (SignalFilter) null;
      this.m_fovFilter.Destructor();
      this.m_fovFilter = (SignalFilter) null;
    }

    public void loadM3GAnims()
    {
      Node node = AppEngine.getCanvas().getResourceManager().loadM3GNode((int) ResourceManager.get("IDI_CAMERA_M3G"));
      Object3D object3D1 = node.find(176);
      Object3D object3D2 = node.find(175);
      this.m_cameraLookFromNode = Node.m3g_cast(object3D1);
      this.m_cameraLookAtNode = Node.m3g_cast(object3D2);
      M3GAssets.orphanNode(this.m_cameraLookFromNode);
      M3GAssets.orphanNode(this.m_cameraLookAtNode);
      this.m_lookAtAnimPlayer.setNode(this.m_cameraLookAtNode);
      this.m_lookFromAnimPlayer.setNode(this.m_cameraLookFromNode);
    }

    public void setM3GCamera(Camera m3gCam)
    {
      this.m_m3gCamera = m3gCam;
      this.recalcCameraProjection();
    }

    public Camera getM3GCamera() => this.m_m3gCamera;

    public float getTargetSpeedFactor() => this.m_velocityScaleFilter.getFilteredValue();

    public void update(int timeStep)
    {
      this.updateCameraProjection(timeStep);
      this.updateShaking(timeStep);
      this.updateVerticalBump(timeStep);
      if (!this.m_dataAnim)
        this.updateFollowGameObject(timeStep);
      this.updateCameraAnim(timeStep);
      this.m_lookAtXFilter.update(timeStep);
      this.m_lookAtYFilter.update(timeStep);
      this.m_lookAtZFilter.update(timeStep);
      this.m_animWeightFilter.update(timeStep);
      if (!this.isAnimating())
      {
        MathVector cameraPos = this.m_cameraPos;
        MathVector mathVector = (cameraPos - this.m_desiredCameraPos) * -25f - this.m_velocity * 12f;
        float num = (float) timeStep / 1000f;
        this.m_velocity += mathVector * 1f * num;
        this.m_cameraPos = cameraPos + this.m_velocity * num;
      }
      this.recalcCameraTransform();
      this.m_cameraTransform.postRotate(0.0f, 0.0f, 0.0f, 1f);
      this.m_m3gCamera.setTransform(ref this.m_cameraTransform);
      this.m_lastXPos = this.m_targetGameObject.m_position.x;
    }

    public void updateFollowGameObject(int timeStep)
    {
      this.updateFollowGameObject(timeStep, false);
    }

    public void updateFollowGameObject(int timeStep, bool snap)
    {
      MathVector cameraLookatOffset = TweakConstants.CAMERA_LOOKAT_OFFSET;
      MathVector cameraLookfromOffset = TweakConstants.CAMERA_LOOKFROM_OFFSET;
      float forwardVelocity = this.m_targetGameObject.getForwardVelocity();
      this.m_velocityScaleFilter.setTargetValue((double) this.m_targetGameObject.m_position.x == (double) this.m_lastXPos ? 0.0f : Math.Max(-6f, Math.Min(forwardVelocity, 6f)) / 6f);
      this.m_velocityScaleFilter.update(timeStep);
      float gameTimeFactor = AppEngine.getCanvas().getSceneGame().getGameTimeFactor();
      MathVector mathVector1 = new MathVector(this.m_targetGameObject.m_position);
      MathVector velocity = this.m_targetGameObject.getVelocity();
      this.m_targetVelocityXFilter.setTargetValue(velocity.x);
      this.m_targetVelocityYFilter.setTargetValue(velocity.y);
      this.m_targetVelocityZFilter.setTargetValue(velocity.z);
      this.m_targetVelocityXFilter.update(timeStep);
      this.m_targetVelocityYFilter.update(timeStep);
      this.m_targetVelocityZFilter.update(timeStep);
      float num1 = this.m_targetVelocityXFilter.getFilteredValue() * 0.4f * gameTimeFactor;
      float num2 = this.m_targetVelocityYFilter.getFilteredValue() * 0.0f * gameTimeFactor;
      float num3 = this.m_targetVelocityZFilter.getFilteredValue() * 0.4f * gameTimeFactor;
      MathVector mathVector2 = new MathVector(mathVector1.x + num1 + cameraLookatOffset.x, mathVector1.y + num2 + cameraLookatOffset.y, mathVector1.z + num3 + cameraLookatOffset.z);
      if (snap)
      {
        this.m_lookAtXFilter.setSteadyState(mathVector2.x);
        this.m_lookAtYFilter.setSteadyState(mathVector2.y);
        this.m_lookAtZFilter.setSteadyState(mathVector2.z);
      }
      else
      {
        this.m_lookAtXFilter.setTargetValue(mathVector2.x);
        this.m_lookAtYFilter.setTargetValue(mathVector2.y);
        this.m_lookAtZFilter.setTargetValue(mathVector2.z);
      }
      this.m_desiredCameraPos.x = this.m_lookAtXFilter.getFilteredValue();
      this.m_desiredCameraPos.y = this.m_lookAtYFilter.getFilteredValue();
      this.m_desiredCameraPos.z = this.m_lookAtZFilter.getFilteredValue();
      this.m_desiredCameraPos.z += cameraLookfromOffset.z * gameTimeFactor;
      this.m_desiredCameraPos.x += cameraLookfromOffset.x;
      this.m_desiredCameraPos.y += cameraLookfromOffset.y;
      if (!snap)
        return;
      this.m_cameraPos.CopyFrom(this.m_desiredCameraPos);
    }

    public void updateCameraAnim(int timeStep)
    {
      if (this.m_dataAnim)
      {
        this.updateDataAnim(timeStep);
      }
      else
      {
        if (this.isAnimating())
        {
          this.m_lookAtAnimPlayer.updateAnim(timeStep);
          this.m_lookFromAnimPlayer.updateAnim(timeStep);
        }
        if (this.isAnimating())
          return;
        this.m_animWeightFilter.setTargetValue(0.0f);
      }
    }

    private void updateCameraProjection(int timeStep)
    {
      this.m_fovFilter.setTargetValue((float) (55.0 + 20.0 * (double) Math.Abs(this.getTargetSpeedFactor())));
      this.m_fovFilter.update(timeStep);
      this.recalcCameraProjection();
    }

    private void updateShaking(int timeStep)
    {
      this.m_shakeTimer -= timeStep;
      if (this.m_shakeTimer > 0)
      {
        this.m_shakeXFilter.setTargetValue(0.01f * (float) (100 - AppEngine.getCanvas().rand(0, 200)));
        this.m_shakeYFilter.setTargetValue(0.01f * (float) (100 - AppEngine.getCanvas().rand(0, 200)));
        this.m_shakeZFilter.setTargetValue(0.01f * (float) (100 - AppEngine.getCanvas().rand(0, 200)));
      }
      else
      {
        this.m_shakeXFilter.setTargetValue(0.0f);
        this.m_shakeYFilter.setTargetValue(0.0f);
        this.m_shakeZFilter.setTargetValue(0.0f);
      }
      this.m_shakeXFilter.update(timeStep);
      this.m_shakeYFilter.update(timeStep);
      this.m_shakeZFilter.update(timeStep);
      this.m_shakeScaleFilter.update(timeStep);
    }

    private void updateVerticalBump(int timeStep)
    {
      this.m_yBumpOffsetTimer -= timeStep;
      if (this.m_yBumpOffsetTimer < 0)
        this.m_yBumpOffsetFilter.setTargetValue(0.0f);
      this.m_yBumpOffsetFilter.update(timeStep);
    }

    private void recalcCameraTransform()
    {
      float filteredValue1 = this.m_shakeScaleFilter.getFilteredValue();
      float num1 = filteredValue1 * this.m_shakeXFilter.getFilteredValue();
      float num2 = filteredValue1 * this.m_shakeYFilter.getFilteredValue();
      float num3 = filteredValue1 * this.m_shakeZFilter.getFilteredValue();
      float filteredValue2 = this.m_yBumpOffsetFilter.getFilteredValue();
      float num4 = this.m_cameraPos.x + num1;
      float num5 = this.m_cameraPos.y + num2 + filteredValue2;
      float num6 = this.m_cameraPos.z + num3;
      float filteredValue3 = this.m_lookAtXFilter.getFilteredValue();
      float filteredValue4 = this.m_lookAtYFilter.getFilteredValue();
      float filteredValue5 = this.m_lookAtZFilter.getFilteredValue();
      float num7 = (float) (0.0 + 0.20000000298023224 * (double) num1);
      float num8 = (float) (1.0 + 0.20000000298023224 * (double) num2);
      float num9 = (float) (0.0 + 0.20000000298023224 * (double) num3);
      this.m_cameraLookFromNode.getTranslation(ref GameCamera.translation);
      float num10 = GameCamera.translation[2] + this.m_targetGameObject.m_position.x;
      float num11 = GameCamera.translation[1] + this.m_targetGameObject.m_position.y;
      float num12 = -GameCamera.translation[0] + this.m_targetGameObject.m_position.z;
      this.m_cameraLookAtNode.getTranslation(ref GameCamera.translation);
      float num13 = GameCamera.translation[2] + this.m_targetGameObject.m_position.x;
      float num14 = GameCamera.translation[1] + this.m_targetGameObject.m_position.y;
      float num15 = -GameCamera.translation[0] + this.m_targetGameObject.m_position.z;
      float filteredValue6 = this.m_animWeightFilter.getFilteredValue();
      this.m_cameraPos.x = num4 + filteredValue6 * (num10 - num4);
      this.m_cameraPos.y = num5 + filteredValue6 * (num11 - num5);
      this.m_cameraPos.z = num6 + filteredValue6 * (num12 - num6);
      this.m_lookAtPos.x = filteredValue3 + filteredValue6 * (num13 - filteredValue3);
      this.m_lookAtPos.y = filteredValue4 + filteredValue6 * (num14 - filteredValue4);
      this.m_lookAtPos.z = filteredValue5 + filteredValue6 * (num15 - filteredValue5);
      GameCamera.createLookAtTransform(this.m_cameraTransform, this.m_cameraPos.x, this.m_cameraPos.y, this.m_cameraPos.z, this.m_lookAtPos.x, this.m_lookAtPos.y, this.m_lookAtPos.z, num7 + filteredValue6 * (0.0f - num7), num8 + filteredValue6 * (1f - num8), num9 + filteredValue6 * (0.0f - num9));
    }

    private void recalcCameraProjection()
    {
      float filteredValue = this.m_animWeightFilter.getFilteredValue();
      float nearClip = this.m_nearClip;
      float lengthSq = (this.m_cameraPos - new MathVector(this.m_lookAtXFilter.getFilteredValue(), this.m_lookAtYFilter.getFilteredValue(), this.m_lookAtZFilter.getFilteredValue())).getLengthSq();
      if ((double) lengthSq < 81.0)
      {
        float num = (float) Math.Sqrt((double) lengthSq) * 0.3f;
        if ((double) num < (double) nearClip)
          nearClip = num;
      }
      this.m_m3gCamera.setPerspective(this.m_fovFilter.getFilteredValue() + filteredValue * (55f - this.m_fovFilter.getFilteredValue()), this.m_aspectRatio, nearClip, this.m_farClip);
    }

    public static void createLookAtTransform(
      Transform cameraTransform,
      float fromX,
      float fromY,
      float fromZ,
      float atX,
      float atY,
      float atZ,
      float upX,
      float upY,
      float upZ)
    {
      float num1 = fromX;
      float num2 = fromY;
      float num3 = fromZ;
      float num4 = num1 - atX;
      float num5 = num2 - atY;
      float num6 = num3 - atZ;
      float num7 = 1f / (float) Math.Sqrt((double) num4 * (double) num4 + (double) num5 * (double) num5 + (double) num6 * (double) num6);
      float num8 = num4 * num7;
      float num9 = num5 * num7;
      float num10 = num6 * num7;
      bool flag = (double) Math.Abs((float) ((double) upX * (double) upX + (double) upY * (double) upY + (double) upZ * (double) upZ) - 1f) < 1.0000000116860974E-07;
      float num11 = (float) ((double) upY * (double) num10 - (double) upZ * (double) num9);
      float num12 = (float) ((double) upZ * (double) num8 - (double) upX * (double) num10);
      float num13 = (float) ((double) upX * (double) num9 - (double) upY * (double) num8);
      if (!flag)
      {
        float num14 = 1f / (float) Math.Sqrt((double) num11 * (double) num11 + (double) num12 * (double) num12 + (double) num13 * (double) num13);
        num11 *= num14;
        num12 *= num14;
        num13 *= num14;
      }
      float num15 = (float) ((double) num9 * (double) num13 - (double) num10 * (double) num12);
      float num16 = (float) ((double) num10 * (double) num11 - (double) num8 * (double) num13);
      float num17 = (float) ((double) num8 * (double) num12 - (double) num9 * (double) num11);
      GameCamera.temp[0] = num11;
      GameCamera.temp[1] = num12;
      GameCamera.temp[2] = num13;
      GameCamera.temp[3] = 0.0f;
      GameCamera.temp[4] = num15;
      GameCamera.temp[5] = num16;
      GameCamera.temp[6] = num17;
      GameCamera.temp[7] = 0.0f;
      GameCamera.temp[8] = num8;
      GameCamera.temp[9] = num9;
      GameCamera.temp[10] = num10;
      GameCamera.temp[11] = 0.0f;
      GameCamera.temp[12] = 0.0f;
      GameCamera.temp[13] = 0.0f;
      GameCamera.temp[14] = 0.0f;
      GameCamera.temp[15] = 1f;
      cameraTransform.set(GameCamera.temp);
      cameraTransform.postTranslate(-num1, -num2, -num3);
      cameraTransform.invert();
    }

    public void startSmoothTracking(GameObject gameObject, bool snap)
    {
      if (snap)
        this.m_animWeightFilter.setSteadyState(0.0f);
      else
        this.m_animWeightFilter.setTargetValue(0.0f);
      this.m_targetGameObject = gameObject;
      this.updateFollowGameObject(0, snap);
      this.recalcCameraTransform();
      this.stateTransition(1);
    }

    public void startCameraAnimation(int animId, bool snap)
    {
      if (snap)
        this.m_animWeightFilter.setSteadyState(1f);
      else
        this.m_animWeightFilter.setTargetValue(1f);
      this.m_lookAtAnimPlayer.startAnim(animId, 16);
      this.m_lookFromAnimPlayer.startAnim(animId, 16);
      this.recalcCameraTransform();
    }

    public bool isAnimating()
    {
      return this.m_lookFromAnimPlayer.isAnimating() || this.m_lookAtAnimPlayer.isAnimating() || this.m_dataAnim;
    }

    public void startShake(float shakeMagnitude, int shakeDuration)
    {
      this.m_shakeScaleFilter.setTargetValue(shakeMagnitude);
      this.m_shakeTimer = shakeDuration;
    }

    public void startVerticalBump(float bumpMagnitude)
    {
      this.m_yBumpOffsetTimer = 100;
      this.m_yBumpOffsetFilter.setTargetValue(bumpMagnitude);
    }

    private void stateTransition(int newState) => this.m_state = newState;

    public void updateDataAnim(int timeStep)
    {
      this.m_velocityScaleFilter.setTargetValue(0.0f);
      this.m_velocityScaleFilter.update(timeStep);
      if (this.m_currentDataAnim.update(timeStep))
        return;
      this.m_dataAnim = false;
      this.m_currentDataAnim = (DataCameraAnim) null;
    }

    public void playDataAnim(DataCameraAnim anim) => this.playDataAnim(anim, (GameObject) null);

    public void playDataAnim(DataCameraAnim anim, GameObject target)
    {
      this.m_dataAnim = true;
      this.m_currentDataAnim = anim;
      this.m_currentDataAnim.play(this, target);
    }

    public void setLookFromLookAt(
      float lookFromX,
      float lookFromY,
      float lookFromZ,
      float lookAtX,
      float lookAtY,
      float lookAtZ,
      bool snap)
    {
      if (snap)
      {
        this.m_cameraPos.x = lookFromX;
        this.m_cameraPos.y = lookFromY;
        this.m_cameraPos.z = lookFromZ;
        this.m_lookAtXFilter.setSteadyState(lookAtX);
        this.m_lookAtYFilter.setSteadyState(lookAtY);
        this.m_lookAtZFilter.setSteadyState(lookAtZ);
      }
      else
      {
        this.m_desiredCameraPos.x = lookFromX;
        this.m_desiredCameraPos.y = lookFromY;
        this.m_desiredCameraPos.z = lookFromZ;
        this.m_lookAtXFilter.setTargetValue(lookAtX);
        this.m_lookAtYFilter.setTargetValue(lookAtY);
        this.m_lookAtZFilter.setTargetValue(lookAtZ);
      }
    }

    public int getAnimTimeLeft()
    {
      return Math.Max(this.m_lookAtAnimPlayer.getAnimDuration() - this.m_lookAtAnimPlayer.getAnimTime(), this.m_lookFromAnimPlayer.getAnimDuration() - this.m_lookFromAnimPlayer.getAnimTime());
    }

    public bool checkDistance(MathVector min, MathVector max)
    {
      float num = Math.Max(Math.Abs(this.m_cameraPos.x - min.x), Math.Abs(this.m_cameraPos.x - max.x));
      return AppEngine.isUnderground() ? (double) num < (double) this.m_farClip / 2.0 : (double) num < (double) this.m_farClip * 2.0;
    }
  }
}
