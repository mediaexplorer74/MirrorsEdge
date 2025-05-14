
// Type: microedition.m3g.VertexBuffer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mirrorsedge_wp7;
using System;
using System.Collections.Generic;

#nullable disable
namespace microedition.m3g
{
  public class VertexBuffer : Object3D
  {
    public new const int M3G_UNIQUE_CLASS_ID = 21;
    private List<VertexBuffer.TranslatedPart> partsToDelete = new List<VertexBuffer.TranslatedPart>();
    private Transform trans0 = new Transform();
    private Transform trans1 = new Transform();
    private int m_VertexCount;
    private uint m_DefaultColor;
    private VertexArray m_Positions;
    private float m_PositionScale;
    private float m_PositionBiasX;
    private float m_PositionBiasY;
    private float m_PositionBiasZ;
    private float texCoordTransX;
    private float texCoordTransY;
    private float texCoordTransZ;
    private VertexArray m_Colors;
    private VertexArray m_Normals;
    private VertexArray m_PointSizes;
    private VertexArrayTextureUnit[] m_TextureUnits;
    private VertexArray m_SkinIndices;
    private VertexArray m_SkinWeights;
    private Vertex[] finalVertexData;
    private Vertex2D[] finalVertexData2D;
    private SkinnedVertex[] finalSkinnedVertexData;
    private VertexBuffer.InvalidVertexData needUpdateVertexData = VertexBuffer.InvalidVertexData.ALL;
    private Matrix[] prevTrans = new Matrix[2];
    private List<VertexBuffer.TranslatedPart> translatedParts = new List<VertexBuffer.TranslatedPart>();
    public Microsoft.Xna.Framework.Graphics.VertexBuffer finalVertexBuffer;
    public bool useVertexBuffer = true;
    public bool canUseVertexBuffer;
    private bool m_Mutable;
    private byte[] m_SharedBufferData;

    public VertexBuffer()
    {
      this.m_VertexCount = 0;
      this.m_DefaultColor = uint.MaxValue;
      this.m_Positions = (VertexArray) null;
      this.m_PositionScale = 1f;
      this.m_PositionBiasX = 0.0f;
      this.m_PositionBiasY = 0.0f;
      this.m_PositionBiasZ = 0.0f;
      this.m_Colors = (VertexArray) null;
      this.m_Normals = (VertexArray) null;
      this.m_PointSizes = (VertexArray) null;
      this.m_TextureUnits = new VertexArrayTextureUnit[2];
      this.m_SkinIndices = (VertexArray) null;
      this.m_SkinWeights = (VertexArray) null;
      this.m_Mutable = true;
      this.m_SharedBufferData = (byte[]) null;
      this.prevTrans[0] = Matrix.Identity;
      this.prevTrans[1] = Matrix.Identity;
    }

    public override void Destructor()
    {
      if (this.m_Positions != null)
        this.m_Positions.Destructor();
      this.m_Positions = (VertexArray) null;
      if (this.m_Colors != null)
        this.m_Colors.Destructor();
      this.m_Colors = (VertexArray) null;
      if (this.m_Normals != null)
        this.m_Normals.Destructor();
      this.m_Normals = (VertexArray) null;
      if (this.m_PointSizes != null)
        this.m_PointSizes.Destructor();
      this.m_PointSizes = (VertexArray) null;
      this.m_TextureUnits = (VertexArrayTextureUnit[]) null;
      this.finalVertexData = (Vertex[]) null;
      this.finalVertexData2D = (Vertex2D[]) null;
      this.finalVertexBuffer = (Microsoft.Xna.Framework.Graphics.VertexBuffer) null;
      this.m_SharedBufferData = (byte[]) null;
      base.Destructor();
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      VertexBuffer vertexBuffer = (VertexBuffer) ret;
      vertexBuffer.setDefaultColor(this.getDefaultColor());
      float[] scaleBias1 = new float[4];
      VertexArray positions = this.getPositions(ref scaleBias1);
      float scale1 = scaleBias1[0];
      float[] numArray1 = new float[scaleBias1.Length - 1];
      Array.Copy((Array) scaleBias1, 1, (Array) numArray1, 0, scaleBias1.Length - 1);
      vertexBuffer.setPositions(positions, scale1, numArray1);
      vertexBuffer.setColors(this.getColors());
      vertexBuffer.setNormals(this.getNormals());
      vertexBuffer.setPointSizes(this.getPointSizes());
      vertexBuffer.setSkinIndices(this.getSkinIndices());
      vertexBuffer.setSkinWeights(this.getSkinWeights());
      float[] scaleBias2 = new float[4];
      for (int index = 0; index < 2; ++index)
      {
        VertexArray texCoords = this.getTexCoords(index, ref scaleBias2);
        float scale2 = scaleBias2[0];
        float[] numArray2 = new float[scaleBias2.Length - 1];
        Array.Copy((Array) scaleBias2, 1, (Array) numArray2, 0, scaleBias2.Length - 1);
        vertexBuffer.setTexCoords(index, texCoords, scale2, numArray2);
      }
    }

    private static bool verifyTextureUnit(int index) => index >= 0 && index < 2;

    private bool verifyMutable() => this.m_Mutable;

    private void updateVertexCount()
    {
      if (this.m_Positions != null)
        this.m_VertexCount = this.m_Positions.getVertexCount();
      else if (this.m_Colors != null)
        this.m_VertexCount = this.m_Colors.getVertexCount();
      else if (this.m_Normals != null)
        this.m_VertexCount = this.m_Normals.getVertexCount();
      else if (this.m_TextureUnits[0] != null)
        this.m_VertexCount = this.m_TextureUnits[0].getVertexCount();
      else if (this.m_TextureUnits[1] != null)
        this.m_VertexCount = this.m_TextureUnits[1].getVertexCount();
      else
        this.m_VertexCount = 0;
    }

    public VertexArray getColors() => this.m_Colors;

    public uint getDefaultColor() => this.m_DefaultColor;

    public VertexArray getNormals() => this.m_Normals;

    public VertexArray getPointSizes() => this.m_PointSizes;

    public VertexArray getPositions(ref float[] scaleBias)
    {
      if (scaleBias != null)
      {
        scaleBias[0] = this.m_PositionScale;
        scaleBias[1] = this.m_PositionBiasX;
        scaleBias[2] = this.m_PositionBiasY;
        scaleBias[3] = this.m_PositionBiasZ;
      }
      return this.m_Positions;
    }

    public VertexArray getPositions() => this.m_Positions;

    public VertexArray getTexCoords(int index, ref float[] scaleBias)
    {
      if (!VertexBuffer.verifyTextureUnit(index))
        return (VertexArray) null;
      VertexArrayTextureUnit textureUnit = this.m_TextureUnits[index];
      if (textureUnit == null)
        return (VertexArray) null;
      if (scaleBias != null)
      {
        scaleBias[0] = textureUnit.texcoordScale;
        scaleBias[1] = textureUnit.texcoordBiasU;
        scaleBias[2] = textureUnit.texcoordBiasV;
        scaleBias[3] = 0.0f;
      }
      return textureUnit.texcoords;
    }

    public VertexArray getTexCoords(int index)
    {
      if (!VertexBuffer.verifyTextureUnit(index))
        return (VertexArray) null;
      return this.m_TextureUnits[index]?.texcoords;
    }

    public void setColors(VertexArray arr)
    {
      if (!this.verifyMutable())
        return;
      this.m_Colors = arr;
      this.updateVertexCount();
      this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.COLORS;
    }

    public void setDefaultColor(uint ARGB) => this.m_DefaultColor = ARGB;

    public void setNormals(VertexArray arr)
    {
      if (!this.verifyMutable())
        return;
      this.m_Normals = arr;
      this.updateVertexCount();
      this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.NORMALS;
    }

    public void setPointSizes(VertexArray pointSizes)
    {
      this.m_PointSizes = pointSizes;
      this.updateVertexCount();
    }

    public void setPositions(VertexArray arr, float scale, float[] bias)
    {
      this.setPositions(arr, scale, bias, 0);
    }

    public void setPositions(VertexArray arr, float scale, float[] bias, int sindex)
    {
      if (!this.verifyMutable())
        return;
      this.m_Positions = arr;
      this.m_PositionScale = scale;
      if (bias != null)
      {
        this.m_PositionBiasX = bias[sindex];
        this.m_PositionBiasY = bias[sindex + 1];
        this.m_PositionBiasZ = bias[sindex + 2];
      }
      else
      {
        this.m_PositionBiasX = 0.0f;
        this.m_PositionBiasY = 0.0f;
        this.m_PositionBiasZ = 0.0f;
      }
      this.updateVertexCount();
      this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.POSITIONS;
    }

    public void setTexCoords(int index, VertexArray arr, float scale, float[] bias)
    {
      this.setTexCoords(index, arr, scale, bias, 0);
    }

    public void setTexCoords(int index, VertexArray arr, float scale, float[] bias, int sindex)
    {
      if (!VertexBuffer.verifyTextureUnit(index) || !this.verifyMutable())
        return;
      this.m_TextureUnits[index] = arr == null ? (VertexArrayTextureUnit) null : new VertexArrayTextureUnit(arr, scale, bias, sindex);
      this.updateVertexCount();
      if (index == 0)
        this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.TEXTURES0;
      else
        this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.TEXTURES1;
    }

    public int getVertexCount() => this.m_VertexCount;

    public VertexArray getSkinIndices() => this.m_SkinIndices;

    public VertexArray getSkinWeights() => this.m_SkinWeights;

    public void setSkinIndices(VertexArray indices)
    {
      if (this.m_SkinIndices != null)
        this.m_SkinIndices = (VertexArray) null;
      this.m_SkinIndices = indices;
      this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.SKIN_INDICIES;
    }

    public void setSkinWeights(VertexArray weights)
    {
      if (this.m_SkinWeights != null)
        this.m_SkinWeights = (VertexArray) null;
      this.m_SkinWeights = weights;
      this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.SKIN_WEIGHTS;
    }

    public static VertexArray createSharedVertexArray(
      VertexArray oldArr,
      byte[] sharedData,
      int dataOffset,
      int dataStride)
    {
      int vertexCount = oldArr.getVertexCount();
      int componentCount = oldArr.getComponentCount();
      int componentType = oldArr.getComponentType();
      int length = vertexCount * componentCount;
      VertexArray sharedVertexArray = new VertexArray(vertexCount, componentCount, componentType, sharedData, dataOffset, dataStride);
      switch (componentType)
      {
        case 1:
          byte[] values1 = new byte[length];
          oldArr.get(0, vertexCount, ref values1);
          sharedVertexArray.set(0, vertexCount, values1);
          break;
        case 2:
          float[] values2 = new float[length];
          oldArr.get(0, vertexCount, ref values2);
          sharedVertexArray.set(0, vertexCount, values2);
          break;
        case 4:
          float[] values3 = new float[length];
          oldArr.get(0, vertexCount, ref values3);
          sharedVertexArray.set(0, vertexCount, values3);
          break;
      }
      return sharedVertexArray;
    }

    public static int alignOffset(int offset)
    {
      int num = offset & 3;
      return num > 0 ? offset + (4 - num) : offset;
    }

    public void commit()
    {
      if (!this.m_Mutable)
        return;
      float[] scaleBias1 = new float[4];
      VertexArray positions = this.getPositions(ref scaleBias1);
      VertexArray normals = this.getNormals();
      VertexArray colors = this.getColors();
      float[] scaleBias2 = new float[4];
      VertexArray texCoords1 = this.getTexCoords(0, ref scaleBias2);
      float[] scaleBias3 = new float[4];
      VertexArray texCoords2 = this.getTexCoords(1, ref scaleBias3);
      int dataOffset1 = 0;
      int offset1 = VertexBuffer.alignOffset(positions.getVertexDataSize());
      int dataOffset2 = offset1;
      if (normals != null)
        offset1 += normals.getVertexDataSize();
      int offset2 = VertexBuffer.alignOffset(offset1);
      int dataOffset3 = offset2;
      if (colors != null)
        offset2 += colors.getVertexDataSize();
      int offset3 = VertexBuffer.alignOffset(offset2);
      int dataOffset4 = offset3;
      if (texCoords1 != null)
        offset3 += texCoords1.getVertexDataSize();
      int offset4 = VertexBuffer.alignOffset(offset3);
      int dataOffset5 = offset4;
      if (texCoords2 != null)
        offset4 += texCoords2.getVertexDataSize();
      int dataStride = VertexBuffer.alignOffset(offset4);
      byte[] sharedData = new byte[dataStride * this.getVertexCount()];
      this.setPositions(VertexBuffer.createSharedVertexArray(positions, sharedData, dataOffset1, dataStride), scaleBias1[0], new float[3]
      {
        scaleBias1[1],
        scaleBias1[2],
        scaleBias1[3]
      });
      if (normals != null)
        this.setNormals(VertexBuffer.createSharedVertexArray(normals, sharedData, dataOffset2, dataStride));
      if (colors != null)
        this.setColors(VertexBuffer.createSharedVertexArray(colors, sharedData, dataOffset3, dataStride));
      if (texCoords1 != null)
        this.setTexCoords(0, VertexBuffer.createSharedVertexArray(texCoords1, sharedData, dataOffset4, dataStride), scaleBias2[0], new float[3]
        {
          scaleBias2[1],
          scaleBias2[2],
          scaleBias2[3]
        });
      if (texCoords2 != null)
        this.setTexCoords(1, VertexBuffer.createSharedVertexArray(texCoords2, sharedData, dataOffset5, dataStride), scaleBias3[0], new float[3]
        {
          scaleBias3[1],
          scaleBias3[2],
          scaleBias3[3]
        });
      this.m_SharedBufferData = sharedData;
      this.m_Mutable = false;
    }

    public void processPositions()
    {
      if (this.m_Positions == null)
      {
        this.finalVertexData = (Vertex[]) null;
        this.finalVertexData2D = (Vertex2D[]) null;
      }
      else
      {
        int vertexCount = this.m_Positions.getVertexCount();
        if (vertexCount <= 0)
          return;
        float[] floatArray = this.m_Positions.getFloatArray();
        int num1 = 0;
        if (this.m_Normals != null)
        {
          if (this.finalVertexData == null || this.finalVertexData.Length < vertexCount)
            this.finalVertexData = new Vertex[vertexCount];
          for (int index1 = 0; index1 < vertexCount; ++index1)
          {
            ref Vector3 local1 = ref this.finalVertexData[index1].position;
            float[] numArray1 = floatArray;
            int index2 = num1;
            int num2 = index2 + 1;
            double num3 = (double) numArray1[index2] * (double) this.m_PositionScale + (double) this.m_PositionBiasX;
            local1.X = (float) num3;
            ref Vector3 local2 = ref this.finalVertexData[index1].position;
            float[] numArray2 = floatArray;
            int index3 = num2;
            int num4 = index3 + 1;
            double num5 = (double) numArray2[index3] * (double) this.m_PositionScale + (double) this.m_PositionBiasY;
            local2.Y = (float) num5;
            ref Vector3 local3 = ref this.finalVertexData[index1].position;
            float[] numArray3 = floatArray;
            int index4 = num4;
            num1 = index4 + 1;
            double num6 = (double) numArray3[index4] * (double) this.m_PositionScale + (double) this.m_PositionBiasZ;
            local3.Z = (float) num6;
            if (this.m_Positions.getComponentCount() == 4)
              ++num1;
          }
        }
        else
        {
          if (this.finalVertexData2D == null || this.finalVertexData2D.Length < vertexCount)
            this.finalVertexData2D = new Vertex2D[vertexCount];
          for (int index5 = 0; index5 < vertexCount; ++index5)
          {
            ref Vector3 local4 = ref this.finalVertexData2D[index5].position;
            float[] numArray4 = floatArray;
            int index6 = num1;
            int num7 = index6 + 1;
            double num8 = (double) numArray4[index6] * (double) this.m_PositionScale + (double) this.m_PositionBiasX;
            local4.X = (float) num8;
            ref Vector3 local5 = ref this.finalVertexData2D[index5].position;
            float[] numArray5 = floatArray;
            int index7 = num7;
            int num9 = index7 + 1;
            double num10 = (double) numArray5[index7] * (double) this.m_PositionScale + (double) this.m_PositionBiasY;
            local5.Y = (float) num10;
            ref Vector3 local6 = ref this.finalVertexData2D[index5].position;
            float[] numArray6 = floatArray;
            int index8 = num9;
            num1 = index8 + 1;
            double num11 = (double) numArray6[index8] * (double) this.m_PositionScale + (double) this.m_PositionBiasZ;
            local6.Z = (float) num11;
            if (this.m_Positions.getComponentCount() == 4)
              ++num1;
          }
        }
      }
    }

    public void processSkinnedPositions()
    {
      if (this.m_Positions == null)
      {
        this.finalSkinnedVertexData = (SkinnedVertex[]) null;
      }
      else
      {
        int vertexCount = this.m_Positions.getVertexCount();
        if (vertexCount <= 0)
          return;
        float[] floatArray = this.m_Positions.getFloatArray();
        int num1 = 0;
        if (this.finalSkinnedVertexData == null || this.finalSkinnedVertexData.Length < vertexCount)
          this.finalSkinnedVertexData = new SkinnedVertex[vertexCount];
        for (int index1 = 0; index1 < vertexCount; ++index1)
        {
          ref Vector3 local1 = ref this.finalSkinnedVertexData[index1].position;
          float[] numArray1 = floatArray;
          int index2 = num1;
          int num2 = index2 + 1;
          double num3 = (double) numArray1[index2] * (double) this.m_PositionScale + (double) this.m_PositionBiasX;
          local1.X = (float) num3;
          ref Vector3 local2 = ref this.finalSkinnedVertexData[index1].position;
          float[] numArray2 = floatArray;
          int index3 = num2;
          int num4 = index3 + 1;
          double num5 = (double) numArray2[index3] * (double) this.m_PositionScale + (double) this.m_PositionBiasY;
          local2.Y = (float) num5;
          ref Vector3 local3 = ref this.finalSkinnedVertexData[index1].position;
          float[] numArray3 = floatArray;
          int index4 = num4;
          num1 = index4 + 1;
          double num6 = (double) numArray3[index4] * (double) this.m_PositionScale + (double) this.m_PositionBiasZ;
          local3.Z = (float) num6;
          if (this.m_Positions.getComponentCount() == 4)
            ++num1;
          this.finalSkinnedVertexData[index1].normal.X = 1f;
          this.finalSkinnedVertexData[index1].normal.Y = 0.0f;
          this.finalSkinnedVertexData[index1].normal.Z = 0.0f;
        }
      }
    }

    public void processSkinned()
    {
      if (this.m_SkinIndices == null || this.m_SkinWeights == null)
      {
        this.finalSkinnedVertexData = (SkinnedVertex[]) null;
      }
      else
      {
        int vertexCount = this.m_SkinIndices.getVertexCount();
        if (vertexCount <= 0)
          return;
        byte[] data = this.m_SkinIndices.m_Data;
        float[] floatArray = this.m_SkinWeights.getFloatArray();
        int index1 = 0;
        if (this.finalSkinnedVertexData == null || this.finalSkinnedVertexData.Length < vertexCount)
          this.finalSkinnedVertexData = new SkinnedVertex[vertexCount];
        for (int index2 = 0; index2 < vertexCount; ++index2)
        {
          this.finalSkinnedVertexData[index2].skinIndex0 = data[index1];
          ref Vector4 local1 = ref this.finalSkinnedVertexData[index2].skinWeight;
          float[] numArray1 = floatArray;
          int index3 = index1;
          int index4 = index3 + 1;
          double num1 = (double) numArray1[index3];
          local1.X = (float) num1;
          this.finalSkinnedVertexData[index2].skinIndex1 = data[index4];
          ref Vector4 local2 = ref this.finalSkinnedVertexData[index2].skinWeight;
          float[] numArray2 = floatArray;
          int index5 = index4;
          int index6 = index5 + 1;
          double num2 = (double) numArray2[index5];
          local2.Y = (float) num2;
          this.finalSkinnedVertexData[index2].skinIndex2 = data[index6];
          ref Vector4 local3 = ref this.finalSkinnedVertexData[index2].skinWeight;
          float[] numArray3 = floatArray;
          int index7 = index6;
          int index8 = index7 + 1;
          double num3 = (double) numArray3[index7];
          local3.Z = (float) num3;
          this.finalSkinnedVertexData[index2].skinIndex3 = data[index8];
          ref Vector4 local4 = ref this.finalSkinnedVertexData[index2].skinWeight;
          float[] numArray4 = floatArray;
          int index9 = index8;
          index1 = index9 + 1;
          double num4 = (double) numArray4[index9];
          local4.W = (float) num4;
        }
      }
    }

    public void setTexCoordTranslation(float x, float y, float z)
    {
      this.texCoordTransX = x;
      this.texCoordTransY = y;
      this.texCoordTransZ = z;
      this.processTextureCoords(0, Matrix.Identity, 0, this.m_TextureUnits[0].texcoords.getVertexCount() - 1);
    }

    public void processTextureCoords(int index, Matrix mat, int minIndex, int maxIndex)
    {
      if (this.m_TextureUnits == null || this.m_TextureUnits[index] == null)
        return;
      int vertexCount = this.m_TextureUnits[index].texcoords.getVertexCount();
      if (maxIndex - minIndex < 0)
        return;
      mat = new Matrix(this.m_TextureUnits[index].texcoordScale, 0.0f, 0.0f, 0.0f, 0.0f, this.m_TextureUnits[index].texcoordScale, 0.0f, 0.0f, 0.0f, 0.0f, this.m_TextureUnits[index].texcoordScale, 0.0f, this.m_TextureUnits[index].texcoordBiasU + this.texCoordTransX, this.m_TextureUnits[index].texcoordBiasV + this.texCoordTransY, 0.0f, 1f) * mat;
      bool flag1 = !mat.Equals(Matrix.Identity);
      float[] floatArray = this.m_TextureUnits[index].texcoords.getFloatArray();
      bool flag2 = this.m_TextureUnits[index].texcoords.getComponentCount() > 2;
      int num1 = minIndex * (flag2 ? 3 : 2);
      Vector2 position = new Vector2();
      if (this.m_Normals != null)
      {
        if (this.finalVertexData == null || this.finalVertexData.Length < vertexCount)
          this.finalVertexData = new Vertex[vertexCount];
        if (floatArray != null)
        {
          if (flag2)
          {
            for (int index1 = minIndex; index1 <= maxIndex; ++index1)
            {
              if (flag1)
              {
                float[] numArray1 = floatArray;
                int index2 = num1;
                int num2 = index2 + 1;
                float x = numArray1[index2];
                float[] numArray2 = floatArray;
                int index3 = num2;
                int num3 = index3 + 1;
                float y = numArray2[index3];
                float[] numArray3 = floatArray;
                int index4 = num3;
                num1 = index4 + 1;
                float z = numArray3[index4];
                Vector3 vector3 = Vector3.Transform(new Vector3(x, y, z), mat);
                position.X = vector3.X;
                position.Y = vector3.Y;
              }
              else
              {
                ref Vector2 local1 = ref position;
                float[] numArray4 = floatArray;
                int index5 = num1;
                int num4 = index5 + 1;
                double num5 = (double) numArray4[index5];
                local1.X = (float) num5;
                ref Vector2 local2 = ref position;
                float[] numArray5 = floatArray;
                int index6 = num4;
                int num6 = index6 + 1;
                double num7 = (double) numArray5[index6];
                local2.Y = (float) num7;
                num1 = num6 + 1;
              }
              if (index == 0)
                this.finalVertexData[index1].textureCoordinate = position;
              else
                this.finalVertexData[index1].textureCoordinate2 = position;
            }
          }
          else
          {
            for (int index7 = minIndex; index7 <= maxIndex; ++index7)
            {
              ref Vector2 local3 = ref position;
              float[] numArray6 = floatArray;
              int index8 = num1;
              int num8 = index8 + 1;
              double num9 = (double) numArray6[index8];
              local3.X = (float) num9;
              ref Vector2 local4 = ref position;
              float[] numArray7 = floatArray;
              int index9 = num8;
              num1 = index9 + 1;
              double num10 = (double) numArray7[index9];
              local4.Y = (float) num10;
              if (flag1)
                position = Vector2.Transform(position, mat);
              if (index == 0)
                this.finalVertexData[index7].textureCoordinate = position;
              else
                this.finalVertexData[index7].textureCoordinate2 = position;
            }
          }
        }
        else
        {
          byte[] data = this.m_TextureUnits[index].texcoords.m_Data;
          if (flag2)
          {
            for (int index10 = minIndex; index10 <= maxIndex; ++index10)
            {
              if (flag1)
              {
                byte[] numArray8 = data;
                int index11 = num1;
                int num11 = index11 + 1;
                float x = (float) (sbyte) numArray8[index11];
                byte[] numArray9 = data;
                int index12 = num11;
                int num12 = index12 + 1;
                float y = (float) (sbyte) numArray9[index12];
                byte[] numArray10 = data;
                int index13 = num12;
                num1 = index13 + 1;
                float z = (float) (sbyte) numArray10[index13];
                Vector3 vector3 = Vector3.Transform(new Vector3(x, y, z), mat);
                position.X = vector3.X;
                position.Y = vector3.Y;
              }
              else
              {
                ref Vector2 local5 = ref position;
                byte[] numArray11 = data;
                int index14 = num1;
                int num13 = index14 + 1;
                double num14 = (double) (sbyte) numArray11[index14];
                local5.X = (float) num14;
                ref Vector2 local6 = ref position;
                byte[] numArray12 = data;
                int index15 = num13;
                int num15 = index15 + 1;
                double num16 = (double) (sbyte) numArray12[index15];
                local6.Y = (float) num16;
                num1 = num15 + 1;
              }
              if (index == 0)
                this.finalVertexData[index10].textureCoordinate = position;
              else
                this.finalVertexData[index10].textureCoordinate2 = position;
            }
          }
          else
          {
            for (int index16 = minIndex; index16 <= maxIndex; ++index16)
            {
              ref Vector2 local7 = ref position;
              byte[] numArray13 = data;
              int index17 = num1;
              int num17 = index17 + 1;
              double num18 = (double) (sbyte) numArray13[index17];
              local7.X = (float) num18;
              ref Vector2 local8 = ref position;
              byte[] numArray14 = data;
              int index18 = num17;
              num1 = index18 + 1;
              double num19 = (double) (sbyte) numArray14[index18];
              local8.Y = (float) num19;
              if (flag1)
                position = Vector2.Transform(position, mat);
              if (index == 0)
                this.finalVertexData[index16].textureCoordinate = position;
              else
                this.finalVertexData[index16].textureCoordinate2 = position;
            }
          }
        }
      }
      else
      {
        if (this.finalVertexData2D == null || this.finalVertexData2D.Length < vertexCount)
          this.finalVertexData2D = new Vertex2D[vertexCount];
        if (floatArray != null)
        {
          if (flag2)
          {
            for (int index19 = minIndex; index19 <= maxIndex; ++index19)
            {
              if (flag1)
              {
                float[] numArray15 = floatArray;
                int index20 = num1;
                int num20 = index20 + 1;
                float x = numArray15[index20];
                float[] numArray16 = floatArray;
                int index21 = num20;
                int num21 = index21 + 1;
                float y = numArray16[index21];
                float[] numArray17 = floatArray;
                int index22 = num21;
                num1 = index22 + 1;
                float z = numArray17[index22];
                Vector3 vector3 = Vector3.Transform(new Vector3(x, y, z), mat);
                position.X = vector3.X;
                position.Y = vector3.Y;
              }
              else
              {
                ref Vector2 local9 = ref position;
                float[] numArray18 = floatArray;
                int index23 = num1;
                int num22 = index23 + 1;
                double num23 = (double) numArray18[index23];
                local9.X = (float) num23;
                ref Vector2 local10 = ref position;
                float[] numArray19 = floatArray;
                int index24 = num22;
                int num24 = index24 + 1;
                double num25 = (double) numArray19[index24];
                local10.Y = (float) num25;
                num1 = num24 + 1;
              }
              if (index == 0)
                this.finalVertexData2D[index19].textureCoordinate = position;
              else
                this.finalVertexData2D[index19].textureCoordinate2 = position;
            }
          }
          else
          {
            for (int index25 = minIndex; index25 <= maxIndex; ++index25)
            {
              ref Vector2 local11 = ref position;
              float[] numArray20 = floatArray;
              int index26 = num1;
              int num26 = index26 + 1;
              double num27 = (double) numArray20[index26];
              local11.X = (float) num27;
              ref Vector2 local12 = ref position;
              float[] numArray21 = floatArray;
              int index27 = num26;
              num1 = index27 + 1;
              double num28 = (double) numArray21[index27];
              local12.Y = (float) num28;
              if (flag1)
                position = Vector2.Transform(position, mat);
              if (index == 0)
                this.finalVertexData2D[index25].textureCoordinate = position;
              else
                this.finalVertexData2D[index25].textureCoordinate2 = position;
            }
          }
        }
        else
        {
          byte[] data = this.m_TextureUnits[index].texcoords.m_Data;
          if (flag2)
          {
            for (int index28 = minIndex; index28 <= maxIndex; ++index28)
            {
              if (flag1)
              {
                byte[] numArray22 = data;
                int index29 = num1;
                int num29 = index29 + 1;
                float x = (float) (sbyte) numArray22[index29];
                byte[] numArray23 = data;
                int index30 = num29;
                int num30 = index30 + 1;
                float y = (float) (sbyte) numArray23[index30];
                byte[] numArray24 = data;
                int index31 = num30;
                num1 = index31 + 1;
                float z = (float) (sbyte) numArray24[index31];
                Vector3 vector3 = Vector3.Transform(new Vector3(x, y, z), mat);
                position.X = vector3.X;
                position.Y = vector3.Y;
              }
              else
              {
                ref Vector2 local13 = ref position;
                byte[] numArray25 = data;
                int index32 = num1;
                int num31 = index32 + 1;
                double num32 = (double) (sbyte) numArray25[index32];
                local13.X = (float) num32;
                ref Vector2 local14 = ref position;
                byte[] numArray26 = data;
                int index33 = num31;
                int num33 = index33 + 1;
                double num34 = (double) (sbyte) numArray26[index33];
                local14.Y = (float) num34;
                num1 = num33 + 1;
              }
              if (index == 0)
                this.finalVertexData2D[index28].textureCoordinate = position;
              else
                this.finalVertexData2D[index28].textureCoordinate2 = position;
            }
          }
          else
          {
            for (int index34 = minIndex; index34 <= maxIndex; ++index34)
            {
              ref Vector2 local15 = ref position;
              byte[] numArray27 = data;
              int index35 = num1;
              int num35 = index35 + 1;
              double num36 = (double) (sbyte) numArray27[index35];
              local15.X = (float) num36;
              ref Vector2 local16 = ref position;
              byte[] numArray28 = data;
              int index36 = num35;
              num1 = index36 + 1;
              double num37 = (double) (sbyte) numArray28[index36];
              local16.Y = (float) num37;
              if (flag1)
                position = Vector2.Transform(position, mat);
              if (index == 0)
                this.finalVertexData2D[index34].textureCoordinate = position;
              else
                this.finalVertexData2D[index34].textureCoordinate2 = position;
            }
          }
        }
      }
    }

    public void processSkinnedTextureCoords(int index, Matrix mat)
    {
      if (this.m_TextureUnits == null || this.m_TextureUnits[index] == null)
        return;
      int vertexCount = this.m_TextureUnits[index].texcoords.getVertexCount();
      if (vertexCount <= 0)
        return;
      float[] floatArray = this.m_TextureUnits[index].texcoords.getFloatArray();
      int num1 = 0;
      bool flag1 = this.m_TextureUnits[index].texcoords.getComponentCount() > 2;
      if (this.finalSkinnedVertexData == null || this.finalSkinnedVertexData.Length < vertexCount)
        this.finalSkinnedVertexData = new SkinnedVertex[vertexCount];
      Matrix matrix = new Matrix(this.m_TextureUnits[index].texcoordScale, 0.0f, 0.0f, 0.0f, 0.0f, this.m_TextureUnits[index].texcoordScale, 0.0f, 0.0f, 0.0f, 0.0f, this.m_TextureUnits[index].texcoordScale, 0.0f, this.m_TextureUnits[index].texcoordBiasU + this.texCoordTransX, this.m_TextureUnits[index].texcoordBiasV + this.texCoordTransY, 0.0f, 1f);
      mat *= matrix;
      bool flag2 = !mat.Equals(Matrix.Identity);
      Vector2 position = new Vector2();
      if (floatArray != null)
      {
        if (flag1)
        {
          for (int index1 = 0; index1 < vertexCount; ++index1)
          {
            if (flag2)
            {
              float[] numArray1 = floatArray;
              int index2 = num1;
              int num2 = index2 + 1;
              float x = numArray1[index2];
              float[] numArray2 = floatArray;
              int index3 = num2;
              int num3 = index3 + 1;
              float y = numArray2[index3];
              float[] numArray3 = floatArray;
              int index4 = num3;
              num1 = index4 + 1;
              float z = numArray3[index4];
              Vector3 vector3 = Vector3.Transform(new Vector3(x, y, z), mat);
              position.X = vector3.X;
              position.Y = vector3.Y;
            }
            else
            {
              ref Vector2 local1 = ref position;
              float[] numArray4 = floatArray;
              int index5 = num1;
              int num4 = index5 + 1;
              double num5 = (double) numArray4[index5];
              local1.X = (float) num5;
              ref Vector2 local2 = ref position;
              float[] numArray5 = floatArray;
              int index6 = num4;
              int num6 = index6 + 1;
              double num7 = (double) numArray5[index6];
              local2.Y = (float) num7;
              num1 = num6 + 1;
            }
            this.finalSkinnedVertexData[index1].textureCoordinate = position;
          }
        }
        else
        {
          for (int index7 = 0; index7 < vertexCount; ++index7)
          {
            ref Vector2 local3 = ref position;
            float[] numArray6 = floatArray;
            int index8 = num1;
            int num8 = index8 + 1;
            double num9 = (double) numArray6[index8];
            local3.X = (float) num9;
            ref Vector2 local4 = ref position;
            float[] numArray7 = floatArray;
            int index9 = num8;
            num1 = index9 + 1;
            double num10 = (double) numArray7[index9];
            local4.Y = (float) num10;
            if (flag2)
              position = Vector2.Transform(position, mat);
            this.finalSkinnedVertexData[index7].textureCoordinate = position;
          }
        }
      }
      else
      {
        byte[] data = this.m_TextureUnits[index].texcoords.m_Data;
        if (flag1)
        {
          for (int index10 = 0; index10 < vertexCount; ++index10)
          {
            if (flag2)
            {
              byte[] numArray8 = data;
              int index11 = num1;
              int num11 = index11 + 1;
              float x = (float) (sbyte) numArray8[index11];
              byte[] numArray9 = data;
              int index12 = num11;
              int num12 = index12 + 1;
              float y = (float) (sbyte) numArray9[index12];
              byte[] numArray10 = data;
              int index13 = num12;
              num1 = index13 + 1;
              float z = (float) (sbyte) numArray10[index13];
              Vector3 vector3 = Vector3.Transform(new Vector3(x, y, z), mat);
              position.X = vector3.X;
              position.Y = vector3.Y;
            }
            else
            {
              ref Vector2 local5 = ref position;
              byte[] numArray11 = data;
              int index14 = num1;
              int num13 = index14 + 1;
              double num14 = (double) (sbyte) numArray11[index14];
              local5.X = (float) num14;
              ref Vector2 local6 = ref position;
              byte[] numArray12 = data;
              int index15 = num13;
              int num15 = index15 + 1;
              double num16 = (double) (sbyte) numArray12[index15];
              local6.Y = (float) num16;
              num1 = num15 + 1;
            }
            this.finalSkinnedVertexData[index10].textureCoordinate = position;
          }
        }
        else
        {
          for (int index16 = 0; index16 < vertexCount; ++index16)
          {
            ref Vector2 local7 = ref position;
            byte[] numArray13 = data;
            int index17 = num1;
            int num17 = index17 + 1;
            double num18 = (double) (sbyte) numArray13[index17];
            local7.X = (float) num18;
            ref Vector2 local8 = ref position;
            byte[] numArray14 = data;
            int index18 = num17;
            num1 = index18 + 1;
            double num19 = (double) (sbyte) numArray14[index18];
            local8.Y = (float) num19;
            if (flag2)
              position = Vector2.Transform(position, mat);
            this.finalSkinnedVertexData[index16].textureCoordinate = position;
          }
        }
      }
    }

    public void processColors()
    {
      if (this.m_Colors == null)
        return;
      int vertexCount = this.m_Colors.getVertexCount();
      if (vertexCount <= 0)
        return;
      int componentStride = this.m_Colors.getComponentStride();
      int componentCount = this.m_Colors.getComponentCount();
      int dataOffset = this.m_Colors.getDataOffset();
      byte[] data = this.m_Colors.m_Data;
      if (this.m_Normals != null)
      {
        if (this.finalVertexData == null || this.finalVertexData.Length < vertexCount)
          this.finalVertexData = new Vertex[vertexCount];
        for (int index1 = 0; index1 < vertexCount; ++index1)
        {
          int num1 = dataOffset;
          byte[] numArray1 = data;
          int index2 = num1;
          int num2 = index2 + 1;
          byte r = numArray1[index2];
          byte[] numArray2 = data;
          int index3 = num2;
          int num3 = index3 + 1;
          byte g = numArray2[index3];
          byte[] numArray3 = data;
          int index4 = num3;
          int num4 = index4 + 1;
          byte b = numArray3[index4];
          byte maxValue;
          if (componentCount == 4)
          {
            byte[] numArray4 = data;
            int index5 = num4;
            int num5 = index5 + 1;
            maxValue = numArray4[index5];
          }
          else
            maxValue = byte.MaxValue;
          this.finalVertexData[index1].color = new Color((int) r, (int) g, (int) b, (int) maxValue);
          dataOffset += componentStride;
        }
      }
      else
      {
        if (this.finalVertexData2D == null || this.finalVertexData2D.Length < vertexCount)
          this.finalVertexData2D = new Vertex2D[vertexCount];
        for (int index6 = 0; index6 < vertexCount; ++index6)
        {
          int num6 = dataOffset;
          byte[] numArray5 = data;
          int index7 = num6;
          int num7 = index7 + 1;
          byte r = numArray5[index7];
          byte[] numArray6 = data;
          int index8 = num7;
          int num8 = index8 + 1;
          byte g = numArray6[index8];
          byte[] numArray7 = data;
          int index9 = num8;
          int num9 = index9 + 1;
          byte b = numArray7[index9];
          byte maxValue;
          if (componentCount == 4)
          {
            byte[] numArray8 = data;
            int index10 = num9;
            int num10 = index10 + 1;
            maxValue = numArray8[index10];
          }
          else
            maxValue = byte.MaxValue;
          this.finalVertexData2D[index6].color = new Color((int) r, (int) g, (int) b, (int) maxValue);
          dataOffset += componentStride;
        }
      }
    }

    private void processNormals()
    {
      if (this.m_Normals == null)
        return;
      int vertexCount = this.m_Normals.getVertexCount();
      if (vertexCount <= 0)
        return;
      if (this.finalVertexData == null || this.finalVertexData.Length < vertexCount)
        this.finalVertexData = new Vertex[vertexCount];
      int num1 = 0;
      float[] floatArray = this.m_Normals.getFloatArray();
      if (floatArray != null)
      {
        for (int index1 = 0; index1 < vertexCount; ++index1)
        {
          ref Vector3 local1 = ref this.finalVertexData[index1].normal;
          float[] numArray1 = floatArray;
          int index2 = num1;
          int num2 = index2 + 1;
          double num3 = (double) numArray1[index2];
          local1.X = (float) num3;
          ref Vector3 local2 = ref this.finalVertexData[index1].normal;
          float[] numArray2 = floatArray;
          int index3 = num2;
          int num4 = index3 + 1;
          double num5 = (double) numArray2[index3];
          local2.Y = (float) num5;
          ref Vector3 local3 = ref this.finalVertexData[index1].normal;
          float[] numArray3 = floatArray;
          int index4 = num4;
          num1 = index4 + 1;
          double num6 = (double) numArray3[index4];
          local3.Z = (float) num6;
        }
      }
      else
      {
        int dataOffset = this.m_Normals.getDataOffset();
        int componentStride = this.m_Normals.getComponentStride();
        for (int index5 = 0; index5 < vertexCount; ++index5)
        {
          int num7 = dataOffset;
          ref Vector3 local4 = ref this.finalVertexData[index5].normal;
          byte[] data1 = this.m_Normals.m_Data;
          int index6 = num7;
          int num8 = index6 + 1;
          double num9 = (double) (sbyte) data1[index6];
          local4.X = (float) num9;
          ref Vector3 local5 = ref this.finalVertexData[index5].normal;
          byte[] data2 = this.m_Normals.m_Data;
          int index7 = num8;
          int index8 = index7 + 1;
          double num10 = (double) (sbyte) data2[index7];
          local5.Y = (float) num10;
          this.finalVertexData[index5].normal.Z = (float) (sbyte) this.m_Normals.m_Data[index8];
          dataOffset += componentStride;
        }
      }
    }

    private void clearTranslatedParts(ref int minIndex)
    {
      if (this.partsToDelete.Count > 0 && this.partsToDelete[0].minIndex < minIndex)
        minIndex = this.partsToDelete[0].minIndex;
      foreach (VertexBuffer.TranslatedPart translatedPart in this.partsToDelete)
        this.translatedParts.Remove(translatedPart);
      this.partsToDelete.Clear();
    }

    private bool checkTextureUpdateRequired(ref int minIndex, ref int maxIndex, Matrix trans)
    {
      if (this.translatedParts.Count == 0)
      {
        this.translatedParts.Add(new VertexBuffer.TranslatedPart()
        {
          minIndex = minIndex,
          maxIndex = maxIndex,
          trans = trans
        });
        return true;
      }
      for (int index = 0; index < this.translatedParts.Count; ++index)
      {
        VertexBuffer.TranslatedPart translatedPart1 = this.translatedParts[index];
        if (translatedPart1.minIndex > maxIndex)
        {
          this.translatedParts.Insert(index, new VertexBuffer.TranslatedPart()
          {
            minIndex = minIndex,
            maxIndex = maxIndex,
            trans = trans
          });
          this.clearTranslatedParts(ref minIndex);
          return true;
        }
        if (translatedPart1.maxIndex >= minIndex)
        {
          if (translatedPart1.trans == trans)
          {
            if (translatedPart1.minIndex <= minIndex && translatedPart1.maxIndex >= maxIndex)
            {
              this.clearTranslatedParts(ref minIndex);
              return false;
            }
            if (translatedPart1.minIndex > minIndex && translatedPart1.maxIndex >= maxIndex)
            {
              maxIndex = translatedPart1.minIndex - 1;
              translatedPart1.minIndex = minIndex;
              this.clearTranslatedParts(ref minIndex);
              return true;
            }
            if (translatedPart1.minIndex <= minIndex && translatedPart1.maxIndex < maxIndex)
            {
              minIndex = translatedPart1.maxIndex + 1;
            }
            else
            {
              translatedPart1.minIndex = minIndex;
              minIndex = translatedPart1.maxIndex + 1;
            }
          }
          else
          {
            if (translatedPart1.minIndex == minIndex && translatedPart1.maxIndex == maxIndex)
            {
              translatedPart1.trans = trans;
              this.clearTranslatedParts(ref minIndex);
              return true;
            }
            if (translatedPart1.minIndex >= minIndex && translatedPart1.maxIndex > maxIndex)
            {
              translatedPart1.minIndex = maxIndex + 1;
              this.translatedParts.Insert(index, new VertexBuffer.TranslatedPart()
              {
                minIndex = minIndex,
                maxIndex = maxIndex,
                trans = trans
              });
              this.clearTranslatedParts(ref minIndex);
              return true;
            }
            if (translatedPart1.minIndex > minIndex && translatedPart1.maxIndex == maxIndex)
            {
              translatedPart1.minIndex = minIndex;
              translatedPart1.trans = trans;
              this.clearTranslatedParts(ref minIndex);
              return true;
            }
            if (translatedPart1.minIndex < minIndex && translatedPart1.maxIndex == maxIndex)
            {
              translatedPart1.maxIndex = minIndex - 1;
              this.translatedParts.Insert(index + 1, new VertexBuffer.TranslatedPart()
              {
                minIndex = minIndex,
                maxIndex = maxIndex,
                trans = trans
              });
              this.clearTranslatedParts(ref minIndex);
              return true;
            }
            if (translatedPart1.minIndex < minIndex && translatedPart1.maxIndex > maxIndex)
            {
              VertexBuffer.TranslatedPart translatedPart2 = new VertexBuffer.TranslatedPart();
              translatedPart2.maxIndex = translatedPart1.maxIndex;
              translatedPart2.minIndex = maxIndex + 1;
              translatedPart2.trans = translatedPart1.trans;
              translatedPart1.maxIndex = minIndex - 1;
              this.translatedParts.Insert(index + 1, new VertexBuffer.TranslatedPart()
              {
                minIndex = minIndex,
                maxIndex = maxIndex,
                trans = trans
              });
              this.translatedParts.Insert(index + 2, translatedPart2);
              this.clearTranslatedParts(ref minIndex);
              return true;
            }
            if (translatedPart1.minIndex < minIndex && translatedPart1.maxIndex < maxIndex)
              translatedPart1.maxIndex = minIndex - 1;
            else
              this.partsToDelete.Add(translatedPart1);
          }
        }
      }
      this.translatedParts.Add(new VertexBuffer.TranslatedPart()
      {
        minIndex = minIndex,
        maxIndex = maxIndex,
        trans = trans
      });
      this.clearTranslatedParts(ref minIndex);
      return true;
    }

    public void updateVertexData(Appearance app, int minIndex, int maxIndex)
    {
      if (app.getTexture(0) != null)
        app.getTexture(0).getCompositeTransform(ref this.trans0);
      Matrix matrix = Matrix.Identity;
      if (this.m_TextureUnits[0] != null)
      {
        matrix = this.trans0.getMatrix();
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.TEXTURES0) != VertexBuffer.InvalidVertexData.NONE)
        {
          minIndex = 0;
          maxIndex = this.m_TextureUnits[0].texcoords.getVertexCount() - 1;
          this.translatedParts.Clear();
          this.translatedParts.Add(new VertexBuffer.TranslatedPart()
          {
            minIndex = minIndex,
            maxIndex = maxIndex,
            trans = matrix
          });
          if (this.finalVertexBuffer == null && this.useVertexBuffer && matrix == Matrix.Identity)
            this.canUseVertexBuffer = true;
        }
        else if (this.needUpdateVertexData == VertexBuffer.InvalidVertexData.NONE && matrix == Matrix.Identity && this.canUseVertexBuffer)
          this.useVertexBuffer = true;
        else if (this.checkTextureUpdateRequired(ref minIndex, ref maxIndex, matrix))
          this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.TEXTURES0;
      }
      if (this.needUpdateVertexData != VertexBuffer.InvalidVertexData.NONE)
      {
        if (this.m_Normals != null && (this.needUpdateVertexData & VertexBuffer.InvalidVertexData.NORMALS) != VertexBuffer.InvalidVertexData.NONE)
          this.processNormals();
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.POSITIONS) != VertexBuffer.InvalidVertexData.NONE)
          this.processPositions();
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.TEXTURES0) != VertexBuffer.InvalidVertexData.NONE)
          this.processTextureCoords(0, matrix, minIndex, maxIndex);
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.COLORS) != VertexBuffer.InvalidVertexData.NONE)
          this.processColors();
        if (this.finalVertexBuffer == null && this.useVertexBuffer)
        {
          if (this.m_Normals != null)
          {
            this.finalVertexBuffer = new Microsoft.Xna.Framework.Graphics.VertexBuffer(MirrorsEdge.graphicsDevice, typeof (Vertex), this.m_VertexCount, BufferUsage.None);
            this.finalVertexBuffer.SetData<Vertex>(this.finalVertexData);
          }
          else
          {
            this.finalVertexBuffer = new Microsoft.Xna.Framework.Graphics.VertexBuffer(MirrorsEdge.graphicsDevice, typeof (Vertex2D), this.m_VertexCount, BufferUsage.None);
            this.finalVertexBuffer.SetData<Vertex2D>(this.finalVertexData2D);
          }
        }
        else
        {
          if (!this.canUseVertexBuffer)
            this.finalVertexBuffer = (Microsoft.Xna.Framework.Graphics.VertexBuffer) null;
          this.useVertexBuffer = false;
        }
      }
      this.needUpdateVertexData = VertexBuffer.InvalidVertexData.NONE;
    }

    public void updateSkinnedVertexData(Appearance app)
    {
      if (app.getTexture(0) != null)
        app.getTexture(0).getCompositeTransform(ref this.trans0);
      if (this.trans0.getMatrix() != this.prevTrans[0])
      {
        this.needUpdateVertexData |= VertexBuffer.InvalidVertexData.TEXTURES0;
        this.prevTrans[0] = this.trans0.getMatrix();
      }
      if (this.needUpdateVertexData != VertexBuffer.InvalidVertexData.NONE)
      {
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.POSITIONS) != VertexBuffer.InvalidVertexData.NONE)
          this.processSkinnedPositions();
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.TEXTURES0) != VertexBuffer.InvalidVertexData.NONE)
          this.processSkinnedTextureCoords(0, this.trans0.getMatrix());
        if ((this.needUpdateVertexData & VertexBuffer.InvalidVertexData.SKIN_INDICIES) != VertexBuffer.InvalidVertexData.NONE || (this.needUpdateVertexData & VertexBuffer.InvalidVertexData.SKIN_WEIGHTS) != VertexBuffer.InvalidVertexData.NONE)
          this.processSkinned();
        if (this.finalVertexBuffer == null && this.useVertexBuffer)
        {
          this.finalVertexBuffer = new Microsoft.Xna.Framework.Graphics.VertexBuffer(MirrorsEdge.graphicsDevice, typeof (SkinnedVertex), this.m_VertexCount, BufferUsage.None);
          this.finalVertexBuffer.SetData<SkinnedVertex>(this.finalSkinnedVertexData);
        }
        else
        {
          this.finalVertexBuffer = (Microsoft.Xna.Framework.Graphics.VertexBuffer) null;
          this.useVertexBuffer = false;
        }
      }
      this.needUpdateVertexData = VertexBuffer.InvalidVertexData.NONE;
    }

    public void invalidateVertexData(VertexBuffer.InvalidVertexData type)
    {
      this.needUpdateVertexData |= type;
    }

    public Vertex[] getFinalVertices() => this.finalVertexData;

    public Vertex2D[] getFinalVertices2D() => this.finalVertexData2D;

    public SkinnedVertex[] getFinalSkinnedVertices() => this.finalSkinnedVertexData;

    public override int getM3GUniqueClassID() => 21;

    public static VertexBuffer m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 21 ? (VertexBuffer) obj : (VertexBuffer) null;
    }

    [Flags]
    public enum InvalidVertexData
    {
      NONE = 0,
      POSITIONS = 1,
      NORMALS = 2,
      COLORS = 4,
      TEXTURES0 = 8,
      TEXTURES1 = 16, // 0x00000010
      SKIN_INDICIES = 32, // 0x00000020
      SKIN_WEIGHTS = 64, // 0x00000040
      ALL = SKIN_WEIGHTS | SKIN_INDICIES | TEXTURES1 | TEXTURES0 | COLORS | NORMALS | POSITIONS, // 0x0000007F
    }

    private class TranslatedPart
    {
      public int minIndex;
      public int maxIndex;
      public Matrix trans;
    }
  }
}
