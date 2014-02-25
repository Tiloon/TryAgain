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
                    if (ButtonPlay.isClicked == true)
                        return ScreenType.Game;

                    if (ButtonExit.isClicked == true)
                        return ScreenType.Quit;

                    ButtonPlay.Update(mouse);
                    if (ButtonOption.isClicked == true)
                        CurrentMenuState = MenuState.Option;
                    ButtonOption.Update(mouse);

                    if (ButtonAbout.isClicked == true)
                        CurrentMenuState = MenuState.About;
                    ButtonAbout.Update(mouse);
                    ButtonExit.Update(mouse);

                    break;

                case MenuState.About:
                case MenuState.Option:
                    if (ButtonReturn.isClicked == true)
                        CurrentMenuState = MenuState.MainMenu;
                    ButtonReturn.Update(mouse);
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
            ButtonPlay.SetPosition(new Vector2(562, 145));       // A regler une fois immage définitive
            ButtonOption = new cButton(Textures.Button_Option, graphics);
            ButtonOption.SetPosition(new Vector2(562, 225));
            ButtonExit = new cButton(Textures.Button_Exit, graphics);
            ButtonExit.SetPosition(new Vector2(562, 382));
            ButtonAbout = new cButton(Textures.Button_About, graphics);
            ButtonAbout.SetPosition(new Vector2(562, 303));
            ButtonReturn = new cButton(Textures.Button_Return, graphics);
            ButtonReturn.SetPosition(new Vector2(graphics.Viewport.Width - 230, graphics.Viewport.Height - 90));
        }
    }
}
