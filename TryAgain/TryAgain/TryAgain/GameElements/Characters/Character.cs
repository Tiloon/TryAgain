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
            sb.Draw(apparence, position, Color.White);
        }

        public override void update()
        {
            if (this.stats.lp <= 0)
                this.exists = false;
        }

        public Character(String type, String UID) : base("Character" + type, UID)
        {
        }

        public override void jsonUpdate(string json)
        {
            // Stats should be updated here
        }

        public bool NotCollision(Character perso)
        {
            return (position.X <= perso.position.X) && (position.Y <= perso.position.Y)
                 && (position.X >= position.X + perso.longueur) && (position.Y >= position.Y + perso.largeur);
        }
    }
}
