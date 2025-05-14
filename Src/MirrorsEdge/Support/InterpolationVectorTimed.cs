// Decompiled with JetBrains decompiler
// Type: support.InterpolationVectorTimed
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;

#nullable disable
namespace support
{
  public class InterpolationVectorTimed : InterpolationTimed
  {
    private MathVector m_startValue;
    private MathVector m_endValue;
    private MathVector m_currentValue;

    public InterpolationVectorTimed()
    {
      this.m_startValue = new MathVector();
      this.m_endValue = new MathVector();
      this.m_currentValue = new MathVector();
    }

    public override void Destructor() => base.Destructor();

    public void start(
      MathVector startValue,
      MathVector endValue,
      int durationMillis,
      InterpolationTimed.InterpolationType type)
    {
      this.start(durationMillis, type);
      this.m_startValue = startValue;
      this.m_endValue = endValue;
      this.m_currentValue = startValue;
    }

    protected override void applyProgress(float progress)
    {
      this.m_currentValue.setAsLinearInterpolation(this.m_startValue, this.m_endValue, progress);
    }

    protected override void applyEndValue() => this.m_currentValue = this.m_endValue;

    public MathVector getStartValue() => this.m_startValue;

    public MathVector getEndValue() => this.m_endValue;

    public MathVector getCurrentValue() => this.m_currentValue;
  }
}
