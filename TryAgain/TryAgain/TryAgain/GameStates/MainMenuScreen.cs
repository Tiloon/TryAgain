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
using TryAgain.Menu;
using System.Windows.Forms;

namespace TryAgain.GameStates
{
    class MainMenuScreen : Screen
    {
        enum MenuState
        {
            MainMenu,
            Option,
            About
        }
        MenuState CurrentMenuState = MenuState.MainMenu;
        cButton ButtonPlay;
        cButton ButtonOption;
        cButton ButtonAbout;
        cButton ButtonExit;
        cButton ButtonReturn;
        cButton ButtonFullscreen;
        cButton ButtonFenetre;
        static public bool fullscreen = false;

        public MainMenuScreen()
        {
            this.state = ScreenType.MainMenu;
        }
        public override ScreenType update()
        {
            MouseState mouse = Mouse.GetState();

            switch (CurrentMenuState)
            {

                case MenuState.MainMenu:
                    if (ButtonPlay.IsClicked(mouse))
                        return ScreenType.Game;
                    if (ButtonExit.IsClicked(mouse))
                        return ScreenType.Quit;
                    if (ButtonOption.IsClicked(mouse))
                        CurrentMenuState = MenuState.Option;
                    if (ButtonAbout.IsClicked(mouse))
                        CurrentMenuState = MenuState.About;
                    break;
                case MenuState.About:
                    if (ButtonReturn.IsClicked(mouse))
                        CurrentMenuState = MenuState.MainMenu;
                    break;
                case MenuState.Option:
                    if (ButtonReturn.IsClicked(mouse))
                        CurrentMenuState = MenuState.MainMenu;
                    if (ButtonFullscreen.IsClicked(mouse))
                    {
                        if(!Game1.graphics.IsFullScreen)
                        {
                        Game1.graphics.IsFullScreen = true;
                        Game1.graphics.ApplyChanges();
                        }
                    }
                    if (ButtonFenetre.IsClicked(mouse))
                    {
                        if (Game1.graphics.IsFullScreen)
                        {
                            Game1.graphics.IsFullScreen = false;
                            Game1.graphics.ApplyChanges();
                        }
                    }
                    break;
            }
            return this.GetState();
        }


        public override void draw(SpriteBatch sb, int Width, int Height)
        {
            switch (CurrentMenuState)
            {
                case MenuState.MainMenu:
                    sb.Draw(Textures.MainMenuBG, new Rectangle(0, 0, Width, Height), Color.White);
                    ButtonPlay.Draw(sb);
                    ButtonOption.Draw(sb);
                    ButtonExit.Draw(sb);
                    ButtonAbout.Draw(sb);
                    //ButtonOnePlayer.Draw(sb);
                    //ButtonTwoPlayers.Draw(sb);
                    break;

                case MenuState.Option:
                    //sb.Draw(Content.Load<Texture2D>("Option"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
                    ButtonReturn.Draw(sb);
                    ButtonFullscreen.Draw(sb);
                    ButtonFenetre.Draw(sb);
                    break;

                case MenuState.About:
                    //sb.Draw(Content.Load<Texture2D>("About"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
                    ButtonReturn.Draw(sb);
                    break;
            }
        }
        public override void init(GraphicsDevice graphics)
        {
            ButtonPlay = new cButton(Textures.Cache["UIBplay"], graphics);
            ButtonPlay.SetPosition(new Vector2(1062, 145+150));
            ButtonOption = new cButton(Textures.Cache["UIBoption"], graphics);
            ButtonOption.SetPosition(new Vector2(1062, 235+150+50));
            ButtonAbout = new cButton(Textures.Cache["UIBabout"], graphics);
            ButtonAbout.SetPosition(new Vector2(1062, 313+150+100));
            ButtonExit = new cButton(Textures.Cache["UIBexit"], graphics);
            ButtonExit.SetPosition(new Vector2(1062, 392 + 150 + 150));
            ButtonReturn = new cButton(Textures.Cache["UIBreturn"], graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704+100));
            ButtonFullscreen = new cButton(Textures.FullscreenBG, graphics);
            ButtonFullscreen.SetPosition(new Vector2(500, 100));
            ButtonFenetre = new cButton(Textures.FenetreBG, graphics);
            ButtonFenetre.SetPosition(new Vector2(500, 300));
            /*
            ButtonOnePlayer = new cButton(Textures.Button_OnePlayer, graphics);
            ButtonOnePlayer.SetPosition(new Vector2(1062, 900));
            ButtonTwoPlayers = new cButton(Textures.Button_TwoPlayers, graphics);
            ButtonTwoPlayers.SetPosition(new Vector2(1062, 1000));*/
        }
    }
}
