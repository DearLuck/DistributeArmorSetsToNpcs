using System.Collections.Generic;
using MessagePack;

namespace RecombinePatch.Ingress.DtoV1
{
    [MessagePackObject]
    public class RootV1
    {
        public RootV1(uint version, string gameRelease, string gameDataPath, 
            PluginV1 patch, PluginV1[] plugins, 
            ArmorV1[] armors, WeaponV1[] weapons, MiscV1[] miscs, 
            List<LeveledItemV1> leveledItems, List<OutfitV1> outfits, List<ContainerV1> containers, 
            List<ItemGroupV1> itemGroups)
        {
            Version = version;
            GameRelease = gameRelease;
            GameDataPath = gameDataPath;
            Patch = patch;
            Plugins = plugins;
            Armors = armors;
            Weapons = weapons;
            Miscs = miscs;
            LeveledItems = leveledItems;
            Outfits = outfits;
            Containers = containers;
            ItemGroups = itemGroups;
        }

        [Key(0)] public readonly uint Version;
        [Key(1)] public readonly string GameRelease;
        [Key(2)] public readonly string GameDataPath;
        
        [Key(3)] public readonly PluginV1 Patch;
        [Key(4)] public readonly PluginV1[] Plugins;
        
        [Key(5)] public readonly ArmorV1[] Armors;
        [Key(6)] public readonly WeaponV1[] Weapons;
        [Key(7)] public readonly MiscV1[] Miscs;
        
        [Key(8)] public readonly List<LeveledItemV1> LeveledItems;
        [Key(9)] public readonly List<OutfitV1> Outfits;
        [Key(10)] public readonly List<ContainerV1> Containers;
        
        [Key(11)] public readonly List<ItemGroupV1> ItemGroups;
    };
}