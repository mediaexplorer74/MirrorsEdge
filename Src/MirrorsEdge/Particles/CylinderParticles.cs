
// Type: particles.CylinderParticles
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using microedition.m3g;
using System;

#nullable disable
namespace particles
{
  public class CylinderParticles(int maxParticleCount, ParticleMode particleMode) : Particles(maxParticleCount, particleMode)
  {
    private float[] colorArray = new float[4];
    private byte[] colorBytes4 = new byte[16];
    private byte[] byteArray = new byte[4];
    private float[] nullFloatArray;
    private float[] nullFloatArray2;
    private float[] quadPosition = new float[16];
    private float[] quadPositionKill = new float[16]
    {
      0.0f,
      0.0f,
      0.0f,
      1f,
      0.0f,
      0.0f,
      0.0f,
      1f,
      0.0f,
      0.0f,
      0.0f,
      1f,
      0.0f,
      0.0f,
      0.0f,
      1f
    };

    public override void Destructor() => base.Destructor();

    public override int getVertexCount() => this.getMaxParticleCount() * 4;

    public override IndexBuffer createIndexBuffer(int firstVertex)
    {
      int maxParticleCount = this.getMaxParticleCount();
      int[] indices = new int[maxParticleCount * 6];
      for (int index1 = 0; index1 < maxParticleCount; ++index1)
      {
        int num = firstVertex + index1 * 4;
        int index2 = index1 * 6;
        indices[index2] = num;
        indices[index2 + 1] = num + 2;
        indices[index2 + 2] = num + 1;
        indices[index2 + 3] = num + 1;
        indices[index2 + 4] = num + 2;
        indices[index2 + 5] = num + 3;
      }
      return new IndexBuffer(8, maxParticleCount * 2, indices);
    }

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
      float lifetimeDuration = this.getInverseLifetimeDuration(index);
      float num1 = lifetime * lifetimeDuration;
      KeyframeSequence scale = this.getParticleMode().getScale();
      float num2;
      float num3;
      if (scale != null)
      {
        float sequenceTime = num1 * (float) scale.getDuration();
        float[] numArray = new float[2];
        scale.sample(sequenceTime, 0, ref numArray);
        num2 = numArray[0] * 0.5f;
        num3 = numArray[1] * 0.5f;
      }
      else
      {
        num2 = 0.5f;
        num3 = 0.5f;
      }
      float[] vector1 = new float[4];
      float[] vector2 = new float[4];
      invCameraTransform?.transform(velocity, 4);
      vector1[0] = -velocity[0];
      vector1[1] = -velocity[1];
      vector1[2] = velocity[2];
      vector1[3] = 0.0f;
      vector2[0] = -velocity[1];
      vector2[1] = velocity[0];
      vector2[2] = 0.0f;
      vector2[3] = 0.0f;
      float num4 = (float) Math.Sqrt((double) velocity[0] * (double) velocity[0] + (double) velocity[1] * (double) velocity[1] + (double) velocity[2] * (double) velocity[2]);
      if ((double) num4 > 0.0)
      {
        float num5 = 1f / num4;
        vector1[0] *= num5;
        vector1[1] *= num5;
        vector1[2] *= num5;
        vector1[3] = 0.0f;
      }
      float num6 = (float) Math.Sqrt((double) vector2[0] * (double) vector2[0] + (double) vector2[1] * (double) vector2[1]);
      if ((double) num6 > 0.0)
      {
        float num7 = 1f / num6;
        vector2[0] *= num7;
        vector2[1] *= num7;
        vector2[2] = 0.0f;
        vector2[3] = 0.0f;
      }
      if (cameraTransform != null)
      {
        cameraTransform.transform(vector1, 4);
        cameraTransform.transform(vector2, 4);
      }
      vector1[0] *= num3;
      vector1[1] *= num3;
      vector1[2] *= num3;
      vector2[0] *= num2;
      vector2[1] *= num2;
      vector2[2] *= num2;
      for (int index1 = 0; index1 < 4; ++index1)
      {
        this.quadPosition[index1] = position[index1] + vector1[index1] - vector2[index1];
        this.quadPosition[4 + index1] = position[index1] + vector1[index1] + vector2[index1];
        this.quadPosition[8 + index1] = position[index1] - vector1[index1] - vector2[index1];
        this.quadPosition[12 + index1] = position[index1] - vector1[index1] + vector2[index1];
      }
      int firstVertex1 = firstVertex + index * 4;
      vertexBuffer.getPositions(ref this.nullFloatArray).set(firstVertex1, 4, this.quadPosition);
      KeyframeSequence color = this.getParticleMode().getColor();
      if (color != null)
      {
        float sequenceTime = num1 * (float) color.getDuration();
        color.sample(sequenceTime, 0, ref this.colorArray);
        CylinderParticles.colorFloatsToBytes(this.colorArray, ref this.byteArray);
        for (int index2 = 0; index2 < 16; ++index2)
          this.colorBytes4[index2] = this.byteArray[index2 & 3];
        vertexBuffer.getColors().set(firstVertex1, 4, this.colorBytes4);
      }
      KeyframeSequence crop = this.getParticleMode().getCrop();
      if (crop != null)
      {
        float[] numArray = new float[4];
        float sequenceTime = num1 * (float) crop.getDuration();
        crop.sample(sequenceTime, 0, ref numArray);
        float num8 = numArray[0];
        float num9 = numArray[1];
        float num10 = numArray[2];
        float num11 = numArray[3];
        float[] src = new float[8]
        {
          num8,
          num9,
          num8 + num10,
          num9,
          num8,
          num9 + num11,
          num8 + num10,
          num9 + num11
        };
        vertexBuffer.getTexCoords(0, ref this.nullFloatArray2).set(firstVertex1, 4, src);
      }
      vertexBuffer.invalidateVertexData(VertexBuffer.InvalidVertexData.ALL);
    }

    public override void killParticle(int index, int firstVertex, VertexBuffer vertexBuffer)
    {
      base.killParticle(index, firstVertex, vertexBuffer);
      int firstVertex1 = firstVertex + index * 4;
      vertexBuffer.getPositions(ref this.nullFloatArray).set(firstVertex1, 4, this.quadPositionKill);
      vertexBuffer.invalidateVertexData(VertexBuffer.InvalidVertexData.POSITIONS);
    }
  }
}
