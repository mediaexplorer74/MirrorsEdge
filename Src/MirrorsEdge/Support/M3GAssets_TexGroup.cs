// Decompiled with JetBrains decompiler
// Type: support.M3GAssets_TexGroup
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_TexGroup
  {
    private M3GAssets_AppGroup m_appearanceGroup;
    private M3GAssets_Texture[] m_textureArray;
    private M3GAssets_Texture m_defaultTexture;
    private bool m_loaded;
    private int m_loadFlags;
    private int m_modelRefCount;

    public M3GAssets_TexGroup(int numElements, M3GAssets_Texture defaultTexture)
    {
      this.m_appearanceGroup = new M3GAssets_AppGroup(numElements, (Appearance) null);
      this.m_textureArray = new M3GAssets_Texture[numElements];
      this.m_defaultTexture = defaultTexture;
      this.m_loaded = false;
      this.m_loadFlags = 0;
      this.m_modelRefCount = 0;
    }

    public void Destructor()
    {
      this.m_loadFlags = 0;
      this.m_modelRefCount = 0;
      this.free();
      this.m_defaultTexture = (M3GAssets_Texture) null;
      this.m_appearanceGroup = (M3GAssets_AppGroup) null;
      for (int index = 0; index < this.m_textureArray.Length; ++index)
      {
        if (this.m_textureArray[index] != null)
          this.m_textureArray[index].Destructor();
        this.m_textureArray[index] = (M3GAssets_Texture) null;
      }
      this.m_textureArray = (M3GAssets_Texture[]) null;
    }

    public void addElement(int userId, M3GAssets_Texture texture)
    {
      this.m_textureArray[this.m_appearanceGroup.getNumMappings()] = texture;
      this.m_appearanceGroup.addElement(userId, (Appearance) null);
    }

    public M3GAssets_AppGroup loadCached(int loadFlags)
    {
      this.m_loadFlags |= loadFlags;
      return this.load();
    }

    public M3GAssets_AppGroup loadForModel()
    {
      ++this.m_modelRefCount;
      return this.load();
    }

    private M3GAssets_AppGroup load()
    {
      if (!this.m_loaded)
      {
        this.m_appearanceGroup.setDefaultAppearance(this.m_defaultTexture.loadForInternalAsset());
        int length = this.m_textureArray.Length;
        for (int index = 0; index != length; ++index)
          this.m_appearanceGroup.setMappedAppearance(this.m_textureArray[index].loadForInternalAsset(), index);
        this.m_loaded = true;
      }
      return this.m_appearanceGroup;
    }

    public void freeCached(int loadFlags)
    {
      this.m_loadFlags &= ~loadFlags;
      this.free();
    }

    public void freeForModel()
    {
      --this.m_modelRefCount;
      this.free();
    }

    private void free()
    {
      if (!this.m_loaded || this.m_loadFlags != 0 || this.m_modelRefCount != 0)
        return;
      this.m_defaultTexture.freeInternalAssetReference();
      int length = this.m_textureArray.Length;
      for (int index = 0; index != length; ++index)
        this.m_textureArray[index].freeInternalAssetReference();
      this.m_appearanceGroup.clearAppearances();
      this.m_loaded = false;
    }

    public M3GAssets_AppGroup getAppGroup() => this.m_appearanceGroup;
  }
}
