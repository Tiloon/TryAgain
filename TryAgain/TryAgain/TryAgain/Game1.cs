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
using TryAgain.Online;
using DPSF;
using DPSF_Demo.ParticleSystems;

namespace TryAgain
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Screen screen = new MainMenuScreen();
        ScreenType gamestate;
        ScreenType newscreen;
        public static GraphicsDeviceManager gamegfx;
        bool skip = false;
        public static bool cannonpartic = false;

        DefaultQuadParticleSystemTemplate mcParticleSystem = null;
        FlameParticle part = null;
        FlameParticle part2 = null;
        Vector3 cameraPosition = new Vector3(0, 50, -200);
        public static GameTime gmt;

        Video video;
        VideoPlayer player;
        Texture2D videoTexture;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1472;
            graphics.PreferredBackBufferHeight = 960;
        }

        protected override void Initialize()
        {
            this.IsFixedTimeStep = false;
            IsMouseVisible = true;
            // TODO: Add your initialization logic here
            base.Initialize();
            screen.init(graphics.GraphicsDevice);
            gamestate = screen.GetState();
            gamegfx = graphics;
            Initializer.InitAll();
            Textures.LoadTextureList("data.textures.json");
            TryAgain.GameElements.Ressourceslist.init();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures.load(Content);

            video = Content.Load<Video>("video");
            player = new VideoPlayer();

            mcParticleSystem = new DefaultQuadParticleSystemTemplate(this);
            mcParticleSystem.AutoInitialize(this.GraphicsDevice, this.Content, null); //null
            part = new FlameParticle(this);
            part.AutoInitialize(this.GraphicsDevice, this.Content, null);
            part2 = new FlameParticle(this);
            part2.AutoInitialize(this.GraphicsDevice, this.Content, null);
            part2.InitialProperties.PositionMin.X = 500;
            part2.InitialProperties.PositionMax.X = 500;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            mcParticleSystem.Destroy();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //if (Keyboard.GetState().IsKeyDown(Keys.Escape) && (skip == false))
            //    skip = true;
            newscreen = screen.update();
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

            if ((player.State == MediaState.Stopped) && (!lavideoaetelancee))
            {
                lavideoaetelancee = true;

                player.Play(video);
            }

            Matrix sViewMatrix = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 50, 0), Vector3.Right);
            Matrix sProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height, 1, 10000);

            if (newscreen == ScreenType.MainMenu)
            {
                //part.Emitter pour les propriétés générales
                part.SetDefaultEffect();
                part.SetCameraPosition(cameraPosition);
                part.SetWorldViewProjectionMatrices(Matrix.Identity, sViewMatrix, sProjectionMatrix);
                part.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                part2.SetDefaultEffect();
                part2.SetCameraPosition(cameraPosition);
                part2.SetWorldViewProjectionMatrices(Matrix.Identity, sViewMatrix, sProjectionMatrix);
                part2.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (cannonpartic)
            {
                mcParticleSystem.SetWorldViewProjectionMatrices(Matrix.Identity, sViewMatrix, sProjectionMatrix);
                mcParticleSystem.SetCameraPosition(cameraPosition);
                mcParticleSystem.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            gmt = gameTime;
            base.Update(gameTime);
        }
        private bool lavideoaetelancee = false, lavideoestfini = false;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            screen.draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            Connection.Draw(spriteBatch);
            //if (gameTime.TotalGameTime.TotalSeconds <= 11) 
            //    scenar.Drawlancement(spriteBatch, gameTime, skip);
            if (!lavideoestfini)
            {
                lavideoestfini = player.State == MediaState.Stopped;
                if (player.State != MediaState.Stopped)
                {

                    videoTexture = player.GetTexture();
                }
                if (videoTexture != null)
                {
                    spriteBatch.Draw(videoTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                }
            }
            spriteBatch.End();
            if (newscreen == ScreenType.MainMenu)
            {
                part.Draw();
                part2.Draw();
            }
            if (cannonpartic)
                mcParticleSystem.Draw();
            base.Draw(gameTime);


        }
    }
}
