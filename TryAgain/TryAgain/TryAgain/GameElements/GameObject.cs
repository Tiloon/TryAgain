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
        protected Vector2 position;
        protected Vector2 size;
        public bool ticked;
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

        public abstract void update();
        public abstract void Draw(SpriteBatch sb);
        protected bool exists = true;
        public bool toRemove()
        {
            return exists;
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
