using System;

namespace CCC.Stats
{
    /// <summary>
    /// Represents a stat in the game.
    /// </summary>
    public class Stat : IComparable<Stat>, IEquatable<Stat>
    {
        public const string STR = "str";
        public const string STR_MULT = "strx";
        public const string HEALTH = "hp";
        public const string HEALTH_MULT = "hpx";
        public const string HEALTH_REGEN = "hpreg";
        public const string HEALTH_REGEN_MULT = "hpregx";
        public const string MELEE_ATTACK = "meleeatt";
        public const string MELEE_ATTACK_MULT = "meleeattx";

        public const string DEX = "dex";
        public const string DEX_MULT = "dexx";
        public const string MOVE_SPEED = "ms";
        public const string MOVE_SPEED_MULT = "msx";
        public const string ATTACK_SPEED = "atps";
        public const string ATTACK_SPEED_MULT = "atpsx";
        public const string RANGED_ATTACK = "rangedatt";
        public const string RANGED_ATTACK_MULT = "rangedattx";

        public const string MYST = "myst";
        public const string MYST_MULT = "mystx";
        public const string CDR_MULT = "cdrx";
        public const string SPELL = "spell";
        public const string SPELL_MULT = "spellx";

        public const string FORT = "fort";
        public const string FORT_MULT = "fortx";
        public const string MAGIC_RES = "mr";
        public const string MAGIC_RES_MULT = "mrx";
        public const string STATUS_REC = "statrec";
        public const string STATUS_REC_MULT = "statrecx";
        public const string AFFLICT_RES = "affres";
        public const string AFFLICT_RES_MULT = "affresx";

        public const string ARMOR = "arm";
        public const string ARMOR_MULT = "armx";
        public const string DMG_MULT = "dmg";
        public const string CRIT_DMG = "critdmg";
        public const string CRIT_DMG_MULT = "critdmgx";
        public const string CRIT_CHANCE = "critchan";
        public const string CRIT_CHANCE_MULT = "critchanx";


        public const string AS_DMG_MIN = "as_dmgmin";
        public const string AS_DMG_MAX = "as_dmgmax";
        public const string AS_CD = "as_cd";
        public const string AS_PROJ_COUNT = "as_projcount";
        public const string AS_PROJ_SPEED = "as_projspeed";
        public const string AS_PROJ_SPREAD = "as_projspread";
        public const string AS_IGNITE_MULT = "as_ignitex";

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
            set { this.value = value; }
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
        /// Initializes a new instance of the <see cref="T:CCC.Stats.Stat"/> 
        /// struct with the value set to 0.
        /// </summary>
        /// <param name="name">The name for the new Stat.</param>
        public Stat(string name)
        {
            this.name = name;
            value = 0f;
        }

        /// <summary>
        /// The name of this Stat.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The value of this Stat.
        /// </summary>
        private float value;

        public int CompareTo(Stat other)
        {
            return Name.CompareTo(other.Name);
        }

        public bool Equals(Stat other)
        {
            return Name.Equals(other.Name);
        }
    }
}
