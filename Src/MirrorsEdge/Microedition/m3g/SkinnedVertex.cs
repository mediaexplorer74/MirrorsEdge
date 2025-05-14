// Decompiled with JetBrains decompiler
// Type: microedition.m3g.SkinnedVertex
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace microedition.m3g
{
  public struct SkinnedVertex : IVertexType
  {
    public Vector3 position;
    public Vector2 textureCoordinate;
    public byte skinIndex0;
    public byte skinIndex1;
    public byte skinIndex2;
    public byte skinIndex3;
    public Vector4 skinWeight;
    public Vector3 normal;
    public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[5]
    {
      new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
      new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
      new VertexElement(20, VertexElementFormat.Byte4, VertexElementUsage.BlendIndices, 0),
      new VertexElement(24, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
      new VertexElement(40, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
    });

    VertexDeclaration IVertexType.VertexDeclaration => SkinnedVertex.VertexDeclaration;
  }
}
