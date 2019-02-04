using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Perks
{
    [CreateAssetMenu(menuName = "Prototypes/Perk Prototype")]
    public class PerkPrototype : ScriptableObject
    {
        public string PerkName
        {
            get { return perkName; }
        }

        public List<StatPrototype> StatPrototypes
        {
            get { return statPrototypes; }
        }

        private List<StatPrototype> statPrototypes;

        [SerializeField]
        private string perkName;

        [SerializeField]
        private string internalDescription;

        [SerializeField]
        private List<StatPrototypeSlotEntry> statPrototypeList;

        private void Awake()
        {
            statPrototypes = new List<StatPrototype>();

            foreach (StatPrototypeSlotEntry entry in statPrototypeList)
            {
                statPrototypes.Add(new StatPrototype(entry.StatName, entry.MinValue,
                    entry.MaxValue));
            }
        }
    }
}
