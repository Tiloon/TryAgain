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

namespace TryAgain
{
    class Sprite
    {
        Vector2 position;
        Texture2D texture;
        public Sprite(Vector2 position)
        {
            this.position = position;
        }
        public Sprite(float x, float y)
        {
            position = new Vector2(x, y);
        }
        public void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }
        public void Update(Vector2 translation)
        {
            position += translation;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
