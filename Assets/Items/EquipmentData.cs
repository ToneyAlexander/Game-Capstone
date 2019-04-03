using UnityEngine;
using System.Collections.Generic;

namespace CCC.Items
{
    /// <summary>
    /// Represents equipment data that is stored on disk.
    /// </summary>
    [System.Serializable]
    sealed class EquipmentData
    {
        /// <summary>
        /// Create a new instance of EquipmentData for a given dictionary of 
        /// EquipmentSlots to Item.
        /// </summary>
        /// <returns>
        /// A new EquipmentData that represents the equipment data in the 
        /// dictionary.
        /// </returns>
        /// <param name="equipment">
        /// The dictionary of EquipmentSlot to Item to create a new 
        /// EquipmentData from.
        /// </param>
        public static EquipmentData ForEquipment(Dictionary<EquipmentSlot, Item> equipment)
        {
            return new EquipmentData(equipment);
        }

        /// <summary>
        /// Gets the list of EquipmentEntries that this EquipmentData has.
        /// </summary>
        /// <value>The entries.</value>
        public List<EquipmentEntry> Entries
        {
            get { return equipmentEntries; }
        }

        /// <summary>
        /// The list of EquipmentEntries gathered from disk.
        /// </summary>
        [SerializeField]
        private List<EquipmentEntry> equipmentEntries;

        private EquipmentData(Dictionary<EquipmentSlot, Item> equipment)
        {
            equipmentEntries = new List<EquipmentEntry>();

            foreach (KeyValuePair<EquipmentSlot, Item> pair in equipment)
            {
                equipmentEntries.Add(
                    EquipmentEntry.ForSlotWithItem(pair.Key, pair.Value));
            }
        }
    }
}
