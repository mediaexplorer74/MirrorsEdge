// Decompiled with JetBrains decompiler
// Type: microedition.m3g.AnimationTrack
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class AnimationTrack : Object3D
  {
    public const int ALPHA = 256;
    public const int AMBIENT_COLOR = 257;
    public const int COLOR = 258;
    public const int CROP = 259;
    public const int DENSITY = 260;
    public const int DIFFUSE_COLOR = 261;
    public const int EMISSIVE_COLOR = 262;
    public const int FAR_DISTANCE = 263;
    public const int FIELD_OF_VIEW = 264;
    public const int INTENSITY = 265;
    public const int MORPH_WEIGHTS = 266;
    public const int NEAR_DISTANCE = 267;
    public const int ORIENTATION = 268;
    public const int PICKABILITY = 269;
    public const int SCALE = 270;
    public const int SHININESS = 271;
    public const int SPECULAR_COLOR = 272;
    public const int SPOT_ANGLE = 273;
    public const int SPOT_EXPONENT = 274;
    public const int TRANSLATION = 275;
    public const int VISIBILITY = 276;
    public new const int M3G_UNIQUE_CLASS_ID = 2;
    private KeyframeSequence m_KeyframeSequence;
    public AnimationController m_Controller;
    public int m_Property;
    private float[] m_Value;

    public AnimationTrack()
    {
      this.m_KeyframeSequence = (KeyframeSequence) null;
      this.m_Controller = (AnimationController) null;
      this.m_Property = 0;
      this.m_Value = (float[]) null;
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      AnimationTrack animationTrack = (AnimationTrack) ret;
      animationTrack.setProperty(this.getTargetProperty());
      animationTrack.setController(this.getController());
      animationTrack.setKeyframeSequence(this.getKeyframeSequence());
    }

    public override int getReferences(ref Object3D[] references)
    {
      int references1 = base.getReferences(ref references);
      int num1 = references1;
      if (this.m_KeyframeSequence != null)
        ++references1;
      if (this.m_Controller != null)
        ++references1;
      if (references != null)
      {
        if (this.m_KeyframeSequence != null)
          references[num1++] = (Object3D) this.m_KeyframeSequence;
        if (this.m_Controller != null)
        {
          Object3D[] object3DArray = references;
          int index = num1;
          int num2 = index + 1;
          AnimationController controller = this.m_Controller;
          object3DArray[index] = (Object3D) controller;
        }
      }
      return references1;
    }

    protected override void findReferences(ref Object3DFinder finder)
    {
      base.findReferences(ref finder);
      if (finder.getFound() != null)
        return;
      finder.find((Object3D) this.getKeyframeSequence());
      finder.find((Object3D) this.getController());
    }

    protected override void animateReferences(int time)
    {
    }

    public AnimationTrack(KeyframeSequence sequence, int property)
    {
      this.m_KeyframeSequence = (KeyframeSequence) null;
      this.m_Controller = (AnimationController) null;
      this.m_Property = 0;
      this.m_Value = (float[]) null;
      this.setKeyframeSequence(sequence);
      this.setProperty(property);
    }

    public void setKeyframeSequence(KeyframeSequence sequence)
    {
      this.m_KeyframeSequence = sequence;
      this.m_Value = new float[this.m_KeyframeSequence.getComponentCount()];
    }

    public void setProperty(int property) => this.m_Property = property;

    public float[] getSampleValue(int worldTime)
    {
      if (this.m_Controller == null)
        return (float[]) null;
      this.m_KeyframeSequence.sample(this.m_Controller.getPosition(worldTime), 0, ref this.m_Value);
      float weight = this.m_Controller.getWeight();
      for (int index = 0; index < this.m_Value.Length; ++index)
        this.m_Value[index] *= weight;
      return this.m_Value;
    }

    public AnimationController getController() => this.m_Controller;

    public KeyframeSequence getKeyframeSequence() => this.m_KeyframeSequence;

    public int getTargetProperty() => this.m_Property;

    public void setController(AnimationController controller) => this.m_Controller = controller;

    public override int getM3GUniqueClassID() => 2;

    public static AnimationTrack m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 2 ? (AnimationTrack) obj : (AnimationTrack) null;
    }
  }
}
