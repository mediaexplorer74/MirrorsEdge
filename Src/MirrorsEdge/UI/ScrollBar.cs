
// Type: UI.ScrollBar
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using System;

#nullable disable
namespace UI
{
  public abstract class ScrollBar : WindowElement
  {
    public const int SCROLLBAR_WIDTH = 10;
    public const int SCROLLBAR_MIN_HEIGHT = 10;
    public const float SCROLL_DRAG = 4f;
    public const float ZERO_VELOCITY = 0.001f;
    protected Window m_window;
    protected bool m_scrolling;
    protected int m_lastOffset;
    protected float m_velocity;

    public ScrollBar(Window window)
    {
      this.m_window = window;
      this.m_scrolling = false;
      this.m_lastOffset = 0;
      this.m_velocity = 0.0f;
    }

    public override void Destructor()
    {
      this.m_window = (Window) null;
      base.Destructor();
    }

    public new void update(int timeStep)
    {
      float num1 = (float) timeStep / 1000f;
      int offset = this.getOffset();
      if (this.m_scrolling)
      {
        this.m_velocity = (float) (this.m_lastOffset - offset) / num1;
        this.m_lastOffset = offset;
      }
      else
        this.setOffset(offset - (int) ((double) this.m_velocity * (double) num1));
      float num2 = 4f * Math.Abs(this.m_velocity) * num1;
      if ((double) this.m_velocity > 0.0)
      {
        this.m_velocity -= num2;
        if ((double) this.m_velocity > 1.0 / 1000.0)
          return;
        this.m_velocity = 0.0f;
      }
      else
      {
        if ((double) this.m_velocity >= 0.0)
          return;
        this.m_velocity += num2;
        if ((double) this.m_velocity < -1.0 / 1000.0)
          return;
        this.m_velocity = 0.0f;
      }
    }

    public void setScrolling(bool scrolling)
    {
      this.m_scrolling = scrolling;
      if (!this.m_scrolling)
        return;
      this.m_lastOffset = this.getOffset();
      this.m_velocity = 0.0f;
    }

    protected abstract int getOffset();

    protected abstract void setOffset(int offset);
  }
}
