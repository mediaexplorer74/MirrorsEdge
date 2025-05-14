
// Type: UI.UnlockablePanel
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace UI
{
  public class UnlockablePanel : NotchedSlider
  {
    public const int WIDTH = 363;
    public const int HEIGHT = 162;
    public const int RENDER_EXTRA = 2;
    private UnlockableWindow m_displayWindow;

    public UnlockablePanel(UnlockableWindow displayWindow)
    {
      this.m_displayWindow = displayWindow;
      this.setWidth(363);
      this.setHeight(162);
      this.setNotchWidth(170);
      this.setRenderExtra(2);
      this.m_offset = 85f;
    }

    public override void Destructor()
    {
      this.m_displayWindow = (UnlockableWindow) null;
      base.Destructor();
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      UnlockableItem selectedItem = this.m_selectedItem as UnlockableItem;
      if (!this.m_draging && selectedItem != null && selectedItem.isUnlocked())
        this.m_displayWindow.displayUnlockable(selectedItem);
      return base.pointerReleased(x, y, pointerNum);
    }
  }
}
