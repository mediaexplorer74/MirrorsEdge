
// Type: microedition.m3g.IndexBuffer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using Microsoft.Xna.Framework.Graphics;
using mirrorsedge_wp7;

#nullable disable
namespace microedition.m3g
{
  public class IndexBuffer : Object3D
  {
    public const int TRIANGLES = 8;
    public const int LINES = 9;
    public const int POINT_SPRITES = 10;
    public new const int M3G_UNIQUE_CLASS_ID = 39;
    private int m_FirstIndex;
    private int m_PrimitiveCount;
    private short[] m_Indices;
    private ushort[] m_StripLengths;
    private bool m_Commited;
    private int m_Type;
    private int minIndex = -1;
    private int maxIndex = -1;
    public Microsoft.Xna.Framework.Graphics.IndexBuffer finalIndexBuffer;
    private bool needUpdateIndexBuffer = true;

    public static void requireValidIndex(int index)
    {
    }

    public static void requireValidPrimitiveCount(int count)
    {
    }

    public static void requireValidType(int type)
    {
    }

    public IndexBuffer()
    {
      this.m_FirstIndex = -1;
      this.m_PrimitiveCount = -1;
      this.m_Indices = (short[]) null;
      this.m_StripLengths = (ushort[]) null;
      this.m_Commited = false;
      this.m_Type = 0;
    }

    public IndexBuffer(int type, int[] stripLengths, int firstIndex)
    {
      this.m_FirstIndex = -1;
      this.m_PrimitiveCount = 0;
      this.m_Indices = (short[]) null;
      this.m_StripLengths = (ushort[]) null;
      this.m_Commited = false;
      this.m_Type = 0;
      int[] indices = this.collapseStripsImplicit(stripLengths, firstIndex);
      this.setPrimitiveType(8);
      this.setIndices(indices);
    }

    public IndexBuffer(int type, int[] stripLengths, int[] indices)
    {
      this.m_FirstIndex = -1;
      this.m_PrimitiveCount = 0;
      this.m_Indices = (short[]) null;
      this.m_StripLengths = (ushort[]) null;
      this.m_Commited = false;
      this.m_Type = 0;
      int[] indices1 = this.collapseStrips(stripLengths, indices);
      this.setPrimitiveType(8);
      this.setIndices(indices1);
    }

    public IndexBuffer(int type, int primitiveCount, int firstIndex)
    {
      this.m_FirstIndex = -1;
      this.m_PrimitiveCount = -1;
      this.m_Indices = (short[]) null;
      this.m_StripLengths = (ushort[]) null;
      this.m_Commited = false;
      this.m_Type = 0;
      this.setPrimitiveType(type);
      this.setPrimitiveCount(primitiveCount);
      this.setFirstIndex(firstIndex);
    }

    public IndexBuffer(int type, int primitiveCount, int[] indices)
    {
      this.m_FirstIndex = -1;
      this.m_PrimitiveCount = -1;
      this.m_Indices = (short[]) null;
      this.m_StripLengths = (ushort[]) null;
      this.m_Commited = false;
      this.m_Type = 0;
      this.setPrimitiveType(type);
      this.setPrimitiveCount(primitiveCount);
      this.setIndices(indices);
    }

    public override void Destructor()
    {
      this.m_Indices = (short[]) null;
      this.m_StripLengths = (ushort[]) null;
      this.finalIndexBuffer = (Microsoft.Xna.Framework.Graphics.IndexBuffer) null;
      base.Destructor();
    }

    protected int[] collapseStrips(int[] striplengths, int[] indices)
    {
      int num1 = 0;
      for (int index = 0; index < striplengths.Length; ++index)
        num1 += striplengths[index] - 2;
      this.m_PrimitiveCount = num1;
      int[] numArray = new int[num1 * 3];
      int index1 = 0;
      int num2 = 0;
      for (int index2 = 0; index2 < striplengths.Length; ++index2)
      {
        int index3 = num2;
        for (int index4 = 0; index4 < striplengths[index2] - 2; ++index4)
        {
          if ((index4 & 1) != 0)
          {
            numArray[index1] = indices[index3 + 1];
            numArray[index1 + 1] = indices[index3];
            numArray[index1 + 2] = indices[index3 + 2];
          }
          else
          {
            numArray[index1] = indices[index3];
            numArray[index1 + 1] = indices[index3 + 1];
            numArray[index1 + 2] = indices[index3 + 2];
          }
          ++index3;
          index1 += 3;
        }
        num2 += striplengths[index2];
      }
      return numArray;
    }

    protected int[] collapseStripsImplicit(int[] striplengths, int firstindex)
    {
      int num1 = 0;
      for (int index = 0; index < striplengths.Length; ++index)
        num1 += striplengths[index] - 2;
      this.m_PrimitiveCount = num1;
      int[] numArray = new int[num1 * 3];
      int index1 = 0;
      int num2 = firstindex;
      for (int index2 = 0; index2 < striplengths.GetLength(0); ++index2)
      {
        int num3 = num2;
        for (int index3 = 0; index3 < striplengths[index2] - 2; ++index3)
        {
          if ((index3 & 1) != 0)
          {
            numArray[index1] = num3 + 1;
            numArray[index1 + 1] = num3;
            numArray[index1 + 2] = num3 + 2;
          }
          else
          {
            numArray[index1] = num3;
            numArray[index1 + 1] = num3 + 1;
            numArray[index1 + 2] = num3 + 2;
          }
          ++num3;
          index1 += 3;
        }
        num2 += striplengths[index2];
      }
      return numArray;
    }

    public virtual void commit() => this.m_Commited = true;

    public int getMinIndex()
    {
      if (this.minIndex == -1)
        this.calculateMinMaxIndex();
      return this.minIndex;
    }

    public int getMaxIndex()
    {
      if (this.maxIndex == -1)
        this.calculateMinMaxIndex();
      return this.maxIndex;
    }

    private void calculateMinMaxIndex()
    {
      if (this.m_Indices == null)
        return;
      for (int index = 0; index < this.m_Indices.Length; ++index)
      {
        if ((int) this.m_Indices[index] > this.maxIndex)
          this.maxIndex = (int) this.m_Indices[index];
        if ((int) this.m_Indices[index] < this.minIndex || this.minIndex == -1)
          this.minIndex = (int) this.m_Indices[index];
      }
    }

    public int getIndexCount() => this.m_Indices.Length;

    public void getIndices(ref int[] indices)
    {
      int length = this.m_Indices.Length;
      for (int index = 0; index < length; ++index)
        indices[index] = (int) this.m_Indices[index];
    }

    public int getPrimitiveType() => this.m_Type;

    public bool isReadable() => !this.m_Commited;

    public int getFirstIndex() => this.m_FirstIndex;

    public int getPrimitiveCount() => this.m_PrimitiveCount;

    public short[] getExplicitIndices() => this.m_Indices;

    public ushort[] getStripLengths() => this.m_StripLengths;

    public bool isStripped() => this.m_StripLengths != null && this.m_StripLengths.Length > 0;

    public bool isImplicit() => this.m_FirstIndex > -1;

    public virtual IndexBuffer commitDuplicate() => this;

    public void setFirstIndex(int index)
    {
      IndexBuffer.requireValidIndex(index);
      this.m_FirstIndex = index;
    }

    public void setStripLengths(int[] stripLengths)
    {
      if (this.m_FirstIndex == -1 && this.m_Indices.Length > 0)
      {
        int[] indices = new int[this.m_Indices.Length];
        this.getIndices(ref indices);
        this.setIndices(this.collapseStrips(stripLengths, indices));
      }
      else
        this.setIndices(this.collapseStripsImplicit(stripLengths, this.m_FirstIndex));
    }

    public void setPrimitiveCount(int count)
    {
      IndexBuffer.requireValidPrimitiveCount(count);
      this.m_PrimitiveCount = count;
    }

    public void setPrimitiveType(int type)
    {
      IndexBuffer.requireValidType(type);
      this.m_Type = type;
    }

    public void setIndices(short[] indices)
    {
      this.m_Indices = indices;
      this.minIndex = -1;
      this.maxIndex = -1;
      this.needUpdateIndexBuffer = true;
    }

    public void setIndices(int[] indices)
    {
      int length = indices.Length;
      this.m_Indices = new short[length];
      for (int index = 0; index < length; ++index)
        this.m_Indices[index] = (short) indices[index];
      this.minIndex = -1;
      this.maxIndex = -1;
      this.needUpdateIndexBuffer = true;
    }

    public void updateIndexData()
    {
      if (this.needUpdateIndexBuffer)
      {
        this.finalIndexBuffer = new Microsoft.Xna.Framework.Graphics.IndexBuffer(MirrorsEdge.graphicsDevice, typeof (short), this.m_Indices.Length, BufferUsage.None);
        this.finalIndexBuffer.SetData<short>(this.m_Indices);
      }
      this.needUpdateIndexBuffer = false;
    }

    public override int getM3GUniqueClassID() => 39;
  }
}
