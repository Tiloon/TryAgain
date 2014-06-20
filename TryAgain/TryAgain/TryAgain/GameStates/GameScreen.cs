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
using TryAgain.Characters;
using TryAgain.GameElements.misc;
using TryAgain.GameElements;
using TryAgain.GameElements.Map___environnement;
using TryAgain.Sounds;
using TryAgain.Online;
using TryAgain.GameElements.Managers;

namespace TryAgain.GameStates
{
    class GameScreen : Screen
    {
        ButtonState previousstate;
        public static UI userinterface = new UI();
        public static Hero hero;
        public static string name = "Tony";
        KeyboardState ancienState, newState;
        public static List<Ressource> RessList = new List<Ressource>();
        public static List<GameObject> GOList = new List<GameObject>();
        private static bool hasStarted = false;
        Vector2 expos;
        static public int actualmap = 1;

        public GameScreen()
        {
            this.state = ScreenType.Game;
            previousstate = Mouse.GetState().LeftButton;
        }
        public override ScreenType update()
        {
            MouseState mouse = Mouse.GetState();
            GameObject gob = null;
            expos = hero.position;

            TimeMGR.Update();

            if ((hero.stats.lp <= 0) && (Connection.Updated))
                Connection.HeroDead();

            if ((mouse.LeftButton == ButtonState.Pressed) && (mouse.LeftButton != previousstate))
            {
                gob = this.GetClicked(mouse);
                if (gob == null)
                {
                    gob = new GobVoid(new Vector2(mouse.X, mouse.Y));
                    GOList.Add(gob);
                }

                if (gob.Type == "GameObject,GObItem") // If it's an item, player take it
                {
                    hero.addItem((GObItem)gob);
                    GOList.Remove(gob);
                    GameObject.Delete(ref gob);
                }
                if (gob.Type == "GameObject,Character,Player") // If it's an item, player take it
                {
                    Connection.Command("rm:" + gob.UID);
                    //System.Windows.Forms.MessageBox.Show(gob.UID);
                }
                else if (hero.equipedItem() != null)// Else, player use its item
                {
                    Tuple<String, String> jsonUpdates = (hero.equipedItem()).useItem(hero, gob);
                    Vector2 heroPos = hero.getPosition();
                    hero.jsonUpdate(jsonUpdates.Item1); // As user
                    hero.SetPosition(heroPos);
                    gob.jsonUpdate(jsonUpdates.Item2); // As target
                }

                else if (hero.equipedItem() != null)
                {
                    GOList.Add(new GobVoid(new Vector2(mouse.X, mouse.Y)));
                }
            }

            previousstate = mouse.LeftButton;
            ancienState = newState;
            newState = Keyboard.GetState();
            if(newState.IsKeyDown(Keys.RightControl) && ancienState.IsKeyUp(Keys.RightControl)) 
                Skills.gauche = !Skills.gauche;
            if (newState.IsKeyDown(Keys.Escape))
                return ScreenType.Pause;
            if (newState.IsKeyDown(Keys.NumPad9))
                hero.stats.lp = 0;
            if (newState.IsKeyDown(Keys.W) && ancienState.IsKeyUp(Keys.W))
            {
                RessList.Clear();
                RessList.Add(new Ressource());
            }

            for (int i = 0; i < GOList.Count; i++)
            {
                if (!GOList[i].toRemove()) // || !GOList[i].toupdate ???
                {
                    GameObject.GobjectList.Remove(GOList[i].UID);
                    GOList.RemoveAt(i);
                    --i;
                }
                else
                {
                    if (GOList[i].toupdate)
                        GOList[i].update();
                }
            }
            Rectangle herorect = new Rectangle((int)(hero.position.X - (Hero.view.X + Hero.padding.X)) * 64, (int)(hero.position.Y - (Hero.view.Y + Hero.padding.Y)) * 64, 20, 60);
            /*if (RessList.Count != 0)
            {
                foreach (Ressource r in RessList)
                    if (r.Update(herorect))
                        RessList.Remove(r);
            }*/
            if (RessList.Count != 0)
                if (RessList[0].Update(herorect, expos, hero.position) || RessList[0].t > 250)
                    RessList.Remove(RessList[0]);
            if (RessList.Count == 0)
                RessList.Add(new Ressource());
            userinterface.update(ref hero);
            return this.GetState();
        }
        public override void draw(SpriteBatch sb, int Width, int Height)
        {
            if (hero.stats.lp != 0)
            {
                if (actualmap == 1)
                    Tilemap.Drawmap(sb, Tilemap.tiles);
                foreach (Ressource r in RessList)
                    r.Draw(sb);
                foreach (var todraw in GOList)
                {
                    /*
                    if(todraw.UID != hero.UID)
                        Chat.AddMessage((todraw.UID));*/
                    if (todraw.toupdate)
                        todraw.Draw(sb);
                }

                KeyboardState newState = Keyboard.GetState();

                if (!Chat.isWriting)
                    Skills.Draw(sb, hero.getPosition(), hero, newState);

                userinterface.Draw(sb);
                Chat.Draw(sb);

                if (!Chat.isWriting)
                {
                    if (newState.IsKeyDown(Keys.C))
                    {
                        Craft.Draw(sb);
                        Craft.Update(newState);
                    }
                }
            }
            else
            {
                GameOver.Draw(sb, Game1.gmt);
            }
        }

        public GameObject GetClicked(MouseState mouse)
        {
            Rectangle mouse_rectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            foreach (var gobject in GOList)
            {

                //Rectangle rectangle = new Rectangle((int)gobject.getPosition().X, (int)gobject.getPosition().Y, (int)gobject.getSize().X, (int)gobject.getSize().Y);
                //Rectangle rectangle = new Rectangle((int)gobject.getPosition().X, (int)gobject.getPosition().Y, (int)gobject.getSize().X, (int)gobject.getSize().Y);
                Rectangle rectangle = new Rectangle((int)((gobject.X - (Hero.view.X + Hero.padding.X)) * 64 + 64 * 4), (int)((gobject.Y - (Hero.view.Y + Hero.padding.Y)) * 64), 64, 64);
                if ((mouse_rectangle.Intersects(rectangle)) && (mouse.LeftButton == ButtonState.Pressed))
                    return gobject;
            }
            return null;
        }

        public override void init(GraphicsDevice graphics)
        {
            if (!hasStarted)
            {
                Connection.Command("login:" + Connection.avatar);
                Themes.currentTheme = 1;
                Vector2 pos1 = new Vector2(327.5F, 354.5F);
                hero = new Hero(name, Classes.Classe.gunner, Textures.Cache[Connection.avatar], Keys.Up, Keys.Down, Keys.Left, Keys.Right, pos1);
                GOList.Add(hero);
                /*hero2 = new Hero("Tony", Classes.Classe.gunner, Textures.persopierre_texture, Keys.Z, Keys.S, Keys.Q, Keys.D, pos2);
                GOList.Add(hero2);*/
                //Tilemap.MapFullINIT();

                GOList.Add(new Monster(Monstertype.minion, 50, 10, 20, 0.15F, new Vector2(6, 8)));
                //GOList.Add(new Monster(Monstertype.minion, 50, 10, 20, 0.15F, new Vector2(5, 5)));
                //GOList.Add(new Monster(Monstertype.minion, 50, 10, 20, 0.15F, new Vector2(8, 8)));

                hasStarted = true;
            }
        }

        public static bool IsGameStarted()
        {
            return hasStarted;
        }
    }
}