
// Type: UI.Window
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using support;
using System;
using System.Collections.Generic;

#nullable disable
namespace UI
{
  public class Window : WindowElement
  {
    public const int DEFAULT_CLIENT_PADDING = 10;
    public const int DEFAULT_CLIENT_EXCESSX = 5;
    public const int DEFAULT_CLIENT_EXCESSY = 5;
    protected int m_title;
    protected int m_clientPaddingX;
    protected int m_clientPaddingY;
    protected int m_clientX;
    protected int m_clientY;
    protected int m_clientWidth;
    protected int m_clientHeight;
    protected int m_clientMaxXOffset;
    protected int m_clientMaxYOffset;
    protected int m_clientOffsetX;
    protected int m_clientOffsetY;
    protected int m_mousePosX;
    protected int m_mousePosY;
    protected List<WindowElement> m_elements;
    protected int m_clientBoundsW;
    protected int m_clientBoundsH;
    protected VerticalScrollBar m_verticalScrollbar;
    protected HorizontalScrollBar m_horizontalScrollbar;
    protected bool m_closed;
    protected WindowResult m_result;
    protected bool m_deleteOnClose;

    public Window()
      : base(0, 0, 533, 320)
    {
      this.m_title = 2048;
      this.m_clientPaddingX = 10;
      this.m_clientPaddingY = 10;
      this.m_clientX = this.m_clientPaddingX;
      this.m_clientY = this.m_clientPaddingY;
      this.m_clientWidth = 533 - (this.m_clientPaddingX << 1);
      this.m_clientHeight = 320 - (this.m_clientPaddingY << 1);
      this.m_clientMaxXOffset = 0;
      this.m_clientMaxYOffset = 0;
      this.m_clientOffsetX = 0;
      this.m_clientOffsetY = 0;
      this.m_mousePosX = 0;
      this.m_mousePosY = 0;
      this.m_elements = new List<WindowElement>();
      this.m_clientBoundsW = 0;
      this.m_clientBoundsH = 0;
      this.m_verticalScrollbar = (VerticalScrollBar) null;
      this.m_horizontalScrollbar = (HorizontalScrollBar) null;
      this.m_closed = false;
      this.m_result = WindowResult.WINDOW_RESULT_NONE;
      this.m_deleteOnClose = true;
    }

    public Window(int x, int y, int width, int height)
      : base(x, y, width, height)
    {
      this.m_title = 2048;
      this.m_clientPaddingX = 10;
      this.m_clientPaddingY = 10;
      this.m_clientX = x + this.m_clientPaddingX;
      this.m_clientY = y + this.m_clientPaddingY;
      this.m_clientWidth = width - (this.m_clientPaddingX << 1);
      this.m_clientHeight = height - (this.m_clientPaddingY << 1);
      this.m_clientMaxXOffset = 0;
      this.m_clientMaxYOffset = 0;
      this.m_clientOffsetX = 0;
      this.m_clientOffsetY = 0;
      this.m_mousePosX = 0;
      this.m_mousePosY = 0;
      this.m_elements = new List<WindowElement>();
      this.m_clientBoundsW = 0;
      this.m_clientBoundsH = 0;
      this.m_verticalScrollbar = (VerticalScrollBar) null;
      this.m_horizontalScrollbar = (HorizontalScrollBar) null;
      this.m_closed = false;
      this.m_result = WindowResult.WINDOW_RESULT_NONE;
      this.m_deleteOnClose = true;
      this.m_clientWidth = Math.Max(0, this.m_clientWidth);
      this.m_clientHeight = Math.Max(0, this.m_clientHeight);
    }

    public override void Destructor()
    {
      foreach (WindowElement element in this.m_elements)
        element.Destructor();
      this.m_elements.Clear();
      this.m_elements = (List<WindowElement>) null;
      if (this.m_verticalScrollbar != null)
      {
        if (this.m_verticalScrollbar != null)
          this.m_verticalScrollbar.Destructor();
        this.m_verticalScrollbar = (VerticalScrollBar) null;
      }
      if (this.m_horizontalScrollbar != null)
      {
        if (this.m_horizontalScrollbar != null)
          this.m_horizontalScrollbar.Destructor();
        this.m_horizontalScrollbar = (HorizontalScrollBar) null;
      }
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      foreach (WindowElement element in this.m_elements)
        element.update(timeStep);
      if (this.m_verticalScrollbar != null)
        this.m_verticalScrollbar.update(timeStep);
      if (this.m_horizontalScrollbar == null)
        return;
      this.m_horizontalScrollbar.update(timeStep);
    }

    public override void render(Graphics g, int top, int left)
    {
      int clipX = g.getClipX();
      int clipY = g.getClipY();
      int val2_1 = clipX + g.getClipWidth();
      int val2_2 = clipY + g.getClipHeight();
      int x = Math.Max(this.m_clientX, clipX);
      int y = Math.Max(this.m_clientY, clipY);
      int num1 = Math.Min(this.m_clientX + this.m_clientWidth, val2_1);
      int num2 = Math.Min(this.m_clientY + this.m_clientHeight, val2_2);
      g.setClip(x, y, num1 - x, num2 - y);
      foreach (WindowElement element in this.m_elements)
        element.render(g, this.m_clientY + this.m_clientOffsetY, this.m_clientX + this.m_clientOffsetX);
      g.setClip(clipX, clipY, val2_1 - clipX, val2_2 - clipY);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BACKING"), false);
      if (this.m_verticalScrollbar != null)
        this.m_verticalScrollbar.render(g, this.m_clientY, this.m_clientX);
      if (this.m_horizontalScrollbar == null)
        return;
      this.m_horizontalScrollbar.render(g, this.m_clientY, this.m_clientX);
    }

    public virtual bool GetBackKeyCenterIfAny(out int x, out int y)
    {
      x = 0;
      y = 0;
      return false;
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      foreach (WindowElement element in this.m_elements)
      {
        int x1 = element.getX();
        int y1 = element.getY();
        int width = element.getWidth();
        int height = element.getHeight();
        int x2 = x - x1 - this.m_clientOffsetX;
        int y2 = y - y1 - this.m_clientOffsetY;
        bool flag1 = x2 < width;
        bool flag2 = y2 < height;
        if (flag1 && flag2)
        {
          if (element.pointerPressed(x2, y2, pointerNum))
            return true;
          break;
        }
      }
      if (this.m_horizontalScrollbar != null)
        this.m_horizontalScrollbar.setScrolling(true);
      if (this.m_verticalScrollbar != null)
        this.m_verticalScrollbar.setScrolling(true);
      this.m_mousePosX = x;
      this.m_mousePosY = y;
      return false;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      foreach (WindowElement element in this.m_elements)
      {
        int x1 = element.getX();
        int y1 = element.getY();
        int width = element.getWidth();
        int height = element.getHeight();
        int x2 = x - x1 - this.m_clientOffsetX;
        int y2 = y - y1 - this.m_clientOffsetY;
        bool flag1 = x2 < width;
        bool flag2 = y2 < height;
        if (flag1 && flag2)
        {
          if (element.pointerReleased(x2, y2, pointerNum))
            return true;
          break;
        }
      }
      if (this.m_horizontalScrollbar != null)
        this.m_horizontalScrollbar.setScrolling(false);
      if (this.m_verticalScrollbar != null)
        this.m_verticalScrollbar.setScrolling(false);
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      foreach (WindowElement element in this.m_elements)
      {
        int x1 = element.getX();
        int y1 = element.getY();
        int width = element.getWidth();
        int height = element.getHeight();
        int x2 = x - x1 - this.m_clientOffsetX;
        int y2 = y - y1 - this.m_clientOffsetY;
        bool flag1 = x2 < width;
        bool flag2 = y2 < height;
        if (flag1 && flag2)
        {
          if (element.pointerDragged(x2, y2, pointerNum))
            return true;
          break;
        }
      }
      int num1 = x - this.m_mousePosX;
      int num2 = y - this.m_mousePosY;
      this.m_mousePosX = x;
      this.m_mousePosY = y;
      this.setClientOffsetX(this.m_clientOffsetX + num1);
      this.setClientOffsetY(this.m_clientOffsetY + num2);
      return false;
    }

    public override void setX(int x)
    {
      base.setX(x);
      this.adjustClientArea();
    }

    public override void setY(int y)
    {
      base.setY(y);
      this.adjustClientArea();
    }

    public override void setWidth(int width)
    {
      base.setWidth(width);
      this.adjustClientArea();
    }

    public override void setHeight(int height)
    {
      base.setHeight(height);
      this.adjustClientArea();
    }

    public override void setPosition(int x, int y)
    {
      base.setPosition(x, y);
      this.adjustClientArea();
    }

    public override void setDimensions(int width, int height)
    {
      base.setDimensions(width, height);
      this.adjustClientArea();
    }

    public void setClientOffsetX(int offset)
    {
      this.m_clientOffsetX = offset;
      if (-this.m_clientOffsetX < 0)
        this.m_clientOffsetX = 0;
      if (-this.m_clientOffsetX <= this.m_clientMaxXOffset)
        return;
      this.m_clientOffsetX = -this.m_clientMaxXOffset;
    }

    public void setClientOffsetY(int offset)
    {
      this.m_clientOffsetY = offset;
      if (-this.m_clientOffsetY < 0)
        this.m_clientOffsetY = 0;
      if (-this.m_clientOffsetY <= this.m_clientMaxYOffset)
        return;
      this.m_clientOffsetY = -this.m_clientMaxYOffset;
    }

    public int getClientOffsetX() => this.m_clientOffsetX;

    public int getClientOffsetY() => this.m_clientOffsetY;

    public int getClientMaxX() => this.m_clientMaxXOffset;

    public int getClientMaxY() => this.m_clientMaxYOffset;

    public int getClientWidth() => this.m_clientWidth;

    public int getClientHeight() => this.m_clientHeight;

    public WindowElement addElement(WindowElement item)
    {
      int y = item.getY();
      int x = item.getX();
      int num1 = y + item.getHeight() + 5;
      int num2 = x + item.getWidth() + 5;
      if (num2 > this.m_clientBoundsW)
        this.m_clientBoundsW = num2;
      if (num1 > this.m_clientBoundsH)
        this.m_clientBoundsH = num1;
      this.m_elements.Add(item);
      this.adjustClientArea();
      return item;
    }

    public WindowElement removeElement(WindowElement item)
    {
      if (!this.m_elements.Contains(item))
        return (WindowElement) null;
      this.m_elements.Remove(item);
      return item;
    }

    public WindowElement getElement(int idx)
    {
      return idx >= this.m_elements.Count ? (WindowElement) null : this.m_elements[idx];
    }

    public void close() => this.close(WindowResult.WINDOW_RESULT_NONE);

    public void close(WindowResult result)
    {
      this.m_closed = true;
      this.m_result = result;
    }

    public bool isClosed() => this.m_closed;

    public void clearClosed()
    {
      this.m_closed = false;
      this.m_result = WindowResult.WINDOW_RESULT_NONE;
    }

    public WindowResult getWindowResult() => this.m_result;

    public void setDeleteOnClosed(bool doc) => this.m_deleteOnClose = doc;

    public bool deleteOnClosed() => this.m_deleteOnClose;

    protected void adjustClientArea()
    {
      this.m_clientX = this.m_x + this.m_clientPaddingX;
      this.m_clientY = this.m_y + this.m_clientPaddingY;
      this.m_clientWidth = this.m_width - (this.m_clientPaddingX << 1);
      this.m_clientHeight = this.m_height - (this.m_clientPaddingY << 1);
      this.m_clientMaxXOffset = Math.Max(0, this.m_clientBoundsW - this.m_clientWidth);
      this.m_clientMaxYOffset = Math.Max(0, this.m_clientBoundsH - this.m_clientHeight);
      this.adjustScrollBars();
    }

    protected void adjustScrollBars()
    {
      if (this.m_clientMaxXOffset != 0)
        this.showHorizontalScrollbar();
      else if (this.m_horizontalScrollbar != null)
      {
        if (this.m_horizontalScrollbar != null)
          this.m_horizontalScrollbar.Destructor();
        this.m_horizontalScrollbar = (HorizontalScrollBar) null;
      }
      if (this.m_clientMaxYOffset != 0)
      {
        this.showVerticalScrollbar();
      }
      else
      {
        if (this.m_verticalScrollbar == null)
          return;
        if (this.m_verticalScrollbar != null)
          this.m_verticalScrollbar.Destructor();
        this.m_verticalScrollbar = (VerticalScrollBar) null;
      }
    }

    protected void showVerticalScrollbar()
    {
      if (this.m_verticalScrollbar != null)
        return;
      this.m_verticalScrollbar = new VerticalScrollBar(this);
      this.m_verticalScrollbar.setX(this.m_clientWidth - this.m_verticalScrollbar.getWidth());
      this.m_verticalScrollbar.setY(0);
      this.m_verticalScrollbar.setHeight(this.m_clientHeight);
      this.m_clientWidth -= this.m_verticalScrollbar.getWidth();
      this.m_clientMaxXOffset = Math.Max(0, this.m_clientBoundsW - this.m_clientWidth);
      this.adjustScrollBars();
    }

    protected void showHorizontalScrollbar()
    {
      if (this.m_horizontalScrollbar != null)
        return;
      this.m_horizontalScrollbar = new HorizontalScrollBar(this);
      this.m_horizontalScrollbar.setY(this.m_clientHeight - this.m_horizontalScrollbar.getHeight());
      this.m_horizontalScrollbar.setX(0);
      this.m_horizontalScrollbar.setWidth(this.m_clientWidth);
      this.m_clientHeight -= this.m_horizontalScrollbar.getHeight();
      this.m_clientMaxYOffset = Math.Max(0, this.m_clientBoundsH - this.m_clientHeight);
      this.adjustScrollBars();
    }
  }
}
