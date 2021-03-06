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
using TryAgain.Characters;
using TryAgain.GameStates;

namespace TryAgain.GameElements.misc
{
    class UI
    {
        public Color statcolor = Color.Blue;
        public Color caracolor = Color.Crimson;
        public int lp, lpmax;
        public int cp, cpmax;
        public int mp, mpmax;
        public int lvl;
        public int equiped;
        public int force, intelligence, defense, criticalrate;
        public float speed;
        Item[] items = new Item[10];

        public void Draw(SpriteBatch sb)
        {
            //Textures.DrawRectangle(sb, new Rectangle(0, 0, Tilemap.variationsizegraphicsX, 15 * 64), Color.White);
            //Textures.DrawRectangle(sb, new Rectangle(Tilemap.variationsizegraphicsX + 15 * 64, 0, Tilemap.variationsizegraphicsX, 15 * 64), Color.White);
            //sb.DrawString(Textures.UIfont, "health : " + lp.ToString() + "/" + lpmax.ToString(), new Vector2(15, 12), statcolor);
            //Cafeine (TODO : Change lp <-> cp)
            if (cp>0)
            {
                if(cp < 40)
                    Textures.DrawRectangle(sb, new Rectangle(0, 0, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height), Color.Black * (1.0f - (float)cp / 40.0F));

                int width = (int)((sb.GraphicsDevice.Viewport.Width / 2) * 3 * (1 + 3 * (float)cp / (float)cpmax) / 4), height = (int)((sb.GraphicsDevice.Viewport.Height / 2) * 3 * (1 + 3 * (float)cp / (float)cpmax) / 4);
                sb.Draw(Textures.fogCafeine, new Rectangle((sb.GraphicsDevice.Viewport.Width - width) / 2, (sb.GraphicsDevice.Viewport.Height - height) / 2, width, height), Color.White);
                Textures.DrawRectangle(sb, new Rectangle(0, 0, (sb.GraphicsDevice.Viewport.Width - width) / 2, sb.GraphicsDevice.Viewport.Height), Color.Black);
                Textures.DrawRectangle(sb, new Rectangle(0, 0, sb.GraphicsDevice.Viewport.Width, (sb.GraphicsDevice.Viewport.Height - height) / 2), Color.Black);
                Textures.DrawRectangle(sb, new Rectangle((sb.GraphicsDevice.Viewport.Width + width) / 2, 0, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height), Color.Black);
                Textures.DrawRectangle(sb, new Rectangle(0, (sb.GraphicsDevice.Viewport.Height + height) / 2, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height), Color.Black);
            }

            // Health
            sb.Draw(Textures.healthGauge[0], new Rectangle(sb.GraphicsDevice.Viewport.Width - 136, 4, 64, 64), Color.White);
            if ((lpmax > 0) && (lp <= lpmax) && (lp > 0))
            {
                int max = (lp * 8) / lpmax;
                for (int i = 0; i < max; i++)
                    sb.Draw(Textures.healthGauge[i + 1], new Rectangle(sb.GraphicsDevice.Viewport.Width - 136, 4, 64, 64), Color.White);
                if (max < 8)
                {
                    int fraction = lpmax / 8;
                    sb.Draw(Textures.healthGauge[max + 1], new Rectangle(sb.GraphicsDevice.Viewport.Width - 136, 4, 64, 64), Color.White * (((float)(lp - max * fraction)) / ((float)fraction)));
                }
            }

            // Mana
            sb.Draw(Textures.manaGauge[0], new Rectangle(sb.GraphicsDevice.Viewport.Width - 68, 4, 64, 64), Color.White);
            if ((mpmax > 0) && (mp <= mpmax) && (mp > 0))
            {
                int max = (mp * 8) / mpmax;
                for (int i = 0; i < max; i++)
                    sb.Draw(Textures.manaGauge[i + 1], new Rectangle(sb.GraphicsDevice.Viewport.Width - 68, 4, 64, 64), Color.White);
                if (max < 8)
                {
                    int fraction = mpmax / 8;
                    sb.Draw(Textures.manaGauge[max + 1], new Rectangle(sb.GraphicsDevice.Viewport.Width - 68, 4, 64, 64), Color.White * (((float)(mp - max * fraction)) / ((float)fraction)));
                }
            }

            /*
            for (int i = 0; i < 10; i++)
            {
                sb.Draw(Textures.UIitemHolder, new Rectangle(300 + 80*i, 884, 64, 64), Color.White);
                if(equiped == i)
                    sb.Draw(Textures.UIitemSelected, new Rectangle(300 + 80 * i, 884, 64, 64), Color.White);
                if (items[i] != null)
                {
                    if (items[i].exists)
                        sb.Draw(items[i].getIcon(), new Rectangle(300 + 80 * i, 884, 64, 64), Color.White);
                    else
                        items[i] = Item.voidItem;
                }
            }*/

            /*
            if (false) // Debug shit
            {
                for (int i = 0; i < GameScreen.GOList.Count; i++)
                {
                    //sb.DrawString(Textures.UIfont, GameScreen.GOList[i].UID, new Vector2(1280, 220 + 20*i), caracolor);
                }

                for (int i = 0; i < GameObject.GobjectList.Count; i++)
                {
                    sb.DrawString(Textures.UIfont, GameObject.GobjectList.ToList()[i].Value.UID, new Vector2(1280, 220 + 20 * i), caracolor);
                }
            }*/
        }
        public void update(ref Hero hero)
        {
            MouseState mouse = Mouse.GetState();
            Rectangle mouse_rectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            for (int i = 0; i < 10; i++)
            {
                Rectangle rectangle = new Rectangle(300 + 80 * i, 884, 64, 64);
                if ((mouse_rectangle.Intersects(rectangle)) && (mouse.LeftButton == ButtonState.Pressed)) {
                    hero.setEquipedItem(i);
                }
            }
                  
            lp = hero.getStats().lp;
            lpmax = hero.getStats().lpmax;
            cp = hero.getStats().ch;
            cpmax = hero.getStats().chmax;
            mp = hero.getStats().mp;
            mpmax = hero.getStats().mpmax;
            equiped = hero.getEquipedItem();
            lvl = hero.getStats().lvl;
            force = hero.getStats().force;
            intelligence = hero.getStats().intelligence;
            defense = hero.getStats().defense;
            criticalrate = hero.getStats().criticalrate;
            speed = hero.getStats().speed;
            items = hero.getItemList();
        }
    }
}
