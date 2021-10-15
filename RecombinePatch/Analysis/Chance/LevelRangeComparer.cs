using System.Collections.Generic;

namespace RecombinePatch.Analysis.Chance
{
    public class LevelRangeComparer : IComparer<LevelRange>
    {
        public int Compare(LevelRange x, LevelRange y)
        {
            var byteComparer = Comparer<short?>.Default;
            return x.From != y.From
                ? byteComparer.Compare(x.From, y.From) 
                : byteComparer.Compare(x.UpTo, y.UpTo);
        }
    }
}