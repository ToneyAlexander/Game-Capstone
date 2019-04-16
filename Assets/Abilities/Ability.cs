using CCC.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Abilities
{
    [System.Serializable]
    public sealed class Ability
    {
        // Still available for backwards compatibility. Use Ability.Null 
        // instead.
        public static readonly Ability nullAbility = Null;

        public static readonly Ability Null = new Ability {
            AbilityName = "Null Ability",
            abilityType = AbilityType.Null,
            SpriteFilename = "NullAbilityIcon",
            Prefab = null,
            Stats = new List<Stat>(),
            TypeString = "",
            cdRemain = 0.0f,
            isAttack = false,
            update = false,
            use = false
        };

        public string AbilityName;

        /// <summary>
        /// The AbilityType for this Ability.
        /// </summary>
        public AbilityType abilityType;

        public string TypeString;
        public bool use;
        public bool update;
        public bool isAttack;
        public float cdRemain;

        public string SpriteFilename;

        public GameObject Prefab;
        public List<Stat> Stats;

        public static Ability FromPrototype(AbilityPrototype ap)
        {
            var ability = Null;

            if (ap.AbilityName != Null.AbilityName)
            {
                ability = new Ability(ap);
            }

            return ability;
        }

        private Ability()
        {

        }

        private Ability(string abilityName, AbilityType abilityType, 
            string spriteFilename, GameObject prefab, List<Stat> stats, 
            string typeString, float cdRemain, bool isAttack, bool update, 
            bool use)
        {
            AbilityName = abilityName;
            this.abilityType = abilityType;
            SpriteFilename = spriteFilename;
            Prefab = prefab;
            Stats = stats;
            TypeString = typeString;
            this.cdRemain = cdRemain;
            this.isAttack = isAttack;
            this.update = update;
            this.use = use;
        }

        private Ability(AbilityPrototype ap)
        {
            AbilityName = ap.AbilityName;
            abilityType = ap.AbilityType;
            SpriteFilename = ap.Icon.name;
            Prefab = ap.Prefab;
            Stats = ap.Stats;
            TypeString = ap.TypeString;
            cdRemain = 0f;
            isAttack = ap.IsAttack;
            update = false;
            use = false;//TODO replace with proper listener
                        // Script = ap.Script;
        }
    }
}
