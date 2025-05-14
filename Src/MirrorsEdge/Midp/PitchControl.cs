
// Type: midp.PitchControl
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace midp
{
  public interface PitchControl : Control
  {
    void setPitchFactor(float factor);

    float getPitchFactor();
  }
}
