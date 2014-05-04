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
        protected Texture2D apparence;
        public string name;
        public int longueur, largeur;
        public override void Draw(SpriteBatch sb)
        {
            if((position.X >= (float)(Hero.view.X) + Hero.padding.X) && 
               (position.Y >= (float)(Hero.view.Y) + Hero.padding.Y) && 
               (position.X <= (float)(Hero.view.X + Hero.view.Width) + Hero.padding.X) && 
               (position.Y <= (float)(Hero.view.Y + Hero.view.Height) + Hero.padding.Y))
                sb.Draw(apparence, new Vector2((position.X - (Hero.view.X + Hero.padding.X)) * 64 + 64 * 4, (position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), Color.White);
        }

        public override void update()
        {
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
