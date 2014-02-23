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
    class Hero
    {
        Vector2 positionhero;
        Texture2D apparence;
        string name;
        Classes.Classe classe;
        int[] stats = new int[10];
        
        /*0 lvl;
          1 pv;
          2 pvmax;
          3 mp;
          4 mpmax;
          5 force;
          6 intel;
          7 defense;
          8 ccrate;
          9 ccbonus;*/

        public Hero(string name, Classes.Classe classe, Texture2D apparence)
        {
            positionhero = new Vector2(64, 0);
            this.name = name;
            this.classe = classe;
            this.apparence = apparence;
            if (classe == Classes.Classe.gunner)
            {
                stats[0] = 1;
                stats[1] = 100;
                stats[2] = 100;
                stats[3] = 100;
                stats[4] = 100;
                stats[5] = 10;
                stats[6] = 10;
                stats[7] = 10;
                stats[8] = 10;
                stats[9] = 50;
            }
        }


        public void Levelup()
        {
            for (int i = 0; i <= stats.Length; i++)
            {
                stats[i] *= 11;
                stats[i] /= 10;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(apparence, positionhero, Color.White);
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
