
// Type: game.PainBox
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace game
{
  public class PainBox
  {
    private CollOrthoHexahedron m_box;

    public PainBox(DataInputStream dis)
    {
      this.m_box = new CollOrthoHexahedron(dis.readFloat(), dis.readFloat(), dis.readFloat(), dis.readFloat(), dis.readFloat(), dis.readFloat());
    }

    public void Destructor()
    {
      this.m_box.Destructor();
      this.m_box = (CollOrthoHexahedron) null;
    }

    public bool intersects(GameObject obj)
    {
      return CollShape.testIntersection((CollShape) this.m_box, obj.m_globalShape);
    }
  }
}
