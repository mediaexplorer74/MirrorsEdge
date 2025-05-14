
// Type: support.InterpolationTimed
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace support
{
  public abstract class InterpolationTimed
  {
    private int m_timeMillis;
    private int m_durationMillis;
    private InterpolationTimed.InterpolationType m_interpolationType;

    public InterpolationTimed()
    {
      this.m_timeMillis = 0;
      this.m_durationMillis = 0;
      this.m_interpolationType = InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR;
    }

    public virtual void Destructor()
    {
    }

    protected void start(int durationMillis, InterpolationTimed.InterpolationType type)
    {
      this.m_timeMillis = 0;
      this.m_durationMillis = durationMillis;
      this.m_interpolationType = type;
    }

    public void stop()
    {
      this.m_timeMillis = this.m_durationMillis;
      this.applyEndValue();
    }

    public void update(int timeStepMillis)
    {
      this.m_timeMillis += timeStepMillis;
      if (this.m_durationMillis <= this.m_timeMillis)
      {
        this.stop();
      }
      else
      {
        float progress = (float) this.m_timeMillis / (float) this.m_durationMillis;
        switch (this.m_interpolationType)
        {
          case InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_LINEAR:
            this.applyProgress(progress);
            break;
          case InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_BEHIND:
            this.applyProgress(progress * progress);
            break;
          case InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_QUAD_AHEAD:
            float num1 = 1f - progress;
            this.applyProgress((float) (1.0 - (double) num1 * (double) num1));
            break;
          case InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_CUBIC_BEHIND:
            this.applyProgress(progress * progress * progress);
            break;
          case InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_CUBIC_AHEAD:
            float num2 = 1f - progress;
            this.applyProgress((float) (1.0 - (double) num2 * (double) num2 * (double) num2));
            break;
        }
      }
    }

    protected abstract void applyProgress(float progress);

    protected abstract void applyEndValue();

    public bool isFinished() => this.m_timeMillis == this.m_durationMillis;

    public enum InterpolationType
    {
      INTERPOLATION_TYPE_LINEAR,
      INTERPOLATION_TYPE_QUAD_BEHIND,
      INTERPOLATION_TYPE_QUAD_AHEAD,
      INTERPOLATION_TYPE_CUBIC_BEHIND,
      INTERPOLATION_TYPE_CUBIC_AHEAD,
    }
  }
}
