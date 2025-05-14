
// Type: game.GameObjectEffectHit
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using particles;
using support;

#nullable disable
namespace game
{
  public class GameObjectEffectHit : GameObject
  {
    private Node m_positionNode;
    private EmissionMode m_emissionMode;
    private ParticleEffect m_particleEffect;
    private int m_timeToLive;

    public GameObjectEffectHit(MEdgeMap map, Node positionNode)
      : base(map, 22, 0.0f, 0.0f, 0.0f)
    {
      this.m_positionNode = positionNode;
      this.m_emissionMode = new EmissionMode();
      this.m_timeToLive = 1000;
      ParticleMode particleMode = new ParticleMode((AppearanceBase) AppEngine.getM3GAssets().loadTexturedAppearance((int) M3GAssets.get("TEX_EFFECT_PARTICLES_ALPHA"), 4));
      particleMode.setMeanTimeToLive(175f, 75f);
      float[] numArray1 = new float[3]{ 0.0f, 1f, 2.5f };
      KeyframeSequence sequence1 = new KeyframeSequence(3, 1, 176);
      sequence1.setKeyframe(0, 0, numArray1, 0);
      sequence1.setKeyframe(1, 50, numArray1, 1);
      sequence1.setKeyframe(2, 250, numArray1, 2);
      sequence1.setDuration(251);
      particleMode.setScale(sequence1);
      KeyframeSequence sequence2 = new KeyframeSequence(2, 1, 176);
      float[] numArray2 = new float[2];
      sequence2.setKeyframe(0, 0, numArray2, 0);
      sequence2.setKeyframe(1, 250, numArray2, 1);
      sequence2.setDuration(251);
      particleMode.setRotation(sequence2);
      KeyframeSequence sequence3 = new KeyframeSequence(1, 4, 180);
      float num = 1f / 256f;
      float[] numArray3 = new float[4]
      {
        0.0f * num,
        64f * num,
        64f * num,
        64f * num
      };
      sequence3.setKeyframe(0, 0, numArray3, 0);
      sequence3.setDuration(251);
      particleMode.setCrop(sequence3);
      KeyframeSequence sequence4 = new KeyframeSequence(3, 4, 176);
      float[] numArray4 = new float[12]
      {
        1f,
        1f,
        1f,
        0.0f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        1f,
        0.0f
      };
      sequence4.setKeyframe(0, 0, numArray4, 0);
      sequence4.setKeyframe(1, 50, numArray4, 4);
      sequence4.setKeyframe(2, 250, numArray4, 8);
      sequence4.setDuration(251);
      particleMode.setColor(sequence4);
      this.m_emissionMode = new EmissionMode();
      this.m_emissionMode.setRate(600f);
      this.m_emissionMode.setSpeed(3f, 2f);
      this.m_emissionMode.setSpreadAngle(90f, 90f);
      this.m_particleEffect = new ParticleEffect(Emitter.createEmitter(1, 1, particleMode, this.m_emissionMode));
      M3GAssets.addNode((Group) AppEngine.getCanvas().getSceneGame().getM3GWorld(), (Node) this.m_particleEffect);
      this.updatePosition();
    }

    public override void Destructor()
    {
      if (this.m_emissionMode == null)
        return;
      AppEngine.getCanvas().getSceneGame().getM3GWorld().removeChild((Node) this.m_particleEffect);
      this.m_emissionMode.Destructor();
      this.m_emissionMode = (EmissionMode) null;
      this.m_particleEffect.Destructor();
      this.m_particleEffect = (ParticleEffect) null;
      base.Destructor();
    }

    public override void update(int timeStepMillis)
    {
      this.m_particleEffect.updateStep(timeStepMillis, (Transform) null, (Transform) null);
      this.m_emissionMode.setRate(0.0f);
      this.m_timeToLive -= timeStepMillis;
      if (this.m_timeToLive < 0)
        this.m_map.removeObject((GameObject) this);
      else
        this.updatePosition();
    }

    private void updatePosition()
    {
      World m3Gworld = AppEngine.getCanvas().getSceneGame().getM3GWorld();
      Transform transform = new Transform();
      this.m_positionNode.getTransformTo((Node) m3Gworld, transform);
      float[] vector = new float[4]{ 0.0f, 0.0f, 0.0f, 1f };
      transform.transform(vector, 4);
      this.m_particleEffect.setTranslation(vector[0], vector[1], vector[2]);
    }
  }
}
