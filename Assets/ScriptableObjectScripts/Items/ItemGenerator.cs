using UnityEngine;

namespace CCC.Items
{
    /// <summary>
    /// Represents something that can generate Items.
    /// </summary>
    public abstract class ItemGenerator : ScriptableObject
    {
        /// <summary>
        /// Generate new Item.
        /// </summary>
        /// <returns>A newly generated Item.</returns>
        public abstract Item GenerateItem();
    }
}
