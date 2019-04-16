using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CCC.Items
{
    /// <summary>
    /// Represents a set of Items that make up the equipment of a GameObject.
    /// </summary>
    [CreateAssetMenu(menuName = "Items/EquipmentDictionary")]
    public sealed class EquipmentDictionary : ScriptableObject
    {
        /// <summary>
        /// Gets the dictionary of EquipmentSlot to Item of the items that make
        /// up a GameObject's equipment.
        /// </summary>
        /// <value>The dictionary of EquipmentSlot to Item.</value>
        public Dictionary<EquipmentSlot, Item> Equipment
        {
            get { return equipment; }
        }

        /// <summary>
        /// The internal Dictionary that is modified at run-time.
        /// </summary>
        private Dictionary<EquipmentSlot, Item> equipment =
            new Dictionary<EquipmentSlot, Item>
        {
            {EquipmentSlot.Head, Item.Null},
            {EquipmentSlot.Body, Item.Null},
            {EquipmentSlot.Weapon, Item.Null},
            {EquipmentSlot.Offhand, Item.Null},
            {EquipmentSlot.Ring, Item.Null},
            {EquipmentSlot.Amulet, Item.Null}
        };

        /// <summary>
        /// The name of the file to save the EquipmentData created from this 
        /// EquipmentDitionary's equipment.
        /// </summary>
        [SerializeField]
        private string filename;

        [SerializeField]
        private string folderName = "Player";

        /// <summary>
        /// The path to the file on disk that this EquipmentDictionary 
        /// references.
        /// </summary>
        private string path;

        /// <summary>
        /// The actual data on disk that this EquipmentDictionary references.
        /// </summary>
        private EquipmentData data;

        /// <summary>
        /// Equip the given Item.
        /// </summary>
        /// <param name="item">Item.</param>
        public void EquipItem(Item item)
        {
            equipment[item.EquipmentSlot] = item;
        }
        /// <summary>
        /// Disequip the item corresponding to this item's slot.
        /// </summary>
        /// <param name="item"></param>
        public void DisequipItem(Item item)
        {
            equipment[item.EquipmentSlot] = Item.Null;
        }
        public void CheckAndDisequipItem(Item item)
        {
            if (equipment[item.EquipmentSlot] == item)
            {
                equipment[item.EquipmentSlot] = Item.Null;
            }
        }

        /// <summary>
        /// Load the equipment data from the JSON file that this 
        /// EquipmentDictionary references.
        /// </summary>
        public void Load()
        {
            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    string jsonString = streamReader.ReadToEnd();
                    EquipmentData loadedData = 
                        JsonUtility.FromJson<EquipmentData>(jsonString);
                    data = loadedData;

                    // Load the actual equipment
                    foreach (EquipmentEntry entry in loadedData.Entries)
                    {
                        equipment[entry.Slot] = entry.Item;
                    }
                }
            }
        }

        /// <summary>
        /// Save the equipment data to the JSON file that this 
        /// EquipmentDictionary references.
        /// </summary>
        public void Save()
        {
            string jsonString = 
                JsonUtility.ToJson(EquipmentData.ForEquipment(equipment), true);
            var directoryPath =
                Path.Combine(Application.persistentDataPath, folderName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jsonString);
            }
        }

        #region ScriptableObject Messages
        private void OnEnable()
        {
            path = Path.Combine(Application.persistentDataPath, 
                folderName);
            path = Path.Combine(path, filename);
        }
        #endregion
    }
}
