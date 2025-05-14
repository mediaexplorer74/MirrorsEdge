// Decompiled with JetBrains decompiler
// Type: game.DataCameraAnim
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game.Splines;
using midp;

#nullable disable
namespace game
{
  public class DataCameraAnim
  {
    private GameCamera m_cam;
    private GameObject m_target;
    private DataCameraAnimStep[] m_steps;
    private int m_targetId;
    private bool m_animating;
    private int m_currentSplineTime;
    private int m_currentRealTime;
    private int m_currentStep;
    private int m_totalTime;
    private TimedSpline m_lookFromSpline;
    private TimedSpline m_lookAtSpline;

    public DataCameraAnim(DataInputStream dis)
    {
      this.m_cam = (GameCamera) null;
      this.m_target = (GameObject) null;
      this.m_targetId = 0;
      this.m_currentRealTime = 0;
      this.m_currentSplineTime = 0;
      this.m_currentStep = 0;
      this.m_totalTime = 0;
      this.m_animating = false;
      this.m_lookFromSpline = new TimedSpline();
      this.m_lookAtSpline = new TimedSpline();
      this.m_targetId = (int) dis.readByte();
      int length = (int) dis.readShort();
      this.m_steps = new DataCameraAnimStep[length];
      this.m_totalTime = 0;
      for (int index = 0; index < length; ++index)
      {
        this.m_steps[index] = new DataCameraAnimStep(dis, this.m_totalTime);
        this.m_totalTime += this.m_steps[index].m_moveTime + this.m_steps[index].m_pauseTime;
      }
    }

    public void Destructor()
    {
      int length = this.m_steps.Length;
      for (int index = 0; index < length; ++index)
        this.m_steps[index].Destructor();
      this.m_steps = (DataCameraAnimStep[]) null;
      this.m_lookFromSpline.Destructor();
      this.m_lookAtSpline.Destructor();
      this.m_lookFromSpline = (TimedSpline) null;
      this.m_lookAtSpline = (TimedSpline) null;
    }

    public void play(GameCamera cam) => this.play(cam, (GameObject) null);

    public void play(GameCamera cam, GameObject target)
    {
      this.m_target = target;
      this.m_cam = cam;
      this.m_currentRealTime = 0;
      this.m_currentSplineTime = 0;
      this.m_currentStep = 0;
      this.m_animating = true;
      this.m_lookFromSpline.clear();
      this.m_lookAtSpline.clear();
      this.m_lookFromSpline.addNode(new MathVector(this.m_cam.getLookFromX(), this.m_cam.getLookFromY(), this.m_cam.getLookFromZ()), 0);
      this.m_lookAtSpline.addNode(new MathVector(this.m_cam.getLookAtX(), this.m_cam.getLookAtY(), this.m_cam.getLookAtZ()), 0);
      for (int index = 0; index < this.m_steps.Length; ++index)
      {
        MathVector pos1 = new MathVector(this.m_steps[index].m_tLookFrom[0], this.m_steps[index].m_tLookFrom[1], this.m_steps[index].m_tLookFrom[2]);
        MathVector pos2 = new MathVector(this.m_steps[index].m_tLookAt[0], this.m_steps[index].m_tLookAt[1], this.m_steps[index].m_tLookAt[2]);
        this.m_lookFromSpline.addNode(pos1, this.m_steps[index].m_moveTime);
        this.m_lookAtSpline.addNode(pos2, this.m_steps[index].m_moveTime);
      }
      this.m_lookFromSpline.buildSpline();
      this.m_lookAtSpline.buildSpline();
    }

    public void stop() => this.m_animating = false;

    public bool update(int timeStep)
    {
      if (this.m_animating)
      {
        MathVector mathVector1 = new MathVector(this.m_cam.getLookFromX(), this.m_cam.getLookFromY(), this.m_cam.getLookFromZ());
        MathVector mathVector2 = new MathVector(this.m_cam.getLookAtX(), this.m_cam.getLookAtY(), this.m_cam.getLookAtZ());
        int num1 = timeStep;
        int currentRealTime = this.m_currentRealTime;
        int num2 = currentRealTime + timeStep;
        do
        {
          int beginTime = this.m_steps[this.m_currentStep].m_beginTime;
          int pauseAt = this.m_steps[this.m_currentStep].m_pauseAt;
          int endTime = this.m_steps[this.m_currentStep].m_endTime;
          int num3 = num2 > endTime ? endTime - currentRealTime : num1;
          int num4 = currentRealTime + num3;
          int num5 = num4 > pauseAt ? num4 - pauseAt : 0;
          int num6 = num3 - num5;
          if (num6 > 0)
          {
            this.m_currentSplineTime += num6;
            mathVector1 = this.m_lookFromSpline.getPosition(this.m_currentSplineTime);
            if (this.m_target == null)
              mathVector2 = this.m_lookAtSpline.getPosition(this.m_currentSplineTime);
          }
          else if (beginTime == pauseAt)
          {
            mathVector1 = this.m_lookFromSpline.getNodePosition(this.m_currentStep + 1);
            if (this.m_target == null)
              mathVector2 = this.m_lookAtSpline.getNodePosition(this.m_currentStep + 1);
          }
          currentRealTime += num3;
          num1 -= num3;
          if (num1 > 0)
            ++this.m_currentStep;
        }
        while (num1 > 0 && this.m_currentStep < this.m_steps.Length);
        this.m_currentRealTime = num2;
        if (this.m_currentRealTime > this.m_totalTime)
          this.m_animating = false;
        if (this.m_target != null)
        {
          mathVector2 = new MathVector(this.m_target.m_position.x, this.m_target.m_position.y, this.m_target.m_position.z);
          mathVector2.x += TweakConstants.CAMERA_LOOKAT_OFFSET.x;
          mathVector2.y += TweakConstants.CAMERA_LOOKAT_OFFSET.y;
          mathVector2.z += TweakConstants.CAMERA_LOOKAT_OFFSET.z;
        }
        this.m_cam.setLookFromLookAt(mathVector1.x, mathVector1.y, mathVector1.z, mathVector2.x, mathVector2.y, mathVector2.z, true);
      }
      return this.m_animating;
    }

    public int getTargetID() => this.m_targetId;
  }
}
