using System.Collections.Generic;
using System.IO;

namespace PeacefulBeast.ModLib.Mod
{
    public class ModLoader<M, T> where M : Mod<T>, new() where T : IModConfig
    {
        public Dictionary<string, M> Mods { get; set; }
        
        public void LoadAllMods(string path)
        {
            var mods = new Dictionary<string, M>();
            var modFolders = System.IO.Directory.GetDirectories(path);

            foreach (var modFolder in modFolders)
            {
                var modName = System.IO.Path.GetFileName(modFolder);
                var mod = new M();
                mods.Add(modName, mod);
            }

            Mods = mods;
        }
    }
}