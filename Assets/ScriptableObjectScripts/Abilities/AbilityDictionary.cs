using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCC.Abilities
{
    public class AbilityDictionary : ScriptableObject
    {
        [SerializeField]
        private AbilitySlot one;

        [SerializeField]
        private AbilitySlot two;

        [SerializeField]
        private AbilitySlot three;
        private AbilitySlot four;
        private AbilitySlot five;
    }
}
