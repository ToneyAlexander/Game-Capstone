using CCC.GameManagement;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace CCC.Items
{
    /// <summary>
    /// Represents a collection of Items owned by a GameObject.
    /// </summary>
    [CreateAssetMenu(menuName = "Items/Inventory")]
    public sealed class Inventory : ScriptableObject, IJsonSavable
    {
        /// <summary>
        /// The list of Item that this Inventory has in it.
        /// </summary>
        private List<Item> items = new List<Item>();

        /// <summary>
        /// The maximum number of Item that this Inventory can have in it.
        /// </summary>
        [SerializeField]
        private int maxCapacity = 15;

        [SerializeField]
        private string filename;

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

        #region IJsonSavable
        public void Load()
        {
            Debug.Log("Loading Inventory");
            path = System.IO.Path.Combine(Application.persistentDataPath, 
                filename);
            Debug.Log(name + ".dataPath = " + path);

            // Load save data from disk
            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    string jsonString = streamReader.ReadToEnd();
                    Debug.Log("jsonString = " + jsonString);
                    InventoryData data = 
                        JsonUtility.FromJson<InventoryData>(jsonString);
                    items = data.Items;
                }
            }
        }

        public void Save()
        {
            Debug.Log("Saving Inventory");
            string jsonString = 
                JsonUtility.ToJson(InventoryData.ForItems(items));
            Debug.Log("Save jsonString = " + jsonString);

            // dataPath will have already been set by Awake
            using (StreamWriter streamWriter = File.CreateText(path))
            {
                foreach (Item item in items)
                {
                    Debug.Log(item.Name);
                }
                streamWriter.Write(jsonString);
            }
        }
        #endregion

        #region ScriptableObject Messages
        //private void OnDisable()
        //{
        //    Save();
        //}

        private void OnEnable()
        {
            Debug.Log("In Inventory.OnEnable");
            items = new List<Item>();
        }
        #endregion
    }
}
