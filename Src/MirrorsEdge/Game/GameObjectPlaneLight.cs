
// Type: game.GameObjectPlaneLight
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using support;

#nullable disable
namespace game
{
  public class GameObjectPlaneLight : GameObject
  {
    public const int FLASH_DURATION = 2000;
    public const int FLASH_ON = 80;
    private Node m_flashNode;
    private int m_flashTimer;

    public GameObjectPlaneLight(MEdgeMap map, float x, float y, float z)
      : base(map, 7, x, y, z)
    {
      this.m_flashNode = (Node) null;
      this.m_flashTimer = 0;
      AppEngine canvas = AppEngine.getCanvas();
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      World m3Gworld = canvas.getSceneGame().getM3GWorld();
      this.m_flashNode = m3Gassets.loadModel((int) M3GAssets.get("MODEL_PLANE"), 4);
      Node child1 = m3Gassets.loadModel((int) M3GAssets.get("MODEL_PLANE"), 4);
      child1.setScale(3f, 3f, 3f);
      this.m_flashNode.setScale(10f, 10f, 10f);
      this.m_flashNode.setRenderingEnable(false);
      Group child2 = new Group();
      child2.addChild(child1);
      child2.addChild(this.m_flashNode);
      m3Gworld.addChild((Node) child2);
      this.m_objectNode = (Node) child2;
      if (AppEngine.getCanvas().randPercent() < 50)
        this.m_velocity = new MathVector(3f, 0.0f, 0.0f);
      else
        this.m_velocity = new MathVector(-3f, 0.0f, 0.0f);
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      this.m_flashTimer = AppEngine.getCanvas().rand(0, 2000);
    }

    public override void resetLevel()
    {
      this.m_position = this.m_mapPlacementPosition;
      this.m_prevVelocity = this.m_velocity;
      if (this.m_objectNode == null)
        return;
      this.m_objectNode.setRenderingEnable(true);
    }

    public override void update(int timeStepMillis)
    {
      float num = (float) timeStepMillis / 1000f;
      GameObjectPlaneLight objectPlaneLight = this;
      objectPlaneLight.m_position = objectPlaneLight.m_position + this.m_velocity * num;
      this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
      this.m_flashTimer -= timeStepMillis;
      if (this.m_flashTimer <= 0)
        this.m_flashTimer = 2000;
      else if (this.m_flashTimer <= 80)
        this.m_flashNode.setRenderingEnable(true);
      else
        this.m_flashNode.setRenderingEnable(false);
    }
  }
}
