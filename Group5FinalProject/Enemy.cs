using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

        // ROTATION will represent which axis the enemy moves.
        // (-1,0) - Enemy will move LEFT on the screen
        // (1,0) - Enemy will move RIGHT on the screen
        // (0,1) - Enemy will move UP on the screen
        // (0,-1) - Enemy will move DOWN on the screen

        public Enemy(Game1 gameReference, MapManager mapManager, Vector2 position)
        {
            GameReference = gameReference;
            MapManager = mapManager;
            Position = position;
        }

        public void MoveEnemy()
        {
            if (Rotation == Vector2.Zero) { Rotation = new Vector2(0, 1); }
            // This method will be called whenever the game wants to move the enemy (about 10 times a second)
            // TODO: Move the enemy in whatever direction it is facing
            // TODO: When the enemy reaches a wall, rotate the enemy to move in a different direction
        }
    }
}
