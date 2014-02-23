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


namespace TryAgain
{
    class Tilemap
    {
        public static int lgmap = 15;
        public static Texture2D[,] map1 = new Texture2D[lgmap, lgmap];
        public static string[,] map1contains = new string[lgmap, lgmap];

        public static void MapFullINIT()
        {
            MapFirstInit(map1, map1contains, Textures.herbe_texture);
            Mapmodify(map1, Textures.sable_texture, 10, lgmap-1, 10, lgmap-1);
        }

        public static void MapFirstInit(Texture2D[,] map, string[,] mapcont, Texture2D basetile)
        {
            for (int i = 0; i < lgmap; i++)
                for (int j = 0; j < lgmap; j++)
                {
                    map[i, j] = basetile;
                    mapcont[i, j] = null;
                }
        }

        public static void Mapmodify(Texture2D[,] map, Texture2D newtile, int imin, int imax, int jmin, int jmax) //max et min a modifier en fait
        {
            for (int i = imin; i <= imax; i++)
                for (int j = jmin; j <= jmax; j++)
                    map[i, j] = newtile;
        }
        public static void Mapmodify(Texture2D[,] map, Texture2D newtile, int i, int j)
        {
            map[i, j] = newtile;
        }

        public static void Drawmap(SpriteBatch sb, Texture2D[,] map)
        {
            for (int i = 0; i < lgmap; i++)
                for (int j = 0; j < lgmap; j++)
                {
                    sb.Draw(map[i, j], new Vector2(64 * i, 64 * j), Color.White);
                }
        }

        public static bool Walkable(Texture2D tx)
        {
            return (tx == Textures.herbe_texture | tx == Textures.sable_texture);
        }

        public static void Popitem(string[,] mapcontenu, int i, int j, string item)
        {
            mapcontenu[i, j] = item;
        }
    }
}
