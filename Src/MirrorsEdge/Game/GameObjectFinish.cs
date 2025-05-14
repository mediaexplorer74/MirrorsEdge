
// Type: game.GameObjectFinish
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class GameObjectFinish : GameObject
  {
    public GameObjectFinish(MEdgeMap map, float minX, float minY, float lengthX, float lengthY)
      : base(map, 9, minX, minY, 0.0f)
    {
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(0.0f, 0.0f, -0.5f, lengthX, lengthY, 0.5f);
    }

    public override void Destructor() => base.Destructor();

    public override void collidedWith(GameObject other)
    {
      if (!other.isFlagSet(1))
        return;
      AppEngine.getCanvas().getSceneGame().completeLevel();
      this.m_map.removeObject((GameObject) this);
    }
  }
}
