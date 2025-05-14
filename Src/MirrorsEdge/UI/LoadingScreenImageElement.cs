
// Type: UI.LoadingScreenImageElement
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;

#nullable disable
namespace UI
{
  public class LoadingScreenImageElement : WindowElement
  {
    private int m_imageId;
    private int m_align;

    public LoadingScreenImageElement(DataInputStream dis, int yOffset)
      : base(0, 0, 0, 0)
    {
      this.m_imageId = 0;
      this.m_align = 0;
      int num1 = (int) dis.readShort();
      this.m_imageId = ResourceManager.LOADING_IMAGE_LOOKUP[(int) dis.readShort()];
      int num2 = (int) dis.readShort();
      this.m_align = (int) dis.readShort();
      this.m_x = dis.readInt() * Runtime.pixelScale;
      this.m_y = dis.readInt() * Runtime.pixelScale;
      this.m_width = dis.readInt() * Runtime.pixelScale;
      this.m_height = dis.readInt() * Runtime.pixelScale;
    }

    public new void Destructor() => base.Destructor();

    public override void render(Graphics g, int top, int left)
    {
      int x_dest = this.m_x;
      int num = this.m_y;
      if ((this.m_align & 16) != 0)
        num = this.m_y + (this.m_height >> 1);
      else if ((this.m_align & 32) != 0)
        num = this.m_y + this.m_height;
      if ((this.m_align & 2) != 0)
        x_dest = this.m_x + (this.m_width >> 1);
      else if ((this.m_align & 4) != 0)
        x_dest = this.m_x + this.m_width;
      Image src = AppEngine.getCanvas().getResourceManager().loadImage(this.m_imageId);
      g.drawRegion(src, 0, 0, src.getWidth(), src.getHeight(), 0, x_dest, num - this.m_height + 18, this.m_align);
    }
  }
}
