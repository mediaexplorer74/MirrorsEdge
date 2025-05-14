
// Type: particles.ParticleEffect
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;

#nullable disable
namespace particles
{
  internal class ParticleEffect : Mesh
  {
    private int m_emitterCount;
    private Emitter[] m_emitters;
    private int[] m_emitterVertexOffsets;
    private int m_worldTimeMillis;

    public ParticleEffect(Emitter emitter)
      : base(1, 0)
    {
      this.m_emitterCount = 1;
      this.m_emitters = new Emitter[this.m_emitterCount];
      this.m_emitters[0] = emitter;
      this.m_emitterVertexOffsets = new int[1];
      this.createVertexBuffer();
      this.createIndexBuffers();
      this.updateAppearances();
    }

    public ParticleEffect(Emitter[] emitters)
      : base(emitters.Length, 0)
    {
      this.m_emitterCount = emitters.Length;
      this.m_emitters = new Emitter[this.m_emitterCount];
      for (int index = 0; index < this.m_emitterCount; ++index)
        this.m_emitters[index] = emitters[index];
      this.m_emitterVertexOffsets = new int[emitters.Length];
      this.createVertexBuffer();
      this.createIndexBuffers();
      this.updateAppearances();
    }

    public override void Destructor()
    {
      if (this.m_emitters == null)
        return;
      for (int index = 0; index < this.m_emitterCount; ++index)
        this.m_emitters[index] = (Emitter) null;
      this.m_emitters = (Emitter[]) null;
      this.m_emitterVertexOffsets = (int[]) null;
    }

    public int getEmitterCount() => this.m_emitterCount;

    public Emitter getEmitter(int index) => this.m_emitters[index];

    private void createVertexBuffer()
    {
      bool flag1 = true;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      int vertexCount = 0;
      int emitterCount = this.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
      {
        Particles particles = this.getEmitter(index).getParticles();
        this.m_emitterVertexOffsets[index] = vertexCount;
        vertexCount += particles.getVertexCount();
        flag2 |= particles.hasColors();
        flag3 |= particles.hasNormals();
        flag4 |= particles.hasTexcoords();
        flag5 |= particles.hasPointSizes();
      }
      VertexBuffer vertexBuffer = new VertexBuffer();
      if (flag1)
      {
        VertexArray arr = new VertexArray(vertexCount, 4, 4);
        vertexBuffer.setPositions(arr, 1f, (float[]) null);
      }
      if (flag2)
      {
        VertexArray arr = new VertexArray(vertexCount, 4, 1);
        vertexBuffer.setColors(arr);
      }
      if (flag3)
      {
        VertexArray arr = new VertexArray(vertexCount, 3, 4);
        vertexBuffer.setNormals(arr);
      }
      if (flag4)
      {
        VertexArray arr = new VertexArray(vertexCount, 2, 4);
        vertexBuffer.setTexCoords(0, arr, 1f, (float[]) null);
      }
      if (flag5)
      {
        VertexArray pointSizes = new VertexArray(vertexCount, 1, 4);
        vertexBuffer.setPointSizes(pointSizes);
      }
      this.setVertexBuffer(vertexBuffer);
      this.killAllParticles();
    }

    private void createIndexBuffers()
    {
      int emitterCount = this.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
      {
        IndexBuffer indexBuffer = this.getEmitter(index).getParticles().createIndexBuffer(this.m_emitterVertexOffsets[index]);
        this.setIndexBuffer(index, indexBuffer);
      }
    }

    public void updateAppearances()
    {
      int emitterCount = this.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
      {
        AppearanceBase appearance = this.getEmitter(index).getParticles().getParticleMode().getAppearance();
        this.setAppearanceBase(index, appearance);
      }
    }

    public void updateStep(
      int timeStepTimeMillis,
      Transform cameraTransform,
      Transform invCameraTransform)
    {
      this.update(this.m_worldTimeMillis + timeStepTimeMillis, cameraTransform, invCameraTransform);
    }

    public void update(
      int worldTimeMillis,
      Transform cameraTransform,
      Transform invCameraTransform)
    {
      int emitterCount = this.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
      {
        Emitter emitter = this.getEmitter(index);
        int emitterVertexOffset = this.m_emitterVertexOffsets[index];
        emitter.update(worldTimeMillis, emitterVertexOffset, this.getVertexBuffer(), cameraTransform, invCameraTransform);
      }
      this.m_worldTimeMillis = worldTimeMillis;
    }

    public void killAllParticles()
    {
      int emitterCount = this.getEmitterCount();
      for (int index = 0; index < emitterCount; ++index)
        this.getEmitter(index).killAllParticles(this.m_emitterVertexOffsets[index], this.getVertexBuffer());
    }
  }
}
