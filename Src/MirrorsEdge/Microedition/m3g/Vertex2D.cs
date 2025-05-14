// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Vertex2D
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace microedition.m3g
{
  public struct Vertex2D : IVertexType
  {
    public Vector3 position;
    public Vector2 textureCoordinate;
    public Vector2 textureCoordinate2;
    public Color color;
    public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[4]
    {
      new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
      new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
      new VertexElement(20, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),
      new VertexElement(28, VertexElementFormat.Color, VertexElementUsage.Color, 0)
    });

    VertexDeclaration IVertexType.VertexDeclaration => Vertex2D.VertexDeclaration;
  }
}
