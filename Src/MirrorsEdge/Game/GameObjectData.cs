
// Type: game.GameObjectData
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using midp;

#nullable disable
namespace game
{
  public class GameObjectData
  {
    private ushort[] m_typeFlagArray;
    private byte[] m_collidesWithArray;

    public GameObjectData()
    {
      this.m_typeFlagArray = (ushort[]) null;
      this.m_collidesWithArray = (byte[]) null;
    }

    public void Destructor()
    {
      this.m_typeFlagArray = (ushort[]) null;
      this.m_collidesWithArray = (byte[]) null;
    }

    public void loadData()
    {
      if (this.m_typeFlagArray != null)
        return;
      DataInputStream dataInputStream = new DataInputStream(AppEngine.getCanvas().getResourceManager().loadBinaryFile((int) ResourceManager.get("IDI_GAME_OBJECTS_BIN")));
      int length = dataInputStream.readUnsignedShort();
      this.m_typeFlagArray = new ushort[length];
      this.m_collidesWithArray = new byte[length];
      for (int index = 0; index != length; ++index)
      {
        this.m_typeFlagArray[index] = (ushort) dataInputStream.readUnsignedShort();
        this.m_collidesWithArray[index] = (byte) dataInputStream.readByte();
      }
      dataInputStream.close();
    }

    public int getFlags(int objectType) => (int) this.m_typeFlagArray[objectType];

    public byte getCollidesWith(int objectType) => this.m_collidesWithArray[objectType];
  }
}
