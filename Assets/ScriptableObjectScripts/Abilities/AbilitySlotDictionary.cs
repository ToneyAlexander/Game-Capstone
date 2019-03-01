using System.Collections.Generic;
using UnityEngine;

namespace CCC.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/AbilitySlotDictionary")]
    public class AbilitySlotDictionary : ScriptableObject
    {
        private Dictionary<AbilitySlot, Ability> abilities;

        public Ability GetAbility(AbilitySlot slot)
        {
            return abilities[slot];
        }

        public void SetSlotAbility(AbilitySlot slot, Ability ability)
        {
            abilities[slot] = ability;
        }

        private void OnEnable()
        {
            abilities = new Dictionary<AbilitySlot, Ability> {
                { AbilitySlot.One, Ability.nullAbility },
                { AbilitySlot.Two, Ability.nullAbility},
                { AbilitySlot.Three, Ability.nullAbility },
                { AbilitySlot.Four, Ability.nullAbility },
                { AbilitySlot.Five, Ability.nullAbility }
            };
        }
    }
}
