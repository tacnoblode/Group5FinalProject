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
        public bool enemyWalkFrame = false;

        private Random enemyMoveRandom = new Random();


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
            enemyWalkFrame = (enemyMoveRandom.Next(0,1) == 0);
        }

        public void MoveEnemy()
        {
            if (Rotation == Vector2.Zero) { Rotation = new Vector2(0, 1); }

            // calculates enemy next postion based on it's position and rotation
            Vector2 enemyNextPosition = Position + Rotation;

            // gets object located at the enemy's next position on the map
            char objectInPath = MapManager.GetObjectAtCoordinate(enemyNextPosition);

            // rotates enemy randomly if next object in path is a wall or previous player location, otherwise updates position to next position
            if (objectInPath == '#')
            {
                RotateEnemyRandomly();
            }
            else if (objectInPath == 'p')
            {
                RotateEnemyRandomly();
            }
            else
            {
                if (enemyMoveRandom.Next(0, MapManager.AllEnemies.Count) == 0) { GameReference.snd_EnemyMove.Play(); }
                Position = enemyNextPosition;
                enemyWalkFrame = !enemyWalkFrame;
            }
            // This method will be called whenever the game wants to move the enemy (about 10 times a second)
            // TODO: Move the enemy in whatever direction it is facing
            // TODO: When the enemy reaches a wall, rotate the enemy to move in a different direction

            // You should use the function MapManager.GetObjectAtCoordinate(Vector2) to check what the space ahead of the enemy is.
            // GetObjectAtCoordinate(Vector2) returns a char representing what object is in that space. If it returns '#', that means the object at that coordinate is the wall.
        }

        private void RotateEnemyRandomly()
        {
            // Switch case for the 4 directions the enemy will choose from to move randomly
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