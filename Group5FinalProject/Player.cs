using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Group5FinalProject
{
	internal class Player
	{
		// Game references
		Game1 GameReference;
		MapManager MapManager;
		Camera Camera;

		// Control Variables
		public Vector2 Position;


		public Player(Vector2 position, Game1 gameRef, MapManager mapManager, Camera camera)
		{
			Position = position;
			MapManager = mapManager;
			GameReference = gameRef;
			Camera = camera;
		}

		public void DoInputLogic()
		{

		}
	}
}
