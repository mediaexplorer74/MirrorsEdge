
// Type: game.ChunkLayer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using midp;
using support;
using System.Collections.Generic;

#nullable disable
namespace game
{
  public class ChunkLayer
  {
    private ChunkColumn[] m_columnArray;
    private int m_firstVisibleIndex;
    private int m_lastVisibleIndex;
    private List<ChunkDynamic> m_dynamicChunkList;

    public ChunkLayer(DataInputStream dis, ref MapPalette mapPalette, Group groupToAddChunksTo)
    {
      this.m_firstVisibleIndex = 0;
      this.m_lastVisibleIndex = 0;
      this.m_dynamicChunkList = new List<ChunkDynamic>();
      int length = (int) dis.readByte();
      this.m_columnArray = new ChunkColumn[length];
      this.m_lastVisibleIndex = length;
      for (int index = 0; index != length; ++index)
      {
        ChunkColumn chunkColumn = new ChunkColumn(dis, ref mapPalette);
        this.m_columnArray[index] = chunkColumn;
        M3GAssets.addNode(groupToAddChunksTo, (microedition.m3g.Node) chunkColumn.getColumnGroup());
      }
    }

    public void Destructor()
    {
      int length = this.m_columnArray.Length;
      for (int index = 0; index != length; ++index)
        this.m_columnArray[index].Destructor();
      this.m_columnArray = (ChunkColumn[]) null;
      foreach (ChunkDynamic dynamicChunk in this.m_dynamicChunkList)
        dynamicChunk.Destructor();
      this.m_dynamicChunkList.Clear();
      this.m_dynamicChunkList = (List<ChunkDynamic>) null;
    }

    public ChunkColumn getColumn(int index) => this.m_columnArray[index];

    public ChunkDynamic addDynamicNode(microedition.m3g.Node chunkNode, GameObject gObject)
    {
      ChunkDynamic chunkDynamic = new ChunkDynamic(chunkNode, gObject);
      this.m_dynamicChunkList.Add(chunkDynamic);
      M3GAssets.addNode(this.m_columnArray[chunkDynamic.getColumnIndex()].getColumnGroup(), chunkNode);
      return chunkDynamic;
    }

    public void removeDynamicNode(ChunkDynamic dynamic) => this.m_dynamicChunkList.Remove(dynamic);

    public void disableRendering()
    {
      for (int firstVisibleIndex = this.m_firstVisibleIndex; firstVisibleIndex != this.m_lastVisibleIndex; ++firstVisibleIndex)
        this.m_columnArray[firstVisibleIndex].getColumnGroup().setRenderingEnable(false);
      this.m_lastVisibleIndex = this.m_firstVisibleIndex;
    }

    public void update(float timeStepSecs)
    {
      this.updateDynamicNodes(timeStepSecs);
      this.updateViewCulling(timeStepSecs);
    }

    private void updateDynamicNodes(float timeStepSecs)
    {
      int length = this.m_columnArray.Length;
      foreach (ChunkDynamic dynamicChunk in this.m_dynamicChunkList)
      {
        MathVector position = dynamicChunk.getPosition();
        int columnIndex = dynamicChunk.getColumnIndex();
        int newIndex = columnIndex;
        MathOrthoBox bounds;
        for (bounds = this.m_columnArray[newIndex].getBounds(); (double) position.x < (double) bounds.min.x && 0 < newIndex; bounds = this.m_columnArray[newIndex].getBounds())
          --newIndex;
        for (; (double) bounds.max.x < (double) position.x && newIndex < length - 1; bounds = this.m_columnArray[newIndex].getBounds())
          ++newIndex;
        if (newIndex != columnIndex)
        {
          dynamicChunk.setColumnIndex(newIndex);
          M3GAssets.addNode(this.m_columnArray[newIndex].getColumnGroup(), dynamicChunk.getNode());
        }
      }
    }

    private void updateViewCulling(float timeStepSecs)
    {
      SceneGame sceneGame = AppEngine.getCanvas().getSceneGame();
      GameObjectPlayer playerObject = sceneGame.getMap().getPlayerObject();
      MathVector position = playerObject.m_position;
      int facingDir = (int) playerObject.getFacingDir();
      MathFrustum cameraFrustum = sceneGame.getCameraFrustum();
      int length = this.m_columnArray.Length;
      for (int index = 0; index != length; ++index)
      {
        if (cameraFrustum.intersectXAABBCoherency(this.m_columnArray[index].getBounds().min, this.m_columnArray[index].getBounds().max, ref this.m_columnArray[index].m_planeCoherency) != -1 && sceneGame.getCamera().checkDistance(this.m_columnArray[index].getBounds().min, this.m_columnArray[index].getBounds().max))
          this.m_columnArray[index].update(timeStepSecs, cameraFrustum, position, facingDir);
        else
          this.m_columnArray[index].getColumnGroup().setRenderingEnable(false);
      }
    }
  }
}
