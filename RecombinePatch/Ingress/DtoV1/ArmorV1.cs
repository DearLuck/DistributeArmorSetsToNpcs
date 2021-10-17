using System.Collections.Generic;

namespace RecombinePatch.Ingress.DtoV1
{
    public record ArmorV1
    (
        string? Eid,
        string? Name,
        // ReSharper disable once InconsistentNaming
        IEnumerable<int> OIG, // Outfit item group
        // ReSharper disable once InconsistentNaming
        IEnumerable<int> CIG, // Container item group
        // ReSharper disable once InconsistentNaming
        IEnumerable<int> DIG // Direct item group
    );
    
    public record WeaponV1
    (
        string? Eid,
        string? Name,
        // ReSharper disable once InconsistentNaming
        IEnumerable<int> OIG, // Outfit item group
        // ReSharper disable once InconsistentNaming
        IEnumerable<int> CIG, // Container item group
        // ReSharper disable once InconsistentNaming
        IEnumerable<int> DIG // Direct item group
    );
}