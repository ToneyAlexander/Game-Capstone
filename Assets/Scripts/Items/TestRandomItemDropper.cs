using CCC.Items;
using CCC.Stats;
using UnityEngine;

namespace CCC.Behaviors
{
    /// <summary>
    /// Represents a testing Component that causes its GameObject to drop a randomly
    /// generated Item and automatically places in in a given Inventory and 
    /// EquipmentDictionary.
    /// </summary>
    public class TestRandomItemDropper : MonoBehaviour, IItemDropper
    {
        public void DropItem(Item item, Vector3 position)
        {
            Debug.Log("Dropped " + item.Name + "!");
            foreach (Stat stat in item.Stats)
            {
                Debug.Log(stat.Name + ": " + stat.Value);
            }

            inventory.AddItem(item);
            equipment.EquipItem(item);
            Debug.Log("Equipped " + equipment.Equipment[EquipmentSlot.Weapon].Name);
            Debug.Log(equipment.Equipment[EquipmentSlot.Weapon]);
        }

        /// <summary>
        /// The EquipmentDictionary to put the newly generated Item in.
        /// </summary>
        [SerializeField]
        private EquipmentDictionary equipment;

        /// <summary>
        /// The Iventory to place the newly generated Item in.
        /// </summary>
        [SerializeField]
        private Inventory inventory;
    }
}
