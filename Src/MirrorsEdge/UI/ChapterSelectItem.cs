// Decompiled with JetBrains decompiler
// Type: UI.ChapterSelectItem
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class ChapterSelectItem : WindowElement
  {
    public const int WIDTH = 303;
    public const int HEIGHT = 40;
    public const int CHAPTER_X_PADDING = 4;
    public const int CHAPTER_Y_PADDING = 5;
    public const int STAT_Y_PADDING = 5;
    public int CHAPTER_TITLE_FONT = 8;
    public int STAT_FONT = 2;
    protected Level m_level;
    protected string m_name;

    public ChapterSelectItem(Level level)
    {
      this.m_level = level;
      this.m_name = AppEngine.getCanvas().getTextManager().getString(level.getName()).ToUpper();
      this.setWidth(303);
      this.setHeight(40);
    }

    public override void Destructor()
    {
      this.m_level = (Level) null;
      this.m_name = (string) null;
      base.Destructor();
    }

    public Level getLevelObject() => this.m_level;

    public override void render(Graphics g, int top, int left)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int x = left + this.m_x + 4;
      StringRenderer stringRenderer = textManager.getStringRenderer(this.CHAPTER_TITLE_FONT);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(0);
      textManager.drawString(g, this.m_name, this.CHAPTER_TITLE_FONT, x + 1, top + this.m_y + 5 + 1, 9);
      stringRenderer.setColor(color);
      textManager.drawString(g, this.m_name, this.CHAPTER_TITLE_FONT, x, top + this.m_y + 5, 9);
    }
  }
}
