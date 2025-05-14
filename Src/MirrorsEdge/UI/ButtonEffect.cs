
// Type: UI.ButtonEffect
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using support;

#nullable disable
namespace UI
{
  public class ButtonEffect
  {
    private bool m_animating;

    public ButtonEffect() => this.m_animating = false;

    public void play(int x, int y)
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      quadManager.setGroupPosition((int) QuadManager.get("GROUP_BUTTON_EFFECT"), (float) x, (float) y);
      quadManager.playAnim((int) QuadManager.get("ANIM_BUTTON_EFFECT"), 2);
      this.m_animating = true;
    }

    public void update(int timeStep)
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      if (this.m_animating && quadManager.isAnimating((int) QuadManager.get("ANIM_BUTTON_EFFECT")))
      {
        quadManager.updateAnim((int) QuadManager.get("ANIM_BUTTON_EFFECT"), timeStep);
        quadManager.setGroupVisible((int) QuadManager.get("GROUP_BUTTON_EFFECT"), true);
      }
      else
      {
        if (!this.m_animating)
          return;
        quadManager.setGroupVisible((int) QuadManager.get("GROUP_BUTTON_EFFECT"), false);
        this.m_animating = false;
      }
    }

    public bool isAnimating() => this.m_animating;
  }
}
