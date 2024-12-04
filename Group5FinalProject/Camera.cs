using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

namespace Group5FinalProject
{
	internal class Camera
	{
		// References
		Player player;
		Game1 GameReference;

		// Control Variables
		public Vector2 Position;

		public Vector2 TempPosition;

		public Camera(Vector2 position, Game1 GameRef)
		{
			Position = position;
			GameReference = GameRef;
		}

		public void SetPlayerReference(Player player)
		{
			this.player = player;
		}

		public void UpdatePosition()
		{
			// Linear interpolate to the player's position + an offset
			TempPosition = player.Position;
			Position += ((TempPosition + new Vector2(-6, -3)) - Position) * 1.15f;
		}

        // Don't worry about any of the code above, it's just the UI that we need to worry about.

        public void DrawUIOnScreen(SpriteBatch _spriteBatch)
        {
            // Ensure the SpriteBatch is started before calling this
            _spriteBatch.DrawString(GameReference.defaultFont, $"Score: {GameReference.gameScore}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(GameReference.defaultFont, $"Level: {GameReference.levelId}", new Vector2(10, 40), Color.White);
            _spriteBatch.DrawString(GameReference.defaultFont, $"Time: {GameReference.SecondsElapsed} seconds", new Vector2(10, 70), Color.White);
            // Optional: Add instructions for restarting
            _spriteBatch.DrawString(GameReference.defaultFont, "Press R to restart level", new Vector2(10, 100), Color.White);
        }
    }
}
