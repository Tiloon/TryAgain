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

namespace TryAgain.Characters
{
    enum Monstertype { bebeglauque, biotech1, biotech2, biotech3, biotech4, biotech5 }

    class Monster : Character
    {
        public int hp;
        public int dmgmin;
        public int dmgmax;
        public int speed;
        int posmin = 0;
        int posmax = Tilemap.lgmap;
        Vector2 postablo;

        public Monster(Monstertype mst, int hp, int dmgmin, int dmgmax, int speed, Vector2 postablo, ref Monster[,] mapmonster)
        {
            this.hp = hp;
            this.dmgmin = dmgmin;
            this.dmgmax = dmgmax;
            this.speed = speed;
            this.postablo = postablo;
            this.position = new Vector2 (Tilemap.variationsizegraphicsX + postablo.X * 64, postablo.Y * 64);

            if (mst == Monstertype.bebeglauque)
                this.apparence = Textures.bebeglauque_texture;
            mapmonster[(int)postablo.X, (int)postablo.Y] = this;
        }

        public void Moverandom(ref Monster[,] mapmonster)
        {
            Random rand = new Random();
            int direction = rand.Next(8);
            Vector2 newposition = postablo;
            switch (direction)
            {
                case 0:
                    if (postablo.X > posmin)
                        newposition = new Vector2(position.X - 1, position.Y);
                    break;
                case 1:
                    if (position.X < posmax && position.Y < posmax)
                        newposition = new Vector2(position.X + 1, position.Y + 1);
                    break;
                case 2:
                    if (position.X < posmax)
                        newposition = new Vector2(position.X + 1, position.Y);
                    break;
                case 3:
                    if (position.X < posmax && position.Y > posmin)
                        newposition = new Vector2(position.X + 1, position.Y - 1);
                    break;
                case 4:
                    if (position.X > posmin && position.Y > posmin)
                        newposition = new Vector2(position.X - 1, position.Y - 1);
                    break;
                case 5:
                    if (position.X > posmin && position.Y < posmax)
                        newposition = new Vector2(position.X - 1, position.Y + 1);
                    break;
                case 6:
                    if (position.Y > posmin)
                        newposition = new Vector2(position.X, position.Y - 1);
                    break;
                default:
                    if (position.Y < posmax)
                        newposition = new Vector2(position.X, position.Y + 1);
                    break;
            }
            if (mapmonster[(int)newposition.X, (int)newposition.Y] == null)
            {
                mapmonster[(int)position.X, (int)position.Y] = null;
                mapmonster[(int)newposition.X, (int)newposition.Y] = this;
                //monsteranimation(direction);
                position = newposition;
            }
        }

        public void AddMonster(ref Monster[,] MapMonster, int i, int j)
        {
            MapMonster[i, j] = this;
        }

        /*public static void AddMultipleMonsters(Monster Monster, ref Monster[,] MapMonster, int lengthmap, int maxmob)
        {
            Random rd = new Random();
            for (int n = 0; n < maxmob; n++)
                MapMonster[rd.Next(lengthmap), rd.Next(lengthmap)] = Monster;
        }*/

        public override void update()
        {
            if (GameStates.GameScreen.actualmap == 1)
                Moverandom(ref Tilemap.map1monsters);
            else if(GameStates.GameScreen.actualmap == 2)
                Moverandom(ref Tilemap.map2monsters);
        }


    }
}
