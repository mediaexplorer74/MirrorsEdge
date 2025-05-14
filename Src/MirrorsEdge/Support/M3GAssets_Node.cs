
// Type: support.M3GAssets_Node
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;

#nullable disable
namespace support
{
  public class M3GAssets_Node
  {
    private int m_loadFlags;
    private M3GAssets_File m_file;
    private int m_userId;
    private Node m_node;
    private bool m_allowDirectLoad;
    private int m_modelRefCount;
    private int m_maxModelReferences;

    public void addModelRef() => ++this.m_maxModelReferences;

    public M3GAssets_Node(M3GAssets_File file, int userId, bool allowDirectLoad)
    {
      this.m_loadFlags = 0;
      this.m_file = file;
      this.m_userId = userId;
      this.m_node = (Node) null;
      this.m_allowDirectLoad = allowDirectLoad;
      this.m_modelRefCount = 0;
      this.m_maxModelReferences = 0;
    }

    public void Destructor()
    {
      this.m_loadFlags = 0;
      this.m_modelRefCount = 0;
      this.free();
      this.m_file = (M3GAssets_File) null;
      this.m_node = (Node) null;
    }

    public Node loadUnique()
    {
      this.load();
      Node node = (Node) this.m_node.duplicate();
      this.free();
      return node;
    }

    public Node loadUniqueCached(int loadFlags)
    {
      this.m_loadFlags |= loadFlags;
      this.load();
      return (Node) this.m_node.duplicate();
    }

    public Node loadForModel()
    {
      ++this.m_modelRefCount;
      this.load();
      return this.m_allowDirectLoad || this.m_modelRefCount != this.m_maxModelReferences ? (Node) this.m_node.duplicate() : this.m_node;
    }

    private void load()
    {
      if (this.m_node != null)
        return;
      this.m_node = this.m_file.load();
      if (this.m_userId != -1)
        this.m_node = (Node) this.m_node.find(this.m_userId);
      M3GAssets.orphanNode(this.m_node);
    }

    public void freeCached(int loadFlags)
    {
      this.m_loadFlags &= ~loadFlags;
      this.free();
    }

    public void freedFromModel()
    {
      --this.m_modelRefCount;
      this.free();
    }

    private void free()
    {
      if (this.m_loadFlags != 0 || this.m_modelRefCount != 0 || this.m_node == null)
        return;
      M3GAssets.orphanNode(this.m_node);
      this.m_node = (Node) null;
      this.m_file.free();
    }
  }
}
