using UnityEngine;
using System.Collections.Generic;

namespace CCC.Items
{
    /// <summary>
    /// The that represents an Inventory. This class is needed so that an 
    /// Inventory can be serialized to JSON.
    /// </summary>
    [System.Serializable]
    public sealed class InventoryData
    {
        /// <summary>
        /// Create a new InventoryData for a given list of Items.
        /// </summary>
        /// <returns>
        /// A new InventoryData that holds the given list of Items.
        /// </returns>
        /// <param name="items">The list of Items.</param>
        public static InventoryData ForItems(List<Item> items)
        {
            return new InventoryData(items);
        }

        /// <summary>
        /// Gets the list of Items that this InventoryData has.
        /// </summary>
        /// <value>The list of Items.</value>
        public List<Item> Items
        {
            get { return items; }
        }

        /// <summary>
        /// The list of Items that this InventoryData has. This field can't be 
        /// readonly because Unity's JsonUtility can't serialize readonly 
        /// fields.
        /// </summary>
        [SerializeField]
        private List<Item> items;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="T:CCC.Items.InventoryData"/> class.
        /// </summary>
        /// <param name="items">The list of Items to store.</param>
        private InventoryData(List<Item> items)
        {
            this.items = items;
        }
    }
}