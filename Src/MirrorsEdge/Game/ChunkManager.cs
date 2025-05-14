
// Type: game.ChunkManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using midp;

#nullable disable
namespace game
{
  public class ChunkManager
  {
    private ChunkLayer[] m_layerArray;

    public ChunkManager() => this.m_layerArray = (ChunkLayer[]) null;

    public void Destructor()
    {
      int length = this.m_layerArray.Length;
      for (int index = 0; index != length; ++index)
        this.m_layerArray[index].Destructor();
      this.m_layerArray = (ChunkLayer[]) null;
    }

    public void load(DataInputStream dis, ref MapPalette mapPalette, Group groupToAddChunksTo)
    {
      int length = (int) dis.readByte();
      this.m_layerArray = new ChunkLayer[length];
      for (int index = 0; index != length; ++index)
        this.m_layerArray[index] = new ChunkLayer(dis, ref mapPalette, groupToAddChunksTo);
    }

    public void update(float timeStepSecs)
    {
      int length = this.m_layerArray.Length;
      for (int index = 0; index != length; ++index)
        this.m_layerArray[index].update(timeStepSecs);
    }

    public ChunkLayer getForegroundLayer() => this.m_layerArray[0];

    public Node getGraphicsReference(DataInputStream dis, ref ChunkRunnerVision runnerVisionChunk)
    {
      ChunkSection section = this.m_layerArray[(int) dis.readShort()].getColumn((int) dis.readShort()).getSection((int) dis.readShort());
      int index = (int) dis.readShort();
      runnerVisionChunk = section.getRunnerVisionChunk(index);
      return section.getChunkNode(index);
    }
  }
}
