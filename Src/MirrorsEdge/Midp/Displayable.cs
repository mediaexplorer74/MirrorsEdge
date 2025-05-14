// Decompiled with JetBrains decompiler
// Type: midp.Displayable
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

#nullable disable
namespace midp
{
  public class Displayable : meObject
  {
    public const int ORIENTATION_PORTRAIT_RIGHT = 0;
    public const int ORIENTATION_PORTRAIT_LEFT = 1;
    public const int ORIENTATION_LANDSCAPE_RIGHT = 2;
    public const int ORIENTATION_LANDSCAPE_LEFT = 3;
    private Display m_currentDisplay;
    private int m_width;
    private int m_height;
    private int m_orientation;

    protected Displayable()
    {
      this.m_currentDisplay = (Display) null;
      this.m_width = 0;
      this.m_height = 0;
      this.m_orientation = 0;
    }

    public void setDisplay(Display display) => this.m_currentDisplay = display;

    public Display getDisplay() => this.m_currentDisplay;

    public virtual void showNotify()
    {
    }

    public virtual void hideNotify()
    {
    }

    public virtual void memoryWarning()
    {
    }

    public virtual void sizeChanged(int w, int h)
    {
      this.m_width = w;
      this.m_height = h;
    }

    public virtual void orientationChanged(int orientation) => this.m_orientation = orientation;

    public virtual void keyPressed(int keyCode)
    {
    }

    public virtual void keyReleased(int keyCode)
    {
    }

    public virtual void keyRepeated(int keyCode)
    {
    }

    public override void Destructor() => base.Destructor();

    public override meClass getClass() => (meClass) new DisplayableClass();

    public virtual bool OnHardBackKeyEvent() => false;

    public virtual void pointerDragged(int x, int y, int pointerNum)
    {
    }

    public virtual void pointerPressed(int x, int y, int pointerNum)
    {
    }

    public virtual void pointerReleased(int x, int y, int pointerNum)
    {
    }

    public virtual bool getPointerPos(ref int[] xy, int pointerNum) => false;

    public virtual int getPointerTapCount(int pointerNum) => 0;

    public int getWidth()
    {
      switch (this.m_orientation)
      {
        case 0:
        case 1:
          return this.m_width;
        case 2:
        case 3:
          return this.m_height;
        default:
          return this.m_width;
      }
    }

    public int getHeight()
    {
      switch (this.m_orientation)
      {
        case 0:
        case 1:
          return this.m_height;
        case 2:
        case 3:
          return this.m_width;
        default:
          return this.m_height;
      }
    }

    public int getOrientation() => this.m_orientation;

    public void transformRect(int[] rect)
    {
      int num1 = rect[0];
      int num2 = rect[1];
      int num3 = rect[2];
      int num4 = rect[3];
      switch (this.getOrientation())
      {
        case 1:
          rect[0] = this.m_width - num1 - num3;
          rect[1] = this.m_height - num2 - num4;
          break;
        case 2:
          rect[0] = this.m_width - num2 - num4;
          rect[1] = num1;
          rect[2] = num4;
          rect[3] = num3;
          break;
        case 3:
          rect[0] = num2;
          rect[1] = this.m_height - num1 - num3;
          rect[2] = num4;
          rect[3] = num3;
          break;
      }
    }

    public virtual bool isCanvas() => false;
  }
}
