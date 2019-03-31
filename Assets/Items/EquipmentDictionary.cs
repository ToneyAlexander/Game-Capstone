using System.Collections.Generic;
using UnityEngine;

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

        private readonly Dictionary<EquipmentSlot, Item> equipment = 
            new Dictionary<EquipmentSlot, Item>
            {
                {EquipmentSlot.Head, Item.Null},
                {EquipmentSlot.Body, Item.Null},
                {EquipmentSlot.Weapon, Item.Null},
                {EquipmentSlot.Offhand, Item.Null},
                {EquipmentSlot.Ring, Item.Null},
                {EquipmentSlot.Amulet, Item.Null}
            };
    }
}
