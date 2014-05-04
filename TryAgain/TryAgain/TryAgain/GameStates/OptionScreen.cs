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
using TryAgain.Menu;

namespace TryAgain.GameStates
{
    class OptionScreen : Screen
    {
        cButton ButtonReturn;
        cButton ButtonFullscreen;
        cButton ButtonFenetre;
        static public bool fullscreen = false;

        public OptionScreen()
        {
            this.state = ScreenType.Options;
        }

        public override ScreenType update()
        {
            MouseState mouse = Mouse.GetState();

            if (ButtonReturn.IsClicked(mouse))
            {
                if (GameScreen.IsGameStarted())
                    return ScreenType.Pause;
                else
                    return ScreenType.MainMenu;
            }
            if (ButtonFullscreen.IsClicked(mouse))
            {
                if (!Game1.graphics.IsFullScreen)
                {
                    Game1.graphics.IsFullScreen = true;
                    Game1.graphics.ApplyChanges();
                }
            }
            if (ButtonFenetre.IsClicked(mouse))
            {
                if (Game1.graphics.IsFullScreen)
                {
                    Game1.graphics.IsFullScreen = false;
                    Game1.graphics.ApplyChanges();
                }
            }
            return this.GetState();
        }


        public override void draw(SpriteBatch sb, int Width, int Height)
        {

            //sb.Draw(Content.Load<Texture2D>("Option"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
            ButtonReturn.Draw(sb);
            ButtonFullscreen.Draw(sb);
            ButtonFenetre.Draw(sb);
        }
        public override void init(GraphicsDevice graphics)
        {
            ButtonReturn = new cButton(Textures.Cache["UIBreturn"], graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704 + 100));
            ButtonFullscreen = new cButton(Textures.FullscreenBG, graphics);
            ButtonFullscreen.SetPosition(new Vector2(500, 100));
            ButtonFenetre = new cButton(Textures.FenetreBG, graphics);
            ButtonFenetre.SetPosition(new Vector2(500, 300));
            /*
            ButtonOnePlayer = new cButton(Textures.Button_OnePlayer, graphics);
            ButtonOnePlayer.SetPosition(new Vector2(1062, 900));
            ButtonTwoPlayers = new cButton(Textures.Button_TwoPlayers, graphics);
            ButtonTwoPlayers.SetPosition(new Vector2(1062, 1000));*/
        }
    }
}
