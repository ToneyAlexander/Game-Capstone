using UnityEngine;

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

        [SerializeField]
        private AbilitySet usableAbilities;

        /// <summary>
        /// Use the given Ability.
        /// </summary>
        /// <param name="ability">The Ability to use.</param>
        public void Use(Ability ability)
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

        private void Start()
        {
            Debug.Log("Test");
            foreach (Ability ability in usableAbilities.Set)
            {
                Debug.Log(ability.AbilityName);
            }
        }
    }
}
