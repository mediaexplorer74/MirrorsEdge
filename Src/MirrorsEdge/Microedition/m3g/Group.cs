
// Type: microedition.m3g.Group
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace microedition.m3g
{
  public class Group : Node
  {
    public new const int M3G_UNIQUE_CLASS_ID = 9;
    private List<Node> m_Children;

    public Group() => this.m_Children = (List<Node>) null;

    public override void Destructor()
    {
      if (this.m_Children != null)
      {
        foreach (Object3D child in this.m_Children)
          child.Destructor();
        this.m_Children.Clear();
        this.m_Children = (List<Node>) null;
      }
      base.Destructor();
    }

    protected override void duplicateTo(ref Object3D ret)
    {
      base.duplicateTo(ref ret);
      Group group = (Group) ret;
      for (int index = 0; index < this.getChildCount(); ++index)
      {
        Node child = (Node) this.getChild(index).duplicate();
        group.addChild(child);
      }
    }

    public override int getReferences(ref Object3D[] references)
    {
      int references1 = base.getReferences(ref references);
      int num = references1;
      int references2 = references1 + this.getChildCount();
      if (references != null)
      {
        for (int index = 0; index < this.getChildCount(); ++index)
          references[num++] = (Object3D) this.getChild(index);
      }
      return references2;
    }

    protected override void findReferences(ref Object3DFinder finder)
    {
      base.findReferences(ref finder);
      int childCount = this.getChildCount();
      for (int index = 0; index < childCount; ++index)
        finder.find((Object3D) this.getChild(index));
    }

    protected override void animateReferences(int time)
    {
      if (!this.isRenderingEnabled())
        return;
      this.animateReferencesGroup(time);
    }

    protected void animateReferencesGroup(int time)
    {
      int childCount = this.getChildCount();
      if (childCount == 0)
        return;
      for (int index = 0; index < childCount; ++index)
        this.m_Children[index].animate(time);
    }

    public void addChild(Node child)
    {
      if (this.m_Children == null)
        this.m_Children = new List<Node>();
      child.setParent((Node) this);
      this.m_Children.Add(child);
    }

    public void clearChildren()
    {
      if (this.m_Children == null)
        return;
      this.m_Children.Clear();
    }

    public void removeChild(Node child)
    {
      child.setParent((Node) null);
      if (this.m_Children == null)
        return;
      this.m_Children.Remove(child);
    }

    public int getChildCount() => this.m_Children == null ? 0 : this.m_Children.Count;

    public Node getChild(int index) => this.m_Children.ElementAt<Node>(index);

    public override int getM3GUniqueClassID() => 9;

    public static Group m3g_cast(Object3D obj)
    {
      switch (obj.getM3GUniqueClassID())
      {
        case 9:
        case 22:
          return (Group) obj;
        default:
          return (Group) null;
      }
    }
  }
}
