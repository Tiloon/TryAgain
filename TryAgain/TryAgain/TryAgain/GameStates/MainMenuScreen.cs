using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TryAgain.GameStates
{
    class MainMenuScreen : Screen
    {
        public MainMenuScreen()
        {
            this.state = ScreenType.MainMenu;
        }
        public override ScreenType update()
        {
            return this.GetState();
        }
        public override void draw(SpriteBatch sb)
        {
        }
        public override void init()
        {
        }
    }
}
