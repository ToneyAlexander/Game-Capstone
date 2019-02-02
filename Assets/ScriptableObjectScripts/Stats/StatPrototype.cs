namespace CCC.Stats
{
    public struct StatPrototype
    {
        public string StatName
        {
            get { return internalStatName; }
        }

        public float MinValue
        {
            get { return minValue; }
        }

        public float MaxValue
        {
            get { return maxValue; }
        }

        public StatPrototype(string internalStatName, float minValue, float maxValue)
        {
            this.internalStatName = internalStatName;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        private readonly string internalStatName;

        private readonly float minValue;

        private readonly float maxValue;
    }
}
