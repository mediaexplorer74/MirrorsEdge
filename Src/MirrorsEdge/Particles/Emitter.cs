
// Type: particles.Emitter
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using System;

#nullable disable
namespace particles
{
  public class Emitter : Transformable
  {
    public const int POINT_SPRITE = 0;
    public const int QUAD = 1;
    public const int CYLINDER = 2;
    public new const int M3G_UNIQUE_CLASS_ID = 127;
    private Particles m_particles;
    private AnimationController m_controller;
    private EmissionMode m_emissionMode;
    private Random m_randomGenerator;

    public override int getM3GUniqueClassID() => (int) sbyte.MaxValue;

    public static Emitter createEmitter(
      int type,
      int maxParticles,
      ParticleMode particleMode,
      EmissionMode emissionMode)
    {
      Particles particles;
      switch (type)
      {
        case 0:
          particles = (Particles) new PointParticles(maxParticles, particleMode);
          break;
        case 1:
          particles = (Particles) new QuadParticles(maxParticles, particleMode);
          break;
        case 2:
          particles = (Particles) new CylinderParticles(maxParticles, particleMode);
          break;
        default:
          return (Emitter) null;
      }
      return new Emitter(particles, emissionMode);
    }

    private Emitter(Particles particles, EmissionMode emissionMode)
    {
      this.m_particles = (Particles) null;
      this.m_controller = (AnimationController) null;
      this.m_emissionMode = (EmissionMode) null;
      this.m_randomGenerator = (Random) null;
      this.m_particles = particles;
      this.setRandomGenerator(new Random());
      this.setEmissionMode(emissionMode);
      this.setAnimationController(new AnimationController());
    }

    public override void Destructor()
    {
      this.m_particles = (Particles) null;
      this.m_controller.Destructor();
      this.m_controller = (AnimationController) null;
      this.m_emissionMode = (EmissionMode) null;
      this.m_randomGenerator = (Random) null;
    }

    public void setAnimationController(AnimationController controller)
    {
      this.m_controller = controller;
    }

    public AnimationController getAnimationController() => this.m_controller;

    public void setEmissionMode(EmissionMode mode) => this.m_emissionMode = mode;

    public EmissionMode getEmissionMode() => this.m_emissionMode;

    public void setParticleMode(ParticleMode mode) => this.m_particles.setParticleMode(mode);

    public ParticleMode getParticleMode() => this.m_particles.getParticleMode();

    public void setRandomGenerator(Random random) => this.m_randomGenerator = random;

    public Random getRandomGenerator() => this.m_randomGenerator;

    public void update(
      int worldTimeMillis,
      int firstVertex,
      VertexBuffer vertexBuffer,
      Transform cameraTransform,
      Transform invCameraTransform)
    {
      this.m_particles.update(this.m_controller.getPosition(worldTimeMillis), firstVertex, vertexBuffer, cameraTransform, invCameraTransform, this);
    }

    public void killAllParticles(int firstVertex, VertexBuffer vertexBuffer)
    {
      this.m_particles.killAll(firstVertex, vertexBuffer);
    }

    public Particles getParticles() => this.m_particles;
  }
}
