using System.Collections.Generic;
using MessagePack;

namespace RecombinePatch.Ingress.DtoV1
{
    [MessagePackObject]
    public record ArmorV1
    {
        [Key(0)]
        public readonly FormKeyV1 FormKey;
        [Key(1)]
        public readonly string? EditorId;
        [Key(2)]
        public readonly string? Name;
        [Key(3)]
        public readonly List<uint> OutfitGroups;
        [Key(4)]
        public readonly List<uint> ContainerGroups;
        [Key(5)]
        public readonly List<uint> LeveledItemGroups;

        public ArmorV1(FormKeyV1 formKey, string? editorId, string? name, List<uint> outfitGroups, List<uint> containerGroups, List<uint> leveledItemGroups)
        {
            FormKey = formKey;
            EditorId = editorId;
            Name = name;
            OutfitGroups = outfitGroups;
            ContainerGroups = containerGroups;
            LeveledItemGroups = leveledItemGroups;
        }
    }

    [MessagePackObject]
    public record WeaponV1
    {
        [Key(0)]
        public readonly FormKeyV1 FormKey;
        [Key(1)]
        public readonly string? EditorId;
        [Key(2)]
        public readonly string? Name;
        [Key(3)]
        public readonly List<uint> OutfitGroups;
        [Key(4)]
        public readonly List<uint> ContainerGroups;
        [Key(5)]
        public readonly List<uint> LeveledItemGroups;
        
        public WeaponV1(FormKeyV1 formKey, string? editorId, string? name, List<uint> outfitGroups, List<uint> containerGroups, List<uint> leveledItemGroups)
        {
            FormKey = formKey;
            EditorId = editorId;
            Name = name;
            OutfitGroups = outfitGroups;
            ContainerGroups = containerGroups;
            LeveledItemGroups = leveledItemGroups;
        }
    }
    
    [MessagePackObject]
    public record MiscV1
    {
        [Key(0)]
        public readonly FormKeyV1 FormKey;
        [Key(1)]
        public readonly string? EditorId;
        [Key(2)]
        public readonly string? Name;
        [Key(3)]
        public readonly List<uint> OutfitGroups;
        [Key(4)]
        public readonly List<uint> ContainerGroups;
        [Key(5)]
        public readonly List<uint> LeveledItemGroups;
        
        public MiscV1(FormKeyV1 formKey, string? editorId, string? name, List<uint> outfitGroups, List<uint> containerGroups, List<uint> leveledItemGroups)
        {
            FormKey = formKey;
            EditorId = editorId;
            Name = name;
            OutfitGroups = outfitGroups;
            ContainerGroups = containerGroups;
            LeveledItemGroups = leveledItemGroups;
        }
    }
}