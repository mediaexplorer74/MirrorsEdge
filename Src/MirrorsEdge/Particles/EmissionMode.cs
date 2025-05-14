// Decompiled with JetBrains decompiler
// Type: particles.EmissionMode
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;
using System;

#nullable disable
namespace particles
{
  public class EmissionMode : Transformable
  {
    private string m_id;
    private float m_emissionRate;
    private float m_emissionSpeed;
    private float m_emissionSpeedDeviation;
    private float[] m_acceleration;
    private float m_spreadAngle;
    private float m_spreadAngleDeviation;

    public override int getM3GUniqueClassID() => 0;

    public EmissionMode()
    {
      this.m_acceleration = (float[]) null;
      this.setSpeed(1f, 0.1f);
      this.setRate(1f);
      this.setSpreadAngle(10f, 10f);
      this.setAcceleration((float[]) null);
    }

    public override void Destructor()
    {
      this.setAcceleration((float[]) null);
      this.m_acceleration = (float[]) null;
    }

    public void setId(string id) => this.m_id = id;

    public string getId() => this.m_id;

    public void setRate(float rate) => this.m_emissionRate = rate;

    public float getRate() => this.m_emissionRate;

    public void setSpeed(float speed, float deviation)
    {
      this.m_emissionSpeed = speed;
      this.m_emissionSpeedDeviation = deviation;
    }

    public float getSpeed() => this.m_emissionSpeed;

    public float getSpeedDeviation() => this.m_emissionSpeedDeviation;

    public void setSpreadAngle(float angle, float deviation)
    {
      this.m_spreadAngle = angle;
      this.m_spreadAngleDeviation = deviation;
    }

    public float getSpreadAngle() => this.m_spreadAngle;

    public float getSpreadAngleDeviation() => this.m_spreadAngleDeviation;

    public float[] getAcceleration() => this.m_acceleration;

    public void setAcceleration(float[] acceleration)
    {
      if (acceleration != null)
      {
        if (this.m_acceleration == null)
          this.m_acceleration = new float[3];
        Array.Copy((Array) acceleration, (Array) this.m_acceleration, 3);
      }
      else
      {
        if (this.m_acceleration == null)
          return;
        this.m_acceleration = (float[]) null;
      }
    }

    public void getParticleStartPosition(Random random, float[] position)
    {
      position[0] = 0.0f;
      position[1] = 0.0f;
      position[2] = 0.0f;
      position[3] = 1f;
    }

    public void getParticleStartVelocity(Random random, float[] velocity)
    {
      float num1 = (float) random.NextDouble();
      float num2 = this.getSpeed() + num1 * this.getSpeedDeviation();
      velocity[0] = 0.0f;
      velocity[1] = num2;
      velocity[2] = 0.0f;
      velocity[3] = 0.0f;
      float num3 = (float) random.NextDouble();
      float spreadAngle = this.getSpreadAngle();
      float spreadAngleDeviation = this.getSpreadAngleDeviation();
      float degrees1 = spreadAngle + (float) (2.0 * (double) num3 - 1.0) * spreadAngleDeviation;
      float degrees2 = (float) (2.0 * random.NextDouble() - 1.0) * 180f;
      Transform transform = new Transform();
      transform.postRotate(degrees2, 0.0f, 1f, 0.0f);
      transform.postRotate(degrees1, 1f, 0.0f, 0.0f);
      transform.transform(velocity, 4);
    }
  }
}
