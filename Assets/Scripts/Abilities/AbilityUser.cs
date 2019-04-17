using UnityEngine;
using System;

namespace CCC.Abilities
{
    /// <summary>
    /// Represents a Component that allows a GameObject to use an Ability.
    /// </summary>
   
    [RequireComponent(typeof(RemyAttacking))]

    public sealed class AbilityUser : MonoBehaviour
    {
        /// <summary>
        /// Get the AbilitySet of all the Abilities that are available to this
        /// AbilityUser.
        /// </summary>
        /// <value>The AbilitySet of all usable Abilities.</value>
        public AbilitySet UsableAbilities
        {
            get { return usableAbilities; }
        }

        /// <summary>
        /// The AbilitySet of Abilities that this AbilityUser's GameObject can 
        /// use.
        /// </summary>
        [SerializeField]
        private AbilitySet usableAbilities;
        private RemyAttacking remyAttacking;
        private AbilityType abilityType;
        

        /// <summary>
        /// Use the given Ability.
        /// </summary>
        /// <param name="ability">The Ability to use.</param>
        /// <param name="mouseWorldPosition">
        /// The world space position of the mouse during the frame the given 
        /// Ability was cast.
        /// </param>
        public void Use(Ability ability, Vector3 mouseWorldPosition)
        {
            // We must go through all the instances of AbilityBase because a 
            // single GameObject may have more than one if it has multiple 
            // abilities.
            var abilityComponents = GetComponents<AbilityBase>();
            foreach (var abilityComponent in abilityComponents)
            {
                if (abilityComponent.Ability.AbilityName == ability.AbilityName)
                {
                    abilityComponent.Ability = ability;
                }
            }

            RemyAttacking.ability = ability;
            RemyAttacking.attackDirection = mouseWorldPosition;

            if (ability.abilityType == AbilityType.Melee)
            {
                if (ability.cdRemain <= 0) {
                    remyAttacking.MeleeAttack();
                }
            }

            else
            {
                if (ability.cdRemain <= 0) {
                    remyAttacking.MagicAttack(ability);
                }
            }

            if (usableAbilities.Set.ContainsKey(ability.AbilityName))
            {
                ability.use = true;
            }
            else
            {
                Debug.LogError("[" + gameObject.name + ".AbilityUser] Ability '" +
                    ability.AbilityName + "' is not available to " + gameObject.name);
            }
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            remyAttacking = GetComponent <RemyAttacking>();
        }
        #endregion
    }
}
