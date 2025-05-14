using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManager
{
    public class InputManager
    {
        private static InputManager instance;
      

        public static InputManager GetInstance()
        {
            if (InputManager.instance == null)
                InputManager.instance = new InputManager();
            return InputManager.instance;
        }

        public static void SetInput(string input, Keys key)
        {
            if(!inputs.ContainsKey(input))
            {
                inputNames.Add(input);
            }
            inputs[input] = key;
        }

        public static void UpdateInput(KeyboardState k)
        {
            pressed.Clear();
            released.Clear();

            if (rebind == null)
            {
                foreach (KeyValuePair<string, Keys> input in inputs)
                {
                    if (!keyboard.IsKeyDown(input.Value) && k.IsKeyDown(input.Value)) pressed.Add(input.Key);
                    if (keyboard.IsKeyDown(input.Value) && !k.IsKeyDown(input.Value)) released.Add(input.Key);
                }
            }
            else
            {
                foreach (Keys key in k.GetPressedKeys())
                {
                    bool flag = true;
                    foreach(KeyValuePair<string, Keys> pair in inputs)
                    {
                        if (pair.Key == rebind) continue;
                        if (pair.Value == key)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag && keyboard.IsKeyUp(key))
                    {
                        inputs[rebind] = key;
                        rebind = null;
                        break;
                    }
                }
            }

            keyboard = k;
        }

        static private KeyboardState keyboard = new KeyboardState();
        static private Dictionary<string, Keys> inputs = new Dictionary<string, Keys>();
        static private List<string> inputNames = new List<string>();
        static private HashSet<string> pressed = new HashSet<string>();
        static private HashSet<string> released = new HashSet<string>();
        static private string rebind = null;

        public static bool IsHeld(string input)
        {
            if (!inputs.ContainsKey(input)) return false;
            return keyboard.IsKeyDown(inputs[input]);
        }

        public static bool IsPressed(string input)
        {
            return pressed.Contains(input);
        }

        public static bool IsReleased(string input)
        {
            return released.Contains(input);
        }

        public static List<string> GetInputList()
        {
            List<string> retVal = new List<string>();
            for(int i = 0; i < inputNames.Count; i++)
            {
                retVal.Add(inputNames[i]);
            }
            return retVal;
        }

        public static List<Keys> GetKeysList()
        {
            List<Keys> retVal = new List<Keys>();
            for (int i = 0; i < inputNames.Count; i++)
            {
                retVal.Add(inputs[inputNames[i]]);
            }
            return retVal;
        }

        public static Keys GetInputKey(string input)
        {
            return inputs[input];
        }

        public static void SetRebind(string input)
        {
            rebind = input;
        }

        public static bool IsRebinding()
        {
            return rebind != null;
        }

        public static Dictionary<string, Keys> GetInputDictionary()
        {
            Dictionary<string, Keys> retVal = new Dictionary<string, Keys>();
            foreach(KeyValuePair<string, Keys> pair in inputs)
            {
                retVal[pair.Key] = pair.Value;
            }
            return retVal;
        }

        public void ClearActions(uint v)
        {
            //
        }

        internal void Update(float dt)
        {
            //
        }

        internal void LoadConfigFile(string v)
        {
            //
        }

        //internal void RegisterInputCallback(string hash1, InputActionMapper.InputCallback inputCallback)
        //{
        //}

        internal void Init()
        {
            if (InputManager.instance == null)
                InputManager.instance = new InputManager();
        }
    }
}
