using System;

namespace RecombinePatch.Analysis.Chance
{
    public struct LevelRange
    {
        public short? From;
        public short? UpTo;

        public LevelRange(short? @from, short? upTo)
        {
            From = @from;
            UpTo = upTo;
        }

        public override string ToString()
        {
            if (From == null && UpTo == null)
            {
                return "ANY";
            }

            if (From != null && UpTo == null)
            {
                return $"FROM {From}";
            }
            
            if (From == null && UpTo != null)
            {
                return $"TO {UpTo}";
            }
            
            return $"{From}-{UpTo}";
        }

        public bool TryIntersect(LevelRange range, out LevelRange intersection)
        {
            var min = Math.Max(range.From ?? -999, From ?? -999);
            var max = Math.Min(range.UpTo ?? 999, UpTo ?? 999);

            intersection = new LevelRange(min == -999 ? null : min, max == 999 ? null : max);
            
            return min < max;
        }
    }
}