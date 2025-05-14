
// Type: game.GameObjectCollectable
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using GameManager;

#nullable disable
namespace game
{
  public class GameObjectCollectable : GameObject
  {
    private const float SFX_PLAYER_DISTANCE_SQ = 100f;
    private ChunkDynamic m_dynamicNode;
    private bool m_hasPlayerBeenClose;

    public GameObjectCollectable(
      MEdgeMap map,
      ref MapPalette mapPalette,
      int type,
      float posX,
      float posY,
      float posZ)
      : base(map, type, posX, posY, posZ)
    {
      this.m_hasPlayerBeenClose = false;
      int userId = 271;
      if (this.getType() == 18)
        userId = 270;
      this.m_objectNode = mapPalette.createUniqueNode(userId);
      this.m_objectNode.setTranslation(posX, posY, posZ);
      this.m_dynamicNode = this.m_map.getForegroundLayer().addDynamicNode(this.m_objectNode, (GameObject) this);
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(-0.5f, -0.5f, -0.5f, 0.5f, 0.5f, 0.5f);
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      if (!soundManager.isEventLoaded((int) ResourceManager.get("SOUNDEVENT_SFX_BAG_STINGER")))
        soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_BAG_STINGER"));
      if (soundManager.isEventLoaded((int) ResourceManager.get("SOUNDEVENT_SFX_PUZZLE_INTRO")))
        return;
      soundManager.loadEvent((int) ResourceManager.get("SOUNDEVENT_SFX_PUZZLE_INTRO"));
    }

    public override void Destructor()
    {
      if (this.m_dynamicNode == null)
        return;
      this.m_map.getForegroundLayer().removeDynamicNode(this.m_dynamicNode);
      this.m_dynamicNode = (ChunkDynamic) null;
      base.Destructor();
    }

    public override void collidedWith(GameObject other)
    {
      if (other.getType() != 0)
        return;
      this.m_map.addObject((GameObject) new GameObjectEffectPickup(this.m_map, (GameObject) this));
      this.m_map.removeObject((GameObject) this);
      if (!MirrorsEdge.TrialMode)
        AppEngine.getAchievementData().registerBag();
      AppEngine.getCanvas().getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_BAG_STINGER"));
    }

    public override void update(int timeStep)
    {
      if (this.m_hasPlayerBeenClose || (double) this.getDistanceToSq((GameObject) this.m_map.getPlayerObject()) >= 100.0)
        return;
      AppEngine.getCanvas().getSoundManager().playEvent((int) ResourceManager.get("SOUNDEVENT_SFX_PUZZLE_INTRO"), 1f);
      this.m_hasPlayerBeenClose = true;
    }
  }
}
