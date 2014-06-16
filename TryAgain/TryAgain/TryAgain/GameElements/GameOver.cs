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
using Newtonsoft.Json;
using TryAgain.Datas;
using System.Windows.Forms;
using TryAgain.Sounds;

namespace TryAgain.GameElements
{
    enum Ranks{bronze, argent, or}
    class GameOver
    {
        static int actualpoints = 3;

        public static int PointsObtenus(GameTime gmt)
        {
            return actualpoints + (int)gmt.ElapsedGameTime.TotalSeconds; //+ durée/10 + nbmobtués/5 +... 
        }

        public static Ranks Rang(int pts)
        {
            if (pts > 100)
                return Ranks.or;
            else if (pts > 42)
                return Ranks.argent;
            else
                return Ranks.bronze;
        }

        public static void Draw(SpriteBatch sb, GameTime gmt)     //faut que je pense a uploader des images de rang
        {
            sb.Draw(Textures.GameOver, new Vector2(Tilemap.variationsizegraphicsX, 0), Color.White);
            sb.DrawString(Textures.UIfont, "      POINTS: " + PointsObtenus(gmt).ToString() , new Vector2(Tilemap.variationsizegraphicsX + 64*5, 120 + 64 * 10), Color.Red);
            sb.DrawString(Textures.UIfont, "      RANG: " + Rang(PointsObtenus(gmt)).ToString(), new Vector2(Tilemap.variationsizegraphicsX + 64*5, 120 + 64 * 11), Color.Red);
        }
    }
}
