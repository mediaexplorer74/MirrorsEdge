
// Type: UI.SoundOptions
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using game;
using generic;
using midp;
using GameManager;
using support;
using text;

#nullable disable
namespace UI
{
  public class SoundOptions : TitledWindow
  {
    public const int BACKGROUND_X = 50;
    public const int BACKGROUND_Y = 86;
    public const int SLIDER_X = 160;
    public const int SLIDER_SPACING = 60;
    public const int SLIDER_WIDTH = 220;
    public new const int BUTTON_X_PADDING = 10;
    public new const int BUTTON_Y_PADDING = 20;
    public const int LABEL_X_PADDING = 10;
    public const int ICON_X_PADDING = 15;
    public int LABEL_FONT = 6;
    private HorizontalSliderElement m_musicLevel;
    private HorizontalSliderElement m_soundLevel;

    public SoundOptions()
      : base(2263, 2078)
    {
      this.m_musicLevel = new HorizontalSliderElement(220);
      this.m_soundLevel = new HorizontalSliderElement(220);
      int width1 = AppEngine.getCanvas().getWidth();
      int height1 = AppEngine.getCanvas().getHeight();
      int width2 = width1 - 100;
      int height2 = height1 - 172;
      this.m_backgroundBorder.setPosition(50, 86);
      this.m_backgroundBorder.setDimensions(width2, height2);
      if (MirrorsEdge.externalMusic)
      {
        int y = (height1 >> 1) - 20;
        this.m_musicLevel.setPosition(-500, -500);
        this.m_soundLevel.setPosition(160, y);
      }
      else
      {
        int y1 = (height1 >> 1) - 30 - 20;
        int y2 = (height1 >> 1) + 30 - 20;
        this.m_musicLevel.setPosition(160, y1);
        this.m_soundLevel.setPosition(160, y2);
      }
      SoundManager soundManager = AppEngine.getCanvas().getSoundManager();
      this.m_musicLevel.setValue(soundManager.getVolumeMusic());
      this.m_soundLevel.setValue(soundManager.getVolumeSFX());
    }

    public override void Destructor()
    {
      this.m_musicLevel.Destructor();
      this.m_musicLevel = (HorizontalSliderElement) null;
      this.m_soundLevel.Destructor();
      this.m_soundLevel = (HorizontalSliderElement) null;
      base.Destructor();
    }

    public override void update(int timeStep)
    {
      base.update(timeStep);
      this.m_musicLevel.update(timeStep);
      this.m_soundLevel.update(timeStep);
    }

    public override void render(Graphics g, int top, int left)
    {
      base.render(g, top, left);
      TextManager textManager = AppEngine.getCanvas().getTextManager();
      textManager.drawString(g, 2264, this.LABEL_FONT, 60, this.m_musicLevel.getY() + 20, 17);
      textManager.drawString(g, 2265, this.LABEL_FONT, 60, this.m_soundLevel.getY() + 20, 17);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_SLIDER"), true);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_SLIDER_ICON_OFF"), true);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_SLIDER_ICON_ON"), true);
      this.m_quadManager.setMeshPosition((int) QuadManager.get("MESH_SLIDER_ICON_OFF"), (float) (this.m_musicLevel.getX() - 15), (float) (this.m_musicLevel.getY() + 20), 20);
      this.m_quadManager.setMeshPosition((int) QuadManager.get("MESH_SLIDER_ICON_ON"), (float) (this.m_musicLevel.getX() + this.m_musicLevel.getWidth() + 15), (float) (this.m_musicLevel.getY() + 20), 17);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setMeshPosition((int) QuadManager.get("MESH_SLIDER_ICON_OFF"), (float) (this.m_soundLevel.getX() - 15), (float) (this.m_soundLevel.getY() + 20), 20);
      this.m_quadManager.setMeshPosition((int) QuadManager.get("MESH_SLIDER_ICON_ON"), (float) (this.m_soundLevel.getX() + this.m_soundLevel.getWidth() + 15), (float) (this.m_soundLevel.getY() + 20), 17);
      this.m_quadManager.render(g, 2);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_SLIDER_ICON_ON"), false);
      this.m_quadManager.setMeshVisible((int) QuadManager.get("MESH_SLIDER_ICON_OFF"), false);
      this.m_quadManager.setGroupVisible((int) QuadManager.get("GROUP_SLIDER"), false);
      this.m_musicLevel.render(g, top, left);
      this.m_soundLevel.render(g, top, left);
    }

    public override bool pointerPressed(int x, int y, int pointerNum)
    {
      base.pointerPressed(x, y, pointerNum);
      if (pointerNum > 0)
        return false;
      if (this.m_musicLevel.contains(x, y))
        return this.m_musicLevel.pointerPressed(this.m_musicLevel.toRelativeX(x), this.m_musicLevel.toRelativeY(y), pointerNum);
      return this.m_soundLevel.contains(x, y) && this.m_soundLevel.pointerPressed(this.m_soundLevel.toRelativeX(x), this.m_soundLevel.toRelativeY(y), pointerNum);
    }

    public override bool pointerReleased(int x, int y, int pointerNum)
    {
      base.pointerReleased(x, y, pointerNum);
      if (pointerNum > 0)
        return false;
      if (this.m_musicLevel.contains(x, y))
        return this.m_musicLevel.pointerReleased(this.m_musicLevel.toRelativeX(x), this.m_musicLevel.toRelativeY(y), pointerNum);
      return this.m_soundLevel.contains(x, y) && this.m_soundLevel.pointerReleased(this.m_soundLevel.toRelativeX(x), this.m_soundLevel.toRelativeY(y), pointerNum);
    }

    public override bool pointerDragged(int x, int y, int pointerNum)
    {
      if (pointerNum > 0)
        return false;
      if (this.m_musicLevel.contains(x, y))
        return this.m_musicLevel.pointerDragged(this.m_musicLevel.toRelativeX(x), this.m_musicLevel.toRelativeY(y), pointerNum);
      return this.m_soundLevel.contains(x, y) && this.m_soundLevel.pointerDragged(this.m_soundLevel.toRelativeX(x), this.m_soundLevel.toRelativeY(y), pointerNum);
    }

    public float getMusicLevel() => this.m_musicLevel.getValue();

    public float getEffectsLevel() => this.m_soundLevel.getValue();
  }
}
