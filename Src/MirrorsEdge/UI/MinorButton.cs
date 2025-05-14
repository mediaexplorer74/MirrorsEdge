
// Type: UI.MinorButton
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using midp;
using support;
using text;

#nullable disable
namespace UI
{
  public class MinorButton : Button
  {
    public MinorButton(int stringId)
      : base(stringId)
    {
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int meshWidth = this.m_quadManager.getMeshWidth((int) QuadManager.get("MESH_WINDOW_BUTTON_MINOR"));
      int meshHeight = this.m_quadManager.getMeshHeight((int) QuadManager.get("MESH_WINDOW_BUTTON_MINOR"));
      int stringWidth = textManager.getStringWidth(this.m_stringId, this.m_fontId);
      if (stringWidth > meshWidth)
        this.setWidth(stringWidth);
      else
        this.setWidth(meshWidth);
      this.setHeight(meshHeight);
    }

    public override void render(Graphics g, int top, int left)
    {
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BUTTON_MINOR"), true);
      int meshWidth = this.m_quadManager.getMeshWidth((int) QuadManager.get("MESH_WINDOW_BUTTON_MINOR"));
      int meshHeight = this.m_quadManager.getMeshHeight((int) QuadManager.get("MESH_WINDOW_BUTTON_MINOR"));
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_BUTTON_MINOR"), (float) (left + this.m_x), (float) (top + this.m_y), (float) this.m_width, (float) this.m_height, 9);
      if (this.m_enabled)
        this.m_quadManager.setMeshAlpha((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"), 1f);
      else
        this.m_quadManager.setMeshAlpha((int) QuadManager.get("MESH_WINDOW_BUTTON_MAJOR"), 0.3f);
      this.m_quadManager.render(g, 2);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int font = this.m_pressed ? this.m_highlightFontId : this.m_fontId;
      textManager.drawString(g, this.m_stringId, font, left + this.m_x + (this.m_width >> 1), top + this.m_y + (this.m_height >> 1), 18);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_WINDOW_BUTTON_MINOR"), false);
      this.m_quadManager.setMeshBounds((int) QuadManager.get("MESH_WINDOW_BUTTON_MINOR"), 0.0f, 0.0f, (float) meshWidth, (float) meshHeight, 9);
    }
  }
}
