using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;

namespace Group5FinalProject
{
    internal class IntermissionScreenManager
    {
        Game1 GameReference;
        MapManager MapManager;

        public double GetTotalTime()
        {
            return MapManager.levelTimes.Values.Sum();
        }

        public IntermissionScreenManager(Game1 gameReference, MapManager mapManager)
        {
            GameReference = gameReference;
            MapManager = mapManager;
        }

        public void IntermissionScreenInputs()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (GameReference.levelId < MapManager.Maps.Count - 1)
                {
                    GameReference.levelId += 1; // Increment only if there's a next level
                    GameReference.gameState = 1;
                    MapManager.LoadMap(GameReference.levelId);
                    Debug.WriteLine($"Loading Level {GameReference.levelId}");
                }
                else
                {
                    // Reset game state to main menu
                    GameReference.gameState = 0;
                    GameReference.levelId = 0;
                    Debug.WriteLine("Game Over.");
                }
            }
        }

        public void DrawIntermissionScreen(SpriteBatch _spriteBatch)
        {
            // Center screen positioning
            Vector2 screenCenter = new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 2);
            if (GameReference.levelId+1 < MapManager.Maps.Count)
            {
                // Text configuration
                string levelCompletedText = "Level Completed!";
                string totalScoreText = $"Total Score: {GameReference.gameScore}";
                string timeLevelText = $"Time for this level: {MapManager.levelFinishTime:F2} seconds";
                string nextLevelText = $"Entering Level {GameReference.levelId + 2}...";
                string continueText = "PRESS ENTER TO CONTINUE";

                

                // Measure the width of each string to center them horizontally
                Vector2 completedTextSize = GameReference.defaultFont.MeasureString(levelCompletedText);
                Vector2 scoreTextSize = GameReference.defaultFont.MeasureString(totalScoreText);
                Vector2 timeLevelTextSize = GameReference.defaultFont.MeasureString(timeLevelText);
                Vector2 nextLevelTextSize = GameReference.defaultFont.MeasureString(nextLevelText);
                Vector2 continueTextSize = GameReference.defaultFont.MeasureString(continueText);

                // Positions for text
                Vector2 completedTextPosition = screenCenter - new Vector2(completedTextSize.X / 2, 120);
                Vector2 scoreTextPosition = screenCenter - new Vector2(scoreTextSize.X / 2, 70);
                Vector2 timeLevelTextPosition = screenCenter - new Vector2(timeLevelTextSize.X / 2, 20);
                Vector2 nextLevelTextPosition = screenCenter - new Vector2(nextLevelTextSize.X / 2, -30);
                Vector2 continueTextPosition = screenCenter - new Vector2(continueTextSize.X / 2, -80);

                // Draw intermission screen text
                _spriteBatch.DrawString(GameReference.defaultFont, levelCompletedText, completedTextPosition, Color.Yellow);
                _spriteBatch.DrawString(GameReference.defaultFont, totalScoreText, scoreTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, timeLevelText, timeLevelTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, nextLevelText, nextLevelTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, continueText, continueTextPosition, Color.White);
            }
            else
            {
                // Final screen after the last level
                string gameOverText = "Congratulations!";
                string totalScoreText = $"Total Score: {MapManager.levelScores.Values.Sum()}";
                string totalTimeText = $"Total Time: {MapManager.levelTimes.Values.Sum():F2} seconds";
                string restartText = "PRESS ENTER TO RESTART";


                // Measure string sizes
                Vector2 gameOverTextSize = GameReference.defaultFont.MeasureString(gameOverText);
                Vector2 totalScoreTextSize = GameReference.defaultFont.MeasureString(totalScoreText);
                Vector2 totalTimeTextSize = GameReference.defaultFont.MeasureString(totalTimeText);
                Vector2 restartTextSize = GameReference.defaultFont.MeasureString(restartText);

                // Positions for text
                Vector2 gameOverTextPosition = screenCenter - new Vector2(gameOverTextSize.X / 2, 70);
                Vector2 totalScoreTextPosition = screenCenter - new Vector2(totalScoreTextSize.X / 2, 30);
                Vector2 totalTimeTextPosition = screenCenter - new Vector2(totalTimeTextSize.X / 2, -10);
                Vector2 restartTextPosition = screenCenter - new Vector2(restartTextSize.X / 2, -50);

                // Draw text
                _spriteBatch.DrawString(GameReference.defaultFont, gameOverText, gameOverTextPosition, Color.Red);
                _spriteBatch.DrawString(GameReference.defaultFont, totalScoreText, totalScoreTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, totalTimeText, totalTimeTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, restartText, restartTextPosition, Color.Yellow);
            }
        }
    }
}
