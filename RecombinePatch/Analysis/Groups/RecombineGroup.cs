using System.Collections.Generic;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using RecombinePatch.Analysis.Chance;

namespace RecombinePatch.Analysis.Groups
{
    public class RecombineGroup
    {
        private RecombineGroup(FormKey formKey, string? editorId, ILeveledItemGetter? leveledItem, float listChance)
        {
            DisplayName = editorId == null ? formKey.ToString() : $"{editorId} [{formKey.ToString()}]";
            FormKey = formKey;
            EditorId = editorId;
            LeveledItem = leveledItem;
            ListChance = listChance;
        }

        public static RecombineGroup ForLeveledList(ILeveledItemGetter leveledItem)
        {
            return new RecombineGroup(leveledItem.FormKey, leveledItem.EditorID, leveledItem, (100 - leveledItem.ChanceNone) / 100f);
        }

        public string DisplayName { get; }
        public FormKey FormKey { get; }
        public string? EditorId { get; }
        public float ListChance { get; }
        public ILeveledItemGetter? LeveledItem { get; set; }
        public readonly Dictionary<FormKey, GroupItem> Items = new();

        public void MergeItem(IItemGetter item, Drop drop)
        {
            // if (item.EditorID != "IWHonedSilverWarAxe") return;
            
            if (Items.TryGetValue(item.FormKey, out var existing))
            {
                existing.Drops.Add(drop);
            }
            else
            {
                Items.Add(item.FormKey, new GroupItem(item, drop));
            }
        }

        public void MergeLeveledList(RecombineGroup subRecombineGroup, Drop leveledListDrop, float listChance)
        {
            foreach (var (subItemFormKey, subItem) in subRecombineGroup.Items)
            {
                MergeSubItem(subRecombineGroup, subItemFormKey, subItem, leveledListDrop, listChance);
            }
        }

        private void MergeSubItem(RecombineGroup subRecombineGroup, FormKey subItemFormKey, GroupItem subItem, Drop leveledListDrop, float listChance)
        {
            // if (subItem.Item.EditorID != "IWHonedSilverWarAxe") return;
            
            if (Items.TryGetValue(subItemFormKey, out var existing))
            {
                existing.MergeSubGroup(leveledListDrop, subRecombineGroup, subItem, listChance);
            }
            else
            {
                var newGroupItem = GroupItem.FromSubGroup(subItem.Item, leveledListDrop, subRecombineGroup, subItem, listChance);
                if (newGroupItem != null)
                {
                    Items.Add(subItemFormKey, newGroupItem);
                }
            }
        }
    }
}
