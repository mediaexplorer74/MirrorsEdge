
// Type: game.GameObjectZipLine
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using support;

#nullable disable
namespace game
{
  public class GameObjectZipLine : GameObject
  {
    public MathLine m_zipLineLine;
    public float m_endOffset;

    public GameObjectZipLine(
      MEdgeMap map,
      ref MapPalette mapPalette,
      float x1,
      float y1,
      float x2,
      float y2,
      float endOffset)
      : base(map, 16)
    {
      this.m_zipLineLine = new MathLine();
      this.m_endOffset = endOffset;
      World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
      if ((double) y2 < (double) y1)
      {
        this.m_zipLineLine.origin.set(x1, y1, 0.0f);
        this.m_zipLineLine.direction.set(x2 - x1, y2 - y1, 0.0f);
      }
      else
      {
        this.m_zipLineLine.origin.set(x2, y2, 0.0f);
        this.m_zipLineLine.direction.set(x1 - x2, y1 - y2, 0.0f);
      }
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(x1, y1, 0.0f, x2, y2, 0.0f);
      int[] indices = new int[2]{ 0, 1 };
      IndexBuffer submesh = new IndexBuffer(9, indices.Length >> 1, indices);
      float[] src = new float[6]
      {
        x1,
        y1,
        0.0f,
        x2,
        y2,
        0.0f
      };
      VertexArray arr = new VertexArray(2, 3, 4);
      arr.set(0, 2, src);
      VertexBuffer vertices = new VertexBuffer();
      vertices.setDefaultColor(4289331200U);
      vertices.setPositions(arr, 1f, (float[]) null);
      Appearance appearance = AppEngine.getM3GAssets().getAppearance(0);
      Mesh child = new Mesh(vertices, submesh, appearance);
      M3GAssets.commit((Node) child);
      M3GAssets.addNode((Group) m3Gworld, (Node) child);
    }

    public new void Destructor() => base.Destructor();

    public MathLine getZipLineLine() => this.m_zipLineLine;

    public float getEndOffset() => this.m_endOffset;
  }
}
