﻿using UnityEngine;

namespace CCC.Abilities
{
    /// <summary>
    /// Represents a Component that allows a GameObject to use an Ability.
    /// </summary>
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
            if (usableAbilities.Set.Contains(ability))
            {
                Debug.Log(gameObject.name + " used Ability " + ability.AbilityName);
            }
            else
            {
                Debug.LogError("[" + gameObject.name + ".AbilityUser] Ability '" +
                    ability.AbilityName + "' is not available to " + gameObject.name);
            }
        }

        #region MonoBehaviour Messages
        private void Start()
        {
            foreach (Ability ability in usableAbilities.Set)
            {
                Debug.Log(ability.AbilityName);
            }
        }
        #endregion
    }
}
