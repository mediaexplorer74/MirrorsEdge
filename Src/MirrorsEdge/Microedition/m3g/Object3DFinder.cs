
// Type: microedition.m3g.Object3DFinder
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public struct Object3DFinder(int userID)
  {
    private readonly int m_UserID = userID;
    private Object3D m_Found = (Object3D) null;

    public void find(Object3D obj)
    {
      if (obj == null || this.m_Found != null)
        return;
      this.m_Found = obj.find(this.m_UserID);
    }

    public Object3D getFound() => this.m_Found;
  }
}
