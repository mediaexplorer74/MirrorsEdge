// Decompiled with JetBrains decompiler
// Type: game.GameObjectCutsceneTrigger
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace game
{
  public class GameObjectCutsceneTrigger : GameObject
  {
    private int m_cutsceneId;
    private bool m_triggered;

    public GameObjectCutsceneTrigger(
      MEdgeMap map,
      int cutsceneId,
      float minX,
      float minY,
      float maxX,
      float maxY)
      : base(map, 15)
    {
      this.m_cutsceneId = cutsceneId;
      this.m_triggered = false;
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(minX, minY, -1f, maxX, maxY, 1f);
    }

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
      AppEngine canvas = AppEngine.getCanvas();
      if (!this.m_triggered)
      {
        canvas.getSceneGame().playCutscene(this.m_cutsceneId);
        this.m_triggered = true;
      }
      this.m_map.removeObject((GameObject) this);
    }
  }
}
