using UnityEngine;

namespace CCC.Abilities
{
    /// <summary>
    /// Represents an ordered pair (Ability, AbilitySlot).
    /// </summary>
    [System.Serializable]
    struct AbilityDictionaryEntry
    {
        /// <summary>
        /// The unique AbilityDictionaryEntry (Ability.Null, AbilitySlot.Null).
        /// </summary>
        public static readonly AbilityDictionaryEntry Null = 
            new AbilityDictionaryEntry(Ability.Null, AbilitySlot.Null);

        /// <summary>
        /// The Ability for this AbilityDictionaryEntry.
        /// </summary>
        [SerializeField]
        public readonly Ability Ability;

        /// <summary>
        /// The AbilitySlot for this AbilityDictionaryEntry.
        /// </summary>
        [SerializeField]
        public readonly AbilitySlot Slot;

        /// <summary>
        /// Creates a new AbilityDictionaryEntry for the given Ability and 
        /// AbilitySlot or AbilityDictionaryEntry.Null if the given AbilitySlot 
        /// is AbilitySlot.Null.
        /// </summary>
        /// <returns>
        /// A new AbilityDictionaryEntry for the given Ability and AbilitySlot 
        /// or AbilityDictionaryEntry.Null.
        /// </returns>
        /// <param name="ability">
        /// The Ability for the new AbilityDictionaryEntry.
        /// </param>
        /// <param name="slot">
        /// The AbilitySlot for the new AbilityDictionaryEntry.
        /// </param>
        public static AbilityDictionaryEntry ForAbilityInSlot(Ability ability, 
            AbilitySlot slot)
        {
            var entry = Null;

            // AbilityDictionaryEntry.Null already is the unique ordered pair 
            // (Ability.Null, AbilitySlot.Null).
            if (slot != AbilitySlot.Null)
            {
                entry = new AbilityDictionaryEntry(ability, slot);
            }

            return entry;
        }

        /// <summary>
        /// Returns an AbilityDictionaryEntry for the given AbilitySlot with 
        /// its Ability set to Ability.nullAbility.
        /// </summary>
        /// <returns>
        /// An AbilityDictionaryEntry with its Abilty set to 
        /// Ability.nullAbility and its AbilitySlot set to the given 
        /// AbilitySlot.
        /// </returns>
        /// <param name="slot">
        /// The AbilitySlot for the new AbilityDictionaryEntry.
        /// </param>
        public static AbilityDictionaryEntry ForSlot(AbilitySlot slot)
        {
            var entry = Null;

            // AbilityDictionaryEntry.Null already is the unique ordered pair 
            // (Ability.Null, AbilitySlot.Null).
            if (slot != AbilitySlot.Null)
            {
                entry = ForAbilityInSlot(Ability.Null, slot);
            }

            return entry;
        }

        private AbilityDictionaryEntry(Ability ability, AbilitySlot slot)
        {
            Ability = ability;
            Slot = slot;
        }
    }
}