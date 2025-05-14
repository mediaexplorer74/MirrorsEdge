// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Texture2D
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class Texture2D : Transformable
  {
    public const int FILTER_BASE_LEVEL = 208;
    public const int FILTER_LINEAR = 209;
    public const int FILTER_NEAREST = 210;
    public const int FUNC_ADD = 224;
    public const int FUNC_BLEND = 225;
    public const int FUNC_DECAL = 226;
    public const int FUNC_MODULATE = 227;
    public const int FUNC_REPLACE = 228;
    public const int WRAP_CLAMP = 240;
    public const int WRAP_REPEAT = 241;
    public new const int M3G_UNIQUE_CLASS_ID = 17;
    private Image2D m_Image;
    private int m_BlendColor;
    private int m_Blending;
    private int m_ImageFilter;
    private int m_LevelFilter;
    private int m_WrappingS;
    private int m_WrappingT;

    public Texture2D()
    {
      this.m_Image = (Image2D) null;
      this.m_BlendColor = -1;
      this.m_Blending = 227;
      this.m_ImageFilter = 210;
      this.m_LevelFilter = 208;
      this.m_WrappingS = 241;
      this.m_WrappingT = 241;
    }

    public Texture2D(Image2D image)
    {
      this.m_Image = (Image2D) null;
      this.m_BlendColor = -1;
      this.m_Blending = 227;
      this.m_ImageFilter = 210;
      this.m_LevelFilter = 208;
      this.m_WrappingS = 241;
      this.m_WrappingT = 241;
      this.setImage(image);
    }

    public override void Destructor()
    {
      this.m_Image = (Image2D) null;
      base.Destructor();
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      Texture2D texture2D = (Texture2D) ret;
      texture2D.setImage(this.getImage());
      texture2D.setBlending(this.getBlending());
      texture2D.setFiltering(this.getLevelFilter(), this.getImageFilter());
      texture2D.setWrapping(this.getWrappingS(), this.getWrappingT());
    }

    public int getBlending() => this.m_Blending;

    public Image2D getImage() => this.m_Image;

    public int getImageFilter() => this.m_ImageFilter;

    public int getLevelFilter() => this.m_LevelFilter;

    public int getWrappingS() => this.m_WrappingS;

    public int getWrappingT() => this.m_WrappingT;

    public void setBlendColor(int ARGB) => this.m_BlendColor = ARGB;

    public void setBlending(int blending) => this.m_Blending = blending;

    public void setFiltering(int levelFilter, int imageFilter)
    {
      this.m_LevelFilter = levelFilter;
      this.m_ImageFilter = imageFilter;
    }

    public void setImage(Image2D image) => this.m_Image = image;

    public void setWrapping(int wrapS, int wrapT)
    {
      this.m_WrappingS = wrapS;
      this.m_WrappingT = wrapT;
    }

    public override int getM3GUniqueClassID() => 17;

    public static Texture2D m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 17 ? (Texture2D) obj : (Texture2D) null;
    }
  }
}
