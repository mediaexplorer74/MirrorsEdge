
// Type: UI.LoadingScreen
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;
using System.Collections.Generic;

#nullable disable
namespace UI
{
  public class LoadingScreen : WindowElement
  {
    public const int LOADING_SCREEN_WIDTH = 256;
    public const int LOADING_SCREEN_HEIGHT = 1024;
    public const int LOADING_SCREEN_SHORT_HEIGHT = 512;
    protected int m_totalHeight;
    protected int m_colour;
    protected List<WindowElement> m_elements;

    public LoadingScreen(DataInputStream dis)
      : base(0, 0, 256, 1024)
    {
      this.m_colour = 0;
      this.m_elements = new List<WindowElement>();
      this.m_totalHeight = 0;
      this.loadScreen(dis);
    }

    public override void Destructor()
    {
      foreach (WindowElement element in this.m_elements)
        element.Destructor();
      this.m_elements.Clear();
      this.m_elements = (List<WindowElement>) null;
      base.Destructor();
    }

    public int getColour() => this.m_colour;

    public override void render(Graphics g, int top, int left)
    {
      int resourceID = (int) ResourceManager.get("IDI_UI_LOADING_TEXT_BLANK_PNG");
      Image src = AppEngine.getCanvas().getResourceManager().loadImage(resourceID);
      g.drawRegion(src, 0, 0, this.m_width * Runtime.pixelScale, this.m_height * Runtime.pixelScale, 0, this.m_x, this.m_y, 9);
      foreach (WindowElement element in this.m_elements)
        element.render(g, top, left);
    }

    public int getTotalHeight() => this.m_totalHeight;

    protected void loadScreen(DataInputStream dis)
    {
      this.m_colour = (int) dis.readByte();
      int num = dis.readInt();
      int yOffset = 5;
      for (int index = 0; index < num; ++index)
      {
        switch (dis.readByte())
        {
          case 0:
            LoadingScreenStringElement screenStringElement = new LoadingScreenStringElement(dis, yOffset);
            this.m_elements.Add((WindowElement) screenStringElement);
            yOffset += screenStringElement.getHeight();
            if (screenStringElement.usesLargeFont() && screenStringElement.isCentered())
            {
              yOffset += 10;
              break;
            }
            if (!screenStringElement.usesLargeFont() && !screenStringElement.isCentered())
            {
              yOffset += 25;
              break;
            }
            if (!screenStringElement.usesLargeFont() && screenStringElement.isCentered())
            {
              yOffset += 10;
              break;
            }
            break;
          case 1:
            LoadingScreenImageElement screenImageElement = new LoadingScreenImageElement(dis, yOffset);
            this.m_elements.Add((WindowElement) screenImageElement);
            yOffset += screenImageElement.getHeight();
            break;
        }
      }
      this.m_totalHeight = yOffset;
    }
  }
}
