using System;
using System.IO;
using PeacefulBeast.ModLib.Mod;
using UnityEngine;

namespace PeacefulBeast.ModLib
{
    public abstract class Mod<T> where T : IModConfig
    {
        public IModConfig Config {get; private set; }
        public string ModPath { get; }
        public ConfigFileType UsedConfigType { get; private set; }

        protected Mod(T config, string modPath)
        {
            Config = config;
            ModPath = modPath;
        }

        public virtual void Init()
        {
            try
            {
                CheckConfigFile();
                LoadConfig();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }

        protected virtual void LoadConfig()
        {
            Config = UsedConfigType switch
            {
                ConfigFileType.Json => File.LoadJSON<T>(ModPath),
                ConfigFileType.Toml => File.LoadTOML<T>(ModPath),
                _ => throw new Exception("Config language not supported")
            };
        }

        private void CheckConfigFile()
        {
            if (File.Exists(Path.Combine(Config.AssetsPath, "config.json")))
                UsedConfigType = ConfigFileType.Json;
            else if (File.Exists(Path.Combine(Config.AssetsPath, "config.toml")))
                UsedConfigType = ConfigFileType.Toml;
            else
                throw new FileNotFoundException("No config file found");
        }
    }
}