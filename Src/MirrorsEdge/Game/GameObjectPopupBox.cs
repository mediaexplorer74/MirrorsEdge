
// Type: game.GameObjectPopupBox
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using GameManager;

#nullable disable
namespace game
{
  public class GameObjectPopupBox : GameObject
  {
    private int m_stringId;
    private bool m_triggered;

    public GameObjectPopupBox(
      MEdgeMap map,
      int stringId,
      float minX,
      float minY,
      float maxX,
      float maxY)
      : base(map, 15)
    {
      this.m_stringId = stringId;
      this.m_triggered = false;
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(minX, minY, -1f, maxX, maxY, 1f);
    }

    public override void Destructor() => base.Destructor();

    public override void resetCheckpoint()
    {
    }

    public override void resetLevel()
    {
      base.resetLevel();
      this.m_triggered = false;
    }

    public override void collidedWith(GameObject other)
    {
      if (!other.isFlagSet(1))
        return;
      if (AppEngine.getAchievementData().registerPopupBox(this.m_stringId) && !MirrorsEdge.TrialMode)
      {
        this.m_triggered = true;
        this.m_map.removeObject((GameObject) this);
      }
      else
      {
        AppEngine canvas = AppEngine.getCanvas();
        if (!this.m_triggered && canvas.getTutorialBoxes())
        {
          if (!AppEngine.getCanvas().getSceneGame().triggerPopupBox(this.m_stringId))
            return;
          this.m_triggered = true;
        }
        this.m_map.removeObject((GameObject) this);
      }
    }
  }
}
