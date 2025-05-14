
// Type: game.ChunkSection
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using midp;
using support;

#nullable disable
namespace game
{
  public class ChunkSection
  {
    private MathOrthoBox m_bounds = new MathOrthoBox();
    private int m_numChunks;
    private Node m_sectionNode;
    private ChunkRunnerVision[] m_runnerVisionChunkArray;
    public uint m_planeCoherency;

    public ChunkSection(DataInputStream dis, ref MapPalette mapPalette)
    {
      this.m_bounds = new MathOrthoBox();
      this.m_numChunks = 0;
      this.m_sectionNode = (Node) null;
      this.m_planeCoherency = (uint) int.MaxValue;
      this.m_bounds.setF(dis);
      this.m_runnerVisionChunkArray = new ChunkRunnerVision[(int) dis.readByte()];
      int index1 = 0;
      this.m_numChunks = (int) dis.readByte();
      Group parent = (Group) null;
      if (1 < this.m_numChunks)
      {
        parent = new Group();
        this.m_sectionNode = (Node) parent;
      }
      for (int index2 = 0; index2 != this.m_numChunks; ++index2)
      {
        Node node = this.readNode(dis, ref mapPalette);
        if (dis.readByte() == (sbyte) 1)
        {
          this.m_runnerVisionChunkArray[index1] = new ChunkRunnerVision(dis, node);
          ++index1;
        }
        if (this.m_numChunks == 1)
          this.m_sectionNode = node;
        else
          M3GAssets.addNode(parent, node);
      }
      if ((double) this.m_bounds.min.z - (double) this.m_bounds.max.z <= -10.0)
        return;
      this.m_bounds.min.z = this.m_bounds.max.z - 10f;
    }

    public void Destructor()
    {
      this.m_sectionNode = (Node) null;
      for (int index = 0; index != this.m_runnerVisionChunkArray.Length; ++index)
        this.m_runnerVisionChunkArray[index].Destructor();
      this.m_runnerVisionChunkArray = (ChunkRunnerVision[]) null;
    }

    public Node getSectionNode() => this.m_sectionNode;

    public MathOrthoBox getBounds() => this.m_bounds;

    public Node getChunkNode(int index)
    {
      return this.m_numChunks == 1 ? this.m_sectionNode : ((Group) this.m_sectionNode).getChild(index);
    }

    public ChunkRunnerVision getRunnerVisionChunk(int index)
    {
      return index >= this.m_runnerVisionChunkArray.Length ? (ChunkRunnerVision) null : this.m_runnerVisionChunkArray[index];
    }

    private Node readNode(DataInputStream dis, ref MapPalette mapPalette)
    {
      int userId = dis.readInt();
      float x = (float) dis.readInt() * 1.52587891E-05f;
      float y = (float) dis.readInt() * 1.52587891E-05f;
      float z = (float) dis.readInt() * 1.52587891E-05f;
      Node node = !dis.readBoolean() ? mapPalette.createUniqueNode(userId) : mapPalette.getNode(userId);
      node.translate(x, y, z);
      return node;
    }

    public void updateRunnerVision(float timeStepSecs, MathVector playerPosition, int facingDir)
    {
      this.m_sectionNode.setRenderingEnable(true);
      for (int index = 0; index != this.m_runnerVisionChunkArray.Length; ++index)
        this.m_runnerVisionChunkArray[index].updateIntensity(timeStepSecs, playerPosition, facingDir);
    }
  }
}
