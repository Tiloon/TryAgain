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
using TryAgain.Characters;

namespace TryAgain.GameElements.misc
{
    class UI
    {
        int lp, lpmax;
        int cp, cpmax;
        int mp, mpmax;
        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(Textures.UIfont, "health : " + lp.ToString() + "/" + lpmax.ToString(), new Vector2(15, 12), Color.Blue);
            sb.DrawString(Textures.UIfont, "cafeine : " + cp.ToString() + "/" + cpmax.ToString(), new Vector2(15, 32), Color.Blue);
            sb.DrawString(Textures.UIfont, "mental : " + mp.ToString() + "/" + mpmax.ToString(), new Vector2(15, 52), Color.Blue);
        }
        public void update(ref Hero hero)
        {
            lp = hero.getStats().lp;
            lpmax = hero.getStats().lpmax;
            cp = hero.getStats().ch;
            cpmax = hero.getStats().chmax;
            mp = hero.getStats().mh;
            mpmax = hero.getStats().mhmax;
        }
    }
}
