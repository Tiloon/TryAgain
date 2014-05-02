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

namespace TryAgain
{
    class Textures
    {
        public static Dictionary<String, Texture2D> Cache = new Dictionary<string,Texture2D>();

        public static Texture2D roche_herbe, halfsable;

        public static Texture2D Button_Play, Button_Option, Button_Exit, Button_About, Button_Return, Button_OnePlayer, Button_TwoPlayers;
        public static Texture2D MainMenuBG, OptionBG, AboutBG;

        public static SpriteFont UIfont;
        public static Texture2D UIitemHolder, UIitemSelected;
        public static void load(ContentManager cm)
        {
            Tuple<String, String>[] texturesList = JsonConvert.DeserializeObject<Tuple<String, String>[]>(Initializer.ReadTextFile(@"elements\textures\texturesList.json"));

            foreach (var t in texturesList)
            {
                Cache[t.Item1] = cm.Load<Texture2D>(t.Item2);
            }
            
            //On quitte les textures purgame
            Button_Play = cm.Load<Texture2D>(@"Menu\Button_Play");
            Button_Option = cm.Load<Texture2D>(@"Menu\Button_Option");
            Button_Exit = cm.Load<Texture2D>(@"Menu\Button_Exit");
            Button_About = cm.Load<Texture2D>(@"Menu\Button_About");
            Button_Return = cm.Load<Texture2D>(@"Menu\Button_Return");
            Button_OnePlayer = cm.Load<Texture2D>(@"Menu\Button_1player");
            Button_TwoPlayers = cm.Load<Texture2D>(@"Menu\Button_2players");

            MainMenuBG = cm.Load<Texture2D>(@"Menu\MainMenu");
            OptionBG = cm.Load<Texture2D>(@"Menu\Option");
            AboutBG = cm.Load<Texture2D>(@"Menu\About");

            UIfont = cm.Load<SpriteFont>(@"Fonts\UIfont");
            UIitemHolder = cm.Load<Texture2D>(@"UI\uiitems");
            UIitemSelected = cm.Load<Texture2D>(@"UI\uiselected");

            roche_herbe = cm.Load<Texture2D>(@"Sprites\herbe_roche");
            halfsable = cm.Load<Texture2D>(@"Sprites\halfsable");
        }

    }
}
