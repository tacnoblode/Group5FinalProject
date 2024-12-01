using Microsoft.Xna.Framework;
using System;

namespace Group5FinalProject
{
    internal class Enemy
    {
        // Game references
        Game1 GameReference;
        MapManager MapManager;

        // Control Variables
        public Vector2 Position = Vector2.Zero;
        public Vector2 Rotation = Vector2.Zero;
        public bool isEnemyActive = true;

        private int enemyMoveCounter = 0;
        private Random enemyMoveRandom = new Random();

        // position =+ 1 * rotation
        // ROTATION will represent which axis the enemy moves.
        // (-1,0) - Enemy will move LEFT on the screen
        // (1,0) - Enemy will move RIGHT on the screen
        // (0,1) - Enemy will move DOWN on the screen
        // (0,-1) - Enemy will move UP on the screen

        public Enemy(Game1 gameReference, MapManager mapManager, Vector2 position)
        {
            GameReference = gameReference;
            MapManager = mapManager;
            Position = position;
        }

        public void MoveEnemy()
        {
            enemyMoveCounter++;
            if (enemyMoveCounter < 7)
                return;

            enemyMoveCounter = 0;

            if (Rotation == Vector2.Zero) { Rotation = new Vector2(0, 1); }

            Vector2 enemyNextPosition = Position + Rotation;
            char objectInPath = MapManager.GetObjectAtCoordinate(enemyNextPosition);

            if (objectInPath == '#')
            {
                RotateEnemyRandomly();
            }
            else
            {
                Position = enemyNextPosition;
            }
            // This method will be called whenever the game wants to move the enemy (about 10 times a second)
            // TODO: Move the enemy in whatever direction it is facing
            // TODO: When the enemy reaches a wall, rotate the enemy to move in a different direction

            // You should use the function MapManager.GetObjectAtCoordinate(Vector2) to check what the space ahead of the enemy is.
            // GetObjectAtCoordinate(Vector2) returns a char representing what object is in that space. If it returns '#', that means the object at that coordinate is the wall.
        }

        private void RotateEnemyRandomly()
        {
            int moveRandomDirection = enemyMoveRandom.Next(4);
            switch (moveRandomDirection)
            {
                case 0:
                    Rotation = new Vector2(0, 1);
                    break;

                case 1:
                    Rotation = new Vector2(-1, 0);
                    break;

                case 2:
                    Rotation = new Vector2(0, -1);
                    break;

                case 3:
                    Rotation = new Vector2(1, 0);
                    break;
            }
        }
    }
}
