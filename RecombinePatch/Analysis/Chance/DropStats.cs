using System;
using System.Collections.Generic;

namespace RecombinePatch.Analysis.Chance
{
    public class DropStats
    {
        public LeveledDrops? Leveled;
        public float? Flat;

        private DropStats(LeveledDrops? leveled, float? flat)
        {
            Leveled = leveled;
            Flat = flat;
        }
        
        public static DropStats From(Drop drop)
        {
            return new DropStats(
                drop.Range != null 
                    ? new LeveledDrops(drop.Range.Value, drop.Chance) 
                    : null,
                drop.Range == null 
                    ? drop.Chance 
                    : null
            );
        }

        public void Add(Drop drop)
        {
            if (Flat != null && Leveled == null && drop.Range == null)
            {
                Flat += drop.Chance;
            }

            if (Flat == null && Leveled != null && drop.Range == null)
            {
                Leveled.Value.Add(drop.Chance);
            }

            if (Flat != null && Leveled == null && drop.Range != null)
            {
                Leveled = new LeveledDrops(drop.Range.Value, Flat.Value + drop.Chance);
                Flat = null;
            }

            if (Flat == null && Leveled != null && drop.Range != null)
            {
                Leveled.Value.Add(drop.Range.Value, drop.Chance);
            }
        }

        public void WriteLine(string depth)
        {
            if (Flat != null)
            {
                Console.WriteLine($"{depth}- OVERALL = {Flat:0.######}");
            }
            else if (Leveled != null)
            {
                foreach (var (levelRange, chance) in Leveled.Value.Items)
                {
                    Console.WriteLine($"{depth}- {levelRange} = {chance:0.######}");
                }
            }
        }

        public IEnumerable<Drop> Multiply(Drop parentSlot, float listChance)
        {
            if (Leveled != null)
            {
                foreach (var (subLevelRange, subChance) in Leveled.Value.Items)
                {
                    if (parentSlot.Range == null)
                    {
                        yield return Drop.Leveled(subLevelRange, parentSlot.Chance * subChance * listChance);
                    }
                    else
                    {
                        if (subLevelRange.TryIntersect(parentSlot.Range.Value, out var intersectionRange))
                        { 
                            yield return Drop.Leveled(intersectionRange, parentSlot.Chance * subChance * listChance);
                        }
                    }
                }
            }
            else if (Flat != null)
            {
                if (parentSlot.Range == null)
                {
                    yield return Drop.Flat(parentSlot.Chance * Flat.Value * listChance);
                }
                else
                {
                    yield return Drop.Leveled(parentSlot.Range.Value, parentSlot.Chance * Flat.Value * listChance);
                }
            }
        }
    }
}