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
    class Textures
    {
        public static Dictionary<String, Texture2D> Cache = new Dictionary<string,Texture2D>();
        public static Texture2D herbe_texture, sable_texture, neige_texture, solrocailleux_texture;
        public static Texture2D aqua_halfwkbtexture, fire_hakfwkbtexture;
        public static Texture2D cascadedroite_unwkbtexture, cascadegauche_unwkbtexture,
                                neigetrou_unwkbtexture;
        public static Texture2D persopierre_texture;
        public static Texture2D bebeglauque_texture;

        public static Texture2D Button_Play, Button_Option, Button_Exit, Button_About, Button_Return, Button_OnePlayer, Button_TwoPlayers;
        public static Texture2D MainMenuBG, OptionBG, AboutBG;

        public static SpriteFont UIfont;
        public static Texture2D UIitemHolder, UIitemSelected;
        public static void load(ContentManager cm)
        {
            herbe_texture = cm.Load<Texture2D>(@"Sprites\tileherbe");          //0
            sable_texture = cm.Load<Texture2D>(@"Sprites\tilesable");          //1
            neige_texture = cm.Load<Texture2D>(@"Sprites\tileneige");          //2
            solrocailleux_texture = cm.Load<Texture2D>(@"Sprites\tilecaillou");//3 

            aqua_halfwkbtexture = cm.Load<Texture2D>(@"Sprites\Halfwalkable\tileaqua");
            fire_hakfwkbtexture = cm.Load<Texture2D>(@"Sprites\Halfwalkable\tilefire");

            cascadegauche_unwkbtexture = cm.Load<Texture2D>(@"Sprites\Unwalkable\tilecascadegauche");
            cascadedroite_unwkbtexture = cm.Load<Texture2D>(@"Sprites\Unwalkable\tilecascadedroite");
            neigetrou_unwkbtexture = cm.Load<Texture2D>(@"Sprites\Unwalkable\tileneigetrou");

            persopierre_texture = cm.Load<Texture2D>(@"Sprites\persopierre");
            bebeglauque_texture = cm.Load<Texture2D>(@"Sprites\Monsters\bebeglauque");
            //On quitte les textures purgame
            Button_Play = cm.Load<Texture2D>(@"Menu\Button_Play");
            Button_Option = cm.Load<Texture2D>(@"Menu\Button_Option");
            Button_Exit = cm.Load<Texture2D>(@"Menu\Button_Exit");
            Button_About = cm.Load<Texture2D>(@"Menu\Button_About");
            Button_Return = cm.Load<Texture2D>(@"Menu\Button_Return");
            Button_OnePlayer = cm.Load<Texture2D>(@"Menu\Button_1player");
            Button_TwoPlayers = cm.Load<Texture2D>(@"Menu\Button_2players");

            MainMenuBG = cm.Load<Texture2D>(@"Menu\MainMenu");
            OptionBG = cm.Load<Texture2D>(@"Menu\Option");
            AboutBG = cm.Load<Texture2D>(@"Menu\About");

            UIfont = cm.Load<SpriteFont>(@"Fonts\UIfont");
            UIitemHolder = cm.Load<Texture2D>(@"UI\uiitems");
            UIitemSelected = cm.Load<Texture2D>(@"UI\uiselected");

        }
    }
}
