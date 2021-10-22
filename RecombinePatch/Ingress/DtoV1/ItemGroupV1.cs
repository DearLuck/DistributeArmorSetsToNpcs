using System.Collections.Generic;
using MessagePack;

namespace RecombinePatch.Ingress.DtoV1
{
    [MessagePackObject]
    public struct ItemGroupV1
    {
        [Key(0)]
        public readonly uint Index;
        
        [Key(1)]
        public readonly FormKeyV1? LeveledList;
        
        [Key(2)]
        public readonly FormKeyV1? Outfit;
        
        [Key(3)]
        public readonly FormKeyV1? Container;

        [Key(4)]
        public readonly List<ItemGroupEntryV1> Entries;

        public ItemGroupV1(uint index, FormKeyV1? leveledList, FormKeyV1? outfit, FormKeyV1? container, List<ItemGroupEntryV1> entries)
        {
            Index = index;
            LeveledList = leveledList;
            Outfit = outfit;
            Container = container;
            Entries = entries;
        }
    }
    
    [MessagePackObject]
    public struct LevelChanceV1
    {
        [Key(0)]
        public readonly short Level;
        
        [Key(1)]
        public readonly float Chance;

        public LevelChanceV1(short level, float chance)
        {
            Level = level;
            Chance = chance;
        }
    }

    [MessagePackObject]
    public struct ItemGroupEntryV1
    {
        [Key(0)]
        public readonly FormKeyV1 ItemFormKey;
        [Key(1)]
        public readonly float? Flat;
        [Key(2)]
        public readonly LevelChanceV1[]? Leveled;
        [Key(3)]
        public readonly uint[] SubGroupIds;

        public ItemGroupEntryV1(FormKeyV1 itemFormKey, float? flat, LevelChanceV1[]? leveled, uint[] subGroupIds)
        {
            ItemFormKey = itemFormKey;
            Flat = flat;
            Leveled = leveled;
            SubGroupIds = subGroupIds;
        }
    }
}