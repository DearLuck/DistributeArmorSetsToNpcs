using System.Collections.Immutable;

namespace RecombinePatch.Ingress.DtoV1
{
    public record ItemGroupEntryV1(
        ImmutableArray<int> SubGroupIds,
        ImmutableDictionary<short, float> Chances
    );
}