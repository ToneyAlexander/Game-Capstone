using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace CCC.Items
{
    /// <summary>
    /// A run-time only reference to inventory data that is stored on disk.
    /// </summary>
    [CreateAssetMenu(menuName = "Items/Inventory")]
    public sealed class Inventory : ScriptableObject
    {
        /// <summary>
        /// The maximum number of Item that this Inventory can have in it.
        /// </summary>
        [SerializeField]
        private int maxCapacity = 15;

        /// <summary>
        /// The name of the JSON file that this Inventory references.
        /// </summary>
        [SerializeField]
        private string filename;

        /// <summary>
        /// The actual inventory data that this Inventory references.
        /// </summary>
        private InventoryData data = InventoryData.Null;

        /// <summary>
        /// Gets the path to the file that this Inventory should be saved in.
        /// </summary>
        /// <value>The path to the file.</value>
        public string Path
        {
            get { return path; }
        }

        /// <summary>
        /// The path to the file that this Inventory should be saved in.
        /// </summary>
        private string path;

        /// <summary>
        /// Gets the current capacity of this Inventory.
        /// </summary>
        /// <value>The current capacity.</value>
        public int CurrentCapacity
        {
            get { return data.Items.Count; }
        }

        /// <summary>
        /// Gets the list of Item that this Inventory has.
        /// </summary>
        /// <value>The list of Item.</value>
        public List<Item> Items
        {
            get { return data.Items; }
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
                data.Items.Add(item);
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

            if (data.Items.Contains(item))
            {
                data.Items.Remove(item);
                removedItem = item;
            }

            return removedItem;
        }

        /// <summary>
        /// Load the inventory data from the JSON file that this Inventory 
        /// references.
        /// </summary>
        public void Load()
        {
            path = System.IO.Path.Combine(Application.persistentDataPath, 
                filename);

            // Load save data from disk
            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    string jsonString = streamReader.ReadToEnd();
                    InventoryData loadedData = 
                        JsonUtility.FromJson<InventoryData>(jsonString);
                    data = loadedData;
                }
            }
            else
            {
                data = InventoryData.Null;
            }
        }

        /// <summary>
        /// Save the inventory data to the JSON file that this Inventory 
        /// references.
        /// </summary>
        public void Save()
        {
            string jsonString = JsonUtility.ToJson(data, true);

            using (StreamWriter streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jsonString);
            }

            data = InventoryData.Null;
        }
    }
}
