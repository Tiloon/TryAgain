using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TryAgain.GameStates;
using TryAgain.Datas;
using TryAgain.Online;

namespace TryAgain
{
    class scenar
    {
        public static void Update(bool skip)
        {
        }

        public static void Drawlancement(SpriteBatch sb, GameTime gt, bool skip)
        {
            if (!skip)
            {
                if (gt.TotalGameTime.Seconds < 3)
                    sb.Draw(Textures.scenar1, Vector2.Zero, Color.White);
                else if (gt.TotalGameTime.Seconds < 5)
                    sb.Draw(Textures.scenar2, Vector2.Zero, Color.White);
                else if (gt.TotalGameTime.Seconds < 8)
                    sb.Draw(Textures.scenar3, Vector2.Zero, Color.White);
                else if (gt.TotalGameTime.Seconds < 11)
                    sb.Draw(Textures.scenar4, Vector2.Zero, Color.White);
                else
                    skip = true;
            }
        }
    }
}
