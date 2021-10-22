using Mutagen.Bethesda.Plugins;
using RecombinePatch.Ingress.DtoV1;

namespace RecombinePatch.Ingress
{
    public class MapperV1
    {
        public static PluginV1 ModV1(ushort index, ModKey modKey)
        {
            return new PluginV1(
                index, 
                modKey.Type switch
                {
                    ModType.Master => PluginType.Master,
                    ModType.Plugin => PluginType.Plugin,
                    _ => PluginType.LightMaster
                }, modKey.ToString());
        }
    }
}