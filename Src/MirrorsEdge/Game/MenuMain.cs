// Decompiled with JetBrains decompiler
// Type: game.MenuMain
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using ea;
//using Microsoft.Phone.Tasks;
using midp;
using mirrorsedge_wp7;
using support;
using System;
using System.Diagnostics;
using text;
using UI;

#nullable disable
namespace game
{
  public class MenuMain : Menu
  {
    public const int SUBMENU_PLAY = 0;
    public const int SUBMENU_OPTIONS = 1;
    public const int SUBMENU_EXTRAS = 2;
    public const int SUBMENU_HELP = 3;
    public const int SUBMENU_NUM = 4;
    private const int BANNER_MAX_Y_OFFSET = -163;
    private const int BANNER_MOTION_DELAY = 1500;
    private const int BANNER_MOTION_SPEED = 220;
    private float m_bannerYOffset;
    private float m_targetBannerYOffset;
    private int m_bannerMotionDelay;
    private Image m_bannerImage;

    public MenuMain()
    {
      this.m_bannerYOffset = 0.0f;
      this.m_targetBannerYOffset = 0.0f;
      this.m_bannerMotionDelay = 0;
      this.m_bannerImage = (Image) null;
    }

    public override void create()
    {
      this.m_subMenuArray = new MenuMainSubMenu[4];
      for (int index = 0; index < 4; ++index)
        this.m_subMenuArray[index] = new MenuMainSubMenu();
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_1_SELECT_BUTTON"), 5f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_2_SELECT_BUTTON"), 138f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_3_SELECT_BUTTON"), 272f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_4_SELECT_BUTTON"), 405f, 276f, 122f, 27f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_1_WHITE_CENTER"), -3f, 0.0f, 135f, 320f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_2_WHITE_CENTER"), 130f, 0.0f, 135f, 320f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_3_WHITE_CENTER"), 264f, 0.0f, 135f, 320f, 9);
      quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_RIBBON_4_WHITE_CENTER"), 397f, 0.0f, 135f, 320f, 9);
      this.m_subMenuArray[0].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_1_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_1_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_1_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_1_WIDTH"));
      this.m_subMenuArray[0].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_1_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_1_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_1_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_1_WHITE_RIGHT"));
      this.m_subMenuArray[1].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_2_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_2_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_2_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_2_WIDTH"));
      this.m_subMenuArray[1].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_2_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_2_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_2_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_2_WHITE_RIGHT"));
      this.m_subMenuArray[1].create(3, 2078);
      this.m_subMenuArray[1].append(2082);
      this.m_subMenuArray[1].append(2263);
      this.m_subMenuArray[1].append(2307);
      if (MirrorsEdge.TrialMode)
      {
        quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_BUTTON_MG"), true);
        quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL"), true);
        int stringWidth1 = AppEngine.getCanvas().getTextManager().getStringWidth(2397, 7);
        quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL"), (float) (520 - (stringWidth1 + 20)), 20f, (float) (stringWidth1 + 20), 36f, 9);
        int stringWidth2 = AppEngine.getCanvas().getTextManager().getStringWidth(2266, 7);
        quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_BUTTON_MG"), (float) (520 - (stringWidth2 + 20)), 80f, (float) (stringWidth2 + 20), 36f, 9);
      }
      this.m_subMenuArray[2].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_3_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_3_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_3_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_3_WIDTH"));
      this.m_subMenuArray[2].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_3_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_3_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_3_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_3_WHITE_RIGHT"));
      int numItems = 3;
      if (MirrorsEdge.TrialMode && !MirrorsEdge.GS_Supported)
        numItems = 1;
      else if (MirrorsEdge.TrialMode)
        numItems = 2;
      this.m_subMenuArray[2].create(numItems, 2083);
      if (!MirrorsEdge.TrialMode)
        this.m_subMenuArray[2].append(2305);
      this.m_subMenuArray[2].append(2085);
      if (MirrorsEdge.GS_Supported)
        this.m_subMenuArray[2].append(2077);
      this.m_subMenuArray[3].init((int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_4_RED"), (int) QuadManager.get("MESH_MENU_RIBBON_4_SELECT_BUTTON"), (int) QuadManager.get("GROUP_MENU_MAIN_RIBBON_4_WHITE"), (int) QuadManager.get("ANIM_MENU_RIBBON_4_WIDTH"));
      this.m_subMenuArray[3].setScollingUVs((int) QuadManager.get("TEXTURE_MENU_RIBBON_4_RED_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_4_RED_RIGHT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_4_WHITE_LEFT"), (int) QuadManager.get("TEXTURE_MENU_RIBBON_4_WHITE_RIGHT"));
      this.m_subMenuArray[3].create(2, 2051);
      this.m_subMenuArray[3].append(2051);
      this.m_subMenuArray[3].append(2049);
      if (!MirrorsEdge.TrialMode)
      {
        quadManager.setMeshVisible((int) QuadManager.get("MESH_MENU_BUTTON_MG"), true);
        int stringWidth = AppEngine.getCanvas().getTextManager().getStringWidth(2266, 7);
        quadManager.setMeshBounds((int) QuadManager.get("MESH_MENU_BUTTON_MG"), (float) (520 - (stringWidth + 20)), 20f, (float) (stringWidth + 20), 36f, 9);
      }
      this.m_targetBannerYOffset = 0.0f;
      this.m_bannerYOffset = -163f;
      this.m_bannerMotionDelay = 1500;
      this.m_bannerImage = EASpywareManager.getInstance().getBannerImage();
    }

    public override bool OnHardBackKeyEvent()
    {
      if (!base.OnHardBackKeyEvent())
        return true;
      MirrorsEdge.m_MirrorsEdge.Exit();
      return false;
    }

    public override bool pointerReleased(int x, int y)
    {
      bool flag = base.pointerReleased(x, y);
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      if (quadManager.isPointWithinMesh((int) QuadManager.get("MESH_MENU_BUTTON_MG"), x, y) && !this.isAnySubMenuAnimating())
      {
        SpywareManager.getInstance().trackViewMoreGames();
        canvas.getSceneMenu().stateTransition(SceneMenu.MenuState.STATE_MOREGAMES);
        return false;
      }
      if ((double) this.m_bannerYOffset == 0.0 && !this.isAnySubMenuAnimating())
      {
        int meshX = AppEngine.getCanvas().getQuadManager().getMeshX((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
        int meshY = AppEngine.getCanvas().getQuadManager().getMeshY((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
        int meshWidth = AppEngine.getCanvas().getQuadManager().getMeshWidth((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
        int meshHeight = AppEngine.getCanvas().getQuadManager().getMeshHeight((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
        int num1 = meshY + (meshHeight >> 1);
        int num2 = num1 + 130;
        int num3 = meshX - (meshWidth >> 1);
        int num4 = meshX + (meshWidth >> 1);
        if (y > num1 && y < num2 && x > num3 && x < num4)
        {
          string bannerUrl = EASpywareManager.getInstance().getBannerURL();
          if (bannerUrl != null)
          {
            EASpywareManager.getInstance().logEvent(30011);
            try
            {
                // DELETE THIS LINE
                //new WebBrowserTask() { Uri = new Uri(bannerUrl) }.Show();

                // TODO
                //var uri = new Uri(bannerUrl);
                //System.Launcher.LaunchUriAsync(uri).AsTask().Wait();
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine(ex.Message);
            }
          }
        }
      }
      if (!MirrorsEdge.TrialMode || !quadManager.isPointWithinMesh((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL"), x, y) || this.isAnySubMenuAnimating())
        return flag;
      canvas.getSceneMenu().stateTransitionFade(SceneMenu.MenuState.STATE_LITE_UPSELL);
      canvas.getWindowStore().getButtonEffect().play(quadManager.getMeshX((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL")), quadManager.getMeshY((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL")));
      return false;
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      bool flag = false;
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
      {
        if (this.m_subMenuArray[index].isActive() || this.m_subMenuArray[index].isAnimating())
          flag = true;
      }
      if (this.m_bannerImage == null)
        return;
      float num = this.m_bannerYOffset - this.m_targetBannerYOffset;
      if (this.m_bannerMotionDelay > 0 && !flag)
      {
        this.m_bannerMotionDelay -= timeStep;
      }
      else
      {
        if (this.m_bannerMotionDelay > 0 || (double) Math.Abs(num) <= 0.0099999997764825821)
          return;
        if ((double) num < 0.0)
        {
          this.m_bannerYOffset += (float) (220.0 * ((double) timeStep / 1000.0));
          if ((double) this.m_bannerYOffset <= (double) this.m_targetBannerYOffset)
            return;
          this.m_bannerYOffset = this.m_targetBannerYOffset;
        }
        else
        {
          this.m_bannerYOffset -= (float) (220.0 * ((double) timeStep / 1000.0));
          if ((double) this.m_bannerYOffset >= (double) this.m_targetBannerYOffset)
            return;
          this.m_bannerYOffset = this.m_targetBannerYOffset;
        }
      }
    }

    public override void render(Graphics g)
    {
      int meshX1 = AppEngine.getCanvas().getQuadManager().getMeshX((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
      int meshY = AppEngine.getCanvas().getQuadManager().getMeshY((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
      int meshWidth = AppEngine.getCanvas().getQuadManager().getMeshWidth((int) QuadManager.get("MESH_MENU_BUTTON_MG"));
      AppEngine.getCanvas().getQuadManager().render(g, 128);
      if (this.m_bannerImage != null)
      {
        int y = (int) ((double) meshY + (double) this.m_bannerYOffset);
        g.setClip(meshX1 - (meshWidth >> 1), meshY, meshWidth, 320);
        if ((double) this.m_bannerImage.getWidth() > (double) meshWidth * 1.5)
        {
          int destLeft = meshX1 - (meshWidth >> 1);
          int destTop = y;
          g.drawScaledRegion(this.m_bannerImage, 0, 0, this.m_bannerImage.getWidth(), this.m_bannerImage.getHeight(), destLeft, destTop, destLeft + this.m_bannerImage.getWidth() / Runtime.pixelScale, destTop + this.m_bannerImage.getHeight() / Runtime.pixelScale);
        }
        else
          g.drawImage(this.m_bannerImage, meshX1 - (meshWidth >> 1), y, 9);
        g.setClip(0, 0, 533, 320);
      }
      else
        this.m_bannerImage = EASpywareManager.getInstance().getBannerImage();
      AppEngine.getCanvas().getQuadManager().setMeshBounds((int) QuadManager.get("MESH_MENU_TITLE"), 10f, 20f, 148f, 32f, 9);
      AppEngine.getCanvas().getQuadManager().setMeshVisible((int) QuadManager.get("MESH_MENU_TITLE"), true);
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
        this.m_subMenuArray[index].render(g);
      AppEngine.getCanvas().getQuadManager().render(g, 1024);
      AppEngine.getCanvas().getQuadManager().render(g, 512);
      int x = meshX1;
      int y1 = meshY - 5;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      StringRenderer stringRenderer = textManager.getStringRenderer(7);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(8421504);
      textManager.drawString(g, 2266, 7, x + 1, y1 + 1, 18);
      stringRenderer.setColor(16777215);
      textManager.drawString(g, 2266, 7, x, y1, 18);
      if (MirrorsEdge.TrialMode)
      {
        int meshX2 = AppEngine.getCanvas().getQuadManager().getMeshX((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL"));
        int y2 = AppEngine.getCanvas().getQuadManager().getMeshY((int) QuadManager.get("MESH_MENU_BUTTON_UPSELL")) - 1;
        stringRenderer.setColor(8421504);
        textManager.drawString(g, 2397, 7, meshX2 + 1, y2 + 1, 18);
        stringRenderer.setColor(16777215);
        textManager.drawString(g, 2397, 7, meshX2, y2, 18);
      }
      stringRenderer.setColor(color);
    }

    public override void activateSubMenu(int idx) => this.activateSubMenu(idx, -1);

    public override void activateSubMenu(int idx, int subItem)
    {
      bool flag = false;
      this.m_selectionIndex = idx;
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
      {
        if (index == this.m_selectionIndex && this.m_subMenuArray[index].getLength() > 0 && !this.m_subMenuArray[index].isActive())
        {
          this.m_subMenuArray[index].transitionToActive();
          flag = true;
        }
        else
          this.m_subMenuArray[index].transitionToIdle();
      }
      if ((double) this.m_targetBannerYOffset == 0.0 && flag)
        this.m_targetBannerYOffset = -163f;
      else if ((double) this.m_targetBannerYOffset == -163.0 && !flag)
        this.m_targetBannerYOffset = 0.0f;
      if (this.m_subMenuArray[this.m_selectionIndex].getLength() == 0)
      {
        this.m_subMenuArray[this.m_selectionIndex].pointerOn(0, 0);
      }
      else
      {
        if (subItem < 0)
          return;
        this.m_subMenuArray[this.m_selectionIndex].setSelectedItem(subItem);
      }
    }
  }
}
