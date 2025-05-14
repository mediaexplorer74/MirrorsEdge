
// Type: game.GameObjectRunnerData
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using midp;

#nullable disable
namespace game
{
  public class GameObjectRunnerData
  {
    private GameObjectData_SoloVisual[] m_soloArray;
    private GameObjectData_OriginAnimVisual[] m_originArray;
    private GameObjectData_3ChannelBlendVisual[] m_3ChannelBlendArray;

    public GameObjectRunnerData()
    {
      this.m_soloArray = (GameObjectData_SoloVisual[]) null;
      this.m_originArray = (GameObjectData_OriginAnimVisual[]) null;
      this.m_3ChannelBlendArray = (GameObjectData_3ChannelBlendVisual[]) null;
    }

    public virtual void Destructor()
    {
      this.m_soloArray = (GameObjectData_SoloVisual[]) null;
      this.m_originArray = (GameObjectData_OriginAnimVisual[]) null;
      this.m_3ChannelBlendArray = (GameObjectData_3ChannelBlendVisual[]) null;
    }

    public void loadData()
    {
      if (this.m_soloArray != null)
        return;
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_RUNNER_DATA_BIN")));
      int length1 = dataInputStream.readUnsignedShort();
      this.m_soloArray = new GameObjectData_SoloVisual[length1];
      for (int index = 0; index != length1; ++index)
      {
        this.m_soloArray[index] = new GameObjectData_SoloVisual();
        GameObjectData_SoloVisual solo = this.m_soloArray[index];
        solo.animId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        solo.blendId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        solo.animFlags = (int) GameObjectRunner.ANIM_FLAG_ARRAY[dataInputStream.readUnsignedShort()];
        solo.blendFlags = !dataInputStream.readBoolean() ? 0 : 4;
      }
      int length2 = dataInputStream.readUnsignedShort();
      this.m_originArray = new GameObjectData_OriginAnimVisual[length2];
      for (int index = 0; index != length2; ++index)
      {
        this.m_originArray[index] = new GameObjectData_OriginAnimVisual();
        GameObjectData_OriginAnimVisual origin = this.m_originArray[index];
        origin.animId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        origin.blendId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        origin.originAnimId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        origin.clipping = dataInputStream.readBoolean();
        origin.gravity = dataInputStream.readBoolean();
        origin.yDist = (float) dataInputStream.readInt() * 1.52587891E-05f;
        origin.blendFlags = !dataInputStream.readBoolean() ? 0 : 4;
      }
      int length3 = dataInputStream.readUnsignedShort();
      this.m_3ChannelBlendArray = new GameObjectData_3ChannelBlendVisual[length3];
      for (int index = 0; index != length3; ++index)
      {
        this.m_3ChannelBlendArray[index] = new GameObjectData_3ChannelBlendVisual();
        GameObjectData_3ChannelBlendVisual channelBlendVisual = this.m_3ChannelBlendArray[index];
        channelBlendVisual.animId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        channelBlendVisual.lowBlendId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        channelBlendVisual.midBlendId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
        channelBlendVisual.hiBlendId = (int) GameObjectRunner.RESOURCE_ARRAY[dataInputStream.readUnsignedShort()];
      }
      dataInputStream.close();
    }

    public GameObjectData_SoloVisual getSoloAnim(int animIndex) => this.m_soloArray[animIndex];

    public GameObjectData_OriginAnimVisual getOriginAnim(int animIndex)
    {
      return this.m_originArray[animIndex];
    }

    public GameObjectData_3ChannelBlendVisual get3ChannelBlendAnim(int animIndex)
    {
      return this.m_3ChannelBlendArray[animIndex];
    }
  }
}
