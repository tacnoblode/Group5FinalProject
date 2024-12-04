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
            // TODO: Replace the code in this function with the text/graphics for an intermission screen (the screen between advancing to the next level)
            // INCLUDE: A label showing your total score in the level (from GameReference.gameScore)
            // INCLUDE: A label telling you what level is next (eg. Entering Level 2...)
            // INCLUDE: A label that says "Level Completed!" or something similar
            // INCLUDE: A label that tells the player to press enter to continue.

            // Configure Content to show on the screen
            string gameTitle = "SUPER AWESOME GAME";
            Vector2 FontOrigin = GameReference.defaultFont.MeasureString(gameTitle) / 2;
            Vector2 FontPosition = new Vector2(GameReference.viewport.Width / 2, GameReference.viewport.Height / 2);

            // Draw the contents onto the main menu screen
            _spriteBatch.DrawString(GameReference.defaultFont, gameTitle, FontPosition - FontOrigin, Color.White);
            _spriteBatch.DrawString(GameReference.defaultFont, "PRESS ENTER TO START", FontPosition - FontOrigin + new Vector2(0, 200), Color.White);

            // Debug: Show the time elapsed
            _spriteBatch.DrawString(GameReference.defaultFont, $"Seconds Elapsed: {GameReference.SecondsElapsed}", new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(GameReference.defaultFont, $"Frames Elapsed: {GameReference.FramesElapsed}", new Vector2(0, 16), Color.White);
        }
    }
}
