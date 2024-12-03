using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace Group5FinalProject
{
    internal class MapManager
    {
        Game1 GameReference;
        Player player;
        Camera camera;
        double timeSinceLastEnemyUpdate;

        // The list of all the maps
        List<string[]> Maps = new List<string[]>();

        // The map that is loaded into memory
        public List<string> CurrentMap;
		public List<Enemy> AllEnemies;

		public MapManager(Game1 gameReference)
        {
            GameReference = gameReference;
            LoadMapDatabase();
            AllEnemies = new List<Enemy>();
        }

        public void LoadMap(int mapIndex)
        {
            // Reset the score
            GameReference.gameScore = 0;

            // This function loads whichever map specified into memory.

            CurrentMap = new List<string>();
            AllEnemies = new List<Enemy>();
            if (Maps.Count > mapIndex)
            {
                for (int i = 0; i < Maps[mapIndex].Length; i++)
                {
                    string MapLine = "";
                    for (int j = 0; j < Maps[mapIndex][i].Length; j++)
                    {
                        if (Maps[mapIndex][i][j] == 'E')
                        {
                            // If an enemy is found in the loading process, create a new enemy object
                            // and change the line to be a blank character.
                            AllEnemies.Add(new Enemy(GameReference, this, new Vector2(j, i)));
                            MapLine += '_';
                        }
                        else if (Maps[mapIndex][i][j] == 'P')
                        {
                            // If a player spawn is found, set the player's position to it, and change the line to be blank
                            player.Position = new Vector2(j, i);
                            MapLine += '_';
                        }
                        else
                        {
                            MapLine += Maps[mapIndex][i][j];
                        }
                    }
                    CurrentMap.Add(MapLine);
                }
            }
        }

        public void RenderMap(SpriteBatch spriteBatch)
        {
            if (GameReference.SecondsElapsed > timeSinceLastEnemyUpdate)
            {
                timeSinceLastEnemyUpdate = GameReference.SecondsElapsed + 0.5;
                foreach (Enemy enemy in AllEnemies)
                {
                    enemy.MoveEnemy();
                }
            }

            // Map Rendering
            for (int i = 0; i < CurrentMap.Count; i++)
            {
                for (int j = 0; j < CurrentMap[i].Length; j++)
                {
                    if (CurrentMap[i][j] == '#') { spriteBatch.Draw(GameReference.spr_Rock, new Vector2(j * 64, i * 64) + (camera.Position * -64), Color.White); }
                    if (CurrentMap[i][j] == '_' || CurrentMap[i][j] == 'E') { spriteBatch.Draw(GameReference.spr_Rock, new Vector2(j * 64, i * 64) + (camera.Position * -64), Color.Gray); }
                    
                    if (CurrentMap[i][j] == 'F') { spriteBatch.Draw(GameReference.fallbackTexture, new Vector2(j * 64, i * 64) + (camera.Position * -64), Color.Yellow); }
                    if (CurrentMap[i][j] == 'p') { spriteBatch.Draw(GameReference.spr_Rock, new Vector2(j * 64, i * 64) + (camera.Position * -64), new Color(64, 64, 64)); }
                }
            }

            // Enemy Rendering
            foreach (Enemy enemy in AllEnemies)
            {
                if (enemy.isEnemyActive)
                {
                    if (enemy.enemyWalkFrame) { spriteBatch.Draw(GameReference.spr_Enemy0, enemy.Position * 64 + (camera.Position * -64), Color.White); }
                    else { spriteBatch.Draw(GameReference.spr_Enemy1, enemy.Position * 64 + (camera.Position * -64), Color.White); }
                }
            }


            if (player.PickaxeSwing) { spriteBatch.Draw(GameReference.spr_Player0, player.Position * 64 + (camera.Position * -64), Color.White); }
            else { spriteBatch.Draw(GameReference.spr_Player1, player.Position * 64 + (camera.Position * -64), Color.White); }
        }

		public void SetPlayerReference(Player player)
		{
			this.player = player;
		}

		public void SetCameraReference(Camera camera)
		{
			this.camera = camera;
		}


        public char GetObjectAtCoordinate(Vector2 coords)
        {
            // This function should work not just for the player but every class that needs collision.
            // For collision checking: Check the space that the object is trying to move to with this function
            // (eg. if an object is at {0,0} and is moving right, check if GetObjectAtCoordinate({1,0}) != "#"
            if (coords.X >= 0 && coords.Y >= 0 && CurrentMap.Count > coords.Y)
            {
                if (CurrentMap[Convert.ToInt32(coords.Y)].Length > coords.X)
                {
                    // Check all enemies first to see if they're at the position
                    foreach( Enemy enemy in AllEnemies)
                    {
                        if (enemy.isEnemyActive && enemy.Position == coords) { Debug.Print("E"); return 'E'; }
                    }
                    // If the code hasn't returned from any of those, return whatever's at the map at the coords
                    return CurrentMap[Convert.ToInt32(coords.Y)][Convert.ToInt32(coords.X)];

				}
            }
            // If the coordinates are outside the map range, just return a blank space instead.
            return '_';
        }

        public void ReplaceObjectAtPositionWith(Vector2 coords, char ToReplaceWith)
        {
            // Check if the position at the coordinates are valid
			if (coords.X >= 0 && coords.Y >= 0 && CurrentMap.Count > coords.Y)
			{
				if (CurrentMap[Convert.ToInt32(coords.Y)].Length > coords.X)
				{
                    string MapLine = "";
                    for (int i = 0; i < CurrentMap[Convert.ToInt32(coords.Y)].Length; i++)
                    {
                        if(i == Convert.ToInt32(coords.X)) { MapLine += ToReplaceWith; }
                        else { MapLine += CurrentMap[Convert.ToInt32(coords.Y)][i]; }
                    }
					CurrentMap[Convert.ToInt32(coords.Y)] = MapLine;
				}
			}
		}

		// MAP DATABASE BELOW //

		// Symbols:
		// # - Wall object
		// E - Enemy object
		// P - Player spawn point
		// F - Level End Flag
        // p - The previous positions the player were in
        // v - Void (this character is only to add a space, it will render as black ingame

		public void LoadMapDatabase()
		{
			// Place your maps in one of these functions to add them into the list
			Maps.Add(new string[]
			{
				"###################",
				"#_P__#___E________#",
				"#____#___##__#____#",
				"#____#_______###__#",
				"##__#######__#____#",
				"v#__#_E___#__#__###",
				"v#__#___E____#____#",
				"v#__#________#___F#",
				"##__#######_####__#",
				"#____________E_####",
				"#####__###_____#vv",
				"vvvv####v#######vv"
			});
            Maps.Add(new string[]
{
                "######################",
                "#P___#__________E____#",
                "#____#______E________#",
                "#________##########__#",
                "#________#________#__#",
                "##########____##__#__#",
                "#__F________E_____#__#",
                "#____##______________#",
                "######################",
});
        }
	}
}