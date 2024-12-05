using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
			// TODO: Draw the UI (Text that shows the player's score, the time spent in the level, and what level # the player is on.)
		}
	}
}
