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

namespace TryAgain.GameElements.Map___environnement
{
    class TextureBlend
    {

        private static bool[][] bits3 = 
        {
            new bool[] {false, true, false, true, false, true, false, false},
            new bool[] {false, true, false, true, false, false, true, false},
            new bool[] {false, true, false, true, false, false, false, true},
            new bool[] {false, true, false, false, true, true, false, false},
            new bool[] {false, true, false, false, true, false, true, false},
            new bool[] {false, true, false, false, true, false, false, true},
            new bool[] {false, true, false, false, false, true, true, false},
            new bool[] {false, true, false, false, false, true, false, true},
            new bool[] {false, false, true, true, false, false, false, true},
            new bool[] {false, false, true, false, true, false, false, true},
            new bool[] {false, false, true, false, false, true, false, true},
            new bool[] {false, false, true, false, false, false, true, true},
            new bool[] {false, false, true, true, false, false, true, false},
            new bool[] {false, false, true, false, true, false, true, false},
            new bool[] {false, false, true, false, false, true, true, false}
        };

        private static bool[][] bits4 = 
        {
            new bool[] {false, true, true, true, false, false, false, true},
            new bool[] {false, true, true, false, true, false, false, true},
            new bool[] {false, true, true, false, false, true, false, true},
            new bool[] {false, true, true, false, false, false, true, true},
            new bool[] {false, true, false, true, true, false, false, true},
            new bool[] {false, true, false, true, false, true, false, true},
            new bool[] {false, true, false, true, false, false, true, true},
            new bool[] {false, true, false, false, true, true, false, true},
            new bool[] {false, true, false, false, true, false, true, true},
            new bool[] {false, true, false, false, false, true, true, true},
            new bool[] {true, true, true, false, false, false, true, false},
            new bool[] {true, true, false, true, false, false, true, false},
            new bool[] {true, true, false, false, true, false, true, false},
            new bool[] {true, true, false, false, false, true, true, false},
            new bool[] {true, false, true, true, false, false, true, false},
            new bool[] {true, false, true, false, true, false, true, false},
            new bool[] {true, false, true, false, false, true, true, false},
            new bool[] {true, false, false, true, true, false, true, false},
            new bool[] {true, false, false, true, false, true, true, false},
            new bool[] {true, false, false, false, true, true, true, false}
        };

        private static bool[][] bits5 = 
        {
            new bool[] {true, false, true, false, true, false, true, true},
            new bool[] {true, false, true, false, true, true, false, true},
            new bool[] {true, false, true, false, true, true, true, false},
            new bool[] {true, false, true, true, false, false, true, true},
            new bool[] {true, false, true, true, false, true, false, true},
            new bool[] {true, false, true, true, false, true, true, false},
            new bool[] {true, false, true, true, true, false, false, true},
            new bool[] {true, false, true, true, true, false, true, false},
            new bool[] {true, true, false, false, true, true, true, false},
            new bool[] {true, true, false, true, false, true, true, false},
            new bool[] {true, true, false, true, true, false, true, false},
            new bool[] {true, true, false, true, true, true, false, false},
            new bool[] {true, true, false, false, true, true, false, true},
            new bool[] {true, true, false, true, false, true, false, true},
            new bool[] {true, true, false, true, true, false, false, true}
        };

        private static bool[] GetBitArray(int x, int y)
        {
            if (x % 2 == 0)
                return bits4[(Math.Abs(x + y)) % bits4.Length];
            else
            {
                if (y % 2 == 0)
                    return bits3[(Math.Abs(x + y)) % bits3.Length];
                else
                    return bits5[(Math.Abs(x + y)) % bits5.Length];
            }
        }

        private static Texture2D BlendAddL(Texture2D txa, Texture2D txb, bool[] bitArray)
        {
            var graphics = txa.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, txa.Bounds.Width, txa.Bounds.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            sb.Draw(txa, txa.Bounds, Color.White);
            for (int i = 0; i < bitArray.Length; i++)
                if (bitArray[i])
                    sb.Draw(txb, new Rectangle(i * 8, 0, 8, 8), Color.White);
            sb.End();

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }

        private static Texture2D BlendAddD(Texture2D txa, Texture2D txb, bool[] bitArray)
        {
            var graphics = txa.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, txa.Bounds.Width, txa.Bounds.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            sb.Draw(txa, txa.Bounds, Color.White);
            for (int i = 0; i < bitArray.Length; i++)
                if (bitArray[i])
                    sb.Draw(txb, new Rectangle(0, i * 8, 8, 8), Color.White);
            sb.End();

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }

        public static Texture2D DrawL(Texture2D txa, Texture2D txb, int x, int y)
        {
            bool[] bl1 = GetBitArray(x, y);
            bool[] bl2 = GetBitArray((x + 3) * 2, (y - 5) * 3);
            bool[] bl = new bool[bl1.Length + bl2.Length];

            for (int i = 0; i < bl1.Length; i++)
                bl[i] = bl1[i];

            for (int i = 0; i < bl2.Length; i++)
                bl[i + bl1.Length] = bl2[i];

            txa = BlendAddL(txa, txb, bl);
            return txa;
        }

        public static Texture2D DrawD(Texture2D txa, Texture2D txb, int x, int y)
        {
            bool[] bl1 = GetBitArray(x, y);
            bool[] bl2 = GetBitArray((x + 3) * 2, (y - 5) * 3);
            bool[] bl = new bool[bl1.Length + bl2.Length];

            for (int i = 0; i < bl1.Length; i++)
                bl[i] = bl1[i];

            for (int i = 0; i < bl2.Length; i++)
                bl[i + bl1.Length] = bl2[i];

            txa = BlendAddD(txa, txb, bl);
            return txa;
        }
    }
}
