// Decompiled with JetBrains decompiler
// Type: UI.LanguageWindow
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using ea;
using game;
using midp;
using text;

#nullable disable
namespace UI
{
  public class LanguageWindow : TitledWindow
  {
    public const int BUTTON_PADDING = 10;
    private int m_currentLang;
    private LanguagePanel m_languagePanel;
    private LeftArrowButton m_prevButton;
    private RightArrowButton m_nextButton;

    public LanguageWindow()
      : base(2307, 2078)
    {
      this.m_currentLang = 0;
      this.m_languagePanel = new LanguagePanel();
      this.m_nextButton = new RightArrowButton();
      this.m_prevButton = new LeftArrowButton();
      this.m_backgroundBorder.setPosition(52, 93);
      this.m_backgroundBorder.setDimensions(433, 83);
      this.m_prevButton.setPosition(this.m_backgroundBorder.getX() + 10, this.m_backgroundBorder.getY() + (this.m_backgroundBorder.getHeight() - this.m_prevButton.getHeight() >> 1));
      this.m_nextButton.setPosition(this.m_backgroundBorder.getX() + this.m_backgroundBorder.getWidth() - this.m_prevButton.getWidth() - 10, this.m_backgroundBorder.getY() + (this.m_backgroundBorder.getHeight() - this.m_nextButton.getHeight() >> 1));
      this.m_languagePanel.setPosition(this.m_width - this.m_languagePanel.getWidth() >> 1, this.m_backgroundBorder.getY() + (this.m_backgroundBorder.getHeight() - this.m_languagePanel.getHeight() >> 1));
      this.m_currentLang = AppEngine.getCanvas().getTextManager().getCurrentLanguage();
    }

    public override void Destructor()
    {
      this.m_languagePanel.Destructor();
      this.m_languagePanel = (LanguagePanel) null;
      this.m_nextButton.Destructor();
      this.m_nextButton = (RightArrowButton) null;
      this.m_prevButton.Destructor();
      this.m_prevButton = (LeftArrowButton) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      this.m_languagePanel.update(timeStep);
      this.m_currentLang = (this.m_languagePanel.getSelectedItem() as LanguageItem).getLangId();
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      if (this.m_closed || this.m_currentLang != textManager.getCurrentLanguage())
      {
        textManager.setCurrentLanguage(this.m_currentLang);
        this.setTitles(2307, 2078);
      }
      if (!this.m_closed)
        return;
      EASpywareManager instance = EASpywareManager.getInstance();
      instance.logEvent(50012);
      instance.setLanguage(textManager.getCurrentLocale());
      instance.retryTickers();
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      this.m_languagePanel.render(g, top, left);
      this.m_nextButton.render(g, top, left);
      this.m_prevButton.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      base.pointerPressed(x, y, pointerNum);
      if (this.m_languagePanel.contains(x, y))
        this.m_languagePanel.pointerPressed(this.m_languagePanel.toRelativeX(x), this.m_languagePanel.toRelativeY(y), pointerNum);
      return true;
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      base.pointerReleased(x, y, pointerNum);
      if (this.m_languagePanel.contains(x, y))
        this.m_languagePanel.pointerReleased(this.m_languagePanel.toRelativeX(x), this.m_languagePanel.toRelativeY(y), pointerNum);
      if (this.m_nextButton.contains(x, y))
      {
        this.m_languagePanel.next();
        this.m_nextButton.pointerReleased(x, y, pointerNum);
      }
      else if (this.m_prevButton.contains(x, y))
      {
        this.m_languagePanel.prev();
        this.m_prevButton.pointerReleased(x, y, pointerNum);
      }
      return true;
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      base.pointerDragged(x, y, pointerNum);
      if (this.m_languagePanel.contains(x, y))
        this.m_languagePanel.pointerDragged(this.m_languagePanel.toRelativeX(x), this.m_languagePanel.toRelativeY(y), pointerNum);
      else if (this.m_languagePanel.isDragging())
        this.m_languagePanel.pointerReleased(this.m_languagePanel.toRelativeX(x), this.m_languagePanel.toRelativeY(y), pointerNum);
      return true;
    }
  }
}
