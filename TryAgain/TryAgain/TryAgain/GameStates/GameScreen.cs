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

namespace TryAgain.GameStates
{
    class GameScreen : Screen
    {
        Hero hero;
        Hero hero2;

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
            return this.GetState();
        }
        public override void draw(SpriteBatch sb, int Width, int Height)
        {
            /*if (Tilemap.Walkable(Textures.herbe_texture))*/
            Tilemap.Drawmap(sb, Tilemap.map1);
            hero.Draw(sb);
            hero2.Draw(sb);
        }
        public override void init(GraphicsDevice graphics)
        {
            Vector2 pos1 = new Vector2(Tilemap.variationsizegraphicsX + 64, 64);
            Vector2 pos2 = new Vector2(Tilemap.variationsizegraphicsX + (Tilemap.lgmap - 4) * 64, 64);
            hero = new Hero("Pierre", Classes.Classe.gunner, Textures.persopierre_texture, Keys.Up, Keys.Down, Keys.Left, Keys.Right, pos1);
            hero2 = new Hero("Tony", Classes.Classe.gunner, Textures.persopierre_texture, Keys.Z, Keys.S, Keys.Q, Keys.D, pos2);
            Tilemap.MapFullINIT();
        }
    }
}
