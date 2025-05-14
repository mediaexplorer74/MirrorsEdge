// Decompiled with JetBrains decompiler
// Type: support.M3GAssets_Model
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_Model
  {
    private int m_loadFlags;
    private M3GAssets_Node m_nodeData;
    private M3GAssets_Texture m_texture;
    private M3GAssets_TexGroup m_texGroup;
    private Node m_node;
    private bool m_duplicate;
    private bool m_commit;

    public M3GAssets_Model(
      M3GAssets_Node nodeData,
      M3GAssets_Texture texture,
      M3GAssets_TexGroup texGroup,
      bool duplicate,
      bool commit)
    {
      this.m_loadFlags = 0;
      this.m_nodeData = nodeData;
      this.m_texture = texture;
      this.m_texGroup = texGroup;
      this.m_node = (Node) null;
      this.m_duplicate = duplicate;
      this.m_commit = commit;
      if (this.m_nodeData == null)
        return;
      this.m_nodeData.addModelRef();
    }

    public void Destructor()
    {
      this.freeCached(this.m_loadFlags);
      this.m_nodeData = (M3GAssets_Node) null;
      this.m_texture = (M3GAssets_Texture) null;
      this.m_texGroup = (M3GAssets_TexGroup) null;
      this.m_node = (Node) null;
    }

    public Node loadCached(int loadFlags)
    {
      this.m_loadFlags |= loadFlags;
      if (this.m_node == null)
      {
        this.m_node = this.m_nodeData.loadForModel();
        if (this.m_texture != null)
          M3GAssets.applyAppearance(this.m_node, this.m_texture.loadForInternalAsset());
        else if (this.m_texGroup != null)
          M3GAssets.applyAppearanceGroup(this.m_node, this.m_texGroup.loadForModel());
        if (this.m_commit)
          M3GAssets.commit(this.m_node);
      }
      if (this.m_duplicate)
      {
        Node node = (Node) this.m_node.duplicate();
        M3GAssets.cacheSkinTransforms(node);
        return node;
      }
      M3GAssets.cacheSkinTransforms(this.m_node);
      return this.m_node;
    }

    public void freeCached(int loadFlags)
    {
      this.m_loadFlags &= ~loadFlags;
      if (this.m_node == null || this.m_loadFlags != 0)
        return;
      this.m_node = (Node) null;
      this.m_nodeData.freedFromModel();
      if (this.m_texture != null)
        this.m_texture.freeInternalAssetReference();
      if (this.m_texGroup == null)
        return;
      this.m_texGroup.freeForModel();
    }
  }
}
