using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tomlet;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace PeacefulBeast.ModLib
{
    /// <summary>
    /// Helper functions to work with the filesystem
    /// </summary>
    public static class File
    {
        /// <summary>
        /// Deserializes a file.
        /// </summary>
        /// <typeparam name="T">Object type into which deserialize the file</typeparam>
        /// <param name="path">Path to the file to be deserialized</param>
        /// <returns>Object of type T</returns>
        public static T LoadTOML<T>(string path)
        {
            var tomlString = System.IO.File.ReadAllText(path);
            return TomletMain.To<T>(tomlString);
        }
        /// <summary>
        /// Serializes an object to a file.
        /// </summary>
        /// <typeparam name="T">Type to serialize</typeparam>
        /// <param name="path">Path to save the file to</param>
        /// <param name="obj">Object to serialize</param>
        public static void SaveTOML<T>(string path, T obj)
        {
            var tomlString = TomletMain.TomlStringFrom(obj);
            System.IO.File.WriteAllText(path, tomlString);
        }
        /// <summary>
        /// Creates a Texture from an image file. Image has to be square!
        /// </summary>
        /// <param name="path">Path to the image file.</param>
        /// <param name="size">Size of the image.</param>
        /// <param name="filterMode">Filter mode to apply to the texture.</param>
        public static Texture2D LoadTexture(string path, ushort size, FilterMode filterMode = FilterMode.Point)
        {
            byte[] imgBytes = System.IO.File.ReadAllBytes(path);

            Texture2D texture = new(size, size);
            texture.LoadImage(imgBytes);
            texture.filterMode = filterMode;

            return texture;
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
