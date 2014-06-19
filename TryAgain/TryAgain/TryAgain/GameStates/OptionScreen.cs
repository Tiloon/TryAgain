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
        cButton ButtonPierre, ButtonDenis, ButtonAldric;
        cButton ButtonFlecheGauche, ButtonFlecheDroite, soundOff, buttonFrancais, buttonEnglish;
        cButton ButtonEasy, ButtonMedium, ButtonHard;
        float son = Sounds.Themes.volume;
        float saveSon = 0;
        bool u = true;
        static public bool eng = true;
        string mode = "medium";
        public static int lp = 60;

        static public bool fullscreen = false;


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
            if (ButtonDenis.IsClicked(mouse))
            {
                GameScreen.name = "Denis";
                Online.Connection.avatar = "Tdenis";
            }
            if (ButtonAldric.IsClicked(mouse))
            {
                GameScreen.name = "Aldoux";
                Online.Connection.avatar = "Taldric";
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
                init(Game1.graphics.GraphicsDevice);
            }
            if (buttonFrancais.IsClicked(mouse))
            {
                eng = false;
                init(Game1.graphics.GraphicsDevice);
            }
            if (ButtonEasy.IsClicked(mouse))
            {
                mode = "easy";
                lp = 100;
                
            }
            if (ButtonMedium.IsClicked(mouse))
            {
                mode = "medium";
                lp = 60;
            }
            if (ButtonHard.IsClicked(mouse))
            {
                mode = "hard";
                lp = 80;
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
            if (eng)
                sb.DrawString(Textures.UIfont, "Character choice:", new Vector2(400, 10), Color.Black);
            else
                sb.DrawString(Textures.UIfont, "Choix du personnage:", new Vector2(400, 10), Color.Black);

            if (Online.Connection.avatar == "Ttony")
                sb.Draw(Textures.Chosen, new Vector2(295, 45), Color.Black);
            sb.DrawString(Textures.UIfont, "Tony:", new Vector2(200, 100), Color.Black);
            ButtonPierre.Draw(sb);

            if (Online.Connection.avatar == "Tpierre")
                sb.Draw(Textures.Chosen, new Vector2(495, 45), Color.Black);
            sb.DrawString(Textures.UIfont, "Pierre:", new Vector2(400, 100), Color.Black);
            ButtonTony.Draw(sb);

            if (Online.Connection.avatar == "Tdenis")
                sb.Draw(Textures.Chosen, new Vector2(695, 45), Color.Black);
            sb.DrawString(Textures.UIfont, "Denis:", new Vector2(600, 100), Color.Black);
            ButtonDenis.Draw(sb);

            if (Online.Connection.avatar == "Taldric")
                sb.Draw(Textures.Chosen, new Vector2(895, 45), Color.Black);
            sb.DrawString(Textures.UIfont, "Aldric:", new Vector2(800, 100), Color.Black);
            ButtonAldric.Draw(sb);

            //changer son
            ButtonFlecheGauche.Draw(sb);
            ButtonFlecheDroite.Draw(sb);
            sb.DrawString(Textures.UIfont, ((int)(son * 100)).ToString() + "%", new Vector2(410, 410), Color.Black);
            if (eng)
                sb.DrawString(Textures.UIfont, "Sound:", new Vector2(200, 400), Color.Black, 0F, new Vector2(0, 0), 1.42F, SpriteEffects.None, 0);
            else
                sb.DrawString(Textures.UIfont, "Son:", new Vector2(200, 400), Color.Black, 0F, new Vector2(0, 0), 1.42F, SpriteEffects.None, 0);
            soundOff.Draw(sb);

            //changer langue
            if (eng)
                sb.DrawString(Textures.UIfont, "Choose language:", new Vector2(200, 550), Color.Black);
            else
                sb.DrawString(Textures.UIfont, "Choisir la langue:", new Vector2(200, 550), Color.Black);
            buttonEnglish.Draw(sb);
            buttonFrancais.Draw(sb);

            //changer difficulté
            ButtonEasy.Draw(sb);
            ButtonMedium.Draw(sb);
            ButtonHard.Draw(sb);
            if (eng)
            {
                if (mode == "easy")
                    sb.DrawString(Textures.UIfont, "mode: easy", new Vector2(200, 800), Color.Black);
                if (mode == "medium")
                    sb.DrawString(Textures.UIfont, "mode: medium", new Vector2(200, 800), Color.Black);
                if (mode == "hard")
                    sb.DrawString(Textures.UIfont, "mode: hard", new Vector2(200, 800), Color.Black);
            }
            else
            {
                if (mode == "easy")
                    sb.DrawString(Textures.UIfont, "mode: facile", new Vector2(200, 800), Color.Black);
                if (mode == "medium")
                    sb.DrawString(Textures.UIfont, "mode: moyen", new Vector2(200, 800), Color.Black);
                if (mode == "hard")
                    sb.DrawString(Textures.UIfont, "mode: dur", new Vector2(200, 800), Color.Black);
            }
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
            ButtonDenis = new cButton(Textures.Cache["Tdenis"], graphics, 65 * 2, 65 * 2);
            ButtonDenis.SetPosition(new Vector2(657, 50));
            ButtonAldric = new cButton(Textures.Cache["Taldric"], graphics, 65 * 2, 65 * 2);
            ButtonAldric.SetPosition(new Vector2(857, 50));
            ButtonFlecheGauche = new cButton(Textures.Fleche, graphics, 50, 50);
            ButtonFlecheGauche.SetPosition(new Vector2(300, 400));
            ButtonFlecheDroite = new cButton(Textures.Fleche2, graphics, 50, 50);
            ButtonFlecheDroite.SetPosition(new Vector2(500, 400));
            soundOff = new cButton(Textures.soundOff, graphics, 50, 50);
            soundOff.SetPosition(new Vector2(600, 400));
            buttonEnglish = new cButton(Textures.english, graphics);
            buttonEnglish.SetPosition(new Vector2(200, 550));
            buttonFrancais = new cButton(Textures.francais, graphics);
            buttonFrancais.SetPosition(new Vector2(600, 550));
            if (eng)
                ButtonEasy= new cButton(Textures.easy, graphics);
            else
                ButtonEasy= new cButton(Textures.facile, graphics);
            ButtonEasy.SetPosition(new Vector2(200, 700));
            if (eng)
                ButtonMedium = new cButton(Textures.medium, graphics);
            else
                ButtonMedium= new cButton(Textures.moyen, graphics);
            ButtonMedium.SetPosition(new Vector2(600, 700));
            if (eng)
                ButtonHard   = new cButton(Textures.hard, graphics);
            else
                ButtonHard= new cButton(Textures.dur, graphics);
            ButtonHard.SetPosition(new Vector2(1000, 700));
        }
    }
}
