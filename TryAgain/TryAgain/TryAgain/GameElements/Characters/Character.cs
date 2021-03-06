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
using TryAgain.GameElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TryAgain.Characters
{
    enum Direction { haut, bas, gauche, droite }
    abstract class Character : GameObject
    {
        public CharacterStats stats;
        public CharacterStats getStats()
        {
            return stats;
        }
        public Texture2D apparence;
        public string name;
        public int longueur, largeur;
        public override void Draw(SpriteBatch sb)
        {
            if ((position.X >= (float)(Hero.view.X) + Hero.padding.X - 1) &&
               (position.Y >= (float)(Hero.view.Y) + Hero.padding.Y - 1) &&
               (position.X <= (float)(Hero.view.X + Hero.view.Width) + Hero.padding.X) &&
               (position.Y <= (float)(Hero.view.Y + Hero.view.Height) + Hero.padding.Y))
            {
                if (this.name == "KRIS")
                {
                    sb.Draw(apparence, new Vector2((position.X - (Hero.view.X + Hero.padding.X)) * 64, (position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), null, Color.White, 0f, Vector2.Zero,
                        new Vector2(128.0F / (float)(apparence.Width), 128.0F / (float)(apparence.Height)), SpriteEffects.None, 0f);

                }else {
                    sb.Draw(apparence, new Vector2((position.X - (Hero.view.X + Hero.padding.X)) * 64, (position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), null, Color.White, 0f, Vector2.Zero,
                        new Vector2(96.0F / (float)(apparence.Width), 96.0F / (float)(apparence.Height)), SpriteEffects.None, 0f);
                }
            }
                //sb.Draw(apparence, new Vector2((position.X - (Hero.view.X + Hero.padding.X)) * 64 + 64 * 4, (position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), Color.White);
        }

        public override void TakeDamages(int points)
        {
            GameOver.actualpoints += points / 2;
            if (Online.Connection.isOnline())
                Online.Connection.SendDamage(this.UID, points);
            this.stats.lp -= points;
        }

        public override void update()
        {
            base.update();
            /*this.position = new Vector2(this.X, this.Y) - this.size / 2;*/
            if (this.stats.lp <= 0)
            {
                this.exists = false;
            }
        }

        public Character(String type, String UID) : base("Character," + type, UID)
        {
        }

        public override void jsonUpdate(string json)
        {
            // Stats should be updated here
        }


        public bool Collision(Rectangle truc, Direction dir)
        {
            if (dir == Direction.haut)
                return new Rectangle((int)position.X, (int)position.Y -5, largeur, longueur).Intersects(truc);
            else if (dir== Direction.droite)
                return new Rectangle((int)position.X, (int)position.Y, largeur + 5, longueur).Intersects(truc);
            else if (dir == Direction.gauche)
                return new Rectangle((int)position.X-5, (int)position.Y, largeur, longueur).Intersects(truc);
            else
                return new Rectangle((int)position.X, (int)position.Y, largeur, longueur+5).Intersects(truc);
        }
    }
}
