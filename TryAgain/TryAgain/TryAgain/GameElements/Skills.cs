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
        static int shieldtimer;
        //constructor

        //methods
        public static void ParticuleCannon(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState) //canon
        {
            if ((keyBoardState.IsKeyDown(Keys.Tab)))
            {
                Game1.cannonpartic = true;
                sb.DrawString(Textures.UIfont, "Cannon!", new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64 + 50, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64 + 50), Color.Blue);
            }
            else
                Game1.cannonpartic = false;
        }
        public static void Coca(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState) //coca
        {
            if ((keyBoardState.IsKeyDown(Keys.M) && (hero.stats.ch < hero.stats.chmax)))
            {
                hero.stats.ch++;
                sb.DrawString(Textures.UIfont, "Mmh coca!", new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64 - 60, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), Color.Brown);
            }
        }
        public static void Missile(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState) //missile
        {
            if (keyBoardState.IsKeyDown(Keys.Space) && !missile)
            {
                missile = true;
                missilepos = new Vector2(Textures.Missile.Width + (hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64);
            }
            if (missile)
            {
                sb.DrawString(Textures.UIfont, "Missile!", new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64 + 50, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), Color.Red);
                sb.Draw(Textures.Missile, missilepos, null, Color.White, 0f, Vector2.Zero,
    new Vector2(64.0F / (float)(Textures.Missile.Width), 64.0F / (float)(Textures.Missile.Height)), SpriteEffects.None, 0f);
                missilepos.X += missilespeed;
                foreach (GameObject obj in GameScreen.GOList)
                {
                    if (/*(obj.Type == "GameObject,Character,Monster") && */
                       (new Rectangle((int)missilepos.X, (int)missilepos.Y, Textures.Missile.Width, Textures.Missile.Height).Intersects
                       (new Rectangle((int)(obj.position.X - (Hero.view.X + Hero.padding.X) * 64), (int)(obj.position.Y - (Hero.view.Y + Hero.padding.Y) * 64), 60, 60))))
                    {
                        obj.pv -= 25;
                        missile = false;
                    }
                }
                if (missilepos.X - missilespeed > Game1.graphics.PreferredBackBufferWidth)
                    missile = false;
                /*if (keyBoardState.IsKeyDown(Keys.Right) && (missilepos.X > 0))
                    missilepos.X += 20 * hero.getStats().speed;*/
                if (keyBoardState.IsKeyDown(Keys.Left) && (missilepos.X < Game1.graphics.PreferredBackBufferWidth))
                    missilepos.X -= 20 * hero.getStats().speed;
                if (keyBoardState.IsKeyDown(Keys.Up) && (missilepos.Y > 0))
                    missilepos.Y += 20 * hero.getStats().speed;
                if (keyBoardState.IsKeyDown(Keys.Down) && (missilepos.Y < Game1.graphics.PreferredBackBufferHeight))
                    missilepos.Y -= 20 * hero.getStats().speed;
            }
        }

        public static void Shield(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState) //shield
        {
            if (keyBoardState.IsKeyDown(Keys.S) && !shield)
            {
                shield = true;
                shieldtimer = 1;
                hero.stats.defense += 20;//stats a changées
            }
            if (!keyBoardState.IsKeyDown(Keys.S) && shield)
            {
                shield = false;
                hero.stats.defense -= 20;
            }
            if (shield)
            {
                if (GameStates.OptionScreen.eng)
                    sb.DrawString(Textures.UIfont, "Armor up!", new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64 - 50, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64 - 40), Color.Red);
                else
                    sb.DrawString(Textures.UIfont, "Armure +!", new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64 - 50, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64 - 40), Color.Red);
                if (shieldtimer < 10)
                {
                    //sb.Draw(Textures.Shield1, new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), null, Color.White, 0f, Vector2.Zero,
        //new Vector2(64.0F / (float)(Textures.Shield1.Width), 64.0F / (float)(Textures.Shield1.Height)), SpriteEffects.None, 0f);
                    sb.Draw(Textures.Shield1, new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), null, Color.White, 0f, Vector2.Zero,
    new Vector2(96.0F / (float)(Textures.Shield1.Width), 96.0F / (float)(Textures.Shield1.Height)), SpriteEffects.None, 0f); 
                    shieldtimer++;
                }
                if (shieldtimer >= 10)
                {
                    sb.Draw(Textures.Shield2, new Vector2((hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64), null, Color.White, 0f, Vector2.Zero,
    new Vector2(96.0F / (float)(Textures.Shield2.Width), 96.0F / (float)(Textures.Shield2.Height)), SpriteEffects.None, 0f); 
                    shieldtimer++;
                }
                if (shieldtimer == 20)
                    shieldtimer = 1;
            }
        }

        public static void Boots(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState) //boots
        {
            if ((keyBoardState.IsKeyDown(Keys.LeftControl) && (hero.stats.speed < 0.65F)))
                hero.stats.speed += 0.01F;
            if ((keyBoardState.IsKeyDown(Keys.LeftShift) && (hero.stats.speed > 0.15F)))
                hero.stats.speed -= 0.01F;
        }

        //update draw
        static public void Draw(SpriteBatch sb, Vector2 pos, Hero hero, KeyboardState keyBoardState)
        {
            Shield(sb, pos, hero, keyBoardState);
            Missile(sb, pos, hero, keyBoardState);
            Boots(sb, pos, hero, keyBoardState);
            Coca(sb, pos, hero, keyBoardState);
            ParticuleCannon(sb, pos, hero, keyBoardState);
        }
    }
}
