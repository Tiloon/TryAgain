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
                        return ScreenType.Options;
                    if (ButtonAbout.IsClicked(mouse))
                        CurrentMenuState = MenuState.About;
                    break;
                case MenuState.About:
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
                    //ButtonOnePlayer.Draw(sb);
                    //ButtonTwoPlayers.Draw(sb);
                    break;

                case MenuState.About:
                    sb.Draw(Textures.storyboard, Vector2.Zero, Color.White);
                    sb.DrawString(Textures.UIfont, "Voici le beau scenario de TryAgain", new Vector2(1050, 100), Color.Black);
                    ButtonReturn.Draw(sb);
                    break;
            }
        }
        public override void init(GraphicsDevice graphics)
        {
            if (OptionScreen.eng)
                ButtonPlay = new cButton(Textures.play, graphics);
            else
                ButtonPlay = new cButton(Textures.jouer, graphics);
            ButtonPlay.SetPosition(new Vector2(1062, 145 + 150));

            ButtonOption = new cButton(Textures.MenuOption, graphics);
            ButtonOption.SetPosition(new Vector2(1062, 235 + 150 + 50));

            if (OptionScreen.eng)
                ButtonAbout = new cButton(Textures.AboutBG, graphics);
            else
                ButtonAbout = new cButton(Textures.aPropos, graphics);
            ButtonAbout.SetPosition(new Vector2(1062, 313 + 150 + 100));

            if (OptionScreen.eng)
                ButtonExit = new cButton(Textures.exit, graphics);
            else
                ButtonExit = new cButton(Textures.quitter, graphics);
            ButtonExit.SetPosition(new Vector2(1062, 392 + 150 + 150));

            if (OptionScreen.eng)
                ButtonReturn = new cButton(Textures.Return, graphics);
            else
                ButtonReturn = new cButton(Textures.retour, graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704 + 100));
            /*
            ButtonOnePlayer = new cButton(Textures.Button_OnePlayer, graphics);
            ButtonOnePlayer.SetPosition(new Vector2(1062, 900));
            ButtonTwoPlayers = new cButton(Textures.Button_TwoPlayers, graphics);
            ButtonTwoPlayers.SetPosition(new Vector2(1062, 1000));*/
        }
    }
}
