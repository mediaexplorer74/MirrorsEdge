// Decompiled with JetBrains decompiler
// Type: game.MenuMainSubMenu
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using generic;
using microedition.m3g;
using midp;
using support;
using text;

#nullable disable
namespace game
{
  public class MenuMainSubMenu : MenuStringChoice
  {
    public const int RIBBON_SELECTION_HEIGHT = 43;
    public const int MENU_ITEMS_WIDTH = 118;
    public const int MENU_ITEMS_FROM_BOTTOM = 82;
    public const int MENU_ITEM_Y_OFFSET = -53;
    public const int RIBBON_WAVE_FILTER_DURATION = 3000;
    public const int SELECT_MOVE_TRANS_TIME = 500;
    private bool m_itemIsSelected;
    private MenuMainSubMenu.AnimState m_animState;
    private int m_animTime;
    private int m_ribbonGroupId;
    private int m_buttonMeshId;
    private int m_selectOverlayGroupId;
    private int m_ribbonAnimId;
    private SignalFilter m_xOffsetFilter;
    private int m_xOffsetTime;
    private int m_xOffsetSide;
    private float m_ribbonLeftProgress;
    private float m_ribbonRightProgress;
    private Texture2D m_ribbonRedLeftTex;
    private Texture2D m_ribbonRedRightTex;
    private Texture2D m_ribbonWhiteLeftTex;
    private Texture2D m_ribbonWhiteRightTex;
    private int m_selectionAnimationCurItem;
    private int m_selectionAnimationNextItem;
    private InterpolationFloatTimed m_selectInterpolation = new InterpolationFloatTimed();

    public int getButtonMeshId() => this.m_buttonMeshId;

    public int getRibbonAnimId() => this.m_ribbonAnimId;

    public MenuMainSubMenu()
    {
      this.m_itemIsSelected = false;
      this.m_animState = MenuMainSubMenu.AnimState.ANIM_STATE_IDLE;
      this.m_animTime = 0;
      this.m_ribbonGroupId = -1;
      this.m_buttonMeshId = -1;
      this.m_selectOverlayGroupId = -1;
      this.m_ribbonAnimId = -1;
      this.m_xOffsetFilter = new SignalFilter(0, 3000f, 0.0f);
      this.m_xOffsetTime = 3000;
      this.m_xOffsetSide = 1;
      this.m_ribbonLeftProgress = 0.0f;
      this.m_ribbonRightProgress = 0.0f;
      this.m_ribbonRedLeftTex = (Texture2D) null;
      this.m_ribbonRedRightTex = (Texture2D) null;
      this.m_ribbonWhiteLeftTex = (Texture2D) null;
      this.m_ribbonWhiteRightTex = (Texture2D) null;
      this.m_selectionAnimationCurItem = -1;
      this.m_selectionAnimationNextItem = -1;
      this.m_selectInterpolation = new InterpolationFloatTimed();
      AppEngine canvas = AppEngine.getCanvas();
      this.m_xOffsetTime = canvas.rand(1500);
      this.m_xOffsetSide = (canvas.rand(2) >> 1) - 1;
    }

    public MenuMainSubMenu(MenuMainSubMenu other)
      : base((MenuStringChoice) other)
    {
      this.m_itemIsSelected = other.m_itemIsSelected;
      this.m_animState = other.m_animState;
      this.m_animTime = other.m_animTime;
      this.m_ribbonGroupId = other.m_ribbonGroupId;
      this.m_buttonMeshId = other.m_buttonMeshId;
      this.m_selectOverlayGroupId = other.m_buttonMeshId;
      this.m_ribbonAnimId = other.m_ribbonAnimId;
      this.m_xOffsetFilter = new SignalFilter(1, 3000f, 0.0f);
      this.m_xOffsetTime = other.m_xOffsetTime;
      this.m_xOffsetSide = other.m_xOffsetSide;
      this.m_ribbonLeftProgress = other.m_ribbonLeftProgress;
      this.m_ribbonRightProgress = other.m_ribbonRightProgress;
      this.m_ribbonRedLeftTex = other.m_ribbonRedLeftTex;
      this.m_ribbonRedRightTex = other.m_ribbonRedRightTex;
      this.m_ribbonWhiteLeftTex = other.m_ribbonWhiteLeftTex;
      this.m_ribbonWhiteRightTex = other.m_ribbonWhiteRightTex;
      this.m_selectionAnimationCurItem = -1;
      this.m_selectionAnimationNextItem = -1;
      this.m_selectInterpolation = new InterpolationFloatTimed();
    }

    public override void Destructor()
    {
      this.m_ribbonRedLeftTex = (Texture2D) null;
      this.m_ribbonRedRightTex = (Texture2D) null;
      this.m_ribbonWhiteLeftTex = (Texture2D) null;
      this.m_ribbonWhiteRightTex = (Texture2D) null;
      this.m_xOffsetFilter.Destructor();
      this.m_selectInterpolation.Destructor();
      base.Destructor();
    }

    public void init(
      int ribbonGroupId,
      int buttonMeshId,
      int selectOverlayGroupId,
      int ribbonAnimId)
    {
      this.m_itemIsSelected = false;
      this.m_ribbonGroupId = ribbonGroupId;
      this.m_buttonMeshId = buttonMeshId;
      this.m_selectOverlayGroupId = selectOverlayGroupId;
      this.m_ribbonAnimId = ribbonAnimId;
    }

    public void setScollingUVs(
      int ribbonRedLeft,
      int ribbonRedRight,
      int ribbonWhiteLeft,
      int ribbonWhiteRight)
    {
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      this.m_ribbonRedLeftTex = quadManager.getTexture(ribbonRedLeft).texture;
      this.m_ribbonRedRightTex = quadManager.getTexture(ribbonRedRight).texture;
      this.m_ribbonWhiteLeftTex = quadManager.getTexture(ribbonWhiteLeft).texture;
      this.m_ribbonWhiteRightTex = quadManager.getTexture(ribbonWhiteRight).texture;
      this.m_ribbonLeftProgress = canvas.randPercentile();
      this.m_ribbonRightProgress = canvas.randPercentile();
      this.m_ribbonRedLeftTex.setTranslation(0.0f, this.m_ribbonLeftProgress, 0.0f);
      this.m_ribbonRedRightTex.setTranslation(0.0f, this.m_ribbonRightProgress, 0.0f);
      this.m_ribbonWhiteLeftTex.setTranslation(0.0f, this.m_ribbonLeftProgress, 0.0f);
      this.m_ribbonWhiteRightTex.setTranslation(0.0f, this.m_ribbonRightProgress, 0.0f);
    }

    public bool isSelected() => this.m_itemIsSelected && this.m_selectInterpolation.isFinished();

    public void stateTransition(MenuMainSubMenu.AnimState state)
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      this.m_animState = state;
      switch (this.m_animState)
      {
        case MenuMainSubMenu.AnimState.ANIM_STATE_IDLE:
          quadManager.setAnimFrame(this.m_ribbonAnimId, 0);
          break;
        case MenuMainSubMenu.AnimState.ANIM_STATE_EXPANDING:
          quadManager.playAnim(this.m_ribbonAnimId, 2);
          this.m_xOffsetFilter.setTargetValue(0.0f);
          this.m_itemIsSelected = false;
          break;
        case MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE:
          quadManager.setAnimFrame(this.m_ribbonAnimId, 1);
          break;
        case MenuMainSubMenu.AnimState.ANIM_STATE_RETRACTING:
          quadManager.playAnimReverse(this.m_ribbonAnimId, 2);
          break;
      }
      quadManager.setMeshVisible(this.m_buttonMeshId, this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_IDLE);
    }

    public void update(int timeStepMillis)
    {
      this.m_ribbonLeftProgress += (float) timeStepMillis * 0.0002f;
      while (1.0 <= (double) this.m_ribbonLeftProgress)
        --this.m_ribbonLeftProgress;
      this.m_ribbonRightProgress += (float) timeStepMillis * 0.0002f;
      while (1.0 <= (double) this.m_ribbonRightProgress)
        --this.m_ribbonRightProgress;
      this.m_ribbonRedLeftTex.setTranslation(0.0f, this.m_ribbonLeftProgress, 0.0f);
      this.m_ribbonRedRightTex.setTranslation(0.0f, this.m_ribbonRightProgress, 0.0f);
      this.m_ribbonWhiteLeftTex.setTranslation(0.0f, this.m_ribbonLeftProgress, 0.0f);
      this.m_ribbonWhiteRightTex.setTranslation(0.0f, this.m_ribbonRightProgress, 0.0f);
      this.m_xOffsetFilter.update(timeStepMillis);
      if (!this.m_selectInterpolation.isFinished())
      {
        this.m_selectInterpolation.update(timeStepMillis);
        if (this.m_selectInterpolation.isFinished() && this.m_selectionAnimationNextItem != -1)
        {
          int animationNextItem = this.m_selectionAnimationNextItem;
          this.m_selectionAnimationNextItem = -1;
          this.animateToSelection(animationNextItem, false);
        }
      }
      switch (this.m_animState)
      {
        case MenuMainSubMenu.AnimState.ANIM_STATE_IDLE:
          this.m_xOffsetTime -= timeStepMillis;
          if (this.m_xOffsetTime <= 0)
          {
            this.m_xOffsetTime = 2250;
            this.m_xOffsetSide = -this.m_xOffsetSide;
            this.m_xOffsetFilter.setTargetValue(AppEngine.getCanvas().randPercentile() * 8f * (float) this.m_xOffsetSide);
            break;
          }
          break;
        case MenuMainSubMenu.AnimState.ANIM_STATE_EXPANDING:
          QuadManager quadManager1 = AppEngine.getCanvas().getQuadManager();
          quadManager1.updateAnim(this.m_ribbonAnimId, timeStepMillis);
          if (!quadManager1.isAnimating(this.m_ribbonAnimId))
          {
            this.stateTransition(MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE);
            break;
          }
          break;
        case MenuMainSubMenu.AnimState.ANIM_STATE_RETRACTING:
          QuadManager quadManager2 = AppEngine.getCanvas().getQuadManager();
          quadManager2.updateAnim(this.m_ribbonAnimId, timeStepMillis);
          if (!quadManager2.isAnimating(this.m_ribbonAnimId))
          {
            this.stateTransition(MenuMainSubMenu.AnimState.ANIM_STATE_IDLE);
            break;
          }
          break;
      }
      QuadManager quadManager3 = AppEngine.getCanvas().getQuadManager();
      float filteredValue = this.m_xOffsetFilter.getFilteredValue();
      quadManager3.setGroupPosition(this.m_ribbonGroupId, filteredValue, 0.0f);
      quadManager3.setGroupPosition(this.m_selectOverlayGroupId, filteredValue, 0.0f);
    }

    public void render(Graphics g)
    {
      AppEngine canvas = AppEngine.getCanvas();
      TextManager textManager = canvas.getTextManager();
      QuadManager quadManager = canvas.getQuadManager();
      if (this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE)
        this.renderActiveRibbon(g);
      int font = 7;
      string titleAllCapsString = this.getThisTitleAllCapsString();
      if (this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE)
        font = 11;
      int left = 0;
      int top = 0;
      int width = 0;
      int height = 0;
      quadManager.getMeshBounds(this.m_buttonMeshId, ref left, ref top, ref width, ref height);
      if (textManager.getStringWidth(titleAllCapsString, font) >= width)
      {
        font = 15;
        if (textManager.getStringWidth(titleAllCapsString, font) >= width)
        {
          font = 18;
          if (textManager.getStringWidth(titleAllCapsString, font) >= width)
          {
            font = 5;
            if (textManager.getStringWidth(titleAllCapsString, font) >= width)
              font = 25;
          }
        }
      }
      StringRenderer stringRenderer = textManager.getStringRenderer(font);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(0);
      textManager.drawString(g, titleAllCapsString, font, quadManager.getMeshX(this.m_buttonMeshId) - 3 + 1, quadManager.getMeshY(this.m_buttonMeshId) - 1 + 1, 18);
      stringRenderer.setColor(16777215);
      textManager.drawString(g, titleAllCapsString, font, quadManager.getMeshX(this.m_buttonMeshId) - 3, quadManager.getMeshY(this.m_buttonMeshId) - 1, 18);
      stringRenderer.setColor(color);
    }

    private void renderActiveRibbon(Graphics g)
    {
      AppEngine canvas = AppEngine.getCanvas();
      TextManager textManager = canvas.getTextManager();
      QuadManager quadManager = canvas.getQuadManager();
      int x = quadManager.getMeshX(this.m_buttonMeshId) - 59;
      canvas.getHeight();
      int num1 = this.m_selectInterpolation.isFinished() ? canvas.getHeight() - 82 + this.getSelectBarIndex() * -53 : (int) this.m_selectInterpolation.getCurrentValue();
      g.setClip(0, num1 - 21, canvas.getWidth(), 43);
      quadManager.setGroupVisible(this.m_selectOverlayGroupId, true);
      quadManager.render(g, 256);
      quadManager.setGroupVisible(this.m_selectOverlayGroupId, false);
      g.setClip(0, 0, canvas.getWidth(), canvas.getHeight());
      int length = this.getLength();
      int selectBarIndex = this.getSelectBarIndex();
      int y = canvas.getHeight() - 82;
      for (int index = 0; index != length; ++index)
      {
        int m_fontId1;
        int num2;
        if (index == selectBarIndex)
        {
          m_fontId1 = 7;
          num2 = 6;
        }
        else
        {
          num2 = 7;
          m_fontId1 = 6;
        }
        if (this.isStringsWrapped())
        {
          WrappedString itemWrappedString = this.getMenuItemWrappedString(index);
          string upper = textManager.getString(this.getItem(index)).ToUpper();
          if (upper.IndexOf(' ') < 0)
          {
            int font = 7;
            int left = 0;
            int top = 0;
            int width = 0;
            int height = 0;
            quadManager.getMeshBounds(this.m_buttonMeshId, ref left, ref top, ref width, ref height);
            if (textManager.getStringWidth(upper, font) >= width)
            {
              font = 15;
              if (textManager.getStringWidth(upper, font) >= width)
              {
                font = 18;
                if (textManager.getStringWidth(upper, font) >= width)
                  font = 5;
              }
            }
            StringRenderer stringRenderer = textManager.getStringRenderer(font);
            int color = stringRenderer.getColor();
            stringRenderer.setColor(index == selectBarIndex ? 8421504 : 0);
            textManager.drawString(g, upper, font, x + 1, y + 1, 17);
            stringRenderer.setColor(index == selectBarIndex ? 0 : 16777215);
            textManager.drawString(g, upper, font, x, y, 17);
            stringRenderer.setColor(color);
          }
          else
            itemWrappedString.drawWithFont(g, num2, x, y, 17, index == selectBarIndex, m_fontId1);
        }
        else
        {
          StringRenderer stringRenderer = textManager.getStringRenderer(num2);
          int color = stringRenderer.getColor();
          stringRenderer.setColor(index == selectBarIndex ? 8421504 : 0);
          textManager.drawString(g, this.getItem(index), num2, x + 1, y + 1, 17);
          stringRenderer.setColor(index == selectBarIndex ? 0 : 16777215);
          textManager.drawString(g, this.getItem(index), num2, x, y, 17);
          stringRenderer.setColor(color);
        }
        y += -53;
      }
    }

    public void pointerOn(int x, int y)
    {
      if (this.getLength() == 0)
      {
        this.m_itemIsSelected = true;
      }
      else
      {
        if (this.m_animState != MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE)
          return;
        int num1 = AppEngine.getCanvas().getHeight() - 82;
        int length = this.getLength();
        int num2 = 21;
        for (int newSelection = 0; newSelection != length; ++newSelection)
        {
          int num3 = num1 + newSelection * -53;
          if (num3 - num2 <= y && y <= num3 + num2)
          {
            this.animateToSelection(newSelection, false);
            break;
          }
        }
      }
    }

    public void pointerOff(int x, int y) => this.animateToSelection(this.getSelectedIndex(), false);

    public void pointerReleased(int x, int y)
    {
      if (this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE)
      {
        int num1 = AppEngine.getCanvas().getHeight() - 82;
        int length = this.getLength();
        int num2 = 21;
        for (int index = 0; index != length; ++index)
        {
          int y1 = num1 + index * -53;
          if (y1 - num2 <= y && y <= y1 + num2)
          {
            if (this.m_selectionAnimationCurItem == index)
            {
              this.setSelectedIndex(this.m_selectionAnimationCurItem);
              this.m_itemIsSelected = true;
              AppEngine canvas = AppEngine.getCanvas();
              QuadManager quadManager = canvas.getQuadManager();
              canvas.getWindowStore().getButtonEffect().play(quadManager.getMeshX(this.m_buttonMeshId), y1);
              break;
            }
            break;
          }
        }
      }
      this.m_selectionAnimationCurItem = -1;
    }

    public bool isAnimating()
    {
      return this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_RETRACTING || this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_EXPANDING;
    }

    public bool isActive() => this.m_animState == MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE;

    public void transitionToIdle()
    {
      if (this.m_animState != MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE)
        return;
      this.stateTransition(MenuMainSubMenu.AnimState.ANIM_STATE_RETRACTING);
    }

    public void transitionToActive()
    {
      if (this.m_animState != MenuMainSubMenu.AnimState.ANIM_STATE_IDLE)
        return;
      this.stateTransition(MenuMainSubMenu.AnimState.ANIM_STATE_EXPANDING);
    }

    public void setOpen(int subItem)
    {
      AppEngine.getCanvas().getQuadManager().setAnimFrame(this.m_ribbonAnimId, 1);
      this.m_xOffsetFilter.setTargetValue(0.0f);
      this.m_itemIsSelected = false;
      this.stateTransition(MenuMainSubMenu.AnimState.ANIM_STATE_ACTIVE);
      this.animateToSelection(subItem, true);
    }

    public void animateToSelection(int newSelection, bool snap)
    {
      if (this.m_selectionAnimationNextItem != -1 || !this.m_selectInterpolation.isFinished())
        return;
      if (snap)
      {
        this.m_selectInterpolation.stop();
        this.m_selectionAnimationNextItem = -1;
        this.setSelectedIndex(newSelection);
      }
      else if (this.m_selectInterpolation.isFinished())
      {
        if (newSelection == this.m_selectionAnimationCurItem)
          return;
        int num = AppEngine.getCanvas().getHeight() - 82;
        this.m_selectInterpolation.start((float) (num + this.getSelectBarIndex() * -53), (float) (num + newSelection * -53), 500, InterpolationTimed.InterpolationType.INTERPOLATION_TYPE_CUBIC_AHEAD);
        this.m_selectionAnimationCurItem = newSelection;
      }
      else if (newSelection == this.m_selectionAnimationCurItem)
        this.m_selectionAnimationNextItem = -1;
      else
        this.m_selectionAnimationNextItem = newSelection;
    }

    private int getSelectBarIndex()
    {
      return this.m_selectionAnimationCurItem != -1 ? this.m_selectionAnimationCurItem : this.getSelectedIndex();
    }

    public enum AnimState
    {
      ANIM_STATE_IDLE,
      ANIM_STATE_EXPANDING,
      ANIM_STATE_ACTIVE,
      ANIM_STATE_RETRACTING,
    }
  }
}
