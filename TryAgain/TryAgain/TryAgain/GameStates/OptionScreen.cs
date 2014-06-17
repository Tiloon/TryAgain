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
        cButton ButtonTony;
        cButton ButtonPierre;
        cButton ButtonFlecheGauche, ButtonFlecheDroite, soundOff, buttonFrancais, buttonEnglish;
        float son = Sounds.Themes.volume;
        float saveSon = 0;
        bool u = true;
        static public bool eng = true;
        string sound = "sound";
        string choix = "Choix du personnage";

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

            if (ButtonTony.IsClicked(mouse))
            {
                GameScreen.name = "Tony";
                Online.Connection.avatar = "Ttony";
            }
            if (ButtonPierre.IsClicked(mouse))
            {
                GameScreen.name = "Pierre";
                Online.Connection.avatar = "Tpierre";
            }
            if (ButtonReturn.IsClicked(mouse))
            {
                if (GameScreen.IsGameStarted())
                    return ScreenType.Pause;
                else
                    return ScreenType.MainMenu;
            }
            if (ButtonFlecheGauche.IsClicked(mouse))
            {
                if (Sounds.Themes.volume >= 0.01F)
                {
                    Sounds.Themes.volume -= 0.01F;
                    son = Sounds.Themes.volume;
                }
                else
                {
                    Sounds.Themes.volume = 0;
                    son = 0;
                }
            }
            if (ButtonFlecheDroite.IsClicked(mouse))
            {
                if (Sounds.Themes.volume <= 0.99F)
                {
                    Sounds.Themes.volume += 0.01F;
                    son = Sounds.Themes.volume;
                }
                else
                {
                    Sounds.Themes.volume = 1F;
                    son = 1;
                }
            }
            if (soundOff.IsClicked(mouse))
            {
                if (u)
                {
                    if (son != 0)
                    {
                        saveSon = son;
                        son = 0;
                        Sounds.Themes.volume = 0;
                    }
                    else
                    {
                        son = saveSon;
                        Sounds.Themes.volume = son;
                    }
                    u = false;
                }
            }
            else // u est la pour que le bouton s qctive qu'une fois quand on appuie sur le bouton
            {
                if (soundOff.IsReleased(mouse))
                    u = true;
            }
            if (buttonEnglish.IsClicked(mouse))
            {
                eng = true;
                sound = "sound";
                choix = "choice character";
                init(Game1.graphics.GraphicsDevice);
            }
            if (buttonFrancais.IsClicked(mouse))
            {
                eng = false;
                sound = "son";
                init(Game1.graphics.GraphicsDevice);
                choix = "Choix du personnage";
            }
            if (ButtonFullscreen.IsClicked(mouse) || ButtonFenetre.IsClicked(mouse))
            {
                Game1.graphics.ToggleFullScreen();
                Game1.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Last().Width;
                Game1.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Last().Height;
                Game1.graphics.ApplyChanges();
            }
            return this.GetState();
        }


        public override void draw(SpriteBatch sb, int Width, int Height)
        {

            //sb.Draw(Content.Load<Texture2D>("Option"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
            sb.Draw(Textures.OptionBG, new Rectangle(0, 0, Width, Height), Color.White);
            ButtonReturn.Draw(sb);
            if (Game1.graphics.IsFullScreen)
                ButtonFenetre.Draw(sb);
            else
                ButtonFullscreen.Draw(sb);

            //choix du personnage
            sb.DrawString(Textures.UIfont, choix + ":", new Vector2(400, 10), Color.Black);
            if (Online.Connection.avatar == "Ttony")
                sb.Draw(Textures.Chosen, new Vector2(295, 45), Color.Black);
            sb.DrawString(Textures.UIfont, "Tony:", new Vector2(200, 100), Color.Black);
            ButtonPierre.Draw(sb);
            if (Online.Connection.avatar == "Tpierre")
                sb.Draw(Textures.Chosen, new Vector2(495, 45), Color.Black);
            sb.DrawString(Textures.UIfont, "Pierre:", new Vector2(400, 100), Color.Black);
            ButtonTony.Draw(sb);

            //changer son
            ButtonFlecheGauche.Draw(sb);
            ButtonFlecheDroite.Draw(sb);
            sb.DrawString(Textures.UIfont, ((int)(son * 100)).ToString() + "%", new Vector2(410, 410), Color.Black);
            sb.DrawString(Textures.UIfont, sound + ":", new Vector2(200, 400), Color.Black, 0F, new Vector2(0, 0), 1.42F, SpriteEffects.None, 0);
            soundOff.Draw(sb);

            //changer langue
            buttonEnglish.Draw(sb);
            buttonFrancais.Draw(sb);

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
            if (eng)
                ButtonReturn = new cButton(Textures.Return, graphics);
            else
                ButtonReturn = new cButton(Textures.retour, graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704 + 100));

            if (eng)
                ButtonFullscreen = new cButton(Textures.FullscreenBG, graphics);
            else
                ButtonFullscreen = new cButton(Textures.pleinEcran, graphics);
            ButtonFullscreen.SetPosition(new Vector2(500, 200));

            if (eng)
                ButtonFenetre = new cButton(Textures.FenetreBG, graphics);
            else
                ButtonFenetre = new cButton(Textures.fenetre, graphics);
            ButtonFenetre.SetPosition(new Vector2(500, 200));

            ButtonTony = new cButton(Textures.buttonTony, graphics, 22 * 2, 65 * 2);
            ButtonTony.SetPosition(new Vector2(300, 50));
            ButtonPierre = new cButton(Textures.buttonPierre, graphics, 22 * 2, 65 * 2);
            ButtonPierre.SetPosition(new Vector2(500, 50));
            ButtonFlecheGauche = new cButton(Textures.Fleche, graphics, 50, 50);
            ButtonFlecheGauche.SetPosition(new Vector2(300, 400));
            ButtonFlecheDroite = new cButton(Textures.Fleche2, graphics, 50, 50);
            ButtonFlecheDroite.SetPosition(new Vector2(500, 400));
            soundOff = new cButton(Textures.soundOff, graphics, 50, 50);
            soundOff.SetPosition(new Vector2(600, 400));
            buttonEnglish = new cButton(Textures.english, graphics);
            buttonEnglish.SetPosition(new Vector2(600, 400));
            buttonFrancais = new cButton(Textures.francais, graphics);
            buttonFrancais.SetPosition(new Vector2(600, 600));
            loadResoltutions(graphics);
        }
    }
}
