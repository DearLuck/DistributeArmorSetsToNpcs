using System.Collections.Immutable;

namespace RecombinePatch.Ingress.DtoV1
{
    public record RootV1
    (
        string Version,
        string GameRelease,
        string GameDataPath,
        PluginV1 Patch,
        ImmutableArray<PluginV1> Plugins
    );
}