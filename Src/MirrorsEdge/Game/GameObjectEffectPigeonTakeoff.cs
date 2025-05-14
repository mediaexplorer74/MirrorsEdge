// Decompiled with JetBrains decompiler
// Type: game.GameObjectEffectPigeonTakeoff
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using microedition.m3g;
using support;

#nullable disable
namespace game
{
  public class GameObjectEffectPigeonTakeoff : GameObject
  {
    private Group m_pigeonGroup;
    private GameObjectEffectPigeonTakeoff_Pigeon[] m_pigeonArray;
    private bool m_spooked;
    private bool m_pigeonEffectPlayed;

    public GameObjectEffectPigeonTakeoff(MEdgeMap map, float posX, float posY, float posZ)
      : base(map, 21, posX, posY, posZ)
    {
      this.m_pigeonGroup = (Group) null;
      this.m_spooked = false;
      this.m_pigeonEffectPlayed = false;
      AppEngine canvas = AppEngine.getCanvas();
      World m3Gworld = canvas.getSceneGame().getM3GWorld();
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(-6f, -6f, -6f, 6f, 6f, 6f);
      this.m_pigeonGroup = new Group();
      M3GAssets.addNode((Group) m3Gworld, (Node) this.m_pigeonGroup);
      int length = canvas.rand(4, 6);
      this.m_pigeonArray = new GameObjectEffectPigeonTakeoff_Pigeon[length];
      switch (length)
      {
        case 1:
          this.m_pigeonArray[0] = new GameObjectEffectPigeonTakeoff_Pigeon(0, this, this.m_pigeonGroup, posX, posY, posZ);
          break;
        case 2:
          this.m_pigeonArray[0] = new GameObjectEffectPigeonTakeoff_Pigeon(0, this, this.m_pigeonGroup, posX, posY, posZ);
          this.m_pigeonArray[1] = new GameObjectEffectPigeonTakeoff_Pigeon(1, this, this.m_pigeonGroup, posX - 0.6f, posY, posZ - 0.6f);
          break;
        case 3:
          this.m_pigeonArray[0] = new GameObjectEffectPigeonTakeoff_Pigeon(0, this, this.m_pigeonGroup, posX, posY, posZ);
          this.m_pigeonArray[1] = new GameObjectEffectPigeonTakeoff_Pigeon(1, this, this.m_pigeonGroup, posX - 0.6f, posY, posZ - 0.6f);
          this.m_pigeonArray[2] = new GameObjectEffectPigeonTakeoff_Pigeon(2, this, this.m_pigeonGroup, posX - 0.6f, posY, posZ + 0.6f);
          break;
        case 4:
          this.m_pigeonArray[0] = new GameObjectEffectPigeonTakeoff_Pigeon(0, this, this.m_pigeonGroup, posX - 0.5f, posY, posZ - 0.5f);
          this.m_pigeonArray[1] = new GameObjectEffectPigeonTakeoff_Pigeon(1, this, this.m_pigeonGroup, posX - 0.5f, posY, posZ + 0.5f);
          this.m_pigeonArray[2] = new GameObjectEffectPigeonTakeoff_Pigeon(2, this, this.m_pigeonGroup, posX + 0.5f, posY, posZ - 0.5f);
          this.m_pigeonArray[3] = new GameObjectEffectPigeonTakeoff_Pigeon(3, this, this.m_pigeonGroup, posX + 0.5f, posY, posZ + 0.5f);
          break;
        case 5:
          this.m_pigeonArray[0] = new GameObjectEffectPigeonTakeoff_Pigeon(0, this, this.m_pigeonGroup, posX, posY, posZ);
          this.m_pigeonArray[1] = new GameObjectEffectPigeonTakeoff_Pigeon(1, this, this.m_pigeonGroup, posX - 0.6f, posY, posZ - 0.6f);
          this.m_pigeonArray[2] = new GameObjectEffectPigeonTakeoff_Pigeon(2, this, this.m_pigeonGroup, posX - 0.6f, posY, posZ + 0.6f);
          this.m_pigeonArray[3] = new GameObjectEffectPigeonTakeoff_Pigeon(3, this, this.m_pigeonGroup, posX + 0.6f, posY, posZ - 0.6f);
          this.m_pigeonArray[4] = new GameObjectEffectPigeonTakeoff_Pigeon(4, this, this.m_pigeonGroup, posX + 0.6f, posY, posZ + 0.6f);
          break;
        case 6:
          this.m_pigeonArray[0] = new GameObjectEffectPigeonTakeoff_Pigeon(0, this, this.m_pigeonGroup, posX - 0.5f, posY, posZ - 0.5f);
          this.m_pigeonArray[1] = new GameObjectEffectPigeonTakeoff_Pigeon(1, this, this.m_pigeonGroup, posX - 0.5f, posY, posZ + 0.5f);
          this.m_pigeonArray[2] = new GameObjectEffectPigeonTakeoff_Pigeon(2, this, this.m_pigeonGroup, posX + 0.5f, posY, posZ - 0.5f);
          this.m_pigeonArray[3] = new GameObjectEffectPigeonTakeoff_Pigeon(3, this, this.m_pigeonGroup, posX + 0.5f, posY, posZ + 0.5f);
          this.m_pigeonArray[4] = new GameObjectEffectPigeonTakeoff_Pigeon(4, this, this.m_pigeonGroup, posX - 1f, posY, posZ);
          this.m_pigeonArray[5] = new GameObjectEffectPigeonTakeoff_Pigeon(5, this, this.m_pigeonGroup, posX + 1f, posY, posZ);
          break;
      }
    }

    public override void Destructor()
    {
      if (this.m_pigeonArray == null)
        return;
      for (int index = 0; index != this.m_pigeonArray.Length; ++index)
      {
        this.m_pigeonArray[index].Destructor();
        this.m_pigeonArray[index] = (GameObjectEffectPigeonTakeoff_Pigeon) null;
      }
      this.m_pigeonArray = (GameObjectEffectPigeonTakeoff_Pigeon[]) null;
      M3GAssets.orphanNode((Node) this.m_pigeonGroup);
      this.m_pigeonGroup = (Group) null;
      base.Destructor();
    }

    public override void resetCheckpoint()
    {
      if (!this.m_spooked)
        return;
      for (int index = 0; index != this.m_pigeonArray.Length; ++index)
        this.m_pigeonArray[index].reset();
      this.m_spooked = false;
    }

    public override void resetLevel()
    {
      if (!this.m_spooked)
        return;
      for (int index = 0; index != this.m_pigeonArray.Length; ++index)
        this.m_pigeonArray[index].reset();
      this.m_spooked = false;
    }

    public override void collidedWith(GameObject other)
    {
      if (this.m_spooked)
        return;
      for (int index = 0; index != this.m_pigeonArray.Length; ++index)
        this.m_pigeonArray[index].takeoff(other.m_position);
      this.m_spooked = true;
    }

    public override void update(int timeStepMillis)
    {
      for (int index = 0; index != this.m_pigeonArray.Length; ++index)
        this.m_pigeonArray[index].update(timeStepMillis);
    }

    public void pigeonTakenOff()
    {
      if (this.m_pigeonEffectPlayed)
        return;
      AppEngine.getCanvas().getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_PIGEON_WINGS"));
      this.m_pigeonEffectPlayed = true;
    }
  }
}
