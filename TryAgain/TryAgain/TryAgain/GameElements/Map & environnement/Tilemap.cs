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
using Newtonsoft.Json;


namespace TryAgain
{
    class Tilemap
    {
        public static int variationsizegraphicsX = 0; // contour blanc en largeur de cette longueur à gauche et à droite
        // si vous voulez pas de contour, mettez la à 0
        public static int lgmap = 30;
        public static int lgbigmapunscrolled = 100;


        //public static Texture2D[,] map1 = new Texture2D[lgmap, lgmap];
        public static Tile[,] tiles = new Tile[32, 32];
        public static Item[,] map1contains = new Item[lgmap + 3, lgmap + 3];

        public static Texture2D[,] map2 = new Texture2D[lgbigmapunscrolled, lgbigmapunscrolled];
        public static Item[,] map2contains = new Item[lgbigmapunscrolled, lgbigmapunscrolled];

        private static Dictionary<Texture2D, Texture2D> melanges = new Dictionary<Texture2D, Texture2D>();

        public static void MapLoadFromJSON(String JSON)
        {
            String[,] mapArray = JsonConvert.DeserializeObject<String[,]>(JSON);
            tiles = new Tile[mapArray.GetLength(0), mapArray.GetLength(1)];
            for (int i = 0; i < mapArray.GetLength(0); i++)
                for (int j = 0; j < mapArray.GetLength(1); j++)
                    if ((mapArray[i, j] != null) && (mapArray[i, j] != ""))
                        MapSetTile(tiles, mapArray[i,j], i, j);
        }

        public static void MapFullINIT()
        {
            if (!Online.Connection.isOnline())
            {
                MapFirstInit(ref tiles, "Therbe");

                /*MapLoadFromJSON(
                    "[[\"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\"], [\"Taqua\", \"Taqua\", \"Taqua\", \"Tsable\", \"\", \"Taqua\", \"Tsable\", \"Tsable\", \"\", \"Tsable\", \"\", \"Taqua\", \"Tsable\", \"Tsable\", \"\", \"Tsable\", \"\",\"Taqua\"], [\"Taqua\", \"Taqua\", \"Taqua\"], [\"Taqua\", \"Taqua\", \"Taqua\"], [\"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"\", \"\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"\", \"\", \"\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\", \"Taqua\"]]");
                */
                MapLoadFromJSON(TryAgain.Datas.Initializer.ReadTextFile("data\\map\\map.json"));
            }
        }

        public static void MapFirstInit(ref Tile[,] map, String basetile)
        {
            Tile tile = new Tile(basetile);
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = tile;
        }

        public static void Mapmodify(Tile[,] map, String newtile, int imin, int imax, int jmin, int jmax) //max et min a modifier en fait
        {
            Tile tile = new Tile(newtile);
            for (int i = imin; i <= imax; i++)
                for (int j = jmin; j <= jmax; j++)
                    map[i, j] = tile;
        }

        public static void MapSetTile(Tile[,] map, String newtile, int i, int j)
        {
            Tile tile = new Tile(newtile);
            map[i, j] = tile;
        }

        public static void Mapmodify(Texture2D[,] map, Texture2D newtile, int i, int j)
        {
            map[i, j] = newtile;
        }

        public static void Drawmap(SpriteBatch sb, Tile[,] map)
        {

            for (int i = -2; i < Hero.view.Width + 1; i++)
            {

                for (int j = -2; j < Hero.view.Height + 1; j++)
                {
                    //sb.Draw(map[i, j], new Vector2(variationsizegraphicsX + 64 * i, 64 * j), Color.White);
      
                    int x = (i + Hero.view.X) % map.GetLength(0),
                        y = (j + Hero.view.Y) % map.GetLength(1);

                    if (x < 0)
                        x += map.GetLength(0);
                    if (y < 0)
                        y += map.GetLength(1);
                    sb.Draw(
                        map[x, y].getTexture(), new Rectangle(
                        (int)(variationsizegraphicsX + 64 * (i - Hero.padding.X)),
                        (int)(64 * (j - Hero.padding.Y)), 64, 64), Color.White);
                    if (!map[x, y].isBlended)
                    {
                        if (y >= 1)
                        {
                            if (map[x, y].type != map[x, y - 1].type)
                            {
                                Tile fadedTile = new Tile(TextureBlend.DrawL(map[x, y].getTexture(), map[x, y - 1].getTexture(), y, x), map[x, y].IsWalkable());
                                fadedTile.isBlended = true;
                                fadedTile.type = map[x, y].type;
                                map[x, y] = fadedTile;
                            }
                        }
                        else // y = 0 ou moins
                        {
                            if (map[x, y].type != map[x, map.GetLength(1) - 1].type)
                            {
                                Tile fadedTile = new Tile(TextureBlend.DrawL(map[x, y].getTexture(), map[x, map.GetLength(1) - 1].getTexture(), y, x), map[x, y].IsWalkable());
                                fadedTile.isBlended = true;
                                fadedTile.type = map[x, y].type;
                                map[x, y] = fadedTile;
                            }
                        }
                        if (x >= 1)
                        {
                            if (map[x, y].type != map[x - 1, y].type)
                            {
                                Tile fadedTile = new Tile(TextureBlend.DrawD(map[x, y].getTexture(), map[x - 1, y].getTexture(), x, y), map[x, y].IsWalkable());
                                fadedTile.isBlended = true;
                                fadedTile.type = map[x, y].type;
                                map[x, y] = fadedTile;
                            }
                        }
                        else
                        {
                            if (map[x, y].type != map[map.GetLength(0) - 1, y].type)
                            {
                                Tile fadedTile = new Tile(TextureBlend.DrawD(map[x, y].getTexture(), map[map.GetLength(0) - 1, y].getTexture(), x, y), map[x, y].IsWalkable());
                                fadedTile.isBlended = true;
                                fadedTile.type = map[x, y].type;
                                map[x, y] = fadedTile;
                            }
                        }
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
