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
using TryAgain.GameStates;
using TryAgain.GameElements;

namespace TryAgain.Characters
{
    [Serializable()]

    class Hero : Character
    {
        private Item[] items = new Item[10];
        Classes.Classe classe;
        Keys keyup, keydown, keyleft, keyright;
        private int equiped;

        public Hero(string name, Classes.Classe classe, Texture2D apparence, Keys keyup, Keys keydown, Keys keyleft, Keys keyright, Vector2 position)
            : base("Hero", "Hero00")
        {
            this.items[0] = Items.itemsBank["sword00"];
            this.items[1] = Items.itemsBank["banana00"];
            this.equiped = 0;
            this.longueur = 66;
            this.largeur = 25;
            this.size = new Vector2(this.largeur, this.longueur);
            this.position = position;
            this.X = position.X;
            this.Y = position.Y;
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

        Rectangle rectangcascade = new Rectangle(Tilemap.variationsizegraphicsX + (Tilemap.lgmap - 2) * 64, 0, 64 * 2, 64 * 4);
        public override void update()
        {
            base.update();
            KeyboardState newState = Keyboard.GetState();

            //collision monstre = degats subis
            foreach (GameObject obj in GameScreen.GOList)
            {
                if ((obj.Type == "GameObject,Character,Monster") && (stats.lp > 0) &&
                    (new Rectangle((int)position.X, (int)position.Y, largeur, longueur).Intersects(new Rectangle((int)obj.getPosition().X, (int)obj.getPosition().Y, (int)obj.getSize().X, (int)obj.getSize().Y))))
                    stats.lp--;
            }

            Vector2 normalizedSpeed = new Vector2(0, 0);
            if (newState.IsKeyDown(keyup) && position.Y > 0)
                if (!Collision(rectangcascade, Direction.haut)) //collision cascade et mer
                    normalizedSpeed += new Vector2(0, -1);
            if (newState.IsKeyDown(keydown) && position.Y < 64 * Tilemap.lgmap - longueur)
                normalizedSpeed += new Vector2(0, 1);
            if (newState.IsKeyDown(keyright) && position.X < 64 * Tilemap.lgmap + Tilemap.variationsizegraphicsX - largeur)
                if (!Collision(rectangcascade, Direction.droite)) //collision cascade et mer
                    normalizedSpeed += new Vector2(1, 0);
            if (newState.IsKeyDown(keyleft) && position.X > Tilemap.variationsizegraphicsX)
                normalizedSpeed += new Vector2(-1, 0);

            if ((normalizedSpeed.X != 0) || (normalizedSpeed.Y != 0))
            {
                float sqrtsum = (float)Math.Sqrt(Math.Abs(normalizedSpeed.X) + Math.Abs(normalizedSpeed.Y));
                this.position += normalizedSpeed * this.stats.speed / sqrtsum;
                this.X = this.position.X + this.size.X / 2;
                this.Y = this.position.Y + this.size.Y / 2;
            }
            if (newState.IsKeyDown(Keys.G))
            {
                if ((equiped != -1) && (this.items[equiped] != null))
                {
                    GObItem gobitem = new GObItem(this.items[equiped], this.position);
                    GameScreen.GOList.Add(gobitem);
                    this.items[equiped] = null;
                }
            }
        }

        public override void jsonUpdate(string json)
        {
            base.jsonUpdate(json);
            Hero data = JsonConvert.DeserializeObject<Hero>(json);
            this.items = data.items;
            this.stats = data.stats;
            this.position = data.position;
            GameObject gob = data;
            Delete(ref gob);
        }

        public void Levelup()
        {
            stats = stats + Stats.GetStats(stats.lvl);
        }

        internal Item[] getItemList()
        {
            return this.items;
        }

        internal Item equipedItem()
        {
            if (equiped != -1)
                return this.items[equiped];
            else
                return null;
        }

        public void setEquipedItem(int i)
        {
            equiped = i;
            if ((i < 0) || (i >= 10))
                equiped = -1;
        }

        public int getEquipedItem()
        {
            return this.equiped;
        }

        public bool addItem(GObItem item)
        {
            for (int i = 0; i < 10; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item.item();
                    return true;
                }
            }
            return false;
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
