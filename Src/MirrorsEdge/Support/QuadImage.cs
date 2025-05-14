
// Type: support.QuadImage
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;

#nullable disable
namespace support
{
  public class QuadImage
  {
    public Image2D image;
    public int resId;
    public int refCount;
    public float texMeshScale;
    public int texMax;
    public float texXMulti;
    public float texYMulti;

    public QuadImage()
    {
      this.image = (Image2D) null;
      this.resId = -1;
      this.refCount = 0;
      this.texMeshScale = 0.0f;
      this.texMax = -1;
      this.texXMulti = -1f;
      this.texYMulti = -1f;
    }

    public QuadImage(QuadImage quadImage)
    {
      this.image = quadImage.image;
      this.resId = quadImage.resId;
      this.refCount = quadImage.refCount;
      this.texMeshScale = quadImage.texMeshScale;
      this.texMax = quadImage.texMax;
      this.texXMulti = quadImage.texXMulti;
      this.texYMulti = quadImage.texYMulti;
    }

    public QuadImage CopyFrom(QuadImage quadImage)
    {
      this.image = quadImage.image;
      this.resId = quadImage.resId;
      this.refCount = quadImage.refCount;
      this.texMeshScale = quadImage.texMeshScale;
      this.texMax = quadImage.texMax;
      this.texXMulti = quadImage.texXMulti;
      this.texYMulti = quadImage.texYMulti;
      return this;
    }

    public void Destructor() => this.image = (Image2D) null;
  }
}
