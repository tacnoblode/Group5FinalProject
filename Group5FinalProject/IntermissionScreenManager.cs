using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Group5FinalProject
{
    internal class IntermissionScreenManager
    {
        Game1 GameReference;
        MapManager MapManager;


        public IntermissionScreenManager(Game1 gameReference, MapManager mapManager)
        {
            GameReference = gameReference;
            MapManager = mapManager;
        }

        public void IntermissionScreenInputs()
        {
            // This code is done, this should be okay to leave for now

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Set the game state to 1 (The main game stage)
                GameReference.gameState = 1;

                GameReference.levelId += 1;
                MapManager.LoadMap(GameReference.levelId);
            }
        }
        public void DrawIntermissionScreen(SpriteBatch _spriteBatch)
        {
            // Text configuration
            string levelCompletedText = "Level Completed!";
            string totalScoreText = $"Total Score: {GameReference.gameScore}";
            string nextLevelText = $"Entering Level {GameReference.levelId + 2}...";
            string continueText = "PRESS ENTER TO CONTINUE";
            // TODO: Replace the code in this function with the text/graphics for an intermission screen (the screen between advancing to the next level)
            // INCLUDE: A label showing your total score in the level (from GameReference.gameScore)
            // INCLUDE: A label telling you what level is next (eg. Entering Level 2...)
            // INCLUDE: A label that says "Level Completed!" or something similar
            // INCLUDE: A label that tells the player to press enter to continue.


            // Center screen positioning
            Vector2 screenCenter = new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 2);

            // Measure the width of each string to center them horizontally
            Vector2 completedTextSize = GameReference.defaultFont.MeasureString(levelCompletedText);
            Vector2 scoreTextSize = GameReference.defaultFont.MeasureString(totalScoreText);
            Vector2 nextLevelTextSize = GameReference.defaultFont.MeasureString(nextLevelText);
            Vector2 continueTextSize = GameReference.defaultFont.MeasureString(continueText);

            // Positions for text
            Vector2 completedTextPosition = screenCenter - new Vector2(completedTextSize.X / 2, 100);
            Vector2 scoreTextPosition = screenCenter - new Vector2(scoreTextSize.X / 2, 50);
            Vector2 nextLevelTextPosition = screenCenter - new Vector2(nextLevelTextSize.X / 2, 0);
            Vector2 continueTextPosition = screenCenter - new Vector2(continueTextSize.X / 2, -50);

            // Draw intermission screen text

            _spriteBatch.DrawString(GameReference.defaultFont, levelCompletedText, completedTextPosition, Color.Yellow);
            _spriteBatch.DrawString(GameReference.defaultFont, totalScoreText, scoreTextPosition, Color.White);
            _spriteBatch.DrawString(GameReference.defaultFont, nextLevelText, nextLevelTextPosition, Color.White);
            _spriteBatch.DrawString(GameReference.defaultFont, continueText, continueTextPosition, Color.White);
        }
    }
}
