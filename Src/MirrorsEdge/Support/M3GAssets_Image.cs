// Decompiled with JetBrains decompiler
// Type: support.M3GAssets_Image
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_Image
  {
    private int m_resId;
    private Image2D m_image;
    private int m_refCount;

    public M3GAssets_Image(int resId)
    {
      this.m_resId = resId;
      this.m_image = (Image2D) null;
      this.m_refCount = 0;
    }

    public void Destructor() => this.m_image = (Image2D) null;

    public bool isImageLoaded() => this.m_image != null;

    public Image2D loadImage()
    {
      ++this.m_refCount;
      if (this.m_image == null)
        this.m_image = AppEngine.getCanvas().getResourceManager().loadM3GImage2D(this.m_resId);
      return this.m_image;
    }

    public void freeImage()
    {
      --this.m_refCount;
      if (this.m_refCount != 0)
        return;
      this.m_image = (Image2D) null;
    }
  }
}
