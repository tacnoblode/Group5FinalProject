﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.CSharp;
using System.Collections.Generic;

namespace Group5FinalProject
{
    public class Game1 : Game
    {
        // Components
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Viewport viewport;

        private MapManager mapManager;
        private TitleScreenManager titleScreenManager;
        private IntermissionScreenManager intermissionScreenManager;

        Player GamePlayer;
        Camera GameCamera;


        // GAME STATES:
        // 0 - Main Menu logic
        // 1 - Ingame Logic
        // 2 - Intermission Screen
        public int gameState = 0;
        public int levelId = 0;

        public int gameScore = 0;

        // Timers
        public int FramesElapsed = 0;
        public double SecondsElapsed = 0;

        // Resources:
        public SpriteFont defaultFont;
        public Texture2D fallbackTexture;


        // Objects

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            // Objects
            mapManager = new MapManager(this);
            GameCamera = new Camera(Vector2.Zero, this);
            GamePlayer = new Player(Vector2.Zero, this, mapManager, GameCamera);
            titleScreenManager = new TitleScreenManager(this,mapManager);
            intermissionScreenManager = new IntermissionScreenManager(this,mapManager);

            GameCamera.SetPlayerReference(GamePlayer);
            mapManager.SetPlayerReference(GamePlayer);
            mapManager.SetCameraReference(GameCamera);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            viewport = _graphics.GraphicsDevice.Viewport;

            // Fonts and Sprites
            defaultFont = Content.Load<SpriteFont>("DefaultFont");
            fallbackTexture = Content.Load<Texture2D>("WhiteBlock");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Always update the times elapsed no matter what.
            FramesElapsed += 1;
            SecondsElapsed += gameTime.ElapsedGameTime.TotalSeconds;


            switch (gameState)
            {
                case 0:
                    titleScreenManager.TitleScreenInputs();
                    break;
                case 1:
                    GamePlayer.DoInputLogic();
                    GameCamera.UpdatePosition();
                    break;
                case 2:
                    intermissionScreenManager.IntermissionScreenInputs();
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Remember, this is ONLY to draw the screen, not to execute any logic.

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (gameState)
            {
                case 0:
                    // All drawing of the title screen to be handled inside of TitleScreenManager.cs
                    titleScreenManager.DrawTitleScreen(_spriteBatch);
                    break;
                case 1:
                    // All game drawing is inside of MapManager.cs
                    mapManager.RenderMap(_spriteBatch);
                    GameCamera.DrawUIOnScreen(_spriteBatch);
                    break;
                case 2:
                    intermissionScreenManager.DrawIntermissionScreen(_spriteBatch);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}