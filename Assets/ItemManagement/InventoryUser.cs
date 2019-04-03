using CCC.Items;
using UnityEngine;

namespace CCC.ItemManagement
{
    /// <summary>
    /// Represents a Component that allows a GameObject to have an Inventory.
    /// </summary>
    public class InventoryUser : MonoBehaviour
    {
        public Inventory Inventory
        {
            get { return inventory; }
        }

        [SerializeField]
        private Inventory inventory;
    }
}
