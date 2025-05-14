// Decompiled with JetBrains decompiler
// Type: game.MapPalette
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using microedition.m3g;
using support;

#nullable disable
namespace game
{
  public class MapPalette
  {
    private Node m_paletteNode;

    public MapPalette(int paletteResId, ModelSet modelSet)
    {
      ResourceManager resourceManager = AppEngine.getCanvas().getResourceManager();
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      this.m_paletteNode = resourceManager.loadM3GNode(paletteResId);
      M3GAssets.applyAppearanceGroup(this.m_paletteNode, m3Gassets.loadTextureGroup(modelSet.getModelId(0), 8));
      M3GAssets.commit(this.m_paletteNode);
    }

    public void Destructor() => this.m_paletteNode = (Node) null;

    public Node createUniqueNode(int userId)
    {
      Node uniqueNode = (Node) this.m_paletteNode.find(userId);
      if (uniqueNode != null)
        uniqueNode = (Node) uniqueNode.duplicate();
      return uniqueNode;
    }

    public Node getNode(int userId) => (Node) this.m_paletteNode.find(userId);
  }
}
