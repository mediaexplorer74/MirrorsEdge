
// Type: support.M3GAssets_File
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_File
  {
    private int m_resId;
    private Node m_node;
    private int m_refCount;

    public M3GAssets_File(int resId)
    {
      this.m_resId = resId;
      this.m_node = (Node) null;
      this.m_refCount = 0;
    }

    public void Destructor() => this.m_node = (Node) null;

    public int getResId() => this.m_resId;

    public bool isLoaded() => this.m_node != null;

    public Node load()
    {
      if (this.m_node == null)
        this.m_node = AppEngine.getCanvas().getResourceManager().loadM3GNode(this.m_resId);
      ++this.m_refCount;
      return this.m_node;
    }

    public void free()
    {
      --this.m_refCount;
      if (this.m_node == null || this.m_refCount != 0)
        return;
      this.m_node = (Node) null;
    }
  }
}
