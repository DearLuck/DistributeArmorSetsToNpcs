using Mutagen.Bethesda.Skyrim;

namespace RecombinePatch.Analysis.LeveledItem
{
    public struct Entry
    {
        public short Level;
        public short Count;
        public IItemGetter Item;
    }
}