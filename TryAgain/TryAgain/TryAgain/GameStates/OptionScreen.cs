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

        Tuple<cButton, Vector2>[] resolutions;

        private static Vector2[] reslist = 
        {
            new Vector2(800, 600),
            new Vector2(1024, 768),
            new Vector2(1360, 768),
            new Vector2(1280, 800),
            new Vector2(1280, 1024),
            new Vector2(1400, 1050),
            new Vector2(1600, 900),
            new Vector2(1600, 1200),
            new Vector2(1920, 1080),
            new Vector2(1920, 1200),
            new Vector2(2048, 1080),
            new Vector2(2048, 1152),
            new Vector2(2048, 1536),
            new Vector2(2560, 1440),
            new Vector2(2560, 1600),
            new Vector2(2880, 1800),
            new Vector2(3200, 2048),
            new Vector2(3200, 2400)
        };

        static public bool fullscreen = false;

        private void loadResoltutions(GraphicsDevice graphics)
        {
            if (resolutions == null)
            {
                List<Vector2> l = new List<Vector2>();
                foreach (Vector2 resolution in reslist)
                {
                    if ((Game1.graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height >= resolution.Y) &&
                        (Game1.graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width >= resolution.X))
                        l.Add(resolution);
                }
                resolutions = new Tuple<cButton, Vector2>[l.Count];
                for (int i = 0; i < l.Count; i++)
                {
                    //cButton button = new cButton(Textures.GetStringTexture(Textures.Cache["UIBreturn"], l[i].X + "x" + l[i].Y, new Rectangle(0, 0, 128, 128)), Game1.graphics.GraphicsDevice);

                    // Affiche la même chose que le bouton return
                    resolutions[i] = new Tuple<cButton, Vector2>(new cButton(Textures.Cache["UIBreturn"], graphics), l[i]);
                    resolutions[i].Item1.SetPosition(new Vector2(800, 600));
                }
            }
        }


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
            if (ButtonFullscreen.IsClicked(mouse) || ButtonFenetre.IsClicked(mouse))
            {
                Game1.graphics.IsFullScreen = !Game1.graphics.IsFullScreen;
                Game1.graphics.ApplyChanges();
            }
            return this.GetState();
        }


        public override void draw(SpriteBatch sb, int Width, int Height)
        {

            //sb.Draw(Content.Load<Texture2D>("Option"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
            sb.Draw(Textures.MenuOption, new Rectangle(0, 0, Width, Height), Color.White);
            ButtonReturn.Draw(sb);
            if (Game1.graphics.IsFullScreen)
                ButtonFenetre.Draw(sb);
            else
                ButtonFullscreen.Draw(sb);
            
            // La personne qui arrive à afficher quelque chose grâce à 
            resolutions[0].Item1.Draw(sb); // ça,
            foreach (Tuple<cButton, Vector2> resbutton in resolutions)
            {
                resbutton.Item1.Draw(sb); // ou encore mieux ça,
            }
            // je lui dit gros GG.
            // Normalement ça devrait afficher EXACTEMENT la même chose que le bouton return (voir au dessus)
        }
        public override void init(GraphicsDevice graphics)
        {
            ButtonReturn = new cButton(Textures.Cache["UIBreturn"], graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704 + 100));
            ButtonFullscreen = new cButton(Textures.FullscreenBG, graphics);
            ButtonFullscreen.SetPosition(new Vector2(500, 100));
            ButtonFenetre = new cButton(Textures.FenetreBG, graphics);
            ButtonFenetre.SetPosition(new Vector2(500, 100));
            loadResoltutions(graphics);
        }
    }
}
