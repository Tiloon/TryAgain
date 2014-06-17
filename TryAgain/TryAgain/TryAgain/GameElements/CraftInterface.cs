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
    class Ressourceslist
    {
        public static Dictionary<int, Texture2D> idtoitem;
        public static int totalnbressources = 10; //+1 en comptant index 0
        public static int[] quantity = new int[totalnbressources];      //un tableau de données avec les quantités de ressources i
        public static string[] names = new string[totalnbressources];   /*juste un tableau informatif avec les noms de ressources
                                                                 pour dire a quoi correspond l'index de chaque ressources du tableau d'int*/

        public static void init()
        {
            for (int i = 0; i < totalnbressources; i++)
                quantity[i] = 10;
            for (int i = 0; i < Craft.nbcraftpossibles; i++)
                Craft.CraftInventory[i] = false;

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

            idtoitem.Add(0, Textures.O);
            idtoitem.Add(1, Textures.I);
            idtoitem.Add(2, Textures.II);
            idtoitem.Add(3, Textures.III);
            idtoitem.Add(4, Textures.IV);
            idtoitem.Add(5, Textures.V);
            idtoitem.Add(6, Textures.VI);
            idtoitem.Add(7, Textures.VII);
            idtoitem.Add(8, Textures.VIII);
            idtoitem.Add(9, Textures.IX);
            idtoitem.Add(10, Textures.X);
        }
    }

    enum Craftpossible { torche, baton, hache, abri, ailesdeco, superepee }
    class Craft
    {
        public static int nbcraftpossibles = 6;
        public static bool[] CraftInventory = new bool[nbcraftpossibles];
        static public bool Crafter(Craftpossible test)
        {
            if (test == Craftpossible.torche) //Torche = 2 feu + branche
            {
                if (Ressourceslist.quantity[0] < 1 && Ressourceslist.quantity[11] < 2)
                    return false;
                else
                {
                    Ressourceslist.quantity[0]--;
                    Ressourceslist.quantity[11] -= 2;
                    CraftInventory[1] = true;
                    return true;
                }
            }
            else
                return false;   //pas encore prêts les autres trucs
        }
        static public void Update()
        {
            //si une certaine touche, alors, qqs ressources générées, vu que pas encore added pour l'instant.?
        }

        static public void Draw(SpriteBatch sb)    // ici, drawer les images des ressources et des items craftés a coté des quantités
        {
            sb.Draw(Textures.CraftInterface, new Vector2(Tilemap.variationsizegraphicsX, 0), Color.White);
            sb.DrawString(Textures.UIfont, "Ressources                   Crafts                Recette", new Vector2(Tilemap.variationsizegraphicsX, 120), Color.Blue);
            sb.Draw(Textures.I, new Rectangle(Tilemap.variationsizegraphicsX, 160, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 160), Color.Black);
            sb.Draw(Textures.II, new Rectangle(Tilemap.variationsizegraphicsX, 200, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 200), Color.Black);
            sb.Draw(Textures.III, new Rectangle(Tilemap.variationsizegraphicsX, 240, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 240), Color.Black);
            sb.Draw(Textures.IV, new Rectangle(Tilemap.variationsizegraphicsX, 280, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 280), Color.Black);
            sb.Draw(Textures.V, new Rectangle(Tilemap.variationsizegraphicsX, 320, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 320), Color.Black);
            sb.Draw(Textures.VI, new Rectangle(Tilemap.variationsizegraphicsX, 360, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 360), Color.Black);
            sb.Draw(Textures.VII, new Rectangle(Tilemap.variationsizegraphicsX, 400, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 400), Color.Black);
            sb.Draw(Textures.VIII, new Rectangle(Tilemap.variationsizegraphicsX, 440, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 440), Color.Black);
            sb.Draw(Textures.IX, new Rectangle(Tilemap.variationsizegraphicsX, 480, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 480), Color.Black);
            sb.Draw(Textures.X, new Rectangle(Tilemap.variationsizegraphicsX, 520, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 520), Color.Black);
            sb.Draw(Textures.XI, new Rectangle(Tilemap.variationsizegraphicsX, 560, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 560), Color.Black);
        }
    }
}
