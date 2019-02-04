using UnityEngine;
using System.Collections.Generic;

using CCC.Items;

namespace CCC.Stats
{
    [CreateAssetMenu]
    sealed class StatPrototypeList : ScriptableObject
    {
        public List<StatPrototype> Prototypes
        {
            get { return prototypes; }
        }

        public bool IsUnique
        {
            get { return isUnique; }
        }

        [SerializeField]
        private bool isUnique;

        [SerializeField]
        private EquipmentSlot slot;

        [SerializeField]
        private List<StatPrototypeSlotEntry> slotEntries = new List<StatPrototypeSlotEntry>();

        private List<StatPrototype> prototypes;

        private void Awake()
        {
            prototypes = new List<StatPrototype>();

            foreach (StatPrototypeSlotEntry entry in slotEntries)
            {
                prototypes.Add(new StatPrototype(entry.StatName, entry.MinValue, entry.MaxValue));
            }
        }
    }

    [System.Serializable]
    sealed class StatPrototypeSlotEntry
    {
        public string StatName
        { 
            get { return statIdentifier.InternalStatName; }
        }

        public float MinValue
        {
            get { return minValue; }
        }

        public float MaxValue
        {
            get { return maxValue; }
        }

        [SerializeField]
        private StatIdentifier statIdentifier;

        [SerializeField]
        private float minValue;

        [SerializeField]
        private float maxValue;
    }
}
