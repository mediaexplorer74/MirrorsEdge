// Decompiled with JetBrains decompiler
// Type: generic.SignalFilter
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace generic
{
  public class SignalFilter
  {
    public const int SMOOTH_STEP = 0;
    public const int ONE_POLE_LP = 1;
    public const int ONE_POLE_HP = 2;
    private const int INTERNAL_TIMESTEP = 30;
    private int m_filterType;
    private float[] m_a;
    private float[] m_b;
    private float[] m_x;
    private float[] m_y;
    private int m_xBufferSize;
    private int m_yBufferSize;
    private int m_xCurrentIndex;
    private int m_yCurrentIndex;
    private float m_yPrevious;
    private float m_yCurrent;
    private int m_interpTimer;
    private float m_targetValue;

    public SignalFilter(int filterType, float riseTime, float initialValue)
    {
      this.m_filterType = filterType;
      this.m_xBufferSize = -1;
      this.m_yBufferSize = -1;
      this.m_xCurrentIndex = 0;
      this.m_yCurrentIndex = 0;
      this.m_yPrevious = 0.0f;
      this.m_yCurrent = 0.0f;
      this.m_interpTimer = 0;
      this.m_targetValue = 0.0f;
      this.m_a = (float[]) null;
      this.m_b = (float[]) null;
      this.m_x = (float[]) null;
      this.m_y = (float[]) null;
      if (this.m_filterType == 0)
        this.initSmoothstepFIR(riseTime);
      else if (this.m_filterType == 1)
        this.initOnePoleLP(riseTime);
      else if (this.m_filterType == 2)
        this.initOnePoleHP(riseTime);
      this.m_targetValue = initialValue;
      this.setSteadyState(this.m_targetValue);
    }

    public void Destructor()
    {
      this.m_a = (float[]) null;
      this.m_b = (float[]) null;
      this.m_x = (float[]) null;
      this.m_y = (float[]) null;
    }

    public void setTargetValue(float targetValue) => this.m_targetValue = targetValue;

    public float getTargetValue() => this.m_targetValue;

    public void setSteadyState(float targetValue)
    {
      for (int index = 0; index < this.m_xBufferSize; ++index)
        this.m_x[index] = targetValue;
      if (this.m_yBufferSize > 0)
      {
        for (int index = 0; index < this.m_yBufferSize; ++index)
          this.m_y[index] = targetValue;
      }
      this.m_yPrevious = this.m_yCurrent = this.m_targetValue = targetValue;
    }

    public void update(int timeStep)
    {
      this.m_interpTimer += timeStep;
      if (this.m_interpTimer < 30)
        return;
      for (; this.m_interpTimer >= 30; this.m_interpTimer -= 30)
        this.doFilterStep();
    }

    public float getFilteredValue()
    {
      float num = (float) this.m_interpTimer / 30f;
      return (float) ((double) this.m_yPrevious * (1.0 - (double) num) + (double) this.m_yCurrent * (double) num);
    }

    private void doFilterStep()
    {
      this.m_yPrevious = this.m_yCurrent;
      this.m_yCurrent = 0.0f;
      this.m_x[this.m_xCurrentIndex] = this.m_targetValue;
      for (int index = 0; index < this.m_xBufferSize; ++index)
        this.m_yCurrent += this.m_x[(index + this.m_xCurrentIndex) % this.m_xBufferSize] * this.m_b[index];
      --this.m_xCurrentIndex;
      if (this.m_xCurrentIndex < 0)
        this.m_xCurrentIndex += this.m_xBufferSize;
      if (this.m_yBufferSize <= 0)
        return;
      for (int index = 0; index < this.m_yBufferSize; ++index)
        this.m_yCurrent -= this.m_y[(index + this.m_yCurrentIndex) % this.m_yBufferSize] * this.m_a[index];
      this.m_y[this.m_yCurrentIndex] = this.m_yCurrent;
      --this.m_yCurrentIndex;
      if (this.m_yCurrentIndex >= 0)
        return;
      this.m_yCurrentIndex += this.m_yBufferSize;
    }

    private void initBuffers()
    {
      this.m_b = new float[this.m_xBufferSize];
      this.m_x = new float[this.m_xBufferSize];
      this.m_a = new float[this.m_yBufferSize];
      this.m_y = new float[this.m_yBufferSize];
    }

    private void initSmoothstepFIR(float riseTime)
    {
      this.m_yBufferSize = 0;
      this.m_xBufferSize = (int) ((double) riseTime / 30.0);
      if (this.m_xBufferSize < 3)
        this.m_xBufferSize = 3;
      if (this.m_xBufferSize % 2 == 1)
        ++this.m_xBufferSize;
      this.initBuffers();
      float num1 = 0.0f;
      for (int index = 0; index < this.m_xBufferSize / 2; ++index)
      {
        float num2 = 2f / (float) this.m_xBufferSize;
        this.m_b[index] = this.smoothStep((float) index * num2);
        this.m_b[this.m_xBufferSize - index - 1] = this.smoothStep((float) index * num2);
        num1 += 2f * this.m_b[index];
      }
      for (int index = 0; index < this.m_xBufferSize; ++index)
        this.m_b[index] /= num1;
    }

    private float smoothStep(float t)
    {
      return (float) (-2.0 * (double) t * (double) t * (double) t + 3.0 * (double) t * (double) t);
    }

    private void initOnePoleLP(float riseTime)
    {
      this.m_xBufferSize = 1;
      this.m_yBufferSize = 1;
      this.initBuffers();
      this.m_a[0] = -(float) Math.Exp(Math.Log(0.10000000149011612) * 30.0 / (double) riseTime);
      this.m_b[0] = 1f + this.m_a[0];
    }

    private void initOnePoleHP(float riseTime)
    {
      this.m_xBufferSize = 2;
      this.m_yBufferSize = 1;
      this.initBuffers();
      float num = (float) Math.Exp(Math.Log(0.10000000149011612) * 30.0 / (double) riseTime);
      this.m_a[0] = -num;
      this.m_b[0] = num;
      this.m_b[1] = -num;
    }
  }
}
