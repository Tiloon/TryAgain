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
using TryAgain.GameStates;
using TryAgain.Datas;
using TryAgain.Online;

namespace TryAgain.GameElements
{
    class Ressource
    {
        int id;
        Texture2D item;
        int x;
        int y;

        Dictionary<int, Texture2D> idtoitem;
        public void init()
        {
            idtoitem.Add(0, Textures.O);
            idtoitem.Add(1, Textures.I);
            idtoitem.Add(2, Textures.II);
            idtoitem.Add(3, Textures.III);
            idtoitem.Add(4, Textures.IV);
            idtoitem.Add(5, Textures.V);
            idtoitem.Add(6, Textures.VI);
            idtoitem.Add(7, Textures.VII);
            idtoitem.Add(8, Textures.VIII);
            idtoitem.Add(9, Textures.IX);
            idtoitem.Add(10, Textures.X);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(item, new Rectangle (x, y, item.Width, item.Height), Color.White);
        }

        public void Update(Rectangle persoRectangle)
        {
        }
    }
}
