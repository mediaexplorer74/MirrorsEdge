// Decompiled with JetBrains decompiler
// Type: UI.StoryEndOfLevelPrompt
// Assembly: mirrorsedge_wp7, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9
// Assembly location: C:\Users\Admin\Desktop\RE\MirrorsEdge1_1\mirrorsedge_wp7.dll

using game;
using midp;
using support;
using text;

#nullable disable
namespace UI
{
  public class StoryEndOfLevelPrompt : EndOfLevelPrompt
  {
    protected const int BAG_X_POS = 15;
    protected const int COUNT_X_ADJUST = -4;
    protected const int COUNT_Y_ADJUST = 4;
    protected string m_bagString;
    protected int LEVEL_COMPLETE_BAG_COUNT_FONT = 28;

    public StoryEndOfLevelPrompt()
    {
      this.m_bagString = (string) null;
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      Level currentLevelObject = AppEngine.getLevelData().getCurrentLevelObject();
      string string0 = string.Concat((object) currentLevelObject.getNumBagsFound());
      string string1 = string.Concat((object) currentLevelObject.getNumTotalBags());
      textManager.dynamicString(-12, 2302, string0, string1);
      StringBuffer stringBuffer = textManager.clearStringBuffer();
      textManager.appendStringIdToBuffer(stringBuffer, -12);
      this.m_bagString = stringBuffer.toString();
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      int x = this.m_width - 15 - this.m_quadManager.getMeshWidth((int) QuadManager.get("MESH_LEVEL_COMPLETE_BAG_ICON"));
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_LEVEL_COMPLETE"), true);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_LEVEL_COMPLETE_BAG_ICON"), true);
      this.m_quadManager.setMeshPosition((int) QuadManager.get("MESH_LEVEL_COMPLETE_BAG_ICON"), (float) x, 15f, 9);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_LEVEL_COMPLETE_BAG_ICON"), false);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_LEVEL_COMPLETE"), false);
      StringRenderer stringRenderer = textManager.getStringRenderer(this.LEVEL_COMPLETE_BAG_COUNT_FONT);
      int color = stringRenderer.getColor();
      stringRenderer.setColor(0);
      textManager.drawString(g, this.m_bagString, this.LEVEL_COMPLETE_BAG_COUNT_FONT, x - 4 + 1, 20, 12);
      stringRenderer.setColor(color);
      textManager.drawString(g, this.m_bagString, this.LEVEL_COMPLETE_BAG_COUNT_FONT, x - 4, 19, 12);
    }

    public override void UnHide()
    {
    }
  }
}
