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
using TryAgain.GameElements.Map___environnement;


namespace TryAgain
{
    class Tilemap
    {
        public static int variationsizegraphicsX = 64 * 4; // contour blanc en largeur de cette longueur à gauche et à droite
        // si vous voulez pas de contour, mettez la à 0
        public static int lgmap = 15;
        public static int lgbigmapunscrolled = 100;

        
        //public static Texture2D[,] map1 = new Texture2D[lgmap, lgmap];
        public static Tile[,] tiles = new Tile[lgmap, lgmap];
        public static Item[,] map1contains = new Item[lgmap, lgmap];

        public static Texture2D[,] map2 = new Texture2D[lgbigmapunscrolled, lgbigmapunscrolled];
        public static Item[,] map2contains = new Item[lgbigmapunscrolled, lgbigmapunscrolled];

        private static Dictionary<Texture2D, Texture2D> melanges = new Dictionary<Texture2D, Texture2D>();

        public static void MapFullINIT()
        {
            // Chargement des textures mixtes
            //melanges.Add(Textures.solrocailleux_texture, Textures.roche_herbe);
            //melanges.Add(Textures.sable_texture, Textures.halfsable);

            //map1 : une map non scrollée qui fait la longueur de l'écran, ni plus ni moins, minimap classique
            MapFirstInit(ref tiles, "Therbe");
            Mapmodify(tiles, "Tsable", 7, lgmap - 1, 10, lgmap - 1);
            Mapmodify(tiles, "Tcaillou", 0, 6, lgmap - 2, lgmap - 1);
            Mapmodify(tiles, "Taqua", lgmap - 2, lgmap - 1, 0, 3);

            //map2 : grosse et scrollable


            //MapFirstInit(map2, Textures.solrocailleux_texture);

        }

        public static void MapFirstInit(ref Tile[,] map, String basetile)
        {
            Tile tile = new Tile(basetile);
            for (int i = 0; i < lgmap; i++)
                for (int j = 0; j < lgmap; j++)
                    map[i, j] = tile;
        }

        public static void Mapmodify(Tile[,] map, String newtile, int imin, int imax, int jmin, int jmax) //max et min a modifier en fait
        {
            Tile tile = new Tile(newtile);
            for (int i = imin; i <= imax; i++)
                for (int j = jmin; j <= jmax; j++)
                    map[i, j] = tile;
        }

        public static void Mapmodify(Texture2D[,] map, Texture2D newtile, int i, int j)
        {
            map[i, j] = newtile;
        }

        public static void Drawmap(SpriteBatch sb, Tile[,] map)
        {
            for (int i = 0; i < lgmap; i++)
                for (int j = 0; j < lgmap; j++)
                {
                    //sb.Draw(map[i, j], new Vector2(variationsizegraphicsX + 64 * i, 64 * j), Color.White);
                    sb.Draw(map[i, j].getTexture(), new Rectangle(variationsizegraphicsX + 64 * i, 64 * j, 64, 64), Color.White);
                    if (j >= 1)
                    {
                        if ((map[i, j] != map[i, j - 1]) && melanges.ContainsKey(map[i, j].getTexture()))
                        {
                            sb.Draw(melanges[map[i, j].getTexture()], new Rectangle(variationsizegraphicsX + 64 * i, 64 * j - 64, 64, 64), Color.White);
                        }
                    }
                    if (i >= 1)
                    {
                        if ((map[i, j] != map[i - 1, j]) && melanges.ContainsKey(map[i, j].getTexture()))
                        {
                            sb.Draw(melanges[map[i, j].getTexture()], new Rectangle(variationsizegraphicsX + 64 * i - 64, 64 * j + 64, 64, 64), null, Color.White, (float)-Math.PI / 2, new Vector2(0, 0), SpriteEffects.None, 0f);
                        }
                    }

                }
        }

        public static void Drawmap(SpriteBatch sb, Texture2D[,] map, int scale)
        {
        }

        public static bool Walkable(Tile tx)
        {
            return tx.IsWalkable();
        }

        public static void Popitem(string[,] mapcontenu, int i, int j, string item)
        {
            mapcontenu[i, j] = item;
        }
    }
}
