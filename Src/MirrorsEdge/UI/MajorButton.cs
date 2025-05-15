
// Type: UI.MajorButton
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;
using support;
using text;

#nullable disable
namespace UI
{
  public class MajorButton : Button
  {
    public const int TEXT_X_ADJUST = -3;
    public const int TEXT_Y_ADJUST = -1;
    private int m_langId;
    private int m_sfx;
    private string m_upperStr;

    public MajorButton(int stringId)
      : this(stringId, (int) ResourceManager.get("SOUNDEVENT_SFX_UI_POSITIVE"))
    {
    }

    public MajorButton(int stringId, int sfxId)
      : base(stringId)
    {
      this.m_upperStr = (string) null;
      this.m_sfx = sfxId;
      this.m_langId = 0;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      this.m_langId = textManager.getCurrentLanguage();
      if (stringId != -1)
      {
        this.m_upperStr = textManager.getString(this.m_stringId).ToUpper();
        int meshWidth = this.m_quadManager.getMeshWidth((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"));
        int meshHeight = this.m_quadManager.getMeshHeight((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"));
        int width = textManager.getStringWidth(this.m_stringId.ToString(), this.m_fontId) + 40;
        if (width > meshWidth)
          this.setWidth(width);
        else
          this.setWidth(meshWidth);
        this.setHeight(meshHeight);
      }
      else
      {
        int meshWidth = this.m_quadManager.getMeshWidth((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"));
        int meshHeight = this.m_quadManager.getMeshHeight((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"));
        this.setWidth(meshWidth);
        this.setHeight(meshHeight);
      }
    }

    public override void Destructor()
    {
      this.m_upperStr = (string) null;
      base.Destructor();
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      if (this.m_enabled)
      {
        SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
        if (soundManager.isEventPlaying(this.m_sfx))
          soundManager.stopEvent(this.m_sfx);
        soundManager.playEvent(this.m_sfx);
      }
      return base.pointerReleased(x, y, pointerNum);
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BUTTON_MAJOR"), true);
      int meshWidth = this.m_quadManager.getMeshWidth((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"));
      int meshHeight = this.m_quadManager.getMeshHeight((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"));
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"), (float) (left + this.m_x), (float) (top + this.m_y), (float) this.m_width, (float) this.m_height, 9);
      if (this.m_enabled)
        this.m_quadManager.setMeshAlpha((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"), 1f);
      else
        this.m_quadManager.setMeshAlpha((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"), 0.3f);
      this.m_quadManager.render(g, 2);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      if (textManager.getCurrentLanguage() != this.m_langId)
      {
        this.m_langId = textManager.getCurrentLanguage();
        this.m_upperStr = textManager.getString(this.m_stringId).ToUpper();
      }
      int font1 = this.m_pressed ? this.m_highlightFontId : this.m_fontId;
      int font2 = this.m_pressed ? this.m_fontId : this.m_highlightFontId;
      if (font1 != font2)
      {
        textManager.drawString(g, this.m_upperStr, font2, left + this.m_x + (this.m_width >> 1) - 3 + 1, top + this.m_y + (this.m_height >> 1) - 1 + 1, 18);
        textManager.drawString(g, this.m_upperStr, font1, left + this.m_x + (this.m_width >> 1) - 3, top + this.m_y + (this.m_height >> 1) - 1, 18);
      }
      else
        textManager.drawString(g, this.m_upperStr, font1, left + this.m_x + (this.m_width >> 1) - 3, top + this.m_y + (this.m_height >> 1) - 1, 18);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BUTTON_MAJOR"), false);
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"), 0.0f, 0.0f, (float) meshWidth, (float) meshHeight, 9);
    }
  }
}
