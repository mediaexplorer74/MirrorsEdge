
// Type: particles.ParticleMode
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using midp;

#nullable disable
namespace particles
{
  public class ParticleMode : meObject
  {
    private string m_id;
    private string m_appearanceId;
    private AppearanceBase m_appearance;
    private float m_meanTimeToLiveMillis;
    private float m_meanTimeToLiveDeviation;
    private KeyframeSequence m_scaleSequence;
    private KeyframeSequence m_rotationSequence;
    private KeyframeSequence m_colorSequence;
    private KeyframeSequence m_cropSequence;

    public ParticleMode()
    {
      this.setMeanTimeToLive(1000f, 100f);
      this.setScale((KeyframeSequence) null);
      this.setRotation((KeyframeSequence) null);
      this.setColor((KeyframeSequence) null);
    }

    public ParticleMode(AppearanceBase appearance)
    {
      this.setMeanTimeToLive(1000f, 100f);
      this.setScale((KeyframeSequence) null);
      this.setRotation((KeyframeSequence) null);
      this.setColor((KeyframeSequence) null);
      this.setAppearance(appearance);
    }

    public override void Destructor()
    {
      this.m_appearance = (AppearanceBase) null;
      this.m_scaleSequence = (KeyframeSequence) null;
      this.m_rotationSequence = (KeyframeSequence) null;
      this.m_colorSequence = (KeyframeSequence) null;
      this.m_cropSequence = (KeyframeSequence) null;
    }

    public override meClass getClass() => (meClass) null;

    public void setId(string id) => this.m_id = id;

    public string getId() => this.m_id;

    public void setAppearance(AppearanceBase appearance) => this.m_appearance = appearance;

    public AppearanceBase getAppearance() => this.m_appearance;

    public void setScale(KeyframeSequence sequence) => this.m_scaleSequence = sequence;

    public KeyframeSequence getScale() => this.m_scaleSequence;

    public void setRotation(KeyframeSequence sequence) => this.m_rotationSequence = sequence;

    public KeyframeSequence getRotation() => this.m_rotationSequence;

    public void setColor(KeyframeSequence sequence) => this.m_colorSequence = sequence;

    public KeyframeSequence getColor() => this.m_colorSequence;

    public void setCrop(KeyframeSequence sequence) => this.m_cropSequence = sequence;

    public KeyframeSequence getCrop() => this.m_cropSequence;

    public void setMeanTimeToLive(float timeMillis, float deviation)
    {
      this.m_meanTimeToLiveMillis = timeMillis;
      this.m_meanTimeToLiveDeviation = deviation;
    }

    public float getMeanTimeToLive() => this.m_meanTimeToLiveMillis;

    public float getMeanTimeToLiveDeviation() => this.m_meanTimeToLiveDeviation;

    public void setAppearanceId(string id) => this.m_appearanceId = id;

    public string getAppearanceId() => this.m_appearanceId;
  }
}
