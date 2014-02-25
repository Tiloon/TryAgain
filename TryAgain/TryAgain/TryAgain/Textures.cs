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

namespace TryAgain
{
    class Textures
    {
        public static Texture2D herbe_texture, sable_texture;
        public static Texture2D persopierre_texture;
        public static Texture2D Button_Play, Button_Option, Button_Exit, Button_About, Button_Return;
        public static Texture2D MainMenuBG;
        public static void load(ContentManager cm)
        {
            herbe_texture = cm.Load<Texture2D>(@"Sprites\tileherbe"); //0
            sable_texture = cm.Load<Texture2D>(@"Sprites\tilesable"); //1
            persopierre_texture = cm.Load<Texture2D>(@"Sprites\persopierre");
            Button_Play = cm.Load<Texture2D>(@"Menu\Button_Play");
            Button_Option = cm.Load<Texture2D>(@"Menu\Button_Option");
            Button_Exit = cm.Load<Texture2D>(@"Menu\Button_Exit");
            Button_About = cm.Load<Texture2D>(@"Menu\Button_About");
            Button_Return = cm.Load<Texture2D>(@"Menu\Button_Return");
            MainMenuBG = cm.Load<Texture2D>(@"Menu\MainMenu");
        }
    }
}
