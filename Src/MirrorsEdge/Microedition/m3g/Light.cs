// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Light
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class Light : Node
  {
    public const int AMBIENT = 128;
    public const int DIRECTIONAL = 129;
    public const int OMNI = 130;
    public const int SPOT = 131;
    public new const int M3G_UNIQUE_CLASS_ID = 12;
    private int mColor;
    private float mIntensity;
    private float mConstantAttenuation;
    private float mLinearAttenuation;
    private float mQuadraticAttenuation;
    private float mSpotAngle;
    private float mSpotExponent;

    public Light()
    {
      this.mColor = 16777215;
      this.mIntensity = 1f;
      this.mConstantAttenuation = 1f;
      this.mLinearAttenuation = 0.0f;
      this.mQuadraticAttenuation = 0.0f;
      this.mSpotAngle = 45f;
      this.mSpotExponent = 0.0f;
    }

    public int getColor() => this.mColor;

    public float getConstantAttenuation() => this.mConstantAttenuation;

    public float getIntensity() => this.mIntensity;

    public float getLinearAttenuation() => this.mLinearAttenuation;

    public float getQuadraticAttenuation() => this.mQuadraticAttenuation;

    public float getSpotAngle() => this.mSpotAngle;

    public float getSpotExponent() => this.mSpotExponent;

    public void setAttenuation(float constant, float linear, float quadratic)
    {
      this.mConstantAttenuation = constant;
      this.mLinearAttenuation = linear;
      this.mQuadraticAttenuation = quadratic;
    }

    public void setColor(int RGB) => this.mColor = RGB;

    public void setIntensity(float intensity) => this.mIntensity = intensity;

    public void setSpotAngle(float angle) => this.mSpotAngle = angle;

    public void setSpotExponent(float exponent) => this.mSpotExponent = exponent;

    public override int getM3GUniqueClassID() => 12;
  }
}
