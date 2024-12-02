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

        public Texture2D spr_Player0;
        public Texture2D spr_Player1;
        public Texture2D spr_Enemy0;
        public Texture2D spr_Enemy1;
        public Texture2D spr_Rock;
        public Texture2D spr_Ground;


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

            spr_Enemy0 = Content.Load<Texture2D>("Sprites/Enemy0");
            spr_Enemy1 = Content.Load<Texture2D>("Sprites/Enemy1");
            spr_Ground = Content.Load<Texture2D>("Sprites/Floor");
            spr_Rock = Content.Load<Texture2D>("Sprites/Rocks");
            spr_Player0 = Content.Load<Texture2D>("Sprites/Player0");
            spr_Player1 = Content.Load<Texture2D>("Sprites/Player1");
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


            switch (gameState)
            {
                case 0:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    _spriteBatch.Begin();
                    // All drawing of the title screen to be handled inside of TitleScreenManager.cs
                    titleScreenManager.DrawTitleScreen(_spriteBatch);
                    break;
                case 1:
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Begin();
                    // All game drawing is inside of MapManager.cs
                    mapManager.RenderMap(_spriteBatch);
                    GameCamera.DrawUIOnScreen(_spriteBatch);
                    break;
                case 2:
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    _spriteBatch.Begin();
                    intermissionScreenManager.DrawIntermissionScreen(_spriteBatch);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}