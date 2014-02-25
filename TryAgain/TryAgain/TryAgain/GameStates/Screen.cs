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
    enum ScreenType
	{
        MainMenu,
        Game,
        Quit
	}
    abstract class Screen
    {
        protected ScreenType state;
        public abstract ScreenType update();
        public abstract void draw(SpriteBatch sb, int Width, int Height);
        public abstract void init(GraphicsDevice graphics);
        public ScreenType GetState()
        {
            return this.state;
        }
        public static void ChangeScreen(ref Screen sc, ScreenType st)
        {
            switch (st)
            {
                case ScreenType.MainMenu:
                    sc = new MainMenuScreen();
                    break;
                case ScreenType.Game:
                    sc = new GameScreen();
                    break;
                default:
                    break;
            }
        }
    }
}
