using UnityEngine;

namespace CCC.Items
{
    /// <summary>
    /// Represents an entry in an EquipmentDictionary. This class is needed 
    /// because Unity does not support the serialization of Dictionaries.
    /// </summary>
    [System.Serializable]
    sealed class EquipmentEntry
    {
        /// <summary>
        /// Create a new instance of EquipmentEntry that represents the given 
        /// EquipmentSlot and Item.
        /// </summary>
        /// <returns>
        /// A new EquipmentEntry that represents the given EquipmentSlot and 
        /// Item.
        /// </returns>
        /// <param name="slot">The EquipmentSlot that the Item uses.</param>
        /// <param name="item">The Item that is in the EquipmentSlot.</param>
        public static EquipmentEntry ForSlotWithItem(EquipmentSlot slot, 
            Item item)
        {
            return new EquipmentEntry(slot, item);
        }

        /// <summary>
        /// Gets the EquipmentSlot that this EquipmentEntry's Item occupies.
        /// </summary>
        /// <value>The EquipmentSlot.</value>
        public EquipmentSlot Slot
        {
            get { return slot; }
        }

        /// <summary>
        /// Gets the Item that is in this EquipmentEntry's EquipmentSlot.
        /// </summary>
        /// <value>The Item.</value>
        public Item Item
        {
            get { return item; }
        }

        /// <summary>
        /// The EquipmentSlot that is used.
        /// </summary>
        [SerializeField]
        private EquipmentSlot slot;

        /// <summary>
        /// The Itemt that is in the EquipmentSlot.
        /// </summary>
        [SerializeField]
        private Item item;

        private EquipmentEntry(EquipmentSlot slot, Item item)
        {
            this.slot = slot;
            this.item = item;
        }
    }
}
