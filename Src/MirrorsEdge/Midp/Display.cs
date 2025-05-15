
// Type: midp.Display
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace midp
{
  public abstract class Display : meObject
  {
    private Displayable m_currentDisplayable;
    private Graphics m_graphics;
    private int m_width;
    private int m_height;
    private int m_orientation;
    private bool m_isShowing;
    private bool m_isRefreshing;

    protected Display(int w, int h)
    {
      this.m_currentDisplayable = (Displayable) null;
      this.m_graphics = (Graphics) null;
      this.m_width = w;
      this.m_height = h;
      this.m_orientation = 0;
      this.m_isShowing = false;
      this.m_isRefreshing = false;
    }

    public virtual void showNotify()
    {
      if (!this.m_isShowing && this.m_currentDisplayable != null)
        this.m_currentDisplayable.showNotify();
      this.m_isShowing = true;
    }

    public virtual void hideNotify()
    {
      if (this.m_isShowing && this.m_currentDisplayable != null)
        this.m_currentDisplayable.hideNotify();
      this.m_isShowing = false;
    }

    public virtual void memoryWarning()
    {
      if (this.m_currentDisplayable == null)
        return;
      this.m_currentDisplayable.memoryWarning();
    }

    public virtual void refresh()
    {
      if (!this.m_isShowing)
        return;
      this.m_isRefreshing = true;
      Displayable current = this.getCurrent();
      if (current != null && current.isCanvas())
      {
        Canvas canvas = (Canvas) current;
        Graphics graphics = this.getGraphics();
        graphics.bind2D();
        canvas.paint(graphics);
      }
      this.m_isRefreshing = false;
    }

    public int getWidth() => this.m_width;

    public int getHeight() => this.m_height;

    protected void setGraphics(Graphics g)
    {
      this.m_graphics = g;
      g.pixelScale = Runtime.pixelScale;
    }

    public override void Destructor()
    {
      this.m_currentDisplayable = (Displayable) null;
      this.m_graphics = (Graphics) null;
      base.Destructor();
    }

    public override meClass getClass() => (meClass) new DisplayClass();

    public Displayable getCurrent() => this.m_currentDisplayable;

    public static Display getDisplay(MIDlet m) => m.getDisplay();

    public virtual void setCurrent(Displayable nextDisplayable)
    {
      if (nextDisplayable == this.m_currentDisplayable)
        return;
      while (this.m_isRefreshing)
        Task.Delay(1);
      if (this.m_currentDisplayable != null)
      {
        this.m_currentDisplayable.hideNotify();
        this.m_currentDisplayable.setDisplay((Display) null);
      }
      this.m_currentDisplayable = nextDisplayable;
      if (this.m_currentDisplayable == null)
        return;
      this.m_currentDisplayable.setDisplay(this);
      this.m_currentDisplayable.sizeChanged(this.getWidth(), this.getHeight());
      this.m_currentDisplayable.orientationChanged(this.getOrientation());
      if (!this.m_isShowing)
        return;
      this.m_currentDisplayable.showNotify();
    }

    public void setOrientation(int orientation)
    {
      if (this.m_currentDisplayable != null && this.m_orientation != orientation)
        this.m_currentDisplayable.orientationChanged(orientation);
      this.m_orientation = orientation;
    }

    public int getOrientation() => this.m_orientation;

    public Graphics getGraphics() => this.m_graphics;

    public virtual bool vibrate(int duration) => false;
  }
}
