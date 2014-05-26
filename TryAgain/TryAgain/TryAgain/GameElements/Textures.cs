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

        public static Texture2D O, I, II, III, IV, V, VI, VII, VIII, IX, X, XI;  //numéros de ressources, se reférer à la clase craftinterface
        public static Texture2D CraftInterface;
        public static Texture2D GameOver, storyboard;

        public static Texture2D MainMenuBG, OptionBG, AboutBG, FullscreenBG, FenetreBG, MenuOption, buttonTony, buttonPierre;
        public static Texture2D whitePixel;

        public static Texture2D Shield, Missile, Chosen;

        public static SpriteFont UIfont, UIfontSmall;
        public static Texture2D UIitemHolder, UIitemSelected;

        // UI gauges
        public static Texture2D[] healthGauge = new Texture2D[9]; // BG + 8 others
        public static Texture2D[] manaGauge = new Texture2D[9]; // BG + 8 others

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

            Shield = cm.Load<Texture2D>(@"Sprites\RandomAnimations\Shield");
            Missile = cm.Load<Texture2D>(@"Sprites\RandomAnimations\Missile");

            I = cm.Load<Texture2D>(@"Sprites\Ressources\1");
            II = cm.Load<Texture2D>(@"Sprites\Ressources\2");
            III = cm.Load<Texture2D>(@"Sprites\Ressources\3");
            IV = cm.Load<Texture2D>(@"Sprites\Ressources\4");
            V = cm.Load<Texture2D>(@"Sprites\Ressources\5");
            VI = cm.Load<Texture2D>(@"Sprites\Ressources\6");
            VII = cm.Load<Texture2D>(@"Sprites\Ressources\7");
            VIII = cm.Load<Texture2D>(@"Sprites\Ressources\8");
            IX = cm.Load<Texture2D>(@"Sprites\Ressources\9");
            X = cm.Load<Texture2D>(@"Sprites\Ressources\10");
            XI = cm.Load<Texture2D>(@"Sprites\Ressources\11");
            CraftInterface = cm.Load<Texture2D>(@"Menu\CraftInterface");
            GameOver = whitePixel;
            storyboard = cm.Load<Texture2D>(@"Menu\storyboard");

            MainMenuBG = cm.Load<Texture2D>(@"Menu\MainMenu");
            OptionBG = cm.Load<Texture2D>(@"Menu\Option");
            AboutBG = cm.Load<Texture2D>(@"Menu\About");
            FullscreenBG = cm.Load<Texture2D>(@"Menu\fullscreen");
            FenetreBG = cm.Load<Texture2D>(@"Menu\fenetre");
            MenuOption = cm.Load<Texture2D>(@"Menu\MenuOption");
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



            Themes.PlayTheme();
        }

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
