using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Abilities
{
    [CreateAssetMenu]
    public sealed class Ability : ScriptableObject
    {
        public string AbilityName
        {
            get { return abilityName; }
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

        /// <summary>
        /// The name of this Ability.
        /// </summary>
        [SerializeField]
        private string abilityName;

        [SerializeField]
        private Sprite icon;

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
