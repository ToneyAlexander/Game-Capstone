using UnityEngine;

using CCC.Stats;

namespace CCC.Abilities
{
    [System.Serializable]
    public struct AbilityStatSlotEntry1
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
