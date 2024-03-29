﻿using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using RecombinePatch.Analysis.Chance;

namespace RecombinePatch.Analysis.Groups
{
    public class GroupItem
    {
        public IItemGetter Item { get; }
        public DropStats Drops { get; }
        public Dictionary<FormKey, DropBranch> DropBranches { get; }
        
        /// <summary>
        /// This list or outfit has this item added directly instead of over another list
        /// </summary>
        public int ActualEntryCount { get; set; }
        
        public GroupItem(IItemGetter item, Drop drop, int actualEntryCount)
        {
            Item = item;
            Drops = DropStats.From(drop);
            DropBranches = new Dictionary<FormKey, DropBranch>();
            ActualEntryCount = actualEntryCount;
        }
        
        private GroupItem(IItemGetter item, DropStats drops, Dictionary<FormKey, DropBranch> dropBranches, int actualEntryCount)
        {
            Item = item;
            Drops = drops;
            DropBranches = dropBranches;
            ActualEntryCount = actualEntryCount;
        }

        public static GroupItem? FromSubGroup(IItemGetter item, Drop leveledListDrop, RecombineGroup subRecombineGroup, GroupItem subItem, float listChance)
        {
            DropStats? drops = null;
            
            foreach (var drop in subItem.Drops.Multiply(leveledListDrop, listChance))
            {
                if (drops == null)
                {
                    drops = DropStats.From(drop);
                }
                else
                {
                    drops.Add(drop);
                }
            }

            if (drops == null) return null;
            
            var dropBranches = new Dictionary<FormKey, DropBranch>();
            
            if (subRecombineGroup.LeveledItem != null)
            {
                dropBranches.Add(subRecombineGroup.FormKey,
                    new DropBranch(subRecombineGroup.LeveledItem, subItem.Drops, subItem.DropBranches));
            }

            return new GroupItem(item, drops, dropBranches, 0);
        }

        public void MergeSubGroup(Drop leveledListDrop, RecombineGroup subRecombineGroup, GroupItem subItem, float listChance)
        {
            if (subRecombineGroup.LeveledItem != null)
            {
                if (!DropBranches.ContainsKey(subRecombineGroup.FormKey))
                {
                    DropBranches.Add(subRecombineGroup.FormKey,
                        new DropBranch(subRecombineGroup.LeveledItem, subItem.Drops, subItem.DropBranches));
                }
            }

            foreach (var drop in subItem.Drops.Multiply(leveledListDrop, listChance))
            {
                Drops.Add(drop);
            }
        }

        public void WriteLine(string depth)
        {
            Drops.WriteLine(depth);

            foreach (var branch in DropBranches)
            {
                branch.Value.WriteLine($"{depth}  ");
            }
        }
    }
}
