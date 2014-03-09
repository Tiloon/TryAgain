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
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace TryAgain.Characters
{
    [Serializable()]

    class Hero : Character
    {
   
        private Item[] items = new Item[10];
        public CharacterStats stats;
        public CharacterStats getStats()
        {
            return stats;
        }
        Classes.Classe classe;
        Keys keyup, keydown, keyleft, keyright;

        public Hero(string name, Classes.Classe classe, Texture2D apparence, Keys keyup, Keys keydown, Keys keyleft, Keys keyright, Vector2 position)
        {
            this.items[0] = new MeleWeapon();
            this.longueur = 66;
            this.largeur = 25;
            this.position = position;
            this.name = name;
            this.classe = classe;
            this.apparence = apparence;
            this.keyup = keyup;
            this.keydown = keydown;
            this.keyleft = keyleft;
            this.keyright = keyright;
            if (classe == Classes.Classe.gunner)
            {
                stats = Stats.GetStats(0);
            }
        }
        public override void update()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(keyup) && position.Y > 0)
                this.position += new Vector2(0, -7);
            if (newState.IsKeyDown(keydown) && position.Y < 64 * Tilemap.lgmap - longueur)
                this.position += new Vector2(0, 7);
            if (newState.IsKeyDown(keyright) && position.X < 64 * Tilemap.lgmap + Tilemap.variationsizegraphicsX - largeur)
                this.position += new Vector2(7, 0);
            if (newState.IsKeyDown(keyleft) && position.X > Tilemap.variationsizegraphicsX)
                this.position += new Vector2(-7, 0);
        }

        public override void jsonUpdate(string json)
        {
            Hero data = JsonConvert.DeserializeObject<Hero>(json);
            this.items = data.items;
            this.stats = data.stats;
        }

        public void Levelup()
        {
            stats = stats + Stats.GetStats(stats.lvl);
        }

        internal Item[] getItemList()
        {
            return this.items;
        }
    }

    class Classes
    {
        public enum Classe { warrior, mage, gunner };
        public enum GunnerPower { bigcanon, bigjump };
        public enum MagePower { bigfireball, bigexplosion };
        public enum WarriorPower { bigslash, bigdash };
    }

}
