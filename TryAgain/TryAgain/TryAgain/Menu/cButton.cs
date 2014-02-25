using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TryAgain.Menu
{
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;


        public Vector2 size;
        public cButton(Texture2D newtexture, GraphicsDevice graphics)
        {
            texture = newtexture;
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 10);   // règle la taille en fonction de celle de l'immage


            //size = new Vector2(610, 150);
        }


        public bool isClicked;


        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                isClicked = mouse.LeftButton == ButtonState.Pressed;
            }
        }

        public void SetPosition(Vector2 newpos)
        {
            position = newpos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
