
// Type: midp.Canvas
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace midp
{
  public abstract class Canvas : Displayable
  {
    public const int DOWN = 6;
    public const int FIRE = 8;
    public const int GAME_A = 9;
    public const int GAME_B = 10;
    public const int GAME_C = 11;
    public const int GAME_D = 12;
    public const int KEY_NUM0 = 48;
    public const int KEY_NUM1 = 49;
    public const int KEY_NUM2 = 50;
    public const int KEY_NUM3 = 51;
    public const int KEY_NUM4 = 52;
    public const int KEY_NUM5 = 53;
    public const int KEY_NUM6 = 54;
    public const int KEY_NUM7 = 55;
    public const int KEY_NUM8 = 56;
    public const int KEY_NUM9 = 57;
    public const int KEY_POUND = 35;
    public const int KEY_STAR = 42;
    public const int LEFT = 2;
    public const int RIGHT = 5;
    public const int UP = 1;

    public override void Destructor() => base.Destructor();

    public override meClass getClass() => (meClass) new CanvasClass();

    public virtual int getGameAction(int keyCode)
    {
      switch (keyCode)
      {
        case 1:
        case 2:
        case 5:
        case 6:
        case 8:
        case 9:
        case 10:
        case 11:
        case 12:
          return keyCode;
        case 35:
        case 42:
        case 48:
        case 49:
        case 51:
        case 55:
        case 57:
          return 0;
        case 50:
          return 1;
        case 52:
          return 2;
        case 53:
          return 8;
        case 54:
          return 5;
        case 56:
          return 6;
        default:
          return 0;
      }
    }

    public virtual int getKeyCode(int gameAction)
    {
      switch (gameAction)
      {
        case 1:
          return 50;
        case 2:
          return 52;
        case 5:
          return 54;
        case 6:
          return 56;
        case 8:
        case 9:
        case 10:
        case 11:
        case 12:
          return 53;
        case 35:
        case 42:
        case 48:
        case 49:
        case 50:
        case 51:
        case 52:
        case 53:
        case 54:
        case 55:
        case 56:
        case 57:
          return gameAction;
        default:
          return 0;
      }
    }

    public virtual bool hasPointerEvents() => false;

    public virtual bool hasPointerMotionEvents() => false;

    public virtual bool hasRepeatEvents() => false;

    public override void hideNotify()
    {
    }

    public virtual bool isDoubleBuffered() => true;

    public virtual void setFullScreenMode(bool UnnamedParameter1)
    {
    }

    public override void showNotify()
    {
    }

    public override void sizeChanged(int w, int h) => base.sizeChanged(w, h);

    public override bool isCanvas() => true;

    public abstract void paint(Graphics g);
  }
}
