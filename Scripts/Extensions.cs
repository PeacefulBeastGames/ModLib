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
    }
}