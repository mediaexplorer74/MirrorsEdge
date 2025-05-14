// Decompiled with JetBrains decompiler
// Type: support.M3GAssets_Texture
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_Texture
  {
    private Appearance m_baseAppearance;
    private M3GAssets_Image m_image;
    private Appearance m_texturedAppearance;
    private int m_levelFilter;
    private int m_imageFilter;
    private int m_blending;
    private int m_loadFlags;
    private int m_internalAssetRefCount;

    public M3GAssets_Texture(
      Appearance baseAppearance,
      M3GAssets_Image image,
      int levelFilter,
      int imageFilter,
      int blending)
    {
      this.m_baseAppearance = baseAppearance;
      this.m_image = image;
      this.m_texturedAppearance = (Appearance) null;
      this.m_levelFilter = levelFilter;
      this.m_imageFilter = imageFilter;
      this.m_blending = blending;
      this.m_loadFlags = 0;
      this.m_internalAssetRefCount = 0;
    }

    public void Destructor()
    {
      this.m_loadFlags = 0;
      this.m_internalAssetRefCount = 0;
      this.free();
      this.m_baseAppearance = (Appearance) null;
    }

    public Appearance get() => this.m_texturedAppearance;

    public Appearance loadCached(int loadFlags)
    {
      this.m_loadFlags |= loadFlags;
      return this.load();
    }

    public Appearance loadForInternalAsset()
    {
      ++this.m_internalAssetRefCount;
      return this.load();
    }

    private Appearance load()
    {
      if (this.m_texturedAppearance == null)
        this.m_texturedAppearance = M3GAssets.createTexturedAppearance(this.m_baseAppearance, this.m_image.loadImage(), this.m_levelFilter, this.m_imageFilter, this.m_blending);
      return this.m_texturedAppearance;
    }

    public void freeCached(int loadFlags)
    {
      this.m_loadFlags &= ~loadFlags;
      this.free();
    }

    public void freeInternalAssetReference()
    {
      --this.m_internalAssetRefCount;
      this.free();
    }

    private void free()
    {
      if (this.m_loadFlags != 0 || this.m_internalAssetRefCount != 0 || this.m_texturedAppearance == null)
        return;
      this.m_image.freeImage();
      this.m_texturedAppearance = (Appearance) null;
    }
  }
}
