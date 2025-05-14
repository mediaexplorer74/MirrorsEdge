// Decompiled with JetBrains decompiler
// Type: particles.PointParticles
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace particles
{
  public class PointParticles(int maxParticleCount, ParticleMode particleMode) : Particles(maxParticleCount, particleMode)
  {
    private float[] colorArray = new float[4];
    private byte[] byteArray = new byte[4];
    private float[] nullFloatArray;
    private float[] pointSize = new float[1];

    public override void Destructor() => base.Destructor();

    public override int getVertexCount() => this.getMaxParticleCount();

    public override IndexBuffer createIndexBuffer(int firstVertex)
    {
      return new IndexBuffer(10, this.getVertexCount(), firstVertex);
    }

    public override bool hasTexcoords() => false;

    public override bool hasPointSizes() => true;

    private static void colorFloatsToBytes(float[] floats, ref byte[] bytes)
    {
      for (int index = 0; index < 4; ++index)
        bytes[index] = (byte) ((double) floats[index] * (double) byte.MaxValue);
    }

    public override void updateParticle(
      int index,
      int firstVertex,
      VertexBuffer vertexBuffer,
      float[] position,
      float[] velocity,
      float lifetime,
      Transform cameraTransform,
      Transform invCameraTransform)
    {
      vertexBuffer.getPositions(ref this.nullFloatArray).set(firstVertex + index, 1, position);
      KeyframeSequence color = this.getParticleMode().getColor();
      if (color != null)
      {
        color.sample(lifetime, 0, ref this.colorArray);
        PointParticles.colorFloatsToBytes(this.colorArray, ref this.byteArray);
        vertexBuffer.getColors().set(firstVertex + index, 1, this.byteArray);
      }
      vertexBuffer.invalidateVertexData(VertexBuffer.InvalidVertexData.ALL);
    }

    public override void killParticle(int index, int firstVertex, VertexBuffer vertexBuffer)
    {
      base.killParticle(index, firstVertex, vertexBuffer);
      this.pointSize[0] = 0.0f;
      vertexBuffer.getPointSizes().set(firstVertex + index, 1, this.pointSize);
      vertexBuffer.invalidateVertexData(VertexBuffer.InvalidVertexData.POSITIONS);
    }
  }
}
