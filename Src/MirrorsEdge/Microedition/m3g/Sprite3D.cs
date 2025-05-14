
// Type: microedition.m3g.Sprite3D
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class Sprite3D : Node
  {
    public new const int M3G_UNIQUE_CLASS_ID = 18;
    private Appearance m_Appearance;
    private Image2D m_Image;
    private int m_CropX;
    private int m_CropY;
    private int m_CropW;
    private int m_CropH;

    public Sprite3D(bool scaled, Image2D image, Appearance app)
    {
      this.m_Appearance = app;
      this.m_Image = image;
      this.m_CropX = 0;
      this.m_CropY = 0;
      this.m_CropW = 0;
      this.m_CropH = 0;
      this.m_CropW = this.m_Image.getWidth();
      this.m_CropH = this.m_Image.getHeight();
    }

    public override void updateAnimationProperty(int property, float[] value)
    {
      base.updateAnimationProperty(property, value);
      if (property != 259)
        return;
      this.setCrop((int) value[0], (int) value[1], (int) value[2], (int) value[3]);
    }

    public override void updateAnimationProperty(AnimationTrack track, int time)
    {
      base.updateAnimationProperty(track, time);
      if (track.m_Property != 259)
        return;
      float[] sampleValue = track.getSampleValue(time);
      this.setCrop((int) sampleValue[0], (int) sampleValue[1], (int) sampleValue[2], (int) sampleValue[3]);
    }

    public Appearance getAppearance() => this.m_Appearance;

    public Image2D getImage2D() => this.m_Image;

    public void setCrop(int x, int y, int width, int height)
    {
      this.m_CropX = x;
      this.m_CropY = y;
      this.m_CropW = width;
      this.m_CropH = height;
    }

    public override int getM3GUniqueClassID() => 18;
  }
}
