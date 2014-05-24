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
using TryAgain.GameElements.misc;

namespace TryAgain.GameElements
{
    abstract class GameObject
    {
        public static Dictionary<String, GameObject> GobjectList = new Dictionary<string,GameObject>();
        public readonly String Type;
        //public readonly String UID; // Unique IDentifier
        public String UID; // Unique IDentifier
        public Vector2 position;
        protected Vector2 movingTo;
        protected Vector2 speed;

        protected Vector2 size;
        public bool ticked, toupdate = true;
        public float X, Y;
        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getSize()
        {
            return size;
        }

        public GameObject(String type, String UID)
        {
            this.Type = "GameObject," + type;
            int i = 0;
            while(GobjectList.ContainsKey(UID + i.ToString()))
                ++i;
            this.UID = UID + i.ToString();
            GobjectList.Add(this.UID, this);
        }

        public static void Delete(ref GameObject gob)
        {
            GobjectList.Remove(gob.UID);
            gob = null;
        }

        public virtual void update()
        {
            if ((this.speed != null) && (this.movingTo != null) && (this.speed != Vector2.Zero))
            {
                /*
                if (new Rectangle((int)(128 * this.position.X), (int)(128 * this.position.Y), (int)(128 * this.speed.X), (int)(128 * this.speed.Y)).Intersects(
                    new Rectangle((int)(128 * this.movingTo.X), (int)(128 * this.movingTo.Y), 1, 1))) // Ca marche pas tout le temps, faire en sorte de s'arréter lorsque l'on est au niveau du point vers lequel on va.
                
                 */
                if((((this.speed.X >= 0 ) && (this.movingTo.X >= this.position.X) && (this.movingTo.X <= this.position.X + this.speed.X)) ||
                    ((this.speed.X < 0 ) && (this.movingTo.X <= this.position.X) && (this.movingTo.X >= this.position.X + this.speed.X))) && 
                    (((this.speed.Y >= 0 ) && (this.movingTo.Y >= this.position.Y) && (this.movingTo.Y <= this.position.Y + this.speed.Y)) ||
                    ((this.speed.Y < 0 ) && (this.movingTo.Y <= this.position.Y) && (this.movingTo.Y >= this.position.Y + this.speed.Y))))

                {
                    this.speed = Vector2.Zero;
                    this.position = this.movingTo;
                }
                else if (this.position != this.movingTo)
                    this.position += this.speed;
                
            }
        }

        public abstract void Draw(SpriteBatch sb);
        protected bool exists = true;
        public bool toRemove()
        {
            return exists;
        }

        public void TravelTo(Vector2 position, float speed)
        {
            this.movingTo = position;
            this.speed = this.movingTo - this.position;
            this.speed.Normalize();
            this.speed *= speed;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
            this.X = position.X;
            this.Y = position.Y;
        }

        public abstract void jsonUpdate(string json);
    }
}
