﻿using System;
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
using TryAgain.Characters;

namespace TryAgain.GameElements.misc
{
    class UI
    {
        Color statcolor = Color.Blue;
        Color caracolor = Color.Crimson;
        int lp, lpmax;
        int cp, cpmax;
        int mp, mpmax;
        int lvl;
        int force, intelligence, defense, criticalrate, speed;

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(Textures.UIfont, "health : " + lp.ToString() + "/" + lpmax.ToString(), new Vector2(15, 12), statcolor);
            sb.DrawString(Textures.UIfont, "cafeine : " + cp.ToString() + "/" + cpmax.ToString(), new Vector2(15, 32), statcolor);
            sb.DrawString(Textures.UIfont, "mental : " + mp.ToString() + "/" + mpmax.ToString(), new Vector2(15, 52), statcolor);
            sb.DrawString(Textures.UIfont, "Caracteristiques :", new Vector2(15, 100), caracolor);
            sb.DrawString(Textures.UIfont, "lvl :" + lvl.ToString(), new Vector2(15, 120), caracolor);
            sb.DrawString(Textures.UIfont, "force :" + force.ToString(), new Vector2(15, 140), caracolor);
            sb.DrawString(Textures.UIfont, "intelligence :" + intelligence.ToString(), new Vector2(15, 160), caracolor);
            sb.DrawString(Textures.UIfont, "defense :" + defense.ToString(), new Vector2(15, 180), caracolor);
            sb.DrawString(Textures.UIfont, "criticalrate :" + criticalrate.ToString(), new Vector2(15, 200), caracolor);
            sb.DrawString(Textures.UIfont, "speed :" + speed.ToString(), new Vector2(15, 220), caracolor);
        }
        public void update(ref Hero hero)
        {
            lp = hero.getStats().lp;
            lpmax = hero.getStats().lpmax;
            cp = hero.getStats().ch;
            cpmax = hero.getStats().chmax;
            mp = hero.getStats().mh;
            mpmax = hero.getStats().mhmax;

            lvl = hero.getStats().lvl;
            force = hero.getStats().force;
            intelligence = hero.getStats().intelligence;
            defense = hero.getStats().defense;
            criticalrate = hero.getStats().criticalrate;
            speed = hero.getStats().speed;
        }
    }
}
