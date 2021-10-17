using System.Collections.Generic;

namespace RecombinePatch.Ingress.DtoV1
{
    public class RootV1
    {
        public RootV1(string version,
            string gameRelease,
            string gameDataPath,
            PluginV1 patch,
            IDictionary<int, PluginV1> plugins,
            IDictionary<int, FormKeyV1> formKeys,
            IDictionary<int, ArmorV1> armorItems,
            IDictionary<int, WeaponV1> weaponItems,
            IDictionary<int, LeveledItemV1> leveledItems,
            IDictionary<int, OutfitV1> outfits, 
            IDictionary<int, ContainerV1> containers,
            IDictionary<int, IDictionary<int, ItemGroupEntryV1>> itemGroups)
        {
            Version = version;
            GameRelease = gameRelease;
            GameDataPath = gameDataPath;
            Patch = patch;
            Plugins = plugins;
            FormKeys = formKeys;
            ArmorItems = armorItems;
            WeaponItems = weaponItems;
            LeveledItems = leveledItems;
            ItemGroups = itemGroups;
            Outfits = outfits;
            Containers = containers;
        }

        /// <summary>
        /// File version - for compatibility check.
        /// </summary>
        public string Version { get; }
        
        /// <summary>
        /// Game release name.
        /// </summary>
        public string GameRelease { get; }
        
        /// <summary>
        /// Full path to the game's Data folder
        /// </summary>
        public string GameDataPath { get; }
        
        /// <summary>
        /// Target plugin that will contain the patch.
        /// </summary>
        public PluginV1 Patch { get; }
        
        /// <summary>
        /// List of all plugins seen by patcher. Key is export-only plugin id.
        /// </summary>
        public IDictionary<int, PluginV1> Plugins { get; }
        
        /// <summary>
        /// Key is export-only form key id. Value is combination of export-only plugin id and mod-local form id.
        /// </summary>
        public IDictionary<int, FormKeyV1> FormKeys { get; }

        /// <summary>
        /// Key is export-only form-id.
        /// </summary>
        public IDictionary<int, ArmorV1> ArmorItems { get; }
        
        /// <summary>
        /// Key is export-only form-id.
        /// </summary>
        public IDictionary<int, WeaponV1> WeaponItems { get; }
        
        /// <summary>
        /// Key is export-only form-id.
        /// </summary>
        public IDictionary<int, LeveledItemV1> LeveledItems { get; }
        
        /// <summary>
        /// Key is export-only form-id.
        /// </summary>
        public IDictionary<int, OutfitV1> Outfits { get; }
        
        /// <summary>
        /// Key is export-only form-id.
        /// </summary>
        public IDictionary<int, ContainerV1> Containers { get; }
        
        /// <summary>
        /// Key is export-only id identifying the item group.
        /// </summary>
        public IDictionary<int, IDictionary<int, ItemGroupEntryV1>> ItemGroups { get; }
    };
}