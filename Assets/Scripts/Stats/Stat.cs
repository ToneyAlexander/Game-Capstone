namespace CCC.Stats
{
    /// <summary>
    /// Represents a stat in the game.
    /// </summary>
    public struct Stat
    {
        public static readonly string STR = "str";
        public static readonly string STR_MULT = "strx";
        public static readonly string HEALTH = "hp";
        public static readonly string HEALTH_MULT = "hpx";
        public static readonly string HEALTH_REGEN = "hpreg";
        public static readonly string HEALTH_REGEN_MULT = "hpregx";
        public static readonly string MELEE_ATTACK_MULT = "meleeattx";

        public static readonly string DEX = "dex";
        public static readonly string DEX_MULT = "dexx";
        public static readonly string MOVE_SPEED = "ms";
        public static readonly string MOVE_SPEED_MULT = "msx";
        public static readonly string ATTACK_SPEED = "atps";
        public static readonly string ATTACK_SPEED_MULT = "atpsx";
        public static readonly string RANGED_ATTACK_MULT = "rangedattx";

        public static readonly string MYST = "myst";
        public static readonly string MYST_MULT = "mystx";
        public static readonly string CDR_MULT = "cdrx";
        public static readonly string SPELL_MULT = "spellx";

        public static readonly string FORT = "fort";
        public static readonly string FORT_MULT = "fortx";
        public static readonly string MAGIC_RES = "mr";
        public static readonly string MAGIC_RES_MULT = "mrx";
        public static readonly string STATUS_REC = "statrec";
        public static readonly string STATUS_REC_MULT = "statrecx";
        public static readonly string AFFLICT_RES = "affres";
        public static readonly string AFFLICT_RES_MULT = "affresx";

        public static readonly string ARMOR = "arm";
        public static readonly string ARMOR_MULT = "armx";
        public static readonly string DMG_MULT = "dmg";
        public static readonly string CRIT_DMG = "critdmg";
        public static readonly string CRIT_DMG_MULT = "critdmgx";
        public static readonly string CRIT_CHANCE = "critchan";
        public static readonly string CRIT_CHASE_MULT = "critchanx";

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
