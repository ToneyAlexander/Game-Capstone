namespace CCC.Items
{
    /// <summary>
    /// Represents the raw data of an Item that can be saved to disk.
    /// </summary>
    public struct ItemData
    {
        public readonly string name;
        public readonly string longName;
        public readonly string flavorText;
        public readonly bool isUnique;
        public readonly int tier;

    }
}