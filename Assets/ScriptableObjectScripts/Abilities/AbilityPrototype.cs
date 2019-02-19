using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Abilities
{
    [CreateAssetMenu]
    public sealed class AbilityPrototype : ScriptableObject
    {

        public string AbilityName
        {
            get { return abilityName; }
        }

        public AbilityType AbilityType
        {
            get { return abilityType; }
        }

        public string TypeString
        {
            get { return typeString; }
        }

        public Sprite Icon
        {
            get { return icon; }
        }

        public GameObject Prefab
        {
            get { return prefab; }
        }

        public List<Stat> Stats
        {
            get { return stats; }
        }

        public Ability Instance { get { return new Ability(this); }}

        /// <summary>
        /// The name of this Ability.
        /// </summary>
        [SerializeField]
        private string abilityName;

        [SerializeField]
        private AbilityType abilityType = AbilityType.Null;

        [SerializeField]
        private string typeString;

        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private MonoBehaviour script;
        /// <summary>
        /// The prefab that represents the animation of this Ability.
        /// </summary>
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private List<AbilityStatSlotEntry> statSlots;

        private List<Stat> stats;

        #region ScriptableObject Messages
        private void OnEnable()
        {
            stats = new List<Stat>();
            if (statSlots != null)
            {
                foreach (AbilityStatSlotEntry entry in statSlots)
                {
                    Stat stat = new Stat(entry.StatName, entry.Value);
                    stats.Add(stat);
                }
            }
        }
        #endregion
    }

    [System.Serializable]
    class AbilityStatSlotEntry
    {
        public string StatName
        {
            get { return statIdentifier.InternalStatName; }
        }

        public float Value
        {
            get { return value; }
        }

        [SerializeField]
        private AbilityStatIdentifier statIdentifier;

        [SerializeField]
        private float value;
    }
}
