using System.IO;
using Newtonsoft.Json;
using PeacefulBeast.ModLib.DataModels;
using PeacefulBeast.ModLib.Mod;
using Tomlet;
using UnityEngine;

namespace PeacefulBeast.ModLib
{
    /// <summary>
    /// Helper functions to work with the filesystem
    /// </summary>
    public static class File
    {
        #region TOML
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
        #endregion

        #region JSON

        /// <summary>
        /// Deserializes a file.
        /// </summary>
        /// <typeparam name="T">Object type into which deserialize the file</typeparam>
        /// <param name="path">Path to the file to be deserialized</param>
        /// <returns>Object of type T</returns>
        public static T LoadJSON<T>(string path)
        {
            var jsonString = System.IO.File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Serializes an object to a file.
        /// </summary>
        /// <typeparam name="T">Type to serialize</typeparam>
        /// <param name="path">Path to save the file to</param>
        /// <param name="obj">Object to serialize</param>
        public static void SaveJSON<T>(string path, T obj)
        {
            var jsonString = JsonConvert.SerializeObject(obj);
            System.IO.File.WriteAllText(path, jsonString);
        }

        #endregion
        
        #region Mod
        
        /// <summary>
        /// Creates an empty mod folder with default config file if it doesn't exist.
        /// </summary>
        public static void CreateEmptyMod(string modName, string path, ConfigFileType configType)
        {
            var modPath = Path.Combine(path, modName);
            if (!Directory.Exists(modPath))
            {
                Directory.CreateDirectory(modPath);
            }
            else
            {
                Debug.LogError($"Mod {modName} already exists!");
            }
            var fileName = $"config.{configType.ToString().ToLower()}";
            if (!Exists(Path.Combine(modPath, fileName)))
            {
                var modInfo = new ModInfo
                {
                    Name = modName,
                    Version = "0.1.0",
                    Description = "A mod made with ModLib"
                };
                //System.IO.File.Create(Path.Combine(modPath, fileName));
                switch (configType)
                {
                    case ConfigFileType.Json:
                        SaveJSON(Path.Combine(modPath, fileName), modInfo);
                        break;
                    case ConfigFileType.Toml:
                        SaveTOML(Path.Combine(modPath, fileName), modInfo);
                        break;
                }
            }
            else
            {
                Debug.LogError($"Config file for mod {modName} already exists!");
            }
        }

        public static void CreateEmptyMod<T>(Mod<T> mod) where T : IModConfig
        {
            CreateEmptyMod(mod.Config.Name, mod.ModPath, mod.UsedConfigType);
        }
        #endregion
        
        public static bool Exists(string path) => System.IO.File.Exists(path);

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
    }
}
