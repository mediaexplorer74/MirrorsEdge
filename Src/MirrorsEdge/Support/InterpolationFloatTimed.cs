
// Type: support.InterpolationFloatTimed
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace support
{
  public class InterpolationFloatTimed : InterpolationTimed
  {
    private float m_startValue;
    private float m_endValue;
    private float m_currentValue;

    public InterpolationFloatTimed()
    {
      this.m_startValue = 0.0f;
      this.m_endValue = 0.0f;
      this.m_currentValue = 0.0f;
    }

    public override void Destructor() => base.Destructor();

    public void start(
      float startValue,
      float endValue,
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
      this.m_currentValue = this.m_startValue + progress * (this.m_endValue - this.m_startValue);
    }

    protected override void applyEndValue() => this.m_currentValue = this.m_endValue;

    public float getCurrentValue() => this.m_currentValue;
  }
}
