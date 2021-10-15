namespace RecombinePatch.Analysis.Chance
{
    public struct Drop
    {
        public LevelRange? Range;
        public float Chance;

        public static Drop Leveled(LevelRange range, float chance)
        {
            return new Drop
            {
                Range = range,
                Chance = chance,
            };
        }
        
        public static Drop Flat(float chance)
        {
            return new Drop
            {
                Range = null,
                Chance = chance,
            };
        }
    }
}