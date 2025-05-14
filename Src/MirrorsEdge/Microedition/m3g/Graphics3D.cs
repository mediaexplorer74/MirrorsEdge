// Decompiled with JetBrains decompiler
// Type: microedition.m3g.Graphics3D
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using mirrorsedge_wp7;
using System.Collections.Generic;

#nullable disable
namespace microedition.m3g
{
  public class Graphics3D
  {
    public const int ANTIALIAS = 2;
    public const int DEPTH = 32;
    public const int DITHER = 4;
    public const int GENERATE_MIPMAPS = 128;
    public const int OVERWRITE = 16;
    public const int STENCIL = 64;
    public const int TRUE_COLOR = 8;
    private Graphics3D.TransformCache m_transformCache = new Graphics3D.TransformCache();
    protected Camera m_camera;
    protected int m_cameraScope;
    protected Transform m_cameraTransform;
    protected Transform m_cameraInverseTransform;
    protected Transform m_cameraProjection;
    private object m_target;
    private int m_renderingFlags;
    private int m_viewportX;
    private int m_viewportY;
    private int m_viewportW;
    private int m_viewportH;
    private Dictionary<object, Renderer> m_renderers;
    private int m_reservedCapacity;
    private Transform m_cameraToWorld;
    private List<RenderNode> m_renderNodeList;
    private List<Transform> m_transformList;
    private int m_transformListPosition;
    private static Graphics3D g3d;
    public static RendererXNA render_target;

    private Graphics3D()
    {
      this.m_camera = (Camera) null;
      this.m_cameraScope = -1;
      this.m_cameraTransform = new Transform();
      this.m_cameraInverseTransform = new Transform();
      this.m_cameraProjection = new Transform();
      this.m_target = (object) null;
      this.m_renderingFlags = 0;
      this.m_viewportX = 0;
      this.m_viewportY = 0;
      this.m_viewportW = 0;
      this.m_viewportH = 0;
      this.m_renderers = new Dictionary<object, Renderer>();
      this.m_cameraToWorld = new Transform();
      this.m_renderNodeList = new List<RenderNode>();
      this.m_transformList = new List<Transform>(256);
      this.m_transformListPosition = 0;
      this.m_reservedCapacity = 256;
      this.m_renderNodeList.Capacity = 256;
      for (int index = 0; index < 256; ++index)
        this.m_transformList.Add(new Transform());
    }

    private void compileRenderableNodeList(Node node, Transform transform, int scope)
    {
      if (!node.isRenderingEnabled())
        return;
      switch (node)
      {
        case Group group:
          Transform transform1 = this.m_transformCache.get();
          int childCount = group.getChildCount();
          if (transform != null)
          {
            for (int index = 0; index < childCount; ++index)
            {
              Node child = group.getChild(index);
              if (child.isRenderingEnabled())
              {
                transform1.set(transform);
                child.getCompositeTransformCumulative(ref transform1);
                this.compileRenderableNodeList(child, transform1, scope);
              }
            }
          }
          else
          {
            for (int index = 0; index < childCount; ++index)
            {
              Node child = group.getChild(index);
              if (child.isRenderingEnabled())
              {
                child.getCompositeTransform(ref transform1);
                this.compileRenderableNodeList(child, transform1, scope);
              }
            }
          }
          this.m_transformCache.free(transform1);
          break;
        case Mesh n:
          if ((scope & n.getScope()) == 0)
            break;
          SkinnedMesh skinnedMesh = SkinnedMesh.m3g_cast((Object3D) n);
          bool skinned = skinnedMesh != null;
          if (skinned)
          {
            Transform transform2 = this.m_transformCache.get();
            Node skeleton = (Node) skinnedMesh.getSkeleton();
            skeleton.getCompositeTransform(ref transform2);
            if (transform != null)
            {
              Transform transform3 = this.m_transformCache.get();
              transform3.postMultiply(transform2);
              this.compileRenderableNodeList(skeleton, transform3, scope);
              this.m_transformCache.free(transform3);
            }
            else
              this.compileRenderableNodeList(skeleton, transform2, scope);
            this.m_transformCache.free(transform2);
          }
          if (this.m_transformListPosition >= this.m_transformList.Count)
            this.m_transformList.Add(new Transform());
          Transform transform4 = this.m_transformList[this.m_transformListPosition++];
          if (transform != null)
            transform4.set(transform);
          else
            transform4.setIdentity();
          int submeshCount = n.getSubmeshCount();
          for (int submeshIndex = 0; submeshIndex != submeshCount; ++submeshIndex)
            this.m_renderNodeList.Add(new RenderNode((Node) n, submeshIndex, transform4, skinned));
          break;
      }
    }

    public void Destructor() => this.m_camera = (Camera) null;

    public void bindTarget(object target) => this.bindTarget(target, 32);

    public void render(Node node, Transform transform)
    {
      this.m_renderNodeList.Clear();
      this.m_transformListPosition = 0;
      this.compileRenderableNodeList(node, transform, this.m_cameraScope);
      this.m_renderNodeList.Sort();
      int count = this.m_renderNodeList.Count;
      int cameraScope = this.m_cameraScope;
      Renderer renderer = this.getRenderer();
      for (int index = 0; index < count; ++index)
      {
        RenderNode renderNode1 = this.m_renderNodeList[index];
        Mesh renderNode2 = renderNode1.renderNode == null ? (Mesh) null : renderNode1.renderNode as Mesh;
        if (renderNode2 != null && (renderNode2.getScope() & cameraScope) != 0)
        {
          AppearanceBase appearance = renderNode1.m_appearance;
          if (appearance != null)
          {
            VertexBuffer vertexBuffer = renderNode1.m_vertexBuffer;
            IndexBuffer indexBuffer = renderNode1.m_indexBuffer;
            float alphaFactor = renderNode2.getAlphaFactor();
            renderer.pushModelTransform(renderNode1.compositeTransform);
            SkinnedMesh skinnedMesh = SkinnedMesh.m3g_cast((Object3D) renderNode2);
            if (skinnedMesh != null)
              renderer.render(vertexBuffer, skinnedMesh.getSkinIndices(), skinnedMesh.getSkinWeights(), skinnedMesh.getBoneTransforms(), indexBuffer, appearance, alphaFactor);
            else
              renderer.render(vertexBuffer, indexBuffer, appearance, alphaFactor);
            renderer.popModelTransform();
          }
        }
      }
      if (this.m_renderNodeList.Count > this.m_reservedCapacity)
      {
        this.m_reservedCapacity = this.m_renderNodeList.Count;
        this.m_renderNodeList.Capacity = this.m_reservedCapacity;
      }
      this.m_renderNodeList.Clear();
    }

    public void render(
      VertexBuffer vertices,
      IndexBuffer primitives,
      AppearanceBase appearance,
      Transform transform)
    {
      this.render(vertices, primitives, appearance, transform, -1, 1f);
    }

    public virtual void render(
      VertexBuffer vertices,
      IndexBuffer primitives,
      AppearanceBase appearance,
      Transform transform,
      int scope,
      float alphaFactor)
    {
      if ((this.m_cameraScope & scope) == 0)
        return;
      Renderer renderer = this.getRenderer();
      renderer.pushModelTransform(transform);
      renderer.render(vertices, primitives, appearance, alphaFactor);
      renderer.popModelTransform();
    }

    public virtual void setViewport(int x, int y, int width, int height)
    {
      this.m_viewportX = x;
      this.m_viewportY = y;
      this.m_viewportW = width;
      this.m_viewportH = height;
      this.getRenderer().setViewport((int) ((double) x * 1.5), (int) ((double) y * 1.5), (int) ((double) width * 1.5) + 1, (int) ((double) height * 1.5));
    }

    public virtual void setCamera(Camera camera, Transform transform)
    {
      this.m_camera = camera;
      if (transform != null)
      {
        this.m_cameraTransform.set(transform);
        this.m_cameraInverseTransform.set(transform);
        this.m_cameraInverseTransform.invert();
      }
      else
      {
        this.m_cameraTransform.setIdentity();
        this.m_cameraInverseTransform.setIdentity();
      }
      if (camera != null)
      {
        camera.getProjection(this.m_cameraProjection);
        this.m_cameraScope = camera.getScope();
      }
      else
      {
        this.m_cameraProjection.setIdentity();
        this.m_cameraScope = -1;
      }
      this.getRenderer().setProjectionAndViewTransform(this.m_cameraProjection, this.m_cameraInverseTransform);
    }

    public virtual void clear(Background background)
    {
      if (this.getViewportWidth() == 0 || this.getViewportHeight() == 0)
        return;
      this.getRenderer().clear(background);
    }

    public virtual Camera getCamera(Transform transform)
    {
      transform?.set(this.m_cameraTransform);
      return this.m_camera;
    }

    public virtual void releaseTarget() => this.m_target = (object) null;

    public static Graphics3D getInstance() => Graphics3D.implementation_Graphics3D_getInstance();

    public void bindTarget(object target, bool depthBuffer, int flags)
    {
      this.bindTarget(target, flags | (depthBuffer ? 32 : 0));
    }

    public virtual void bindTarget(object target, int flags)
    {
      this.m_target = target;
      this.m_renderingFlags = flags;
      Renderer renderer = (Renderer) null;
      this.m_renderers.TryGetValue(target, out renderer);
      if (renderer != null)
        return;
      Renderer rendererForTarget = Graphics3D.implementation_Graphics3D_getRendererForTarget(target);
      if (this.m_renderers.ContainsKey(target))
        this.m_renderers[target] = rendererForTarget;
      else
        this.m_renderers.Add(target, rendererForTarget);
    }

    public object getTarget() => this.m_target;

    public int getViewportHeight() => this.m_viewportH;

    public int getViewportWidth() => this.m_viewportW;

    public int getViewportX() => this.m_viewportX;

    public int getViewportY() => this.m_viewportY;

    public void render(World world)
    {
      Camera activeCamera = world.getActiveCamera();
      activeCamera.getTransformTo((Node) world, this.m_cameraToWorld);
      this.setCamera(activeCamera, this.m_cameraToWorld);
      this.clear(world.getBackground());
      this.render((Node) world, (Transform) null);
    }

    protected Renderer getRenderer()
    {
      Renderer renderer;
      return this.m_renderers.TryGetValue(this.getTarget(), out renderer) ? renderer : (Renderer) null;
    }

    public static Graphics3D implementation_Graphics3D_getInstance()
    {
      if (Graphics3D.g3d == null)
        Graphics3D.g3d = new Graphics3D();
      return Graphics3D.g3d;
    }

    public static Renderer implementation_Graphics3D_getRendererForTarget(object target)
    {
      if (Graphics3D.render_target == null)
        Graphics3D.render_target = new RendererXNA(MirrorsEdge.graphicsDevice);
      return (Renderer) Graphics3D.render_target;
    }

    private class TransformCache
    {
      private List<Transform> m_transforms = new List<Transform>();

      public Transform get()
      {
        Transform transform;
        if (this.m_transforms.Count > 0)
        {
          transform = this.m_transforms[0];
          this.m_transforms.RemoveAt(0);
        }
        else
          transform = new Transform();
        return transform;
      }

      public void free(Transform t) => this.m_transforms.Add(t);
    }
  }
}
