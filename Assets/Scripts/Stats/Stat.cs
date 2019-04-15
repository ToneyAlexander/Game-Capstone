using UnityEngine;
using System;

namespace CCC.Stats
{
    /// <summary>
    /// Represents a stat in the game.
    /// </summary>
    [System.Serializable]
    public sealed class Stat : IComparable<Stat>, IEquatable<Stat>
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
        public const string CDR = "cdr";
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
        public const string DMG = "dmg";
        public const string DMG_MULT = "dmgx";
        public const string PHYS_DMG = "pdmg";
        public const string PHYS_DMG_MULT = "pdmgx";
        public const string MAGIC_DMG = "mdmg";
        public const string MAGIC_DMG_MULT = "mdmgx";


        public const string CRIT_DMG = "critdmg";
        public const string CRIT_DMG_MULT = "critdmgx";
        public const string CRIT_CHANCE = "critchan";
        public const string CRIT_CHANCE_MULT = "critchanx";
        public const string FLAT_DMG_REDUCTION = "flatred";
        public const string FLAT_DMG_REDUCTION_MULT = "flatredx";

        //Weird special stats
        public const string HEMO_PHANTOM_HP_MULT = "hemohpx";
        public const string HEMO_BLOOD_POWER = "hemobloodx";

        //ability stats
        public const string AS_DMG_MIN = "as_dmgmin";
        public const string AS_DMG_MAX = "as_dmgmax";
        public const string AS_CD = "as_cd";
        public const string AS_PROJ_COUNT = "as_projcount";
        public const string AS_PROJ_SPEED = "as_projspeed";
        public const string AS_PROJ_SPREAD = "as_projspread";
        public const string AS_IGNITE_MULT = "as_ignitex";
        public const string AS_IGNITE_STACK = "as_ignitestack";
        public const string AS_DUR = "as_dur";
        public const string AS_SIZE = "as_size";
        public const string AS_DASH_MULT = "as_dashx";
        public const string AS_BUFFS = "as_buffs";
        public const string AS_DEBUFFS = "as_debuffs";
        public const string AS_VAMP = "as_vamp";
        public const string AS_COST = "as_cost";
        public const string AS_CORRUPT = "as_corrupt";
        public const string AS_CRITDAM = "as_critdam";

        public static string GetStatString(Stat stat)
        {

            string value = stat.Value.ToString("n1");
            if (stat.Name == Stat.AFFLICT_RES_MULT ||
                stat.Name == Stat.ARMOR_MULT ||
                stat.Name == Stat.ATTACK_SPEED_MULT ||
                stat.Name == Stat.CDR ||
                stat.Name == Stat.CDR_MULT ||
                stat.Name == Stat.CRIT_CHANCE ||
                stat.Name == Stat.CRIT_CHANCE_MULT ||
                stat.Name == Stat.CRIT_DMG ||
                stat.Name == Stat.CRIT_DMG_MULT ||
                stat.Name == Stat.DEX_MULT ||
                stat.Name == Stat.DMG_MULT ||
                stat.Name == Stat.FLAT_DMG_REDUCTION ||
                stat.Name == Stat.FLAT_DMG_REDUCTION_MULT ||
                stat.Name == Stat.FORT_MULT ||
                stat.Name == Stat.HEALTH_MULT ||
                stat.Name == Stat.HEALTH_REGEN_MULT ||
                stat.Name == Stat.MAGIC_DMG_MULT ||
                stat.Name == Stat.MAGIC_RES_MULT ||
                stat.Name == Stat.MELEE_ATTACK_MULT ||
                stat.Name == Stat.MOVE_SPEED_MULT ||
                stat.Name == Stat.MYST_MULT ||
                stat.Name == Stat.PHYS_DMG_MULT ||
                stat.Name == Stat.RANGED_ATTACK_MULT ||
                stat.Name == Stat.SPELL_MULT ||
                stat.Name == Stat.STATUS_REC ||
                stat.Name == Stat.STATUS_REC_MULT ||
                stat.Name == Stat.STR_MULT
                )
            {
                value = (stat.Value * 100).ToString("n1") + "%";
            }
            return value;
        }


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
        [SerializeField]
        private string name;

        /// <summary>
        /// The value of this Stat.
        /// </summary>
        [SerializeField]
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
