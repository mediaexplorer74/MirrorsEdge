// Decompiled with JetBrains decompiler
// Type: game.ChunkDynamic
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace game
{
  public class ChunkDynamic
  {
    private Node m_chunkNode;
    private GameObject m_gObject;
    private int m_columnIndex;

    public ChunkDynamic(Node chunkNode, GameObject gObject)
    {
      this.m_chunkNode = chunkNode;
      this.m_gObject = gObject;
      this.m_columnIndex = 0;
    }

    public void Destructor() => this.m_chunkNode = (Node) null;

    public Node getNode() => this.m_chunkNode;

    public MathVector getPosition() => this.m_gObject.m_position;

    public int getColumnIndex() => this.m_columnIndex;

    public void setColumnIndex(int newIndex) => this.m_columnIndex = newIndex;

    public bool isVisible()
    {
      if (this.m_chunkNode == null)
        return false;
      Node parent = this.m_chunkNode.getParent();
      return parent != null && parent.isRenderingEnabled();
    }
  }
}
