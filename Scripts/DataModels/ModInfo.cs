#if ODIN_INSPECTOR
using Sirenix.Serialization;
#endif

namespace PeacefulBeast.ModLib.DataModels
{
    public class ModInfo
    {
        #if ODIN_INSPECTOR
            [OdinSerialize] public string Name { get; set; }
            [OdinSerialize] public string Version { get; set; }
            [OdinSerialize] public string Description { get; set; }
        #else
            public string Name { get; set; }
            public string Version { get; set; }
            public string Description { get; set; }
        #endif
    }
}
