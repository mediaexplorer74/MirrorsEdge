
// Type: microedition.m3g.Renderer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace microedition.m3g
{
  public abstract class Renderer
  {
    public abstract void setProjectionAndViewTransform(Transform projection, Transform view);

    public abstract void pushModelTransform(Transform transform);

    public abstract void popModelTransform();

    public abstract void setViewport(int x, int y, int width, int height);

    public abstract void clear(Background background);

    public abstract void bind(int w, int h);

    public abstract void release();

    public abstract void render(
      VertexBuffer vertices,
      IndexBuffer primitives,
      AppearanceBase appearance,
      float alphaFactor);

    public abstract void render(
      VertexBuffer vertices,
      VertexArray skinIndices,
      VertexArray skinWeights,
      Transform[] matrixPalette,
      IndexBuffer primitives,
      AppearanceBase appearance,
      float alphaFactor);
  }
}
