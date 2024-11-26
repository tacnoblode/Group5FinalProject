using Microsoft.Xna.Framework;
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
        private Viewport viewport;

        private MapManager mapManager;

        Player GamePlayer;
        Camera GameCamera;


        // GAME STATES:
        // 0 - Main Menu logic
        // 1 - Ingame Logic
        // 2 - Intermission Screen
        public int gameState = 0;
        public int levelId = 0;

        // Timers
        public int FramesElapsed = 0;
        public double SecondsElapsed = 0;

        // Resources:
        SpriteFont defaultFont;
        public Texture2D fallbackTexture;

        public Vector2 CameraPosition;

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
            CameraPosition = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            viewport = _graphics.GraphicsDevice.Viewport;

            // Fonts and Sprites
            defaultFont = Content.Load<SpriteFont>("DefaultFont");
            fallbackTexture = Content.Load<Texture2D>("WhiteBlock");

            // Objects
            mapManager = new MapManager(this);
            GameCamera = new Camera(Vector2.Zero, this);
            GamePlayer = new Player(Vector2.Zero,this,mapManager,GameCamera);
            GameCamera.SetPlayerReference(GamePlayer);
            mapManager.SetPlayerReference(GamePlayer);
            mapManager.SetCameraReference(GameCamera);
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
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    { 
                        // Set the game state to 1 (The main game stage)
                        gameState = 1;
                        
                        // TODO: Replace the 0 in the function call with the level number to load.
                        mapManager.LoadMap(levelId); 
                    }
                    break;
                case 1:
                    GamePlayer.DoInputLogic();
                    GameCamera.UpdatePosition();
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
                    // Configure Content to show on the screen
                    string gameTitle = "SUPER AWESOME GAME";
                    Vector2 FontOrigin = defaultFont.MeasureString(gameTitle) / 2;
                    Vector2 FontPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

                    // Draw the contents onto the main menu screen
                    _spriteBatch.DrawString(defaultFont,gameTitle,FontPosition - FontOrigin,Color.White);
                    _spriteBatch.DrawString(defaultFont,"PRESS ENTER TO START",FontPosition - FontOrigin + new Vector2(0,200),Color.White);

                    // Debug: Show the time elapsed
                    _spriteBatch.DrawString(defaultFont,$"Seconds Elapsed: {SecondsElapsed}",new Vector2(0,0),Color.White);
                    _spriteBatch.DrawString(defaultFont,$"Frames Elapsed: {FramesElapsed}",new Vector2(0,16),Color.White);


                    break;
                case 1:
                    mapManager.RenderMap(_spriteBatch);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}