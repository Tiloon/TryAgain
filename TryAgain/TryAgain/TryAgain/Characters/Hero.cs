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

    class Hero: Character
    {
        string name;
        Classes.Classe classe;
        

        public Hero(string name, Classes.Classe classe, Texture2D apparence)
        {
            this.position = new Vector2(64, 64);
            this.name = name;
            this.classe = classe;
            this.apparence = apparence;
            if (classe == Classes.Classe.gunner)
            {
                stats = Stats.GetStats(0);
            }
        }
        public override void update()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Up))
                this.position += new Vector2(0, -5);
            if (newState.IsKeyDown(Keys.Down))
                this.position += new Vector2(0, 5);
            if (newState.IsKeyDown(Keys.Right))
                this.position += new Vector2(5, 0);
            if (newState.IsKeyDown(Keys.Left))
                this.position += new Vector2(-5, 0);
        }

        public void Levelup()
        {
            stats = stats + Stats.GetStats(stats.lvl);
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
