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

namespace TryAgain.GameStates
{
    class GameScreen : Screen
    {
        UI userinterface = new UI();
        Hero hero;
        Hero hero2;
        Monster[] monsterlist = new Monster[12];
        static public int actualmap = 1;

        public GameScreen()
        {
            this.state = ScreenType.Game;
        }
        public override ScreenType update()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Escape))
                return ScreenType.Quit;
            hero.update();
            hero2.update();
            userinterface.update(ref hero);
            monsterlist[0].update();
            return this.GetState();
        }
        public override void draw(SpriteBatch sb, int Width, int Height)
        {
            if (actualmap == 1)
                Tilemap.Drawmap(sb, Tilemap.map1);
            hero.Draw(sb);
            hero2.Draw(sb);
            monsterlist[0].Draw(sb);// monster1.Draw(sb);
            monsterlist[1].Draw(sb);
            userinterface.Draw(sb);
        }
        public override void init(GraphicsDevice graphics)
        {
            Vector2 pos1 = new Vector2(Tilemap.variationsizegraphicsX + 64, 64);
            Vector2 pos2 = new Vector2(Tilemap.variationsizegraphicsX + (Tilemap.lgmap - 4) * 64, 64);
            hero = new Hero("Pierre", Classes.Classe.gunner, Textures.persopierre_texture, Keys.Up, Keys.Down, Keys.Left, Keys.Right, pos1);
            hero2 = new Hero("Tony", Classes.Classe.gunner, Textures.persopierre_texture, Keys.Z, Keys.S, Keys.Q, Keys.D, pos2);
            Tilemap.MapFullINIT();
            monsterlist[0] = new Monster(Monstertype.bebeglauque, 50, 10, 20, 10, new Vector2(5, 5));
            monsterlist[1] = new Monster(Monstertype.bebeglauque, 50, 10, 20, 10, new Vector2(8, 8));
        }
    }
}
