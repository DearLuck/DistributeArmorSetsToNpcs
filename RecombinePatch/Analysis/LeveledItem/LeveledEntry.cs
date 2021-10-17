using Mutagen.Bethesda.Skyrim;

namespace RecombinePatch.Analysis.LeveledItem
{
    public struct Entry
    {
        public int Count;
        public IItemGetter Item;
    }
    
    public struct LeveledEntry
    {
        public short Level;
        public short Count;
        public IItemGetter Item;
    }
}