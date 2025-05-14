// Decompiled with JetBrains decompiler
// Type: game.GameObjectEffectPickup
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;
using support;

#nullable disable
namespace game
{
  public class GameObjectEffectPickup : GameObject
  {
    private Node m_otherObjectMesh;
    private Node m_largeMesh;
    private Node m_smallMesh;
    private int m_animTime;
    private float m_rotation;

    public GameObjectEffectPickup(MEdgeMap map, GameObject otherObject)
      : base(map, 8, otherObject.m_position)
    {
      this.m_animTime = 0;
      this.m_rotation = 0.0f;
      AppEngine canvas = AppEngine.getCanvas();
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      QuadManager quadManager = canvas.getQuadManager();
      World m3Gworld = canvas.getSceneGame().getM3GWorld();
      this.m_position.y += 0.5f;
      Group group = new Group();
      this.m_objectNode = (Node) group;
      quadManager.addRawChild((Node) group);
      this.m_otherObjectMesh = (Node) otherObject.getObjectNode().duplicate();
      M3GAssets.addNode((Group) m3Gworld, this.m_otherObjectMesh);
      this.m_otherObjectMesh.setTranslation(this.m_position.x, this.m_position.y - 0.5f, this.m_position.z);
      this.m_largeMesh = m3Gassets.loadModel((int) M3GAssets.get("MODEL_EFFECT_COLLECT_LARGE"), 4);
      this.m_largeMesh.setScope(16);
      M3GAssets.addNode(group, this.m_largeMesh);
      this.m_smallMesh = m3Gassets.loadModel((int) M3GAssets.get("MODEL_EFFECT_COLLECT_SMALL"), 4);
      this.m_smallMesh.setScope(16);
      M3GAssets.addNode(group, this.m_smallMesh);
      this.update(0);
    }

    public override void Destructor()
    {
      if (this.m_objectNode == null)
        return;
      AppEngine.getCanvas().getQuadManager().removeRawChild(this.m_objectNode);
      this.m_objectNode = (Node) null;
      M3GAssets.orphanNode(this.m_otherObjectMesh);
      this.m_otherObjectMesh = (Node) null;
      base.Destructor();
    }

    public override void update(int timeStepMillis)
    {
      float num1 = (float) timeStepMillis * (1f / 1000f);
      this.m_animTime += timeStepMillis;
      int num2 = 1200;
      if (this.m_animTime < 300)
      {
        float alpha = 1f - (float) this.m_animTime / 300f;
        float num3 = (float) (0.30000001192092896 * (1.0 - (double) alpha * (double) alpha * (double) alpha));
        this.m_objectNode.setScale(num3, num3, num3);
        this.m_otherObjectMesh.setAlphaFactor(alpha);
      }
      else if (this.m_animTime < num2)
      {
        float alpha = (float) (1.0 - (double) (this.m_animTime - 300) / 900.0);
        float num4 = (float) (0.15000000596046448 + 0.15000000596046448 * (double) alpha * (double) alpha * (double) alpha);
        this.m_objectNode.setScale(num4, num4, num4);
        this.m_largeMesh.setAlphaFactor(alpha);
        this.m_smallMesh.setAlphaFactor(alpha);
      }
      else
      {
        this.m_map.removeObject((GameObject) this);
        return;
      }
      MathFrustum cameraFrustum = AppEngine.getCanvas().getSceneGame().getCameraFrustum();
      int screenX = 0;
      int screenY = 0;
      cameraFrustum.getScreenPosition(new MathVector(this.m_position), ref screenX, ref screenY);
      this.m_objectNode.setTranslation((float) screenX, (float) -screenY, 0.0f);
      this.m_rotation += 180f * num1;
      while (360.0 <= (double) this.m_rotation)
        this.m_rotation -= 360f;
      this.m_largeMesh.setOrientation(this.m_rotation, 0.0f, 0.0f, 1f);
      this.m_smallMesh.setOrientation(-this.m_rotation, 0.0f, 0.0f, 1f);
    }
  }
}
