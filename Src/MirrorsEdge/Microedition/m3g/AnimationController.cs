
// Type: microedition.m3g.AnimationController
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class AnimationController : Object3D
  {
    public new const int M3G_UNIQUE_CLASS_ID = 1;
    private int m_ActiveIntervalStart;
    private int m_ActiveIntervalEnd;
    public float m_Weight;
    private float m_Speed;
    private float m_ReferenceSequenceTime;
    private int m_ReferenceWorldTime;

    public AnimationController()
    {
      this.m_ActiveIntervalStart = 0;
      this.m_ActiveIntervalEnd = 0;
      this.m_Weight = 1f;
      this.m_Speed = 1f;
      this.m_ReferenceSequenceTime = 0.0f;
      this.m_ReferenceWorldTime = 0;
    }

    public new Object3D duplicate()
    {
      AnimationController animationController = new AnimationController();
      Object3D ret = (Object3D) animationController;
      this.duplicateTo(ref ret);
      animationController.m_ActiveIntervalStart = this.m_ActiveIntervalStart;
      animationController.m_ActiveIntervalEnd = this.m_ActiveIntervalEnd;
      animationController.m_Weight = this.m_Weight;
      animationController.m_Speed = this.m_Speed;
      animationController.m_ReferenceSequenceTime = this.m_ReferenceSequenceTime;
      animationController.m_ReferenceWorldTime = this.m_ReferenceWorldTime;
      return (Object3D) animationController;
    }

    public override void updateAnimationProperty(int property, float[] value)
    {
    }

    public override void updateAnimationProperty(AnimationTrack track, int time)
    {
    }

    public override void updateAnimationProperty(int property, int[] value)
    {
    }

    public int getActiveIntervalEnd() => this.m_ActiveIntervalEnd;

    public int getActiveIntervalStart() => this.m_ActiveIntervalStart;

    public float getPosition(int worldTime)
    {
      return this.m_ReferenceSequenceTime + this.m_Speed * (float) (worldTime - this.m_ReferenceWorldTime);
    }

    public int getRefWorldTime() => this.m_ReferenceWorldTime;

    public int getRefSequenceTime() => (int) this.m_ReferenceSequenceTime;

    public float getSpeed() => this.m_Speed;

    public float getWeight() => this.m_Weight;

    public bool isZeroWeight() => (double) this.m_Weight == 0.0;

    public void setActiveInterval(int start, int end)
    {
      this.m_ActiveIntervalStart = start;
      this.m_ActiveIntervalEnd = end;
    }

    public void setPosition(float sequenceTime, int worldTime)
    {
      this.m_ReferenceSequenceTime = sequenceTime;
      this.m_ReferenceWorldTime = worldTime;
    }

    public void setSpeed(float speed, int worldTime)
    {
      this.m_ReferenceSequenceTime = this.getPosition(worldTime);
      this.m_ReferenceWorldTime = worldTime;
      this.m_Speed = speed;
    }

    public void setWeight(float weight) => this.m_Weight = weight;

    public override int getM3GUniqueClassID() => 1;

    public static AnimationController m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 1 ? (AnimationController) obj : (AnimationController) null;
    }
  }
}
