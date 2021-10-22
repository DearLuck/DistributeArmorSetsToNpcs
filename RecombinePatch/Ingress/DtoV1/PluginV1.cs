using MessagePack;

namespace RecombinePatch.Ingress.DtoV1
{
    public enum PluginType: byte
    {
        Master = 0,
        LightMaster = 1,
        Plugin = 2,
    }
    
    [MessagePackObject]
    public struct PluginV1
    {
        [Key(0)]
        public readonly ushort Index;
        [Key(1)]
        public readonly PluginType Type;
        [Key(2)]
        public readonly string ModKey;

        public PluginV1(ushort index, PluginType type, string modKey)
        {
            Index = index;
            Type = type;
            ModKey = modKey;
        }
    }
}