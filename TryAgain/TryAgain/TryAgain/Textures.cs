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
        public static void load(ContentManager cm)
        {
            herbe_texture = cm.Load<Texture2D>(@"Sprites\tileherbe"); //0
            sable_texture = cm.Load<Texture2D>(@"Sprites\tilesable"); //1
            persopierre_texture = cm.Load<Texture2D>(@"Sprites\persopierre");
        }
    }
}
