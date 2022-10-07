using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace PeacefulBeast.ModLib.DataModels
{
    public class TileInfo
    {
        #if ODIN_INSPECTOR
            [SerializeField, PreviewField] public Sprite Sprite { get; private set; }
            [SerializeField] public string Path { get; set; }
            [SerializeField] public string Name { get; set; }
            [SerializeField] public int Size { get; set; }
        #else
            public Sprite Sprite { get; private set; }
            public string Path { get; set; }
            public string Name { get; set; }
            public int Size { get; set; }
        #endif
    }
}
