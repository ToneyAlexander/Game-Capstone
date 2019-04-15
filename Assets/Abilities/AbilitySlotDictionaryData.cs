using UnityEngine;
using System.Collections.Generic;

namespace CCC.Abilities
{
    [System.Serializable]
    struct AbilitySlotDictionaryData
    {
        public static readonly AbilitySlotDictionaryData Null = 
            new AbilitySlotDictionaryData();

        public static AbilitySlotDictionaryData FromDictionary(
            Dictionary<AbilitySlot, Ability> dictionary)
        {
            return ForAbilities(dictionary[AbilitySlot.One], 
                dictionary[AbilitySlot.Two], dictionary[AbilitySlot.Three], 
                dictionary[AbilitySlot.Four], dictionary[AbilitySlot.Five], 
                dictionary[AbilitySlot.Six]);
        }

        public AbilitySlotDictionaryData CreateEmpty()
        {
            return ForAbilities(Ability.nullAbility, Ability.nullAbility, 
                Ability.nullAbility, Ability.nullAbility, Ability.nullAbility, 
                Ability.nullAbility);
        }

        public static AbilitySlotDictionaryData ForAbilities(Ability one, 
            Ability two, Ability three, Ability four, Ability five, Ability six)
        {
            return new AbilitySlotDictionaryData(one, two, three, four, five, 
                six);
        }

        [SerializeField]
        public readonly Ability One;

        [SerializeField]
        public readonly Ability Two;

        [SerializeField]
        public readonly Ability Three;

        [SerializeField]
        public readonly Ability Four;

        [SerializeField]
        public readonly Ability Five;

        [SerializeField]
        public readonly Ability Six;

        private AbilitySlotDictionaryData(Ability one, Ability two, 
            Ability three, Ability four, Ability five, Ability six)
        {
            One = one;
            Two = two;
            Three = three;
            Four = four;
            Five = five;
            Six = six;
        }
    }
}