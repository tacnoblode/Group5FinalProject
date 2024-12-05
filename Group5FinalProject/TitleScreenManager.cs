using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Group5FinalProject
{
    internal class TitleScreenManager
    {
        Game1 GameReference;
        MapManager MapManager;
        private int selectedLevelIndex = 0; // Default level selection
        private int totalLevels; // Total number of levels

        private bool KeyPressedAlready = false;

        public TitleScreenManager(Game1 gameReference, MapManager mapManager)
        {
            GameReference = gameReference;
            MapManager = mapManager;
            totalLevels = MapManager.Maps.Count; // Use .Count to get the number of maps
        }

        // Handle input for the title screen, including level selection and starting the game
        public void TitleScreenInputs()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && GameReference.SecondsElapsed > 2)
            {
                // Start the game and load the selected level
                GameReference.gameState = 1; // Assuming game state 1 is the main game
                GameReference.levelId = selectedLevelIndex; // Set levelId to the selected level
                MapManager.LoadMap(GameReference.levelId);
            }

            // Handle left and right arrow keys for level selection
            if (!KeyPressedAlready)
            {
                // If left or right keys are pressed, toggle KeyPressedAlready to on, and wait until the key is not pressed.
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    selectedLevelIndex = (selectedLevelIndex - 1 + totalLevels) % totalLevels; // Wrap around left
                    KeyPressedAlready = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    selectedLevelIndex = (selectedLevelIndex + 1) % totalLevels; // Wrap around right
                    KeyPressedAlready = true;
                }
            }
            else
            {
                // Once the left or right keys are let go, turn KeyPressedAlready to false, allowing the user to press a key again.
                if (!keyboardState.IsKeyDown(Keys.Left) && !keyboardState.IsKeyDown(Keys.Right)) { KeyPressedAlready = false; }
            }
        }

        // Draw the title screen and the level selection options
        public void DrawTitleScreen(SpriteBatch _spriteBatch)
        {
            string gameTitle = "MASTER MINER";
            string gameCredits = "Created by Tyson, Meeran, Paulo, and Selahattin";
            Vector2 fontOrigin = GameReference.defaultFont.MeasureString(gameTitle) / 2;
            Vector2 creditsFontOrigin = GameReference.defaultFont.MeasureString(gameCredits) / 2;
            Vector2 fontPosition = new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 4); // Title centered at the top

            // Draw game title at the top
            _spriteBatch.DrawString(GameReference.defaultFont, gameTitle, fontPosition - fontOrigin, Color.White);

            // Draw level selection (currently using MapManager.Maps for levels)
            Vector2 levelPosition = new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 2); // Position for the levels

            // Draw level select section - Horizontal alignment
            _spriteBatch.DrawString(GameReference.defaultFont, "Select Level:", new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 2 - 50), Color.White);

            // Display level names with an indicator of the selected level
            for (int i = 0; i < totalLevels; i++)
            {
                string levelName = "Level " + (i + 1);  // Default naming "Level 1", "Level 2", etc.
                Color levelColor = (i == selectedLevelIndex) ? Color.Yellow : Color.White; // Highlight selected level in yellow
                _spriteBatch.DrawString(GameReference.defaultFont, levelName, levelPosition + new Vector2((i - selectedLevelIndex) * 150, 0), levelColor); // Spread out horizontally
            }

            // Instructions to press Enter to start the game
            _spriteBatch.DrawString(GameReference.defaultFont, "PRESS ENTER TO START", fontPosition + new Vector2(0, 100), Color.White);
            
            // Game credits at the bottom
            _spriteBatch.DrawString(GameReference.defaultFont, gameCredits, fontPosition + new Vector2(0, 300) - creditsFontOrigin, Color.White);

            // Debug info (optional)
            _spriteBatch.DrawString(GameReference.defaultFont, $"Seconds Elapsed: {GameReference.SecondsElapsed}", new Vector2(0, 0), Color.White);
            //_spriteBatch.DrawString(GameReference.defaultFont, $"Frames Elapsed: {GameReference.FramesElapsed}", new Vector2(0, 16), Color.White);
        }
    }
}
