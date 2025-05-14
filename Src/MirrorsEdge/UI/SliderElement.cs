// Decompiled with JetBrains decompiler
// Type: UI.SliderElement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace UI
{
  public abstract class SliderElement : WindowElement
  {
    public const float SLIDER_DRAG = 8f;
    public const float ZERO_VELOCITY = 0.0001f;
    protected bool m_sliding;
    protected float m_lastSlidePos;
    protected float m_slidePos;
    protected float m_slideVel;

    public SliderElement(int width, int height)
      : base(0, 0, width, height)
    {
      this.m_sliding = false;
      this.m_lastSlidePos = 0.0f;
      this.m_slidePos = 0.0f;
      this.m_slideVel = 0.0f;
    }

    public override void update(int timeStep)
    {
      float num1 = (float) timeStep / 1000f;
      if (this.m_sliding)
      {
        this.m_slideVel = (this.m_lastSlidePos - this.m_slidePos) / num1;
        this.m_lastSlidePos = this.m_slidePos;
      }
      else
      {
        this.m_slidePos -= this.m_slideVel * num1;
        this.m_slidePos = Math.Max(0.0f, Math.Min(1f, this.m_slidePos));
      }
      float num2 = 8f * Math.Abs(this.m_slideVel) * num1;
      if ((double) this.m_slideVel > 0.0)
      {
        this.m_slideVel -= num2;
        if ((double) this.m_slideVel > 9.9999997473787516E-05)
          return;
        this.m_slideVel = 0.0f;
      }
      else
      {
        if ((double) this.m_slideVel >= 0.0)
          return;
        this.m_slideVel += num2;
        if ((double) this.m_slideVel < -9.9999997473787516E-05)
          return;
        this.m_slideVel = 0.0f;
      }
    }

    public void setSliding(bool sliding) => this.m_sliding = sliding;

    public bool getSliding() => this.m_sliding;

    public float getValue() => this.m_slidePos;

    public void setValue(float value)
    {
      this.m_slidePos = value;
      this.m_lastSlidePos = value;
    }
  }
}
