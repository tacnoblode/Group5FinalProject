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

        // The list of all the maps
        List<string[]> Maps = new List<string[]>();

        // The map that is loaded into memory
        List<string> CurrentMap;
		public List<Enemy> AllEnemies;

		public MapManager(Game1 gameReference)
        {
            GameReference = gameReference;
            LoadMapDatabase();
            AllEnemies = new List<Enemy>();
        }

        public void LoadMap(int mapIndex)
        {
            // This function loads whichever map specified into memory.

            CurrentMap = new List<string>();
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
                        MapLine += "-";
					}
					else if (Maps[mapIndex][i][j] == 'P')
					{
                        // If a player spawn is found, set the player's position to it, and change the line to be blank
                        player.Position = new Vector2(j, i);
						MapLine += "-";
					}
					else
                    {
						MapLine += Maps[mapIndex][i][j];
					}
                }
                CurrentMap.Add(MapLine);
            }
        }

        public void RenderMap(SpriteBatch spriteBatch)
        {
            // Map Rendering
            for (int i = 0; i < CurrentMap.Count; i++)
            {
                for (int j = 0; j < CurrentMap[i].Length; j++)
                {
                    if (CurrentMap[i][j] == '#') { spriteBatch.Draw(GameReference.fallbackTexture, new Vector2(j * 64, i * 64) + (camera.Position * 64), Color.Black); }
                    
                    if (CurrentMap[i][j] == 'X') { spriteBatch.Draw(GameReference.fallbackTexture, new Vector2(j * 64, i * 64) + (camera.Position * 64), Color.Brown); }
                }
            }

            // Enemy Rendering
            foreach (Enemy enemy in AllEnemies)
            {
                if (enemy.isEnemyActive)
                {
                    spriteBatch.Draw(GameReference.fallbackTexture, enemy.Position * 64 + (camera.Position * 64), Color.Red);
                }
            }

            spriteBatch.Draw(GameReference.fallbackTexture, player.Position * 64 + (camera.Position * 64), Color.Green);
        }

		public void SetPlayerReference(Player player)
		{
			this.player = player;
		}

		public void SetCameraReference(Camera camera)
		{
			this.camera = camera;
		}

		// MAP DATABASE BELOW //

		// Symbols:
		// # - Wall object
		// E - Enemy object
		// P - Player spawn point

		public void LoadMapDatabase()
		{
			// Place your maps in one of these functions to add them into the list
			Maps.Add(new string[]
			{
				"##########",
				"#_E__#___#",
				"#____#_X_#",
				"#____###_#",
				"#P_______#",
				"##########"
			});

		}
	}
}