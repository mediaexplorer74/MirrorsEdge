
// Type: game.Scene
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


using midp;

#nullable disable
namespace game
{
  public abstract class Scene : Runnable
  {
    public const int SCENE_NONE = 0;
    public const int SCENE_STARTUP = 1;
    public const int SCENE_MENU = 2;
    public const int SCENE_GAME = 3;
    public const int SCENE_CHEAT = 4;
    public const int COMMAND_NONE = 0;
    public const int COMMAND_BACK = 1;
    public const int COMMAND_NEXT = 2;
    public const int COMMAND_HOME = 4;
    public const int COMMAND_PURCHASE = 8;
    public const int COMMAND_LEAD = 16;
    protected AppEngine m_engine;
    protected int m_loadingProgress;

    public override meClass getClass() => (meClass) new SceneClass();

    public Scene(AppEngine ae)
    {
      this.m_engine = ae;
      this.m_loadingProgress = 0;
    }

    public override void Destructor() => base.Destructor();

    public abstract void start(int initialState);

    public abstract void pause();

    public abstract void resume();

    public abstract void end();

    public abstract void render(Graphics g);

    public abstract void OnHardBackKeyEvent();

    public abstract void pointerDragged(int x, int y, int pointerNum);

    public abstract void pointerPressed(int x, int y, int pointerNum);

    public abstract void pointerReleased(int x, int y, int pointerNum);

    public abstract void update(int timeStep);
  }
}
