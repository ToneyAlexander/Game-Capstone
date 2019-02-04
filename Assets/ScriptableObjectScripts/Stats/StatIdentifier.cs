using UnityEngine;

namespace CCC.Stats
{
    /// <summary>
    /// Encapsulates the internal name of a Stat inside a ScriptableObject that it 
    /// can be dragged around in the Unity Inspector.
    /// </summary>
    [CreateAssetMenu]
    sealed class StatIdentifier : ScriptableObject
    {
        /// <summary>
        /// Get the internal name of this StatIdentifier's Stat.
        /// </summary>
        /// <value>The internal name of the Stat.</value>
        public string InternalStatName
        {
            get { return internalName; }
        }

        /// <summary>
        /// The internal name of the Stat.
        /// </summary>
        [SerializeField]
        private string internalName;
    }
}
