using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// Represents something that can generate Items.
    /// </summary>
    public abstract class ItemGenerator : ScriptableObject
    {
        private static float[] tierOdds = {115, 85, 60, 40, 15, 5, 0};
        private static readonly float tierIncrease = 1f;

        protected static void ApplyAffixes(List<AffixSetPrototype> allAffixSets, ItemGenerator gen, Item item, ItemPrototype proto)
        {
            List<string> shortNames = new List<string>();
            List<string> longNames = new List<string>();
            float[] itemTierOdds = new float[tierOdds.Length];
            System.Array.Copy(tierOdds, itemTierOdds, tierOdds.Length);
            for (int i = 1; i < itemTierOdds.Length; ++i)
            {
                itemTierOdds[i] += tierIncrease * item.Tier * i;
            }
            for (int i = 0; i < proto.MaxNumberAffixes; ++i)
            {
                AffixSetPrototype toApply = allAffixSets[Random.Range(0, allAffixSets.Count)];
                float tierTry = 0;
                float sum = 0;
                for (int j = 0; j < toApply.Tiers.Count; ++j)
                {
                    sum += itemTierOdds[j];
                }
                float tier = Random.Range(0, sum);
                for (int j = 0; j < toApply.Tiers.Count; ++j)
                {
                    tierTry += itemTierOdds[j];
                    if(tier < tierTry)
                    {
                        longNames.Add(item.Name + " of " + toApply.Tiers[j].affixName + " " + toApply.SetName);
                        shortNames.Add(item.Name + " of " + toApply.SetNameShort);

                        foreach (StatPrototype stat in toApply.Tiers[j].statPrototypes) {
                            gen.ApplyStat(item.Stats, stat);
                        }
                        break;
                    }
                }
            }
            if (longNames.Count > 0)
            {
                int i = Random.Range(0, longNames.Count);
                item.Name = shortNames[i];
                item.LongName = longNames[i];
            }

        }

        public abstract void ApplyStat(List<Stat> stats, StatPrototype stat);

        /// <summary>
        /// Generate new Item.
        /// </summary>
        /// <returns>A newly generated Item.</returns>
        public abstract Item GenerateItem();
    }
}
