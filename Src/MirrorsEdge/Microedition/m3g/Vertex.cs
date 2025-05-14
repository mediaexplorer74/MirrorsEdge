
// Type: microedition.m3g.Vertex
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace microedition.m3g
{
  public struct Vertex : IVertexType
  {
    public Vector3 position;
    public Vector3 normal;
    public Vector2 textureCoordinate;
    public Vector2 textureCoordinate2;
    public Color color;
    public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[5]
    {
      new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
      new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
      new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
      new VertexElement(32, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),
      new VertexElement(40, VertexElementFormat.Color, VertexElementUsage.Color, 0)
    });

    VertexDeclaration IVertexType.VertexDeclaration => Vertex.VertexDeclaration;
  }
}
