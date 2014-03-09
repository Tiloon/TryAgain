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
            sb.Draw(apparence, position, Color.White);
        }

        public override void update()
        {
            if (this.stats.lp <= 0)
            {
                this.exists = false;
            }
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
        public bool NotCollision(Character perso2, Direction direction, Vector2 newpos)
        {
            if (direction == Direction.haut)
                return (newpos.Y < perso2.position.Y + perso2.longueur);
            else if (direction == Direction.bas)
                return (newpos.Y <= perso2.position.Y);
            else if (direction == Direction.gauche)
                return (newpos.X >= perso2.position.X + perso2.largeur);
            else
                return (newpos.X <= perso2.position.X);
        }
        public bool NotCollision(Character perso2, Direction direction)
        {
            if (direction == Direction.haut)
                return (position.Y < perso2.position.Y + perso2.longueur);
            else if (direction == Direction.bas)
                return (position.Y <= perso2.position.Y);
            else if (direction == Direction.gauche)
                return (position.X >= perso2.position.X + perso2.largeur);
            else
                return (position.X <= perso2.position.X);
        }
    }
}
