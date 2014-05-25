using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TryAgain.Characters;

namespace TryAgain.GameElements
{
    class Skills
    {
        //proprietes
        static bool shield = false;

        //constructor

        //methods
        public static void Shield(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState)
        {
            if (keyBoardState.IsKeyDown(Keys.S) && !shield)
            {
                shield = true;
                hero.stats.defense += 20;//stats a changées
            }
            if (!keyBoardState.IsKeyDown(Keys.S) && shield)
            {
                shield = false;
                hero.stats.defense -= 20;
            }
            if (shield)
            {
                sb.Draw(Textures.Shield, new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), null, Color.White, 0f, Vector2.Zero,
    new Vector2(64.0F / (float)(Textures.Shield.Width), 64.0F / (float)(Textures.Shield.Height)), SpriteEffects.None, 0f);

            }
        }

        //update draw
        static public void Draw(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState)
        {
            Shield(sb, pos, hero, keyBoardState);
        }
    }
}
