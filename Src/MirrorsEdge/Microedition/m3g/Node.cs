
// Type: microedition.m3g.Node
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace microedition.m3g
{
  public class Node : Transformable
  {
    private const float fixedToFloat = 1.52587891E-05f;
    public const int NONE = 144;
    public const int ORIGIN = 145;
    public const int X_AXIS = 146;
    public const int Y_AXIS = 147;
    public const int Z_AXIS = 148;
    private static List<Node> s_NodeList;
    private static object s_NodeListLock = new object();
    private Node m_Parent;
    private bool m_RenderingEnabled;
    private int m_AlphaFactor;
    private int m_Scope;

    protected Node()
    {
      this.m_Parent = (Node) null;
      this.m_RenderingEnabled = true;
      this.m_AlphaFactor = 65536;
      this.m_Scope = -1;
      if (Node.s_NodeList != null)
        return;
      Node.s_NodeList = new List<Node>();
    }

    public override void Destructor()
    {
      this.m_Parent = (Node) null;
      base.Destructor();
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      ((Node) ret).setAlphaFactorx(this.getAlphaFactorx());
    }

    public override void updateAnimationProperty(int property, float[] value)
    {
      base.updateAnimationProperty(property, value);
      switch (property)
      {
        case 256:
          this.setAlphaFactor(value[0]);
          break;
        case 276:
          this.setRenderingEnable((double) value[0] > 0.5);
          break;
      }
    }

    public override void updateAnimationProperty(AnimationTrack track, int time)
    {
      base.updateAnimationProperty(track, time);
      switch (track.m_Property)
      {
        case 256:
          this.setAlphaFactor(track.getSampleValue(time)[0]);
          break;
        case 276:
          this.setRenderingEnable((double) track.getSampleValue(time)[0] > 0.5);
          break;
      }
    }

    public override void updateAnimationProperty(int property, int[] value)
    {
      base.updateAnimationProperty(property, value);
      switch (property)
      {
        case 256:
          this.setAlphaFactorx(value[0]);
          break;
        case 276:
          this.setRenderingEnable(value[0] > (int) short.MaxValue);
          break;
      }
    }

    public void align(Node node)
    {
    }

    public Node getAlignmentReference(int axis) => (Node) null;

    public int getAlignmentTarget(int axis) => 0;

    public Node getParent() => this.m_Parent;

    public void setParent(Node parent) => this.m_Parent = parent;

    public float getAlphaFactor() => (float) this.getAlphaFactorx() * 1.52587891E-05f;

    public int getAlphaFactorx() => this.m_AlphaFactor;

    public RenderPass getPreRenderPass(int index) => (RenderPass) null;

    public int getPreRenderPassCount() => 0;

    public int getScope() => this.m_Scope;

    private static void requireNodeNotNull(Node node)
    {
    }

    private static bool getPathToParent(Node from, Node target, ref List<Node> path)
    {
      Node.requireNodeNotNull(from);
      Node.requireNodeNotNull(target);
      Node node;
      Node parent;
      for (node = from; node != target; node = parent)
      {
        path.Add(node);
        parent = node.getParent();
        if (parent == null)
          break;
      }
      return node == target;
    }

    public bool getTransformTo(Node target, Transform transform)
    {
      Node.requireNodeNotNull(target);
      lock (Node.s_NodeListLock)
      {
        List<Node> nodeList = Node.s_NodeList;
        bool pathToParent = Node.getPathToParent(this, target, ref nodeList);
        if (!pathToParent)
        {
          nodeList.Clear();
          if (!Node.getPathToParent(target, this, ref nodeList))
          {
            nodeList.Clear();
            return false;
          }
        }
        transform.setIdentity();
        for (int index = nodeList.Count - 1; index >= 0; --index)
          nodeList.ElementAt<Node>(index).getCompositeTransformCumulative(ref transform);
        if (!pathToParent)
          transform.invert();
        nodeList.Clear();
      }
      return true;
    }

    public bool isPickingEnabled() => false;

    public bool isRenderingEnabled() => this.m_RenderingEnabled;

    public void preRender()
    {
    }

    public void setAlignment(Node zRef, int zTarget, Node yRef, int yTarget)
    {
    }

    public void setAlphaFactor(float alpha)
    {
      this.setAlphaFactorx((int) ((double) alpha * 65536.0 + 0.5));
    }

    public void setAlphaFactorx(int alpha) => this.m_AlphaFactor = alpha;

    public void setPickingEnable(bool enable)
    {
    }

    public void setPreRenderPass(int index, RenderPass pass)
    {
    }

    public void setRenderingEnable(bool enable) => this.m_RenderingEnabled = enable;

    public void setScope(int scope) => this.m_Scope = scope;

    public static Node m3g_cast(Object3D obj)
    {
      switch (obj.getM3GUniqueClassID())
      {
        case 5:
        case 9:
        case 12:
        case 14:
        case 16:
        case 18:
        case 22:
          return (Node) obj;
        default:
          return (Node) null;
      }
    }
  }
}
