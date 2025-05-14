// Decompiled with JetBrains decompiler
// Type: midp.Image
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public abstract class Image : meObject
  {
    private int m_width;
    private int m_height;
    private bool m_mutableFlag;

    public override meClass getClass() => (meClass) new ImageClass();

    protected Image(bool mutableFlag)
    {
      this.m_width = 0;
      this.m_height = 0;
      this.m_mutableFlag = mutableFlag;
    }

    public override void Destructor() => base.Destructor();

    protected void setSize(int width, int height)
    {
      this.m_width = width;
      this.m_height = height;
    }

    protected void setMutable(bool mutableFlag) => this.m_mutableFlag = mutableFlag;

    public static Image createImage(sbyte[] imageData, int imageOffset, int imageLength)
    {
      return (Image) null;
    }

    public static Image createImage(Image source) => (Image) null;

    public static Image createImage(
      Image image,
      int x,
      int y,
      int width,
      int height,
      int transform)
    {
      return (Image) null;
    }

    public static Image createImage(InputStream stream) => (Image) new ImageWP7(stream);

    public static Image createImage(int width, int height) => (Image) new ImageWP7(width, height);

    public static Image createImage(string name) => (Image) new ImageWP7(name);

    public static Image createRGBImage(int[] rgb, int width, int height, bool processAlpha)
    {
      return (Image) null;
    }

    public static Image createRGBImage(byte[] rgb, int width, int height, bool processAlpha)
    {
      return (Image) null;
    }

    public Graphics getGraphics() => ImageWP7.implementation_Image_getGraphics(this);

    public abstract void getRGB(
      ref int[] rgbData,
      int offset,
      int scanlength,
      int x,
      int y,
      int width,
      int height);

    public int getWidth() => this.m_width;

    public int getHeight() => this.m_height;

    public bool isMutable() => this.m_mutableFlag;
  }
}
