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
        private static float[] tierOdds = {150, 110, 80, 55, 25, 10, 0};
        private static float[] affixCountOdds = { 25, 55, 80, 55, 25, 10, 0 };
        private static readonly float tierIncrease = 1f;

        protected static void ApplyAffixes(List<AffixSetPrototype> allAffixSets, ItemGenerator gen, Item item, ItemPrototype proto)
        {
            List<string> shortNames = new List<string>();
            List<string> longNames = new List<string>();
            float[] itemTierOdds = new float[tierOdds.Length];
            float[] itemAffixCountOdds = new float[affixCountOdds.Length];
            System.Array.Copy(tierOdds, itemTierOdds, tierOdds.Length);
            System.Array.Copy(affixCountOdds, itemAffixCountOdds, affixCountOdds.Length);
            for (int i = 1; i < itemTierOdds.Length; ++i)
            {
                itemTierOdds[i] += tierIncrease * item.Tier * i;
            }
            float affixCountSum = 0;
            for (int i = 1; i < affixCountOdds.Length; ++i)
            {
                itemAffixCountOdds[i] += tierIncrease * item.Tier * i;
                affixCountSum += itemAffixCountOdds[i];
            }
            int affixCount = 0;
            float affixCountRng = Random.Range(0, affixCountSum);
            float affixCountTry = 0;
            for (int j = 0; j < itemAffixCountOdds.Length; ++j)
            {
                affixCountTry += itemAffixCountOdds[j];
                if (affixCountRng <= affixCountTry)
                {
                    affixCount = j;
                    break;
                }
            }
            affixCount = proto.MaxNumberAffixes < affixCount ? proto.MaxNumberAffixes : affixCount;
            List<int> usedAffixIndex = new List<int>();
            for (int i = 0; i < affixCount; ++i)
            {
                AffixSetPrototype toApply;
                int index;
                int itr = 0;
                do
                {
                    ++itr;
                    index = Random.Range(0, allAffixSets.Count);
                } while (usedAffixIndex.Contains(index) && itr < 100);
                usedAffixIndex.Add(index);
                toApply = allAffixSets[index];
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
                    if(tier <= tierTry)
                    {
                        Debug.Log(tier + " actual affix: " + j + " item tier: " + item.Tier);
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
