using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace RecombinePatch.Analysis.Chance
{
    public struct DropBranch
    {
        public readonly ILeveledItemGetter LeveledItem;
        public readonly DropStats Drops;
        public readonly Dictionary<FormKey, DropBranch> DropBranches;

        public DropBranch(ILeveledItemGetter leveledItem, DropStats drops, Dictionary<FormKey, DropBranch> dropBranches)
        {
            LeveledItem = leveledItem;
            Drops = drops;
            DropBranches = dropBranches;
        }

        public void WriteLine(string depth)
        {
            Console.WriteLine($"{depth}{LeveledItem.FormKey} {LeveledItem.EditorID} {(100 - LeveledItem.ChanceNone) / 100f}");
            Drops.WriteLine(depth);
            foreach (var branch in DropBranches)
            {
                branch.Value.WriteLine($"{depth}  ");
            }
        }
    }
}