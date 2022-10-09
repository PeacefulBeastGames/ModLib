using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PeacefulBeast.ModLib
{
    public static class Extensions
    {
        /// <summary>
        /// Extension function to convert a Texture2D into a Sprite
        /// </summary>
        /// <param name="size">Size of the image</param>
        public static Sprite ToSprite(this Texture2D texture, ushort size)
        {
            var sprite = Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);

            return sprite;
        }

        /// <summary>
        /// Return positions of all the 8 neighbours.
        /// </summary>
        public static List<Vector2Int> Neighbours(this Vector2Int vector)
        {
            var result = new List<Vector2Int>();
            for (int y = 1; y > -1; y--)
            {
                for (int x = -1; x < 1; x++)
                {
                    if (x == 0 && y == 0) continue;
                    result.Add(new Vector2Int(x, y));
                }
            }
            return result;
        }
    }
}