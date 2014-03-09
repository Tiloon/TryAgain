using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TryAgain.GameStates;
using TryAgain.Datas;

namespace TryAgain
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Screen screen = new MainMenuScreen();
        ScreenType gamestate;
        public static GraphicsDeviceManager gamegfx;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Tilemap.lgmap * 64 + Tilemap.variationsizegraphicsX * 2;
            graphics.PreferredBackBufferHeight = Tilemap.lgmap * 64;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            // TODO: Add your initialization logic here    
            base.Initialize();
            screen.init(graphics.GraphicsDevice);
            gamestate = screen.GetState();
            gamegfx = graphics;
            Initializer.InitAll();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures.load(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ScreenType newscreen = screen.update();
            if (newscreen != gamestate)
            {
                if (newscreen == ScreenType.Quit)
                {
                    this.Exit();
                }
                else
                {
                    Screen.ChangeScreen(ref screen, newscreen);
                    screen.init(graphics.GraphicsDevice);
                    gamestate = screen.GetState();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            screen.draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            spriteBatch.Draw(Textures.Button_OnePlayer, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
