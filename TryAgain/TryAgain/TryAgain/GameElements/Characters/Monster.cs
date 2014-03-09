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
    enum Monsterstate { normal, enraged, weaken, friendly, dead }
                              //dmg*=2   dmg/=2  dmg=0                   
    class Monster : Character
    {
        int compteurvitesse = 0;
        public int hp;
        public int dmgmin;
        public int dmgmax;
        public int speed;
        Random rand = new Random();
        int posmin = 0;
        int posmax = Tilemap.lgmap - 1;
        Vector2 posmap;
        
        public Monster(Monstertype mst, int hp, int dmgmin, int dmgmax, int speed, Vector2 posmap)
        {
            this.hp = hp;
            this.dmgmin = dmgmin;
            this.dmgmax = dmgmax;
            this.speed = speed;
            this.posmap = posmap;
            this.position = new Vector2(Tilemap.variationsizegraphicsX + posmap.X * 64, posmap.Y * 64);
            if (mst == Monstertype.bebeglauque)
            {
                this.apparence = Textures.bebeglauque_texture;
                this.longueur = 64;
                this.largeur = 43;
            }
        }

        public void Moverandom()   //Cette methode sera seulement appliquée lorsque des monstres subiront des altérations d'état qui leur empêchent d'utiliser leur "IA" pour trouver le joueur
                                   //ou par les monstres cons tout simplement
        {
            int direction = rand.Next(0, 8);
            Vector2 newposmap = posmap;
            switch (direction)
            {
                case 0:
                    if (posmap.X > posmin)
                        newposmap = new Vector2(posmap.X - 1, posmap.Y);
                    break;
                case 1:
                    if (posmap.X < posmax && posmap.Y < posmax)
                        newposmap = new Vector2(posmap.X + 1, posmap.Y + 1);
                    break;
                case 2:
                    if (posmap.X < posmax)
                        newposmap = new Vector2(posmap.X + 1, posmap.Y);
                    break;
                case 3:
                    if (posmap.X < posmax && posmap.Y > posmin)
                        newposmap = new Vector2(posmap.X + 1, posmap.Y - 1);
                    break;
                case 4:
                    if (posmap.X > posmin && posmap.Y > posmin)
                        newposmap = new Vector2(posmap.X - 1, posmap.Y - 1);
                    break;
                case 5:
                    if (posmap.X > posmin && posmap.Y < posmax)
                        newposmap = new Vector2(posmap.X - 1, posmap.Y + 1);
                    break;
                case 6:
                    if (posmap.Y > posmin)
                        newposmap = new Vector2(posmap.X, posmap.Y - 1);
                    break;
                case 7:
                    if (posmap.Y < posmax)
                        newposmap = new Vector2(posmap.X, posmap.Y + 1);
                    break;
            }
            posmap = newposmap;
            //Animation()
            position = new Vector2(Tilemap.variationsizegraphicsX + posmap.X*43, posmap.Y * 64);
        }

        public void Collision()
        { }

        public void AddMonster(Monster[] MapMonster, int i)
        {
           MapMonster[i] = this;
        }
        public void AddMonster(Monster[] MapMonster, int i, Vector2 posmap)
        {
            MapMonster[i] = this;
            MapMonster[i].posmap = posmap;
            MapMonster[i].position = new Vector2(Tilemap.variationsizegraphicsX + posmap.X * 64, posmap.Y * 64);
        }

        public override void update()
        {
            compteurvitesse++;
            if (compteurvitesse > 30 - speed)
            {
                Moverandom();
                compteurvitesse = 0;
            }
        }

        public override void jsonUpdate(string json)
        {
            
        }

    }
}
