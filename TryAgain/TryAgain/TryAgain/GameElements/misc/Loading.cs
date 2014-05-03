using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace TryAgain.GameElements.misc
{
    class Loading
    {
        private static int state = 0;
        private const int frames = 12;
        private static bool back = true;

        public static void stopLoading()
        {
            state = 0;
        }

        public static void draw(SpriteBatch sb)
        {
            sb.Draw(Textures.Cache["UILoading_" + state], new Rectangle(0, 0, 1800, 1200), Color.White);


            if ((state >= frames) || (state <= 0))
                back = !back;

            if (back)
                state--;
            else
                state++;
        }
    }
}
