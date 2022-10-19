using System;
using System.IO;
using UnityEngine;
using PeacefulBeast.ModLib.DataModels;

namespace PeacefulBeast.ModLib.Example
{
    public class ModManager : MonoBehaviour
    {
        public ConfigFileType configType;

        private void Start()
        {
            CreateEmpty();
        }

        private void CreateEmpty()
        {
            Debug.Log("Creating empty mod...");
            try
            {
                File.CreateEmptyMod
                (
                    "ExampleMod",
                    Path.Combine(Application.streamingAssetsPath, "Mods"),
                    configType
                );
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }

            Debug.Log("Done!");
        }

        private static void LoadModInfoFromZip()
        {
            var modInfo = new ModInfo();
            modInfo = Zip.Zip.LoadToml<ModInfo>(Application.streamingAssetsPath + "/Mods/TestMod.zip",
                "TestMod/info.toml");
            Debug.Log(modInfo.Name);
            Debug.Log(modInfo.Version);
        }
    }
}