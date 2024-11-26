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
			TempPosition = player.Position;
			Position += ((TempPosition + new Vector2(-6, -3)) - Position) * 1.15f;
		}

		public void DrawUIOnScreen()
		{
			// TODO: Draw the UI (Text that shows the player's score, the time spent in the level, and what level # the player is on.)
		}
	}
}
