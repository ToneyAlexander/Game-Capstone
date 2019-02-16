using System.Collections.Generic;
using UnityEngine;

namespace CCC.Abilities
{
    [CreateAssetMenu]
    public class AbilitySlotDictionary : ScriptableObject
    {
        [SerializeField]
        private Ability nullAbility;

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
                { AbilitySlot.One, nullAbility },
                { AbilitySlot.Two, nullAbility },
                { AbilitySlot.Three, nullAbility },
                { AbilitySlot.Four, nullAbility },
                { AbilitySlot.Five, nullAbility }
            };
        }
    }
}
