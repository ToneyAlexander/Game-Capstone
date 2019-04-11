using System.Collections.Generic;
using UnityEngine;

namespace CCC.Combat.Perks
{
    sealed class PerkListData
    {
        public static readonly PerkListData Null = new PerkListData();
        public static PerkListData Empty()
        {
            return new PerkListData();
        }

        public static PerkListData ForPerkPrototypes(
            List<PerkPrototype> perkPrototypes)
        {
            return new PerkListData(perkPrototypes);
        }

        public List<PerkPrototype> Perks
        {
            get { return perks; }
        }

        [SerializeField]
        private List<PerkPrototype> perks;

        private PerkListData()
        {
            perks = new List<PerkPrototype>();
        }

        private PerkListData(List<PerkPrototype> perkPrototypes)
        {
            perks = perkPrototypes;
        }
    }
}