using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Group5FinalProject
{
    internal class TitleScreenManager
    {
        Game1 GameReference;
        MapManager MapManager;


        public TitleScreenManager(Game1 gameReference, MapManager mapManager)
        {
            GameReference = gameReference;
            MapManager = mapManager;
        }

        public void TitleScreenInputs()
        {
            // TODO: Finish the code in this function with the controls needed.
            // INCLUDE: Left and right arrows control which level you want to select
            // INCLUDE: Enter key should start the game

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Set the game state to 1 (The main game stage)
                GameReference.gameState = 1;


                // Set and use GameReference.levelId for which level you want to load.
                MapManager.LoadMap(GameReference.levelId);
            }
        }
        public void DrawTitleScreen(SpriteBatch _spriteBatch)
        {
            // TODO: Replace everything in this function with a more polished main menu.
            // INCLUDE: A title of the game
            // INCLUDE: A list of the levels available to play (use the MapManager reference to find how many maps there are)
            // INCLUDE: Use the left and right arrows to switch what level you want to play
            // INCLUDE: A label for "Press enter to play" and the controls necessary to enter the game.

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
