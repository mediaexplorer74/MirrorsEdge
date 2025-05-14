
// Type: microedition.m3g.Mesh
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.Collections.Generic;

#nullable disable
namespace microedition.m3g
{
  public class Mesh : Node
  {
    public new const int M3G_UNIQUE_CLASS_ID = 14;
    private static float[] scalebias = new float[4];
    private static float[] bias = new float[3];
    private VertexBuffer m_VertexBuffer;
    private List<IndexBuffer> m_IndexBuffers;
    private List<AppearanceBase> m_Appearances;
    private bool m_Mutable;

    public Mesh()
    {
      this.m_VertexBuffer = (VertexBuffer) null;
      this.m_Mutable = true;
      this.m_IndexBuffers = new List<IndexBuffer>();
      this.m_Appearances = new List<AppearanceBase>();
    }

    public Mesh(int submeshCount, int morphCount)
    {
      this.m_VertexBuffer = (VertexBuffer) null;
      this.m_Mutable = true;
      this.m_IndexBuffers = new List<IndexBuffer>();
      this.m_Appearances = new List<AppearanceBase>();
      this.setSubmeshCount(submeshCount);
    }

    public Mesh(VertexBuffer vertices, ref IndexBuffer[] submeshes, ref Appearance[] appearances)
    {
      this.m_VertexBuffer = (VertexBuffer) null;
      this.m_Mutable = true;
      this.m_IndexBuffers = new List<IndexBuffer>();
      this.m_Appearances = new List<AppearanceBase>();
      this.setVertexBuffer(vertices);
      this.setSubmeshCount(submeshes.Length);
      for (int index = 0; index < submeshes.Length; ++index)
        this.setIndexBuffer(index, submeshes[index]);
      for (int index = 0; index < appearances.Length; ++index)
        this.setAppearance(index, appearances[index]);
    }

    public Mesh(VertexBuffer vertices, IndexBuffer submesh, Appearance appearance)
    {
      this.m_VertexBuffer = (VertexBuffer) null;
      this.m_Mutable = true;
      this.m_IndexBuffers = new List<IndexBuffer>();
      this.m_Appearances = new List<AppearanceBase>();
      this.setVertexBuffer(vertices);
      this.setSubmeshCount(1);
      this.setIndexBuffer(0, submesh);
      this.setAppearance(0, appearance);
    }

    public override void Destructor()
    {
      if (this.m_VertexBuffer != null)
        this.m_VertexBuffer.Destructor();
      this.m_VertexBuffer = (VertexBuffer) null;
      this.m_IndexBuffers.Clear();
      this.m_Appearances.Clear();
      base.Destructor();
    }

    public void commit()
    {
      if (!this.m_Mutable)
        return;
      this.setVertexBuffer((VertexBuffer) this.getVertexBuffer().duplicate());
      for (int index = 0; index < this.getSubmeshCount(); ++index)
      {
        IndexBuffer indexBuffer = this.getIndexBuffer(index).commitDuplicate();
        this.setIndexBuffer(index, indexBuffer);
      }
      this.m_Mutable = false;
    }

    public Appearance getAppearance(int index)
    {
      return !this.verifyIndex(index) ? (Appearance) null : this.m_Appearances[index] as Appearance;
    }

    public IndexBuffer getIndexBuffer(int index)
    {
      return !this.verifyIndex(index) ? (IndexBuffer) null : this.m_IndexBuffers[index];
    }

    public int getSubmeshCount() => this.m_IndexBuffers.Count;

    public VertexBuffer getVertexBuffer() => this.m_VertexBuffer;

    public void setIndexBuffer(int index, IndexBuffer indexBuffer)
    {
      if (!this.verifyMutable() || !this.verifyIndex(index))
        return;
      this.m_IndexBuffers[index] = indexBuffer;
    }

    public void setAppearance(int index, Appearance appearance)
    {
      this.setAppearanceBase(index, (AppearanceBase) appearance);
    }

    public void setAppearanceBase(int index, AppearanceBase appearance)
    {
      if (!this.verifyMutable() || !this.verifyIndex(index))
        return;
      this.m_Appearances[index] = appearance;
    }

    public bool isMutable() => this.m_Mutable;

    public void setVertexBuffer(VertexBuffer vertexBuffer)
    {
      if (!this.verifyMutable())
        return;
      this.m_VertexBuffer = vertexBuffer;
    }

    public void setSubmeshCount(int count)
    {
      if (!this.verifyMutable())
        return;
      this.m_IndexBuffers.Clear();
      this.m_IndexBuffers.Capacity = count;
      this.m_Appearances.Clear();
      this.m_Appearances.Capacity = count;
      for (int index = 0; index < count; ++index)
      {
        this.m_IndexBuffers.Add((IndexBuffer) null);
        this.m_Appearances.Add((AppearanceBase) null);
      }
    }

    public void preparePositionsForSkinning()
    {
      VertexArray positions = this.getVertexBuffer().getPositions(ref Mesh.scalebias);
      if (positions.getComponentCount() != 3 || positions.getComponentType() == 4 && (double) Mesh.scalebias[0] == 1.0 && (double) Mesh.scalebias[1] == 0.0 && (double) Mesh.scalebias[2] == 0.0 && (double) Mesh.scalebias[3] == 0.0)
        return;
      float[] values = new float[positions.getVertexCount() * positions.getComponentCount()];
      VertexArray arr = new VertexArray(positions.getVertexCount(), positions.getComponentCount(), 4);
      positions.get(0, positions.getVertexCount(), ref values);
      int index1 = 0;
      for (int index2 = 0; index2 < positions.getVertexCount(); ++index2)
      {
        values[index1] = values[index1] * Mesh.scalebias[0] + Mesh.scalebias[1];
        values[index1 + 1] = values[index1 + 1] * Mesh.scalebias[0] + Mesh.scalebias[2];
        values[index1 + 2] = values[index1 + 2] * Mesh.scalebias[0] + Mesh.scalebias[3];
        index1 += 3;
      }
      arr.set(0, positions.getVertexCount(), values);
      this.getVertexBuffer().setPositions(arr, 1f, Mesh.bias);
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      Mesh mesh = (Mesh) ret;
      mesh.setSubmeshCount(this.getSubmeshCount());
      VertexBuffer vertexBuffer = this.getVertexBuffer();
      if (vertexBuffer != null)
        mesh.setVertexBuffer(vertexBuffer);
      for (int index = 0; index < this.getSubmeshCount(); ++index)
      {
        mesh.setAppearance(index, this.getAppearance(index));
        mesh.setIndexBuffer(index, this.getIndexBuffer(index));
      }
      mesh.m_Mutable = this.isMutable();
    }

    public override int getReferences(ref Object3D[] references)
    {
      int references1 = base.getReferences(ref references);
      int num1 = references1;
      for (int index = 0; index < this.getSubmeshCount(); ++index)
      {
        if (this.getAppearance(index) != null)
          ++references1;
        if (this.getIndexBuffer(index) != null)
          ++references1;
      }
      int references2 = references1 + 1;
      if (references != null)
      {
        for (int index = 0; index < this.getSubmeshCount(); ++index)
        {
          Appearance appearance = this.getAppearance(index);
          if (appearance != null)
            references[num1++] = (Object3D) appearance;
          IndexBuffer indexBuffer = this.getIndexBuffer(index);
          if (indexBuffer != null)
            references[num1++] = (Object3D) indexBuffer;
        }
        Object3D[] object3DArray = references;
        int index1 = num1;
        int num2 = index1 + 1;
        VertexBuffer vertexBuffer = this.getVertexBuffer();
        object3DArray[index1] = (Object3D) vertexBuffer;
      }
      return references2;
    }

    protected override void findReferences(ref Object3DFinder finder)
    {
      base.findReferences(ref finder);
      finder.find((Object3D) this.getVertexBuffer());
      int submeshCount = this.getSubmeshCount();
      for (int index = 0; index < submeshCount; ++index)
        finder.find((Object3D) this.getAppearance(index));
      for (int index = 0; index < submeshCount; ++index)
        finder.find((Object3D) this.getIndexBuffer(index));
    }

    protected override void animateReferences(int time)
    {
      if (!this.isRenderingEnabled())
        return;
      this.getVertexBuffer().animate(time);
    }

    private bool verifyIndex(int index) => true;

    private bool verifyMutable() => true;

    public override int getM3GUniqueClassID() => 14;

    public static Mesh m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 14 ? (Mesh) obj : (Mesh) null;
    }
  }
}
