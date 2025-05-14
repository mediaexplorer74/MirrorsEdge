
// Type: game.ChunkColumn
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using midp;
using support;

#nullable disable
namespace game
{
  public class ChunkColumn
  {
    private MathOrthoBox m_bounds = new MathOrthoBox();
    private ChunkSection[] m_sectionArray;
    private Group m_columnNode;
    private int m_lastVisibleIndex;
    public uint m_planeCoherency;

    public ChunkColumn(DataInputStream dis, ref MapPalette mapPalette)
    {
      this.m_bounds = new MathOrthoBox();
      this.m_lastVisibleIndex = 0;
      this.m_planeCoherency = (uint) int.MaxValue;
      this.m_bounds.setF(dis);
      int length = (int) dis.readByte();
      this.m_sectionArray = new ChunkSection[length];
      this.m_lastVisibleIndex = length;
      for (int index = 0; index != length; ++index)
        this.m_sectionArray[index] = new ChunkSection(dis, ref mapPalette);
      Group parent = new Group();
      this.m_columnNode = parent;
      for (int index = 0; index != length; ++index)
        M3GAssets.addNode(parent, this.m_sectionArray[index].getSectionNode());
    }

    public void Destructor()
    {
      int length = this.m_sectionArray.Length;
      for (int index = 0; index != length; ++index)
        this.m_sectionArray[index].Destructor();
      this.m_sectionArray = (ChunkSection[]) null;
      this.m_columnNode = (Group) null;
    }

    public ChunkSection getSection(int index) => this.m_sectionArray[index];

    public MathOrthoBox getBounds() => this.m_bounds;

    public Group getColumnGroup() => this.m_columnNode;

    public void update(
      float timeStepSecs,
      MathFrustum cameraViewFrustum,
      MathVector playerPosition,
      int facingDir)
    {
      this.m_columnNode.setRenderingEnable(true);
      int length = this.m_sectionArray.Length;
      if (length == 1)
      {
        this.m_sectionArray[0].updateRunnerVision(timeStepSecs, playerPosition, facingDir);
      }
      else
      {
        for (int index = 0; index != length; ++index)
        {
          if (cameraViewFrustum.intersectAABBCoherency(this.m_sectionArray[index].getBounds().min, this.m_sectionArray[index].getBounds().max, ref this.m_sectionArray[index].m_planeCoherency) != -1)
            this.m_sectionArray[index].updateRunnerVision(timeStepSecs, playerPosition, facingDir);
          else
            this.m_sectionArray[index].getSectionNode().setRenderingEnable(false);
        }
      }
    }
  }
}
