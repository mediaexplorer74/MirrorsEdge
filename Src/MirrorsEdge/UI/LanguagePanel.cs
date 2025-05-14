
// Type: UI.LanguagePanel
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using text;

#nullable disable
namespace UI
{
  public class LanguagePanel : NotchedSlider
  {
    public const int WIDTH = 246;
    public const int HEIGHT = 62;
    public const int RENDER_EXTRA = 2;

    public LanguagePanel()
    {
      this.setWidth(246);
      this.setHeight(62);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int languageCount = textManager.getLanguageCount();
      int width = 0;
      for (int index = 0; index < languageCount; ++index)
      {
        string langString = textManager.getLangString(index);
        LanguageItem newItem = new LanguageItem(index, langString);
        if (newItem.getWidth() > width)
          width = newItem.getWidth();
        this.addItem((WindowElement) newItem);
      }
      this.setNotchWidth(width);
      this.setRenderExtra(2);
      this.m_offset = (float) (width * textManager.getCurrentLanguage() + (width >> 1));
    }
  }
}
