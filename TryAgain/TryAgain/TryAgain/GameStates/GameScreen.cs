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
        public GameScreen()
        {
            this.state = ScreenType.Game;
        }
        public override ScreenType update()
        {
            hero.update();
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Escape))
                return ScreenType.Quit;
            hero.update();
            return this.GetState();
        }
        public override void draw(SpriteBatch sb, int Width, int Height)
        {
            if (Tilemap.Walkable(Textures.herbe_texture))
                Tilemap.Drawmap(sb, Tilemap.map1);
            hero.Draw(sb);
        }
        public override void init(GraphicsDevice graphics)
        {
            hero = new Hero("Pierre", Classes.Classe.gunner, Textures.persopierre_texture);
            Tilemap.MapFullINIT();
        }
    }
}
