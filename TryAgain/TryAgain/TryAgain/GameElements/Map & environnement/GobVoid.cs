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

namespace TryAgain.GameElements.Map___environnement
{
    class GobVoid : GameObject
    {
        public GobVoid(Vector2 pos)
            : base("void", "void")
        {
            this.position = pos;
            this.size = new Vector2(1, 1);
        }
        public override void update()
        {

            this.exists = false;
        }
        public override void jsonUpdate(String json)
        {

        }
        public override void Draw(SpriteBatch sb)
        {
            
        }
    }
}
