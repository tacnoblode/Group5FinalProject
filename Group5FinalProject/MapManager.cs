using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Group5FinalProject
{
    internal class MapManager
    {
        Game1 GameReference;

        string[] TempMap =
        {
            "##########",
            "#____#___#",
            "#____#_X_#",
            "#____###_#",
            "#________#",
            "##########"
        };

        public MapManager(Game1 gameReference)
        {
            GameReference = gameReference;
        }

        public void RenderMap(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < TempMap.Length; i++)
            {
                for (int j = 0; j < TempMap[i].Length; j++)
                {
                    if (TempMap[i][j] == '#') { spriteBatch.Draw(GameReference.fallbackTexture, new Vector2(j * 64, i * 64) + GameReference.CameraPosition,Color.Black); }
                    if (TempMap[i][j] == 'X') { spriteBatch.Draw(GameReference.fallbackTexture, new Vector2(j * 64, i * 64) + GameReference.CameraPosition,Color.Brown); }
                }
            }

        }
    }
}