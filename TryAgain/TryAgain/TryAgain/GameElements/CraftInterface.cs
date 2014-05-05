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
using TryAgain.Datas;
using System.Windows.Forms;
using TryAgain.Sounds;

namespace TryAgain.GameElements
{
    class Ressources
    {
        public static int totalnbressources = 12;
        public static int[] quantity = new int[totalnbressources];      //un tableau de données avec les quantités de ressources i
        public static string[] names = new string[totalnbressources];   /*juste un tableau informatif avec les noms de ressources
                                                                 pour dire a quoi correspond l'index de chaque ressources du tableau d'int*/

        public static void init()
        {
            for (int i = 0; i < totalnbressources; i++)
                quantity[i] = 10;
            for (int i = 0; i < Craft.nbcraftpossibles; i++)
                Craft.CraftInventory[i] = 0;

            names[0] = "branche d'arbre";
            names[1] = "tas de bois";
            names[2] = "feuille d'arbre";
            names[3] = "eau";
            names[4] = "pierre";
            names[5] = "fer";
            names[6] = "or";
            names[7] = "herbe";
            names[8] = "terre de bonne qualite";
            names[9] = "tete de poney";
            names[10] = "bout de carton";
            names[11] = "feu";
        }
    }

    enum Craftpossible { torche, baton, hache, abri, ailesdeco, superepee }
    class Craft
    {
        public static int nbcraftpossibles = 6;
        public static int[] CraftInventory = new int[nbcraftpossibles];
        static public bool Crafter(Craftpossible test)
        {
            if (test == Craftpossible.torche) //Torche = 2 feu + branche
            {
                if (Ressources.quantity[0] < 1 && Ressources.quantity[11] < 2)
                    return false;
                else
                {
                    Ressources.quantity[0]--;
                    Ressources.quantity[11] -= 2;
                    CraftInventory[1]++;
                    return true;
                }
            }
            else
                return false;   //pas encore prêts les autres trucs
        }
        static public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(Textures.CraftInterface, new Vector2(Tilemap.variationsizegraphicsX, 0), Color.White);
            sb.End();
        }
    }
}
