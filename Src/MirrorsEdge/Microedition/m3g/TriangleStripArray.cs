// Decompiled with JetBrains decompiler
// Type: microedition.m3g.TriangleStripArray
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace microedition.m3g
{
  public class TriangleStripArray : IndexBuffer
  {
    private const int MAX_INDICIES = 8192;
    public new const int M3G_UNIQUE_CLASS_ID = 11;
    private static int[] newIndices = new int[8192];

    public TriangleStripArray() => this.setPrimitiveType(8);

    public TriangleStripArray(int[] indices, int[] stripLengths)
      : base(8, stripLengths, indices)
    {
    }

    public TriangleStripArray(int firstIndex, int[] stripLengths)
      : base(8, stripLengths, firstIndex)
    {
    }

    public override IndexBuffer commitDuplicate()
    {
      int[] indices = new int[this.getIndexCount()];
      this.getIndices(ref indices);
      return new IndexBuffer(8, this.getPrimitiveCount(), indices);
    }

    public override int getM3GUniqueClassID() => 11;

    public static TriangleStripArray m3g_cast(Object3D obj)
    {
      return obj.getM3GUniqueClassID() == 11 ? (TriangleStripArray) obj : (TriangleStripArray) null;
    }
  }
}
