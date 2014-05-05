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

namespace TryAgain
{
    class Textures
    {
        public static Dictionary<String, Texture2D> Cache = new Dictionary<string,Texture2D>();

        public static Texture2D roche_herbe, halfsable;

        public static Texture2D MainMenuBG, OptionBG, AboutBG, FullscreenBG, FenetreBG;
        public static Texture2D whitePixel;

        public static SpriteFont UIfont, UIfontSmall;
        public static Texture2D UIitemHolder, UIitemSelected;
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
            MainMenuBG = cm.Load<Texture2D>(@"Menu\MainMenu");
            OptionBG = cm.Load<Texture2D>(@"Menu\Option");
            AboutBG = cm.Load<Texture2D>(@"Menu\About");
            FullscreenBG = cm.Load<Texture2D>(@"Menu\fullscreen");
            FenetreBG = cm.Load<Texture2D>(@"Menu\fenetre");

            UIfont = cm.Load<SpriteFont>(@"Fonts\UIfont");
            UIfontSmall = cm.Load<SpriteFont>(@"Fonts\UIfontsmall");
            UIitemHolder = cm.Load<Texture2D>(@"UI\uiitems");
            UIitemSelected = cm.Load<Texture2D>(@"UI\uiselected");

            roche_herbe = cm.Load<Texture2D>(@"Sprites\herbe_roche");
            halfsable = cm.Load<Texture2D>(@"Sprites\halfsable");
            whitePixel = cm.Load<Texture2D>(@"UI/pxl");
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
    }
}
