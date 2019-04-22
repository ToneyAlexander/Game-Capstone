using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// An ItemGenerator that always creates a random item from it's list
    /// </summary>
    [CreateAssetMenu(menuName = "Items/RegularItemGenerator")]
    public sealed class RegularItemGenerator : ItemGenerator
    {
        [SerializeField]
        private List<AffixSetPrototype> affixSets;
        private float raritySum;

        public override Item GenerateItem(int levelToAdd = 0)
        {
            if (raritySum == 0)
                CalcRaritySum();

            // Pick a random ItemPrototype
            float rand = Random.Range(0, raritySum);
            //Debug.Log("Picked " + rand + " between 0 and " + raritySum);
            int index = 0;
            float sumTry = 0;
            for(int i = 0; i < itemPrototypes.Count; ++i)
            {
                sumTry += itemPrototypes[i].Rarity;
                if(rand <= sumTry)
                {
                    index = i;
                    break;
                }
            }
            ItemPrototype proto = itemPrototypes[index].Item;
            
            List<Stat> stats = new List<Stat>();
            foreach (StatPrototype statPrototype in proto.StatPrototypes)
            {
                ApplyStat(stats, statPrototype);
            }

            Item item = new Item(proto.ItemName, proto.FlavorText, false, proto.BaseItemTier + levelToAdd,
                proto.EquipmentSlot, proto.Sprite.name, stats, proto.WorldDropPrefab);
            //Debug.Log(proto.Sprite.name);

            ApplyAffixes(affixSets, this, item, proto);

            //Debug.Log(proto.WorldDropPrefab);
            return item;
        }

        public override void ApplyStat(List<Stat> stats, StatPrototype stat)
        {
            stats.Add(new Stat(stat.StatName, Random.Range(stat.MinValue, stat.MaxValue)));
        }

        private void CalcRaritySum()
        {
            raritySum = 0;
            foreach (ItemRarity ir in itemPrototypes)
            {
                raritySum += ir.Rarity;
            }
        }

        void Awake()
        {
            CalcRaritySum();
        }

        [SerializeField]
        private List<ItemRarity> itemPrototypes;
    }
}
