
// Type: game.GameObject
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using generic;
using microedition.m3g;
using support;

#nullable disable
namespace game
{
  public class GameObject
  {
    public const float GRAVITY = -9.8f;
    public MEdgeMap m_map;
    private int m_type;
    private int m_id;
    private int m_flags;
    private bool m_staticLevelObject;
    public MathVector m_position;
    protected MathVector m_velocity;
    protected MathVector m_prevVelocity;
    public CollShape m_globalShape;
    protected MathVector m_mapPlacementPosition = new MathVector();
    private float m_verticalAcceleration;
    protected Node m_objectNode;
    protected AnimPlayer3D m_objectAnimPlayer;
    private bool m_active;
    private MathVector m_forwardDirection = new MathVector();
    private MathVector m_rightDirection = new MathVector();
    protected uint m_planeCoherency;
    protected bool m_passedVFC;
    protected bool m_firstAlwaysPass;

    public int getType() => this.m_type;

    public int getID() => this.m_id;

    public void setID(int uniqueId) => this.m_id = uniqueId;

    public int getFlags() => this.m_flags;

    public bool isFlagSet(int flag) => (this.m_flags & flag) != 0;

    public bool isStaticLevelObject() => this.m_staticLevelObject;

    public void markObjectStatic() => this.m_staticLevelObject = true;

    public MathVector getVelocity() => this.m_velocity;

    public MathOrthoBox getGlobalBounds()
    {
      this.m_globalShape.setPosition(this.m_position);
      return this.m_globalShape.getBounds();
    }

    public float getVerticalAcceleration() => this.m_verticalAcceleration;

    public void setVerticalAcceleration(float newAccel) => this.m_verticalAcceleration = newAccel;

    public Node getObjectNode() => this.m_objectNode;

    public bool isAnimating() => this.m_objectAnimPlayer.isAnimating();

    public GameObject(MEdgeMap map, int type)
    {
      this.m_map = map;
      this.m_type = type;
      this.m_flags = 0;
      this.m_staticLevelObject = false;
      this.m_position = new MathVector();
      this.m_velocity = new MathVector();
      this.m_prevVelocity = new MathVector();
      this.m_globalShape = (CollShape) null;
      this.m_mapPlacementPosition = new MathVector();
      this.m_verticalAcceleration = 0.0f;
      this.m_objectNode = (Node) null;
      this.m_objectAnimPlayer = (AnimPlayer3D) null;
      this.m_active = true;
      this.m_forwardDirection = new MathVector(1f, 0.0f, 0.0f);
      this.m_rightDirection = new MathVector(0.0f, 0.0f, 1f);
      this.m_id = -1;
      this.m_planeCoherency = (uint) int.MaxValue;
      this.m_passedVFC = true;
      this.m_firstAlwaysPass = true;
      this.construct();
    }

    public GameObject(MEdgeMap map, int type, MathVector position)
    {
      this.m_map = map;
      this.m_type = type;
      this.m_flags = 0;
      this.m_staticLevelObject = false;
      this.m_position = new MathVector(position);
      this.m_velocity = new MathVector();
      this.m_prevVelocity = new MathVector();
      this.m_globalShape = (CollShape) null;
      this.m_mapPlacementPosition = new MathVector(position);
      this.m_verticalAcceleration = 0.0f;
      this.m_objectNode = (Node) null;
      this.m_objectAnimPlayer = (AnimPlayer3D) null;
      this.m_active = true;
      this.m_forwardDirection = new MathVector(1f, 0.0f, 0.0f);
      this.m_rightDirection = new MathVector(0.0f, 0.0f, 1f);
      this.m_id = -1;
      this.m_planeCoherency = (uint) int.MaxValue;
      this.m_passedVFC = true;
      this.m_firstAlwaysPass = true;
      this.construct();
    }

    public GameObject(MEdgeMap map, int type, float posX, float posY, float posZ)
    {
      this.m_map = map;
      this.m_type = type;
      this.m_flags = 0;
      this.m_staticLevelObject = false;
      this.m_position = new MathVector(posX, posY, posZ);
      this.m_velocity = new MathVector();
      this.m_prevVelocity = new MathVector();
      this.m_globalShape = (CollShape) null;
      this.m_mapPlacementPosition = new MathVector(posX, posY, posZ);
      this.m_verticalAcceleration = 0.0f;
      this.m_objectNode = (Node) null;
      this.m_objectAnimPlayer = (AnimPlayer3D) null;
      this.m_active = true;
      this.m_forwardDirection = new MathVector(1f, 0.0f, 0.0f);
      this.m_rightDirection = new MathVector(0.0f, 0.0f, 1f);
      this.m_id = -1;
      this.m_planeCoherency = (uint) int.MaxValue;
      this.m_passedVFC = true;
      this.m_firstAlwaysPass = true;
      this.construct();
    }

    private void construct()
    {
      this.m_flags = AppEngine.getGameObjectData().getFlags(this.m_type);
      if (this.m_type != 17)
        return;
      this.m_globalShape = (CollShape) new CollOrthoHexahedron(-0.2f, -0.2f, -0.2f, 0.2f, 0.2f, 0.2f);
    }

    public virtual void Destructor()
    {
      this.m_map = (MEdgeMap) null;
      if (this.m_globalShape != null)
      {
        this.m_globalShape.Destructor();
        this.m_globalShape = (CollShape) null;
      }
      if (this.m_objectAnimPlayer != null)
      {
        this.m_objectAnimPlayer.Destructor();
        this.m_objectAnimPlayer = (AnimPlayer3D) null;
      }
      if (this.m_objectNode == null)
        return;
      M3GAssets.orphanNode(this.m_objectNode);
      this.m_objectNode = (Node) null;
    }

    public virtual void checkpointActivated()
    {
    }

    public virtual void resetCheckpoint() => this.resetLevel();

    public virtual void resetLevel()
    {
      this.m_firstAlwaysPass = true;
      this.m_active = true;
      this.m_position = this.m_mapPlacementPosition;
      this.m_velocity.set(0.0f, 0.0f, 0.0f);
      this.m_prevVelocity = this.m_velocity;
      if (this.m_objectNode == null)
        return;
      this.m_objectNode.setRenderingEnable(true);
    }

    public bool isActive() => this.m_active;

    public void deactivate()
    {
      this.m_active = false;
      if (this.m_objectNode == null)
        return;
      this.m_objectNode.setRenderingEnable(false);
    }

    public bool intersects(MathOrthoBox otherBounds)
    {
      return (new MathOrthoBox(this.m_globalShape.getLocalBounds()) + this.m_position).intersects(otherBounds);
    }

    public bool intersectsMap()
    {
      this.m_globalShape.setPosition(this.m_position);
      return this.m_map.intersects(this.m_globalShape, 0);
    }

    public virtual void validateCollShape()
    {
      if (this.m_globalShape == null)
        return;
      this.m_globalShape.setPosition(this.m_position);
    }

    public virtual void collidedWith(GameObject other)
    {
    }

    public MathVector getForwardDirection() => this.m_forwardDirection;

    public MathVector getRightDirection() => this.m_rightDirection;

    public float getForwardVelocity() => this.m_velocity.x;

    public void setForwardVelocity(float vel) => this.m_velocity.x = vel;

    public float getUpVelocity() => this.m_velocity.y;

    public float getPrevUpVelocity() => this.m_prevVelocity.y;

    public void setUpVelocity(float vel) => this.m_velocity.y = vel;

    public void setNoVelocity() => this.m_velocity.set(0.0f, 0.0f, 0.0f);

    public void setNoLateralVelocity()
    {
      this.m_velocity.x = 0.0f;
      this.m_velocity.z = 0.0f;
    }

    public float getDistanceToSq(GameObject other)
    {
      return new MathVector(this.m_position.x - other.m_position.x, this.m_position.y - other.m_position.y, this.m_position.z - other.m_position.z).getLengthSq();
    }

    public float getLateralDistanceToSq(GameObject other)
    {
      return new MathVector(this.m_position.x - other.m_position.x, 0.0f, this.m_position.z - other.m_position.z).getLengthSq();
    }

    public virtual void update(int timeStepMillis)
    {
      this.m_prevVelocity = this.m_velocity;
      this.m_velocity.y += this.m_verticalAcceleration * ((float) timeStepMillis / 1000f);
    }

    public virtual void updateAnimation(int timeStepMillis)
    {
    }

    public MathVector positionRelativeTo(GameObject @object)
    {
      return @object.m_position - this.m_position;
    }

    public bool checkLOSWithPlayer()
    {
      MathLine line = new MathLine(this.m_position, this.m_map.getPlayerObject().m_position);
      line.direction -= line.origin;
      line.origin.y += 1.2f;
      float minT = 0.0f;
      float maxT = 1f;
      return !this.m_map.intersects(line, 0, ref minT, ref maxT);
    }

    protected void testVFC()
    {
      if (this.m_firstAlwaysPass)
      {
        this.m_firstAlwaysPass = false;
        this.m_passedVFC = true;
      }
      else if (this.m_globalShape == null || this.isFlagSet(1))
      {
        this.m_passedVFC = true;
      }
      else
      {
        MathOrthoBox globalBounds = this.getGlobalBounds();
        this.m_passedVFC = AppEngine.getCanvas().getSceneGame().getCameraFrustum().intersectAABBCoherency(globalBounds.min, globalBounds.max, ref this.m_planeCoherency) != -1;
      }
    }
  }
}
