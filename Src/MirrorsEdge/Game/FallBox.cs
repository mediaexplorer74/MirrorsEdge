
// Type: game.FallBox
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace game
{
  public class FallBox
  {
    private CollOrthoHexahedron m_box;

    public FallBox(DataInputStream dis)
    {
      this.m_box = new CollOrthoHexahedron(dis.readFloat(), dis.readFloat(), dis.readFloat(), dis.readFloat(), dis.readFloat(), dis.readFloat());
    }

    public void Destructor()
    {
      this.m_box.Destructor();
      this.m_box = (CollOrthoHexahedron) null;
    }

    public bool contains(MathVector point)
    {
      this.m_box.getBounds();
      return this.m_box.pointIntersects(point);
    }
  }
}
