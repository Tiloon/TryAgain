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
using Newtonsoft.Json;
using TryAgain.GameElements;

namespace TryAgain.Characters
{
    enum Monstertype { minion, biotech1, biotech2, biotech3, biotech4, biotech5 }
    enum Monsterstate { normal, enraged, weaken, friendly, dead }
                              //dmg*=2   dmg/=2  dmg=0                   
    class Monster : Character
    {
        int compteurvitesse = 0;
        public int dmgmin;
        public int dmgmax;
        public float speed;
        private Vector2 direction = new Vector2(0,0);
        int posmin = 0;
        int posmax = Tilemap.lgmap - 1;
        Vector2 posmap;

        public Monster(Monstertype mst, int hp, int dmgmin, int dmgmax, float speed, Vector2 posmap)
            : base("Monster", "Monster")
        {
            this.dmgmin = dmgmin;
            this.dmgmax = dmgmax;
            this.speed = speed;
            this.posmap = posmap;
            this.stats.lp = 60;
            this.position = new Vector2(posmap.X, posmap.Y);
            this.X = position.X;
            this.Y = position.Y;
            if (mst == Monstertype.minion)
            {
                this.apparence = Textures.Cache["Tminion"];
                this.longueur = 64;
                this.largeur = 43;
                this.size = new Vector2(this.longueur, this.largeur);
            }
        }

        public static Random rand = new Random();
        public static void Moverandom(ref Vector2 direction, Vector2 posmap, int posmin, int posmax)   //Cette methode sera seulement appliquée lorsque des monstres subiront des altérations d'état qui leur empêchent d'utiliser leur "IA" pour trouver le joueur
                                   //ou par les monstres cons tout simplement
        {
            
            int d = rand.Next(0, 8);
            Vector2 newposmap = posmap;
            switch (d)
            {
                case 0:
                    if (posmap.X > posmin)
                        direction = new Vector2(-1, 0);
                    break;
                case 1:
                    if (posmap.X < posmax && posmap.Y < posmax)
                        direction = new Vector2((float)(Math.Sqrt(2) / 2), (float)(Math.Sqrt(2) / 2));
                    break;
                case 2:
                    if (posmap.X < posmax)
                        direction = new Vector2(1, 0);
                    break;
                case 3:
                    if (posmap.X < posmax && posmap.Y > posmin)
                        direction = new Vector2((float)(Math.Sqrt(2) / 2), -(float)(Math.Sqrt(2) / 2));
                    break;
                case 4:
                    if (posmap.X > posmin && posmap.Y > posmin)
                        direction = new Vector2(-(float)(Math.Sqrt(2) / 2), -(float)(Math.Sqrt(2) / 2));
                    break;
                case 5:
                    if (posmap.X > posmin && posmap.Y < posmax)
                        direction = new Vector2(-(float)(Math.Sqrt(2) / 2), (float)(Math.Sqrt(2) / 2));
                    break;
                case 6:
                    if (posmap.Y > posmin)
                        direction = new Vector2(0, -1);
                    break;
                case 7:
                    if (posmap.Y < posmax)
                        direction = new Vector2(0, 1);
                    break;
            }
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
            base.update();
            this.X += this.direction.X;
            this.Y += this.direction.Y;
            compteurvitesse++;
            if (compteurvitesse > 30 - speed)
            {
                Moverandom(ref this.direction, this.posmap, this.posmin, this.posmax);
                compteurvitesse = 0;
            }
        }

        public override void jsonUpdate(string json)
        {
            base.jsonUpdate(json);
            Monster data = JsonConvert.DeserializeObject<Monster>(json);
            this.stats = data.stats;
            this.position = data.position;
            GameObject gob = data;
            Delete(ref gob);
        }

    }
}
