using Mutagen.Bethesda.Plugins;
using RecombinePatch.Ingress.DtoV1;

namespace RecombinePatch.Ingress
{
    public class MapperV1
    {
        public static PluginV1 ModV1(ModKey modKey)
        {
            return new PluginV1(modKey.FileName, modKey.ToString(), 
                modKey.Type == ModType.Master 
                    ? "master"
                    : modKey.Type == ModType.Plugin
                        ? "plugin"
                        : "light");
        }
    }
}