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
    class zClassbrouillonTile
    {
        int id;
        String name;
        Boolean walkable;
        Texture2D pathtexture;

        public zClassbrouillonTile(int id, String name, Boolean walkable, Texture2D pathtexture)
        {
            this.id = id;
            this.name = name;
            this.walkable = walkable;
            this.pathtexture = pathtexture;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Boolean Walkable
        {
            get
            {
                return walkable;
            }
            set
            {
                walkable = value;
            }
        }

        public Texture2D Pathtexture
        {
            get
            {
                return pathtexture;
            }
            set
            {
                pathtexture = value;
            }
        }

        static public zClassbrouillonTile herbTile = new zClassbrouillonTile(0, "herbe", true, Textures.herbe_texture);
        static public zClassbrouillonTile[,] mapherbeuse = new zClassbrouillonTile[10, 10];
        static public void mapsinit()
        {
            //premiere map
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    mapherbeuse[i, j] = herbTile;
        }

        static public void mapdraw(zClassbrouillonTile[,] map, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    spriteBatch.Draw(map[i,j].Pathtexture/*Textures.herbe_texture*/, new Vector2(64 * i, 64 * j), Color.White);
                }
        }

    }
}
