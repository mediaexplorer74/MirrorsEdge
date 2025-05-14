
// Type: UI.UnlockableWindow
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;
using support;

#nullable disable
namespace UI
{
  public class UnlockableWindow : TitledWindow
  {
    public const string UNLOCKABLES_FILENAME = "res/unlockables.bin";
    public const int BACKGROUND_WIDTH = 460;
    public const int BACKGROUND_HEIGHT = 244;
    public const int DISPLAY_WINDOW_Y = 30;
    public const int TRANSITION_TIME = 300;
    public int DESCRIPTION_FONT = 2;
    private UnlockableWindow.STATE m_state;
    private int m_transTime;
    private UnlockableItem m_displayItem;
    private UnlockablePanel m_unlockablePanel;
    private MajorButton m_saveButton;
    private LeftArrowButton m_prevButton;
    private RightArrowButton m_nextButton;

    public UnlockableWindow()
      : base(2305, 2083)
    {
      this.m_state = UnlockableWindow.STATE.SELECT;
      this.m_displayItem = (UnlockableItem) null;
      this.m_unlockablePanel = new UnlockablePanel(this);
      this.m_transTime = 0;
      this.m_saveButton = new MajorButton(2306);
      this.m_nextButton = new RightArrowButton();
      this.m_prevButton = new LeftArrowButton();
      this.m_backgroundBorder.setDimensions(460, 244);
      int x1 = this.m_width - 460 >> 1;
      int y1 = this.m_height - 244 >> 1;
      this.m_backgroundBorder.setPosition(x1, y1);
      this.m_unlockablePanel.setPosition(this.m_width - 363 >> 1, y1 + 30);
      this.m_saveButton.setPosition(this.m_backButton.getX() - this.m_saveButton.getWidth() - 5, this.m_backButton.getY());
      int y2 = this.m_unlockablePanel.getY() + (this.m_unlockablePanel.getHeight() - this.m_nextButton.getHeight() >> 1);
      int x2 = x1 + (this.m_unlockablePanel.getX() - x1 - this.m_prevButton.getWidth() >> 1);
      this.m_nextButton.setPosition(this.m_width - x2 - this.m_nextButton.getWidth(), y2);
      this.m_prevButton.setPosition(x2, y2);
      InputStream resourceAsStream = meClass.getResourceAsStream("res/unlockables.bin");
      if (resourceAsStream == null)
        return;
      DataInputStream dataInputStream = new DataInputStream(resourceAsStream);
      int num = dataInputStream.readInt();
      for (int index = 0; index < num; ++index)
      {
        int quadId = ResourceManager.UNLOCKABLE_QUAD_DATA[(int) dataInputStream.readShort()];
        int saveableId = ResourceManager.UNLOCKABLE_SAVEABLE_DATA[(int) dataInputStream.readShort()];
        int itemDesc = ResourceManager.UNLOCKABLE_STRING_DATA[(int) dataInputStream.readShort()];
        dataInputStream.readInt();
        int numBadges = index + 1;
        this.m_unlockablePanel.addItem((WindowElement) new UnlockableItem(quadId, saveableId, itemDesc, numBadges));
      }
      resourceAsStream.close();
    }

    public override void Destructor()
    {
      if (this.m_displayItem != null)
      {
        this.m_displayItem.Destructor();
        this.m_displayItem = (UnlockableItem) null;
      }
      this.m_unlockablePanel.Destructor();
      this.m_unlockablePanel = (UnlockablePanel) null;
      this.m_saveButton.Destructor();
      this.m_saveButton = (MajorButton) null;
      this.m_prevButton.Destructor();
      this.m_prevButton = (LeftArrowButton) null;
      this.m_nextButton.Destructor();
      this.m_nextButton = (RightArrowButton) null;
      base.Destructor();
    }

    public void displayUnlockable(UnlockableItem unlockable)
    {
      this.m_displayItem = unlockable;
      this.m_state = UnlockableWindow.STATE.TRANS_IN;
      this.m_transTime = 300;
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      if (this.m_saveButton.isActivated())
      {
        this.m_saveButton.reset();
        this.m_displayItem.saveToLibrary();
        AppEngine.getCanvas().startFadeIn(true);
      }
      switch (this.m_state)
      {
        case UnlockableWindow.STATE.SELECT:
          this.m_unlockablePanel.update(timeStep);
          break;
        case UnlockableWindow.STATE.TRANS_IN:
          this.m_transTime -= timeStep;
          if (this.m_transTime > 0)
            break;
          this.m_transTime = 0;
          this.m_state = UnlockableWindow.STATE.DISPLAY;
          break;
        case UnlockableWindow.STATE.TRANS_OUT:
          if (AppEngine.getCanvas().isFading())
            break;
          this.m_transTime -= timeStep;
          if (this.m_transTime > 0)
            break;
          this.m_transTime = 0;
          this.m_state = UnlockableWindow.STATE.SELECT;
          break;
      }
    }

    public override void render(Graphics g, int top, int left)
    {
      AppEngine canvas = AppEngine.getCanvas();
      QuadManager quadManager = canvas.getQuadManager();
      base.render(g, top, left);
      this.m_unlockablePanel.render(g, top, left);
      if (this.m_unlockablePanel.getSelectedItem() is UnlockableItem selectedItem && !selectedItem.isUnlocked())
        canvas.getTextManager().drawString(g, selectedItem.getDescription(), this.DESCRIPTION_FONT, this.m_width >> 1, 232, 18);
      this.m_nextButton.render(g, top, left);
      this.m_prevButton.render(g, top, left);
      switch (this.m_state)
      {
        case UnlockableWindow.STATE.TRANS_IN:
        case UnlockableWindow.STATE.DISPLAY:
        case UnlockableWindow.STATE.TRANS_OUT:
          float num1 = (float) this.m_transTime / 300f;
          float num2 = this.m_state == UnlockableWindow.STATE.TRANS_OUT ? num1 : 1f - num1;
          int thumbWidth = this.m_displayItem.getThumbWidth();
          int thumbHeight = this.m_displayItem.getThumbHeight();
          int thumbX = this.m_displayItem.getThumbX();
          int thumbY = this.m_displayItem.getThumbY();
          int w = (int) ((double) thumbWidth + (double) (this.m_width - thumbWidth) * (double) num2);
          int h = (int) ((double) thumbHeight + (double) (this.m_height - thumbHeight) * (double) num2);
          int x = (int) ((double) thumbX - (double) thumbX * (double) num2);
          int y = (int) ((double) thumbY - (double) thumbY * (double) num2);
          int quadId = this.m_displayItem.getQuadId();
          quadManager.setMeshBounds(quadId, (float) x, (float) y, (float) w, (float) h, 9);
          quadManager.setGroupVisible((int) QuadManager.get("GROUP_UNLOCKABLES"), true);
          quadManager.setMeshVisible(quadId, true);
          quadManager.render(g);
          quadManager.setMeshVisible(quadId, false);
          quadManager.setGroupVisible((int) QuadManager.get("GROUP_UNLOCKABLES"), false);
          if (canvas.isFading())
            break;
          this.m_backButton.render(g, top, left);
          if (this.m_state != UnlockableWindow.STATE.DISPLAY)
            break;
          this.m_saveButton.render(g, top, left);
          break;
      }
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      if (this.m_state == UnlockableWindow.STATE.SELECT)
      {
        base.pointerPressed(x, y, pointerNum);
        if (this.m_saveButton.contains(x, y))
          this.m_saveButton.pointerPressed(x, y, pointerNum);
        else if (this.m_unlockablePanel.contains(x, y))
        {
          this.m_unlockablePanel.pointerPressed(this.m_unlockablePanel.toRelativeX(x), this.m_unlockablePanel.toRelativeY(y), pointerNum);
          return true;
        }
      }
      return false;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      switch (this.m_state)
      {
        case UnlockableWindow.STATE.SELECT:
          base.pointerReleased(x, y, pointerNum);
          if (this.m_unlockablePanel.contains(x, y))
          {
            this.m_unlockablePanel.pointerReleased(this.m_unlockablePanel.toRelativeX(x), this.m_unlockablePanel.toRelativeY(y), pointerNum);
            return true;
          }
          if (this.m_nextButton.contains(x, y))
          {
            this.m_unlockablePanel.next();
            this.m_nextButton.pointerReleased(x, y, pointerNum);
            break;
          }
          if (this.m_prevButton.contains(x, y))
          {
            this.m_unlockablePanel.prev();
            this.m_prevButton.pointerReleased(x, y, pointerNum);
            break;
          }
          break;
        case UnlockableWindow.STATE.DISPLAY:
          if (this.m_saveButton.contains(x, y))
          {
            this.m_saveButton.pointerReleased(x, y, pointerNum);
            this.m_state = UnlockableWindow.STATE.TRANS_OUT;
            this.m_transTime = 300;
          }
          else if (this.m_backButton.contains(x, y))
          {
            this.m_state = UnlockableWindow.STATE.TRANS_OUT;
            this.m_transTime = 300;
          }
          return true;
      }
      return false;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (this.m_state == UnlockableWindow.STATE.SELECT)
      {
        base.pointerDragged(x, y, pointerNum);
        if (!this.m_saveButton.contains(x, y) && this.m_saveButton.isPressed())
        {
          this.m_saveButton.unpress();
        }
        else
        {
          if (this.m_unlockablePanel.contains(x, y))
          {
            this.m_unlockablePanel.pointerDragged(this.m_unlockablePanel.toRelativeX(x), this.m_unlockablePanel.toRelativeY(y), pointerNum);
            return true;
          }
          if (this.m_unlockablePanel.isDragging())
            this.m_unlockablePanel.pointerReleased(this.m_unlockablePanel.toRelativeX(x), this.m_unlockablePanel.toRelativeY(y), pointerNum);
        }
      }
      return false;
    }

    public enum STATE
    {
      SELECT,
      TRANS_IN,
      DISPLAY,
      TRANS_OUT,
    }
  }
}
