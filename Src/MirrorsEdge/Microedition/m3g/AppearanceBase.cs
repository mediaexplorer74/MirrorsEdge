
// Type: microedition.m3g.AppearanceBase
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public class AppearanceBase : Object3D
  {
    private int m_Layer;
    private LineMode m_LineMode;
    private PolygonMode m_PolygonMode;
    private CompositingMode m_CompositingMode;

    protected AppearanceBase()
    {
      this.m_Layer = 0;
      this.m_LineMode = (LineMode) null;
      this.m_PolygonMode = (PolygonMode) null;
      this.m_CompositingMode = (CompositingMode) null;
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      AppearanceBase appearanceBase = (AppearanceBase) ret;
      appearanceBase.setLayer(this.getLayer());
      appearanceBase.setCompositingMode(this.getCompositingMode());
      appearanceBase.setPolygonMode(this.getPolygonMode());
      appearanceBase.setLineMode(this.getLineMode());
    }

    public override int getReferences(ref Object3D[] references)
    {
      int references1 = base.getReferences(ref references);
      int num1 = references1;
      if (this.m_CompositingMode != null)
        ++references1;
      if (this.m_PolygonMode != null)
        ++references1;
      if (this.m_LineMode != null)
        ++references1;
      if (references != null)
      {
        if (this.m_CompositingMode != null)
          references[num1++] = (Object3D) this.m_CompositingMode;
        if (this.m_PolygonMode != null)
          references[num1++] = (Object3D) this.m_PolygonMode;
        if (this.m_LineMode != null)
        {
          Object3D[] object3DArray = references;
          int index = num1;
          int num2 = index + 1;
          LineMode lineMode = this.m_LineMode;
          object3DArray[index] = (Object3D) lineMode;
        }
      }
      return references1;
    }

    public void setLayer(int layer) => this.m_Layer = layer;

    public int getLayer() => this.m_Layer;

    public void setCompositingMode(CompositingMode compositingMode)
    {
      this.m_CompositingMode = compositingMode;
    }

    public CompositingMode getCompositingMode() => this.m_CompositingMode;

    public void setPolygonMode(PolygonMode polygonMode) => this.m_PolygonMode = polygonMode;

    public PolygonMode getPolygonMode() => this.m_PolygonMode;

    public void setLineMode(LineMode lineMode) => this.m_LineMode = lineMode;

    public LineMode getLineMode() => this.m_LineMode;
  }
}
