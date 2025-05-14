
// Type: midp.ImageWP7
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;

#nullable disable
namespace midp
{
  internal class ImageWP7 : Image
  {
    private int m_textureOffsetX;
    private int m_textureOffsetY;
    private int m_textureWidth;
    private int m_textureHeight;
    private bool m_needsTextureReload;
    public Texture2D m_texture;
    private GraphicsWP7 m_graphics;

    public ImageWP7(InputStream stream)
      : base(false)
    {
    }

    public ImageWP7(string name)
      : base(false)
    {
      this.m_textureOffsetX = 0;
      this.m_textureOffsetY = 0;
      this.m_textureWidth = 0;
      this.m_textureHeight = 0;
      this.m_needsTextureReload = false;
      this.m_graphics = (GraphicsWP7) null;
      this.m_texture = MirrorsEdge.content.Load<Texture2D>(name);
      this.m_textureWidth = this.m_texture.Bounds.Width;
      this.m_textureHeight = this.m_texture.Bounds.Height;
      this.setSize(this.m_textureWidth, this.m_textureHeight);
    }

    public ImageWP7(int width, int height)
      : base(false)
    {
      this.m_textureOffsetX = 0;
      this.m_textureOffsetY = 0;
      this.m_textureWidth = 0;
      this.m_textureHeight = 0;
      this.m_needsTextureReload = false;
      this.set(width, height, 4);
      this.m_graphics = (GraphicsWP7) null;
    }

    public override void Destructor() => this.m_texture = (Texture2D) null;

    public override void getRGB(
      ref int[] rgbData,
      int offset,
      int scanlength,
      int x,
      int y,
      int width,
      int height)
    {
      if (this.m_texture == null)
        return;
      this.m_texture.GetData<int>(0, new Rectangle?(new Rectangle(x, y, width, height)), rgbData, offset, height * scanlength);
    }

    public int getTextureOffsetX() => this.m_textureOffsetX;

    public int getTextureOffsetY() => this.m_textureOffsetY;

    public int getTextureWidth() => this.m_textureWidth;

    public int getTextureHeight() => this.m_textureHeight;

    public bool isInvalid() => this.m_needsTextureReload;

    protected ImageWP7()
      : base(false)
    {
      this.m_textureOffsetX = 0;
      this.m_textureOffsetY = 0;
      this.m_textureWidth = 0;
      this.m_textureHeight = 0;
      this.m_needsTextureReload = false;
      this.m_graphics = (GraphicsWP7) null;
      this.m_texture = (Texture2D) null;
    }

    private void set(int width, int height, int bpp)
    {
      this.setSize(width, height);
      this.m_textureWidth = width;
      this.m_textureHeight = height;
      this.m_texture = (Texture2D) new RenderTarget2D(MirrorsEdge.graphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 1, RenderTargetUsage.PreserveContents);
    }

    public static midp.Graphics implementation_Image_getGraphics(Image source)
    {
      if (!(source is ImageWP7))
        return (midp.Graphics) null;
      ImageWP7 imageWp7 = source as ImageWP7;
      Texture2D texture = imageWp7.m_texture;
      if (!(texture is RenderTarget2D))
        return (midp.Graphics) null;
      if (imageWp7.m_graphics == null)
        imageWp7.m_graphics = new GraphicsWP7(texture as RenderTarget2D);
      return (midp.Graphics) imageWp7.m_graphics;
    }
  }
}
