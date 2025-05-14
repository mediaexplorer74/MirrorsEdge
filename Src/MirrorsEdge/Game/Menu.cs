
// Type: game.Menu
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;
using GameManager;
using support;
using System;

#nullable disable
namespace game
{
  public abstract class Menu
  {
    private int m_buttonPressed;
    protected MenuMainSubMenu[] m_subMenuArray;
    protected int m_selectionIndex;

    public Menu()
    {
      this.m_subMenuArray = new MenuMainSubMenu[0];
      this.m_selectionIndex = -1;
      this.m_buttonPressed = -1;
    }

    public virtual void Destructor()
    {
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
      {
        this.m_subMenuArray[index].Destructor();
        this.m_subMenuArray[index] = (MenuMainSubMenu) null;
      }
      this.m_subMenuArray = (MenuMainSubMenu[]) null;
    }

    public void reset()
    {
      this.m_selectionIndex = -1;
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
        this.m_subMenuArray[index].stateTransition(MenuMainSubMenu.AnimState.ANIM_STATE_IDLE);
    }

    public int getSelectionIndex() => this.m_selectionIndex;

    public void setSelectionIndex(int selectionIndex) => this.m_selectionIndex = selectionIndex;

    public bool isItemSelected()
    {
      return this.m_selectionIndex != -1 && this.m_subMenuArray[this.m_selectionIndex].isSelected();
    }

    public abstract void create();

    public bool isCreated() => this.m_subMenuArray != null && this.m_subMenuArray.Length > 0;

    public MenuMainSubMenu getSubMenu(int index) => this.m_subMenuArray[index];

    public MenuMainSubMenu getSelectedSubMenu()
    {
      return this.m_selectionIndex >= 0 ? this.m_subMenuArray[this.m_selectionIndex] : (MenuMainSubMenu) null;
    }

    public virtual void update(int timeStepMillis)
    {
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
        this.m_subMenuArray[index].update(timeStepMillis);
    }

    public virtual void render(Graphics g)
    {
      AppEngine.getCanvas().getQuadManager().render(g, 128);
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
        this.m_subMenuArray[index].render(g);
    }

    public virtual bool OnHardBackKeyEvent()
    {
      if (this.isAnySubMenuAnimating())
        return false;
      if (this.m_selectionIndex == -1 || !this.m_subMenuArray[this.m_selectionIndex].isActive())
        return true;
      int x;
      int y;
      AppEngine.getCanvas().getQuadManager().getCenterPointWithinMesh(this.m_subMenuArray[this.m_selectionIndex].getButtonMeshId(), out x, out y);
      this.pointerPressed(x, y);
      this.pointerReleased(x, y);
      return false;
    }

    public virtual void pointerPressed(int x, int y)
    {
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      int selectionIndex = this.m_selectionIndex;
      int index = Math.Min(x / (canvas.getWidth() / 4), this.m_subMenuArray.Length - 1);
      bool meshVisible = quadManager.getMeshVisible(this.m_subMenuArray[index].getButtonMeshId());
      quadManager.setMeshVisible(this.m_subMenuArray[index].getButtonMeshId(), true);
      bool flag = quadManager.isPointWithinMesh(this.m_subMenuArray[index].getButtonMeshId(), x, y);
      quadManager.setMeshVisible(this.m_subMenuArray[index].getButtonMeshId(), meshVisible);
      if (flag)
        this.m_buttonPressed = index;
      if (selectionIndex != index)
        return;
      this.m_subMenuArray[index].pointerOn(x, y);
    }

    public virtual void pointerDragged(int x, int y)
    {
      AppEngine canvas = AppEngine.getCanvas();
      int selectionIndex = this.m_selectionIndex;
      int index = Math.Min(x / (canvas.getWidth() / 4), this.m_subMenuArray.Length - 1);
      if (selectionIndex == index)
        this.m_subMenuArray[index].pointerOn(x, y);
      else
        this.m_subMenuArray[index].pointerOff(x, y);
    }

    public virtual bool pointerReleased(int x, int y)
    {
      if (this.isAnySubMenuAnimating())
        return false;
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      int selectionIndex = this.m_selectionIndex;
      int idx = Math.Min(x / (canvas.getWidth() / 4), this.m_subMenuArray.Length - 1);
      bool meshVisible = quadManager.getMeshVisible(this.m_subMenuArray[idx].getButtonMeshId());
      quadManager.setMeshVisible(this.m_subMenuArray[idx].getButtonMeshId(), true);
      bool flag = quadManager.isPointWithinMesh(this.m_subMenuArray[idx].getButtonMeshId(), x, y);
      quadManager.setMeshVisible(this.m_subMenuArray[idx].getButtonMeshId(), meshVisible);
      float num = 1f;
      if (MirrorsEdge.TrialMode)
        num = quadManager.getMeshAlpha(this.m_subMenuArray[idx].getButtonMeshId());
      if ((double) num == 1.0 && flag && this.m_buttonPressed == idx)
      {
        this.m_buttonPressed = -1;
        this.activateSubMenu(idx);
        int meshX = quadManager.getMeshX(this.m_subMenuArray[idx].getButtonMeshId());
        int meshY = quadManager.getMeshY(this.m_subMenuArray[idx].getButtonMeshId());
        AppEngine.getCanvas().getWindowStore().getButtonEffect().play(meshX, meshY);
        return true;
      }
      if (selectionIndex == idx)
        this.m_subMenuArray[idx].pointerReleased(x, y);
      this.m_buttonPressed = -1;
      return false;
    }

    public void wrapStrings(bool allCaps)
    {
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
        this.m_subMenuArray[index].wrapStrings(6, 118, allCaps);
    }

    public bool isAnySubMenuAnimating()
    {
      for (int index = 0; index < this.m_subMenuArray.Length; ++index)
      {
        if (this.m_subMenuArray[index].isAnimating())
          return true;
      }
      return false;
    }

    public virtual void activateSubMenu(int idx) => this.activateSubMenu(idx, -1);

    public virtual void activateSubMenu(int idx, int subItem)
    {
      this.m_selectionIndex = idx;
      for (int index = 0; index != this.m_subMenuArray.Length; ++index)
      {
        if (index == this.m_selectionIndex && this.m_subMenuArray[index].getLength() > 0 && !this.m_subMenuArray[index].isActive())
          this.m_subMenuArray[index].transitionToActive();
        else
          this.m_subMenuArray[index].transitionToIdle();
      }
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

    public void setSubMenuOpen(int idx) => this.setSubMenuOpen(idx, -1);

    public void setSubMenuOpen(int idx, int subItem)
    {
      this.m_selectionIndex = idx;
      this.m_subMenuArray[idx].setOpen(subItem);
    }
  }
}
