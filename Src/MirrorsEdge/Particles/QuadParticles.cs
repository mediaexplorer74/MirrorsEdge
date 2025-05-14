// Decompiled with JetBrains decompiler
// Type: particles.QuadParticles
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;
using midp;
using System;

#nullable disable
namespace particles
{
  public class QuadParticles(int maxParticleCount, ParticleMode particleMode) : Particles(maxParticleCount, particleMode)
  {
    private float[] quadPosition = new float[16];
    private float[] colorArray = new float[4];
    private byte[] colorBytes4 = new byte[16];
    private byte[] byteArray = new byte[4];
    private float[] nullFloatArray;
    private float[] nullFloatArray2;
    private float[] upVector = new float[4];
    private float[] sideVector = new float[4];
    private float[] crop = new float[4];
    private float[] cropCoords4 = new float[8];
    private float[] quadPosition2 = new float[16]
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
      if (scale != null)
      {
        float sequenceTime = num1 * (float) scale.getDuration();
        num2 = scale.sample(sequenceTime, 0) * 0.5f;
      }
      else
        num2 = 0.5f;
      KeyframeSequence rotation = this.getParticleMode().getRotation();
      float num3;
      if (rotation != null)
      {
        float sequenceTime = num1 * (float) rotation.getDuration();
        num3 = JMath.toRadians(rotation.sample(sequenceTime, 0));
      }
      else
        num3 = 0.0f;
      float num4 = (float) Math.Sin((double) num3) * num2;
      float num5 = (float) Math.Cos((double) num3) * num2;
      this.upVector[0] = num4;
      this.upVector[1] = num5;
      this.upVector[2] = 0.0f;
      this.upVector[3] = 0.0f;
      this.sideVector[0] = num5;
      this.sideVector[1] = -num4;
      this.sideVector[2] = 0.0f;
      this.sideVector[3] = 0.0f;
      if (cameraTransform != null)
      {
        cameraTransform.transform(this.upVector, 4);
        cameraTransform.transform(this.sideVector, 4);
      }
      for (int index1 = 0; index1 < 4; ++index1)
        this.quadPosition[index1] = position[index1] + this.upVector[index1] - this.sideVector[index1];
      for (int index2 = 0; index2 < 4; ++index2)
        this.quadPosition[4 + index2] = position[index2] + this.upVector[index2] + this.sideVector[index2];
      for (int index3 = 0; index3 < 4; ++index3)
        this.quadPosition[8 + index3] = position[index3] - this.upVector[index3] - this.sideVector[index3];
      for (int index4 = 0; index4 < 4; ++index4)
        this.quadPosition[12 + index4] = position[index4] - this.upVector[index4] + this.sideVector[index4];
      int firstVertex1 = firstVertex + index * 4;
      vertexBuffer.getPositions(ref this.nullFloatArray).set(firstVertex1, 4, this.quadPosition);
      KeyframeSequence color = this.getParticleMode().getColor();
      if (color != null)
      {
        float sequenceTime = num1 * (float) color.getDuration();
        color.sample(sequenceTime, 0, ref this.colorArray);
        QuadParticles.colorFloatsToBytes(this.colorArray, ref this.byteArray);
        for (int index5 = 0; index5 < 16; ++index5)
          this.colorBytes4[index5] = this.byteArray[index5 & 3];
        vertexBuffer.getColors().set(firstVertex1, 4, this.colorBytes4);
      }
      KeyframeSequence crop = this.getParticleMode().getCrop();
      if (crop != null)
      {
        float sequenceTime = num1 * (float) crop.getDuration();
        crop.sample(sequenceTime, 0, ref this.crop);
        float num6 = this.crop[0];
        float num7 = this.crop[1];
        float num8 = this.crop[2];
        float num9 = this.crop[3];
        this.cropCoords4[0] = num6;
        this.cropCoords4[1] = num7;
        this.cropCoords4[2] = num6 + num8;
        this.cropCoords4[3] = num7;
        this.cropCoords4[4] = num6;
        this.cropCoords4[5] = num7 + num9;
        this.cropCoords4[6] = num6 + num8;
        this.cropCoords4[7] = num7 + num9;
        vertexBuffer.getTexCoords(0, ref this.nullFloatArray2).set(firstVertex1, 4, this.cropCoords4);
      }
      vertexBuffer.invalidateVertexData(VertexBuffer.InvalidVertexData.ALL);
    }

    public override void killParticle(int index, int firstVertex, VertexBuffer vertexBuffer)
    {
      base.killParticle(index, firstVertex, vertexBuffer);
      int firstVertex1 = firstVertex + index * 4;
      vertexBuffer.getPositions(ref this.nullFloatArray).set(firstVertex1, 4, this.quadPosition2);
      vertexBuffer.invalidateVertexData(VertexBuffer.InvalidVertexData.POSITIONS);
    }
  }
}
