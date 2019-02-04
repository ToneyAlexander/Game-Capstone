namespace CCC.Stats
{
    /// <summary>
    /// Represents a stat in the game.
    /// </summary>
    public struct Stat
    {
        /// <summary>
        /// Gets the name of this Stat.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the value of this Stat.
        /// </summary>
        /// <value>The value.</value>
        public float Value
        {
            get { return value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CCC.Stats.Stat"/> 
        /// struct.
        /// </summary>
        /// <param name="name">The name for the new Stat.</param>
        /// <param name="value">The value for the new Stat.</param>
        public Stat(string name, float value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// The name of this Stat.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The value of this Stat.
        /// </summary>
        private readonly float value;
    }
}
