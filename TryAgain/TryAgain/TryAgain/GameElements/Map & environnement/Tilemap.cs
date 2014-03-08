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


namespace TryAgain
{
    class Tilemap
    {
        public static int variationsizegraphicsX = 64 * 4; // contour blanc en largeur de cette longueur à gauche et à droite
                                                           // si vous voulez pas de contour, mettez la à 0
        public static int lgmap = 15;
        public static int lgbigmapunscrolled = 100;

        public static Texture2D[,] map1 = new Texture2D[lgmap, lgmap];
        public static Item[,] map1contains = new Item[lgmap, lgmap];
        public static Monster[,] map1monsters = new Monster[lgmap, lgmap];

        public static Texture2D[,] map2 = new Texture2D[lgbigmapunscrolled, lgbigmapunscrolled];
        public static Item[,] map2contains = new Item[lgbigmapunscrolled, lgbigmapunscrolled];
        public static Monster[,] map2monsters = new Monster[lgbigmapunscrolled, lgbigmapunscrolled];

        public static void MapFullINIT()
        {
            //map1 : une map non scrollée qui fait la longueur de l'écran, ni plus ni moins, minimap classique
            MapFirstInit(map1, Textures.herbe_texture);
            Mapmodify(map1, Textures.sable_texture, 7, lgmap-1, 10, lgmap-1);
            Mapmodify(map1, Textures.solrocailleux_texture, 0, 6, lgmap-2, lgmap-1);
            Mapmodify(map1, Textures.aqua_halfwkbtexture, lgmap-2, lgmap-1, 0, 3);
            Mapmodify(map1, Textures.cascadegauche_unwkbtexture, lgmap-2, 0);
            Mapmodify(map1, Textures.cascadedroite_unwkbtexture, lgmap-1, 0);

            //map2 : grosse et scrollable
            MapFirstInit(map2, Textures.solrocailleux_texture);

        }

        public static void MapFirstInit(Texture2D[,] map, Texture2D basetile)
        {
            for (int i = 0; i < lgmap; i++)
                for (int j = 0; j < lgmap; j++)
                {
                    map[i, j] = basetile;
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
                    sb.Draw(map[i, j], new Vector2(variationsizegraphicsX + 64 * i, 64 * j), Color.White);
                }
        }

        public static void Drawmap(SpriteBatch sb, Texture2D[,] map, int scale)
        {
        }

        public static bool Walkable(Texture2D tx)
        {
            return (tx == Textures.herbe_texture | tx == Textures.sable_texture | tx == Textures.neige_texture | tx == Textures.solrocailleux_texture);
        }

        public static void Popitem(string[,] mapcontenu, int i, int j, string item)
        {
            mapcontenu[i, j] = item;
        }
    }
}
