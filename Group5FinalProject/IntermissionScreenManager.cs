using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Group5FinalProject
{
    internal class IntermissionScreenManager
    {
        Game1 GameReference;
        MapManager MapManager;

        string userName = string.Empty; // Store the username
        KeyboardState previousKeyboardState; // Store previous keyboard state.

        private const string ScoreFile = "top_scores.txt";

        public static List<Tuple<string, int>> LoadTopScores()
        {
            List<Tuple<string, int>> scores = new List<Tuple<string, int>>();

            if (File.Exists(ScoreFile))
            {
                var lines = File.ReadAllLines(ScoreFile);

                foreach (var line in lines)
                {
                    var parts = line.Split(',');

                    if (parts.Length == 2)
                    {
                        string name = parts[0];
                        int score;

                        if (int.TryParse(parts[1], out score))
                        {
                            scores.Add(new Tuple<string, int>(name, score));
                        }
                    }
                }
            }

            // Sort by score (desc order) and return the top 3 scores
            return scores.OrderByDescending(s => s.Item2).Take(3).ToList();
        }

        // Save the new score to the file
        public static void SaveScore(string playerName, int score)
        {
            // Load existing scores
            List<Tuple<string, int>> scores = LoadTopScores();

            // Add the new score to the list
            scores.Add(new Tuple<string, int>(playerName, score));

            // Sort by score (descending order)
            scores = scores.OrderByDescending(s => s.Item2).Take(3).ToList();

            // Write the top scores back to the file
            using (StreamWriter writer = new StreamWriter(ScoreFile, false))
            {
                foreach (var playerScore in scores)
                {
                    writer.WriteLine($"{playerScore.Item1},{playerScore.Item2}");
                }
            }
        }

        public IntermissionScreenManager(Game1 gameReference, MapManager mapManager)
        {
            GameReference = gameReference;
            MapManager = mapManager;
            previousKeyboardState = Keyboard.GetState(); // Initialize previous state
        }

        public void IntermissionScreenInputs()
        {
            // Handlle name input
            if (GameReference.levelId + 1 == MapManager.Maps.Count)
            {
                KeyboardState currentKeyboardState = Keyboard.GetState();

                // Loop through all the keys pressed
                foreach (var key in currentKeyboardState.GetPressedKeys())
                {
                    // Check for key press
                    if (!previousKeyboardState.IsKeyDown(key))
                    {
                        // Only add letters if there's space (3 letters)
                        if (key >= Keys.A && key <= Keys.Z && userName.Length < 3)
                        {
                            userName += key.ToString().Substring(0, 1).ToUpper();
                        }
                        // Backspace to delete a character
                        else if (key == Keys.Back && userName.Length > 0)
                        {
                            userName = userName.Substring(0, userName.Length - 1);
                        }
                    }
                }

                // Update the previous keyboard state
                previousKeyboardState = currentKeyboardState;

                if (currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    if (userName.Length == 3)
                    {
                        SaveScore(userName, (int)MapManager.levelScores.Values.Sum());
                        GameReference.gameState = 0; // Return to the main menu
                        GameReference.levelId = 0;  // Reset to the first level
                        Debug.WriteLine("Game Over.");
                    }
                }
            }
            else
            {
                // If it's not the end of the game, proceed with others levels 
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
        }

        public void DrawIntermissionScreen(SpriteBatch _spriteBatch)
        {
            // Center screen positioning
            Vector2 screenCenter = new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 2);

            if (GameReference.levelId + 1 < MapManager.Maps.Count)
            {
                // Text configuration for level completion screen
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
                string gameOverText = "Congratulations! Enter your Initials!";
                string userNameText = $"Name: {userName}";
                string totalScoreText = $"Total Score: {MapManager.levelScores.Values.Sum()}";
                string totalTimeText = $"Total Time: {MapManager.levelTimes.Values.Sum():F2} seconds";
                string restartText = "PRESS ENTER TO RESTART";

                // Measure string sizes
                Vector2 gameOverTextSize = GameReference.defaultFont.MeasureString(gameOverText);
                Vector2 userNameTextSize = GameReference.defaultFont.MeasureString(userNameText);
                Vector2 totalScoreTextSize = GameReference.defaultFont.MeasureString(totalScoreText);
                Vector2 totalTimeTextSize = GameReference.defaultFont.MeasureString(totalTimeText);
                Vector2 restartTextSize = GameReference.defaultFont.MeasureString(restartText);

                // Positions for text
                Vector2 gameOverTextPosition = screenCenter - new Vector2(gameOverTextSize.X / 2, 70);
                Vector2 userNameTextPosition = screenCenter - new Vector2(userNameTextSize.X / 2, -50);
                Vector2 totalScoreTextPosition = screenCenter - new Vector2(totalScoreTextSize.X / 2, 30);
                Vector2 totalTimeTextPosition = screenCenter - new Vector2(totalTimeTextSize.X / 2, -10);
                Vector2 restartTextPosition = screenCenter - new Vector2(restartTextSize.X / 2, -90);

                // Draw text
                _spriteBatch.DrawString(GameReference.defaultFont, gameOverText, gameOverTextPosition, Color.Red);
                _spriteBatch.DrawString(GameReference.defaultFont, totalScoreText, totalScoreTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, totalTimeText, totalTimeTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, userNameText, userNameTextPosition, Color.White);
                _spriteBatch.DrawString(GameReference.defaultFont, restartText, restartTextPosition, Color.Yellow);


                // Draw Top 3 Scores

                List<Tuple<string, int>> topScores = LoadTopScores();

                // Top-right corner position
                Vector2 topRightPosition = new Vector2(GameReference.viewport.Width - 150, 20);

                string topScoresLabel = "Top 3 MINERS:";
                Vector2 topScoresLabelSize = GameReference.defaultFont.MeasureString(topScoresLabel);

                // Draw the label for Top 3 scores
                _spriteBatch.DrawString(GameReference.defaultFont, topScoresLabel, topRightPosition, Color.Yellow);

                // Loop through the top 3 scores and draw them
                for (int i = 0; i < topScores.Count; i++)
                {
                    string scoreText = $"{topScores[i].Item1}: {topScores[i].Item2}";

                    // Measure the width of the score text
                    Vector2 scoreTextSize = GameReference.defaultFont.MeasureString(scoreText);

                    // Draw each score with a vertical offset
                    _spriteBatch.DrawString(GameReference.defaultFont, scoreText,
                        topRightPosition + new Vector2(0, (i + 1) * 30), Color.White);
                }

            }
        }
    }
}
