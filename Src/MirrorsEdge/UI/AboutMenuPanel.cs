
// Type: UI.AboutMenuPanel
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
//using Microsoft.Phone.Tasks;
using System;
using System.Diagnostics;
using text;

#nullable disable
namespace UI
{
  public class AboutMenuPanel : NotchedSlider
  {
    public const int WIDTH = 200;
    public const int HEIGHT = 62;
    public const int RENDER_EXTRA = 2;

    public AboutMenuPanel(AboutMenu parent)
    {
      this.m_parent = (WindowElement) parent;
      this.setWidth(200);
      this.setHeight(62);
      this.addItem((WindowElement) new AboutMenuItem(2049));
      this.addItem((WindowElement) new AboutMenuItem(2311));
      this.addItem((WindowElement) new AboutMenuItem(2312));
      this.addItem((WindowElement) new AboutMenuItem(2313));
      this.setNotchWidth(199);
      this.setRenderExtra(2);
      this.m_offset = 99f;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (!this.m_draging && this.m_selectedItem != null)
      {
        int stringId = (this.m_selectedItem as AboutMenuItem).getStringId();
        SceneMenu sceneMenu = AppEngine.getCanvas().getSceneMenu();
        TextManager textManager = AppEngine.getCanvas().getTextManager();
        switch (stringId)
        {
          case 2049:
            sceneMenu.stateTransition(SceneMenu.MenuState.STATE_ABOUT);
            (this.m_parent as AboutMenu).setHidden(true);
            break;
          case 2311:
            try
            {
              //new WebBrowserTask()
              //{
              //  Uri = new Uri(textManager.getString(2328))
              //}.Show();
              break;
            }
            catch (InvalidOperationException ex)
            {
              break;
            }
          case 2312:
            try
            {
              //new WebBrowserTask()
              //{
              //  Uri = new Uri(textManager.getString(2330))
              //}.Show();
              break;
            }
            catch (InvalidOperationException ex)
            {
              break;
            }
          case 2313:
            try
            {
              //new WebBrowserTask()
              //{
              //  Uri = new Uri(textManager.getString(2329))
              //}.Show();
              break;
            }
            catch (InvalidOperationException ex)
            {
              Debug.WriteLine(ex.Message);
              break;
            }
        }
      }
      return base.pointerReleased(x, y, pointerNum);
    }
  }
}
