using UnityEngine;

namespace PeacefulBeast.ModLib
{
    public static class SpriteTools
    {
        /// <summary>
        /// Creates a list of sprites from a sprite sheet texture.
        /// </summary>
        /// <param name="texture2D">Texture to be sliced</param>
        /// <param name="spriteWidth">Width of individual sprite.</param>
        /// <param name="spriteHeight">Height of individual sprite.</param>
        public static Sprite[] CreateSpritesFromTexture(Texture2D texture2D, int spriteWidth, int spriteHeight)
        {
            if(texture2D.width%spriteWidth != 0 || texture2D.height%spriteHeight != 0)
            {
                Debug.LogError("Sprite sheet dimensions are not divisible by sprite dimensions.");
                return null;
            }
            var spriteCount = (texture2D.width / spriteWidth) * (texture2D.height / spriteHeight);
            var sprites = new Sprite[spriteCount];
            var spriteIndex = 0;
            for (int y = 0; y < texture2D.height; y += spriteHeight)
            {
                for (int x = 0; x < texture2D.width; x += spriteWidth)
                {
                    sprites[spriteIndex] = Sprite.Create(texture2D, new Rect(x, y, spriteWidth, spriteHeight), new Vector2(0.5f, 0.5f));
                    spriteIndex++;
                }
            }
            return sprites;
        }
        
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