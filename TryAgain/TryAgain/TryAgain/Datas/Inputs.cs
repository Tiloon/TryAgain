using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TryAgain.Datas
{
    class Inputs
    {
        static private MouseState curMouseState = new MouseState();
        static private KeyboardState curKeyBState = new KeyboardState();
        static private MouseState prevMouseState = new MouseState();
        static private KeyboardState prevKeyBState = new KeyboardState();

        static public void upDate()
        {
            prevMouseState = curMouseState;
            prevKeyBState = curKeyBState;

            curMouseState = Mouse.GetState();
            curKeyBState = Keyboard.GetState();
        }

        static public Vector2 getMousePos()
        {
            return new Vector2(curMouseState.X, curMouseState.Y);
        }
        static public bool isKeyDown(Keys k)
        {
            return curKeyBState.IsKeyDown(k);
        }
        static public bool isKeyRelease(Keys k)
        {
            return curKeyBState.IsKeyUp(k) && prevKeyBState.IsKeyDown(k);
        }
        static public bool isLMBClick()
        {
            return curMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}