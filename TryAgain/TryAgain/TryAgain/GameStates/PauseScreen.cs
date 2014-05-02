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

namespace TryAgain.GameStates
{
    class PauseScreen : Screen
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
        cButton ButtonOnePlayer;
        cButton ButtonTwoPlayers;

        public PauseScreen()
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
                    ButtonOnePlayer.Draw(sb);
                    ButtonTwoPlayers.Draw(sb);
                    break;

                case MenuState.Option:
                    //sb.Draw(Content.Load<Texture2D>("Option"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
                    ButtonReturn.Draw(sb);
                    break;

                case MenuState.About:
                    //sb.Draw(Content.Load<Texture2D>("About"), new Rectangle(0, 0, screenWidth, screenHigh), Color.White);
                    ButtonReturn.Draw(sb);
                    break;
            }
        }
        public override void init(GraphicsDevice graphics)
        {
            ButtonPlay = new cButton(Textures.Button_Play, graphics);
            ButtonPlay.SetPosition(new Vector2(1062, 145+150));  
            ButtonOption = new cButton(Textures.Button_Option, graphics);
            ButtonOption.SetPosition(new Vector2(1062, 235+150+50));
            ButtonAbout = new cButton(Textures.Button_About, graphics);
            ButtonAbout.SetPosition(new Vector2(1062, 313+150+100));
            ButtonExit = new cButton(Textures.Button_Exit, graphics);
            ButtonExit.SetPosition(new Vector2(1062, 392 + 150 + 150));
            ButtonReturn = new cButton(Textures.Button_Return, graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704+100));
            
            ButtonOnePlayer = new cButton(Textures.Button_OnePlayer, graphics);
            ButtonOnePlayer.SetPosition(new Vector2(1062, 900));
            ButtonTwoPlayers = new cButton(Textures.Button_TwoPlayers, graphics);
            ButtonTwoPlayers.SetPosition(new Vector2(1062, 1000));
        }
    }
}
