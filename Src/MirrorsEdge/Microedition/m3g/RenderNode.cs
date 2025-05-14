// Decompiled with JetBrains decompiler
// Type: microedition.m3g.RenderNode
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using System;

#nullable disable
namespace microedition.m3g
{
  internal struct RenderNode : IComparable<RenderNode>
  {
    public Node renderNode;
    public Transform compositeTransform;
    public int m_layer;
    public int m_blending;
    public bool m_isSkinning;
    public readonly AppearanceBase m_appearance;
    public readonly IndexBuffer m_indexBuffer;
    public readonly VertexBuffer m_vertexBuffer;

    public RenderNode(RenderNode r)
    {
      this.renderNode = r.renderNode;
      this.compositeTransform = r.compositeTransform;
      this.m_layer = r.m_layer;
      this.m_blending = r.m_blending;
      this.m_isSkinning = r.m_isSkinning;
      this.m_appearance = r.m_appearance;
      this.m_indexBuffer = r.m_indexBuffer;
      this.m_vertexBuffer = r.m_vertexBuffer;
    }

    public RenderNode(Node n, int submeshIndex, Transform t, bool skinned)
    {
      this.renderNode = (Node) null;
      this.compositeTransform = (Transform) null;
      this.m_layer = 0;
      this.m_blending = 68;
      this.m_isSkinning = false;
      this.m_appearance = (AppearanceBase) null;
      this.m_indexBuffer = (IndexBuffer) null;
      this.m_vertexBuffer = (VertexBuffer) null;
      if (n == null || !(n is Mesh mesh))
        return;
      AppearanceBase appearance = (AppearanceBase) mesh.getAppearance(submeshIndex);
      IndexBuffer indexBuffer = mesh.getIndexBuffer(submeshIndex);
      if (appearance == null || indexBuffer == null)
        return;
      this.renderNode = n;
      this.compositeTransform = t;
      this.m_layer = appearance.getLayer();
      this.m_appearance = appearance;
      this.m_indexBuffer = indexBuffer;
      this.m_vertexBuffer = mesh.getVertexBuffer();
      CompositingMode compositingMode = appearance.getCompositingMode();
      if (compositingMode == null)
        return;
      this.m_blending = compositingMode.getBlending();
    }

    public void Destructor()
    {
      this.renderNode = (Node) null;
      this.compositeTransform = (Transform) null;
    }

    public int CompareTo(RenderNode rhs)
    {
      int layer1 = this.m_layer;
      int layer2 = rhs.m_layer;
      if (layer1 != layer2)
        return layer1.CompareTo(layer2);
      if (this.m_isSkinning && rhs.m_isSkinning && this.m_vertexBuffer != rhs.m_vertexBuffer)
        return this.m_vertexBuffer.CompareTo((Object3D) rhs.m_vertexBuffer);
      int blending1 = this.m_blending;
      int blending2 = rhs.m_blending;
      if (blending1 != blending2)
      {
        if (blending1 == 68)
          return -1;
        if (blending2 == 68)
          return 1;
      }
      Appearance appearance1 = (Appearance) this.m_appearance;
      Appearance appearance2 = (Appearance) rhs.m_appearance;
      if (appearance1 != appearance2)
      {
        for (int index = 0; index < 2; ++index)
        {
          Texture2D texture1 = appearance1.getTexture(index);
          Texture2D texture2 = appearance2.getTexture(index);
          if (texture1 != texture2)
            return texture1 == null ? -1 : texture1.CompareTo((Object3D) texture2);
        }
        Fog fog1 = appearance1.getFog();
        Fog fog2 = appearance2.getFog();
        if (fog1 != fog2)
          return fog1.CompareTo((Object3D) fog2);
        CompositingMode compositingMode1 = appearance1.getCompositingMode();
        CompositingMode compositingMode2 = appearance2.getCompositingMode();
        if (compositingMode1 != compositingMode2)
          return compositingMode1 == null ? -1 : compositingMode1.CompareTo((Object3D) compositingMode2);
        PolygonMode polygonMode1 = appearance1.getPolygonMode();
        PolygonMode polygonMode2 = appearance2.getPolygonMode();
        if (polygonMode1 != polygonMode2)
          return polygonMode1 == null ? -1 : polygonMode1.CompareTo((Object3D) polygonMode2);
      }
      return this.m_vertexBuffer != rhs.m_vertexBuffer ? this.m_vertexBuffer.CompareTo((Object3D) rhs.m_vertexBuffer) : this.m_indexBuffer.CompareTo((Object3D) rhs.m_indexBuffer);
    }
  }
}
