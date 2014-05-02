using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TryAgain.GameElements.Map___environnement
{
    class GobParticle : GameObject // Ammo and stuff
    {
        private Texture2D tx;
        private Vector2 v;

        public GobParticle(string UID, Vector2 pos, Vector2 v)
            : base("particle,", UID)
        {
            this.position = pos;
            this.v = v;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(tx, position, Color.White);
        }

        public override void update()
        {
            position += v;
        }

        public override void jsonUpdate(String json)
        {
        }
    }
}
