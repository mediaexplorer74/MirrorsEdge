// Decompiled with JetBrains decompiler
// Type: game.MenuPause
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using midp;
using mirrorsedge_wp7;
using support;
using text;

#nullable disable
namespace game
{
  public class MenuPause : Menu
  {
    public const int SUBMENU_RESUME = 0;
    public const int SUBMENU_RESTART = 1;
    public const int SUBMENU_QUIT = 2;
    public const int SUBMENU_OPTIONS = 3;
    public const int SUBMENU_NUM = 4;
    private string m_chapterName;
    private string m_pausedString;

    public MenuPause()
    {
      this.m_chapterName = (string) null;
      this.m_pausedString = (string) null;
    }

    public override void create()
    {
      this.m_subMenuArray = new MenuMainSubMenu[4];
      for (int index = 0; index < 4; ++index)
        this.m_subMenuArray[index] = new MenuMainSubMenu();
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_1_SELECT_BUTTON"), 5f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_2_SELECT_BUTTON"), 138f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_3_SELECT_BUTTON"), 272f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_4_SELECT_BUTTON"), 405f, 276f, 122f, 27f, 9);
      this.m_subMenuArray[0].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_1_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_1_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_1_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_1_WIDTH"));
      this.m_subMenuArray[0].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_1_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_1_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_1_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_1_WHITE_RIGHT"));
      this.m_subMenuArray[0].create(0, 2089);
      this.m_subMenuArray[1].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_2_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_2_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_2_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_2_WIDTH"));
      this.m_subMenuArray[1].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_2_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_2_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_2_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_2_WHITE_RIGHT"));
      this.m_subMenuArray[1].create(0, 2090);
      this.m_subMenuArray[2].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_3_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_3_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_3_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_3_WIDTH"));
      this.m_subMenuArray[2].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_3_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_3_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_3_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_3_WHITE_RIGHT"));
      this.m_subMenuArray[2].create(0, 2091);
      this.m_subMenuArray[3].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_4_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_4_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_4_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_4_WIDTH"));
      this.m_subMenuArray[3].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_4_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_4_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_4_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_4_WHITE_RIGHT"));
      this.m_subMenuArray[3].create(4, 2078);
      this.m_subMenuArray[3].append(2263);
      this.m_subMenuArray[3].append(2082);
      this.m_subMenuArray[3].append(2051);
      TextManager textManager = canvas.getTextManager();
      Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
      this.m_chapterName = textManager.getString(currentLevelObject.getName()).ToUpper();
      this.m_pausedString = textManager.getString(2088).ToUpper();
      quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_BUTTON_MG"), false);
      if (!MirrorsEdge.TrialMode)
        return;
      quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL"), false);
    }

    public override void render(Graphics g)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      base.render(g);
      int y = 20 + textManager.getLineHeight(34);
      StringRenderer stringRenderer1 = textManager.getStringRenderer(15);
      int color1 = stringRenderer1.getColor();
      stringRenderer1.setColor(0);
      textManager.drawString(g, this.m_chapterName, 15, 25, 21, 9);
      stringRenderer1.setColor(color1);
      textManager.drawString(g, this.m_chapterName, 15, 24, 20, 9);
      StringRenderer stringRenderer2 = textManager.getStringRenderer(14);
      int color2 = stringRenderer2.getColor();
      stringRenderer2.setColor(0);
      textManager.drawString(g, this.m_pausedString, 14, 25, y + 1, 9);
      stringRenderer2.setColor(color2);
      textManager.drawString(g, this.m_pausedString, 14, 24, y, 9);
    }
  }
}
