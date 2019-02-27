using System.Collections.Generic;
using UnityEngine;

namespace CCC.Items
{
    /// <summary>
    /// Represents a collection of Items owned by a GameObject.
    /// </summary>
    [CreateAssetMenu]
    public sealed class Inventory : ScriptableObject
    {
        /// <summary>
        /// Gets the current capacity of this Inventory.
        /// </summary>
        /// <value>The current capacity.</value>
        public int CurrentCapacity
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Gets the list of Item that this Inventory has.
        /// </summary>
        /// <value>The list of Item.</value>
        public List<Item> Items
        {
            get { return items; }
        }

        /// <summary>
        /// Gets the maximum capacity of this Inventory.
        /// </summary>
        /// <value>The max capacity.</value>
        public int MaxCapacity
        {
            get { return maxCapacity; }
        }

        /// <summary>
        /// Add the given Item to this Inventory if there is room.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if item was added, <c>false</c> otherwise.
        /// </returns>
        /// <param name="item">The Item to add.</param>
        public bool AddItem(Item item)
        {
            bool wasAdded = false;

            if (CurrentCapacity < maxCapacity)
            {
                items.Add(item);
                wasAdded = true;
            }

            return wasAdded;
        }

        /// <summary>
        /// Remove the given Item from this Inventory if it is in it.
        /// </summary>
        /// <returns>
        /// The Item that was removed if it was in this Inventory. Item.Null 
        /// otherwise.
        /// </returns>
        /// <param name="item">Item.</param>
        public Item RemoveItem(Item item)
        {
            Item removedItem = Item.Null;

            if (items.Contains(item))
            {
                items.Remove(item);
                removedItem = item;
            }

            return removedItem;
        }

        /// <summary>
        /// The list of Item that this Inventory has in it.
        /// </summary>
        private List<Item> items = new List<Item>();

        /// <summary>
        /// The maximum number of Item that this Inventory can have in it.
        /// </summary>
        [SerializeField]
        private int maxCapacity = 15;
    }
}
