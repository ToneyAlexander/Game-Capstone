using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// An ItemGenerator that always creates a perfect Item (one with the
    /// maximum values for every Stat.
    /// </summary>
    [CreateAssetMenu]
    public sealed class PerfectItemGenerator : ItemGenerator
    {
        public override Item GenerateItem()
        {
            // Pick a random ItemPrototype
            int rand = Random.Range(0, itemPrototypes.Count);
            ItemPrototype proto = itemPrototypes[rand];

            // Always use its maximum values because we're very lucky
            List<Stat> stats = new List<Stat>();
            foreach (StatPrototype statPrototype in proto.StatPrototypes)
            {
                stats.Add(new Stat(statPrototype.StatName, statPrototype.MaxValue));
            }

            Item item = new Item(proto.ItemName, proto.FlavorText, false, 
                proto.EquipmentSlot, proto.Sprite, stats);

            return item;
        }

        [SerializeField]
        private List<ItemPrototype> itemPrototypes;
    }
}
