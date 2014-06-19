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
using Newtonsoft.Json;
using TryAgain.Datas;
using System.Windows.Forms;
using TryAgain.Sounds;
using System.IO;

namespace TryAgain
{
    class Textures
    {
        public static Dictionary<String, Texture2D> Cache = new Dictionary<string, Texture2D>();

        public static Texture2D roche_herbe, halfsable;
        public static Texture2D scenar1, scenar2, scenar3, scenar4;

        public static Texture2D O, I, II, III, IV, V, VI, VII, VIII, IX, X, XI;  //numéros de ressources, se reférer à la clase craftinterface
        public static Texture2D CraftInterface;
        public static Texture2D GameOver, storyboard;

        public static Texture2D MainMenuBG, OptionBG, AboutBG, FullscreenBG, FenetreBG, MenuOption, Return, exit, play, english, easy, medium, hard, pause, craftInterface;
        public static Texture2D aPropos, jouer, pleinEcran, quitter, retour, fenetre, francais, facile, moyen, dur;

        public static Texture2D buttonTony, buttonPierre, Fleche, Fleche2, soundOff;
        public static Texture2D whitePixel;

        public static Texture2D Shield1, Shield2, Missile, Chosen;
        public static Texture2D c0potion, c1shield, c2gun1, c3bottes, c4cannon, c5soda;

        public static SpriteFont UIfont, UIfontSmall;
        public static Texture2D UIitemHolder, UIitemSelected;

        public static bool everythingLoaded = false;

        // UI gauges
        public static Texture2D[] healthGauge = new Texture2D[9]; // BG + 8 others
        public static Texture2D[] manaGauge = new Texture2D[9]; // BG + 8 others
        public static Texture2D fogCafeine;

        public static void load(ContentManager cm)
        {
            Tuple<String, String>[] texturesList = JsonConvert.DeserializeObject<Tuple<String, String>[]>(Initializer.ReadTextFile(@"elements\textures\texturesList.json"));

            foreach (var t in texturesList)
            {
                Cache[t.Item1] = cm.Load<Texture2D>(t.Item2);
            }

            //On quitte les textures purgame
            /*Button_Play = cm.Load<Texture2D>(@"Menu\Button_Play");
            Button_Option = cm.Load<Texture2D>(@"Menu\Button_Option");
            Button_Exit = cm.Load<Texture2D>(@"Menu\Button_Exit");
            Button_About = cm.Load<Texture2D>(@"Menu\Button_About");
            Button_Return = cm.Load<Texture2D>(@"Menu\Button_Return");
            */
            whitePixel = cm.Load<Texture2D>(@"UI/pxl");

            Shield1 = cm.Load<Texture2D>(@"Sprites\RandomAnimations\bouclier1");
            Shield2 = cm.Load<Texture2D>(@"Sprites\RandomAnimations\bouclier2");
            Missile = cm.Load<Texture2D>(@"Sprites\RandomAnimations\Missile");
            Fleche = cm.Load<Texture2D>(@"Menu\fleche");
            Fleche2 = cm.Load<Texture2D>(@"Menu\fleche2");
            soundOff = cm.Load<Texture2D>(@"Menu\soundOff");

            scenar1 = cm.Load<Texture2D>(@"Scenar\Planche1");
            scenar2 = cm.Load<Texture2D>(@"Scenar\Planche2");
            scenar3 = cm.Load<Texture2D>(@"Scenar\Planche3");
            scenar4 = cm.Load<Texture2D>(@"Scenar\Planche4");

            O = cm.Load<Texture2D>(@"Sprites\Ressources\1");
            I = cm.Load<Texture2D>(@"Sprites\Ressources\2");
            II = cm.Load<Texture2D>(@"Sprites\Ressources\3");
            III = cm.Load<Texture2D>(@"Sprites\Ressources\4");
            IV = cm.Load<Texture2D>(@"Sprites\Ressources\5");
            V = cm.Load<Texture2D>(@"Sprites\Ressources\6");
            VI = cm.Load<Texture2D>(@"Sprites\Ressources\7");
            VII = cm.Load<Texture2D>(@"Sprites\Ressources\8");
            VIII = cm.Load<Texture2D>(@"Sprites\Ressources\9");
            IX = cm.Load<Texture2D>(@"Sprites\Ressources\10");
            X = cm.Load<Texture2D>(@"Sprites\Ressources\11");
            CraftInterface = cm.Load<Texture2D>(@"Menu\CraftInterface");
            c0potion = cm.Load<Texture2D>(@"Crafts\0potion");
            c1shield = cm.Load<Texture2D>(@"Crafts\1shield");
            c2gun1 = cm.Load<Texture2D>(@"Crafts\2gun1");
            c3bottes = cm.Load<Texture2D>(@"Crafts\3bottes");
            c4cannon = cm.Load<Texture2D>(@"Crafts\4canon");
            c5soda = cm.Load<Texture2D>(@"Crafts\5soda");
            GameOver = cm.Load<Texture2D>(@"Menu\GameOver");
            storyboard = cm.Load<Texture2D>(@"Menu\storyboard");

            MainMenuBG = cm.Load<Texture2D>(@"Menu\MainMenu");
            OptionBG = cm.Load<Texture2D>(@"Menu\MenuOption");

            //eng
            AboutBG = cm.Load<Texture2D>(@"Menu\eng\Button_About");
            FullscreenBG = cm.Load<Texture2D>(@"Menu\eng\fullscreen");
            FenetreBG = cm.Load<Texture2D>(@"Menu\eng\Button_Windowed");
            MenuOption = cm.Load<Texture2D>(@"Menu\fr\Button_Option");
            Return = cm.Load<Texture2D>(@"Menu\eng\Button_Exit");
            exit = cm.Load<Texture2D>(@"Menu\eng\Button_Exit");
            play = cm.Load<Texture2D>(@"Menu\eng\Button_Play");
            english = cm.Load<Texture2D>(@"Menu\eng\Button_English");
            easy = cm.Load<Texture2D>(@"Menu\eng\easy");
            medium = cm.Load<Texture2D>(@"Menu\eng\medium");
            hard = cm.Load<Texture2D>(@"Menu\eng\hard");
            //fr
            aPropos = cm.Load<Texture2D>(@"Menu\fr\Button_APropos");
            jouer = cm.Load<Texture2D>(@"Menu\fr\Button_jouer");
            pleinEcran = cm.Load<Texture2D>(@"Menu\fr\Button_PleinEcran");
            quitter = cm.Load<Texture2D>(@"Menu\fr\Button_Quitter");
            retour = cm.Load<Texture2D>(@"Menu\fr\Button_Retour");
            fenetre = cm.Load<Texture2D>(@"Menu\fr\fenetre");
            francais = cm.Load<Texture2D>(@"Menu\fr\Button-francais");
            facile = cm.Load<Texture2D>(@"Menu\fr\button_facil");
            moyen = cm.Load<Texture2D>(@"Menu\fr\button_moyen");
            dur = cm.Load<Texture2D>(@"Menu\fr\button_difficile");

            buttonPierre = cm.Load<Texture2D>(@"Sprites\persopierre");
            buttonTony = cm.Load<Texture2D>(@"Sprites\persotony");
            Chosen = cm.Load<Texture2D>(@"Sprites\Chosen");

            UIfont = cm.Load<SpriteFont>(@"Fonts\UIfont");
            UIfontSmall = cm.Load<SpriteFont>(@"Fonts\UIfontsmall");
            UIitemHolder = cm.Load<Texture2D>(@"UI\uiitems");
            UIitemSelected = cm.Load<Texture2D>(@"UI\uiselected");

            roche_herbe = cm.Load<Texture2D>(@"Sprites\herbe_roche");
            halfsable = cm.Load<Texture2D>(@"Sprites\halfsable");
            whitePixel = cm.Load<Texture2D>(@"UI/pxl");

            healthGauge[0] = cm.Load<Texture2D>(@"UI\gauge\healthBG");
            for (int i = 0; i < 8; i++)
                healthGauge[i + 1] = cm.Load<Texture2D>(@"UI\gauge\health" + i.ToString());

            manaGauge[0] = cm.Load<Texture2D>(@"UI\gauge\manaBG");
            for (int i = 0; i < 8; i++)
                manaGauge[i + 1] = cm.Load<Texture2D>(@"UI\gauge\mana" + i.ToString());

            fogCafeine = cm.Load<Texture2D>(@"UI\gauge\fog");

            everythingLoaded = true;
        }
        /*
        public static Texture2D GetOrLoad(string name)
        {
            if(

        }*/

        public static Texture2D Crop(Texture2D image, Rectangle source)
        {
            var graphics = image.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, source.Width, source.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            sb.Draw(image, Vector2.Zero, source, Color.White);
            sb.End();

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }

        public static Texture2D Add(Texture2D txa, Texture2D txb, Rectangle txbPos)
        {
            var graphics = txa.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, txa.Bounds.Width, txa.Bounds.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            sb.Draw(txa, txa.Bounds, Color.White);
            sb.Draw(txb, txbPos, Color.White);
            sb.End();

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }


        public static Texture2D GetStringTexture(Texture2D tx, String str, Rectangle size)
        {
            return GetStringTexture(tx, str, size, Color.Blue);
        }

        public static Texture2D GetStringTexture(Texture2D tx, String str, Rectangle size, Color color)
        {
            var graphics = tx.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, size.Width, size.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            //sb.Draw(txa, txa.Bounds, Color.White);
            sb.DrawString(Textures.UIfont, str, new Vector2(size.X, size.Y), color);
            sb.End();

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }

        public static void DrawRectangle(SpriteBatch sb, Rectangle position, Color color)
        {
            sb.Draw(whitePixel, position, color);
        }

        public static void LoadTexture(String str, String file)
        {
            if (!Textures.Cache.ContainsKey(str))
            {
                Textures.Cache.Add(str, Texture2D.FromStream(Game1.gamegfx.GraphicsDevice, new FileStream(@"data\" + file, FileMode.Open)));
            }
        }
        public static void LoadTextureList(String file)
        {
            Tuple<String, String>[] texturesList = JsonConvert.DeserializeObject<Tuple<String, String>[]>(Initializer.ReadTextFile(@"elements\textures\" + file));

            foreach (var t in texturesList)
            {
                LoadTexture(t.Item1, t.Item2);
            }
        }
    }
}
