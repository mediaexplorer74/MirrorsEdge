
// Type: particles.Particles
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using System;

#nullable disable
namespace particles
{
  public abstract class Particles
  {
    public const int COMPONENT_COUNT = 4;
    private readonly int m_maxParticleCount;
    private ParticleMode m_particleMode;
    private bool[] m_aliveFlags;
    private float[] m_lifetimeDurations;
    private float[] m_inverseLifetimeDurations;
    private float[] m_startTimeMillis;
    private float m_timeMillisOfLastSpawn;
    private float m_timeMillisOfLastUpdate;
    private float[] m_startPositions;
    private float[] m_startVelocities;
    private static float[] position2 = new float[4];
    private static float[] velocity2 = new float[4];
    private static float[] position1 = new float[4];
    private static float[] velocity1 = new float[4];
    private static Transform compositeTransform = new Transform();

    public Particles(int maxParticleCount, ParticleMode particleMode)
    {
      this.m_maxParticleCount = maxParticleCount;
      this.m_particleMode = (ParticleMode) null;
      this.m_particleMode = particleMode;
      this.m_aliveFlags = new bool[maxParticleCount];
      for (int index = 0; index < maxParticleCount; ++index)
        this.m_aliveFlags[index] = false;
      this.m_startTimeMillis = new float[maxParticleCount];
      this.m_lifetimeDurations = new float[maxParticleCount];
      this.m_inverseLifetimeDurations = new float[maxParticleCount];
      this.m_startPositions = new float[4 * maxParticleCount];
      this.m_startVelocities = new float[4 * maxParticleCount];
    }

    public virtual void Destructor()
    {
      this.m_aliveFlags = (bool[]) null;
      this.m_startTimeMillis = (float[]) null;
      this.m_lifetimeDurations = (float[]) null;
      this.m_inverseLifetimeDurations = (float[]) null;
      this.m_startPositions = (float[]) null;
      this.m_startVelocities = (float[]) null;
    }

    public bool isParticleAlive(int index) => this.m_aliveFlags[index];

    public abstract int getVertexCount();

    public abstract IndexBuffer createIndexBuffer(int firstVertex);

    public virtual bool hasColors() => this.getParticleMode().getColor() != null;

    public virtual bool hasNormals() => false;

    public virtual bool hasTexcoords() => this.getParticleMode().getCrop() != null;

    public virtual bool hasPointSizes() => false;

    public float generateLifetimeDuration(Random random)
    {
      ParticleMode particleMode = this.getParticleMode();
      float meanTimeToLive = particleMode.getMeanTimeToLive();
      float timeToLiveDeviation = particleMode.getMeanTimeToLiveDeviation();
      float num = (float) (2.0 * random.NextDouble() - 1.0) * timeToLiveDeviation;
      return meanTimeToLive + num;
    }

    public float generateStartTime(float updateTimeMillis, Random random)
    {
      float num = (float) random.NextDouble();
      return (float) ((double) updateTimeMillis * (1.0 - (double) num) + (double) this.m_timeMillisOfLastUpdate * (double) num);
    }

    public float getLifetimeDuration(int index) => this.m_lifetimeDurations[index];

    public float getInverseLifetimeDuration(int index) => this.m_inverseLifetimeDurations[index];

    public void generateParticle(int index, Emitter emitter, Transform compositeTransform)
    {
      Random randomGenerator = emitter.getRandomGenerator();
      this.setLifetimeDuration(index, this.generateLifetimeDuration(randomGenerator));
      EmissionMode emissionMode = emitter.getEmissionMode();
      emissionMode.getParticleStartPosition(randomGenerator, Particles.position2);
      emissionMode.getParticleStartVelocity(randomGenerator, Particles.velocity2);
      if (compositeTransform != null)
      {
        compositeTransform.transform(Particles.position2, 4);
        compositeTransform.transform(Particles.velocity2, 4);
      }
      int destinationIndex = index * 4;
      Array.Copy((Array) Particles.position2, 0, (Array) this.m_startPositions, destinationIndex, 4);
      Array.Copy((Array) Particles.velocity2, 0, (Array) this.m_startVelocities, destinationIndex, 4);
      this.m_aliveFlags[index] = true;
    }

    public abstract void updateParticle(
      int index,
      int firstVertex,
      VertexBuffer vertexBuffer,
      float[] position,
      float[] velocity,
      float lifetime,
      Transform cameraTransform,
      Transform invCameraTransform);

    public virtual void killParticle(int index, int firstVertex, VertexBuffer vertexBuffer)
    {
      this.m_aliveFlags[index] = false;
    }

    public int getMaxParticleCount() => this.m_maxParticleCount;

    public ParticleMode getParticleMode() => this.m_particleMode;

    public void setLifetimeDuration(int index, float duration)
    {
      this.m_lifetimeDurations[index] = duration;
      this.m_inverseLifetimeDurations[index] = 1f / duration;
    }

    public void setParticleMode(ParticleMode mode) => this.m_particleMode = mode;

    public void update(
      float timeMillis,
      int firstVertex,
      VertexBuffer vertexBuffer,
      Transform cameraTransform,
      Transform invCameraTransform,
      Emitter emitter)
    {
      emitter.getCompositeTransform(ref Particles.compositeTransform);
      EmissionMode emissionMode = emitter.getEmissionMode();
      Random randomGenerator = emitter.getRandomGenerator();
      int maxParticleCount = this.getMaxParticleCount();
      bool flag1 = (double) emissionMode.getRate() > 0.0;
      float num1 = 1000f;
      float num2 = 0.0f;
      if (flag1)
      {
        num1 /= emissionMode.getRate();
        num2 = Math.Abs(timeMillis - this.m_timeMillisOfLastSpawn);
        if ((double) num2 >= (double) num1)
          this.m_timeMillisOfLastSpawn = timeMillis;
      }
      float[] acceleration = emissionMode.getAcceleration();
      for (int index1 = 0; index1 < maxParticleCount; ++index1)
      {
        float lifetime = timeMillis - this.m_startTimeMillis[index1];
        float lifetimeDuration1 = this.m_lifetimeDurations[index1];
        bool flag2 = !this.isParticleAlive(index1);
        if (flag2)
        {
          if ((double) num2 >= (double) num1)
          {
            this.generateParticle(index1, emitter, Particles.compositeTransform);
            float startTime = this.generateStartTime(timeMillis, randomGenerator);
            lifetime = timeMillis - startTime;
            this.m_startTimeMillis[index1] = startTime;
            num2 -= num1;
            flag2 = false;
          }
        }
        else if ((double) lifetime < 0.0)
        {
          if ((double) num2 >= (double) num1)
          {
            this.generateParticle(index1, emitter, Particles.compositeTransform);
            float lifetimeDuration2 = this.getLifetimeDuration(index1);
            float num3 = this.generateStartTime(timeMillis, randomGenerator) - lifetimeDuration2;
            lifetime = timeMillis - num3;
            this.m_startTimeMillis[index1] = num3;
            num2 -= num1;
            flag2 = false;
          }
          else
          {
            flag2 = true;
            this.killParticle(index1, firstVertex, vertexBuffer);
          }
        }
        else if ((double) lifetime > (double) lifetimeDuration1)
        {
          if ((double) num2 >= (double) num1)
          {
            this.generateParticle(index1, emitter, Particles.compositeTransform);
            this.getLifetimeDuration(index1);
            float startTime = this.generateStartTime(timeMillis, randomGenerator);
            lifetime = timeMillis - startTime;
            this.m_startTimeMillis[index1] = startTime;
            num2 -= num1;
            flag2 = false;
          }
          else
          {
            flag2 = true;
            this.killParticle(index1, firstVertex, vertexBuffer);
          }
        }
        if (!flag2)
        {
          float num4 = lifetime * (1f / 1000f);
          for (int index2 = 0; index2 < 4; ++index2)
          {
            int index3 = index1 * 4 + index2;
            Particles.position1[index2] = this.m_startPositions[index3] + num4 * this.m_startVelocities[index3];
            Particles.velocity1[index2] = this.m_startVelocities[index3];
          }
          if (acceleration != null)
          {
            float num5 = (float) ((double) num4 * (double) num4 * 0.5);
            for (int index4 = 0; index4 < 3; ++index4)
            {
              Particles.position1[index4] += num5 * acceleration[index4];
              Particles.velocity1[index4] += num4 * acceleration[index4];
            }
          }
          this.updateParticle(index1, firstVertex, vertexBuffer, Particles.position1, Particles.velocity1, lifetime, cameraTransform, invCameraTransform);
        }
      }
      this.m_timeMillisOfLastUpdate = timeMillis;
    }

    public void killAll(int firstVertex, VertexBuffer vertexBuffer)
    {
      int maxParticleCount = this.getMaxParticleCount();
      for (int index = 0; index < maxParticleCount; ++index)
        this.killParticle(index, firstVertex, vertexBuffer);
    }
  }
}
