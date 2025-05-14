
// Type: game.SceneGamePointer
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9


#nullable disable
namespace game
{
  public class SceneGamePointer
  {
    public const int POINTER_STOP_GESTURE_TIME = 500;
    public const int POINTER_NONE = -1;
    public int pointerIndex;
    public int pressX;
    public int pressY;
    public int pressTime;
    public bool swiped;
    public int swipeGestureMade;

    public SceneGamePointer()
    {
      this.pointerIndex = -1;
      this.pressX = 0;
      this.pressY = 0;
      this.pressTime = 0;
      this.swiped = false;
      this.swipeGestureMade = 0;
    }

    public bool isPressed() => this.pointerIndex != -1;

    public static bool operator ==(SceneGamePointer ImpliedObject, int rhs)
    {
      return ImpliedObject.pointerIndex == rhs;
    }

    public static bool operator !=(SceneGamePointer ImpliedObject, int rhs)
    {
      return ImpliedObject.pointerIndex != rhs;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool Equals(object obj) => base.Equals(obj);

    public void press(int index, int x, int y)
    {
      this.pointerIndex = index;
      this.pressX = x;
      this.pressY = y;
      this.pressTime = 0;
      this.swiped = false;
      this.swipeGestureMade = 0;
    }

    public void swipe(int gestureMade, int x, int y)
    {
      this.swiped = true;
      this.swipeGestureMade = gestureMade;
      this.pressX = x;
      this.pressY = y;
    }

    public void release() => this.pointerIndex = -1;

    public bool checkHold(int timeStepMillis)
    {
      if (this.pointerIndex == -1 || this.swipeGestureMade != 0)
        return false;
      int pressTime = this.pressTime;
      this.pressTime += timeStepMillis;
      return pressTime < 500 && this.pressTime >= 500;
    }
  }
}
