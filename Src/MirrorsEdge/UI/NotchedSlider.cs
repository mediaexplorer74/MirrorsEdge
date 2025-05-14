// Decompiled with JetBrains decompiler
// Type: UI.NotchedSlider
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using System;
using System.Collections.Generic;

#nullable disable
namespace UI
{
  public class NotchedSlider : WindowElement
  {
    public const float NOTCH_FORCE = 2000f;
    public const float NOTCH_CATCH_VELOCITY = 500.5f;
    public const int NOTCH_CATCH_DISTANCE = 5;
    public const float TARGETING_VEL_PER_NOTCH = 5f;
    public const int MIN_TARGET_DISTANCE = 50;
    protected bool m_enabled;
    protected bool m_pressed;
    protected List<WindowElement> m_items;
    protected float m_offset;
    protected float m_velocity;
    protected int m_lastDragX;
    protected bool m_draging;
    protected WindowElement m_selectedItem;
    protected int m_selectedNotch;
    protected int m_targetNotch;
    protected int m_notchWidth;
    protected int m_renderExtra;
    protected int m_dragDistance;
    protected bool m_looped;

    public NotchedSlider()
    {
      this.m_items = new List<WindowElement>();
      this.m_offset = 0.0f;
      this.m_velocity = 0.0f;
      this.m_lastDragX = 0;
      this.m_draging = false;
      this.m_selectedItem = (WindowElement) null;
      this.m_selectedNotch = 0;
      this.m_notchWidth = 0;
      this.m_renderExtra = 0;
      this.m_targetNotch = -1;
      this.m_dragDistance = 0;
      this.m_looped = true;
      this.m_enabled = true;
      this.m_pressed = false;
    }

    public override void Destructor()
    {
      foreach (WindowElement windowElement in this.m_items)
        windowElement.Destructor();
      this.m_items.Clear();
      this.m_selectedItem = (WindowElement) null;
      base.Destructor();
    }

    public void addItem(WindowElement newItem)
    {
      newItem.setX(0);
      newItem.setY(this.m_height - newItem.getHeight() >> 1);
      this.m_items.Add(newItem);
    }

    public WindowElement getSelectedItem() => this.m_selectedItem;

    public void setNotchWidth(int width) => this.m_notchWidth = width;

    public int getNotchWidth() => this.m_notchWidth;

    public void setRenderExtra(int extra) => this.m_renderExtra = extra;

    public int getRenderExtra() => this.m_renderExtra;

    public bool canNext()
    {
      if (!this.m_enabled)
        return false;
      return this.m_looped || (this.m_targetNotch == -1 ? this.m_selectedNotch : this.m_targetNotch) < this.m_items.Count - 1;
    }

    public bool canPrev()
    {
      if (!this.m_enabled)
        return false;
      return this.m_looped || (this.m_targetNotch == -1 ? this.m_selectedNotch : this.m_targetNotch) > 0;
    }

    public void next()
    {
      if (!this.m_enabled)
        return;
      int num = this.m_targetNotch == -1 ? this.m_selectedNotch : this.m_targetNotch;
      if (this.m_looped)
        this.m_targetNotch = (num + 1) % this.m_items.Count;
      else
        this.m_targetNotch = Math.Min(num + 1, this.m_items.Count - 1);
    }

    public void prev()
    {
      if (!this.m_enabled)
        return;
      this.m_targetNotch = (this.m_targetNotch == -1 ? this.m_selectedNotch : this.m_targetNotch) - 1;
      if (this.m_targetNotch >= 0)
        return;
      if (this.m_looped)
        this.m_targetNotch += this.m_items.Count;
      else
        this.m_targetNotch = 0;
    }

    public void setTargetNotch(int index) => this.m_targetNotch = index;

    public int getSelectedNotch() => this.m_selectedNotch;

    public bool isDragging() => this.m_draging;

    public void setLooped(bool looped) => this.m_looped = looped;

    public bool isIdle() => (double) this.m_velocity == 0.0 && !this.m_draging;

    public bool isDraggingButNotAtAnEdgeToKeepQAHappy()
    {
      return (this.m_draging || (double) this.m_velocity != 0.0) && (double) this.m_offset != (double) (this.m_notchWidth / 2) && (double) this.m_offset != (double) (this.m_items.Count * this.m_notchWidth);
    }

    public bool isEnabled() => this.m_enabled;

    public void setEnabled(bool enabled) => this.m_enabled = enabled;

    public override void update(int timeStep)
    {
      float num1 = (float) timeStep / 1000f;
      if (this.m_draging)
      {
        this.m_velocity = (float) -((double) this.m_dragDistance / (double) num1);
        this.m_dragDistance = 0;
      }
      else
      {
        bool flag = true;
        if (this.m_targetNotch != -1)
        {
          if (this.m_looped)
          {
            int num2 = this.m_items.Count * this.m_notchWidth;
            int num3 = num2 >> 1;
            int num4 = (int) ((double) (this.m_targetNotch * this.m_notchWidth + (this.m_notchWidth >> 1)) - (double) this.m_offset);
            int num5 = num4 < 0 ? num4 + num2 : num4;
            int num6 = num5 > num3 ? num2 - num5 : num5;
            if (num6 > 50)
            {
              this.m_velocity = num5 >= num2 >> 1 ? 5f * (float) -Math.Abs(num6) : 5f * (float) Math.Abs(num6);
              flag = false;
            }
            else
              this.m_targetNotch = -1;
          }
          else
          {
            int num7 = this.m_targetNotch * this.m_notchWidth + (this.m_notchWidth >> 1);
            int num8 = (int) ((double) this.m_offset - (double) num7);
            if (Math.Abs(num8) > 50)
            {
              if ((double) num7 > (double) this.m_offset)
                this.m_velocity = 5f * (float) Math.Abs(num8);
              else if ((double) num7 < (double) this.m_offset)
                this.m_velocity = 5f * (float) -Math.Abs(num8);
              flag = false;
            }
            else
              this.m_targetNotch = -1;
          }
        }
        if (flag)
        {
          int num9 = (int) this.m_offset % this.m_notchWidth;
          if ((double) Math.Abs(this.m_velocity) < 500.5 && Math.Abs(num9 - (this.m_notchWidth >> 1)) < 5)
          {
            this.m_velocity = 0.0f;
            this.m_selectedNotch = Math.Min((int) ((double) this.m_offset / (double) this.m_notchWidth), this.m_items.Count - 1);
            this.m_selectedNotch = Math.Max(0, this.m_selectedNotch);
            this.m_selectedItem = this.m_items[this.m_selectedNotch];
            this.m_offset = (float) (this.m_selectedNotch * this.m_notchWidth + (this.m_notchWidth >> 1));
          }
          else if (num9 < this.m_notchWidth >> 1)
            this.m_velocity += 2000f * (float) (1.0 - (double) num9 / (double) (this.m_notchWidth >> 1)) * num1;
          else if (num9 > this.m_notchWidth >> 1)
            this.m_velocity -= 2000f * (((float) num9 - (float) (this.m_notchWidth >> 1)) / (float) (this.m_notchWidth >> 1)) * num1;
        }
        this.m_offset += this.m_velocity * num1;
        this.m_selectedNotch = Math.Min((int) ((double) this.m_offset / (double) this.m_notchWidth), this.m_items.Count - 1);
        this.m_selectedNotch = Math.Max(0, this.m_selectedNotch);
        this.m_selectedItem = this.m_items[this.m_selectedNotch];
      }
      float num10 = 4f * Math.Abs(this.m_velocity) * num1;
      if ((double) this.m_velocity > 0.0)
      {
        this.m_velocity -= num10;
        if ((double) this.m_velocity <= 1.0 / 1000.0)
          this.m_velocity = 0.0f;
      }
      else if ((double) this.m_velocity < 0.0)
      {
        this.m_velocity += num10;
        if ((double) this.m_velocity >= -1.0 / 1000.0)
          this.m_velocity = 0.0f;
      }
      float num11 = (float) (this.m_items.Count * this.m_notchWidth);
      if (this.m_looped)
      {
        while ((double) this.m_offset < 0.0)
          this.m_offset += num11;
        while ((double) this.m_offset >= (double) num11)
          this.m_offset -= num11;
      }
      else
      {
        int num12 = this.m_notchWidth >> 1;
        int num13 = (int) ((double) num11 - (double) (this.m_notchWidth >> 1));
        if ((double) this.m_offset < (double) num12)
        {
          this.m_offset = (float) num12;
        }
        else
        {
          if ((double) this.m_offset <= (double) num13)
            return;
          this.m_offset = (float) num13;
        }
      }
    }

    public override void render(Graphics g, int top, int left)
    {
      int clipX = g.getClipX();
      int clipY = g.getClipY();
      int clipWidth = g.getClipWidth();
      int clipHeight = g.getClipHeight();
      g.setClip(left + this.m_x, top + this.m_y, this.m_width, this.m_height);
      int num1 = (int) ((double) this.m_offset / (double) this.m_notchWidth);
      int num2 = num1 - this.m_renderExtra;
      int num3 = num1 + this.m_renderExtra;
      if (this.m_looped)
      {
        num2 = num2 < 0 ? this.m_items.Count + num2 : num2;
        int num4 = num3 > this.m_items.Count ? num3 - this.m_items.Count : num3;
      }
      int num5 = (int) this.m_offset % this.m_notchWidth;
      int left1 = left + this.m_x + (this.m_width >> 1) - this.m_notchWidth * this.m_renderExtra - num5;
      int index1 = num2;
      for (int index2 = 0; index2 < (this.m_renderExtra << 1) + 1; ++index2)
      {
        if (index1 < 0 || index1 >= this.m_items.Count)
        {
          left1 += this.m_notchWidth;
          ++index1;
        }
        else
        {
          this.m_items[index1].render(g, top + this.m_y, left1);
          left1 += this.m_notchWidth;
          ++index1;
          if (this.m_looped && index1 == this.m_items.Count)
            index1 -= this.m_items.Count;
        }
      }
      g.setClip(clipX, clipY, clipWidth, clipHeight);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (!this.m_enabled)
        return false;
      this.m_lastDragX = x;
      this.m_pressed = true;
      return false;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      this.m_draging = false;
      this.m_pressed = false;
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (!this.m_enabled || !this.m_pressed || Math.Abs(this.m_lastDragX - x) < 10)
        return false;
      this.m_targetNotch = -1;
      this.m_draging = true;
      this.m_offset -= (float) (x - this.m_lastDragX);
      if (!this.m_looped)
      {
        this.m_offset = (float) Math.Max(this.m_notchWidth >> 1, Math.Min(this.m_items.Count * this.m_notchWidth - (this.m_notchWidth >> 1), (int) this.m_offset));
        this.m_velocity = 0.0f;
      }
      this.m_dragDistance += x - this.m_lastDragX;
      this.m_lastDragX = x;
      return true;
    }
  }
}
