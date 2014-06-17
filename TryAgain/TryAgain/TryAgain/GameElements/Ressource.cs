﻿using System;
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
        Random rd = new Random();

        public Ressource(int id)
        {
            this.x = rd.Next(Game1.graphics.PreferredBackBufferWidth);
            this.y = rd.Next(Game1.graphics.PreferredBackBufferHeight);
            this.id = id;
            this.item = Ressourceslist.idtoitem[id];
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(item, new Rectangle (x, y, item.Width, item.Height), Color.White);
        }

        public bool Update(Rectangle persoRectangle)   //retourne en plus s'il y'a intersect et dans ce cas, on enlève ressource de liste
        {
            if (persoRectangle.Intersects(new Rectangle(x, y, item.Width, item.Height)))
            {
                Ressourceslist.quantity[id]++;
                return true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && (x > 0))
                x--;
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && (x < Game1.graphics.PreferredBackBufferWidth))
                x++;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && (y > 0))
                y--;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && (y < Game1.graphics.PreferredBackBufferHeight))
                y++;
            return false;
        }
    }
}
