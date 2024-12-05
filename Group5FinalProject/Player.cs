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
        private Random playerRNG = new Random();

        // List of objects to collide with
        char[] objectsToCollideWith = {'#','p','X'};

		// Animation and Sound Controls
		public bool PickaxeSwing = false;
		public bool Moved = false;

		public Player(Vector2 position, Game1 gameRef, MapManager mapManager, Camera camera)
		{
			Position = position;
			MapManager = mapManager;
			GameReference = gameRef;
			Camera = camera;
		}
		public void UpdateMoveState()
		{
            MapManager.ReplaceObjectAtPositionWith(Position, 'p');
            IsKeyAlreadyPressed = true;
            Moved = true;
            PickaxeSwing = !PickaxeSwing;
			GameReference.snd_Mine.Play();
            GameReference.gameScore++;
        }
		public void DoInputLogic()
		{
			// Actual input logic here

			// If R key is pressed, reset the level.
			if (Keyboard.GetState().IsKeyDown(Keys.R)) { MapManager.LoadMap(GameReference.levelId); }
            if (!IsKeyAlreadyPressed)
			{
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
				{
					IsKeyAlreadyPressed = true; 
				}

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(-1, 0)))) {
                    UpdateMoveState();
                    Position.X -= 1;
				}
				if (Keyboard.GetState().IsKeyDown(Keys.Right) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(1, 0)))) {
                    UpdateMoveState();
                    Position.X += 1;
                }
				if (Keyboard.GetState().IsKeyDown(Keys.Up) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(0, -1)))) {
                    UpdateMoveState();
                    Position.Y -= 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !objectsToCollideWith.Contains(MapManager.GetObjectAtCoordinate(Position + new Vector2(0, 1)))) {
                    UpdateMoveState();
                    Position.Y += 1;
                }
            }
			else
			{
				if (!Keyboard.GetState().IsKeyDown(Keys.Left) && !Keyboard.GetState().IsKeyDown(Keys.Right) && !Keyboard.GetState().IsKeyDown(Keys.Up) && !Keyboard.GetState().IsKeyDown(Keys.Down))
				{
					IsKeyAlreadyPressed = false;
                    if (!Moved) { GameReference.snd_InvalidMove.Play(); }
					Moved = false;
                }
			}
			
			// Check if the player is overlapping any objects and do logic accordingly
			if (MapManager.GetObjectAtCoordinate(Position) == 'F')
			{
				GameReference.gameState = 2;

				MapManager.levelFinishTime = MapManager.levelElapsedTime;
				MapManager.levelTimes[GameReference.levelId] = MapManager.levelFinishTime;
                MapManager.levelScores[GameReference.levelId] = GameReference.gameScore;

                GameReference.snd_EndLevel.Play();
            }
			if (MapManager.GetObjectAtCoordinate(Position) == 'E') { MapManager.LoadMap(GameReference.levelId); GameReference.snd_InvalidMove.Play(); }
		}
	}
}
