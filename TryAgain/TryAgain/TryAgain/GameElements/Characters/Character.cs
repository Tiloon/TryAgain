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

namespace TryAgain.Characters
{
    abstract class Character
    {
        protected Vector2 position;
        protected Texture2D apparence;
        public string name;
        public int longueur, largeur;
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(apparence, position, Color.White);
        }
        public abstract void update();
        public abstract void jsonUpdate(string json);


        public bool NotCollision(Character perso)
        {
            return (position.X <= perso.position.X) && (position.Y <= perso.position.Y)
                 && (position.X >= position.X + perso.longueur) && (position.Y >= position.Y + perso.largeur);
        }
    }
}
