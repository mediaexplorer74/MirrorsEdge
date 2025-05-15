
// Type: support.WP7_TouchManager
// Assembly: MirrorsEdge, Version=1.1.25.0, Culture=neutral, PublicKeyToken=null
// MVID: AADE1522-6AC0-41D0-BFE0-4276CBF513F9

/*
 Explanation of Changes:
1.	Mouse Input Handling:
•	Added mouseBegan, mouseMoved, and mouseEnded methods to handle mouse input.
•	Used Mouse.GetState() to retrieve the current mouse state.
•	Used m_MousePressed to track whether the mouse button is currently pressed.
2.	Integration with Process:
•	Extended the Process method to handle mouse input in addition to touch input.
3.	Pointer ID for Mouse:
•	Used pointer ID 0 for mouse input to differentiate it from touch inputs.
This implementation ensures that both touch and mouse inputs are handled seamlessly.
 */

/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using midp;
using System.Collections.Generic;

#nullable disable
namespace support
{
  internal class WP7_TouchManager
  {
    private static List<WP7_TouchManager.TouchInfo> m_Touches;

    public static void Init()
    {
      WP7_TouchManager.m_Touches = new List<WP7_TouchManager.TouchInfo>(10);
    }

    public static WP7_TouchManager.TouchInfo findTouch(TouchLocation tl)
    {
      Vector2 position = tl.Position;
      Vector2 vector2;
      vector2.X = position.X;
      vector2.Y = position.Y;
      TouchLocation previousLocation;
      if (tl.TryGetPreviousLocation(out previousLocation))
      {
        vector2.X = previousLocation.Position.X;
        vector2.Y = previousLocation.Position.Y;
      }
      foreach (WP7_TouchManager.TouchInfo touch in WP7_TouchManager.m_Touches)
      {
        if ((double) vector2.X == (double) touch.x && (double) vector2.Y == (double) touch.y 
           || (double) position.X == (double) touch.x && (double) position.Y == (double) touch.y)
          return touch;
      }
      return (WP7_TouchManager.TouchInfo) null;
    }

    public static WP7_TouchManager.TouchInfo updateTouch(TouchLocation touch, bool addNewTouch)
    {
      Vector2 position = touch.Position;
      WP7_TouchManager.TouchInfo touch1 = WP7_TouchManager.findTouch(touch);
      if (touch1 != null)
      {
        touch1.x = (int) position.X;
        touch1.y = (int) position.Y;
        return touch1;
      }
      if (!addNewTouch)
        return (WP7_TouchManager.TouchInfo) null;
      int pointer_ = 0;
      while (true)
      {
        bool flag = false;
        foreach (WP7_TouchManager.TouchInfo touch2 in WP7_TouchManager.m_Touches)
        {
          if (touch2.pointer == pointer_)
          {
            flag = true;
            break;
          }
        }
        if (flag)
          ++pointer_;
        else
          break;
      }
      WP7_TouchManager.TouchInfo touchInfo = new WP7_TouchManager.TouchInfo(pointer_, (int) position.X, (int) position.Y);
      WP7_TouchManager.m_Touches.Add(touchInfo);
      return touchInfo;
    }

    public static void touchBegan(TouchLocation touch)
    {
      WP7_TouchManager.TouchInfo touchInfo = WP7_TouchManager.updateTouch(touch, true);
      if (touchInfo == null)
        return;
      int x = touchInfo.x * 32 / 48;
      int y = touchInfo.y * 32 / 48;
      Runtime.getRuntime().pointerPressed(x, y, touchInfo.pointer);
    }

    public static void touchMoved(TouchLocation touch)
    {
      WP7_TouchManager.TouchInfo touchInfo = WP7_TouchManager.updateTouch(touch, false);
      if (touchInfo == null)
        return;
      int x = touchInfo.x * 32 / 48;
      int y = touchInfo.y * 32 / 48;
      Runtime.getRuntime().pointerDragged(x, y, touchInfo.pointer);
    }

    public static void touchEnded(TouchLocation touch)
    {
      WP7_TouchManager.TouchInfo touch1 = WP7_TouchManager.findTouch(touch);
      if (touch1 != null)
      {
        int x = touch1.x * 32 / 48;
        int y = touch1.y * 32 / 48;
        Runtime.getRuntime().pointerReleased(x, y, touch1.pointer);
      }
      WP7_TouchManager.m_Touches.Remove(touch1);
    }

    public static void Process()
    {
      foreach (TouchLocation touch in TouchPanel.GetState())
      {
        if (touch.State == TouchLocationState.Pressed)
          WP7_TouchManager.touchBegan(touch);
        if (touch.State == TouchLocationState.Moved)
          WP7_TouchManager.touchMoved(touch);
        if (touch.State == TouchLocationState.Released)
          WP7_TouchManager.touchEnded(touch);
      }
    }

    public class TouchInfo
    {
      public int x;
      public int y;
      public int pointer;

      public TouchInfo(int pointer_, int x_, int y_)
      {
        this.x = x_;
        this.y = y_;
        this.pointer = pointer_;
      }
    }
  }
}
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using midp;
using System.Collections.Generic;

#nullable disable
namespace support
{
    internal class WP7_TouchManager
    {
        private static List<WP7_TouchManager.TouchInfo> m_Touches;
        private static bool m_MousePressed = false;

        public static void Init()
        {
            WP7_TouchManager.m_Touches = new List<WP7_TouchManager.TouchInfo>(10);
        }

        public static WP7_TouchManager.TouchInfo findTouch(TouchLocation tl)
        {
            Vector2 position = tl.Position;
            Vector2 vector2;
            vector2.X = position.X;
            vector2.Y = position.Y;
            TouchLocation previousLocation;
            if (tl.TryGetPreviousLocation(out previousLocation))
            {
                vector2.X = previousLocation.Position.X;
                vector2.Y = previousLocation.Position.Y;
            }
            foreach (WP7_TouchManager.TouchInfo touch in WP7_TouchManager.m_Touches)
            {
                if ((double)vector2.X == (double)touch.x && (double)vector2.Y == (double)touch.y
                   || (double)position.X == (double)touch.x && (double)position.Y == (double)touch.y)
                    return touch;
            }
            return (WP7_TouchManager.TouchInfo)null;
        }

        public static WP7_TouchManager.TouchInfo updateTouch(TouchLocation touch, bool addNewTouch)
        {
            Vector2 position = touch.Position;
            WP7_TouchManager.TouchInfo touch1 = WP7_TouchManager.findTouch(touch);
            if (touch1 != null)
            {
                touch1.x = (int)position.X;
                touch1.y = (int)position.Y;
                return touch1;
            }
            if (!addNewTouch)
                return (WP7_TouchManager.TouchInfo)null;
            int pointer_ = 0;
            while (true)
            {
                bool flag = false;
                foreach (WP7_TouchManager.TouchInfo touch2 in WP7_TouchManager.m_Touches)
                {
                    if (touch2.pointer == pointer_)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    ++pointer_;
                else
                    break;
            }
            WP7_TouchManager.TouchInfo touchInfo = new WP7_TouchManager.TouchInfo(pointer_, (int)position.X, (int)position.Y);
            WP7_TouchManager.m_Touches.Add(touchInfo);
            return touchInfo;
        }

        public static void touchBegan(TouchLocation touch)
        {
            WP7_TouchManager.TouchInfo touchInfo = WP7_TouchManager.updateTouch(touch, true);
            if (touchInfo == null)
                return;
            int x = touchInfo.x * 32 / 48;
            int y = touchInfo.y * 32 / 48;
            Runtime.getRuntime().pointerPressed(x, y, touchInfo.pointer);
        }

        public static void touchMoved(TouchLocation touch)
        {
            WP7_TouchManager.TouchInfo touchInfo = WP7_TouchManager.updateTouch(touch, false);
            if (touchInfo == null)
                return;
            int x = touchInfo.x * 32 / 48;
            int y = touchInfo.y * 32 / 48;
            Runtime.getRuntime().pointerDragged(x, y, touchInfo.pointer);
        }

        public static void touchEnded(TouchLocation touch)
        {
            WP7_TouchManager.TouchInfo touch1 = WP7_TouchManager.findTouch(touch);
            if (touch1 != null)
            {
                int x = touch1.x * 32 / 48;
                int y = touch1.y * 32 / 48;
                Runtime.getRuntime().pointerReleased(x, y, touch1.pointer);
            }
            WP7_TouchManager.m_Touches.Remove(touch1);
        }

        public static void mouseBegan(MouseState mouseState)
        {
            if (m_MousePressed)
                return;

            m_MousePressed = true;
            int x = mouseState.X * 32 / 48;
            int y = mouseState.Y * 32 / 48;
            Runtime.getRuntime().pointerPressed(x, y, 0); // Use pointer 0 for mouse
        }

        public static void mouseMoved(MouseState mouseState)
        {
            if (!m_MousePressed)
                return;

            int x = mouseState.X * 32 / 48;
            int y = mouseState.Y * 32 / 48;
            Runtime.getRuntime().pointerDragged(x, y, 0); // Use pointer 0 for mouse
        }

        public static void mouseEnded(MouseState mouseState)
        {
            if (!m_MousePressed)
                return;

            m_MousePressed = false;
            int x = mouseState.X * 32 / 48;
            int y = mouseState.Y * 32 / 48;
            Runtime.getRuntime().pointerReleased(x, y, 0); // Use pointer 0 for mouse
        }

        public static void Process()
        {
            // Process touch input
            foreach (TouchLocation touch in TouchPanel.GetState())
            {
                if (touch.State == TouchLocationState.Pressed)
                    WP7_TouchManager.touchBegan(touch);
                if (touch.State == TouchLocationState.Moved)
                    WP7_TouchManager.touchMoved(touch);
                if (touch.State == TouchLocationState.Released)
                    WP7_TouchManager.touchEnded(touch);
            }

            // Process mouse input
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                WP7_TouchManager.mouseBegan(mouseState);
            else if (mouseState.LeftButton == ButtonState.Released && m_MousePressed)
                WP7_TouchManager.mouseEnded(mouseState);
            else if (m_MousePressed)
                WP7_TouchManager.mouseMoved(mouseState);
        }

        public class TouchInfo
        {
            public int x;
            public int y;
            public int pointer;

            public TouchInfo(int pointer_, int x_, int y_)
            {
                this.x = x_;
                this.y = y_;
                this.pointer = pointer_;
            }
        }
    }
}