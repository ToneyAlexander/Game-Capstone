using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// An ItemGenerator that always creates a perfect Item (one with the
    /// maximum values for every Stat.
    /// </summary>
    [CreateAssetMenu(menuName = "Items/PerfectItemGenerator")]
    public sealed class PerfectItemGenerator : ItemGenerator
    {
        [SerializeField]
        private List<AffixSetPrototype> affixSets;

        public override Item GenerateItem()
        {
            // Pick a random ItemPrototype
            int rand = Random.Range(0, itemPrototypes.Count);
            ItemPrototype proto = itemPrototypes[rand];

            // Always use its maximum values because we're very lucky
            List<Stat> stats = new List<Stat>();
            foreach (StatPrototype statPrototype in proto.StatPrototypes)
            {
                ApplyStat(stats, statPrototype);
            }

            Item item = new Item(proto.ItemName, proto.FlavorText, false, proto.BaseItemTier,
                proto.EquipmentSlot, proto.Sprite, stats, proto.WorldDropPrefab);

            ApplyAffixes(affixSets, this, item, proto);

            Debug.Log(proto.WorldDropPrefab);
            return item;
        }

        public override void ApplyStat(List<Stat> stats, StatPrototype stat)
        {
            stats.Add(new Stat(stat.StatName, stat.MaxValue));
        }

        [SerializeField]
        private List<ItemPrototype> itemPrototypes;
    }
}
