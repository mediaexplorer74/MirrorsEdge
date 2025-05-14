
// Type: game.GameObjectBullet
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using midp;
using support;
using System;

#nullable disable
namespace game
{
  public class GameObjectBullet : GameObject
  {
    private const int MAX_AGE = 4000;
    public static int m_StaticGameObjectIndex;
    public int m_gameObjectIndex;
    private int m_age;

    public GameObjectBullet(
      MEdgeMap map,
      float posX,
      float posY,
      float posZ,
      MathVector velocity)
      : base(map, 20, posX, posY, posZ)
    {
      this.m_gameObjectIndex = GameObjectBullet.m_StaticGameObjectIndex++;
      this.m_age = 0;
      AppEngine canvas = AppEngine.getCanvas();
      M3GAssets m3Gassets = AppEngine.getM3GAssets();
      World m3Gworld = canvas.getSceneGame().getM3GWorld();
      this.m_velocity.set(velocity);
      float degrees1 = JMath.toDegrees((float) Math.Atan2((double) velocity.z, (double) velocity.x) - 1.57079637f);
      float degrees2 = JMath.toDegrees((float) Math.Atan2((double) velocity.z, (double) velocity.y) - 1.57079637f);
      MathVector mathVector1 = new MathVector(velocity);
      MathVector mathVector2 = mathVector1 * (float) -(0.60000002384185791 / (double) mathVector1.getLength());
      this.m_objectNode = m3Gassets.loadModel((int) M3GAssets.get("MODEL_EFFECT_BULLET"), 4);
      Transform transform = new Transform();
      transform.postRotate(degrees2, 1f, 0.0f, 0.0f);
      transform.postRotate(degrees1, 0.0f, -1f, 0.0f);
      this.m_objectNode.setTranslation(posX, posY, posZ);
      this.m_objectNode.setTransform(ref transform);
      this.m_objectNode.setScale(0.0f, 1f, 1f);
      M3GAssets.addNode((Group) m3Gworld, this.m_objectNode);
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(-0.1f, -0.1f, -0.1f, 0.1f, 0.1f, 0.1f);
    }

    public override void Destructor()
    {
      this.m_age = 0;
      base.Destructor();
    }

    public override void collidedWith(GameObject other)
    {
      if (other.getType() != 0)
        return;
      GameObjectPlayer gameObjectPlayer = other as GameObjectPlayer;
      AppEngine canvas = AppEngine.getCanvas();
      canvas.getSoundManager().playEvent(canvas.getSceneGame().getSoundPoolManager().getRandomSoundEventID((int) ResourceManager.get("SOUNDEVENTPOOLSET_BULLET_IMPACTS"), 0));
      gameObjectPlayer.hurt(1f, false);
      this.m_map.removeObject((GameObject) this);
    }

    public override void update(int timeStepMillis)
    {
      this.m_age += timeStepMillis;
      if (this.m_age >= 4000)
      {
        this.m_map.removeObject((GameObject) this);
      }
      else
      {
        this.m_objectNode.setScale(Math.Min(1f, (float) this.m_age / 200f), 1f, 1f);
        MathVector mathVector = new MathVector(this.m_velocity);
        mathVector *= (float) timeStepMillis * (1f / 1000f);
        GameObjectBullet gameObjectBullet = this;
        gameObjectBullet.m_position = gameObjectBullet.m_position + mathVector;
        this.m_objectNode.setTranslation(this.m_position.x, this.m_position.y, this.m_position.z);
        this.m_globalShape.setPosition(this.m_position);
        if (this.m_map.intersects(this.m_globalShape, 0))
        {
          this.m_map.removeObject((GameObject) this);
        }
        else
        {
          this.testVFC();
          if (this.m_passedVFC)
            this.m_objectNode.setRenderingEnable(true);
          else
            this.m_objectNode.setRenderingEnable(false);
        }
      }
    }
  }
}
