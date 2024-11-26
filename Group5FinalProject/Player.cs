using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

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
		bool IsKeyAlreadyPressed = false;

		// List of objects to collide with
		char[] objectsToCollideWith = {'#','p','X'};


		public Player(Vector2 position, Game1 gameRef, MapManager mapManager, Camera camera)
		{
			Position = position;
			MapManager = mapManager;
			GameReference = gameRef;
			Camera = camera;
		}

		public void DoInputLogic()
		{
			// Actual input logic here

			// If R key is pressed, reset the level.
			if (Keyboard.GetState().IsKeyDown(Keys.R)) { MapManager.LoadMap(GameReference.levelId); }

			if (!IsKeyAlreadyPressed)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Left) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(-1, 0)))) { MapManager.ReplaceObjectAtPositionWith(Position, 'p'); Position.X -= 1; IsKeyAlreadyPressed = true; }
				if (Keyboard.GetState().IsKeyDown(Keys.Right) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(1, 0)))) { MapManager.ReplaceObjectAtPositionWith(Position, 'p'); Position.X += 1; IsKeyAlreadyPressed = true; }
				if (Keyboard.GetState().IsKeyDown(Keys.Up) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(0, -1)))) { MapManager.ReplaceObjectAtPositionWith(Position, 'p'); Position.Y -= 1; IsKeyAlreadyPressed = true; }
				if (Keyboard.GetState().IsKeyDown(Keys.Down) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(0, 1)))) { MapManager.ReplaceObjectAtPositionWith(Position, 'p'); Position.Y += 1; IsKeyAlreadyPressed = true; }
			}
			else
			{
				if (!Keyboard.GetState().IsKeyDown(Keys.Left) && !Keyboard.GetState().IsKeyDown(Keys.Right) && !Keyboard.GetState().IsKeyDown(Keys.Up) && !Keyboard.GetState().IsKeyDown(Keys.Down))
				{IsKeyAlreadyPressed = false; }
			}
			
			// Check if the player is overlapping any objects and do logic accordingly
			if (MapManager.GetObjectAtCoordinate(Position) == 'F')
			{
				// TODO: Transition into intermisison screen.
				// TEMP CODE BELOW:
				GameReference.gameState = 0;
			}
		}
	}
}
