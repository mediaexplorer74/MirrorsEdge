// Decompiled with JetBrains decompiler
// Type: support.QuadMesh
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using microedition.m3g;

#nullable disable
namespace support
{
  public class QuadMesh
  {
    public const int ATTRIB_X = 0;
    public const int ATTRIB_Y = 1;
    public const int ATTRIB_W = 2;
    public const int ATTRIB_H = 3;
    public const int ATTRIB_ALPHA = 4;
    public const int ATTRIB_ROTATION = 5;
    public const int ATTRIB_NUM = 6;
    public Mesh mesh;
    public VertexBuffer vertexBuffer;
    public int groupIndex;
    public int textureIndex;
    public QuadTexture texturePtr;
    public int scope;
    public int layer;
    public uint color;
    public float baseX;
    public float baseY;
    public int[] attribF = new int[6];
    public bool initVisible;
    public bool modified;

    public QuadMesh()
    {
      this.mesh = (Mesh) null;
      this.vertexBuffer = (VertexBuffer) null;
      this.groupIndex = 0;
      this.textureIndex = 0;
      this.texturePtr = (QuadTexture) null;
      this.scope = -1;
      this.layer = 0;
      this.color = uint.MaxValue;
      this.baseX = 0.0f;
      this.baseY = 0.0f;
      this.initVisible = true;
      this.modified = false;
    }

    public void Destructor()
    {
      this.mesh = (Mesh) null;
      this.vertexBuffer = (VertexBuffer) null;
      this.texturePtr = (QuadTexture) null;
      this.attribF = (int[]) null;
    }

    public bool isVisible() => this.mesh != null && this.mesh.isRenderingEnabled();

    public void setVisible(bool visible)
    {
      if (this.mesh == null)
        return;
      this.mesh.setRenderingEnable(visible);
    }
  }
}
