namespace PeacefulBeast.ModLib.Mod
{
    public interface IModConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }
}
