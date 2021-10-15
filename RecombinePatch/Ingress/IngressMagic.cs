using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RecombinePatch.Analysis.LeveledItem;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using RecombinePatch.Analysis.Chance;
using RecombinePatch.Analysis.Groups;

namespace RecombinePatch.Ingress
{
    public static class IngressMagic
    {
        private static IEnumerable<Entry> ValidLeveledListEntries(IngressCache ingressCache, ILeveledItemGetter item)
        {
            if (item.ChanceNone == 100) yield break;
            var entries = item.Entries;
            if (entries == null) yield break;
            
            foreach (var link in entries)
            {
                var linkData = link.Data;
                if (linkData == null) continue;

                var count = linkData.Count;
                if (count <= 0) continue;
                
                if (!linkData.Reference.TryResolve(ingressCache.LinkCache, out var referencedItem)) continue;
                
                yield return new Entry
                {
                    Item = referencedItem,
                    Level = linkData.Level,
                    Count = count,
                };
            }
        }

        private static IEnumerable<EntryWithLevelRangeAndChance> LeveledListEntriesWithChance(IngressCache ingressCache, ILeveledItemGetter item)
        {
            if (item.Flags.And(LeveledItem.Flag.UseAll) > 0)
            {
                foreach (var entry in ValidLeveledListEntries(ingressCache, item))
                {
                    yield return new EntryWithLevelRangeAndChance
                    {
                        Item = entry.Item,
                        Drop = Drop.Flat(1.0f * entry.Count),
                    };
                }
            } 
            else if (item.Flags.And(LeveledItem.Flag.CalculateFromAllLevelsLessThanOrEqualPlayer) > 0)
            {
                var entries = ValidLeveledListEntries(ingressCache, item)
                    .GroupBy(e => e.Level)
                    .Select(g => new { Level = g.Key, ItemsStartingAtThisLevel = g.ToImmutableArray() })
                    .OrderBy(i => i.Level)
                    .ToImmutableArray();

                var bellowLevelChances = entries
                    .ToDictionary(
                        entry => entry.Level,
                        entry => new
                        {
                            UpperLevel = entries
                                .Select(e => e)
                                .FirstOrDefault(e => e.Level > entry.Level)?
                                .Level,
                            Chance = 1.0f / entries
                                .Where(e => e.Level <= entry.Level)
                                .Sum(e => e.ItemsStartingAtThisLevel.Sum(i => i.Count))
                        });
                
                foreach (var entry in entries.SelectMany(g => g.ItemsStartingAtThisLevel))
                {
                    foreach (var (level, range) in bellowLevelChances.Where(r => r.Key >= entry.Level))
                    {
                        yield return new EntryWithLevelRangeAndChance
                        {
                            Item = entry.Item,
                            Drop = Drop.Leveled(
                                new LevelRange(level, range.UpperLevel),
                                range.Chance),
                        };
                    }
                }
            }
            else
            {
                var entries = ValidLeveledListEntries(ingressCache, item)
                    .OrderBy(e => e.Level)
                    .ToImmutableArray();

                var singleItemChance = 1.0f / entries.Length;
                
                for (var i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];
                    
                    short? upperLevel = null;
                    for (var j = i + 1; j < entries.Length; j++)
                    {
                        if (entries[j].Level > entry.Level)
                        {
                            upperLevel = entries[j].Level;
                        }
                    }
                    
                    yield return new EntryWithLevelRangeAndChance
                    {
                        Item = entry.Item,
                        Drop = Drop.Leveled(
                            new LevelRange(entry.Level, upperLevel), 
                            singleItemChance * entry.Count),
                    };
                }
            }
        }
        
        public static RecombineGroup? ArmorGroupFromLeveledList(IngressCache ingressCache, ILeveledItemGetter item)
        {
            if (ingressCache.GroupCache.TryGetValue(item.FormKey, out var res))
            {
                return res;
            }

            var result = RecombineGroup.ForLeveledList(item);

            foreach (var entry in LeveledListEntriesWithChance(ingressCache, item))
            {
                if (entry.Item is IArmorGetter armor)
                {
                    result.MergeItem(armor, entry.Drop);
                }
                else if (entry.Item is IWeaponGetter weap)
                {
                    result.MergeItem(weap, entry.Drop);
                }
                else if (entry.Item is IMiscItem misc)
                {
                    result.MergeItem(misc, entry.Drop);
                }
                else if (entry.Item is ILeveledItemGetter leveledItem)
                {
                    var subArmorGroup = ArmorGroupFromLeveledList(ingressCache, leveledItem);
                    if (subArmorGroup != null)
                    {
                        result.MergeLeveledList(subArmorGroup, entry.Drop, subArmorGroup.ListChance);
                    }
                }
            }

            res = result.Items.Count == 0
                ? null
                : result;
            
            ingressCache.GroupCache[item.FormKey] = res;

            return res;
        }
    }
}