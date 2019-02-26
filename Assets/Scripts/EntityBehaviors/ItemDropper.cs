using CCC.Items;
using UnityEngine;

namespace CCC.Behaviors
{
    /// <summary>
    /// Represents something that can drop Items.
    /// </summary>
    public interface IItemDropper
    {
        /// <summary>
        /// Drop the given Item.
        /// </summary>
        /// <returns>The Item.</returns>
        void DropItem(Item item, Vector3 position);
    }
}
