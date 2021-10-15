using System.Collections.Generic;

namespace RecombinePatch.Analysis.Chance
{
    public readonly struct LeveledDrops
    {
        public readonly SortedList<LevelRange, float> Items;

        public LeveledDrops(LevelRange range, float chance)
        {
            Items = new SortedList<LevelRange, float>(16, new LevelRangeComparer());
            Add(range, chance);
        }
        
        private LeveledDrops(SortedList<LevelRange, float> items)
        {
            Items = new SortedList<LevelRange, float>(items.Capacity, new LevelRangeComparer());
            foreach (var (key, value) in items)
            {
                Items[key] = value;
            }
        }

        public void Add(float chance)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Items[Items.Keys[i]] += chance;
            }
        }

        public void Add(LevelRange range, float chance)
        {
            var i = 0;
            while (i < Items.Count)
            {
                var iKey = Items.Keys[i];

                if (LsStart(range.From, iKey.From))
                {
                    if (LsEnd(iKey.From, range.UpTo))
                    {
                        Items.Add(new LevelRange(range.From, iKey.From), chance);
                        range.From = iKey.From;
                    }
                    else if (LeStart(range.UpTo, iKey.From))
                    {
                        Items.Add(new LevelRange(range.From, range.UpTo), chance);
                        return;
                    }
                } 
                else
                {
                    float iChance;
                    if (range.From == iKey.From)
                    {
                        if (range.UpTo == iKey.UpTo)
                        {
                            Items[iKey] += chance;
                            return;
                        }

                        if (LsEnd(range.UpTo, iKey.UpTo))
                        {
                            iChance = Items[iKey];
                            Items.RemoveAt(i);
                            Items.Add(new LevelRange(iKey.From, range.UpTo), iChance + chance);
                            Items.Add(new LevelRange(range.UpTo, iKey.UpTo), iChance);
                            return;
                        }

                        Items[iKey] += chance;
                        range.From = iKey.UpTo;
                    }
                    else if (LsStart(iKey.From, range.From) && LsEnd(range.From, iKey.UpTo))
                    {
                        if (range.UpTo == iKey.UpTo)
                        {
                            iChance = Items[iKey];
                            Items.RemoveAt(i);
                            Items.Add(new LevelRange(iKey.From, range.From), iChance);
                            Items.Add(new LevelRange(range.From, range.UpTo), iChance + chance);
                            return;
                        }
                    
                        if (LsEnd(range.UpTo, iKey.UpTo))
                        {
                            iChance = Items[iKey];
                            Items.RemoveAt(i);
                            Items.Add(new LevelRange(iKey.From, range.From), iChance);
                            Items.Add(new LevelRange(range.From, range.UpTo), iChance + chance);
                            Items.Add(new LevelRange(range.UpTo, iKey.UpTo), iChance);
                            return;
                        }
                    
                        if (LsEnd(iKey.UpTo, range.UpTo))
                        {
                            iChance = Items[iKey];
                            Items.RemoveAt(i);
                            Items.Add(new LevelRange(iKey.From, range.From), iChance);
                            Items.Add(new LevelRange(range.From, iKey.UpTo), iChance + chance);
                            i += 1;
                            range.From = iKey.UpTo;
                        }
                    }
                }

                i++;
            }

            if ((range.From == null && range.UpTo == null) 
                || (range.From == null && range.UpTo != null)
                || (range.From != null && range.UpTo == null) 
                || range.From < range.UpTo)
            {
                Items.Add(new LevelRange(range.From, range.UpTo), chance);
            }
        }
        
        private static bool LsStart(short? a, short? b)
        {
            if (a == null && b == null) return false;
            if (a == null && b != null) return true;
            if (a != null && b == null) return false;
            return a < b;
        }
        
        private static bool LeStart(short? a, short? b)
        {
            if (a == null && b == null) return true;
            if (a == null && b != null) return true;
            if (a != null && b == null) return false;
            return a <= b;
        }
        
        private static bool LsEnd(short? a, short? b)
        {
            if (a == null && b == null) return false;
            if (a == null && b != null) return false;
            if (a != null && b == null) return true;
            return a < b;
        }

        public LeveledDrops Clone()
        {
            return new LeveledDrops(Items);
        }
    }
}