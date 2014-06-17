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
using System.Net;

namespace TryAgain.GameElements
{
    enum Ranks { bronze, argent, or }
    class GameOver
    {
        static int actualpoints = 3;
        static bool lose = false;
        public static int PointsObtenus(GameTime gmt)
        {
            return actualpoints + (int)gmt.TotalGameTime.TotalSeconds; //+ durée/10 + nbmobtués/5 +... 
        }

        public static void SendScore(int pts)
        {
            if (!lose)
            {
                WebClient myWebClient = new WebClient();
                try
                {
                    if ((Online.Connection.UserID.IndexOf('&') >= 0) ||
                        (Online.Connection.UserID.IndexOf('?') >= 0) ||
                        (Online.Connection.UserID.IndexOf('<') >= 0) ||
                        (Online.Connection.UserID.IndexOf('>') >= 0) ||
                        (Online.Connection.UserID.IndexOf(' ') >= 0) ||
                        (Online.Connection.UserID.IndexOf('\n') >= 0) ||
                        (Online.Connection.UserID.IndexOf('=') >= 0))
                        throw new Exception("Invalid name");

                    myWebClient.DownloadData("http://tryagain.pimzero.com/highscore.php?u=" + Online.Connection.UserID + "&s=" + pts.ToString());
                }
                catch (Exception e)
                {
                    TryAgain.GameElements.misc.Debug.Show(e.Message);
                }
            }
            lose = true;
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
            sb.Draw(Textures.GameOver, new Vector2(Game1.graphics.PreferredBackBufferWidth / 2, Game1.graphics.PreferredBackBufferHeight / 2), Color.White);
            sb.DrawString(Textures.UIfont, "      POINTS: " + PointsObtenus(gmt).ToString(), new Vector2(Tilemap.variationsizegraphicsX + 64 * 5, 120 + 64 * 10), Color.Red);
            sb.DrawString(Textures.UIfont, "      RANG: " + Rang(PointsObtenus(gmt)).ToString(), new Vector2(Tilemap.variationsizegraphicsX + 64 * 5, 120 + 64 * 11), Color.Red);
            SendScore(PointsObtenus(gmt));
        }
    }
}
