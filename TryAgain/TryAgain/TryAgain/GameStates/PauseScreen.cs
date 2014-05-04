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
        }
        MenuState CurrentMenuState = MenuState.MainMenu;
        cButton ButtonPlay;
        cButton ButtonOption;
        cButton ButtonExit;
        cButton ButtonReturn;

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
                        return ScreenType.Options;
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
                    //sb.Draw(Textures.MainMenuBG, new Rectangle(Width, 0, Width, Height), Color.White);
                    sb.Draw(Textures.Cache["UISTRpause"], new Rectangle(Width / 4, Height / 12, Width / 2, Height / 8), Color.White);
                    ButtonPlay.Draw(sb);
                    ButtonOption.Draw(sb);
                    ButtonExit.Draw(sb);
                    break;
            }
        }
        public override void init(GraphicsDevice graphics)
        {

            ButtonPlay = new cButton(Textures.Cache["UIBplay"], graphics);
            ButtonPlay.SetPosition(new Vector2(1062, 145 + 150));
            ButtonOption = new cButton(Textures.Cache["UIBoption"], graphics);
            ButtonOption.SetPosition(new Vector2(1062, 235 + 150 + 50));
            ButtonExit = new cButton(Textures.Cache["UIBexit"], graphics);
            ButtonExit.SetPosition(new Vector2(1062, 392 + 150 + 150));
            ButtonReturn = new cButton(Textures.Cache["UIBreturn"], graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704 + 100));
        }
    }
}
