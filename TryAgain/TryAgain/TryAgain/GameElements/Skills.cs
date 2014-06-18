using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TryAgain.Characters;
using TryAgain.GameStates;

namespace TryAgain.GameElements
{
    class Skills
    {
        //proprietes
        static bool shield = false;
        static bool missile = false;
        static Vector2 missilepos;
        static int missilespeed = 23;
        //constructor

        //methods

        public static void Missile(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState)
        {
            if (keyBoardState.IsKeyDown(Keys.Space) && !missile)
            {
                missile = true;
                missilepos = new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64);
            }
            if (missile)
            {
                sb.Draw(Textures.Missile, missilepos, null, Color.White, 0f, Vector2.Zero,
    new Vector2(64.0F / (float)(Textures.Missile.Width), 64.0F / (float)(Textures.Missile.Height)), SpriteEffects.None, 0f);
                missilepos.X += missilespeed;
                foreach (GameObject obj in GameScreen.GOList)
                {
                    if (/*(obj.Type == "GameObject,Character,Monster") && */
                       (new Rectangle((int)missilepos.X, (int)missilepos.Y, Textures.Missile.Width, Textures.Missile.Height).Intersects
                       (new Rectangle((int)(obj.position.X - (Hero.view.X + Hero.padding.X) *64), (int)(obj.position.Y - (Hero.view.Y + Hero.padding.Y) * 64), 60, 60))))
                    {
                        obj.pv -= 25;
                        missile = false;
                    }
                }
                if (missilepos.X - missilespeed > Game1.graphics.PreferredBackBufferWidth)
                    missile = false;
            }
        }

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

        public static void Boots(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState)
        {
            if((keyBoardState.IsKeyDown(Keys.LeftControl) && (hero.stats.speed < 0.65F)))
                hero.stats.speed+=0.01F;
            if((keyBoardState.IsKeyDown(Keys.LeftShift) && (hero.stats.speed > 0.15F)))
                hero.stats.speed-=0.01F;
                
        }

        //update draw
        static public void Draw(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState)
        {
            Shield(sb, pos, hero, keyBoardState);
            Missile(sb, pos, hero, keyBoardState);
            Boots(sb, pos, hero, keyBoardState);
        }
    }
}
