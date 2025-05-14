// Decompiled with JetBrains decompiler
// Type: UI.WindowElement
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using support;

#nullable disable
namespace UI
{
  public abstract class WindowElement
  {
    protected int m_x;
    protected int m_y;
    protected int m_width;
    protected int m_height;
    protected WindowElement m_parent;
    protected QuadManager m_quadManager;

    public WindowElement()
    {
      this.m_x = 0;
      this.m_y = 0;
      this.m_width = 0;
      this.m_height = 0;
      this.m_quadManager = AppEngine.getCanvas().getQuadManager();
      this.m_parent = (WindowElement) null;
    }

    public WindowElement(int x, int y, int width, int height)
    {
      this.m_x = x;
      this.m_y = y;
      this.m_width = width;
      this.m_height = height;
      this.m_quadManager = AppEngine.getCanvas().getQuadManager();
      this.m_parent = (WindowElement) null;
    }

    public virtual void Destructor()
    {
      this.m_parent = (WindowElement) null;
      this.m_quadManager = (QuadManager) null;
    }

    public virtual void update(int timeStep)
    {
    }

    public virtual void render(Graphics g, int top) => this.render(g, top, 0);

    public virtual void render(Graphics g) => this.render(g, 0, 0);

    public abstract void render(Graphics g, int top, int left);

    public virtual bool pointerPressed(int x, int y, int pointerNum) => false;

    public virtual bool pointerReleased(int x, int y, int pointerNum) => false;

    public virtual bool pointerDragged(int x, int y, int pointerNum) => false;

    public virtual void setX(int x) => this.m_x = x;

    public virtual void setY(int y) => this.m_y = y;

    public virtual void setWidth(int width) => this.m_width = width;

    public virtual void setHeight(int height) => this.m_height = height;

    public virtual void setPosition(int x, int y)
    {
      this.m_x = x;
      this.m_y = y;
    }

    public virtual void setDimensions(int width, int height)
    {
      this.m_width = width;
      this.m_height = height;
    }

    public virtual int getX() => this.m_x;

    public virtual int getY() => this.m_y;

    public virtual int getWidth() => this.m_width;

    public virtual int getHeight() => this.m_height;

    public virtual bool contains(int x, int y)
    {
      int num1 = x - this.m_x;
      int num2 = y - this.m_y;
      bool flag1 = num1 > 0 && num1 < this.m_width;
      bool flag2 = num2 > 0 && num2 < this.m_height;
      return flag1 && flag2;
    }

    public virtual int toRelativeX(int x) => x - this.m_x;

    public virtual int toRelativeY(int y) => y - this.m_y;

    public void setParent(WindowElement parent) => this.m_parent = parent;

    public WindowElement getParent() => this.m_parent;
  }
}
