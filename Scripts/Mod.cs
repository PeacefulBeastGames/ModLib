using PeacefulBeast.ModLib.Mod;

namespace PeacefulBeast.ModLib
{
    public class Mod<T> where T : IModConfig
    {
        private IModConfig _config;

        public Mod(T config)
        {
            _config = config;
        }

        public T GetConfig()
        {
            return (T)_config;
        }
    }
}
