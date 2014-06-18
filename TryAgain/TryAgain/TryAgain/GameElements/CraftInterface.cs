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
        public static Dictionary<int, Texture2D> idtoitem = new Dictionary<int, Texture2D>();
        public static int totalnbressources = 11;
        public static int[] quantity = new int[totalnbressources];      //un tableau de données avec les quantités de ressources i
        public static string[] names = new string[totalnbressources];   /*juste un tableau informatif avec les noms de ressources
                                                                 pour dire a quoi correspond l'index de chaque ressources du tableau d'int*/

        public static void init()
        {
            for (int i = 0; i < totalnbressources; i++)
                quantity[i] = 2;
            for (int i = 0; i < Craft.nbcraftpossibles; i++)
                Craft.CraftInventory[i] = false;

            names[0] = "branche d'arbre";
            names[1] = "tas de bois";
            names[2] = "feuille d'arbre";
            names[3] = "eau";
            names[4] = "fer";
            names[5] = "or";
            names[6] = "herbe";
            names[7] = "terre de bonne qualite";
            names[8] = "tete de poney";
            names[9] = "bout de carton";
            names[10] = "feu";

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

    enum Craftpossible { potion, bouclier, gun1, bottes, coca, particulecannon }
    class Craft
    {
        public static int nbcraftpossibles = 6;
        public static bool[] CraftInventory = new bool[nbcraftpossibles];
        static public bool Crafter(Craftpossible test)
        {
            if (test == Craftpossible.potion) //Potion = 2 feu + branche
            {
                if (Ressourceslist.quantity[0] < 1 && Ressourceslist.quantity[11] < 2)
                    return false;
                else
                {
                    Ressourceslist.quantity[0]--;
                    Ressourceslist.quantity[11] -= 2;
                    CraftInventory[0] = true;
                    return true;
                }
            }
            else if (test == Craftpossible.bouclier) //Bouclier = 1 pierre + 1 bout de carton + 1 fer
            {
                if (Ressourceslist.quantity[4] < 1 && Ressourceslist.quantity[5] < 1 && Ressourceslist.quantity[10] < 1)
                    return false;
                else
                {
                    Ressourceslist.quantity[4]--;
                    Ressourceslist.quantity[5]--;
                    Ressourceslist.quantity[10]--;
                    CraftInventory[1] = true;
                    return true;
                }
            }
            else if (test == Craftpossible.gun1)  //gun1 = 1 poney + 1 fer + 1 terre
            {
                if (Ressourceslist.quantity[8] < 1 && Ressourceslist.quantity[5] < 1 && Ressourceslist.quantity[9] < 1)
                    return false;
                else
                {
                    Ressourceslist.quantity[8]--;
                    Ressourceslist.quantity[5]--;
                    Ressourceslist.quantity[9]--;
                    CraftInventory[2] = true;
                    return true;
                }
            }

            else if (test == Craftpossible.bottes)
            {
                if (Ressourceslist.quantity[3] < 1 && Ressourceslist.quantity[6] < 2)
                    return false;
                else
                {
                    Ressourceslist.quantity[3]--;
                    Ressourceslist.quantity[6] -= 2;
                    CraftInventory[3] = true;
                    return true;
                }
            }

            else if (test == Craftpossible.coca)
            {
                if (Ressourceslist.quantity[10] < 1 && Ressourceslist.quantity[8] < 2)
                    return false;
                else
                {
                    Ressourceslist.quantity[10]--;
                    Ressourceslist.quantity[8] -= 2;
                    CraftInventory[4] = true;
                    return true;
                }
            }

            else if (test == Craftpossible.particulecannon)
            {
                if (Ressourceslist.quantity[2] < 1 && Ressourceslist.quantity[6] < 1 && Ressourceslist.quantity[7] < 1)
                    return false;
                else
                {
                    Ressourceslist.quantity[2]--;
                    Ressourceslist.quantity[6]--;
                    Ressourceslist.quantity[7]--;
                    CraftInventory[5] = true;
                    return true;
                }
            }
            return false;   //pas encore prêts les autres trucs
        }
        static public void Update()
        {
            //si une certaine touche, alors, qqs ressources générées, vu que pas encore added pour l'instant.?
        }

        static public void Draw(SpriteBatch sb)    // ici, drawer les images des ressources et des items craftés a coté des quantités
        {
            sb.Draw(Textures.CraftInterface, new Vector2(Tilemap.variationsizegraphicsX, 0), Color.White);
            if (GameStates.OptionScreen.eng)
                sb.DrawString(Textures.UIfont, "Resources                    Crafts                Recipes", new Vector2(Tilemap.variationsizegraphicsX, 120), Color.Blue);
            else
                sb.DrawString(Textures.UIfont, "Ressources                   Artisanat             Recette", new Vector2(Tilemap.variationsizegraphicsX, 120), Color.Blue);
            sb.Draw(Textures.O, new Rectangle(Tilemap.variationsizegraphicsX, 160, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[0].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 160), Color.Black);
            sb.Draw(Textures.I, new Rectangle(Tilemap.variationsizegraphicsX, 200, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[1].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 200), Color.Black);
            sb.Draw(Textures.II, new Rectangle(Tilemap.variationsizegraphicsX, 240, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[2].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 240), Color.Black);
            sb.Draw(Textures.III, new Rectangle(Tilemap.variationsizegraphicsX, 280, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[3].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 280), Color.Black);
            sb.Draw(Textures.IV, new Rectangle(Tilemap.variationsizegraphicsX, 320, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[4].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 320), Color.Black);
            sb.Draw(Textures.V, new Rectangle(Tilemap.variationsizegraphicsX, 360, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[5].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 360), Color.Black);
            sb.Draw(Textures.VI, new Rectangle(Tilemap.variationsizegraphicsX, 400, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[6].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 400), Color.Black);
            sb.Draw(Textures.VII, new Rectangle(Tilemap.variationsizegraphicsX, 440, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[7].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 440), Color.Black);
            sb.Draw(Textures.VIII, new Rectangle(Tilemap.variationsizegraphicsX, 480, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[8].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 480), Color.Black);
            sb.Draw(Textures.IX, new Rectangle(Tilemap.variationsizegraphicsX, 520, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[9].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 520), Color.Black);
            sb.Draw(Textures.X, new Rectangle(Tilemap.variationsizegraphicsX, 560, 40, 40), Color.White);
            sb.DrawString(Textures.UIfont, "* " + Ressourceslist.quantity[10].ToString(), new Vector2(Tilemap.variationsizegraphicsX + 50, 560), Color.Black);


        }
    }
}
