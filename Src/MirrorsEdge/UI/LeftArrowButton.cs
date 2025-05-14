// Decompiled with JetBrains decompiler
// Type: UI.LeftArrowButton
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using support;

#nullable disable
namespace UI
{
  public class LeftArrowButton : Button
  {
    public LeftArrowButton()
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      this.setWidth(quadManager.getMeshWidth((int) QuadManager.get("MESH_ARROW_LEFT_ACTIVE")));
      this.setHeight(quadManager.getMeshHeight((int) QuadManager.get("MESH_ARROW_LEFT_ACTIVE")));
    }

    public override void render(Graphics g, int top, int left)
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      int meshIndex = this.m_enabled ? (int) QuadManager.get("MESH_ARROW_LEFT_ACTIVE") : (int) QuadManager.get("MESH_ARROW_LEFT_DISABLED");
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_ARROWS"), true);
      quadManager.setMeshVisible(meshIndex, true);
      quadManager.setMeshBounds(meshIndex, (float) (left + this.m_x), (float) (top + this.m_y), (float) this.m_width, (float) this.m_height, 9);
      quadManager.render(g, 2);
      quadManager.setMeshVisible(meshIndex, false);
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_ARROWS"), false);
    }
  }
}
