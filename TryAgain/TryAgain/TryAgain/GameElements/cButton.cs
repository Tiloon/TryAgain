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
            size = new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 10);   // règle la taille en fonction de celle de l'image
        }
        public cButton(Texture2D newtexture, GraphicsDevice graphics, int width, int height)
        {
            texture = newtexture;
            size = new Vector2(width, height);   // surcharge pour choisir la taille du boutton
        }

        public bool IsClicked(MouseState mouse)  //retourne vrai si intersection bouton-souris
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouse_rectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            return ((mouse_rectangle.Intersects(rectangle)) && (mouse.LeftButton == ButtonState.Pressed));
        }

        public bool IsReleased(MouseState mouse)  //retourne vrai si intersection bouton-souris
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouse_rectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            return ((mouse_rectangle.Intersects(rectangle)) && (mouse.LeftButton == ButtonState.Released));
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
