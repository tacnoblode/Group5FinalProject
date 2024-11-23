using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Group5FinalProject
{
	internal class Camera
	{
		// References
		Player player;

		// Control Variables
		public Vector2 Position;

		public Vector2 TempPosition;

		public Camera(Vector2 position)
		{
			Position = position;
		}

		public void SetPlayerReference(Player player)
		{
			this.player = player;
		}

		public void UpdatePosition()
		{
			TempPosition += new Vector2((float)((player.Position.X - TempPosition.X) * 1.05), (float)((player.Position.Y - TempPosition.Y) * 1.05));
			Position = TempPosition + new Vector2(4f, -5f);
		}
	}
}
