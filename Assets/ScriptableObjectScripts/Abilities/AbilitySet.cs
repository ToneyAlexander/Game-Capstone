using System.Collections.Generic;
using UnityEngine;

namespace CCC.Abilities
{
    /// <summary>
    /// A wrapper around a HashSet of Ability that can be dragged around in the 
    /// Unity inspector.
    /// </summary>
    [CreateAssetMenu]
    public class AbilitySet : ScriptableObject
    {
        /// <summary>
        /// Get the HashSet of Ability that this AbilitySet is a wrapper for.
        /// </summary>
        /// <value>The actual HashSet of Ability.</value>
        public HashSet<Ability> Set
        {
            get { return set; }
        }

        /// <summary>
        /// The actual HashSet of Ability.
        /// </summary>
        private HashSet<Ability> set = new HashSet<Ability>();
    }
}
