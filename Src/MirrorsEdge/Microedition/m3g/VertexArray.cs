// Decompiled with JetBrains decompiler
// Type: microedition.m3g.VertexArray
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using Microsoft.Xna.Framework;
using System.IO;

#nullable disable
namespace microedition.m3g
{
  public class VertexArray : Object3D
  {
    public const int BYTE = 1;
    public const int FIXED = 3;
    public const int FLOAT = 4;
    public const int HALF = 5;
    public const int SHORT = 2;
    public new const int M3G_UNIQUE_CLASS_ID = 20;
    private int m_NumVertices;
    private int m_NumComponents;
    private int m_ComponentType;
    public byte[] m_Data;
    private MemoryStream m_ms;
    private BinaryReader m_br;
    private BinaryWriter m_bw;
    private float[] vData;
    private bool m_DataIsShared;
    private int m_DataOffset;
    private int m_DataStride;
    private int m_ComponentStride;

    public void setFormat(int vertexCount, int componentCount, int componentType)
    {
      if (!this.m_DataIsShared && this.m_Data != null)
        VertexArray.deallocate(ref this.m_Data, this.getComponentType());
      this.m_NumVertices = vertexCount;
      this.m_NumComponents = componentCount;
      this.m_ComponentType = componentType;
      this.vData = (float[]) null;
      if (!this.m_DataIsShared)
      {
        this.m_Data = VertexArray.allocate(this.getVertexCount(), this.getComponentCount(), this.getComponentType());
        this.m_DataStride = VertexArray.componentSize(this.getComponentType()) * this.getComponentCount();
        this.m_ms = (MemoryStream) null;
        this.m_br = (BinaryReader) null;
        this.m_bw = (BinaryWriter) null;
      }
      if (this.m_Data == null || componentType != 1)
        this.vData = new float[vertexCount * this.getComponentCount()];
      this.m_ComponentStride = this.getDataStride() / VertexArray.componentSize(this.getComponentType());
    }

    public static int componentSize(int componentType)
    {
      switch (componentType)
      {
        case 1:
          return 1;
        case 2:
          return 2;
        case 3:
          return 4;
        case 4:
          return 4;
        case 5:
          return 2;
        default:
          return 0;
      }
    }

    public VertexArray()
    {
      this.m_NumVertices = 0;
      this.m_NumComponents = 0;
      this.m_ComponentType = 0;
      this.m_Data = (byte[]) null;
      this.m_ms = (MemoryStream) null;
      this.m_br = (BinaryReader) null;
      this.m_bw = (BinaryWriter) null;
      this.m_DataIsShared = false;
      this.m_DataOffset = 0;
      this.m_DataStride = 0;
      this.m_ComponentStride = 0;
    }

    public VertexArray(int vertexCount, int componentCount, int componentType)
    {
      this.m_NumVertices = 0;
      this.m_NumComponents = 0;
      this.m_ComponentType = 0;
      this.m_Data = (byte[]) null;
      this.m_ms = (MemoryStream) null;
      this.m_br = (BinaryReader) null;
      this.m_bw = (BinaryWriter) null;
      this.m_DataIsShared = false;
      this.m_DataOffset = 0;
      this.m_DataStride = 0;
      this.m_ComponentStride = 0;
      this.setFormat(vertexCount, componentCount, componentType);
    }

    public VertexArray(
      int vertexCount,
      int componentCount,
      int componentType,
      byte[] sharedData,
      int dataOffset,
      int dataStride)
    {
      this.m_NumVertices = 0;
      this.m_NumComponents = 0;
      this.m_ComponentType = 0;
      this.m_Data = sharedData;
      this.m_ms = (MemoryStream) null;
      this.m_br = (BinaryReader) null;
      this.m_bw = (BinaryWriter) null;
      this.m_DataIsShared = true;
      this.m_DataOffset = dataOffset;
      this.m_DataStride = dataStride;
      this.m_ComponentStride = 0;
      this.setFormat(vertexCount, componentCount, componentType);
    }

    public override void Destructor()
    {
      this.m_br = (BinaryReader) null;
      this.m_bw = (BinaryWriter) null;
      this.m_ms = (MemoryStream) null;
      this.m_Data = (byte[]) null;
      this.vData = (float[]) null;
      base.Destructor();
    }

    private static byte[] allocate(int numVertices, int numComponents, int componentType)
    {
      return componentType == 1 ? new byte[numVertices * numComponents] : (byte[]) null;
    }

    private static void deallocate(ref byte[] data, int componentType) => data = (byte[]) null;

    public float[] getFloatArray() => this.vData;

    private static void __getVertsFromInt(
      ref int[] to,
      int length,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      if (vertexCount == 0)
        return;
      int num1 = (int) from.Position + firstVertex * stride * 4;
      int num2 = 0;
      int num3 = vertexCount;
      do
      {
        from.Position = (long) num1;
        int[] numArray1 = to;
        int index1 = num2;
        int num4 = index1 + 1;
        int num5 = br.ReadInt32();
        numArray1[index1] = num5;
        int[] numArray2 = to;
        int index2 = num4;
        num2 = index2 + 1;
        int num6 = br.ReadInt32();
        numArray2[index2] = num6;
        if (componentCount > 2)
          to[num2++] = br.ReadInt32();
        if (componentCount > 3)
          to[num2++] = br.ReadInt32();
        num1 += stride * 4;
      }
      while (--num3 > 0);
    }

    private static void __getVertsFromShort(
      ref short[] to,
      int length,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      if (vertexCount == 0)
        return;
      int num1 = (int) from.Position + firstVertex * stride * 2;
      int num2 = 0;
      int num3 = vertexCount;
      do
      {
        from.Position = (long) num1;
        short[] numArray1 = to;
        int index1 = num2;
        int num4 = index1 + 1;
        int num5 = (int) br.ReadInt16();
        numArray1[index1] = (short) num5;
        short[] numArray2 = to;
        int index2 = num4;
        num2 = index2 + 1;
        int num6 = (int) br.ReadInt16();
        numArray2[index2] = (short) num6;
        if (componentCount > 2)
          to[num2++] = br.ReadInt16();
        if (componentCount > 3)
          to[num2++] = br.ReadInt16();
        num1 += stride * 2;
      }
      while (--num3 > 0);
    }

    private static void __getVertsFromSByte(
      ref byte[] to,
      int length,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      if (vertexCount == 0)
        return;
      int num1 = (int) from.Position + firstVertex * stride;
      int num2 = 0;
      int num3 = vertexCount;
      do
      {
        from.Position = (long) num1;
        byte[] numArray1 = to;
        int index1 = num2;
        int num4 = index1 + 1;
        int num5 = (int) br.ReadByte();
        numArray1[index1] = (byte) num5;
        byte[] numArray2 = to;
        int index2 = num4;
        num2 = index2 + 1;
        int num6 = (int) br.ReadByte();
        numArray2[index2] = (byte) num6;
        if (componentCount > 2)
          to[num2++] = br.ReadByte();
        if (componentCount > 3)
          to[num2++] = br.ReadByte();
        num1 += stride;
      }
      while (--num3 > 0);
    }

    private static void __getVertsFromFloat(
      ref float[] to,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      if (vertexCount == 0)
        return;
      int num1 = (int) from.Position + firstVertex * stride * 4;
      int num2 = 0;
      int num3 = vertexCount;
      do
      {
        from.Position = (long) num1;
        float[] numArray1 = to;
        int index1 = num2;
        int num4 = index1 + 1;
        double num5 = (double) br.ReadSingle();
        numArray1[index1] = (float) num5;
        float[] numArray2 = to;
        int index2 = num4;
        num2 = index2 + 1;
        double num6 = (double) br.ReadSingle();
        numArray2[index2] = (float) num6;
        if (componentCount > 2)
          to[num2++] = br.ReadSingle();
        if (componentCount > 3)
          to[num2++] = br.ReadSingle();
        num1 += stride * 4;
      }
      while (--num3 > 0);
    }

    private static void __getVertsFromSByte(
      ref float[] to,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      if (vertexCount == 0)
        return;
      int num1 = (int) from.Position + firstVertex * stride;
      int num2 = 0;
      int num3 = vertexCount;
      do
      {
        from.Position = (long) num1;
        float[] numArray1 = to;
        int index1 = num2;
        int num4 = index1 + 1;
        double num5 = (double) br.ReadSByte();
        numArray1[index1] = (float) num5;
        float[] numArray2 = to;
        int index2 = num4;
        num2 = index2 + 1;
        double num6 = (double) br.ReadSByte();
        numArray2[index2] = (float) num6;
        if (componentCount > 2)
          to[num2++] = (float) br.ReadSByte();
        if (componentCount > 3)
          to[num2++] = (float) br.ReadSByte();
        num1 += stride;
      }
      while (--num3 > 0);
    }

    private static void __getVertsFromShort(
      ref float[] to,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      if (vertexCount == 0)
        return;
      int num1 = (int) from.Position + firstVertex * stride * 2;
      int num2 = 0;
      int num3 = vertexCount;
      do
      {
        from.Position = (long) num1;
        float[] numArray1 = to;
        int index1 = num2;
        int num4 = index1 + 1;
        double num5 = (double) br.ReadInt16();
        numArray1[index1] = (float) num5;
        float[] numArray2 = to;
        int index2 = num4;
        num2 = index2 + 1;
        double num6 = (double) br.ReadInt16();
        numArray2[index2] = (float) num6;
        if (componentCount > 2)
          to[num2++] = (float) br.ReadInt16();
        if (componentCount > 3)
          to[num2++] = (float) br.ReadInt16();
        num1 += stride * 2;
      }
      while (--num3 > 0);
    }

    private static float fixedToFloat(int value) => (float) value * 1.52587891E-05f;

    private static void __getVertsTransformFromFixed(
      ref float[] to,
      int length,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) from.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        from.Position = (long) (position + (firstVertex + index2) * stride * 4);
        int num = 0;
        while (num < componentCount)
        {
          to[index1] = VertexArray.fixedToFloat(br.ReadInt32());
          ++num;
          ++index1;
        }
      }
    }

    private static float halfToFloat(short v)
    {
      int num1 = v > (short) 0 ? -1 : 1;
      int num2 = ((int) v >> 10 & 31) - 15;
      float num3 = (float) ((int) v & 1023) / 1024f;
      if (num2 != -15)
        ++num3;
      return (float) ((double) num1 * (double) num3 * (double) (1 << num2 + 15) / 32768.0);
    }

    private static void __getVertsTransformFromHalf(
      ref float[] to,
      int length,
      MemoryStream from,
      BinaryReader br,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) from.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        from.Position = (long) (position + (firstVertex + index2) * stride * 2);
        int num = 0;
        while (num < componentCount)
        {
          to[index1] = VertexArray.halfToFloat(br.ReadInt16());
          ++num;
          ++index1;
        }
      }
    }

    public void get(int firstVertex, int numVertices, ref byte[] values)
    {
      int componentStride = this.getComponentStride();
      int componentCount = this.getComponentCount();
      if (numVertices == 0)
        return;
      int num1 = this.m_DataOffset + firstVertex * componentStride;
      int num2 = 0;
      int num3 = numVertices;
      do
      {
        int num4 = num1;
        byte[] numArray1 = values;
        int index1 = num2;
        int num5 = index1 + 1;
        byte[] data1 = this.m_Data;
        int index2 = num4;
        int num6 = index2 + 1;
        int num7 = (int) data1[index2];
        numArray1[index1] = (byte) num7;
        byte[] numArray2 = values;
        int index3 = num5;
        num2 = index3 + 1;
        byte[] data2 = this.m_Data;
        int index4 = num6;
        int num8 = index4 + 1;
        int num9 = (int) data2[index4];
        numArray2[index3] = (byte) num9;
        if (componentCount > 2)
          values[num2++] = this.m_Data[num8++];
        if (componentCount > 3)
        {
          byte[] numArray3 = values;
          int index5 = num2++;
          byte[] data3 = this.m_Data;
          int index6 = num8;
          int num10 = index6 + 1;
          int num11 = (int) data3[index6];
          numArray3[index5] = (byte) num11;
        }
        num1 += componentStride;
      }
      while (--num3 > 0);
    }

    public void get(int firstVertex, int numVertices, ref float[] values)
    {
      int componentStride = this.getComponentStride();
      int componentCount = this.getComponentCount();
      if (numVertices == 0)
        return;
      int num1 = this.m_DataOffset / 4 + firstVertex * componentStride;
      int num2 = 0;
      int num3 = numVertices;
      do
      {
        int num4 = num1;
        float[] numArray1 = values;
        int index1 = num2;
        int num5 = index1 + 1;
        float[] vData1 = this.vData;
        int index2 = num4;
        int num6 = index2 + 1;
        double num7 = (double) vData1[index2];
        numArray1[index1] = (float) num7;
        float[] numArray2 = values;
        int index3 = num5;
        num2 = index3 + 1;
        float[] vData2 = this.vData;
        int index4 = num6;
        int num8 = index4 + 1;
        double num9 = (double) vData2[index4];
        numArray2[index3] = (float) num9;
        if (componentCount > 2)
          values[num2++] = this.vData[num8++];
        if (componentCount > 3)
        {
          float[] numArray3 = values;
          int index5 = num2++;
          float[] vData3 = this.vData;
          int index6 = num8;
          int num10 = index6 + 1;
          double num11 = (double) vData3[index6];
          numArray3[index5] = (float) num11;
        }
        num1 += componentStride;
      }
      while (--num3 > 0);
    }

    public int getComponentCount() => this.m_NumComponents;

    public int getComponentType() => this.m_ComponentType;

    public int getVertexCount() => this.m_NumVertices;

    public void setVertexCount(int numVertices) => this.m_NumVertices = numVertices;

    private void __setVertsSByte(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      byte[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int num1 = 0;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        to.Position = (long) (num1 + (firstVertex + index2) * stride);
        int num2 = 0;
        while (num2 < componentCount)
        {
          bw.Write(from[index1]);
          ++num2;
          ++index1;
        }
      }
    }

    private void __setVertsSByte(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      BinaryReader from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < vertexCount; ++index)
      {
        to.Position = (long) (num2 + (firstVertex + index) * stride);
        int num3 = 0;
        while (num3 < componentCount)
        {
          bw.Write(from.ReadByte());
          ++num3;
          ++num1;
        }
      }
    }

    private void __setVertsShort(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      short[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) to.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        to.Position = (long) (position + (firstVertex + index2) * stride * 2);
        int num = 0;
        while (num < componentCount)
        {
          bw.Write(from[index1]);
          ++num;
          ++index1;
        }
      }
    }

    private void __setVertsShort(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      BinaryReader from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int position = (int) to.Position;
      for (int index1 = 0; index1 < vertexCount; ++index1)
      {
        to.Position = (long) (position + (firstVertex + index1) * stride * 2);
        for (int index2 = 0; index2 < componentCount; ++index2)
          bw.Write(from.ReadInt16());
      }
    }

    private void __setVertsInt(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      int[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) to.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        to.Position = (long) (position + (firstVertex + index2) * stride * 4);
        int num = 0;
        while (num < componentCount)
        {
          bw.Write(from[index1]);
          ++num;
          ++index1;
        }
      }
    }

    private void __setVertsInt(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      BinaryReader from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int num1 = 0;
      int position = (int) to.Position;
      for (int index = 0; index < vertexCount; ++index)
      {
        to.Position = (long) (position + (firstVertex + index) * stride * 4);
        int num2 = 0;
        while (num2 < componentCount)
        {
          bw.Write(from.ReadInt32());
          ++num2;
          ++num1;
        }
      }
    }

    private void __setVertsFloat(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      float[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) to.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        to.Position = (long) (position + (firstVertex + index2) * stride * 4);
        int num = 0;
        while (num < componentCount)
        {
          bw.Write(from[index1]);
          ++num;
          ++index1;
        }
      }
    }

    private void __setVertsFloat(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      BinaryReader from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int num1 = 0;
      int position = (int) to.Position;
      for (int index = 0; index < vertexCount; ++index)
      {
        to.Position = (long) (position + (firstVertex + index) * stride * 4);
        int num2 = 0;
        while (num2 < componentCount)
        {
          bw.Write(from.ReadSingle());
          ++num2;
          ++num1;
        }
      }
    }

    private void __setVertsFloat(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      Vector4[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int position = (int) to.Position;
      for (int index = 0; index < vertexCount; ++index)
      {
        to.Position = (long) (position + (firstVertex + index) * stride * 4);
        bw.Write(from[index].X);
        bw.Write(from[index].Y);
        if (componentCount > 2)
          bw.Write(from[index].Z);
        if (componentCount > 3)
          bw.Write(from[index].W);
      }
    }

    private void __setVertsSByte(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      float[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) to.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        to.Position = (long) (position + (firstVertex + index2) * stride);
        int num = 0;
        while (num < componentCount)
        {
          bw.Write((sbyte) from[index1]);
          ++num;
          ++index1;
        }
      }
    }

    private void __setVertsShort(
      MemoryStream to,
      BinaryWriter bw,
      int length,
      float[] from,
      int firstVertex,
      int vertexCount,
      int componentCount,
      int stride)
    {
      int index1 = 0;
      int position = (int) to.Position;
      for (int index2 = 0; index2 < vertexCount; ++index2)
      {
        to.Position = (long) (position + (firstVertex + index2) * stride * 2);
        int num = 0;
        while (num < componentCount)
        {
          bw.Write((short) from[index1]);
          ++num;
          ++index1;
        }
      }
    }

    public void set(int firstVertex, int numVertices, byte[] src)
    {
      this.setNoCheckLength(firstVertex, numVertices, src);
    }

    public void setNoCheckLength(int firstVertex, int numVertices, byte[] src)
    {
      this.__setVertsSByte(this.getData(), this.m_bw, numVertices * this.getComponentCount(), src, firstVertex, numVertices, this.getComponentCount(), this.getComponentStride());
    }

    public void setByte(int firstVertex, int numVertices, BinaryReader src)
    {
      if (this.getComponentCount() == this.getComponentStride())
      {
        src.Read(this.m_Data, (int) this.getData().Position, numVertices * this.getComponentCount());
        this.getData().Position += (long) (numVertices * this.getComponentCount());
      }
      else
        this.__setVertsSByte(this.getData(), this.m_bw, numVertices * this.getComponentCount(), src, firstVertex, numVertices, this.getComponentCount(), this.getComponentStride());
    }

    public void set(int firstVertex, int numVertices, float[] src)
    {
      int num1 = 0;
      int componentCount = this.getComponentCount();
      int num2 = firstVertex * componentCount;
      for (int index1 = 0; index1 < numVertices; ++index1)
      {
        float[] vData1 = this.vData;
        int index2 = num2;
        int num3 = index2 + 1;
        float[] numArray1 = src;
        int index3 = num1;
        int num4 = index3 + 1;
        double num5 = (double) numArray1[index3];
        vData1[index2] = (float) num5;
        float[] vData2 = this.vData;
        int index4 = num3;
        num2 = index4 + 1;
        float[] numArray2 = src;
        int index5 = num4;
        num1 = index5 + 1;
        double num6 = (double) numArray2[index5];
        vData2[index4] = (float) num6;
        if (componentCount > 2)
          this.vData[num2++] = src[num1++];
        if (componentCount > 3)
          this.vData[num2++] = src[num1++];
      }
    }

    public void setFloat(int firstVertex, int numVertices, BinaryReader src)
    {
      long position = src.BaseStream.Position;
      int componentCount = this.getComponentCount();
      int num1 = firstVertex * componentCount;
      src.BaseStream.Position = position;
      for (int index1 = 0; index1 < numVertices; ++index1)
      {
        float[] vData1 = this.vData;
        int index2 = num1;
        int num2 = index2 + 1;
        double num3 = (double) src.ReadSingle();
        vData1[index2] = (float) num3;
        float[] vData2 = this.vData;
        int index3 = num2;
        num1 = index3 + 1;
        double num4 = (double) src.ReadSingle();
        vData2[index3] = (float) num4;
        if (componentCount > 2)
          this.vData[num1++] = src.ReadSingle();
        if (componentCount > 3)
          this.vData[num1++] = src.ReadSingle();
      }
    }

    public void set(int firstVertex, int numVertices, Vector4[] src)
    {
      this.setNoCheckLength(firstVertex, numVertices, src);
    }

    public void setNoCheckLength(int firstVertex, int numVertices, Vector4[] src)
    {
      int componentCount = this.getComponentCount();
      int num1 = firstVertex * componentCount;
      for (int index1 = 0; index1 < numVertices; ++index1)
      {
        float[] vData1 = this.vData;
        int index2 = num1;
        int num2 = index2 + 1;
        double x = (double) src[index1].X;
        vData1[index2] = (float) x;
        float[] vData2 = this.vData;
        int index3 = num2;
        num1 = index3 + 1;
        double y = (double) src[index1].Y;
        vData2[index3] = (float) y;
        if (componentCount > 2)
          this.vData[num1++] = src[index1].Z;
        if (componentCount > 3)
          this.vData[num1++] = src[index1].W;
      }
    }

    public void set(int firstVertex, int numVertices, int[] src)
    {
      this.setNoCheckLength(firstVertex, numVertices, src);
    }

    public void setNoCheckLength(int firstVertex, int numVertices, int[] src)
    {
      int num1 = 0;
      int componentCount = this.getComponentCount();
      int num2 = firstVertex * componentCount;
      for (int index1 = 0; index1 < numVertices; ++index1)
      {
        float[] vData1 = this.vData;
        int index2 = num2;
        int num3 = index2 + 1;
        int[] numArray1 = src;
        int index3 = num1;
        int num4 = index3 + 1;
        double num5 = (double) ((float) numArray1[index3] / 65536f);
        vData1[index2] = (float) num5;
        float[] vData2 = this.vData;
        int index4 = num3;
        num2 = index4 + 1;
        int[] numArray2 = src;
        int index5 = num4;
        num1 = index5 + 1;
        double num6 = (double) ((float) numArray2[index5] / 65536f);
        vData2[index4] = (float) num6;
        if (componentCount > 2)
          this.vData[num2++] = (float) src[num1++] / 65536f;
        if (componentCount > 3)
          this.vData[num2++] = (float) src[num1++] / 65536f;
      }
    }

    public void setFixed(int firstVertex, int numVertices, BinaryReader src)
    {
      long position = src.BaseStream.Position;
      int componentCount = this.getComponentCount();
      int num1 = firstVertex * componentCount;
      src.BaseStream.Position = position;
      for (int index1 = 0; index1 < numVertices; ++index1)
      {
        float[] vData1 = this.vData;
        int index2 = num1;
        int num2 = index2 + 1;
        double num3 = (double) ((float) src.ReadInt32() / 65536f);
        vData1[index2] = (float) num3;
        float[] vData2 = this.vData;
        int index3 = num2;
        num1 = index3 + 1;
        double num4 = (double) ((float) src.ReadInt32() / 65536f);
        vData2[index3] = (float) num4;
        if (componentCount > 2)
          this.vData[num1++] = (float) src.ReadInt32() / 65536f;
        if (componentCount > 3)
          this.vData[num1++] = (float) src.ReadInt32() / 65536f;
      }
    }

    public void set(int firstVertex, int numVertices, short[] src)
    {
      int num1 = 0;
      int componentCount = this.getComponentCount();
      int num2 = firstVertex * componentCount;
      for (int index1 = 0; index1 < numVertices; ++index1)
      {
        if (this.m_ComponentType == 2)
        {
          float[] vData1 = this.vData;
          int index2 = num2;
          int num3 = index2 + 1;
          short[] numArray1 = src;
          int index3 = num1;
          int num4 = index3 + 1;
          double num5 = (double) numArray1[index3];
          vData1[index2] = (float) num5;
          float[] vData2 = this.vData;
          int index4 = num3;
          num2 = index4 + 1;
          short[] numArray2 = src;
          int index5 = num4;
          num1 = index5 + 1;
          double num6 = (double) numArray2[index5];
          vData2[index4] = (float) num6;
          if (componentCount > 2)
            this.vData[num2++] = (float) src[num1++];
          if (componentCount > 3)
            this.vData[num2++] = (float) src[num1++];
        }
        else if (this.m_ComponentType == 5)
        {
          float[] vData3 = this.vData;
          int index6 = num2;
          int num7 = index6 + 1;
          short[] numArray3 = src;
          int index7 = num1;
          int num8 = index7 + 1;
          double num9 = (double) VertexArray.halfToFloat(numArray3[index7]);
          vData3[index6] = (float) num9;
          float[] vData4 = this.vData;
          int index8 = num7;
          num2 = index8 + 1;
          short[] numArray4 = src;
          int index9 = num8;
          num1 = index9 + 1;
          double num10 = (double) VertexArray.halfToFloat(numArray4[index9]);
          vData4[index8] = (float) num10;
          if (componentCount > 2)
            this.vData[num2++] = VertexArray.halfToFloat(src[num1++]);
          if (componentCount > 3)
            this.vData[num2++] = VertexArray.halfToFloat(src[num1++]);
        }
      }
    }

    public void setShortHalf(int firstVertex, int numVertices, BinaryReader src)
    {
      long position = src.BaseStream.Position;
      int componentCount = this.getComponentCount();
      int num1 = firstVertex * componentCount;
      src.BaseStream.Position = position;
      if (this.m_ComponentType == 2)
      {
        for (int index = 0; index < numVertices * componentCount; ++index)
          this.vData[num1++] = (float) src.ReadInt16();
      }
      else
      {
        if (this.m_ComponentType != 5)
          return;
        for (int index1 = 0; index1 < numVertices; ++index1)
        {
          float[] vData1 = this.vData;
          int index2 = num1;
          int num2 = index2 + 1;
          double num3 = (double) VertexArray.halfToFloat(src.ReadInt16());
          vData1[index2] = (float) num3;
          float[] vData2 = this.vData;
          int index3 = num2;
          num1 = index3 + 1;
          double num4 = (double) VertexArray.halfToFloat(src.ReadInt16());
          vData2[index3] = (float) num4;
          if (componentCount > 2)
            this.vData[num1++] = VertexArray.halfToFloat(src.ReadInt16());
          if (componentCount > 3)
            this.vData[num1++] = VertexArray.halfToFloat(src.ReadInt16());
        }
      }
    }

    public void convert(int componentType)
    {
      float[] values = new float[this.getVertexCount() * this.getComponentCount()];
      this.get(0, this.getVertexCount(), ref values);
      this.setFormat(this.getVertexCount(), this.getComponentCount(), componentType);
      switch (componentType)
      {
        case 1:
          this.__setVertsSByte(this.getData(), this.m_bw, values.Length, values, 0, this.getVertexCount(), this.getComponentCount(), this.getComponentStride());
          break;
        case 2:
          this.__setVertsShort(this.getData(), this.m_bw, values.Length, values, 0, this.getVertexCount(), this.getComponentCount(), this.getComponentStride());
          break;
        case 4:
          this.__setVertsFloat(this.getData(), this.m_bw, values.Length, values, 0, this.getVertexCount(), this.getComponentCount(), this.getComponentStride());
          break;
      }
    }

    public MemoryStream getData()
    {
      if (this.m_ms == null && this.m_Data != null)
      {
        this.m_ms = new MemoryStream(this.m_Data);
        this.m_br = new BinaryReader((Stream) this.m_ms);
        this.m_bw = new BinaryWriter((Stream) this.m_ms);
      }
      this.m_ms.Position = (long) this.m_DataOffset;
      return this.m_ms;
    }

    public int getDataOffset() => this.m_DataOffset;

    public int getVertexDataSize()
    {
      return this.getComponentCount() * VertexArray.componentSize(this.getComponentType());
    }

    public int getDataStride() => this.m_DataStride;

    public int getComponentStride() => this.m_ComponentStride;

    public override int getM3GUniqueClassID() => 20;
  }
}
