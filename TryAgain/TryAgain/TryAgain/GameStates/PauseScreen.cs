﻿using System;
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
                    sb.Draw(Textures.OptionBG, new Rectangle(0, 0, Width, Height), Color.White);
                    sb.Draw(Textures.Cache["UISTRpause"], new Rectangle(Width / 4, Height / 12, Width / 2, Height / 8), Color.White);
                    ButtonPlay.Draw(sb);
                    ButtonOption.Draw(sb);
                    ButtonExit.Draw(sb);
                    if (OptionScreen.eng)
                    {
                        sb.DrawString(Textures.UIfont, "caffeine : " + GameScreen.userinterface.cp.ToString() + "/" + GameScreen.userinterface.cpmax.ToString(), new Vector2(15, 32), GameScreen.userinterface.statcolor);
                        sb.DrawString(Textures.UIfont, "mental : " + GameScreen.userinterface.mp.ToString() + "/" + GameScreen.userinterface.mpmax.ToString(), new Vector2(15, 52), GameScreen.userinterface.statcolor);
                        sb.DrawString(Textures.UIfont, "characteristic :", new Vector2(15, 100), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "level :" + GameScreen.userinterface.lvl.ToString(), new Vector2(15, 120), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "strenght :" + GameScreen.userinterface.force.ToString(), new Vector2(15, 140), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "intelligence :" + GameScreen.userinterface.intelligence.ToString(), new Vector2(15, 160), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "defense :" + GameScreen.userinterface.defense.ToString(), new Vector2(15, 180), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "critical rate :" + GameScreen.userinterface.criticalrate.ToString(), new Vector2(15, 200), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "speed :" + GameScreen.userinterface.speed.ToString(), new Vector2(15, 220), GameScreen.userinterface.caracolor);

                        sb.DrawString(Textures.UIfont, "some commands: ", new Vector2(15, 260), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Echap: Pause", new Vector2(15, 280), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Enter: write", new Vector2(15, 300), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Alt: move the camera", new Vector2(15, 320), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "G: throw the objet", new Vector2(15, 340), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "C: Craft interface", new Vector2(15, 360), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Left click: lots\n of things!", new Vector2(15, 380), GameScreen.userinterface.caracolor);
                    }
                    else
                    {
                        sb.DrawString(Textures.UIfont, "cafeine : " + GameScreen.userinterface.cp.ToString() + "/" + GameScreen.userinterface.cpmax.ToString(), new Vector2(15, 32), GameScreen.userinterface.statcolor);
                        sb.DrawString(Textures.UIfont, "mental : " + GameScreen.userinterface.mp.ToString() + "/" + GameScreen.userinterface.mpmax.ToString(), new Vector2(15, 52), GameScreen.userinterface.statcolor);
                        sb.DrawString(Textures.UIfont, "Caracteristiques :", new Vector2(15, 100), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "niveau :" + GameScreen.userinterface.lvl.ToString(), new Vector2(15, 120), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "force :" + GameScreen.userinterface.force.ToString(), new Vector2(15, 140), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "intelligence :" + GameScreen.userinterface.intelligence.ToString(), new Vector2(15, 160), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "defense :" + GameScreen.userinterface.defense.ToString(), new Vector2(15, 180), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "taux de critique :" + GameScreen.userinterface.criticalrate.ToString(), new Vector2(15, 200), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "vitesse :" + GameScreen.userinterface.speed.ToString(), new Vector2(15, 220), GameScreen.userinterface.caracolor);

                        sb.DrawString(Textures.UIfont, "Quelques commandes: ", new Vector2(15, 260), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Echap: Pause", new Vector2(15, 280), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Enter: ecrire", new Vector2(15, 300), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Alt: gerer la camera", new Vector2(15, 320), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "G: jeter l'objet", new Vector2(15, 340), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "C: interface d'artisanat", new Vector2(15, 360), GameScreen.userinterface.caracolor);
                        sb.DrawString(Textures.UIfont, "Clic gauche: beaucoup\n de choses!", new Vector2(15, 380), GameScreen.userinterface.caracolor);
                    }
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
                ButtonExit = new cButton(Textures.exit, graphics);
            else
                ButtonExit = new cButton(Textures.quitter, graphics);
            ButtonExit.SetPosition(new Vector2(1062, 392 + 150 + 150));

            if (OptionScreen.eng)
                ButtonReturn = new cButton(Textures.Return, graphics);
            else
                ButtonReturn = new cButton(Textures.retour, graphics);
            ButtonReturn.SetPosition(new Vector2(1062, 704 + 100));
        }
    }
}
