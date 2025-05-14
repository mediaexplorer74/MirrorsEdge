
// Type: UI.NetworkWaitEffect
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using support;

#nullable disable
namespace UI
{
  public class NetworkWaitEffect
  {
    private int m_x;
    private int m_y;
    private bool m_playing;

    public NetworkWaitEffect()
    {
      this.m_x = 0;
      this.m_y = 0;
      this.m_playing = false;
    }

    public void play(int x, int y)
    {
      QuadManager quadManager = AppEngine.getCanvas().getQuadManager();
      quadManager.setGroupVisible((int) QuadManager.get("GROUP_NETWORK_WAIT_EFFECT"), true);
      quadManager.setGroupPosition((int) QuadManager.get("GROUP_NETWORK_WAIT_EFFECT"), (float) x, (float) y);
      quadManager.playAnim((int) QuadManager.get("ANIM_NETWORK_WAIT_EFFECT"), 1);
      this.m_x = x;
      this.m_y = y;
      this.m_playing = true;
    }

    public void stop()
    {
      AppEngine.getCanvas().getQuadManager().setGroupVisible((int) QuadManager.get("GROUP_NETWORK_WAIT_EFFECT"), false);
      AppEngine.getCanvas().getWindowStore().getButtonEffect().play(this.m_x, this.m_y);
      this.m_playing = false;
    }

    public void update(int timeStep)
    {
      AppEngine.getCanvas().getQuadManager().updateAnim((int) QuadManager.get("ANIM_NETWORK_WAIT_EFFECT"), timeStep);
    }

    public bool isAnimating() => this.m_playing;
  }
}
